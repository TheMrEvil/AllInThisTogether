using System;

namespace Photon.Realtime
{
	// Token: 0x02000022 RID: 34
	public class ErrorCode
	{
		// Token: 0x06000115 RID: 277 RVA: 0x000083A7 File Offset: 0x000065A7
		public ErrorCode()
		{
		}

		// Token: 0x040000CC RID: 204
		public const int Ok = 0;

		// Token: 0x040000CD RID: 205
		public const int OperationNotAllowedInCurrentState = -3;

		// Token: 0x040000CE RID: 206
		[Obsolete("Use InvalidOperation.")]
		public const int InvalidOperationCode = -2;

		// Token: 0x040000CF RID: 207
		public const int InvalidOperation = -2;

		// Token: 0x040000D0 RID: 208
		public const int InternalServerError = -1;

		// Token: 0x040000D1 RID: 209
		public const int InvalidAuthentication = 32767;

		// Token: 0x040000D2 RID: 210
		public const int GameIdAlreadyExists = 32766;

		// Token: 0x040000D3 RID: 211
		public const int GameFull = 32765;

		// Token: 0x040000D4 RID: 212
		public const int GameClosed = 32764;

		// Token: 0x040000D5 RID: 213
		[Obsolete("No longer used, cause random matchmaking is no longer a process.")]
		public const int AlreadyMatched = 32763;

		// Token: 0x040000D6 RID: 214
		public const int ServerFull = 32762;

		// Token: 0x040000D7 RID: 215
		public const int UserBlocked = 32761;

		// Token: 0x040000D8 RID: 216
		public const int NoRandomMatchFound = 32760;

		// Token: 0x040000D9 RID: 217
		public const int GameDoesNotExist = 32758;

		// Token: 0x040000DA RID: 218
		public const int MaxCcuReached = 32757;

		// Token: 0x040000DB RID: 219
		public const int InvalidRegion = 32756;

		// Token: 0x040000DC RID: 220
		public const int CustomAuthenticationFailed = 32755;

		// Token: 0x040000DD RID: 221
		public const int AuthenticationTicketExpired = 32753;

		// Token: 0x040000DE RID: 222
		public const int PluginReportedError = 32752;

		// Token: 0x040000DF RID: 223
		public const int PluginMismatch = 32751;

		// Token: 0x040000E0 RID: 224
		public const int JoinFailedPeerAlreadyJoined = 32750;

		// Token: 0x040000E1 RID: 225
		public const int JoinFailedFoundInactiveJoiner = 32749;

		// Token: 0x040000E2 RID: 226
		public const int JoinFailedWithRejoinerNotFound = 32748;

		// Token: 0x040000E3 RID: 227
		public const int JoinFailedFoundExcludedUserId = 32747;

		// Token: 0x040000E4 RID: 228
		public const int JoinFailedFoundActiveJoiner = 32746;

		// Token: 0x040000E5 RID: 229
		public const int HttpLimitReached = 32745;

		// Token: 0x040000E6 RID: 230
		public const int ExternalHttpCallFailed = 32744;

		// Token: 0x040000E7 RID: 231
		public const int OperationLimitReached = 32743;

		// Token: 0x040000E8 RID: 232
		public const int SlotError = 32742;

		// Token: 0x040000E9 RID: 233
		public const int InvalidEncryptionParameters = 32741;
	}
}
