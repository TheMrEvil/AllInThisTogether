using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004D3 RID: 1235
	internal class QilUnary : QilNode
	{
		// Token: 0x060032A2 RID: 12962 RVA: 0x001239AF File Offset: 0x00121BAF
		public QilUnary(QilNodeType nodeType, QilNode child) : base(nodeType)
		{
			this._child = child;
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x0001222F File Offset: 0x0001042F
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008FC RID: 2300
		public override QilNode this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this._child;
			}
			set
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				this._child = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x001239E2 File Offset: 0x00121BE2
		// (set) Token: 0x060032A7 RID: 12967 RVA: 0x001239EA File Offset: 0x00121BEA
		public QilNode Child
		{
			get
			{
				return this._child;
			}
			set
			{
				this._child = value;
			}
		}

		// Token: 0x0400264F RID: 9807
		private QilNode _child;
	}
}
