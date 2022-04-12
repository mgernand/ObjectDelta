namespace ObjectDelta
{
	using System;
	using System.Diagnostics;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	[DebuggerDisplay("Name={Name}, Path={Path}, OldValue={OldValue}, NewValue={NewValue}")]
	public sealed class PropertyDelta
	{
		public PropertyDelta(Type dataType, string name, string path, object oldValue, object newValue)
		{
			this.DataType = Guard.Against.Null(dataType, nameof(dataType));
			this.Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
			this.Path = Guard.Against.NullOrWhiteSpace(path, nameof(path));
			this.OldValue = Guard.Against.Null(oldValue, nameof(oldValue));
			this.NewValue = Guard.Against.Null(newValue, nameof(newValue));
		}

		public Type DataType { get; }

		public string Name { get; }

		public string Path { get; }

		public object OldValue { get; }

		public object NewValue { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return @$"DataType={this.DataType.Name}, Name={this.Name}, Path={this.Path}, OldValue={this.OldValue}, NewValue={this.NewValue}";
		}
	}
}
