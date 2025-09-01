using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000152 RID: 338
	[NativeHeader("Runtime/Camera/Flare.h")]
	public sealed class LensFlare : Behaviour
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000E26 RID: 3622
		// (set) Token: 0x06000E27 RID: 3623
		public extern float brightness { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000E28 RID: 3624
		// (set) Token: 0x06000E29 RID: 3625
		public extern float fadeSpeed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00012B7C File Offset: 0x00010D7C
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00012B92 File Offset: 0x00010D92
		public Color color
		{
			get
			{
				Color result;
				this.get_color_Injected(out result);
				return result;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000E2C RID: 3628
		// (set) Token: 0x06000E2D RID: 3629
		public extern Flare flare { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000E2E RID: 3630 RVA: 0x000084C0 File Offset: 0x000066C0
		public LensFlare()
		{
		}

		// Token: 0x06000E2F RID: 3631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x06000E30 RID: 3632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_color_Injected(ref Color value);
	}
}
