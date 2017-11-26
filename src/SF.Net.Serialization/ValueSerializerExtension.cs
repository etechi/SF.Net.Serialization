using SF.Net.Serialization.Abstractions;
using System.Reflection;
using SF.Net.Serialization.Annotations;
using System;

namespace SF.Net.Serialization
{
	public static class ValueSerializerExtension
	{
		class FuncValueSerializer<TPacketWriter, TValue> : IValueSerializer<TPacketWriter, TValue>
		{
			public Action<TValue, TPacketWriter, IFieldOption> FuncSerialize { get; set; }
			public void Serialize(TValue Value, TPacketWriter Writer, IFieldOption Field) => FuncSerialize(Value, Writer, Field);
		}
		
		public static IValueSerializerProviderBuilder<TPacketWriter> Set<TPacketWriter, TValue>(
			this IValueSerializerProviderBuilder<TPacketWriter> Builder,
			Action<TValue, TPacketWriter, IFieldOption> Serialize,
			string Channel = null,
			string Field = null
			)
		{
			Builder.Set(
				new FuncValueSerializer<TPacketWriter, TValue> { FuncSerialize = Serialize },
				Channel,
				Field
			);
			return Builder;
		}

		public static IValueSerializerProviderBuilder<TPacketWriter> Set<TPacketWriter, TValue,TResult>(
			this IValueSerializerProviderBuilder<TPacketWriter> Builder,
			Func<TValue, IFieldOption,TResult> Serialize,
			string Channel = null,
			string Field = null
			) where TPacketWriter:DictPacketWriter<TResult>
		{
			Builder.Set<TPacketWriter,TValue>(
				(v,w,f)=>w.Write(Serialize(v,f),f),
				Channel,
				Field
			);
			return Builder;
		}

		
	}
}
