using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	[NativeHeader("Modules/Physics2D/RelativeJoint2D.h")]
	public sealed class RelativeJoint2D : Joint2D
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000400 RID: 1024
		// (set) Token: 0x06000401 RID: 1025
		public extern float maxForce { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000402 RID: 1026
		// (set) Token: 0x06000403 RID: 1027
		public extern float maxTorque { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000404 RID: 1028
		// (set) Token: 0x06000405 RID: 1029
		public extern float correctionScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000406 RID: 1030
		// (set) Token: 0x06000407 RID: 1031
		public extern bool autoConfigureOffset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000859C File Offset: 0x0000679C
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x000085B2 File Offset: 0x000067B2
		public Vector2 linearOffset
		{
			get
			{
				Vector2 result;
				this.get_linearOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_linearOffset_Injected(ref value);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600040A RID: 1034
		// (set) Token: 0x0600040B RID: 1035
		public extern float angularOffset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x000085BC File Offset: 0x000067BC
		public Vector2 target
		{
			get
			{
				Vector2 result;
				this.get_target_Injected(out result);
				return result;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00008548 File Offset: 0x00006748
		public RelativeJoint2D()
		{
		}

		// Token: 0x0600040E RID: 1038
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_linearOffset_Injected(out Vector2 ret);

		// Token: 0x0600040F RID: 1039
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_linearOffset_Injected(ref Vector2 value);

		// Token: 0x06000410 RID: 1040
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_target_Injected(out Vector2 ret);
	}
}
