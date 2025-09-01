using System;

namespace XInputDotNetPure
{
	// Token: 0x02000005 RID: 5
	public struct GamePadDPad
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002136 File Offset: 0x00000336
		internal GamePadDPad(ButtonState up, ButtonState down, ButtonState left, ButtonState right)
		{
			this.up = up;
			this.down = down;
			this.left = left;
			this.right = right;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002155 File Offset: 0x00000355
		public ButtonState Up
		{
			get
			{
				return this.up;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000215D File Offset: 0x0000035D
		public ButtonState Down
		{
			get
			{
				return this.down;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002165 File Offset: 0x00000365
		public ButtonState Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000216D File Offset: 0x0000036D
		public ButtonState Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x0400000E RID: 14
		private ButtonState up;

		// Token: 0x0400000F RID: 15
		private ButtonState down;

		// Token: 0x04000010 RID: 16
		private ButtonState left;

		// Token: 0x04000011 RID: 17
		private ButtonState right;
	}
}
