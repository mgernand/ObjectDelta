namespace ObjectDelta
{
	using System;
	using System.Diagnostics;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	/// </summary>
	[PublicAPI]
	[DebuggerDisplay("Path={Path} ({OldValue} => {NewValue})")]
	public sealed class PropertyDelta
	{
		/// <summary>
		///     Creates a new instance of the <see cref="PropertyDelta" /> type.
		/// </summary>
		/// <param name="dataType"></param>
		/// <param name="name"></param>
		/// <param name="path"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		internal PropertyDelta(Type dataType, string name, string path, object oldValue, object newValue)
		{
			this.Type = Guard.Against.Null(dataType, nameof(dataType));
			this.Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
			this.Path = Guard.Against.NullOrWhiteSpace(path, nameof(path));

			this.OldValue = oldValue;
			this.NewValue = newValue;
		}

		/// <summary>
		///     Gets the type of the property.
		/// </summary>
		public Type Type { get; }

		/// <summary>
		///     Gets the name of the property.
		/// </summary>
		public string Name { get; }

		/// <summary>
		///     Gets the deep path to the property value.
		/// </summary>
		public string Path { get; }

		/// <summary>
		///     Gets the initial value of the property.
		/// </summary>
		public object OldValue { get; }

		/// <summary>
		///     Gets the changed value of the property.
		/// </summary>
		public object NewValue { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return @$"{this.Path} ({GetValueDisplayString(this.OldValue)} => {GetValueDisplayString(this.NewValue)})";
		}

		private static string GetValueDisplayString(object obj)
		{
			if(obj is null)
			{
				return "null";
			}

			return obj.ToString();
		}
	}
}
