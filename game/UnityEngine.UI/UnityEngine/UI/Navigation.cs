using System;

namespace UnityEngine.UI
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public struct Navigation : IEquatable<Navigation>
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000100EF File Offset: 0x0000E2EF
		// (set) Token: 0x06000306 RID: 774 RVA: 0x000100F7 File Offset: 0x0000E2F7
		public Navigation.Mode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00010100 File Offset: 0x0000E300
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00010108 File Offset: 0x0000E308
		public bool wrapAround
		{
			get
			{
				return this.m_WrapAround;
			}
			set
			{
				this.m_WrapAround = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00010111 File Offset: 0x0000E311
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00010119 File Offset: 0x0000E319
		public Selectable selectOnUp
		{
			get
			{
				return this.m_SelectOnUp;
			}
			set
			{
				this.m_SelectOnUp = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00010122 File Offset: 0x0000E322
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0001012A File Offset: 0x0000E32A
		public Selectable selectOnDown
		{
			get
			{
				return this.m_SelectOnDown;
			}
			set
			{
				this.m_SelectOnDown = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00010133 File Offset: 0x0000E333
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0001013B File Offset: 0x0000E33B
		public Selectable selectOnLeft
		{
			get
			{
				return this.m_SelectOnLeft;
			}
			set
			{
				this.m_SelectOnLeft = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00010144 File Offset: 0x0000E344
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0001014C File Offset: 0x0000E34C
		public Selectable selectOnRight
		{
			get
			{
				return this.m_SelectOnRight;
			}
			set
			{
				this.m_SelectOnRight = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00010158 File Offset: 0x0000E358
		public static Navigation defaultNavigation
		{
			get
			{
				return new Navigation
				{
					m_Mode = Navigation.Mode.Automatic,
					m_WrapAround = false
				};
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00010180 File Offset: 0x0000E380
		public bool Equals(Navigation other)
		{
			return this.mode == other.mode && this.selectOnUp == other.selectOnUp && this.selectOnDown == other.selectOnDown && this.selectOnLeft == other.selectOnLeft && this.selectOnRight == other.selectOnRight;
		}

		// Token: 0x04000105 RID: 261
		[SerializeField]
		private Navigation.Mode m_Mode;

		// Token: 0x04000106 RID: 262
		[Tooltip("Enables navigation to wrap around from last to first or first to last element. Does not work for automatic grid navigation")]
		[SerializeField]
		private bool m_WrapAround;

		// Token: 0x04000107 RID: 263
		[SerializeField]
		private Selectable m_SelectOnUp;

		// Token: 0x04000108 RID: 264
		[SerializeField]
		private Selectable m_SelectOnDown;

		// Token: 0x04000109 RID: 265
		[SerializeField]
		private Selectable m_SelectOnLeft;

		// Token: 0x0400010A RID: 266
		[SerializeField]
		private Selectable m_SelectOnRight;

		// Token: 0x020000A1 RID: 161
		[Flags]
		public enum Mode
		{
			// Token: 0x040002DB RID: 731
			None = 0,
			// Token: 0x040002DC RID: 732
			Horizontal = 1,
			// Token: 0x040002DD RID: 733
			Vertical = 2,
			// Token: 0x040002DE RID: 734
			Automatic = 3,
			// Token: 0x040002DF RID: 735
			Explicit = 4
		}
	}
}
