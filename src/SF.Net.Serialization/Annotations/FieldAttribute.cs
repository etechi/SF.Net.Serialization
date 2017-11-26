using System;

namespace SF.Net.Serialization.Annotations
{
    public class FieldAttribute : Attribute
    {
		public string Name { get; }
		public int Order { get;  }
		public string Channel { get;  }
		public int Size { get; }
		public FieldAttribute(int Order,string Name=null,string Channel = null,int Size=0)
		{
			this.Order = Order;
			this.Name = Name;
			this.Channel = Channel;
			this.Size = Size;
		}
    }
}
