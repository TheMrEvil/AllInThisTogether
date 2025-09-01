using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000151 RID: 337
	[NativeHeader("Runtime/Camera/Flare.h")]
	public sealed class Flare : Object
	{
		// Token: 0x06000E24 RID: 3620 RVA: 0x00012B68 File Offset: 0x00010D68
		public Flare()
		{
			Flare.Internal_Create(this);
		}

		// Token: 0x06000E25 RID: 3621
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Flare self);
	}
}
