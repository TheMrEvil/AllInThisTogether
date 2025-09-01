using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Physics2D/Public/CapsuleCollider2D.h")]
	public sealed class CapsuleCollider2D : Collider2D
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00008158 File Offset: 0x00006358
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000816E File Offset: 0x0000636E
		public Vector2 size
		{
			get
			{
				Vector2 result;
				this.get_size_Injected(out result);
				return result;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000371 RID: 881
		// (set) Token: 0x06000372 RID: 882
		public extern CapsuleDirection2D direction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000373 RID: 883 RVA: 0x0000814F File Offset: 0x0000634F
		public CapsuleCollider2D()
		{
		}

		// Token: 0x06000374 RID: 884
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector2 ret);

		// Token: 0x06000375 RID: 885
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector2 value);
	}
}
