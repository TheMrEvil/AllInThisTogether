using System;
using System.Globalization;

// Token: 0x02000002 RID: 2
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

	// Token: 0x04000001 RID: 1
	public const string Argument_AddAttribute = "An attribute cannot be added to content.";

	// Token: 0x04000002 RID: 2
	public const string Argument_AddNode = "A node of type {0} cannot be added to content.";

	// Token: 0x04000003 RID: 3
	public const string Argument_AddNonWhitespace = "Non-whitespace characters cannot be added to content.";

	// Token: 0x04000004 RID: 4
	public const string Argument_ConvertToString = "The argument cannot be converted to a string.";

	// Token: 0x04000005 RID: 5
	public const string Argument_InvalidExpandedName = "'{0}' is an invalid expanded name.";

	// Token: 0x04000006 RID: 6
	public const string Argument_InvalidPIName = "'{0}' is an invalid name for a processing instruction.";

	// Token: 0x04000007 RID: 7
	public const string Argument_InvalidPrefix = "'{0}' is an invalid prefix.";

	// Token: 0x04000008 RID: 8
	public const string Argument_MustBeDerivedFrom = "The argument must be derived from {0}.";

	// Token: 0x04000009 RID: 9
	public const string Argument_NamespaceDeclarationPrefixed = "The prefix '{0}' cannot be bound to the empty namespace name.";

	// Token: 0x0400000A RID: 10
	public const string Argument_NamespaceDeclarationXml = "The prefix 'xml' is bound to the namespace name 'http://www.w3.org/XML/1998/namespace'. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.";

	// Token: 0x0400000B RID: 11
	public const string Argument_NamespaceDeclarationXmlns = "The prefix 'xmlns' is bound to the namespace name 'http://www.w3.org/2000/xmlns/'. It must not be declared. Other prefixes must not be bound to this namespace name, and it must not be declared as the default namespace.";

	// Token: 0x0400000C RID: 12
	public const string Argument_XObjectValue = "An XObject cannot be used as a value.";

	// Token: 0x0400000D RID: 13
	public const string InvalidOperation_DeserializeInstance = "This instance cannot be deserialized.";

	// Token: 0x0400000E RID: 14
	public const string InvalidOperation_DocumentStructure = "This operation would create an incorrectly structured document.";

	// Token: 0x0400000F RID: 15
	public const string InvalidOperation_DuplicateAttribute = "Duplicate attribute.";

	// Token: 0x04000010 RID: 16
	public const string InvalidOperation_ExpectedEndOfFile = "The XmlReader state should be EndOfFile after this operation.";

	// Token: 0x04000011 RID: 17
	public const string InvalidOperation_ExpectedInteractive = "The XmlReader state should be Interactive.";

	// Token: 0x04000012 RID: 18
	public const string InvalidOperation_ExpectedNodeType = "The XmlReader must be on a node of type {0} instead of a node of type {1}.";

	// Token: 0x04000013 RID: 19
	public const string InvalidOperation_ExternalCode = "This operation was corrupted by external code.";

	// Token: 0x04000014 RID: 20
	public const string InvalidOperation_MissingAncestor = "A common ancestor is missing.";

	// Token: 0x04000015 RID: 21
	public const string InvalidOperation_MissingParent = "The parent is missing.";

	// Token: 0x04000016 RID: 22
	public const string InvalidOperation_MissingRoot = "The root element is missing.";

	// Token: 0x04000017 RID: 23
	public const string InvalidOperation_UnexpectedNodeType = "The XmlReader should not be on a node of type {0}.";

	// Token: 0x04000018 RID: 24
	public const string InvalidOperation_UnresolvedEntityReference = "The XmlReader cannot resolve entity references.";

	// Token: 0x04000019 RID: 25
	public const string InvalidOperation_WriteAttribute = "An attribute cannot be written after content.";

	// Token: 0x0400001A RID: 26
	public const string NotSupported_WriteBase64 = "This XmlWriter does not support base64 encoded data.";

	// Token: 0x0400001B RID: 27
	public const string NotSupported_WriteEntityRef = "This XmlWriter does not support entity references.";

	// Token: 0x0400001C RID: 28
	public const string Argument_CreateNavigator = "This XPathNavigator cannot be created on a node of type {0}.";

	// Token: 0x0400001D RID: 29
	public const string InvalidOperation_BadNodeType = "This operation is not valid on a node of type {0}.";

	// Token: 0x0400001E RID: 30
	public const string InvalidOperation_UnexpectedEvaluation = "The XPath expression evaluated to unexpected type {0}.";

	// Token: 0x0400001F RID: 31
	public const string NotSupported_MoveToId = "This XPathNavigator does not support IDs.";
}
