using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004B7 RID: 1207
	internal class QilChoice : QilBinary
	{
		// Token: 0x0600300E RID: 12302 RVA: 0x0011FEDA File Offset: 0x0011E0DA
		public QilChoice(QilNodeType nodeType, QilNode expression, QilNode branches) : base(nodeType, expression, branches)
		{
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x0011FEE5 File Offset: 0x0011E0E5
		// (set) Token: 0x06003010 RID: 12304 RVA: 0x0011FEED File Offset: 0x0011E0ED
		public QilNode Expression
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

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x0011FEF6 File Offset: 0x0011E0F6
		// (set) Token: 0x06003012 RID: 12306 RVA: 0x0011FF03 File Offset: 0x0011E103
		public QilList Branches
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
