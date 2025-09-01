using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Utils;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003A2 RID: 930
	internal class ReaderOutput : XmlReader, RecordOutput
	{
		// Token: 0x060025C7 RID: 9671 RVA: 0x000E40C0 File Offset: 0x000E22C0
		internal ReaderOutput(Processor processor)
		{
			this.processor = processor;
			this.nameTable = processor.NameTable;
			this.Reset();
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x000E40F7 File Offset: 0x000E22F7
		public override XmlNodeType NodeType
		{
			get
			{
				return this.currentInfo.NodeType;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000E4104 File Offset: 0x000E2304
		public override string Name
		{
			get
			{
				string prefix = this.Prefix;
				string localName = this.LocalName;
				if (prefix == null || prefix.Length <= 0)
				{
					return localName;
				}
				if (localName.Length > 0)
				{
					return this.nameTable.Add(prefix + ":" + localName);
				}
				return prefix;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000E414F File Offset: 0x000E234F
		public override string LocalName
		{
			get
			{
				return this.currentInfo.LocalName;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000E415C File Offset: 0x000E235C
		public override string NamespaceURI
		{
			get
			{
				return this.currentInfo.NamespaceURI;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000E4169 File Offset: 0x000E2369
		public override string Prefix
		{
			get
			{
				return this.currentInfo.Prefix;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x0001E50D File Offset: 0x0001C70D
		public override bool HasValue
		{
			get
			{
				return XmlReader.HasValueInternal(this.NodeType);
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x000E4176 File Offset: 0x000E2376
		public override string Value
		{
			get
			{
				return this.currentInfo.Value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x000E4183 File Offset: 0x000E2383
		public override int Depth
		{
			get
			{
				return this.currentInfo.Depth;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string BaseURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000E4190 File Offset: 0x000E2390
		public override bool IsEmptyElement
		{
			get
			{
				return this.currentInfo.IsEmptyTag;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x000E419D File Offset: 0x000E239D
		public override char QuoteChar
		{
			get
			{
				return this.encoder.QuoteChar;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x000E41AA File Offset: 0x000E23AA
		public override XmlSpace XmlSpace
		{
			get
			{
				if (this.manager == null)
				{
					return XmlSpace.None;
				}
				return this.manager.XmlSpace;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000E41C1 File Offset: 0x000E23C1
		public override string XmlLang
		{
			get
			{
				if (this.manager == null)
				{
					return string.Empty;
				}
				return this.manager.XmlLang;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060025D6 RID: 9686 RVA: 0x000E41DC File Offset: 0x000E23DC
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000E41E4 File Offset: 0x000E23E4
		public override string GetAttribute(string name)
		{
			int index;
			if (this.FindAttribute(name, out index))
			{
				return ((BuilderInfo)this.attributeList[index]).Value;
			}
			return null;
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000E4214 File Offset: 0x000E2414
		public override string GetAttribute(string localName, string namespaceURI)
		{
			int index;
			if (this.FindAttribute(localName, namespaceURI, out index))
			{
				return ((BuilderInfo)this.attributeList[index]).Value;
			}
			return null;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000E4245 File Offset: 0x000E2445
		public override string GetAttribute(int i)
		{
			return this.GetBuilderInfo(i).Value;
		}

		// Token: 0x1700077C RID: 1916
		public override string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		// Token: 0x1700077D RID: 1917
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x1700077E RID: 1918
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000E4254 File Offset: 0x000E2454
		public override bool MoveToAttribute(string name)
		{
			int attribute;
			if (this.FindAttribute(name, out attribute))
			{
				this.SetAttribute(attribute);
				return true;
			}
			return false;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000E4278 File Offset: 0x000E2478
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			int attribute;
			if (this.FindAttribute(localName, namespaceURI, out attribute))
			{
				this.SetAttribute(attribute);
				return true;
			}
			return false;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000E429B File Offset: 0x000E249B
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || this.attributeCount <= i)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.SetAttribute(i);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000E42BC File Offset: 0x000E24BC
		public override bool MoveToFirstAttribute()
		{
			if (this.attributeCount <= 0)
			{
				return false;
			}
			this.SetAttribute(0);
			return true;
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000E42D1 File Offset: 0x000E24D1
		public override bool MoveToNextAttribute()
		{
			if (this.currentIndex + 1 < this.attributeCount)
			{
				this.SetAttribute(this.currentIndex + 1);
				return true;
			}
			return false;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000E42F4 File Offset: 0x000E24F4
		public override bool MoveToElement()
		{
			if (this.NodeType == XmlNodeType.Attribute || this.currentInfo == this.attributeValue)
			{
				this.SetMainNode();
				return true;
			}
			return false;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000E4318 File Offset: 0x000E2518
		public override bool Read()
		{
			if (this.state != ReadState.Interactive)
			{
				if (this.state != ReadState.Initial)
				{
					return false;
				}
				this.state = ReadState.Interactive;
			}
			for (;;)
			{
				if (this.haveRecord)
				{
					this.processor.ResetOutput();
					this.haveRecord = false;
				}
				this.processor.Execute();
				if (!this.haveRecord)
				{
					goto IL_A0;
				}
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Text)
				{
					if (nodeType != XmlNodeType.Whitespace)
					{
						break;
					}
				}
				else
				{
					if (!this.xmlCharType.IsOnlyWhitespace(this.Value))
					{
						break;
					}
					this.currentInfo.NodeType = XmlNodeType.Whitespace;
				}
				if (this.Value.Length != 0)
				{
					goto Block_8;
				}
			}
			goto IL_AD;
			Block_8:
			if (this.XmlSpace == XmlSpace.Preserve)
			{
				this.currentInfo.NodeType = XmlNodeType.SignificantWhitespace;
				goto IL_AD;
			}
			goto IL_AD;
			IL_A0:
			this.state = ReadState.EndOfFile;
			this.Reset();
			IL_AD:
			return this.haveRecord;
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000E43D8 File Offset: 0x000E25D8
		public override bool EOF
		{
			get
			{
				return this.state == ReadState.EndOfFile;
			}
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000E43E3 File Offset: 0x000E25E3
		public override void Close()
		{
			this.processor = null;
			this.state = ReadState.Closed;
			this.Reset();
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000E43F9 File Offset: 0x000E25F9
		public override ReadState ReadState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000E4404 File Offset: 0x000E2604
		public override string ReadString()
		{
			string text = string.Empty;
			if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.Attribute || this.currentInfo == this.attributeValue)
			{
				if (this.mainNode.IsEmptyTag)
				{
					return text;
				}
				if (!this.Read())
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
			}
			StringBuilder stringBuilder = null;
			bool flag = true;
			do
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType != XmlNodeType.Text && nodeType - XmlNodeType.Whitespace > 1)
				{
					goto IL_A0;
				}
				if (flag)
				{
					text = this.Value;
					flag = false;
				}
				else
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(text);
					}
					stringBuilder.Append(this.Value);
				}
			}
			while (this.Read());
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			IL_A0:
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000E44BC File Offset: 0x000E26BC
		public override string ReadInnerXml()
		{
			if (this.ReadState == ReadState.Interactive)
			{
				if (this.NodeType == XmlNodeType.Element && !this.IsEmptyElement)
				{
					StringOutput stringOutput = new StringOutput(this.processor);
					stringOutput.OmitXmlDecl();
					int i = this.Depth;
					this.Read();
					while (i < this.Depth)
					{
						stringOutput.RecordDone(this.builder);
						this.Read();
					}
					this.Read();
					stringOutput.TheEnd();
					return stringOutput.Result;
				}
				if (this.NodeType == XmlNodeType.Attribute)
				{
					return this.encoder.AtributeInnerXml(this.Value);
				}
				this.Read();
			}
			return string.Empty;
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000E4560 File Offset: 0x000E2760
		public override string ReadOuterXml()
		{
			if (this.ReadState == ReadState.Interactive)
			{
				if (this.NodeType == XmlNodeType.Element)
				{
					StringOutput stringOutput = new StringOutput(this.processor);
					stringOutput.OmitXmlDecl();
					bool isEmptyElement = this.IsEmptyElement;
					int i = this.Depth;
					stringOutput.RecordDone(this.builder);
					this.Read();
					while (i < this.Depth)
					{
						stringOutput.RecordDone(this.builder);
						this.Read();
					}
					if (!isEmptyElement)
					{
						stringOutput.RecordDone(this.builder);
						this.Read();
					}
					stringOutput.TheEnd();
					return stringOutput.Result;
				}
				if (this.NodeType == XmlNodeType.Attribute)
				{
					return this.encoder.AtributeOuterXml(this.Name, this.Value);
				}
				this.Read();
			}
			return string.Empty;
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x000E4626 File Offset: 0x000E2826
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000E462E File Offset: 0x000E282E
		public override string LookupNamespace(string prefix)
		{
			prefix = this.nameTable.Get(prefix);
			if (this.manager != null && prefix != null)
			{
				return this.manager.ResolveNamespace(prefix);
			}
			return null;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000E4657 File Offset: 0x000E2857
		public override void ResolveEntity()
		{
			if (this.NodeType != XmlNodeType.EntityReference)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000E4674 File Offset: 0x000E2874
		public override bool ReadAttributeValue()
		{
			if (this.ReadState != ReadState.Interactive || this.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			if (this.attributeValue == null)
			{
				this.attributeValue = new BuilderInfo();
				this.attributeValue.NodeType = XmlNodeType.Text;
			}
			if (this.currentInfo == this.attributeValue)
			{
				return false;
			}
			this.attributeValue.Value = this.currentInfo.Value;
			this.attributeValue.Depth = this.currentInfo.Depth + 1;
			this.currentInfo = this.attributeValue;
			return true;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000E4700 File Offset: 0x000E2900
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			this.builder = record;
			this.mainNode = record.MainNode;
			this.attributeList = record.AttributeList;
			this.attributeCount = record.AttributeCount;
			this.manager = record.Manager;
			this.haveRecord = true;
			this.SetMainNode();
			return Processor.OutputResult.Interrupt;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0000B528 File Offset: 0x00009728
		public void TheEnd()
		{
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000E4752 File Offset: 0x000E2952
		private void SetMainNode()
		{
			this.currentIndex = -1;
			this.currentInfo = this.mainNode;
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x000E4767 File Offset: 0x000E2967
		private void SetAttribute(int attrib)
		{
			this.currentIndex = attrib;
			this.currentInfo = (BuilderInfo)this.attributeList[attrib];
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000E4787 File Offset: 0x000E2987
		private BuilderInfo GetBuilderInfo(int attrib)
		{
			if (attrib < 0 || this.attributeCount <= attrib)
			{
				throw new ArgumentOutOfRangeException("attrib");
			}
			return (BuilderInfo)this.attributeList[attrib];
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000E47B4 File Offset: 0x000E29B4
		private bool FindAttribute(string localName, string namespaceURI, out int attrIndex)
		{
			if (namespaceURI == null)
			{
				namespaceURI = string.Empty;
			}
			if (localName == null)
			{
				localName = string.Empty;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)this.attributeList[i];
				if (builderInfo.NamespaceURI == namespaceURI && builderInfo.LocalName == localName)
				{
					attrIndex = i;
					return true;
				}
			}
			attrIndex = -1;
			return false;
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000E4820 File Offset: 0x000E2A20
		private bool FindAttribute(string name, out int attrIndex)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			for (int i = 0; i < this.attributeCount; i++)
			{
				if (((BuilderInfo)this.attributeList[i]).Name == name)
				{
					attrIndex = i;
					return true;
				}
			}
			attrIndex = -1;
			return false;
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000E486F File Offset: 0x000E2A6F
		private void Reset()
		{
			this.currentIndex = -1;
			this.currentInfo = ReaderOutput.s_DefaultInfo;
			this.mainNode = ReaderOutput.s_DefaultInfo;
			this.manager = null;
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckCurrentInfo()
		{
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000E4895 File Offset: 0x000E2A95
		// Note: this type is marked as 'beforefieldinit'.
		static ReaderOutput()
		{
		}

		// Token: 0x04001DAA RID: 7594
		private Processor processor;

		// Token: 0x04001DAB RID: 7595
		private XmlNameTable nameTable;

		// Token: 0x04001DAC RID: 7596
		private RecordBuilder builder;

		// Token: 0x04001DAD RID: 7597
		private BuilderInfo mainNode;

		// Token: 0x04001DAE RID: 7598
		private ArrayList attributeList;

		// Token: 0x04001DAF RID: 7599
		private int attributeCount;

		// Token: 0x04001DB0 RID: 7600
		private BuilderInfo attributeValue;

		// Token: 0x04001DB1 RID: 7601
		private OutputScopeManager manager;

		// Token: 0x04001DB2 RID: 7602
		private int currentIndex;

		// Token: 0x04001DB3 RID: 7603
		private BuilderInfo currentInfo;

		// Token: 0x04001DB4 RID: 7604
		private ReadState state;

		// Token: 0x04001DB5 RID: 7605
		private bool haveRecord;

		// Token: 0x04001DB6 RID: 7606
		private static BuilderInfo s_DefaultInfo = new BuilderInfo();

		// Token: 0x04001DB7 RID: 7607
		private ReaderOutput.XmlEncoder encoder = new ReaderOutput.XmlEncoder();

		// Token: 0x04001DB8 RID: 7608
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x020003A3 RID: 931
		private class XmlEncoder
		{
			// Token: 0x060025F8 RID: 9720 RVA: 0x000E48A1 File Offset: 0x000E2AA1
			private void Init()
			{
				this.buffer = new StringBuilder();
				this.encoder = new XmlTextEncoder(new StringWriter(this.buffer, CultureInfo.InvariantCulture));
			}

			// Token: 0x060025F9 RID: 9721 RVA: 0x000E48CC File Offset: 0x000E2ACC
			public string AtributeInnerXml(string value)
			{
				if (this.encoder == null)
				{
					this.Init();
				}
				this.buffer.Length = 0;
				this.encoder.StartAttribute(false);
				this.encoder.Write(value);
				this.encoder.EndAttribute();
				return this.buffer.ToString();
			}

			// Token: 0x060025FA RID: 9722 RVA: 0x000E4924 File Offset: 0x000E2B24
			public string AtributeOuterXml(string name, string value)
			{
				if (this.encoder == null)
				{
					this.Init();
				}
				this.buffer.Length = 0;
				this.buffer.Append(name);
				this.buffer.Append('=');
				this.buffer.Append(this.QuoteChar);
				this.encoder.StartAttribute(false);
				this.encoder.Write(value);
				this.encoder.EndAttribute();
				this.buffer.Append(this.QuoteChar);
				return this.buffer.ToString();
			}

			// Token: 0x17000782 RID: 1922
			// (get) Token: 0x060025FB RID: 9723 RVA: 0x0001E51A File Offset: 0x0001C71A
			public char QuoteChar
			{
				get
				{
					return '"';
				}
			}

			// Token: 0x060025FC RID: 9724 RVA: 0x0000216B File Offset: 0x0000036B
			public XmlEncoder()
			{
			}

			// Token: 0x04001DB9 RID: 7609
			private StringBuilder buffer;

			// Token: 0x04001DBA RID: 7610
			private XmlTextEncoder encoder;
		}
	}
}
