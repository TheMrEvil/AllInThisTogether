using System;
using System.Runtime.InteropServices;

namespace XInputDotNetPure
{
	// Token: 0x02000002 RID: 2
	internal class Imports
	{
		// Token: 0x06000001 RID: 1
		[DllImport("XInputInterface32", EntryPoint = "XInputGamePadGetState")]
		public static extern uint XInputGamePadGetState32(uint playerIndex, IntPtr state);

		// Token: 0x06000002 RID: 2
		[DllImport("XInputInterface32", EntryPoint = "XInputGamePadSetState")]
		public static extern void XInputGamePadSetState32(uint playerIndex, float leftMotor, float rightMotor);

		// Token: 0x06000003 RID: 3
		[DllImport("XInputInterface64", EntryPoint = "XInputGamePadGetState")]
		public static extern uint XInputGamePadGetState64(uint playerIndex, IntPtr state);

		// Token: 0x06000004 RID: 4
		[DllImport("XInputInterface64", EntryPoint = "XInputGamePadSetState")]
		public static extern void XInputGamePadSetState64(uint playerIndex, float leftMotor, float rightMotor);

		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		public static uint XInputGamePadGetState(uint playerIndex, IntPtr state)
		{
			if (IntPtr.Size == 4)
			{
				return Imports.XInputGamePadGetState32(playerIndex, state);
			}
			return Imports.XInputGamePadGetState64(playerIndex, state);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002069 File Offset: 0x00000269
		public static void XInputGamePadSetState(uint playerIndex, float leftMotor, float rightMotor)
		{
			if (IntPtr.Size == 4)
			{
				Imports.XInputGamePadSetState32(playerIndex, leftMotor, rightMotor);
				return;
			}
			Imports.XInputGamePadSetState64(playerIndex, leftMotor, rightMotor);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002084 File Offset: 0x00000284
		public Imports()
		{
		}
	}
}
