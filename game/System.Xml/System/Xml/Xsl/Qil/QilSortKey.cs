using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004CE RID: 1230
	internal class QilSortKey : QilBinary
	{
		// Token: 0x06003211 RID: 12817 RVA: 0x0011FEDA File Offset: 0x0011E0DA
		public QilSortKey(QilNodeType nodeType, QilNode key, QilNode collation) : base(nodeType, key, collation)
		{
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x0011FEE5 File Offset: 0x0011E0E5
		// (set) Token: 0x06003213 RID: 12819 RVA: 0x0011FEED File Offset: 0x0011E0ED
		public QilNode Key
		{
			get
			{
				return base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x00120033 File Offset: 0x0011E233
		// (set) Token: 0x06003215 RID: 12821 RVA: 0x0011FF03 File Offset: 0x0011E103
		public QilNode Collation
		{
			get
			{
				return base.Right;
			}
			set
			{
				base.Right = value;
			}
		}
	}
}
