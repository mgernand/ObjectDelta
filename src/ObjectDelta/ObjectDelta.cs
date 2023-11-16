namespace ObjectDelta
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using System.Text;
	using JetBrains.Annotations;

	/// <summary>
	///     Provides the delta of two instances of the same type type.
	/// </summary>
	[PublicAPI]
	[DebuggerDisplay("Type={Type.Name}, ChangeCount={PropertyDeltas.Length}")]
	public sealed class ObjectDelta
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ObjectDelta" /> type.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="oldObject"></param>
		/// <param name="newObject"></param>
		/// <param name="propertyDeltas"></param>
		internal ObjectDelta(Type type, object oldObject, object newObject, PropertyDelta[] propertyDeltas)
		{
			ArgumentNullException.ThrowIfNull(type);
			ArgumentNullException.ThrowIfNull(oldObject);
			ArgumentNullException.ThrowIfNull(newObject);

			propertyDeltas ??= Array.Empty<PropertyDelta>();

			this.Type = type;
			this.OldObject = oldObject;
			this.NewObject = newObject;
			this.PropertyDeltas = propertyDeltas;
		}

		/// <summary>
		///     Gets the type that is compared.
		/// </summary>
		public Type Type { get; }

		/// <summary>
		///     Gets the initial instance.
		/// </summary>
		public object OldObject { get; }

		/// <summary>
		///     Gets the changed instance.
		/// </summary>
		public object NewObject { get; }

		/// <summary>
		///     Gets the changed properties.
		/// </summary>
		public PropertyDelta[] PropertyDeltas { get; }

		/// <summary>
		///     Flag, indicating if the comparison found changes.
		/// </summary>
		public bool HasChanges => this.PropertyDeltas.Length != 0;

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

    /// <summary>
    ///     Provides the delta of two instances of the same type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [PublicAPI]
	[DebuggerDisplay("Type={Type.Name}, ChangeCount={PropertyDeltas.Length}")]
	public sealed class ObjectDelta<T>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ObjectDelta" /> type.
		/// </summary>
		/// <param name="oldObject"></param>
		/// <param name="newObject"></param>
		/// <param name="propertyDeltas"></param>
		internal ObjectDelta(T oldObject, T newObject, PropertyDelta[] propertyDeltas)
		{
			ArgumentNullException.ThrowIfNull(oldObject);
			ArgumentNullException.ThrowIfNull(newObject);

			propertyDeltas ??= Array.Empty<PropertyDelta>();

			this.Type = typeof(T);
			this.OldObject = oldObject;
			this.NewObject = newObject;
			this.PropertyDeltas = propertyDeltas;
		}

		/// <summary>
		///     Gets the type that is compared.
		/// </summary>
		public Type Type { get; }

		/// <summary>
		///     Gets the initial instance.
		/// </summary>
		public object OldObject { get; }

		/// <summary>
		///     Gets the changed instance.
		/// </summary>
		public object NewObject { get; }

		/// <summary>
		///     Gets the changed properties.
		/// </summary>
		public PropertyDelta[] PropertyDeltas { get; }

		/// <summary>
		///     Flag, indicating if the comparison found changes.
		/// </summary>
		public bool HasChanges => this.PropertyDeltas.Length != 0;

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
