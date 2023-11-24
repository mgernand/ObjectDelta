namespace ObjectDelta.Structure.Reflection
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	internal static class TypeExtensions
	{
		private const string ExtractEnumerableGenericTypeMessage =
			"When extracting the generic element type from enumerables, " +
			"a maximum of two generic arguments are supported, which then " +
			"are supposed to belong to KeyValuePair<TKey, TValue>.";

		private static readonly HashSet<Type> extraPrimitiveTypes = new HashSet<Type>
		{
			typeof (string),
			typeof (decimal),
			typeof (DateOnly),
			typeof (TimeOnly),
			typeof (DateTime),
			typeof (DateTimeOffset),
			typeof (TimeSpan),
			typeof (Guid)
		};

		internal static bool IsSimpleType(this Type type)
		{
			return
				type.IsPrimitive(true) ||
				type.IsValueType();
		}

		internal static bool IsEnumerableType(this Type type)
		{
			return type != typeof(string)
				&& !type.IsValueType()
				&& !type.IsPrimitive()
				&& type.IsEnumerable();
		}

		internal static bool IsPrimitive(this Type type, bool includeEnums = false)
		{
			type = type.UnwrapNullableType();
			return type.IsPrimitive || includeEnums && type.IsEnum || TypeExtensions.extraPrimitiveTypes.Contains(type);
		}

		internal static bool IsValueType(this Type type)
		{
			type = type.UnwrapNullableType();
			return type.IsValueType;
		}

		internal static bool IsEnumerable(this Type type)
		{
			return typeof(IEnumerable).IsAssignableFrom(type);
		}

		internal static bool IsNumeric(this Type type)
		{
			type = type.UnwrapNullableType();
			return type == typeof(sbyte) || 
			       type == typeof(byte) || 
			       type == typeof(short) || 
			       type == typeof(ushort) || 
			       type == typeof(int) || 
			       type == typeof(uint) || 
			       type == typeof(long) || 
			       type == typeof(ulong) || 
			       type == typeof(decimal) || 
			       type == typeof(float) || 
			       type == typeof(double);
		}

		internal static bool IsKeyValuePairType(this Type type)
		{
			return
				type.IsGenericType &&
				type.IsValueType &&
				type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
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

		private static Type UnwrapNullableType(this Type type)
		{
			Type underlyingType = Nullable.GetUnderlyingType(type);
			return (object)underlyingType != null ? underlyingType : type;
		}

		private static Type ExtractEnumerableGenericType(Type type)
		{
			Type[] generics = type.GetGenericArguments();

			if(generics.Length == 1)
			{
				return generics[0];
			}

			if(generics.Length == 2 && (typeof(IDictionary).IsAssignableFrom(type) || type.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
			{
				return typeof(KeyValuePair<,>).MakeGenericType(generics[0], generics[1]);
			}

			throw new InvalidOperationException(ExtractEnumerableGenericTypeMessage);
		}
	}
}
