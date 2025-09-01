using System;

namespace Photon.Pun
{
	// Token: 0x02000019 RID: 25
	internal class PunEvent
	{
		// Token: 0x06000140 RID: 320 RVA: 0x000088C3 File Offset: 0x00006AC3
		public PunEvent()
		{
		}

		// Token: 0x040000A8 RID: 168
		public const byte RPC = 200;

		// Token: 0x040000A9 RID: 169
		public const byte SendSerialize = 201;

		// Token: 0x040000AA RID: 170
		public const byte Instantiation = 202;

		// Token: 0x040000AB RID: 171
		public const byte CloseConnection = 203;

		// Token: 0x040000AC RID: 172
		public const byte Destroy = 204;

		// Token: 0x040000AD RID: 173
		public const byte RemoveCachedRPCs = 205;

		// Token: 0x040000AE RID: 174
		public const byte SendSerializeReliable = 206;

		// Token: 0x040000AF RID: 175
		public const byte DestroyPlayer = 207;

		// Token: 0x040000B0 RID: 176
		public const byte OwnershipRequest = 209;

		// Token: 0x040000B1 RID: 177
		public const byte OwnershipTransfer = 210;

		// Token: 0x040000B2 RID: 178
		public const byte VacantViewIds = 211;

		// Token: 0x040000B3 RID: 179
		public const byte OwnershipUpdate = 212;
	}
}
