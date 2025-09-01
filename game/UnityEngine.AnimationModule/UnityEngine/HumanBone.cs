using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	[NativeType(CodegenOptions.Custom, "MonoHumanBone")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct HumanBone
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00003C20 File Offset: 0x00001E20
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00003C38 File Offset: 0x00001E38
		public string boneName
		{
			get
			{
				return this.m_BoneName;
			}
			set
			{
				this.m_BoneName = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00003C44 File Offset: 0x00001E44
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00003C5C File Offset: 0x00001E5C
		public string humanName
		{
			get
			{
				return this.m_HumanName;
			}
			set
			{
				this.m_HumanName = value;
			}
		}

		// Token: 0x04000113 RID: 275
		private string m_BoneName;

		// Token: 0x04000114 RID: 276
		private string m_HumanName;

		// Token: 0x04000115 RID: 277
		[NativeName("m_Limit")]
		public HumanLimit limit;
	}
}
