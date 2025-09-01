using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200014F RID: 335
	[NativeHeader("Runtime/Camera/OcclusionPortal.h")]
	public sealed class OcclusionPortal : Component
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000E18 RID: 3608
		// (set) Token: 0x06000E19 RID: 3609
		[NativeProperty("IsOpen")]
		public extern bool open { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000E1A RID: 3610 RVA: 0x00010727 File Offset: 0x0000E927
		public OcclusionPortal()
		{
		}
	}
}
