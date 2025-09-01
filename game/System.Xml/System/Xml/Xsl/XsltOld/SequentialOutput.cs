using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003A9 RID: 937
	internal abstract class SequentialOutput : RecordOutput
	{
		// Token: 0x06002641 RID: 9793 RVA: 0x000E5E64 File Offset: 0x000E4064
		private void CacheOuptutProps(XsltOutput output)
		{
			this.output = output;
			this.isXmlOutput = (this.output.Method == XsltOutput.OutputMethod.Xml);
			this.isHtmlOutput = (this.output.Method == XsltOutput.OutputMethod.Html);
			this.cdataElements = this.output.CDataElements;
			this.indentOutput = this.output.Indent;
			this.outputDoctype = (this.output.DoctypeSystem != null || (this.isHtmlOutput && this.output.DoctypePublic != null));
			this.outputXmlDecl = (this.isXmlOutput && !this.output.OmitXmlDeclaration && !this.omitXmlDeclCalled);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000E5F18 File Offset: 0x000E4118
		internal SequentialOutput(Processor processor)
		{
			this.processor = processor;
			this.CacheOuptutProps(processor.Output);
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000E5F45 File Offset: 0x000E4145
		public void OmitXmlDecl()
		{
			this.omitXmlDeclCalled = true;
			this.outputXmlDecl = false;
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000E5F58 File Offset: 0x000E4158
		private void WriteStartElement(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			HtmlElementProps htmlElementProps = null;
			if (this.isHtmlOutput)
			{
				if (mainNode.Prefix.Length == 0)
				{
					htmlElementProps = mainNode.htmlProps;
					if (htmlElementProps == null && mainNode.search)
					{
						htmlElementProps = HtmlElementProps.GetProps(mainNode.LocalName);
					}
					record.Manager.CurrentElementScope.HtmlElementProps = htmlElementProps;
					mainNode.IsEmptyTag = false;
				}
			}
			else if (this.isXmlOutput && mainNode.Depth == 0)
			{
				if (this.secondRoot && (this.output.DoctypeSystem != null || this.output.Standalone))
				{
					throw XsltException.Create("There are multiple root elements in the output XML.", Array.Empty<string>());
				}
				this.secondRoot = true;
			}
			if (this.outputDoctype)
			{
				this.WriteDoctype(mainNode);
				this.outputDoctype = false;
			}
			if (this.cdataElements != null && this.cdataElements.Contains(new XmlQualifiedName(mainNode.LocalName, mainNode.NamespaceURI)) && this.isXmlOutput)
			{
				record.Manager.CurrentElementScope.ToCData = true;
			}
			this.Indent(record);
			this.Write('<');
			this.WriteName(mainNode.Prefix, mainNode.LocalName);
			this.WriteAttributes(record.AttributeList, record.AttributeCount, htmlElementProps);
			if (mainNode.IsEmptyTag)
			{
				this.Write(" />");
			}
			else
			{
				this.Write('>');
			}
			if (htmlElementProps != null && htmlElementProps.Head)
			{
				BuilderInfo builderInfo = mainNode;
				int depth = builderInfo.Depth;
				builderInfo.Depth = depth + 1;
				this.Indent(record);
				BuilderInfo builderInfo2 = mainNode;
				depth = builderInfo2.Depth;
				builderInfo2.Depth = depth - 1;
				this.Write("<META http-equiv=\"Content-Type\" content=\"");
				this.Write(this.output.MediaType);
				this.Write("; charset=");
				this.Write(this.encoding.WebName);
				this.Write("\">");
			}
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000E6124 File Offset: 0x000E4324
		private void WriteTextNode(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			OutputScope currentElementScope = record.Manager.CurrentElementScope;
			currentElementScope.Mixed = true;
			if (currentElementScope.HtmlElementProps != null && currentElementScope.HtmlElementProps.NoEntities)
			{
				this.Write(mainNode.Value);
				return;
			}
			if (currentElementScope.ToCData)
			{
				this.WriteCDataSection(mainNode.Value);
				return;
			}
			this.WriteTextNode(mainNode);
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000E618C File Offset: 0x000E438C
		private void WriteTextNode(BuilderInfo node)
		{
			for (int i = 0; i < node.TextInfoCount; i++)
			{
				string text = node.TextInfo[i];
				if (text == null)
				{
					i++;
					this.Write(node.TextInfo[i]);
				}
				else
				{
					this.WriteWithReplace(text, SequentialOutput.s_TextValueFind, SequentialOutput.s_TextValueReplace);
				}
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000E61DB File Offset: 0x000E43DB
		private void WriteCDataSection(string value)
		{
			this.Write("<![CDATA[");
			this.WriteCData(value);
			this.Write("]]>");
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000E61FC File Offset: 0x000E43FC
		private void WriteDoctype(BuilderInfo mainNode)
		{
			this.Indent(0);
			this.Write("<!DOCTYPE ");
			if (this.isXmlOutput)
			{
				this.WriteName(mainNode.Prefix, mainNode.LocalName);
			}
			else
			{
				this.WriteName(string.Empty, "html");
			}
			this.Write(' ');
			if (this.output.DoctypePublic != null)
			{
				this.Write("PUBLIC ");
				this.Write('"');
				this.Write(this.output.DoctypePublic);
				this.Write("\" ");
			}
			else
			{
				this.Write("SYSTEM ");
			}
			if (this.output.DoctypeSystem != null)
			{
				this.Write('"');
				this.Write(this.output.DoctypeSystem);
				this.Write('"');
			}
			this.Write('>');
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000E62D0 File Offset: 0x000E44D0
		private void WriteXmlDeclaration()
		{
			this.outputXmlDecl = false;
			this.Indent(0);
			this.Write("<?");
			this.WriteName(string.Empty, "xml");
			this.Write(" version=\"1.0\"");
			if (this.encoding != null)
			{
				this.Write(" encoding=\"");
				this.Write(this.encoding.WebName);
				this.Write('"');
			}
			if (this.output.HasStandalone)
			{
				this.Write(" standalone=\"");
				this.Write(this.output.Standalone ? "yes" : "no");
				this.Write('"');
			}
			this.Write("?>");
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000E6387 File Offset: 0x000E4587
		private void WriteProcessingInstruction(RecordBuilder record)
		{
			this.Indent(record);
			this.WriteProcessingInstruction(record.MainNode);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000E639C File Offset: 0x000E459C
		private void WriteProcessingInstruction(BuilderInfo node)
		{
			this.Write("<?");
			this.WriteName(node.Prefix, node.LocalName);
			this.Write(' ');
			this.Write(node.Value);
			if (this.isHtmlOutput)
			{
				this.Write('>');
				return;
			}
			this.Write("?>");
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000E63F8 File Offset: 0x000E45F8
		private void WriteEndElement(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			HtmlElementProps htmlElementProps = record.Manager.CurrentElementScope.HtmlElementProps;
			if (htmlElementProps != null && htmlElementProps.Empty)
			{
				return;
			}
			this.Indent(record);
			this.Write("</");
			this.WriteName(record.MainNode.Prefix, record.MainNode.LocalName);
			this.Write('>');
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000E6460 File Offset: 0x000E4660
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			if (this.output.Method == XsltOutput.OutputMethod.Unknown)
			{
				if (!this.DecideDefaultOutput(record.MainNode))
				{
					this.CacheRecord(record);
				}
				else
				{
					this.OutputCachedRecords();
					this.OutputRecord(record);
				}
			}
			else
			{
				this.OutputRecord(record);
			}
			record.Reset();
			return Processor.OutputResult.Continue;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000E64AF File Offset: 0x000E46AF
		public void TheEnd()
		{
			this.OutputCachedRecords();
			this.Close();
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000E64C0 File Offset: 0x000E46C0
		private bool DecideDefaultOutput(BuilderInfo node)
		{
			XsltOutput.OutputMethod defaultOutput = XsltOutput.OutputMethod.Xml;
			XmlNodeType nodeType = node.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType != XmlNodeType.Text && nodeType - XmlNodeType.Whitespace > 1)
				{
					return false;
				}
				if (this.xmlCharType.IsOnlyWhitespace(node.Value))
				{
					return false;
				}
				defaultOutput = XsltOutput.OutputMethod.Xml;
			}
			else if (node.NamespaceURI.Length == 0 && string.Compare("html", node.LocalName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				defaultOutput = XsltOutput.OutputMethod.Html;
			}
			if (this.processor.SetDefaultOutput(defaultOutput))
			{
				this.CacheOuptutProps(this.processor.Output);
			}
			return true;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000E6546 File Offset: 0x000E4746
		private void CacheRecord(RecordBuilder record)
		{
			if (this.outputCache == null)
			{
				this.outputCache = new ArrayList();
			}
			this.outputCache.Add(record.MainNode.Clone());
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000E6574 File Offset: 0x000E4774
		private void OutputCachedRecords()
		{
			if (this.outputCache == null)
			{
				return;
			}
			for (int i = 0; i < this.outputCache.Count; i++)
			{
				BuilderInfo node = (BuilderInfo)this.outputCache[i];
				this.OutputRecord(node);
			}
			this.outputCache = null;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000E65C0 File Offset: 0x000E47C0
		private void OutputRecord(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			if (this.outputXmlDecl)
			{
				this.WriteXmlDeclaration();
			}
			switch (mainNode.NodeType)
			{
			case XmlNodeType.Element:
				this.WriteStartElement(record);
				return;
			case XmlNodeType.Attribute:
			case XmlNodeType.CDATA:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
				break;
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.WriteTextNode(record);
				return;
			case XmlNodeType.EntityReference:
				this.Write('&');
				this.WriteName(mainNode.Prefix, mainNode.LocalName);
				this.Write(';');
				return;
			case XmlNodeType.ProcessingInstruction:
				this.WriteProcessingInstruction(record);
				return;
			case XmlNodeType.Comment:
				this.Indent(record);
				this.Write("<!--");
				this.Write(mainNode.Value);
				this.Write("-->");
				return;
			case XmlNodeType.DocumentType:
				this.Write(mainNode.Value);
				return;
			case XmlNodeType.EndElement:
				this.WriteEndElement(record);
				break;
			default:
				return;
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000E66A8 File Offset: 0x000E48A8
		private void OutputRecord(BuilderInfo node)
		{
			if (this.outputXmlDecl)
			{
				this.WriteXmlDeclaration();
			}
			this.Indent(0);
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.CDATA:
			case XmlNodeType.Entity:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Notation:
			case XmlNodeType.EndElement:
				break;
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this.WriteTextNode(node);
				return;
			case XmlNodeType.EntityReference:
				this.Write('&');
				this.WriteName(node.Prefix, node.LocalName);
				this.Write(';');
				return;
			case XmlNodeType.ProcessingInstruction:
				this.WriteProcessingInstruction(node);
				return;
			case XmlNodeType.Comment:
				this.Write("<!--");
				this.Write(node.Value);
				this.Write("-->");
				return;
			case XmlNodeType.DocumentType:
				this.Write(node.Value);
				break;
			default:
				return;
			}
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000E6778 File Offset: 0x000E4978
		private void WriteName(string prefix, string name)
		{
			if (prefix != null && prefix.Length > 0)
			{
				this.Write(prefix);
				if (name == null || name.Length <= 0)
				{
					return;
				}
				this.Write(':');
			}
			this.Write(name);
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000E67AB File Offset: 0x000E49AB
		private void WriteXmlAttributeValue(string value)
		{
			this.WriteWithReplace(value, SequentialOutput.s_XmlAttributeValueFind, SequentialOutput.s_XmlAttributeValueReplace);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000E67C0 File Offset: 0x000E49C0
		private void WriteHtmlAttributeValue(string value)
		{
			int length = value.Length;
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				i++;
				if (c != '"')
				{
					if (c == '&')
					{
						if (i != length && value[i] == '{')
						{
							this.Write(c);
						}
						else
						{
							this.Write("&amp;");
						}
					}
					else
					{
						this.Write(c);
					}
				}
				else
				{
					this.Write("&quot;");
				}
			}
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000E682C File Offset: 0x000E4A2C
		private void WriteHtmlUri(string value)
		{
			int length = value.Length;
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				i++;
				if (c <= '\r')
				{
					if (c == '\n')
					{
						this.Write("&#xA;");
						continue;
					}
					if (c == '\r')
					{
						this.Write("&#xD;");
						continue;
					}
				}
				else
				{
					if (c == '"')
					{
						this.Write("&quot;");
						continue;
					}
					if (c == '&')
					{
						if (i != length && value[i] == '{')
						{
							this.Write(c);
							continue;
						}
						this.Write("&amp;");
						continue;
					}
				}
				if ('\u007f' < c)
				{
					if (this.utf8Encoding == null)
					{
						this.utf8Encoding = Encoding.UTF8;
						this.byteBuffer = new byte[this.utf8Encoding.GetMaxByteCount(1)];
					}
					int bytes = this.utf8Encoding.GetBytes(value, i - 1, 1, this.byteBuffer, 0);
					for (int j = 0; j < bytes; j++)
					{
						this.Write("%");
						uint num = (uint)this.byteBuffer[j];
						this.Write(num.ToString("X2", CultureInfo.InvariantCulture));
					}
				}
				else
				{
					this.Write(c);
				}
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000E6960 File Offset: 0x000E4B60
		private void WriteWithReplace(string value, char[] find, string[] replace)
		{
			int length = value.Length;
			int i;
			for (i = 0; i < length; i++)
			{
				int num = value.IndexOfAny(find, i);
				if (num == -1)
				{
					break;
				}
				while (i < num)
				{
					this.Write(value[i]);
					i++;
				}
				char c = value[i];
				int num2 = find.Length - 1;
				while (0 <= num2)
				{
					if (find[num2] == c)
					{
						this.Write(replace[num2]);
						break;
					}
					num2--;
				}
			}
			if (i == 0)
			{
				this.Write(value);
				return;
			}
			while (i < length)
			{
				this.Write(value[i]);
				i++;
			}
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000E69F3 File Offset: 0x000E4BF3
		private void WriteCData(string value)
		{
			this.Write(value.Replace("]]>", "]]]]><![CDATA[>"));
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000E6A0C File Offset: 0x000E4C0C
		private void WriteAttributes(ArrayList list, int count, HtmlElementProps htmlElementsProps)
		{
			for (int i = 0; i < count; i++)
			{
				BuilderInfo builderInfo = (BuilderInfo)list[i];
				string value = builderInfo.Value;
				bool flag = false;
				bool flag2 = false;
				if (htmlElementsProps != null && builderInfo.Prefix.Length == 0)
				{
					HtmlAttributeProps htmlAttributeProps = builderInfo.htmlAttrProps;
					if (htmlAttributeProps == null && builderInfo.search)
					{
						htmlAttributeProps = HtmlAttributeProps.GetProps(builderInfo.LocalName);
					}
					if (htmlAttributeProps != null)
					{
						flag = (htmlElementsProps.AbrParent && htmlAttributeProps.Abr);
						flag2 = (htmlElementsProps.UriParent && (htmlAttributeProps.Uri || (htmlElementsProps.NameParent && htmlAttributeProps.Name)));
					}
				}
				this.Write(' ');
				this.WriteName(builderInfo.Prefix, builderInfo.LocalName);
				if (!flag || string.Compare(builderInfo.LocalName, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.Write("=\"");
					if (flag2)
					{
						this.WriteHtmlUri(value);
					}
					else if (this.isHtmlOutput)
					{
						this.WriteHtmlAttributeValue(value);
					}
					else
					{
						this.WriteXmlAttributeValue(value);
					}
					this.Write('"');
				}
			}
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000E6B1B File Offset: 0x000E4D1B
		private void Indent(RecordBuilder record)
		{
			if (!record.Manager.CurrentElementScope.Mixed)
			{
				this.Indent(record.MainNode.Depth);
			}
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000E6B40 File Offset: 0x000E4D40
		private void Indent(int depth)
		{
			if (this.firstLine)
			{
				if (this.indentOutput)
				{
					this.firstLine = false;
				}
				return;
			}
			this.Write("\r\n");
			int num = 2 * depth;
			while (0 < num)
			{
				this.Write(" ");
				num--;
			}
		}

		// Token: 0x0600265D RID: 9821
		internal abstract void Write(char outputChar);

		// Token: 0x0600265E RID: 9822
		internal abstract void Write(string outputText);

		// Token: 0x0600265F RID: 9823
		internal abstract void Close();

		// Token: 0x06002660 RID: 9824 RVA: 0x000E6B8C File Offset: 0x000E4D8C
		// Note: this type is marked as 'beforefieldinit'.
		static SequentialOutput()
		{
		}

		// Token: 0x04001DE3 RID: 7651
		private const char s_Colon = ':';

		// Token: 0x04001DE4 RID: 7652
		private const char s_GreaterThan = '>';

		// Token: 0x04001DE5 RID: 7653
		private const char s_LessThan = '<';

		// Token: 0x04001DE6 RID: 7654
		private const char s_Space = ' ';

		// Token: 0x04001DE7 RID: 7655
		private const char s_Quote = '"';

		// Token: 0x04001DE8 RID: 7656
		private const char s_Semicolon = ';';

		// Token: 0x04001DE9 RID: 7657
		private const char s_NewLine = '\n';

		// Token: 0x04001DEA RID: 7658
		private const char s_Return = '\r';

		// Token: 0x04001DEB RID: 7659
		private const char s_Ampersand = '&';

		// Token: 0x04001DEC RID: 7660
		private const string s_LessThanQuestion = "<?";

		// Token: 0x04001DED RID: 7661
		private const string s_QuestionGreaterThan = "?>";

		// Token: 0x04001DEE RID: 7662
		private const string s_LessThanSlash = "</";

		// Token: 0x04001DEF RID: 7663
		private const string s_SlashGreaterThan = " />";

		// Token: 0x04001DF0 RID: 7664
		private const string s_EqualQuote = "=\"";

		// Token: 0x04001DF1 RID: 7665
		private const string s_DocType = "<!DOCTYPE ";

		// Token: 0x04001DF2 RID: 7666
		private const string s_CommentBegin = "<!--";

		// Token: 0x04001DF3 RID: 7667
		private const string s_CommentEnd = "-->";

		// Token: 0x04001DF4 RID: 7668
		private const string s_CDataBegin = "<![CDATA[";

		// Token: 0x04001DF5 RID: 7669
		private const string s_CDataEnd = "]]>";

		// Token: 0x04001DF6 RID: 7670
		private const string s_VersionAll = " version=\"1.0\"";

		// Token: 0x04001DF7 RID: 7671
		private const string s_Standalone = " standalone=\"";

		// Token: 0x04001DF8 RID: 7672
		private const string s_EncodingStart = " encoding=\"";

		// Token: 0x04001DF9 RID: 7673
		private const string s_Public = "PUBLIC ";

		// Token: 0x04001DFA RID: 7674
		private const string s_System = "SYSTEM ";

		// Token: 0x04001DFB RID: 7675
		private const string s_Html = "html";

		// Token: 0x04001DFC RID: 7676
		private const string s_QuoteSpace = "\" ";

		// Token: 0x04001DFD RID: 7677
		private const string s_CDataSplit = "]]]]><![CDATA[>";

		// Token: 0x04001DFE RID: 7678
		private const string s_EnLessThan = "&lt;";

		// Token: 0x04001DFF RID: 7679
		private const string s_EnGreaterThan = "&gt;";

		// Token: 0x04001E00 RID: 7680
		private const string s_EnAmpersand = "&amp;";

		// Token: 0x04001E01 RID: 7681
		private const string s_EnQuote = "&quot;";

		// Token: 0x04001E02 RID: 7682
		private const string s_EnNewLine = "&#xA;";

		// Token: 0x04001E03 RID: 7683
		private const string s_EnReturn = "&#xD;";

		// Token: 0x04001E04 RID: 7684
		private const string s_EndOfLine = "\r\n";

		// Token: 0x04001E05 RID: 7685
		private static char[] s_TextValueFind = new char[]
		{
			'&',
			'>',
			'<'
		};

		// Token: 0x04001E06 RID: 7686
		private static string[] s_TextValueReplace = new string[]
		{
			"&amp;",
			"&gt;",
			"&lt;"
		};

		// Token: 0x04001E07 RID: 7687
		private static char[] s_XmlAttributeValueFind = new char[]
		{
			'&',
			'>',
			'<',
			'"',
			'\n',
			'\r'
		};

		// Token: 0x04001E08 RID: 7688
		private static string[] s_XmlAttributeValueReplace = new string[]
		{
			"&amp;",
			"&gt;",
			"&lt;",
			"&quot;",
			"&#xA;",
			"&#xD;"
		};

		// Token: 0x04001E09 RID: 7689
		private Processor processor;

		// Token: 0x04001E0A RID: 7690
		protected Encoding encoding;

		// Token: 0x04001E0B RID: 7691
		private ArrayList outputCache;

		// Token: 0x04001E0C RID: 7692
		private bool firstLine = true;

		// Token: 0x04001E0D RID: 7693
		private bool secondRoot;

		// Token: 0x04001E0E RID: 7694
		private XsltOutput output;

		// Token: 0x04001E0F RID: 7695
		private bool isHtmlOutput;

		// Token: 0x04001E10 RID: 7696
		private bool isXmlOutput;

		// Token: 0x04001E11 RID: 7697
		private Hashtable cdataElements;

		// Token: 0x04001E12 RID: 7698
		private bool indentOutput;

		// Token: 0x04001E13 RID: 7699
		private bool outputDoctype;

		// Token: 0x04001E14 RID: 7700
		private bool outputXmlDecl;

		// Token: 0x04001E15 RID: 7701
		private bool omitXmlDeclCalled;

		// Token: 0x04001E16 RID: 7702
		private byte[] byteBuffer;

		// Token: 0x04001E17 RID: 7703
		private Encoding utf8Encoding;

		// Token: 0x04001E18 RID: 7704
		private XmlCharType xmlCharType = XmlCharType.Instance;
	}
}
