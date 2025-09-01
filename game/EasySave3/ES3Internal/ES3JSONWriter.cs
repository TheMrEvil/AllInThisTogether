using System;
using System.Globalization;
using System.IO;

namespace ES3Internal
{
	// Token: 0x020000EA RID: 234
	internal class ES3JSONWriter : ES3Writer
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x0001EFC5 File Offset: 0x0001D1C5
		public ES3JSONWriter(Stream stream, ES3Settings settings) : this(stream, settings, true, true)
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001EFD1 File Offset: 0x0001D1D1
		internal ES3JSONWriter(Stream stream, ES3Settings settings, bool writeHeaderAndFooter, bool mergeKeys) : base(settings, writeHeaderAndFooter, mergeKeys)
		{
			this.baseWriter = new StreamWriter(stream);
			this.StartWriteFile();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001EFF6 File Offset: 0x0001D1F6
		internal override void WritePrimitive(int value)
		{
			this.baseWriter.Write(value);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001F004 File Offset: 0x0001D204
		internal override void WritePrimitive(float value)
		{
			this.baseWriter.Write(value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001F022 File Offset: 0x0001D222
		internal override void WritePrimitive(bool value)
		{
			this.baseWriter.Write(value ? "true" : "false");
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001F03E File Offset: 0x0001D23E
		internal override void WritePrimitive(decimal value)
		{
			this.baseWriter.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001F057 File Offset: 0x0001D257
		internal override void WritePrimitive(double value)
		{
			this.baseWriter.Write(value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001F075 File Offset: 0x0001D275
		internal override void WritePrimitive(long value)
		{
			this.baseWriter.Write(value);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001F083 File Offset: 0x0001D283
		internal override void WritePrimitive(ulong value)
		{
			this.baseWriter.Write(value);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001F091 File Offset: 0x0001D291
		internal override void WritePrimitive(uint value)
		{
			this.baseWriter.Write(value);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001F09F File Offset: 0x0001D29F
		internal override void WritePrimitive(byte value)
		{
			this.baseWriter.Write(Convert.ToInt32(value));
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001F0B2 File Offset: 0x0001D2B2
		internal override void WritePrimitive(sbyte value)
		{
			this.baseWriter.Write(Convert.ToInt32(value));
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001F0C5 File Offset: 0x0001D2C5
		internal override void WritePrimitive(short value)
		{
			this.baseWriter.Write(Convert.ToInt32(value));
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		internal override void WritePrimitive(ushort value)
		{
			this.baseWriter.Write(Convert.ToInt32(value));
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001F0EB File Offset: 0x0001D2EB
		internal override void WritePrimitive(char value)
		{
			this.WritePrimitive(value.ToString());
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001F0FA File Offset: 0x0001D2FA
		internal override void WritePrimitive(byte[] value)
		{
			this.WritePrimitive(Convert.ToBase64String(value));
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001F108 File Offset: 0x0001D308
		internal override void WritePrimitive(string value)
		{
			this.baseWriter.Write("\"");
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '/')
				{
					switch (c)
					{
					case '\b':
						this.baseWriter.Write("\\b");
						break;
					case '\t':
						this.baseWriter.Write("\\t");
						break;
					case '\n':
						this.baseWriter.Write("\\n");
						break;
					case '\v':
						goto IL_DD;
					case '\f':
						this.baseWriter.Write("\\f");
						break;
					case '\r':
						this.baseWriter.Write("\\r");
						break;
					default:
						if (c != '"' && c != '/')
						{
							goto IL_DD;
						}
						goto IL_68;
					}
				}
				else
				{
					if (c == '\\' || c == '“' || c == '”')
					{
						goto IL_68;
					}
					goto IL_DD;
				}
				IL_E9:
				i++;
				continue;
				IL_68:
				this.baseWriter.Write('\\');
				this.baseWriter.Write(c);
				goto IL_E9;
				IL_DD:
				this.baseWriter.Write(c);
				goto IL_E9;
			}
			this.baseWriter.Write("\"");
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001F21E File Offset: 0x0001D41E
		internal override void WriteNull()
		{
			this.baseWriter.Write("null");
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001F230 File Offset: 0x0001D430
		private static bool CharacterRequiresEscaping(char c)
		{
			return c == '"' || c == '\\' || c == '“' || c == '”';
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001F24E File Offset: 0x0001D44E
		private void WriteCommaIfRequired()
		{
			if (!this.isFirstProperty)
			{
				this.baseWriter.Write(',');
			}
			else
			{
				this.isFirstProperty = false;
			}
			this.WriteNewlineAndTabs();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001F274 File Offset: 0x0001D474
		internal override void WriteRawProperty(string name, byte[] value)
		{
			this.StartWriteProperty(name);
			this.baseWriter.Write(this.settings.encoding.GetString(value, 0, value.Length));
			this.EndWriteProperty(name);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001F2A4 File Offset: 0x0001D4A4
		internal override void StartWriteFile()
		{
			if (this.writeHeaderAndFooter)
			{
				this.baseWriter.Write('{');
			}
			base.StartWriteFile();
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001F2C1 File Offset: 0x0001D4C1
		internal override void EndWriteFile()
		{
			base.EndWriteFile();
			this.WriteNewlineAndTabs();
			if (this.writeHeaderAndFooter)
			{
				this.baseWriter.Write('}');
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001F2E4 File Offset: 0x0001D4E4
		internal override void StartWriteProperty(string name)
		{
			base.StartWriteProperty(name);
			this.WriteCommaIfRequired();
			this.Write(name, ES3.ReferenceMode.ByRef);
			if (this.settings.prettyPrint)
			{
				this.baseWriter.Write(' ');
			}
			this.baseWriter.Write(':');
			if (this.settings.prettyPrint)
			{
				this.baseWriter.Write(' ');
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001F347 File Offset: 0x0001D547
		internal override void EndWriteProperty(string name)
		{
			base.EndWriteProperty(name);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001F350 File Offset: 0x0001D550
		internal override void StartWriteObject(string name)
		{
			base.StartWriteObject(name);
			this.isFirstProperty = true;
			this.baseWriter.Write('{');
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001F36D File Offset: 0x0001D56D
		internal override void EndWriteObject(string name)
		{
			base.EndWriteObject(name);
			this.isFirstProperty = false;
			this.WriteNewlineAndTabs();
			this.baseWriter.Write('}');
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001F390 File Offset: 0x0001D590
		internal override void StartWriteCollection()
		{
			base.StartWriteCollection();
			this.baseWriter.Write('[');
			this.WriteNewlineAndTabs();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001F3AB File Offset: 0x0001D5AB
		internal override void EndWriteCollection()
		{
			base.EndWriteCollection();
			this.WriteNewlineAndTabs();
			this.baseWriter.Write(']');
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001F3C6 File Offset: 0x0001D5C6
		internal override void StartWriteCollectionItem(int index)
		{
			if (index != 0)
			{
				this.baseWriter.Write(',');
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		internal override void EndWriteCollectionItem(int index)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001F3DA File Offset: 0x0001D5DA
		internal override void StartWriteDictionary()
		{
			this.StartWriteObject(null);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001F3E3 File Offset: 0x0001D5E3
		internal override void EndWriteDictionary()
		{
			this.EndWriteObject(null);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001F3EC File Offset: 0x0001D5EC
		internal override void StartWriteDictionaryKey(int index)
		{
			if (index != 0)
			{
				this.baseWriter.Write(',');
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001F3FE File Offset: 0x0001D5FE
		internal override void EndWriteDictionaryKey(int index)
		{
			this.baseWriter.Write(':');
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001F40D File Offset: 0x0001D60D
		internal override void StartWriteDictionaryValue(int index)
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001F40F File Offset: 0x0001D60F
		internal override void EndWriteDictionaryValue(int index)
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001F411 File Offset: 0x0001D611
		public override void Dispose()
		{
			this.baseWriter.Dispose();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001F420 File Offset: 0x0001D620
		public void WriteNewlineAndTabs()
		{
			if (this.settings.prettyPrint)
			{
				this.baseWriter.Write(Environment.NewLine);
				for (int i = 0; i < this.serializationDepth; i++)
				{
					this.baseWriter.Write('\t');
				}
			}
		}

		// Token: 0x04000185 RID: 389
		internal StreamWriter baseWriter;

		// Token: 0x04000186 RID: 390
		private bool isFirstProperty = true;
	}
}
