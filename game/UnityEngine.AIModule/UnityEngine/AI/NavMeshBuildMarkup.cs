using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x0200001B RID: 27
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	public struct NavMeshBuildMarkup
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00002DE0 File Offset: 0x00000FE0
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00002DFB File Offset: 0x00000FFB
		public bool overrideArea
		{
			get
			{
				return this.m_OverrideArea != 0;
			}
			set
			{
				this.m_OverrideArea = (value ? 1 : 0);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00002E0C File Offset: 0x0000100C
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00002E24 File Offset: 0x00001024
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00002E30 File Offset: 0x00001030
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00002E4B File Offset: 0x0000104B
		public bool ignoreFromBuild
		{
			get
			{
				return this.m_IgnoreFromBuild != 0;
			}
			set
			{
				this.m_IgnoreFromBuild = (value ? 1 : 0);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00002E5C File Offset: 0x0000105C
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00002E79 File Offset: 0x00001079
		public Transform root
		{
			get
			{
				return NavMeshBuildMarkup.InternalGetRootGO(this.m_InstanceID);
			}
			set
			{
				this.m_InstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x06000149 RID: 329
		[StaticAccessor("NavMeshBuildMarkup", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Transform InternalGetRootGO(int instanceID);

		// Token: 0x0400004B RID: 75
		private int m_OverrideArea;

		// Token: 0x0400004C RID: 76
		private int m_Area;

		// Token: 0x0400004D RID: 77
		private int m_IgnoreFromBuild;

		// Token: 0x0400004E RID: 78
		private int m_InstanceID;
	}
}
