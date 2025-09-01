using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x02000005 RID: 5
	[CLSCompliant(false)]
	public class MiniParser
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002064 File Offset: 0x00000264
		public MiniParser()
		{
			this.twoCharBuff = new int[2];
			this.splitCData = false;
			this.Reset();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002085 File Offset: 0x00000285
		public void Reset()
		{
			this.line = 0;
			this.col = 0;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002098 File Offset: 0x00000298
		protected static bool StrEquals(string str, StringBuilder sb, int sbStart, int len)
		{
			if (len != str.Length)
			{
				return false;
			}
			for (int i = 0; i < len; i++)
			{
				if (str[i] != sb[sbStart + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D1 File Offset: 0x000002D1
		protected void FatalErr(string descr)
		{
			throw new MiniParser.XMLError(descr, this.line, this.col);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		protected static int Xlat(int charCode, int state)
		{
			int num = state * MiniParser.INPUT_RANGE;
			int num2 = Math.Min(MiniParser.tbl.Length - num, MiniParser.INPUT_RANGE);
			while (--num2 >= 0)
			{
				ushort num3 = MiniParser.tbl[num];
				if (charCode == num3 >> 12)
				{
					return (int)(num3 & 4095);
				}
				num++;
			}
			return 4095;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public void Parse(MiniParser.IReader reader, MiniParser.IHandler handler)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (handler == null)
			{
				handler = new MiniParser.HandlerAdapter();
			}
			MiniParser.AttrListImpl attrListImpl = new MiniParser.AttrListImpl();
			string text = null;
			Stack stack = new Stack();
			string text2 = null;
			this.line = 1;
			this.col = 0;
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num3 = 0;
			handler.OnStartParsing(this);
			for (;;)
			{
				this.col++;
				num = reader.Read();
				if (num == -1)
				{
					break;
				}
				int num4 = "<>/?=&'\"![ ]\t\r\n".IndexOf((char)num) & 15;
				if (num4 != 13)
				{
					if (num4 == 12)
					{
						num4 = 10;
					}
					if (num4 == 14)
					{
						this.col = 0;
						this.line++;
						num4 = 10;
					}
					int num5 = MiniParser.Xlat(num4, num2);
					num2 = (num5 & 255);
					if (num != 10 || (num2 != 14 && num2 != 15))
					{
						num5 >>= 8;
						if (num2 >= 128)
						{
							if (num2 == 255)
							{
								this.FatalErr("State dispatch error.");
							}
							else
							{
								this.FatalErr(MiniParser.errors[num2 ^ 128]);
							}
						}
						switch (num5)
						{
						case 0:
							break;
						case 1:
						{
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							string text3 = null;
							if (stack.Count == 0 || text2 != (text3 = (stack.Pop() as string)))
							{
								if (text3 == null)
								{
									this.FatalErr("Tag stack underflow");
								}
								else
								{
									this.FatalErr(string.Format("Expected end tag '{0}' but found '{1}'", text2, text3));
								}
							}
							handler.OnEndElement(text2);
							continue;
						}
						case 2:
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							if (num != 47 && num != 62)
							{
								continue;
							}
							break;
						case 3:
							text = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							continue;
						case 4:
							if (text == null)
							{
								this.FatalErr("Internal error.");
							}
							attrListImpl.Add(text, stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							text = null;
							continue;
						case 5:
							handler.OnChars(stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							continue;
						case 6:
						{
							string text4 = "CDATA[";
							flag2 = false;
							flag3 = false;
							if (num == 45)
							{
								num = reader.Read();
								if (num != 45)
								{
									this.FatalErr("Invalid comment");
								}
								this.col++;
								flag2 = true;
								this.twoCharBuff[0] = -1;
								this.twoCharBuff[1] = -1;
								continue;
							}
							if (num != 91)
							{
								flag3 = true;
								num3 = 0;
								continue;
							}
							for (int i = 0; i < text4.Length; i++)
							{
								if (reader.Read() != (int)text4[i])
								{
									this.col += i + 1;
									break;
								}
							}
							this.col += text4.Length;
							flag = true;
							continue;
						}
						case 7:
						{
							int num6 = 0;
							num = 93;
							while (num == 93)
							{
								num = reader.Read();
								num6++;
							}
							if (num != 62)
							{
								for (int j = 0; j < num6; j++)
								{
									stringBuilder.Append(']');
								}
								stringBuilder.Append((char)num);
								num2 = 18;
							}
							else
							{
								for (int k = 0; k < num6 - 2; k++)
								{
									stringBuilder.Append(']');
								}
								flag = false;
							}
							this.col += num6;
							continue;
						}
						case 8:
							this.FatalErr(string.Format("Error {0}", num2));
							continue;
						case 9:
							continue;
						case 10:
							stringBuilder = new StringBuilder();
							if (num != 60)
							{
								goto IL_3E3;
							}
							continue;
						case 11:
							goto IL_3E3;
						case 12:
							if (flag2)
							{
								if (num == 62 && this.twoCharBuff[0] == 45 && this.twoCharBuff[1] == 45)
								{
									flag2 = false;
									num2 = 0;
									continue;
								}
								this.twoCharBuff[0] = this.twoCharBuff[1];
								this.twoCharBuff[1] = num;
								continue;
							}
							else
							{
								if (!flag3)
								{
									if (this.splitCData && stringBuilder.Length > 0 && flag)
									{
										handler.OnChars(stringBuilder.ToString());
										stringBuilder = new StringBuilder();
									}
									flag = false;
									stringBuilder.Append((char)num);
									continue;
								}
								if (num == 60 || num == 62)
								{
									num3 ^= 1;
								}
								if (num == 62 && num3 != 0)
								{
									flag3 = false;
									num2 = 0;
									continue;
								}
								continue;
							}
							break;
						case 13:
						{
							num = reader.Read();
							int num7 = this.col + 1;
							if (num == 35)
							{
								int num8 = 10;
								int num9 = 0;
								int num10 = 0;
								num = reader.Read();
								num7++;
								if (num == 120)
								{
									num = reader.Read();
									num7++;
									num8 = 16;
								}
								NumberStyles style = (num8 == 16) ? NumberStyles.HexNumber : NumberStyles.Integer;
								for (;;)
								{
									int num11 = -1;
									if (char.IsNumber((char)num) || "abcdef".IndexOf(char.ToLower((char)num)) != -1)
									{
										try
										{
											num11 = int.Parse(new string((char)num, 1), style);
										}
										catch (FormatException)
										{
											num11 = -1;
										}
									}
									if (num11 == -1)
									{
										break;
									}
									num9 *= num8;
									num9 += num11;
									num10++;
									num = reader.Read();
									num7++;
								}
								if (num == 59 && num10 > 0)
								{
									stringBuilder.Append((char)num9);
								}
								else
								{
									this.FatalErr("Bad char ref");
								}
							}
							else
							{
								string text5 = "aglmopqstu";
								string text6 = "&'\"><";
								int num12 = 0;
								int num13 = 15;
								int num14 = 0;
								int length = stringBuilder.Length;
								for (;;)
								{
									if (num12 != 15)
									{
										num12 = (text5.IndexOf((char)num) & 15);
									}
									if (num12 == 15)
									{
										this.FatalErr(MiniParser.errors[7]);
									}
									stringBuilder.Append((char)num);
									int num15 = (int)"Ｕ㾏侏ཟｸ⊙ｏ"[num12];
									int num16 = num15 >> 4 & 15;
									int num17 = num15 & 15;
									int num18 = num15 >> 12;
									int num19 = num15 >> 8 & 15;
									num = reader.Read();
									num7++;
									num12 = 15;
									if (num16 != 15 && num == (int)text5[num16])
									{
										if (num18 < 14)
										{
											num13 = num18;
										}
										num14 = 12;
									}
									else if (num17 != 15 && num == (int)text5[num17])
									{
										if (num19 < 14)
										{
											num13 = num19;
										}
										num14 = 8;
									}
									else if (num == 59)
									{
										if (num13 != 15 && num14 != 0 && (num15 >> num14 & 15) == 14)
										{
											break;
										}
										continue;
									}
									num12 = 0;
								}
								int num20 = num7 - this.col - 1;
								if (num20 > 0 && num20 < 5 && (MiniParser.StrEquals("amp", stringBuilder, length, num20) || MiniParser.StrEquals("apos", stringBuilder, length, num20) || MiniParser.StrEquals("quot", stringBuilder, length, num20) || MiniParser.StrEquals("lt", stringBuilder, length, num20) || MiniParser.StrEquals("gt", stringBuilder, length, num20)))
								{
									stringBuilder.Length = length;
									stringBuilder.Append(text6[num13]);
								}
								else
								{
									this.FatalErr(MiniParser.errors[7]);
								}
							}
							this.col = num7;
							continue;
						}
						default:
							this.FatalErr(string.Format("Unexpected action code - {0}.", num5));
							continue;
						}
						handler.OnStartElement(text2, attrListImpl);
						if (num != 47)
						{
							stack.Push(text2);
						}
						else
						{
							handler.OnEndElement(text2);
						}
						attrListImpl.Clear();
						continue;
						IL_3E3:
						stringBuilder.Append((char)num);
					}
				}
			}
			if (num2 != 0)
			{
				this.FatalErr("Unexpected EOF");
			}
			handler.OnEndParsing(this);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000028CC File Offset: 0x00000ACC
		// Note: this type is marked as 'beforefieldinit'.
		static MiniParser()
		{
		}

		// Token: 0x04000038 RID: 56
		private static readonly int INPUT_RANGE = 13;

		// Token: 0x04000039 RID: 57
		private static readonly ushort[] tbl = new ushort[]
		{
			2305,
			43264,
			63616,
			10368,
			6272,
			14464,
			18560,
			22656,
			26752,
			34944,
			39040,
			47232,
			30848,
			2177,
			10498,
			6277,
			14595,
			18561,
			22657,
			26753,
			35088,
			39041,
			43137,
			47233,
			30849,
			64004,
			4352,
			43266,
			64258,
			2177,
			10369,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			14597,
			2307,
			10499,
			6403,
			18691,
			22787,
			26883,
			35075,
			39171,
			43267,
			47363,
			30979,
			63747,
			64260,
			8710,
			4615,
			41480,
			2177,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			6400,
			2307,
			10499,
			14595,
			18691,
			22787,
			26883,
			35075,
			39171,
			43267,
			47363,
			30979,
			63747,
			6400,
			2177,
			10369,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			43137,
			47233,
			30849,
			63617,
			2561,
			23818,
			11274,
			7178,
			15370,
			19466,
			27658,
			35850,
			39946,
			43783,
			48138,
			31754,
			64522,
			64265,
			8198,
			4103,
			43272,
			2177,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			64265,
			17163,
			43276,
			2178,
			10370,
			6274,
			14466,
			22658,
			26754,
			34946,
			39042,
			47234,
			30850,
			2317,
			23818,
			11274,
			7178,
			15370,
			19466,
			27658,
			35850,
			39946,
			44042,
			48138,
			31754,
			64522,
			26894,
			30991,
			43275,
			2180,
			10372,
			6276,
			14468,
			18564,
			22660,
			34948,
			39044,
			47236,
			63620,
			17163,
			43276,
			2178,
			10370,
			6274,
			14466,
			22658,
			26754,
			34946,
			39042,
			47234,
			30850,
			63618,
			9474,
			35088,
			2182,
			6278,
			14470,
			18566,
			22662,
			26758,
			39046,
			43142,
			47238,
			30854,
			63622,
			25617,
			23822,
			2830,
			11022,
			6926,
			15118,
			19214,
			35598,
			39694,
			43790,
			47886,
			31502,
			64270,
			29713,
			23823,
			2831,
			11023,
			6927,
			15119,
			19215,
			27407,
			35599,
			39695,
			43791,
			47887,
			64271,
			38418,
			6400,
			1555,
			9747,
			13843,
			17939,
			22035,
			26131,
			34323,
			42515,
			46611,
			30227,
			62995,
			8198,
			4103,
			43281,
			64265,
			2177,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			46858,
			3090,
			11282,
			7186,
			15378,
			19474,
			23570,
			27666,
			35858,
			39954,
			44050,
			31762,
			64530,
			3091,
			11283,
			7187,
			15379,
			19475,
			23571,
			27667,
			35859,
			39955,
			44051,
			48147,
			31763,
			64531,
			ushort.MaxValue,
			ushort.MaxValue
		};

		// Token: 0x0400003A RID: 58
		protected static string[] errors = new string[]
		{
			"Expected element",
			"Invalid character in tag",
			"No '='",
			"Invalid character entity",
			"Invalid attr value",
			"Empty tag",
			"No end tag",
			"Bad entity ref"
		};

		// Token: 0x0400003B RID: 59
		protected int line;

		// Token: 0x0400003C RID: 60
		protected int col;

		// Token: 0x0400003D RID: 61
		protected int[] twoCharBuff;

		// Token: 0x0400003E RID: 62
		protected bool splitCData;

		// Token: 0x02000072 RID: 114
		public interface IReader
		{
			// Token: 0x06000489 RID: 1161
			int Read();
		}

		// Token: 0x02000073 RID: 115
		public interface IAttrList
		{
			// Token: 0x17000126 RID: 294
			// (get) Token: 0x0600048A RID: 1162
			int Length { get; }

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x0600048B RID: 1163
			bool IsEmpty { get; }

			// Token: 0x0600048C RID: 1164
			string GetName(int i);

			// Token: 0x0600048D RID: 1165
			string GetValue(int i);

			// Token: 0x0600048E RID: 1166
			string GetValue(string name);

			// Token: 0x0600048F RID: 1167
			void ChangeValue(string name, string newValue);

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000490 RID: 1168
			string[] Names { get; }

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06000491 RID: 1169
			string[] Values { get; }
		}

		// Token: 0x02000074 RID: 116
		public interface IMutableAttrList : MiniParser.IAttrList
		{
			// Token: 0x06000492 RID: 1170
			void Clear();

			// Token: 0x06000493 RID: 1171
			void Add(string name, string value);

			// Token: 0x06000494 RID: 1172
			void CopyFrom(MiniParser.IAttrList attrs);

			// Token: 0x06000495 RID: 1173
			void Remove(int i);

			// Token: 0x06000496 RID: 1174
			void Remove(string name);
		}

		// Token: 0x02000075 RID: 117
		public interface IHandler
		{
			// Token: 0x06000497 RID: 1175
			void OnStartParsing(MiniParser parser);

			// Token: 0x06000498 RID: 1176
			void OnStartElement(string name, MiniParser.IAttrList attrs);

			// Token: 0x06000499 RID: 1177
			void OnEndElement(string name);

			// Token: 0x0600049A RID: 1178
			void OnChars(string ch);

			// Token: 0x0600049B RID: 1179
			void OnEndParsing(MiniParser parser);
		}

		// Token: 0x02000076 RID: 118
		public class HandlerAdapter : MiniParser.IHandler
		{
			// Token: 0x0600049C RID: 1180 RVA: 0x00017E3C File Offset: 0x0001603C
			public HandlerAdapter()
			{
			}

			// Token: 0x0600049D RID: 1181 RVA: 0x00017E44 File Offset: 0x00016044
			public void OnStartParsing(MiniParser parser)
			{
			}

			// Token: 0x0600049E RID: 1182 RVA: 0x00017E46 File Offset: 0x00016046
			public void OnStartElement(string name, MiniParser.IAttrList attrs)
			{
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x00017E48 File Offset: 0x00016048
			public void OnEndElement(string name)
			{
			}

			// Token: 0x060004A0 RID: 1184 RVA: 0x00017E4A File Offset: 0x0001604A
			public void OnChars(string ch)
			{
			}

			// Token: 0x060004A1 RID: 1185 RVA: 0x00017E4C File Offset: 0x0001604C
			public void OnEndParsing(MiniParser parser)
			{
			}
		}

		// Token: 0x02000077 RID: 119
		private enum CharKind : byte
		{
			// Token: 0x04000367 RID: 871
			LEFT_BR,
			// Token: 0x04000368 RID: 872
			RIGHT_BR,
			// Token: 0x04000369 RID: 873
			SLASH,
			// Token: 0x0400036A RID: 874
			PI_MARK,
			// Token: 0x0400036B RID: 875
			EQ,
			// Token: 0x0400036C RID: 876
			AMP,
			// Token: 0x0400036D RID: 877
			SQUOTE,
			// Token: 0x0400036E RID: 878
			DQUOTE,
			// Token: 0x0400036F RID: 879
			BANG,
			// Token: 0x04000370 RID: 880
			LEFT_SQBR,
			// Token: 0x04000371 RID: 881
			SPACE,
			// Token: 0x04000372 RID: 882
			RIGHT_SQBR,
			// Token: 0x04000373 RID: 883
			TAB,
			// Token: 0x04000374 RID: 884
			CR,
			// Token: 0x04000375 RID: 885
			EOL,
			// Token: 0x04000376 RID: 886
			CHARS,
			// Token: 0x04000377 RID: 887
			UNKNOWN = 31
		}

		// Token: 0x02000078 RID: 120
		private enum ActionCode : byte
		{
			// Token: 0x04000379 RID: 889
			START_ELEM,
			// Token: 0x0400037A RID: 890
			END_ELEM,
			// Token: 0x0400037B RID: 891
			END_NAME,
			// Token: 0x0400037C RID: 892
			SET_ATTR_NAME,
			// Token: 0x0400037D RID: 893
			SET_ATTR_VAL,
			// Token: 0x0400037E RID: 894
			SEND_CHARS,
			// Token: 0x0400037F RID: 895
			START_CDATA,
			// Token: 0x04000380 RID: 896
			END_CDATA,
			// Token: 0x04000381 RID: 897
			ERROR,
			// Token: 0x04000382 RID: 898
			STATE_CHANGE,
			// Token: 0x04000383 RID: 899
			FLUSH_CHARS_STATE_CHANGE,
			// Token: 0x04000384 RID: 900
			ACC_CHARS_STATE_CHANGE,
			// Token: 0x04000385 RID: 901
			ACC_CDATA,
			// Token: 0x04000386 RID: 902
			PROC_CHAR_REF,
			// Token: 0x04000387 RID: 903
			UNKNOWN = 15
		}

		// Token: 0x02000079 RID: 121
		public class AttrListImpl : MiniParser.IMutableAttrList, MiniParser.IAttrList
		{
			// Token: 0x060004A2 RID: 1186 RVA: 0x00017E4E File Offset: 0x0001604E
			public AttrListImpl() : this(0)
			{
			}

			// Token: 0x060004A3 RID: 1187 RVA: 0x00017E57 File Offset: 0x00016057
			public AttrListImpl(int initialCapacity)
			{
				if (initialCapacity <= 0)
				{
					this.names = new ArrayList();
					this.values = new ArrayList();
					return;
				}
				this.names = new ArrayList(initialCapacity);
				this.values = new ArrayList(initialCapacity);
			}

			// Token: 0x060004A4 RID: 1188 RVA: 0x00017E92 File Offset: 0x00016092
			public AttrListImpl(MiniParser.IAttrList attrs) : this((attrs != null) ? attrs.Length : 0)
			{
				if (attrs != null)
				{
					this.CopyFrom(attrs);
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00017EB0 File Offset: 0x000160B0
			public int Length
			{
				get
				{
					return this.names.Count;
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00017EBD File Offset: 0x000160BD
			public bool IsEmpty
			{
				get
				{
					return this.Length != 0;
				}
			}

			// Token: 0x060004A7 RID: 1191 RVA: 0x00017EC8 File Offset: 0x000160C8
			public string GetName(int i)
			{
				string result = null;
				if (i >= 0 && i < this.Length)
				{
					result = (this.names[i] as string);
				}
				return result;
			}

			// Token: 0x060004A8 RID: 1192 RVA: 0x00017EF8 File Offset: 0x000160F8
			public string GetValue(int i)
			{
				string result = null;
				if (i >= 0 && i < this.Length)
				{
					result = (this.values[i] as string);
				}
				return result;
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x00017F27 File Offset: 0x00016127
			public string GetValue(string name)
			{
				return this.GetValue(this.names.IndexOf(name));
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x00017F3C File Offset: 0x0001613C
			public void ChangeValue(string name, string newValue)
			{
				int num = this.names.IndexOf(name);
				if (num >= 0 && num < this.Length)
				{
					this.values[num] = newValue;
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060004AB RID: 1195 RVA: 0x00017F70 File Offset: 0x00016170
			public string[] Names
			{
				get
				{
					return this.names.ToArray(typeof(string)) as string[];
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060004AC RID: 1196 RVA: 0x00017F8C File Offset: 0x0001618C
			public string[] Values
			{
				get
				{
					return this.values.ToArray(typeof(string)) as string[];
				}
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x00017FA8 File Offset: 0x000161A8
			public void Clear()
			{
				this.names.Clear();
				this.values.Clear();
			}

			// Token: 0x060004AE RID: 1198 RVA: 0x00017FC0 File Offset: 0x000161C0
			public void Add(string name, string value)
			{
				this.names.Add(name);
				this.values.Add(value);
			}

			// Token: 0x060004AF RID: 1199 RVA: 0x00017FDC File Offset: 0x000161DC
			public void Remove(int i)
			{
				if (i >= 0)
				{
					this.names.RemoveAt(i);
					this.values.RemoveAt(i);
				}
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x00017FFA File Offset: 0x000161FA
			public void Remove(string name)
			{
				this.Remove(this.names.IndexOf(name));
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x00018010 File Offset: 0x00016210
			public void CopyFrom(MiniParser.IAttrList attrs)
			{
				if (attrs != null && this == attrs)
				{
					this.Clear();
					int length = attrs.Length;
					for (int i = 0; i < length; i++)
					{
						this.Add(attrs.GetName(i), attrs.GetValue(i));
					}
				}
			}

			// Token: 0x04000388 RID: 904
			protected ArrayList names;

			// Token: 0x04000389 RID: 905
			protected ArrayList values;
		}

		// Token: 0x0200007A RID: 122
		public class XMLError : Exception
		{
			// Token: 0x060004B2 RID: 1202 RVA: 0x00018051 File Offset: 0x00016251
			public XMLError() : this("Unknown")
			{
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x0001805E File Offset: 0x0001625E
			public XMLError(string descr) : this(descr, -1, -1)
			{
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x00018069 File Offset: 0x00016269
			public XMLError(string descr, int line, int column) : base(descr)
			{
				this.descr = descr;
				this.line = line;
				this.column = column;
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00018087 File Offset: 0x00016287
			public int Line
			{
				get
				{
					return this.line;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0001808F File Offset: 0x0001628F
			public int Column
			{
				get
				{
					return this.column;
				}
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x00018097 File Offset: 0x00016297
			public override string ToString()
			{
				return string.Format("{0} @ (line = {1}, col = {2})", this.descr, this.line, this.column);
			}

			// Token: 0x0400038A RID: 906
			protected string descr;

			// Token: 0x0400038B RID: 907
			protected int line;

			// Token: 0x0400038C RID: 908
			protected int column;
		}
	}
}
