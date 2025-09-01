using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x02000565 RID: 1381
	internal sealed class Parser
	{
		// Token: 0x060036DF RID: 14047 RVA: 0x00134084 File Offset: 0x00132284
		public Parser(SchemaType schemaType, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler)
		{
			this.schemaType = schemaType;
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.eventHandler = eventHandler;
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
			this.processMarkup = true;
			this.dummyDocument = new XmlDocument();
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x001340DC File Offset: 0x001322DC
		public SchemaType Parse(XmlReader reader, string targetNamespace)
		{
			this.StartParsing(reader, targetNamespace);
			while (this.ParseReaderNode() && reader.Read())
			{
			}
			return this.FinishParsing();
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x001340FC File Offset: 0x001322FC
		public void StartParsing(XmlReader reader, string targetNamespace)
		{
			this.reader = reader;
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
			this.namespaceManager = reader.NamespaceManager;
			if (this.namespaceManager == null)
			{
				this.namespaceManager = new XmlNamespaceManager(this.nameTable);
				this.isProcessNamespaces = true;
			}
			else
			{
				this.isProcessNamespaces = false;
			}
			while (reader.NodeType != XmlNodeType.Element && reader.Read())
			{
			}
			this.markupDepth = int.MaxValue;
			this.schemaXmlDepth = reader.Depth;
			SchemaType rootType = this.schemaNames.SchemaTypeFromRoot(reader.LocalName, reader.NamespaceURI);
			string res;
			if (!this.CheckSchemaRoot(rootType, out res))
			{
				throw new XmlSchemaException(res, reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
			}
			if (this.schemaType == SchemaType.XSD)
			{
				this.schema = new XmlSchema();
				this.schema.BaseUri = new Uri(reader.BaseURI, UriKind.RelativeOrAbsolute);
				this.builder = new XsdBuilder(reader, this.namespaceManager, this.schema, this.nameTable, this.schemaNames, this.eventHandler);
				return;
			}
			this.xdrSchema = new SchemaInfo();
			this.xdrSchema.SchemaType = SchemaType.XDR;
			this.builder = new XdrBuilder(reader, this.namespaceManager, this.xdrSchema, targetNamespace, this.nameTable, this.schemaNames, this.eventHandler);
			((XdrBuilder)this.builder).XmlResolver = this.xmlResolver;
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x00134270 File Offset: 0x00132470
		private bool CheckSchemaRoot(SchemaType rootType, out string code)
		{
			code = null;
			if (this.schemaType == SchemaType.None)
			{
				this.schemaType = rootType;
			}
			switch (rootType)
			{
			case SchemaType.None:
			case SchemaType.DTD:
				code = "Expected schema root. Make sure the root element is <schema> and the namespace is 'http://www.w3.org/2001/XMLSchema' for an XSD schema or 'urn:schemas-microsoft-com:xml-data' for an XDR schema.";
				if (this.schemaType == SchemaType.XSD)
				{
					code = "The root element of a W3C XML Schema should be <schema> and its namespace should be 'http://www.w3.org/2001/XMLSchema'.";
				}
				return false;
			case SchemaType.XDR:
				if (this.schemaType == SchemaType.XSD)
				{
					code = "'XmlSchemaSet' can load only W3C XML Schemas.";
					return false;
				}
				if (this.schemaType != SchemaType.XDR)
				{
					code = "Different schema types cannot be mixed.";
					return false;
				}
				break;
			case SchemaType.XSD:
				if (this.schemaType != SchemaType.XSD)
				{
					code = "Different schema types cannot be mixed.";
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x001342F7 File Offset: 0x001324F7
		public SchemaType FinishParsing()
		{
			return this.schemaType;
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x001342FF File Offset: 0x001324FF
		public XmlSchema XmlSchema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (set) Token: 0x060036E5 RID: 14053 RVA: 0x00134307 File Offset: 0x00132507
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060036E6 RID: 14054 RVA: 0x00134310 File Offset: 0x00132510
		public SchemaInfo XdrSchema
		{
			get
			{
				return this.xdrSchema;
			}
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x00134318 File Offset: 0x00132518
		public bool ParseReaderNode()
		{
			if (this.reader.Depth > this.markupDepth)
			{
				if (this.processMarkup)
				{
					this.ProcessAppInfoDocMarkup(false);
				}
				return true;
			}
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				if (this.builder.ProcessElement(this.reader.Prefix, this.reader.LocalName, this.reader.NamespaceURI))
				{
					this.namespaceManager.PushScope();
					if (this.reader.MoveToFirstAttribute())
					{
						do
						{
							this.builder.ProcessAttribute(this.reader.Prefix, this.reader.LocalName, this.reader.NamespaceURI, this.reader.Value);
							if (Ref.Equal(this.reader.NamespaceURI, this.schemaNames.NsXmlNs) && this.isProcessNamespaces)
							{
								this.namespaceManager.AddNamespace((this.reader.Prefix.Length == 0) ? string.Empty : this.reader.LocalName, this.reader.Value);
							}
						}
						while (this.reader.MoveToNextAttribute());
						this.reader.MoveToElement();
					}
					this.builder.StartChildren();
					if (this.reader.IsEmptyElement)
					{
						this.namespaceManager.PopScope();
						this.builder.EndChildren();
						if (this.reader.Depth == this.schemaXmlDepth)
						{
							return false;
						}
					}
					else if (!this.builder.IsContentParsed())
					{
						this.markupDepth = this.reader.Depth;
						this.processMarkup = true;
						if (this.annotationNSManager == null)
						{
							this.annotationNSManager = new XmlNamespaceManager(this.nameTable);
							this.xmlns = this.nameTable.Add("xmlns");
						}
						this.ProcessAppInfoDocMarkup(true);
					}
				}
				else if (!this.reader.IsEmptyElement)
				{
					this.markupDepth = this.reader.Depth;
					this.processMarkup = false;
				}
			}
			else if (this.reader.NodeType == XmlNodeType.Text)
			{
				if (!this.xmlCharType.IsOnlyWhitespace(this.reader.Value))
				{
					this.builder.ProcessCData(this.reader.Value);
				}
			}
			else if (this.reader.NodeType == XmlNodeType.EntityReference || this.reader.NodeType == XmlNodeType.SignificantWhitespace || this.reader.NodeType == XmlNodeType.CDATA)
			{
				this.builder.ProcessCData(this.reader.Value);
			}
			else if (this.reader.NodeType == XmlNodeType.EndElement)
			{
				if (this.reader.Depth == this.markupDepth)
				{
					if (this.processMarkup)
					{
						XmlNodeList childNodes = this.parentNode.ChildNodes;
						XmlNode[] array = new XmlNode[childNodes.Count];
						for (int i = 0; i < childNodes.Count; i++)
						{
							array[i] = childNodes[i];
						}
						this.builder.ProcessMarkup(array);
						this.namespaceManager.PopScope();
						this.builder.EndChildren();
					}
					this.markupDepth = int.MaxValue;
				}
				else
				{
					this.namespaceManager.PopScope();
					this.builder.EndChildren();
				}
				if (this.reader.Depth == this.schemaXmlDepth)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x00134678 File Offset: 0x00132878
		private void ProcessAppInfoDocMarkup(bool root)
		{
			XmlNode newChild = null;
			switch (this.reader.NodeType)
			{
			case XmlNodeType.Element:
				this.annotationNSManager.PushScope();
				newChild = this.LoadElementNode(root);
				return;
			case XmlNodeType.Text:
				newChild = this.dummyDocument.CreateTextNode(this.reader.Value);
				break;
			case XmlNodeType.CDATA:
				newChild = this.dummyDocument.CreateCDataSection(this.reader.Value);
				break;
			case XmlNodeType.EntityReference:
				newChild = this.dummyDocument.CreateEntityReference(this.reader.Name);
				break;
			case XmlNodeType.ProcessingInstruction:
				newChild = this.dummyDocument.CreateProcessingInstruction(this.reader.Name, this.reader.Value);
				break;
			case XmlNodeType.Comment:
				newChild = this.dummyDocument.CreateComment(this.reader.Value);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.EndEntity:
				return;
			case XmlNodeType.SignificantWhitespace:
				newChild = this.dummyDocument.CreateSignificantWhitespace(this.reader.Value);
				break;
			case XmlNodeType.EndElement:
				this.annotationNSManager.PopScope();
				this.parentNode = this.parentNode.ParentNode;
				return;
			}
			this.parentNode.AppendChild(newChild);
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x001347C8 File Offset: 0x001329C8
		private XmlElement LoadElementNode(bool root)
		{
			XmlReader xmlReader = this.reader;
			bool isEmptyElement = xmlReader.IsEmptyElement;
			XmlElement xmlElement = this.dummyDocument.CreateElement(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
			xmlElement.IsEmpty = isEmptyElement;
			if (root)
			{
				this.parentNode = xmlElement;
			}
			else
			{
				XmlAttributeCollection attributes = xmlElement.Attributes;
				if (xmlReader.MoveToFirstAttribute())
				{
					do
					{
						if (Ref.Equal(xmlReader.NamespaceURI, this.schemaNames.NsXmlNs))
						{
							this.annotationNSManager.AddNamespace((xmlReader.Prefix.Length == 0) ? string.Empty : this.reader.LocalName, this.reader.Value);
						}
						XmlAttribute node = this.LoadAttributeNode();
						attributes.Append(node);
					}
					while (xmlReader.MoveToNextAttribute());
				}
				xmlReader.MoveToElement();
				string text = this.annotationNSManager.LookupNamespace(xmlReader.Prefix);
				if (text == null)
				{
					XmlAttribute node2 = this.CreateXmlNsAttribute(xmlReader.Prefix, this.namespaceManager.LookupNamespace(xmlReader.Prefix));
					attributes.Append(node2);
				}
				else if (text.Length == 0)
				{
					string text2 = this.namespaceManager.LookupNamespace(xmlReader.Prefix);
					if (text2 != string.Empty)
					{
						XmlAttribute node3 = this.CreateXmlNsAttribute(xmlReader.Prefix, text2);
						attributes.Append(node3);
					}
				}
				while (xmlReader.MoveToNextAttribute())
				{
					if (xmlReader.Prefix.Length != 0 && this.annotationNSManager.LookupNamespace(xmlReader.Prefix) == null)
					{
						XmlAttribute node4 = this.CreateXmlNsAttribute(xmlReader.Prefix, this.namespaceManager.LookupNamespace(xmlReader.Prefix));
						attributes.Append(node4);
					}
				}
				xmlReader.MoveToElement();
				this.parentNode.AppendChild(xmlElement);
				if (!xmlReader.IsEmptyElement)
				{
					this.parentNode = xmlElement;
				}
			}
			return xmlElement;
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x00134994 File Offset: 0x00132B94
		private XmlAttribute CreateXmlNsAttribute(string prefix, string value)
		{
			XmlAttribute xmlAttribute;
			if (prefix.Length == 0)
			{
				xmlAttribute = this.dummyDocument.CreateAttribute(string.Empty, this.xmlns, "http://www.w3.org/2000/xmlns/");
			}
			else
			{
				xmlAttribute = this.dummyDocument.CreateAttribute(this.xmlns, prefix, "http://www.w3.org/2000/xmlns/");
			}
			xmlAttribute.AppendChild(this.dummyDocument.CreateTextNode(value));
			this.annotationNSManager.AddNamespace(prefix, value);
			return xmlAttribute;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x00134A00 File Offset: 0x00132C00
		private XmlAttribute LoadAttributeNode()
		{
			XmlReader xmlReader = this.reader;
			XmlAttribute xmlAttribute = this.dummyDocument.CreateAttribute(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
			while (xmlReader.ReadAttributeValue())
			{
				XmlNodeType nodeType = xmlReader.NodeType;
				if (nodeType != XmlNodeType.Text)
				{
					if (nodeType != XmlNodeType.EntityReference)
					{
						throw XmlLoader.UnexpectedNodeType(xmlReader.NodeType);
					}
					xmlAttribute.AppendChild(this.LoadEntityReferenceInAttribute());
				}
				else
				{
					xmlAttribute.AppendChild(this.dummyDocument.CreateTextNode(xmlReader.Value));
				}
			}
			return xmlAttribute;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x00134A84 File Offset: 0x00132C84
		private XmlEntityReference LoadEntityReferenceInAttribute()
		{
			XmlEntityReference xmlEntityReference = this.dummyDocument.CreateEntityReference(this.reader.LocalName);
			if (!this.reader.CanResolveEntity)
			{
				return xmlEntityReference;
			}
			this.reader.ResolveEntity();
			while (this.reader.ReadAttributeValue())
			{
				XmlNodeType nodeType = this.reader.NodeType;
				if (nodeType != XmlNodeType.Text)
				{
					if (nodeType != XmlNodeType.EntityReference)
					{
						if (nodeType != XmlNodeType.EndEntity)
						{
							throw XmlLoader.UnexpectedNodeType(this.reader.NodeType);
						}
						if (xmlEntityReference.ChildNodes.Count == 0)
						{
							xmlEntityReference.AppendChild(this.dummyDocument.CreateTextNode(string.Empty));
						}
						return xmlEntityReference;
					}
					else
					{
						xmlEntityReference.AppendChild(this.LoadEntityReferenceInAttribute());
					}
				}
				else
				{
					xmlEntityReference.AppendChild(this.dummyDocument.CreateTextNode(this.reader.Value));
				}
			}
			return xmlEntityReference;
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x00134B58 File Offset: 0x00132D58
		public Task<SchemaType> ParseAsync(XmlReader reader, string targetNamespace)
		{
			Parser.<ParseAsync>d__37 <ParseAsync>d__;
			<ParseAsync>d__.<>4__this = this;
			<ParseAsync>d__.reader = reader;
			<ParseAsync>d__.targetNamespace = targetNamespace;
			<ParseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SchemaType>.Create();
			<ParseAsync>d__.<>1__state = -1;
			<ParseAsync>d__.<>t__builder.Start<Parser.<ParseAsync>d__37>(ref <ParseAsync>d__);
			return <ParseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x00134BAC File Offset: 0x00132DAC
		public Task StartParsingAsync(XmlReader reader, string targetNamespace)
		{
			Parser.<StartParsingAsync>d__38 <StartParsingAsync>d__;
			<StartParsingAsync>d__.<>4__this = this;
			<StartParsingAsync>d__.reader = reader;
			<StartParsingAsync>d__.targetNamespace = targetNamespace;
			<StartParsingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StartParsingAsync>d__.<>1__state = -1;
			<StartParsingAsync>d__.<>t__builder.Start<Parser.<StartParsingAsync>d__38>(ref <StartParsingAsync>d__);
			return <StartParsingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04002831 RID: 10289
		private SchemaType schemaType;

		// Token: 0x04002832 RID: 10290
		private XmlNameTable nameTable;

		// Token: 0x04002833 RID: 10291
		private SchemaNames schemaNames;

		// Token: 0x04002834 RID: 10292
		private ValidationEventHandler eventHandler;

		// Token: 0x04002835 RID: 10293
		private XmlNamespaceManager namespaceManager;

		// Token: 0x04002836 RID: 10294
		private XmlReader reader;

		// Token: 0x04002837 RID: 10295
		private PositionInfo positionInfo;

		// Token: 0x04002838 RID: 10296
		private bool isProcessNamespaces;

		// Token: 0x04002839 RID: 10297
		private int schemaXmlDepth;

		// Token: 0x0400283A RID: 10298
		private int markupDepth;

		// Token: 0x0400283B RID: 10299
		private SchemaBuilder builder;

		// Token: 0x0400283C RID: 10300
		private XmlSchema schema;

		// Token: 0x0400283D RID: 10301
		private SchemaInfo xdrSchema;

		// Token: 0x0400283E RID: 10302
		private XmlResolver xmlResolver;

		// Token: 0x0400283F RID: 10303
		private XmlDocument dummyDocument;

		// Token: 0x04002840 RID: 10304
		private bool processMarkup;

		// Token: 0x04002841 RID: 10305
		private XmlNode parentNode;

		// Token: 0x04002842 RID: 10306
		private XmlNamespaceManager annotationNSManager;

		// Token: 0x04002843 RID: 10307
		private string xmlns;

		// Token: 0x04002844 RID: 10308
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x02000566 RID: 1382
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseAsync>d__37 : IAsyncStateMachine
		{
			// Token: 0x060036EF RID: 14063 RVA: 0x00134C00 File Offset: 0x00132E00
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				Parser parser = this.<>4__this;
				SchemaType result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_F8;
						}
						awaiter2 = parser.StartParsingAsync(this.reader, this.targetNamespace).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, Parser.<ParseAsync>d__37>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					IL_89:
					bool flag = parser.ParseReaderNode();
					if (!flag)
					{
						goto IL_101;
					}
					awaiter = this.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, Parser.<ParseAsync>d__37>(ref awaiter, ref this);
						return;
					}
					IL_F8:
					flag = awaiter.GetResult();
					IL_101:
					if (flag)
					{
						goto IL_89;
					}
					result = parser.FinishParsing();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060036F0 RID: 14064 RVA: 0x00134D64 File Offset: 0x00132F64
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002845 RID: 10309
			public int <>1__state;

			// Token: 0x04002846 RID: 10310
			public AsyncTaskMethodBuilder<SchemaType> <>t__builder;

			// Token: 0x04002847 RID: 10311
			public Parser <>4__this;

			// Token: 0x04002848 RID: 10312
			public XmlReader reader;

			// Token: 0x04002849 RID: 10313
			public string targetNamespace;

			// Token: 0x0400284A RID: 10314
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400284B RID: 10315
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000567 RID: 1383
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <StartParsingAsync>d__38 : IAsyncStateMachine
		{
			// Token: 0x060036F1 RID: 14065 RVA: 0x00134D74 File Offset: 0x00132F74
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				Parser parser = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_E8;
					}
					parser.reader = this.reader;
					parser.positionInfo = PositionInfo.GetPositionInfo(this.reader);
					parser.namespaceManager = this.reader.NamespaceManager;
					if (parser.namespaceManager == null)
					{
						parser.namespaceManager = new XmlNamespaceManager(parser.nameTable);
						parser.isProcessNamespaces = true;
					}
					else
					{
						parser.isProcessNamespaces = false;
					}
					IL_6B:
					bool flag = this.reader.NodeType != XmlNodeType.Element;
					if (!flag)
					{
						goto IL_F1;
					}
					awaiter = this.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, Parser.<StartParsingAsync>d__38>(ref awaiter, ref this);
						return;
					}
					IL_E8:
					flag = awaiter.GetResult();
					IL_F1:
					if (flag)
					{
						goto IL_6B;
					}
					parser.markupDepth = int.MaxValue;
					parser.schemaXmlDepth = this.reader.Depth;
					SchemaType rootType = parser.schemaNames.SchemaTypeFromRoot(this.reader.LocalName, this.reader.NamespaceURI);
					string res;
					if (!parser.CheckSchemaRoot(rootType, out res))
					{
						throw new XmlSchemaException(res, this.reader.BaseURI, parser.positionInfo.LineNumber, parser.positionInfo.LinePosition);
					}
					if (parser.schemaType == SchemaType.XSD)
					{
						parser.schema = new XmlSchema();
						parser.schema.BaseUri = new Uri(this.reader.BaseURI, UriKind.RelativeOrAbsolute);
						parser.builder = new XsdBuilder(this.reader, parser.namespaceManager, parser.schema, parser.nameTable, parser.schemaNames, parser.eventHandler);
					}
					else
					{
						parser.xdrSchema = new SchemaInfo();
						parser.xdrSchema.SchemaType = SchemaType.XDR;
						parser.builder = new XdrBuilder(this.reader, parser.namespaceManager, parser.xdrSchema, this.targetNamespace, parser.nameTable, parser.schemaNames, parser.eventHandler);
						((XdrBuilder)parser.builder).XmlResolver = parser.xmlResolver;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060036F2 RID: 14066 RVA: 0x00134FF8 File Offset: 0x001331F8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400284C RID: 10316
			public int <>1__state;

			// Token: 0x0400284D RID: 10317
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400284E RID: 10318
			public Parser <>4__this;

			// Token: 0x0400284F RID: 10319
			public XmlReader reader;

			// Token: 0x04002850 RID: 10320
			public string targetNamespace;

			// Token: 0x04002851 RID: 10321
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
