using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the C14N XML canonicalization transform for a digital signature as defined by the World Wide Web Consortium (W3C), with comments.</summary>
	// Token: 0x02000061 RID: 97
	public class XmlDsigC14NWithCommentsTransform : XmlDsigC14NTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform" /> class.</summary>
		// Token: 0x06000338 RID: 824 RVA: 0x0000F89F File Offset: 0x0000DA9F
		public XmlDsigC14NWithCommentsTransform() : base(true)
		{
			base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";
		}
	}
}
