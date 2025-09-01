using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040C RID: 1036
	public struct RenderTargetBlendState : IEquatable<RenderTargetBlendState>
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x0003BCA4 File Offset: 0x00039EA4
		public static RenderTargetBlendState defaultValue
		{
			get
			{
				return new RenderTargetBlendState(ColorWriteMask.All, BlendMode.One, BlendMode.Zero, BlendMode.One, BlendMode.Zero, BlendOp.Add, BlendOp.Add);
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0003BCC4 File Offset: 0x00039EC4
		public RenderTargetBlendState(ColorWriteMask writeMask = ColorWriteMask.All, BlendMode sourceColorBlendMode = BlendMode.One, BlendMode destinationColorBlendMode = BlendMode.Zero, BlendMode sourceAlphaBlendMode = BlendMode.One, BlendMode destinationAlphaBlendMode = BlendMode.Zero, BlendOp colorBlendOperation = BlendOp.Add, BlendOp alphaBlendOperation = BlendOp.Add)
		{
			this.m_WriteMask = (byte)writeMask;
			this.m_SourceColorBlendMode = (byte)sourceColorBlendMode;
			this.m_DestinationColorBlendMode = (byte)destinationColorBlendMode;
			this.m_SourceAlphaBlendMode = (byte)sourceAlphaBlendMode;
			this.m_DestinationAlphaBlendMode = (byte)destinationAlphaBlendMode;
			this.m_ColorBlendOperation = (byte)colorBlendOperation;
			this.m_AlphaBlendOperation = (byte)alphaBlendOperation;
			this.m_Padding = 0;
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x0003BD18 File Offset: 0x00039F18
		// (set) Token: 0x06002368 RID: 9064 RVA: 0x0003BD30 File Offset: 0x00039F30
		public ColorWriteMask writeMask
		{
			get
			{
				return (ColorWriteMask)this.m_WriteMask;
			}
			set
			{
				this.m_WriteMask = (byte)value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x0003BD3C File Offset: 0x00039F3C
		// (set) Token: 0x0600236A RID: 9066 RVA: 0x0003BD54 File Offset: 0x00039F54
		public BlendMode sourceColorBlendMode
		{
			get
			{
				return (BlendMode)this.m_SourceColorBlendMode;
			}
			set
			{
				this.m_SourceColorBlendMode = (byte)value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x0003BD60 File Offset: 0x00039F60
		// (set) Token: 0x0600236C RID: 9068 RVA: 0x0003BD78 File Offset: 0x00039F78
		public BlendMode destinationColorBlendMode
		{
			get
			{
				return (BlendMode)this.m_DestinationColorBlendMode;
			}
			set
			{
				this.m_DestinationColorBlendMode = (byte)value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x0003BD84 File Offset: 0x00039F84
		// (set) Token: 0x0600236E RID: 9070 RVA: 0x0003BD9C File Offset: 0x00039F9C
		public BlendMode sourceAlphaBlendMode
		{
			get
			{
				return (BlendMode)this.m_SourceAlphaBlendMode;
			}
			set
			{
				this.m_SourceAlphaBlendMode = (byte)value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x0003BDA8 File Offset: 0x00039FA8
		// (set) Token: 0x06002370 RID: 9072 RVA: 0x0003BDC0 File Offset: 0x00039FC0
		public BlendMode destinationAlphaBlendMode
		{
			get
			{
				return (BlendMode)this.m_DestinationAlphaBlendMode;
			}
			set
			{
				this.m_DestinationAlphaBlendMode = (byte)value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x0003BDCC File Offset: 0x00039FCC
		// (set) Token: 0x06002372 RID: 9074 RVA: 0x0003BDE4 File Offset: 0x00039FE4
		public BlendOp colorBlendOperation
		{
			get
			{
				return (BlendOp)this.m_ColorBlendOperation;
			}
			set
			{
				this.m_ColorBlendOperation = (byte)value;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x0003BDF0 File Offset: 0x00039FF0
		// (set) Token: 0x06002374 RID: 9076 RVA: 0x0003BE08 File Offset: 0x0003A008
		public BlendOp alphaBlendOperation
		{
			get
			{
				return (BlendOp)this.m_AlphaBlendOperation;
			}
			set
			{
				this.m_AlphaBlendOperation = (byte)value;
			}
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x0003BE14 File Offset: 0x0003A014
		public bool Equals(RenderTargetBlendState other)
		{
			return this.m_WriteMask == other.m_WriteMask && this.m_SourceColorBlendMode == other.m_SourceColorBlendMode && this.m_DestinationColorBlendMode == other.m_DestinationColorBlendMode && this.m_SourceAlphaBlendMode == other.m_SourceAlphaBlendMode && this.m_DestinationAlphaBlendMode == other.m_DestinationAlphaBlendMode && this.m_ColorBlendOperation == other.m_ColorBlendOperation && this.m_AlphaBlendOperation == other.m_AlphaBlendOperation;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0003BE8C File Offset: 0x0003A08C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RenderTargetBlendState && this.Equals((RenderTargetBlendState)obj);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0003BEC4 File Offset: 0x0003A0C4
		public override int GetHashCode()
		{
			int num = this.m_WriteMask.GetHashCode();
			num = (num * 397 ^ this.m_SourceColorBlendMode.GetHashCode());
			num = (num * 397 ^ this.m_DestinationColorBlendMode.GetHashCode());
			num = (num * 397 ^ this.m_SourceAlphaBlendMode.GetHashCode());
			num = (num * 397 ^ this.m_DestinationAlphaBlendMode.GetHashCode());
			num = (num * 397 ^ this.m_ColorBlendOperation.GetHashCode());
			return num * 397 ^ this.m_AlphaBlendOperation.GetHashCode();
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0003BF5C File Offset: 0x0003A15C
		public static bool operator ==(RenderTargetBlendState left, RenderTargetBlendState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0003BF78 File Offset: 0x0003A178
		public static bool operator !=(RenderTargetBlendState left, RenderTargetBlendState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D2D RID: 3373
		private byte m_WriteMask;

		// Token: 0x04000D2E RID: 3374
		private byte m_SourceColorBlendMode;

		// Token: 0x04000D2F RID: 3375
		private byte m_DestinationColorBlendMode;

		// Token: 0x04000D30 RID: 3376
		private byte m_SourceAlphaBlendMode;

		// Token: 0x04000D31 RID: 3377
		private byte m_DestinationAlphaBlendMode;

		// Token: 0x04000D32 RID: 3378
		private byte m_ColorBlendOperation;

		// Token: 0x04000D33 RID: 3379
		private byte m_AlphaBlendOperation;

		// Token: 0x04000D34 RID: 3380
		private byte m_Padding;
	}
}
