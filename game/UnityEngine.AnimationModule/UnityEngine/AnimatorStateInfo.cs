using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	public struct AnimatorStateInfo
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00002674 File Offset: 0x00000874
		public bool IsName(string name)
		{
			int num = Animator.StringToHash(name);
			return num == this.m_FullPath || num == this.m_Name || num == this.m_Path;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000026AC File Offset: 0x000008AC
		public int fullPathHash
		{
			get
			{
				return this.m_FullPath;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000026C4 File Offset: 0x000008C4
		[Obsolete("AnimatorStateInfo.nameHash has been deprecated. Use AnimatorStateInfo.fullPathHash instead.")]
		public int nameHash
		{
			get
			{
				return this.m_Path;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000026DC File Offset: 0x000008DC
		public int shortNameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000026F4 File Offset: 0x000008F4
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000270C File Offset: 0x0000090C
		public float length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002724 File Offset: 0x00000924
		public float speed
		{
			get
			{
				return this.m_Speed;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000273C File Offset: 0x0000093C
		public float speedMultiplier
		{
			get
			{
				return this.m_SpeedMultiplier;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002754 File Offset: 0x00000954
		public int tagHash
		{
			get
			{
				return this.m_Tag;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000276C File Offset: 0x0000096C
		public bool IsTag(string tag)
		{
			return Animator.StringToHash(tag) == this.m_Tag;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000278C File Offset: 0x0000098C
		public bool loop
		{
			get
			{
				return this.m_Loop != 0;
			}
		}

		// Token: 0x04000055 RID: 85
		private int m_Name;

		// Token: 0x04000056 RID: 86
		private int m_Path;

		// Token: 0x04000057 RID: 87
		private int m_FullPath;

		// Token: 0x04000058 RID: 88
		private float m_NormalizedTime;

		// Token: 0x04000059 RID: 89
		private float m_Length;

		// Token: 0x0400005A RID: 90
		private float m_Speed;

		// Token: 0x0400005B RID: 91
		private float m_SpeedMultiplier;

		// Token: 0x0400005C RID: 92
		private int m_Tag;

		// Token: 0x0400005D RID: 93
		private int m_Loop;
	}
}
