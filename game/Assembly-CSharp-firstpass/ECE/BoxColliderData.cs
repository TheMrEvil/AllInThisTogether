using System;
using UnityEngine;

namespace ECE
{
	// Token: 0x0200006D RID: 109
	public class BoxColliderData : EasyColliderData
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x000222C8 File Offset: 0x000204C8
		public void Clone(BoxColliderData data)
		{
			base.Clone(data);
			this.Center = data.Center;
			this.Size = data.Size;
			this.Matrix = data.Matrix;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000222F5 File Offset: 0x000204F5
		public override string ToString()
		{
			return "Rotated box collider. Center:" + this.Center.ToString() + " Size:" + this.Size.ToString();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00022328 File Offset: 0x00020528
		public BoxColliderData()
		{
		}

		// Token: 0x0400041D RID: 1053
		public Vector3 Center;

		// Token: 0x0400041E RID: 1054
		public Vector3 Size;
	}
}
