using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000245 RID: 581
	internal enum ParsingErrorState
	{
		// Token: 0x040012FD RID: 4861
		Undefined,
		// Token: 0x040012FE RID: 4862
		FedAuthInfoLengthTooShortForCountOfInfoIds,
		// Token: 0x040012FF RID: 4863
		FedAuthInfoLengthTooShortForData,
		// Token: 0x04001300 RID: 4864
		FedAuthInfoFailedToReadCountOfInfoIds,
		// Token: 0x04001301 RID: 4865
		FedAuthInfoFailedToReadTokenStream,
		// Token: 0x04001302 RID: 4866
		FedAuthInfoInvalidOffset,
		// Token: 0x04001303 RID: 4867
		FedAuthInfoFailedToReadData,
		// Token: 0x04001304 RID: 4868
		FedAuthInfoDataNotUnicode,
		// Token: 0x04001305 RID: 4869
		FedAuthInfoDoesNotContainStsurlAndSpn,
		// Token: 0x04001306 RID: 4870
		FedAuthInfoNotReceived,
		// Token: 0x04001307 RID: 4871
		FedAuthNotAcknowledged,
		// Token: 0x04001308 RID: 4872
		FedAuthFeatureAckContainsExtraData,
		// Token: 0x04001309 RID: 4873
		FedAuthFeatureAckUnknownLibraryType,
		// Token: 0x0400130A RID: 4874
		UnrequestedFeatureAckReceived,
		// Token: 0x0400130B RID: 4875
		UnknownFeatureAck,
		// Token: 0x0400130C RID: 4876
		InvalidTdsTokenReceived,
		// Token: 0x0400130D RID: 4877
		SessionStateLengthTooShort,
		// Token: 0x0400130E RID: 4878
		SessionStateInvalidStatus,
		// Token: 0x0400130F RID: 4879
		CorruptedTdsStream,
		// Token: 0x04001310 RID: 4880
		ProcessSniPacketFailed,
		// Token: 0x04001311 RID: 4881
		FedAuthRequiredPreLoginResponseInvalidValue
	}
}
