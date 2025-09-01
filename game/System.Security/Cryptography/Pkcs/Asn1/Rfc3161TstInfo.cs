using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000BE RID: 190
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class Rfc3161TstInfo
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x00002145 File Offset: 0x00000345
		public Rfc3161TstInfo()
		{
		}

		// Token: 0x0400035C RID: 860
		internal int Version;

		// Token: 0x0400035D RID: 861
		[ObjectIdentifier(PopulateFriendlyName = true)]
		internal Oid Policy;

		// Token: 0x0400035E RID: 862
		internal MessageImprint MessageImprint;

		// Token: 0x0400035F RID: 863
		[Integer]
		internal ReadOnlyMemory<byte> SerialNumber;

		// Token: 0x04000360 RID: 864
		[GeneralizedTime(DisallowFractions = false)]
		internal DateTimeOffset GenTime;

		// Token: 0x04000361 RID: 865
		[OptionalValue]
		internal Rfc3161Accuracy? Accuracy;

		// Token: 0x04000362 RID: 866
		[DefaultValue(new byte[]
		{
			1,
			1,
			0
		})]
		internal bool Ordering;

		// Token: 0x04000363 RID: 867
		[Integer]
		[OptionalValue]
		internal ReadOnlyMemory<byte>? Nonce;

		// Token: 0x04000364 RID: 868
		[ExpectedTag(0, ExplicitTag = true)]
		[OptionalValue]
		internal GeneralName? Tsa;

		// Token: 0x04000365 RID: 869
		[ExpectedTag(1)]
		[OptionalValue]
		internal X509ExtensionAsn[] Extensions;
	}
}
