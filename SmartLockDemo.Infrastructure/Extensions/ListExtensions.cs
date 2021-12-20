using System;
using System.Collections.Generic;

namespace SmartLockDemo.Infrastructure.Extensions
{
    /// <summary>
    /// Contains utility List extensions
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Adds a new value to list if given condition is satisfied
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="list">List that will be added</param>
        /// <param name="conditionResolver">Condition resolver function</param>
        /// <param name="valueToAdd">Value to add</param>
        /// <returns>Same list instance with new value or without it</returns>
        /// <exception cref="ArgumentNullException">It is thrown if given list is null</exception>
        public static List<T> AddIfConditionSatisfied<T>(this List<T> list, Func<bool> conditionResolver,
            T valueToAdd)
        {
            if (conditionResolver == null)
                throw new ArgumentNullException(nameof(conditionResolver));
            if (!conditionResolver.Invoke())
                return list;

            list.Add(valueToAdd);

            return list;
        }
    }
}
