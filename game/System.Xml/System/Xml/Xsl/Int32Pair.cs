using System;

namespace System.Xml.Xsl
{
	// Token: 0x02000329 RID: 809
	internal struct Int32Pair
	{
		// Token: 0x06002148 RID: 8520 RVA: 0x000D2A55 File Offset: 0x000D0C55
		public Int32Pair(int left, int right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x000D2A65 File Offset: 0x000D0C65
		public int Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x000D2A6D File Offset: 0x000D0C6D
		public int Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x000D2A78 File Offset: 0x000D0C78
		public override bool Equals(object other)
		{
			if (other is Int32Pair)
			{
				Int32Pair int32Pair = (Int32Pair)other;
				return this.left == int32Pair.left && this.right == int32Pair.right;
			}
			return false;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000D2AB4 File Offset: 0x000D0CB4
		public override int GetHashCode()
		{
			return this.left.GetHashCode() ^ this.right.GetHashCode();
		}

		// Token: 0x04001B8B RID: 7051
		private int left;

		// Token: 0x04001B8C RID: 7052
		private int right;
	}
}
