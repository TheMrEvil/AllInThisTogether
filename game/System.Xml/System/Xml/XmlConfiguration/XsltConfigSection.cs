using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.XmlConfiguration
{
	/// <summary>Represents an XSLT configuration section.</summary>
	// Token: 0x02000325 RID: 805
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XsltConfigSection : ConfigurationSection
	{
		/// <summary>Gets or sets a string that represents the XSLT prohibit default resolver.</summary>
		/// <returns>A string that represents the XSLT prohibit default resolver.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x000D2502 File Offset: 0x000D0702
		// (set) Token: 0x06002114 RID: 8468 RVA: 0x000D2514 File Offset: 0x000D0714
		[ConfigurationProperty("prohibitDefaultResolver", DefaultValue = "false")]
		public string ProhibitDefaultResolverString
		{
			get
			{
				return (string)base["prohibitDefaultResolver"];
			}
			set
			{
				base["prohibitDefaultResolver"] = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000D25E4 File Offset: 0x000D07E4
		private bool _ProhibitDefaultResolver
		{
			get
			{
				bool result;
				XmlConvert.TryToBoolean(this.ProhibitDefaultResolverString, out result);
				return result;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000D2600 File Offset: 0x000D0800
		private static bool s_ProhibitDefaultUrlResolver
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection != null && xsltConfigSection._ProhibitDefaultResolver;
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000D2628 File Offset: 0x000D0828
		internal static XmlResolver CreateDefaultResolver()
		{
			if (XsltConfigSection.s_ProhibitDefaultUrlResolver)
			{
				return XmlNullResolver.Singleton;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x000D263C File Offset: 0x000D083C
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x000D264E File Offset: 0x000D084E
		[ConfigurationProperty("limitXPathComplexity", DefaultValue = "true")]
		internal string LimitXPathComplexityString
		{
			get
			{
				return (string)base["limitXPathComplexity"];
			}
			set
			{
				base["limitXPathComplexity"] = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x000D265C File Offset: 0x000D085C
		private bool _LimitXPathComplexity
		{
			get
			{
				string limitXPathComplexityString = this.LimitXPathComplexityString;
				bool result = true;
				XmlConvert.TryToBoolean(limitXPathComplexityString, out result);
				return result;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x000D267C File Offset: 0x000D087C
		internal static bool LimitXPathComplexity
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection == null || xsltConfigSection._LimitXPathComplexity;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x000D26A4 File Offset: 0x000D08A4
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x000D26B6 File Offset: 0x000D08B6
		[ConfigurationProperty("enableMemberAccessForXslCompiledTransform", DefaultValue = "False")]
		internal string EnableMemberAccessForXslCompiledTransformString
		{
			get
			{
				return (string)base["enableMemberAccessForXslCompiledTransform"];
			}
			set
			{
				base["enableMemberAccessForXslCompiledTransform"] = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x000D26C4 File Offset: 0x000D08C4
		private bool _EnableMemberAccessForXslCompiledTransform
		{
			get
			{
				string enableMemberAccessForXslCompiledTransformString = this.EnableMemberAccessForXslCompiledTransformString;
				bool result = false;
				XmlConvert.TryToBoolean(enableMemberAccessForXslCompiledTransformString, out result);
				return result;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x000D26E4 File Offset: 0x000D08E4
		internal static bool EnableMemberAccessForXslCompiledTransform
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection != null && xsltConfigSection._EnableMemberAccessForXslCompiledTransform;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlConfiguration.XsltConfigSection" /> class.</summary>
		// Token: 0x06002120 RID: 8480 RVA: 0x000D25DC File Offset: 0x000D07DC
		public XsltConfigSection()
		{
		}
	}
}
