using System;

namespace Steamworks
{
	// Token: 0x02000005 RID: 5
	internal interface ICallbackData
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8
		CallbackType CallbackType { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9
		int DataSize { get; }
	}
}
