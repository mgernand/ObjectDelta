namespace ObjectStructure.Reflection
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Reflection.Emit;

	internal static class DynamicPropertyFactory
	{
		private static readonly Type ObjectType = typeof(object);
		private static readonly Type IlGetterType = typeof(Func<object, object>);

		public static DynamicGetter GetterFor(PropertyInfo p)
		{
			if(p.DeclaringType.IsKeyValuePairType())
			{
				return new DynamicGetter(CreateLambdaGetter(p.DeclaringType, p));
			}

			return new DynamicGetter(CreateIlGetter(p));
		}

		private static Func<object, object> CreateLambdaGetter(Type type, PropertyInfo property)
		{
			ParameterExpression objExpr = Expression.Parameter(ObjectType, "theItem");
			UnaryExpression castedObjExpr = Expression.Convert(objExpr, type);

			MemberExpression p = Expression.Property(castedObjExpr, property);
			UnaryExpression castedProp = Expression.Convert(p, ObjectType);

			Expression<Func<object, object>> lambda = Expression.Lambda<Func<object, object>>(castedProp, objExpr);

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
			LocalBuilder z = generator.DeclareLocal(ObjectType); //Prop val as obj

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

			return (Func<object, object>)getter.CreateDelegate(IlGetterType);
		}

		private static DynamicMethod CreateDynamicGetMethod(PropertyInfo propertyInfo)
		{
			Type[] args = { ObjectType };
			string name = $"_{propertyInfo.DeclaringType.Name}_Get{propertyInfo.Name}_";
			Type returnType = ObjectType;

			return !propertyInfo.DeclaringType.GetTypeInfo().IsInterface
				? new DynamicMethod(
					name,
					returnType,
					args,
					propertyInfo.DeclaringType,
					true)
				: new DynamicMethod(
					name,
					returnType,
					args,
					propertyInfo.Module,
					true);
		}
	}
}
