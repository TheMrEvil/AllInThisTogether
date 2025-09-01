using System;
using System.Collections.Generic;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000417 RID: 1047
	internal class XsltInput : IErrorHelper
	{
		// Token: 0x0600295D RID: 10589 RVA: 0x000F740C File Offset: 0x000F560C
		public XsltInput(XmlReader reader, Compiler compiler, KeywordsTable atoms)
		{
			XsltInput.EnsureExpandEntities(reader);
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			this.atoms = atoms;
			this.reader = reader;
			this.reatomize = (reader.NameTable != atoms.NameTable);
			this.readerLineInfo = ((xmlLineInfo != null && xmlLineInfo.HasLineInfo()) ? xmlLineInfo : null);
			this.topLevelReader = (reader.ReadState == ReadState.Initial);
			this.scopeManager = new CompilerScopeManager<VarPar>(atoms);
			this.compiler = compiler;
			this.nodeType = XmlNodeType.Document;
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x000F74AA File Offset: 0x000F56AA
		public XmlNodeType NodeType
		{
			get
			{
				if (this.nodeType != XmlNodeType.Element || 0 >= this.currentRecord)
				{
					return this.nodeType;
				}
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x000F74C6 File Offset: 0x000F56C6
		public string LocalName
		{
			get
			{
				return this.records[this.currentRecord].localName;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x000F74DE File Offset: 0x000F56DE
		public string NamespaceUri
		{
			get
			{
				return this.records[this.currentRecord].nsUri;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x000F74F6 File Offset: 0x000F56F6
		public string Prefix
		{
			get
			{
				return this.records[this.currentRecord].prefix;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x000F750E File Offset: 0x000F570E
		public string Value
		{
			get
			{
				return this.records[this.currentRecord].value;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x000F7526 File Offset: 0x000F5726
		public string BaseUri
		{
			get
			{
				return this.records[this.currentRecord].baseUri;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x000F753E File Offset: 0x000F573E
		public string QualifiedName
		{
			get
			{
				return this.records[this.currentRecord].QualifiedName;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002965 RID: 10597 RVA: 0x000F7556 File Offset: 0x000F5756
		public bool IsEmptyElement
		{
			get
			{
				return this.isEmptyElement;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x000F7526 File Offset: 0x000F5726
		public string Uri
		{
			get
			{
				return this.records[this.currentRecord].baseUri;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000F755E File Offset: 0x000F575E
		public Location Start
		{
			get
			{
				return this.records[this.currentRecord].start;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x000F7576 File Offset: 0x000F5776
		public Location End
		{
			get
			{
				return this.records[this.currentRecord].end;
			}
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000F7590 File Offset: 0x000F5790
		private static void EnsureExpandEntities(XmlReader reader)
		{
			XmlTextReader xmlTextReader = reader as XmlTextReader;
			if (xmlTextReader != null && xmlTextReader.EntityHandling != EntityHandling.ExpandEntities)
			{
				xmlTextReader.EntityHandling = EntityHandling.ExpandEntities;
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000F75B8 File Offset: 0x000F57B8
		private void ExtendRecordBuffer(int position)
		{
			if (this.records.Length <= position)
			{
				int num = this.records.Length * 2;
				if (num <= position)
				{
					num = position + 1;
				}
				XsltInput.Record[] destinationArray = new XsltInput.Record[num];
				Array.Copy(this.records, destinationArray, this.records.Length);
				this.records = destinationArray;
			}
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000F7608 File Offset: 0x000F5808
		public bool FindStylesheetElement()
		{
			if (!this.topLevelReader && this.reader.ReadState != ReadState.Interactive)
			{
				return false;
			}
			IDictionary<string, string> dictionary = null;
			if (this.reader.ReadState == ReadState.Interactive)
			{
				IXmlNamespaceResolver xmlNamespaceResolver = this.reader as IXmlNamespaceResolver;
				if (xmlNamespaceResolver != null)
				{
					dictionary = xmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
				}
			}
			while (this.MoveToNextSibling() && this.nodeType == XmlNodeType.Whitespace)
			{
			}
			if (this.nodeType == XmlNodeType.Element)
			{
				if (dictionary != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in dictionary)
					{
						if (this.scopeManager.LookupNamespace(keyValuePair.Key) == null)
						{
							string nsUri = this.atoms.NameTable.Add(keyValuePair.Value);
							this.scopeManager.AddNsDeclaration(keyValuePair.Key, nsUri);
							this.ctxInfo.AddNamespace(keyValuePair.Key, nsUri);
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000F7700 File Offset: 0x000F5900
		public void Finish()
		{
			if (this.topLevelReader)
			{
				while (this.reader.ReadState == ReadState.Interactive)
				{
					this.reader.Skip();
				}
			}
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000F7728 File Offset: 0x000F5928
		private void FillupRecord(ref XsltInput.Record rec)
		{
			rec.localName = this.reader.LocalName;
			rec.nsUri = this.reader.NamespaceURI;
			rec.prefix = this.reader.Prefix;
			rec.value = this.reader.Value;
			rec.baseUri = this.reader.BaseURI;
			if (this.reatomize)
			{
				rec.localName = this.atoms.NameTable.Add(rec.localName);
				rec.nsUri = this.atoms.NameTable.Add(rec.nsUri);
				rec.prefix = this.atoms.NameTable.Add(rec.prefix);
			}
			if (this.readerLineInfo != null)
			{
				rec.start = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition - XsltInput.PositionAdjustment(this.reader.NodeType));
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000F7820 File Offset: 0x000F5A20
		private void SetRecordEnd(ref XsltInput.Record rec)
		{
			if (this.readerLineInfo != null)
			{
				rec.end = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition - XsltInput.PositionAdjustment(this.reader.NodeType));
				if (this.reader.BaseURI != rec.baseUri || rec.end.LessOrEqual(rec.start))
				{
					rec.end = new Location(rec.start.Line, int.MaxValue);
				}
			}
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000F78B0 File Offset: 0x000F5AB0
		private void FillupTextRecord(ref XsltInput.Record rec)
		{
			rec.localName = string.Empty;
			rec.nsUri = string.Empty;
			rec.prefix = string.Empty;
			rec.value = this.reader.Value;
			rec.baseUri = this.reader.BaseURI;
			if (this.readerLineInfo != null)
			{
				bool flag = this.reader.NodeType == XmlNodeType.CDATA;
				int num = this.readerLineInfo.LineNumber;
				int num2 = this.readerLineInfo.LinePosition;
				rec.start = new Location(num, num2 - (flag ? 9 : 0));
				char c = ' ';
				string value = rec.value;
				int i = 0;
				while (i < value.Length)
				{
					char c2 = value[i];
					if (c2 != '\n')
					{
						if (c2 == '\r')
						{
							goto IL_B9;
						}
						num2++;
					}
					else if (c != '\r')
					{
						goto IL_B9;
					}
					IL_C5:
					c = c2;
					i++;
					continue;
					IL_B9:
					num++;
					num2 = 1;
					goto IL_C5;
				}
				rec.end = new Location(num, num2 + (flag ? 3 : 0));
			}
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000F79AC File Offset: 0x000F5BAC
		private void FillupCharacterEntityRecord(ref XsltInput.Record rec)
		{
			string localName = this.reader.LocalName;
			rec.localName = string.Empty;
			rec.nsUri = string.Empty;
			rec.prefix = string.Empty;
			rec.baseUri = this.reader.BaseURI;
			if (this.readerLineInfo != null)
			{
				rec.start = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition - 1);
			}
			this.reader.ResolveEntity();
			this.reader.Read();
			rec.value = this.reader.Value;
			this.reader.Read();
			if (this.readerLineInfo != null)
			{
				int lineNumber = this.readerLineInfo.LineNumber;
				int linePosition = this.readerLineInfo.LinePosition;
				rec.end = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition + 1);
			}
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000F7A9C File Offset: 0x000F5C9C
		private bool ReadAttribute(ref XsltInput.Record rec)
		{
			this.FillupRecord(ref rec);
			if (Ref.Equal(rec.prefix, this.atoms.Xmlns))
			{
				string nsUri = this.atoms.NameTable.Add(this.reader.Value);
				if (!Ref.Equal(rec.localName, this.atoms.Xml))
				{
					this.scopeManager.AddNsDeclaration(rec.localName, nsUri);
					this.ctxInfo.AddNamespace(rec.localName, nsUri);
				}
				return false;
			}
			if (rec.prefix.Length == 0 && Ref.Equal(rec.localName, this.atoms.Xmlns))
			{
				string nsUri2 = this.atoms.NameTable.Add(this.reader.Value);
				this.scopeManager.AddNsDeclaration(string.Empty, nsUri2);
				this.ctxInfo.AddNamespace(string.Empty, nsUri2);
				return false;
			}
			if (!this.reader.ReadAttributeValue())
			{
				rec.value = string.Empty;
				this.SetRecordEnd(ref rec);
				return true;
			}
			if (this.readerLineInfo != null)
			{
				int num = (this.reader.NodeType == XmlNodeType.EntityReference) ? -2 : -1;
				rec.valueStart = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition + num);
				if (this.reader.BaseURI != rec.baseUri || rec.valueStart.LessOrEqual(rec.start))
				{
					int num2 = ((rec.prefix.Length != 0) ? (rec.prefix.Length + 1) : 0) + rec.localName.Length;
					rec.end = new Location(rec.start.Line, rec.start.Pos + num2 + 1);
				}
			}
			string text = string.Empty;
			this.strConcat.Clear();
			do
			{
				XmlNodeType xmlNodeType = this.reader.NodeType;
				if (xmlNodeType != XmlNodeType.EntityReference)
				{
					if (xmlNodeType != XmlNodeType.EndEntity)
					{
						text = this.reader.Value;
						this.strConcat.Concat(text);
					}
				}
				else
				{
					this.reader.ResolveEntity();
				}
			}
			while (this.reader.ReadAttributeValue());
			rec.value = this.strConcat.GetResult();
			if (this.readerLineInfo != null)
			{
				int num3 = ((this.reader.NodeType == XmlNodeType.EndEntity) ? 1 : text.Length) + 1;
				rec.end = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition + num3);
				if (this.reader.BaseURI != rec.baseUri || rec.end.LessOrEqual(rec.valueStart))
				{
					rec.end = new Location(rec.start.Line, int.MaxValue);
				}
			}
			return true;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000F7D66 File Offset: 0x000F5F66
		public bool MoveToFirstChild()
		{
			return !this.IsEmptyElement && this.ReadNextSibling();
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000F7D78 File Offset: 0x000F5F78
		public bool MoveToNextSibling()
		{
			if (this.nodeType == XmlNodeType.Element || this.nodeType == XmlNodeType.EndElement)
			{
				this.scopeManager.ExitScope();
			}
			return this.ReadNextSibling();
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000F7D9E File Offset: 0x000F5F9E
		public void SkipNode()
		{
			if (this.nodeType == XmlNodeType.Element && this.MoveToFirstChild())
			{
				do
				{
					this.SkipNode();
				}
				while (this.MoveToNextSibling());
			}
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000F7DC0 File Offset: 0x000F5FC0
		private int ReadTextNodes()
		{
			bool flag = this.reader.XmlSpace == XmlSpace.Preserve;
			bool flag2 = true;
			int num = 0;
			for (;;)
			{
				XmlNodeType xmlNodeType = this.reader.NodeType;
				if (xmlNodeType <= XmlNodeType.EntityReference)
				{
					if (xmlNodeType - XmlNodeType.Text > 1)
					{
						if (xmlNodeType != XmlNodeType.EntityReference)
						{
							break;
						}
						string localName = this.reader.LocalName;
						if (localName.Length > 0 && (localName[0] == '#' || localName == "lt" || localName == "gt" || localName == "quot" || localName == "apos"))
						{
							this.ExtendRecordBuffer(num);
							this.FillupCharacterEntityRecord(ref this.records[num]);
							if (flag2 && !XmlCharType.Instance.IsOnlyWhitespace(this.records[num].value))
							{
								flag2 = false;
							}
							num++;
							continue;
						}
						this.reader.ResolveEntity();
						this.reader.Read();
						continue;
					}
					else if (flag2 && !XmlCharType.Instance.IsOnlyWhitespace(this.reader.Value))
					{
						flag2 = false;
					}
				}
				else if (xmlNodeType - XmlNodeType.Whitespace > 1)
				{
					if (xmlNodeType != XmlNodeType.EndEntity)
					{
						break;
					}
					this.reader.Read();
					continue;
				}
				this.ExtendRecordBuffer(num);
				this.FillupTextRecord(ref this.records[num]);
				this.reader.Read();
				num++;
			}
			this.nodeType = ((!flag2) ? XmlNodeType.Text : (flag ? XmlNodeType.SignificantWhitespace : XmlNodeType.Whitespace));
			return num;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000F7F4C File Offset: 0x000F614C
		private bool ReadNextSibling()
		{
			if (this.currentRecord < this.lastTextNode)
			{
				this.currentRecord++;
				if (this.currentRecord == this.lastTextNode)
				{
					this.lastTextNode = 0;
				}
				return true;
			}
			this.currentRecord = 0;
			while (!this.reader.EOF)
			{
				XmlNodeType xmlNodeType = this.reader.NodeType;
				if (xmlNodeType <= XmlNodeType.EntityReference)
				{
					if (xmlNodeType == XmlNodeType.Element)
					{
						this.scopeManager.EnterScope();
						this.numAttributes = this.ReadElement();
						return true;
					}
					if (xmlNodeType - XmlNodeType.Text > 2)
					{
						goto IL_D8;
					}
				}
				else if (xmlNodeType - XmlNodeType.Whitespace > 1)
				{
					if (xmlNodeType != XmlNodeType.EndElement)
					{
						goto IL_D8;
					}
					this.nodeType = XmlNodeType.EndElement;
					this.isEmptyElement = false;
					this.FillupRecord(ref this.records[0]);
					this.reader.Read();
					this.SetRecordEnd(ref this.records[0]);
					return false;
				}
				int num = this.ReadTextNodes();
				if (num != 0)
				{
					this.lastTextNode = num - 1;
					return true;
				}
				continue;
				IL_D8:
				this.reader.Read();
			}
			return false;
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000F8050 File Offset: 0x000F6250
		private int ReadElement()
		{
			this.attributesRead = false;
			this.FillupRecord(ref this.records[0]);
			this.nodeType = XmlNodeType.Element;
			this.isEmptyElement = this.reader.IsEmptyElement;
			this.ctxInfo = new XsltInput.ContextInfo(this);
			int num = 1;
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					this.ExtendRecordBuffer(num);
					if (this.ReadAttribute(ref this.records[num]))
					{
						num++;
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
			this.reader.Read();
			this.SetRecordEnd(ref this.records[0]);
			this.ctxInfo.lineInfo = this.BuildLineInfo();
			this.attributes = null;
			return num - 1;
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000F811A File Offset: 0x000F631A
		public void MoveToElement()
		{
			this.currentRecord = 0;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000F8123 File Offset: 0x000F6323
		private bool MoveToAttributeBase(int attNum)
		{
			if (0 < attNum && attNum <= this.numAttributes)
			{
				this.currentRecord = attNum;
				return true;
			}
			this.currentRecord = 0;
			return false;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000F8123 File Offset: 0x000F6323
		public bool MoveToLiteralAttribute(int attNum)
		{
			if (0 < attNum && attNum <= this.numAttributes)
			{
				this.currentRecord = attNum;
				return true;
			}
			this.currentRecord = 0;
			return false;
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000F8143 File Offset: 0x000F6343
		public bool MoveToXsltAttribute(int attNum, string attName)
		{
			this.currentRecord = this.xsltAttributeNumber[attNum];
			return this.currentRecord != 0;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000F815C File Offset: 0x000F635C
		public bool IsRequiredAttribute(int attNum)
		{
			return (this.attributes[attNum].flags & ((this.compiler.Version == 2) ? XsltLoader.V2Req : XsltLoader.V1Req)) != 0;
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000F818D File Offset: 0x000F638D
		public bool AttributeExists(int attNum, string attName)
		{
			return this.xsltAttributeNumber[attNum] != 0;
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000F819A File Offset: 0x000F639A
		public XsltInput.DelayedQName ElementName
		{
			get
			{
				return new XsltInput.DelayedQName(ref this.records[0]);
			}
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000F81AD File Offset: 0x000F63AD
		public bool IsNs(string ns)
		{
			return Ref.Equal(ns, this.NamespaceUri);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000F81BB File Offset: 0x000F63BB
		public bool IsKeyword(string kwd)
		{
			return Ref.Equal(kwd, this.LocalName);
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000F81C9 File Offset: 0x000F63C9
		public bool IsXsltNamespace()
		{
			return this.IsNs(this.atoms.UriXsl);
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000F81DC File Offset: 0x000F63DC
		public bool IsNullNamespace()
		{
			return this.IsNs(string.Empty);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000F81E9 File Offset: 0x000F63E9
		public bool IsXsltAttribute(string kwd)
		{
			return this.IsKeyword(kwd) && this.IsNullNamespace();
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000F81FC File Offset: 0x000F63FC
		public bool IsXsltKeyword(string kwd)
		{
			return this.IsKeyword(kwd) && this.IsXsltNamespace();
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x000F820F File Offset: 0x000F640F
		// (set) Token: 0x06002986 RID: 10630 RVA: 0x000F821C File Offset: 0x000F641C
		public bool CanHaveApplyImports
		{
			get
			{
				return this.scopeManager.CanHaveApplyImports;
			}
			set
			{
				this.scopeManager.CanHaveApplyImports = value;
			}
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000F822A File Offset: 0x000F642A
		public bool IsExtensionNamespace(string uri)
		{
			return this.scopeManager.IsExNamespace(uri);
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000F8238 File Offset: 0x000F6438
		public bool ForwardCompatibility
		{
			get
			{
				return this.scopeManager.ForwardCompatibility;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000F8245 File Offset: 0x000F6445
		public bool BackwardCompatibility
		{
			get
			{
				return this.scopeManager.BackwardCompatibility;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x000F8252 File Offset: 0x000F6452
		public XslVersion XslVersion
		{
			get
			{
				if (!this.scopeManager.ForwardCompatibility)
				{
					return XslVersion.Version10;
				}
				return XslVersion.ForwardsCompatible;
			}
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000F8264 File Offset: 0x000F6464
		private void SetVersion(int attVersion)
		{
			this.MoveToLiteralAttribute(attVersion);
			double num = XPathConvert.StringToDouble(this.Value);
			if (double.IsNaN(num))
			{
				this.ReportError("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					this.atoms.Version,
					this.Value
				});
				num = 1.0;
			}
			this.SetVersion(num);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000F82C8 File Offset: 0x000F64C8
		private void SetVersion(double version)
		{
			if (this.compiler.Version == 0)
			{
				this.compiler.Version = 1;
			}
			if (this.compiler.Version == 1)
			{
				this.scopeManager.BackwardCompatibility = false;
				this.scopeManager.ForwardCompatibility = (version != 1.0);
				return;
			}
			this.scopeManager.BackwardCompatibility = (version < 2.0);
			this.scopeManager.ForwardCompatibility = (2.0 < version);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000F8351 File Offset: 0x000F6551
		public XsltInput.ContextInfo GetAttributes()
		{
			return this.GetAttributes(XsltInput.noAttributes);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000F8360 File Offset: 0x000F6560
		public XsltInput.ContextInfo GetAttributes(XsltInput.XsltAttribute[] attributes)
		{
			this.attributes = attributes;
			this.records[0].value = null;
			int attExPrefixes = 0;
			int attExPrefixes2 = 0;
			int xpathDefaultNamespace = 0;
			int defaultCollation = 0;
			int num = 0;
			bool flag = this.IsXsltNamespace() && this.IsKeyword(this.atoms.Output);
			bool flag2 = this.IsXsltNamespace() && (this.IsKeyword(this.atoms.Stylesheet) || this.IsKeyword(this.atoms.Transform));
			bool flag3 = this.compiler.Version == 2;
			for (int i = 0; i < attributes.Length; i++)
			{
				this.xsltAttributeNumber[i] = 0;
			}
			this.compiler.EnterForwardsCompatible();
			if (flag2 || (flag3 && !flag))
			{
				int num2 = 1;
				while (this.MoveToAttributeBase(num2))
				{
					if (this.IsNullNamespace() && this.IsKeyword(this.atoms.Version))
					{
						this.SetVersion(num2);
						break;
					}
					num2++;
				}
			}
			if (this.compiler.Version == 0)
			{
				this.SetVersion(1.0);
			}
			flag3 = (this.compiler.Version == 2);
			int num3 = flag3 ? (XsltLoader.V2Opt | XsltLoader.V2Req) : (XsltLoader.V1Opt | XsltLoader.V1Req);
			int num4 = 1;
			while (this.MoveToAttributeBase(num4))
			{
				if (this.IsNullNamespace())
				{
					string localName = this.LocalName;
					int j;
					for (j = 0; j < attributes.Length; j++)
					{
						if (Ref.Equal(localName, attributes[j].name) && (attributes[j].flags & num3) != 0)
						{
							this.xsltAttributeNumber[j] = num4;
							break;
						}
					}
					if (j == attributes.Length)
					{
						if (Ref.Equal(localName, this.atoms.ExcludeResultPrefixes) && (flag2 || flag3))
						{
							attExPrefixes2 = num4;
						}
						else if (Ref.Equal(localName, this.atoms.ExtensionElementPrefixes) && (flag2 || flag3))
						{
							attExPrefixes = num4;
						}
						else if (Ref.Equal(localName, this.atoms.XPathDefaultNamespace) && flag3)
						{
							xpathDefaultNamespace = num4;
						}
						else if (Ref.Equal(localName, this.atoms.DefaultCollation) && flag3)
						{
							defaultCollation = num4;
						}
						else if (Ref.Equal(localName, this.atoms.UseWhen) && flag3)
						{
							num = num4;
						}
						else
						{
							this.ReportError("'{0}' is an invalid attribute for the '{1}' element.", new string[]
							{
								this.QualifiedName,
								this.records[0].QualifiedName
							});
						}
					}
				}
				else if (this.IsXsltNamespace())
				{
					this.ReportError("'{0}' is an invalid attribute for the '{1}' element.", new string[]
					{
						this.QualifiedName,
						this.records[0].QualifiedName
					});
				}
				num4++;
			}
			this.attributesRead = true;
			this.compiler.ExitForwardsCompatible(this.ForwardCompatibility);
			this.InsertExNamespaces(attExPrefixes, this.ctxInfo, true);
			this.InsertExNamespaces(attExPrefixes2, this.ctxInfo, false);
			this.SetXPathDefaultNamespace(xpathDefaultNamespace);
			this.SetDefaultCollation(defaultCollation);
			if (num != 0)
			{
				this.ReportNYI(this.atoms.UseWhen);
			}
			this.MoveToElement();
			for (int k = 0; k < attributes.Length; k++)
			{
				if (this.xsltAttributeNumber[k] == 0)
				{
					int flags = attributes[k].flags;
					if ((this.compiler.Version == 2 && (flags & XsltLoader.V2Req) != 0) || (this.compiler.Version == 1 && (flags & XsltLoader.V1Req) != 0 && (!this.ForwardCompatibility || (flags & XsltLoader.V2Req) != 0)))
					{
						this.ReportError("Missing mandatory attribute '{0}'.", new string[]
						{
							attributes[k].name
						});
					}
				}
			}
			return this.ctxInfo;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000F872C File Offset: 0x000F692C
		public XsltInput.ContextInfo GetLiteralAttributes(bool asStylesheet)
		{
			int num = 0;
			int attExPrefixes = 0;
			int attExPrefixes2 = 0;
			int xpathDefaultNamespace = 0;
			int defaultCollation = 0;
			int num2 = 0;
			int num3 = 1;
			while (this.MoveToLiteralAttribute(num3))
			{
				if (this.IsXsltNamespace())
				{
					string localName = this.LocalName;
					if (Ref.Equal(localName, this.atoms.Version))
					{
						num = num3;
					}
					else if (Ref.Equal(localName, this.atoms.ExtensionElementPrefixes))
					{
						attExPrefixes = num3;
					}
					else if (Ref.Equal(localName, this.atoms.ExcludeResultPrefixes))
					{
						attExPrefixes2 = num3;
					}
					else if (Ref.Equal(localName, this.atoms.XPathDefaultNamespace))
					{
						xpathDefaultNamespace = num3;
					}
					else if (Ref.Equal(localName, this.atoms.DefaultCollation))
					{
						defaultCollation = num3;
					}
					else if (Ref.Equal(localName, this.atoms.UseWhen))
					{
						num2 = num3;
					}
				}
				num3++;
			}
			this.attributesRead = true;
			this.MoveToElement();
			if (num != 0)
			{
				this.SetVersion(num);
			}
			else if (asStylesheet)
			{
				this.ReportError((Ref.Equal(this.NamespaceUri, this.atoms.UriWdXsl) && Ref.Equal(this.LocalName, this.atoms.Stylesheet)) ? "The 'http://www.w3.org/TR/WD-xsl' namespace is no longer supported." : "Stylesheet must start either with an 'xsl:stylesheet' or an 'xsl:transform' element, or with a literal result element that has an 'xsl:version' attribute, where prefix 'xsl' denotes the 'http://www.w3.org/1999/XSL/Transform' namespace.", Array.Empty<string>());
				this.SetVersion(1.0);
			}
			this.InsertExNamespaces(attExPrefixes, this.ctxInfo, true);
			if (!this.IsExtensionNamespace(this.records[0].nsUri))
			{
				if (this.compiler.Version == 2)
				{
					this.SetXPathDefaultNamespace(xpathDefaultNamespace);
					this.SetDefaultCollation(defaultCollation);
					if (num2 != 0)
					{
						this.ReportNYI(this.atoms.UseWhen);
					}
				}
				this.InsertExNamespaces(attExPrefixes2, this.ctxInfo, false);
			}
			return this.ctxInfo;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000F88EC File Offset: 0x000F6AEC
		public void GetVersionAttribute()
		{
			if (this.compiler.Version == 2)
			{
				int num = 1;
				while (this.MoveToAttributeBase(num))
				{
					if (this.IsNullNamespace() && this.IsKeyword(this.atoms.Version))
					{
						this.SetVersion(num);
						break;
					}
					num++;
				}
			}
			this.attributesRead = true;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000F8948 File Offset: 0x000F6B48
		private void InsertExNamespaces(int attExPrefixes, XsltInput.ContextInfo ctxInfo, bool extensions)
		{
			if (this.MoveToLiteralAttribute(attExPrefixes))
			{
				string value = this.Value;
				if (value.Length != 0)
				{
					if (!extensions && this.compiler.Version != 1 && value == "#all")
					{
						ctxInfo.nsList = new NsDecl(ctxInfo.nsList, null, null);
						return;
					}
					this.compiler.EnterForwardsCompatible();
					string[] array = XmlConvert.SplitString(value);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] == "#default")
						{
							array[i] = this.LookupXmlNamespace(string.Empty);
							if (array[i].Length == 0 && this.compiler.Version != 1 && !this.BackwardCompatibility)
							{
								this.ReportError("Value '#default' is used within the 'exclude-result-prefixes' attribute and the parent element of this attribute has no default namespace.", Array.Empty<string>());
							}
						}
						else
						{
							array[i] = this.LookupXmlNamespace(array[i]);
						}
					}
					if (!this.compiler.ExitForwardsCompatible(this.ForwardCompatibility))
					{
						return;
					}
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j] != null)
						{
							ctxInfo.nsList = new NsDecl(ctxInfo.nsList, null, array[j]);
							if (extensions)
							{
								this.scopeManager.AddExNamespace(array[j]);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000F8A6D File Offset: 0x000F6C6D
		private void SetXPathDefaultNamespace(int attNamespace)
		{
			if (this.MoveToLiteralAttribute(attNamespace) && this.Value.Length != 0)
			{
				this.ReportNYI(this.atoms.XPathDefaultNamespace);
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000F8A98 File Offset: 0x000F6C98
		private void SetDefaultCollation(int attCollation)
		{
			if (this.MoveToLiteralAttribute(attCollation))
			{
				string[] array = XmlConvert.SplitString(this.Value);
				int num = 0;
				while (num < array.Length && XmlCollation.Create(array[num], false) == null)
				{
					num++;
				}
				if (num == array.Length)
				{
					this.ReportErrorFC("The value of an 'default-collation' attribute contains no recognized collation URI.", Array.Empty<string>());
					return;
				}
				if (array[num] != "http://www.w3.org/2004/10/xpath-functions/collation/codepoint")
				{
					this.ReportNYI(this.atoms.DefaultCollation);
				}
			}
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000F8B0A File Offset: 0x000F6D0A
		private static int PositionAdjustment(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.Element:
				return 1;
			case XmlNodeType.Attribute:
			case XmlNodeType.Text:
			case XmlNodeType.Entity:
				break;
			case XmlNodeType.CDATA:
				return 9;
			case XmlNodeType.EntityReference:
				return 1;
			case XmlNodeType.ProcessingInstruction:
				return 2;
			case XmlNodeType.Comment:
				return 4;
			default:
				if (nt == XmlNodeType.EndElement)
				{
					return 2;
				}
				break;
			}
			return 0;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000F8B49 File Offset: 0x000F6D49
		public ISourceLineInfo BuildLineInfo()
		{
			return new SourceLineInfo(this.Uri, this.Start, this.End);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000F8B64 File Offset: 0x000F6D64
		public ISourceLineInfo BuildNameLineInfo()
		{
			if (this.readerLineInfo == null)
			{
				return this.BuildLineInfo();
			}
			if (this.LocalName == null)
			{
				this.FillupRecord(ref this.records[this.currentRecord]);
			}
			Location start = this.Start;
			int line = start.Line;
			int num = start.Pos + XsltInput.PositionAdjustment(this.NodeType);
			return new SourceLineInfo(this.Uri, new Location(line, num), new Location(line, num + this.QualifiedName.Length));
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000F8BE8 File Offset: 0x000F6DE8
		public ISourceLineInfo BuildReaderLineInfo()
		{
			Location location;
			if (this.readerLineInfo != null)
			{
				location = new Location(this.readerLineInfo.LineNumber, this.readerLineInfo.LinePosition);
			}
			else
			{
				location = new Location(0, 0);
			}
			return new SourceLineInfo(this.reader.BaseURI, location, location);
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000F8C38 File Offset: 0x000F6E38
		public string LookupXmlNamespace(string prefix)
		{
			string text = this.scopeManager.LookupNamespace(prefix);
			if (text != null)
			{
				return text;
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			this.ReportError("Prefix '{0}' is not defined.", new string[]
			{
				prefix
			});
			return null;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000F8C7B File Offset: 0x000F6E7B
		public void ReportError(string res, params string[] args)
		{
			this.compiler.ReportError(this.BuildNameLineInfo(), res, args);
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000F8C90 File Offset: 0x000F6E90
		public void ReportErrorFC(string res, params string[] args)
		{
			if (!this.ForwardCompatibility)
			{
				this.compiler.ReportError(this.BuildNameLineInfo(), res, args);
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000F8CAD File Offset: 0x000F6EAD
		public void ReportWarning(string res, params string[] args)
		{
			this.compiler.ReportWarning(this.BuildNameLineInfo(), res, args);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000F8CC2 File Offset: 0x000F6EC2
		private void ReportNYI(string arg)
		{
			this.ReportErrorFC("'{0}' is not yet implemented.", new string[]
			{
				arg
			});
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000F8CD9 File Offset: 0x000F6ED9
		// Note: this type is marked as 'beforefieldinit'.
		static XsltInput()
		{
		}

		// Token: 0x04002095 RID: 8341
		private const int InitRecordsSize = 22;

		// Token: 0x04002096 RID: 8342
		private XmlReader reader;

		// Token: 0x04002097 RID: 8343
		private IXmlLineInfo readerLineInfo;

		// Token: 0x04002098 RID: 8344
		private bool topLevelReader;

		// Token: 0x04002099 RID: 8345
		private CompilerScopeManager<VarPar> scopeManager;

		// Token: 0x0400209A RID: 8346
		private KeywordsTable atoms;

		// Token: 0x0400209B RID: 8347
		private Compiler compiler;

		// Token: 0x0400209C RID: 8348
		private bool reatomize;

		// Token: 0x0400209D RID: 8349
		private XmlNodeType nodeType;

		// Token: 0x0400209E RID: 8350
		private XsltInput.Record[] records = new XsltInput.Record[22];

		// Token: 0x0400209F RID: 8351
		private int currentRecord;

		// Token: 0x040020A0 RID: 8352
		private bool isEmptyElement;

		// Token: 0x040020A1 RID: 8353
		private int lastTextNode;

		// Token: 0x040020A2 RID: 8354
		private int numAttributes;

		// Token: 0x040020A3 RID: 8355
		private XsltInput.ContextInfo ctxInfo;

		// Token: 0x040020A4 RID: 8356
		private bool attributesRead;

		// Token: 0x040020A5 RID: 8357
		private StringConcat strConcat;

		// Token: 0x040020A6 RID: 8358
		private XsltInput.XsltAttribute[] attributes;

		// Token: 0x040020A7 RID: 8359
		private int[] xsltAttributeNumber = new int[21];

		// Token: 0x040020A8 RID: 8360
		private static XsltInput.XsltAttribute[] noAttributes = new XsltInput.XsltAttribute[0];

		// Token: 0x02000418 RID: 1048
		public struct DelayedQName
		{
			// Token: 0x0600299E RID: 10654 RVA: 0x000F8CE6 File Offset: 0x000F6EE6
			public DelayedQName(ref XsltInput.Record rec)
			{
				this.prefix = rec.prefix;
				this.localName = rec.localName;
			}

			// Token: 0x0600299F RID: 10655 RVA: 0x000F8D00 File Offset: 0x000F6F00
			public static implicit operator string(XsltInput.DelayedQName qn)
			{
				if (qn.prefix.Length != 0)
				{
					return qn.prefix + ":" + qn.localName;
				}
				return qn.localName;
			}

			// Token: 0x040020A9 RID: 8361
			private string prefix;

			// Token: 0x040020AA RID: 8362
			private string localName;
		}

		// Token: 0x02000419 RID: 1049
		public struct XsltAttribute
		{
			// Token: 0x060029A0 RID: 10656 RVA: 0x000F8D2C File Offset: 0x000F6F2C
			public XsltAttribute(string name, int flags)
			{
				this.name = name;
				this.flags = flags;
			}

			// Token: 0x040020AB RID: 8363
			public string name;

			// Token: 0x040020AC RID: 8364
			public int flags;
		}

		// Token: 0x0200041A RID: 1050
		internal class ContextInfo
		{
			// Token: 0x060029A1 RID: 10657 RVA: 0x000F8D3C File Offset: 0x000F6F3C
			internal ContextInfo(ISourceLineInfo lineinfo)
			{
				this.elemNameLi = lineinfo;
				this.endTagLi = lineinfo;
				this.lineInfo = lineinfo;
			}

			// Token: 0x060029A2 RID: 10658 RVA: 0x000F8D59 File Offset: 0x000F6F59
			public ContextInfo(XsltInput input)
			{
				this.elemNameLength = input.QualifiedName.Length;
			}

			// Token: 0x060029A3 RID: 10659 RVA: 0x000F8D72 File Offset: 0x000F6F72
			public void AddNamespace(string prefix, string nsUri)
			{
				this.nsList = new NsDecl(this.nsList, prefix, nsUri);
			}

			// Token: 0x060029A4 RID: 10660 RVA: 0x000F8D88 File Offset: 0x000F6F88
			public void SaveExtendedLineInfo(XsltInput input)
			{
				if (this.lineInfo.Start.Line == 0)
				{
					this.elemNameLi = (this.endTagLi = null);
					return;
				}
				this.elemNameLi = new SourceLineInfo(this.lineInfo.Uri, this.lineInfo.Start.Line, this.lineInfo.Start.Pos + 1, this.lineInfo.Start.Line, this.lineInfo.Start.Pos + 1 + this.elemNameLength);
				if (!input.IsEmptyElement)
				{
					this.endTagLi = input.BuildLineInfo();
					return;
				}
				this.endTagLi = new XsltInput.ContextInfo.EmptyElementEndTag(this.lineInfo);
			}

			// Token: 0x040020AD RID: 8365
			public NsDecl nsList;

			// Token: 0x040020AE RID: 8366
			public ISourceLineInfo lineInfo;

			// Token: 0x040020AF RID: 8367
			public ISourceLineInfo elemNameLi;

			// Token: 0x040020B0 RID: 8368
			public ISourceLineInfo endTagLi;

			// Token: 0x040020B1 RID: 8369
			private int elemNameLength;

			// Token: 0x0200041B RID: 1051
			internal class EmptyElementEndTag : ISourceLineInfo
			{
				// Token: 0x060029A5 RID: 10661 RVA: 0x000F8E4E File Offset: 0x000F704E
				public EmptyElementEndTag(ISourceLineInfo elementTagLi)
				{
					this.elementTagLi = elementTagLi;
				}

				// Token: 0x170007EE RID: 2030
				// (get) Token: 0x060029A6 RID: 10662 RVA: 0x000F8E5D File Offset: 0x000F705D
				public string Uri
				{
					get
					{
						return this.elementTagLi.Uri;
					}
				}

				// Token: 0x170007EF RID: 2031
				// (get) Token: 0x060029A7 RID: 10663 RVA: 0x000F8E6A File Offset: 0x000F706A
				public bool IsNoSource
				{
					get
					{
						return this.elementTagLi.IsNoSource;
					}
				}

				// Token: 0x170007F0 RID: 2032
				// (get) Token: 0x060029A8 RID: 10664 RVA: 0x000F8E78 File Offset: 0x000F7078
				public Location Start
				{
					get
					{
						return new Location(this.elementTagLi.End.Line, this.elementTagLi.End.Pos - 2);
					}
				}

				// Token: 0x170007F1 RID: 2033
				// (get) Token: 0x060029A9 RID: 10665 RVA: 0x000F8EB2 File Offset: 0x000F70B2
				public Location End
				{
					get
					{
						return this.elementTagLi.End;
					}
				}

				// Token: 0x040020B2 RID: 8370
				private ISourceLineInfo elementTagLi;
			}
		}

		// Token: 0x0200041C RID: 1052
		internal struct Record
		{
			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x060029AA RID: 10666 RVA: 0x000F8EBF File Offset: 0x000F70BF
			public string QualifiedName
			{
				get
				{
					if (this.prefix.Length != 0)
					{
						return this.prefix + ":" + this.localName;
					}
					return this.localName;
				}
			}

			// Token: 0x040020B3 RID: 8371
			public string localName;

			// Token: 0x040020B4 RID: 8372
			public string nsUri;

			// Token: 0x040020B5 RID: 8373
			public string prefix;

			// Token: 0x040020B6 RID: 8374
			public string value;

			// Token: 0x040020B7 RID: 8375
			public string baseUri;

			// Token: 0x040020B8 RID: 8376
			public Location start;

			// Token: 0x040020B9 RID: 8377
			public Location valueStart;

			// Token: 0x040020BA RID: 8378
			public Location end;
		}
	}
}
