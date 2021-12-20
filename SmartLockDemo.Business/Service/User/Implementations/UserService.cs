using FluentValidation;
using SmartLockDemo.Business.Utilities;
using SmartLockDemo.Infrastructure.Utilities;
using System.Collections.Generic;

namespace SmartLockDemo.Business.Service.User
{
    /// <summary>
    /// Default implementation of IUserService service
    /// </summary>
    internal class UserService : IUserService
    {
        private readonly Data.IUnitOfWork _unitOfWork;
        private readonly IValidatorAccessor _validatorAccessor;
        private readonly IEncryptionUtilities _encryptionUtilities;

        public UserService(Data.IUnitOfWork unitOfWork, IValidatorAccessor validatorAccessor, IEncryptionUtilities encryptionUtilities)
        {
            _unitOfWork = unitOfWork;
            _validatorAccessor = validatorAccessor;
            _encryptionUtilities = encryptionUtilities;
        }

        public UserCreationResult CreateUser(UserCreationRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.UserCreationRequest.ValidateWithExceptionOption(request);

            _unitOfWork.UserRepository.Add(new Data.Entities.User
            {
                Email = request.Email,
                HashedPassword = _encryptionUtilities.Hash(request.Password),
                Role = (byte)Role.User
            });
            _unitOfWork.SaveChanges();

            return new UserCreationResult(true);
        }

        public DoorAccessControlResult CheckDoorAccess(DoorAccessControlRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.DoorAccessControlRequest.ValidateWithExceptionOption(request);

            return new DoorAccessControlResult
            {
                IsUserAuthorized = _unitOfWork.UserDoorAccessRepository.CheckThatUserHasAccessTheDoor(request.UserId, request.DoorId)
            };
        }

        public UserUpdateResult UpdateUser(UserUpdateRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.UserUpdateRequest.ValidateWithExceptionOption(request);

            // TO-DO: Change with a mapper!
            _unitOfWork.UserRepository.Update(new Data.Entities.User
            {
                Id = request.Id,
                Email = request.Email,
                HashedPassword = !string.IsNullOrWhiteSpace(request.Password)
                    ? _encryptionUtilities.Hash(request.Password)
                    : null
            });
            _unitOfWork.SaveChanges();

            return new UserUpdateResult(true);
        }

        public LogInResult LogIn(LogInRequest request)
        {
            if (request is null)
                throw new ValidationException("Request cannot be null!");
            _validatorAccessor.LogInRequest.ValidateWithExceptionOption(request);

            Data.Entities.User userWhoTriesToLogin = _unitOfWork.UserRepository.Get(user => user.Email == request.Email);
            if (!_encryptionUtilities.ValidateHashedValue(request.Password, userWhoTriesToLogin.HashedPassword))
                return new LogInResult(false, null, "Wrong Password!");

            string currentToken = _encryptionUtilities.CreateBearerToken(new BearerTokenCreationRequest
            {
                Id = userWhoTriesToLogin.Id,
                Email = userWhoTriesToLogin.Email,
                Role = ((Role)userWhoTriesToLogin.Role).ToString()
            });

            userWhoTriesToLogin.AuthorizationToken = currentToken;
            _unitOfWork.UserRepository.Update(userWhoTriesToLogin);
            _unitOfWork.SaveChanges();

            return new LogInResult(true, currentToken);
        }

        public bool CheckIfUserIsAdmin(int userIdToCheck)
        {
            if (userIdToCheck <= 0)
                return false;
            return _unitOfWork.UserRepository.Get(user =>
                user.Id == userIdToCheck &&
                user.Role == (byte)Role.Admin) != null;
        }

        public List<User> GetAllUsers()
            => _unitOfWork.UserRepository.GetList(user => true).MapTo<List<User>>();
    }
}
