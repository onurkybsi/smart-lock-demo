# SmartLockDemo.Infrastructure

It provides business rule-independent services that all other layers can use.

## Some of Services

`IEncryptionUtilities`

- _**Hash(string): string**_
  - _Creates hashed form of given value using given salt in the module context_
- _**ValidateHashedValue(string, string): bool**_
  - _Validates given value whether hashed by salt given in the module context_
- _**CreateBearerToken(): string**_
  - _Creates bearer token for a user of a system using the secret key given in the module context_
- _**CreateBearerToken(BearerTokenCreationRequest): string**_
  - _Creates bearer token for a user of a system using the secret key given in the module context_

`ConfigurationUtilities`
- _**BuildConfiguration(string, string): IConfiguration**_
  - _Builds a configuration by compose appsetting.json files and environment variables_
