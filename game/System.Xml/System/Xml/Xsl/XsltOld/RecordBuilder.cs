using System;
using System.Collections;
using System.Text;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003A4 RID: 932
	internal sealed class RecordBuilder
	{
		// Token: 0x060025FD RID: 9725 RVA: 0x000E49B8 File Offset: 0x000E2BB8
		internal RecordBuilder(RecordOutput output, XmlNameTable nameTable)
		{
			this.output = output;
			this.nameTable = ((nameTable != null) ? nameTable : new NameTable());
			this.atoms = new OutKeywords(this.nameTable);
			this.scopeManager = new OutputScopeManager(this.nameTable, this.atoms);
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x000E4A37 File Offset: 0x000E2C37
		// (set) Token: 0x060025FF RID: 9727 RVA: 0x000E4A3F File Offset: 0x000E2C3F
		internal int OutputState
		{
			get
			{
				return this.outputState;
			}
			set
			{
				this.outputState = value;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x000E4A48 File Offset: 0x000E2C48
		// (set) Token: 0x06002601 RID: 9729 RVA: 0x000E4A50 File Offset: 0x000E2C50
		internal RecordBuilder Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x000E4A59 File Offset: 0x000E2C59
		internal RecordOutput Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x000E4A61 File Offset: 0x000E2C61
		internal BuilderInfo MainNode
		{
			get
			{
				return this.mainNode;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x000E4A69 File Offset: 0x000E2C69
		internal ArrayList AttributeList
		{
			get
			{
				return this.attributeList;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x000E4A71 File Offset: 0x000E2C71
		internal int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000E4A79 File Offset: 0x000E2C79
		internal OutputScopeManager Manager
		{
			get
			{
				return this.scopeManager;
			}
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000E4A81 File Offset: 0x000E2C81
		private void ValueAppend(string s, bool disableOutputEscaping)
		{
			this.currentInfo.ValueAppend(s, disableOutputEscaping);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000E4A90 File Offset: 0x000E2C90
		private bool CanOutput(int state)
		{
			if (this.recordState == 0 || (state & 8192) == 0)
			{
				return true;
			}
			this.recordState = 2;
			this.FinalizeRecord();
			this.SetEmptyFlag(state);
			return this.output.RecordDone(this) == Processor.OutputResult.Continue;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000E4AC8 File Offset: 0x000E2CC8
		internal Processor.OutputResult BeginEvent(int state, XPathNodeType nodeType, string prefix, string name, string nspace, bool empty, object htmlProps, bool search)
		{
			if (!this.CanOutput(state))
			{
				return Processor.OutputResult.Overflow;
			}
			this.AdjustDepth(state);
			this.ResetRecord(state);
			this.PopElementScope();
			prefix = ((prefix != null) ? this.nameTable.Add(prefix) : this.atoms.Empty);
			name = ((name != null) ? this.nameTable.Add(name) : this.atoms.Empty);
			nspace = ((nspace != null) ? this.nameTable.Add(nspace) : this.atoms.Empty);
			switch (nodeType)
			{
			case XPathNodeType.Element:
				this.mainNode.htmlProps = (htmlProps as HtmlElementProps);
				this.mainNode.search = search;
				this.BeginElement(prefix, name, nspace, empty);
				break;
			case XPathNodeType.Attribute:
				this.BeginAttribute(prefix, name, nspace, htmlProps, search);
				break;
			case XPathNodeType.Namespace:
				this.BeginNamespace(name, nspace);
				break;
			case XPathNodeType.ProcessingInstruction:
				if (!this.BeginProcessingInstruction(prefix, name, nspace))
				{
					return Processor.OutputResult.Error;
				}
				break;
			case XPathNodeType.Comment:
				this.BeginComment();
				break;
			}
			return this.CheckRecordBegin(state);
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000E4BEC File Offset: 0x000E2DEC
		internal Processor.OutputResult TextEvent(int state, string text, bool disableOutputEscaping)
		{
			if (!this.CanOutput(state))
			{
				return Processor.OutputResult.Overflow;
			}
			this.AdjustDepth(state);
			this.ResetRecord(state);
			this.PopElementScope();
			if ((state & 8192) != 0)
			{
				this.currentInfo.Depth = this.recordDepth;
				this.currentInfo.NodeType = XmlNodeType.Text;
			}
			this.ValueAppend(text, disableOutputEscaping);
			return this.CheckRecordBegin(state);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000E4C50 File Offset: 0x000E2E50
		internal Processor.OutputResult EndEvent(int state, XPathNodeType nodeType)
		{
			if (!this.CanOutput(state))
			{
				return Processor.OutputResult.Overflow;
			}
			this.AdjustDepth(state);
			this.PopElementScope();
			this.popScope = ((state & 65536) != 0);
			if ((state & 4096) != 0 && this.mainNode.IsEmptyTag)
			{
				return Processor.OutputResult.Continue;
			}
			this.ResetRecord(state);
			if ((state & 8192) != 0 && nodeType == XPathNodeType.Element)
			{
				this.EndElement();
			}
			return this.CheckRecordEnd(state);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000E4CBE File Offset: 0x000E2EBE
		internal void Reset()
		{
			if (this.recordState == 2)
			{
				this.recordState = 0;
			}
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000E4CD0 File Offset: 0x000E2ED0
		internal void TheEnd()
		{
			if (this.recordState == 1)
			{
				this.recordState = 2;
				this.FinalizeRecord();
				this.output.RecordDone(this);
			}
			this.output.TheEnd();
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000E4D00 File Offset: 0x000E2F00
		private int FindAttribute(string name, string nspace, ref string prefix)
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)this.attributeList[i];
				if (Ref.Equal(builderInfo.LocalName, name))
				{
					if (Ref.Equal(builderInfo.NamespaceURI, nspace))
					{
						return i;
					}
					if (Ref.Equal(builderInfo.Prefix, prefix))
					{
						prefix = string.Empty;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000E4D68 File Offset: 0x000E2F68
		private void BeginElement(string prefix, string name, string nspace, bool empty)
		{
			this.currentInfo.NodeType = XmlNodeType.Element;
			this.currentInfo.Prefix = prefix;
			this.currentInfo.LocalName = name;
			this.currentInfo.NamespaceURI = nspace;
			this.currentInfo.Depth = this.recordDepth;
			this.currentInfo.IsEmptyTag = empty;
			this.scopeManager.PushScope(name, nspace, prefix);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000E4DD4 File Offset: 0x000E2FD4
		private void EndElement()
		{
			OutputScope currentElementScope = this.scopeManager.CurrentElementScope;
			this.currentInfo.NodeType = XmlNodeType.EndElement;
			this.currentInfo.Prefix = currentElementScope.Prefix;
			this.currentInfo.LocalName = currentElementScope.Name;
			this.currentInfo.NamespaceURI = currentElementScope.Namespace;
			this.currentInfo.Depth = this.recordDepth;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000E4E40 File Offset: 0x000E3040
		private int NewAttribute()
		{
			if (this.attributeCount >= this.attributeList.Count)
			{
				this.attributeList.Add(new BuilderInfo());
			}
			int num = this.attributeCount;
			this.attributeCount = num + 1;
			return num;
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000E4E84 File Offset: 0x000E3084
		private void BeginAttribute(string prefix, string name, string nspace, object htmlAttrProps, bool search)
		{
			int num = this.FindAttribute(name, nspace, ref prefix);
			if (num == -1)
			{
				num = this.NewAttribute();
			}
			BuilderInfo builderInfo = (BuilderInfo)this.attributeList[num];
			builderInfo.Initialize(prefix, name, nspace);
			builderInfo.Depth = this.recordDepth;
			builderInfo.NodeType = XmlNodeType.Attribute;
			builderInfo.htmlAttrProps = (htmlAttrProps as HtmlAttributeProps);
			builderInfo.search = search;
			this.currentInfo = builderInfo;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000E4EF4 File Offset: 0x000E30F4
		private void BeginNamespace(string name, string nspace)
		{
			bool flag = false;
			if (Ref.Equal(name, this.atoms.Empty))
			{
				if (!Ref.Equal(nspace, this.scopeManager.DefaultNamespace) && !Ref.Equal(this.mainNode.NamespaceURI, this.atoms.Empty))
				{
					this.DeclareNamespace(nspace, name);
				}
			}
			else
			{
				string text = this.scopeManager.ResolveNamespace(name, out flag);
				if (text != null)
				{
					if (!Ref.Equal(nspace, text) && !flag)
					{
						this.DeclareNamespace(nspace, name);
					}
				}
				else
				{
					this.DeclareNamespace(nspace, name);
				}
			}
			this.currentInfo = this.dummy;
			this.currentInfo.NodeType = XmlNodeType.Attribute;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000E4F98 File Offset: 0x000E3198
		private bool BeginProcessingInstruction(string prefix, string name, string nspace)
		{
			this.currentInfo.NodeType = XmlNodeType.ProcessingInstruction;
			this.currentInfo.Prefix = prefix;
			this.currentInfo.LocalName = name;
			this.currentInfo.NamespaceURI = nspace;
			this.currentInfo.Depth = this.recordDepth;
			return true;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000E4FE7 File Offset: 0x000E31E7
		private void BeginComment()
		{
			this.currentInfo.NodeType = XmlNodeType.Comment;
			this.currentInfo.Depth = this.recordDepth;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000E5008 File Offset: 0x000E3208
		private void AdjustDepth(int state)
		{
			int num = state & 768;
			if (num == 256)
			{
				this.recordDepth++;
				return;
			}
			if (num != 512)
			{
				return;
			}
			this.recordDepth--;
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000E504C File Offset: 0x000E324C
		private void ResetRecord(int state)
		{
			if ((state & 8192) != 0)
			{
				this.attributeCount = 0;
				this.namespaceCount = 0;
				this.currentInfo = this.mainNode;
				this.currentInfo.Initialize(this.atoms.Empty, this.atoms.Empty, this.atoms.Empty);
				this.currentInfo.NodeType = XmlNodeType.None;
				this.currentInfo.IsEmptyTag = false;
				this.currentInfo.htmlProps = null;
				this.currentInfo.htmlAttrProps = null;
			}
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000E50D8 File Offset: 0x000E32D8
		private void PopElementScope()
		{
			if (this.popScope)
			{
				this.scopeManager.PopScope();
				this.popScope = false;
			}
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000E50F4 File Offset: 0x000E32F4
		private Processor.OutputResult CheckRecordBegin(int state)
		{
			if ((state & 16384) != 0)
			{
				this.recordState = 2;
				this.FinalizeRecord();
				this.SetEmptyFlag(state);
				return this.output.RecordDone(this);
			}
			this.recordState = 1;
			return Processor.OutputResult.Continue;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000E5128 File Offset: 0x000E3328
		private Processor.OutputResult CheckRecordEnd(int state)
		{
			if ((state & 16384) != 0)
			{
				this.recordState = 2;
				this.FinalizeRecord();
				this.SetEmptyFlag(state);
				return this.output.RecordDone(this);
			}
			return Processor.OutputResult.Continue;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000E5155 File Offset: 0x000E3355
		private void SetEmptyFlag(int state)
		{
			if ((state & 1024) != 0)
			{
				this.mainNode.IsEmptyTag = false;
			}
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000E516C File Offset: 0x000E336C
		private void AnalyzeSpaceLang()
		{
			for (int i = 0; i < this.attributeCount; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)this.attributeList[i];
				if (Ref.Equal(builderInfo.Prefix, this.atoms.Xml))
				{
					OutputScope currentElementScope = this.scopeManager.CurrentElementScope;
					if (Ref.Equal(builderInfo.LocalName, this.atoms.Lang))
					{
						currentElementScope.Lang = builderInfo.Value;
					}
					else if (Ref.Equal(builderInfo.LocalName, this.atoms.Space))
					{
						currentElementScope.Space = RecordBuilder.TranslateXmlSpace(builderInfo.Value);
					}
				}
			}
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000E5218 File Offset: 0x000E3418
		private void FixupElement()
		{
			if (Ref.Equal(this.mainNode.NamespaceURI, this.atoms.Empty))
			{
				this.mainNode.Prefix = this.atoms.Empty;
			}
			if (Ref.Equal(this.mainNode.Prefix, this.atoms.Empty))
			{
				if (!Ref.Equal(this.mainNode.NamespaceURI, this.scopeManager.DefaultNamespace))
				{
					this.DeclareNamespace(this.mainNode.NamespaceURI, this.mainNode.Prefix);
				}
			}
			else
			{
				bool flag = false;
				string text = this.scopeManager.ResolveNamespace(this.mainNode.Prefix, out flag);
				if (text != null)
				{
					if (!Ref.Equal(this.mainNode.NamespaceURI, text))
					{
						if (flag)
						{
							this.mainNode.Prefix = this.GetPrefixForNamespace(this.mainNode.NamespaceURI);
						}
						else
						{
							this.DeclareNamespace(this.mainNode.NamespaceURI, this.mainNode.Prefix);
						}
					}
				}
				else
				{
					this.DeclareNamespace(this.mainNode.NamespaceURI, this.mainNode.Prefix);
				}
			}
			this.scopeManager.CurrentElementScope.Prefix = this.mainNode.Prefix;
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000E5360 File Offset: 0x000E3560
		private void FixupAttributes(int attributeCount)
		{
			for (int i = 0; i < attributeCount; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)this.attributeList[i];
				if (Ref.Equal(builderInfo.NamespaceURI, this.atoms.Empty))
				{
					builderInfo.Prefix = this.atoms.Empty;
				}
				else if (Ref.Equal(builderInfo.Prefix, this.atoms.Empty))
				{
					builderInfo.Prefix = this.GetPrefixForNamespace(builderInfo.NamespaceURI);
				}
				else
				{
					bool flag = false;
					string text = this.scopeManager.ResolveNamespace(builderInfo.Prefix, out flag);
					if (text != null)
					{
						if (!Ref.Equal(builderInfo.NamespaceURI, text))
						{
							if (flag)
							{
								builderInfo.Prefix = this.GetPrefixForNamespace(builderInfo.NamespaceURI);
							}
							else
							{
								this.DeclareNamespace(builderInfo.NamespaceURI, builderInfo.Prefix);
							}
						}
					}
					else
					{
						this.DeclareNamespace(builderInfo.NamespaceURI, builderInfo.Prefix);
					}
				}
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000E5450 File Offset: 0x000E3650
		private void AppendNamespaces()
		{
			for (int i = this.namespaceCount - 1; i >= 0; i--)
			{
				((BuilderInfo)this.attributeList[this.NewAttribute()]).Initialize((BuilderInfo)this.namespaceList[i]);
			}
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000E549C File Offset: 0x000E369C
		private void AnalyzeComment()
		{
			StringBuilder stringBuilder = null;
			string value = this.mainNode.Value;
			bool flag = false;
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				if (value[i] == '-')
				{
					if (flag)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value, num, i, 2 * value.Length);
						}
						else
						{
							stringBuilder.Append(value, num, i - num);
						}
						stringBuilder.Append(" -");
						num = i + 1;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
				i++;
			}
			if (stringBuilder != null)
			{
				if (num < value.Length)
				{
					stringBuilder.Append(value, num, value.Length - num);
				}
				if (flag)
				{
					stringBuilder.Append(" ");
				}
				this.mainNode.Value = stringBuilder.ToString();
				return;
			}
			if (flag)
			{
				this.mainNode.ValueAppend(" ", false);
			}
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000E556C File Offset: 0x000E376C
		private void AnalyzeProcessingInstruction()
		{
			StringBuilder stringBuilder = null;
			string value = this.mainNode.Value;
			bool flag = false;
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c != '>')
				{
					flag = (c == '?');
				}
				else
				{
					if (flag)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value, num, i, 2 * value.Length);
						}
						else
						{
							stringBuilder.Append(value, num, i - num);
						}
						stringBuilder.Append(" >");
						num = i + 1;
					}
					flag = false;
				}
				i++;
			}
			if (stringBuilder != null)
			{
				if (num < value.Length)
				{
					stringBuilder.Append(value, num, value.Length - num);
				}
				this.mainNode.Value = stringBuilder.ToString();
			}
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000E5628 File Offset: 0x000E3828
		private void FinalizeRecord()
		{
			XmlNodeType nodeType = this.mainNode.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				int num = this.attributeCount;
				this.FixupElement();
				this.FixupAttributes(num);
				this.AnalyzeSpaceLang();
				this.AppendNamespaces();
				return;
			}
			if (nodeType == XmlNodeType.ProcessingInstruction)
			{
				this.AnalyzeProcessingInstruction();
				return;
			}
			if (nodeType != XmlNodeType.Comment)
			{
				return;
			}
			this.AnalyzeComment();
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000E567C File Offset: 0x000E387C
		private int NewNamespace()
		{
			if (this.namespaceCount >= this.namespaceList.Count)
			{
				this.namespaceList.Add(new BuilderInfo());
			}
			int num = this.namespaceCount;
			this.namespaceCount = num + 1;
			return num;
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000E56C0 File Offset: 0x000E38C0
		private void DeclareNamespace(string nspace, string prefix)
		{
			int index = this.NewNamespace();
			BuilderInfo builderInfo = (BuilderInfo)this.namespaceList[index];
			if (prefix == this.atoms.Empty)
			{
				builderInfo.Initialize(this.atoms.Empty, this.atoms.Xmlns, this.atoms.XmlnsNamespace);
			}
			else
			{
				builderInfo.Initialize(this.atoms.Xmlns, prefix, this.atoms.XmlnsNamespace);
			}
			builderInfo.Depth = this.recordDepth;
			builderInfo.NodeType = XmlNodeType.Attribute;
			builderInfo.Value = nspace;
			this.scopeManager.PushNamespace(prefix, nspace);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000E5768 File Offset: 0x000E3968
		private string DeclareNewNamespace(string nspace)
		{
			string text = this.scopeManager.GeneratePrefix("xp_{0}");
			this.DeclareNamespace(nspace, text);
			return text;
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000E5790 File Offset: 0x000E3990
		internal string GetPrefixForNamespace(string nspace)
		{
			string result = null;
			if (this.scopeManager.FindPrefix(nspace, out result))
			{
				return result;
			}
			return this.DeclareNewNamespace(nspace);
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000E57B8 File Offset: 0x000E39B8
		private static XmlSpace TranslateXmlSpace(string space)
		{
			if (space == "default")
			{
				return XmlSpace.Default;
			}
			if (space == "preserve")
			{
				return XmlSpace.Preserve;
			}
			return XmlSpace.None;
		}

		// Token: 0x04001DBB RID: 7611
		private int outputState;

		// Token: 0x04001DBC RID: 7612
		private RecordBuilder next;

		// Token: 0x04001DBD RID: 7613
		private RecordOutput output;

		// Token: 0x04001DBE RID: 7614
		private XmlNameTable nameTable;

		// Token: 0x04001DBF RID: 7615
		private OutKeywords atoms;

		// Token: 0x04001DC0 RID: 7616
		private OutputScopeManager scopeManager;

		// Token: 0x04001DC1 RID: 7617
		private BuilderInfo mainNode = new BuilderInfo();

		// Token: 0x04001DC2 RID: 7618
		private ArrayList attributeList = new ArrayList();

		// Token: 0x04001DC3 RID: 7619
		private int attributeCount;

		// Token: 0x04001DC4 RID: 7620
		private ArrayList namespaceList = new ArrayList();

		// Token: 0x04001DC5 RID: 7621
		private int namespaceCount;

		// Token: 0x04001DC6 RID: 7622
		private BuilderInfo dummy = new BuilderInfo();

		// Token: 0x04001DC7 RID: 7623
		private BuilderInfo currentInfo;

		// Token: 0x04001DC8 RID: 7624
		private bool popScope;

		// Token: 0x04001DC9 RID: 7625
		private int recordState;

		// Token: 0x04001DCA RID: 7626
		private int recordDepth;

		// Token: 0x04001DCB RID: 7627
		private const int NoRecord = 0;

		// Token: 0x04001DCC RID: 7628
		private const int SomeRecord = 1;

		// Token: 0x04001DCD RID: 7629
		private const int HaveRecord = 2;

		// Token: 0x04001DCE RID: 7630
		private const char s_Minus = '-';

		// Token: 0x04001DCF RID: 7631
		private const string s_Space = " ";

		// Token: 0x04001DD0 RID: 7632
		private const string s_SpaceMinus = " -";

		// Token: 0x04001DD1 RID: 7633
		private const char s_Question = '?';

		// Token: 0x04001DD2 RID: 7634
		private const char s_Greater = '>';

		// Token: 0x04001DD3 RID: 7635
		private const string s_SpaceGreater = " >";

		// Token: 0x04001DD4 RID: 7636
		private const string PrefixFormat = "xp_{0}";
	}
}
