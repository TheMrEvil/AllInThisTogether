using System;

namespace ECE
{
	// Token: 0x0200006C RID: 108
	public class CapsuleColliderData : SphereColliderData
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x0002229F File Offset: 0x0002049F
		public void Clone(CapsuleColliderData data)
		{
			base.Clone(data);
			this.Direction = data.Direction;
			this.Height = data.Height;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000222C0 File Offset: 0x000204C0
		public CapsuleColliderData()
		{
		}

		// Token: 0x0400041B RID: 1051
		public int Direction;

		// Token: 0x0400041C RID: 1052
		public float Height;
	}
}
