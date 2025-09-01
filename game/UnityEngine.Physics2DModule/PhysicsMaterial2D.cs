using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003D RID: 61
	[NativeHeader("Modules/Physics2D/Public/PhysicsMaterial2D.h")]
	public sealed class PhysicsMaterial2D : Object
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x000086E9 File Offset: 0x000068E9
		public PhysicsMaterial2D()
		{
			PhysicsMaterial2D.Create_Internal(this, null);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000086FB File Offset: 0x000068FB
		public PhysicsMaterial2D(string name)
		{
			PhysicsMaterial2D.Create_Internal(this, name);
		}

		// Token: 0x060004AF RID: 1199
		[NativeMethod("Create_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Create_Internal([Writable] PhysicsMaterial2D scriptMaterial, string name);

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004B0 RID: 1200
		// (set) Token: 0x060004B1 RID: 1201
		public extern float bounciness { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004B2 RID: 1202
		// (set) Token: 0x060004B3 RID: 1203
		public extern float friction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
