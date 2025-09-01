using System;

namespace System.Data
{
	// Token: 0x020000F8 RID: 248
	internal sealed class Operators
	{
		// Token: 0x06000EB6 RID: 3766 RVA: 0x00003D93 File Offset: 0x00001F93
		private Operators()
		{
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003D470 File Offset: 0x0003B670
		internal static bool IsArithmetical(int op)
		{
			return op == 15 || op == 16 || op == 17 || op == 18 || op == 20;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003D48D File Offset: 0x0003B68D
		internal static bool IsLogical(int op)
		{
			return op == 26 || op == 27 || op == 3 || op == 13 || op == 39;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003D4A9 File Offset: 0x0003B6A9
		internal static bool IsRelational(int op)
		{
			return 7 <= op && op <= 12;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003D4B9 File Offset: 0x0003B6B9
		internal static int Priority(int op)
		{
			if (op > Operators.s_priority.Length)
			{
				return 24;
			}
			return Operators.s_priority[op];
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0003D4D0 File Offset: 0x0003B6D0
		internal static string ToString(int op)
		{
			string result;
			if (op <= Operators.s_looks.Length)
			{
				result = Operators.s_looks[op];
			}
			else
			{
				result = "Unknown op";
			}
			return result;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003D4F8 File Offset: 0x0003B6F8
		// Note: this type is marked as 'beforefieldinit'.
		static Operators()
		{
		}

		// Token: 0x04000919 RID: 2329
		internal const int Noop = 0;

		// Token: 0x0400091A RID: 2330
		internal const int Negative = 1;

		// Token: 0x0400091B RID: 2331
		internal const int UnaryPlus = 2;

		// Token: 0x0400091C RID: 2332
		internal const int Not = 3;

		// Token: 0x0400091D RID: 2333
		internal const int BetweenAnd = 4;

		// Token: 0x0400091E RID: 2334
		internal const int In = 5;

		// Token: 0x0400091F RID: 2335
		internal const int Between = 6;

		// Token: 0x04000920 RID: 2336
		internal const int EqualTo = 7;

		// Token: 0x04000921 RID: 2337
		internal const int GreaterThen = 8;

		// Token: 0x04000922 RID: 2338
		internal const int LessThen = 9;

		// Token: 0x04000923 RID: 2339
		internal const int GreaterOrEqual = 10;

		// Token: 0x04000924 RID: 2340
		internal const int LessOrEqual = 11;

		// Token: 0x04000925 RID: 2341
		internal const int NotEqual = 12;

		// Token: 0x04000926 RID: 2342
		internal const int Is = 13;

		// Token: 0x04000927 RID: 2343
		internal const int Like = 14;

		// Token: 0x04000928 RID: 2344
		internal const int Plus = 15;

		// Token: 0x04000929 RID: 2345
		internal const int Minus = 16;

		// Token: 0x0400092A RID: 2346
		internal const int Multiply = 17;

		// Token: 0x0400092B RID: 2347
		internal const int Divide = 18;

		// Token: 0x0400092C RID: 2348
		internal const int Modulo = 20;

		// Token: 0x0400092D RID: 2349
		internal const int BitwiseAnd = 22;

		// Token: 0x0400092E RID: 2350
		internal const int BitwiseOr = 23;

		// Token: 0x0400092F RID: 2351
		internal const int BitwiseXor = 24;

		// Token: 0x04000930 RID: 2352
		internal const int BitwiseNot = 25;

		// Token: 0x04000931 RID: 2353
		internal const int And = 26;

		// Token: 0x04000932 RID: 2354
		internal const int Or = 27;

		// Token: 0x04000933 RID: 2355
		internal const int Proc = 28;

		// Token: 0x04000934 RID: 2356
		internal const int Iff = 29;

		// Token: 0x04000935 RID: 2357
		internal const int Qual = 30;

		// Token: 0x04000936 RID: 2358
		internal const int Dot = 31;

		// Token: 0x04000937 RID: 2359
		internal const int Null = 32;

		// Token: 0x04000938 RID: 2360
		internal const int True = 33;

		// Token: 0x04000939 RID: 2361
		internal const int False = 34;

		// Token: 0x0400093A RID: 2362
		internal const int Date = 35;

		// Token: 0x0400093B RID: 2363
		internal const int GenUniqueId = 36;

		// Token: 0x0400093C RID: 2364
		internal const int GenGUID = 37;

		// Token: 0x0400093D RID: 2365
		internal const int GUID = 38;

		// Token: 0x0400093E RID: 2366
		internal const int IsNot = 39;

		// Token: 0x0400093F RID: 2367
		internal const int priStart = 0;

		// Token: 0x04000940 RID: 2368
		internal const int priSubstr = 1;

		// Token: 0x04000941 RID: 2369
		internal const int priParen = 2;

		// Token: 0x04000942 RID: 2370
		internal const int priLow = 3;

		// Token: 0x04000943 RID: 2371
		internal const int priImp = 4;

		// Token: 0x04000944 RID: 2372
		internal const int priEqv = 5;

		// Token: 0x04000945 RID: 2373
		internal const int priXor = 6;

		// Token: 0x04000946 RID: 2374
		internal const int priOr = 7;

		// Token: 0x04000947 RID: 2375
		internal const int priAnd = 8;

		// Token: 0x04000948 RID: 2376
		internal const int priNot = 9;

		// Token: 0x04000949 RID: 2377
		internal const int priIs = 10;

		// Token: 0x0400094A RID: 2378
		internal const int priBetweenInLike = 11;

		// Token: 0x0400094B RID: 2379
		internal const int priBetweenAnd = 12;

		// Token: 0x0400094C RID: 2380
		internal const int priRelOp = 13;

		// Token: 0x0400094D RID: 2381
		internal const int priConcat = 14;

		// Token: 0x0400094E RID: 2382
		internal const int priContains = 15;

		// Token: 0x0400094F RID: 2383
		internal const int priPlusMinus = 16;

		// Token: 0x04000950 RID: 2384
		internal const int priMod = 17;

		// Token: 0x04000951 RID: 2385
		internal const int priIDiv = 18;

		// Token: 0x04000952 RID: 2386
		internal const int priMulDiv = 19;

		// Token: 0x04000953 RID: 2387
		internal const int priNeg = 20;

		// Token: 0x04000954 RID: 2388
		internal const int priExp = 21;

		// Token: 0x04000955 RID: 2389
		internal const int priProc = 22;

		// Token: 0x04000956 RID: 2390
		internal const int priDot = 23;

		// Token: 0x04000957 RID: 2391
		internal const int priMax = 24;

		// Token: 0x04000958 RID: 2392
		private static readonly int[] s_priority = new int[]
		{
			0,
			20,
			20,
			9,
			12,
			11,
			11,
			13,
			13,
			13,
			13,
			13,
			13,
			10,
			11,
			16,
			16,
			19,
			19,
			18,
			17,
			21,
			8,
			7,
			6,
			9,
			8,
			7,
			2,
			22,
			23,
			23,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24,
			24
		};

		// Token: 0x04000959 RID: 2393
		private static readonly string[] s_looks = new string[]
		{
			"",
			"-",
			"+",
			"Not",
			"BetweenAnd",
			"In",
			"Between",
			"=",
			">",
			"<",
			">=",
			"<=",
			"<>",
			"Is",
			"Like",
			"+",
			"-",
			"*",
			"/",
			"\\",
			"Mod",
			"**",
			"&",
			"|",
			"^",
			"~",
			"And",
			"Or",
			"Proc",
			"Iff",
			".",
			".",
			"Null",
			"True",
			"False",
			"Date",
			"GenUniqueId()",
			"GenGuid()",
			"Guid {..}",
			"Is Not"
		};
	}
}
