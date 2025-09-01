using System;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Modules/AI/Public/NavMeshBuildDebugSettings.h")]
	public struct NavMeshBuildDebugSettings
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000030AC File Offset: 0x000012AC
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000030C4 File Offset: 0x000012C4
		public NavMeshBuildDebugFlags flags
		{
			get
			{
				return (NavMeshBuildDebugFlags)this.m_Flags;
			}
			set
			{
				this.m_Flags = (byte)value;
			}
		}

		// Token: 0x0400005F RID: 95
		private byte m_Flags;
	}
}
