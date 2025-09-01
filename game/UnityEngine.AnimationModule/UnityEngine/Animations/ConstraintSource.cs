using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000064 RID: 100
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[NativeType(CodegenOptions = CodegenOptions.Custom, Header = "Modules/Animation/Constraints/ConstraintSource.h", IntermediateScriptingStructName = "MonoConstraintSource")]
	[UsedByNativeCode]
	[Serializable]
	public struct ConstraintSource
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00007B6C File Offset: 0x00005D6C
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00007B84 File Offset: 0x00005D84
		public Transform sourceTransform
		{
			get
			{
				return this.m_SourceTransform;
			}
			set
			{
				this.m_SourceTransform = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00007B90 File Offset: 0x00005D90
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x00007BA8 File Offset: 0x00005DA8
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
			set
			{
				this.m_Weight = value;
			}
		}

		// Token: 0x04000181 RID: 385
		[NativeName("sourceTransform")]
		private Transform m_SourceTransform;

		// Token: 0x04000182 RID: 386
		[NativeName("weight")]
		private float m_Weight;
	}
}
