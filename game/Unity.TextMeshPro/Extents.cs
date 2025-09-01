using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000019 RID: 25
	public struct Extents
	{
		// Token: 0x0600010C RID: 268 RVA: 0x000171BD File Offset: 0x000153BD
		public Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000171D0 File Offset: 0x000153D0
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

		// Token: 0x0600010E RID: 270 RVA: 0x00017271 File Offset: 0x00015471
		// Note: this type is marked as 'beforefieldinit'.
		static Extents()
		{
		}

		// Token: 0x040000B7 RID: 183
		internal static Extents zero = new Extents(Vector2.zero, Vector2.zero);

		// Token: 0x040000B8 RID: 184
		internal static Extents uninitialized = new Extents(new Vector2(32767f, 32767f), new Vector2(-32767f, -32767f));

		// Token: 0x040000B9 RID: 185
		public Vector2 min;

		// Token: 0x040000BA RID: 186
		public Vector2 max;
	}
}
