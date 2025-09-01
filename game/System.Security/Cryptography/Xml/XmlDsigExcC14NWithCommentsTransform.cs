using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the exclusive C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), with comments.</summary>
	// Token: 0x02000064 RID: 100
	public class XmlDsigExcC14NWithCommentsTransform : XmlDsigExcC14NTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform" /> class.</summary>
		// Token: 0x06000354 RID: 852 RVA: 0x0001011B File Offset: 0x0000E31B
		public XmlDsigExcC14NWithCommentsTransform() : base(true)
		{
			base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform" /> class specifying a list of namespace prefixes to canonicalize using the standard canonicalization algorithm.</summary>
		/// <param name="inclusiveNamespacesPrefixList">The namespace prefixes to canonicalize using the standard canonicalization algorithm.</param>
		// Token: 0x06000355 RID: 853 RVA: 0x0001012F File Offset: 0x0000E32F
		public XmlDsigExcC14NWithCommentsTransform(string inclusiveNamespacesPrefixList) : base(true, inclusiveNamespacesPrefixList)
		{
			base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
		}
	}
}
