using System;

namespace System.Xml
{
	// Token: 0x02000022 RID: 34
	internal class StringHandle
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00003A8E File Offset: 0x00001C8E
		public StringHandle(XmlBufferReader bufferReader)
		{
			this.bufferReader = bufferReader;
			this.SetValue(0, 0);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003AA5 File Offset: 0x00001CA5
		public void SetValue(int offset, int length)
		{
			this.type = StringHandle.StringHandleType.UTF8;
			this.offset = offset;
			this.length = length;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003ABC File Offset: 0x00001CBC
		public void SetConstantValue(StringHandleConstStringType constStringType)
		{
			this.type = StringHandle.StringHandleType.ConstString;
			this.key = (int)constStringType;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003ACC File Offset: 0x00001CCC
		public void SetValue(int offset, int length, bool escaped)
		{
			this.type = (escaped ? StringHandle.StringHandleType.EscapedUTF8 : StringHandle.StringHandleType.UTF8);
			this.offset = offset;
			this.length = length;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003AE9 File Offset: 0x00001CE9
		public void SetValue(int key)
		{
			this.type = StringHandle.StringHandleType.Dictionary;
			this.key = key;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003AF9 File Offset: 0x00001CF9
		public void SetValue(StringHandle value)
		{
			this.type = value.type;
			this.key = value.key;
			this.offset = value.offset;
			this.length = value.length;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003B2B File Offset: 0x00001D2B
		public bool IsEmpty
		{
			get
			{
				if (this.type == StringHandle.StringHandleType.UTF8)
				{
					return this.length == 0;
				}
				return this.Equals2(string.Empty);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003B4C File Offset: 0x00001D4C
		public bool IsXmlns
		{
			get
			{
				if (this.type != StringHandle.StringHandleType.UTF8)
				{
					return this.Equals2("xmlns");
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

		// Token: 0x060000B8 RID: 184 RVA: 0x00003BB9 File Offset: 0x00001DB9
		public void ToPrefixHandle(PrefixHandle prefix)
		{
			prefix.SetValue(this.offset, this.length);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public string GetString(XmlNameTable nameTable)
		{
			StringHandle.StringHandleType stringHandleType = this.type;
			if (stringHandleType == StringHandle.StringHandleType.UTF8)
			{
				return this.bufferReader.GetString(this.offset, this.length, nameTable);
			}
			if (stringHandleType == StringHandle.StringHandleType.Dictionary)
			{
				return nameTable.Add(this.bufferReader.GetDictionaryString(this.key).Value);
			}
			if (stringHandleType == StringHandle.StringHandleType.ConstString)
			{
				return nameTable.Add(StringHandle.constStrings[this.key]);
			}
			return this.bufferReader.GetEscapedString(this.offset, this.length, nameTable);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003C50 File Offset: 0x00001E50
		public string GetString()
		{
			StringHandle.StringHandleType stringHandleType = this.type;
			if (stringHandleType == StringHandle.StringHandleType.UTF8)
			{
				return this.bufferReader.GetString(this.offset, this.length);
			}
			if (stringHandleType == StringHandle.StringHandleType.Dictionary)
			{
				return this.bufferReader.GetDictionaryString(this.key).Value;
			}
			if (stringHandleType == StringHandle.StringHandleType.ConstString)
			{
				return StringHandle.constStrings[this.key];
			}
			return this.bufferReader.GetEscapedString(this.offset, this.length);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public byte[] GetString(out int offset, out int length)
		{
			StringHandle.StringHandleType stringHandleType = this.type;
			if (stringHandleType == StringHandle.StringHandleType.UTF8)
			{
				offset = this.offset;
				length = this.length;
				return this.bufferReader.Buffer;
			}
			if (stringHandleType == StringHandle.StringHandleType.Dictionary)
			{
				byte[] array = this.bufferReader.GetDictionaryString(this.key).ToUTF8();
				offset = 0;
				length = array.Length;
				return array;
			}
			if (stringHandleType == StringHandle.StringHandleType.ConstString)
			{
				byte[] array2 = XmlConverter.ToBytes(StringHandle.constStrings[this.key]);
				offset = 0;
				length = array2.Length;
				return array2;
			}
			byte[] array3 = XmlConverter.ToBytes(this.bufferReader.GetEscapedString(this.offset, this.length));
			offset = 0;
			length = array3.Length;
			return array3;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003D62 File Offset: 0x00001F62
		public bool TryGetDictionaryString(out XmlDictionaryString value)
		{
			if (this.type == StringHandle.StringHandleType.Dictionary)
			{
				value = this.bufferReader.GetDictionaryString(this.key);
				return true;
			}
			if (this.IsEmpty)
			{
				value = XmlDictionaryString.Empty;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003D96 File Offset: 0x00001F96
		public override string ToString()
		{
			return this.GetString();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003DA0 File Offset: 0x00001FA0
		private bool Equals2(int key2, XmlBufferReader bufferReader2)
		{
			StringHandle.StringHandleType stringHandleType = this.type;
			if (stringHandleType == StringHandle.StringHandleType.Dictionary)
			{
				return this.bufferReader.Equals2(this.key, key2, bufferReader2);
			}
			if (stringHandleType == StringHandle.StringHandleType.UTF8)
			{
				return this.bufferReader.Equals2(this.offset, this.length, bufferReader2.GetDictionaryString(key2).Value);
			}
			return this.GetString() == this.bufferReader.GetDictionaryString(key2).Value;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003E10 File Offset: 0x00002010
		private bool Equals2(XmlDictionaryString xmlString2)
		{
			StringHandle.StringHandleType stringHandleType = this.type;
			if (stringHandleType == StringHandle.StringHandleType.Dictionary)
			{
				return this.bufferReader.Equals2(this.key, xmlString2);
			}
			if (stringHandleType == StringHandle.StringHandleType.UTF8)
			{
				return this.bufferReader.Equals2(this.offset, this.length, xmlString2.ToUTF8());
			}
			return this.GetString() == xmlString2.Value;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003E70 File Offset: 0x00002070
		private bool Equals2(string s2)
		{
			StringHandle.StringHandleType stringHandleType = this.type;
			if (stringHandleType == StringHandle.StringHandleType.Dictionary)
			{
				return this.bufferReader.GetDictionaryString(this.key).Value == s2;
			}
			if (stringHandleType == StringHandle.StringHandleType.UTF8)
			{
				return this.bufferReader.Equals2(this.offset, this.length, s2);
			}
			return this.GetString() == s2;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003ED0 File Offset: 0x000020D0
		private bool Equals2(int offset2, int length2, XmlBufferReader bufferReader2)
		{
			StringHandle.StringHandleType stringHandleType = this.type;
			if (stringHandleType == StringHandle.StringHandleType.Dictionary)
			{
				return bufferReader2.Equals2(offset2, length2, this.bufferReader.GetDictionaryString(this.key).Value);
			}
			if (stringHandleType == StringHandle.StringHandleType.UTF8)
			{
				return this.bufferReader.Equals2(this.offset, this.length, bufferReader2, offset2, length2);
			}
			return this.GetString() == this.bufferReader.GetString(offset2, length2);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003F40 File Offset: 0x00002140
		private bool Equals2(StringHandle s2)
		{
			StringHandle.StringHandleType stringHandleType = s2.type;
			if (stringHandleType == StringHandle.StringHandleType.Dictionary)
			{
				return this.Equals2(s2.key, s2.bufferReader);
			}
			if (stringHandleType == StringHandle.StringHandleType.UTF8)
			{
				return this.Equals2(s2.offset, s2.length, s2.bufferReader);
			}
			return this.Equals2(s2.GetString());
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003F93 File Offset: 0x00002193
		public static bool operator ==(StringHandle s1, XmlDictionaryString xmlString2)
		{
			return s1.Equals2(xmlString2);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003F9C File Offset: 0x0000219C
		public static bool operator !=(StringHandle s1, XmlDictionaryString xmlString2)
		{
			return !s1.Equals2(xmlString2);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003FA8 File Offset: 0x000021A8
		public static bool operator ==(StringHandle s1, string s2)
		{
			return s1.Equals2(s2);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003FB1 File Offset: 0x000021B1
		public static bool operator !=(StringHandle s1, string s2)
		{
			return !s1.Equals2(s2);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003FBD File Offset: 0x000021BD
		public static bool operator ==(StringHandle s1, StringHandle s2)
		{
			return s1.Equals2(s2);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003FC6 File Offset: 0x000021C6
		public static bool operator !=(StringHandle s1, StringHandle s2)
		{
			return !s1.Equals2(s2);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003FD4 File Offset: 0x000021D4
		public int CompareTo(StringHandle that)
		{
			if (this.type == StringHandle.StringHandleType.UTF8 && that.type == StringHandle.StringHandleType.UTF8)
			{
				return this.bufferReader.Compare(this.offset, this.length, that.offset, that.length);
			}
			return string.Compare(this.GetString(), that.GetString(), StringComparison.Ordinal);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000402C File Offset: 0x0000222C
		public override bool Equals(object obj)
		{
			StringHandle stringHandle = obj as StringHandle;
			return stringHandle != null && this == stringHandle;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000404C File Offset: 0x0000224C
		public override int GetHashCode()
		{
			return this.GetString().GetHashCode();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004059 File Offset: 0x00002259
		// Note: this type is marked as 'beforefieldinit'.
		static StringHandle()
		{
		}

		// Token: 0x0400005B RID: 91
		private XmlBufferReader bufferReader;

		// Token: 0x0400005C RID: 92
		private StringHandle.StringHandleType type;

		// Token: 0x0400005D RID: 93
		private int key;

		// Token: 0x0400005E RID: 94
		private int offset;

		// Token: 0x0400005F RID: 95
		private int length;

		// Token: 0x04000060 RID: 96
		private static string[] constStrings = new string[]
		{
			"type",
			"root",
			"item"
		};

		// Token: 0x02000023 RID: 35
		private enum StringHandleType
		{
			// Token: 0x04000062 RID: 98
			Dictionary,
			// Token: 0x04000063 RID: 99
			UTF8,
			// Token: 0x04000064 RID: 100
			EscapedUTF8,
			// Token: 0x04000065 RID: 101
			ConstString
		}
	}
}
