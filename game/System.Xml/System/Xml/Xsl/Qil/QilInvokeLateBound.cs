using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004BF RID: 1215
	internal class QilInvokeLateBound : QilBinary
	{
		// Token: 0x060030C2 RID: 12482 RVA: 0x0011FEDA File Offset: 0x0011E0DA
		public QilInvokeLateBound(QilNodeType nodeType, QilNode name, QilNode arguments) : base(nodeType, name, arguments)
		{
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x0012168E File Offset: 0x0011F88E
		// (set) Token: 0x060030C4 RID: 12484 RVA: 0x0011FEED File Offset: 0x0011E0ED
		public QilName Name
		{
			get
			{
				return (QilName)base.Left;
			}
			set
			{
				base.Left = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060030C5 RID: 12485 RVA: 0x0011FEF6 File Offset: 0x0011E0F6
		// (set) Token: 0x060030C6 RID: 12486 RVA: 0x0011FF03 File Offset: 0x0011E103
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
