using System;

namespace Photon.Realtime
{
	// Token: 0x02000025 RID: 37
	public class EventCode
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000083BF File Offset: 0x000065BF
		public EventCode()
		{
		}

		// Token: 0x040000F9 RID: 249
		public const byte GameList = 230;

		// Token: 0x040000FA RID: 250
		public const byte GameListUpdate = 229;

		// Token: 0x040000FB RID: 251
		public const byte QueueState = 228;

		// Token: 0x040000FC RID: 252
		public const byte Match = 227;

		// Token: 0x040000FD RID: 253
		public const byte AppStats = 226;

		// Token: 0x040000FE RID: 254
		public const byte LobbyStats = 224;

		// Token: 0x040000FF RID: 255
		[Obsolete("TCP routing was removed after becoming obsolete.")]
		public const byte AzureNodeInfo = 210;

		// Token: 0x04000100 RID: 256
		public const byte Join = 255;

		// Token: 0x04000101 RID: 257
		public const byte Leave = 254;

		// Token: 0x04000102 RID: 258
		public const byte PropertiesChanged = 253;

		// Token: 0x04000103 RID: 259
		[Obsolete("Use PropertiesChanged now.")]
		public const byte SetProperties = 253;

		// Token: 0x04000104 RID: 260
		public const byte ErrorInfo = 251;

		// Token: 0x04000105 RID: 261
		public const byte CacheSliceChanged = 250;

		// Token: 0x04000106 RID: 262
		public const byte AuthEvent = 223;
	}
}
