using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000028 RID: 40
	internal sealed class MeshHandle
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00013894 File Offset: 0x00011A94
		public Mesh mesh
		{
			get
			{
				return this.m_Mesh;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0001389C File Offset: 0x00011A9C
		public MeshHandle(Transform transform, Mesh mesh)
		{
			this.m_Transform = transform;
			this.m_Mesh = mesh;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000138B2 File Offset: 0x00011AB2
		public void DrawMeshNow(int submeshIndex)
		{
			if (this.m_Transform == null || this.m_Mesh == null)
			{
				return;
			}
			Graphics.DrawMeshNow(this.m_Mesh, this.m_Transform.localToWorldMatrix, submeshIndex);
		}

		// Token: 0x04000084 RID: 132
		private Transform m_Transform;

		// Token: 0x04000085 RID: 133
		private Mesh m_Mesh;
	}
}
