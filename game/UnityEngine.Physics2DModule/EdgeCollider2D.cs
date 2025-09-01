using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000024 RID: 36
	[NativeHeader("Modules/Physics2D/Public/EdgeCollider2D.h")]
	public sealed class EdgeCollider2D : Collider2D
	{
		// Token: 0x06000376 RID: 886
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000377 RID: 887
		// (set) Token: 0x06000378 RID: 888
		public extern float edgeRadius { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000379 RID: 889
		public extern int edgeCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600037A RID: 890
		public extern int pointCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600037B RID: 891
		// (set) Token: 0x0600037C RID: 892
		public extern Vector2[] points { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600037D RID: 893
		[NativeMethod("GetPoints_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetPoints([NotNull("ArgumentNullException")] List<Vector2> points);

		// Token: 0x0600037E RID: 894
		[NativeMethod("SetPoints_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPoints([NotNull("ArgumentNullException")] List<Vector2> points);

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600037F RID: 895
		// (set) Token: 0x06000380 RID: 896
		public extern bool useAdjacentStartPoint { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000381 RID: 897
		// (set) Token: 0x06000382 RID: 898
		public extern bool useAdjacentEndPoint { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00008178 File Offset: 0x00006378
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000818E File Offset: 0x0000638E
		public Vector2 adjacentStartPoint
		{
			get
			{
				Vector2 result;
				this.get_adjacentStartPoint_Injected(out result);
				return result;
			}
			set
			{
				this.set_adjacentStartPoint_Injected(ref value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00008198 File Offset: 0x00006398
		// (set) Token: 0x06000386 RID: 902 RVA: 0x000081AE File Offset: 0x000063AE
		public Vector2 adjacentEndPoint
		{
			get
			{
				Vector2 result;
				this.get_adjacentEndPoint_Injected(out result);
				return result;
			}
			set
			{
				this.set_adjacentEndPoint_Injected(ref value);
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000814F File Offset: 0x0000634F
		public EdgeCollider2D()
		{
		}

		// Token: 0x06000388 RID: 904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_adjacentStartPoint_Injected(out Vector2 ret);

		// Token: 0x06000389 RID: 905
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_adjacentStartPoint_Injected(ref Vector2 value);

		// Token: 0x0600038A RID: 906
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_adjacentEndPoint_Injected(out Vector2 ret);

		// Token: 0x0600038B RID: 907
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_adjacentEndPoint_Injected(ref Vector2 value);
	}
}
