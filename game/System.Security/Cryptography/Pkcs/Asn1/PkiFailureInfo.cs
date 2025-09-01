using System;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000BC RID: 188
	[Flags]
	internal enum PkiFailureInfo
	{
		// Token: 0x04000338 RID: 824
		None = 0,
		// Token: 0x04000339 RID: 825
		BadAlg = 1,
		// Token: 0x0400033A RID: 826
		BadMessageCheck = 2,
		// Token: 0x0400033B RID: 827
		BadRequest = 4,
		// Token: 0x0400033C RID: 828
		BadTime = 8,
		// Token: 0x0400033D RID: 829
		BadCertId = 16,
		// Token: 0x0400033E RID: 830
		BadDataFormat = 32,
		// Token: 0x0400033F RID: 831
		WrongAuthority = 64,
		// Token: 0x04000340 RID: 832
		IncorrectData = 128,
		// Token: 0x04000341 RID: 833
		MissingTimeStamp = 256,
		// Token: 0x04000342 RID: 834
		BadPop = 512,
		// Token: 0x04000343 RID: 835
		CertRevoked = 1024,
		// Token: 0x04000344 RID: 836
		CertConfirmed = 2048,
		// Token: 0x04000345 RID: 837
		WrongIntegrity = 4096,
		// Token: 0x04000346 RID: 838
		BadRecipientNonce = 8192,
		// Token: 0x04000347 RID: 839
		TimeNotAvailable = 16384,
		// Token: 0x04000348 RID: 840
		UnacceptedPolicy = 32768,
		// Token: 0x04000349 RID: 841
		UnacceptedExtension = 65536,
		// Token: 0x0400034A RID: 842
		AddInfoNotAvailable = 131072,
		// Token: 0x0400034B RID: 843
		BadSenderNonce = 262144,
		// Token: 0x0400034C RID: 844
		BadCertTemplate = 524288,
		// Token: 0x0400034D RID: 845
		SignerNotTrusted = 1048576,
		// Token: 0x0400034E RID: 846
		TransactionIdInUse = 2097152,
		// Token: 0x0400034F RID: 847
		UnsupportedVersion = 4194304,
		// Token: 0x04000350 RID: 848
		NotAuthorized = 8388608,
		// Token: 0x04000351 RID: 849
		SystemUnavail = 16777216,
		// Token: 0x04000352 RID: 850
		SystemFailure = 33554432,
		// Token: 0x04000353 RID: 851
		DuplicateCertReq = 67108864
	}
}
