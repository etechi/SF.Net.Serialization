using SF.Net.Serialization.Abstractions;
using System.Reflection;
using SF.Net.Serialization.Annotations;
using System;

namespace SF.Net.Serialization.StringPackets
{
	public static class StringValueDeserializer
	{
		public static IValueDeserializerProviderBuilder<TPacketReader> SetToString<TPacketReader, TValue>(
		this IValueDeserializerProviderBuilder<TPacketReader> Builder,
			Func<string,TValue> Convert,
			string Format = null,
			string Channel = null,
			string Field = null
		) where TPacketReader : DictPacketReader<string>
		{
			if (Format == null)
				Builder.Set<TPacketReader, TValue, string>((r, f) => r.Read<TValue> r.re.ToString(), Channel, Field);
			else
				Builder.Set<TPacketReader, TValue, string>((r, f) => string.Format(Format, v), Channel, Field);
			return Builder;
		}

		public static IValueDeserializerProviderBuilder<TPacketReader> SetStringValueDeserializers<TPacketReader>(
			this IValueDeserializerProviderBuilder<TPacketReader> Builder
			) where TPacketReader : DictPacketReader<string>
		{
			Builder.Set((r, f) => sbyte.Parse(r.Read(f)));
			Builder.SetToString<TPacketWriter, byte>();
			Builder.SetToString<TPacketWriter, short>();
			Builder.SetToString<TPacketWriter, ushort>();
			Builder.SetToString<TPacketWriter, int>();
			Builder.SetToString<TPacketWriter, uint>();
			Builder.SetToString<TPacketWriter, long>();
			Builder.SetToString<TPacketWriter, ulong>();
			Builder.SetToString<TPacketWriter, float>();
			Builder.SetToString<TPacketWriter, decimal>();
			Builder.SetToString<TPacketWriter, double>();

			return Builder;
		}
	}
}
