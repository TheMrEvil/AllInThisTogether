using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000204 RID: 516
	internal sealed class RegexInterpreter : RegexRunner
	{
		// Token: 0x06000E6F RID: 3695 RVA: 0x0003E651 File Offset: 0x0003C851
		public RegexInterpreter(RegexCode code, CultureInfo culture)
		{
			this._code = code;
			this._culture = culture;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0003E667 File Offset: 0x0003C867
		protected override void InitTrackCount()
		{
			this.runtrackcount = this._code.TrackCount;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003E67A File Offset: 0x0003C87A
		private void Advance(int i)
		{
			this._codepos += i + 1;
			this.SetOperator(this._code.Codes[this._codepos]);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003E6A4 File Offset: 0x0003C8A4
		private void Goto(int newpos)
		{
			if (newpos < this._codepos)
			{
				base.EnsureStorage();
			}
			this.SetOperator(this._code.Codes[newpos]);
			this._codepos = newpos;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003E6CF File Offset: 0x0003C8CF
		private void Textto(int newpos)
		{
			this.runtextpos = newpos;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003E6D8 File Offset: 0x0003C8D8
		private void Trackto(int newpos)
		{
			this.runtrackpos = this.runtrack.Length - newpos;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003E6EA File Offset: 0x0003C8EA
		private int Textstart()
		{
			return this.runtextstart;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003E6F2 File Offset: 0x0003C8F2
		private int Textpos()
		{
			return this.runtextpos;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003E6FA File Offset: 0x0003C8FA
		private int Trackpos()
		{
			return this.runtrack.Length - this.runtrackpos;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003E70C File Offset: 0x0003C90C
		private void TrackPush()
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = this._codepos;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003E738 File Offset: 0x0003C938
		private void TrackPush(int I1)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = this._codepos;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003E77C File Offset: 0x0003C97C
		private void TrackPush(int I1, int I2)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = I2;
			int[] runtrack3 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack3[num] = this._codepos;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003E7DC File Offset: 0x0003C9DC
		private void TrackPush(int I1, int I2, int I3)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = I2;
			int[] runtrack3 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack3[num] = I3;
			int[] runtrack4 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack4[num] = this._codepos;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003E854 File Offset: 0x0003CA54
		private void TrackPush2(int I1)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = -this._codepos;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003E89C File Offset: 0x0003CA9C
		private void TrackPush2(int I1, int I2)
		{
			int[] runtrack = this.runtrack;
			int num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack[num] = I1;
			int[] runtrack2 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack2[num] = I2;
			int[] runtrack3 = this.runtrack;
			num = this.runtrackpos - 1;
			this.runtrackpos = num;
			runtrack3[num] = -this._codepos;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003E8FC File Offset: 0x0003CAFC
		private void Backtrack()
		{
			int[] runtrack = this.runtrack;
			int runtrackpos = this.runtrackpos;
			this.runtrackpos = runtrackpos + 1;
			int num = runtrack[runtrackpos];
			if (num < 0)
			{
				num = -num;
				this.SetOperator(this._code.Codes[num] | 256);
			}
			else
			{
				this.SetOperator(this._code.Codes[num] | 128);
			}
			if (num < this._codepos)
			{
				base.EnsureStorage();
			}
			this._codepos = num;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003E973 File Offset: 0x0003CB73
		private void SetOperator(int op)
		{
			this._caseInsensitive = ((op & 512) != 0);
			this._rightToLeft = ((op & 64) != 0);
			this._operator = (op & -577);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003E99F File Offset: 0x0003CB9F
		private void TrackPop()
		{
			this.runtrackpos++;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003E9AF File Offset: 0x0003CBAF
		private void TrackPop(int framesize)
		{
			this.runtrackpos += framesize;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003E9BF File Offset: 0x0003CBBF
		private int TrackPeek()
		{
			return this.runtrack[this.runtrackpos - 1];
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003E9D0 File Offset: 0x0003CBD0
		private int TrackPeek(int i)
		{
			return this.runtrack[this.runtrackpos - i - 1];
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003E9E4 File Offset: 0x0003CBE4
		private void StackPush(int I1)
		{
			int[] runstack = this.runstack;
			int num = this.runstackpos - 1;
			this.runstackpos = num;
			runstack[num] = I1;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003EA0C File Offset: 0x0003CC0C
		private void StackPush(int I1, int I2)
		{
			int[] runstack = this.runstack;
			int num = this.runstackpos - 1;
			this.runstackpos = num;
			runstack[num] = I1;
			int[] runstack2 = this.runstack;
			num = this.runstackpos - 1;
			this.runstackpos = num;
			runstack2[num] = I2;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003EA4B File Offset: 0x0003CC4B
		private void StackPop()
		{
			this.runstackpos++;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003EA5B File Offset: 0x0003CC5B
		private void StackPop(int framesize)
		{
			this.runstackpos += framesize;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003EA6B File Offset: 0x0003CC6B
		private int StackPeek()
		{
			return this.runstack[this.runstackpos - 1];
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003EA7C File Offset: 0x0003CC7C
		private int StackPeek(int i)
		{
			return this.runstack[this.runstackpos - i - 1];
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003EA8F File Offset: 0x0003CC8F
		private int Operator()
		{
			return this._operator;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003EA97 File Offset: 0x0003CC97
		private int Operand(int i)
		{
			return this._code.Codes[this._codepos + i + 1];
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003EAAF File Offset: 0x0003CCAF
		private int Leftchars()
		{
			return this.runtextpos - this.runtextbeg;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003EABE File Offset: 0x0003CCBE
		private int Rightchars()
		{
			return this.runtextend - this.runtextpos;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003EACD File Offset: 0x0003CCCD
		private int Bump()
		{
			if (!this._rightToLeft)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003EADA File Offset: 0x0003CCDA
		private int Forwardchars()
		{
			if (!this._rightToLeft)
			{
				return this.runtextend - this.runtextpos;
			}
			return this.runtextpos - this.runtextbeg;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003EB00 File Offset: 0x0003CD00
		private char Forwardcharnext()
		{
			char c;
			if (!this._rightToLeft)
			{
				string runtext = this.runtext;
				int num = this.runtextpos;
				this.runtextpos = num + 1;
				c = runtext[num];
			}
			else
			{
				string runtext2 = this.runtext;
				int num = this.runtextpos - 1;
				this.runtextpos = num;
				c = runtext2[num];
			}
			char c2 = c;
			if (!this._caseInsensitive)
			{
				return c2;
			}
			return this._culture.TextInfo.ToLower(c2);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003EB6C File Offset: 0x0003CD6C
		private bool Stringmatch(string str)
		{
			int num;
			int num2;
			if (!this._rightToLeft)
			{
				if (this.runtextend - this.runtextpos < (num = str.Length))
				{
					return false;
				}
				num2 = this.runtextpos + num;
			}
			else
			{
				if (this.runtextpos - this.runtextbeg < (num = str.Length))
				{
					return false;
				}
				num2 = this.runtextpos;
			}
			if (!this._caseInsensitive)
			{
				while (num != 0)
				{
					if (str[--num] != this.runtext[--num2])
					{
						return false;
					}
				}
			}
			else
			{
				while (num != 0)
				{
					if (str[--num] != this._culture.TextInfo.ToLower(this.runtext[--num2]))
					{
						return false;
					}
				}
			}
			if (!this._rightToLeft)
			{
				num2 += str.Length;
			}
			this.runtextpos = num2;
			return true;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003EC40 File Offset: 0x0003CE40
		private bool Refmatch(int index, int len)
		{
			int num;
			if (!this._rightToLeft)
			{
				if (this.runtextend - this.runtextpos < len)
				{
					return false;
				}
				num = this.runtextpos + len;
			}
			else
			{
				if (this.runtextpos - this.runtextbeg < len)
				{
					return false;
				}
				num = this.runtextpos;
			}
			int num2 = index + len;
			int num3 = len;
			if (!this._caseInsensitive)
			{
				while (num3-- != 0)
				{
					if (this.runtext[--num2] != this.runtext[--num])
					{
						return false;
					}
				}
			}
			else
			{
				while (num3-- != 0)
				{
					if (this._culture.TextInfo.ToLower(this.runtext[--num2]) != this._culture.TextInfo.ToLower(this.runtext[--num]))
					{
						return false;
					}
				}
			}
			if (!this._rightToLeft)
			{
				num += len;
			}
			this.runtextpos = num;
			return true;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003ED27 File Offset: 0x0003CF27
		private void Backwardnext()
		{
			this.runtextpos += (this._rightToLeft ? 1 : -1);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003ED42 File Offset: 0x0003CF42
		private char CharAt(int j)
		{
			return this.runtext[j];
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003ED50 File Offset: 0x0003CF50
		protected override bool FindFirstChar()
		{
			if ((this._code.Anchors & 53) != 0)
			{
				if (!this._code.RightToLeft)
				{
					if (((this._code.Anchors & 1) != 0 && this.runtextpos > this.runtextbeg) || ((this._code.Anchors & 4) != 0 && this.runtextpos > this.runtextstart))
					{
						this.runtextpos = this.runtextend;
						return false;
					}
					if ((this._code.Anchors & 16) != 0 && this.runtextpos < this.runtextend - 1)
					{
						this.runtextpos = this.runtextend - 1;
					}
					else if ((this._code.Anchors & 32) != 0 && this.runtextpos < this.runtextend)
					{
						this.runtextpos = this.runtextend;
					}
				}
				else
				{
					if (((this._code.Anchors & 32) != 0 && this.runtextpos < this.runtextend) || ((this._code.Anchors & 16) != 0 && (this.runtextpos < this.runtextend - 1 || (this.runtextpos == this.runtextend - 1 && this.CharAt(this.runtextpos) != '\n'))) || ((this._code.Anchors & 4) != 0 && this.runtextpos < this.runtextstart))
					{
						this.runtextpos = this.runtextbeg;
						return false;
					}
					if ((this._code.Anchors & 1) != 0 && this.runtextpos > this.runtextbeg)
					{
						this.runtextpos = this.runtextbeg;
					}
				}
				return this._code.BMPrefix == null || this._code.BMPrefix.IsMatch(this.runtext, this.runtextpos, this.runtextbeg, this.runtextend);
			}
			if (this._code.BMPrefix != null)
			{
				this.runtextpos = this._code.BMPrefix.Scan(this.runtext, this.runtextpos, this.runtextbeg, this.runtextend);
				if (this.runtextpos == -1)
				{
					this.runtextpos = (this._code.RightToLeft ? this.runtextbeg : this.runtextend);
					return false;
				}
				return true;
			}
			else
			{
				if (this._code.FCPrefix == null)
				{
					return true;
				}
				this._rightToLeft = this._code.RightToLeft;
				this._caseInsensitive = this._code.FCPrefix.GetValueOrDefault().CaseInsensitive;
				string prefix = this._code.FCPrefix.GetValueOrDefault().Prefix;
				if (RegexCharClass.IsSingleton(prefix))
				{
					char c = RegexCharClass.SingletonChar(prefix);
					for (int i = this.Forwardchars(); i > 0; i--)
					{
						if (c == this.Forwardcharnext())
						{
							this.Backwardnext();
							return true;
						}
					}
				}
				else
				{
					for (int j = this.Forwardchars(); j > 0; j--)
					{
						if (RegexCharClass.CharInClass(this.Forwardcharnext(), prefix))
						{
							this.Backwardnext();
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003F040 File Offset: 0x0003D240
		protected override void Go()
		{
			this.Goto(0);
			int num = -1;
			for (;;)
			{
				if (num >= 0)
				{
					this.Advance(num);
					num = -1;
				}
				base.CheckTimeout();
				int num2 = this.Operator();
				switch (num2)
				{
				case 0:
				{
					int num3 = this.Operand(1);
					if (this.Forwardchars() >= num3)
					{
						char c = (char)this.Operand(0);
						while (num3-- > 0)
						{
							if (this.Forwardcharnext() != c)
							{
								goto IL_DA8;
							}
						}
						num = 2;
						continue;
					}
					break;
				}
				case 1:
				{
					int num4 = this.Operand(1);
					if (this.Forwardchars() >= num4)
					{
						char c2 = (char)this.Operand(0);
						while (num4-- > 0)
						{
							if (this.Forwardcharnext() == c2)
							{
								goto IL_DA8;
							}
						}
						num = 2;
						continue;
					}
					break;
				}
				case 2:
				{
					int num5 = this.Operand(1);
					if (this.Forwardchars() >= num5)
					{
						string set = this._code.Strings[this.Operand(0)];
						while (num5-- > 0)
						{
							if (!RegexCharClass.CharInClass(this.Forwardcharnext(), set))
							{
								goto IL_DA8;
							}
						}
						num = 2;
						continue;
					}
					break;
				}
				case 3:
				{
					int num6 = this.Operand(1);
					if (num6 > this.Forwardchars())
					{
						num6 = this.Forwardchars();
					}
					char c3 = (char)this.Operand(0);
					int i;
					for (i = num6; i > 0; i--)
					{
						if (this.Forwardcharnext() != c3)
						{
							this.Backwardnext();
							break;
						}
					}
					if (num6 > i)
					{
						this.TrackPush(num6 - i - 1, this.Textpos() - this.Bump());
					}
					num = 2;
					continue;
				}
				case 4:
				{
					int num7 = this.Operand(1);
					if (num7 > this.Forwardchars())
					{
						num7 = this.Forwardchars();
					}
					char c4 = (char)this.Operand(0);
					int j;
					for (j = num7; j > 0; j--)
					{
						if (this.Forwardcharnext() == c4)
						{
							this.Backwardnext();
							break;
						}
					}
					if (num7 > j)
					{
						this.TrackPush(num7 - j - 1, this.Textpos() - this.Bump());
					}
					num = 2;
					continue;
				}
				case 5:
				{
					int num8 = this.Operand(1);
					if (num8 > this.Forwardchars())
					{
						num8 = this.Forwardchars();
					}
					string set2 = this._code.Strings[this.Operand(0)];
					int k;
					for (k = num8; k > 0; k--)
					{
						if (!RegexCharClass.CharInClass(this.Forwardcharnext(), set2))
						{
							this.Backwardnext();
							break;
						}
					}
					if (num8 > k)
					{
						this.TrackPush(num8 - k - 1, this.Textpos() - this.Bump());
					}
					num = 2;
					continue;
				}
				case 6:
				case 7:
				{
					int num9 = this.Operand(1);
					if (num9 > this.Forwardchars())
					{
						num9 = this.Forwardchars();
					}
					if (num9 > 0)
					{
						this.TrackPush(num9 - 1, this.Textpos());
					}
					num = 2;
					continue;
				}
				case 8:
				{
					int num10 = this.Operand(1);
					if (num10 > this.Forwardchars())
					{
						num10 = this.Forwardchars();
					}
					if (num10 > 0)
					{
						this.TrackPush(num10 - 1, this.Textpos());
					}
					num = 2;
					continue;
				}
				case 9:
					if (this.Forwardchars() >= 1 && this.Forwardcharnext() == (char)this.Operand(0))
					{
						num = 1;
						continue;
					}
					break;
				case 10:
					if (this.Forwardchars() >= 1 && this.Forwardcharnext() != (char)this.Operand(0))
					{
						num = 1;
						continue;
					}
					break;
				case 11:
					if (this.Forwardchars() >= 1 && RegexCharClass.CharInClass(this.Forwardcharnext(), this._code.Strings[this.Operand(0)]))
					{
						num = 1;
						continue;
					}
					break;
				case 12:
					if (this.Stringmatch(this._code.Strings[this.Operand(0)]))
					{
						num = 1;
						continue;
					}
					break;
				case 13:
				{
					int cap = this.Operand(0);
					if (base.IsMatched(cap))
					{
						if (!this.Refmatch(base.MatchIndex(cap), base.MatchLength(cap)))
						{
							break;
						}
					}
					else if ((this.runregex.roptions & RegexOptions.ECMAScript) == RegexOptions.None)
					{
						break;
					}
					num = 1;
					continue;
				}
				case 14:
					if (this.Leftchars() <= 0 || this.CharAt(this.Textpos() - 1) == '\n')
					{
						num = 0;
						continue;
					}
					break;
				case 15:
					if (this.Rightchars() <= 0 || this.CharAt(this.Textpos()) == '\n')
					{
						num = 0;
						continue;
					}
					break;
				case 16:
					if (base.IsBoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						num = 0;
						continue;
					}
					break;
				case 17:
					if (!base.IsBoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						num = 0;
						continue;
					}
					break;
				case 18:
					if (this.Leftchars() <= 0)
					{
						num = 0;
						continue;
					}
					break;
				case 19:
					if (this.Textpos() == this.Textstart())
					{
						num = 0;
						continue;
					}
					break;
				case 20:
					if (this.Rightchars() <= 1 && (this.Rightchars() != 1 || this.CharAt(this.Textpos()) == '\n'))
					{
						num = 0;
						continue;
					}
					break;
				case 21:
					if (this.Rightchars() <= 0)
					{
						num = 0;
						continue;
					}
					break;
				case 22:
					break;
				case 23:
					this.TrackPush(this.Textpos());
					num = 1;
					continue;
				case 24:
					this.StackPop();
					if (this.Textpos() - this.StackPeek() != 0)
					{
						this.TrackPush(this.StackPeek(), this.Textpos());
						this.StackPush(this.Textpos());
						this.Goto(this.Operand(0));
						continue;
					}
					this.TrackPush2(this.StackPeek());
					num = 1;
					continue;
				case 25:
				{
					this.StackPop();
					int num11 = this.StackPeek();
					if (this.Textpos() != num11)
					{
						if (num11 != -1)
						{
							this.TrackPush(num11, this.Textpos());
						}
						else
						{
							this.TrackPush(this.Textpos(), this.Textpos());
						}
					}
					else
					{
						this.StackPush(num11);
						this.TrackPush2(this.StackPeek());
					}
					num = 1;
					continue;
				}
				case 26:
					this.StackPush(-1, this.Operand(0));
					this.TrackPush();
					num = 1;
					continue;
				case 27:
					this.StackPush(this.Textpos(), this.Operand(0));
					this.TrackPush();
					num = 1;
					continue;
				case 28:
				{
					this.StackPop(2);
					int num12 = this.StackPeek();
					int num13 = this.StackPeek(1);
					int num14 = this.Textpos() - num12;
					if (num13 >= this.Operand(1) || (num14 == 0 && num13 >= 0))
					{
						this.TrackPush2(num12, num13);
						num = 2;
						continue;
					}
					this.TrackPush(num12);
					this.StackPush(this.Textpos(), num13 + 1);
					this.Goto(this.Operand(0));
					continue;
				}
				case 29:
				{
					this.StackPop(2);
					int i2 = this.StackPeek();
					int num15 = this.StackPeek(1);
					if (num15 < 0)
					{
						this.TrackPush2(i2);
						this.StackPush(this.Textpos(), num15 + 1);
						this.Goto(this.Operand(0));
						continue;
					}
					this.TrackPush(i2, num15, this.Textpos());
					num = 2;
					continue;
				}
				case 30:
					this.StackPush(-1);
					this.TrackPush();
					num = 0;
					continue;
				case 31:
					this.StackPush(this.Textpos());
					this.TrackPush();
					num = 0;
					continue;
				case 32:
					if (this.Operand(1) == -1 || base.IsMatched(this.Operand(1)))
					{
						this.StackPop();
						if (this.Operand(1) != -1)
						{
							base.TransferCapture(this.Operand(0), this.Operand(1), this.StackPeek(), this.Textpos());
						}
						else
						{
							base.Capture(this.Operand(0), this.StackPeek(), this.Textpos());
						}
						this.TrackPush(this.StackPeek());
						num = 2;
						continue;
					}
					break;
				case 33:
					this.StackPop();
					this.TrackPush(this.StackPeek());
					this.Textto(this.StackPeek());
					num = 0;
					continue;
				case 34:
					this.StackPush(this.Trackpos(), base.Crawlpos());
					this.TrackPush();
					num = 0;
					continue;
				case 35:
					this.StackPop(2);
					this.Trackto(this.StackPeek());
					while (base.Crawlpos() != this.StackPeek(1))
					{
						base.Uncapture();
					}
					break;
				case 36:
					this.StackPop(2);
					this.Trackto(this.StackPeek());
					this.TrackPush(this.StackPeek(1));
					num = 0;
					continue;
				case 37:
					if (base.IsMatched(this.Operand(0)))
					{
						num = 1;
						continue;
					}
					break;
				case 38:
					this.Goto(this.Operand(0));
					continue;
				case 39:
					goto IL_D9D;
				case 40:
					return;
				case 41:
					if (base.IsECMABoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						num = 0;
						continue;
					}
					break;
				case 42:
					if (!base.IsECMABoundary(this.Textpos(), this.runtextbeg, this.runtextend))
					{
						num = 0;
						continue;
					}
					break;
				default:
					switch (num2)
					{
					case 131:
					case 132:
					{
						this.TrackPop(2);
						int num16 = this.TrackPeek();
						int num17 = this.TrackPeek(1);
						this.Textto(num17);
						if (num16 > 0)
						{
							this.TrackPush(num16 - 1, num17 - this.Bump());
						}
						num = 2;
						continue;
					}
					case 133:
					{
						this.TrackPop(2);
						int num18 = this.TrackPeek();
						int num19 = this.TrackPeek(1);
						this.Textto(num19);
						if (num18 > 0)
						{
							this.TrackPush(num18 - 1, num19 - this.Bump());
						}
						num = 2;
						continue;
					}
					case 134:
					{
						this.TrackPop(2);
						int num20 = this.TrackPeek(1);
						this.Textto(num20);
						if (this.Forwardcharnext() == (char)this.Operand(0))
						{
							int num21 = this.TrackPeek();
							if (num21 > 0)
							{
								this.TrackPush(num21 - 1, num20 + this.Bump());
							}
							num = 2;
							continue;
						}
						break;
					}
					case 135:
					{
						this.TrackPop(2);
						int num22 = this.TrackPeek(1);
						this.Textto(num22);
						if (this.Forwardcharnext() != (char)this.Operand(0))
						{
							int num23 = this.TrackPeek();
							if (num23 > 0)
							{
								this.TrackPush(num23 - 1, num22 + this.Bump());
							}
							num = 2;
							continue;
						}
						break;
					}
					case 136:
					{
						this.TrackPop(2);
						int num24 = this.TrackPeek(1);
						this.Textto(num24);
						if (RegexCharClass.CharInClass(this.Forwardcharnext(), this._code.Strings[this.Operand(0)]))
						{
							int num25 = this.TrackPeek();
							if (num25 > 0)
							{
								this.TrackPush(num25 - 1, num24 + this.Bump());
							}
							num = 2;
							continue;
						}
						break;
					}
					case 137:
					case 138:
					case 139:
					case 140:
					case 141:
					case 142:
					case 143:
					case 144:
					case 145:
					case 146:
					case 147:
					case 148:
					case 149:
					case 150:
					case 163:
						goto IL_D9D;
					case 151:
						this.TrackPop();
						this.Textto(this.TrackPeek());
						this.Goto(this.Operand(0));
						continue;
					case 152:
						this.TrackPop(2);
						this.StackPop();
						this.Textto(this.TrackPeek(1));
						this.TrackPush2(this.TrackPeek());
						num = 1;
						continue;
					case 153:
					{
						this.TrackPop(2);
						int num26 = this.TrackPeek(1);
						this.TrackPush2(this.TrackPeek());
						this.StackPush(num26);
						this.Textto(num26);
						this.Goto(this.Operand(0));
						continue;
					}
					case 154:
						this.StackPop(2);
						break;
					case 155:
						this.StackPop(2);
						break;
					case 156:
						this.TrackPop();
						this.StackPop(2);
						if (this.StackPeek(1) > 0)
						{
							this.Textto(this.StackPeek());
							this.TrackPush2(this.TrackPeek(), this.StackPeek(1) - 1);
							num = 2;
							continue;
						}
						this.StackPush(this.TrackPeek(), this.StackPeek(1) - 1);
						break;
					case 157:
					{
						this.TrackPop(3);
						int num27 = this.TrackPeek();
						int num28 = this.TrackPeek(2);
						if (this.TrackPeek(1) < this.Operand(1) && num28 != num27)
						{
							this.Textto(num28);
							this.StackPush(num28, this.TrackPeek(1) + 1);
							this.TrackPush2(num27);
							this.Goto(this.Operand(0));
							continue;
						}
						this.StackPush(this.TrackPeek(), this.TrackPeek(1));
						break;
					}
					case 158:
					case 159:
						this.StackPop();
						break;
					case 160:
						this.TrackPop();
						this.StackPush(this.TrackPeek());
						base.Uncapture();
						if (this.Operand(0) != -1 && this.Operand(1) != -1)
						{
							base.Uncapture();
						}
						break;
					case 161:
						this.TrackPop();
						this.StackPush(this.TrackPeek());
						break;
					case 162:
						this.StackPop(2);
						break;
					case 164:
						this.TrackPop();
						while (base.Crawlpos() != this.TrackPeek())
						{
							base.Uncapture();
						}
						break;
					default:
						switch (num2)
						{
						case 280:
							this.TrackPop();
							this.StackPush(this.TrackPeek());
							goto IL_DA8;
						case 281:
							this.StackPop();
							this.TrackPop();
							this.StackPush(this.TrackPeek());
							goto IL_DA8;
						case 284:
							this.TrackPop(2);
							this.StackPush(this.TrackPeek(), this.TrackPeek(1));
							goto IL_DA8;
						case 285:
							this.TrackPop();
							this.StackPop(2);
							this.StackPush(this.TrackPeek(), this.StackPeek(1) - 1);
							goto IL_DA8;
						}
						goto Block_4;
					}
					break;
				}
				IL_DA8:
				this.Backtrack();
			}
			Block_4:
			IL_D9D:
			throw NotImplemented.ByDesignWithMessage("Unimplemented state.");
		}

		// Token: 0x04000902 RID: 2306
		private readonly RegexCode _code;

		// Token: 0x04000903 RID: 2307
		private readonly CultureInfo _culture;

		// Token: 0x04000904 RID: 2308
		private int _operator;

		// Token: 0x04000905 RID: 2309
		private int _codepos;

		// Token: 0x04000906 RID: 2310
		private bool _rightToLeft;

		// Token: 0x04000907 RID: 2311
		private bool _caseInsensitive;
	}
}
