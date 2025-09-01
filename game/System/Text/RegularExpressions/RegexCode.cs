using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001FE RID: 510
	internal sealed class RegexCode
	{
		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003A250 File Offset: 0x00038450
		public RegexCode(int[] codes, List<string> stringlist, int trackcount, Hashtable caps, int capsize, RegexBoyerMoore bmPrefix, RegexPrefix? fcPrefix, int anchors, bool rightToLeft)
		{
			this.Codes = codes;
			this.Strings = stringlist.ToArray();
			this.TrackCount = trackcount;
			this.Caps = caps;
			this.CapSize = capsize;
			this.BMPrefix = bmPrefix;
			this.FCPrefix = fcPrefix;
			this.Anchors = anchors;
			this.RightToLeft = rightToLeft;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003A2B0 File Offset: 0x000384B0
		public static bool OpcodeBacktracks(int Op)
		{
			Op &= 63;
			switch (Op)
			{
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 38:
				return true;
			}
			return false;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003A360 File Offset: 0x00038560
		public static int OpcodeSize(int opcode)
		{
			opcode &= 63;
			switch (opcode)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 28:
			case 29:
			case 32:
				return 3;
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 37:
			case 38:
			case 39:
				return 2;
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 30:
			case 31:
			case 33:
			case 34:
			case 35:
			case 36:
			case 40:
			case 41:
			case 42:
				return 1;
			default:
				throw new ArgumentException(SR.Format("Unexpected opcode in regular expression generation: {0}.", opcode.ToString(CultureInfo.CurrentCulture)));
			}
		}

		// Token: 0x0400086A RID: 2154
		public const int Onerep = 0;

		// Token: 0x0400086B RID: 2155
		public const int Notonerep = 1;

		// Token: 0x0400086C RID: 2156
		public const int Setrep = 2;

		// Token: 0x0400086D RID: 2157
		public const int Oneloop = 3;

		// Token: 0x0400086E RID: 2158
		public const int Notoneloop = 4;

		// Token: 0x0400086F RID: 2159
		public const int Setloop = 5;

		// Token: 0x04000870 RID: 2160
		public const int Onelazy = 6;

		// Token: 0x04000871 RID: 2161
		public const int Notonelazy = 7;

		// Token: 0x04000872 RID: 2162
		public const int Setlazy = 8;

		// Token: 0x04000873 RID: 2163
		public const int One = 9;

		// Token: 0x04000874 RID: 2164
		public const int Notone = 10;

		// Token: 0x04000875 RID: 2165
		public const int Set = 11;

		// Token: 0x04000876 RID: 2166
		public const int Multi = 12;

		// Token: 0x04000877 RID: 2167
		public const int Ref = 13;

		// Token: 0x04000878 RID: 2168
		public const int Bol = 14;

		// Token: 0x04000879 RID: 2169
		public const int Eol = 15;

		// Token: 0x0400087A RID: 2170
		public const int Boundary = 16;

		// Token: 0x0400087B RID: 2171
		public const int Nonboundary = 17;

		// Token: 0x0400087C RID: 2172
		public const int Beginning = 18;

		// Token: 0x0400087D RID: 2173
		public const int Start = 19;

		// Token: 0x0400087E RID: 2174
		public const int EndZ = 20;

		// Token: 0x0400087F RID: 2175
		public const int End = 21;

		// Token: 0x04000880 RID: 2176
		public const int Nothing = 22;

		// Token: 0x04000881 RID: 2177
		public const int Lazybranch = 23;

		// Token: 0x04000882 RID: 2178
		public const int Branchmark = 24;

		// Token: 0x04000883 RID: 2179
		public const int Lazybranchmark = 25;

		// Token: 0x04000884 RID: 2180
		public const int Nullcount = 26;

		// Token: 0x04000885 RID: 2181
		public const int Setcount = 27;

		// Token: 0x04000886 RID: 2182
		public const int Branchcount = 28;

		// Token: 0x04000887 RID: 2183
		public const int Lazybranchcount = 29;

		// Token: 0x04000888 RID: 2184
		public const int Nullmark = 30;

		// Token: 0x04000889 RID: 2185
		public const int Setmark = 31;

		// Token: 0x0400088A RID: 2186
		public const int Capturemark = 32;

		// Token: 0x0400088B RID: 2187
		public const int Getmark = 33;

		// Token: 0x0400088C RID: 2188
		public const int Setjump = 34;

		// Token: 0x0400088D RID: 2189
		public const int Backjump = 35;

		// Token: 0x0400088E RID: 2190
		public const int Forejump = 36;

		// Token: 0x0400088F RID: 2191
		public const int Testref = 37;

		// Token: 0x04000890 RID: 2192
		public const int Goto = 38;

		// Token: 0x04000891 RID: 2193
		public const int Prune = 39;

		// Token: 0x04000892 RID: 2194
		public const int Stop = 40;

		// Token: 0x04000893 RID: 2195
		public const int ECMABoundary = 41;

		// Token: 0x04000894 RID: 2196
		public const int NonECMABoundary = 42;

		// Token: 0x04000895 RID: 2197
		public const int Mask = 63;

		// Token: 0x04000896 RID: 2198
		public const int Rtl = 64;

		// Token: 0x04000897 RID: 2199
		public const int Back = 128;

		// Token: 0x04000898 RID: 2200
		public const int Back2 = 256;

		// Token: 0x04000899 RID: 2201
		public const int Ci = 512;

		// Token: 0x0400089A RID: 2202
		public readonly int[] Codes;

		// Token: 0x0400089B RID: 2203
		public readonly string[] Strings;

		// Token: 0x0400089C RID: 2204
		public readonly int TrackCount;

		// Token: 0x0400089D RID: 2205
		public readonly Hashtable Caps;

		// Token: 0x0400089E RID: 2206
		public readonly int CapSize;

		// Token: 0x0400089F RID: 2207
		public readonly RegexPrefix? FCPrefix;

		// Token: 0x040008A0 RID: 2208
		public readonly RegexBoyerMoore BMPrefix;

		// Token: 0x040008A1 RID: 2209
		public readonly int Anchors;

		// Token: 0x040008A2 RID: 2210
		public readonly bool RightToLeft;
	}
}
