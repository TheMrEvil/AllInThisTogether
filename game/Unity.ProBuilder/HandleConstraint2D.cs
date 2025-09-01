using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200001B RID: 27
	internal sealed class HandleConstraint2D
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000102C0 File Offset: 0x0000E4C0
		public HandleConstraint2D(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000102D6 File Offset: 0x0000E4D6
		public HandleConstraint2D Inverse()
		{
			return new HandleConstraint2D((this.x == 1) ? 0 : 1, (this.y == 1) ? 0 : 1);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000102F7 File Offset: 0x0000E4F7
		public Vector2 Mask(Vector2 v)
		{
			v.x *= (float)this.x;
			v.y *= (float)this.y;
			return v;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00010320 File Offset: 0x0000E520
		public Vector2 InverseMask(Vector2 v)
		{
			v.x *= ((this.x == 1) ? 0f : 1f);
			v.y *= ((this.y == 1) ? 0f : 1f);
			return v;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0001036E File Offset: 0x0000E56E
		public static bool operator ==(HandleConstraint2D a, HandleConstraint2D b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0001038E File Offset: 0x0000E58E
		public static bool operator !=(HandleConstraint2D a, HandleConstraint2D b)
		{
			return a.x != b.x || a.y != b.y;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000103B1 File Offset: 0x0000E5B1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000103B9 File Offset: 0x0000E5B9
		public override bool Equals(object o)
		{
			return o is HandleConstraint2D && ((HandleConstraint2D)o).x == this.x && ((HandleConstraint2D)o).y == this.y;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000103EC File Offset: 0x0000E5EC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"(",
				this.x.ToString(),
				", ",
				this.y.ToString(),
				")"
			});
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00010438 File Offset: 0x0000E638
		// Note: this type is marked as 'beforefieldinit'.
		static HandleConstraint2D()
		{
		}

		// Token: 0x04000061 RID: 97
		public int x;

		// Token: 0x04000062 RID: 98
		public int y;

		// Token: 0x04000063 RID: 99
		public static readonly HandleConstraint2D None = new HandleConstraint2D(1, 1);
	}
}
