using System;
using UnityEngine;

namespace ECE
{
	// Token: 0x0200006E RID: 110
	public class MeshColliderData : EasyColliderData
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x00022330 File Offset: 0x00020530
		public void Clone(MeshColliderData data)
		{
			base.Clone(data);
			this.ConvexMesh = data.ConvexMesh;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00022345 File Offset: 0x00020545
		public MeshColliderData()
		{
		}

		// Token: 0x0400041F RID: 1055
		public Mesh ConvexMesh;
	}
}
