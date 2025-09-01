using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[NativeAsStruct]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h")]
	[NativeType(CodegenOptions.Custom, "MonoAnimatorControllerParameter")]
	[NativeHeader("Modules/Animation/AnimatorControllerParameter.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class AnimatorControllerParameter
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00003654 File Offset: 0x00001854
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000366C File Offset: 0x0000186C
		public int nameHash
		{
			get
			{
				return Animator.StringToHash(this.m_Name);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000368C File Offset: 0x0000188C
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000036A4 File Offset: 0x000018A4
		public AnimatorControllerParameterType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000036B0 File Offset: 0x000018B0
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000036C8 File Offset: 0x000018C8
		public float defaultFloat
		{
			get
			{
				return this.m_DefaultFloat;
			}
			set
			{
				this.m_DefaultFloat = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000036D4 File Offset: 0x000018D4
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000036EC File Offset: 0x000018EC
		public int defaultInt
		{
			get
			{
				return this.m_DefaultInt;
			}
			set
			{
				this.m_DefaultInt = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000036F8 File Offset: 0x000018F8
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00003710 File Offset: 0x00001910
		public bool defaultBool
		{
			get
			{
				return this.m_DefaultBool;
			}
			set
			{
				this.m_DefaultBool = value;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000371C File Offset: 0x0000191C
		public override bool Equals(object o)
		{
			AnimatorControllerParameter animatorControllerParameter = o as AnimatorControllerParameter;
			return animatorControllerParameter != null && this.m_Name == animatorControllerParameter.m_Name && this.m_Type == animatorControllerParameter.m_Type && this.m_DefaultFloat == animatorControllerParameter.m_DefaultFloat && this.m_DefaultInt == animatorControllerParameter.m_DefaultInt && this.m_DefaultBool == animatorControllerParameter.m_DefaultBool;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00003788 File Offset: 0x00001988
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000037A5 File Offset: 0x000019A5
		public AnimatorControllerParameter()
		{
		}

		// Token: 0x04000068 RID: 104
		internal string m_Name = "";

		// Token: 0x04000069 RID: 105
		internal AnimatorControllerParameterType m_Type;

		// Token: 0x0400006A RID: 106
		internal float m_DefaultFloat;

		// Token: 0x0400006B RID: 107
		internal int m_DefaultInt;

		// Token: 0x0400006C RID: 108
		internal bool m_DefaultBool;
	}
}
