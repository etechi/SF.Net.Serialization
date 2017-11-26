using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using SF.Net.Serialization.Annotations;
using SF.Net.Serialization.Abstractions;

namespace SF.Net.Serialization
{
	public class PacketDeserializer<TPacketReader, TPacket> : IPacketDeserializer<TPacketReader, TPacket>
	{
		Action<TPacketReader, TPacket> FuncDeserialize { get; }

		public PacketDeserializer(IValueDeserializerProvider<TPacketReader> ValueDeserializerProvider)
		{
			var FieldGroups =
			(from prop in typeof(TPacket).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
			 where prop.CanWrite
			 let field = prop.GetCustomAttribute<FieldAttribute>()
			 where field != null
			 let option = new FieldOption(prop, field)
			 let Deserializer = ValueDeserializerProvider.GetValueDeserializer(option) ?? throw new NotSupportedException($"找不到字段{option}的值反序列化器")
			 group (Deserializer, option) by field.Channel ?? "" into g
			 select (channel: g.Key, fields: g.OrderBy(i => i.option.Order).ToArray())
			).ToArray();

			var argPacket = Expression.Parameter(typeof(TPacket), "package");
			var argReader = Expression.Parameter(typeof(TPacketReader), "Reader");
			FuncDeserialize = Expression.Lambda<Action<TPacketReader, TPacket>>(
				Expression.Block(
					from grp in FieldGroups
					from field in grp.fields
					select
						Expression.Call(
							argPacket,
							field.option.Property.GetSetMethod(),
							Expression.Call(
								Expression.Constant(field.Deserializer),
								field.Deserializer.GetType().GetMethod("Deserialize"),
								argReader,
								Expression.Constant(field.option, typeof(IFieldOption))
								)
							)
						),
				argReader,
				argPacket
				).Compile();
		}


		public void Deserialize(TPacketReader Reader, TPacket Package)
		{
			if (Package == null) throw new ArgumentNullException(nameof(Package));
			if (Reader == null) throw new ArgumentNullException(nameof(Reader));
			FuncDeserialize(Reader, Package);
		}
	}
}
