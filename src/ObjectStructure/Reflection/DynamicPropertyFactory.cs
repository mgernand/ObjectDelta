namespace ObjectStructure.Reflection
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Reflection.Emit;
	using Fluxera.Guards;

	internal static class DynamicPropertyFactory
	{
		private static readonly Type objectType = typeof(object);
		private static readonly Type ilGetterType = typeof(Func<object, object>);

		public static DynamicGetter GetterFor(PropertyInfo propertyInfo)
		{
			Guard.Against.Null(propertyInfo, nameof(propertyInfo));

			if(propertyInfo.DeclaringType.IsKeyValuePairType())
			{
				return new DynamicGetter(CreateLambdaGetter(propertyInfo.DeclaringType, propertyInfo));
			}

			return new DynamicGetter(CreateIlGetter(propertyInfo));
		}

		private static Func<object, object> CreateLambdaGetter(Type type, PropertyInfo property)
		{
			ParameterExpression objExpr = Expression.Parameter(objectType, "x");
			UnaryExpression castObjExpr = Expression.Convert(objExpr, type);

			MemberExpression p = Expression.Property(castObjExpr, property);
			UnaryExpression castProp = Expression.Convert(p, objectType);

			Expression<Func<object, object>> lambda = Expression.Lambda<Func<object, object>>(castProp, objExpr);

			return lambda.Compile();
		}

		private static Func<object, object> CreateIlGetter(PropertyInfo propertyInfo)
		{
			MethodInfo propGetMethod = propertyInfo.GetGetMethod(true);
			if(propGetMethod == null)
			{
				return null;
			}

			DynamicMethod getter = CreateDynamicGetMethod(propertyInfo);
			ILGenerator generator = getter.GetILGenerator();

			LocalBuilder x = generator.DeclareLocal(propertyInfo.DeclaringType); //Arg
			LocalBuilder y = generator.DeclareLocal(propertyInfo.PropertyType); //Prop val
			LocalBuilder z = generator.DeclareLocal(objectType); //Prop val as obj

			generator.Emit(OpCodes.Ldarg_0);
			generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
			generator.Emit(OpCodes.Stloc, x);

			generator.Emit(OpCodes.Ldloc, x);
			generator.EmitCall(OpCodes.Callvirt, propGetMethod, null);
			generator.Emit(OpCodes.Stloc, y);

			generator.Emit(OpCodes.Ldloc, y);

			if(!propertyInfo.PropertyType.GetTypeInfo().IsClass)
			{
				generator.Emit(OpCodes.Box, propertyInfo.PropertyType);
				generator.Emit(OpCodes.Stloc, z);
				generator.Emit(OpCodes.Ldloc, z);
			}

			generator.Emit(OpCodes.Ret);

			return (Func<object, object>)getter.CreateDelegate(ilGetterType);
		}

		private static DynamicMethod CreateDynamicGetMethod(MemberInfo memberInfo)
		{
			Type declaringType = memberInfo.DeclaringType;

			Type[] args = { objectType };
			string name = $"_{declaringType.Name}_Get{memberInfo.Name}_";
			Type returnType = objectType;

			return !declaringType.IsInterface
				? new DynamicMethod(name, returnType, args, declaringType, true)
				: new DynamicMethod(name, returnType, args, memberInfo.Module, true);
		}
	}
}
