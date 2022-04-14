namespace ObjectStructure.Reflection
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;

	internal static class TypeExtensions
	{
		private const string ExtractEnumerableGenericTypeMessage =
			"When extracting the generic element type from enumerables, " +
			"a maximum of two generic arguments are supported, which then " +
			"are supposed to belong to KeyValuePair<TKey, TValue>.";

		internal static bool IsSimpleType(this Type type)
		{
			return
				type.IsPrimitive(includeEnums: true) ||
				type.IsValueType;
		}

		internal static bool IsEnumerableType(this Type type)
		{
			return (type != typeof(string))
				&& !type.IsValueType
				&& !type.IsPrimitive
				&& type.IsEnumerable();
		}

		internal static bool IsKeyValuePairType(this Type type)
		{
			return
				type.IsGenericType &&
				type.IsValueType &&
				(type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>));
		}

		internal static Type GetEnumerableElementType(this Type type)
		{
			Type elementType = type.IsGenericType ? ExtractEnumerableGenericType(type) : type.GetElementType();
			if(elementType != null)
			{
				return elementType;
			}

			if(type.BaseType.IsEnumerableType())
			{
				elementType = type.BaseType.GetEnumerableElementType();
			}

			return elementType;
		}

		private static Type ExtractEnumerableGenericType(Type type)
		{
			Type[] generics = type.GetGenericArguments();

			if(generics.Length == 1)
			{
				return generics[0];
			}

			if((generics.Length == 2) && (typeof(IDictionary).IsAssignableFrom(type) || (type.GetGenericTypeDefinition() == typeof(IDictionary<,>))))
			{
				return typeof(KeyValuePair<,>).MakeGenericType(generics[0], generics[1]);
			}

			throw new InvalidOperationException(ExtractEnumerableGenericTypeMessage);
		}
	}
}
