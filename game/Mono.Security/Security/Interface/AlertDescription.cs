using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000035 RID: 53
	public enum AlertDescription : byte
	{
		// Token: 0x04000128 RID: 296
		CloseNotify,
		// Token: 0x04000129 RID: 297
		UnexpectedMessage = 10,
		// Token: 0x0400012A RID: 298
		BadRecordMAC = 20,
		// Token: 0x0400012B RID: 299
		DecryptionFailed_RESERVED,
		// Token: 0x0400012C RID: 300
		RecordOverflow,
		// Token: 0x0400012D RID: 301
		DecompressionFailure = 30,
		// Token: 0x0400012E RID: 302
		HandshakeFailure = 40,
		// Token: 0x0400012F RID: 303
		NoCertificate_RESERVED,
		// Token: 0x04000130 RID: 304
		BadCertificate,
		// Token: 0x04000131 RID: 305
		UnsupportedCertificate,
		// Token: 0x04000132 RID: 306
		CertificateRevoked,
		// Token: 0x04000133 RID: 307
		CertificateExpired,
		// Token: 0x04000134 RID: 308
		CertificateUnknown,
		// Token: 0x04000135 RID: 309
		IlegalParameter,
		// Token: 0x04000136 RID: 310
		UnknownCA,
		// Token: 0x04000137 RID: 311
		AccessDenied,
		// Token: 0x04000138 RID: 312
		DecodeError,
		// Token: 0x04000139 RID: 313
		DecryptError,
		// Token: 0x0400013A RID: 314
		ExportRestriction = 60,
		// Token: 0x0400013B RID: 315
		ProtocolVersion = 70,
		// Token: 0x0400013C RID: 316
		InsuficientSecurity,
		// Token: 0x0400013D RID: 317
		InternalError = 80,
		// Token: 0x0400013E RID: 318
		UserCancelled = 90,
		// Token: 0x0400013F RID: 319
		NoRenegotiation = 100,
		// Token: 0x04000140 RID: 320
		UnsupportedExtension = 110
	}
}
