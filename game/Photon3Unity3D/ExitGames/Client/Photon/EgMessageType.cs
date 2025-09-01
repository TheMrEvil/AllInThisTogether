using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200001A RID: 26
	internal enum EgMessageType : byte
	{
		// Token: 0x040000CF RID: 207
		Init,
		// Token: 0x040000D0 RID: 208
		InitResponse,
		// Token: 0x040000D1 RID: 209
		Operation,
		// Token: 0x040000D2 RID: 210
		OperationResponse,
		// Token: 0x040000D3 RID: 211
		Event,
		// Token: 0x040000D4 RID: 212
		DisconnectReason,
		// Token: 0x040000D5 RID: 213
		InternalOperationRequest,
		// Token: 0x040000D6 RID: 214
		InternalOperationResponse,
		// Token: 0x040000D7 RID: 215
		Message,
		// Token: 0x040000D8 RID: 216
		RawMessage
	}
}
