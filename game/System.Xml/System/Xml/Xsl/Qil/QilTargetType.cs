using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004D0 RID: 1232
	internal class QilTargetType : QilBinary
	{
		// Token: 0x0600321B RID: 12827 RVA: 0x0011FEDA File Offset: 0x0011E0DA
		public QilTargetType(QilNodeType nodeType, QilNode expr, QilNode targetType) : base(nodeType, expr, targetType)
		{
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x0011FEE5 File Offset: 0x0011E0E5
		// (set) Token: 0x0600321D RID: 12829 RVA: 0x0011FEED File Offset: 0x0011E0ED
		public QilNode Source
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

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x00122B57 File Offset: 0x00120D57
		// (set) Token: 0x0600321F RID: 12831 RVA: 0x00122B6E File Offset: 0x00120D6E
		public XmlQueryType TargetType
		{
			get
			{
				return (XmlQueryType)((QilLiteral)base.Right).Value;
			}
			set
			{
				((QilLiteral)base.Right).Value = value;
			}
		}
	}
}
