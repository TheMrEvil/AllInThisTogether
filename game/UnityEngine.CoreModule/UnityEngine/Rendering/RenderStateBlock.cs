using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040A RID: 1034
	public struct RenderStateBlock : IEquatable<RenderStateBlock>
	{
		// Token: 0x06002353 RID: 9043 RVA: 0x0003BA07 File Offset: 0x00039C07
		public RenderStateBlock(RenderStateMask mask)
		{
			this.m_BlendState = BlendState.defaultValue;
			this.m_RasterState = RasterState.defaultValue;
			this.m_DepthState = DepthState.defaultValue;
			this.m_StencilState = StencilState.defaultValue;
			this.m_StencilReference = 0;
			this.m_Mask = mask;
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x0003BA44 File Offset: 0x00039C44
		// (set) Token: 0x06002355 RID: 9045 RVA: 0x0003BA5C File Offset: 0x00039C5C
		public BlendState blendState
		{
			get
			{
				return this.m_BlendState;
			}
			set
			{
				this.m_BlendState = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x0003BA68 File Offset: 0x00039C68
		// (set) Token: 0x06002357 RID: 9047 RVA: 0x0003BA80 File Offset: 0x00039C80
		public RasterState rasterState
		{
			get
			{
				return this.m_RasterState;
			}
			set
			{
				this.m_RasterState = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x0003BA8C File Offset: 0x00039C8C
		// (set) Token: 0x06002359 RID: 9049 RVA: 0x0003BAA4 File Offset: 0x00039CA4
		public DepthState depthState
		{
			get
			{
				return this.m_DepthState;
			}
			set
			{
				this.m_DepthState = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x0003BAB0 File Offset: 0x00039CB0
		// (set) Token: 0x0600235B RID: 9051 RVA: 0x0003BAC8 File Offset: 0x00039CC8
		public StencilState stencilState
		{
			get
			{
				return this.m_StencilState;
			}
			set
			{
				this.m_StencilState = value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x0003BAD4 File Offset: 0x00039CD4
		// (set) Token: 0x0600235D RID: 9053 RVA: 0x0003BAEC File Offset: 0x00039CEC
		public int stencilReference
		{
			get
			{
				return this.m_StencilReference;
			}
			set
			{
				this.m_StencilReference = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x0003BAF8 File Offset: 0x00039CF8
		// (set) Token: 0x0600235F RID: 9055 RVA: 0x0003BB10 File Offset: 0x00039D10
		public RenderStateMask mask
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0003BB1C File Offset: 0x00039D1C
		public bool Equals(RenderStateBlock other)
		{
			return this.m_BlendState.Equals(other.m_BlendState) && this.m_RasterState.Equals(other.m_RasterState) && this.m_DepthState.Equals(other.m_DepthState) && this.m_StencilState.Equals(other.m_StencilState) && this.m_StencilReference == other.m_StencilReference && this.m_Mask == other.m_Mask;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0003BB9C File Offset: 0x00039D9C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderStateBlock && this.Equals((RenderStateBlock)obj);
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0003BBD4 File Offset: 0x00039DD4
		public override int GetHashCode()
		{
			int num = this.m_BlendState.GetHashCode();
			num = (num * 397 ^ this.m_RasterState.GetHashCode());
			num = (num * 397 ^ this.m_DepthState.GetHashCode());
			num = (num * 397 ^ this.m_StencilState.GetHashCode());
			num = (num * 397 ^ this.m_StencilReference);
			return num * 397 ^ (int)this.m_Mask;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0003BC68 File Offset: 0x00039E68
		public static bool operator ==(RenderStateBlock left, RenderStateBlock right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0003BC84 File Offset: 0x00039E84
		public static bool operator !=(RenderStateBlock left, RenderStateBlock right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D20 RID: 3360
		private BlendState m_BlendState;

		// Token: 0x04000D21 RID: 3361
		private RasterState m_RasterState;

		// Token: 0x04000D22 RID: 3362
		private DepthState m_DepthState;

		// Token: 0x04000D23 RID: 3363
		private StencilState m_StencilState;

		// Token: 0x04000D24 RID: 3364
		private int m_StencilReference;

		// Token: 0x04000D25 RID: 3365
		private RenderStateMask m_Mask;
	}
}
