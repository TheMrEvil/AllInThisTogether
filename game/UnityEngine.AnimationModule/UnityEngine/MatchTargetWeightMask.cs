using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000020 RID: 32
	[NativeHeader("Modules/Animation/Animator.h")]
	public struct MatchTargetWeightMask
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000028E9 File Offset: 0x00000AE9
		public MatchTargetWeightMask(Vector3 positionXYZWeight, float rotationWeight)
		{
			this.m_PositionXYZWeight = positionXYZWeight;
			this.m_RotationWeight = rotationWeight;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000028FC File Offset: 0x00000AFC
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002914 File Offset: 0x00000B14
		public Vector3 positionXYZWeight
		{
			get
			{
				return this.m_PositionXYZWeight;
			}
			set
			{
				this.m_PositionXYZWeight = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002920 File Offset: 0x00000B20
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002938 File Offset: 0x00000B38
		public float rotationWeight
		{
			get
			{
				return this.m_RotationWeight;
			}
			set
			{
				this.m_RotationWeight = value;
			}
		}

		// Token: 0x04000066 RID: 102
		private Vector3 m_PositionXYZWeight;

		// Token: 0x04000067 RID: 103
		private float m_RotationWeight;
	}
}
