using System;

namespace System.Xml.XPath
{
	// Token: 0x02000260 RID: 608
	internal class XmlEmptyNavigator : XPathNavigator
	{
		// Token: 0x060016D2 RID: 5842 RVA: 0x00088575 File Offset: 0x00086775
		private XmlEmptyNavigator()
		{
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0008857D File Offset: 0x0008677D
		public static XmlEmptyNavigator Singleton
		{
			get
			{
				if (XmlEmptyNavigator.singleton == null)
				{
					XmlEmptyNavigator.singleton = new XmlEmptyNavigator();
				}
				return XmlEmptyNavigator.singleton;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0006807E File Offset: 0x0006627E
		public override XPathNodeType NodeType
		{
			get
			{
				return XPathNodeType.All;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string LocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string Prefix
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string BaseURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string Value
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string XmlLang
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool HasAttributes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x0008859B File Offset: 0x0008679B
		public override XmlNameTable NameTable
		{
			get
			{
				return new NameTable();
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToFirstChild()
		{
			return false;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0000B528 File Offset: 0x00009728
		public override void MoveToRoot()
		{
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToNext()
		{
			return false;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToPrevious()
		{
			return false;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToFirst()
		{
			return false;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToFirstAttribute()
		{
			return false;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToNextAttribute()
		{
			return false;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToId(string id)
		{
			return false;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override string GetAttribute(string localName, string namespaceName)
		{
			return null;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToAttribute(string localName, string namespaceName)
		{
			return false;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override string GetNamespace(string name)
		{
			return null;
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToNamespace(string prefix)
		{
			return false;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToFirstNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToNextNamespace(XPathNamespaceScope scope)
		{
			return false;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool MoveToParent()
		{
			return false;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00071147 File Offset: 0x0006F347
		public override bool MoveTo(XPathNavigator other)
		{
			return this == other;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000885A2 File Offset: 0x000867A2
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			if (this != other)
			{
				return XmlNodeOrder.Unknown;
			}
			return XmlNodeOrder.Same;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00071147 File Offset: 0x0006F347
		public override bool IsSamePosition(XPathNavigator other)
		{
			return this == other;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00002068 File Offset: 0x00000268
		public override XPathNavigator Clone()
		{
			return this;
		}

		// Token: 0x04001823 RID: 6179
		private static volatile XmlEmptyNavigator singleton;
	}
}
