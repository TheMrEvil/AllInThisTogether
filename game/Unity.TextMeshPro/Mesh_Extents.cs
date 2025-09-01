using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public struct Mesh_Extents
	{
		// Token: 0x0600010F RID: 271 RVA: 0x000172AF File Offset: 0x000154AF
		public Mesh_Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000172C0 File Offset: 0x000154C0
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

		// Token: 0x040000BB RID: 187
		public Vector2 min;

		// Token: 0x040000BC RID: 188
		public Vector2 max;
	}
}
