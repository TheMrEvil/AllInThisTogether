using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[ExcludeFromPreset]
	[NativeHeader("Modules/Terrain/Public/Tree.h")]
	public sealed class Tree : Component
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007A RID: 122
		// (set) Token: 0x0600007B RID: 123
		[NativeProperty("TreeData")]
		public extern ScriptableObject data { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007C RID: 124
		public extern bool hasSpeedTreeWind { [NativeMethod("HasSpeedTreeWind")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600007D RID: 125 RVA: 0x0000234B File Offset: 0x0000054B
		public Tree()
		{
		}
	}
}
