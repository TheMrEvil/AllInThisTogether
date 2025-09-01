using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C5 RID: 965
	public struct RenderTargetBinding
	{
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x00033AA0 File Offset: 0x00031CA0
		// (set) Token: 0x06001F8F RID: 8079 RVA: 0x00033AB8 File Offset: 0x00031CB8
		public RenderTargetIdentifier[] colorRenderTargets
		{
			get
			{
				return this.m_ColorRenderTargets;
			}
			set
			{
				this.m_ColorRenderTargets = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x00033AC4 File Offset: 0x00031CC4
		// (set) Token: 0x06001F91 RID: 8081 RVA: 0x00033ADC File Offset: 0x00031CDC
		public RenderTargetIdentifier depthRenderTarget
		{
			get
			{
				return this.m_DepthRenderTarget;
			}
			set
			{
				this.m_DepthRenderTarget = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x00033AE8 File Offset: 0x00031CE8
		// (set) Token: 0x06001F93 RID: 8083 RVA: 0x00033B00 File Offset: 0x00031D00
		public RenderBufferLoadAction[] colorLoadActions
		{
			get
			{
				return this.m_ColorLoadActions;
			}
			set
			{
				this.m_ColorLoadActions = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x00033B0C File Offset: 0x00031D0C
		// (set) Token: 0x06001F95 RID: 8085 RVA: 0x00033B24 File Offset: 0x00031D24
		public RenderBufferStoreAction[] colorStoreActions
		{
			get
			{
				return this.m_ColorStoreActions;
			}
			set
			{
				this.m_ColorStoreActions = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x00033B30 File Offset: 0x00031D30
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x00033B48 File Offset: 0x00031D48
		public RenderBufferLoadAction depthLoadAction
		{
			get
			{
				return this.m_DepthLoadAction;
			}
			set
			{
				this.m_DepthLoadAction = value;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00033B54 File Offset: 0x00031D54
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x00033B6C File Offset: 0x00031D6C
		public RenderBufferStoreAction depthStoreAction
		{
			get
			{
				return this.m_DepthStoreAction;
			}
			set
			{
				this.m_DepthStoreAction = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x00033B78 File Offset: 0x00031D78
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x00033B90 File Offset: 0x00031D90
		public RenderTargetFlags flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00033B9A File Offset: 0x00031D9A
		public RenderTargetBinding(RenderTargetIdentifier[] colorRenderTargets, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this.m_ColorRenderTargets = colorRenderTargets;
			this.m_DepthRenderTarget = depthRenderTarget;
			this.m_ColorLoadActions = colorLoadActions;
			this.m_ColorStoreActions = colorStoreActions;
			this.m_DepthLoadAction = depthLoadAction;
			this.m_DepthStoreAction = depthStoreAction;
			this.m_Flags = RenderTargetFlags.None;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00033BD1 File Offset: 0x00031DD1
		public RenderTargetBinding(RenderTargetIdentifier colorRenderTarget, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			this = new RenderTargetBinding(new RenderTargetIdentifier[]
			{
				colorRenderTarget
			}, new RenderBufferLoadAction[]
			{
				colorLoadAction
			}, new RenderBufferStoreAction[]
			{
				colorStoreAction
			}, depthRenderTarget, depthLoadAction, depthStoreAction);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00033C04 File Offset: 0x00031E04
		public RenderTargetBinding(RenderTargetSetup setup)
		{
			this.m_ColorRenderTargets = new RenderTargetIdentifier[setup.color.Length];
			for (int i = 0; i < this.m_ColorRenderTargets.Length; i++)
			{
				this.m_ColorRenderTargets[i] = new RenderTargetIdentifier(setup.color[i], setup.mipLevel, setup.cubemapFace, setup.depthSlice);
			}
			this.m_DepthRenderTarget = setup.depth;
			this.m_ColorLoadActions = (RenderBufferLoadAction[])setup.colorLoad.Clone();
			this.m_ColorStoreActions = (RenderBufferStoreAction[])setup.colorStore.Clone();
			this.m_DepthLoadAction = setup.depthLoad;
			this.m_DepthStoreAction = setup.depthStore;
			this.m_Flags = RenderTargetFlags.None;
		}

		// Token: 0x04000B85 RID: 2949
		private RenderTargetIdentifier[] m_ColorRenderTargets;

		// Token: 0x04000B86 RID: 2950
		private RenderTargetIdentifier m_DepthRenderTarget;

		// Token: 0x04000B87 RID: 2951
		private RenderBufferLoadAction[] m_ColorLoadActions;

		// Token: 0x04000B88 RID: 2952
		private RenderBufferStoreAction[] m_ColorStoreActions;

		// Token: 0x04000B89 RID: 2953
		private RenderBufferLoadAction m_DepthLoadAction;

		// Token: 0x04000B8A RID: 2954
		private RenderBufferStoreAction m_DepthStoreAction;

		// Token: 0x04000B8B RID: 2955
		private RenderTargetFlags m_Flags;
	}
}
