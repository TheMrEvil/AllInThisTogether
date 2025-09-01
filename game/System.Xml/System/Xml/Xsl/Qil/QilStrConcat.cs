using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004CF RID: 1231
	internal class QilStrConcat : QilBinary
	{
		// Token: 0x06003216 RID: 12822 RVA: 0x0011FEDA File Offset: 0x0011E0DA
		public QilStrConcat(QilNodeType nodeType, QilNode delimiter, QilNode values) : base(nodeType, delimiter, values)
		{
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x0011FEE5 File Offset: 0x0011E0E5
		// (set) Token: 0x06003218 RID: 12824 RVA: 0x0011FEED File Offset: 0x0011E0ED
		public QilNode Delimiter
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

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x00120033 File Offset: 0x0011E233
		// (set) Token: 0x0600321A RID: 12826 RVA: 0x0011FF03 File Offset: 0x0011E103
		public QilNode Values
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
