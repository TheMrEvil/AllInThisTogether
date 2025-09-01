using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public struct SteamInputActionEvent_t
	{
		// Token: 0x04000AB6 RID: 2742
		public InputHandle_t controllerHandle;

		// Token: 0x04000AB7 RID: 2743
		public ESteamInputActionEventType eEventType;

		// Token: 0x04000AB8 RID: 2744
		public SteamInputActionEvent_t.OptionValue m_val;

		// Token: 0x020001EF RID: 495
		[Serializable]
		public struct AnalogAction_t
		{
			// Token: 0x04000B29 RID: 2857
			public InputAnalogActionHandle_t actionHandle;

			// Token: 0x04000B2A RID: 2858
			public InputAnalogActionData_t analogActionData;
		}

		// Token: 0x020001F0 RID: 496
		[Serializable]
		public struct DigitalAction_t
		{
			// Token: 0x04000B2B RID: 2859
			public InputDigitalActionHandle_t actionHandle;

			// Token: 0x04000B2C RID: 2860
			public InputDigitalActionData_t digitalActionData;
		}

		// Token: 0x020001F1 RID: 497
		[Serializable]
		[StructLayout(LayoutKind.Explicit)]
		public struct OptionValue
		{
			// Token: 0x04000B2D RID: 2861
			[FieldOffset(0)]
			public SteamInputActionEvent_t.AnalogAction_t analogAction;

			// Token: 0x04000B2E RID: 2862
			[FieldOffset(0)]
			public SteamInputActionEvent_t.DigitalAction_t digitalAction;
		}
	}
}
