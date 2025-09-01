using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000FB RID: 251
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GamepadTextInputDismissed_t : ICallbackData
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0001571E File Offset: 0x0001391E
		public int DataSize
		{
			get
			{
				return GamepadTextInputDismissed_t._datasize;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00015725 File Offset: 0x00013925
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GamepadTextInputDismissed;
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001572C File Offset: 0x0001392C
		// Note: this type is marked as 'beforefieldinit'.
		static GamepadTextInputDismissed_t()
		{
		}

		// Token: 0x0400085B RID: 2139
		[MarshalAs(UnmanagedType.I1)]
		internal bool Submitted;

		// Token: 0x0400085C RID: 2140
		internal uint SubmittedText;

		// Token: 0x0400085D RID: 2141
		public static int _datasize = Marshal.SizeOf(typeof(GamepadTextInputDismissed_t));
	}
}
