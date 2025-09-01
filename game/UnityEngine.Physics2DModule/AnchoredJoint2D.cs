using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	[NativeHeader("Modules/Physics2D/AnchoredJoint2D.h")]
	public class AnchoredJoint2D : Joint2D
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00008508 File Offset: 0x00006708
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000851E File Offset: 0x0000671E
		public Vector2 anchor
		{
			get
			{
				Vector2 result;
				this.get_anchor_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchor_Injected(ref value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00008528 File Offset: 0x00006728
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000853E File Offset: 0x0000673E
		public Vector2 connectedAnchor
		{
			get
			{
				Vector2 result;
				this.get_connectedAnchor_Injected(out result);
				return result;
			}
			set
			{
				this.set_connectedAnchor_Injected(ref value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003D2 RID: 978
		// (set) Token: 0x060003D3 RID: 979
		public extern bool autoConfigureConnectedAnchor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060003D4 RID: 980 RVA: 0x00008548 File Offset: 0x00006748
		public AnchoredJoint2D()
		{
		}

		// Token: 0x060003D5 RID: 981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchor_Injected(out Vector2 ret);

		// Token: 0x060003D6 RID: 982
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchor_Injected(ref Vector2 value);

		// Token: 0x060003D7 RID: 983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_connectedAnchor_Injected(out Vector2 ret);

		// Token: 0x060003D8 RID: 984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_connectedAnchor_Injected(ref Vector2 value);
	}
}
