using System;

namespace System.IO.Compression
{
	// Token: 0x0200000C RID: 12
	internal static class BrotliUtils
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002E22 File Offset: 0x00001022
		internal static int GetQualityFromCompressionLevel(CompressionLevel level)
		{
			switch (level)
			{
			case CompressionLevel.Optimal:
				return 11;
			case CompressionLevel.Fastest:
				return 1;
			case CompressionLevel.NoCompression:
				return 0;
			default:
				return (int)level;
			}
		}

		// Token: 0x04000099 RID: 153
		public const int WindowBits_Min = 10;

		// Token: 0x0400009A RID: 154
		public const int WindowBits_Default = 22;

		// Token: 0x0400009B RID: 155
		public const int WindowBits_Max = 24;

		// Token: 0x0400009C RID: 156
		public const int Quality_Min = 0;

		// Token: 0x0400009D RID: 157
		public const int Quality_Default = 11;

		// Token: 0x0400009E RID: 158
		public const int Quality_Max = 11;

		// Token: 0x0400009F RID: 159
		public const int MaxInputSize = 2147483132;
	}
}
