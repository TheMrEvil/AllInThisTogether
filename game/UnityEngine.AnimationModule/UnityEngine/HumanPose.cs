using System;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	public struct HumanPose
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00003EA8 File Offset: 0x000020A8
		internal void Init()
		{
			bool flag = this.muscles != null;
			if (flag)
			{
				bool flag2 = this.muscles.Length != HumanTrait.MuscleCount;
				if (flag2)
				{
					throw new InvalidOperationException("Bad array size for HumanPose.muscles. Size must equal HumanTrait.MuscleCount");
				}
			}
			bool flag3 = this.muscles == null;
			if (flag3)
			{
				this.muscles = new float[HumanTrait.MuscleCount];
				bool flag4 = this.bodyRotation.x == 0f && this.bodyRotation.y == 0f && this.bodyRotation.z == 0f && this.bodyRotation.w == 0f;
				if (flag4)
				{
					this.bodyRotation.w = 1f;
				}
			}
		}

		// Token: 0x04000133 RID: 307
		public Vector3 bodyPosition;

		// Token: 0x04000134 RID: 308
		public Quaternion bodyRotation;

		// Token: 0x04000135 RID: 309
		public float[] muscles;
	}
}
