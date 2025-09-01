using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C3 RID: 1219
	internal class QilLoop : QilBinary
	{
		// Token: 0x060030DE RID: 12510 RVA: 0x0011FEDA File Offset: 0x0011E0DA
		public QilLoop(QilNodeType nodeType, QilNode variable, QilNode body) : base(nodeType, variable, body)
		{
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060030DF RID: 12511 RVA: 0x00121988 File Offset: 0x0011FB88
		// (set) Token: 0x060030E0 RID: 12512 RVA: 0x0011FEED File Offset: 0x0011E0ED
		public QilIterator Variable
		{
			get
			{
				return (QilIterator)base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060030E1 RID: 12513 RVA: 0x00120033 File Offset: 0x0011E233
		// (set) Token: 0x060030E2 RID: 12514 RVA: 0x0011FF03 File Offset: 0x0011E103
		public QilNode Body
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
