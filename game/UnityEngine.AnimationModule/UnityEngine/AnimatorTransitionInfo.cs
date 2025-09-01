using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001F RID: 31
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	[RequiredByNativeCode]
	public struct AnimatorTransitionInfo
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000027A8 File Offset: 0x000009A8
		public bool IsName(string name)
		{
			return Animator.StringToHash(name) == this.m_Name || Animator.StringToHash(name) == this.m_FullPath;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000027DC File Offset: 0x000009DC
		public bool IsUserName(string name)
		{
			return Animator.StringToHash(name) == this.m_UserName;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000027FC File Offset: 0x000009FC
		public int fullPathHash
		{
			get
			{
				return this.m_FullPath;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002814 File Offset: 0x00000A14
		public int nameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000282C File Offset: 0x00000A2C
		public int userNameHash
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002844 File Offset: 0x00000A44
		public DurationUnit durationUnit
		{
			get
			{
				return this.m_HasFixedDuration ? DurationUnit.Fixed : DurationUnit.Normalized;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002864 File Offset: 0x00000A64
		public float duration
		{
			get
			{
				return this.m_Duration;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000287C File Offset: 0x00000A7C
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002894 File Offset: 0x00000A94
		public bool anyState
		{
			get
			{
				return this.m_AnyState;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000028AC File Offset: 0x00000AAC
		internal bool entry
		{
			get
			{
				return (this.m_TransitionType & 2) != 0;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000028CC File Offset: 0x00000ACC
		internal bool exit
		{
			get
			{
				return (this.m_TransitionType & 4) != 0;
			}
		}

		// Token: 0x0400005E RID: 94
		[NativeName("fullPathHash")]
		private int m_FullPath;

		// Token: 0x0400005F RID: 95
		[NativeName("userNameHash")]
		private int m_UserName;

		// Token: 0x04000060 RID: 96
		[NativeName("nameHash")]
		private int m_Name;

		// Token: 0x04000061 RID: 97
		[NativeName("hasFixedDuration")]
		private bool m_HasFixedDuration;

		// Token: 0x04000062 RID: 98
		[NativeName("duration")]
		private float m_Duration;

		// Token: 0x04000063 RID: 99
		[NativeName("normalizedTime")]
		private float m_NormalizedTime;

		// Token: 0x04000064 RID: 100
		[NativeName("anyState")]
		private bool m_AnyState;

		// Token: 0x04000065 RID: 101
		[NativeName("transitionType")]
		private int m_TransitionType;
	}
}
