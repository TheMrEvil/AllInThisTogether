using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000150 RID: 336
	[NativeHeader("Runtime/Camera/OcclusionArea.h")]
	public sealed class OcclusionArea : Component
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00012B28 File Offset: 0x00010D28
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x00012B3E File Offset: 0x00010D3E
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.get_center_Injected(out result);
				return result;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00012B48 File Offset: 0x00010D48
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x00012B5E File Offset: 0x00010D5E
		public Vector3 size
		{
			get
			{
				Vector3 result;
				this.get_size_Injected(out result);
				return result;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00010727 File Offset: 0x0000E927
		public OcclusionArea()
		{
		}

		// Token: 0x06000E20 RID: 3616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000E21 RID: 3617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x06000E22 RID: 3618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3 ret);

		// Token: 0x06000E23 RID: 3619
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3 value);
	}
}
