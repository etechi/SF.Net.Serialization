using System;
using System.Linq;
namespace SF.Net.Serialization.StringPackets
{
	public class StringPacketSetting
	{
		public string KeyValueSplitter { get; set; }
		public string FieldSplitter { get; set; }
		public Func<string, string> Escape { get; set; }
		public Func<string, string> Unescape { get; set; }
	}

	public class StringPacketWriter : PacketWriter<string>
	{
		public virtual string GetPacket(StringPacketSetting Setting)
		{
			return string.Join(
				Setting.FieldSplitter,
				this["#default"].Select(p => 
					Setting.Escape(p.Field.Name) + 
					Setting.KeyValueSplitter + 
					Setting.Escape(p.Value ?? "")
					)
				);

		}
	}

	public class QueryStringPacketSetting : StringPacketSetting
	{
		public static QueryStringPacketSetting Instance { get; } = new QueryStringPacketSetting();
		public QueryStringPacketSetting()
		{
			KeyValueSplitter = "=";
			FieldSplitter = "&";
			Escape = Uri.EscapeDataString;
			Unescape = Uri.UnescapeDataString;
		}
	}
	

}
