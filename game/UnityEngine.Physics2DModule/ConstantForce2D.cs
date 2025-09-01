using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003C RID: 60
	[RequireComponent(typeof(Rigidbody2D))]
	[NativeHeader("Modules/Physics2D/ConstantForce2D.h")]
	public sealed class ConstantForce2D : PhysicsUpdateBehaviour2D
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000086A0 File Offset: 0x000068A0
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x000086B6 File Offset: 0x000068B6
		public Vector2 force
		{
			get
			{
				Vector2 result;
				this.get_force_Injected(out result);
				return result;
			}
			set
			{
				this.set_force_Injected(ref value);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000086C0 File Offset: 0x000068C0
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000086D6 File Offset: 0x000068D6
		public Vector2 relativeForce
		{
			get
			{
				Vector2 result;
				this.get_relativeForce_Injected(out result);
				return result;
			}
			set
			{
				this.set_relativeForce_Injected(ref value);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004A6 RID: 1190
		// (set) Token: 0x060004A7 RID: 1191
		public extern float torque { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060004A8 RID: 1192 RVA: 0x000086E0 File Offset: 0x000068E0
		public ConstantForce2D()
		{
		}

		// Token: 0x060004A9 RID: 1193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_force_Injected(out Vector2 ret);

		// Token: 0x060004AA RID: 1194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_force_Injected(ref Vector2 value);

		// Token: 0x060004AB RID: 1195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_relativeForce_Injected(out Vector2 ret);

		// Token: 0x060004AC RID: 1196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_relativeForce_Injected(ref Vector2 value);
	}
}
