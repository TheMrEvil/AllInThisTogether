using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Xml.Schema;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200047B RID: 1147
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlQueryRuntime
	{
		// Token: 0x06002C9A RID: 11418 RVA: 0x00107718 File Offset: 0x00105918
		internal XmlQueryRuntime(XmlQueryStaticData data, object defaultDataSource, XmlResolver dataSources, XsltArgumentList argList, XmlSequenceWriter seqWrt)
		{
			string[] names = data.Names;
			Int32Pair[] array = data.Filters;
			WhitespaceRuleLookup wsRules = (data.WhitespaceRules != null && data.WhitespaceRules.Count != 0) ? new WhitespaceRuleLookup(data.WhitespaceRules) : null;
			this.ctxt = new XmlQueryContext(this, defaultDataSource, dataSources, argList, wsRules);
			this.xsltLib = null;
			this.earlyInfo = data.EarlyBound;
			this.earlyObjects = ((this.earlyInfo != null) ? new object[this.earlyInfo.Length] : null);
			this.globalNames = data.GlobalNames;
			this.globalValues = ((this.globalNames != null) ? new object[this.globalNames.Length] : null);
			this.nameTableQuery = this.ctxt.QueryNameTable;
			this.atomizedNames = null;
			if (names != null)
			{
				XmlNameTable defaultNameTable = this.ctxt.DefaultNameTable;
				this.atomizedNames = new string[names.Length];
				if (defaultNameTable != this.nameTableQuery && defaultNameTable != null)
				{
					for (int i = 0; i < names.Length; i++)
					{
						string text = defaultNameTable.Get(names[i]);
						this.atomizedNames[i] = this.nameTableQuery.Add(text ?? names[i]);
					}
				}
				else
				{
					for (int i = 0; i < names.Length; i++)
					{
						this.atomizedNames[i] = this.nameTableQuery.Add(names[i]);
					}
				}
			}
			this.filters = null;
			if (array != null)
			{
				this.filters = new XmlNavigatorFilter[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.filters[i] = XmlNavNameFilter.Create(this.atomizedNames[array[i].Left], this.atomizedNames[array[i].Right]);
				}
			}
			this.prefixMappingsList = data.PrefixMappingsList;
			this.types = data.Types;
			this.collations = data.Collations;
			this.docOrderCmp = new DocumentOrderComparer();
			this.indexes = null;
			this.stkOutput = new Stack<XmlQueryOutput>(16);
			this.output = new XmlQueryOutput(this, seqWrt);
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x00107916 File Offset: 0x00105B16
		public string[] DebugGetGlobalNames()
		{
			return this.globalNames;
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x00107920 File Offset: 0x00105B20
		public IList DebugGetGlobalValue(string name)
		{
			for (int i = 0; i < this.globalNames.Length; i++)
			{
				if (this.globalNames[i] == name)
				{
					return (IList)this.globalValues[i];
				}
			}
			return null;
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x00107960 File Offset: 0x00105B60
		public void DebugSetGlobalValue(string name, object value)
		{
			for (int i = 0; i < this.globalNames.Length; i++)
			{
				if (this.globalNames[i] == name)
				{
					this.globalValues[i] = (IList<XPathItem>)XmlAnyListConverter.ItemList.ChangeType(value, typeof(XPathItem[]), null);
					return;
				}
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x001079B4 File Offset: 0x00105BB4
		public object DebugGetXsltValue(IList seq)
		{
			if (seq != null && seq.Count == 1)
			{
				XPathItem xpathItem = seq[0] as XPathItem;
				if (xpathItem != null && !xpathItem.IsNode)
				{
					return xpathItem.TypedValue;
				}
				if (xpathItem is RtfNavigator)
				{
					return ((RtfNavigator)xpathItem).ToNavigator();
				}
			}
			return seq;
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x00107A01 File Offset: 0x00105C01
		public XmlQueryContext ExternalContext
		{
			get
			{
				return this.ctxt;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x00107A09 File Offset: 0x00105C09
		public XsltLibrary XsltFunctions
		{
			get
			{
				if (this.xsltLib == null)
				{
					this.xsltLib = new XsltLibrary(this);
				}
				return this.xsltLib;
			}
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x00107A28 File Offset: 0x00105C28
		public object GetEarlyBoundObject(int index)
		{
			object obj = this.earlyObjects[index];
			if (obj == null)
			{
				obj = this.earlyInfo[index].CreateObject();
				this.earlyObjects[index] = obj;
			}
			return obj;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x00107A5C File Offset: 0x00105C5C
		public bool EarlyBoundFunctionExists(string name, string namespaceUri)
		{
			if (this.earlyInfo == null)
			{
				return false;
			}
			for (int i = 0; i < this.earlyInfo.Length; i++)
			{
				if (namespaceUri == this.earlyInfo[i].NamespaceUri)
				{
					return new XmlExtensionFunction(name, namespaceUri, -1, this.earlyInfo[i].EarlyBoundType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public).CanBind();
				}
			}
			return false;
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x00107AB9 File Offset: 0x00105CB9
		public bool IsGlobalComputed(int index)
		{
			return this.globalValues[index] != null;
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x00107AC6 File Offset: 0x00105CC6
		public object GetGlobalValue(int index)
		{
			return this.globalValues[index];
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x00107AD0 File Offset: 0x00105CD0
		public void SetGlobalValue(int index, object value)
		{
			this.globalValues[index] = value;
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x00107ADB File Offset: 0x00105CDB
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTableQuery;
			}
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x00107AE3 File Offset: 0x00105CE3
		public string GetAtomizedName(int index)
		{
			return this.atomizedNames[index];
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x00107AED File Offset: 0x00105CED
		public XmlNavigatorFilter GetNameFilter(int index)
		{
			return this.filters[index];
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x00107AF7 File Offset: 0x00105CF7
		public XmlNavigatorFilter GetTypeFilter(XPathNodeType nodeType)
		{
			if (nodeType == XPathNodeType.All)
			{
				return XmlNavNeverFilter.Create();
			}
			if (nodeType == XPathNodeType.Attribute)
			{
				return XmlNavAttrFilter.Create();
			}
			return XmlNavTypeFilter.Create(nodeType);
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x00107B14 File Offset: 0x00105D14
		public XmlQualifiedName ParseTagName(string tagName, int indexPrefixMappings)
		{
			string text;
			string name;
			string ns;
			this.ParseTagName(tagName, indexPrefixMappings, out text, out name, out ns);
			return new XmlQualifiedName(name, ns);
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x00107B38 File Offset: 0x00105D38
		public XmlQualifiedName ParseTagName(string tagName, string ns)
		{
			string text;
			string name;
			ValidateNames.ParseQNameThrow(tagName, out text, out name);
			return new XmlQualifiedName(name, ns);
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x00107B58 File Offset: 0x00105D58
		internal void ParseTagName(string tagName, int idxPrefixMappings, out string prefix, out string localName, out string ns)
		{
			ValidateNames.ParseQNameThrow(tagName, out prefix, out localName);
			ns = null;
			foreach (StringPair stringPair in this.prefixMappingsList[idxPrefixMappings])
			{
				if (prefix == stringPair.Left)
				{
					ns = stringPair.Right;
					break;
				}
			}
			if (ns != null)
			{
				return;
			}
			if (prefix.Length == 0)
			{
				ns = "";
				return;
			}
			if (prefix.Equals("xml"))
			{
				ns = "http://www.w3.org/XML/1998/namespace";
				return;
			}
			if (prefix.Equals("xmlns"))
			{
				ns = "http://www.w3.org/2000/xmlns/";
				return;
			}
			throw new XslTransformException("Prefix '{0}' is not defined.", new string[]
			{
				prefix
			});
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x00107C08 File Offset: 0x00105E08
		public bool IsQNameEqual(XPathNavigator n1, XPathNavigator n2)
		{
			if (n1.NameTable == n2.NameTable)
			{
				return n1.LocalName == n2.LocalName && n1.NamespaceURI == n2.NamespaceURI;
			}
			return n1.LocalName == n2.LocalName && n1.NamespaceURI == n2.NamespaceURI;
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x00107C68 File Offset: 0x00105E68
		public bool IsQNameEqual(XPathNavigator navigator, int indexLocalName, int indexNamespaceUri)
		{
			if (navigator.NameTable == this.nameTableQuery)
			{
				return this.GetAtomizedName(indexLocalName) == navigator.LocalName && this.GetAtomizedName(indexNamespaceUri) == navigator.NamespaceURI;
			}
			return this.GetAtomizedName(indexLocalName) == navigator.LocalName && this.GetAtomizedName(indexNamespaceUri) == navigator.NamespaceURI;
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x00107CCC File Offset: 0x00105ECC
		internal XmlQueryType[] XmlTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x00107CD4 File Offset: 0x00105ED4
		internal XmlQueryType GetXmlType(int idxType)
		{
			return this.types[idxType];
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x00107CDE File Offset: 0x00105EDE
		public object ChangeTypeXsltArgument(int indexType, object value, Type destinationType)
		{
			return this.ChangeTypeXsltArgument(this.GetXmlType(indexType), value, destinationType);
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x00107CF0 File Offset: 0x00105EF0
		internal object ChangeTypeXsltArgument(XmlQueryType xmlType, object value, Type destinationType)
		{
			XmlTypeCode typeCode = xmlType.TypeCode;
			if (typeCode <= XmlTypeCode.Node)
			{
				if (typeCode != XmlTypeCode.Item)
				{
					if (typeCode == XmlTypeCode.Node)
					{
						if (destinationType == XsltConvert.XPathNodeIteratorType)
						{
							value = new XPathArrayIterator((IList)value);
						}
						else if (destinationType == XsltConvert.XPathNavigatorArrayType)
						{
							IList<XPathNavigator> list = (IList<XPathNavigator>)value;
							XPathNavigator[] array = new XPathNavigator[list.Count];
							for (int i = 0; i < list.Count; i++)
							{
								array[i] = list[i];
							}
							value = array;
						}
					}
				}
				else
				{
					if (destinationType != XsltConvert.ObjectType)
					{
						throw new XslTransformException("Extension function parameters or return values which have Clr type '{0}' are not supported.", new string[]
						{
							destinationType.Name
						});
					}
					IList<XPathItem> list2 = (IList<XPathItem>)value;
					if (list2.Count == 1)
					{
						XPathItem xpathItem = list2[0];
						if (xpathItem.IsNode)
						{
							RtfNavigator rtfNavigator = xpathItem as RtfNavigator;
							if (rtfNavigator != null)
							{
								value = rtfNavigator.ToNavigator();
							}
							else
							{
								value = new XPathArrayIterator((IList)value);
							}
						}
						else
						{
							value = xpathItem.TypedValue;
						}
					}
					else
					{
						value = new XPathArrayIterator((IList)value);
					}
				}
			}
			else if (typeCode != XmlTypeCode.String)
			{
				if (typeCode == XmlTypeCode.Double)
				{
					if (destinationType != XsltConvert.DoubleType)
					{
						value = Convert.ChangeType(value, destinationType, CultureInfo.InvariantCulture);
					}
				}
			}
			else if (destinationType == XsltConvert.DateTimeType)
			{
				value = XsltConvert.ToDateTime((string)value);
			}
			return value;
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x00107E62 File Offset: 0x00106062
		public object ChangeTypeXsltResult(int indexType, object value)
		{
			return this.ChangeTypeXsltResult(this.GetXmlType(indexType), value);
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x00107E74 File Offset: 0x00106074
		internal object ChangeTypeXsltResult(XmlQueryType xmlType, object value)
		{
			if (value == null)
			{
				throw new XslTransformException("Extension functions cannot return null values.", new string[]
				{
					string.Empty
				});
			}
			XmlTypeCode typeCode = xmlType.TypeCode;
			if (typeCode <= XmlTypeCode.Node)
			{
				if (typeCode != XmlTypeCode.Item)
				{
					if (typeCode == XmlTypeCode.Node)
					{
						if (!xmlType.IsSingleton)
						{
							XPathArrayIterator xpathArrayIterator = value as XPathArrayIterator;
							if (xpathArrayIterator != null && xpathArrayIterator.AsList is XmlQueryNodeSequence)
							{
								value = (xpathArrayIterator.AsList as XmlQueryNodeSequence);
							}
							else
							{
								XmlQueryNodeSequence xmlQueryNodeSequence = new XmlQueryNodeSequence();
								IList list = value as IList;
								if (list != null)
								{
									for (int i = 0; i < list.Count; i++)
									{
										xmlQueryNodeSequence.Add(XmlQueryRuntime.EnsureNavigator(list[i]));
									}
								}
								else
								{
									foreach (object value2 in ((IEnumerable)value))
									{
										xmlQueryNodeSequence.Add(XmlQueryRuntime.EnsureNavigator(value2));
									}
								}
								value = xmlQueryNodeSequence;
							}
							value = ((XmlQueryNodeSequence)value).DocOrderDistinct(this.docOrderCmp);
						}
					}
				}
				else
				{
					Type type = value.GetType();
					XmlTypeCode typeCode2 = XsltConvert.InferXsltType(type).TypeCode;
					if (typeCode2 != XmlTypeCode.Item)
					{
						if (typeCode2 != XmlTypeCode.Node)
						{
							switch (typeCode2)
							{
							case XmlTypeCode.String:
								if (type == XsltConvert.DateTimeType)
								{
									value = new XmlQueryItemSequence(new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), XsltConvert.ToString((DateTime)value)));
								}
								else
								{
									value = new XmlQueryItemSequence(new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), value));
								}
								break;
							case XmlTypeCode.Boolean:
								value = new XmlQueryItemSequence(new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean), value));
								break;
							case XmlTypeCode.Double:
								value = new XmlQueryItemSequence(new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Double), ((IConvertible)value).ToDouble(null)));
								break;
							}
						}
						else
						{
							value = this.ChangeTypeXsltResult(XmlQueryTypeFactory.NodeS, value);
						}
					}
					else if (value is XPathNodeIterator)
					{
						value = this.ChangeTypeXsltResult(XmlQueryTypeFactory.NodeS, value);
					}
					else
					{
						IXPathNavigable ixpathNavigable = value as IXPathNavigable;
						if (ixpathNavigable == null)
						{
							throw new XslTransformException("Extension function parameters or return values which have Clr type '{0}' are not supported.", new string[]
							{
								type.Name
							});
						}
						if (value is XPathNavigator)
						{
							value = new XmlQueryNodeSequence((XPathNavigator)value);
						}
						else
						{
							value = new XmlQueryNodeSequence(ixpathNavigable.CreateNavigator());
						}
					}
				}
			}
			else if (typeCode != XmlTypeCode.String)
			{
				if (typeCode == XmlTypeCode.Double)
				{
					if (value.GetType() != XsltConvert.DoubleType)
					{
						value = ((IConvertible)value).ToDouble(null);
					}
				}
			}
			else if (value.GetType() == XsltConvert.DateTimeType)
			{
				value = XsltConvert.ToString((DateTime)value);
			}
			return value;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x00108140 File Offset: 0x00106340
		private static XPathNavigator EnsureNavigator(object value)
		{
			XPathNavigator xpathNavigator = value as XPathNavigator;
			if (xpathNavigator == null)
			{
				throw new XslTransformException("Extension functions cannot return null values.", new string[]
				{
					string.Empty
				});
			}
			return xpathNavigator;
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x00108174 File Offset: 0x00106374
		public bool MatchesXmlType(IList<XPathItem> seq, int indexType)
		{
			XmlQueryType xmlQueryType = this.GetXmlType(indexType);
			int count = seq.Count;
			XmlQueryCardinality left;
			if (count != 0)
			{
				if (count != 1)
				{
					left = XmlQueryCardinality.More;
				}
				else
				{
					left = XmlQueryCardinality.One;
				}
			}
			else
			{
				left = XmlQueryCardinality.Zero;
			}
			if (!(left <= xmlQueryType.Cardinality))
			{
				return false;
			}
			xmlQueryType = xmlQueryType.Prime;
			for (int i = 0; i < seq.Count; i++)
			{
				if (!this.CreateXmlType(seq[0]).IsSubtypeOf(xmlQueryType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x001081EF File Offset: 0x001063EF
		public bool MatchesXmlType(XPathItem item, int indexType)
		{
			return this.CreateXmlType(item).IsSubtypeOf(this.GetXmlType(indexType));
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x00108204 File Offset: 0x00106404
		public bool MatchesXmlType(IList<XPathItem> seq, XmlTypeCode code)
		{
			return seq.Count == 1 && this.MatchesXmlType(seq[0], code);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x00108220 File Offset: 0x00106420
		public bool MatchesXmlType(XPathItem item, XmlTypeCode code)
		{
			if (code > XmlTypeCode.AnyAtomicType)
			{
				return !item.IsNode && item.XmlType.TypeCode == code;
			}
			if (code == XmlTypeCode.Item)
			{
				return true;
			}
			if (code == XmlTypeCode.Node)
			{
				return item.IsNode;
			}
			if (code == XmlTypeCode.AnyAtomicType)
			{
				return !item.IsNode;
			}
			if (!item.IsNode)
			{
				return false;
			}
			switch (((XPathNavigator)item).NodeType)
			{
			case XPathNodeType.Root:
				return code == XmlTypeCode.Document;
			case XPathNodeType.Element:
				return code == XmlTypeCode.Element;
			case XPathNodeType.Attribute:
				return code == XmlTypeCode.Attribute;
			case XPathNodeType.Namespace:
				return code == XmlTypeCode.Namespace;
			case XPathNodeType.Text:
				return code == XmlTypeCode.Text;
			case XPathNodeType.SignificantWhitespace:
				return code == XmlTypeCode.Text;
			case XPathNodeType.Whitespace:
				return code == XmlTypeCode.Text;
			case XPathNodeType.ProcessingInstruction:
				return code == XmlTypeCode.ProcessingInstruction;
			case XPathNodeType.Comment:
				return code == XmlTypeCode.Comment;
			default:
				return false;
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x001082E0 File Offset: 0x001064E0
		private XmlQueryType CreateXmlType(XPathItem item)
		{
			if (!item.IsNode)
			{
				return XmlQueryTypeFactory.Type((XmlSchemaSimpleType)item.XmlType, true);
			}
			if (item is RtfNavigator)
			{
				return XmlQueryTypeFactory.Node;
			}
			XPathNavigator xpathNavigator = (XPathNavigator)item;
			XPathNodeType nodeType = xpathNavigator.NodeType;
			if (nodeType > XPathNodeType.Element)
			{
				if (nodeType != XPathNodeType.Attribute)
				{
					return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.Wildcard, XmlSchemaComplexType.AnyType, false);
				}
				if (xpathNavigator.XmlType == null)
				{
					return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.New(xpathNavigator.LocalName, xpathNavigator.NamespaceURI), DatatypeImplementation.UntypedAtomicType, false);
				}
				return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.New(xpathNavigator.LocalName, xpathNavigator.NamespaceURI), xpathNavigator.XmlType, false);
			}
			else
			{
				if (xpathNavigator.XmlType == null)
				{
					return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.New(xpathNavigator.LocalName, xpathNavigator.NamespaceURI), XmlSchemaComplexType.UntypedAnyType, false);
				}
				return XmlQueryTypeFactory.Type(xpathNavigator.NodeType, XmlQualifiedNameTest.New(xpathNavigator.LocalName, xpathNavigator.NamespaceURI), xpathNavigator.XmlType, xpathNavigator.SchemaInfo.SchemaElement.IsNillable);
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x001083F6 File Offset: 0x001065F6
		public XmlCollation GetCollation(int index)
		{
			return this.collations[index];
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x00108400 File Offset: 0x00106600
		public XmlCollation CreateCollation(string collation)
		{
			return XmlCollation.Create(collation);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x00108408 File Offset: 0x00106608
		public int ComparePosition(XPathNavigator navigatorThis, XPathNavigator navigatorThat)
		{
			return this.docOrderCmp.Compare(navigatorThis, navigatorThat);
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x00108418 File Offset: 0x00106618
		public IList<XPathNavigator> DocOrderDistinct(IList<XPathNavigator> seq)
		{
			if (seq.Count <= 1)
			{
				return seq;
			}
			XmlQueryNodeSequence xmlQueryNodeSequence = (XmlQueryNodeSequence)seq;
			if (xmlQueryNodeSequence == null)
			{
				xmlQueryNodeSequence = new XmlQueryNodeSequence(seq);
			}
			return xmlQueryNodeSequence.DocOrderDistinct(this.docOrderCmp);
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x00108450 File Offset: 0x00106650
		public string GenerateId(XPathNavigator navigator)
		{
			return "ID" + this.docOrderCmp.GetDocumentIndex(navigator).ToString(CultureInfo.InvariantCulture) + navigator.UniqueId;
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x00108488 File Offset: 0x00106688
		public bool FindIndex(XPathNavigator context, int indexId, out XmlILIndex index)
		{
			XPathNavigator xpathNavigator = context.Clone();
			xpathNavigator.MoveToRoot();
			if (this.indexes != null && indexId < this.indexes.Length)
			{
				ArrayList arrayList = this.indexes[indexId];
				if (arrayList != null)
				{
					for (int i = 0; i < arrayList.Count; i += 2)
					{
						if (((XPathNavigator)arrayList[i]).IsSamePosition(xpathNavigator))
						{
							index = (XmlILIndex)arrayList[i + 1];
							return true;
						}
					}
				}
			}
			index = new XmlILIndex();
			return false;
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x00108500 File Offset: 0x00106700
		public void AddNewIndex(XPathNavigator context, int indexId, XmlILIndex index)
		{
			XPathNavigator xpathNavigator = context.Clone();
			xpathNavigator.MoveToRoot();
			if (this.indexes == null)
			{
				this.indexes = new ArrayList[indexId + 4];
			}
			else if (indexId >= this.indexes.Length)
			{
				ArrayList[] destinationArray = new ArrayList[indexId + 4];
				Array.Copy(this.indexes, 0, destinationArray, 0, this.indexes.Length);
				this.indexes = destinationArray;
			}
			ArrayList arrayList = this.indexes[indexId];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				this.indexes[indexId] = arrayList;
			}
			arrayList.Add(xpathNavigator);
			arrayList.Add(index);
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x0010858E File Offset: 0x0010678E
		public XmlQueryOutput Output
		{
			get
			{
				return this.output;
			}
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x00108598 File Offset: 0x00106798
		public void StartSequenceConstruction(out XmlQueryOutput output)
		{
			this.stkOutput.Push(this.output);
			output = (this.output = new XmlQueryOutput(this, new XmlCachedSequenceWriter()));
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x001085CC File Offset: 0x001067CC
		public IList<XPathItem> EndSequenceConstruction(out XmlQueryOutput output)
		{
			IList<XPathItem> resultSequence = ((XmlCachedSequenceWriter)this.output.SequenceWriter).ResultSequence;
			output = (this.output = this.stkOutput.Pop());
			return resultSequence;
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x00108604 File Offset: 0x00106804
		public void StartRtfConstruction(string baseUri, out XmlQueryOutput output)
		{
			this.stkOutput.Push(this.output);
			output = (this.output = new XmlQueryOutput(this, new XmlEventCache(baseUri, true)));
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x0010863C File Offset: 0x0010683C
		public XPathNavigator EndRtfConstruction(out XmlQueryOutput output)
		{
			XmlEventCache xmlEventCache = (XmlEventCache)this.output.Writer;
			output = (this.output = this.stkOutput.Pop());
			xmlEventCache.EndEvents();
			return new RtfTreeNavigator(xmlEventCache, this.nameTableQuery);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x00108680 File Offset: 0x00106880
		public XPathNavigator TextRtfConstruction(string text, string baseUri)
		{
			return new RtfTextNavigator(text, baseUri);
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x00108689 File Offset: 0x00106889
		public void SendMessage(string message)
		{
			this.ctxt.OnXsltMessageEncountered(message);
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x00108697 File Offset: 0x00106897
		public void ThrowException(string text)
		{
			throw new XslTransformException(text);
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x0010869F File Offset: 0x0010689F
		internal static XPathNavigator SyncToNavigator(XPathNavigator navigatorThis, XPathNavigator navigatorThat)
		{
			if (navigatorThis == null || !navigatorThis.MoveTo(navigatorThat))
			{
				return navigatorThat.Clone();
			}
			return navigatorThis;
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x001086B8 File Offset: 0x001068B8
		public static int OnCurrentNodeChanged(XPathNavigator currentNode)
		{
			IXmlLineInfo xmlLineInfo = currentNode as IXmlLineInfo;
			if (xmlLineInfo != null && (currentNode.NodeType != XPathNodeType.Namespace || !XmlQueryRuntime.IsInheritedNamespace(currentNode)))
			{
				XmlQueryRuntime.OnCurrentNodeChanged2(currentNode.BaseURI, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
			}
			return 0;
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x001086F8 File Offset: 0x001068F8
		private static bool IsInheritedNamespace(XPathNavigator node)
		{
			XPathNavigator xpathNavigator = node.Clone();
			if (xpathNavigator.MoveToParent() && xpathNavigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
			{
				while (xpathNavigator.LocalName != node.LocalName)
				{
					if (!xpathNavigator.MoveToNextNamespace(XPathNamespaceScope.Local))
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x0000B528 File Offset: 0x00009728
		private static void OnCurrentNodeChanged2(string baseUri, int lineNumber, int linePosition)
		{
		}

		// Token: 0x040022FA RID: 8954
		private XmlQueryContext ctxt;

		// Token: 0x040022FB RID: 8955
		private XsltLibrary xsltLib;

		// Token: 0x040022FC RID: 8956
		private EarlyBoundInfo[] earlyInfo;

		// Token: 0x040022FD RID: 8957
		private object[] earlyObjects;

		// Token: 0x040022FE RID: 8958
		private string[] globalNames;

		// Token: 0x040022FF RID: 8959
		private object[] globalValues;

		// Token: 0x04002300 RID: 8960
		private XmlNameTable nameTableQuery;

		// Token: 0x04002301 RID: 8961
		private string[] atomizedNames;

		// Token: 0x04002302 RID: 8962
		private XmlNavigatorFilter[] filters;

		// Token: 0x04002303 RID: 8963
		private StringPair[][] prefixMappingsList;

		// Token: 0x04002304 RID: 8964
		private XmlQueryType[] types;

		// Token: 0x04002305 RID: 8965
		private XmlCollation[] collations;

		// Token: 0x04002306 RID: 8966
		private DocumentOrderComparer docOrderCmp;

		// Token: 0x04002307 RID: 8967
		private ArrayList[] indexes;

		// Token: 0x04002308 RID: 8968
		private XmlQueryOutput output;

		// Token: 0x04002309 RID: 8969
		private Stack<XmlQueryOutput> stkOutput;

		// Token: 0x0400230A RID: 8970
		internal const BindingFlags EarlyBoundFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x0400230B RID: 8971
		internal const BindingFlags LateBoundFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}
