using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000396 RID: 918
	internal class NumberAction : ContainerAction
	{
		// Token: 0x06002530 RID: 9520 RVA: 0x000E1960 File Offset: 0x000DFB60
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string text = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Level))
			{
				if (text != "any" && text != "multiple" && text != "single")
				{
					throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
					{
						"level",
						text
					});
				}
				this.level = text;
			}
			else if (Ref.Equal(localName, compiler.Atoms.Count))
			{
				this.countPattern = text;
				this.countKey = compiler.AddQuery(text, true, true, true);
			}
			else if (Ref.Equal(localName, compiler.Atoms.From))
			{
				this.from = text;
				this.fromKey = compiler.AddQuery(text, true, true, true);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Value))
			{
				this.value = text;
				this.valueKey = compiler.AddQuery(text);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Format))
			{
				this.formatAvt = Avt.CompileAvt(compiler, text);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Lang))
			{
				this.langAvt = Avt.CompileAvt(compiler, text);
			}
			else if (Ref.Equal(localName, compiler.Atoms.LetterValue))
			{
				this.letterAvt = Avt.CompileAvt(compiler, text);
			}
			else if (Ref.Equal(localName, compiler.Atoms.GroupingSeparator))
			{
				this.groupingSepAvt = Avt.CompileAvt(compiler, text);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.GroupingSize))
				{
					return false;
				}
				this.groupingSizeAvt = Avt.CompileAvt(compiler, text);
			}
			return true;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000E1B24 File Offset: 0x000DFD24
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckEmpty(compiler);
			this.forwardCompatibility = compiler.ForwardCompatibility;
			this.formatTokens = NumberAction.ParseFormat(CompiledAction.PrecalculateAvt(ref this.formatAvt));
			this.letter = this.ParseLetter(CompiledAction.PrecalculateAvt(ref this.letterAvt));
			this.lang = CompiledAction.PrecalculateAvt(ref this.langAvt);
			this.groupingSep = CompiledAction.PrecalculateAvt(ref this.groupingSepAvt);
			if (this.groupingSep != null && this.groupingSep.Length > 1)
			{
				throw XsltException.Create("The value of the '{0}' attribute must be a single character.", new string[]
				{
					"grouping-separator"
				});
			}
			this.groupingSize = CompiledAction.PrecalculateAvt(ref this.groupingSizeAvt);
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000E1BDC File Offset: 0x000DFDDC
		private int numberAny(Processor processor, ActionFrame frame)
		{
			int num = 0;
			XPathNavigator xpathNavigator = frame.Node;
			if (xpathNavigator.NodeType == XPathNodeType.Attribute || xpathNavigator.NodeType == XPathNodeType.Namespace)
			{
				xpathNavigator = xpathNavigator.Clone();
				xpathNavigator.MoveToParent();
			}
			XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
			if (this.fromKey != -1)
			{
				bool flag = false;
				while (!processor.Matches(xpathNavigator2, this.fromKey))
				{
					if (!xpathNavigator2.MoveToParent())
					{
						IL_56:
						XPathNodeIterator xpathNodeIterator = xpathNavigator2.SelectDescendants(XPathNodeType.All, true);
						while (xpathNodeIterator.MoveNext())
						{
							if (processor.Matches(xpathNodeIterator.Current, this.fromKey))
							{
								flag = true;
								num = 0;
							}
							else if (this.MatchCountKey(processor, frame.Node, xpathNodeIterator.Current))
							{
								num++;
							}
							if (xpathNodeIterator.Current.IsSamePosition(xpathNavigator))
							{
								break;
							}
						}
						if (!flag)
						{
							return 0;
						}
						return num;
					}
				}
				flag = true;
				goto IL_56;
			}
			xpathNavigator2.MoveToRoot();
			XPathNodeIterator xpathNodeIterator2 = xpathNavigator2.SelectDescendants(XPathNodeType.All, true);
			while (xpathNodeIterator2.MoveNext())
			{
				if (this.MatchCountKey(processor, frame.Node, xpathNodeIterator2.Current))
				{
					num++;
				}
				if (xpathNodeIterator2.Current.IsSamePosition(xpathNavigator))
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x000E1CE6 File Offset: 0x000DFEE6
		private bool checkFrom(Processor processor, XPathNavigator nav)
		{
			if (this.fromKey == -1)
			{
				return true;
			}
			while (!processor.Matches(nav, this.fromKey))
			{
				if (!nav.MoveToParent())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x000E1D0D File Offset: 0x000DFF0D
		private bool moveToCount(XPathNavigator nav, Processor processor, XPathNavigator contextNode)
		{
			while (this.fromKey == -1 || !processor.Matches(nav, this.fromKey))
			{
				if (this.MatchCountKey(processor, contextNode, nav))
				{
					return true;
				}
				if (!nav.MoveToParent())
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000E1D40 File Offset: 0x000DFF40
		private int numberCount(XPathNavigator nav, Processor processor, XPathNavigator contextNode)
		{
			XPathNavigator xpathNavigator = nav.Clone();
			int num = 1;
			if (xpathNavigator.MoveToParent())
			{
				xpathNavigator.MoveToFirstChild();
				while (!xpathNavigator.IsSamePosition(nav))
				{
					if (this.MatchCountKey(processor, contextNode, xpathNavigator))
					{
						num++;
					}
					if (!xpathNavigator.MoveToNext())
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000E1D88 File Offset: 0x000DFF88
		private static object SimplifyValue(object value)
		{
			if (Type.GetTypeCode(value.GetType()) == TypeCode.Object)
			{
				XPathNodeIterator xpathNodeIterator = value as XPathNodeIterator;
				if (xpathNodeIterator != null)
				{
					if (xpathNodeIterator.MoveNext())
					{
						return xpathNodeIterator.Current.Value;
					}
					return string.Empty;
				}
				else
				{
					XPathNavigator xpathNavigator = value as XPathNavigator;
					if (xpathNavigator != null)
					{
						return xpathNavigator.Value;
					}
				}
			}
			return value;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000E1DDC File Offset: 0x000DFFDC
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			ArrayList numberList = processor.NumberList;
			int state = frame.State;
			if (state != 0)
			{
				if (state != 2)
				{
					return;
				}
			}
			else
			{
				numberList.Clear();
				if (this.valueKey != -1)
				{
					numberList.Add(NumberAction.SimplifyValue(processor.Evaluate(frame, this.valueKey)));
				}
				else if (this.level == "any")
				{
					int num = this.numberAny(processor, frame);
					if (num != 0)
					{
						numberList.Add(num);
					}
				}
				else
				{
					bool flag = this.level == "multiple";
					XPathNavigator node = frame.Node;
					XPathNavigator xpathNavigator = frame.Node.Clone();
					if (xpathNavigator.NodeType == XPathNodeType.Attribute || xpathNavigator.NodeType == XPathNodeType.Namespace)
					{
						xpathNavigator.MoveToParent();
					}
					while (this.moveToCount(xpathNavigator, processor, node))
					{
						numberList.Insert(0, this.numberCount(xpathNavigator, processor, node));
						if (!flag || !xpathNavigator.MoveToParent())
						{
							break;
						}
					}
					if (!this.checkFrom(processor, xpathNavigator))
					{
						numberList.Clear();
					}
				}
				frame.StoredOutput = NumberAction.Format(numberList, (this.formatAvt == null) ? this.formatTokens : NumberAction.ParseFormat(this.formatAvt.Evaluate(processor, frame)), (this.langAvt == null) ? this.lang : this.langAvt.Evaluate(processor, frame), (this.letterAvt == null) ? this.letter : this.ParseLetter(this.letterAvt.Evaluate(processor, frame)), (this.groupingSepAvt == null) ? this.groupingSep : this.groupingSepAvt.Evaluate(processor, frame), (this.groupingSizeAvt == null) ? this.groupingSize : this.groupingSizeAvt.Evaluate(processor, frame));
			}
			if (!processor.TextEvent(frame.StoredOutput))
			{
				frame.State = 2;
				return;
			}
			frame.Finished();
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000E1FAC File Offset: 0x000E01AC
		private bool MatchCountKey(Processor processor, XPathNavigator contextNode, XPathNavigator nav)
		{
			if (this.countKey != -1)
			{
				return processor.Matches(nav, this.countKey);
			}
			return contextNode.Name == nav.Name && this.BasicNodeType(contextNode.NodeType) == this.BasicNodeType(nav.NodeType);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000E2000 File Offset: 0x000E0200
		private XPathNodeType BasicNodeType(XPathNodeType type)
		{
			if (type == XPathNodeType.SignificantWhitespace || type == XPathNodeType.Whitespace)
			{
				return XPathNodeType.Text;
			}
			return type;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000E2010 File Offset: 0x000E0210
		private static string Format(ArrayList numberlist, List<NumberAction.FormatInfo> tokens, string lang, string letter, string groupingSep, string groupingSize)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			if (tokens != null)
			{
				num = tokens.Count;
			}
			NumberAction.NumberingFormat numberingFormat = new NumberAction.NumberingFormat();
			if (groupingSize != null)
			{
				try
				{
					numberingFormat.setGroupingSize(Convert.ToInt32(groupingSize, CultureInfo.InvariantCulture));
				}
				catch (FormatException)
				{
				}
				catch (OverflowException)
				{
				}
			}
			if (groupingSep != null)
			{
				int length = groupingSep.Length;
				numberingFormat.setGroupingSeparator(groupingSep);
			}
			if (0 < num)
			{
				NumberAction.FormatInfo formatInfo = tokens[0];
				NumberAction.FormatInfo formatInfo2 = null;
				if (num % 2 == 1)
				{
					formatInfo2 = tokens[num - 1];
					num--;
				}
				NumberAction.FormatInfo formatInfo3 = (2 < num) ? tokens[num - 2] : NumberAction.DefaultSeparator;
				NumberAction.FormatInfo formatInfo4 = (0 < num) ? tokens[num - 1] : NumberAction.DefaultFormat;
				if (formatInfo != null)
				{
					stringBuilder.Append(formatInfo.formatString);
				}
				int count = numberlist.Count;
				for (int i = 0; i < count; i++)
				{
					int num2 = i * 2;
					bool flag = num2 < num;
					if (0 < i)
					{
						NumberAction.FormatInfo formatInfo5 = flag ? tokens[num2] : formatInfo3;
						stringBuilder.Append(formatInfo5.formatString);
					}
					NumberAction.FormatInfo formatInfo6 = flag ? tokens[num2 + 1] : formatInfo4;
					numberingFormat.setNumberingType(formatInfo6.numSequence);
					numberingFormat.setMinLen(formatInfo6.length);
					stringBuilder.Append(numberingFormat.FormatItem(numberlist[i]));
				}
				if (formatInfo2 != null)
				{
					stringBuilder.Append(formatInfo2.formatString);
				}
			}
			else
			{
				numberingFormat.setNumberingType(NumberingSequence.FirstDecimal);
				for (int j = 0; j < numberlist.Count; j++)
				{
					if (j != 0)
					{
						stringBuilder.Append(".");
					}
					stringBuilder.Append(numberingFormat.FormatItem(numberlist[j]));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000E21D4 File Offset: 0x000E03D4
		private static void mapFormatToken(string wsToken, int startLen, int tokLen, out NumberingSequence seq, out int pminlen)
		{
			char c = wsToken[startLen];
			bool flag = false;
			pminlen = 1;
			seq = NumberingSequence.Nil;
			int num = (int)c;
			if (num <= 2406)
			{
				if (num != 48 && num != 2406)
				{
					goto IL_71;
				}
			}
			else if (num != 3664 && num != 51067 && num != 65296)
			{
				goto IL_71;
			}
			do
			{
				pminlen++;
			}
			while (--tokLen > 0 && c == wsToken[++startLen]);
			if (wsToken[startLen] != c + '\u0001')
			{
				flag = true;
			}
			IL_71:
			if (!flag)
			{
				num = (int)wsToken[startLen];
				if (num <= 3665)
				{
					if (num <= 1072)
					{
						if (num <= 73)
						{
							if (num == 49)
							{
								seq = NumberingSequence.FirstDecimal;
								goto IL_31F;
							}
							if (num == 65)
							{
								seq = NumberingSequence.FirstAlpha;
								goto IL_31F;
							}
							if (num == 73)
							{
								seq = NumberingSequence.FirstSpecial;
								goto IL_31F;
							}
						}
						else if (num <= 105)
						{
							if (num == 97)
							{
								seq = NumberingSequence.LCLetter;
								goto IL_31F;
							}
							if (num == 105)
							{
								seq = NumberingSequence.LCRoman;
								goto IL_31F;
							}
						}
						else
						{
							if (num == 1040)
							{
								seq = NumberingSequence.UCRus;
								goto IL_31F;
							}
							if (num == 1072)
							{
								seq = NumberingSequence.LCRus;
								goto IL_31F;
							}
						}
					}
					else if (num <= 2309)
					{
						if (num == 1488)
						{
							seq = NumberingSequence.Hebrew;
							goto IL_31F;
						}
						if (num == 1571)
						{
							seq = NumberingSequence.ArabicScript;
							goto IL_31F;
						}
						if (num == 2309)
						{
							seq = NumberingSequence.Hindi2;
							goto IL_31F;
						}
					}
					else if (num <= 2407)
					{
						if (num == 2325)
						{
							seq = NumberingSequence.Hindi1;
							goto IL_31F;
						}
						if (num == 2407)
						{
							seq = NumberingSequence.Hindi3;
							goto IL_31F;
						}
					}
					else
					{
						if (num == 3585)
						{
							seq = NumberingSequence.Thai1;
							goto IL_31F;
						}
						if (num == 3665)
						{
							seq = NumberingSequence.Thai2;
							goto IL_31F;
						}
					}
				}
				else if (num <= 23376)
				{
					if (num <= 12593)
					{
						if (num == 12450)
						{
							seq = NumberingSequence.DAiueo;
							goto IL_31F;
						}
						if (num == 12452)
						{
							seq = NumberingSequence.DIroha;
							goto IL_31F;
						}
						if (num == 12593)
						{
							seq = NumberingSequence.DChosung;
							goto IL_31F;
						}
					}
					else if (num <= 22769)
					{
						if (num == 19968)
						{
							seq = NumberingSequence.FEDecimal;
							goto IL_31F;
						}
						if (num == 22769)
						{
							seq = NumberingSequence.DbNum3;
							goto IL_31F;
						}
					}
					else
					{
						if (num == 22777)
						{
							seq = NumberingSequence.ChnCmplx;
							goto IL_31F;
						}
						if (num == 23376)
						{
							seq = NumberingSequence.Zodiac2;
							goto IL_31F;
						}
					}
				}
				else if (num <= 51068)
				{
					if (num != 30002)
					{
						if (num == 44032)
						{
							seq = NumberingSequence.Ganada;
							goto IL_31F;
						}
						if (num == 51068)
						{
							seq = NumberingSequence.KorDbNum1;
							goto IL_31F;
						}
					}
					else
					{
						if (tokLen > 1 && wsToken[startLen + 1] == '子')
						{
							seq = NumberingSequence.Zodiac3;
							tokLen--;
							startLen++;
							goto IL_31F;
						}
						seq = NumberingSequence.Zodiac1;
						goto IL_31F;
					}
				}
				else if (num <= 65297)
				{
					if (num == 54616)
					{
						seq = NumberingSequence.KorDbNum3;
						goto IL_31F;
					}
					if (num == 65297)
					{
						seq = NumberingSequence.DArabic;
						goto IL_31F;
					}
				}
				else
				{
					if (num == 65393)
					{
						seq = NumberingSequence.Aiueo;
						goto IL_31F;
					}
					if (num == 65394)
					{
						seq = NumberingSequence.Iroha;
						goto IL_31F;
					}
				}
				seq = NumberingSequence.FirstDecimal;
			}
			IL_31F:
			if (flag)
			{
				seq = NumberingSequence.FirstDecimal;
				pminlen = 0;
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000E250C File Offset: 0x000E070C
		private static List<NumberAction.FormatInfo> ParseFormat(string formatString)
		{
			if (formatString == null || formatString.Length == 0)
			{
				return null;
			}
			int i = 0;
			bool flag = CharUtil.IsAlphaNumeric(formatString[i]);
			List<NumberAction.FormatInfo> list = new List<NumberAction.FormatInfo>();
			int num = 0;
			if (flag)
			{
				list.Add(null);
			}
			while (i <= formatString.Length)
			{
				bool flag2 = (i < formatString.Length) ? CharUtil.IsAlphaNumeric(formatString[i]) : (!flag);
				if (flag != flag2)
				{
					NumberAction.FormatInfo formatInfo = new NumberAction.FormatInfo();
					if (flag)
					{
						NumberAction.mapFormatToken(formatString, num, i - num, out formatInfo.numSequence, out formatInfo.length);
					}
					else
					{
						formatInfo.isSeparator = true;
						formatInfo.formatString = formatString.Substring(num, i - num);
					}
					num = i;
					i++;
					list.Add(formatInfo);
					flag = flag2;
				}
				else
				{
					i++;
				}
			}
			return list;
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x000E25CC File Offset: 0x000E07CC
		private string ParseLetter(string letter)
		{
			if (letter == null || letter == "traditional" || letter == "alphabetic")
			{
				return letter;
			}
			if (!this.forwardCompatibility)
			{
				throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[]
				{
					"letter-value",
					letter
				});
			}
			return null;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000E261E File Offset: 0x000E081E
		public NumberAction()
		{
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000E263B File Offset: 0x000E083B
		// Note: this type is marked as 'beforefieldinit'.
		static NumberAction()
		{
		}

		// Token: 0x04001D3F RID: 7487
		private const long msofnfcNil = 0L;

		// Token: 0x04001D40 RID: 7488
		private const long msofnfcTraditional = 1L;

		// Token: 0x04001D41 RID: 7489
		private const long msofnfcAlwaysFormat = 2L;

		// Token: 0x04001D42 RID: 7490
		private const int cchMaxFormat = 63;

		// Token: 0x04001D43 RID: 7491
		private const int cchMaxFormatDecimal = 11;

		// Token: 0x04001D44 RID: 7492
		private static NumberAction.FormatInfo DefaultFormat = new NumberAction.FormatInfo(false, "0");

		// Token: 0x04001D45 RID: 7493
		private static NumberAction.FormatInfo DefaultSeparator = new NumberAction.FormatInfo(true, ".");

		// Token: 0x04001D46 RID: 7494
		private const int OutputNumber = 2;

		// Token: 0x04001D47 RID: 7495
		private string level;

		// Token: 0x04001D48 RID: 7496
		private string countPattern;

		// Token: 0x04001D49 RID: 7497
		private int countKey = -1;

		// Token: 0x04001D4A RID: 7498
		private string from;

		// Token: 0x04001D4B RID: 7499
		private int fromKey = -1;

		// Token: 0x04001D4C RID: 7500
		private string value;

		// Token: 0x04001D4D RID: 7501
		private int valueKey = -1;

		// Token: 0x04001D4E RID: 7502
		private Avt formatAvt;

		// Token: 0x04001D4F RID: 7503
		private Avt langAvt;

		// Token: 0x04001D50 RID: 7504
		private Avt letterAvt;

		// Token: 0x04001D51 RID: 7505
		private Avt groupingSepAvt;

		// Token: 0x04001D52 RID: 7506
		private Avt groupingSizeAvt;

		// Token: 0x04001D53 RID: 7507
		private List<NumberAction.FormatInfo> formatTokens;

		// Token: 0x04001D54 RID: 7508
		private string lang;

		// Token: 0x04001D55 RID: 7509
		private string letter;

		// Token: 0x04001D56 RID: 7510
		private string groupingSep;

		// Token: 0x04001D57 RID: 7511
		private string groupingSize;

		// Token: 0x04001D58 RID: 7512
		private bool forwardCompatibility;

		// Token: 0x02000397 RID: 919
		internal class FormatInfo
		{
			// Token: 0x06002540 RID: 9536 RVA: 0x000E265D File Offset: 0x000E085D
			public FormatInfo(bool isSeparator, string formatString)
			{
				this.isSeparator = isSeparator;
				this.formatString = formatString;
			}

			// Token: 0x06002541 RID: 9537 RVA: 0x0000216B File Offset: 0x0000036B
			public FormatInfo()
			{
			}

			// Token: 0x04001D59 RID: 7513
			public bool isSeparator;

			// Token: 0x04001D5A RID: 7514
			public NumberingSequence numSequence;

			// Token: 0x04001D5B RID: 7515
			public int length;

			// Token: 0x04001D5C RID: 7516
			public string formatString;
		}

		// Token: 0x02000398 RID: 920
		private class NumberingFormat : NumberFormatterBase
		{
			// Token: 0x06002542 RID: 9538 RVA: 0x000E2673 File Offset: 0x000E0873
			internal NumberingFormat()
			{
			}

			// Token: 0x06002543 RID: 9539 RVA: 0x000E267B File Offset: 0x000E087B
			internal void setNumberingType(NumberingSequence seq)
			{
				this.seq = seq;
			}

			// Token: 0x06002544 RID: 9540 RVA: 0x000E2684 File Offset: 0x000E0884
			internal void setMinLen(int cMinLen)
			{
				this.cMinLen = cMinLen;
			}

			// Token: 0x06002545 RID: 9541 RVA: 0x000E268D File Offset: 0x000E088D
			internal void setGroupingSeparator(string separator)
			{
				this.separator = separator;
			}

			// Token: 0x06002546 RID: 9542 RVA: 0x000E2696 File Offset: 0x000E0896
			internal void setGroupingSize(int sizeGroup)
			{
				if (0 <= sizeGroup && sizeGroup <= 9)
				{
					this.sizeGroup = sizeGroup;
				}
			}

			// Token: 0x06002547 RID: 9543 RVA: 0x000E26A8 File Offset: 0x000E08A8
			internal string FormatItem(object value)
			{
				double num;
				if (value is int)
				{
					num = (double)((int)value);
				}
				else
				{
					num = XmlConvert.ToXPathDouble(value);
					if (0.5 > num || double.IsPositiveInfinity(num))
					{
						return XmlConvert.ToXPathString(value);
					}
					num = XmlConvert.XPathRound(num);
				}
				NumberingSequence numberingSequence = this.seq;
				if (numberingSequence != NumberingSequence.FirstDecimal)
				{
					if (numberingSequence - NumberingSequence.FirstAlpha > 1)
					{
						if (numberingSequence - NumberingSequence.FirstSpecial <= 1)
						{
							if (num <= 32767.0)
							{
								StringBuilder stringBuilder = new StringBuilder();
								NumberFormatterBase.ConvertToRoman(stringBuilder, num, this.seq == NumberingSequence.FirstSpecial);
								return stringBuilder.ToString();
							}
						}
					}
					else if (num <= 2147483647.0)
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						NumberFormatterBase.ConvertToAlphabetic(stringBuilder2, num, (this.seq == NumberingSequence.FirstAlpha) ? 'A' : 'a', 26);
						return stringBuilder2.ToString();
					}
				}
				return NumberAction.NumberingFormat.ConvertToArabic(num, this.cMinLen, this.sizeGroup, this.separator);
			}

			// Token: 0x06002548 RID: 9544 RVA: 0x000E277C File Offset: 0x000E097C
			private static string ConvertToArabic(double val, int minLength, int groupSize, string groupSeparator)
			{
				string text;
				if (groupSize != 0 && groupSeparator != null)
				{
					NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
					numberFormatInfo.NumberGroupSizes = new int[]
					{
						groupSize
					};
					numberFormatInfo.NumberGroupSeparator = groupSeparator;
					if (Math.Floor(val) == val)
					{
						numberFormatInfo.NumberDecimalDigits = 0;
					}
					text = val.ToString("N", numberFormatInfo);
				}
				else
				{
					text = Convert.ToString(val, CultureInfo.InvariantCulture);
				}
				if (text.Length >= minLength)
				{
					return text;
				}
				StringBuilder stringBuilder = new StringBuilder(minLength);
				stringBuilder.Append('0', minLength - text.Length);
				stringBuilder.Append(text);
				return stringBuilder.ToString();
			}

			// Token: 0x04001D5D RID: 7517
			private NumberingSequence seq;

			// Token: 0x04001D5E RID: 7518
			private int cMinLen;

			// Token: 0x04001D5F RID: 7519
			private string separator;

			// Token: 0x04001D60 RID: 7520
			private int sizeGroup;
		}
	}
}
