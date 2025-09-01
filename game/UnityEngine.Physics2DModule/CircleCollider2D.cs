using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[NativeHeader("Modules/Physics2D/Public/CircleCollider2D.h")]
	public sealed class CircleCollider2D : Collider2D
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600036C RID: 876
		// (set) Token: 0x0600036D RID: 877
		public extern float radius { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600036E RID: 878 RVA: 0x0000814F File Offset: 0x0000634F
		public CircleCollider2D()
		{
		}
	}
}
