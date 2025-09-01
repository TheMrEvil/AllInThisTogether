using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000153 RID: 339
	[NativeHeader("Runtime/Camera/Projector.h")]
	public sealed class Projector : Behaviour
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000E31 RID: 3633
		// (set) Token: 0x06000E32 RID: 3634
		public extern float nearClipPlane { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000E33 RID: 3635
		// (set) Token: 0x06000E34 RID: 3636
		public extern float farClipPlane { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000E35 RID: 3637
		// (set) Token: 0x06000E36 RID: 3638
		public extern float fieldOfView { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000E37 RID: 3639
		// (set) Token: 0x06000E38 RID: 3640
		public extern float aspectRatio { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000E39 RID: 3641
		// (set) Token: 0x06000E3A RID: 3642
		public extern bool orthographic { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000E3B RID: 3643
		// (set) Token: 0x06000E3C RID: 3644
		public extern float orthographicSize { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000E3D RID: 3645
		// (set) Token: 0x06000E3E RID: 3646
		public extern int ignoreLayers { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000E3F RID: 3647
		// (set) Token: 0x06000E40 RID: 3648
		public extern Material material { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000E41 RID: 3649 RVA: 0x000084C0 File Offset: 0x000066C0
		public Projector()
		{
		}
	}
}
