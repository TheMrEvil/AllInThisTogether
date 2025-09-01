using System;

namespace System.Xml
{
	// Token: 0x02000020 RID: 32
	internal class PrefixHandle
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000035D3 File Offset: 0x000017D3
		public PrefixHandle(XmlBufferReader bufferReader)
		{
			this.bufferReader = bufferReader;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000035E2 File Offset: 0x000017E2
		public void SetValue(PrefixHandleType type)
		{
			this.type = type;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000035EB File Offset: 0x000017EB
		public void SetValue(PrefixHandle prefix)
		{
			this.type = prefix.type;
			this.offset = prefix.offset;
			this.length = prefix.length;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003614 File Offset: 0x00001814
		public void SetValue(int offset, int length)
		{
			if (length == 0)
			{
				this.SetValue(PrefixHandleType.Empty);
				return;
			}
			if (length == 1)
			{
				byte @byte = this.bufferReader.GetByte(offset);
				if (@byte >= 97 && @byte <= 122)
				{
					this.SetValue(PrefixHandle.GetAlphaPrefix((int)(@byte - 97)));
					return;
				}
			}
			this.type = PrefixHandleType.Buffer;
			this.offset = offset;
			this.length = length;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000366D File Offset: 0x0000186D
		public bool IsEmpty
		{
			get
			{
				return this.type == PrefixHandleType.Empty;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003678 File Offset: 0x00001878
		public bool IsXmlns
		{
			get
			{
				if (this.type != PrefixHandleType.Buffer)
				{
					return false;
				}
				if (this.length != 5)
				{
					return false;
				}
				byte[] buffer = this.bufferReader.Buffer;
				int num = this.offset;
				return buffer[num] == 120 && buffer[num + 1] == 109 && buffer[num + 2] == 108 && buffer[num + 3] == 110 && buffer[num + 4] == 115;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000036DC File Offset: 0x000018DC
		public bool IsXml
		{
			get
			{
				if (this.type != PrefixHandleType.Buffer)
				{
					return false;
				}
				if (this.length != 3)
				{
					return false;
				}
				byte[] buffer = this.bufferReader.Buffer;
				int num = this.offset;
				return buffer[num] == 120 && buffer[num + 1] == 109 && buffer[num + 2] == 108;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000372E File Offset: 0x0000192E
		public bool TryGetShortPrefix(out PrefixHandleType type)
		{
			type = this.type;
			return type != PrefixHandleType.Buffer;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003741 File Offset: 0x00001941
		public static string GetString(PrefixHandleType type)
		{
			return PrefixHandle.prefixStrings[(int)type];
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000374A File Offset: 0x0000194A
		public static PrefixHandleType GetAlphaPrefix(int index)
		{
			return PrefixHandleType.A + index;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000374F File Offset: 0x0000194F
		public static byte[] GetString(PrefixHandleType type, out int offset, out int length)
		{
			if (type == PrefixHandleType.Empty)
			{
				offset = 0;
				length = 0;
			}
			else
			{
				length = 1;
				offset = type - PrefixHandleType.A;
			}
			return PrefixHandle.prefixBuffer;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000376C File Offset: 0x0000196C
		public string GetString(XmlNameTable nameTable)
		{
			PrefixHandleType prefixHandleType = this.type;
			if (prefixHandleType != PrefixHandleType.Buffer)
			{
				return PrefixHandle.GetString(prefixHandleType);
			}
			return this.bufferReader.GetString(this.offset, this.length, nameTable);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000037A4 File Offset: 0x000019A4
		public string GetString()
		{
			PrefixHandleType prefixHandleType = this.type;
			if (prefixHandleType != PrefixHandleType.Buffer)
			{
				return PrefixHandle.GetString(prefixHandleType);
			}
			return this.bufferReader.GetString(this.offset, this.length);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000037DC File Offset: 0x000019DC
		public byte[] GetString(out int offset, out int length)
		{
			PrefixHandleType prefixHandleType = this.type;
			if (prefixHandleType != PrefixHandleType.Buffer)
			{
				return PrefixHandle.GetString(prefixHandleType, out offset, out length);
			}
			offset = this.offset;
			length = this.length;
			return this.bufferReader.Buffer;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003819 File Offset: 0x00001A19
		public int CompareTo(PrefixHandle that)
		{
			return this.GetString().CompareTo(that.GetString());
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000382C File Offset: 0x00001A2C
		private bool Equals2(PrefixHandle prefix2)
		{
			PrefixHandleType prefixHandleType = this.type;
			PrefixHandleType prefixHandleType2 = prefix2.type;
			if (prefixHandleType != prefixHandleType2)
			{
				return false;
			}
			if (prefixHandleType != PrefixHandleType.Buffer)
			{
				return true;
			}
			if (this.bufferReader == prefix2.bufferReader)
			{
				return this.bufferReader.Equals2(this.offset, this.length, prefix2.offset, prefix2.length);
			}
			return this.bufferReader.Equals2(this.offset, this.length, prefix2.bufferReader, prefix2.offset, prefix2.length);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000038B0 File Offset: 0x00001AB0
		private bool Equals2(string prefix2)
		{
			PrefixHandleType prefixHandleType = this.type;
			if (prefixHandleType != PrefixHandleType.Buffer)
			{
				return PrefixHandle.GetString(prefixHandleType) == prefix2;
			}
			return this.bufferReader.Equals2(this.offset, this.length, prefix2);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000038EE File Offset: 0x00001AEE
		private bool Equals2(XmlDictionaryString prefix2)
		{
			return this.Equals2(prefix2.Value);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000038FC File Offset: 0x00001AFC
		public static bool operator ==(PrefixHandle prefix1, string prefix2)
		{
			return prefix1.Equals2(prefix2);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003905 File Offset: 0x00001B05
		public static bool operator !=(PrefixHandle prefix1, string prefix2)
		{
			return !prefix1.Equals2(prefix2);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003911 File Offset: 0x00001B11
		public static bool operator ==(PrefixHandle prefix1, XmlDictionaryString prefix2)
		{
			return prefix1.Equals2(prefix2);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000391A File Offset: 0x00001B1A
		public static bool operator !=(PrefixHandle prefix1, XmlDictionaryString prefix2)
		{
			return !prefix1.Equals2(prefix2);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003926 File Offset: 0x00001B26
		public static bool operator ==(PrefixHandle prefix1, PrefixHandle prefix2)
		{
			return prefix1.Equals2(prefix2);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000392F File Offset: 0x00001B2F
		public static bool operator !=(PrefixHandle prefix1, PrefixHandle prefix2)
		{
			return !prefix1.Equals2(prefix2);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000393C File Offset: 0x00001B3C
		public override bool Equals(object obj)
		{
			PrefixHandle prefixHandle = obj as PrefixHandle;
			return prefixHandle != null && this == prefixHandle;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000395C File Offset: 0x00001B5C
		public override string ToString()
		{
			return this.GetString();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003964 File Offset: 0x00001B64
		public override int GetHashCode()
		{
			return this.GetString().GetHashCode();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003974 File Offset: 0x00001B74
		// Note: this type is marked as 'beforefieldinit'.
		static PrefixHandle()
		{
		}

		// Token: 0x04000051 RID: 81
		private XmlBufferReader bufferReader;

		// Token: 0x04000052 RID: 82
		private PrefixHandleType type;

		// Token: 0x04000053 RID: 83
		private int offset;

		// Token: 0x04000054 RID: 84
		private int length;

		// Token: 0x04000055 RID: 85
		private static string[] prefixStrings = new string[]
		{
			"",
			"a",
			"b",
			"c",
			"d",
			"e",
			"f",
			"g",
			"h",
			"i",
			"j",
			"k",
			"l",
			"m",
			"n",
			"o",
			"p",
			"q",
			"r",
			"s",
			"t",
			"u",
			"v",
			"w",
			"x",
			"y",
			"z"
		};

		// Token: 0x04000056 RID: 86
		private static byte[] prefixBuffer = new byte[]
		{
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122
		};
	}
}
