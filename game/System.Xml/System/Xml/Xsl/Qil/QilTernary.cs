using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004D1 RID: 1233
	internal class QilTernary : QilNode
	{
		// Token: 0x06003220 RID: 12832 RVA: 0x00122B81 File Offset: 0x00120D81
		public QilTernary(QilNodeType nodeType, QilNode left, QilNode center, QilNode right) : base(nodeType)
		{
			this._left = left;
			this._center = center;
			this._right = right;
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override int Count
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170008F7 RID: 2295
		public override QilNode this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this._left;
				case 1:
					return this._center;
				case 2:
					return this._right;
				default:
					throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this._left = value;
					return;
				case 1:
					this._center = value;
					return;
				case 2:
					this._right = value;
					return;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x00122C03 File Offset: 0x00120E03
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x00122C0B File Offset: 0x00120E0B
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

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x00122C14 File Offset: 0x00120E14
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x00122C1C File Offset: 0x00120E1C
		public QilNode Center
		{
			get
			{
				return this._center;
			}
			set
			{
				this._center = value;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x00122C25 File Offset: 0x00120E25
		// (set) Token: 0x06003229 RID: 12841 RVA: 0x00122C2D File Offset: 0x00120E2D
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

		// Token: 0x0400264C RID: 9804
		private QilNode _left;

		// Token: 0x0400264D RID: 9805
		private QilNode _center;

		// Token: 0x0400264E RID: 9806
		private QilNode _right;
	}
}
