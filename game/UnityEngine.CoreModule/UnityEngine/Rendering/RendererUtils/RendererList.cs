using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.RendererUtils
{
	// Token: 0x0200042B RID: 1067
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/RendererList.h")]
	public struct RendererList
	{
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x0003EE2D File Offset: 0x0003D02D
		public bool isValid
		{
			get
			{
				return RendererList.get_isValid_Injected(ref this);
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0003EE35 File Offset: 0x0003D035
		internal RendererList(UIntPtr ctx, uint indx)
		{
			this.context = ctx;
			this.index = indx;
			this.frame = 0U;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x0003EE4D File Offset: 0x0003D04D
		// Note: this type is marked as 'beforefieldinit'.
		static RendererList()
		{
		}

		// Token: 0x06002534 RID: 9524
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isValid_Injected(ref RendererList _unity_self);

		// Token: 0x04000DD5 RID: 3541
		internal UIntPtr context;

		// Token: 0x04000DD6 RID: 3542
		internal uint index;

		// Token: 0x04000DD7 RID: 3543
		internal uint frame;

		// Token: 0x04000DD8 RID: 3544
		public static readonly RendererList nullRendererList = new RendererList(UIntPtr.Zero, uint.MaxValue);
	}
}
