﻿#region Arx One Persistence
// Arx One Persistence
// The one who keeps you alive after death
// https://github.com/ArxOne/Persistence
// MIT License
#endregion
namespace ArxOne.Persistence.Utility
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extensions for Type
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the given type is nullable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Gets the original type that was made Nullable.
        /// (this is probably the worst method name I ever wrote)
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static Type GetNullabled(this Type type)
        {
            if (!IsNullable(type))
                return null;
            return type.GetGenericArguments()[0];
        }

        /// <summary>
        /// Enumerates all types from given type to System.Object.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetSelfAndBaseTypes(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }

        /// <summary>
        /// Creates a default instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object Default(this Type type)
        {
            if (type.IsClass)
                return null;
            return Activator.CreateInstance(type);
        }
    }
}
