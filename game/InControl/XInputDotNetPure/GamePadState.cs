using System;

namespace XInputDotNetPure
{
	// Token: 0x02000008 RID: 8
	public struct GamePadState
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000021B8 File Offset: 0x000003B8
		internal GamePadState(bool isConnected, GamePadState.RawState rawState)
		{
			this.isConnected = isConnected;
			if (!isConnected)
			{
				rawState.dwPacketNumber = 0U;
				rawState.Gamepad.dwButtons = 0;
				rawState.Gamepad.bLeftTrigger = 0;
				rawState.Gamepad.bRightTrigger = 0;
				rawState.Gamepad.sThumbLX = 0;
				rawState.Gamepad.sThumbLY = 0;
				rawState.Gamepad.sThumbRX = 0;
				rawState.Gamepad.sThumbRY = 0;
			}
			this.packetNumber = rawState.dwPacketNumber;
			this.buttons = new GamePadButtons(((rawState.Gamepad.dwButtons & 16) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 32) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 64) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 128) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 256) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 512) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 4096) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 8192) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 16384) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 32768) != 0) ? ButtonState.Pressed : ButtonState.Released);
			this.dPad = new GamePadDPad(((rawState.Gamepad.dwButtons & 1) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 2) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 4) != 0) ? ButtonState.Pressed : ButtonState.Released, ((rawState.Gamepad.dwButtons & 8) != 0) ? ButtonState.Pressed : ButtonState.Released);
			this.thumbSticks = new GamePadThumbSticks(new GamePadThumbSticks.StickValue((float)rawState.Gamepad.sThumbLX / 32767f, (float)rawState.Gamepad.sThumbLY / 32767f), new GamePadThumbSticks.StickValue((float)rawState.Gamepad.sThumbRX / 32767f, (float)rawState.Gamepad.sThumbRY / 32767f));
			this.triggers = new GamePadTriggers((float)rawState.Gamepad.bLeftTrigger / 255f, (float)rawState.Gamepad.bRightTrigger / 255f);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002409 File Offset: 0x00000609
		public uint PacketNumber
		{
			get
			{
				return this.packetNumber;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002411 File Offset: 0x00000611
		public bool IsConnected
		{
			get
			{
				return this.isConnected;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002419 File Offset: 0x00000619
		public GamePadButtons Buttons
		{
			get
			{
				return this.buttons;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002421 File Offset: 0x00000621
		public GamePadDPad DPad
		{
			get
			{
				return this.dPad;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002429 File Offset: 0x00000629
		public GamePadTriggers Triggers
		{
			get
			{
				return this.triggers;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002431 File Offset: 0x00000631
		public GamePadThumbSticks ThumbSticks
		{
			get
			{
				return this.thumbSticks;
			}
		}

		// Token: 0x04000016 RID: 22
		private bool isConnected;

		// Token: 0x04000017 RID: 23
		private uint packetNumber;

		// Token: 0x04000018 RID: 24
		private GamePadButtons buttons;

		// Token: 0x04000019 RID: 25
		private GamePadDPad dPad;

		// Token: 0x0400001A RID: 26
		private GamePadThumbSticks thumbSticks;

		// Token: 0x0400001B RID: 27
		private GamePadTriggers triggers;

		// Token: 0x0200020C RID: 524
		internal struct RawState
		{
			// Token: 0x04000438 RID: 1080
			public uint dwPacketNumber;

			// Token: 0x04000439 RID: 1081
			public GamePadState.RawState.GamePad Gamepad;

			// Token: 0x02000221 RID: 545
			public struct GamePad
			{
				// Token: 0x040004CA RID: 1226
				public ushort dwButtons;

				// Token: 0x040004CB RID: 1227
				public byte bLeftTrigger;

				// Token: 0x040004CC RID: 1228
				public byte bRightTrigger;

				// Token: 0x040004CD RID: 1229
				public short sThumbLX;

				// Token: 0x040004CE RID: 1230
				public short sThumbLY;

				// Token: 0x040004CF RID: 1231
				public short sThumbRX;

				// Token: 0x040004D0 RID: 1232
				public short sThumbRY;
			}
		}

		// Token: 0x0200020D RID: 525
		private enum ButtonsConstants
		{
			// Token: 0x0400043B RID: 1083
			DPadUp = 1,
			// Token: 0x0400043C RID: 1084
			DPadDown,
			// Token: 0x0400043D RID: 1085
			DPadLeft = 4,
			// Token: 0x0400043E RID: 1086
			DPadRight = 8,
			// Token: 0x0400043F RID: 1087
			Start = 16,
			// Token: 0x04000440 RID: 1088
			Back = 32,
			// Token: 0x04000441 RID: 1089
			LeftThumb = 64,
			// Token: 0x04000442 RID: 1090
			RightThumb = 128,
			// Token: 0x04000443 RID: 1091
			LeftShoulder = 256,
			// Token: 0x04000444 RID: 1092
			RightShoulder = 512,
			// Token: 0x04000445 RID: 1093
			A = 4096,
			// Token: 0x04000446 RID: 1094
			B = 8192,
			// Token: 0x04000447 RID: 1095
			X = 16384,
			// Token: 0x04000448 RID: 1096
			Y = 32768
		}
	}
}
