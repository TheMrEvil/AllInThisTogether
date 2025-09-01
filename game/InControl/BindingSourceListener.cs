using System;

namespace InControl
{
	// Token: 0x0200000D RID: 13
	public interface BindingSourceListener
	{
		// Token: 0x0600003F RID: 63
		void Reset();

		// Token: 0x06000040 RID: 64
		BindingSource Listen(BindingListenOptions listenOptions, InputDevice device);
	}
}
