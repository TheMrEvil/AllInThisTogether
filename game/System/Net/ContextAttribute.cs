using System;

namespace System.Net
{
	// Token: 0x020005E5 RID: 1509
	internal enum ContextAttribute
	{
		// Token: 0x04001B46 RID: 6982
		Sizes,
		// Token: 0x04001B47 RID: 6983
		Names,
		// Token: 0x04001B48 RID: 6984
		Lifespan,
		// Token: 0x04001B49 RID: 6985
		DceInfo,
		// Token: 0x04001B4A RID: 6986
		StreamSizes,
		// Token: 0x04001B4B RID: 6987
		Authority = 6,
		// Token: 0x04001B4C RID: 6988
		PackageInfo = 10,
		// Token: 0x04001B4D RID: 6989
		NegotiationInfo = 12,
		// Token: 0x04001B4E RID: 6990
		UniqueBindings = 25,
		// Token: 0x04001B4F RID: 6991
		EndpointBindings,
		// Token: 0x04001B50 RID: 6992
		ClientSpecifiedSpn,
		// Token: 0x04001B51 RID: 6993
		RemoteCertificate = 83,
		// Token: 0x04001B52 RID: 6994
		LocalCertificate,
		// Token: 0x04001B53 RID: 6995
		RootStore,
		// Token: 0x04001B54 RID: 6996
		IssuerListInfoEx = 89,
		// Token: 0x04001B55 RID: 6997
		ConnectionInfo,
		// Token: 0x04001B56 RID: 6998
		UiInfo = 104
	}
}
