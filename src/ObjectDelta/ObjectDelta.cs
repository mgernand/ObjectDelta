namespace ObjectDelta
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using System.Text;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     Provides the delta of two instances od type <see cref="T" />.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	[DebuggerDisplay("ObjectType={Type.Name}, ChangeCount={PropertyDeltas.Length}")]
	public sealed class ObjectDelta<T>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ObjectDelta{T}" /> type.
		/// </summary>
		/// <param name="oldObject"></param>
		/// <param name="newObject"></param>
		/// <param name="propertyDeltas"></param>
		internal ObjectDelta(T oldObject, T newObject, PropertyDelta[] propertyDeltas)
		{
			Guard.Against.Null(oldObject, nameof(oldObject));
			Guard.Against.Null(newObject, nameof(newObject));

			propertyDeltas ??= Array.Empty<PropertyDelta>();

			this.OldObject = oldObject;
			this.NewObject = newObject;
			this.PropertyDeltas = propertyDeltas;
		}

		/// <summary>
		///     Gets the type that is compared.
		/// </summary>
		public Type Type => typeof(T);

		/// <summary>
		///     Gets the initial instance.
		/// </summary>
		public T OldObject { get; }

		/// <summary>
		///     Gets the changed instance.
		/// </summary>
		public T NewObject { get; }

		/// <summary>
		///     Gets the changed properties.
		/// </summary>
		public PropertyDelta[] PropertyDeltas { get; }

		/// <summary>
		///     Flag, indicating if the comparison found changes.
		/// </summary>
		public bool HasChanges => this.PropertyDeltas.Any();

		/// <inheritdoc />
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			foreach(PropertyDelta propertyDelta in this.PropertyDeltas)
			{
				builder.AppendLine(propertyDelta.ToString());
			}

			return builder.ToString();
		}
	}
}
