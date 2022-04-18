namespace ObjectDelta
{
	using JetBrains.Annotations;

	/// <summary>
	///     Extensions for object types.
	/// </summary>
	[PublicAPI]
	public static class DeltaExtensions
	{
		/// <summary>
		///     Compares the given instances and creates the delta between them.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="oldObject"></param>
		/// <param name="newObject"></param>
		/// <returns></returns>
		public static ObjectDelta<T> CompareTo<T>(this T oldObject, T newObject) where T : class
		{
			return ObjectComparer.Compare(oldObject, newObject);
		}

		/// <summary>
		///     Compares the given instances and creates the delta between them.
		/// </summary>
		/// <param name="oldObject"></param>
		/// <param name="newObject"></param>
		/// <returns></returns>
		public static ObjectDelta CompareTo(this object oldObject, object newObject)
		{
			return ObjectComparer.Compare(oldObject, newObject);
		}
	}
}
