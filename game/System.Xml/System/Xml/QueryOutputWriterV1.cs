using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000048 RID: 72
	internal class QueryOutputWriterV1 : XmlWriter
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		public QueryOutputWriterV1(XmlWriter writer, XmlWriterSettings settings)
		{
			this.wrapped = writer;
			this.systemId = settings.DocTypeSystem;
			this.publicId = settings.DocTypePublic;
			if (settings.OutputMethod == XmlOutputMethod.Xml)
			{
				bool flag = false;
				if (this.systemId != null)
				{
					flag = true;
					this.outputDocType = true;
				}
				if (settings.Standalone == XmlStandalone.Yes)
				{
					flag = true;
					this.standalone = settings.Standalone;
				}
				if (flag)
				{
					if (settings.Standalone == XmlStandalone.Yes)
					{
						this.wrapped.WriteStartDocument(true);
					}
					else
					{
						this.wrapped.WriteStartDocument();
					}
				}
				if (settings.CDataSectionElements != null && settings.CDataSectionElements.Count > 0)
				{
					this.bitsCData = new BitStack();
					this.lookupCDataElems = new Dictionary<XmlQualifiedName, XmlQualifiedName>();
					this.qnameCData = new XmlQualifiedName();
					foreach (XmlQualifiedName key in settings.CDataSectionElements)
					{
						this.lookupCDataElems[key] = null;
					}
					this.bitsCData.PushBit(false);
					return;
				}
			}
			else if (settings.OutputMethod == XmlOutputMethod.Html && (this.systemId != null || this.publicId != null))
			{
				this.outputDocType = true;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000D938 File Offset: 0x0000BB38
		public override WriteState WriteState
		{
			get
			{
				return this.wrapped.WriteState;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D945 File Offset: 0x0000BB45
		public override void WriteStartDocument()
		{
			this.wrapped.WriteStartDocument();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000D952 File Offset: 0x0000BB52
		public override void WriteStartDocument(bool standalone)
		{
			this.wrapped.WriteStartDocument(standalone);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D960 File Offset: 0x0000BB60
		public override void WriteEndDocument()
		{
			this.wrapped.WriteEndDocument();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D96D File Offset: 0x0000BB6D
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.publicId == null && this.systemId == null)
			{
				this.wrapped.WriteDocType(name, pubid, sysid, subset);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D990 File Offset: 0x0000BB90
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			if (this.outputDocType)
			{
				WriteState writeState = this.wrapped.WriteState;
				if (writeState == WriteState.Start || writeState == WriteState.Prolog)
				{
					this.wrapped.WriteDocType((prefix.Length != 0) ? (prefix + ":" + localName) : localName, this.publicId, this.systemId, null);
				}
				this.outputDocType = false;
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
			if (this.lookupCDataElems != null)
			{
				this.qnameCData.Init(localName, ns);
				this.bitsCData.PushBit(this.lookupCDataElems.ContainsKey(this.qnameCData));
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000DA33 File Offset: 0x0000BC33
		public override void WriteEndElement()
		{
			this.EndCDataSection();
			this.wrapped.WriteEndElement();
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000DA5A File Offset: 0x0000BC5A
		public override void WriteFullEndElement()
		{
			this.EndCDataSection();
			this.wrapped.WriteFullEndElement();
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000DA81 File Offset: 0x0000BC81
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttr = true;
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public override void WriteEndAttribute()
		{
			this.inAttr = false;
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000DABA File Offset: 0x0000BCBA
		public override void WriteComment(string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteComment(text);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000DACE File Offset: 0x0000BCCE
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000DAE3 File Offset: 0x0000BCE3
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000DB16 File Offset: 0x0000BD16
		public override void WriteString(string text)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.wrapped.WriteString(text);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000DB49 File Offset: 0x0000BD49
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000DB85 File Offset: 0x0000BD85
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteBase64(buffer, index, count);
				return;
			}
			this.wrapped.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		public override void WriteEntityRef(string name)
		{
			this.EndCDataSection();
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		public override void WriteCharEntity(char ch)
		{
			this.EndCDataSection();
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EndCDataSection();
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000DBF9 File Offset: 0x0000BDF9
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DC35 File Offset: 0x0000BE35
		public override void WriteRaw(string data)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(data);
				return;
			}
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DC68 File Offset: 0x0000BE68
		public override void Close()
		{
			this.wrapped.Close();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000DC75 File Offset: 0x0000BE75
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000DC82 File Offset: 0x0000BE82
		public override string LookupPrefix(string ns)
		{
			return this.wrapped.LookupPrefix(ns);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000DC90 File Offset: 0x0000BE90
		private bool StartCDataSection()
		{
			if (this.lookupCDataElems != null && this.bitsCData.PeekBit())
			{
				this.inCDataSection = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000DCB1 File Offset: 0x0000BEB1
		private void EndCDataSection()
		{
			this.inCDataSection = false;
		}

		// Token: 0x0400061F RID: 1567
		private XmlWriter wrapped;

		// Token: 0x04000620 RID: 1568
		private bool inCDataSection;

		// Token: 0x04000621 RID: 1569
		private Dictionary<XmlQualifiedName, XmlQualifiedName> lookupCDataElems;

		// Token: 0x04000622 RID: 1570
		private BitStack bitsCData;

		// Token: 0x04000623 RID: 1571
		private XmlQualifiedName qnameCData;

		// Token: 0x04000624 RID: 1572
		private bool outputDocType;

		// Token: 0x04000625 RID: 1573
		private bool inAttr;

		// Token: 0x04000626 RID: 1574
		private string systemId;

		// Token: 0x04000627 RID: 1575
		private string publicId;

		// Token: 0x04000628 RID: 1576
		private XmlStandalone standalone;
	}
}
