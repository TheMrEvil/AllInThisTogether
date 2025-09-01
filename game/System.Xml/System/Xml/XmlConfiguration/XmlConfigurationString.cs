using System;
using System.Globalization;

namespace System.Xml.XmlConfiguration
{
	// Token: 0x02000323 RID: 803
	internal static class XmlConfigurationString
	{
		// Token: 0x06002108 RID: 8456 RVA: 0x000D24C4 File Offset: 0x000D06C4
		// Note: this type is marked as 'beforefieldinit'.
		static XmlConfigurationString()
		{
		}

		// Token: 0x04001B7F RID: 7039
		internal const string XmlReaderSectionName = "xmlReader";

		// Token: 0x04001B80 RID: 7040
		internal const string XsltSectionName = "xslt";

		// Token: 0x04001B81 RID: 7041
		internal const string ProhibitDefaultResolverName = "prohibitDefaultResolver";

		// Token: 0x04001B82 RID: 7042
		internal const string LimitXPathComplexityName = "limitXPathComplexity";

		// Token: 0x04001B83 RID: 7043
		internal const string EnableMemberAccessForXslCompiledTransformName = "enableMemberAccessForXslCompiledTransform";

		// Token: 0x04001B84 RID: 7044
		internal const string CollapseWhiteSpaceIntoEmptyStringName = "CollapseWhiteSpaceIntoEmptyString";

		// Token: 0x04001B85 RID: 7045
		internal const string XmlConfigurationSectionName = "system.xml";

		// Token: 0x04001B86 RID: 7046
		internal static string XmlReaderSectionPath = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", "system.xml", "xmlReader");

		// Token: 0x04001B87 RID: 7047
		internal static string XsltSectionPath = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", "system.xml", "xslt");
	}
}
