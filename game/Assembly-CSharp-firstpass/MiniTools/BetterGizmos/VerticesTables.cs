using System;
using UnityEngine;

namespace MiniTools.BetterGizmos
{
	// Token: 0x02000168 RID: 360
	public static class VerticesTables
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0005DAB1 File Offset: 0x0005BCB1
		public static Vector3[] ArrowBottomVertices
		{
			get
			{
				return VerticesTables.arrowBottomVertices;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0005DAB8 File Offset: 0x0005BCB8
		public static Vector3[] ArrowTopVertices
		{
			get
			{
				return VerticesTables.arrowTopVertices;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0005DABF File Offset: 0x0005BCBF
		public static Vector3[] CubeVertices
		{
			get
			{
				return VerticesTables.cubeVertices;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0005DAC6 File Offset: 0x0005BCC6
		public static Vector3[] Circle32Vertices
		{
			get
			{
				return VerticesTables.circle32Vertices;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0005DACD File Offset: 0x0005BCCD
		public static Vector3[] Circle16Vertices
		{
			get
			{
				return VerticesTables.circle16Vertices;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0005DAD4 File Offset: 0x0005BCD4
		public static Vector3[] Circle8Vertices
		{
			get
			{
				return VerticesTables.circle8Vertices;
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0005DADC File Offset: 0x0005BCDC
		// Note: this type is marked as 'beforefieldinit'.
		static VerticesTables()
		{
		}

		// Token: 0x04000BC4 RID: 3012
		private static Vector3[] arrowBottomVertices = new Vector3[]
		{
			new Vector3(0.125f, 0f, 0.5f),
			new Vector3(0.125f, 0f, 0f),
			new Vector3(-0.125f, 0f, 0f),
			new Vector3(-0.125f, 0f, 0.5f)
		};

		// Token: 0x04000BC5 RID: 3013
		private static Vector3[] arrowTopVertices = new Vector3[]
		{
			new Vector3(-0.125f, 0f, -0.5f),
			new Vector3(-0.25f, 0f, -0.5f),
			new Vector3(0f, 0f, 0f),
			new Vector3(0.25f, 0f, -0.5f),
			new Vector3(0.125f, 0f, -0.5f)
		};

		// Token: 0x04000BC6 RID: 3014
		private static Vector3[] cubeVertices = new Vector3[]
		{
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, 0.5f)
		};

		// Token: 0x04000BC7 RID: 3015
		private static Vector3[] circle32Vertices = new Vector3[]
		{
			new Vector3(0.4903921f, 0.09754481f, 0f),
			new Vector3(0.4619393f, 0.19134128f, 0f),
			new Vector3(0.41573447f, 0.27778462f, 0f),
			new Vector3(0.35355318f, 0.35355285f, 0f),
			new Vector3(0.27778503f, 0.4157342f, 0f),
			new Vector3(0.19134174f, 0.46193922f, 0f),
			new Vector3(0.09754529f, 0.49039212f, 0f),
			new Vector3(2.2351742E-07f, 0.49999955f, 0f),
			new Vector3(-0.09754485f, 0.49039224f, 0f),
			new Vector3(-0.19134134f, 0.46193942f, 0f),
			new Vector3(-0.2777847f, 0.41573456f, 0f),
			new Vector3(-0.35355297f, 0.35355324f, 0f),
			new Vector3(-0.41573438f, 0.27778506f, 0f),
			new Vector3(-0.46193936f, 0.19134174f, 0f),
			new Vector3(-0.49039227f, 0.09754526f, 0f),
			new Vector3(-0.49999964f, 1.6391277E-07f, 0f),
			new Vector3(-0.4903924f, -0.09754495f, 0f),
			new Vector3(-0.46193957f, -0.19134147f, 0f),
			new Vector3(-0.41573468f, -0.27778485f, 0f),
			new Vector3(-0.35355332f, -0.35355312f, 0f),
			new Vector3(-0.2777851f, -0.41573456f, 0f),
			new Vector3(-0.19134173f, -0.46193954f, 0f),
			new Vector3(-0.097545214f, -0.49039245f, 0f),
			new Vector3(-8.195639E-08f, -0.49999985f, 0f),
			new Vector3(0.09754506f, -0.49039254f, 0f),
			new Vector3(0.19134161f, -0.4619397f, 0f),
			new Vector3(0.277785f, -0.4157348f, 0f),
			new Vector3(0.35355332f, -0.35355338f, 0f),
			new Vector3(0.41573474f, -0.27778512f, 0f),
			new Vector3(0.46193972f, -0.1913417f, 0f),
			new Vector3(0.4903926f, -0.09754517f, 0f),
			new Vector3(0.5f, 0f, 0f)
		};

		// Token: 0x04000BC8 RID: 3016
		private static Vector3[] circle16Vertices = new Vector3[]
		{
			new Vector3(0.4619393f, 0.19134128f, 0f),
			new Vector3(0.35355318f, 0.35355285f, 0f),
			new Vector3(0.19134174f, 0.46193922f, 0f),
			new Vector3(2.2351742E-07f, 0.49999955f, 0f),
			new Vector3(-0.19134134f, 0.46193942f, 0f),
			new Vector3(-0.35355297f, 0.35355324f, 0f),
			new Vector3(-0.46193936f, 0.19134174f, 0f),
			new Vector3(-0.49999964f, 1.6391277E-07f, 0f),
			new Vector3(-0.46193957f, -0.19134147f, 0f),
			new Vector3(-0.35355332f, -0.35355312f, 0f),
			new Vector3(-0.19134173f, -0.46193954f, 0f),
			new Vector3(-8.195639E-08f, -0.49999985f, 0f),
			new Vector3(0.19134161f, -0.4619397f, 0f),
			new Vector3(0.35355332f, -0.35355338f, 0f),
			new Vector3(0.46193972f, -0.1913417f, 0f),
			new Vector3(0.5f, 0f, 0f)
		};

		// Token: 0x04000BC9 RID: 3017
		private static Vector3[] circle8Vertices = new Vector3[]
		{
			new Vector3(0.35355318f, 0.35355285f, 0f),
			new Vector3(0f, 0.5f, 0f),
			new Vector3(-0.35355297f, 0.35355324f, 0f),
			new Vector3(-0.5f, 0f, 0f),
			new Vector3(-0.35355332f, -0.35355312f, 0f),
			new Vector3(--0f, -0.5f, 0f),
			new Vector3(0.35355332f, -0.35355338f, 0f),
			new Vector3(0.5f, 0f, 0f)
		};
	}
}
