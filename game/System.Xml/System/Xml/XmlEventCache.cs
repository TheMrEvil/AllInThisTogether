using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Xsl.Runtime;

namespace System.Xml
{
	// Token: 0x02000095 RID: 149
	internal sealed class XmlEventCache : XmlRawWriter
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x0001D51E File Offset: 0x0001B71E
		public XmlEventCache(string baseUri, bool hasRootNode)
		{
			this.baseUri = baseUri;
			this.hasRootNode = hasRootNode;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001D534 File Offset: 0x0001B734
		public void EndEvents()
		{
			if (this.singleText.Count == 0)
			{
				this.AddEvent(XmlEventCache.XmlEventType.Unknown);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001D54A File Offset: 0x0001B74A
		public string BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001D552 File Offset: 0x0001B752
		public bool HasRootNode
		{
			get
			{
				return this.hasRootNode;
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001D55C File Offset: 0x0001B75C
		public void EventsToWriter(XmlWriter writer)
		{
			if (this.singleText.Count != 0)
			{
				writer.WriteString(this.singleText.GetResult());
				return;
			}
			XmlRawWriter xmlRawWriter = writer as XmlRawWriter;
			for (int i = 0; i < this.pages.Count; i++)
			{
				XmlEventCache.XmlEvent[] array = this.pages[i];
				for (int j = 0; j < array.Length; j++)
				{
					switch (array[j].EventType)
					{
					case XmlEventCache.XmlEventType.Unknown:
						return;
					case XmlEventCache.XmlEventType.DocType:
						writer.WriteDocType(array[j].String1, array[j].String2, array[j].String3, (string)array[j].Object);
						break;
					case XmlEventCache.XmlEventType.StartElem:
						writer.WriteStartElement(array[j].String1, array[j].String2, array[j].String3);
						break;
					case XmlEventCache.XmlEventType.StartAttr:
						writer.WriteStartAttribute(array[j].String1, array[j].String2, array[j].String3);
						break;
					case XmlEventCache.XmlEventType.EndAttr:
						writer.WriteEndAttribute();
						break;
					case XmlEventCache.XmlEventType.CData:
						writer.WriteCData(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.Comment:
						writer.WriteComment(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.PI:
						writer.WriteProcessingInstruction(array[j].String1, array[j].String2);
						break;
					case XmlEventCache.XmlEventType.Whitespace:
						writer.WriteWhitespace(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.String:
						writer.WriteString(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.Raw:
						writer.WriteRaw(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.EntRef:
						writer.WriteEntityRef(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.CharEnt:
						writer.WriteCharEntity((char)array[j].Object);
						break;
					case XmlEventCache.XmlEventType.SurrCharEnt:
					{
						char[] array2 = (char[])array[j].Object;
						writer.WriteSurrogateCharEntity(array2[0], array2[1]);
						break;
					}
					case XmlEventCache.XmlEventType.Base64:
					{
						byte[] array3 = (byte[])array[j].Object;
						writer.WriteBase64(array3, 0, array3.Length);
						break;
					}
					case XmlEventCache.XmlEventType.BinHex:
					{
						byte[] array3 = (byte[])array[j].Object;
						writer.WriteBinHex(array3, 0, array3.Length);
						break;
					}
					case XmlEventCache.XmlEventType.XmlDecl1:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteXmlDeclaration((XmlStandalone)array[j].Object);
						}
						break;
					case XmlEventCache.XmlEventType.XmlDecl2:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteXmlDeclaration(array[j].String1);
						}
						break;
					case XmlEventCache.XmlEventType.StartContent:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.StartElementContent();
						}
						break;
					case XmlEventCache.XmlEventType.EndElem:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteEndElement(array[j].String1, array[j].String2, array[j].String3);
						}
						else
						{
							writer.WriteEndElement();
						}
						break;
					case XmlEventCache.XmlEventType.FullEndElem:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteFullEndElement(array[j].String1, array[j].String2, array[j].String3);
						}
						else
						{
							writer.WriteFullEndElement();
						}
						break;
					case XmlEventCache.XmlEventType.Nmsp:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteNamespaceDeclaration(array[j].String1, array[j].String2);
						}
						else
						{
							writer.WriteAttributeString("xmlns", array[j].String1, "http://www.w3.org/2000/xmlns/", array[j].String2);
						}
						break;
					case XmlEventCache.XmlEventType.EndBase64:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteEndBase64();
						}
						break;
					case XmlEventCache.XmlEventType.Close:
						writer.Close();
						break;
					case XmlEventCache.XmlEventType.Flush:
						writer.Flush();
						break;
					case XmlEventCache.XmlEventType.Dispose:
						((IDisposable)writer).Dispose();
						break;
					}
				}
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001D96C File Offset: 0x0001BB6C
		public string EventsToString()
		{
			if (this.singleText.Count != 0)
			{
				return this.singleText.GetResult();
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			for (int i = 0; i < this.pages.Count; i++)
			{
				XmlEventCache.XmlEvent[] array = this.pages[i];
				for (int j = 0; j < array.Length; j++)
				{
					switch (array[j].EventType)
					{
					case XmlEventCache.XmlEventType.Unknown:
						return stringBuilder.ToString();
					case XmlEventCache.XmlEventType.StartAttr:
						flag = true;
						break;
					case XmlEventCache.XmlEventType.EndAttr:
						flag = false;
						break;
					case XmlEventCache.XmlEventType.CData:
					case XmlEventCache.XmlEventType.Whitespace:
					case XmlEventCache.XmlEventType.String:
					case XmlEventCache.XmlEventType.Raw:
						if (!flag)
						{
							stringBuilder.Append(array[j].String1);
						}
						break;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override XmlWriterSettings Settings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001DA45 File Offset: 0x0001BC45
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.AddEvent(XmlEventCache.XmlEventType.DocType, name, pubid, sysid, subset);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001DA53 File Offset: 0x0001BC53
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartElem, prefix, localName, ns);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001DA5F File Offset: 0x0001BC5F
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartAttr, prefix, localName, ns);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001DA6B File Offset: 0x0001BC6B
		public override void WriteEndAttribute()
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndAttr);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001DA74 File Offset: 0x0001BC74
		public override void WriteCData(string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.CData, text);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001DA7E File Offset: 0x0001BC7E
		public override void WriteComment(string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Comment, text);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001DA88 File Offset: 0x0001BC88
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.PI, name, text);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001DA93 File Offset: 0x0001BC93
		public override void WriteWhitespace(string ws)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Whitespace, ws);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001DA9D File Offset: 0x0001BC9D
		public override void WriteString(string text)
		{
			if (this.pages == null)
			{
				this.singleText.ConcatNoDelimiter(text);
				return;
			}
			this.AddEvent(XmlEventCache.XmlEventType.String, text);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000118AB File Offset: 0x0000FAAB
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000118BB File Offset: 0x0000FABB
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteRaw(new string(buffer, index, count));
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001DABD File Offset: 0x0001BCBD
		public override void WriteRaw(string data)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Raw, data);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
		public override void WriteEntityRef(string name)
		{
			this.AddEvent(XmlEventCache.XmlEventType.EntRef, name);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001DAD3 File Offset: 0x0001BCD3
		public override void WriteCharEntity(char ch)
		{
			this.AddEvent(XmlEventCache.XmlEventType.CharEnt, ch);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			char[] o = new char[]
			{
				lowChar,
				highChar
			};
			this.AddEvent(XmlEventCache.XmlEventType.SurrCharEnt, o);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001DB09 File Offset: 0x0001BD09
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Base64, XmlEventCache.ToBytes(buffer, index, count));
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001DB1B File Offset: 0x0001BD1B
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.AddEvent(XmlEventCache.XmlEventType.BinHex, XmlEventCache.ToBytes(buffer, index, count));
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001DB2D File Offset: 0x0001BD2D
		public override void Close()
		{
			this.AddEvent(XmlEventCache.XmlEventType.Close);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001DB37 File Offset: 0x0001BD37
		public override void Flush()
		{
			this.AddEvent(XmlEventCache.XmlEventType.Flush);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001DB41 File Offset: 0x0001BD41
		public override void WriteValue(object value)
		{
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, this.resolver));
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001DB5A File Offset: 0x0001BD5A
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001DB64 File Offset: 0x0001BD64
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.AddEvent(XmlEventCache.XmlEventType.Dispose);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001DB98 File Offset: 0x0001BD98
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.AddEvent(XmlEventCache.XmlEventType.XmlDecl1, standalone);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.AddEvent(XmlEventCache.XmlEventType.XmlDecl2, xmldecl);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001DBB3 File Offset: 0x0001BDB3
		internal override void StartElementContent()
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartContent);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001DBBD File Offset: 0x0001BDBD
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndElem, prefix, localName, ns);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001DBCA File Offset: 0x0001BDCA
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.FullEndElem, prefix, localName, ns);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001DBD7 File Offset: 0x0001BDD7
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Nmsp, prefix, ns);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001DBE3 File Offset: 0x0001BDE3
		internal override void WriteEndBase64()
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndBase64);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001DBF0 File Offset: 0x0001BDF0
		private void AddEvent(XmlEventCache.XmlEventType eventType)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001DC18 File Offset: 0x0001BE18
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001DC40 File Offset: 0x0001BE40
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001DC68 File Offset: 0x0001BE68
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2, s3);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001DC94 File Offset: 0x0001BE94
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3, object o)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2, s3, o);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
		private void AddEvent(XmlEventCache.XmlEventType eventType, object o)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, o);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
		private int NewEvent()
		{
			if (this.pages == null)
			{
				this.pages = new List<XmlEventCache.XmlEvent[]>();
				this.pageCurr = new XmlEventCache.XmlEvent[32];
				this.pages.Add(this.pageCurr);
				if (this.singleText.Count != 0)
				{
					this.pageCurr[0].InitEvent(XmlEventCache.XmlEventType.String, this.singleText.GetResult());
					this.pageSize++;
					this.singleText.Clear();
				}
			}
			else if (this.pageSize >= this.pageCurr.Length)
			{
				this.pageCurr = new XmlEventCache.XmlEvent[this.pageSize * 2];
				this.pages.Add(this.pageCurr);
				this.pageSize = 0;
			}
			int num = this.pageSize;
			this.pageSize = num + 1;
			return num;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		private static byte[] ToBytes(byte[] buffer, int index, int count)
		{
			if (index != 0 || count != buffer.Length)
			{
				if (buffer.Length - index > count)
				{
					count = buffer.Length - index;
				}
				byte[] array = new byte[count];
				Array.Copy(buffer, index, array, 0, count);
				return array;
			}
			return buffer;
		}

		// Token: 0x04000803 RID: 2051
		private List<XmlEventCache.XmlEvent[]> pages;

		// Token: 0x04000804 RID: 2052
		private XmlEventCache.XmlEvent[] pageCurr;

		// Token: 0x04000805 RID: 2053
		private int pageSize;

		// Token: 0x04000806 RID: 2054
		private bool hasRootNode;

		// Token: 0x04000807 RID: 2055
		private StringConcat singleText;

		// Token: 0x04000808 RID: 2056
		private string baseUri;

		// Token: 0x04000809 RID: 2057
		private const int InitialPageSize = 32;

		// Token: 0x02000096 RID: 150
		private enum XmlEventType
		{
			// Token: 0x0400080B RID: 2059
			Unknown,
			// Token: 0x0400080C RID: 2060
			DocType,
			// Token: 0x0400080D RID: 2061
			StartElem,
			// Token: 0x0400080E RID: 2062
			StartAttr,
			// Token: 0x0400080F RID: 2063
			EndAttr,
			// Token: 0x04000810 RID: 2064
			CData,
			// Token: 0x04000811 RID: 2065
			Comment,
			// Token: 0x04000812 RID: 2066
			PI,
			// Token: 0x04000813 RID: 2067
			Whitespace,
			// Token: 0x04000814 RID: 2068
			String,
			// Token: 0x04000815 RID: 2069
			Raw,
			// Token: 0x04000816 RID: 2070
			EntRef,
			// Token: 0x04000817 RID: 2071
			CharEnt,
			// Token: 0x04000818 RID: 2072
			SurrCharEnt,
			// Token: 0x04000819 RID: 2073
			Base64,
			// Token: 0x0400081A RID: 2074
			BinHex,
			// Token: 0x0400081B RID: 2075
			XmlDecl1,
			// Token: 0x0400081C RID: 2076
			XmlDecl2,
			// Token: 0x0400081D RID: 2077
			StartContent,
			// Token: 0x0400081E RID: 2078
			EndElem,
			// Token: 0x0400081F RID: 2079
			FullEndElem,
			// Token: 0x04000820 RID: 2080
			Nmsp,
			// Token: 0x04000821 RID: 2081
			EndBase64,
			// Token: 0x04000822 RID: 2082
			Close,
			// Token: 0x04000823 RID: 2083
			Flush,
			// Token: 0x04000824 RID: 2084
			Dispose
		}

		// Token: 0x02000097 RID: 151
		private struct XmlEvent
		{
			// Token: 0x0600055A RID: 1370 RVA: 0x0001DDF1 File Offset: 0x0001BFF1
			public void InitEvent(XmlEventCache.XmlEventType eventType)
			{
				this.eventType = eventType;
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x0001DDFA File Offset: 0x0001BFFA
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1)
			{
				this.eventType = eventType;
				this.s1 = s1;
			}

			// Token: 0x0600055C RID: 1372 RVA: 0x0001DE0A File Offset: 0x0001C00A
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
			}

			// Token: 0x0600055D RID: 1373 RVA: 0x0001DE21 File Offset: 0x0001C021
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
				this.s3 = s3;
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x0001DE40 File Offset: 0x0001C040
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3, object o)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
				this.s3 = s3;
				this.o = o;
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x0001DE67 File Offset: 0x0001C067
			public void InitEvent(XmlEventCache.XmlEventType eventType, object o)
			{
				this.eventType = eventType;
				this.o = o;
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001DE77 File Offset: 0x0001C077
			public XmlEventCache.XmlEventType EventType
			{
				get
				{
					return this.eventType;
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001DE7F File Offset: 0x0001C07F
			public string String1
			{
				get
				{
					return this.s1;
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001DE87 File Offset: 0x0001C087
			public string String2
			{
				get
				{
					return this.s2;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001DE8F File Offset: 0x0001C08F
			public string String3
			{
				get
				{
					return this.s3;
				}
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001DE97 File Offset: 0x0001C097
			public object Object
			{
				get
				{
					return this.o;
				}
			}

			// Token: 0x04000825 RID: 2085
			private XmlEventCache.XmlEventType eventType;

			// Token: 0x04000826 RID: 2086
			private string s1;

			// Token: 0x04000827 RID: 2087
			private string s2;

			// Token: 0x04000828 RID: 2088
			private string s3;

			// Token: 0x04000829 RID: 2089
			private object o;
		}
	}
}
