using System;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000138 RID: 312
	public struct RenderTargetSetup
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x0000EE44 File Offset: 0x0000D044
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, RenderBufferLoadAction[] colorLoad, RenderBufferStoreAction[] colorStore, RenderBufferLoadAction depthLoad, RenderBufferStoreAction depthStore)
		{
			this.color = color;
			this.depth = depth;
			this.mipLevel = mip;
			this.cubemapFace = face;
			this.depthSlice = 0;
			this.colorLoad = colorLoad;
			this.colorStore = colorStore;
			this.depthLoad = depthLoad;
			this.depthStore = depthStore;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0000EE98 File Offset: 0x0000D098
		internal static RenderBufferLoadAction[] LoadActions(RenderBuffer[] buf)
		{
			RenderBufferLoadAction[] array = new RenderBufferLoadAction[buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i].loadAction;
				buf[i].loadAction = RenderBufferLoadAction.Load;
			}
			return array;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		internal static RenderBufferStoreAction[] StoreActions(RenderBuffer[] buf)
		{
			RenderBufferStoreAction[] array = new RenderBufferStoreAction[buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i].storeAction;
				buf[i].storeAction = RenderBufferStoreAction.Store;
			}
			return array;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0000EF35 File Offset: 0x0000D135
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth)
		{
			this = new RenderTargetSetup(new RenderBuffer[]
			{
				color
			}, depth);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0000EF4E File Offset: 0x0000D14E
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel)
		{
			this = new RenderTargetSetup(new RenderBuffer[]
			{
				color
			}, depth, mipLevel);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0000EF68 File Offset: 0x0000D168
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel, CubemapFace face)
		{
			this = new RenderTargetSetup(new RenderBuffer[]
			{
				color
			}, depth, mipLevel, face);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0000EF84 File Offset: 0x0000D184
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel, CubemapFace face, int depthSlice)
		{
			this = new RenderTargetSetup(new RenderBuffer[]
			{
				color
			}, depth, mipLevel, face);
			this.depthSlice = depthSlice;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth)
		{
			this = new RenderTargetSetup(color, depth, 0, CubemapFace.Unknown);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0000EFB6 File Offset: 0x0000D1B6
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mipLevel)
		{
			this = new RenderTargetSetup(color, depth, mipLevel, CubemapFace.Unknown);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face)
		{
			this = new RenderTargetSetup(color, depth, mip, face, RenderTargetSetup.LoadActions(color), RenderTargetSetup.StoreActions(color), depth.loadAction, depth.storeAction);
		}

		// Token: 0x040003E7 RID: 999
		public RenderBuffer[] color;

		// Token: 0x040003E8 RID: 1000
		public RenderBuffer depth;

		// Token: 0x040003E9 RID: 1001
		public int mipLevel;

		// Token: 0x040003EA RID: 1002
		public CubemapFace cubemapFace;

		// Token: 0x040003EB RID: 1003
		public int depthSlice;

		// Token: 0x040003EC RID: 1004
		public RenderBufferLoadAction[] colorLoad;

		// Token: 0x040003ED RID: 1005
		public RenderBufferStoreAction[] colorStore;

		// Token: 0x040003EE RID: 1006
		public RenderBufferLoadAction depthLoad;

		// Token: 0x040003EF RID: 1007
		public RenderBufferStoreAction depthStore;
	}
}
