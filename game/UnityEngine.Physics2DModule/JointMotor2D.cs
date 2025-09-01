using System;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	public struct JointMotor2D
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00006C44 File Offset: 0x00004E44
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00006C5C File Offset: 0x00004E5C
		public float motorSpeed
		{
			get
			{
				return this.m_MotorSpeed;
			}
			set
			{
				this.m_MotorSpeed = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00006C68 File Offset: 0x00004E68
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00006C80 File Offset: 0x00004E80
		public float maxMotorTorque
		{
			get
			{
				return this.m_MaximumMotorTorque;
			}
			set
			{
				this.m_MaximumMotorTorque = value;
			}
		}

		// Token: 0x04000073 RID: 115
		private float m_MotorSpeed;

		// Token: 0x04000074 RID: 116
		private float m_MaximumMotorTorque;
	}
}
