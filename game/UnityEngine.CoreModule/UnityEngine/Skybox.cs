using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000157 RID: 343
	[NativeHeader("Runtime/Camera/Skybox.h")]
	public sealed class Skybox : Behaviour
	{
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000EA2 RID: 3746
		// (set) Token: 0x06000EA3 RID: 3747
		public extern Material material { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000084C0 File Offset: 0x000066C0
		public Skybox()
		{
		}
	}
}
