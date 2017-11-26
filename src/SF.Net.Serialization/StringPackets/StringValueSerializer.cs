using SF.Net.Serialization.Abstractions;
using System.Reflection;
using SF.Net.Serialization.Annotations;
using System;

namespace SF.Net.Serialization.StringPackets
{
	public static class StringValueSerializer
	{
		

		public static IValueSerializerProviderBuilder<TPacketWriter> SetToString<TPacketWriter, TValue>(
			this IValueSerializerProviderBuilder<TPacketWriter> Builder,
			string Format=null,
			string Channel = null,
			string Field = null
			) where TPacketWriter : DictPacketWriter<string>
		{
			if (Format == null)
				Builder.Set<TPacketWriter, TValue, string>((v, f) => v.ToString(), Channel, Field);
			else
				Builder.Set<TPacketWriter, TValue, string>((v, f) => string.Format(Format, v), Channel, Field);
			return Builder;
		}

		public static IValueSerializerProviderBuilder<TPacketWriter> SetStringValueDeserializers<TPacketWriter>(
			this IValueSerializerProviderBuilder<TPacketWriter> Builder
			) where TPacketWriter:DictPacketWriter<string>
		{
			Builder.SetToString<TPacketWriter, sbyte>();
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
