namespace SF.Net.Serialization.Abstractions
{
	public interface IValueSerializer
	{

	}
	public interface IValueSerializer<TPacketWriter, TValue> : IValueSerializer
	{
		void Serialize(TValue Value, TPacketWriter Writer, IFieldOption Field);
	}
	public interface IValueSerializerProvider<TPacketWriter>
	{
		IValueSerializer GetValueSerializer(IFieldOption Field);
	}

	public interface IValueSerializerProviderBuilder<TPacketWriter>
	{
		IValueSerializerProviderBuilder<TPacketWriter> Set<TValue>(
			IValueSerializer<TPacketWriter, TValue> Serializer,
			string Channel,
			string Field
			);
		IValueSerializerProvider<TPacketWriter> Build();
	}





	public interface IValueDeserializer
	{

	}
	public interface IValueDeserializer<TPacketReader, TValue> : IValueDeserializer
	{
		TValue Deserialize(TPacketReader Reader, IFieldOption Field);
	}


	
	public interface IValueDeserializerProvider<TPacketReader>
	{
		IValueDeserializer GetValueDeserializer(IFieldOption Field);
	}

	public interface IValueDeserializerProviderBuilder<TPacketReader>
	{
		IValueDeserializerProviderBuilder<TPacketReader> Set<TValue>(
			IValueDeserializer<TPacketReader, TValue> Deserializer,
			string Channel,
			string Field
			);
		IValueDeserializerProvider<TPacketReader> Build();
	}

}
