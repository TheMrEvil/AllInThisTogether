using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200020B RID: 523
	internal sealed class RegexReplacement
	{
		// Token: 0x06000F0B RID: 3851 RVA: 0x00043370 File Offset: 0x00041570
		public RegexReplacement(string rep, RegexNode concat, Hashtable _caps)
		{
			if (concat.Type() != 25)
			{
				throw new ArgumentException("Replacement pattern error.");
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			List<string> list = new List<string>();
			List<int> list2 = new List<int>();
			int i = 0;
			while (i < concat.ChildCount())
			{
				RegexNode regexNode = concat.Child(i);
				switch (regexNode.Type())
				{
				case 9:
					stringBuilder.Append(regexNode.Ch);
					break;
				case 10:
				case 11:
					goto IL_E2;
				case 12:
					stringBuilder.Append(regexNode.Str);
					break;
				case 13:
				{
					if (stringBuilder.Length > 0)
					{
						list2.Add(list.Count);
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					int num = regexNode.M;
					if (_caps != null && num >= 0)
					{
						num = (int)_caps[num];
					}
					list2.Add(-5 - num);
					break;
				}
				default:
					goto IL_E2;
				}
				i++;
				continue;
				IL_E2:
				throw new ArgumentException("Replacement pattern error.");
			}
			if (stringBuilder.Length > 0)
			{
				list2.Add(list.Count);
				list.Add(stringBuilder.ToString());
			}
			StringBuilderCache.Release(stringBuilder);
			this.Pattern = rep;
			this._strings = list;
			this._rules = list2;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000434B8 File Offset: 0x000416B8
		public static RegexReplacement GetOrCreate(WeakReference<RegexReplacement> replRef, string replacement, Hashtable caps, int capsize, Hashtable capnames, RegexOptions roptions)
		{
			RegexReplacement regexReplacement;
			if (!replRef.TryGetTarget(out regexReplacement) || !regexReplacement.Pattern.Equals(replacement))
			{
				regexReplacement = RegexParser.ParseReplacement(replacement, caps, capsize, capnames, roptions);
				replRef.SetTarget(regexReplacement);
			}
			return regexReplacement;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000434F2 File Offset: 0x000416F2
		public string Pattern
		{
			[CompilerGenerated]
			get
			{
				return this.<Pattern>k__BackingField;
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x000434FC File Offset: 0x000416FC
		private void ReplacementImpl(StringBuilder sb, Match match)
		{
			for (int i = 0; i < this._rules.Count; i++)
			{
				int num = this._rules[i];
				if (num >= 0)
				{
					sb.Append(this._strings[num]);
				}
				else if (num < -4)
				{
					sb.Append(match.GroupToStringImpl(-5 - num));
				}
				else
				{
					switch (-5 - num)
					{
					case -4:
						sb.Append(match.Text);
						break;
					case -3:
						sb.Append(match.LastGroupToStringImpl());
						break;
					case -2:
						sb.Append(match.GetRightSubstring());
						break;
					case -1:
						sb.Append(match.GetLeftSubstring());
						break;
					}
				}
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000435C0 File Offset: 0x000417C0
		private void ReplacementImplRTL(List<string> al, Match match)
		{
			for (int i = this._rules.Count - 1; i >= 0; i--)
			{
				int num = this._rules[i];
				if (num >= 0)
				{
					al.Add(this._strings[num]);
				}
				else if (num < -4)
				{
					al.Add(match.GroupToStringImpl(-5 - num).ToString());
				}
				else
				{
					switch (-5 - num)
					{
					case -4:
						al.Add(match.Text);
						break;
					case -3:
						al.Add(match.LastGroupToStringImpl().ToString());
						break;
					case -2:
						al.Add(match.GetRightSubstring().ToString());
						break;
					case -1:
						al.Add(match.GetLeftSubstring().ToString());
						break;
					}
				}
			}
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000436BC File Offset: 0x000418BC
		public string Replacement(Match match)
		{
			StringBuilder sb = StringBuilderCache.Acquire(16);
			this.ReplacementImpl(sb, match);
			return StringBuilderCache.GetStringAndRelease(sb);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000436E0 File Offset: 0x000418E0
		public string Replace(Regex regex, string input, int count, int startat)
		{
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than -1.");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			if (count == 0)
			{
				return input;
			}
			Match match = regex.Match(input, startat);
			if (!match.Success)
			{
				return input;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			if (!regex.RightToLeft)
			{
				int num = 0;
				do
				{
					if (match.Index != num)
					{
						stringBuilder.Append(input, num, match.Index - num);
					}
					num = match.Index + match.Length;
					this.ReplacementImpl(stringBuilder, match);
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num < input.Length)
				{
					stringBuilder.Append(input, num, input.Length - num);
				}
			}
			else
			{
				List<string> list = new List<string>();
				int num2 = input.Length;
				do
				{
					if (match.Index + match.Length != num2)
					{
						list.Add(input.Substring(match.Index + match.Length, num2 - match.Index - match.Length));
					}
					num2 = match.Index;
					this.ReplacementImplRTL(list, match);
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num2 > 0)
				{
					stringBuilder.Append(input, 0, num2);
				}
				for (int i = list.Count - 1; i >= 0; i--)
				{
					stringBuilder.Append(list[i]);
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x04000960 RID: 2400
		private const int Specials = 4;

		// Token: 0x04000961 RID: 2401
		public const int LeftPortion = -1;

		// Token: 0x04000962 RID: 2402
		public const int RightPortion = -2;

		// Token: 0x04000963 RID: 2403
		public const int LastGroup = -3;

		// Token: 0x04000964 RID: 2404
		public const int WholeString = -4;

		// Token: 0x04000965 RID: 2405
		private readonly List<string> _strings;

		// Token: 0x04000966 RID: 2406
		private readonly List<int> _rules;

		// Token: 0x04000967 RID: 2407
		[CompilerGenerated]
		private readonly string <Pattern>k__BackingField;
	}
}
