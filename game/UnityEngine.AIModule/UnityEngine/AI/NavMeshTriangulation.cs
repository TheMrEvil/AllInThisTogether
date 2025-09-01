using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000F RID: 15
	[UsedByNativeCode]
	[MovedFrom("UnityEngine")]
	public struct NavMeshTriangulation
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002633 File Offset: 0x00000833
		[Obsolete("Use areas instead.")]
		public int[] layers
		{
			get
			{
				return this.areas;
			}
		}

		// Token: 0x0400001F RID: 31
		public Vector3[] vertices;

		// Token: 0x04000020 RID: 32
		public int[] indices;

		// Token: 0x04000021 RID: 33
		public int[] areas;
	}
}
