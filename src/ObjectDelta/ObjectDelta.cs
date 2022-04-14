namespace ObjectDelta
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using System.Text;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	[DebuggerDisplay("ObjectType={ObjectType.Name}, ChangeCount={PropertyDeltas.Length}")]
	public sealed class ObjectDelta<T>
	{
		public ObjectDelta(T oldObject, T newObject, PropertyDelta[] propertyDeltas)
		{
			Guard.Against.Null(oldObject, nameof(oldObject));
			Guard.Against.Null(newObject, nameof(newObject));

			propertyDeltas ??= Array.Empty<PropertyDelta>();

			this.OldObject = oldObject;
			this.NewObject = newObject;
			this.PropertyDeltas = propertyDeltas;
		}

		public Type ObjectType => typeof(T);

		public T OldObject { get; }

		public T NewObject { get; }

		public PropertyDelta[] PropertyDeltas { get; }

		public bool HasChanges => this.PropertyDeltas.Any();

		/// <inheritdoc />
		public override string ToString()
		{
			if(this.HasChanges)
			{
				StringBuilder builder = new StringBuilder();

				foreach(PropertyDelta propertyDelta in this.PropertyDeltas)
				{
					builder
						.Append("\t")
						.Append(propertyDelta)
						.AppendLine(",");
				}

				string propertyDeltasString = builder.ToString().TrimEnd().TrimEnd(',');
				return $"[{Environment.NewLine}{propertyDeltasString}{Environment.NewLine}]";
			}

			return "[]";
		}
	}
}
