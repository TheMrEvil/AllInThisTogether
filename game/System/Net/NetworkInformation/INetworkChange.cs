using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200070F RID: 1807
	internal interface INetworkChange : IDisposable
	{
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060039CB RID: 14795
		// (remove) Token: 0x060039CC RID: 14796
		event NetworkAddressChangedEventHandler NetworkAddressChanged;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060039CD RID: 14797
		// (remove) Token: 0x060039CE RID: 14798
		event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged;

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060039CF RID: 14799
		bool HasRegisteredEvents { get; }
	}
}
