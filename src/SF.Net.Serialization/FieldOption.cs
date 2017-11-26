using SF.Net.Serialization.Abstractions;
using System.Reflection;
using SF.Net.Serialization.Annotations;

namespace SF.Net.Serialization
{
	class FieldOption : IFieldOption
	{
		public FieldOption(PropertyInfo Property,FieldAttribute Field)
		{
			this.Property = Property;
			this.Name = Field.Name ?? Property.Name;
			this.Order = Field.Order;
			this.Size = Field.Size;
			this.Channel = Field.Channel;
		}
		public PropertyInfo Property { get; }

		public int Order { get; }
		public string Name { get; }

		public int Size { get; }

		public string Channel { get; }
	}
}
