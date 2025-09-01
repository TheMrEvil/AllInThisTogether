using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004B6 RID: 1206
	internal class QilBinary : QilNode
	{
		// Token: 0x06003006 RID: 12294 RVA: 0x0011FE63 File Offset: 0x0011E063
		public QilBinary(QilNodeType nodeType, QilNode left, QilNode right) : base(nodeType)
		{
			this._left = left;
			this._right = right;
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x00066748 File Offset: 0x00064948
		public override int Count
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170008B1 RID: 2225
		public override QilNode this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this._left;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				return this._right;
			}
			set
			{
				if (index == 0)
				{
					this._left = value;
					return;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException();
				}
				this._right = value;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x0011FEB8 File Offset: 0x0011E0B8
		// (set) Token: 0x0600300B RID: 12299 RVA: 0x0011FEC0 File Offset: 0x0011E0C0
		public QilNode Left
		{
			get
			{
				return this._left;
			}
			set
			{
				this._left = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x0011FEC9 File Offset: 0x0011E0C9
		// (set) Token: 0x0600300D RID: 12301 RVA: 0x0011FED1 File Offset: 0x0011E0D1
		public QilNode Right
		{
			get
			{
				return this._right;
			}
			set
			{
				this._right = value;
			}
		}

		// Token: 0x040025B9 RID: 9657
		private QilNode _left;

		// Token: 0x040025BA RID: 9658
		private QilNode _right;
	}
}
