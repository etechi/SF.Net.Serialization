using SF.Net.Serialization.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace SF.Net.Serialization
{
	
	public class DictPacketReader<TFieldValueType> :
		Dictionary<(string,string),TFieldValueType>
	{
		public TFieldValueType Read(IFieldOption Field)
		{
			var channel = Field.Channel ?? "#default";
			if (TryGetValue((channel, Field.Name), out var re))
				return re;
			return default(TFieldValueType);
		}
	}
}
