using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000440 RID: 1088
	internal abstract class RtfNavigator : XPathNavigator
	{
		// Token: 0x06002AF8 RID: 11000
		public abstract void CopyToWriter(XmlWriter writer);

		// Token: 0x06002AF9 RID: 11001
		public abstract XPathNavigator ToNavigator();

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override XPathNodeType NodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string LocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string Prefix
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override XmlNameTable NameTable
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool MoveToFirstAttribute()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool MoveToNextAttribute()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool MoveToNext()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool MoveToPrevious()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool MoveToFirstChild()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool MoveToParent()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool MoveToId(string id)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x00005BD6 File Offset: 0x00003DD6
		public override bool IsSamePosition(XPathNavigator other)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x00088575 File Offset: 0x00086775
		protected RtfNavigator()
		{
		}
	}
}
