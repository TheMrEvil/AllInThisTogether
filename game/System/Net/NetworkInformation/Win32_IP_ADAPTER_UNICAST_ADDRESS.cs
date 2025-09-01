using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074D RID: 1869
	internal struct Win32_IP_ADAPTER_UNICAST_ADDRESS
	{
		// Token: 0x0400233C RID: 9020
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x0400233D RID: 9021
		public IntPtr Next;

		// Token: 0x0400233E RID: 9022
		public Win32_SOCKET_ADDRESS Address;

		// Token: 0x0400233F RID: 9023
		public PrefixOrigin PrefixOrigin;

		// Token: 0x04002340 RID: 9024
		public SuffixOrigin SuffixOrigin;

		// Token: 0x04002341 RID: 9025
		public DuplicateAddressDetectionState DadState;

		// Token: 0x04002342 RID: 9026
		public uint ValidLifetime;

		// Token: 0x04002343 RID: 9027
		public uint PreferredLifetime;

		// Token: 0x04002344 RID: 9028
		public uint LeaseLifetime;

		// Token: 0x04002345 RID: 9029
		public byte OnLinkPrefixLength;
	}
}
