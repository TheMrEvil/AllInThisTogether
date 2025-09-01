using System;

namespace XInputDotNetPure
{
	// Token: 0x02000004 RID: 4
	public struct GamePadButtons
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000208C File Offset: 0x0000028C
		internal GamePadButtons(ButtonState start, ButtonState back, ButtonState leftStick, ButtonState rightStick, ButtonState leftShoulder, ButtonState rightShoulder, ButtonState a, ButtonState b, ButtonState x, ButtonState y)
		{
			this.start = start;
			this.back = back;
			this.leftStick = leftStick;
			this.rightStick = rightStick;
			this.leftShoulder = leftShoulder;
			this.rightShoulder = rightShoulder;
			this.a = a;
			this.b = b;
			this.x = x;
			this.y = y;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020E6 File Offset: 0x000002E6
		public ButtonState Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020EE File Offset: 0x000002EE
		public ButtonState Back
		{
			get
			{
				return this.back;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020F6 File Offset: 0x000002F6
		public ButtonState LeftStick
		{
			get
			{
				return this.leftStick;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020FE File Offset: 0x000002FE
		public ButtonState RightStick
		{
			get
			{
				return this.rightStick;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002106 File Offset: 0x00000306
		public ButtonState LeftShoulder
		{
			get
			{
				return this.leftShoulder;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000210E File Offset: 0x0000030E
		public ButtonState RightShoulder
		{
			get
			{
				return this.rightShoulder;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002116 File Offset: 0x00000316
		public ButtonState A
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000211E File Offset: 0x0000031E
		public ButtonState B
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002126 File Offset: 0x00000326
		public ButtonState X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000212E File Offset: 0x0000032E
		public ButtonState Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x04000004 RID: 4
		private ButtonState start;

		// Token: 0x04000005 RID: 5
		private ButtonState back;

		// Token: 0x04000006 RID: 6
		private ButtonState leftStick;

		// Token: 0x04000007 RID: 7
		private ButtonState rightStick;

		// Token: 0x04000008 RID: 8
		private ButtonState leftShoulder;

		// Token: 0x04000009 RID: 9
		private ButtonState rightShoulder;

		// Token: 0x0400000A RID: 10
		private ButtonState a;

		// Token: 0x0400000B RID: 11
		private ButtonState b;

		// Token: 0x0400000C RID: 12
		private ButtonState x;

		// Token: 0x0400000D RID: 13
		private ButtonState y;
	}
}
