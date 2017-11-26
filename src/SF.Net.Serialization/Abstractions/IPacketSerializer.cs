using System;

namespace SF.Net.Serialization.Abstractions
{
	public interface IPacketSerializer<TPacketWriter,TPacket>
    {
		void Serialize(TPacketWriter Writer, TPacket Package);
    }

	public interface IPacketDeserializer<TPacketReader, TPacket>
	{
		void Deserialize(TPacketReader Writer, TPacket Package);
	}
}
