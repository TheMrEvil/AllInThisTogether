using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/Physics2D/Public/BoxCollider2D.h")]
	public sealed class BoxCollider2D : Collider2D
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600038C RID: 908 RVA: 0x000081B8 File Offset: 0x000063B8
		// (set) Token: 0x0600038D RID: 909 RVA: 0x000081CE File Offset: 0x000063CE
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600038E RID: 910
		// (set) Token: 0x0600038F RID: 911
		public extern float edgeRadius { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000390 RID: 912
		// (set) Token: 0x06000391 RID: 913
		public extern bool autoTiling { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000392 RID: 914 RVA: 0x0000814F File Offset: 0x0000634F
		public BoxCollider2D()
		{
		}

		// Token: 0x06000393 RID: 915
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector2 ret);

		// Token: 0x06000394 RID: 916
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector2 value);
	}
}
