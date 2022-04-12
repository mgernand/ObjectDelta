namespace ObjectDelta
{
	using JetBrains.Annotations;

	[PublicAPI]
	public static class DeltaExtensions
	{
		public static ObjectDelta<T> CompareTo<T>(this T oldObject, T newObject) where T : class
		{
			ObjectComparer comparer = new ObjectComparer();
			return comparer.Compare(oldObject, newObject);
		}
	}
}
