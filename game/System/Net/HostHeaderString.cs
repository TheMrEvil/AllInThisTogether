using System;
using System.Text;

namespace System.Net
{
	// Token: 0x0200060E RID: 1550
	internal class HostHeaderString
	{
		// Token: 0x06003111 RID: 12561 RVA: 0x000A96CA File Offset: 0x000A78CA
		internal HostHeaderString()
		{
			this.Init(null);
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000A96D9 File Offset: 0x000A78D9
		internal HostHeaderString(string s)
		{
			this.Init(s);
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000A96E8 File Offset: 0x000A78E8
		private void Init(string s)
		{
			this.m_String = s;
			this.m_Converted = false;
			this.m_Bytes = null;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000A9700 File Offset: 0x000A7900
		private void Convert()
		{
			if (this.m_String != null && !this.m_Converted)
			{
				this.m_Bytes = Encoding.Default.GetBytes(this.m_String);
				string @string = Encoding.Default.GetString(this.m_Bytes);
				if (string.Compare(this.m_String, @string, StringComparison.Ordinal) != 0)
				{
					this.m_Bytes = Encoding.UTF8.GetBytes(this.m_String);
				}
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x000A9769 File Offset: 0x000A7969
		// (set) Token: 0x06003116 RID: 12566 RVA: 0x000A9771 File Offset: 0x000A7971
		internal string String
		{
			get
			{
				return this.m_String;
			}
			set
			{
				this.Init(value);
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000A977A File Offset: 0x000A797A
		internal int ByteCount
		{
			get
			{
				this.Convert();
				return this.m_Bytes.Length;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x000A978A File Offset: 0x000A798A
		internal byte[] Bytes
		{
			get
			{
				this.Convert();
				return this.m_Bytes;
			}
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000A9798 File Offset: 0x000A7998
		internal void Copy(byte[] destBytes, int destByteIndex)
		{
			this.Convert();
			Array.Copy(this.m_Bytes, 0, destBytes, destByteIndex, this.m_Bytes.Length);
		}

		// Token: 0x04001C93 RID: 7315
		private bool m_Converted;

		// Token: 0x04001C94 RID: 7316
		private string m_String;

		// Token: 0x04001C95 RID: 7317
		private byte[] m_Bytes;
	}
}
