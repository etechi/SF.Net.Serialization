namespace SF.Net.Serialization.Abstractions
{
	public interface IFieldOption
	{
		int Order { get; }
		string Name { get; }
		int Size { get; }
		string Channel { get; }
	}
}
