using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000047 RID: 71
	internal class QueryOutputWriter : XmlRawWriter
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		public QueryOutputWriter(XmlRawWriter writer, XmlWriterSettings settings)
		{
			this.wrapped = writer;
			this.systemId = settings.DocTypeSystem;
			this.publicId = settings.DocTypePublic;
			if (settings.OutputMethod == XmlOutputMethod.Xml)
			{
				if (this.systemId != null)
				{
					this.outputDocType = true;
					this.checkWellFormedDoc = true;
				}
				if (settings.AutoXmlDeclaration && settings.Standalone == XmlStandalone.Yes)
				{
					this.checkWellFormedDoc = true;
				}
				if (settings.CDataSectionElements.Count > 0)
				{
					this.bitsCData = new BitStack();
					this.lookupCDataElems = new Dictionary<XmlQualifiedName, int>();
					this.qnameCData = new XmlQualifiedName();
					foreach (XmlQualifiedName key in settings.CDataSectionElements)
					{
						this.lookupCDataElems[key] = 0;
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
		internal override IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
				this.wrapped.NamespaceResolver = value;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000D3D9 File Offset: 0x0000B5D9
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000D3E7 File Offset: 0x0000B5E7
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000D3F5 File Offset: 0x0000B5F5
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = this.wrapped.Settings;
				settings.ReadOnly = false;
				settings.DocTypeSystem = this.systemId;
				settings.DocTypePublic = this.publicId;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D428 File Offset: 0x0000B628
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.publicId == null && this.systemId == null)
			{
				this.wrapped.WriteDocType(name, pubid, sysid, subset);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D44C File Offset: 0x0000B64C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			if (this.checkWellFormedDoc)
			{
				if (this.depth == 0 && this.hasDocElem)
				{
					throw new XmlException("Document cannot have multiple document elements.", string.Empty);
				}
				this.depth++;
				this.hasDocElem = true;
			}
			if (this.outputDocType)
			{
				this.wrapped.WriteDocType((prefix.Length != 0) ? (prefix + ":" + localName) : localName, this.publicId, this.systemId, null);
				this.outputDocType = false;
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
			if (this.lookupCDataElems != null)
			{
				this.qnameCData.Init(localName, ns);
				this.bitsCData.PushBit(this.lookupCDataElems.ContainsKey(this.qnameCData));
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D519 File Offset: 0x0000B719
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D559 File Offset: 0x0000B759
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D599 File Offset: 0x0000B799
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttr = true;
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D5BD File Offset: 0x0000B7BD
		public override void WriteEndAttribute()
		{
			this.inAttr = false;
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D5D1 File Offset: 0x0000B7D1
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return this.wrapped.SupportsNamespaceDeclarationInChunks;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D5ED File Offset: 0x0000B7ED
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.wrapped.WriteStartNamespaceDeclaration(prefix);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D5FB File Offset: 0x0000B7FB
		internal override void WriteEndNamespaceDeclaration()
		{
			this.wrapped.WriteEndNamespaceDeclaration();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D608 File Offset: 0x0000B808
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D616 File Offset: 0x0000B816
		public override void WriteComment(string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteComment(text);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D62A File Offset: 0x0000B82A
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D63F File Offset: 0x0000B83F
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D672 File Offset: 0x0000B872
		public override void WriteString(string text)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.wrapped.WriteString(text);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D6A5 File Offset: 0x0000B8A5
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D6E1 File Offset: 0x0000B8E1
		public override void WriteEntityRef(string name)
		{
			this.EndCDataSection();
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D6F5 File Offset: 0x0000B8F5
		public override void WriteCharEntity(char ch)
		{
			this.EndCDataSection();
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D709 File Offset: 0x0000B909
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EndCDataSection();
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D71E File Offset: 0x0000B91E
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D75A File Offset: 0x0000B95A
		public override void WriteRaw(string data)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(data);
				return;
			}
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D78D File Offset: 0x0000B98D
		public override void Close()
		{
			this.wrapped.Close();
			if (this.checkWellFormedDoc && !this.hasDocElem)
			{
				throw new XmlException("Document does not have a root element.", string.Empty);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D7BA File Offset: 0x0000B9BA
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D7C7 File Offset: 0x0000B9C7
		private bool StartCDataSection()
		{
			if (this.lookupCDataElems != null && this.bitsCData.PeekBit())
			{
				this.inCDataSection = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D7E8 File Offset: 0x0000B9E8
		private void EndCDataSection()
		{
			this.inCDataSection = false;
		}

		// Token: 0x04000613 RID: 1555
		private XmlRawWriter wrapped;

		// Token: 0x04000614 RID: 1556
		private bool inCDataSection;

		// Token: 0x04000615 RID: 1557
		private Dictionary<XmlQualifiedName, int> lookupCDataElems;

		// Token: 0x04000616 RID: 1558
		private BitStack bitsCData;

		// Token: 0x04000617 RID: 1559
		private XmlQualifiedName qnameCData;

		// Token: 0x04000618 RID: 1560
		private bool outputDocType;

		// Token: 0x04000619 RID: 1561
		private bool checkWellFormedDoc;

		// Token: 0x0400061A RID: 1562
		private bool hasDocElem;

		// Token: 0x0400061B RID: 1563
		private bool inAttr;

		// Token: 0x0400061C RID: 1564
		private string systemId;

		// Token: 0x0400061D RID: 1565
		private string publicId;

		// Token: 0x0400061E RID: 1566
		private int depth;
	}
}
