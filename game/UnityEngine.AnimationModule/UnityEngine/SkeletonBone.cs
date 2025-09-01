using System;
using System.ComponentModel;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[NativeType(CodegenOptions.Custom, "MonoSkeletonBone")]
	public struct SkeletonBone
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00003B50 File Offset: 0x00001D50
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00002059 File Offset: 0x00000259
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("transformModified is no longer used and has been deprecated.", true)]
		public int transformModified
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x04000109 RID: 265
		[NativeName("m_Name")]
		public string name;

		// Token: 0x0400010A RID: 266
		[NativeName("m_ParentName")]
		internal string parentName;

		// Token: 0x0400010B RID: 267
		[NativeName("m_Position")]
		public Vector3 position;

		// Token: 0x0400010C RID: 268
		[NativeName("m_Rotation")]
		public Quaternion rotation;

		// Token: 0x0400010D RID: 269
		[NativeName("m_Scale")]
		public Vector3 scale;
	}
}
