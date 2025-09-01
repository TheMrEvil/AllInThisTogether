using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/Cloth/Cloth.h")]
	[UsedByNativeCode]
	public struct ClothSphereColliderPair
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002059 File Offset: 0x00000259
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002061 File Offset: 0x00000261
		public SphereCollider first
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<first>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<first>k__BackingField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000206A File Offset: 0x0000026A
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002072 File Offset: 0x00000272
		public SphereCollider second
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<second>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<second>k__BackingField = value;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000207B File Offset: 0x0000027B
		public ClothSphereColliderPair(SphereCollider a)
		{
			this.first = a;
			this.second = null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000208E File Offset: 0x0000028E
		public ClothSphereColliderPair(SphereCollider a, SphereCollider b)
		{
			this.first = a;
			this.second = b;
		}

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SphereCollider <first>k__BackingField;

		// Token: 0x04000002 RID: 2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private SphereCollider <second>k__BackingField;
	}
}
