using SF.Net.Serialization.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace SF.Net.Serialization
{
	
	public class DictPacketWriter<TFieldValueType> :
		Dictionary<string, List<(IFieldOption Field, TFieldValueType Value)>>
	{
		public void Write(TFieldValueType Value, IFieldOption Field)
		{
			var channel = Field.Channel ?? "#default";
			if (!TryGetValue(channel, out var list))
				this[channel] = list = new List<(IFieldOption Field, TFieldValueType Value)>();
			list.Add((Field, Value));
		}
	}
	//public class StringPacketSetting
	//{
	//	public string KeyValueSplitter { get; set; }
	//	public string FieldSplitter { get; set; }
	//	public Func<string,string> Escape { get; set; }
	//	public Func<string, string> Unescape { get; set; }
	//}

	//public interface IValueSerializerProviderBuilder<TPacketReader, TPacketWriter>
	//{
	//	IValueSerializerProviderBuilder<TPacketReader, TPacketWriter> Set<TValue>(
	//		IValueSerializer<TPacketReader, TPacketWriter, TValue> Serializer,
	//		string Channel,
	//		string Field
	//		);
	//	IValueSerializerProvider<TPacketReader, TPacketWriter> Build();
	//}
	//public static class IValueSerializerProviderBuilderExtension
	//{
	//	class FuncValueSerializer<TPacketReader, TPacketWriter, TValue> : IValueSerializer<TPacketReader,TPacketWriter,TValue>
	//	{
	//		public Action<TValue, TPacketWriter, IFieldOption> FuncSerialize { get; set; }
	//		public Func<TPacketReader , IFieldOption , TValue> FuncDeserialize { get; set; }

	//		public TValue Deserialize(TPacketReader Reader, IFieldOption Field) => FuncDeserialize(Reader, Field);
	//		public void Serialize(TValue Value, TPacketWriter Writer, IFieldOption Field) => FuncSerialize(Value, Writer, Field);
	//	}
	//	public static IValueSerializerProviderBuilder<TPacketReader, TPacketWriter> Set1<TPacketReader, TPacketWriter,TValue>(
	//		this IValueSerializerProviderBuilder<TPacketReader, TPacketWriter> Builder,
	//		Func<TPacketReader, IFieldOption, TValue> Deserialize,
	//		Action<TValue , TPacketWriter , IFieldOption > Serialize,
	//		string Channel=null,
	//		string Field=null
	//		)
	//	{
	//		Builder.Set(new FuncValueSerializer<TPacketReader, TPacketWriter, TValue>
	//			{
	//				FuncSerialize = Serialize,
	//				FuncDeserialize = Deserialize
	//			},
	//			Channel,
	//			Field
	//		);
	//		return Builder;
	//	}
	//}
	//public interface IValueSerializerProvider<TPacketWriter>
	//{
	//	IValueSerializer GetValueSerializer(IFieldOption Field);
	//}
	//public interface IValueDeserializerProvider<TPacketReader>
	//{
	//	IValueDeserializer GetValueDeserializer(IFieldOption Field);
	//}
	
	//public class StringPacketReader : IPacketReader
	//{

	//}

	//public class StringPacketWriter : PacketWriter<string>
	//{
	//	StringPacketSetting Setting { get; }
	//	public StringPacketWriter(IValueSerializer<string> Serializer, StringPacketSetting Setting) : base(Serializer)
	//	{
	//		IValueSerializerProviderBuilder<StringPacketReader, StringPacketWriter> build=null;
	//		build.Set1(
	//			(r, f) => 0,
	//			(s, w, f) => w["Asd"] = null
	//			);
	//		var p = build.Build();
	//		p.GetValueSerializer<>
	//		this.Setting = Setting;
	//	}

	//	public virtual string GetPacket()
	//	{
	//		return string.Join(
	//			Setting.FieldSplitter,
	//			this["#default"].Select(p => 
	//				Setting.Escape(p.Field.Name) + 
	//				Setting.KeyValueSplitter + 
	//				Setting.Escape(p.Value ?? "")
	//				)
	//			);

	//	}
	//}
	//public class QueryStringPacketSetting : StringPacketSetting
	//{
	//	public static QueryStringPacketSetting Instance { get; } = new QueryStringPacketSetting();
	//	public QueryStringPacketSetting()
	//	{
	//		KeyValueSplitter = "=";
	//		FieldSplitter = "&";
	//		Escape = Uri.EscapeDataString;
	//		Unescape = Uri.UnescapeDataString;
	//	}
	//}
	//public class QueryStringPacketWriter : StringPacketWriter
	//{
	//	public QueryStringPacketWriter(IValueSerializer<string> Serializer) : base(Serializer, QueryStringPacketSetting.Instance)
	//	{
	//	}
	//}
}
