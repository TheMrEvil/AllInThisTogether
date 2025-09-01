using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace ES3Internal
{
	// Token: 0x020000DC RID: 220
	public class ES3JSONReader : ES3Reader
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0001D7BC File Offset: 0x0001B9BC
		internal ES3JSONReader(Stream stream, ES3Settings settings, bool readHeaderAndFooter = true) : base(settings, readHeaderAndFooter)
		{
			this.baseReader = new StreamReader(stream);
			if (readHeaderAndFooter)
			{
				try
				{
					this.SkipOpeningBraceOfFile();
				}
				catch
				{
					this.Dispose();
					throw new FormatException("Cannot load from file because the data in it is not JSON data, or the data is encrypted.\nIf the save data is encrypted, please ensure that encryption is enabled when you load, and that you are using the same password used to encrypt the data.");
				}
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001D80C File Offset: 0x0001BA0C
		public override string ReadPropertyName()
		{
			char c = this.PeekCharIgnoreWhitespace();
			if (ES3JSONReader.IsTerminator(c))
			{
				return null;
			}
			if (c == ',')
			{
				this.ReadCharIgnoreWhitespace(true);
			}
			else if (!ES3JSONReader.IsQuotationMark(c))
			{
				throw new FormatException("Expected ',' separating properties or '\"' before property name, found '" + c.ToString() + "'.");
			}
			string text = this.Read_string();
			if (text == null)
			{
				throw new FormatException("Stream isn't positioned before a property.");
			}
			ES3Debug.Log("<b>" + text + "</b> (reading property)", null, this.serializationDepth);
			this.ReadCharIgnoreWhitespace(':');
			return text;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001D898 File Offset: 0x0001BA98
		protected override Type ReadKeyPrefix(bool ignoreType = false)
		{
			this.StartReadObject();
			Type result = null;
			string text = this.ReadPropertyName();
			if (text == "__type")
			{
				string typeString = this.Read_string();
				result = (ignoreType ? null : ES3Reflection.GetType(typeString));
				text = this.ReadPropertyName();
			}
			if (text != "value")
			{
				throw new FormatException("This data is not Easy Save Key Value data. Expected property name \"value\", found \"" + text + "\".");
			}
			return result;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001D901 File Offset: 0x0001BB01
		protected override void ReadKeySuffix()
		{
			this.EndReadObject();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001D909 File Offset: 0x0001BB09
		internal override bool StartReadObject()
		{
			base.StartReadObject();
			return this.ReadNullOrCharIgnoreWhitespace('{');
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001D91A File Offset: 0x0001BB1A
		internal override void EndReadObject()
		{
			this.ReadCharIgnoreWhitespace('}');
			base.EndReadObject();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001D92B File Offset: 0x0001BB2B
		internal override bool StartReadDictionary()
		{
			return this.StartReadObject();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001D933 File Offset: 0x0001BB33
		internal override void EndReadDictionary()
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001D935 File Offset: 0x0001BB35
		internal override bool StartReadDictionaryKey()
		{
			if (this.PeekCharIgnoreWhitespace() == '}')
			{
				this.ReadCharIgnoreWhitespace(true);
				return false;
			}
			return true;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001D94C File Offset: 0x0001BB4C
		internal override void EndReadDictionaryKey()
		{
			this.ReadCharIgnoreWhitespace(':');
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001D957 File Offset: 0x0001BB57
		internal override void StartReadDictionaryValue()
		{
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001D95C File Offset: 0x0001BB5C
		internal override bool EndReadDictionaryValue()
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c == '}')
			{
				return true;
			}
			if (c != ',')
			{
				throw new FormatException("Expected ',' seperating Dictionary items or '}' terminating Dictionary, found '" + c.ToString() + "'.");
			}
			return false;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001D99A File Offset: 0x0001BB9A
		internal override bool StartReadCollection()
		{
			return this.ReadNullOrCharIgnoreWhitespace('[');
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001D9A4 File Offset: 0x0001BBA4
		internal override void EndReadCollection()
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001D9A6 File Offset: 0x0001BBA6
		internal override bool StartReadCollectionItem()
		{
			if (this.PeekCharIgnoreWhitespace() == ']')
			{
				this.ReadCharIgnoreWhitespace(true);
				return false;
			}
			return true;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		internal override bool EndReadCollectionItem()
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c == ']')
			{
				return true;
			}
			if (c != ',')
			{
				throw new FormatException("Expected ',' seperating collection items or ']' terminating collection, found '" + c.ToString() + "'.");
			}
			return false;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001DA00 File Offset: 0x0001BC00
		private void ReadString(StreamWriter writer, bool skip = false)
		{
			bool flag = false;
			while (!flag)
			{
				char c = this.ReadOrSkipChar(writer, skip);
				if (c != '\\')
				{
					if (c == '￿')
					{
						throw new FormatException("String without closing quotation mark detected.");
					}
					if (ES3JSONReader.IsQuotationMark(c))
					{
						flag = true;
					}
				}
				else
				{
					this.ReadOrSkipChar(writer, skip);
				}
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		internal override byte[] ReadElement(bool skip = false)
		{
			StreamWriter streamWriter = skip ? null : new StreamWriter(new MemoryStream(this.settings.bufferSize));
			byte[] result;
			using (streamWriter)
			{
				int num = 0;
				char c = (char)this.baseReader.Peek();
				if (!ES3JSONReader.IsOpeningBrace(c))
				{
					if (c == '"')
					{
						this.ReadOrSkipChar(streamWriter, skip);
						this.ReadString(streamWriter, skip);
					}
					else
					{
						while (!ES3JSONReader.IsEndOfValue((char)this.baseReader.Peek()))
						{
							this.ReadOrSkipChar(streamWriter, skip);
						}
					}
					if (skip)
					{
						result = null;
					}
					else
					{
						streamWriter.Flush();
						result = ((MemoryStream)streamWriter.BaseStream).ToArray();
					}
				}
				else
				{
					for (;;)
					{
						c = this.ReadOrSkipChar(streamWriter, skip);
						if (c == '￿')
						{
							break;
						}
						if (ES3JSONReader.IsQuotationMark(c))
						{
							this.ReadString(streamWriter, skip);
						}
						else
						{
							if (c <= ']')
							{
								if (c != '[')
								{
									if (c != ']')
									{
										continue;
									}
									goto IL_E2;
								}
							}
							else if (c != '{')
							{
								if (c != '}')
								{
									continue;
								}
								goto IL_E2;
							}
							num++;
							continue;
							IL_E2:
							num--;
							if (num < 1)
							{
								goto Block_14;
							}
						}
					}
					throw new FormatException("Missing closing brace detected, as end of stream was reached before finding it.");
					Block_14:
					if (skip)
					{
						result = null;
					}
					else
					{
						streamWriter.Flush();
						result = ((MemoryStream)streamWriter.BaseStream).ToArray();
					}
				}
			}
			return result;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001DB84 File Offset: 0x0001BD84
		private char ReadOrSkipChar(StreamWriter writer, bool skip)
		{
			char c = (char)this.baseReader.Read();
			if (!skip)
			{
				writer.Write(c);
			}
			return c;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001DBAC File Offset: 0x0001BDAC
		private char ReadCharIgnoreWhitespace(bool ignoreTrailingWhitespace = true)
		{
			char result;
			while (ES3JSONReader.IsWhiteSpace(result = (char)this.baseReader.Read()))
			{
			}
			if (ignoreTrailingWhitespace)
			{
				while (ES3JSONReader.IsWhiteSpace((char)this.baseReader.Peek()))
				{
					this.baseReader.Read();
				}
			}
			return result;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		private bool ReadNullOrCharIgnoreWhitespace(char expectedChar)
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c == 'n')
			{
				char[] array = new char[3];
				this.baseReader.ReadBlock(array, 0, 3);
				if (array[0] == 'u' && array[1] == 'l' && array[2] == 'l')
				{
					return true;
				}
			}
			if (c == expectedChar)
			{
				return false;
			}
			if (c == '￿')
			{
				throw new FormatException("End of stream reached when expecting '" + expectedChar.ToString() + "'.");
			}
			throw new FormatException(string.Concat(new string[]
			{
				"Expected '",
				expectedChar.ToString(),
				"' or \"null\", found '",
				c.ToString(),
				"'."
			}));
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001DCA4 File Offset: 0x0001BEA4
		private char ReadCharIgnoreWhitespace(char expectedChar)
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c == expectedChar)
			{
				return c;
			}
			if (c == '￿')
			{
				throw new FormatException("End of stream reached when expecting '" + expectedChar.ToString() + "'.");
			}
			throw new FormatException(string.Concat(new string[]
			{
				"Expected '",
				expectedChar.ToString(),
				"', found '",
				c.ToString(),
				"'."
			}));
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001DD20 File Offset: 0x0001BF20
		private bool ReadQuotationMarkOrNullIgnoreWhitespace()
		{
			char c = this.ReadCharIgnoreWhitespace(false);
			if (c == 'n')
			{
				char[] array = new char[3];
				this.baseReader.ReadBlock(array, 0, 3);
				if (array[0] == 'u' && array[1] == 'l' && array[2] == 'l')
				{
					return true;
				}
			}
			else if (!ES3JSONReader.IsQuotationMark(c))
			{
				if (c == '￿')
				{
					throw new FormatException("End of stream reached when expecting quotation mark.");
				}
				throw new FormatException("Expected quotation mark, found '" + c.ToString() + "'.");
			}
			return false;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001DDA0 File Offset: 0x0001BFA0
		private char PeekCharIgnoreWhitespace(char expectedChar)
		{
			char c = this.PeekCharIgnoreWhitespace();
			if (c == expectedChar)
			{
				return c;
			}
			if (c == '￿')
			{
				throw new FormatException("End of stream reached while peeking, when expecting '" + expectedChar.ToString() + "'.");
			}
			throw new FormatException(string.Concat(new string[]
			{
				"Expected '",
				expectedChar.ToString(),
				"', found '",
				c.ToString(),
				"'."
			}));
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001DE1C File Offset: 0x0001C01C
		private char PeekCharIgnoreWhitespace()
		{
			char result;
			while (ES3JSONReader.IsWhiteSpace(result = (char)this.baseReader.Peek()))
			{
				this.baseReader.Read();
			}
			return result;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001DE4D File Offset: 0x0001C04D
		private void SkipWhiteSpace()
		{
			while (ES3JSONReader.IsWhiteSpace((char)this.baseReader.Peek()))
			{
				this.baseReader.Read();
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001DE70 File Offset: 0x0001C070
		private void SkipOpeningBraceOfFile()
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c != '{')
			{
				throw new FormatException("File is not valid JSON. Expected '{' at beginning of file, but found '" + c.ToString() + "'.");
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001DEA6 File Offset: 0x0001C0A6
		private static bool IsWhiteSpace(char c)
		{
			return c == ' ' || c == '\t' || c == '\n' || c == '\r';
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001DEBE File Offset: 0x0001C0BE
		private static bool IsOpeningBrace(char c)
		{
			return c == '{' || c == '[';
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001DECC File Offset: 0x0001C0CC
		private static bool IsEndOfValue(char c)
		{
			return c == '}' || c == ' ' || c == '\t' || c == ']' || c == ',' || c == ':' || c == char.MaxValue || c == '\n' || c == '\r';
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001DF00 File Offset: 0x0001C100
		private static bool IsTerminator(char c)
		{
			return c == '}' || c == ']';
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001DF0E File Offset: 0x0001C10E
		private static bool IsQuotationMark(char c)
		{
			return c == '"' || c == '“' || c == '”';
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001DF27 File Offset: 0x0001C127
		private static bool IsEndOfStream(char c)
		{
			return c == char.MaxValue;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001DF34 File Offset: 0x0001C134
		private string GetValueString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (!ES3JSONReader.IsEndOfValue(this.PeekCharIgnoreWhitespace()))
			{
				stringBuilder.Append((char)this.baseReader.Read());
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001DF7C File Offset: 0x0001C17C
		internal override string Read_string()
		{
			if (this.ReadQuotationMarkOrNullIgnoreWhitespace())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			char c;
			while (!ES3JSONReader.IsQuotationMark(c = (char)this.baseReader.Read()))
			{
				if (c == '\\')
				{
					c = (char)this.baseReader.Read();
					if (ES3JSONReader.IsEndOfStream(c))
					{
						throw new FormatException("Reached end of stream while trying to read string literal.");
					}
					if (c <= 'f')
					{
						if (c != 'b')
						{
							if (c == 'f')
							{
								c = '\f';
							}
						}
						else
						{
							c = '\b';
						}
					}
					else if (c != 'n')
					{
						if (c != 'r')
						{
							if (c == 't')
							{
								c = '\t';
							}
						}
						else
						{
							c = '\r';
						}
					}
					else
					{
						c = '\n';
					}
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001E01B File Offset: 0x0001C21B
		internal override long Read_ref()
		{
			if (ES3ReferenceMgrBase.Current == null)
			{
				throw new InvalidOperationException("An Easy Save 3 Manager is required to load references. To add one to your scene, exit playmode and go to Tools > Easy Save 3 > Add Manager to Scene");
			}
			if (ES3JSONReader.IsQuotationMark(this.PeekCharIgnoreWhitespace()))
			{
				return long.Parse(this.Read_string());
			}
			return this.Read_long();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001E054 File Offset: 0x0001C254
		internal override char Read_char()
		{
			return char.Parse(this.Read_string());
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001E061 File Offset: 0x0001C261
		internal override float Read_float()
		{
			return float.Parse(this.GetValueString(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001E073 File Offset: 0x0001C273
		internal override int Read_int()
		{
			return int.Parse(this.GetValueString());
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001E080 File Offset: 0x0001C280
		internal override bool Read_bool()
		{
			return bool.Parse(this.GetValueString());
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001E08D File Offset: 0x0001C28D
		internal override decimal Read_decimal()
		{
			return decimal.Parse(this.GetValueString(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001E09F File Offset: 0x0001C29F
		internal override double Read_double()
		{
			return double.Parse(this.GetValueString(), CultureInfo.InvariantCulture);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001E0B1 File Offset: 0x0001C2B1
		internal override long Read_long()
		{
			return long.Parse(this.GetValueString());
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001E0BE File Offset: 0x0001C2BE
		internal override ulong Read_ulong()
		{
			return ulong.Parse(this.GetValueString());
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001E0CB File Offset: 0x0001C2CB
		internal override uint Read_uint()
		{
			return uint.Parse(this.GetValueString());
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001E0D8 File Offset: 0x0001C2D8
		internal override byte Read_byte()
		{
			return (byte)int.Parse(this.GetValueString());
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001E0E6 File Offset: 0x0001C2E6
		internal override sbyte Read_sbyte()
		{
			return (sbyte)int.Parse(this.GetValueString());
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001E0F4 File Offset: 0x0001C2F4
		internal override short Read_short()
		{
			return (short)int.Parse(this.GetValueString());
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001E102 File Offset: 0x0001C302
		internal override ushort Read_ushort()
		{
			return (ushort)int.Parse(this.GetValueString());
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001E110 File Offset: 0x0001C310
		internal override byte[] Read_byteArray()
		{
			return Convert.FromBase64String(this.Read_string());
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001E11D File Offset: 0x0001C31D
		public override void Dispose()
		{
			this.baseReader.Dispose();
		}

		// Token: 0x0400014F RID: 335
		private const char endOfStreamChar = '￿';

		// Token: 0x04000150 RID: 336
		public StreamReader baseReader;
	}
}
