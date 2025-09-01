using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000020 RID: 32
	public struct TMP_Vertex
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0001744B File Offset: 0x0001564B
		public static TMP_Vertex zero
		{
			get
			{
				return TMP_Vertex.k_Zero;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00017452 File Offset: 0x00015652
		// Note: this type is marked as 'beforefieldinit'.
		static TMP_Vertex()
		{
		}

		// Token: 0x0400010B RID: 267
		public Vector3 position;

		// Token: 0x0400010C RID: 268
		public Vector2 uv;

		// Token: 0x0400010D RID: 269
		public Vector2 uv2;

		// Token: 0x0400010E RID: 270
		public Vector2 uv4;

		// Token: 0x0400010F RID: 271
		public Color32 color;

		// Token: 0x04000110 RID: 272
		private static readonly TMP_Vertex k_Zero;
	}
}
