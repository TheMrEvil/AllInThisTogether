using System;

namespace Photon.Realtime
{
	// Token: 0x02000027 RID: 39
	public class OperationCode
	{
		// Token: 0x0600011A RID: 282 RVA: 0x000083CF File Offset: 0x000065CF
		public OperationCode()
		{
		}

		// Token: 0x04000150 RID: 336
		[Obsolete("Exchanging encrpytion keys is done internally in the lib now. Don't expect this operation-result.")]
		public const byte ExchangeKeysForEncryption = 250;

		// Token: 0x04000151 RID: 337
		[Obsolete]
		public const byte Join = 255;

		// Token: 0x04000152 RID: 338
		public const byte AuthenticateOnce = 231;

		// Token: 0x04000153 RID: 339
		public const byte Authenticate = 230;

		// Token: 0x04000154 RID: 340
		public const byte JoinLobby = 229;

		// Token: 0x04000155 RID: 341
		public const byte LeaveLobby = 228;

		// Token: 0x04000156 RID: 342
		public const byte CreateGame = 227;

		// Token: 0x04000157 RID: 343
		public const byte JoinGame = 226;

		// Token: 0x04000158 RID: 344
		public const byte JoinRandomGame = 225;

		// Token: 0x04000159 RID: 345
		public const byte Leave = 254;

		// Token: 0x0400015A RID: 346
		public const byte RaiseEvent = 253;

		// Token: 0x0400015B RID: 347
		public const byte SetProperties = 252;

		// Token: 0x0400015C RID: 348
		public const byte GetProperties = 251;

		// Token: 0x0400015D RID: 349
		public const byte ChangeGroups = 248;

		// Token: 0x0400015E RID: 350
		public const byte FindFriends = 222;

		// Token: 0x0400015F RID: 351
		public const byte GetLobbyStats = 221;

		// Token: 0x04000160 RID: 352
		public const byte GetRegions = 220;

		// Token: 0x04000161 RID: 353
		public const byte WebRpc = 219;

		// Token: 0x04000162 RID: 354
		public const byte ServerSettings = 218;

		// Token: 0x04000163 RID: 355
		public const byte GetGameList = 217;
	}
}
