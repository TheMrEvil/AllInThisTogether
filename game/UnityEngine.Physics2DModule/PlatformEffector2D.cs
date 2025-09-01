using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[NativeHeader("Modules/Physics2D/PlatformEffector2D.h")]
	public class PlatformEffector2D : Effector2D
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000485 RID: 1157
		// (set) Token: 0x06000486 RID: 1158
		public extern bool useOneWay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000487 RID: 1159
		// (set) Token: 0x06000488 RID: 1160
		public extern bool useOneWayGrouping { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000489 RID: 1161
		// (set) Token: 0x0600048A RID: 1162
		public extern bool useSideFriction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600048B RID: 1163
		// (set) Token: 0x0600048C RID: 1164
		public extern bool useSideBounce { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600048D RID: 1165
		// (set) Token: 0x0600048E RID: 1166
		public extern float surfaceArc { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600048F RID: 1167
		// (set) Token: 0x06000490 RID: 1168
		public extern float sideArc { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000491 RID: 1169
		// (set) Token: 0x06000492 RID: 1170
		public extern float rotationalOffset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000493 RID: 1171 RVA: 0x00008694 File Offset: 0x00006894
		public PlatformEffector2D()
		{
		}
	}
}
