using System;
using System.Runtime.InteropServices;

namespace XInputDotNetPure
{
	// Token: 0x0200000A RID: 10
	public class GamePad
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000243C File Offset: 0x0000063C
		public static GamePadState GetState(PlayerIndex playerIndex)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(GamePadState.RawState)));
			int num = (int)Imports.XInputGamePadGetState((uint)playerIndex, intPtr);
			GamePadState.RawState rawState = (GamePadState.RawState)Marshal.PtrToStructure(intPtr, typeof(GamePadState.RawState));
			return new GamePadState(num == 0, rawState);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002484 File Offset: 0x00000684
		public static void SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			Imports.XInputGamePadSetState((uint)playerIndex, leftMotor, rightMotor);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000248E File Offset: 0x0000068E
		public GamePad()
		{
		}
	}
}
