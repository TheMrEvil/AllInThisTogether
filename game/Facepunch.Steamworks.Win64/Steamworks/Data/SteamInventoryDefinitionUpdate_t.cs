using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000185 RID: 389
	internal struct SteamInventoryDefinitionUpdate_t : ICallbackData
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00016C19 File Offset: 0x00014E19
		public int DataSize
		{
			get
			{
				return SteamInventoryDefinitionUpdate_t._datasize;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00016C20 File Offset: 0x00014E20
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamInventoryDefinitionUpdate;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00016C27 File Offset: 0x00014E27
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryDefinitionUpdate_t()
		{
		}

		// Token: 0x04000A72 RID: 2674
		public static int _datasize = Marshal.SizeOf(typeof(SteamInventoryDefinitionUpdate_t));
	}
}
