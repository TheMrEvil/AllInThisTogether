using System;

namespace System.Xml.Xsl
{
	// Token: 0x02000336 RID: 822
	internal class XmlQualifiedNameTest : XmlQualifiedName
	{
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x000D6909 File Offset: 0x000D4B09
		public static XmlQualifiedNameTest Wildcard
		{
			get
			{
				return XmlQualifiedNameTest.wc;
			}
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000D6910 File Offset: 0x000D4B10
		private XmlQualifiedNameTest(string name, string ns, bool exclude) : base(name, ns)
		{
			this.exclude = exclude;
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000D6921 File Offset: 0x000D4B21
		public static XmlQualifiedNameTest New(string name, string ns)
		{
			if (ns == null && name == null)
			{
				return XmlQualifiedNameTest.Wildcard;
			}
			return new XmlQualifiedNameTest((name == null) ? "*" : name, (ns == null) ? "*" : ns, false);
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x000D694B File Offset: 0x000D4B4B
		public bool IsWildcard
		{
			get
			{
				return this == XmlQualifiedNameTest.Wildcard;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x000D6955 File Offset: 0x000D4B55
		public bool IsNameWildcard
		{
			get
			{
				return base.Name == "*";
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x000D6964 File Offset: 0x000D4B64
		public bool IsNamespaceWildcard
		{
			get
			{
				return base.Namespace == "*";
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000D6973 File Offset: 0x000D4B73
		private bool IsNameSubsetOf(XmlQualifiedNameTest other)
		{
			return other.IsNameWildcard || base.Name == other.Name;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000D6990 File Offset: 0x000D4B90
		private bool IsNamespaceSubsetOf(XmlQualifiedNameTest other)
		{
			return other.IsNamespaceWildcard || (this.exclude == other.exclude && base.Namespace == other.Namespace) || (other.exclude && !this.exclude && base.Namespace != other.Namespace);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000D69EB File Offset: 0x000D4BEB
		public bool IsSubsetOf(XmlQualifiedNameTest other)
		{
			return this.IsNameSubsetOf(other) && this.IsNamespaceSubsetOf(other);
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000D69FF File Offset: 0x000D4BFF
		public bool HasIntersection(XmlQualifiedNameTest other)
		{
			return (this.IsNamespaceSubsetOf(other) || other.IsNamespaceSubsetOf(this)) && (this.IsNameSubsetOf(other) || other.IsNameSubsetOf(this));
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000D6A28 File Offset: 0x000D4C28
		public override string ToString()
		{
			if (this == XmlQualifiedNameTest.Wildcard)
			{
				return "*";
			}
			if (base.Namespace.Length == 0)
			{
				return base.Name;
			}
			if (base.Namespace == "*")
			{
				return "*:" + base.Name;
			}
			if (this.exclude)
			{
				return "{~" + base.Namespace + "}:" + base.Name;
			}
			return "{" + base.Namespace + "}:" + base.Name;
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000D6AB4 File Offset: 0x000D4CB4
		// Note: this type is marked as 'beforefieldinit'.
		static XmlQualifiedNameTest()
		{
		}

		// Token: 0x04001BC3 RID: 7107
		private bool exclude;

		// Token: 0x04001BC4 RID: 7108
		private const string wildcard = "*";

		// Token: 0x04001BC5 RID: 7109
		private static XmlQualifiedNameTest wc = XmlQualifiedNameTest.New("*", "*");
	}
}
