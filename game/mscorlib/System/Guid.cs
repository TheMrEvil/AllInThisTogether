using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Represents a globally unique identifier (GUID).</summary>
	// Token: 0x0200012F RID: 303
	[NonVersionable]
	[Serializable]
	public struct Guid : IFormattable, IComparable, IComparable<Guid>, IEquatable<Guid>, ISpanFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure.</summary>
		/// <returns>A new GUID object.</returns>
		// Token: 0x06000B97 RID: 2967 RVA: 0x0002F680 File Offset: 0x0002D880
		public unsafe static Guid NewGuid()
		{
			Guid guid;
			Interop.GetRandomBytes((byte*)(&guid), sizeof(Guid));
			guid._c = (short)(((int)guid._c & -61441) | 16384);
			guid._d = (byte)(((int)guid._d & -193) | 128);
			return guid;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the specified array of bytes.</summary>
		/// <param name="b">A 16-element byte array containing values with which to initialize the GUID.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="b" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="b" /> is not 16 bytes long.</exception>
		// Token: 0x06000B98 RID: 2968 RVA: 0x0002F6D0 File Offset: 0x0002D8D0
		public Guid(byte[] b)
		{
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			this = new Guid(new ReadOnlySpan<byte>(b));
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002F6F0 File Offset: 0x0002D8F0
		public unsafe Guid(ReadOnlySpan<byte> b)
		{
			if (b.Length != 16)
			{
				throw new ArgumentException(SR.Format("Byte array for GUID must be exactly {0} bytes long.", "16"), "b");
			}
			this._a = ((int)(*b[3]) << 24 | (int)(*b[2]) << 16 | (int)(*b[1]) << 8 | (int)(*b[0]));
			this._b = (short)((int)(*b[5]) << 8 | (int)(*b[4]));
			this._c = (short)((int)(*b[7]) << 8 | (int)(*b[6]));
			this._d = *b[8];
			this._e = *b[9];
			this._f = *b[10];
			this._g = *b[11];
			this._h = *b[12];
			this._i = *b[13];
			this._j = *b[14];
			this._k = *b[15];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the specified unsigned integers and bytes.</summary>
		/// <param name="a">The first 4 bytes of the GUID.</param>
		/// <param name="b">The next 2 bytes of the GUID.</param>
		/// <param name="c">The next 2 bytes of the GUID.</param>
		/// <param name="d">The next byte of the GUID.</param>
		/// <param name="e">The next byte of the GUID.</param>
		/// <param name="f">The next byte of the GUID.</param>
		/// <param name="g">The next byte of the GUID.</param>
		/// <param name="h">The next byte of the GUID.</param>
		/// <param name="i">The next byte of the GUID.</param>
		/// <param name="j">The next byte of the GUID.</param>
		/// <param name="k">The next byte of the GUID.</param>
		// Token: 0x06000B9A RID: 2970 RVA: 0x0002F810 File Offset: 0x0002DA10
		[CLSCompliant(false)]
		public Guid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = (int)a;
			this._b = (short)b;
			this._c = (short)c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the specified integers and byte array.</summary>
		/// <param name="a">The first 4 bytes of the GUID.</param>
		/// <param name="b">The next 2 bytes of the GUID.</param>
		/// <param name="c">The next 2 bytes of the GUID.</param>
		/// <param name="d">The remaining 8 bytes of the GUID.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="d" /> is not 8 bytes long.</exception>
		// Token: 0x06000B9B RID: 2971 RVA: 0x0002F874 File Offset: 0x0002DA74
		public Guid(int a, short b, short c, byte[] d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			if (d.Length != 8)
			{
				throw new ArgumentException(SR.Format("Byte array for GUID must be exactly {0} bytes long.", "8"), "d");
			}
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d[0];
			this._e = d[1];
			this._f = d[2];
			this._g = d[3];
			this._h = d[4];
			this._i = d[5];
			this._j = d[6];
			this._k = d[7];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the specified integers and bytes.</summary>
		/// <param name="a">The first 4 bytes of the GUID.</param>
		/// <param name="b">The next 2 bytes of the GUID.</param>
		/// <param name="c">The next 2 bytes of the GUID.</param>
		/// <param name="d">The next byte of the GUID.</param>
		/// <param name="e">The next byte of the GUID.</param>
		/// <param name="f">The next byte of the GUID.</param>
		/// <param name="g">The next byte of the GUID.</param>
		/// <param name="h">The next byte of the GUID.</param>
		/// <param name="i">The next byte of the GUID.</param>
		/// <param name="j">The next byte of the GUID.</param>
		/// <param name="k">The next byte of the GUID.</param>
		// Token: 0x06000B9C RID: 2972 RVA: 0x0002F918 File Offset: 0x0002DB18
		public Guid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Guid" /> structure by using the value represented by the specified string.</summary>
		/// <param name="g">A string that contains a GUID in one of the following formats ("d" represents a hexadecimal digit whose case is ignored):  
		///  32 contiguous digits:  
		///  dddddddddddddddddddddddddddddddd  
		///  -or-  
		///  Groups of 8, 4, 4, 4, and 12 digits with hyphens between the groups. The entire GUID can optionally be enclosed in matching braces or parentheses:  
		///  dddddddd-dddd-dddd-dddd-dddddddddddd  
		///  -or-  
		///  {dddddddd-dddd-dddd-dddd-dddddddddddd}  
		///  -or-  
		///  (dddddddd-dddd-dddd-dddd-dddddddddddd)  
		///  -or-  
		///  Groups of 8, 4, and 4 digits, and a subset of eight groups of 2 digits, with each group prefixed by "0x" or "0X", and separated by commas. The entire GUID, as well as the subset, is enclosed in matching braces:  
		///  {0xdddddddd, 0xdddd, 0xdddd,{0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd}}  
		///  All braces, commas, and "0x" prefixes are required. All embedded spaces are ignored. All leading zeros in a group are ignored.  
		///  The digits shown in a group are the maximum number of meaningful digits that can appear in that group. You can specify from 1 to the number of digits shown for a group. The specified digits are assumed to be the low-order digits of the group.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The format of <paramref name="g" /> is invalid.</exception>
		/// <exception cref="T:System.OverflowException">The format of <paramref name="g" /> is invalid.</exception>
		// Token: 0x06000B9D RID: 2973 RVA: 0x0002F97C File Offset: 0x0002DB7C
		public Guid(string g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.All);
			if (Guid.TryParseGuid(g, Guid.GuidStyles.Any, ref guidResult))
			{
				this = guidResult._parsedGuid;
				return;
			}
			throw guidResult.GetGuidParseException();
		}

		/// <summary>Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid" /> structure.</summary>
		/// <param name="input">The string to convert.</param>
		/// <returns>A structure that contains the value that was parsed.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not in a recognized format.</exception>
		// Token: 0x06000B9E RID: 2974 RVA: 0x0002F9CC File Offset: 0x0002DBCC
		public static Guid Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Guid.Parse(input);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002F9F4 File Offset: 0x0002DBF4
		public static Guid Parse(ReadOnlySpan<char> input)
		{
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.AllButOverflow);
			if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref guidResult))
			{
				return guidResult._parsedGuid;
			}
			throw guidResult.GetGuidParseException();
		}

		/// <summary>Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid" /> structure.</summary>
		/// <param name="input">The GUID to convert.</param>
		/// <param name="result">The structure that will contain the parsed value. If the method returns <see langword="true" />, <paramref name="result" /> contains a valid <see cref="T:System.Guid" />. If the method returns <see langword="false" />, <paramref name="result" /> equals <see cref="F:System.Guid.Empty" />.</param>
		/// <returns>
		///   <see langword="true" /> if the parse operation was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002FA2B File Offset: 0x0002DC2B
		public static bool TryParse(string input, out Guid result)
		{
			if (input == null)
			{
				result = default(Guid);
				return false;
			}
			return Guid.TryParse(input, out result);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002FA48 File Offset: 0x0002DC48
		public static bool TryParse(ReadOnlySpan<char> input, out Guid result)
		{
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.None);
			if (Guid.TryParseGuid(input, Guid.GuidStyles.Any, ref guidResult))
			{
				result = guidResult._parsedGuid;
				return true;
			}
			result = default(Guid);
			return false;
		}

		/// <summary>Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid" /> structure, provided that the string is in the specified format.</summary>
		/// <param name="input">The GUID to convert.</param>
		/// <param name="format">One of the following specifiers that indicates the exact format to use when interpreting <paramref name="input" />: "N", "D", "B", "P", or "X".</param>
		/// <returns>A structure that contains the value that was parsed.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not in the format specified by <paramref name="format" />.</exception>
		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002FA88 File Offset: 0x0002DC88
		public static Guid ParseExact(string input, string format)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			ReadOnlySpan<char> input2 = input;
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return Guid.ParseExact(input2, format);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002FAC4 File Offset: 0x0002DCC4
		public unsafe static Guid ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format)
		{
			if (format.Length != 1)
			{
				throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			}
			char c = (char)(*format[0]);
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_71;
					}
					if (c != 'D')
					{
						goto IL_83;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_6D;
					}
					if (c == 'P')
					{
						goto IL_76;
					}
					if (c != 'X')
					{
						goto IL_83;
					}
					goto IL_7B;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_71;
				}
				if (c != 'd')
				{
					goto IL_83;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_6D;
				}
				if (c == 'p')
				{
					goto IL_76;
				}
				if (c != 'x')
				{
					goto IL_83;
				}
				goto IL_7B;
			}
			Guid.GuidStyles flags = Guid.GuidStyles.RequireDashes;
			goto IL_8E;
			IL_6D:
			flags = Guid.GuidStyles.None;
			goto IL_8E;
			IL_71:
			flags = Guid.GuidStyles.BraceFormat;
			goto IL_8E;
			IL_76:
			flags = Guid.GuidStyles.ParenthesisFormat;
			goto IL_8E;
			IL_7B:
			flags = Guid.GuidStyles.HexFormat;
			goto IL_8E;
			IL_83:
			throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			IL_8E:
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.AllButOverflow);
			if (Guid.TryParseGuid(input, flags, ref guidResult))
			{
				return guidResult._parsedGuid;
			}
			throw guidResult.GetGuidParseException();
		}

		/// <summary>Converts the string representation of a GUID to the equivalent <see cref="T:System.Guid" /> structure, provided that the string is in the specified format.</summary>
		/// <param name="input">The GUID to convert.</param>
		/// <param name="format">One of the following specifiers that indicates the exact format to use when interpreting <paramref name="input" />: "N", "D", "B", "P", or "X".</param>
		/// <param name="result">The structure that will contain the parsed value. If the method returns <see langword="true" />, <paramref name="result" /> contains a valid <see cref="T:System.Guid" />. If the method returns <see langword="false" />, <paramref name="result" /> equals <see cref="F:System.Guid.Empty" />.</param>
		/// <returns>
		///   <see langword="true" /> if the parse operation was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002FB88 File Offset: 0x0002DD88
		public static bool TryParseExact(string input, string format, out Guid result)
		{
			if (input == null)
			{
				result = default(Guid);
				return false;
			}
			return Guid.TryParseExact(input, format, out result);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002FBA8 File Offset: 0x0002DDA8
		public unsafe static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, out Guid result)
		{
			if (format.Length != 1)
			{
				result = default(Guid);
				return false;
			}
			char c = (char)(*format[0]);
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_6F;
					}
					if (c != 'D')
					{
						goto IL_81;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_6B;
					}
					if (c == 'P')
					{
						goto IL_74;
					}
					if (c != 'X')
					{
						goto IL_81;
					}
					goto IL_79;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_6F;
				}
				if (c != 'd')
				{
					goto IL_81;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_6B;
				}
				if (c == 'p')
				{
					goto IL_74;
				}
				if (c != 'x')
				{
					goto IL_81;
				}
				goto IL_79;
			}
			Guid.GuidStyles flags = Guid.GuidStyles.RequireDashes;
			goto IL_8A;
			IL_6B:
			flags = Guid.GuidStyles.None;
			goto IL_8A;
			IL_6F:
			flags = Guid.GuidStyles.BraceFormat;
			goto IL_8A;
			IL_74:
			flags = Guid.GuidStyles.ParenthesisFormat;
			goto IL_8A;
			IL_79:
			flags = Guid.GuidStyles.HexFormat;
			goto IL_8A;
			IL_81:
			result = default(Guid);
			return false;
			IL_8A:
			Guid.GuidResult guidResult = default(Guid.GuidResult);
			guidResult.Init(Guid.GuidParseThrowStyle.None);
			if (Guid.TryParseGuid(input, flags, ref guidResult))
			{
				result = guidResult._parsedGuid;
				return true;
			}
			result = default(Guid);
			return false;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002FC70 File Offset: 0x0002DE70
		private static bool TryParseGuid(ReadOnlySpan<char> guidString, Guid.GuidStyles flags, ref Guid.GuidResult result)
		{
			guidString = guidString.Trim();
			if (guidString.Length == 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			bool flag = guidString.IndexOf('-') >= 0;
			if (flag)
			{
				if ((flags & (Guid.GuidStyles.AllowDashes | Guid.GuidStyles.RequireDashes)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireDashes) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			bool flag2 = guidString.IndexOf('{') >= 0;
			if (flag2)
			{
				if ((flags & (Guid.GuidStyles.AllowBraces | Guid.GuidStyles.RequireBraces)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireBraces) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			if (guidString.IndexOf('(') >= 0)
			{
				if ((flags & (Guid.GuidStyles.AllowParenthesis | Guid.GuidStyles.RequireParenthesis)) == Guid.GuidStyles.None)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
					return false;
				}
			}
			else if ((flags & Guid.GuidStyles.RequireParenthesis) != Guid.GuidStyles.None)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Unrecognized Guid format.");
				return false;
			}
			bool result2;
			try
			{
				if (flag)
				{
					result2 = Guid.TryParseGuidWithDashes(guidString, ref result);
				}
				else if (flag2)
				{
					result2 = Guid.TryParseGuidWithHexPrefix(guidString, ref result);
				}
				else
				{
					result2 = Guid.TryParseGuidWithNoStyle(guidString, ref result);
				}
			}
			catch (IndexOutOfRangeException innerException)
			{
				result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Unrecognized Guid format.", null, null, innerException);
				result2 = false;
			}
			catch (ArgumentException innerException2)
			{
				result.SetFailure(Guid.ParseFailureKind.FormatWithInnerException, "Unrecognized Guid format.", null, null, innerException2);
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002FDB4 File Offset: 0x0002DFB4
		private unsafe static bool TryParseGuidWithHexPrefix(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			guidString = Guid.EatAllWhitespace(guidString);
			if (guidString.Length == 0 || *guidString[0] != 123)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected {0xdddddddd, etc}.");
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{0xdddddddd, etc}");
				return false;
			}
			int num = 3;
			int num2 = guidString.Slice(num).IndexOf(',');
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
				return false;
			}
			if (!Guid.StringToInt(guidString.Slice(num, num2), -1, 4096, out result._parsedGuid._a, ref result))
			{
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{0xdddddddd, 0xdddd, etc}");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.Slice(num).IndexOf(',');
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
				return false;
			}
			if (!Guid.StringToShort(guidString.Slice(num, num2), -1, 4096, out result._parsedGuid._b, ref result))
			{
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{0xdddddddd, 0xdddd, 0xdddd, etc}");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.Slice(num).IndexOf(',');
			if (num2 <= 0)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
				return false;
			}
			if (!Guid.StringToShort(guidString.Slice(num, num2), -1, 4096, out result._parsedGuid._c, ref result))
			{
				return false;
			}
			if (guidString.Length <= num + num2 + 1 || *guidString[num + num2 + 1] != 123)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Expected {0xdddddddd, etc}.");
				return false;
			}
			num2++;
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)8], 8);
			for (int i = 0; i < span.Length; i++)
			{
				if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Expected hex 0x in '{0}'.", "{... { ... 0xdd, ...}}");
					return false;
				}
				num = num + num2 + 3;
				if (i < 7)
				{
					num2 = guidString.Slice(num).IndexOf(',');
					if (num2 <= 0)
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
						return false;
					}
				}
				else
				{
					num2 = guidString.Slice(num).IndexOf('}');
					if (num2 <= 0)
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Could not find a brace, or the length between the previous token and the brace was zero (i.e., '0x,'etc.).");
						return false;
					}
				}
				int num3;
				if (!Guid.StringToInt(guidString.Slice(num, num2), -1, 4096, out num3, ref result))
				{
					return false;
				}
				uint num4 = (uint)num3;
				if (num4 > 255U)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Value was either too large or too small for an unsigned byte.");
					return false;
				}
				*span[i] = (byte)num4;
			}
			result._parsedGuid._d = *span[0];
			result._parsedGuid._e = *span[1];
			result._parsedGuid._f = *span[2];
			result._parsedGuid._g = *span[3];
			result._parsedGuid._h = *span[4];
			result._parsedGuid._i = *span[5];
			result._parsedGuid._j = *span[6];
			result._parsedGuid._k = *span[7];
			if (num + num2 + 1 >= guidString.Length || *guidString[num + num2 + 1] != 125)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Could not find the ending brace.");
				return false;
			}
			if (num + num2 + 1 != guidString.Length - 1)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Additional non-parsable characters are at the end of the string.");
				return false;
			}
			return true;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00030120 File Offset: 0x0002E320
		private unsafe static bool TryParseGuidWithNoStyle(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			int num = 0;
			int num2 = 0;
			if (guidString.Length != 32)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			for (int i = 0; i < guidString.Length; i++)
			{
				char c = (char)(*guidString[i]);
				if (c < '0' || c > '9')
				{
					char c2 = char.ToUpperInvariant(c);
					if (c2 < 'A' || c2 > 'F')
					{
						result.SetFailure(Guid.ParseFailureKind.Format, "Guid string should only contain hexadecimal characters.");
						return false;
					}
				}
			}
			if (!Guid.StringToInt(guidString.Slice(num, 8), -1, 4096, out result._parsedGuid._a, ref result))
			{
				return false;
			}
			num += 8;
			if (!Guid.StringToShort(guidString.Slice(num, 4), -1, 4096, out result._parsedGuid._b, ref result))
			{
				return false;
			}
			num += 4;
			if (!Guid.StringToShort(guidString.Slice(num, 4), -1, 4096, out result._parsedGuid._c, ref result))
			{
				return false;
			}
			num += 4;
			int num3;
			if (!Guid.StringToInt(guidString.Slice(num, 4), -1, 4096, out num3, ref result))
			{
				return false;
			}
			num += 4;
			num2 = num;
			long num4;
			if (!Guid.StringToLong(guidString, ref num2, 8192, out num4, ref result))
			{
				return false;
			}
			if (num2 - num != 12)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			result._parsedGuid._d = (byte)(num3 >> 8);
			result._parsedGuid._e = (byte)num3;
			num3 = (int)(num4 >> 32);
			result._parsedGuid._f = (byte)(num3 >> 8);
			result._parsedGuid._g = (byte)num3;
			num3 = (int)num4;
			result._parsedGuid._h = (byte)(num3 >> 24);
			result._parsedGuid._i = (byte)(num3 >> 16);
			result._parsedGuid._j = (byte)(num3 >> 8);
			result._parsedGuid._k = (byte)num3;
			return true;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000302DC File Offset: 0x0002E4DC
		private unsafe static bool TryParseGuidWithDashes(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			int num = 0;
			int num2 = 0;
			if (*guidString[0] == 123)
			{
				if (guidString.Length != 38 || *guidString[37] != 125)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
					return false;
				}
				num = 1;
			}
			else if (*guidString[0] == 40)
			{
				if (guidString.Length != 38 || *guidString[37] != 41)
				{
					result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
					return false;
				}
				num = 1;
			}
			else if (guidString.Length != 36)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			if (*guidString[8 + num] != 45 || *guidString[13 + num] != 45 || *guidString[18 + num] != 45 || *guidString[23 + num] != 45)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Dashes are in the wrong position for GUID parsing.");
				return false;
			}
			num2 = num;
			int num3;
			if (!Guid.StringToInt(guidString, ref num2, 8, 8192, out num3, ref result))
			{
				return false;
			}
			result._parsedGuid._a = num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			result._parsedGuid._b = (short)num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			result._parsedGuid._c = (short)num3;
			num2++;
			if (!Guid.StringToInt(guidString, ref num2, 4, 8192, out num3, ref result))
			{
				return false;
			}
			num2++;
			num = num2;
			long num4;
			if (!Guid.StringToLong(guidString, ref num2, 8192, out num4, ref result))
			{
				return false;
			}
			if (num2 - num != 12)
			{
				result.SetFailure(Guid.ParseFailureKind.Format, "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
				return false;
			}
			result._parsedGuid._d = (byte)(num3 >> 8);
			result._parsedGuid._e = (byte)num3;
			num3 = (int)(num4 >> 32);
			result._parsedGuid._f = (byte)(num3 >> 8);
			result._parsedGuid._g = (byte)num3;
			num3 = (int)num4;
			result._parsedGuid._h = (byte)(num3 >> 24);
			result._parsedGuid._i = (byte)(num3 >> 16);
			result._parsedGuid._j = (byte)(num3 >> 8);
			result._parsedGuid._k = (byte)num3;
			return true;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000304F8 File Offset: 0x0002E6F8
		private static bool StringToShort(ReadOnlySpan<char> str, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			int num = 0;
			return Guid.StringToShort(str, ref num, requiredLength, flags, out result, ref parseResult);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00030514 File Offset: 0x0002E714
		private static bool StringToShort(ReadOnlySpan<char> str, ref int parsePos, int requiredLength, int flags, out short result, ref Guid.GuidResult parseResult)
		{
			result = 0;
			int num;
			bool result2 = Guid.StringToInt(str, ref parsePos, requiredLength, flags, out num, ref parseResult);
			result = (short)num;
			return result2;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00030538 File Offset: 0x0002E738
		private static bool StringToInt(ReadOnlySpan<char> str, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			int num = 0;
			return Guid.StringToInt(str, ref num, requiredLength, flags, out result, ref parseResult);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00030554 File Offset: 0x0002E754
		private static bool StringToInt(ReadOnlySpan<char> str, ref int parsePos, int requiredLength, int flags, out int result, ref Guid.GuidResult parseResult)
		{
			result = 0;
			int num = parsePos;
			try
			{
				result = ParseNumbers.StringToInt(str, 16, flags, ref parsePos);
			}
			catch (OverflowException ex)
			{
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.All)
				{
					throw;
				}
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
				{
					throw new FormatException("Unrecognized Guid format.", ex);
				}
				parseResult.SetFailure(ex);
				return false;
			}
			catch (Exception failure)
			{
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.None)
				{
					parseResult.SetFailure(failure);
					return false;
				}
				throw;
			}
			if (requiredLength != -1 && parsePos - num != requiredLength)
			{
				parseResult.SetFailure(Guid.ParseFailureKind.Format, "Guid string should only contain hexadecimal characters.");
				return false;
			}
			return true;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x000305F8 File Offset: 0x0002E7F8
		private static bool StringToLong(ReadOnlySpan<char> str, ref int parsePos, int flags, out long result, ref Guid.GuidResult parseResult)
		{
			result = 0L;
			try
			{
				result = ParseNumbers.StringToLong(str, 16, flags, ref parsePos);
			}
			catch (OverflowException ex)
			{
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.All)
				{
					throw;
				}
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.AllButOverflow)
				{
					throw new FormatException("Unrecognized Guid format.", ex);
				}
				parseResult.SetFailure(ex);
				return false;
			}
			catch (Exception failure)
			{
				if (parseResult._throwStyle == Guid.GuidParseThrowStyle.None)
				{
					parseResult.SetFailure(failure);
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00030680 File Offset: 0x0002E880
		private unsafe static ReadOnlySpan<char> EatAllWhitespace(ReadOnlySpan<char> str)
		{
			int i = 0;
			while (i < str.Length && !char.IsWhiteSpace((char)(*str[i])))
			{
				i++;
			}
			if (i == str.Length)
			{
				return str;
			}
			char[] array = new char[str.Length];
			int length = 0;
			if (i > 0)
			{
				length = i;
				str.Slice(0, i).CopyTo(array);
			}
			while (i < str.Length)
			{
				char c = (char)(*str[i]);
				if (!char.IsWhiteSpace(c))
				{
					array[length++] = c;
				}
				i++;
			}
			return new ReadOnlySpan<char>(array, 0, length);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0003071C File Offset: 0x0002E91C
		private unsafe static bool IsHexPrefix(ReadOnlySpan<char> str, int i)
		{
			return i + 1 < str.Length && *str[i] == 48 && (*str[i + 1] == 120 || char.ToLowerInvariant((char)(*str[i + 1])) == 'x');
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0003076C File Offset: 0x0002E96C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteByteHelper(Span<byte> destination)
		{
			*destination[0] = (byte)this._a;
			*destination[1] = (byte)(this._a >> 8);
			*destination[2] = (byte)(this._a >> 16);
			*destination[3] = (byte)(this._a >> 24);
			*destination[4] = (byte)this._b;
			*destination[5] = (byte)(this._b >> 8);
			*destination[6] = (byte)this._c;
			*destination[7] = (byte)(this._c >> 8);
			*destination[8] = this._d;
			*destination[9] = this._e;
			*destination[10] = this._f;
			*destination[11] = this._g;
			*destination[12] = this._h;
			*destination[13] = this._i;
			*destination[14] = this._j;
			*destination[15] = this._k;
		}

		/// <summary>Returns a 16-element byte array that contains the value of this instance.</summary>
		/// <returns>A 16-element byte array.</returns>
		// Token: 0x06000BB2 RID: 2994 RVA: 0x00030884 File Offset: 0x0002EA84
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			this.WriteByteHelper(array);
			return array;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000308A6 File Offset: 0x0002EAA6
		public bool TryWriteBytes(Span<byte> destination)
		{
			if (destination.Length < 16)
			{
				return false;
			}
			this.WriteByteHelper(destination);
			return true;
		}

		/// <summary>Returns a string representation of the value of this instance in registry format.</summary>
		/// <returns>The value of this <see cref="T:System.Guid" />, formatted by using the "D" format specifier as follows:  
		///  xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx  
		///  where the value of the GUID is represented as a series of lowercase hexadecimal digits in groups of 8, 4, 4, 4, and 12 digits and separated by hyphens. An example of a return value is "382c74c3-721d-4f34-80e5-57657b6cbc27". To convert the hexadecimal digits from a through f to uppercase, call the <see cref="M:System.String.ToUpper" /> method on the returned string.</returns>
		// Token: 0x06000BB4 RID: 2996 RVA: 0x000308BD File Offset: 0x0002EABD
		public override string ToString()
		{
			return this.ToString("D", null);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x06000BB5 RID: 2997 RVA: 0x000308CB File Offset: 0x0002EACB
		public unsafe override int GetHashCode()
		{
			return this._a ^ *Unsafe.Add<int>(ref this._a, 1) ^ *Unsafe.Add<int>(ref this._a, 2) ^ *Unsafe.Add<int>(ref this._a, 3);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="o">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Guid" /> that has the same value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BB6 RID: 2998 RVA: 0x00030900 File Offset: 0x0002EB00
		public unsafe override bool Equals(object o)
		{
			if (o == null || !(o is Guid))
			{
				return false;
			}
			Guid guid = (Guid)o;
			return guid._a == this._a && *Unsafe.Add<int>(ref guid._a, 1) == *Unsafe.Add<int>(ref this._a, 1) && *Unsafe.Add<int>(ref guid._a, 2) == *Unsafe.Add<int>(ref this._a, 2) && *Unsafe.Add<int>(ref guid._a, 3) == *Unsafe.Add<int>(ref this._a, 3);
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Guid" /> object represent the same value.</summary>
		/// <param name="g">An object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="g" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BB7 RID: 2999 RVA: 0x00030988 File Offset: 0x0002EB88
		public unsafe bool Equals(Guid g)
		{
			return g._a == this._a && *Unsafe.Add<int>(ref g._a, 1) == *Unsafe.Add<int>(ref this._a, 1) && *Unsafe.Add<int>(ref g._a, 2) == *Unsafe.Add<int>(ref this._a, 2) && *Unsafe.Add<int>(ref g._a, 3) == *Unsafe.Add<int>(ref this._a, 3);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000309FC File Offset: 0x0002EBFC
		private int GetResult(uint me, uint them)
		{
			if (me < them)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <param name="value">An object to compare, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Return value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   A positive integer  
		///
		///   This instance is greater than <paramref name="value" />, or <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Guid" />.</exception>
		// Token: 0x06000BB9 RID: 3001 RVA: 0x00030A08 File Offset: 0x0002EC08
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is Guid))
			{
				throw new ArgumentException("Object must be of type GUID.", "value");
			}
			Guid guid = (Guid)value;
			if (guid._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)guid._a);
			}
			if (guid._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)guid._b);
			}
			if (guid._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)guid._c);
			}
			if (guid._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)guid._d);
			}
			if (guid._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)guid._e);
			}
			if (guid._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)guid._f);
			}
			if (guid._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)guid._g);
			}
			if (guid._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)guid._h);
			}
			if (guid._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)guid._i);
			}
			if (guid._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)guid._j);
			}
			if (guid._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)guid._k);
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Guid" /> object and returns an indication of their relative values.</summary>
		/// <param name="value">An object to compare to this instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Return value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   A positive integer  
		///
		///   This instance is greater than <paramref name="value" />.</returns>
		// Token: 0x06000BBA RID: 3002 RVA: 0x00030BA8 File Offset: 0x0002EDA8
		public int CompareTo(Guid value)
		{
			if (value._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)value._a);
			}
			if (value._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)value._b);
			}
			if (value._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)value._c);
			}
			if (value._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)value._d);
			}
			if (value._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)value._e);
			}
			if (value._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)value._f);
			}
			if (value._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)value._g);
			}
			if (value._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)value._h);
			}
			if (value._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)value._i);
			}
			if (value._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)value._j);
			}
			if (value._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)value._k);
			}
			return 0;
		}

		/// <summary>Indicates whether the values of two specified <see cref="T:System.Guid" /> objects are equal.</summary>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> and <paramref name="b" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BBB RID: 3003 RVA: 0x00030D24 File Offset: 0x0002EF24
		public unsafe static bool operator ==(Guid a, Guid b)
		{
			return a._a == b._a && *Unsafe.Add<int>(ref a._a, 1) == *Unsafe.Add<int>(ref b._a, 1) && *Unsafe.Add<int>(ref a._a, 2) == *Unsafe.Add<int>(ref b._a, 2) && *Unsafe.Add<int>(ref a._a, 3) == *Unsafe.Add<int>(ref b._a, 3);
		}

		/// <summary>Indicates whether the values of two specified <see cref="T:System.Guid" /> objects are not equal.</summary>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000BBC RID: 3004 RVA: 0x00030D9C File Offset: 0x0002EF9C
		public unsafe static bool operator !=(Guid a, Guid b)
		{
			return a._a != b._a || *Unsafe.Add<int>(ref a._a, 1) != *Unsafe.Add<int>(ref b._a, 1) || *Unsafe.Add<int>(ref a._a, 2) != *Unsafe.Add<int>(ref b._a, 2) || *Unsafe.Add<int>(ref a._a, 3) != *Unsafe.Add<int>(ref b._a, 3);
		}

		/// <summary>Returns a string representation of the value of this <see cref="T:System.Guid" /> instance, according to the provided format specifier.</summary>
		/// <param name="format">A single format specifier that indicates how to format the value of this <see cref="T:System.Guid" />. The <paramref name="format" /> parameter can be "N", "D", "B", "P", or "X". If <paramref name="format" /> is <see langword="null" /> or an empty string (""), "D" is used.</param>
		/// <returns>The value of this <see cref="T:System.Guid" />, represented as a series of lowercase hexadecimal digits in the specified format.</returns>
		/// <exception cref="T:System.FormatException">The value of <paramref name="format" /> is not <see langword="null" />, an empty string (""), "N", "D", "B", "P", or "X".</exception>
		// Token: 0x06000BBD RID: 3005 RVA: 0x00030E16 File Offset: 0x0002F016
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00030E20 File Offset: 0x0002F020
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static char HexToChar(int a)
		{
			a &= 15;
			return (char)((a > 9) ? (a - 10 + 97) : (a + 48));
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00030E3B File Offset: 0x0002F03B
		private unsafe static int HexsToChars(char* guidChars, int a, int b)
		{
			*guidChars = Guid.HexToChar(a >> 4);
			guidChars[1] = Guid.HexToChar(a);
			guidChars[2] = Guid.HexToChar(b >> 4);
			guidChars[3] = Guid.HexToChar(b);
			return 4;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00030E70 File Offset: 0x0002F070
		private unsafe static int HexsToCharsHexOutput(char* guidChars, int a, int b)
		{
			*guidChars = '0';
			guidChars[1] = 'x';
			guidChars[2] = Guid.HexToChar(a >> 4);
			guidChars[3] = Guid.HexToChar(a);
			guidChars[4] = ',';
			guidChars[5] = '0';
			guidChars[6] = 'x';
			guidChars[7] = Guid.HexToChar(b >> 4);
			guidChars[8] = Guid.HexToChar(b);
			return 9;
		}

		/// <summary>Returns a string representation of the value of this instance of the <see cref="T:System.Guid" /> class, according to the provided format specifier and culture-specific format information.</summary>
		/// <param name="format">A single format specifier that indicates how to format the value of this <see cref="T:System.Guid" />. The <paramref name="format" /> parameter can be "N", "D", "B", "P", or "X". If <paramref name="format" /> is <see langword="null" /> or an empty string (""), "D" is used.</param>
		/// <param name="provider">(Reserved) An object that supplies culture-specific formatting information.</param>
		/// <returns>The value of this <see cref="T:System.Guid" />, represented as a series of lowercase hexadecimal digits in the specified format.</returns>
		/// <exception cref="T:System.FormatException">The value of <paramref name="format" /> is not <see langword="null" />, an empty string (""), "N", "D", "B", "P", or "X".</exception>
		// Token: 0x06000BC1 RID: 3009 RVA: 0x00030EDC File Offset: 0x0002F0DC
		[SecuritySafeCritical]
		public unsafe string ToString(string format, IFormatProvider provider)
		{
			if (format == null || format.Length == 0)
			{
				format = "D";
			}
			if (format.Length != 1)
			{
				throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			}
			char c = format[0];
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_81;
					}
					if (c != 'D')
					{
						goto IL_8B;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_7C;
					}
					if (c == 'P')
					{
						goto IL_81;
					}
					if (c != 'X')
					{
						goto IL_8B;
					}
					goto IL_86;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_81;
				}
				if (c != 'd')
				{
					goto IL_8B;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_7C;
				}
				if (c == 'p')
				{
					goto IL_81;
				}
				if (c != 'x')
				{
					goto IL_8B;
				}
				goto IL_86;
			}
			int length = 36;
			goto IL_96;
			IL_7C:
			length = 32;
			goto IL_96;
			IL_81:
			length = 38;
			goto IL_96;
			IL_86:
			length = 68;
			goto IL_96;
			IL_8B:
			throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			IL_96:
			string text = string.FastAllocateString(length);
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num;
				this.TryFormat(new Span<char>((void*)ptr, text.Length), out num, format);
			}
			return text;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00030FBC File Offset: 0x0002F1BC
		public unsafe bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>))
		{
			if (format.Length == 0)
			{
				format = "D";
			}
			if (format.Length != 1)
			{
				throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			}
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			char c = (char)(*format[0]);
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_9D;
					}
					if (c != 'D')
					{
						goto IL_C2;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_96;
					}
					if (c == 'P')
					{
						goto IL_A8;
					}
					if (c != 'X')
					{
						goto IL_C2;
					}
					goto IL_B3;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_9D;
				}
				if (c != 'd')
				{
					goto IL_C2;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_96;
				}
				if (c == 'p')
				{
					goto IL_A8;
				}
				if (c != 'x')
				{
					goto IL_C2;
				}
				goto IL_B3;
			}
			int num2 = 36;
			goto IL_CD;
			IL_96:
			flag = false;
			num2 = 32;
			goto IL_CD;
			IL_9D:
			num = 8192123;
			num2 = 38;
			goto IL_CD;
			IL_A8:
			num = 2687016;
			num2 = 38;
			goto IL_CD;
			IL_B3:
			num = 8192123;
			flag = false;
			flag2 = true;
			num2 = 68;
			goto IL_CD;
			IL_C2:
			throw new FormatException("Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.");
			IL_CD:
			if (destination.Length < num2)
			{
				charsWritten = 0;
				return false;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				if (num != 0)
				{
					*(ptr++) = (char)num;
				}
				if (flag2)
				{
					*(ptr++) = '0';
					*(ptr++) = 'x';
					ptr += Guid.HexsToChars(ptr, this._a >> 24, this._a >> 16);
					ptr += Guid.HexsToChars(ptr, this._a >> 8, this._a);
					*(ptr++) = ',';
					*(ptr++) = '0';
					*(ptr++) = 'x';
					ptr += Guid.HexsToChars(ptr, this._b >> 8, (int)this._b);
					*(ptr++) = ',';
					*(ptr++) = '0';
					*(ptr++) = 'x';
					ptr += Guid.HexsToChars(ptr, this._c >> 8, (int)this._c);
					*(ptr++) = ',';
					*(ptr++) = '{';
					ptr += Guid.HexsToCharsHexOutput(ptr, (int)this._d, (int)this._e);
					*(ptr++) = ',';
					ptr += Guid.HexsToCharsHexOutput(ptr, (int)this._f, (int)this._g);
					*(ptr++) = ',';
					ptr += Guid.HexsToCharsHexOutput(ptr, (int)this._h, (int)this._i);
					*(ptr++) = ',';
					ptr += Guid.HexsToCharsHexOutput(ptr, (int)this._j, (int)this._k);
					*(ptr++) = '}';
				}
				else
				{
					ptr += Guid.HexsToChars(ptr, this._a >> 24, this._a >> 16);
					ptr += Guid.HexsToChars(ptr, this._a >> 8, this._a);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += Guid.HexsToChars(ptr, this._b >> 8, (int)this._b);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += Guid.HexsToChars(ptr, this._c >> 8, (int)this._c);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += Guid.HexsToChars(ptr, (int)this._d, (int)this._e);
					if (flag)
					{
						*(ptr++) = '-';
					}
					ptr += Guid.HexsToChars(ptr, (int)this._f, (int)this._g);
					ptr += Guid.HexsToChars(ptr, (int)this._h, (int)this._i);
					ptr += Guid.HexsToChars(ptr, (int)this._j, (int)this._k);
				}
				if (num != 0)
				{
					*(ptr++) = (char)(num >> 16);
				}
			}
			charsWritten = num2;
			return true;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0003136A File Offset: 0x0002F56A
		bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
		{
			return this.TryFormat(destination, out charsWritten, format);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00031378 File Offset: 0x0002F578
		internal unsafe static byte[] FastNewGuidArray()
		{
			byte[] array = new byte[16];
			byte[] array2;
			byte* buffer;
			if ((array2 = array) == null || array2.Length == 0)
			{
				buffer = null;
			}
			else
			{
				buffer = &array2[0];
			}
			Interop.GetRandomBytes(buffer, 16);
			array2 = null;
			array[8] = ((array[8] & 63) | 128);
			array[7] = ((array[7] & 15) | 64);
			return array;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00004BF9 File Offset: 0x00002DF9
		// Note: this type is marked as 'beforefieldinit'.
		static Guid()
		{
		}

		/// <summary>A read-only instance of the <see cref="T:System.Guid" /> structure whose value is all zeros.</summary>
		// Token: 0x040011B3 RID: 4531
		public static readonly Guid Empty;

		// Token: 0x040011B4 RID: 4532
		private int _a;

		// Token: 0x040011B5 RID: 4533
		private short _b;

		// Token: 0x040011B6 RID: 4534
		private short _c;

		// Token: 0x040011B7 RID: 4535
		private byte _d;

		// Token: 0x040011B8 RID: 4536
		private byte _e;

		// Token: 0x040011B9 RID: 4537
		private byte _f;

		// Token: 0x040011BA RID: 4538
		private byte _g;

		// Token: 0x040011BB RID: 4539
		private byte _h;

		// Token: 0x040011BC RID: 4540
		private byte _i;

		// Token: 0x040011BD RID: 4541
		private byte _j;

		// Token: 0x040011BE RID: 4542
		private byte _k;

		// Token: 0x02000130 RID: 304
		[Flags]
		private enum GuidStyles
		{
			// Token: 0x040011C0 RID: 4544
			None = 0,
			// Token: 0x040011C1 RID: 4545
			AllowParenthesis = 1,
			// Token: 0x040011C2 RID: 4546
			AllowBraces = 2,
			// Token: 0x040011C3 RID: 4547
			AllowDashes = 4,
			// Token: 0x040011C4 RID: 4548
			AllowHexPrefix = 8,
			// Token: 0x040011C5 RID: 4549
			RequireParenthesis = 16,
			// Token: 0x040011C6 RID: 4550
			RequireBraces = 32,
			// Token: 0x040011C7 RID: 4551
			RequireDashes = 64,
			// Token: 0x040011C8 RID: 4552
			RequireHexPrefix = 128,
			// Token: 0x040011C9 RID: 4553
			HexFormat = 160,
			// Token: 0x040011CA RID: 4554
			NumberFormat = 0,
			// Token: 0x040011CB RID: 4555
			DigitFormat = 64,
			// Token: 0x040011CC RID: 4556
			BraceFormat = 96,
			// Token: 0x040011CD RID: 4557
			ParenthesisFormat = 80,
			// Token: 0x040011CE RID: 4558
			Any = 15
		}

		// Token: 0x02000131 RID: 305
		private enum GuidParseThrowStyle
		{
			// Token: 0x040011D0 RID: 4560
			None,
			// Token: 0x040011D1 RID: 4561
			All,
			// Token: 0x040011D2 RID: 4562
			AllButOverflow
		}

		// Token: 0x02000132 RID: 306
		private enum ParseFailureKind
		{
			// Token: 0x040011D4 RID: 4564
			None,
			// Token: 0x040011D5 RID: 4565
			ArgumentNull,
			// Token: 0x040011D6 RID: 4566
			Format,
			// Token: 0x040011D7 RID: 4567
			FormatWithParameter,
			// Token: 0x040011D8 RID: 4568
			NativeException,
			// Token: 0x040011D9 RID: 4569
			FormatWithInnerException
		}

		// Token: 0x02000133 RID: 307
		private struct GuidResult
		{
			// Token: 0x06000BC6 RID: 3014 RVA: 0x000313CD File Offset: 0x0002F5CD
			internal void Init(Guid.GuidParseThrowStyle canThrow)
			{
				this._throwStyle = canThrow;
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x000313D6 File Offset: 0x0002F5D6
			internal void SetFailure(Exception nativeException)
			{
				this._failure = Guid.ParseFailureKind.NativeException;
				this._innerException = nativeException;
			}

			// Token: 0x06000BC8 RID: 3016 RVA: 0x000313E6 File Offset: 0x0002F5E6
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID)
			{
				this.SetFailure(failure, failureMessageID, null, null, null);
			}

			// Token: 0x06000BC9 RID: 3017 RVA: 0x000313F3 File Offset: 0x0002F5F3
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, null, null);
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x00031400 File Offset: 0x0002F600
			internal void SetFailure(Guid.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName, Exception innerException)
			{
				this._failure = failure;
				this._failureMessageID = failureMessageID;
				this._failureMessageFormatArgument = failureMessageFormatArgument;
				this._failureArgumentName = failureArgumentName;
				this._innerException = innerException;
				if (this._throwStyle != Guid.GuidParseThrowStyle.None)
				{
					throw this.GetGuidParseException();
				}
			}

			// Token: 0x06000BCB RID: 3019 RVA: 0x00031438 File Offset: 0x0002F638
			internal Exception GetGuidParseException()
			{
				switch (this._failure)
				{
				case Guid.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this._failureArgumentName, SR.GetResourceString(this._failureMessageID));
				case Guid.ParseFailureKind.Format:
					return new FormatException(SR.GetResourceString(this._failureMessageID));
				case Guid.ParseFailureKind.FormatWithParameter:
					return new FormatException(SR.Format(SR.GetResourceString(this._failureMessageID), this._failureMessageFormatArgument));
				case Guid.ParseFailureKind.NativeException:
					return this._innerException;
				case Guid.ParseFailureKind.FormatWithInnerException:
					return new FormatException(SR.GetResourceString(this._failureMessageID), this._innerException);
				default:
					return new FormatException("Unrecognized Guid format.");
				}
			}

			// Token: 0x040011DA RID: 4570
			internal Guid _parsedGuid;

			// Token: 0x040011DB RID: 4571
			internal Guid.GuidParseThrowStyle _throwStyle;

			// Token: 0x040011DC RID: 4572
			private Guid.ParseFailureKind _failure;

			// Token: 0x040011DD RID: 4573
			private string _failureMessageID;

			// Token: 0x040011DE RID: 4574
			private object _failureMessageFormatArgument;

			// Token: 0x040011DF RID: 4575
			private string _failureArgumentName;

			// Token: 0x040011E0 RID: 4576
			private Exception _innerException;
		}
	}
}
