using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using QFSW.QC.Containers;
using QFSW.QC.Pooling;

namespace QFSW.QC
{
	// Token: 0x02000051 RID: 81
	public static class TextProcessing
	{
		// Token: 0x0600019E RID: 414 RVA: 0x000082EE File Offset: 0x000064EE
		public static int GetMaxScopeDepthAtEnd(this string input)
		{
			return input.GetMaxScopeDepthAtEnd(TextProcessing.DefaultLeftScopers, TextProcessing.DefaultRightScopers);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008300 File Offset: 0x00006500
		public static int GetMaxScopeDepthAtEnd(this string input, char leftScoper, char rightScoper)
		{
			return input.GetMaxScopeDepthAtEnd(leftScoper.AsArraySingle<char>(), rightScoper.AsArraySingle<char>());
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008314 File Offset: 0x00006514
		public static int GetMaxScopeDepthAtEnd<T>(this string input, T leftScopers, T rightScopers) where T : IReadOnlyList<char>
		{
			return input.GetMaxScopeDepthAt(input.Length - 1, leftScopers, rightScopers);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008326 File Offset: 0x00006526
		public static int GetMaxScopeDepthAt(this string input, int cursor)
		{
			return input.GetMaxScopeDepthAt(cursor, TextProcessing.DefaultLeftScopers, TextProcessing.DefaultRightScopers);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008339 File Offset: 0x00006539
		public static int GetMaxScopeDepthAt(this string input, int cursor, char leftScoper, char rightScoper)
		{
			return input.GetMaxScopeDepthAt(cursor, leftScoper.AsArraySingle<char>(), rightScoper.AsArraySingle<char>());
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008350 File Offset: 0x00006550
		public static int GetMaxScopeDepthAt<T>(this string input, int cursor, T leftScopers, T rightScopers) where T : IReadOnlyList<char>
		{
			int[] array = new int[leftScopers.Count];
			for (int i = 0; i <= cursor; i++)
			{
				if (i == 0 || input[i - 1] != '\\')
				{
					for (int j = 0; j < leftScopers.Count; j++)
					{
						char c = leftScopers[j];
						char c2 = rightScopers[j];
						if (input[i] == c && c == c2)
						{
							array[j] = 1 - array[j];
						}
						else if (input[i] == c)
						{
							array[j]++;
						}
						else if (input[i] == c2)
						{
							array[j]--;
						}
					}
				}
			}
			return array.Max();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008417 File Offset: 0x00006617
		public static string ReduceScope(this string input)
		{
			return input.ReduceScope(TextProcessing.DefaultLeftScopers, TextProcessing.DefaultRightScopers, TextProcessing.ReduceScopeOptions.Default);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000842E File Offset: 0x0000662E
		public static string ReduceScope(this string input, TextProcessing.ReduceScopeOptions options)
		{
			return input.ReduceScope(TextProcessing.DefaultLeftScopers, TextProcessing.DefaultRightScopers, options);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008441 File Offset: 0x00006641
		public static string ReduceScope(this string input, char leftScoper, char rightScoper)
		{
			return input.ReduceScope(leftScoper.AsArraySingle<char>(), rightScoper.AsArraySingle<char>(), TextProcessing.ReduceScopeOptions.Default);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000845A File Offset: 0x0000665A
		public static string ReduceScope(this string input, char leftScoper, char rightScoper, TextProcessing.ReduceScopeOptions options)
		{
			return input.ReduceScope(leftScoper.AsArraySingle<char>(), rightScoper.AsArraySingle<char>(), options);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000846F File Offset: 0x0000666F
		public static string ReduceScope<T>(this string input, T leftScopers, T rightScopers) where T : IReadOnlyList<char>
		{
			return input.ReduceScope(leftScopers, rightScopers, TextProcessing.ReduceScopeOptions.Default);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00008480 File Offset: 0x00006680
		public static string ReduceScope<T>(this string input, T leftScopers, T rightScopers, TextProcessing.ReduceScopeOptions options) where T : IReadOnlyList<char>
		{
			TextProcessing.<>c__DisplayClass16_0<T> CS$<>8__locals1;
			CS$<>8__locals1.input = input;
			if (leftScopers.Count != rightScopers.Count)
			{
				throw new ArgumentException("There must be an equal number of corresponding left and right scopers");
			}
			if (string.IsNullOrWhiteSpace(CS$<>8__locals1.input))
			{
				return string.Empty;
			}
			if (options.MaxReductions == 0)
			{
				return CS$<>8__locals1.input;
			}
			int num = 0;
			int num2 = CS$<>8__locals1.input.Length - 1;
			int num3 = 0;
			bool flag = true;
			while (flag && (num3 < options.MaxReductions || options.MaxReductions < 0))
			{
				if (num > num2)
				{
					return string.Empty;
				}
				flag = false;
				while (char.IsWhiteSpace(CS$<>8__locals1.input[num]))
				{
					num++;
				}
				while (char.IsWhiteSpace(CS$<>8__locals1.input[num2]))
				{
					num2--;
				}
				if (TextProcessing.<ReduceScope>g__IsEscaped|16_0<T>(num2, ref CS$<>8__locals1))
				{
					break;
				}
				for (int i = 0; i < leftScopers.Count; i++)
				{
					TextProcessing.<>c__DisplayClass16_1<T> CS$<>8__locals2;
					CS$<>8__locals2.leftScoper = leftScopers[i];
					char c = rightScopers[i];
					bool flag2 = CS$<>8__locals2.leftScoper == c;
					bool flag3 = CS$<>8__locals1.input[num] == CS$<>8__locals2.leftScoper && CS$<>8__locals1.input[num2] == c;
					bool flag4 = false;
					if (!flag3 && options.ReduceIncompleteScope)
					{
						flag3 = (CS$<>8__locals1.input[num] == CS$<>8__locals2.leftScoper);
						flag4 = flag3;
					}
					if (flag3)
					{
						bool flag5 = false;
						int num4 = 1;
						int num5 = num + 1;
						int num6 = num2 - 1;
						if (num5 <= num6)
						{
							if (flag2)
							{
								while (TextProcessing.<ReduceScope>g__SkipSearch|16_1<T>(num5, ref CS$<>8__locals1, ref CS$<>8__locals2))
								{
									num5++;
								}
								while (TextProcessing.<ReduceScope>g__SkipSearch|16_1<T>(num6, ref CS$<>8__locals1, ref CS$<>8__locals2))
								{
									num6--;
								}
							}
							for (int j = num5; j <= num6; j++)
							{
								if (!TextProcessing.<ReduceScope>g__IsEscaped|16_0<T>(j, ref CS$<>8__locals1))
								{
									if (flag2)
									{
										if (CS$<>8__locals1.input[j] == CS$<>8__locals2.leftScoper)
										{
											flag5 = true;
											break;
										}
									}
									else
									{
										if (CS$<>8__locals1.input[j] == CS$<>8__locals2.leftScoper)
										{
											num4++;
										}
										else if (CS$<>8__locals1.input[j] == c)
										{
											num4--;
										}
										if (num4 == 0)
										{
											flag5 = true;
											break;
										}
									}
								}
							}
						}
						if (!flag5)
						{
							if (!flag4)
							{
								num2--;
							}
							num++;
							num3++;
							flag = true;
							break;
						}
					}
				}
			}
			if (num3 <= 0)
			{
				return CS$<>8__locals1.input;
			}
			return CS$<>8__locals1.input.Substring(num, num2 - num + 1);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008707 File Offset: 0x00006907
		public static string[] SplitScoped(this string input, char splitChar)
		{
			return input.SplitScoped(splitChar, TextProcessing.ScopedSplitOptions.Default);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008715 File Offset: 0x00006915
		public static string[] SplitScoped(this string input, char splitChar, TextProcessing.ScopedSplitOptions options)
		{
			return input.SplitScoped(splitChar, TextProcessing.DefaultLeftScopers, TextProcessing.DefaultRightScopers, options);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008729 File Offset: 0x00006929
		public static string[] SplitScoped(this string input, char splitChar, char leftScoper, char rightScoper)
		{
			return input.SplitScoped(splitChar, leftScoper.AsArraySingle<char>(), rightScoper.AsArraySingle<char>(), TextProcessing.ScopedSplitOptions.Default);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008743 File Offset: 0x00006943
		public static string[] SplitScoped(this string input, char splitChar, char leftScoper, char rightScoper, TextProcessing.ScopedSplitOptions options)
		{
			return input.SplitScoped(splitChar, leftScoper.AsArraySingle<char>(), rightScoper.AsArraySingle<char>(), options);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000875A File Offset: 0x0000695A
		public static string[] SplitScoped<T>(this string input, char splitChar, T leftScopers, T rightScopers) where T : IReadOnlyList<char>
		{
			return input.SplitScoped(splitChar, leftScopers, rightScopers, TextProcessing.ScopedSplitOptions.Default);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000876C File Offset: 0x0000696C
		public static string[] SplitScoped<T>(this string input, char splitChar, T leftScopers, T rightScopers, TextProcessing.ScopedSplitOptions options) where T : IReadOnlyList<char>
		{
			if (options.AutoReduceScope)
			{
				input = input.ReduceScope(leftScopers, rightScopers);
			}
			if (string.IsNullOrWhiteSpace(input))
			{
				return Array.Empty<string>();
			}
			IEnumerable<int> scopedSplitPoints = TextProcessing.GetScopedSplitPoints<T>(input, splitChar, leftScopers, rightScopers);
			int[] array = (options.MaxCount > 0) ? scopedSplitPoints.Take(options.MaxCount - 1).ToArray<int>() : scopedSplitPoints.ToArray<int>();
			if (array.Length == 0)
			{
				return new string[]
				{
					input
				};
			}
			string[] array2 = new string[array.Length + 1];
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = input.Substring(num, array[i] - num).Trim();
				num = array[i] + 1;
			}
			array2[array.Length] = input.Substring(num).Trim();
			return array2;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008828 File Offset: 0x00006A28
		public static IEnumerable<int> GetScopedSplitPoints<T>(string input, char splitChar, T leftScopers, T rightScopers) where T : IReadOnlyList<char>
		{
			return TextProcessing.GetScopedSplitPoints<T>(input, splitChar, leftScopers, rightScopers, TextProcessing.ScopedSplitOptions.Default);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008838 File Offset: 0x00006A38
		public static IEnumerable<int> GetScopedSplitPoints<T>(string input, char splitChar, T leftScopers, T rightScopers, TextProcessing.ScopedSplitOptions options) where T : IReadOnlyList<char>
		{
			if (leftScopers.Count != rightScopers.Count)
			{
				throw new ArgumentException("There must be an equal number of corresponding left and right scopers");
			}
			int[] scopes = new int[leftScopers.Count];
			int num;
			for (int i = 0; i < input.Length; i = num + 1)
			{
				if (i == 0 || input[i - 1] != '\\')
				{
					for (int j = 0; j < leftScopers.Count; j++)
					{
						char c = leftScopers[j];
						char c2 = rightScopers[j];
						if (input[i] == c && c == c2)
						{
							scopes[j] = 1 - scopes[j];
						}
						else if (input[i] == c)
						{
							scopes[j]++;
						}
						else if (input[i] == c2)
						{
							scopes[j]--;
						}
					}
				}
				if (input[i] == splitChar)
				{
					if (scopes.All((int x) => x == 0))
					{
						yield return i;
					}
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000885D File Offset: 0x00006A5D
		public static bool CanSplitScoped(this string input, char splitChar)
		{
			return input.CanSplitScoped(splitChar, TextProcessing.DefaultLeftScopers, TextProcessing.DefaultRightScopers);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008870 File Offset: 0x00006A70
		public static bool CanSplitScoped(this string input, char splitChar, char leftScoper, char rightScoper)
		{
			return input.CanSplitScoped(splitChar, leftScoper.AsArraySingle<char>(), rightScoper.AsArraySingle<char>());
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008885 File Offset: 0x00006A85
		public static bool CanSplitScoped<T>(this string input, char splitChar, T leftScopers, T rightScopers) where T : IReadOnlyList<char>
		{
			return TextProcessing.GetScopedSplitPoints<T>(input, splitChar, leftScopers, rightScopers).Any<int>();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008895 File Offset: 0x00006A95
		public static string SplitFirst(this string input, char splitChar)
		{
			return input.SplitScopedFirst(splitChar, Array.Empty<char>(), Array.Empty<char>());
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000088A8 File Offset: 0x00006AA8
		public static string SplitScopedFirst(this string input, char splitChar)
		{
			return input.SplitScopedFirst(splitChar, TextProcessing.DefaultLeftScopers, TextProcessing.DefaultRightScopers);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000088BB File Offset: 0x00006ABB
		public static string SplitScopedFirst(this string input, char splitChar, char leftScoper, char rightScoper)
		{
			return input.SplitScopedFirst(splitChar, leftScoper.AsArraySingle<char>(), rightScoper.AsArraySingle<char>());
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000088D0 File Offset: 0x00006AD0
		public static string SplitScopedFirst<T>(this string input, char splitChar, T leftScopers, T rightScopers) where T : IReadOnlyList<char>
		{
			using (IEnumerator<int> enumerator = TextProcessing.GetScopedSplitPoints<T>(input, splitChar, leftScopers, rightScopers).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					int length = enumerator.Current;
					return input.Substring(0, length).Trim();
				}
			}
			return input;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000892C File Offset: 0x00006B2C
		public static string UnescapeText(this string input, char escapeChar)
		{
			return input.UnescapeText(escapeChar.AsArraySingle<char>());
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000893C File Offset: 0x00006B3C
		public static string UnescapeText<T>(this string input, T escapeChars) where T : IReadOnlyCollection<char>
		{
			foreach (char c in escapeChars)
			{
				input = input.Replace(string.Format("\\{0}", c), c.ToString());
			}
			return input;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000089A4 File Offset: 0x00006BA4
		public static string ReverseItems(this string input, char splitChar)
		{
			int num = input.Length;
			StringBuilder stringBuilder = TextProcessing._stringBuilderPool.GetStringBuilder(input.Length);
			for (int i = input.Length - 1; i >= 0; i--)
			{
				if (input[i] == splitChar)
				{
					int num2 = i + 1;
					if (num2 < input.Length)
					{
						stringBuilder.Append(input, num2, num - num2);
					}
					stringBuilder.Append(splitChar);
					num = i;
				}
				else if (i == 0)
				{
					stringBuilder.Append(input, 0, num);
				}
			}
			return TextProcessing._stringBuilderPool.ReleaseAndToString(stringBuilder);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008A23 File Offset: 0x00006C23
		// Note: this type is marked as 'beforefieldinit'.
		static TextProcessing()
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008A5B File Offset: 0x00006C5B
		[CompilerGenerated]
		internal static bool <ReduceScope>g__IsEscaped|16_0<T>(int cursor, ref TextProcessing.<>c__DisplayClass16_0<T> A_1) where T : IReadOnlyList<char>
		{
			return cursor > 0 && A_1.input[cursor - 1] == '\\';
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008A75 File Offset: 0x00006C75
		[CompilerGenerated]
		internal static bool <ReduceScope>g__SkipSearch|16_1<T>(int cursor, ref TextProcessing.<>c__DisplayClass16_0<T> A_1, ref TextProcessing.<>c__DisplayClass16_1<T> A_2) where T : IReadOnlyList<char>
		{
			return !TextProcessing.<ReduceScope>g__IsEscaped|16_0<T>(cursor, ref A_1) && (A_1.input[cursor] == A_2.leftScoper || char.IsWhiteSpace(A_1.input[cursor]));
		}

		// Token: 0x04000130 RID: 304
		public static readonly char[] DefaultLeftScopers = new char[]
		{
			'<',
			'[',
			'(',
			'{',
			'"'
		};

		// Token: 0x04000131 RID: 305
		public static readonly char[] DefaultRightScopers = new char[]
		{
			'>',
			']',
			')',
			'}',
			'"'
		};

		// Token: 0x04000132 RID: 306
		private static readonly ConcurrentStringBuilderPool _stringBuilderPool = new ConcurrentStringBuilderPool();

		// Token: 0x0200009F RID: 159
		public struct ReduceScopeOptions
		{
			// Token: 0x06000315 RID: 789 RVA: 0x0000BD0C File Offset: 0x00009F0C
			// Note: this type is marked as 'beforefieldinit'.
			static ReduceScopeOptions()
			{
			}

			// Token: 0x040001EB RID: 491
			public int MaxReductions;

			// Token: 0x040001EC RID: 492
			public bool ReduceIncompleteScope;

			// Token: 0x040001ED RID: 493
			public static readonly TextProcessing.ReduceScopeOptions Default = new TextProcessing.ReduceScopeOptions
			{
				MaxReductions = -1,
				ReduceIncompleteScope = false
			};
		}

		// Token: 0x020000A0 RID: 160
		public struct ScopedSplitOptions
		{
			// Token: 0x06000316 RID: 790 RVA: 0x0000BD38 File Offset: 0x00009F38
			// Note: this type is marked as 'beforefieldinit'.
			static ScopedSplitOptions()
			{
			}

			// Token: 0x040001EE RID: 494
			public int MaxCount;

			// Token: 0x040001EF RID: 495
			public bool AutoReduceScope;

			// Token: 0x040001F0 RID: 496
			public static readonly TextProcessing.ScopedSplitOptions Default = new TextProcessing.ScopedSplitOptions
			{
				MaxCount = -1,
				AutoReduceScope = false
			};
		}

		// Token: 0x020000A1 RID: 161
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass16_0<T> where T : IReadOnlyList<char>
		{
			// Token: 0x040001F1 RID: 497
			public string input;
		}

		// Token: 0x020000A2 RID: 162
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass16_1<T> where T : IReadOnlyList<char>
		{
			// Token: 0x040001F2 RID: 498
			public char leftScoper;
		}

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__24<T> where T : IReadOnlyList<char>
		{
			// Token: 0x06000317 RID: 791 RVA: 0x0000BD63 File Offset: 0x00009F63
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__24()
			{
			}

			// Token: 0x06000318 RID: 792 RVA: 0x0000BD6F File Offset: 0x00009F6F
			public <>c__24()
			{
			}

			// Token: 0x06000319 RID: 793 RVA: 0x0000BD77 File Offset: 0x00009F77
			internal bool <GetScopedSplitPoints>b__24_0(int x)
			{
				return x == 0;
			}

			// Token: 0x040001F3 RID: 499
			public static readonly TextProcessing.<>c__24<T> <>9 = new TextProcessing.<>c__24<T>();

			// Token: 0x040001F4 RID: 500
			public static Func<int, bool> <>9__24_0;
		}

		// Token: 0x020000A4 RID: 164
		[CompilerGenerated]
		private sealed class <GetScopedSplitPoints>d__24<T> : IEnumerable<int>, IEnumerable, IEnumerator<int>, IEnumerator, IDisposable where T : IReadOnlyList<char>
		{
			// Token: 0x0600031A RID: 794 RVA: 0x0000BD7D File Offset: 0x00009F7D
			[DebuggerHidden]
			public <GetScopedSplitPoints>d__24(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600031B RID: 795 RVA: 0x0000BD97 File Offset: 0x00009F97
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600031C RID: 796 RVA: 0x0000BD9C File Offset: 0x00009F9C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
					if (leftScopers.Count != rightScopers.Count)
					{
						throw new ArgumentException("There must be an equal number of corresponding left and right scopers");
					}
					scopes = new int[leftScopers.Count];
					i = 0;
					goto IL_1C6;
				}
				IL_1B4:
				int num2 = i;
				i = num2 + 1;
				IL_1C6:
				if (i >= input.Length)
				{
					return false;
				}
				if (i == 0 || input[i - 1] != '\\')
				{
					for (int j = 0; j < leftScopers.Count; j++)
					{
						char c = leftScopers[j];
						char c2 = rightScopers[j];
						if (input[i] == c && c == c2)
						{
							scopes[j] = 1 - scopes[j];
						}
						else if (input[i] == c)
						{
							scopes[j]++;
						}
						else if (input[i] == c2)
						{
							scopes[j]--;
						}
					}
				}
				if (input[i] != splitChar)
				{
					goto IL_1B4;
				}
				if (scopes.All(new Func<int, bool>(TextProcessing.<>c__24<T>.<>9.<GetScopedSplitPoints>b__24_0)))
				{
					this.<>2__current = i;
					this.<>1__state = 1;
					return true;
				}
				goto IL_1B4;
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600031D RID: 797 RVA: 0x0000BF86 File Offset: 0x0000A186
			int IEnumerator<int>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600031E RID: 798 RVA: 0x0000BF8E File Offset: 0x0000A18E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x0600031F RID: 799 RVA: 0x0000BF95 File Offset: 0x0000A195
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000320 RID: 800 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
			[DebuggerHidden]
			IEnumerator<int> IEnumerable<int>.GetEnumerator()
			{
				TextProcessing.<GetScopedSplitPoints>d__24<T> <GetScopedSplitPoints>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetScopedSplitPoints>d__ = this;
				}
				else
				{
					<GetScopedSplitPoints>d__ = new TextProcessing.<GetScopedSplitPoints>d__24<T>(0);
				}
				<GetScopedSplitPoints>d__.input = input;
				<GetScopedSplitPoints>d__.splitChar = splitChar;
				<GetScopedSplitPoints>d__.leftScopers = leftScopers;
				<GetScopedSplitPoints>d__.rightScopers = rightScopers;
				return <GetScopedSplitPoints>d__;
			}

			// Token: 0x06000321 RID: 801 RVA: 0x0000C00B File Offset: 0x0000A20B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator();
			}

			// Token: 0x040001F5 RID: 501
			private int <>1__state;

			// Token: 0x040001F6 RID: 502
			private int <>2__current;

			// Token: 0x040001F7 RID: 503
			private int <>l__initialThreadId;

			// Token: 0x040001F8 RID: 504
			private T leftScopers;

			// Token: 0x040001F9 RID: 505
			public T <>3__leftScopers;

			// Token: 0x040001FA RID: 506
			private T rightScopers;

			// Token: 0x040001FB RID: 507
			public T <>3__rightScopers;

			// Token: 0x040001FC RID: 508
			private string input;

			// Token: 0x040001FD RID: 509
			public string <>3__input;

			// Token: 0x040001FE RID: 510
			private char splitChar;

			// Token: 0x040001FF RID: 511
			public char <>3__splitChar;

			// Token: 0x04000200 RID: 512
			private int[] <scopes>5__2;

			// Token: 0x04000201 RID: 513
			private int <i>5__3;
		}
	}
}
