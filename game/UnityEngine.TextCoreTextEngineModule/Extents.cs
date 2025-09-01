using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000011 RID: 17
	internal struct Extents
	{
		// Token: 0x0600009E RID: 158 RVA: 0x0000603C File Offset: 0x0000423C
		public Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006050 File Offset: 0x00004250
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Min (",
				this.min.x.ToString("f2"),
				", ",
				this.min.y.ToString("f2"),
				")   Max (",
				this.max.x.ToString("f2"),
				", ",
				this.max.y.ToString("f2"),
				")"
			});
		}

		// Token: 0x0400006B RID: 107
		public Vector2 min;

		// Token: 0x0400006C RID: 108
		public Vector2 max;
	}
}
