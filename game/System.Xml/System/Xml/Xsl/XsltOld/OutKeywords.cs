using System;
using System.Diagnostics;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000399 RID: 921
	internal class OutKeywords
	{
		// Token: 0x06002549 RID: 9545 RVA: 0x000E2808 File Offset: 0x000E0A08
		internal OutKeywords(XmlNameTable nameTable)
		{
			this._AtomEmpty = nameTable.Add(string.Empty);
			this._AtomLang = nameTable.Add("lang");
			this._AtomSpace = nameTable.Add("space");
			this._AtomXmlns = nameTable.Add("xmlns");
			this._AtomXml = nameTable.Add("xml");
			this._AtomXmlNamespace = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this._AtomXmlnsNamespace = nameTable.Add("http://www.w3.org/2000/xmlns/");
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x000E2892 File Offset: 0x000E0A92
		internal string Empty
		{
			get
			{
				return this._AtomEmpty;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x000E289A File Offset: 0x000E0A9A
		internal string Lang
		{
			get
			{
				return this._AtomLang;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x000E28A2 File Offset: 0x000E0AA2
		internal string Space
		{
			get
			{
				return this._AtomSpace;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x000E28AA File Offset: 0x000E0AAA
		internal string Xmlns
		{
			get
			{
				return this._AtomXmlns;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000E28B2 File Offset: 0x000E0AB2
		internal string Xml
		{
			get
			{
				return this._AtomXml;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x000E28BA File Offset: 0x000E0ABA
		internal string XmlNamespace
		{
			get
			{
				return this._AtomXmlNamespace;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x000E28C2 File Offset: 0x000E0AC2
		internal string XmlnsNamespace
		{
			get
			{
				return this._AtomXmlnsNamespace;
			}
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckKeyword(string keyword)
		{
		}

		// Token: 0x04001D61 RID: 7521
		private string _AtomEmpty;

		// Token: 0x04001D62 RID: 7522
		private string _AtomLang;

		// Token: 0x04001D63 RID: 7523
		private string _AtomSpace;

		// Token: 0x04001D64 RID: 7524
		private string _AtomXmlns;

		// Token: 0x04001D65 RID: 7525
		private string _AtomXml;

		// Token: 0x04001D66 RID: 7526
		private string _AtomXmlNamespace;

		// Token: 0x04001D67 RID: 7527
		private string _AtomXmlnsNamespace;
	}
}
