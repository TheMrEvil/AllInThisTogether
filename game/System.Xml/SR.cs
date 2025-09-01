using System;
using System.Globalization;

// Token: 0x02000004 RID: 4
internal static class SR
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	internal static string GetString(string name, params object[] args)
	{
		return SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205E File Offset: 0x0000025E
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000206B File Offset: 0x0000026B
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000206E File Offset: 0x0000026E
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002081 File Offset: 0x00000281
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000208F File Offset: 0x0000028F
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000209E File Offset: 0x0000029E
	internal static string Format(CultureInfo ci, string resourceFormat, object p1, object p2)
	{
		return string.Format(ci, resourceFormat, p1, p2);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000020A9 File Offset: 0x000002A9
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002068 File Offset: 0x00000268
	internal static string GetResourceString(string str)
	{
		return str;
	}

	// Token: 0x04000038 RID: 56
	public const string Xml_UserException = "{0}";

	// Token: 0x04000039 RID: 57
	public const string Xml_DefaultException = "An XML error has occurred.";

	// Token: 0x0400003A RID: 58
	public const string Xml_InvalidOperation = "Operation is not valid due to the current state of the object.";

	// Token: 0x0400003B RID: 59
	public const string Xml_ErrorFilePosition = "An error occurred at {0}, ({1}, {2}).";

	// Token: 0x0400003C RID: 60
	public const string Xml_StackOverflow = "Stack overflow.";

	// Token: 0x0400003D RID: 61
	public const string Xslt_NoStylesheetLoaded = "No stylesheet was loaded.";

	// Token: 0x0400003E RID: 62
	public const string Xslt_NotCompiledStylesheet = "Type '{0}' is not a compiled stylesheet class.";

	// Token: 0x0400003F RID: 63
	public const string Xslt_IncompatibleCompiledStylesheetVersion = "Executing a stylesheet that was compiled using a later version of the framework is not supported. Stylesheet Version: {0}. Current Framework Version: {1}.";

	// Token: 0x04000040 RID: 64
	public const string Xml_AsyncIsRunningException = "An asynchronous operation is already in progress.";

	// Token: 0x04000041 RID: 65
	public const string Xml_ReaderAsyncNotSetException = "Set XmlReaderSettings.Async to true if you want to use Async Methods.";

	// Token: 0x04000042 RID: 66
	public const string Xml_UnclosedQuote = "There is an unclosed literal string.";

	// Token: 0x04000043 RID: 67
	public const string Xml_UnexpectedEOF = "Unexpected end of file while parsing {0} has occurred.";

	// Token: 0x04000044 RID: 68
	public const string Xml_UnexpectedEOF1 = "Unexpected end of file has occurred.";

	// Token: 0x04000045 RID: 69
	public const string Xml_UnexpectedEOFInElementContent = "Unexpected end of file has occurred. The following elements are not closed: {0}";

	// Token: 0x04000046 RID: 70
	public const string Xml_BadStartNameChar = "Name cannot begin with the '{0}' character, hexadecimal value {1}.";

	// Token: 0x04000047 RID: 71
	public const string Xml_BadNameChar = "The '{0}' character, hexadecimal value {1}, cannot be included in a name.";

	// Token: 0x04000048 RID: 72
	public const string Xml_BadDecimalEntity = "Invalid syntax for a decimal numeric entity reference.";

	// Token: 0x04000049 RID: 73
	public const string Xml_BadHexEntity = "Invalid syntax for a hexadecimal numeric entity reference.";

	// Token: 0x0400004A RID: 74
	public const string Xml_MissingByteOrderMark = "There is no Unicode byte order mark. Cannot switch to Unicode.";

	// Token: 0x0400004B RID: 75
	public const string Xml_UnknownEncoding = "System does not support '{0}' encoding.";

	// Token: 0x0400004C RID: 76
	public const string Xml_InternalError = "An internal error has occurred.";

	// Token: 0x0400004D RID: 77
	public const string Xml_InvalidCharInThisEncoding = "Invalid character in the given encoding.";

	// Token: 0x0400004E RID: 78
	public const string Xml_ErrorPosition = "Line {0}, position {1}.";

	// Token: 0x0400004F RID: 79
	public const string Xml_MessageWithErrorPosition = "{0} Line {1}, position {2}.";

	// Token: 0x04000050 RID: 80
	public const string Xml_UnexpectedTokenEx = "'{0}' is an unexpected token. The expected token is '{1}'.";

	// Token: 0x04000051 RID: 81
	public const string Xml_UnexpectedTokens2 = "'{0}' is an unexpected token. The expected token is '{1}' or '{2}'.";

	// Token: 0x04000052 RID: 82
	public const string Xml_ExpectingWhiteSpace = "'{0}' is an unexpected token. Expecting whitespace.";

	// Token: 0x04000053 RID: 83
	public const string Xml_TagMismatchEx = "The '{0}' start tag on line {1} position {2} does not match the end tag of '{3}'.";

	// Token: 0x04000054 RID: 84
	public const string Xml_UnexpectedEndTag = "Unexpected end tag.";

	// Token: 0x04000055 RID: 85
	public const string Xml_UnknownNs = "'{0}' is an undeclared prefix.";

	// Token: 0x04000056 RID: 86
	public const string Xml_BadAttributeChar = "'{0}', hexadecimal value {1}, is an invalid attribute character.";

	// Token: 0x04000057 RID: 87
	public const string Xml_ExpectExternalOrClose = "Expecting external ID, '[' or '>'.";

	// Token: 0x04000058 RID: 88
	public const string Xml_MissingRoot = "Root element is missing.";

	// Token: 0x04000059 RID: 89
	public const string Xml_MultipleRoots = "There are multiple root elements.";

	// Token: 0x0400005A RID: 90
	public const string Xml_InvalidRootData = "Data at the root level is invalid.";

	// Token: 0x0400005B RID: 91
	public const string Xml_XmlDeclNotFirst = "Unexpected XML declaration. The XML declaration must be the first node in the document, and no whitespace characters are allowed to appear before it.";

	// Token: 0x0400005C RID: 92
	public const string Xml_InvalidXmlDecl = "Syntax for an XML declaration is invalid.";

	// Token: 0x0400005D RID: 93
	public const string Xml_InvalidNodeType = "'{0}' is an invalid XmlNodeType.";

	// Token: 0x0400005E RID: 94
	public const string Xml_InvalidPIName = "'{0}' is an invalid name for processing instructions.";

	// Token: 0x0400005F RID: 95
	public const string Xml_InvalidXmlSpace = "'{0}' is an invalid xml:space value.";

	// Token: 0x04000060 RID: 96
	public const string Xml_InvalidVersionNumber = "Version number '{0}' is invalid.";

	// Token: 0x04000061 RID: 97
	public const string Xml_DupAttributeName = "'{0}' is a duplicate attribute name.";

	// Token: 0x04000062 RID: 98
	public const string Xml_BadDTDLocation = "Unexpected DTD declaration.";

	// Token: 0x04000063 RID: 99
	public const string Xml_ElementNotFound = "Element '{0}' was not found.";

	// Token: 0x04000064 RID: 100
	public const string Xml_ElementNotFoundNs = "Element '{0}' with namespace name '{1}' was not found.";

	// Token: 0x04000065 RID: 101
	public const string Xml_PartialContentNodeTypeNotSupportedEx = "XmlNodeType {0} is not supported for partial content parsing.";

	// Token: 0x04000066 RID: 102
	public const string Xml_MultipleDTDsProvided = "Cannot have multiple DTDs.";

	// Token: 0x04000067 RID: 103
	public const string Xml_CanNotBindToReservedNamespace = "Cannot bind to the reserved namespace.";

	// Token: 0x04000068 RID: 104
	public const string Xml_InvalidCharacter = "'{0}', hexadecimal value {1}, is an invalid character.";

	// Token: 0x04000069 RID: 105
	public const string Xml_InvalidBinHexValue = "'{0}' is not a valid BinHex text sequence.";

	// Token: 0x0400006A RID: 106
	public const string Xml_InvalidBinHexValueOddCount = "'{0}' is not a valid BinHex text sequence. The sequence must contain an even number of characters.";

	// Token: 0x0400006B RID: 107
	public const string Xml_InvalidTextDecl = "Invalid text declaration.";

	// Token: 0x0400006C RID: 108
	public const string Xml_InvalidBase64Value = "'{0}' is not a valid Base64 text sequence.";

	// Token: 0x0400006D RID: 109
	public const string Xml_UndeclaredEntity = "Reference to undeclared entity '{0}'.";

	// Token: 0x0400006E RID: 110
	public const string Xml_RecursiveParEntity = "Parameter entity '{0}' references itself.";

	// Token: 0x0400006F RID: 111
	public const string Xml_RecursiveGenEntity = "General entity '{0}' references itself.";

	// Token: 0x04000070 RID: 112
	public const string Xml_ExternalEntityInAttValue = "External entity '{0}' reference cannot appear in the attribute value.";

	// Token: 0x04000071 RID: 113
	public const string Xml_UnparsedEntityRef = "Reference to unparsed entity '{0}'.";

	// Token: 0x04000072 RID: 114
	public const string Xml_NotSameNametable = "Not the same name table.";

	// Token: 0x04000073 RID: 115
	public const string Xml_NametableMismatch = "XmlReaderSettings.XmlNameTable must be the same name table as in XmlParserContext.NameTable or XmlParserContext.NamespaceManager.NameTable, or it must be null.";

	// Token: 0x04000074 RID: 116
	public const string Xml_BadNamespaceDecl = "Invalid namespace declaration.";

	// Token: 0x04000075 RID: 117
	public const string Xml_ErrorParsingEntityName = "An error occurred while parsing EntityName.";

	// Token: 0x04000076 RID: 118
	public const string Xml_InvalidNmToken = "Invalid NmToken value '{0}'.";

	// Token: 0x04000077 RID: 119
	public const string Xml_EntityRefNesting = "Entity replacement text must nest properly within markup declarations.";

	// Token: 0x04000078 RID: 120
	public const string Xml_CannotResolveEntity = "Cannot resolve entity reference '{0}'.";

	// Token: 0x04000079 RID: 121
	public const string Xml_CannotResolveEntityDtdIgnored = "Cannot resolve entity reference '{0}' because the DTD has been ignored. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.";

	// Token: 0x0400007A RID: 122
	public const string Xml_CannotResolveExternalSubset = "Cannot resolve external DTD subset - public ID = '{0}', system ID = '{1}'.";

	// Token: 0x0400007B RID: 123
	public const string Xml_CannotResolveUrl = "Cannot resolve '{0}'.";

	// Token: 0x0400007C RID: 124
	public const string Xml_CDATAEndInText = "']]>' is not allowed in character data.";

	// Token: 0x0400007D RID: 125
	public const string Xml_ExternalEntityInStandAloneDocument = "Standalone document declaration must have a value of 'no' because an external entity '{0}' is referenced.";

	// Token: 0x0400007E RID: 126
	public const string Xml_DtdAfterRootElement = "DTD must be defined before the document root element.";

	// Token: 0x0400007F RID: 127
	public const string Xml_ReadOnlyProperty = "The '{0}' property is read only and cannot be set.";

	// Token: 0x04000080 RID: 128
	public const string Xml_DtdIsProhibited = "DTD is prohibited in this XML document.";

	// Token: 0x04000081 RID: 129
	public const string Xml_DtdIsProhibitedEx = "For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.";

	// Token: 0x04000082 RID: 130
	public const string Xml_ReadSubtreeNotOnElement = "ReadSubtree() can be called only if the reader is on an element node.";

	// Token: 0x04000083 RID: 131
	public const string Xml_DtdNotAllowedInFragment = "DTD is not allowed in XML fragments.";

	// Token: 0x04000084 RID: 132
	public const string Xml_CannotStartDocumentOnFragment = "WriteStartDocument cannot be called on writers created with ConformanceLevel.Fragment.";

	// Token: 0x04000085 RID: 133
	public const string Xml_ErrorOpeningExternalDtd = "An error has occurred while opening external DTD '{0}': {1}";

	// Token: 0x04000086 RID: 134
	public const string Xml_ErrorOpeningExternalEntity = "An error has occurred while opening external entity '{0}': {1}";

	// Token: 0x04000087 RID: 135
	public const string Xml_ReadBinaryContentNotSupported = "{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.";

	// Token: 0x04000088 RID: 136
	public const string Xml_ReadValueChunkNotSupported = "ReadValueChunk method is not supported on this XmlReader. Use CanReadValueChunk property to find out if an XmlReader implements it.";

	// Token: 0x04000089 RID: 137
	public const string Xml_InvalidReadContentAs = "The {0} method is not supported on node type {1}. If you want to read typed content of an element, use the ReadElementContentAs method.";

	// Token: 0x0400008A RID: 138
	public const string Xml_InvalidReadElementContentAs = "The {0} method is not supported on node type {1}.";

	// Token: 0x0400008B RID: 139
	public const string Xml_MixedReadElementContentAs = "ReadElementContentAs() methods cannot be called on an element that has child elements.";

	// Token: 0x0400008C RID: 140
	public const string Xml_MixingReadValueChunkWithBinary = "ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex.";

	// Token: 0x0400008D RID: 141
	public const string Xml_MixingBinaryContentMethods = "ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex.";

	// Token: 0x0400008E RID: 142
	public const string Xml_MixingV1StreamingWithV2Binary = "ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadChars, ReadBase64, and ReadBinHex.";

	// Token: 0x0400008F RID: 143
	public const string Xml_InvalidReadValueChunk = "The ReadValueAsChunk method is not supported on node type {0}.";

	// Token: 0x04000090 RID: 144
	public const string Xml_ReadContentAsFormatException = "Content cannot be converted to the type {0}.";

	// Token: 0x04000091 RID: 145
	public const string Xml_DoubleBaseUri = "BaseUri must be specified either as an argument of XmlReader.Create or on the XmlParserContext. If it is specified on both, it must be the same base URI.";

	// Token: 0x04000092 RID: 146
	public const string Xml_NotEnoughSpaceForSurrogatePair = "The buffer is not large enough to fit a surrogate pair. Please provide a buffer of size at least 2 characters.";

	// Token: 0x04000093 RID: 147
	public const string Xml_EmptyUrl = "The URL cannot be empty.";

	// Token: 0x04000094 RID: 148
	public const string Xml_UnexpectedNodeInSimpleContent = "Unexpected node type {0}. {1} method can only be called on elements with simple or empty content.";

	// Token: 0x04000095 RID: 149
	public const string Xml_InvalidWhitespaceCharacter = "The Whitespace or SignificantWhitespace node can contain only XML whitespace characters. '{0}' is not an XML white space character.";

	// Token: 0x04000096 RID: 150
	public const string Xml_IncompatibleConformanceLevel = "Cannot change conformance checking to {0}. Make sure the ConformanceLevel in XmlReaderSettings is set to Auto for wrapping scenarios.";

	// Token: 0x04000097 RID: 151
	public const string Xml_LimitExceeded = "The input document has exceeded a limit set by {0}.";

	// Token: 0x04000098 RID: 152
	public const string Xml_ClosedOrErrorReader = "The XmlReader is closed or in error state.";

	// Token: 0x04000099 RID: 153
	public const string Xml_CharEntityOverflow = "Invalid value of a character entity reference.";

	// Token: 0x0400009A RID: 154
	public const string Xml_BadNameCharWithPos = "The '{0}' character, hexadecimal value {1}, at position {2} within the name, cannot be included in a name.";

	// Token: 0x0400009B RID: 155
	public const string Xml_XmlnsBelongsToReservedNs = "The 'xmlns' attribute is bound to the reserved namespace 'http://www.w3.org/2000/xmlns/'.";

	// Token: 0x0400009C RID: 156
	public const string Xml_UndeclaredParEntity = "Reference to undeclared parameter entity '{0}'.";

	// Token: 0x0400009D RID: 157
	public const string Xml_InvalidXmlDocument = "Invalid XML document. {0}";

	// Token: 0x0400009E RID: 158
	public const string Xml_NoDTDPresent = "No DTD found.";

	// Token: 0x0400009F RID: 159
	public const string Xml_MultipleValidaitonTypes = "Unsupported combination of validation types.";

	// Token: 0x040000A0 RID: 160
	public const string Xml_NoValidation = "No validation occurred.";

	// Token: 0x040000A1 RID: 161
	public const string Xml_WhitespaceHandling = "Expected WhitespaceHandling.None, or WhitespaceHandling.All, or WhitespaceHandling.Significant.";

	// Token: 0x040000A2 RID: 162
	public const string Xml_InvalidResetStateCall = "Cannot call ResetState when parsing an XML fragment.";

	// Token: 0x040000A3 RID: 163
	public const string Xml_EntityHandling = "Expected EntityHandling.ExpandEntities or EntityHandling.ExpandCharEntities.";

	// Token: 0x040000A4 RID: 164
	public const string Xml_AttlistDuplEnumValue = "'{0}' is a duplicate enumeration value.";

	// Token: 0x040000A5 RID: 165
	public const string Xml_AttlistDuplNotationValue = "'{0}' is a duplicate notation value.";

	// Token: 0x040000A6 RID: 166
	public const string Xml_EncodingSwitchAfterResetState = "'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.";

	// Token: 0x040000A7 RID: 167
	public const string Xml_UnexpectedNodeType = "Unexpected XmlNodeType: '{0}'.";

	// Token: 0x040000A8 RID: 168
	public const string Xml_InvalidConditionalSection = "A conditional section is not allowed in an internal subset.";

	// Token: 0x040000A9 RID: 169
	public const string Xml_UnexpectedCDataEnd = "']]>' is not expected.";

	// Token: 0x040000AA RID: 170
	public const string Xml_UnclosedConditionalSection = "There is an unclosed conditional section.";

	// Token: 0x040000AB RID: 171
	public const string Xml_ExpectDtdMarkup = "Expected DTD markup was not found.";

	// Token: 0x040000AC RID: 172
	public const string Xml_IncompleteDtdContent = "Incomplete DTD content.";

	// Token: 0x040000AD RID: 173
	public const string Xml_EnumerationRequired = "Enumeration data type required.";

	// Token: 0x040000AE RID: 174
	public const string Xml_InvalidContentModel = "Invalid content model.";

	// Token: 0x040000AF RID: 175
	public const string Xml_FragmentId = "Fragment identifier '{0}' cannot be part of the system identifier '{1}'.";

	// Token: 0x040000B0 RID: 176
	public const string Xml_ExpectPcData = "Expecting 'PCDATA'.";

	// Token: 0x040000B1 RID: 177
	public const string Xml_ExpectNoWhitespace = "Whitespace not allowed before '?', '*', or '+'.";

	// Token: 0x040000B2 RID: 178
	public const string Xml_ExpectOp = "Expecting '?', '*', or '+'.";

	// Token: 0x040000B3 RID: 179
	public const string Xml_InvalidAttributeType = "'{0}' is an invalid attribute type.";

	// Token: 0x040000B4 RID: 180
	public const string Xml_InvalidAttributeType1 = "Invalid attribute type.";

	// Token: 0x040000B5 RID: 181
	public const string Xml_ExpectAttType = "Expecting an attribute type.";

	// Token: 0x040000B6 RID: 182
	public const string Xml_ColonInLocalName = "'{0}' is an unqualified name and cannot contain the character ':'.";

	// Token: 0x040000B7 RID: 183
	public const string Xml_InvalidParEntityRef = "A parameter entity reference is not allowed in internal markup.";

	// Token: 0x040000B8 RID: 184
	public const string Xml_ExpectSubOrClose = "Expecting an internal subset or the end of the DOCTYPE declaration.";

	// Token: 0x040000B9 RID: 185
	public const string Xml_ExpectExternalOrPublicId = "Expecting a system identifier or a public identifier.";

	// Token: 0x040000BA RID: 186
	public const string Xml_ExpectExternalIdOrEntityValue = "Expecting an external identifier or an entity value.";

	// Token: 0x040000BB RID: 187
	public const string Xml_ExpectIgnoreOrInclude = "Conditional sections must specify the keyword 'IGNORE' or 'INCLUDE'.";

	// Token: 0x040000BC RID: 188
	public const string Xml_UnsupportedClass = "Object type is not supported.";

	// Token: 0x040000BD RID: 189
	public const string Xml_NullResolver = "Resolving of external URIs was prohibited.";

	// Token: 0x040000BE RID: 190
	public const string Xml_RelativeUriNotSupported = "Relative URIs are not supported.";

	// Token: 0x040000BF RID: 191
	public const string Xml_WriterAsyncNotSetException = "Set XmlWriterSettings.Async to true if you want to use Async Methods.";

	// Token: 0x040000C0 RID: 192
	public const string Xml_PrefixForEmptyNs = "Cannot use a prefix with an empty namespace.";

	// Token: 0x040000C1 RID: 193
	public const string Xml_InvalidCommentChars = "An XML comment cannot contain '--', and '-' cannot be the last character.";

	// Token: 0x040000C2 RID: 194
	public const string Xml_UndefNamespace = "The '{0}' namespace is not defined.";

	// Token: 0x040000C3 RID: 195
	public const string Xml_EmptyName = "The empty string '' is not a valid name.";

	// Token: 0x040000C4 RID: 196
	public const string Xml_EmptyLocalName = "The empty string '' is not a valid local name.";

	// Token: 0x040000C5 RID: 197
	public const string Xml_InvalidNameCharsDetail = "Invalid name character in '{0}'. The '{1}' character, hexadecimal value {2}, cannot be included in a name.";

	// Token: 0x040000C6 RID: 198
	public const string Xml_NoStartTag = "There was no XML start tag open.";

	// Token: 0x040000C7 RID: 199
	public const string Xml_ClosedOrError = "The Writer is closed or in error state.";

	// Token: 0x040000C8 RID: 200
	public const string Xml_WrongToken = "Token {0} in state {1} would result in an invalid XML document.";

	// Token: 0x040000C9 RID: 201
	public const string Xml_XmlPrefix = "Prefix \"xml\" is reserved for use by XML and can be mapped only to namespace name \"http://www.w3.org/XML/1998/namespace\".";

	// Token: 0x040000CA RID: 202
	public const string Xml_XmlnsPrefix = "Prefix \"xmlns\" is reserved for use by XML.";

	// Token: 0x040000CB RID: 203
	public const string Xml_NamespaceDeclXmlXmlns = "Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".";

	// Token: 0x040000CC RID: 204
	public const string Xml_NonWhitespace = "Only whitespace characters should be used.";

	// Token: 0x040000CD RID: 205
	public const string Xml_DupXmlDecl = "Cannot write XML declaration. WriteStartDocument method has already written it.";

	// Token: 0x040000CE RID: 206
	public const string Xml_CannotWriteXmlDecl = "Cannot write XML declaration. XML declaration can be only at the beginning of the document.";

	// Token: 0x040000CF RID: 207
	public const string Xml_NoRoot = "Document does not have a root element.";

	// Token: 0x040000D0 RID: 208
	public const string Xml_InvalidPosition = "The current position on the Reader is neither an element nor an attribute.";

	// Token: 0x040000D1 RID: 209
	public const string Xml_IncompleteEntity = "Incomplete entity contents.";

	// Token: 0x040000D2 RID: 210
	public const string Xml_InvalidSurrogateHighChar = "Invalid high surrogate character (0x{0}). A high surrogate character must have a value from range (0xD800 - 0xDBFF).";

	// Token: 0x040000D3 RID: 211
	public const string Xml_InvalidSurrogateMissingLowChar = "The surrogate pair is invalid. Missing a low surrogate character.";

	// Token: 0x040000D4 RID: 212
	public const string Xml_InvalidSurrogatePairWithArgs = "The surrogate pair (0x{0}, 0x{1}) is invalid. A high surrogate character (0xD800 - 0xDBFF) must always be paired with a low surrogate character (0xDC00 - 0xDFFF).";

	// Token: 0x040000D5 RID: 213
	public const string Xml_RedefinePrefix = "The prefix '{0}' cannot be redefined from '{1}' to '{2}' within the same start element tag.";

	// Token: 0x040000D6 RID: 214
	public const string Xml_DtdAlreadyWritten = "The DTD has already been written out.";

	// Token: 0x040000D7 RID: 215
	public const string Xml_InvalidCharsInIndent = "XmlWriterSettings.{0} can contain only valid XML text content characters when XmlWriterSettings.CheckCharacters is true. {1}";

	// Token: 0x040000D8 RID: 216
	public const string Xml_IndentCharsNotWhitespace = "XmlWriterSettings.{0} can contain only valid XML whitespace characters when XmlWriterSettings.CheckCharacters and XmlWriterSettings.NewLineOnAttributes are true.";

	// Token: 0x040000D9 RID: 217
	public const string Xml_ConformanceLevelFragment = "Make sure that the ConformanceLevel setting is set to ConformanceLevel.Fragment or ConformanceLevel.Auto if you want to write an XML fragment. ";

	// Token: 0x040000DA RID: 218
	public const string Xml_InvalidQuote = "Invalid XML attribute quote character. Valid attribute quote characters are ' and \".";

	// Token: 0x040000DB RID: 219
	public const string Xml_UndefPrefix = "An undefined prefix is in use.";

	// Token: 0x040000DC RID: 220
	public const string Xml_NoNamespaces = "Cannot set the namespace if Namespaces is 'false'.";

	// Token: 0x040000DD RID: 221
	public const string Xml_InvalidCDataChars = "Cannot have ']]>' inside an XML CDATA block.";

	// Token: 0x040000DE RID: 222
	public const string Xml_NotTheFirst = "WriteStartDocument needs to be the first call.";

	// Token: 0x040000DF RID: 223
	public const string Xml_InvalidPiChars = "Cannot have '?>' inside an XML processing instruction.";

	// Token: 0x040000E0 RID: 224
	public const string Xml_InvalidNameChars = "Invalid name character in '{0}'.";

	// Token: 0x040000E1 RID: 225
	public const string Xml_Closed = "The Writer is closed.";

	// Token: 0x040000E2 RID: 226
	public const string Xml_InvalidPrefix = "Prefixes beginning with \"xml\" (regardless of whether the characters are uppercase, lowercase, or some combination thereof) are reserved for use by XML.";

	// Token: 0x040000E3 RID: 227
	public const string Xml_InvalidIndentation = "Indentation value must be greater than 0.";

	// Token: 0x040000E4 RID: 228
	public const string Xml_NotInWriteState = "NotInWriteState.";

	// Token: 0x040000E5 RID: 229
	public const string Xml_SurrogatePairSplit = "The second character surrogate pair is not in the input buffer to be written.";

	// Token: 0x040000E6 RID: 230
	public const string Xml_NoMultipleRoots = "Document cannot have multiple document elements.";

	// Token: 0x040000E7 RID: 231
	public const string XmlBadName = "A node of type '{0}' cannot have the name '{1}'.";

	// Token: 0x040000E8 RID: 232
	public const string XmlNoNameAllowed = "A node of type '{0}' cannot have a name.";

	// Token: 0x040000E9 RID: 233
	public const string XmlConvert_BadUri = "The string was not recognized as a valid Uri.";

	// Token: 0x040000EA RID: 234
	public const string XmlConvert_BadFormat = "The string '{0}' is not a valid {1} value.";

	// Token: 0x040000EB RID: 235
	public const string XmlConvert_Overflow = "Value '{0}' was either too large or too small for {1}.";

	// Token: 0x040000EC RID: 236
	public const string XmlConvert_TypeBadMapping = "Xml type '{0}' does not support Clr type '{1}'.";

	// Token: 0x040000ED RID: 237
	public const string XmlConvert_TypeBadMapping2 = "Xml type '{0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.";

	// Token: 0x040000EE RID: 238
	public const string XmlConvert_TypeListBadMapping = "Xml type 'List of {0}' does not support Clr type '{1}'.";

	// Token: 0x040000EF RID: 239
	public const string XmlConvert_TypeListBadMapping2 = "Xml type 'List of {0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.";

	// Token: 0x040000F0 RID: 240
	public const string XmlConvert_TypeToString = "Xml type '{0}' cannot convert from Clr type '{1}' unless the destination type is String or XmlAtomicValue.";

	// Token: 0x040000F1 RID: 241
	public const string XmlConvert_TypeFromString = "Xml type '{0}' cannot convert to Clr type '{1}' unless the source value is a String or an XmlAtomicValue.";

	// Token: 0x040000F2 RID: 242
	public const string XmlConvert_TypeNoPrefix = "The QName '{0}' cannot be represented as a String.  A prefix for namespace '{1}' cannot be found.";

	// Token: 0x040000F3 RID: 243
	public const string XmlConvert_TypeNoNamespace = "The String '{0}' cannot be represented as an XmlQualifiedName.  A namespace for prefix '{1}' cannot be found.";

	// Token: 0x040000F4 RID: 244
	public const string XmlConvert_NotOneCharString = "String must be exactly one character long.";

	// Token: 0x040000F5 RID: 245
	public const string Sch_ParEntityRefNesting = "The parameter entity replacement text must nest properly within markup declarations.";

	// Token: 0x040000F6 RID: 246
	public const string Sch_NotTokenString = "line-feed (#xA) or tab (#x9) characters, leading or trailing spaces and sequences of one or more spaces (#x20) are not allowed in 'xs:token'.";

	// Token: 0x040000F7 RID: 247
	public const string Sch_InvalidDateTimeOption = "The '{0}' value for the 'dateTimeOption' parameter is not an allowed value for the 'XmlDateTimeSerializationMode' enumeration.";

	// Token: 0x040000F8 RID: 248
	public const string Sch_StandAloneNormalization = "StandAlone is 'yes' and the value of the attribute '{0}' contains a definition in an external document that changes on normalization.";

	// Token: 0x040000F9 RID: 249
	public const string Sch_UnSpecifiedDefaultAttributeInExternalStandalone = "Markup for unspecified default attribute '{0}' is external and standalone='yes'.";

	// Token: 0x040000FA RID: 250
	public const string Sch_DefaultException = "A schema error occurred.";

	// Token: 0x040000FB RID: 251
	public const string Sch_DupElementDecl = "The '{0}' element has already been declared.";

	// Token: 0x040000FC RID: 252
	public const string Sch_IdAttrDeclared = "The attribute of type ID is already declared on the '{0}' element.";

	// Token: 0x040000FD RID: 253
	public const string Sch_RootMatchDocType = "Root element name must match the DocType name.";

	// Token: 0x040000FE RID: 254
	public const string Sch_DupId = "'{0}' is already used as an ID.";

	// Token: 0x040000FF RID: 255
	public const string Sch_UndeclaredElement = "The '{0}' element is not declared.";

	// Token: 0x04000100 RID: 256
	public const string Sch_UndeclaredAttribute = "The '{0}' attribute is not declared.";

	// Token: 0x04000101 RID: 257
	public const string Sch_UndeclaredNotation = "The '{0}' notation is not declared.";

	// Token: 0x04000102 RID: 258
	public const string Sch_UndeclaredId = "Reference to undeclared ID is '{0}'.";

	// Token: 0x04000103 RID: 259
	public const string Sch_SchemaRootExpected = "Expected schema root. Make sure the root element is <schema> and the namespace is 'http://www.w3.org/2001/XMLSchema' for an XSD schema or 'urn:schemas-microsoft-com:xml-data' for an XDR schema.";

	// Token: 0x04000104 RID: 260
	public const string Sch_XSDSchemaRootExpected = "The root element of a W3C XML Schema should be <schema> and its namespace should be 'http://www.w3.org/2001/XMLSchema'.";

	// Token: 0x04000105 RID: 261
	public const string Sch_UnsupportedAttribute = "The '{0}' attribute is not supported in this context.";

	// Token: 0x04000106 RID: 262
	public const string Sch_UnsupportedElement = "The '{0}' element is not supported in this context.";

	// Token: 0x04000107 RID: 263
	public const string Sch_MissAttribute = "The '{0}' attribute is either invalid or missing.";

	// Token: 0x04000108 RID: 264
	public const string Sch_AnnotationLocation = "The 'annotation' element cannot appear at this location.";

	// Token: 0x04000109 RID: 265
	public const string Sch_DataTypeTextOnly = "Content must be \"textOnly\" when using DataType on an ElementType.";

	// Token: 0x0400010A RID: 266
	public const string Sch_UnknownModel = "The model attribute must have a value of open or closed, not '{0}'.";

	// Token: 0x0400010B RID: 267
	public const string Sch_UnknownOrder = "The order attribute must have a value of 'seq', 'one', or 'many', not '{0}'.";

	// Token: 0x0400010C RID: 268
	public const string Sch_UnknownContent = "The content attribute must have a value of 'textOnly', 'eltOnly', 'mixed', or 'empty', not '{0}'.";

	// Token: 0x0400010D RID: 269
	public const string Sch_UnknownRequired = "The required attribute must have a value of yes or no.";

	// Token: 0x0400010E RID: 270
	public const string Sch_UnknownDtType = "Reference to an unknown data type, '{0}'.";

	// Token: 0x0400010F RID: 271
	public const string Sch_MixedMany = "The order must be many when content is mixed.";

	// Token: 0x04000110 RID: 272
	public const string Sch_GroupDisabled = "The group is not allowed when ElementType has empty or textOnly content.";

	// Token: 0x04000111 RID: 273
	public const string Sch_MissDtvalue = "The DataType value cannot be empty.";

	// Token: 0x04000112 RID: 274
	public const string Sch_MissDtvaluesAttribute = "The dt:values attribute is missing.";

	// Token: 0x04000113 RID: 275
	public const string Sch_DupDtType = "Data type has already been declared.";

	// Token: 0x04000114 RID: 276
	public const string Sch_DupAttribute = "The '{0}' attribute has already been declared for this ElementType.";

	// Token: 0x04000115 RID: 277
	public const string Sch_RequireEnumeration = "Data type should be enumeration when the values attribute is present.";

	// Token: 0x04000116 RID: 278
	public const string Sch_DefaultIdValue = "An attribute or element of type xs:ID or derived from xs:ID, should not have a value constraint.";

	// Token: 0x04000117 RID: 279
	public const string Sch_ElementNotAllowed = "Element is not allowed when the content is empty or textOnly.";

	// Token: 0x04000118 RID: 280
	public const string Sch_ElementMissing = "There is a missing element.";

	// Token: 0x04000119 RID: 281
	public const string Sch_ManyMaxOccurs = "When the order is many, the maxOccurs attribute must have a value of '*'.";

	// Token: 0x0400011A RID: 282
	public const string Sch_MaxOccursInvalid = "The maxOccurs attribute must have a value of 1 or *.";

	// Token: 0x0400011B RID: 283
	public const string Sch_MinOccursInvalid = "The minOccurs attribute must have a value of 0 or 1.";

	// Token: 0x0400011C RID: 284
	public const string Sch_DtMaxLengthInvalid = "The value '{0}' is invalid for dt:maxLength.";

	// Token: 0x0400011D RID: 285
	public const string Sch_DtMinLengthInvalid = "The value '{0}' is invalid for dt:minLength.";

	// Token: 0x0400011E RID: 286
	public const string Sch_DupDtMaxLength = "The value of maxLength has already been declared.";

	// Token: 0x0400011F RID: 287
	public const string Sch_DupDtMinLength = "The value of minLength has already been declared.";

	// Token: 0x04000120 RID: 288
	public const string Sch_DtMinMaxLength = "The maxLength value must be equal to or greater than the minLength value.";

	// Token: 0x04000121 RID: 289
	public const string Sch_DupElement = "The '{0}' element already exists in the content model.";

	// Token: 0x04000122 RID: 290
	public const string Sch_DupGroupParticle = "The content model can only have one of the following; 'all', 'choice', or 'sequence'.";

	// Token: 0x04000123 RID: 291
	public const string Sch_InvalidValue = "The value '{0}' is invalid according to its data type.";

	// Token: 0x04000124 RID: 292
	public const string Sch_InvalidValueDetailed = "The value '{0}' is invalid according to its schema type '{1}' - {2}";

	// Token: 0x04000125 RID: 293
	public const string Sch_InvalidValueDetailedAttribute = "The attribute '{0}' has an invalid value '{1}' according to its schema type '{2}' - {3}";

	// Token: 0x04000126 RID: 294
	public const string Sch_MissRequiredAttribute = "The required attribute '{0}' is missing.";

	// Token: 0x04000127 RID: 295
	public const string Sch_FixedAttributeValue = "The value of the '{0}' attribute does not equal its fixed value.";

	// Token: 0x04000128 RID: 296
	public const string Sch_FixedElementValue = "The value of the '{0}' element does not equal its fixed value.";

	// Token: 0x04000129 RID: 297
	public const string Sch_AttributeValueDataTypeDetailed = "The '{0}' attribute is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}";

	// Token: 0x0400012A RID: 298
	public const string Sch_AttributeDefaultDataType = "The default value of '{0}' attribute is invalid according to its datatype.";

	// Token: 0x0400012B RID: 299
	public const string Sch_IncludeLocation = "The 'include' element cannot appear at this location.";

	// Token: 0x0400012C RID: 300
	public const string Sch_ImportLocation = "The 'import' element cannot appear at this location.";

	// Token: 0x0400012D RID: 301
	public const string Sch_RedefineLocation = "The 'redefine' element cannot appear at this location.";

	// Token: 0x0400012E RID: 302
	public const string Sch_InvalidBlockDefaultValue = "The values 'list' and 'union' are invalid for the blockDefault attribute.";

	// Token: 0x0400012F RID: 303
	public const string Sch_InvalidFinalDefaultValue = "The value 'substitution' is invalid for the finalDefault attribute.";

	// Token: 0x04000130 RID: 304
	public const string Sch_InvalidElementBlockValue = "The values 'list' and 'union' are invalid for the block attribute on element.";

	// Token: 0x04000131 RID: 305
	public const string Sch_InvalidElementFinalValue = "The values 'substitution', 'list', and 'union' are invalid for the final attribute on element.";

	// Token: 0x04000132 RID: 306
	public const string Sch_InvalidSimpleTypeFinalValue = "The values 'substitution' and 'extension' are invalid for the final attribute on simpleType.";

	// Token: 0x04000133 RID: 307
	public const string Sch_InvalidComplexTypeBlockValue = "The values 'substitution', 'list', and 'union' are invalid for the block attribute on complexType.";

	// Token: 0x04000134 RID: 308
	public const string Sch_InvalidComplexTypeFinalValue = "The values 'substitution', 'list', and 'union' are invalid for the final attribute on complexType.";

	// Token: 0x04000135 RID: 309
	public const string Sch_DupIdentityConstraint = "The identity constraint '{0}' has already been declared.";

	// Token: 0x04000136 RID: 310
	public const string Sch_DupGlobalElement = "The global element '{0}' has already been declared.";

	// Token: 0x04000137 RID: 311
	public const string Sch_DupGlobalAttribute = "The global attribute '{0}' has already been declared.";

	// Token: 0x04000138 RID: 312
	public const string Sch_DupSimpleType = "The simpleType '{0}' has already been declared.";

	// Token: 0x04000139 RID: 313
	public const string Sch_DupComplexType = "The complexType '{0}' has already been declared.";

	// Token: 0x0400013A RID: 314
	public const string Sch_DupGroup = "The group '{0}' has already been declared.";

	// Token: 0x0400013B RID: 315
	public const string Sch_DupAttributeGroup = "The attributeGroup '{0}' has already been declared.";

	// Token: 0x0400013C RID: 316
	public const string Sch_DupNotation = "The notation '{0}' has already been declared.";

	// Token: 0x0400013D RID: 317
	public const string Sch_DefaultFixedAttributes = "The fixed and default attributes cannot both be present.";

	// Token: 0x0400013E RID: 318
	public const string Sch_FixedInRef = "The fixed value constraint on the '{0}' attribute reference must match the fixed value constraint on the declaration.";

	// Token: 0x0400013F RID: 319
	public const string Sch_FixedDefaultInRef = "The default value constraint cannot be present on the '{0}' attribute reference if the fixed value constraint is present on the declaration.";

	// Token: 0x04000140 RID: 320
	public const string Sch_DupXsdElement = "'{0}' is a duplicate XSD element.";

	// Token: 0x04000141 RID: 321
	public const string Sch_ForbiddenAttribute = "The '{0}' attribute cannot be present.";

	// Token: 0x04000142 RID: 322
	public const string Sch_AttributeIgnored = "The '{0}' attribute is ignored, because the value of 'prohibited' for attribute use only prevents inheritance of an identically named attribute from the base type definition.";

	// Token: 0x04000143 RID: 323
	public const string Sch_ElementRef = "When the ref attribute is present, the type attribute and complexType, simpleType, key, keyref, and unique elements cannot be present.";

	// Token: 0x04000144 RID: 324
	public const string Sch_TypeMutualExclusive = "The type attribute cannot be present with either simpleType or complexType.";

	// Token: 0x04000145 RID: 325
	public const string Sch_ElementNameRef = "For element declaration, either the name or the ref attribute must be present.";

	// Token: 0x04000146 RID: 326
	public const string Sch_AttributeNameRef = "For attribute '{0}', either the name or the ref attribute must be present, but not both.";

	// Token: 0x04000147 RID: 327
	public const string Sch_TextNotAllowed = "The following text is not allowed in this context: '{0}'.";

	// Token: 0x04000148 RID: 328
	public const string Sch_UndeclaredType = "Type '{0}' is not declared.";

	// Token: 0x04000149 RID: 329
	public const string Sch_UndeclaredSimpleType = "Type '{0}' is not declared, or is not a simple type.";

	// Token: 0x0400014A RID: 330
	public const string Sch_UndeclaredEquivClass = "Substitution group refers to '{0}', an undeclared element.";

	// Token: 0x0400014B RID: 331
	public const string Sch_AttListPresence = "An attribute of type ID must have a declared default of either #IMPLIED or #REQUIRED.";

	// Token: 0x0400014C RID: 332
	public const string Sch_NotationValue = "'{0}' is not in the notation list.";

	// Token: 0x0400014D RID: 333
	public const string Sch_EnumerationValue = "'{0}' is not in the enumeration list.";

	// Token: 0x0400014E RID: 334
	public const string Sch_EmptyAttributeValue = "The attribute value cannot be empty.";

	// Token: 0x0400014F RID: 335
	public const string Sch_InvalidLanguageId = "'{0}' is an invalid language identifier.";

	// Token: 0x04000150 RID: 336
	public const string Sch_XmlSpace = "Invalid xml:space syntax.";

	// Token: 0x04000151 RID: 337
	public const string Sch_InvalidXsdAttributeValue = "'{1}' is an invalid value for the '{0}' attribute.";

	// Token: 0x04000152 RID: 338
	public const string Sch_InvalidXsdAttributeDatatypeValue = "The value for the '{0}' attribute is invalid - {1}";

	// Token: 0x04000153 RID: 339
	public const string Sch_ElementValueDataTypeDetailed = "The '{0}' element is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}";

	// Token: 0x04000154 RID: 340
	public const string Sch_InvalidElementDefaultValue = "The default value '{0}' of element '{1}' is invalid according to the type specified by xsi:type.";

	// Token: 0x04000155 RID: 341
	public const string Sch_NonDeterministic = "Multiple definition of element '{0}' causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.";

	// Token: 0x04000156 RID: 342
	public const string Sch_NonDeterministicAnyEx = "Wildcard '{0}' allows element '{1}', and causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.";

	// Token: 0x04000157 RID: 343
	public const string Sch_NonDeterministicAnyAny = "Wildcards '{0}' and '{1}' have not empty intersection, and causes the content model to become ambiguous. A content model must be formed such that during validation of an element information item sequence, the particle contained directly, indirectly or implicitly therein with which to attempt to validate each item in the sequence in turn can be uniquely determined without examining the content or attributes of that item, and without any information about the items in the remainder of the sequence.";

	// Token: 0x04000158 RID: 344
	public const string Sch_StandAlone = "The standalone document declaration must have a value of 'no'.";

	// Token: 0x04000159 RID: 345
	public const string Sch_XmlNsAttribute = "The value 'xmlns' cannot be used as the name of an attribute declaration.";

	// Token: 0x0400015A RID: 346
	public const string Sch_AllElement = "Element '{0}' cannot appear more than once if content model type is \"all\".";

	// Token: 0x0400015B RID: 347
	public const string Sch_MismatchTargetNamespaceInclude = "The targetNamespace '{0}' of included/redefined schema should be the same as the targetNamespace '{1}' of the including schema.";

	// Token: 0x0400015C RID: 348
	public const string Sch_MismatchTargetNamespaceImport = "The namespace attribute '{0}' of an import should be the same value as the targetNamespace '{1}' of the imported schema.";

	// Token: 0x0400015D RID: 349
	public const string Sch_MismatchTargetNamespaceEx = "The targetNamespace parameter '{0}' should be the same value as the targetNamespace '{1}' of the schema.";

	// Token: 0x0400015E RID: 350
	public const string Sch_XsiTypeNotFound = "This is an invalid xsi:type '{0}'.";

	// Token: 0x0400015F RID: 351
	public const string Sch_XsiTypeAbstract = "The xsi:type '{0}' cannot be abstract.";

	// Token: 0x04000160 RID: 352
	public const string Sch_ListFromNonatomic = "A list data type must be derived from an atomic or union data type.";

	// Token: 0x04000161 RID: 353
	public const string Sch_UnionFromUnion = "It is an error if a union type has a member with variety union and this member cannot be substituted with its own members. This may be due to the fact that the union member is a restriction of a union with facets.";

	// Token: 0x04000162 RID: 354
	public const string Sch_DupLengthFacet = "This is a duplicate Length constraining facet.";

	// Token: 0x04000163 RID: 355
	public const string Sch_DupMinLengthFacet = "This is a duplicate MinLength constraining facet.";

	// Token: 0x04000164 RID: 356
	public const string Sch_DupMaxLengthFacet = "This is a duplicate MaxLength constraining facet.";

	// Token: 0x04000165 RID: 357
	public const string Sch_DupWhiteSpaceFacet = "This is a duplicate WhiteSpace constraining facet.";

	// Token: 0x04000166 RID: 358
	public const string Sch_DupMaxInclusiveFacet = "This is a duplicate MaxInclusive constraining facet.";

	// Token: 0x04000167 RID: 359
	public const string Sch_DupMaxExclusiveFacet = "This is a duplicate MaxExclusive constraining facet.";

	// Token: 0x04000168 RID: 360
	public const string Sch_DupMinInclusiveFacet = "This is a duplicate MinInclusive constraining facet.";

	// Token: 0x04000169 RID: 361
	public const string Sch_DupMinExclusiveFacet = "This is a duplicate MinExclusive constraining facet.";

	// Token: 0x0400016A RID: 362
	public const string Sch_DupTotalDigitsFacet = "This is a duplicate TotalDigits constraining facet.";

	// Token: 0x0400016B RID: 363
	public const string Sch_DupFractionDigitsFacet = "This is a duplicate FractionDigits constraining facet.";

	// Token: 0x0400016C RID: 364
	public const string Sch_LengthFacetProhibited = "The length constraining facet is prohibited for '{0}'.";

	// Token: 0x0400016D RID: 365
	public const string Sch_MinLengthFacetProhibited = "The MinLength constraining facet is prohibited for '{0}'.";

	// Token: 0x0400016E RID: 366
	public const string Sch_MaxLengthFacetProhibited = "The MaxLength constraining facet is prohibited for '{0}'.";

	// Token: 0x0400016F RID: 367
	public const string Sch_PatternFacetProhibited = "The Pattern constraining facet is prohibited for '{0}'.";

	// Token: 0x04000170 RID: 368
	public const string Sch_EnumerationFacetProhibited = "The Enumeration constraining facet is prohibited for '{0}'.";

	// Token: 0x04000171 RID: 369
	public const string Sch_WhiteSpaceFacetProhibited = "The WhiteSpace constraining facet is prohibited for '{0}'.";

	// Token: 0x04000172 RID: 370
	public const string Sch_MaxInclusiveFacetProhibited = "The MaxInclusive constraining facet is prohibited for '{0}'.";

	// Token: 0x04000173 RID: 371
	public const string Sch_MaxExclusiveFacetProhibited = "The MaxExclusive constraining facet is prohibited for '{0}'.";

	// Token: 0x04000174 RID: 372
	public const string Sch_MinInclusiveFacetProhibited = "The MinInclusive constraining facet is prohibited for '{0}'.";

	// Token: 0x04000175 RID: 373
	public const string Sch_MinExclusiveFacetProhibited = "The MinExclusive constraining facet is prohibited for '{0}'.";

	// Token: 0x04000176 RID: 374
	public const string Sch_TotalDigitsFacetProhibited = "The TotalDigits constraining facet is prohibited for '{0}'.";

	// Token: 0x04000177 RID: 375
	public const string Sch_FractionDigitsFacetProhibited = "The FractionDigits constraining facet is prohibited for '{0}'.";

	// Token: 0x04000178 RID: 376
	public const string Sch_LengthFacetInvalid = "The Length constraining facet is invalid - {0}";

	// Token: 0x04000179 RID: 377
	public const string Sch_MinLengthFacetInvalid = "The MinLength constraining facet is invalid - {0}";

	// Token: 0x0400017A RID: 378
	public const string Sch_MaxLengthFacetInvalid = "The MaxLength constraining facet is invalid - {0}";

	// Token: 0x0400017B RID: 379
	public const string Sch_MaxInclusiveFacetInvalid = "The MaxInclusive constraining facet is invalid - {0}";

	// Token: 0x0400017C RID: 380
	public const string Sch_MaxExclusiveFacetInvalid = "The MaxExclusive constraining facet is invalid - {0}";

	// Token: 0x0400017D RID: 381
	public const string Sch_MinInclusiveFacetInvalid = "The MinInclusive constraining facet is invalid - {0}";

	// Token: 0x0400017E RID: 382
	public const string Sch_MinExclusiveFacetInvalid = "The MinExclusive constraining facet is invalid - {0}";

	// Token: 0x0400017F RID: 383
	public const string Sch_TotalDigitsFacetInvalid = "The TotalDigits constraining facet is invalid - {0}";

	// Token: 0x04000180 RID: 384
	public const string Sch_FractionDigitsFacetInvalid = "The FractionDigits constraining facet is invalid - {0}";

	// Token: 0x04000181 RID: 385
	public const string Sch_PatternFacetInvalid = "The Pattern constraining facet is invalid - {0}";

	// Token: 0x04000182 RID: 386
	public const string Sch_EnumerationFacetInvalid = "The Enumeration constraining facet is invalid - {0}";

	// Token: 0x04000183 RID: 387
	public const string Sch_InvalidWhiteSpace = "The whitespace character, '{0}', is invalid.";

	// Token: 0x04000184 RID: 388
	public const string Sch_UnknownFacet = "This is an unknown facet.";

	// Token: 0x04000185 RID: 389
	public const string Sch_LengthAndMinMax = "It is an error for both length and minLength or maxLength to be present.";

	// Token: 0x04000186 RID: 390
	public const string Sch_MinLengthGtMaxLength = "MinLength is greater than MaxLength.";

	// Token: 0x04000187 RID: 391
	public const string Sch_FractionDigitsGtTotalDigits = "FractionDigits is greater than TotalDigits.";

	// Token: 0x04000188 RID: 392
	public const string Sch_LengthConstraintFailed = "The actual length is not equal to the specified length.";

	// Token: 0x04000189 RID: 393
	public const string Sch_MinLengthConstraintFailed = "The actual length is less than the MinLength value.";

	// Token: 0x0400018A RID: 394
	public const string Sch_MaxLengthConstraintFailed = "The actual length is greater than the MaxLength value.";

	// Token: 0x0400018B RID: 395
	public const string Sch_PatternConstraintFailed = "The Pattern constraint failed.";

	// Token: 0x0400018C RID: 396
	public const string Sch_EnumerationConstraintFailed = "The Enumeration constraint failed.";

	// Token: 0x0400018D RID: 397
	public const string Sch_MaxInclusiveConstraintFailed = "The MaxInclusive constraint failed.";

	// Token: 0x0400018E RID: 398
	public const string Sch_MaxExclusiveConstraintFailed = "The MaxExclusive constraint failed.";

	// Token: 0x0400018F RID: 399
	public const string Sch_MinInclusiveConstraintFailed = "The MinInclusive constraint failed.";

	// Token: 0x04000190 RID: 400
	public const string Sch_MinExclusiveConstraintFailed = "The MinExclusive constraint failed.";

	// Token: 0x04000191 RID: 401
	public const string Sch_TotalDigitsConstraintFailed = "The TotalDigits constraint failed.";

	// Token: 0x04000192 RID: 402
	public const string Sch_FractionDigitsConstraintFailed = "The FractionDigits constraint failed.";

	// Token: 0x04000193 RID: 403
	public const string Sch_UnionFailedEx = "The value '{0}' is not valid according to any of the memberTypes of the union.";

	// Token: 0x04000194 RID: 404
	public const string Sch_NotationRequired = "NOTATION cannot be used directly in a schema; only data types derived from NOTATION by specifying an enumeration value can be used in a schema. All enumeration facet values must match the name of a notation declared in the current schema.";

	// Token: 0x04000195 RID: 405
	public const string Sch_DupNotationAttribute = "No element type can have more than one NOTATION attribute specified.";

	// Token: 0x04000196 RID: 406
	public const string Sch_MissingPublicSystemAttribute = "NOTATION must have either the Public or System attribute present.";

	// Token: 0x04000197 RID: 407
	public const string Sch_NotationAttributeOnEmptyElement = "An attribute of type NOTATION must not be declared on an element declared EMPTY.";

	// Token: 0x04000198 RID: 408
	public const string Sch_RefNotInScope = "The Keyref '{0}' cannot find the referred key or unique in scope.";

	// Token: 0x04000199 RID: 409
	public const string Sch_UndeclaredIdentityConstraint = "The '{0}' identity constraint is not declared.";

	// Token: 0x0400019A RID: 410
	public const string Sch_RefInvalidIdentityConstraint = "Reference to an invalid identity constraint, '{0}'.";

	// Token: 0x0400019B RID: 411
	public const string Sch_RefInvalidCardin = "Keyref '{0}' has different cardinality as the referred key or unique element.";

	// Token: 0x0400019C RID: 412
	public const string Sch_ReftoKeyref = "The '{0}' Keyref can refer to key or unique only.";

	// Token: 0x0400019D RID: 413
	public const string Sch_EmptyXPath = "The XPath for selector or field cannot be empty.";

	// Token: 0x0400019E RID: 414
	public const string Sch_UnresolvedPrefix = "The prefix '{0}' in XPath cannot be resolved.";

	// Token: 0x0400019F RID: 415
	public const string Sch_UnresolvedKeyref = "The key sequence '{0}' in '{1}' Keyref fails to refer to some key.";

	// Token: 0x040001A0 RID: 416
	public const string Sch_ICXpathError = "'{0}' is an invalid XPath for selector or field.";

	// Token: 0x040001A1 RID: 417
	public const string Sch_SelectorAttr = "'{0}' is an invalid XPath for selector. Selector cannot have an XPath selection with an attribute node.";

	// Token: 0x040001A2 RID: 418
	public const string Sch_FieldSimpleTypeExpected = "The field '{0}' is expecting an element or attribute with simple type or simple content.";

	// Token: 0x040001A3 RID: 419
	public const string Sch_FieldSingleValueExpected = "The field '{0}' is expecting at the most one value.";

	// Token: 0x040001A4 RID: 420
	public const string Sch_MissingKey = "The identity constraint '{0}' validation has failed. Either a key is missing or the existing key has an empty node.";

	// Token: 0x040001A5 RID: 421
	public const string Sch_DuplicateKey = "There is a duplicate key sequence '{0}' for the '{1}' key or unique identity constraint.";

	// Token: 0x040001A6 RID: 422
	public const string Sch_TargetNamespaceXsi = "The target namespace of an attribute declaration, whether local or global, must not match http://www.w3.org/2001/XMLSchema-instance.";

	// Token: 0x040001A7 RID: 423
	public const string Sch_UndeclaredEntity = "Reference to an undeclared entity, '{0}'.";

	// Token: 0x040001A8 RID: 424
	public const string Sch_UnparsedEntityRef = "Reference to an unparsed entity, '{0}'.";

	// Token: 0x040001A9 RID: 425
	public const string Sch_MaxOccursInvalidXsd = "The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.";

	// Token: 0x040001AA RID: 426
	public const string Sch_MinOccursInvalidXsd = "The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.";

	// Token: 0x040001AB RID: 427
	public const string Sch_MaxInclusiveExclusive = "'maxInclusive' and 'maxExclusive' cannot both be specified for the same data type.";

	// Token: 0x040001AC RID: 428
	public const string Sch_MinInclusiveExclusive = "'minInclusive' and 'minExclusive' cannot both be specified for the same data type.";

	// Token: 0x040001AD RID: 429
	public const string Sch_MinInclusiveGtMaxInclusive = "The value specified for 'minInclusive' cannot be greater than the value specified for 'maxInclusive' for the same data type.";

	// Token: 0x040001AE RID: 430
	public const string Sch_MinExclusiveGtMaxExclusive = "The value specified for 'minExclusive' cannot be greater than the value specified for 'maxExclusive' for the same data type.";

	// Token: 0x040001AF RID: 431
	public const string Sch_MinInclusiveGtMaxExclusive = "The value specified for 'minInclusive' cannot be greater than the value specified for 'maxExclusive' for the same data type.";

	// Token: 0x040001B0 RID: 432
	public const string Sch_MinExclusiveGtMaxInclusive = "The value specified for 'minExclusive' cannot be greater than the value specified for 'maxInclusive' for the same data type.";

	// Token: 0x040001B1 RID: 433
	public const string Sch_SimpleTypeRestriction = "'simpleType' should be the first child of restriction.";

	// Token: 0x040001B2 RID: 434
	public const string Sch_InvalidFacetPosition = "Facet should go before 'attribute', 'attributeGroup', or 'anyAttribute'.";

	// Token: 0x040001B3 RID: 435
	public const string Sch_AttributeMutuallyExclusive = "'{0}' and content model are mutually exclusive.";

	// Token: 0x040001B4 RID: 436
	public const string Sch_AnyAttributeLastChild = "'anyAttribute' must be the last child.";

	// Token: 0x040001B5 RID: 437
	public const string Sch_ComplexTypeContentModel = "The content model of a complex type must consist of 'annotation' (if present); followed by zero or one of the following: 'simpleContent', 'complexContent', 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.";

	// Token: 0x040001B6 RID: 438
	public const string Sch_ComplexContentContentModel = "Complex content restriction or extension should consist of zero or one of 'group', 'choice', 'sequence', or 'all'; followed by zero or more 'attribute' or 'attributeGroup'; followed by zero or one 'anyAttribute'.";

	// Token: 0x040001B7 RID: 439
	public const string Sch_NotNormalizedString = "Carriage return (#xD), line feed (#xA), and tab (#x9) characters are not allowed in xs:normalizedString.";

	// Token: 0x040001B8 RID: 440
	public const string Sch_FractionDigitsNotOnDecimal = "FractionDigits should be equal to 0 on types other then decimal.";

	// Token: 0x040001B9 RID: 441
	public const string Sch_ContentInNill = "Element '{0}' must have no character or element children.";

	// Token: 0x040001BA RID: 442
	public const string Sch_NoElementSchemaFound = "Could not find schema information for the element '{0}'.";

	// Token: 0x040001BB RID: 443
	public const string Sch_NoAttributeSchemaFound = "Could not find schema information for the attribute '{0}'.";

	// Token: 0x040001BC RID: 444
	public const string Sch_InvalidNamespace = "The Namespace '{0}' is an invalid URI.";

	// Token: 0x040001BD RID: 445
	public const string Sch_InvalidTargetNamespaceAttribute = "The targetNamespace attribute cannot have empty string as its value.";

	// Token: 0x040001BE RID: 446
	public const string Sch_InvalidNamespaceAttribute = "The namespace attribute cannot have empty string as its value.";

	// Token: 0x040001BF RID: 447
	public const string Sch_InvalidSchemaLocation = "The SchemaLocation '{0}' is an invalid URI.";

	// Token: 0x040001C0 RID: 448
	public const string Sch_ImportTargetNamespace = "Namespace attribute of an import must not match the real value of the enclosing targetNamespace of the <schema>.";

	// Token: 0x040001C1 RID: 449
	public const string Sch_ImportTargetNamespaceNull = "The enclosing <schema> must have a targetNamespace, if the Namespace attribute is absent on the import element.";

	// Token: 0x040001C2 RID: 450
	public const string Sch_GroupDoubleRedefine = "Double redefine for group.";

	// Token: 0x040001C3 RID: 451
	public const string Sch_ComponentRedefineNotFound = "Cannot find a {0} with name '{1}' to redefine.";

	// Token: 0x040001C4 RID: 452
	public const string Sch_GroupRedefineNotFound = "No group to redefine.";

	// Token: 0x040001C5 RID: 453
	public const string Sch_AttrGroupDoubleRedefine = "Double redefine for attribute group.";

	// Token: 0x040001C6 RID: 454
	public const string Sch_AttrGroupRedefineNotFound = "No attribute group to redefine.";

	// Token: 0x040001C7 RID: 455
	public const string Sch_ComplexTypeDoubleRedefine = "Double redefine for complex type.";

	// Token: 0x040001C8 RID: 456
	public const string Sch_ComplexTypeRedefineNotFound = "No complex type to redefine.";

	// Token: 0x040001C9 RID: 457
	public const string Sch_SimpleToComplexTypeRedefine = "Cannot redefine a simple type as complex type.";

	// Token: 0x040001CA RID: 458
	public const string Sch_SimpleTypeDoubleRedefine = "Double redefine for simple type.";

	// Token: 0x040001CB RID: 459
	public const string Sch_ComplexToSimpleTypeRedefine = "Cannot redefine a complex type as simple type.";

	// Token: 0x040001CC RID: 460
	public const string Sch_SimpleTypeRedefineNotFound = "No simple type to redefine.";

	// Token: 0x040001CD RID: 461
	public const string Sch_MinMaxGroupRedefine = "When group is redefined, the real value of both minOccurs and maxOccurs attribute must be 1 (or absent).";

	// Token: 0x040001CE RID: 462
	public const string Sch_MultipleGroupSelfRef = "Multiple self-reference within a group is redefined.";

	// Token: 0x040001CF RID: 463
	public const string Sch_MultipleAttrGroupSelfRef = "Multiple self-reference within an attribute group is redefined.";

	// Token: 0x040001D0 RID: 464
	public const string Sch_InvalidTypeRedefine = "If type is being redefined, the base type has to be self-referenced.";

	// Token: 0x040001D1 RID: 465
	public const string Sch_InvalidElementRef = "If ref is present, all of <complexType>, <simpleType>, <key>, <keyref>, <unique>, nillable, default, fixed, form, block, and type must be absent.";

	// Token: 0x040001D2 RID: 466
	public const string Sch_MinGtMax = "minOccurs value cannot be greater than maxOccurs value.";

	// Token: 0x040001D3 RID: 467
	public const string Sch_DupSelector = "Selector cannot appear twice in one identity constraint.";

	// Token: 0x040001D4 RID: 468
	public const string Sch_IdConstraintNoSelector = "Selector must be present.";

	// Token: 0x040001D5 RID: 469
	public const string Sch_IdConstraintNoFields = "At least one field must be present.";

	// Token: 0x040001D6 RID: 470
	public const string Sch_IdConstraintNoRefer = "The referring attribute must be present.";

	// Token: 0x040001D7 RID: 471
	public const string Sch_SelectorBeforeFields = "Cannot define fields before selector.";

	// Token: 0x040001D8 RID: 472
	public const string Sch_NoSimpleTypeContent = "SimpleType content is missing.";

	// Token: 0x040001D9 RID: 473
	public const string Sch_SimpleTypeRestRefBase = "SimpleType restriction should have either the base attribute or a simpleType child, but not both.";

	// Token: 0x040001DA RID: 474
	public const string Sch_SimpleTypeRestRefBaseNone = "SimpleType restriction should have either the base attribute or a simpleType child to indicate the base type for the derivation.";

	// Token: 0x040001DB RID: 475
	public const string Sch_SimpleTypeListRefBase = "SimpleType list should have either the itemType attribute or a simpleType child, but not both.";

	// Token: 0x040001DC RID: 476
	public const string Sch_SimpleTypeListRefBaseNone = "SimpleType list should have either the itemType attribute or a simpleType child to indicate the itemType of the list. ";

	// Token: 0x040001DD RID: 477
	public const string Sch_SimpleTypeUnionNoBase = "Either the memberTypes attribute must be non-empty or there must be at least one simpleType child.";

	// Token: 0x040001DE RID: 478
	public const string Sch_NoRestOrExtQName = "'restriction' or 'extension' child is required for complexType '{0}' in namespace '{1}', because it has a simpleContent or complexContent child.";

	// Token: 0x040001DF RID: 479
	public const string Sch_NoRestOrExt = "'restriction' or 'extension' child is required for complexType with simpleContent or complexContent child.";

	// Token: 0x040001E0 RID: 480
	public const string Sch_NoGroupParticle = "'sequence', 'choice', or 'all' child is required.";

	// Token: 0x040001E1 RID: 481
	public const string Sch_InvalidAllMin = "'all' must have 'minOccurs' value of 0 or 1.";

	// Token: 0x040001E2 RID: 482
	public const string Sch_InvalidAllMax = "'all' must have {max occurs}=1.";

	// Token: 0x040001E3 RID: 483
	public const string Sch_InvalidFacet = "The 'value' attribute must be present in facet.";

	// Token: 0x040001E4 RID: 484
	public const string Sch_AbstractElement = "The element '{0}' is abstract or its type is abstract.";

	// Token: 0x040001E5 RID: 485
	public const string Sch_XsiTypeBlockedEx = "The xsi:type attribute value '{0}' is not valid for the element '{1}', either because it is not a type validly derived from the type in the schema, or because it has xsi:type derivation blocked.";

	// Token: 0x040001E6 RID: 486
	public const string Sch_InvalidXsiNill = "If the 'nillable' attribute is false in the schema, the 'xsi:nil' attribute must not be present in the instance.";

	// Token: 0x040001E7 RID: 487
	public const string Sch_SubstitutionNotAllowed = "Element '{0}' cannot substitute in place of head element '{1}' because it has block='substitution'.";

	// Token: 0x040001E8 RID: 488
	public const string Sch_SubstitutionBlocked = "Member element {0}'s type cannot be derived by restriction or extension from head element {1}'s type, because it has block='restriction' or 'extension'.";

	// Token: 0x040001E9 RID: 489
	public const string Sch_InvalidElementInEmptyEx = "The element '{0}' cannot contain child element '{1}' because the parent element's content model is empty.";

	// Token: 0x040001EA RID: 490
	public const string Sch_InvalidElementInTextOnlyEx = "The element '{0}' cannot contain child element '{1}' because the parent element's content model is text only.";

	// Token: 0x040001EB RID: 491
	public const string Sch_InvalidTextInElement = "The element {0} cannot contain text.";

	// Token: 0x040001EC RID: 492
	public const string Sch_InvalidElementContent = "The element {0} has invalid child element {1}.";

	// Token: 0x040001ED RID: 493
	public const string Sch_InvalidElementContentComplex = "The element {0} has invalid child element {1} - {2}";

	// Token: 0x040001EE RID: 494
	public const string Sch_IncompleteContent = "The element {0} has incomplete content.";

	// Token: 0x040001EF RID: 495
	public const string Sch_IncompleteContentComplex = "The element {0} has incomplete content - {2}";

	// Token: 0x040001F0 RID: 496
	public const string Sch_InvalidTextInElementExpecting = "The element {0} cannot contain text. List of possible elements expected: {1}.";

	// Token: 0x040001F1 RID: 497
	public const string Sch_InvalidElementContentExpecting = "The element {0} has invalid child element {1}. List of possible elements expected: {2}.";

	// Token: 0x040001F2 RID: 498
	public const string Sch_InvalidElementContentExpectingComplex = "The element {0} has invalid child element {1}. List of possible elements expected: {2}. {3}";

	// Token: 0x040001F3 RID: 499
	public const string Sch_IncompleteContentExpecting = "The element {0} has incomplete content. List of possible elements expected: {1}.";

	// Token: 0x040001F4 RID: 500
	public const string Sch_IncompleteContentExpectingComplex = "The element {0} has incomplete content. List of possible elements expected: {1}. {2}";

	// Token: 0x040001F5 RID: 501
	public const string Sch_InvalidElementSubstitution = "The element {0} cannot substitute for a local element {1} expected in that position.";

	// Token: 0x040001F6 RID: 502
	public const string Sch_ElementNameAndNamespace = "'{0}' in namespace '{1}'";

	// Token: 0x040001F7 RID: 503
	public const string Sch_ElementName = "'{0}'";

	// Token: 0x040001F8 RID: 504
	public const string Sch_ContinuationString = "{0}as well as ";

	// Token: 0x040001F9 RID: 505
	public const string Sch_AnyElementNS = "any element in namespace '{0}'";

	// Token: 0x040001FA RID: 506
	public const string Sch_AnyElement = "any element";

	// Token: 0x040001FB RID: 507
	public const string Sch_InvalidTextInEmpty = "The element cannot contain text. Content model is empty.";

	// Token: 0x040001FC RID: 508
	public const string Sch_InvalidWhitespaceInEmpty = "The element cannot contain whitespace. Content model is empty.";

	// Token: 0x040001FD RID: 509
	public const string Sch_InvalidPIComment = "The element cannot contain comment or processing instruction. Content model is empty.";

	// Token: 0x040001FE RID: 510
	public const string Sch_InvalidAttributeRef = "If ref is present, all of 'simpleType', 'form', 'type', and 'use' must be absent.";

	// Token: 0x040001FF RID: 511
	public const string Sch_OptionalDefaultAttribute = "The 'use' attribute must be optional (or absent) if the default attribute is present.";

	// Token: 0x04000200 RID: 512
	public const string Sch_AttributeCircularRef = "Circular attribute reference.";

	// Token: 0x04000201 RID: 513
	public const string Sch_IdentityConstraintCircularRef = "Circular identity constraint reference.";

	// Token: 0x04000202 RID: 514
	public const string Sch_SubstitutionCircularRef = "Circular substitution group affiliation.";

	// Token: 0x04000203 RID: 515
	public const string Sch_InvalidAnyAttribute = "Invalid namespace in 'anyAttribute'.";

	// Token: 0x04000204 RID: 516
	public const string Sch_DupIdAttribute = "Duplicate ID attribute.";

	// Token: 0x04000205 RID: 517
	public const string Sch_InvalidAllElementMax = "The {max occurs} of all the particles in the {particles} of an all group must be 0 or 1.";

	// Token: 0x04000206 RID: 518
	public const string Sch_InvalidAny = "Invalid namespace in 'any'.";

	// Token: 0x04000207 RID: 519
	public const string Sch_InvalidAnyDetailed = "The value of the namespace attribute of the element or attribute wildcard is invalid - {0}";

	// Token: 0x04000208 RID: 520
	public const string Sch_InvalidExamplar = "Cannot be nominated as the {substitution group affiliation} of any other declaration.";

	// Token: 0x04000209 RID: 521
	public const string Sch_NoExamplar = "Reference to undeclared substitution group affiliation.";

	// Token: 0x0400020A RID: 522
	public const string Sch_InvalidSubstitutionMember = "'{0}' cannot be a member of substitution group with head element '{1}'.";

	// Token: 0x0400020B RID: 523
	public const string Sch_RedefineNoSchema = "'SchemaLocation' must successfully resolve if <redefine> contains any child other than <annotation>.";

	// Token: 0x0400020C RID: 524
	public const string Sch_ProhibitedAttribute = "The '{0}' attribute is not allowed.";

	// Token: 0x0400020D RID: 525
	public const string Sch_TypeCircularRef = "Circular type reference.";

	// Token: 0x0400020E RID: 526
	public const string Sch_TwoIdAttrUses = "Two distinct members of the attribute uses must not have type definitions which are both xs:ID or are derived from xs:ID.";

	// Token: 0x0400020F RID: 527
	public const string Sch_AttrUseAndWildId = "It is an error if there is a member of the attribute uses of a type definition with type xs:ID or derived from xs:ID and another attribute with type xs:ID matches an attribute wildcard.";

	// Token: 0x04000210 RID: 528
	public const string Sch_MoreThanOneWildId = "It is an error if more than one attribute whose type is xs:ID or is derived from xs:ID, matches an attribute wildcard on an element.";

	// Token: 0x04000211 RID: 529
	public const string Sch_BaseFinalExtension = "The base type is the final extension.";

	// Token: 0x04000212 RID: 530
	public const string Sch_NotSimpleContent = "The content type of the base type must be a simple type definition or it must be mixed, and simpleType child must be present.";

	// Token: 0x04000213 RID: 531
	public const string Sch_NotComplexContent = "The content type of the base type must not be a simple type definition.";

	// Token: 0x04000214 RID: 532
	public const string Sch_BaseFinalRestriction = "The base type is final restriction.";

	// Token: 0x04000215 RID: 533
	public const string Sch_BaseFinalList = "The base type is the final list.";

	// Token: 0x04000216 RID: 534
	public const string Sch_BaseFinalUnion = "The base type is the final union.";

	// Token: 0x04000217 RID: 535
	public const string Sch_UndefBaseRestriction = "Undefined complexType '{0}' is used as a base for complex type restriction.";

	// Token: 0x04000218 RID: 536
	public const string Sch_UndefBaseExtension = "Undefined complexType '{0}' is used as a base for complex type extension.";

	// Token: 0x04000219 RID: 537
	public const string Sch_DifContentType = "The derived type and the base type must have the same content type.";

	// Token: 0x0400021A RID: 538
	public const string Sch_InvalidContentRestriction = "Invalid content type derivation by restriction.";

	// Token: 0x0400021B RID: 539
	public const string Sch_InvalidContentRestrictionDetailed = "Invalid content type derivation by restriction. {0}";

	// Token: 0x0400021C RID: 540
	public const string Sch_InvalidBaseToEmpty = "If the derived content type is Empty, then the base content type should also be Empty or Mixed with Emptiable particle according to rule 5.3 of Schema Component Constraint: Derivation Valid (Restriction, Complex).";

	// Token: 0x0400021D RID: 541
	public const string Sch_InvalidBaseToMixed = "If the derived content type is Mixed, then the base content type should also be Mixed according to rule 5.4 of Schema Component Constraint: Derivation Valid (Restriction, Complex).";

	// Token: 0x0400021E RID: 542
	public const string Sch_DupAttributeUse = "The attribute '{0}' already exists.";

	// Token: 0x0400021F RID: 543
	public const string Sch_InvalidParticleRestriction = "Invalid particle derivation by restriction.";

	// Token: 0x04000220 RID: 544
	public const string Sch_InvalidParticleRestrictionDetailed = "Invalid particle derivation by restriction - '{0}'.";

	// Token: 0x04000221 RID: 545
	public const string Sch_ForbiddenDerivedParticleForAll = "'Choice' or 'any' is forbidden as derived particle when the base particle is 'all'.";

	// Token: 0x04000222 RID: 546
	public const string Sch_ForbiddenDerivedParticleForElem = "Only 'element' is valid as derived particle when the base particle is 'element'.";

	// Token: 0x04000223 RID: 547
	public const string Sch_ForbiddenDerivedParticleForChoice = "'All' or 'any' is forbidden as derived particle when the base particle is 'choice'.";

	// Token: 0x04000224 RID: 548
	public const string Sch_ForbiddenDerivedParticleForSeq = "'All', 'any', and 'choice' are forbidden as derived particles when the base particle is 'sequence'.";

	// Token: 0x04000225 RID: 549
	public const string Sch_ElementFromElement = "Derived element '{0}' is not a valid restriction of base element '{1}' according to Elt:Elt -- NameAndTypeOK.";

	// Token: 0x04000226 RID: 550
	public const string Sch_ElementFromAnyRule1 = "The namespace of element '{0}'is not valid with respect to the wildcard's namespace constraint in the base, Elt:Any -- NSCompat Rule 1.";

	// Token: 0x04000227 RID: 551
	public const string Sch_ElementFromAnyRule2 = "The occurrence range of element '{0}'is not a valid restriction of the wildcard's occurrence range in the base, Elt:Any -- NSCompat Rule2.";

	// Token: 0x04000228 RID: 552
	public const string Sch_AnyFromAnyRule1 = "The derived wildcard's occurrence range is not a valid restriction of the base wildcard's occurrence range, Any:Any -- NSSubset Rule 1.";

	// Token: 0x04000229 RID: 553
	public const string Sch_AnyFromAnyRule2 = "The derived wildcard's namespace constraint must be an intensional subset of the base wildcard's namespace constraint, Any:Any -- NSSubset Rule2.";

	// Token: 0x0400022A RID: 554
	public const string Sch_AnyFromAnyRule3 = "The derived wildcard's 'processContents' must be identical to or stronger than the base wildcard's 'processContents', where 'strict' is stronger than 'lax' and 'lax' is stronger than 'skip', Any:Any -- NSSubset Rule 3.";

	// Token: 0x0400022B RID: 555
	public const string Sch_GroupBaseFromAny1 = "Every member of the derived group particle must be a valid restriction of the base wildcard, NSRecurseCheckCardinality Rule 1.";

	// Token: 0x0400022C RID: 556
	public const string Sch_GroupBaseFromAny2 = "The derived particle's occurrence range at ({0}, {1}) is not a valid restriction of the base wildcard's occurrence range at ({2}, {3}), NSRecurseCheckCardinality Rule 2.";

	// Token: 0x0400022D RID: 557
	public const string Sch_ElementFromGroupBase1 = "The derived element {0} at ({1}, {2}) is not a valid restriction of the base sequence particle at ({3}, {4}) according to Elt:All/Choice/Sequence -- RecurseAsIfGroup.";

	// Token: 0x0400022E RID: 558
	public const string Sch_ElementFromGroupBase2 = "The derived element {0} at ({1}, {2}) is not a valid restriction of the base choice particle at ({3}, {4}) according to Elt:All/Choice/Sequence -- RecurseAsIfGroup.";

	// Token: 0x0400022F RID: 559
	public const string Sch_ElementFromGroupBase3 = "The derived element {0} at ({1}, {2}) is not a valid restriction of the base all particle at ({3}, {4}) according to Elt:All/Choice/Sequence -- RecurseAsIfGroup.";

	// Token: 0x04000230 RID: 560
	public const string Sch_GroupBaseRestRangeInvalid = "The derived particle's range is not a valid restriction of the base particle's range according to All:All,Sequence:Sequence -- Recurse Rule 1 or Choice:Choice -- RecurseLax.";

	// Token: 0x04000231 RID: 561
	public const string Sch_GroupBaseRestNoMap = "The derived particle cannot have more members than the base particle - All:All,Sequence:Sequence -- Recurse Rule 2 / Choice:Choice -- RecurseLax.";

	// Token: 0x04000232 RID: 562
	public const string Sch_GroupBaseRestNotEmptiable = "All particles in the {particles} of the base particle which are not mapped to by any particle in the {particles} of the derived particle should be emptiable - All:All,Sequence:Sequence -- Recurse Rule 2 / Choice:Choice -- RecurseLax.";

	// Token: 0x04000233 RID: 563
	public const string Sch_SeqFromAll = "The derived sequence particle at ({0}, {1}) is not a valid restriction of the base all particle at ({2}, {3}) according to Sequence:All -- RecurseUnordered.";

	// Token: 0x04000234 RID: 564
	public const string Sch_SeqFromChoice = "The derived sequence particle at ({0}, {1}) is not a valid restriction of the base choice particle at ({2}, {3}) according to Sequence:Choice -- MapAndSum.";

	// Token: 0x04000235 RID: 565
	public const string Sch_UndefGroupRef = "Reference to undeclared model group '{0}'.";

	// Token: 0x04000236 RID: 566
	public const string Sch_GroupCircularRef = "Circular group reference.";

	// Token: 0x04000237 RID: 567
	public const string Sch_AllRefNotRoot = "The group ref to 'all' is not the root particle, or it is being used as an extension.";

	// Token: 0x04000238 RID: 568
	public const string Sch_AllRefMinMax = "The group ref to 'all' must have {min occurs}= 0 or 1 and {max occurs}=1.";

	// Token: 0x04000239 RID: 569
	public const string Sch_NotAllAlone = "'all' is not the only particle in a group, or is being used as an extension.";

	// Token: 0x0400023A RID: 570
	public const string Sch_AttributeGroupCircularRef = "Circular attribute group reference.";

	// Token: 0x0400023B RID: 571
	public const string Sch_UndefAttributeGroupRef = "Reference to undeclared attribute group '{0}'.";

	// Token: 0x0400023C RID: 572
	public const string Sch_InvalidAttributeExtension = "Invalid attribute extension.";

	// Token: 0x0400023D RID: 573
	public const string Sch_InvalidAnyAttributeRestriction = "The base any attribute must be a superset of the derived 'anyAttribute'.";

	// Token: 0x0400023E RID: 574
	public const string Sch_AttributeRestrictionProhibited = "Invalid attribute restriction. Attribute restriction is prohibited in base type.";

	// Token: 0x0400023F RID: 575
	public const string Sch_AttributeRestrictionInvalid = "Invalid attribute restriction. Derived attribute's type is not a valid restriction of the base attribute's type.";

	// Token: 0x04000240 RID: 576
	public const string Sch_AttributeFixedInvalid = "Invalid attribute restriction. Derived attribute's fixed value must be the same as the base attribute's fixed value. ";

	// Token: 0x04000241 RID: 577
	public const string Sch_AttributeUseInvalid = "Derived attribute's use has to be required if base attribute's use is required.";

	// Token: 0x04000242 RID: 578
	public const string Sch_AttributeRestrictionInvalidFromWildcard = "The {base type definition} must have an {attribute wildcard} and the {target namespace} of the R's {attribute declaration} must be valid with respect to that wildcard.";

	// Token: 0x04000243 RID: 579
	public const string Sch_NoDerivedAttribute = "The base attribute '{0}' whose use = 'required' does not have a corresponding derived attribute while redefining attribute group '{1}'.";

	// Token: 0x04000244 RID: 580
	public const string Sch_UnexpressibleAnyAttribute = "The 'anyAttribute' is not expressible.";

	// Token: 0x04000245 RID: 581
	public const string Sch_RefInvalidAttribute = "Reference to invalid attribute '{0}'.";

	// Token: 0x04000246 RID: 582
	public const string Sch_ElementCircularRef = "Circular element reference.";

	// Token: 0x04000247 RID: 583
	public const string Sch_RefInvalidElement = "Reference to invalid element '{0}'.";

	// Token: 0x04000248 RID: 584
	public const string Sch_ElementCannotHaveValue = "Element's type does not allow fixed or default value constraint.";

	// Token: 0x04000249 RID: 585
	public const string Sch_ElementInMixedWithFixed = "Although the '{0}' element's content type is mixed, it cannot have element children, because it has a fixed value constraint in the schema.";

	// Token: 0x0400024A RID: 586
	public const string Sch_ElementTypeCollision = "Elements with the same name and in the same scope must have the same type.";

	// Token: 0x0400024B RID: 587
	public const string Sch_InvalidIncludeLocation = "Cannot resolve the 'schemaLocation' attribute.";

	// Token: 0x0400024C RID: 588
	public const string Sch_CannotLoadSchema = "Cannot load the schema for the namespace '{0}' - {1}";

	// Token: 0x0400024D RID: 589
	public const string Sch_CannotLoadSchemaLocation = "Cannot load the schema from the location '{0}' - {1}";

	// Token: 0x0400024E RID: 590
	public const string Sch_LengthGtBaseLength = "It is an error if 'length' is among the members of {facets} of {base type definition} and {value} is greater than the {value} of the parent 'length'.";

	// Token: 0x0400024F RID: 591
	public const string Sch_MinLengthGtBaseMinLength = "It is an error if 'minLength' is among the members of {facets} of {base type definition} and {value} is less than the {value} of the parent 'minLength'.";

	// Token: 0x04000250 RID: 592
	public const string Sch_MaxLengthGtBaseMaxLength = "It is an error if 'maxLength' is among the members of {facets} of {base type definition} and {value} is greater than the {value} of the parent 'maxLength'.";

	// Token: 0x04000251 RID: 593
	public const string Sch_MaxMinLengthBaseLength = "It is an error for both 'length' and either 'minLength' or 'maxLength' to be members of {facets}, unless they are specified in different derivation steps. In which case the following must be true: the {value} of 'minLength' <= the {value} of 'length' <= the {value} of 'maxLength'.";

	// Token: 0x04000252 RID: 594
	public const string Sch_MaxInclusiveMismatch = "It is an error if the derived 'maxInclusive' facet value is greater than the parent 'maxInclusive' facet value.";

	// Token: 0x04000253 RID: 595
	public const string Sch_MaxExclusiveMismatch = "It is an error if the derived 'maxExclusive' facet value is greater than the parent 'maxExclusive' facet value.";

	// Token: 0x04000254 RID: 596
	public const string Sch_MinInclusiveMismatch = "It is an error if the derived 'minInclusive' facet value is less than the parent 'minInclusive' facet value.";

	// Token: 0x04000255 RID: 597
	public const string Sch_MinExclusiveMismatch = "It is an error if the derived 'minExclusive' facet value is less than the parent 'minExclusive' facet value.";

	// Token: 0x04000256 RID: 598
	public const string Sch_MinExlIncMismatch = "It is an error if the derived 'minExclusive' facet value is less than or equal to the parent 'minInclusive' facet value.";

	// Token: 0x04000257 RID: 599
	public const string Sch_MinExlMaxExlMismatch = "It is an error if the derived 'minExclusive' facet value is greater than or equal to the parent 'maxExclusive' facet value.";

	// Token: 0x04000258 RID: 600
	public const string Sch_MinIncMaxExlMismatch = "It is an error if the derived 'minInclusive' facet value is greater than or equal to the parent 'maxExclusive' facet value.";

	// Token: 0x04000259 RID: 601
	public const string Sch_MinIncExlMismatch = "It is an error if the derived 'minInclusive' facet value is less than or equal to the parent 'minExclusive' facet value.";

	// Token: 0x0400025A RID: 602
	public const string Sch_MaxIncExlMismatch = "It is an error if the derived 'maxInclusive' facet value is greater than or equal to the parent 'maxExclusive' facet value.";

	// Token: 0x0400025B RID: 603
	public const string Sch_MaxExlIncMismatch = "It is an error if the derived 'maxExclusive' facet value is greater than or equal to the parent 'maxInclusive' facet value.";

	// Token: 0x0400025C RID: 604
	public const string Sch_TotalDigitsMismatch = "It is an error if the derived 'totalDigits' facet value is greater than the parent 'totalDigits' facet value.";

	// Token: 0x0400025D RID: 605
	public const string Sch_FacetBaseFixed = "Values that are declared as {fixed} in a base type can not be changed in a derived type.";

	// Token: 0x0400025E RID: 606
	public const string Sch_WhiteSpaceRestriction1 = "It is an error if 'whiteSpace' is among the members of {facets} of {base type definition}, {value} is 'replace' or 'preserve', and the {value} of the parent 'whiteSpace' is 'collapse'.";

	// Token: 0x0400025F RID: 607
	public const string Sch_WhiteSpaceRestriction2 = "It is an error if 'whiteSpace' is among the members of {facets} of {base type definition}, {value} is 'preserve', and the {value} of the parent 'whiteSpace' is 'replace'.";

	// Token: 0x04000260 RID: 608
	public const string Sch_XsiNilAndFixed = "There must be no fixed value when an attribute is 'xsi:nil' and has a value of 'true'.";

	// Token: 0x04000261 RID: 609
	public const string Sch_MixSchemaTypes = "Different schema types cannot be mixed.";

	// Token: 0x04000262 RID: 610
	public const string Sch_XSDSchemaOnly = "'XmlSchemaSet' can load only W3C XML Schemas.";

	// Token: 0x04000263 RID: 611
	public const string Sch_InvalidPublicAttribute = "Public attribute '{0}' is an invalid URI.";

	// Token: 0x04000264 RID: 612
	public const string Sch_InvalidSystemAttribute = "System attribute '{0}' is an invalid URI.";

	// Token: 0x04000265 RID: 613
	public const string Sch_TypeAfterConstraints = "'simpleType' or 'complexType' cannot follow 'unique', 'key' or 'keyref'.";

	// Token: 0x04000266 RID: 614
	public const string Sch_XsiNilAndType = "There can be no type value when attribute is 'xsi:nil' and has value 'true'.";

	// Token: 0x04000267 RID: 615
	public const string Sch_DupSimpleTypeChild = "'simpleType' should have only one child 'union', 'list', or 'restriction'.";

	// Token: 0x04000268 RID: 616
	public const string Sch_InvalidIdAttribute = "Invalid 'id' attribute value: {0}";

	// Token: 0x04000269 RID: 617
	public const string Sch_InvalidNameAttributeEx = "Invalid 'name' attribute value '{0}': '{1}'.";

	// Token: 0x0400026A RID: 618
	public const string Sch_InvalidAttribute = "Invalid '{0}' attribute: '{1}'.";

	// Token: 0x0400026B RID: 619
	public const string Sch_EmptyChoice = "Empty choice cannot be satisfied if 'minOccurs' is not equal to 0.";

	// Token: 0x0400026C RID: 620
	public const string Sch_DerivedNotFromBase = "The data type of the simple content is not a valid restriction of the base complex type.";

	// Token: 0x0400026D RID: 621
	public const string Sch_NeedSimpleTypeChild = "Simple content restriction must have a simple type child if the content type of the base type is not a simple type definition.";

	// Token: 0x0400026E RID: 622
	public const string Sch_InvalidCollection = "The schema items collection cannot contain an object of type 'XmlSchemaInclude', 'XmlSchemaImport', or 'XmlSchemaRedefine'.";

	// Token: 0x0400026F RID: 623
	public const string Sch_UnrefNS = "Namespace '{0}' is not available to be referenced in this schema.";

	// Token: 0x04000270 RID: 624
	public const string Sch_InvalidSimpleTypeRestriction = "Restriction of 'anySimpleType' is not allowed.";

	// Token: 0x04000271 RID: 625
	public const string Sch_MultipleRedefine = "Multiple redefines of the same schema will be ignored.";

	// Token: 0x04000272 RID: 626
	public const string Sch_NullValue = "Value cannot be null.";

	// Token: 0x04000273 RID: 627
	public const string Sch_ComplexContentModel = "Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.";

	// Token: 0x04000274 RID: 628
	public const string Sch_SchemaNotPreprocessed = "All schemas in the set should be successfully preprocessed prior to compilation.";

	// Token: 0x04000275 RID: 629
	public const string Sch_SchemaNotRemoved = "The schema could not be removed because other schemas in the set have dependencies on this schema or its imports.";

	// Token: 0x04000276 RID: 630
	public const string Sch_ComponentAlreadySeenForNS = "An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.";

	// Token: 0x04000277 RID: 631
	public const string Sch_DefaultAttributeNotApplied = "Default attribute '{0}' for element '{1}' could not be applied as the attribute namespace is not mapped to a prefix in the instance document.";

	// Token: 0x04000278 RID: 632
	public const string Sch_NotXsiAttribute = "The attribute '{0}' does not match one of the four allowed attributes in the 'xsi' namespace.";

	// Token: 0x04000279 RID: 633
	public const string Sch_SchemaDoesNotExist = "Schema does not exist in the set.";

	// Token: 0x0400027A RID: 634
	public const string XmlDocument_ValidateInvalidNodeType = "Validate method can be called only on nodes of type Document, DocumentFragment, Element, or Attribute.";

	// Token: 0x0400027B RID: 635
	public const string XmlDocument_NodeNotFromDocument = "Cannot validate '{0}' because its owner document is not the current document. ";

	// Token: 0x0400027C RID: 636
	public const string XmlDocument_NoNodeSchemaInfo = "Schema information could not be found for the node passed into Validate. The node may be invalid in its current position. Navigate to the ancestor that has schema information, then call Validate again.";

	// Token: 0x0400027D RID: 637
	public const string XmlDocument_NoSchemaInfo = "The XmlSchemaSet on the document is either null or has no schemas in it. Provide schema information before calling Validate.";

	// Token: 0x0400027E RID: 638
	public const string Sch_InvalidStartTransition = "It is invalid to call the '{0}' method in the current state of the validator. The '{1}' method must be called before proceeding with validation.";

	// Token: 0x0400027F RID: 639
	public const string Sch_InvalidStateTransition = "The transition from the '{0}' method to the '{1}' method is not allowed.";

	// Token: 0x04000280 RID: 640
	public const string Sch_InvalidEndValidation = "The 'EndValidation' method cannot not be called when all the elements have not been validated. 'ValidateEndElement' calls corresponding to 'ValidateElement' calls might be missing.";

	// Token: 0x04000281 RID: 641
	public const string Sch_InvalidEndElementCall = "It is invalid to call the 'ValidateEndElement' overload that takes in a 'typedValue' after 'ValidateText' or 'ValidateWhitespace' methods have been called.";

	// Token: 0x04000282 RID: 642
	public const string Sch_InvalidEndElementCallTyped = "It is invalid to call the 'ValidateEndElement' overload that takes in a 'typedValue' for elements with complex content.";

	// Token: 0x04000283 RID: 643
	public const string Sch_InvalidEndElementMultiple = "The call to the '{0}' method does not match a corresponding call to 'ValidateElement' method.";

	// Token: 0x04000284 RID: 644
	public const string Sch_DuplicateAttribute = "The '{0}' attribute has already been validated and is a duplicate attribute.";

	// Token: 0x04000285 RID: 645
	public const string Sch_InvalidPartialValidationType = "The partial validation type has to be 'XmlSchemaElement', 'XmlSchemaAttribute', or 'XmlSchemaType'.";

	// Token: 0x04000286 RID: 646
	public const string Sch_SchemaElementNameMismatch = "The element name '{0}' does not match the name '{1}' of the 'XmlSchemaElement' set as a partial validation type. ";

	// Token: 0x04000287 RID: 647
	public const string Sch_SchemaAttributeNameMismatch = "The attribute name '{0}' does not match the name '{1}' of the 'XmlSchemaAttribute' set as a partial validation type. ";

	// Token: 0x04000288 RID: 648
	public const string Sch_ValidateAttributeInvalidCall = "If the partial validation type is 'XmlSchemaElement' or 'XmlSchemaType', the 'ValidateAttribute' method cannot be called.";

	// Token: 0x04000289 RID: 649
	public const string Sch_ValidateElementInvalidCall = "If the partial validation type is 'XmlSchemaAttribute', the 'ValidateElement' method cannot be called.";

	// Token: 0x0400028A RID: 650
	public const string Sch_EnumNotStarted = "Enumeration has not started. Call MoveNext.";

	// Token: 0x0400028B RID: 651
	public const string Sch_EnumFinished = "Enumeration has already finished.";

	// Token: 0x0400028C RID: 652
	public const string SchInf_schema = "The supplied xml instance is a schema or contains an inline schema. This class cannot infer a schema for a schema.";

	// Token: 0x0400028D RID: 653
	public const string SchInf_entity = "Inference cannot handle entity references. Pass in an 'XmlReader' that expands entities.";

	// Token: 0x0400028E RID: 654
	public const string SchInf_simplecontent = "Expected simple content. Schema was not created using this tool.";

	// Token: 0x0400028F RID: 655
	public const string SchInf_extension = "Expected 'Extension' within 'SimpleContent'. Schema was not created using this tool.";

	// Token: 0x04000290 RID: 656
	public const string SchInf_particle = "Particle cannot exist along with 'ContentModel'.";

	// Token: 0x04000291 RID: 657
	public const string SchInf_ct = "Complex type expected to exist with at least one 'Element' at this point.";

	// Token: 0x04000292 RID: 658
	public const string SchInf_seq = "sequence expected to contain elements only. Schema was not created using this tool.";

	// Token: 0x04000293 RID: 659
	public const string SchInf_noseq = "The supplied schema contains particles other than Sequence and Choice. Only schemas generated by this tool are supported.";

	// Token: 0x04000294 RID: 660
	public const string SchInf_noct = "Expected ComplexType. Schema was not generated using this tool.";

	// Token: 0x04000295 RID: 661
	public const string SchInf_UnknownParticle = "Expected Element. Schema was not generated using this tool.";

	// Token: 0x04000296 RID: 662
	public const string SchInf_schematype = "Inference can only handle simple built-in types for 'SchemaType'.";

	// Token: 0x04000297 RID: 663
	public const string SchInf_NoElement = "There is no element to infer schema.";

	// Token: 0x04000298 RID: 664
	public const string Xp_UnclosedString = "This is an unclosed string.";

	// Token: 0x04000299 RID: 665
	public const string Xp_ExprExpected = "'{0}' is an invalid expression.";

	// Token: 0x0400029A RID: 666
	public const string Xp_InvalidArgumentType = "The argument to function '{0}' in '{1}' cannot be converted to a node-set.";

	// Token: 0x0400029B RID: 667
	public const string Xp_InvalidNumArgs = "Function '{0}' in '{1}' has an invalid number of arguments.";

	// Token: 0x0400029C RID: 668
	public const string Xp_InvalidName = "'{0}' has an invalid qualified name.";

	// Token: 0x0400029D RID: 669
	public const string Xp_InvalidToken = "'{0}' has an invalid token.";

	// Token: 0x0400029E RID: 670
	public const string Xp_NodeSetExpected = "Expression must evaluate to a node-set.";

	// Token: 0x0400029F RID: 671
	public const string Xp_NotSupported = "The XPath query '{0}' is not supported.";

	// Token: 0x040002A0 RID: 672
	public const string Xp_InvalidPattern = "'{0}' is an invalid XSLT pattern.";

	// Token: 0x040002A1 RID: 673
	public const string Xp_InvalidKeyPattern = "'{0}' is an invalid key pattern. It either contains a variable reference or 'key()' function.";

	// Token: 0x040002A2 RID: 674
	public const string Xp_BadQueryObject = "This is an invalid object. Only objects returned from Compile() can be passed as input.";

	// Token: 0x040002A3 RID: 675
	public const string Xp_UndefinedXsltContext = "XsltContext is needed for this query because of an unknown function.";

	// Token: 0x040002A4 RID: 676
	public const string Xp_NoContext = "Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.";

	// Token: 0x040002A5 RID: 677
	public const string Xp_UndefVar = "The variable '{0}' is undefined.";

	// Token: 0x040002A6 RID: 678
	public const string Xp_UndefFunc = "The function '{0}()' is undefined.";

	// Token: 0x040002A7 RID: 679
	public const string Xp_FunctionFailed = "Function '{0}()' has failed.";

	// Token: 0x040002A8 RID: 680
	public const string Xp_CurrentNotAllowed = "The 'current()' function cannot be used in a pattern.";

	// Token: 0x040002A9 RID: 681
	public const string Xp_QueryTooComplex = "The xpath query is too complex.";

	// Token: 0x040002AA RID: 682
	public const string Xdom_DualDocumentTypeNode = "This document already has a 'DocumentType' node.";

	// Token: 0x040002AB RID: 683
	public const string Xdom_DualDocumentElementNode = "This document already has a 'DocumentElement' node.";

	// Token: 0x040002AC RID: 684
	public const string Xdom_DualDeclarationNode = "This document already has an 'XmlDeclaration' node.";

	// Token: 0x040002AD RID: 685
	public const string Xdom_Import = "Cannot import nodes of type '{0}'.";

	// Token: 0x040002AE RID: 686
	public const string Xdom_Import_NullNode = "Cannot import a null node.";

	// Token: 0x040002AF RID: 687
	public const string Xdom_NoRootEle = "The document does not have a root element.";

	// Token: 0x040002B0 RID: 688
	public const string Xdom_Attr_Name = "The attribute local name cannot be empty.";

	// Token: 0x040002B1 RID: 689
	public const string Xdom_AttrCol_Object = "An 'Attributes' collection can only contain 'Attribute' objects.";

	// Token: 0x040002B2 RID: 690
	public const string Xdom_AttrCol_Insert = "The reference node must be a child of the current node.";

	// Token: 0x040002B3 RID: 691
	public const string Xdom_NamedNode_Context = "The named node is from a different document context.";

	// Token: 0x040002B4 RID: 692
	public const string Xdom_Version = "Wrong XML version information. The XML must match production \"VersionNum ::= '1.' [0-9]+\".";

	// Token: 0x040002B5 RID: 693
	public const string Xdom_standalone = "Wrong value for the XML declaration standalone attribute of '{0}'.";

	// Token: 0x040002B6 RID: 694
	public const string Xdom_Ent_Innertext = "The 'InnerText' of an 'Entity' node is read-only and cannot be set.";

	// Token: 0x040002B7 RID: 695
	public const string Xdom_EntRef_SetVal = "'EntityReference' nodes have no support for setting value.";

	// Token: 0x040002B8 RID: 696
	public const string Xdom_WS_Char = "The string for whitespace contains an invalid character.";

	// Token: 0x040002B9 RID: 697
	public const string Xdom_Node_SetVal = "Cannot set a value on node type '{0}'.";

	// Token: 0x040002BA RID: 698
	public const string Xdom_Empty_LocalName = "The local name for elements or attributes cannot be null or an empty string.";

	// Token: 0x040002BB RID: 699
	public const string Xdom_Set_InnerXml = "Cannot set the 'InnerXml' for the current node because it is either read-only or cannot have children.";

	// Token: 0x040002BC RID: 700
	public const string Xdom_Attr_InUse = "The 'Attribute' node cannot be inserted because it is already an attribute of another element.";

	// Token: 0x040002BD RID: 701
	public const string Xdom_Enum_ElementList = "The element list has changed. The enumeration operation failed to continue.";

	// Token: 0x040002BE RID: 702
	public const string Xdom_Invalid_NT_String = "'{0}' does not represent any 'XmlNodeType'.";

	// Token: 0x040002BF RID: 703
	public const string Xdom_InvalidCharacter_EntityReference = "Cannot create an 'EntityReference' node with a name starting with '#'.";

	// Token: 0x040002C0 RID: 704
	public const string Xdom_IndexOutOfRange = "The index being passed in is out of range.";

	// Token: 0x040002C1 RID: 705
	public const string Xdom_Document_Innertext = "The 'InnerText' of a 'Document' node is read-only and cannot be set.";

	// Token: 0x040002C2 RID: 706
	public const string Xpn_BadPosition = "Operation is not valid due to the current position of the navigator.";

	// Token: 0x040002C3 RID: 707
	public const string Xpn_MissingParent = "The current position of the navigator is missing a valid parent.";

	// Token: 0x040002C4 RID: 708
	public const string Xpn_NoContent = "No content generated as the result of the operation.";

	// Token: 0x040002C5 RID: 709
	public const string Xdom_Load_NoDocument = "The document to be loaded could not be found.";

	// Token: 0x040002C6 RID: 710
	public const string Xdom_Load_NoReader = "There is no reader from which to load the document.";

	// Token: 0x040002C7 RID: 711
	public const string Xdom_Node_Null_Doc = "Cannot create a node without an owner document.";

	// Token: 0x040002C8 RID: 712
	public const string Xdom_Node_Insert_Child = "Cannot insert a node or any ancestor of that node as a child of itself.";

	// Token: 0x040002C9 RID: 713
	public const string Xdom_Node_Insert_Contain = "The current node cannot contain other nodes.";

	// Token: 0x040002CA RID: 714
	public const string Xdom_Node_Insert_Path = "The reference node is not a child of this node.";

	// Token: 0x040002CB RID: 715
	public const string Xdom_Node_Insert_Context = "The node to be inserted is from a different document context.";

	// Token: 0x040002CC RID: 716
	public const string Xdom_Node_Insert_Location = "Cannot insert the node in the specified location.";

	// Token: 0x040002CD RID: 717
	public const string Xdom_Node_Insert_TypeConflict = "The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type.";

	// Token: 0x040002CE RID: 718
	public const string Xdom_Node_Remove_Contain = "The current node cannot contain other nodes, so the node to be removed is not its child.";

	// Token: 0x040002CF RID: 719
	public const string Xdom_Node_Remove_Child = "The node to be removed is not a child of this node.";

	// Token: 0x040002D0 RID: 720
	public const string Xdom_Node_Modify_ReadOnly = "This node is read-only. It cannot be modified.";

	// Token: 0x040002D1 RID: 721
	public const string Xdom_TextNode_SplitText = "The 'Text' node is not connected in the DOM live tree. No 'SplitText' operation could be performed.";

	// Token: 0x040002D2 RID: 722
	public const string Xdom_Attr_Reserved_XmlNS = "The namespace declaration attribute has an incorrect 'namespaceURI': '{0}'.";

	// Token: 0x040002D3 RID: 723
	public const string Xdom_Node_Cloning = "'Entity' and 'Notation' nodes cannot be cloned.";

	// Token: 0x040002D4 RID: 724
	public const string Xnr_ResolveEntity = "The node is not an expandable 'EntityReference' node.";

	// Token: 0x040002D5 RID: 725
	public const string XPathDocument_MissingSchemas = "An XmlSchemaSet must be provided to validate the document.";

	// Token: 0x040002D6 RID: 726
	public const string XPathDocument_NotEnoughSchemaInfo = "Element should have prior schema information to call this method.";

	// Token: 0x040002D7 RID: 727
	public const string XPathDocument_ValidateInvalidNodeType = "Validate and CheckValidity are only allowed on Root or Element nodes.";

	// Token: 0x040002D8 RID: 728
	public const string XPathDocument_SchemaSetNotAllowed = "An XmlSchemaSet is only allowed as a parameter on the Root node.";

	// Token: 0x040002D9 RID: 729
	public const string XmlBin_MissingEndCDATA = "CDATA end token is missing.";

	// Token: 0x040002DA RID: 730
	public const string XmlBin_InvalidQNameID = "Invalid QName ID.";

	// Token: 0x040002DB RID: 731
	public const string XmlBinary_UnexpectedToken = "Unexpected BinaryXml token.";

	// Token: 0x040002DC RID: 732
	public const string XmlBinary_InvalidSqlDecimal = "Unable to parse data as SQL_DECIMAL.";

	// Token: 0x040002DD RID: 733
	public const string XmlBinary_InvalidSignature = "Invalid BinaryXml signature.";

	// Token: 0x040002DE RID: 734
	public const string XmlBinary_InvalidProtocolVersion = "Invalid BinaryXml protocol version.";

	// Token: 0x040002DF RID: 735
	public const string XmlBinary_UnsupportedCodePage = "Unsupported BinaryXml codepage.";

	// Token: 0x040002E0 RID: 736
	public const string XmlBinary_InvalidStandalone = "Invalid BinaryXml standalone token.";

	// Token: 0x040002E1 RID: 737
	public const string XmlBinary_NoParserContext = "BinaryXml Parser does not support initialization with XmlParserContext.";

	// Token: 0x040002E2 RID: 738
	public const string XmlBinary_ListsOfValuesNotSupported = "Lists of BinaryXml value tokens not supported.";

	// Token: 0x040002E3 RID: 739
	public const string XmlBinary_CastNotSupported = "Token '{0}' does not support a conversion to Clr type '{1}'.";

	// Token: 0x040002E4 RID: 740
	public const string XmlBinary_NoRemapPrefix = "Prefix '{0}' is already assigned to namespace '{1}' and cannot be reassigned to '{2}' on this tag.";

	// Token: 0x040002E5 RID: 741
	public const string XmlBinary_AttrWithNsNoPrefix = "Attribute '{0}' has namespace '{1}' but no prefix.";

	// Token: 0x040002E6 RID: 742
	public const string XmlBinary_ValueTooBig = "The value is too big to fit into an Int32. The arithmetic operation resulted in an overflow.";

	// Token: 0x040002E7 RID: 743
	public const string SqlTypes_ArithOverflow = "Arithmetic Overflow.";

	// Token: 0x040002E8 RID: 744
	public const string XmlMissingType = "Invalid serialization assembly: Required type {0} cannot be found in the generated assembly '{1}'.";

	// Token: 0x040002E9 RID: 745
	public const string XmlSerializerUnsupportedType = "{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.";

	// Token: 0x040002EA RID: 746
	public const string XmlSerializerUnsupportedMember = "Cannot serialize member '{0}' of type '{1}', see inner exception for more details.";

	// Token: 0x040002EB RID: 747
	public const string XmlUnsupportedTypeKind = "The type {0} may not be serialized.";

	// Token: 0x040002EC RID: 748
	public const string XmlUnsupportedSoapTypeKind = "The type {0} may not be serialized with SOAP-encoded messages. Set the Use for your message to Literal.";

	// Token: 0x040002ED RID: 749
	public const string XmlUnsupportedIDictionary = "The type {0} is not supported because it implements IDictionary.";

	// Token: 0x040002EE RID: 750
	public const string XmlUnsupportedIDictionaryDetails = "Cannot serialize member {0} of type {1}, because it implements IDictionary.";

	// Token: 0x040002EF RID: 751
	public const string XmlDuplicateTypeName = "A type with the name {0} has already been added in namespace {1}.";

	// Token: 0x040002F0 RID: 752
	public const string XmlSerializableNameMissing1 = "Schema Id is missing. The schema returned from {0}.GetSchema() must have an Id.";

	// Token: 0x040002F1 RID: 753
	public const string XmlConstructorInaccessible = "{0} cannot be serialized because it does not have a parameterless constructor.";

	// Token: 0x040002F2 RID: 754
	public const string XmlTypeInaccessible = "{0} is inaccessible due to its protection level. Only public types can be processed.";

	// Token: 0x040002F3 RID: 755
	public const string XmlTypeStatic = "{0} cannot be serialized. Static types cannot be used as parameters or return types.";

	// Token: 0x040002F4 RID: 756
	public const string XmlNoDefaultAccessors = "You must implement a default accessor on {0} because it inherits from ICollection.";

	// Token: 0x040002F5 RID: 757
	public const string XmlNoAddMethod = "To be XML serializable, types which inherit from {2} must have an implementation of Add({1}) at all levels of their inheritance hierarchy. {0} does not implement Add({1}).";

	// Token: 0x040002F6 RID: 758
	public const string XmlReadOnlyPropertyError = "Cannot deserialize type '{0}' because it contains property '{1}' which has no public setter.";

	// Token: 0x040002F7 RID: 759
	public const string XmlAttributeSetAgain = "'{0}.{1}' already has attributes.";

	// Token: 0x040002F8 RID: 760
	public const string XmlIllegalWildcard = "Cannot use wildcards at the top level of a schema.";

	// Token: 0x040002F9 RID: 761
	public const string XmlIllegalArrayElement = "An element declared at the top level of a schema cannot have maxOccurs > 1. Provide a wrapper element for '{0}' by using XmlArray or XmlArrayItem instead of XmlElementAttribute, or by using the Wrapped parameter style.";

	// Token: 0x040002FA RID: 762
	public const string XmlIllegalForm = "There was an error exporting '{0}': elements declared at the top level of a schema cannot be unqualified.";

	// Token: 0x040002FB RID: 763
	public const string XmlBareTextMember = "There was an error exporting '{0}': bare members cannot contain text content.";

	// Token: 0x040002FC RID: 764
	public const string XmlBareAttributeMember = "There was an error exporting '{0}': bare members cannot be attributes.";

	// Token: 0x040002FD RID: 765
	public const string XmlReflectionError = "There was an error reflecting '{0}'.";

	// Token: 0x040002FE RID: 766
	public const string XmlTypeReflectionError = "There was an error reflecting type '{0}'.";

	// Token: 0x040002FF RID: 767
	public const string XmlPropertyReflectionError = "There was an error reflecting property '{0}'.";

	// Token: 0x04000300 RID: 768
	public const string XmlFieldReflectionError = "There was an error reflecting field '{0}'.";

	// Token: 0x04000301 RID: 769
	public const string XmlInvalidDataTypeUsage = "'{0}' is an invalid value for the {1} property. The property may only be specified for primitive types.";

	// Token: 0x04000302 RID: 770
	public const string XmlInvalidXsdDataType = "Value '{0}' cannot be used for the {1} property. The datatype '{2}' is missing.";

	// Token: 0x04000303 RID: 771
	public const string XmlDataTypeMismatch = "'{0}' is an invalid value for the {1} property. {0} cannot be converted to {2}.";

	// Token: 0x04000304 RID: 772
	public const string XmlIllegalTypeContext = "{0} cannot be used as: 'xml {1}'.";

	// Token: 0x04000305 RID: 773
	public const string XmlUdeclaredXsdType = "The type, {0}, is undeclared.";

	// Token: 0x04000306 RID: 774
	public const string XmlInvalidConstantAttribute = "Only XmlEnum may be used on enumerated constants.";

	// Token: 0x04000307 RID: 775
	public const string XmlIllegalAttributesArrayAttribute = "XmlAttribute and XmlAnyAttribute cannot be used in conjunction with XmlElement, XmlText, XmlAnyElement, XmlArray, or XmlArrayItem.";

	// Token: 0x04000308 RID: 776
	public const string XmlIllegalElementsArrayAttribute = "XmlElement, XmlText, and XmlAnyElement cannot be used in conjunction with XmlAttribute, XmlAnyAttribute, XmlArray, or XmlArrayItem.";

	// Token: 0x04000309 RID: 777
	public const string XmlIllegalArrayArrayAttribute = "XmlArray and XmlArrayItem cannot be used in conjunction with XmlAttribute, XmlAnyAttribute, XmlElement, XmlText, or XmlAnyElement.";

	// Token: 0x0400030A RID: 778
	public const string XmlIllegalAttribute = "For non-array types, you may use the following attributes: XmlAttribute, XmlText, XmlElement, or XmlAnyElement.";

	// Token: 0x0400030B RID: 779
	public const string XmlIllegalType = "The type for {0} may not be specified for primitive types.";

	// Token: 0x0400030C RID: 780
	public const string XmlIllegalAttrOrText = "Cannot serialize member '{0}' of type {1}. XmlAttribute/XmlText cannot be used to encode complex types.";

	// Token: 0x0400030D RID: 781
	public const string XmlIllegalSoapAttribute = "Cannot serialize member '{0}' of type {1}. SoapAttribute cannot be used to encode complex types.";

	// Token: 0x0400030E RID: 782
	public const string XmlIllegalAttrOrTextInterface = "Cannot serialize member '{0}' of type {1}. XmlAttribute/XmlText cannot be used to encode types implementing {2}.";

	// Token: 0x0400030F RID: 783
	public const string XmlIllegalAttributeFlagsArray = "XmlAttribute cannot be used to encode array of {1}, because it is marked with FlagsAttribute.";

	// Token: 0x04000310 RID: 784
	public const string XmlIllegalAnyElement = "Cannot serialize member of type {0}: XmlAnyElement can only be used with classes of type XmlNode or a type deriving from XmlNode.";

	// Token: 0x04000311 RID: 785
	public const string XmlInvalidIsNullable = "IsNullable may not be 'true' for value type {0}.  Please consider using Nullable<{0}> instead.";

	// Token: 0x04000312 RID: 786
	public const string XmlInvalidNotNullable = "IsNullable may not be set to 'false' for a Nullable<{0}> type. Consider using '{0}' type or removing the IsNullable property from the {1} attribute.";

	// Token: 0x04000313 RID: 787
	public const string XmlInvalidFormUnqualified = "The Form property may not be 'Unqualified' when an explicit Namespace property is present.";

	// Token: 0x04000314 RID: 788
	public const string XmlDuplicateNamespace = "The namespace, {0}, is a duplicate.";

	// Token: 0x04000315 RID: 789
	public const string XmlElementHasNoName = "This element has no name. Please review schema type '{0}' from namespace '{1}'.";

	// Token: 0x04000316 RID: 790
	public const string XmlAttributeHasNoName = "This attribute has no name.";

	// Token: 0x04000317 RID: 791
	public const string XmlElementImportedTwice = "The element, {0}, from namespace, {1}, was imported in two different contexts: ({2}, {3}).";

	// Token: 0x04000318 RID: 792
	public const string XmlHiddenMember = "Member {0}.{1} of type {2} hides base class member {3}.{4} of type {5}. Use XmlElementAttribute or XmlAttributeAttribute to specify a new name.";

	// Token: 0x04000319 RID: 793
	public const string XmlInvalidXmlOverride = "Member '{0}.{1}' hides inherited member '{2}.{3}', but has different custom attributes.";

	// Token: 0x0400031A RID: 794
	public const string XmlMembersDeriveError = "These members may not be derived.";

	// Token: 0x0400031B RID: 795
	public const string XmlTypeUsedTwice = "The type '{0}' from namespace '{1}' was used in two different ways.";

	// Token: 0x0400031C RID: 796
	public const string XmlMissingGroup = "Group {0} is missing.";

	// Token: 0x0400031D RID: 797
	public const string XmlMissingAttributeGroup = "The attribute group {0} is missing.";

	// Token: 0x0400031E RID: 798
	public const string XmlMissingDataType = "The datatype '{0}' is missing.";

	// Token: 0x0400031F RID: 799
	public const string XmlInvalidEncoding = "Referenced type '{0}' is only valid for encoded SOAP.";

	// Token: 0x04000320 RID: 800
	public const string XmlMissingElement = "The element '{0}' is missing.";

	// Token: 0x04000321 RID: 801
	public const string XmlMissingAttribute = "The attribute {0} is missing.";

	// Token: 0x04000322 RID: 802
	public const string XmlMissingMethodEnum = "The method for enum {0} is missing.";

	// Token: 0x04000323 RID: 803
	public const string XmlNoAttributeHere = "Cannot write a node of type XmlAttribute as an element value. Use XmlAnyAttributeAttribute with an array of XmlNode or XmlAttribute to write the node as an attribute.";

	// Token: 0x04000324 RID: 804
	public const string XmlNeedAttributeHere = "The node must be either type XmlAttribute or a derived type.";

	// Token: 0x04000325 RID: 805
	public const string XmlElementNameMismatch = "This element was named '{0}' from namespace '{1}' but should have been named '{2}' from namespace '{3}'.";

	// Token: 0x04000326 RID: 806
	public const string XmlUnsupportedDefaultType = "The default value type, {0}, is unsupported.";

	// Token: 0x04000327 RID: 807
	public const string XmlUnsupportedDefaultValue = "The formatter {0} cannot be used for default values.";

	// Token: 0x04000328 RID: 808
	public const string XmlInvalidDefaultValue = "Value '{0}' cannot be converted to {1}.";

	// Token: 0x04000329 RID: 809
	public const string XmlInvalidDefaultEnumValue = "Enum {0} cannot be converted to {1}.";

	// Token: 0x0400032A RID: 810
	public const string XmlUnknownNode = "{0} was not expected.";

	// Token: 0x0400032B RID: 811
	public const string XmlUnknownConstant = "Instance validation error: '{0}' is not a valid value for {1}.";

	// Token: 0x0400032C RID: 812
	public const string XmlSerializeError = "There is an error in the XML document.";

	// Token: 0x0400032D RID: 813
	public const string XmlSerializeErrorDetails = "There is an error in XML document ({0}, {1}).";

	// Token: 0x0400032E RID: 814
	public const string XmlSchemaDuplicateNamespace = "There are more then one schema with targetNamespace='{0}'.";

	// Token: 0x0400032F RID: 815
	public const string XmlSchemaCompiled = "Cannot add schema to compiled schemas collection.";

	// Token: 0x04000330 RID: 816
	public const string XmlInvalidArrayDimentions = "SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.";

	// Token: 0x04000331 RID: 817
	public const string XmlInvalidArrayTypeName = "The SOAP-ENC:arrayType references type is named '{0}'; a type named '{1}' was expected at {2}.";

	// Token: 0x04000332 RID: 818
	public const string XmlInvalidArrayTypeNamespace = "The SOAP-ENC:arrayType references type is from namespace '{0}'; the namespace '{1}' was expected at {2}.";

	// Token: 0x04000333 RID: 819
	public const string XmlMissingArrayType = "SOAP-ENC:arrayType was missing at {0}.";

	// Token: 0x04000334 RID: 820
	public const string XmlEmptyArrayType = "SOAP-ENC:arrayType was empty at {0}.";

	// Token: 0x04000335 RID: 821
	public const string XmlInvalidArraySyntax = "SOAP-ENC:arrayType must end with a ']' character.";

	// Token: 0x04000336 RID: 822
	public const string XmlInvalidArrayTypeSyntax = "Invalid wsd:arrayType syntax: '{0}'.";

	// Token: 0x04000337 RID: 823
	public const string XmlMismatchedArrayBrackets = "SOAP-ENC:arrayType has mismatched brackets.";

	// Token: 0x04000338 RID: 824
	public const string XmlInvalidArrayLength = "SOAP-ENC:arrayType could not handle '{1}' as the length of the array.";

	// Token: 0x04000339 RID: 825
	public const string XmlMissingHref = "The referenced element with ID '{0}' is located outside the current document and cannot be retrieved.";

	// Token: 0x0400033A RID: 826
	public const string XmlInvalidHref = "The referenced element with ID '{0}' was not found in the document.";

	// Token: 0x0400033B RID: 827
	public const string XmlUnknownType = "The specified type was not recognized: name='{0}', namespace='{1}', at {2}.";

	// Token: 0x0400033C RID: 828
	public const string XmlAbstractType = "The specified type is abstract: name='{0}', namespace='{1}', at {2}.";

	// Token: 0x0400033D RID: 829
	public const string XmlMappingsScopeMismatch = "Exported mappings must come from the same importer.";

	// Token: 0x0400033E RID: 830
	public const string XmlMethodTypeNameConflict = "The XML element '{0}' from namespace '{1}' references a method and a type. Change the method's message name using WebMethodAttribute or change the type's root element using the XmlRootAttribute.";

	// Token: 0x0400033F RID: 831
	public const string XmlCannotReconcileAccessor = "The top XML element '{0}' from namespace '{1}' references distinct types {2} and {3}. Use XML attributes to specify another XML name or namespace for the element or types.";

	// Token: 0x04000340 RID: 832
	public const string XmlCannotReconcileAttributeAccessor = "The global XML attribute '{0}' from namespace '{1}' references distinct types {2} and {3}. Use XML attributes to specify another XML name or namespace for the attribute or types.";

	// Token: 0x04000341 RID: 833
	public const string XmlCannotReconcileAccessorDefault = "The global XML item '{0}' from namespace '{1}' has mismatch default value attributes: '{2}' and '{3}' and cannot be mapped to the same schema item. Use XML attributes to specify another XML name or namespace for one of the items, or make sure that the default values match.";

	// Token: 0x04000342 RID: 834
	public const string XmlInvalidTypeAttributes = "XmlRoot and XmlType attributes may not be specified for the type {0}.";

	// Token: 0x04000343 RID: 835
	public const string XmlInvalidAttributeUse = "XML attributes may not be specified for the type {0}.";

	// Token: 0x04000344 RID: 836
	public const string XmlTypesDuplicate = "Types '{0}' and '{1}' both use the XML type name, '{2}', from namespace '{3}'. Use XML attributes to specify a unique XML name and/or namespace for the type.";

	// Token: 0x04000345 RID: 837
	public const string XmlInvalidSoapArray = "An array of type {0} may not be used with XmlArrayType.Soap.";

	// Token: 0x04000346 RID: 838
	public const string XmlCannotIncludeInSchema = "The type {0} may not be exported to a schema because the IncludeInSchema property of the XmlType attribute is 'false'.";

	// Token: 0x04000347 RID: 839
	public const string XmlInvalidSerializable = "The type {0} may not be used in this context. To use {0} as a parameter, return type, or member of a class or struct, the parameter, return type, or member must be declared as type {0} (it cannot be object). Objects of type {0} may not be used in un-typed collections, such as ArrayLists.";

	// Token: 0x04000348 RID: 840
	public const string XmlInvalidUseOfType = "The type {0} may not be used in this context.";

	// Token: 0x04000349 RID: 841
	public const string XmlUnxpectedType = "The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.";

	// Token: 0x0400034A RID: 842
	public const string XmlUnknownAnyElement = "The XML element '{0}' from namespace '{1}' was not expected. The XML element name and namespace must match those provided via XmlAnyElementAttribute(s).";

	// Token: 0x0400034B RID: 843
	public const string XmlMultipleAttributeOverrides = "{0}. {1} already has attributes.";

	// Token: 0x0400034C RID: 844
	public const string XmlInvalidEnumAttribute = "Only SoapEnum may be used on enum constants.";

	// Token: 0x0400034D RID: 845
	public const string XmlInvalidReturnPosition = "The return value must be the first member.";

	// Token: 0x0400034E RID: 846
	public const string XmlInvalidElementAttribute = "Only SoapElementAttribute or SoapAttributeAttribute may be used on members.";

	// Token: 0x0400034F RID: 847
	public const string XmlInvalidVoid = "The type Void is not valid in this context.";

	// Token: 0x04000350 RID: 848
	public const string XmlInvalidContent = "Invalid content {0}.";

	// Token: 0x04000351 RID: 849
	public const string XmlInvalidAttributeType = "{0} may not be used on parameters or return values when they are not wrapped.";

	// Token: 0x04000352 RID: 850
	public const string XmlInvalidBaseType = "Type {0} cannot derive from {1} because it already has base type {2}.";

	// Token: 0x04000353 RID: 851
	public const string XmlInvalidIdentifier = "Identifier '{0}' is not CLS-compliant.";

	// Token: 0x04000354 RID: 852
	public const string XmlGenError = "There was an error generating the XML document.";

	// Token: 0x04000355 RID: 853
	public const string XmlInvalidXmlns = "Invalid namespace attribute: xmlns:{0}=\"\".";

	// Token: 0x04000356 RID: 854
	public const string XmlCircularReference = "A circular reference was detected while serializing an object of type {0}.";

	// Token: 0x04000357 RID: 855
	public const string XmlCircularReference2 = "A circular type reference was detected in anonymous type '{0}'.  Please change '{0}' to be a named type by setting {1}={2} in the type definition.";

	// Token: 0x04000358 RID: 856
	public const string XmlAnonymousBaseType = "Illegal type derivation: Type '{0}' derives from anonymous type '{1}'. Please change '{1}' to be a named type by setting {2}={3} in the type definition.";

	// Token: 0x04000359 RID: 857
	public const string XmlMissingSchema = "Missing schema targetNamespace=\"{0}\".";

	// Token: 0x0400035A RID: 858
	public const string XmlNoSerializableMembers = "Cannot serialize object of type '{0}'. The object does not have serializable members.";

	// Token: 0x0400035B RID: 859
	public const string XmlIllegalOverride = "Error: Type '{0}' could not be imported because it redefines inherited member '{1}' with a different type. '{1}' is declared as type '{3}' on '{0}', but as type '{2}' on base class '{4}'.";

	// Token: 0x0400035C RID: 860
	public const string XmlReadOnlyCollection = "Could not deserialize {0}. Parameterless constructor is required for collections and enumerators.";

	// Token: 0x0400035D RID: 861
	public const string XmlRpcNestedValueType = "Cannot serialize {0}. Nested structs are not supported with encoded SOAP.";

	// Token: 0x0400035E RID: 862
	public const string XmlRpcRefsInValueType = "Cannot serialize {0}. References in structs are not supported with encoded SOAP.";

	// Token: 0x0400035F RID: 863
	public const string XmlRpcArrayOfValueTypes = "Cannot serialize {0}. Arrays of structs are not supported with encoded SOAP.";

	// Token: 0x04000360 RID: 864
	public const string XmlDuplicateElementName = "The XML element '{0}' from namespace '{1}' is already present in the current scope. Use XML attributes to specify another XML name or namespace for the element.";

	// Token: 0x04000361 RID: 865
	public const string XmlDuplicateAttributeName = "The XML attribute '{0}' from namespace '{1}' is already present in the current scope. Use XML attributes to specify another XML name or namespace for the attribute.";

	// Token: 0x04000362 RID: 866
	public const string XmlBadBaseElement = "Element '{0}' from namespace '{1}' is not a complex type and cannot be used as a {2}.";

	// Token: 0x04000363 RID: 867
	public const string XmlBadBaseType = "Type '{0}' from namespace '{1}' is not a complex type and cannot be used as a {2}.";

	// Token: 0x04000364 RID: 868
	public const string XmlUndefinedAlias = "Namespace prefix '{0}' is not defined.";

	// Token: 0x04000365 RID: 869
	public const string XmlChoiceIdentifierType = "Type of choice identifier '{0}' is inconsistent with type of '{1}'. Please use {2}.";

	// Token: 0x04000366 RID: 870
	public const string XmlChoiceIdentifierArrayType = "Type of choice identifier '{0}' is inconsistent with type of '{1}'. Please use array of {2}.";

	// Token: 0x04000367 RID: 871
	public const string XmlChoiceIdentifierTypeEnum = "Choice identifier '{0}' must be an enum.";

	// Token: 0x04000368 RID: 872
	public const string XmlChoiceIdentiferMemberMissing = "Missing '{0}' member needed for serialization of choice '{1}'.";

	// Token: 0x04000369 RID: 873
	public const string XmlChoiceIdentiferAmbiguous = "Ambiguous choice identifier. There are several members named '{0}'.";

	// Token: 0x0400036A RID: 874
	public const string XmlChoiceIdentiferMissing = "You need to add {0} to the '{1}' member.";

	// Token: 0x0400036B RID: 875
	public const string XmlChoiceMissingValue = "Type {0} is missing enumeration value '{1}' for element '{2}' from namespace '{3}'.";

	// Token: 0x0400036C RID: 876
	public const string XmlChoiceMissingAnyValue = "Type {0} is missing enumeration value '##any:' corresponding to XmlAnyElementAttribute.";

	// Token: 0x0400036D RID: 877
	public const string XmlChoiceMismatchChoiceException = "Value of {0} mismatches the type of {1}; you need to set it to {2}.";

	// Token: 0x0400036E RID: 878
	public const string XmlArrayItemAmbiguousTypes = "Ambiguous types specified for member '{0}'.  Items '{1}' and '{2}' have the same type.  Please consider using {3} with {4} instead.";

	// Token: 0x0400036F RID: 879
	public const string XmlUnsupportedInterface = "Cannot serialize interface {0}.";

	// Token: 0x04000370 RID: 880
	public const string XmlUnsupportedInterfaceDetails = "Cannot serialize member {0} of type {1} because it is an interface.";

	// Token: 0x04000371 RID: 881
	public const string XmlUnsupportedRank = "Cannot serialize object of type {0}. Multidimensional arrays are not supported.";

	// Token: 0x04000372 RID: 882
	public const string XmlUnsupportedInheritance = "Using {0} as a base type for a class is not supported by XmlSerializer.";

	// Token: 0x04000373 RID: 883
	public const string XmlIllegalMultipleText = "Cannot serialize object of type '{0}' because it has multiple XmlText attributes. Consider using an array of strings with XmlTextAttribute for serialization of a mixed complex type.";

	// Token: 0x04000374 RID: 884
	public const string XmlIllegalMultipleTextMembers = "XmlText may not be used on multiple parameters or return values.";

	// Token: 0x04000375 RID: 885
	public const string XmlIllegalArrayTextAttribute = "Member '{0}' cannot be encoded using the XmlText attribute. You may use the XmlText attribute to encode primitives, enumerations, arrays of strings, or arrays of XmlNode.";

	// Token: 0x04000376 RID: 886
	public const string XmlIllegalTypedTextAttribute = "Cannot serialize object of type '{0}'. Consider changing type of XmlText member '{0}.{1}' from {2} to string or string array.";

	// Token: 0x04000377 RID: 887
	public const string XmlIllegalSimpleContentExtension = "Cannot serialize object of type '{0}'. Base type '{1}' has simpleContent and can only be extended by adding XmlAttribute elements. Please consider changing XmlText member of the base class to string array.";

	// Token: 0x04000378 RID: 888
	public const string XmlInvalidCast = "Cannot assign object of type {0} to an object of type {1}.";

	// Token: 0x04000379 RID: 889
	public const string XmlInvalidCastWithId = "Cannot assign object of type {0} to an object of type {1}. The error occurred while reading node with id='{2}'.";

	// Token: 0x0400037A RID: 890
	public const string XmlInvalidArrayRef = "Invalid reference id='{0}'. Object of type {1} cannot be stored in an array of this type. Details: array index={2}.";

	// Token: 0x0400037B RID: 891
	public const string XmlInvalidNullCast = "Cannot assign null value to an object of type {1}.";

	// Token: 0x0400037C RID: 892
	public const string XmlMultipleXmlns = "Cannot serialize object of type '{0}' because it has multiple XmlNamespaceDeclarations attributes.";

	// Token: 0x0400037D RID: 893
	public const string XmlMultipleXmlnsMembers = "XmlNamespaceDeclarations may not be used on multiple parameters or return values.";

	// Token: 0x0400037E RID: 894
	public const string XmlXmlnsInvalidType = "Cannot use XmlNamespaceDeclarations attribute on member '{0}' of type {1}.  This attribute is only valid on members of type {2}.";

	// Token: 0x0400037F RID: 895
	public const string XmlSoleXmlnsAttribute = "XmlNamespaceDeclarations attribute cannot be used in conjunction with any other custom attributes.";

	// Token: 0x04000380 RID: 896
	public const string XmlConstructorHasSecurityAttributes = "The type '{0}' cannot be serialized because its parameterless constructor is decorated with declarative security permission attributes. Consider using imperative asserts or demands in the constructor.";

	// Token: 0x04000381 RID: 897
	public const string XmlInvalidChoiceIdentifierValue = "Invalid or missing value of the choice identifier '{1}' of type '{0}[]'.";

	// Token: 0x04000382 RID: 898
	public const string XmlAnyElementDuplicate = "The element '{0}' has been attributed with duplicate XmlAnyElementAttribute(Name=\"{1}\", Namespace=\"{2}\").";

	// Token: 0x04000383 RID: 899
	public const string XmlChoiceIdDuplicate = "Enum values in the XmlChoiceIdentifier '{0}' have to be unique.  Value '{1}' already present.";

	// Token: 0x04000384 RID: 900
	public const string XmlChoiceIdentifierMismatch = "Value '{0}' of the choice identifier '{1}' does not match element '{2}' from namespace '{3}'.";

	// Token: 0x04000385 RID: 901
	public const string XmlUnsupportedRedefine = "Cannot import schema for type '{0}' from namespace '{1}'. Redefine not supported.";

	// Token: 0x04000386 RID: 902
	public const string XmlDuplicateElementInScope = "The XML element named '{0}' from namespace '{1}' is already present in the current scope.";

	// Token: 0x04000387 RID: 903
	public const string XmlDuplicateElementInScope1 = "The XML element named '{0}' from namespace '{1}' is already present in the current scope. Elements with the same name in the same scope must have the same type.";

	// Token: 0x04000388 RID: 904
	public const string XmlNoPartialTrust = "One or more assemblies referenced by the XmlSerializer cannot be called from partially trusted code.";

	// Token: 0x04000389 RID: 905
	public const string XmlInvalidEncodingNotEncoded1 = "The encoding style '{0}' is not valid for this call because this XmlSerializer instance does not support encoding. Use the SoapReflectionImporter to initialize an XmlSerializer that supports encoding.";

	// Token: 0x0400038A RID: 906
	public const string XmlInvalidEncoding3 = "The encoding style '{0}' is not valid for this call. Valid values are '{1}' for SOAP 1.1 encoding or '{2}' for SOAP 1.2 encoding.";

	// Token: 0x0400038B RID: 907
	public const string XmlInvalidSpecifiedType = "Member '{0}' of type {1} cannot be serialized.  Members with names ending on 'Specified' suffix have special meaning to the XmlSerializer: they control serialization of optional ValueType members and have to be of type {2}.";

	// Token: 0x0400038C RID: 908
	public const string XmlUnsupportedOpenGenericType = "Type {0} is not supported because it has unbound generic parameters.  Only instantiated generic types can be serialized.";

	// Token: 0x0400038D RID: 909
	public const string XmlMismatchSchemaObjects = "Warning: Cannot share {0} named '{1}' from '{2}' namespace. Several mismatched schema declarations were found.";

	// Token: 0x0400038E RID: 910
	public const string XmlCircularTypeReference = "Type '{0}' from targetNamespace='{1}' has invalid definition: Circular type reference.";

	// Token: 0x0400038F RID: 911
	public const string XmlCircularGroupReference = "Group '{0}' from targetNamespace='{1}' has invalid definition: Circular group reference.";

	// Token: 0x04000390 RID: 912
	public const string XmlRpcLitElementNamespace = "{0}='{1}' is not supported with rpc\\literal SOAP. The wrapper element has to be unqualified.";

	// Token: 0x04000391 RID: 913
	public const string XmlRpcLitElementNullable = "{0}='{1}' is not supported with rpc\\literal SOAP. The wrapper element cannot be nullable.";

	// Token: 0x04000392 RID: 914
	public const string XmlRpcLitElements = "Multiple accessors are not supported with rpc\\literal SOAP, you may use the following attributes: XmlArray, XmlArrayItem, or single XmlElement.";

	// Token: 0x04000393 RID: 915
	public const string XmlRpcLitArrayElement = "Input or output values of an rpc\\literal method cannot have maxOccurs > 1. Provide a wrapper element for '{0}' by using XmlArray or XmlArrayItem instead of XmlElement attribute.";

	// Token: 0x04000394 RID: 916
	public const string XmlRpcLitAttributeAttributes = "XmlAttribute and XmlAnyAttribute cannot be used with rpc\\literal SOAP, you may use the following attributes: XmlArray, XmlArrayItem, or single XmlElement.";

	// Token: 0x04000395 RID: 917
	public const string XmlRpcLitAttributes = "XmlText, XmlAnyElement, or XmlChoiceIdentifier cannot be used with rpc\\literal SOAP, you may use the following attributes: XmlArray, XmlArrayItem, or single XmlElement.";

	// Token: 0x04000396 RID: 918
	public const string XmlSequenceMembers = "Explicit sequencing may not be used on parameters or return values.  Please remove {0} property from custom attributes.";

	// Token: 0x04000397 RID: 919
	public const string XmlRpcLitXmlns = "Input or output values of an rpc\\literal method cannot have an XmlNamespaceDeclarations attribute (member '{0}').";

	// Token: 0x04000398 RID: 920
	public const string XmlDuplicateNs = "Illegal namespace declaration xmlns:{0}='{1}'. Namespace alias '{0}' already defined in the current scope.";

	// Token: 0x04000399 RID: 921
	public const string XmlAnonymousInclude = "Cannot include anonymous type '{0}'.";

	// Token: 0x0400039A RID: 922
	public const string XmlSchemaIncludeLocation = "Schema attribute schemaLocation='{1}' is not supported on objects of type {0}.  Please set {0}.Schema property.";

	// Token: 0x0400039B RID: 923
	public const string XmlSerializableSchemaError = "Schema type information provided by {0} is invalid: {1}";

	// Token: 0x0400039C RID: 924
	public const string XmlGetSchemaMethodName = "'{0}' is an invalid language identifier.";

	// Token: 0x0400039D RID: 925
	public const string XmlGetSchemaMethodMissing = "You must implement public static {0}({1}) method on {2}.";

	// Token: 0x0400039E RID: 926
	public const string XmlGetSchemaMethodReturnType = "Method {0}.{1}() specified by {2} has invalid signature: return type must be compatible with {3}.";

	// Token: 0x0400039F RID: 927
	public const string XmlGetSchemaEmptyTypeName = "{0}.{1}() must return a valid type name.";

	// Token: 0x040003A0 RID: 928
	public const string XmlGetSchemaTypeMissing = "{0}.{1}() must return a valid type name. Type '{2}' cannot be found in the targetNamespace='{3}'.";

	// Token: 0x040003A1 RID: 929
	public const string XmlGetSchemaInclude = "Multiple schemas with targetNamespace='{0}' returned by {1}.{2}().  Please use only the main (parent) schema, and add the others to the schema Includes.";

	// Token: 0x040003A2 RID: 930
	public const string XmlSerializableAttributes = "Only XmlRoot attribute may be specified for the type {0}. Please use {1} to specify schema type.";

	// Token: 0x040003A3 RID: 931
	public const string XmlSerializableMergeItem = "Cannot merge schemas with targetNamespace='{0}'. Several mismatched declarations were found: {1}";

	// Token: 0x040003A4 RID: 932
	public const string XmlSerializableBadDerivation = "Type '{0}' from namespace '{1}' declared as derivation of type '{2}' from namespace '{3}, but corresponding CLR types are not compatible.  Cannot convert type '{4}' to '{5}'.";

	// Token: 0x040003A5 RID: 933
	public const string XmlSerializableMissingClrType = "Type '{0}' from namespace '{1}' does not have corresponding IXmlSerializable type. Please consider adding {2} to '{3}'.";

	// Token: 0x040003A6 RID: 934
	public const string XmlCircularDerivation = "Circular reference in derivation of IXmlSerializable type '{0}'.";

	// Token: 0x040003A7 RID: 935
	public const string XmlMelformMapping = "This mapping was not crated by reflection importer and cannot be used in this context.";

	// Token: 0x040003A8 RID: 936
	public const string XmlSchemaSyntaxErrorDetails = "Schema with targetNamespace='{0}' has invalid syntax. {1} Line {2}, position {3}.";

	// Token: 0x040003A9 RID: 937
	public const string XmlSchemaElementReference = "Element reference '{0}' declared in schema type '{1}' from namespace '{2}'.";

	// Token: 0x040003AA RID: 938
	public const string XmlSchemaAttributeReference = "Attribute reference '{0}' declared in schema type '{1}' from namespace '{2}'.";

	// Token: 0x040003AB RID: 939
	public const string XmlSchemaItem = "Schema item '{1}' from namespace '{0}'. {2}";

	// Token: 0x040003AC RID: 940
	public const string XmlSchemaNamedItem = "Schema item '{1}' named '{2}' from namespace '{0}'. {3}";

	// Token: 0x040003AD RID: 941
	public const string XmlSchemaContentDef = "Check content definition of schema type '{0}' from namespace '{1}'. {2}";

	// Token: 0x040003AE RID: 942
	public const string XmlSchema = "Schema with targetNamespace='{0}' has invalid syntax. {1}";

	// Token: 0x040003AF RID: 943
	public const string XmlSerializableRootDupName = "Cannot reconcile schema for '{0}'. Please use [XmlRoot] attribute to change default name or namespace of the top-level element to avoid duplicate element declarations: element name='{1}' namespace='{2}'.";

	// Token: 0x040003B0 RID: 944
	public const string XmlNotSerializable = "Type '{0}' is not serializable.";

	// Token: 0x040003B1 RID: 945
	public const string XmlPregenInvalidXmlSerializerAssemblyAttribute = "Invalid XmlSerializerAssemblyAttribute usage. Please use {0} property or {1} property.";

	// Token: 0x040003B2 RID: 946
	public const string XmlSequenceInconsistent = "Inconsistent sequencing: if used on one of the class's members, the '{0}' property is required on all particle-like members, please explicitly set '{0}' using XmlElement, XmlAnyElement or XmlArray custom attribute on class member '{1}'.";

	// Token: 0x040003B3 RID: 947
	public const string XmlSequenceUnique = "'{1}' values must be unique within the same scope. Value '{0}' is in use. Please change '{1}' property on '{2}'.";

	// Token: 0x040003B4 RID: 948
	public const string XmlSequenceHierarchy = "There was an error processing type '{0}'. Type member '{1}' declared in '{2}' is missing required '{3}' property. If one class in the class hierarchy uses explicit sequencing feature ({3}), then its base class and all derived classes have to do the same.";

	// Token: 0x040003B5 RID: 949
	public const string XmlSequenceMatch = "If multiple custom attributes specified on a single member only one of them have to have explicit '{0}' property, however if more that one attribute has the explicit '{0}', all values have to match.";

	// Token: 0x040003B6 RID: 950
	public const string XmlDisallowNegativeValues = "Negative values are prohibited.";

	// Token: 0x040003B7 RID: 951
	public const string Xml_UnexpectedToken = "This is an unexpected token. The expected token is '{0}'.";

	// Token: 0x040003B8 RID: 952
	public const string Sch_AttributeValueDataType = "The '{0}' attribute has an invalid value according to its data type.";

	// Token: 0x040003B9 RID: 953
	public const string Sch_ElementValueDataType = "The '{0}' element has an invalid value according to its data type.";

	// Token: 0x040003BA RID: 954
	public const string XmlInternalError = "Internal error.";

	// Token: 0x040003BB RID: 955
	public const string XmlInternalErrorDetails = "Internal error: {0}.";

	// Token: 0x040003BC RID: 956
	public const string XmlInternalErrorMethod = "Internal error: missing generated method for {0}.";

	// Token: 0x040003BD RID: 957
	public const string Arg_NeverValueType = "Only TypeKind.Root can be set for typeof(object) which is never value type.";

	// Token: 0x040003BE RID: 958
	public const string XmlInternalErrorReaderAdvance = "Internal error: deserialization failed to advance over underlying stream.";

	// Token: 0x040003BF RID: 959
	public const string Enc_InvalidByteInEncoding = "Invalid byte was found at index {0}.";

	// Token: 0x040003C0 RID: 960
	public const string Arg_ExpectingXmlTextReader = "The XmlReader passed in to construct this XmlValidatingReaderImpl must be an instance of a System.Xml.XmlTextReader.";

	// Token: 0x040003C1 RID: 961
	public const string Arg_CannotCreateNode = "Cannot create node of type {0}.";

	// Token: 0x040003C2 RID: 962
	public const string Arg_IncompatibleParamType = "Type is incompatible.";

	// Token: 0x040003C3 RID: 963
	public const string Xml_SystemPathResolverCannotOpenUri = "Cannot open '{0}'. The Uri parameter must be a file system relative or absolute path.";

	// Token: 0x040003C4 RID: 964
	public const string Xml_EndOfInnerExceptionStack = "--- End of inner exception stack trace ---";

	// Token: 0x040003C5 RID: 965
	public const string XPath_UnclosedString = "String literal was not closed.";

	// Token: 0x040003C6 RID: 966
	public const string XPath_ScientificNotation = "Scientific notation is not allowed.";

	// Token: 0x040003C7 RID: 967
	public const string XPath_UnexpectedToken = "Unexpected token '{0}' in the expression.";

	// Token: 0x040003C8 RID: 968
	public const string XPath_NodeTestExpected = "Expected a node test, found '{0}'.";

	// Token: 0x040003C9 RID: 969
	public const string XPath_EofExpected = "Expected end of the expression, found '{0}'.";

	// Token: 0x040003CA RID: 970
	public const string XPath_TokenExpected = "Expected token '{0}', found '{1}'.";

	// Token: 0x040003CB RID: 971
	public const string XPath_InvalidAxisInPattern = "Only 'child' and 'attribute' axes are allowed in a pattern outside predicates.";

	// Token: 0x040003CC RID: 972
	public const string XPath_PredicateAfterDot = "Abbreviated step '.' cannot be followed by a predicate. Use the full form 'self::node()[predicate]' instead.";

	// Token: 0x040003CD RID: 973
	public const string XPath_PredicateAfterDotDot = "Abbreviated step '..' cannot be followed by a predicate. Use the full form 'parent::node()[predicate]' instead.";

	// Token: 0x040003CE RID: 974
	public const string XPath_NArgsExpected = "Function '{0}()' must have {1} argument(s).";

	// Token: 0x040003CF RID: 975
	public const string XPath_NOrMArgsExpected = "Function '{0}()' must have {1} or {2} argument(s).";

	// Token: 0x040003D0 RID: 976
	public const string XPath_AtLeastNArgsExpected = "Function '{0}()' must have at least {1} argument(s).";

	// Token: 0x040003D1 RID: 977
	public const string XPath_AtMostMArgsExpected = "Function '{0}()' must have no more than {2} arguments.";

	// Token: 0x040003D2 RID: 978
	public const string XPath_NodeSetArgumentExpected = "Argument {1} of function '{0}()' cannot be converted to a node-set.";

	// Token: 0x040003D3 RID: 979
	public const string XPath_NodeSetExpected = "Expression must evaluate to a node-set.";

	// Token: 0x040003D4 RID: 980
	public const string XPath_RtfInPathExpr = "To use a result tree fragment in a path expression, first convert it to a node-set using the msxsl:node-set() function.";

	// Token: 0x040003D5 RID: 981
	public const string Xslt_WarningAsError = "Warning as Error: {0}";

	// Token: 0x040003D6 RID: 982
	public const string Xslt_InputTooComplex = "The stylesheet is too complex.";

	// Token: 0x040003D7 RID: 983
	public const string Xslt_CannotLoadStylesheet = "Cannot load the stylesheet object referenced by URI '{0}', because the provided XmlResolver returned an object of type '{1}'. One of Stream, XmlReader, and IXPathNavigable types was expected.";

	// Token: 0x040003D8 RID: 984
	public const string Xslt_WrongStylesheetElement = "Stylesheet must start either with an 'xsl:stylesheet' or an 'xsl:transform' element, or with a literal result element that has an 'xsl:version' attribute, where prefix 'xsl' denotes the 'http://www.w3.org/1999/XSL/Transform' namespace.";

	// Token: 0x040003D9 RID: 985
	public const string Xslt_WdXslNamespace = "The 'http://www.w3.org/TR/WD-xsl' namespace is no longer supported.";

	// Token: 0x040003DA RID: 986
	public const string Xslt_NotAtTop = "'{0}' element children must precede all other children of the '{1}' element.";

	// Token: 0x040003DB RID: 987
	public const string Xslt_UnexpectedElement = "'{0}' cannot be a child of the '{1}' element.";

	// Token: 0x040003DC RID: 988
	public const string Xslt_NullNsAtTopLevel = "Top-level element '{0}' may not have a null namespace URI.";

	// Token: 0x040003DD RID: 989
	public const string Xslt_TextNodesNotAllowed = "'{0}' element cannot have text node children.";

	// Token: 0x040003DE RID: 990
	public const string Xslt_NotEmptyContents = "The contents of '{0}' must be empty.";

	// Token: 0x040003DF RID: 991
	public const string Xslt_InvalidAttribute = "'{0}' is an invalid attribute for the '{1}' element.";

	// Token: 0x040003E0 RID: 992
	public const string Xslt_MissingAttribute = "Missing mandatory attribute '{0}'.";

	// Token: 0x040003E1 RID: 993
	public const string Xslt_InvalidAttrValue = "'{1}' is an invalid value for the '{0}' attribute.";

	// Token: 0x040003E2 RID: 994
	public const string Xslt_BistateAttribute = "The value of the '{0}' attribute must be '{1}' or '{2}'.";

	// Token: 0x040003E3 RID: 995
	public const string Xslt_CharAttribute = "The value of the '{0}' attribute must be a single character.";

	// Token: 0x040003E4 RID: 996
	public const string Xslt_CircularInclude = "Stylesheet '{0}' cannot directly or indirectly include or import itself.";

	// Token: 0x040003E5 RID: 997
	public const string Xslt_SingleRightBraceInAvt = "The right curly brace in an attribute value template '{0}' outside an expression must be doubled.";

	// Token: 0x040003E6 RID: 998
	public const string Xslt_VariableCntSel2 = "The variable or parameter '{0}' cannot have both a 'select' attribute and non-empty content.";

	// Token: 0x040003E7 RID: 999
	public const string Xslt_KeyCntUse = "'xsl:key' has a 'use' attribute and has non-empty content, or it has empty content and no 'use' attribute.";

	// Token: 0x040003E8 RID: 1000
	public const string Xslt_DupTemplateName = "'{0}' is a duplicate template name.";

	// Token: 0x040003E9 RID: 1001
	public const string Xslt_BothMatchNameAbsent = "'xsl:template' must have either a 'match' attribute or a 'name' attribute, or both.";

	// Token: 0x040003EA RID: 1002
	public const string Xslt_InvalidVariable = "The variable or parameter '{0}' is either not defined or it is out of scope.";

	// Token: 0x040003EB RID: 1003
	public const string Xslt_DupGlobalVariable = "The variable or parameter '{0}' was duplicated with the same import precedence.";

	// Token: 0x040003EC RID: 1004
	public const string Xslt_DupLocalVariable = "The variable or parameter '{0}' was duplicated within the same scope.";

	// Token: 0x040003ED RID: 1005
	public const string Xslt_DupNsAlias = "Namespace URI '{0}' is declared to be an alias for multiple different namespace URIs with the same import precedence.";

	// Token: 0x040003EE RID: 1006
	public const string Xslt_EmptyAttrValue = "The value of the '{0}' attribute cannot be empty.";

	// Token: 0x040003EF RID: 1007
	public const string Xslt_EmptyNsAlias = "The value of the '{0}' attribute cannot be empty. Use '#default' to specify the default namespace.";

	// Token: 0x040003F0 RID: 1008
	public const string Xslt_UnknownXsltFunction = "'{0}()' is an unknown XSLT function.";

	// Token: 0x040003F1 RID: 1009
	public const string Xslt_UnsupportedXsltFunction = "'{0}()' is an unsupported XSLT function.";

	// Token: 0x040003F2 RID: 1010
	public const string Xslt_NoAttributeSet = "A reference to attribute set '{0}' cannot be resolved. An 'xsl:attribute-set' of this name must be declared at the top level of the stylesheet.";

	// Token: 0x040003F3 RID: 1011
	public const string Xslt_UndefinedKey = "A reference to key '{0}' cannot be resolved. An 'xsl:key' of this name must be declared at the top level of the stylesheet.";

	// Token: 0x040003F4 RID: 1012
	public const string Xslt_CircularAttributeSet = "Circular reference in the definition of attribute set '{0}'.";

	// Token: 0x040003F5 RID: 1013
	public const string Xslt_InvalidCallTemplate = "The named template '{0}' does not exist.";

	// Token: 0x040003F6 RID: 1014
	public const string Xslt_InvalidPrefix = "Prefix '{0}' is not defined.";

	// Token: 0x040003F7 RID: 1015
	public const string Xslt_ScriptXsltNamespace = "Script block cannot implement the XSLT namespace.";

	// Token: 0x040003F8 RID: 1016
	public const string Xslt_ScriptInvalidLanguage = "Scripting language '{0}' is not supported.";

	// Token: 0x040003F9 RID: 1017
	public const string Xslt_ScriptMixedLanguages = "All script blocks implementing the namespace '{0}' must use the same language.";

	// Token: 0x040003FA RID: 1018
	public const string Xslt_ScriptAndExtensionClash = "Cannot have both an extension object and a script implementing the same namespace '{0}'.";

	// Token: 0x040003FB RID: 1019
	public const string Xslt_NoDecimalFormat = "Decimal format '{0}' is not defined.";

	// Token: 0x040003FC RID: 1020
	public const string Xslt_DecimalFormatSignsNotDistinct = "The '{0}' and '{1}' attributes of 'xsl:decimal-format' must have distinct values.";

	// Token: 0x040003FD RID: 1021
	public const string Xslt_DecimalFormatRedefined = "The '{0}' attribute of 'xsl:decimal-format' cannot be redefined with a value of '{1}'.";

	// Token: 0x040003FE RID: 1022
	public const string Xslt_UnknownExtensionElement = "'{0}' is not a recognized extension element.";

	// Token: 0x040003FF RID: 1023
	public const string Xslt_ModeWithoutMatch = "An 'xsl:template' element without a 'match' attribute cannot have a 'mode' attribute.";

	// Token: 0x04000400 RID: 1024
	public const string Xslt_ModeListEmpty = "List of modes in 'xsl:template' element can't be empty. ";

	// Token: 0x04000401 RID: 1025
	public const string Xslt_ModeListDup = "List of modes in 'xsl:template' element can't contain duplicates ('{0}'). ";

	// Token: 0x04000402 RID: 1026
	public const string Xslt_ModeListAll = "List of modes in 'xsl:template' element can't contain token '#all' together with any other value. ";

	// Token: 0x04000403 RID: 1027
	public const string Xslt_PriorityWithoutMatch = "An 'xsl:template' element without a 'match' attribute cannot have a 'priority' attribute.";

	// Token: 0x04000404 RID: 1028
	public const string Xslt_InvalidApplyImports = "An 'xsl:apply-imports' element can only occur within an 'xsl:template' element with a 'match' attribute, and cannot occur within an 'xsl:for-each' element.";

	// Token: 0x04000405 RID: 1029
	public const string Xslt_DuplicateWithParam = "Value of parameter '{0}' cannot be specified more than once within a single 'xsl:call-template' or 'xsl:apply-templates' element.";

	// Token: 0x04000406 RID: 1030
	public const string Xslt_ReservedNS = "Elements and attributes cannot belong to the reserved namespace '{0}'.";

	// Token: 0x04000407 RID: 1031
	public const string Xslt_XmlnsAttr = "An attribute with a local name 'xmlns' and a null namespace URI cannot be created.";

	// Token: 0x04000408 RID: 1032
	public const string Xslt_NoWhen = "An 'xsl:choose' element must have at least one 'xsl:when' child.";

	// Token: 0x04000409 RID: 1033
	public const string Xslt_WhenAfterOtherwise = "'xsl:when' must precede the 'xsl:otherwise' element.";

	// Token: 0x0400040A RID: 1034
	public const string Xslt_DupOtherwise = "An 'xsl:choose' element can have only one 'xsl:otherwise' child.";

	// Token: 0x0400040B RID: 1035
	public const string Xslt_AttributeRedefinition = "Attribute '{0}' of 'xsl:output' cannot be defined more than once with the same import precedence.";

	// Token: 0x0400040C RID: 1036
	public const string Xslt_InvalidMethod = "'{0}' is not a supported output method. Supported methods are 'xml', 'html', and 'text'.";

	// Token: 0x0400040D RID: 1037
	public const string Xslt_InvalidEncoding = "'{0}' is not a supported encoding name.";

	// Token: 0x0400040E RID: 1038
	public const string Xslt_InvalidLanguage = "'{0}' is not a supported language identifier.";

	// Token: 0x0400040F RID: 1039
	public const string Xslt_InvalidCompareOption = "String comparison option(s) '{0}' are either invalid or cannot be used together.";

	// Token: 0x04000410 RID: 1040
	public const string Xslt_KeyNotAllowed = "The 'key()' function cannot be used in 'use' and 'match' attributes of 'xsl:key' element.";

	// Token: 0x04000411 RID: 1041
	public const string Xslt_VariablesNotAllowed = "Variables cannot be used within this expression.";

	// Token: 0x04000412 RID: 1042
	public const string Xslt_CurrentNotAllowed = "The 'current()' function cannot be used in a pattern.";

	// Token: 0x04000413 RID: 1043
	public const string Xslt_DocumentFuncProhibited = "Execution of the 'document()' function was prohibited. Use the XsltSettings.EnableDocumentFunction property to enable it.";

	// Token: 0x04000414 RID: 1044
	public const string Xslt_ScriptsProhibited = "Execution of scripts was prohibited. Use the XsltSettings.EnableScript property to enable it.";

	// Token: 0x04000415 RID: 1045
	public const string Xslt_ItemNull = "Extension functions cannot return null values.";

	// Token: 0x04000416 RID: 1046
	public const string Xslt_NodeSetNotNode = "Cannot convert a node-set which contains zero nodes or more than one node to a single node.";

	// Token: 0x04000417 RID: 1047
	public const string Xslt_UnsupportedClrType = "Extension function parameters or return values which have Clr type '{0}' are not supported.";

	// Token: 0x04000418 RID: 1048
	public const string Xslt_NotYetImplemented = "'{0}' is not yet implemented.";

	// Token: 0x04000419 RID: 1049
	public const string Xslt_SchemaDeclaration = "'{0}' declaration is not permitted in non-schema aware processor.";

	// Token: 0x0400041A RID: 1050
	public const string Xslt_SchemaAttribute = "Attribute '{0}' is not permitted in basic XSLT processor (http://www.w3.org/TR/xslt20/#dt-basic-xslt-processor).";

	// Token: 0x0400041B RID: 1051
	public const string Xslt_SchemaAttributeValue = "Value '{1}' of attribute '{0}' is not permitted in basic XSLT processor (http://www.w3.org/TR/xslt20/#dt-basic-xslt-processor).";

	// Token: 0x0400041C RID: 1052
	public const string Xslt_ElementCntSel = "The element '{0}' cannot have both a 'select' attribute and non-empty content.";

	// Token: 0x0400041D RID: 1053
	public const string Xslt_PerformSortCntSel = "The element 'xsl:perform-sort' cannot have 'select' attribute any content other than 'xsl:sort' and 'xsl:fallback' instructions.";

	// Token: 0x0400041E RID: 1054
	public const string Xslt_RequiredAndSelect = "Mandatory parameter '{0}' must be empty and must not have a 'select' attribute.";

	// Token: 0x0400041F RID: 1055
	public const string Xslt_NoSelectNoContent = "Element '{0}' must have either 'select' attribute or non-empty content.";

	// Token: 0x04000420 RID: 1056
	public const string Xslt_NonTemplateTunnel = "Stylesheet or function parameter '{0}' cannot have attribute 'tunnel'.";

	// Token: 0x04000421 RID: 1057
	public const string Xslt_RequiredOnFunction = "The 'required' attribute must not be specified for parameter '{0}'. Function parameters are always mandatory. ";

	// Token: 0x04000422 RID: 1058
	public const string Xslt_ExcludeDefault = "Value '#default' is used within the 'exclude-result-prefixes' attribute and the parent element of this attribute has no default namespace.";

	// Token: 0x04000423 RID: 1059
	public const string Xslt_CollationSyntax = "The value of an 'default-collation' attribute contains no recognized collation URI.";

	// Token: 0x04000424 RID: 1060
	public const string Xslt_AnalyzeStringDupChild = "'xsl:analyze-string' cannot have second child with name '{0}'.";

	// Token: 0x04000425 RID: 1061
	public const string Xslt_AnalyzeStringChildOrder = "When both 'xsl:matching-string' and 'xsl:non-matching-string' elements are present, 'xsl:matching-string' element must come first.";

	// Token: 0x04000426 RID: 1062
	public const string Xslt_AnalyzeStringEmpty = "'xsl:analyze-string' must contain either 'xsl:matching-string' or 'xsl:non-matching-string' elements or both.";

	// Token: 0x04000427 RID: 1063
	public const string Xslt_SortStable = "Only the first 'xsl:sort' element may have 'stable' attribute.";

	// Token: 0x04000428 RID: 1064
	public const string Xslt_InputTypeAnnotations = "It is an error if there is a stylesheet module in the stylesheet that specifies 'input-type-annotations'=\"strip\" and another stylesheet module that specifies 'input-type-annotations'=\"preserve\".";

	// Token: 0x04000429 RID: 1065
	public const string Coll_BadOptFormat = "Collation option '{0}' is invalid. Options must have the following format: <option-name>=<option-value>.";

	// Token: 0x0400042A RID: 1066
	public const string Coll_Unsupported = "The collation '{0}' is not supported.";

	// Token: 0x0400042B RID: 1067
	public const string Coll_UnsupportedLanguage = "Collation language '{0}' is not supported.";

	// Token: 0x0400042C RID: 1068
	public const string Coll_UnsupportedOpt = "Unsupported option '{0}' in collation.";

	// Token: 0x0400042D RID: 1069
	public const string Coll_UnsupportedOptVal = "Collation option '{0}' cannot have the value '{1}'.";

	// Token: 0x0400042E RID: 1070
	public const string Coll_UnsupportedSortOpt = "Unsupported sort option '{0}' in collation.";

	// Token: 0x0400042F RID: 1071
	public const string Qil_Validation = "QIL Validation Error! '{0}'.";

	// Token: 0x04000430 RID: 1072
	public const string XmlIl_TooManyParameters = "Functions may not have more than 65535 parameters.";

	// Token: 0x04000431 RID: 1073
	public const string XmlIl_BadXmlState = "An item of type '{0}' cannot be constructed within a node of type '{1}'.";

	// Token: 0x04000432 RID: 1074
	public const string XmlIl_BadXmlStateAttr = "Attribute and namespace nodes cannot be added to the parent element after a text, comment, pi, or sub-element node has already been added.";

	// Token: 0x04000433 RID: 1075
	public const string XmlIl_NmspAfterAttr = "Namespace nodes cannot be added to the parent element after an attribute node has already been added.";

	// Token: 0x04000434 RID: 1076
	public const string XmlIl_NmspConflict = "Cannot construct namespace declaration xmlns{0}{1}='{2}'. Prefix '{1}' is already mapped to namespace '{3}'.";

	// Token: 0x04000435 RID: 1077
	public const string XmlIl_CantResolveEntity = "Cannot query the data source object referenced by URI '{0}', because the provided XmlResolver returned an object of type '{1}'. Only Stream, XmlReader, and IXPathNavigable data source objects are currently supported.";

	// Token: 0x04000436 RID: 1078
	public const string XmlIl_NoDefaultDocument = "Query requires a default data source, but no default was supplied to the query engine.";

	// Token: 0x04000437 RID: 1079
	public const string XmlIl_UnknownDocument = "Data source '{0}' cannot be located.";

	// Token: 0x04000438 RID: 1080
	public const string XmlIl_UnknownParam = "Supplied XsltArgumentList does not contain a parameter with local name '{0}' and namespace '{1}'.";

	// Token: 0x04000439 RID: 1081
	public const string XmlIl_UnknownExtObj = "Cannot find a script or an extension object associated with namespace '{0}'.";

	// Token: 0x0400043A RID: 1082
	public const string XmlIl_CantStripNav = "Whitespace cannot be stripped from input documents that have already been loaded. Provide the input document as an XmlReader instead.";

	// Token: 0x0400043B RID: 1083
	public const string XmlIl_ExtensionError = "An error occurred during a call to extension function '{0}'. See InnerException for a complete description of the error.";

	// Token: 0x0400043C RID: 1084
	public const string XmlIl_TopLevelAttrNmsp = "XmlWriter cannot process the sequence returned by the query, because it contains an attribute or namespace node.";

	// Token: 0x0400043D RID: 1085
	public const string XmlIl_NoExtensionMethod = "Extension object '{0}' does not contain a matching '{1}' method that has {2} parameter(s).";

	// Token: 0x0400043E RID: 1086
	public const string XmlIl_AmbiguousExtensionMethod = "Ambiguous method call. Extension object '{0}' contains multiple '{1}' methods that have {2} parameter(s).";

	// Token: 0x0400043F RID: 1087
	public const string XmlIl_NonPublicExtensionMethod = "Method '{1}' of extension object '{0}' cannot be called because it is not public.";

	// Token: 0x04000440 RID: 1088
	public const string XmlIl_GenericExtensionMethod = "Method '{1}' of extension object '{0}' cannot be called because it is generic.";

	// Token: 0x04000441 RID: 1089
	public const string XmlIl_ByRefType = "Method '{1}' of extension object '{0}' cannot be called because it has one or more ByRef parameters.";

	// Token: 0x04000442 RID: 1090
	public const string XmlIl_DocumentLoadError = "An error occurred while loading document '{0}'. See InnerException for a complete description of the error.";

	// Token: 0x04000443 RID: 1091
	public const string Xslt_CompileError = "XSLT compile error at {0}({1},{2}). See InnerException for details.";

	// Token: 0x04000444 RID: 1092
	public const string Xslt_CompileError2 = "XSLT compile error.";

	// Token: 0x04000445 RID: 1093
	public const string Xslt_UnsuppFunction = "'{0}()' is an unsupported XSLT function.";

	// Token: 0x04000446 RID: 1094
	public const string Xslt_NotFirstImport = "'xsl:import' instructions must precede all other element children of an 'xsl:stylesheet' element.";

	// Token: 0x04000447 RID: 1095
	public const string Xslt_UnexpectedKeyword = "'{0}' cannot be a child of the '{1}' element.";

	// Token: 0x04000448 RID: 1096
	public const string Xslt_InvalidContents = "The contents of '{0}' are invalid.";

	// Token: 0x04000449 RID: 1097
	public const string Xslt_CantResolve = "Cannot resolve the referenced document '{0}'.";

	// Token: 0x0400044A RID: 1098
	public const string Xslt_SingleRightAvt = "Right curly brace in the attribute value template '{0}' must be doubled.";

	// Token: 0x0400044B RID: 1099
	public const string Xslt_OpenBracesAvt = "The braces are not closed in AVT expression '{0}'.";

	// Token: 0x0400044C RID: 1100
	public const string Xslt_OpenLiteralAvt = "The literal in AVT expression is not correctly closed '{0}'.";

	// Token: 0x0400044D RID: 1101
	public const string Xslt_NestedAvt = "AVT cannot be nested in AVT '{0}'.";

	// Token: 0x0400044E RID: 1102
	public const string Xslt_EmptyAvtExpr = "XPath Expression in AVT cannot be empty: '{0}'.";

	// Token: 0x0400044F RID: 1103
	public const string Xslt_InvalidXPath = "'{0}' is an invalid XPath expression.";

	// Token: 0x04000450 RID: 1104
	public const string Xslt_InvalidQName = "'{0}' is an invalid QName.";

	// Token: 0x04000451 RID: 1105
	public const string Xslt_TemplateNoAttrib = "The 'xsl:template' instruction must have the 'match' and/or 'name' attribute present.";

	// Token: 0x04000452 RID: 1106
	public const string Xslt_DupVarName = "Variable or parameter '{0}' was duplicated within the same scope.";

	// Token: 0x04000453 RID: 1107
	public const string Xslt_WrongNumberArgs = "XSLT function '{0}()' has the wrong number of arguments.";

	// Token: 0x04000454 RID: 1108
	public const string Xslt_NoNodeSetConversion = "Cannot convert the operand to a node-set.";

	// Token: 0x04000455 RID: 1109
	public const string Xslt_NoNavigatorConversion = "Cannot convert the operand to 'Result tree fragment'.";

	// Token: 0x04000456 RID: 1110
	public const string Xslt_InvalidFormat = "Format cannot be empty.";

	// Token: 0x04000457 RID: 1111
	public const string Xslt_InvalidFormat1 = "Format '{0}' cannot have digit symbol after zero digit symbol before a decimal point.";

	// Token: 0x04000458 RID: 1112
	public const string Xslt_InvalidFormat2 = "Format '{0}' cannot have zero digit symbol after digit symbol after decimal point.";

	// Token: 0x04000459 RID: 1113
	public const string Xslt_InvalidFormat3 = "Format '{0}' has two pattern separators.";

	// Token: 0x0400045A RID: 1114
	public const string Xslt_InvalidFormat5 = "Format '{0}' cannot have two decimal separators.";

	// Token: 0x0400045B RID: 1115
	public const string Xslt_InvalidFormat8 = "Format string should have at least one digit or zero digit.";

	// Token: 0x0400045C RID: 1116
	public const string Xslt_ScriptInvalidPrefix = "Cannot find the script or external object that implements prefix '{0}'.";

	// Token: 0x0400045D RID: 1117
	public const string Xslt_ScriptDub = "Namespace '{0}' has a duplicate implementation.";

	// Token: 0x0400045E RID: 1118
	public const string Xslt_ScriptEmpty = "The 'msxsl:script' element cannot be empty.";

	// Token: 0x0400045F RID: 1119
	public const string Xslt_DupDecimalFormat = "Decimal format '{0}' has a duplicate declaration.";

	// Token: 0x04000460 RID: 1120
	public const string Xslt_CircularReference = "Circular reference in the definition of variable '{0}'.";

	// Token: 0x04000461 RID: 1121
	public const string Xslt_InvalidExtensionNamespace = "Extension namespace cannot be 'null' or an XSLT namespace URI.";

	// Token: 0x04000462 RID: 1122
	public const string Xslt_InvalidModeAttribute = "An 'xsl:template' element without a 'match' attribute cannot have a 'mode' attribute.";

	// Token: 0x04000463 RID: 1123
	public const string Xslt_MultipleRoots = "There are multiple root elements in the output XML.";

	// Token: 0x04000464 RID: 1124
	public const string Xslt_ApplyImports = "The 'xsl:apply-imports' instruction cannot be included within the content of an 'xsl:for-each' instruction or within an 'xsl:template' instruction without the 'match' attribute.";

	// Token: 0x04000465 RID: 1125
	public const string Xslt_Terminate = "Transform terminated: '{0}'.";

	// Token: 0x04000466 RID: 1126
	public const string Xslt_InvalidPattern = "'{0}' is an invalid XSLT pattern.";

	// Token: 0x04000467 RID: 1127
	public const string XmlInvalidCharSchemaPrimitive = "Char is not a valid schema primitive and should be treated as int in DataContract";

	// Token: 0x04000468 RID: 1128
	public const string UnknownConstantType = "Internal Error: Unrecognized constant type {0}.";

	// Token: 0x04000469 RID: 1129
	public const string ArrayTypeIsNotSupported = "Array of type {0} is not supported.";

	// Token: 0x0400046A RID: 1130
	public const string Xml_MissingSerializationCodeException = "Type '{0}' cannot be serialized by XmlSerializer, serialization code for the type is missing. Consult the SDK documentation for adding it as a root serialization type. http://go.microsoft.com/fwlink/?LinkId=613136";

	// Token: 0x0400046B RID: 1131
	public const string Xslt_UpperCaseFirstNotSupported = "Uppercase-First sorting option is not supported.";

	// Token: 0x0400046C RID: 1132
	public const string XmlPregenTypeDynamic = "Cannot pre-generate serialization code for type '{0}'. Pre-generation of serialization assemblies is not supported for dynamic types. Save the assembly and load it from disk to use it with XmlSerialization.";

	// Token: 0x0400046D RID: 1133
	public const string XmlPregenOrphanType = "Cannot pre-generate serializer for multiple assemblies. Type '{0}' does not belong to assembly {1}.";

	// Token: 0x0400046E RID: 1134
	public const string ErrSerializerExists = "Cannot generate serialization code {0} because the code file already exists. Use /{1} to force an overwrite of the existing file.";

	// Token: 0x0400046F RID: 1135
	public const string ErrDirectoryExists = "Cannot generate serialization code '{0}' because a directory with the same name already exists.";

	// Token: 0x04000470 RID: 1136
	public const string ErrDirectoryNotExists = "Cannot generate serialization code because directory {0} doesn't exist.";

	// Token: 0x04000471 RID: 1137
	public const string ErrInvalidArgument = "Ignoring invalid command line argument: '{0}'.";

	// Token: 0x04000472 RID: 1138
	public const string Warning = "Warning: {0}.";

	// Token: 0x04000473 RID: 1139
	public const string ErrMissingRequiredArgument = "Missing required command-line argument: {0}.";

	// Token: 0x04000474 RID: 1140
	public const string ErrAssembly = "The name of the source assembly.";

	// Token: 0x04000475 RID: 1141
	public const string InfoGeneratedFile = "Generated serialization code for assembly {0} --> '{1}'.";

	// Token: 0x04000476 RID: 1142
	public const string InfoFileName = "Serialization Code File Name: {0}.";

	// Token: 0x04000477 RID: 1143
	public const string ErrGenerationFailed = "Sgen utility failed to pregenerate serialization code for {0}.";

	// Token: 0x04000478 RID: 1144
	public const string ErrorDetails = "Error: {0}.";

	// Token: 0x04000479 RID: 1145
	public const string ErrLoadType = "Type '{0}' was not found in the assembly '{1}'.";

	// Token: 0x0400047A RID: 1146
	public const string DirectoryAccessDenied = "Access to directory {0} is denied.  The process under which XmlSerializer is running does not have sufficient permission to access the directory.";

	// Token: 0x0400047B RID: 1147
	public const string Xslt_NotSupported = "Compilation of XSLT is not supported on this platform.";

	// Token: 0x0400047C RID: 1148
	public const string ErrLoadAssembly = "File or assembly name '{0}', or one of its dependencies, was not found.";

	// Token: 0x0400047D RID: 1149
	public const string InfoNoSerializableTypes = "Assembly '{0}' does not contain any types that can be serialized using XmlSerializer.";

	// Token: 0x0400047E RID: 1150
	public const string InfoIgnoreType = "Ignoring '{0}'.";

	// Token: 0x0400047F RID: 1151
	public const string FailLoadAssemblyUnderPregenMode = "\"Fail to load assembly {0} or {0} doesn't exist under PreGen Mode.";

	// Token: 0x04000480 RID: 1152
	public const string HelpDescription = "Generates serialization code for use with XmlSerializer.\\nThe utility allows developers to pre-generate code for serialization\\nbuilding and deploying the assemblies with the application.\\n    ";

	// Token: 0x04000481 RID: 1153
	public const string HelpUsage = "\\nUsage: dotnet {0} [--assembly <assembly file path>] [--type <type name>]";

	// Token: 0x04000482 RID: 1154
	public const string HelpDevOptions = "  \\n  Developer options:";

	// Token: 0x04000483 RID: 1155
	public const string HelpAssembly = "     {0}|{1}   Assembly location or display name.";

	// Token: 0x04000484 RID: 1156
	public const string HelpType = "     {0}          Generate code for serialization/deserialization of the specified type from the input assembly.";

	// Token: 0x04000485 RID: 1157
	public const string HelpForce = "     {0}         Forces overwrite of a previously generated assembly.";

	// Token: 0x04000486 RID: 1158
	public const string HelpProxy = "     {0}    Generate serialization code only for proxy classes and web method parameters.";

	// Token: 0x04000487 RID: 1159
	public const string HelpOut = "     {0}|{1}        Output directory name (default: target assembly location).";

	// Token: 0x04000488 RID: 1160
	public const string HelpMiscOptions = "  \\n  Miscellaneous options:";

	// Token: 0x04000489 RID: 1161
	public const string HelpHelp = "     {0}|{1}       Show help.";

	// Token: 0x0400048A RID: 1162
	public const string MoreHelp = "If you would like more help, please type \"sgen {0}\".";

	// Token: 0x0400048B RID: 1163
	public const string GenerateSerializerNotFound = "Method 'System.Xml.Serialization.XmlSerializer.GenerateSerializer' was not found. This is likely because you are using an older version of the framework. Please update to .NET Core v2.1 or later.";
}
