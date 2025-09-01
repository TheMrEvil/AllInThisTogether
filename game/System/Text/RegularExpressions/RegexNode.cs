using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000207 RID: 519
	internal sealed class RegexNode
	{
		// Token: 0x06000EA4 RID: 3748 RVA: 0x000400F8 File Offset: 0x0003E2F8
		public RegexNode(int type, RegexOptions options)
		{
			this.NType = type;
			this.Options = options;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0004010E File Offset: 0x0003E30E
		public RegexNode(int type, RegexOptions options, char ch)
		{
			this.NType = type;
			this.Options = options;
			this.Ch = ch;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0004012B File Offset: 0x0003E32B
		public RegexNode(int type, RegexOptions options, string str)
		{
			this.NType = type;
			this.Options = options;
			this.Str = str;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00040148 File Offset: 0x0003E348
		public RegexNode(int type, RegexOptions options, int m)
		{
			this.NType = type;
			this.Options = options;
			this.M = m;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00040165 File Offset: 0x0003E365
		public RegexNode(int type, RegexOptions options, int m, int n)
		{
			this.NType = type;
			this.Options = options;
			this.M = m;
			this.N = n;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0004018A File Offset: 0x0003E38A
		public bool UseOptionR()
		{
			return (this.Options & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00040198 File Offset: 0x0003E398
		public RegexNode ReverseLeft()
		{
			if (this.UseOptionR() && this.NType == 25 && this.Children != null)
			{
				this.Children.Reverse(0, this.Children.Count);
			}
			return this;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000401CC File Offset: 0x0003E3CC
		private void MakeRep(int type, int min, int max)
		{
			this.NType += type - 9;
			this.M = min;
			this.N = max;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000401F0 File Offset: 0x0003E3F0
		private RegexNode Reduce()
		{
			int num = this.Type();
			RegexNode result;
			if (num != 5 && num != 11)
			{
				switch (num)
				{
				case 24:
					return this.ReduceAlternation();
				case 25:
					return this.ReduceConcatenation();
				case 26:
				case 27:
					return this.ReduceRep();
				case 29:
					return this.ReduceGroup();
				}
				result = this;
			}
			else
			{
				result = this.ReduceSet();
			}
			return result;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00040260 File Offset: 0x0003E460
		private RegexNode StripEnation(int emptyType)
		{
			int num = this.ChildCount();
			if (num == 0)
			{
				return new RegexNode(emptyType, this.Options);
			}
			if (num != 1)
			{
				return this;
			}
			return this.Child(0);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00040294 File Offset: 0x0003E494
		private RegexNode ReduceGroup()
		{
			RegexNode regexNode = this;
			while (regexNode.Type() == 29)
			{
				regexNode = regexNode.Child(0);
			}
			return regexNode;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000402B8 File Offset: 0x0003E4B8
		private RegexNode ReduceRep()
		{
			RegexNode regexNode = this;
			int num = this.Type();
			int num2 = this.M;
			int num3 = this.N;
			while (regexNode.ChildCount() != 0)
			{
				RegexNode regexNode2 = regexNode.Child(0);
				if (regexNode2.Type() != num)
				{
					int num4 = regexNode2.Type();
					if ((num4 < 3 || num4 > 5 || num != 26) && (num4 < 6 || num4 > 8 || num != 27))
					{
						break;
					}
				}
				if ((regexNode.M == 0 && regexNode2.M > 1) || regexNode2.N < regexNode2.M * 2)
				{
					break;
				}
				regexNode = regexNode2;
				if (regexNode.M > 0)
				{
					num2 = (regexNode.M = ((2147483646 / regexNode.M < num2) ? int.MaxValue : (regexNode.M * num2)));
				}
				if (regexNode.N > 0)
				{
					num3 = (regexNode.N = ((2147483646 / regexNode.N < num3) ? int.MaxValue : (regexNode.N * num3)));
				}
			}
			if (num2 != 2147483647)
			{
				return regexNode;
			}
			return new RegexNode(22, this.Options);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000403CC File Offset: 0x0003E5CC
		private RegexNode ReduceSet()
		{
			if (RegexCharClass.IsEmpty(this.Str))
			{
				this.NType = 22;
				this.Str = null;
			}
			else if (RegexCharClass.IsSingleton(this.Str))
			{
				this.Ch = RegexCharClass.SingletonChar(this.Str);
				this.Str = null;
				this.NType += -2;
			}
			else if (RegexCharClass.IsSingletonInverse(this.Str))
			{
				this.Ch = RegexCharClass.SingletonChar(this.Str);
				this.Str = null;
				this.NType += -1;
			}
			return this;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00040464 File Offset: 0x0003E664
		private RegexNode ReduceAlternation()
		{
			if (this.Children == null)
			{
				return new RegexNode(22, this.Options);
			}
			bool flag = false;
			bool flag2 = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this.Children.Count)
			{
				RegexNode regexNode = this.Children[i];
				if (num < i)
				{
					this.Children[num] = regexNode;
				}
				if (regexNode.NType == 24)
				{
					for (int j = 0; j < regexNode.Children.Count; j++)
					{
						regexNode.Children[j].Next = this;
					}
					this.Children.InsertRange(i + 1, regexNode.Children);
					num--;
				}
				else if (regexNode.NType == 11 || regexNode.NType == 9)
				{
					RegexOptions regexOptions2 = regexNode.Options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (regexNode.NType == 11)
					{
						if (!flag || regexOptions != regexOptions2 || flag2 || !RegexCharClass.IsMergeable(regexNode.Str))
						{
							flag = true;
							flag2 = !RegexCharClass.IsMergeable(regexNode.Str);
							regexOptions = regexOptions2;
							goto IL_1D0;
						}
					}
					else if (!flag || regexOptions != regexOptions2 || flag2)
					{
						flag = true;
						flag2 = false;
						regexOptions = regexOptions2;
						goto IL_1D0;
					}
					num--;
					RegexNode regexNode2 = this.Children[num];
					RegexCharClass regexCharClass;
					if (regexNode2.NType == 9)
					{
						regexCharClass = new RegexCharClass();
						regexCharClass.AddChar(regexNode2.Ch);
					}
					else
					{
						regexCharClass = RegexCharClass.Parse(regexNode2.Str);
					}
					if (regexNode.NType == 9)
					{
						regexCharClass.AddChar(regexNode.Ch);
					}
					else
					{
						RegexCharClass cc = RegexCharClass.Parse(regexNode.Str);
						regexCharClass.AddCharClass(cc);
					}
					regexNode2.NType = 11;
					regexNode2.Str = regexCharClass.ToStringClass();
				}
				else if (regexNode.NType == 22)
				{
					num--;
				}
				else
				{
					flag = false;
					flag2 = false;
				}
				IL_1D0:
				i++;
				num++;
			}
			if (num < i)
			{
				this.Children.RemoveRange(num, i - num);
			}
			return this.StripEnation(22);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00040680 File Offset: 0x0003E880
		private RegexNode ReduceConcatenation()
		{
			if (this.Children == null)
			{
				return new RegexNode(23, this.Options);
			}
			bool flag = false;
			RegexOptions regexOptions = RegexOptions.None;
			int i = 0;
			int num = 0;
			while (i < this.Children.Count)
			{
				RegexNode regexNode = this.Children[i];
				if (num < i)
				{
					this.Children[num] = regexNode;
				}
				if (regexNode.NType == 25 && (regexNode.Options & RegexOptions.RightToLeft) == (this.Options & RegexOptions.RightToLeft))
				{
					for (int j = 0; j < regexNode.Children.Count; j++)
					{
						regexNode.Children[j].Next = this;
					}
					this.Children.InsertRange(i + 1, regexNode.Children);
					num--;
				}
				else if (regexNode.NType == 12 || regexNode.NType == 9)
				{
					RegexOptions regexOptions2 = regexNode.Options & (RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
					if (!flag || regexOptions != regexOptions2)
					{
						flag = true;
						regexOptions = regexOptions2;
					}
					else
					{
						RegexNode regexNode2 = this.Children[--num];
						if (regexNode2.NType == 9)
						{
							regexNode2.NType = 12;
							regexNode2.Str = Convert.ToString(regexNode2.Ch, CultureInfo.InvariantCulture);
						}
						if ((regexOptions2 & RegexOptions.RightToLeft) == RegexOptions.None)
						{
							if (regexNode.NType == 9)
							{
								RegexNode regexNode3 = regexNode2;
								regexNode3.Str += regexNode.Ch.ToString();
							}
							else
							{
								RegexNode regexNode4 = regexNode2;
								regexNode4.Str += regexNode.Str;
							}
						}
						else if (regexNode.NType == 9)
						{
							regexNode2.Str = regexNode.Ch.ToString() + regexNode2.Str;
						}
						else
						{
							regexNode2.Str = regexNode.Str + regexNode2.Str;
						}
					}
				}
				else if (regexNode.NType == 23)
				{
					num--;
				}
				else
				{
					flag = false;
				}
				i++;
				num++;
			}
			if (num < i)
			{
				this.Children.RemoveRange(num, i - num);
			}
			return this.StripEnation(23);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00040898 File Offset: 0x0003EA98
		public RegexNode MakeQuantifier(bool lazy, int min, int max)
		{
			if (min == 0 && max == 0)
			{
				return new RegexNode(23, this.Options);
			}
			if (min == 1 && max == 1)
			{
				return this;
			}
			int ntype = this.NType;
			if (ntype - 9 <= 2)
			{
				this.MakeRep(lazy ? 6 : 3, min, max);
				return this;
			}
			RegexNode regexNode = new RegexNode(lazy ? 27 : 26, this.Options, min, max);
			regexNode.AddChild(this);
			return regexNode;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00040900 File Offset: 0x0003EB00
		public void AddChild(RegexNode newChild)
		{
			if (this.Children == null)
			{
				this.Children = new List<RegexNode>(4);
			}
			RegexNode regexNode = newChild.Reduce();
			this.Children.Add(regexNode);
			regexNode.Next = this;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0004093B File Offset: 0x0003EB3B
		public RegexNode Child(int i)
		{
			return this.Children[i];
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00040949 File Offset: 0x0003EB49
		public int ChildCount()
		{
			if (this.Children != null)
			{
				return this.Children.Count;
			}
			return 0;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00040960 File Offset: 0x0003EB60
		public int Type()
		{
			return this.NType;
		}

		// Token: 0x0400090D RID: 2317
		public const int Oneloop = 3;

		// Token: 0x0400090E RID: 2318
		public const int Notoneloop = 4;

		// Token: 0x0400090F RID: 2319
		public const int Setloop = 5;

		// Token: 0x04000910 RID: 2320
		public const int Onelazy = 6;

		// Token: 0x04000911 RID: 2321
		public const int Notonelazy = 7;

		// Token: 0x04000912 RID: 2322
		public const int Setlazy = 8;

		// Token: 0x04000913 RID: 2323
		public const int One = 9;

		// Token: 0x04000914 RID: 2324
		public const int Notone = 10;

		// Token: 0x04000915 RID: 2325
		public const int Set = 11;

		// Token: 0x04000916 RID: 2326
		public const int Multi = 12;

		// Token: 0x04000917 RID: 2327
		public const int Ref = 13;

		// Token: 0x04000918 RID: 2328
		public const int Bol = 14;

		// Token: 0x04000919 RID: 2329
		public const int Eol = 15;

		// Token: 0x0400091A RID: 2330
		public const int Boundary = 16;

		// Token: 0x0400091B RID: 2331
		public const int Nonboundary = 17;

		// Token: 0x0400091C RID: 2332
		public const int ECMABoundary = 41;

		// Token: 0x0400091D RID: 2333
		public const int NonECMABoundary = 42;

		// Token: 0x0400091E RID: 2334
		public const int Beginning = 18;

		// Token: 0x0400091F RID: 2335
		public const int Start = 19;

		// Token: 0x04000920 RID: 2336
		public const int EndZ = 20;

		// Token: 0x04000921 RID: 2337
		public const int End = 21;

		// Token: 0x04000922 RID: 2338
		public const int Nothing = 22;

		// Token: 0x04000923 RID: 2339
		public const int Empty = 23;

		// Token: 0x04000924 RID: 2340
		public const int Alternate = 24;

		// Token: 0x04000925 RID: 2341
		public const int Concatenate = 25;

		// Token: 0x04000926 RID: 2342
		public const int Loop = 26;

		// Token: 0x04000927 RID: 2343
		public const int Lazyloop = 27;

		// Token: 0x04000928 RID: 2344
		public const int Capture = 28;

		// Token: 0x04000929 RID: 2345
		public const int Group = 29;

		// Token: 0x0400092A RID: 2346
		public const int Require = 30;

		// Token: 0x0400092B RID: 2347
		public const int Prevent = 31;

		// Token: 0x0400092C RID: 2348
		public const int Greedy = 32;

		// Token: 0x0400092D RID: 2349
		public const int Testref = 33;

		// Token: 0x0400092E RID: 2350
		public const int Testgroup = 34;

		// Token: 0x0400092F RID: 2351
		public int NType;

		// Token: 0x04000930 RID: 2352
		public List<RegexNode> Children;

		// Token: 0x04000931 RID: 2353
		public string Str;

		// Token: 0x04000932 RID: 2354
		public char Ch;

		// Token: 0x04000933 RID: 2355
		public int M;

		// Token: 0x04000934 RID: 2356
		public int N;

		// Token: 0x04000935 RID: 2357
		public readonly RegexOptions Options;

		// Token: 0x04000936 RID: 2358
		public RegexNode Next;
	}
}
