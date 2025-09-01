using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/Physics/BoxCollider.h")]
	[RequiredByNativeCode]
	public class BoxCollider : Collider
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00005398 File Offset: 0x00003598
		// (set) Token: 0x06000307 RID: 775 RVA: 0x000053AE File Offset: 0x000035AE
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000053B8 File Offset: 0x000035B8
		// (set) Token: 0x06000309 RID: 777 RVA: 0x000053CE File Offset: 0x000035CE
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000053D8 File Offset: 0x000035D8
		// (set) Token: 0x0600030B RID: 779 RVA: 0x000053FA File Offset: 0x000035FA
		[Obsolete("Use BoxCollider.size instead. (UnityUpgradable) -> size")]
		public Vector3 extents
		{
			get
			{
				return this.size * 0.5f;
			}
			set
			{
				this.size = value * 2f;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000052FC File Offset: 0x000034FC
		public BoxCollider()
		{
		}

		// Token: 0x0600030D RID: 781
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x0600030E RID: 782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x0600030F RID: 783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3 ret);

		// Token: 0x06000310 RID: 784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3 value);
	}
}
