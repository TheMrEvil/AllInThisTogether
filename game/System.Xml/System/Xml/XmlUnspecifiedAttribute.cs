using System;

namespace System.Xml
{
	// Token: 0x020001DC RID: 476
	internal class XmlUnspecifiedAttribute : XmlAttribute
	{
		// Token: 0x060012F2 RID: 4850 RVA: 0x000709BB File Offset: 0x0006EBBB
		protected internal XmlUnspecifiedAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc) : base(prefix, localName, namespaceURI, doc)
		{
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000709C8 File Offset: 0x0006EBC8
		public override bool Specified
		{
			get
			{
				return this.fSpecified;
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000709D0 File Offset: 0x0006EBD0
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlUnspecifiedAttribute xmlUnspecifiedAttribute = (XmlUnspecifiedAttribute)ownerDocument.CreateDefaultAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlUnspecifiedAttribute.CopyChildren(ownerDocument, this, true);
			xmlUnspecifiedAttribute.fSpecified = true;
			return xmlUnspecifiedAttribute;
		}

		// Token: 0x1700039D RID: 925
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00070A11 File Offset: 0x0006EC11
		public override string InnerText
		{
			set
			{
				base.InnerText = value;
				this.fSpecified = true;
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00070A21 File Offset: 0x0006EC21
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result = base.InsertBefore(newChild, refChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00070A32 File Offset: 0x0006EC32
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result = base.InsertAfter(newChild, refChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00070A43 File Offset: 0x0006EC43
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode result = base.ReplaceChild(newChild, oldChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00070A54 File Offset: 0x0006EC54
		public override XmlNode RemoveChild(XmlNode oldChild)
		{
			XmlNode result = base.RemoveChild(oldChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00070A64 File Offset: 0x0006EC64
		public override XmlNode AppendChild(XmlNode newChild)
		{
			XmlNode result = base.AppendChild(newChild);
			this.fSpecified = true;
			return result;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00070A74 File Offset: 0x0006EC74
		public override void WriteTo(XmlWriter w)
		{
			if (this.fSpecified)
			{
				base.WriteTo(w);
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00070A85 File Offset: 0x0006EC85
		internal void SetSpecified(bool f)
		{
			this.fSpecified = f;
		}

		// Token: 0x040010E5 RID: 4325
		private bool fSpecified;
	}
}
