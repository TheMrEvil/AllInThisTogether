using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004B9 RID: 1209
	internal class QilDataSource : QilBinary
	{
		// Token: 0x0600301C RID: 12316 RVA: 0x0011FEDA File Offset: 0x0011E0DA
		public QilDataSource(QilNodeType nodeType, QilNode name, QilNode baseUri) : base(nodeType, name, baseUri)
		{
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x0011FEE5 File Offset: 0x0011E0E5
		// (set) Token: 0x0600301E RID: 12318 RVA: 0x0011FEED File Offset: 0x0011E0ED
		public QilNode Name
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

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x00120033 File Offset: 0x0011E233
		// (set) Token: 0x06003020 RID: 12320 RVA: 0x0011FF03 File Offset: 0x0011E103
		public QilNode BaseUri
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
