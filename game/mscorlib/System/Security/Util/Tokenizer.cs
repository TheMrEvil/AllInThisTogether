using System;
using System.IO;
using System.Text;

namespace System.Security.Util
{
	// Token: 0x020003EF RID: 1007
	internal sealed class Tokenizer
	{
		// Token: 0x0600298E RID: 10638 RVA: 0x00096FA8 File Offset: 0x000951A8
		internal void BasicInitialization()
		{
			this.LineNo = 1;
			this._inProcessingTag = 0;
			this._inSavedCharacter = -1;
			this._inIndex = 0;
			this._inSize = 0;
			this._inNestedSize = 0;
			this._inNestedIndex = 0;
			this._inTokenSource = Tokenizer.TokenSource.Other;
			this._maker = SharedStatics.GetSharedStringMaker();
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x00096FF8 File Offset: 0x000951F8
		public void Recycle()
		{
			SharedStatics.ReleaseSharedStringMaker(ref this._maker);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x00097005 File Offset: 0x00095205
		internal Tokenizer(string input)
		{
			this.BasicInitialization();
			this._inString = input;
			this._inSize = input.Length;
			this._inTokenSource = Tokenizer.TokenSource.String;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0009702D File Offset: 0x0009522D
		internal Tokenizer(string input, string[] searchStrings, string[] replaceStrings)
		{
			this.BasicInitialization();
			this._inString = input;
			this._inSize = this._inString.Length;
			this._inTokenSource = Tokenizer.TokenSource.NestedStrings;
			this._searchStrings = searchStrings;
			this._replaceStrings = replaceStrings;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00097068 File Offset: 0x00095268
		internal Tokenizer(byte[] array, Tokenizer.ByteTokenEncoding encoding, int startIndex)
		{
			this.BasicInitialization();
			this._inBytes = array;
			this._inSize = array.Length;
			this._inIndex = startIndex;
			switch (encoding)
			{
			case Tokenizer.ByteTokenEncoding.UnicodeTokens:
				this._inTokenSource = Tokenizer.TokenSource.UnicodeByteArray;
				return;
			case Tokenizer.ByteTokenEncoding.UTF8Tokens:
				this._inTokenSource = Tokenizer.TokenSource.UTF8ByteArray;
				return;
			case Tokenizer.ByteTokenEncoding.ByteTokens:
				this._inTokenSource = Tokenizer.TokenSource.ASCIIByteArray;
				return;
			default:
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[]
				{
					(int)encoding
				}));
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000970E2 File Offset: 0x000952E2
		internal Tokenizer(char[] array)
		{
			this.BasicInitialization();
			this._inChars = array;
			this._inSize = array.Length;
			this._inTokenSource = Tokenizer.TokenSource.CharArray;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x00097107 File Offset: 0x00095307
		internal Tokenizer(StreamReader input)
		{
			this.BasicInitialization();
			this._inTokenReader = new Tokenizer.StreamTokenReader(input);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00097124 File Offset: 0x00095324
		internal void ChangeFormat(Encoding encoding)
		{
			if (encoding == null)
			{
				return;
			}
			Tokenizer.TokenSource inTokenSource = this._inTokenSource;
			if (inTokenSource > Tokenizer.TokenSource.ASCIIByteArray)
			{
				if (inTokenSource - Tokenizer.TokenSource.CharArray <= 2)
				{
					return;
				}
			}
			else
			{
				if (encoding == Encoding.Unicode)
				{
					this._inTokenSource = Tokenizer.TokenSource.UnicodeByteArray;
					return;
				}
				if (encoding == Encoding.UTF8)
				{
					this._inTokenSource = Tokenizer.TokenSource.UTF8ByteArray;
					return;
				}
				if (encoding == Encoding.ASCII)
				{
					this._inTokenSource = Tokenizer.TokenSource.ASCIIByteArray;
					return;
				}
			}
			inTokenSource = this._inTokenSource;
			Stream stream;
			if (inTokenSource > Tokenizer.TokenSource.ASCIIByteArray)
			{
				if (inTokenSource - Tokenizer.TokenSource.CharArray <= 2)
				{
					return;
				}
				Tokenizer.StreamTokenReader streamTokenReader = this._inTokenReader as Tokenizer.StreamTokenReader;
				if (streamTokenReader == null)
				{
					return;
				}
				stream = streamTokenReader._in.BaseStream;
				string s = new string(' ', streamTokenReader.NumCharEncountered);
				stream.Position = (long)streamTokenReader._in.CurrentEncoding.GetByteCount(s);
			}
			else
			{
				stream = new MemoryStream(this._inBytes, this._inIndex, this._inSize - this._inIndex);
			}
			this._inTokenReader = new Tokenizer.StreamTokenReader(new StreamReader(stream, encoding));
			this._inTokenSource = Tokenizer.TokenSource.Other;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x0009720C File Offset: 0x0009540C
		internal void GetTokens(TokenizerStream stream, int maxNum, bool endAfterKet)
		{
			while (maxNum == -1 || stream.GetTokenCount() < maxNum)
			{
				int num = 0;
				bool flag = false;
				bool flag2 = false;
				Tokenizer.StringMaker maker = this._maker;
				maker._outStringBuilder = null;
				maker._outIndex = 0;
				int num2;
				for (;;)
				{
					if (this._inSavedCharacter != -1)
					{
						num2 = this._inSavedCharacter;
						this._inSavedCharacter = -1;
					}
					else
					{
						switch (this._inTokenSource)
						{
						case Tokenizer.TokenSource.UnicodeByteArray:
							if (this._inIndex + 1 >= this._inSize)
							{
								goto Block_3;
							}
							num2 = ((int)this._inBytes[this._inIndex + 1] << 8) + (int)this._inBytes[this._inIndex];
							this._inIndex += 2;
							break;
						case Tokenizer.TokenSource.UTF8ByteArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_4;
							}
							byte[] inBytes = this._inBytes;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inBytes[num3];
							if ((num2 & 128) != 0)
							{
								switch ((num2 & 240) >> 4)
								{
								case 8:
								case 9:
								case 10:
								case 11:
									goto IL_12D;
								case 12:
								case 13:
									num2 &= 31;
									num = 2;
									break;
								case 14:
									num2 &= 15;
									num = 3;
									break;
								case 15:
									goto IL_14B;
								}
								if (this._inIndex >= this._inSize)
								{
									goto Block_7;
								}
								byte[] inBytes2 = this._inBytes;
								num3 = this._inIndex;
								this._inIndex = num3 + 1;
								byte b = inBytes2[num3];
								if ((b & 192) != 128)
								{
									goto Block_8;
								}
								num2 = (num2 << 6 | (int)(b & 63));
								if (num != 2)
								{
									if (this._inIndex >= this._inSize)
									{
										goto Block_10;
									}
									byte[] inBytes3 = this._inBytes;
									num3 = this._inIndex;
									this._inIndex = num3 + 1;
									b = inBytes3[num3];
									if ((b & 192) != 128)
									{
										goto Block_11;
									}
									num2 = (num2 << 6 | (int)(b & 63));
								}
							}
							break;
						}
						case Tokenizer.TokenSource.ASCIIByteArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_12;
							}
							byte[] inBytes4 = this._inBytes;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inBytes4[num3];
							break;
						}
						case Tokenizer.TokenSource.CharArray:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_13;
							}
							char[] inChars = this._inChars;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = inChars[num3];
							break;
						}
						case Tokenizer.TokenSource.String:
						{
							if (this._inIndex >= this._inSize)
							{
								goto Block_14;
							}
							string inString = this._inString;
							int num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = (int)inString[num3];
							break;
						}
						case Tokenizer.TokenSource.NestedStrings:
						{
							int num3;
							if (this._inNestedSize != 0)
							{
								if (this._inNestedIndex < this._inNestedSize)
								{
									string inNestedString = this._inNestedString;
									num3 = this._inNestedIndex;
									this._inNestedIndex = num3 + 1;
									num2 = (int)inNestedString[num3];
									break;
								}
								this._inNestedSize = 0;
							}
							if (this._inIndex >= this._inSize)
							{
								goto Block_17;
							}
							string inString2 = this._inString;
							num3 = this._inIndex;
							this._inIndex = num3 + 1;
							num2 = (int)inString2[num3];
							if (num2 == 123)
							{
								for (int i = 0; i < this._searchStrings.Length; i++)
								{
									if (string.Compare(this._searchStrings[i], 0, this._inString, this._inIndex - 1, this._searchStrings[i].Length, StringComparison.Ordinal) == 0)
									{
										this._inNestedString = this._replaceStrings[i];
										this._inNestedSize = this._inNestedString.Length;
										this._inNestedIndex = 1;
										num2 = (int)this._inNestedString[0];
										this._inIndex += this._searchStrings[i].Length - 1;
										break;
									}
								}
							}
							break;
						}
						default:
							num2 = this._inTokenReader.Read();
							if (num2 == -1)
							{
								goto Block_21;
							}
							break;
						}
					}
					if (!flag)
					{
						if (num2 <= 34)
						{
							switch (num2)
							{
							case 9:
							case 13:
								continue;
							case 10:
								this.LineNo++;
								continue;
							case 11:
							case 12:
								break;
							default:
								switch (num2)
								{
								case 32:
									continue;
								case 33:
									if (this._inProcessingTag != 0)
									{
										goto Block_32;
									}
									break;
								case 34:
									flag = true;
									flag2 = true;
									continue;
								}
								break;
							}
						}
						else if (num2 != 45)
						{
							if (num2 != 47)
							{
								switch (num2)
								{
								case 60:
									goto IL_48A;
								case 61:
									goto IL_4C0;
								case 62:
									goto IL_4A4;
								case 63:
									if (this._inProcessingTag != 0)
									{
										goto Block_31;
									}
									break;
								}
							}
							else if (this._inProcessingTag != 0)
							{
								goto Block_30;
							}
						}
						else if (this._inProcessingTag != 0)
						{
							goto Block_33;
						}
					}
					else if (num2 <= 34)
					{
						switch (num2)
						{
						case 9:
						case 13:
							break;
						case 10:
							this.LineNo++;
							if (!flag2)
							{
								goto Block_46;
							}
							goto IL_62F;
						case 11:
						case 12:
							goto IL_62F;
						default:
							if (num2 != 32)
							{
								if (num2 != 34)
								{
									goto IL_62F;
								}
								if (flag2)
								{
									goto Block_44;
								}
								goto IL_62F;
							}
							break;
						}
						if (!flag2)
						{
							goto Block_45;
						}
					}
					else
					{
						if (num2 != 47)
						{
							if (num2 != 60)
							{
								if (num2 - 61 > 1)
								{
									goto IL_62F;
								}
							}
							else
							{
								if (!flag2)
								{
									goto Block_41;
								}
								goto IL_62F;
							}
						}
						if (!flag2 && this._inProcessingTag != 0)
						{
							goto Block_43;
						}
					}
					IL_62F:
					flag = true;
					if (maker._outIndex < 512)
					{
						char[] outChars = maker._outChars;
						Tokenizer.StringMaker stringMaker = maker;
						int num3 = stringMaker._outIndex;
						stringMaker._outIndex = num3 + 1;
						outChars[num3] = (ushort)num2;
					}
					else
					{
						if (maker._outStringBuilder == null)
						{
							maker._outStringBuilder = new StringBuilder();
						}
						maker._outStringBuilder.Append(maker._outChars, 0, 512);
						maker._outChars[0] = (char)num2;
						maker._outIndex = 1;
					}
				}
				IL_48A:
				this._inProcessingTag++;
				stream.AddToken(0);
				continue;
				Block_3:
				stream.AddToken(-1);
				return;
				IL_4A4:
				this._inProcessingTag--;
				stream.AddToken(1);
				if (endAfterKet)
				{
					return;
				}
				continue;
				IL_4C0:
				stream.AddToken(4);
				continue;
				Block_30:
				stream.AddToken(2);
				continue;
				Block_31:
				stream.AddToken(5);
				continue;
				Block_32:
				stream.AddToken(6);
				continue;
				Block_33:
				stream.AddToken(7);
				continue;
				Block_41:
				this._inSavedCharacter = num2;
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_43:
				this._inSavedCharacter = num2;
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_44:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_45:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_46:
				stream.AddToken(3);
				stream.AddString(this.GetStringToken());
				continue;
				Block_4:
				stream.AddToken(-1);
				return;
				IL_12D:
				throw new XmlSyntaxException(this.LineNo);
				IL_14B:
				throw new XmlSyntaxException(this.LineNo);
				Block_7:
				throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("Unexpected end of file."));
				Block_8:
				throw new XmlSyntaxException(this.LineNo);
				Block_10:
				throw new XmlSyntaxException(this.LineNo, Environment.GetResourceString("Unexpected end of file."));
				Block_11:
				throw new XmlSyntaxException(this.LineNo);
				Block_12:
				stream.AddToken(-1);
				return;
				Block_13:
				stream.AddToken(-1);
				return;
				Block_14:
				stream.AddToken(-1);
				return;
				Block_17:
				stream.AddToken(-1);
				return;
				Block_21:
				stream.AddToken(-1);
				return;
			}
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000978D6 File Offset: 0x00095AD6
		private string GetStringToken()
		{
			return this._maker.MakeString();
		}

		// Token: 0x04001F04 RID: 7940
		internal const byte bra = 0;

		// Token: 0x04001F05 RID: 7941
		internal const byte ket = 1;

		// Token: 0x04001F06 RID: 7942
		internal const byte slash = 2;

		// Token: 0x04001F07 RID: 7943
		internal const byte cstr = 3;

		// Token: 0x04001F08 RID: 7944
		internal const byte equals = 4;

		// Token: 0x04001F09 RID: 7945
		internal const byte quest = 5;

		// Token: 0x04001F0A RID: 7946
		internal const byte bang = 6;

		// Token: 0x04001F0B RID: 7947
		internal const byte dash = 7;

		// Token: 0x04001F0C RID: 7948
		internal const int intOpenBracket = 60;

		// Token: 0x04001F0D RID: 7949
		internal const int intCloseBracket = 62;

		// Token: 0x04001F0E RID: 7950
		internal const int intSlash = 47;

		// Token: 0x04001F0F RID: 7951
		internal const int intEquals = 61;

		// Token: 0x04001F10 RID: 7952
		internal const int intQuote = 34;

		// Token: 0x04001F11 RID: 7953
		internal const int intQuest = 63;

		// Token: 0x04001F12 RID: 7954
		internal const int intBang = 33;

		// Token: 0x04001F13 RID: 7955
		internal const int intDash = 45;

		// Token: 0x04001F14 RID: 7956
		internal const int intTab = 9;

		// Token: 0x04001F15 RID: 7957
		internal const int intCR = 13;

		// Token: 0x04001F16 RID: 7958
		internal const int intLF = 10;

		// Token: 0x04001F17 RID: 7959
		internal const int intSpace = 32;

		// Token: 0x04001F18 RID: 7960
		public int LineNo;

		// Token: 0x04001F19 RID: 7961
		private int _inProcessingTag;

		// Token: 0x04001F1A RID: 7962
		private byte[] _inBytes;

		// Token: 0x04001F1B RID: 7963
		private char[] _inChars;

		// Token: 0x04001F1C RID: 7964
		private string _inString;

		// Token: 0x04001F1D RID: 7965
		private int _inIndex;

		// Token: 0x04001F1E RID: 7966
		private int _inSize;

		// Token: 0x04001F1F RID: 7967
		private int _inSavedCharacter;

		// Token: 0x04001F20 RID: 7968
		private Tokenizer.TokenSource _inTokenSource;

		// Token: 0x04001F21 RID: 7969
		private Tokenizer.ITokenReader _inTokenReader;

		// Token: 0x04001F22 RID: 7970
		private Tokenizer.StringMaker _maker;

		// Token: 0x04001F23 RID: 7971
		private string[] _searchStrings;

		// Token: 0x04001F24 RID: 7972
		private string[] _replaceStrings;

		// Token: 0x04001F25 RID: 7973
		private int _inNestedIndex;

		// Token: 0x04001F26 RID: 7974
		private int _inNestedSize;

		// Token: 0x04001F27 RID: 7975
		private string _inNestedString;

		// Token: 0x020003F0 RID: 1008
		private enum TokenSource
		{
			// Token: 0x04001F29 RID: 7977
			UnicodeByteArray,
			// Token: 0x04001F2A RID: 7978
			UTF8ByteArray,
			// Token: 0x04001F2B RID: 7979
			ASCIIByteArray,
			// Token: 0x04001F2C RID: 7980
			CharArray,
			// Token: 0x04001F2D RID: 7981
			String,
			// Token: 0x04001F2E RID: 7982
			NestedStrings,
			// Token: 0x04001F2F RID: 7983
			Other
		}

		// Token: 0x020003F1 RID: 1009
		internal enum ByteTokenEncoding
		{
			// Token: 0x04001F31 RID: 7985
			UnicodeTokens,
			// Token: 0x04001F32 RID: 7986
			UTF8Tokens,
			// Token: 0x04001F33 RID: 7987
			ByteTokens
		}

		// Token: 0x020003F2 RID: 1010
		[Serializable]
		internal sealed class StringMaker
		{
			// Token: 0x06002998 RID: 10648 RVA: 0x000978E4 File Offset: 0x00095AE4
			private static uint HashString(string str)
			{
				uint num = 0U;
				int length = str.Length;
				for (int i = 0; i < length; i++)
				{
					num = (num << 3 ^ (uint)str[i] ^ num >> 29);
				}
				return num;
			}

			// Token: 0x06002999 RID: 10649 RVA: 0x00097918 File Offset: 0x00095B18
			private static uint HashCharArray(char[] a, int l)
			{
				uint num = 0U;
				for (int i = 0; i < l; i++)
				{
					num = (num << 3 ^ (uint)a[i] ^ num >> 29);
				}
				return num;
			}

			// Token: 0x0600299A RID: 10650 RVA: 0x00097941 File Offset: 0x00095B41
			public StringMaker()
			{
				this.cStringsMax = 2048U;
				this.cStringsUsed = 0U;
				this.aStrings = new string[this.cStringsMax];
				this._outChars = new char[512];
			}

			// Token: 0x0600299B RID: 10651 RVA: 0x0009797C File Offset: 0x00095B7C
			private bool CompareStringAndChars(string str, char[] a, int l)
			{
				if (str.Length != l)
				{
					return false;
				}
				for (int i = 0; i < l; i++)
				{
					if (a[i] != str[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600299C RID: 10652 RVA: 0x000979B0 File Offset: 0x00095BB0
			public string MakeString()
			{
				char[] outChars = this._outChars;
				int outIndex = this._outIndex;
				if (this._outStringBuilder != null)
				{
					this._outStringBuilder.Append(this._outChars, 0, this._outIndex);
					return this._outStringBuilder.ToString();
				}
				uint num3;
				if (this.cStringsUsed > this.cStringsMax / 4U * 3U)
				{
					uint num = this.cStringsMax * 2U;
					string[] array = new string[num];
					int num2 = 0;
					while ((long)num2 < (long)((ulong)this.cStringsMax))
					{
						if (this.aStrings[num2] != null)
						{
							num3 = Tokenizer.StringMaker.HashString(this.aStrings[num2]) % num;
							while (array[(int)num3] != null)
							{
								if ((num3 += 1U) >= num)
								{
									num3 = 0U;
								}
							}
							array[(int)num3] = this.aStrings[num2];
						}
						num2++;
					}
					this.cStringsMax = num;
					this.aStrings = array;
				}
				num3 = Tokenizer.StringMaker.HashCharArray(outChars, outIndex) % this.cStringsMax;
				string text;
				while ((text = this.aStrings[(int)num3]) != null)
				{
					if (this.CompareStringAndChars(text, outChars, outIndex))
					{
						return text;
					}
					if ((num3 += 1U) >= this.cStringsMax)
					{
						num3 = 0U;
					}
				}
				text = new string(outChars, 0, outIndex);
				this.aStrings[(int)num3] = text;
				this.cStringsUsed += 1U;
				return text;
			}

			// Token: 0x04001F34 RID: 7988
			private string[] aStrings;

			// Token: 0x04001F35 RID: 7989
			private uint cStringsMax;

			// Token: 0x04001F36 RID: 7990
			private uint cStringsUsed;

			// Token: 0x04001F37 RID: 7991
			public StringBuilder _outStringBuilder;

			// Token: 0x04001F38 RID: 7992
			public char[] _outChars;

			// Token: 0x04001F39 RID: 7993
			public int _outIndex;

			// Token: 0x04001F3A RID: 7994
			public const int outMaxSize = 512;
		}

		// Token: 0x020003F3 RID: 1011
		internal interface ITokenReader
		{
			// Token: 0x0600299D RID: 10653
			int Read();
		}

		// Token: 0x020003F4 RID: 1012
		internal class StreamTokenReader : Tokenizer.ITokenReader
		{
			// Token: 0x0600299E RID: 10654 RVA: 0x00097ADB File Offset: 0x00095CDB
			internal StreamTokenReader(StreamReader input)
			{
				this._in = input;
				this._numCharRead = 0;
			}

			// Token: 0x0600299F RID: 10655 RVA: 0x00097AF1 File Offset: 0x00095CF1
			public virtual int Read()
			{
				int num = this._in.Read();
				if (num != -1)
				{
					this._numCharRead++;
				}
				return num;
			}

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x060029A0 RID: 10656 RVA: 0x00097B10 File Offset: 0x00095D10
			internal int NumCharEncountered
			{
				get
				{
					return this._numCharRead;
				}
			}

			// Token: 0x04001F3B RID: 7995
			internal StreamReader _in;

			// Token: 0x04001F3C RID: 7996
			internal int _numCharRead;
		}
	}
}
