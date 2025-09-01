using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Diagnostics.Application;
using System.Text;

namespace System.Xml
{
	// Token: 0x020000A3 RID: 163
	internal static class XmlExceptionHelper
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x000239A6 File Offset: 0x00021BA6
		private static void ThrowXmlException(XmlDictionaryReader reader, string res)
		{
			XmlExceptionHelper.ThrowXmlException(reader, res, null);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000239B0 File Offset: 0x00021BB0
		private static void ThrowXmlException(XmlDictionaryReader reader, string res, string arg1)
		{
			XmlExceptionHelper.ThrowXmlException(reader, res, arg1, null);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000239BB File Offset: 0x00021BBB
		private static void ThrowXmlException(XmlDictionaryReader reader, string res, string arg1, string arg2)
		{
			XmlExceptionHelper.ThrowXmlException(reader, res, arg1, arg2, null);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000239C8 File Offset: 0x00021BC8
		private static void ThrowXmlException(XmlDictionaryReader reader, string res, string arg1, string arg2, string arg3)
		{
			string text = System.Runtime.Serialization.SR.GetString(res, new object[]
			{
				arg1,
				arg2,
				arg3
			});
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				text = text + " " + System.Runtime.Serialization.SR.GetString("Line {0}, position {1}.", new object[]
				{
					xmlLineInfo.LineNumber,
					xmlLineInfo.LinePosition
				});
			}
			if (TD.ReaderQuotaExceededIsEnabled())
			{
				TD.ReaderQuotaExceeded(text);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(text));
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00023A54 File Offset: 0x00021C54
		public static void ThrowXmlException(XmlDictionaryReader reader, XmlException exception)
		{
			string text = exception.Message;
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				text = text + " " + System.Runtime.Serialization.SR.GetString("Line {0}, position {1}.", new object[]
				{
					xmlLineInfo.LineNumber,
					xmlLineInfo.LinePosition
				});
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(text));
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00023ABD File Offset: 0x00021CBD
		private static string GetName(string prefix, string localName)
		{
			if (prefix.Length == 0)
			{
				return localName;
			}
			return prefix + ":" + localName;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00023AD8 File Offset: 0x00021CD8
		private static string GetWhatWasFound(XmlDictionaryReader reader)
		{
			if (reader.EOF)
			{
				return System.Runtime.Serialization.SR.GetString("end of file");
			}
			XmlNodeType nodeType = reader.NodeType;
			if (nodeType <= XmlNodeType.Comment)
			{
				switch (nodeType)
				{
				case XmlNodeType.Element:
					return System.Runtime.Serialization.SR.GetString("element '{0}' from namespace '{1}'", new object[]
					{
						XmlExceptionHelper.GetName(reader.Prefix, reader.LocalName),
						reader.NamespaceURI
					});
				case XmlNodeType.Attribute:
					goto IL_FD;
				case XmlNodeType.Text:
					break;
				case XmlNodeType.CDATA:
					return System.Runtime.Serialization.SR.GetString("cdata '{0}'", new object[]
					{
						reader.Value
					});
				default:
					if (nodeType != XmlNodeType.Comment)
					{
						goto IL_FD;
					}
					return System.Runtime.Serialization.SR.GetString("comment '{0}'", new object[]
					{
						reader.Value
					});
				}
			}
			else if (nodeType - XmlNodeType.Whitespace > 1)
			{
				if (nodeType != XmlNodeType.EndElement)
				{
					goto IL_FD;
				}
				return System.Runtime.Serialization.SR.GetString("end element '{0}' from namespace '{1}'", new object[]
				{
					XmlExceptionHelper.GetName(reader.Prefix, reader.LocalName),
					reader.NamespaceURI
				});
			}
			return System.Runtime.Serialization.SR.GetString("text '{0}'", new object[]
			{
				reader.Value
			});
			IL_FD:
			return System.Runtime.Serialization.SR.GetString("node {0}", new object[]
			{
				reader.NodeType
			});
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00023C00 File Offset: 0x00021E00
		public static void ThrowStartElementExpected(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Start element expected. Found {0}.", XmlExceptionHelper.GetWhatWasFound(reader));
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00023C13 File Offset: 0x00021E13
		public static void ThrowStartElementExpected(XmlDictionaryReader reader, string name)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Start element '{0}' expected. Found {1}.", name, XmlExceptionHelper.GetWhatWasFound(reader));
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00023C27 File Offset: 0x00021E27
		public static void ThrowStartElementExpected(XmlDictionaryReader reader, string localName, string ns)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Start element '{0}' from namespace '{1}' expected. Found {2}.", localName, ns, XmlExceptionHelper.GetWhatWasFound(reader));
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00023C3C File Offset: 0x00021E3C
		public static void ThrowStartElementExpected(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			XmlExceptionHelper.ThrowStartElementExpected(reader, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(ns));
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00023C50 File Offset: 0x00021E50
		public static void ThrowFullStartElementExpected(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Non-empty start element expected. Found {0}.", XmlExceptionHelper.GetWhatWasFound(reader));
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00023C63 File Offset: 0x00021E63
		public static void ThrowFullStartElementExpected(XmlDictionaryReader reader, string name)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Non-empty start element '{0}' expected. Found {1}.", name, XmlExceptionHelper.GetWhatWasFound(reader));
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00023C77 File Offset: 0x00021E77
		public static void ThrowFullStartElementExpected(XmlDictionaryReader reader, string localName, string ns)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Non-empty start element '{0}' from namespace '{1}' expected. Found {2}.", localName, ns, XmlExceptionHelper.GetWhatWasFound(reader));
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00023C8C File Offset: 0x00021E8C
		public static void ThrowFullStartElementExpected(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			XmlExceptionHelper.ThrowFullStartElementExpected(reader, XmlDictionaryString.GetString(localName), XmlDictionaryString.GetString(ns));
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00023CA0 File Offset: 0x00021EA0
		public static void ThrowEndElementExpected(XmlDictionaryReader reader, string localName, string ns)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "End element '{0}' from namespace '{1}' expected. Found {2}.", localName, ns, XmlExceptionHelper.GetWhatWasFound(reader));
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00023CB5 File Offset: 0x00021EB5
		public static void ThrowMaxStringContentLengthExceeded(XmlDictionaryReader reader, int maxStringContentLength)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "XML max string content length exceeded. It must be less than {0}.", maxStringContentLength.ToString(NumberFormatInfo.CurrentInfo));
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00023CCE File Offset: 0x00021ECE
		public static void ThrowMaxArrayLengthExceeded(XmlDictionaryReader reader, int maxArrayLength)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The maximum array length quota ({0}) has been exceeded while reading XML data. This quota may be increased by changing the MaxArrayLength property on the XmlDictionaryReaderQuotas object used when creating the XML reader.", maxArrayLength.ToString(NumberFormatInfo.CurrentInfo));
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00023CE7 File Offset: 0x00021EE7
		public static void ThrowMaxArrayLengthOrMaxItemsQuotaExceeded(XmlDictionaryReader reader, int maxQuota)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "XML max array length or max items quota exceeded. It must be less than {0}.", maxQuota.ToString(NumberFormatInfo.CurrentInfo));
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00023D00 File Offset: 0x00021F00
		public static void ThrowMaxDepthExceeded(XmlDictionaryReader reader, int maxDepth)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "XML max depth exceeded. It must be less than {0}.", maxDepth.ToString(NumberFormatInfo.CurrentInfo));
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00023D19 File Offset: 0x00021F19
		public static void ThrowMaxBytesPerReadExceeded(XmlDictionaryReader reader, int maxBytesPerRead)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "XML max bytes per read exceeded. It must be less than {0}.", maxBytesPerRead.ToString(NumberFormatInfo.CurrentInfo));
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00023D32 File Offset: 0x00021F32
		public static void ThrowMaxNameTableCharCountExceeded(XmlDictionaryReader reader, int maxNameTableCharCount)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The maximum nametable character count quota ({0}) has been exceeded while reading XML data. The nametable is a data structure used to store strings encountered during XML processing - long XML documents with non-repeating element names, attribute names and attribute values may trigger this quota. This quota may be increased by changing the MaxNameTableCharCount property on the XmlDictionaryReaderQuotas object used when creating the XML reader.", maxNameTableCharCount.ToString(NumberFormatInfo.CurrentInfo));
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00023D4B File Offset: 0x00021F4B
		public static void ThrowBase64DataExpected(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Base64 encoded data expected. Found {0}.", XmlExceptionHelper.GetWhatWasFound(reader));
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00023D5E File Offset: 0x00021F5E
		public static void ThrowUndefinedPrefix(XmlDictionaryReader reader, string prefix)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The prefix '{0}' is not defined.", prefix);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00023D6C File Offset: 0x00021F6C
		public static void ThrowProcessingInstructionNotSupported(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Processing instructions (other than the XML declaration) and DTDs are not supported.");
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00023D79 File Offset: 0x00021F79
		public static void ThrowInvalidXml(XmlDictionaryReader reader, byte b)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The byte 0x{0} is not valid at this location.", b.ToString("X2", CultureInfo.InvariantCulture));
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00023D97 File Offset: 0x00021F97
		public static void ThrowUnexpectedEndOfFile(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Unexpected end of file. Following elements are not closed: {0}.", ((XmlBaseReader)reader).GetOpenElements());
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00023DAF File Offset: 0x00021FAF
		public static void ThrowUnexpectedEndElement(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "No matching start tag for end element.");
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00023DBC File Offset: 0x00021FBC
		public static void ThrowTokenExpected(XmlDictionaryReader reader, string expected, char found)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The token '{0}' was expected but found '{1}'.", expected, found.ToString());
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00023DD1 File Offset: 0x00021FD1
		public static void ThrowTokenExpected(XmlDictionaryReader reader, string expected, string found)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The token '{0}' was expected but found '{1}'.", expected, found);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00023DE0 File Offset: 0x00021FE0
		public static void ThrowInvalidCharRef(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Character reference not valid.");
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00023DED File Offset: 0x00021FED
		public static void ThrowTagMismatch(XmlDictionaryReader reader, string expectedPrefix, string expectedLocalName, string foundPrefix, string foundLocalName)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Start element '{0}' does not match end element '{1}'.", XmlExceptionHelper.GetName(expectedPrefix, expectedLocalName), XmlExceptionHelper.GetName(foundPrefix, foundLocalName));
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00023E0C File Offset: 0x0002200C
		public static void ThrowDuplicateXmlnsAttribute(XmlDictionaryReader reader, string localName, string ns)
		{
			string text;
			if (localName.Length == 0)
			{
				text = "xmlns";
			}
			else
			{
				text = "xmlns:" + localName;
			}
			XmlExceptionHelper.ThrowXmlException(reader, "Duplicate attribute found. Both '{0}' and '{1}' are from the namespace '{2}'.", text, text, ns);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00023E43 File Offset: 0x00022043
		public static void ThrowDuplicateAttribute(XmlDictionaryReader reader, string prefix1, string prefix2, string localName, string ns)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "Duplicate attribute found. Both '{0}' and '{1}' are from the namespace '{2}'.", XmlExceptionHelper.GetName(prefix1, localName), XmlExceptionHelper.GetName(prefix2, localName), ns);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00023E60 File Offset: 0x00022060
		public static void ThrowInvalidBinaryFormat(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The input source is not correctly formatted.");
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00023E6D File Offset: 0x0002206D
		public static void ThrowInvalidRootData(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The data at the root level is invalid.");
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00023E7A File Offset: 0x0002207A
		public static void ThrowMultipleRootElements(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "There are multiple root elements.");
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00023E87 File Offset: 0x00022087
		public static void ThrowDeclarationNotFirst(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "No characters can appear before the XML declaration.");
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00023E94 File Offset: 0x00022094
		public static void ThrowConversionOverflow(XmlDictionaryReader reader, string value, string type)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The value '{0}' cannot be represented with the type '{1}'.", value, type);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00023EA4 File Offset: 0x000220A4
		public static void ThrowXmlDictionaryStringIDOutOfRange(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "XmlDictionaryString IDs must be in the range from {0} to {1}.", 0.ToString(NumberFormatInfo.CurrentInfo), 536870911.ToString(NumberFormatInfo.CurrentInfo));
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00023EDC File Offset: 0x000220DC
		public static void ThrowXmlDictionaryStringIDUndefinedStatic(XmlDictionaryReader reader, int key)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "XmlDictionaryString ID {0} not defined in the static dictionary.", key.ToString(NumberFormatInfo.CurrentInfo));
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00023EF5 File Offset: 0x000220F5
		public static void ThrowXmlDictionaryStringIDUndefinedSession(XmlDictionaryReader reader, int key)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "XmlDictionaryString ID {0} not defined in the XmlBinaryReaderSession.", key.ToString(NumberFormatInfo.CurrentInfo));
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00023F0E File Offset: 0x0002210E
		public static void ThrowEmptyNamespace(XmlDictionaryReader reader)
		{
			XmlExceptionHelper.ThrowXmlException(reader, "The empty namespace requires a null or empty prefix.");
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00023F1B File Offset: 0x0002211B
		public static XmlException CreateConversionException(string value, string type, Exception exception)
		{
			return new XmlException(System.Runtime.Serialization.SR.GetString("The value '{0}' cannot be parsed as the type '{1}'.", new object[]
			{
				value,
				type
			}), exception);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00023F3B File Offset: 0x0002213B
		public static XmlException CreateEncodingException(byte[] buffer, int offset, int count, Exception exception)
		{
			return XmlExceptionHelper.CreateEncodingException(new UTF8Encoding(false, false).GetString(buffer, offset, count), exception);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00023F52 File Offset: 0x00022152
		public static XmlException CreateEncodingException(string value, Exception exception)
		{
			return new XmlException(System.Runtime.Serialization.SR.GetString("'{0}' contains invalid UTF8 bytes.", new object[]
			{
				value
			}), exception);
		}
	}
}
