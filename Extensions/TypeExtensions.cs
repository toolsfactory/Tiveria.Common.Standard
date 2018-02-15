using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Tiveria.Common.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsConcreteType(this Type source)
        {
            if (source == null)
            {
                return false;
            }

            bool isConcreteType = !source.IsAbstract && !source.IsInterface;

            isConcreteType &= !source.IsValueType;
            isConcreteType &= source != typeof(string);
            isConcreteType &= !IsFactoryType(source);

            return isConcreteType;
        }

        public static bool IsFactoryType(this Type source)
        {
            if (source == null || !source.IsGenericType)
            {
                return false;
            }

            Type genericTypeDefinition = source.GetGenericTypeDefinition();

            return genericTypeDefinition == typeof(Func<>)
                   || genericTypeDefinition == typeof(Func<,>)
                   || genericTypeDefinition == typeof(Func<,,>)
                   || genericTypeDefinition == typeof(Func<,,,>)
                   || genericTypeDefinition == typeof(Func<,,,,>);
        }

        public static bool IsGenericEnumerable(this Type source)
        {
            if (source == null || !source.IsGenericType)
            {
                return false;
            }

            var typeArguments = source.GetGenericArguments();

            if (typeArguments.Length > 1)
            {
                return false;
            }

            return typeof(IEnumerable<>).MakeGenericType(typeArguments).IsAssignableFrom(source);
        }

        public static bool Implements<I>(this Type type, I intfce) where I : class
        {
            if ((!(intfce is Type)) || !(intfce as Type).IsInterface)
                throw new ArgumentException("Only interfaces can be 'implemented'.");

            return (intfce as Type).IsAssignableFrom(type);
        }

        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }

        public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type, string name)
        {
            return type.GetMethods().Where(m => m.Name.Equals(name));
        }

    }
}