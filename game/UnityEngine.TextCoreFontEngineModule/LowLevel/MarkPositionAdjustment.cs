using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000022 RID: 34
	[UsedByNativeCode]
	[Serializable]
	internal struct MarkPositionAdjustment
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004F10 File Offset: 0x00003110
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00004F28 File Offset: 0x00003128
		public float xPositionAdjustment
		{
			get
			{
				return this.m_XPositionAdjustment;
			}
			set
			{
				this.m_XPositionAdjustment = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004F34 File Offset: 0x00003134
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00004F4C File Offset: 0x0000314C
		public float yPositionAdjustment
		{
			get
			{
				return this.m_YPositionAdjustment;
			}
			set
			{
				this.m_YPositionAdjustment = value;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004F56 File Offset: 0x00003156
		public MarkPositionAdjustment(float x, float y)
		{
			this.m_XPositionAdjustment = x;
			this.m_YPositionAdjustment = y;
		}

		// Token: 0x040000C6 RID: 198
		[NativeName("xCoordinate")]
		[SerializeField]
		private float m_XPositionAdjustment;

		// Token: 0x040000C7 RID: 199
		[SerializeField]
		[NativeName("yCoordinate")]
		private float m_YPositionAdjustment;
	}
}
