using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public struct VertexGradient
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0001705C File Offset: 0x0001525C
		public VertexGradient(Color color)
		{
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0001707A File Offset: 0x0001527A
		public VertexGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x040000A0 RID: 160
		public Color topLeft;

		// Token: 0x040000A1 RID: 161
		public Color topRight;

		// Token: 0x040000A2 RID: 162
		public Color bottomLeft;

		// Token: 0x040000A3 RID: 163
		public Color bottomRight;
	}
}
