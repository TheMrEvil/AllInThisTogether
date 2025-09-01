using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200005D RID: 93
	internal class Utils
	{
		// Token: 0x060002DF RID: 735 RVA: 0x00002145 File Offset: 0x00000345
		private Utils()
		{
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000D79B File Offset: 0x0000B99B
		private static bool HasNamespace(XmlElement element, string prefix, string value)
		{
			return Utils.IsCommittedNamespace(element, prefix, value) || (element.Prefix == prefix && element.NamespaceURI == value);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		internal static bool IsCommittedNamespace(XmlElement element, string prefix, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			string name = (prefix.Length > 0) ? ("xmlns:" + prefix) : "xmlns";
			return element.HasAttribute(name) && element.GetAttribute(name) == value;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000D81C File Offset: 0x0000BA1C
		internal static bool IsRedundantNamespace(XmlElement element, string prefix, string value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			for (XmlNode parentNode = element.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
			{
				XmlElement xmlElement = parentNode as XmlElement;
				if (xmlElement != null && Utils.HasNamespace(xmlElement, prefix, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000D864 File Offset: 0x0000BA64
		internal static string GetAttribute(XmlElement element, string localName, string namespaceURI)
		{
			string text = element.HasAttribute(localName) ? element.GetAttribute(localName) : null;
			if (text == null && element.HasAttribute(localName, namespaceURI))
			{
				text = element.GetAttribute(localName, namespaceURI);
			}
			return text;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000D89C File Offset: 0x0000BA9C
		internal static bool HasAttribute(XmlElement element, string localName, string namespaceURI)
		{
			return element.HasAttribute(localName) || element.HasAttribute(localName, namespaceURI);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000D8B1 File Offset: 0x0000BAB1
		internal static bool VerifyAttributes(XmlElement element, string expectedAttrName)
		{
			string[] expectedAttrNames;
			if (expectedAttrName != null)
			{
				(expectedAttrNames = new string[1])[0] = expectedAttrName;
			}
			else
			{
				expectedAttrNames = null;
			}
			return Utils.VerifyAttributes(element, expectedAttrNames);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000D8CC File Offset: 0x0000BACC
		internal static bool VerifyAttributes(XmlElement element, string[] expectedAttrNames)
		{
			foreach (object obj in element.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				bool flag = xmlAttribute.Name == "xmlns" || xmlAttribute.Name.StartsWith("xmlns:") || xmlAttribute.Name == "xml:space" || xmlAttribute.Name == "xml:lang" || xmlAttribute.Name == "xml:base";
				int num = 0;
				while (!flag && expectedAttrNames != null && num < expectedAttrNames.Length)
				{
					flag = (xmlAttribute.Name == expectedAttrNames[num]);
					num++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		internal static bool IsNamespaceNode(XmlNode n)
		{
			return n.NodeType == XmlNodeType.Attribute && (n.Prefix.Equals("xmlns") || (n.Prefix.Length == 0 && n.LocalName.Equals("xmlns")));
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		internal static bool IsXmlNamespaceNode(XmlNode n)
		{
			return n.NodeType == XmlNodeType.Attribute && n.Prefix.Equals("xml");
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000DA14 File Offset: 0x0000BC14
		internal static bool IsDefaultNamespaceNode(XmlNode n)
		{
			bool flag = n.NodeType == XmlNodeType.Attribute && n.Prefix.Length == 0 && n.LocalName.Equals("xmlns");
			bool flag2 = Utils.IsXmlNamespaceNode(n);
			return flag || flag2;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000DA53 File Offset: 0x0000BC53
		internal static bool IsEmptyDefaultNamespaceNode(XmlNode n)
		{
			return Utils.IsDefaultNamespaceNode(n) && n.Value.Length == 0;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000DA6D File Offset: 0x0000BC6D
		internal static string GetNamespacePrefix(XmlAttribute a)
		{
			if (a.Prefix.Length != 0)
			{
				return a.LocalName;
			}
			return string.Empty;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000DA88 File Offset: 0x0000BC88
		internal static bool HasNamespacePrefix(XmlAttribute a, string nsPrefix)
		{
			return Utils.GetNamespacePrefix(a).Equals(nsPrefix);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000DA96 File Offset: 0x0000BC96
		internal static bool IsNonRedundantNamespaceDecl(XmlAttribute a, XmlAttribute nearestAncestorWithSamePrefix)
		{
			if (nearestAncestorWithSamePrefix == null)
			{
				return !Utils.IsEmptyDefaultNamespaceNode(a);
			}
			return !nearestAncestorWithSamePrefix.Value.Equals(a.Value);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00002856 File Offset: 0x00000A56
		internal static bool IsXmlPrefixDefinitionNode(XmlAttribute a)
		{
			return false;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000DAB9 File Offset: 0x0000BCB9
		internal static string DiscardWhiteSpaces(string inputBuffer)
		{
			return Utils.DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			char[] array = new char[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return new string(array);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000DB34 File Offset: 0x0000BD34
		internal static void SBReplaceCharWithString(StringBuilder sb, char oldChar, string newString)
		{
			int i = 0;
			int length = newString.Length;
			while (i < sb.Length)
			{
				if (sb[i] == oldChar)
				{
					sb.Remove(i, 1);
					sb.Insert(i, newString);
					i += length;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		internal static XmlReader PreProcessStreamInput(Stream inputStream, XmlResolver xmlResolver, string baseUri)
		{
			XmlReaderSettings secureXmlReaderSettings = Utils.GetSecureXmlReaderSettings(xmlResolver);
			return XmlReader.Create(inputStream, secureXmlReaderSettings, baseUri);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000DB98 File Offset: 0x0000BD98
		internal static XmlReaderSettings GetSecureXmlReaderSettings(XmlResolver xmlResolver)
		{
			return new XmlReaderSettings
			{
				XmlResolver = xmlResolver,
				DtdProcessing = DtdProcessing.Parse,
				MaxCharactersFromEntities = 10000000L,
				MaxCharactersInDocument = 0L
			};
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		internal static XmlDocument PreProcessDocumentInput(XmlDocument document, XmlResolver xmlResolver, string baseUri)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			MyXmlDocument myXmlDocument = new MyXmlDocument();
			myXmlDocument.PreserveWhitespace = document.PreserveWhitespace;
			using (TextReader textReader = new StringReader(document.OuterXml))
			{
				XmlReader reader = XmlReader.Create(textReader, new XmlReaderSettings
				{
					XmlResolver = xmlResolver,
					DtdProcessing = DtdProcessing.Parse,
					MaxCharactersFromEntities = 10000000L,
					MaxCharactersInDocument = 0L
				}, baseUri);
				myXmlDocument.Load(reader);
			}
			return myXmlDocument;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000DC54 File Offset: 0x0000BE54
		internal static XmlDocument PreProcessElementInput(XmlElement elem, XmlResolver xmlResolver, string baseUri)
		{
			if (elem == null)
			{
				throw new ArgumentNullException("elem");
			}
			MyXmlDocument myXmlDocument = new MyXmlDocument();
			myXmlDocument.PreserveWhitespace = true;
			using (TextReader textReader = new StringReader(elem.OuterXml))
			{
				XmlReader reader = XmlReader.Create(textReader, new XmlReaderSettings
				{
					XmlResolver = xmlResolver,
					DtdProcessing = DtdProcessing.Parse,
					MaxCharactersFromEntities = 10000000L,
					MaxCharactersInDocument = 0L
				}, baseUri);
				myXmlDocument.Load(reader);
			}
			return myXmlDocument;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		internal static XmlDocument DiscardComments(XmlDocument document)
		{
			XmlNodeList xmlNodeList = document.SelectNodes("//comment()");
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					xmlNode.ParentNode.RemoveChild(xmlNode);
				}
			}
			return document;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		internal static XmlNodeList AllDescendantNodes(XmlNode node, bool includeComments)
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList2 = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList3 = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList4 = new CanonicalXmlNodeList();
			int num = 0;
			canonicalXmlNodeList2.Add(node);
			do
			{
				XmlNode xmlNode = canonicalXmlNodeList2[num];
				XmlNodeList childNodes = xmlNode.ChildNodes;
				if (childNodes != null)
				{
					foreach (object obj in childNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						if (includeComments || !(xmlNode2 is XmlComment))
						{
							canonicalXmlNodeList2.Add(xmlNode2);
						}
					}
				}
				if (xmlNode.Attributes != null)
				{
					foreach (object obj2 in xmlNode.Attributes)
					{
						XmlNode xmlNode3 = (XmlNode)obj2;
						if (xmlNode3.LocalName == "xmlns" || xmlNode3.Prefix == "xmlns")
						{
							canonicalXmlNodeList4.Add(xmlNode3);
						}
						else
						{
							canonicalXmlNodeList3.Add(xmlNode3);
						}
					}
				}
				num++;
			}
			while (num < canonicalXmlNodeList2.Count);
			foreach (object obj3 in canonicalXmlNodeList2)
			{
				XmlNode value = (XmlNode)obj3;
				canonicalXmlNodeList.Add(value);
			}
			foreach (object obj4 in canonicalXmlNodeList3)
			{
				XmlNode value2 = (XmlNode)obj4;
				canonicalXmlNodeList.Add(value2);
			}
			foreach (object obj5 in canonicalXmlNodeList4)
			{
				XmlNode value3 = (XmlNode)obj5;
				canonicalXmlNodeList.Add(value3);
			}
			return canonicalXmlNodeList;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000DF7C File Offset: 0x0000C17C
		internal static bool NodeInList(XmlNode node, XmlNodeList nodeList)
		{
			using (IEnumerator enumerator = nodeList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((XmlNode)enumerator.Current == node)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		internal static string GetIdFromLocalUri(string uri, out bool discardComments)
		{
			string text = uri.Substring(1);
			discardComments = true;
			if (text.StartsWith("xpointer(id(", StringComparison.Ordinal))
			{
				int num = text.IndexOf("id(", StringComparison.Ordinal);
				int num2 = text.IndexOf(")", StringComparison.Ordinal);
				if (num2 < 0 || num2 < num + 3)
				{
					throw new CryptographicException("Malformed reference element.");
				}
				text = text.Substring(num + 3, num2 - num - 3);
				text = text.Replace("'", "");
				text = text.Replace("\"", "");
				discardComments = false;
			}
			return text;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000E060 File Offset: 0x0000C260
		internal static string ExtractIdFromLocalUri(string uri)
		{
			string text = uri.Substring(1);
			if (text.StartsWith("xpointer(id(", StringComparison.Ordinal))
			{
				int num = text.IndexOf("id(", StringComparison.Ordinal);
				int num2 = text.IndexOf(")", StringComparison.Ordinal);
				if (num2 < 0 || num2 < num + 3)
				{
					throw new CryptographicException("Malformed reference element.");
				}
				text = text.Substring(num + 3, num2 - num - 3);
				text = text.Replace("'", "");
				text = text.Replace("\"", "");
			}
			return text;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		internal static void RemoveAllChildren(XmlElement inputElement)
		{
			XmlNode nextSibling;
			for (XmlNode xmlNode = inputElement.FirstChild; xmlNode != null; xmlNode = nextSibling)
			{
				nextSibling = xmlNode.NextSibling;
				inputElement.RemoveChild(xmlNode);
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E10C File Offset: 0x0000C30C
		internal static long Pump(Stream input, Stream output)
		{
			MemoryStream memoryStream = input as MemoryStream;
			if (memoryStream != null && memoryStream.Position == 0L)
			{
				memoryStream.WriteTo(output);
				return memoryStream.Length;
			}
			byte[] buffer = new byte[4096];
			long num = 0L;
			int num2;
			while ((num2 = input.Read(buffer, 0, 4096)) > 0)
			{
				output.Write(buffer, 0, num2);
				num += (long)num2;
			}
			return num;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000E16C File Offset: 0x0000C36C
		internal static Hashtable TokenizePrefixListString(string s)
		{
			Hashtable hashtable = new Hashtable();
			if (s != null)
			{
				foreach (string text in s.Split(null))
				{
					if (text.Equals("#default"))
					{
						hashtable.Add(string.Empty, true);
					}
					else if (text.Length > 0)
					{
						hashtable.Add(text, true);
					}
				}
			}
			return hashtable;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000E1D3 File Offset: 0x0000C3D3
		internal static string EscapeWhitespaceData(string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(data);
			Utils.SBReplaceCharWithString(stringBuilder, '\r', "&#xD;");
			return stringBuilder.ToString();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		internal static string EscapeTextData(string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(data);
			stringBuilder.Replace("&", "&amp;");
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace(">", "&gt;");
			Utils.SBReplaceCharWithString(stringBuilder, '\r', "&#xD;");
			return stringBuilder.ToString();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000E253 File Offset: 0x0000C453
		internal static string EscapeCData(string data)
		{
			return Utils.EscapeTextData(data);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000E25C File Offset: 0x0000C45C
		internal static string EscapeAttributeValue(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			stringBuilder.Replace("&", "&amp;");
			stringBuilder.Replace("<", "&lt;");
			stringBuilder.Replace("\"", "&quot;");
			Utils.SBReplaceCharWithString(stringBuilder, '\t', "&#x9;");
			Utils.SBReplaceCharWithString(stringBuilder, '\n', "&#xA;");
			Utils.SBReplaceCharWithString(stringBuilder, '\r', "&#xD;");
			return stringBuilder.ToString();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		internal static XmlDocument GetOwnerDocument(XmlNodeList nodeList)
		{
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.OwnerDocument != null)
				{
					return xmlNode.OwnerDocument;
				}
			}
			return null;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000E33C File Offset: 0x0000C53C
		internal static void AddNamespaces(XmlElement elem, CanonicalXmlNodeList namespaces)
		{
			if (namespaces != null)
			{
				foreach (object obj in namespaces)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string text = (xmlNode.Prefix.Length > 0) ? (xmlNode.Prefix + ":" + xmlNode.LocalName) : xmlNode.LocalName;
					if (!elem.HasAttribute(text) && (!text.Equals("xmlns") || elem.Prefix.Length != 0))
					{
						XmlAttribute xmlAttribute = elem.OwnerDocument.CreateAttribute(text);
						xmlAttribute.Value = xmlNode.Value;
						elem.SetAttributeNode(xmlAttribute);
					}
				}
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E408 File Offset: 0x0000C608
		internal static void AddNamespaces(XmlElement elem, Hashtable namespaces)
		{
			if (namespaces != null)
			{
				foreach (object obj in namespaces.Keys)
				{
					string text = (string)obj;
					if (!elem.HasAttribute(text))
					{
						XmlAttribute xmlAttribute = elem.OwnerDocument.CreateAttribute(text);
						xmlAttribute.Value = (namespaces[text] as string);
						elem.SetAttributeNode(xmlAttribute);
					}
				}
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000E490 File Offset: 0x0000C690
		internal static CanonicalXmlNodeList GetPropagatedAttributes(XmlElement elem)
		{
			if (elem == null)
			{
				return null;
			}
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			XmlNode xmlNode = elem;
			if (xmlNode == null)
			{
				return null;
			}
			bool flag = true;
			while (xmlNode != null)
			{
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement == null)
				{
					xmlNode = xmlNode.ParentNode;
				}
				else
				{
					if (!Utils.IsCommittedNamespace(xmlElement, xmlElement.Prefix, xmlElement.NamespaceURI) && !Utils.IsRedundantNamespace(xmlElement, xmlElement.Prefix, xmlElement.NamespaceURI))
					{
						string name = (xmlElement.Prefix.Length > 0) ? ("xmlns:" + xmlElement.Prefix) : "xmlns";
						XmlAttribute xmlAttribute = elem.OwnerDocument.CreateAttribute(name);
						xmlAttribute.Value = xmlElement.NamespaceURI;
						canonicalXmlNodeList.Add(xmlAttribute);
					}
					if (xmlElement.HasAttributes)
					{
						foreach (object obj in xmlElement.Attributes)
						{
							XmlAttribute xmlAttribute2 = (XmlAttribute)obj;
							if (flag && xmlAttribute2.LocalName == "xmlns")
							{
								XmlAttribute xmlAttribute3 = elem.OwnerDocument.CreateAttribute("xmlns");
								xmlAttribute3.Value = xmlAttribute2.Value;
								canonicalXmlNodeList.Add(xmlAttribute3);
								flag = false;
							}
							else if (xmlAttribute2.Prefix == "xmlns" || xmlAttribute2.Prefix == "xml")
							{
								canonicalXmlNodeList.Add(xmlAttribute2);
							}
							else if (xmlAttribute2.NamespaceURI.Length > 0 && !Utils.IsCommittedNamespace(xmlElement, xmlAttribute2.Prefix, xmlAttribute2.NamespaceURI) && !Utils.IsRedundantNamespace(xmlElement, xmlAttribute2.Prefix, xmlAttribute2.NamespaceURI))
							{
								string name2 = (xmlAttribute2.Prefix.Length > 0) ? ("xmlns:" + xmlAttribute2.Prefix) : "xmlns";
								XmlAttribute xmlAttribute4 = elem.OwnerDocument.CreateAttribute(name2);
								xmlAttribute4.Value = xmlAttribute2.NamespaceURI;
								canonicalXmlNodeList.Add(xmlAttribute4);
							}
						}
					}
					xmlNode = xmlNode.ParentNode;
				}
			}
			return canonicalXmlNodeList;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000E6BC File Offset: 0x0000C8BC
		internal static byte[] ConvertIntToByteArray(int dwInput)
		{
			byte[] array = new byte[8];
			int num = 0;
			if (dwInput == 0)
			{
				return new byte[1];
			}
			int i = dwInput;
			while (i > 0)
			{
				int num2 = i % 256;
				array[num] = (byte)num2;
				i = (i - num2) / 256;
				num++;
			}
			byte[] array2 = new byte[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = array[num - j - 1];
			}
			return array2;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E728 File Offset: 0x0000C928
		internal static int ConvertByteArrayToInt(byte[] input)
		{
			int num = 0;
			for (int i = 0; i < input.Length; i++)
			{
				num *= 256;
				num += (int)input[i];
			}
			return num;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000E754 File Offset: 0x0000C954
		internal static int GetHexArraySize(byte[] hex)
		{
			int num = hex.Length;
			while (num-- > 0 && hex[num] == 0)
			{
			}
			return num + 1;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E778 File Offset: 0x0000C978
		internal static X509IssuerSerial CreateX509IssuerSerial(string issuerName, string serialNumber)
		{
			if (issuerName == null || issuerName.Length == 0)
			{
				throw new ArgumentException("String cannot be empty or null.", "issuerName");
			}
			if (serialNumber == null || serialNumber.Length == 0)
			{
				throw new ArgumentException("String cannot be empty or null.", "serialNumber");
			}
			return new X509IssuerSerial
			{
				IssuerName = issuerName,
				SerialNumber = serialNumber
			};
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		internal static X509Certificate2Collection BuildBagOfCerts(KeyInfoX509Data keyInfoX509Data, CertUsageType certUsageType)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			ArrayList arrayList = (certUsageType == CertUsageType.Decryption) ? new ArrayList() : null;
			if (keyInfoX509Data.Certificates != null)
			{
				foreach (object obj in keyInfoX509Data.Certificates)
				{
					X509Certificate2 x509Certificate = (X509Certificate2)obj;
					if (certUsageType != CertUsageType.Verification)
					{
						if (certUsageType == CertUsageType.Decryption)
						{
							arrayList.Add(Utils.CreateX509IssuerSerial(x509Certificate.IssuerName.Name, x509Certificate.SerialNumber));
						}
					}
					else
					{
						x509Certificate2Collection.Add(x509Certificate);
					}
				}
			}
			if (keyInfoX509Data.SubjectNames == null && keyInfoX509Data.IssuerSerials == null && keyInfoX509Data.SubjectKeyIds == null && arrayList == null)
			{
				return x509Certificate2Collection;
			}
			X509Store[] array = new X509Store[2];
			string storeName = (certUsageType == CertUsageType.Verification) ? "AddressBook" : "My";
			array[0] = new X509Store(storeName, StoreLocation.CurrentUser);
			array[1] = new X509Store(storeName, StoreLocation.LocalMachine);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					X509Certificate2Collection x509Certificate2Collection2 = null;
					try
					{
						array[i].Open(OpenFlags.OpenExistingOnly);
						x509Certificate2Collection2 = array[i].Certificates;
						array[i].Close();
						if (keyInfoX509Data.SubjectNames != null)
						{
							foreach (object obj2 in keyInfoX509Data.SubjectNames)
							{
								string findValue = (string)obj2;
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySubjectDistinguishedName, findValue, false);
							}
						}
						if (keyInfoX509Data.IssuerSerials != null)
						{
							foreach (object obj3 in keyInfoX509Data.IssuerSerials)
							{
								X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)obj3;
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindByIssuerDistinguishedName, x509IssuerSerial.IssuerName, false);
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySerialNumber, x509IssuerSerial.SerialNumber, false);
							}
						}
						if (keyInfoX509Data.SubjectKeyIds != null)
						{
							foreach (object obj4 in keyInfoX509Data.SubjectKeyIds)
							{
								string findValue2 = Utils.EncodeHexString((byte[])obj4);
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySubjectKeyIdentifier, findValue2, false);
							}
						}
						if (arrayList != null)
						{
							foreach (object obj5 in arrayList)
							{
								X509IssuerSerial x509IssuerSerial2 = (X509IssuerSerial)obj5;
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindByIssuerDistinguishedName, x509IssuerSerial2.IssuerName, false);
								x509Certificate2Collection2 = x509Certificate2Collection2.Find(X509FindType.FindBySerialNumber, x509IssuerSerial2.SerialNumber, false);
							}
						}
					}
					catch (CryptographicException)
					{
					}
					catch (PlatformNotSupportedException)
					{
					}
					if (x509Certificate2Collection2 != null)
					{
						x509Certificate2Collection.AddRange(x509Certificate2Collection2);
					}
				}
			}
			return x509Certificate2Collection;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000EB30 File Offset: 0x0000CD30
		internal static string EncodeHexString(byte[] sArray)
		{
			return Utils.EncodeHexString(sArray, 0U, (uint)sArray.Length);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		internal static string EncodeHexString(byte[] sArray, uint start, uint end)
		{
			string result = null;
			if (sArray != null)
			{
				char[] array = new char[(end - start) * 2U];
				uint num = start;
				uint num2 = 0U;
				while (num < end)
				{
					uint num3 = (uint)((sArray[(int)num] & 240) >> 4);
					array[(int)num2++] = Utils.s_hexValues[(int)num3];
					num3 = (uint)(sArray[(int)num] & 15);
					array[(int)num2++] = Utils.s_hexValues[(int)num3];
					num += 1U;
				}
				result = new string(array);
			}
			return result;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		internal static byte[] DecodeHexString(string s)
		{
			string text = Utils.DiscardWhiteSpaces(s);
			uint num = (uint)(text.Length / 2);
			byte[] array = new byte[num];
			int num2 = 0;
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num))
			{
				array[num3] = (byte)((int)Utils.HexToByte(text[num2]) << 4 | (int)Utils.HexToByte(text[num2 + 1]));
				num2 += 2;
				num3++;
			}
			return array;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000EC03 File Offset: 0x0000CE03
		internal static byte HexToByte(char val)
		{
			if (val <= '9' && val >= '0')
			{
				return (byte)(val - '0');
			}
			if (val >= 'a' && val <= 'f')
			{
				return (byte)(val - 'a' + '\n');
			}
			if (val >= 'A' && val <= 'F')
			{
				return (byte)(val - 'A' + '\n');
			}
			return byte.MaxValue;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000EC40 File Offset: 0x0000CE40
		internal static bool IsSelfSigned(X509Chain chain)
		{
			X509ChainElementCollection chainElements = chain.ChainElements;
			if (chainElements.Count != 1)
			{
				return false;
			}
			X509Certificate2 certificate = chainElements[0].Certificate;
			return string.Compare(certificate.SubjectName.Name, certificate.IssuerName.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000EC8D File Offset: 0x0000CE8D
		internal static AsymmetricAlgorithm GetAnyPublicKey(X509Certificate2 certificate)
		{
			return certificate.GetRSAPublicKey();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000EC95 File Offset: 0x0000CE95
		// Note: this type is marked as 'beforefieldinit'.
		static Utils()
		{
		}

		// Token: 0x0400022E RID: 558
		internal const int MaxCharactersInDocument = 0;

		// Token: 0x0400022F RID: 559
		internal const long MaxCharactersFromEntities = 10000000L;

		// Token: 0x04000230 RID: 560
		internal const int XmlDsigSearchDepth = 20;

		// Token: 0x04000231 RID: 561
		private static readonly char[] s_hexValues = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		// Token: 0x04000232 RID: 562
		internal const int MaxTransformsPerReference = 10;

		// Token: 0x04000233 RID: 563
		internal const int MaxReferencesPerSignedInfo = 100;
	}
}
