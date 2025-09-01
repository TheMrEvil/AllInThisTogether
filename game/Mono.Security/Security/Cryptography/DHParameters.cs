using System;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public struct DHParameters
	{
		// Token: 0x040002AC RID: 684
		public byte[] P;

		// Token: 0x040002AD RID: 685
		public byte[] G;

		// Token: 0x040002AE RID: 686
		[NonSerialized]
		public byte[] X;
	}
}
