using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000E3 RID: 227
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PersonaStateChange_t : ICallbackData
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00015361 File Offset: 0x00013561
		public int DataSize
		{
			get
			{
				return PersonaStateChange_t._datasize;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00015368 File Offset: 0x00013568
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.PersonaStateChange;
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001536F File Offset: 0x0001356F
		// Note: this type is marked as 'beforefieldinit'.
		static PersonaStateChange_t()
		{
		}

		// Token: 0x04000811 RID: 2065
		internal ulong SteamID;

		// Token: 0x04000812 RID: 2066
		internal int ChangeFlags;

		// Token: 0x04000813 RID: 2067
		public static int _datasize = Marshal.SizeOf(typeof(PersonaStateChange_t));
	}
}
