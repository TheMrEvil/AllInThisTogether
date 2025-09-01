using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x0200065C RID: 1628
	internal class WebProxyData
	{
		// Token: 0x06003361 RID: 13153 RVA: 0x0000219B File Offset: 0x0000039B
		public WebProxyData()
		{
		}

		// Token: 0x04001E1A RID: 7706
		internal bool bypassOnLocal;

		// Token: 0x04001E1B RID: 7707
		internal bool automaticallyDetectSettings;

		// Token: 0x04001E1C RID: 7708
		internal Uri proxyAddress;

		// Token: 0x04001E1D RID: 7709
		internal Hashtable proxyHostAddresses;

		// Token: 0x04001E1E RID: 7710
		internal Uri scriptLocation;

		// Token: 0x04001E1F RID: 7711
		internal ArrayList bypassList;
	}
}
