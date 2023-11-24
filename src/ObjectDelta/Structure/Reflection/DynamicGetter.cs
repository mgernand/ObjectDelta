namespace ObjectDelta.Structure.Reflection
{
	/// <summary>
	///     A specialized delegate that is used in efficiently retrieving property values using reflection.
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	internal delegate object DynamicGetter(object obj);
}
