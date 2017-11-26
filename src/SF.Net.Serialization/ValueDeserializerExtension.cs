using SF.Net.Serialization.Abstractions;
using System.Reflection;
using SF.Net.Serialization.Annotations;
using System;

namespace SF.Net.Serialization
{
	public static class ValueDeserializerExtension
	{
		class FuncValueDeserializer<TPacketReader, TValue> : IValueDeserializer<TPacketReader, TValue>
		{
			public Func<TPacketReader, IFieldOption, TValue> FuncDeserialize { get; set; }
			public TValue Deserialize(TPacketReader Reader, IFieldOption Field) => FuncDeserialize(Reader, Field);
		}
		
		public static IValueDeserializerProviderBuilder<TPacketReader> Set<TPacketReader, TValue>(
			this IValueDeserializerProviderBuilder<TPacketReader> Builder,
			Func<TPacketReader, IFieldOption, TValue> Deserialize,
			string Channel = null,
			string Field = null
			)
		{
			Builder.Set(
				new FuncValueDeserializer<TPacketReader, TValue> { FuncDeserialize = Deserialize },
				Channel,
				Field
			);
			return Builder;
		}

		public static IValueDeserializerProviderBuilder<TPacketReader> Set<TPacketReader, TValue,TResult>(
			this IValueDeserializerProviderBuilder<TPacketReader> Builder,
			Func<TResult, IFieldOption, TValue> Deserialize,
			string Channel = null,
			string Field = null
			) where TPacketReader:DictPacketReader<TResult>
		{
			Builder.Set(
				(r,f)=> Deserialize(r.Read(f),f),
				Channel,
				Field
			);
			return Builder;
		}

		
	}
}
