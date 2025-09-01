using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003EF RID: 1007
	public struct BlendState : IEquatable<BlendState>
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x00038778 File Offset: 0x00036978
		public static BlendState defaultValue
		{
			get
			{
				return new BlendState(false, false);
			}
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00038794 File Offset: 0x00036994
		public BlendState(bool separateMRTBlend = false, bool alphaToMask = false)
		{
			this.m_BlendState0 = RenderTargetBlendState.defaultValue;
			this.m_BlendState1 = RenderTargetBlendState.defaultValue;
			this.m_BlendState2 = RenderTargetBlendState.defaultValue;
			this.m_BlendState3 = RenderTargetBlendState.defaultValue;
			this.m_BlendState4 = RenderTargetBlendState.defaultValue;
			this.m_BlendState5 = RenderTargetBlendState.defaultValue;
			this.m_BlendState6 = RenderTargetBlendState.defaultValue;
			this.m_BlendState7 = RenderTargetBlendState.defaultValue;
			this.m_SeparateMRTBlendStates = Convert.ToByte(separateMRTBlend);
			this.m_AlphaToMask = Convert.ToByte(alphaToMask);
			this.m_Padding = 0;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x0003881C File Offset: 0x00036A1C
		// (set) Token: 0x06002226 RID: 8742 RVA: 0x00038839 File Offset: 0x00036A39
		public bool separateMRTBlendStates
		{
			get
			{
				return Convert.ToBoolean(this.m_SeparateMRTBlendStates);
			}
			set
			{
				this.m_SeparateMRTBlendStates = Convert.ToByte(value);
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x00038848 File Offset: 0x00036A48
		// (set) Token: 0x06002228 RID: 8744 RVA: 0x00038865 File Offset: 0x00036A65
		public bool alphaToMask
		{
			get
			{
				return Convert.ToBoolean(this.m_AlphaToMask);
			}
			set
			{
				this.m_AlphaToMask = Convert.ToByte(value);
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x00038874 File Offset: 0x00036A74
		// (set) Token: 0x0600222A RID: 8746 RVA: 0x0003888C File Offset: 0x00036A8C
		public RenderTargetBlendState blendState0
		{
			get
			{
				return this.m_BlendState0;
			}
			set
			{
				this.m_BlendState0 = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x00038898 File Offset: 0x00036A98
		// (set) Token: 0x0600222C RID: 8748 RVA: 0x000388B0 File Offset: 0x00036AB0
		public RenderTargetBlendState blendState1
		{
			get
			{
				return this.m_BlendState1;
			}
			set
			{
				this.m_BlendState1 = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x000388BC File Offset: 0x00036ABC
		// (set) Token: 0x0600222E RID: 8750 RVA: 0x000388D4 File Offset: 0x00036AD4
		public RenderTargetBlendState blendState2
		{
			get
			{
				return this.m_BlendState2;
			}
			set
			{
				this.m_BlendState2 = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x000388E0 File Offset: 0x00036AE0
		// (set) Token: 0x06002230 RID: 8752 RVA: 0x000388F8 File Offset: 0x00036AF8
		public RenderTargetBlendState blendState3
		{
			get
			{
				return this.m_BlendState3;
			}
			set
			{
				this.m_BlendState3 = value;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x00038904 File Offset: 0x00036B04
		// (set) Token: 0x06002232 RID: 8754 RVA: 0x0003891C File Offset: 0x00036B1C
		public RenderTargetBlendState blendState4
		{
			get
			{
				return this.m_BlendState4;
			}
			set
			{
				this.m_BlendState4 = value;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x00038928 File Offset: 0x00036B28
		// (set) Token: 0x06002234 RID: 8756 RVA: 0x00038940 File Offset: 0x00036B40
		public RenderTargetBlendState blendState5
		{
			get
			{
				return this.m_BlendState5;
			}
			set
			{
				this.m_BlendState5 = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x0003894C File Offset: 0x00036B4C
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x00038964 File Offset: 0x00036B64
		public RenderTargetBlendState blendState6
		{
			get
			{
				return this.m_BlendState6;
			}
			set
			{
				this.m_BlendState6 = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x00038970 File Offset: 0x00036B70
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x00038988 File Offset: 0x00036B88
		public RenderTargetBlendState blendState7
		{
			get
			{
				return this.m_BlendState7;
			}
			set
			{
				this.m_BlendState7 = value;
			}
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x00038994 File Offset: 0x00036B94
		public bool Equals(BlendState other)
		{
			return this.m_BlendState0.Equals(other.m_BlendState0) && this.m_BlendState1.Equals(other.m_BlendState1) && this.m_BlendState2.Equals(other.m_BlendState2) && this.m_BlendState3.Equals(other.m_BlendState3) && this.m_BlendState4.Equals(other.m_BlendState4) && this.m_BlendState5.Equals(other.m_BlendState5) && this.m_BlendState6.Equals(other.m_BlendState6) && this.m_BlendState7.Equals(other.m_BlendState7) && this.m_SeparateMRTBlendStates == other.m_SeparateMRTBlendStates && this.m_AlphaToMask == other.m_AlphaToMask;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00038A64 File Offset: 0x00036C64
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is BlendState && this.Equals((BlendState)obj);
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00038A9C File Offset: 0x00036C9C
		public override int GetHashCode()
		{
			int num = this.m_BlendState0.GetHashCode();
			num = (num * 397 ^ this.m_BlendState1.GetHashCode());
			num = (num * 397 ^ this.m_BlendState2.GetHashCode());
			num = (num * 397 ^ this.m_BlendState3.GetHashCode());
			num = (num * 397 ^ this.m_BlendState4.GetHashCode());
			num = (num * 397 ^ this.m_BlendState5.GetHashCode());
			num = (num * 397 ^ this.m_BlendState6.GetHashCode());
			num = (num * 397 ^ this.m_BlendState7.GetHashCode());
			num = (num * 397 ^ this.m_SeparateMRTBlendStates.GetHashCode());
			return num * 397 ^ this.m_AlphaToMask.GetHashCode();
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00038BA0 File Offset: 0x00036DA0
		public static bool operator ==(BlendState left, BlendState right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x00038BBC File Offset: 0x00036DBC
		public static bool operator !=(BlendState left, BlendState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000C6F RID: 3183
		private RenderTargetBlendState m_BlendState0;

		// Token: 0x04000C70 RID: 3184
		private RenderTargetBlendState m_BlendState1;

		// Token: 0x04000C71 RID: 3185
		private RenderTargetBlendState m_BlendState2;

		// Token: 0x04000C72 RID: 3186
		private RenderTargetBlendState m_BlendState3;

		// Token: 0x04000C73 RID: 3187
		private RenderTargetBlendState m_BlendState4;

		// Token: 0x04000C74 RID: 3188
		private RenderTargetBlendState m_BlendState5;

		// Token: 0x04000C75 RID: 3189
		private RenderTargetBlendState m_BlendState6;

		// Token: 0x04000C76 RID: 3190
		private RenderTargetBlendState m_BlendState7;

		// Token: 0x04000C77 RID: 3191
		private byte m_SeparateMRTBlendStates;

		// Token: 0x04000C78 RID: 3192
		private byte m_AlphaToMask;

		// Token: 0x04000C79 RID: 3193
		private short m_Padding;
	}
}
