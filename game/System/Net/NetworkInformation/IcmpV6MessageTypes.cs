using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000735 RID: 1845
	internal class IcmpV6MessageTypes
	{
		// Token: 0x06003AE2 RID: 15074 RVA: 0x0000219B File Offset: 0x0000039B
		public IcmpV6MessageTypes()
		{
		}

		// Token: 0x04002290 RID: 8848
		public const int DestinationUnreachable = 1;

		// Token: 0x04002291 RID: 8849
		public const int PacketTooBig = 2;

		// Token: 0x04002292 RID: 8850
		public const int TimeExceeded = 3;

		// Token: 0x04002293 RID: 8851
		public const int ParameterProblem = 4;

		// Token: 0x04002294 RID: 8852
		public const int EchoRequest = 128;

		// Token: 0x04002295 RID: 8853
		public const int EchoReply = 129;

		// Token: 0x04002296 RID: 8854
		public const int GroupMembershipQuery = 130;

		// Token: 0x04002297 RID: 8855
		public const int GroupMembershipReport = 131;

		// Token: 0x04002298 RID: 8856
		public const int GroupMembershipReduction = 132;

		// Token: 0x04002299 RID: 8857
		public const int RouterSolicitation = 133;

		// Token: 0x0400229A RID: 8858
		public const int RouterAdvertisement = 134;

		// Token: 0x0400229B RID: 8859
		public const int NeighborSolicitation = 135;

		// Token: 0x0400229C RID: 8860
		public const int NeighborAdvertisement = 136;

		// Token: 0x0400229D RID: 8861
		public const int Redirect = 137;

		// Token: 0x0400229E RID: 8862
		public const int RouterRenumbering = 138;
	}
}
