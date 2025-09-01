using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200061A RID: 1562
	internal interface IInternalMessage
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06003AF3 RID: 15091
		// (set) Token: 0x06003AF4 RID: 15092
		Identity TargetIdentity { get; set; }

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06003AF5 RID: 15093
		// (set) Token: 0x06003AF6 RID: 15094
		string Uri { get; set; }

		// Token: 0x06003AF7 RID: 15095
		bool HasProperties();
	}
}
