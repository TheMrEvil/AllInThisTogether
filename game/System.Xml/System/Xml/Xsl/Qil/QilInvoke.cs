using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004BD RID: 1213
	internal class QilInvoke : QilBinary
	{
		// Token: 0x060030B6 RID: 12470 RVA: 0x0011FEDA File Offset: 0x0011E0DA
		public QilInvoke(QilNodeType nodeType, QilNode function, QilNode arguments) : base(nodeType, function, arguments)
		{
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x00121616 File Offset: 0x0011F816
		// (set) Token: 0x060030B8 RID: 12472 RVA: 0x0011FEED File Offset: 0x0011E0ED
		public QilFunction Function
		{
			get
			{
				return (QilFunction)base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x0011FEF6 File Offset: 0x0011E0F6
		// (set) Token: 0x060030BA RID: 12474 RVA: 0x0011FF03 File Offset: 0x0011E103
		public QilList Arguments
		{
			get
			{
				return (QilList)base.Right;
			}
			set
			{
				base.Right = value;
			}
		}
	}
}
