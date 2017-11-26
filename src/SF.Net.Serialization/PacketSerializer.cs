using System;
using SF.Net.Serialization.Abstractions;
using System.Linq;
using System.Reflection;
using SF.Net.Serialization.Annotations;
using System.Linq.Expressions;

namespace SF.Net.Serialization
{
	public class PacketSerializer<TPacketWriter, TPacket> : IPacketSerializer<TPacketWriter,TPacket>
	{
		Action<TPacketWriter, TPacket> FuncSerialize { get; }

		public PacketSerializer(IValueSerializerProvider<TPacketWriter> ValueSerializerProvider)
		{
			var FieldGroups =
			(from prop in typeof(TPacket).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
			 where prop.CanRead
			 let field = prop.GetCustomAttribute<FieldAttribute>()
			 where field != null
			 let option = new FieldOption(prop, field)
			 let serializer = ValueSerializerProvider.GetValueSerializer(option) ?? throw new NotSupportedException($"找不到字段{option}的值序列化器")
			 group (serializer,option) by field.Channel ?? "" into g
			 select (channel:g.Key,fields: g.OrderBy(i => i.option.Order).ToArray())
			).ToArray();

			var argPacket = Expression.Parameter(typeof(TPacket), "package");
			var argWriter = Expression.Parameter(typeof(TPacketWriter), "writer");
			FuncSerialize = Expression.Lambda<Action<TPacketWriter, TPacket>>(
				Expression.Block(
					from grp in FieldGroups
					from field in grp.fields
					select
						Expression.Call(
							Expression.Constant(field.serializer),
							field.serializer.GetType().GetMethod("Serialize"),
							Expression.Property(argPacket, field.option.Property),
							argWriter,
							Expression.Constant(field.option,typeof(IFieldOption))
							)
						),
				argWriter,
				argPacket
				).Compile();
		}
		

		public void Serialize(TPacketWriter Writer, TPacket Package)
		{
			if (Package == null) throw new ArgumentNullException(nameof(Package));
			if (Writer == null) throw new ArgumentNullException(nameof(Writer));
			FuncSerialize(Writer, Package);
		}
	}
}
