using System;
using System.Diagnostics;
using System.Xml.XPath;
using System.Xml.Xsl.Xslt;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000393 RID: 915
	internal class NavigatorInput
	{
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x000E144F File Offset: 0x000DF64F
		// (set) Token: 0x0600250E RID: 9486 RVA: 0x000E1457 File Offset: 0x000DF657
		internal NavigatorInput Next
		{
			get
			{
				return this._Next;
			}
			set
			{
				this._Next = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x000E1460 File Offset: 0x000DF660
		internal string Href
		{
			get
			{
				return this._Href;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x000E1468 File Offset: 0x000DF668
		internal KeywordsTable Atoms
		{
			get
			{
				return this._Atoms;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x000E1470 File Offset: 0x000DF670
		internal XPathNavigator Navigator
		{
			get
			{
				return this._Navigator;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x000E1478 File Offset: 0x000DF678
		internal InputScopeManager InputScopeManager
		{
			get
			{
				return this._Manager;
			}
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000E1480 File Offset: 0x000DF680
		internal bool Advance()
		{
			return this._Navigator.MoveToNext();
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000E148D File Offset: 0x000DF68D
		internal bool Recurse()
		{
			return this._Navigator.MoveToFirstChild();
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000E149A File Offset: 0x000DF69A
		internal bool ToParent()
		{
			return this._Navigator.MoveToParent();
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000E14A7 File Offset: 0x000DF6A7
		internal void Close()
		{
			this._Navigator = null;
			this._PositionInfo = null;
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000E14B7 File Offset: 0x000DF6B7
		internal int LineNumber
		{
			get
			{
				return this._PositionInfo.LineNumber;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x000E14C4 File Offset: 0x000DF6C4
		internal int LinePosition
		{
			get
			{
				return this._PositionInfo.LinePosition;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000E14D1 File Offset: 0x000DF6D1
		internal XPathNodeType NodeType
		{
			get
			{
				return this._Navigator.NodeType;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x000E14DE File Offset: 0x000DF6DE
		internal string Name
		{
			get
			{
				return this._Navigator.Name;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x000E14EB File Offset: 0x000DF6EB
		internal string LocalName
		{
			get
			{
				return this._Navigator.LocalName;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x000E14F8 File Offset: 0x000DF6F8
		internal string NamespaceURI
		{
			get
			{
				return this._Navigator.NamespaceURI;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000E1505 File Offset: 0x000DF705
		internal string Prefix
		{
			get
			{
				return this._Navigator.Prefix;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x000E1512 File Offset: 0x000DF712
		internal string Value
		{
			get
			{
				return this._Navigator.Value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x000E151F File Offset: 0x000DF71F
		internal bool IsEmptyTag
		{
			get
			{
				return this._Navigator.IsEmptyElement;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x000E152C File Offset: 0x000DF72C
		internal string BaseURI
		{
			get
			{
				return this._Navigator.BaseURI;
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000E1539 File Offset: 0x000DF739
		internal bool MoveToFirstAttribute()
		{
			return this._Navigator.MoveToFirstAttribute();
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000E1546 File Offset: 0x000DF746
		internal bool MoveToNextAttribute()
		{
			return this._Navigator.MoveToNextAttribute();
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000E1553 File Offset: 0x000DF753
		internal bool MoveToFirstNamespace()
		{
			return this._Navigator.MoveToFirstNamespace(XPathNamespaceScope.ExcludeXml);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000E1561 File Offset: 0x000DF761
		internal bool MoveToNextNamespace()
		{
			return this._Navigator.MoveToNextNamespace(XPathNamespaceScope.ExcludeXml);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000E1570 File Offset: 0x000DF770
		internal NavigatorInput(XPathNavigator navigator, string baseUri, InputScope rootScope)
		{
			if (navigator == null)
			{
				throw new ArgumentNullException("navigator");
			}
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			this._Next = null;
			this._Href = baseUri;
			this._Atoms = new KeywordsTable(navigator.NameTable);
			this._Navigator = navigator;
			this._Manager = new InputScopeManager(this._Navigator, rootScope);
			this._PositionInfo = PositionInfo.GetPositionInfo(this._Navigator);
			if (this.NodeType == XPathNodeType.Root)
			{
				this._Navigator.MoveToFirstChild();
			}
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000E15FC File Offset: 0x000DF7FC
		internal NavigatorInput(XPathNavigator navigator) : this(navigator, navigator.BaseURI, null)
		{
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		internal void AssertInput()
		{
		}

		// Token: 0x04001D33 RID: 7475
		private XPathNavigator _Navigator;

		// Token: 0x04001D34 RID: 7476
		private PositionInfo _PositionInfo;

		// Token: 0x04001D35 RID: 7477
		private InputScopeManager _Manager;

		// Token: 0x04001D36 RID: 7478
		private NavigatorInput _Next;

		// Token: 0x04001D37 RID: 7479
		private string _Href;

		// Token: 0x04001D38 RID: 7480
		private KeywordsTable _Atoms;
	}
}
