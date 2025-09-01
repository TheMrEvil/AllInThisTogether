using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Encodes and decodes XML names, and provides methods for converting between common language runtime types and XML Schema definition language (XSD) types. When converting data types, the values returned are locale-independent.</summary>
	// Token: 0x02000228 RID: 552
	public class XmlConvert
	{
		/// <summary>Converts the name to a valid XML name.</summary>
		/// <param name="name">A name to be translated. </param>
		/// <returns>Returns the name with any invalid characters replaced by an escape string.</returns>
		// Token: 0x06001474 RID: 5236 RVA: 0x00080CC6 File Offset: 0x0007EEC6
		public static string EncodeName(string name)
		{
			return XmlConvert.EncodeName(name, true, false);
		}

		/// <summary>Verifies the name is valid according to the XML specification.</summary>
		/// <param name="name">The name to be encoded. </param>
		/// <returns>The encoded name.</returns>
		// Token: 0x06001475 RID: 5237 RVA: 0x00080CD0 File Offset: 0x0007EED0
		public static string EncodeNmToken(string name)
		{
			return XmlConvert.EncodeName(name, false, false);
		}

		/// <summary>Converts the name to a valid XML local name.</summary>
		/// <param name="name">The name to be encoded. </param>
		/// <returns>The encoded name.</returns>
		// Token: 0x06001476 RID: 5238 RVA: 0x00080CDA File Offset: 0x0007EEDA
		public static string EncodeLocalName(string name)
		{
			return XmlConvert.EncodeName(name, true, true);
		}

		/// <summary>Decodes a name. This method does the reverse of the <see cref="M:System.Xml.XmlConvert.EncodeName(System.String)" /> and <see cref="M:System.Xml.XmlConvert.EncodeLocalName(System.String)" /> methods.</summary>
		/// <param name="name">The name to be transformed. </param>
		/// <returns>The decoded name.</returns>
		// Token: 0x06001477 RID: 5239 RVA: 0x00080CE4 File Offset: 0x0007EEE4
		public static string DecodeName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			StringBuilder stringBuilder = null;
			int length = name.Length;
			int num = 0;
			int num2 = name.IndexOf('_');
			if (num2 < 0)
			{
				return name;
			}
			if (XmlConvert.c_DecodeCharPattern == null)
			{
				XmlConvert.c_DecodeCharPattern = new Regex("_[Xx]([0-9a-fA-F]{4}|[0-9a-fA-F]{8})_");
			}
			IEnumerator enumerator = XmlConvert.c_DecodeCharPattern.Matches(name, num2).GetEnumerator();
			int num3 = -1;
			if (enumerator != null && enumerator.MoveNext())
			{
				num3 = ((Match)enumerator.Current).Index;
			}
			for (int i = 0; i < length - XmlConvert.c_EncodedCharLength + 1; i++)
			{
				if (i == num3)
				{
					if (enumerator.MoveNext())
					{
						num3 = ((Match)enumerator.Current).Index;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 20);
					}
					stringBuilder.Append(name, num, i - num);
					if (name[i + 6] != '_')
					{
						int num4 = XmlConvert.FromHex(name[i + 2]) * 268435456 + XmlConvert.FromHex(name[i + 3]) * 16777216 + XmlConvert.FromHex(name[i + 4]) * 1048576 + XmlConvert.FromHex(name[i + 5]) * 65536 + XmlConvert.FromHex(name[i + 6]) * 4096 + XmlConvert.FromHex(name[i + 7]) * 256 + XmlConvert.FromHex(name[i + 8]) * 16 + XmlConvert.FromHex(name[i + 9]);
						if (num4 >= 65536)
						{
							if (num4 <= 1114111)
							{
								num = i + XmlConvert.c_EncodedCharLength + 4;
								char value;
								char value2;
								XmlCharType.SplitSurrogateChar(num4, out value, out value2);
								stringBuilder.Append(value2);
								stringBuilder.Append(value);
							}
						}
						else
						{
							num = i + XmlConvert.c_EncodedCharLength + 4;
							stringBuilder.Append((char)num4);
						}
						i += XmlConvert.c_EncodedCharLength - 1 + 4;
					}
					else
					{
						num = i + XmlConvert.c_EncodedCharLength;
						stringBuilder.Append((char)(XmlConvert.FromHex(name[i + 2]) * 4096 + XmlConvert.FromHex(name[i + 3]) * 256 + XmlConvert.FromHex(name[i + 4]) * 16 + XmlConvert.FromHex(name[i + 5])));
						i += XmlConvert.c_EncodedCharLength - 1;
					}
				}
			}
			if (num == 0)
			{
				return name;
			}
			if (num < length)
			{
				stringBuilder.Append(name, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00080F70 File Offset: 0x0007F170
		private static string EncodeName(string name, bool first, bool local)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			StringBuilder stringBuilder = null;
			int length = name.Length;
			int num = 0;
			int i = 0;
			int num2 = name.IndexOf('_');
			IEnumerator enumerator = null;
			if (num2 >= 0)
			{
				if (XmlConvert.c_EncodeCharPattern == null)
				{
					XmlConvert.c_EncodeCharPattern = new Regex("(?<=_)[Xx]([0-9a-fA-F]{4}|[0-9a-fA-F]{8})_");
				}
				enumerator = XmlConvert.c_EncodeCharPattern.Matches(name, num2).GetEnumerator();
			}
			int num3 = -1;
			if (enumerator != null && enumerator.MoveNext())
			{
				num3 = ((Match)enumerator.Current).Index - 1;
			}
			if (first && ((!XmlConvert.xmlCharType.IsStartNCNameCharXml4e(name[0]) && (local || (!local && name[0] != ':'))) || num3 == 0))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(length + 20);
				}
				stringBuilder.Append("_x");
				if (length > 1 && XmlCharType.IsHighSurrogate((int)name[0]) && XmlCharType.IsLowSurrogate((int)name[1]))
				{
					int highChar = (int)name[0];
					stringBuilder.Append(XmlCharType.CombineSurrogateChar((int)name[1], highChar).ToString("X8", CultureInfo.InvariantCulture));
					i++;
					num = 2;
				}
				else
				{
					stringBuilder.Append(((int)name[0]).ToString("X4", CultureInfo.InvariantCulture));
					num = 1;
				}
				stringBuilder.Append("_");
				i++;
				if (num3 == 0 && enumerator.MoveNext())
				{
					num3 = ((Match)enumerator.Current).Index - 1;
				}
			}
			while (i < length)
			{
				if ((local && !XmlConvert.xmlCharType.IsNCNameCharXml4e(name[i])) || (!local && !XmlConvert.xmlCharType.IsNameCharXml4e(name[i])) || num3 == i)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 20);
					}
					if (num3 == i && enumerator.MoveNext())
					{
						num3 = ((Match)enumerator.Current).Index - 1;
					}
					stringBuilder.Append(name, num, i - num);
					stringBuilder.Append("_x");
					if (length > i + 1 && XmlCharType.IsHighSurrogate((int)name[i]) && XmlCharType.IsLowSurrogate((int)name[i + 1]))
					{
						int highChar2 = (int)name[i];
						stringBuilder.Append(XmlCharType.CombineSurrogateChar((int)name[i + 1], highChar2).ToString("X8", CultureInfo.InvariantCulture));
						num = i + 2;
						i++;
					}
					else
					{
						stringBuilder.Append(((int)name[i]).ToString("X4", CultureInfo.InvariantCulture));
						num = i + 1;
					}
					stringBuilder.Append("_");
				}
				i++;
			}
			if (num == 0)
			{
				return name;
			}
			if (num < length)
			{
				stringBuilder.Append(name, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00081232 File Offset: 0x0007F432
		private static int FromHex(char digit)
		{
			if (digit > '9')
			{
				return (int)(((digit <= 'F') ? (digit - 'A') : (digit - 'a')) + '\n');
			}
			return (int)(digit - '0');
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00081250 File Offset: 0x0007F450
		internal static byte[] FromBinHexString(string s)
		{
			return XmlConvert.FromBinHexString(s, true);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00081259 File Offset: 0x0007F459
		internal static byte[] FromBinHexString(string s, bool allowOddCount)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return BinHexDecoder.Decode(s.ToCharArray(), allowOddCount);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00081275 File Offset: 0x0007F475
		internal static string ToBinHexString(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return BinHexEncoder.Encode(inArray, 0, inArray.Length);
		}

		/// <summary>Verifies that the name is a valid name according to the W3C Extended Markup Language recommendation.</summary>
		/// <param name="name">The name to verify. </param>
		/// <returns>The name, if it is a valid XML name.</returns>
		/// <exception cref="T:System.Xml.XmlException">
		///         <paramref name="name" /> is not a valid XML name. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="name" /> is <see langword="null" /> or String.Empty. </exception>
		// Token: 0x0600147D RID: 5245 RVA: 0x00081290 File Offset: 0x0007F490
		public static string VerifyName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentNullException("name", Res.GetString("The empty string '' is not a valid name."));
			}
			int num = ValidateNames.ParseNameNoNamespaces(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateInvalidNameCharException(name, num, ExceptionType.XmlException);
			}
			return name;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x000812E4 File Offset: 0x0007F4E4
		internal static Exception TryVerifyName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			int num = ValidateNames.ParseNameNoNamespaces(name, 0);
			if (num != name.Length)
			{
				return new XmlException((num == 0) ? "Name cannot begin with the '{0}' character, hexadecimal value {1}." : "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num));
			}
			return null;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0008133A File Offset: 0x0007F53A
		internal static string VerifyQName(string name)
		{
			return XmlConvert.VerifyQName(name, ExceptionType.XmlException);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00081344 File Offset: 0x0007F544
		internal static string VerifyQName(string name, ExceptionType exceptionType)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentNullException("name");
			}
			int num = -1;
			int num2 = ValidateNames.ParseQName(name, 0, out num);
			if (num2 != name.Length)
			{
				throw XmlConvert.CreateException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num2), exceptionType, 0, num2 + 1);
			}
			return name;
		}

		/// <summary>Verifies that the name is a valid <see langword="NCName" /> according to the W3C Extended Markup Language recommendation. An <see langword="NCName" /> is a name that cannot contain a colon.</summary>
		/// <param name="name">The name to verify. </param>
		/// <returns>The name, if it is a valid NCName.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="name" /> is <see langword="null" /> or String.Empty. </exception>
		/// <exception cref="T:System.Xml.XmlException">
		///         <paramref name="name" /> is not a valid non-colon name. </exception>
		// Token: 0x06001481 RID: 5249 RVA: 0x00081394 File Offset: 0x0007F594
		public static string VerifyNCName(string name)
		{
			return XmlConvert.VerifyNCName(name, ExceptionType.XmlException);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x000813A0 File Offset: 0x0007F5A0
		internal static string VerifyNCName(string name, ExceptionType exceptionType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentNullException("name", Res.GetString("The empty string '' is not a valid local name."));
			}
			int num = ValidateNames.ParseNCName(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateInvalidNameCharException(name, num, exceptionType);
			}
			return name;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x000813F4 File Offset: 0x0007F5F4
		internal static Exception TryVerifyNCName(string name)
		{
			int num = ValidateNames.ParseNCName(name);
			if (num == 0 || num != name.Length)
			{
				return ValidateNames.GetInvalidNameException(name, 0, num);
			}
			return null;
		}

		/// <summary>Verifies that the string is a valid token according to the W3C XML Schema Part2: Datatypes recommendation.</summary>
		/// <param name="token">The string value you wish to verify.</param>
		/// <returns>The token, if it is a valid token.</returns>
		/// <exception cref="T:System.Xml.XmlException">The string value is not a valid token.</exception>
		// Token: 0x06001484 RID: 5252 RVA: 0x00081420 File Offset: 0x0007F620
		public static string VerifyTOKEN(string token)
		{
			if (token == null || token.Length == 0)
			{
				return token;
			}
			if (token[0] == ' ' || token[token.Length - 1] == ' ' || token.IndexOfAny(XmlConvert.crt) != -1 || token.IndexOf("  ", StringComparison.Ordinal) != -1)
			{
				throw new XmlException("line-feed (#xA) or tab (#x9) characters, leading or trailing spaces and sequences of one or more spaces (#x20) are not allowed in 'xs:token'.", token);
			}
			return token;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00081484 File Offset: 0x0007F684
		internal static Exception TryVerifyTOKEN(string token)
		{
			if (token == null || token.Length == 0)
			{
				return null;
			}
			if (token[0] == ' ' || token[token.Length - 1] == ' ' || token.IndexOfAny(XmlConvert.crt) != -1 || token.IndexOf("  ", StringComparison.Ordinal) != -1)
			{
				return new XmlException("line-feed (#xA) or tab (#x9) characters, leading or trailing spaces and sequences of one or more spaces (#x20) are not allowed in 'xs:token'.", token);
			}
			return null;
		}

		/// <summary>Verifies that the string is a valid NMTOKEN according to the W3C XML Schema Part2: Datatypes recommendation</summary>
		/// <param name="name">The string you wish to verify.</param>
		/// <returns>The name token, if it is a valid NMTOKEN.</returns>
		/// <exception cref="T:System.Xml.XmlException">The string is not a valid name token.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06001486 RID: 5254 RVA: 0x000814E5 File Offset: 0x0007F6E5
		public static string VerifyNMTOKEN(string name)
		{
			return XmlConvert.VerifyNMTOKEN(name, ExceptionType.XmlException);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x000814F0 File Offset: 0x0007F6F0
		internal static string VerifyNMTOKEN(string name, ExceptionType exceptionType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw XmlConvert.CreateException("Invalid NmToken value '{0}'.", name, exceptionType);
			}
			int num = ValidateNames.ParseNmtokenNoNamespaces(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num), exceptionType, 0, num + 1);
			}
			return name;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0008154C File Offset: 0x0007F74C
		internal static Exception TryVerifyNMTOKEN(string name)
		{
			if (name == null || name.Length == 0)
			{
				return new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			int num = ValidateNames.ParseNmtokenNoNamespaces(name, 0);
			if (num != name.Length)
			{
				return new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num));
			}
			return null;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00081598 File Offset: 0x0007F798
		internal static string VerifyNormalizedString(string str)
		{
			if (str.IndexOfAny(XmlConvert.crt) != -1)
			{
				throw new XmlSchemaException("Carriage return (#xD), line feed (#xA), and tab (#x9) characters are not allowed in xs:normalizedString.", str);
			}
			return str;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x000815B5 File Offset: 0x0007F7B5
		internal static Exception TryVerifyNormalizedString(string str)
		{
			if (str.IndexOfAny(XmlConvert.crt) != -1)
			{
				return new XmlSchemaException("Carriage return (#xD), line feed (#xA), and tab (#x9) characters are not allowed in xs:normalizedString.", str);
			}
			return null;
		}

		/// <summary>Returns the passed-in string if all the characters and surrogate pair characters in the string argument are valid XML characters, otherwise an <see langword="XmlException" /> is thrown with information on the first invalid character encountered. </summary>
		/// <param name="content">
		///       <see cref="T:System.String" /> that contains characters to verify.</param>
		/// <returns>Returns the passed-in string if all the characters and surrogate-pair characters in the string argument are valid XML characters, otherwise an <see langword="XmlException" /> is thrown with information on the first invalid character encountered.</returns>
		// Token: 0x0600148B RID: 5259 RVA: 0x000815D2 File Offset: 0x0007F7D2
		public static string VerifyXmlChars(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			XmlConvert.VerifyCharData(content, ExceptionType.XmlException);
			return content;
		}

		/// <summary>Returns the passed in string instance if all the characters in the string argument are valid public id characters.</summary>
		/// <param name="publicId">
		///       <see cref="T:System.String" /> that contains the id to validate.</param>
		/// <returns>Returns the passed-in string if all the characters in the argument are valid public id characters.</returns>
		// Token: 0x0600148C RID: 5260 RVA: 0x000815EC File Offset: 0x0007F7EC
		public static string VerifyPublicId(string publicId)
		{
			if (publicId == null)
			{
				throw new ArgumentNullException("publicId");
			}
			int num = XmlConvert.xmlCharType.IsPublicId(publicId);
			if (num != -1)
			{
				throw XmlConvert.CreateInvalidCharException(publicId, num, ExceptionType.XmlException);
			}
			return publicId;
		}

		/// <summary>Returns the passed-in string instance if all the characters in the string argument are valid whitespace characters. </summary>
		/// <param name="content">
		///       <see cref="T:System.String" /> to verify.</param>
		/// <returns>Returns the passed-in string instance if all the characters in the string argument are valid whitespace characters, otherwise <see langword="null" />.</returns>
		// Token: 0x0600148D RID: 5261 RVA: 0x00081624 File Offset: 0x0007F824
		public static string VerifyWhitespace(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			int num = XmlConvert.xmlCharType.IsOnlyWhitespaceWithPos(content);
			if (num != -1)
			{
				throw new XmlException("The Whitespace or SignificantWhitespace node can contain only XML white space characters. '{0}' is not an XML white space character.", XmlException.BuildCharExceptionArgs(content, num), 0, num + 1);
			}
			return content;
		}

		/// <summary>Checks if the passed-in character is a valid Start Name Character type.</summary>
		/// <param name="ch">The character to validate.</param>
		/// <returns>
		///     <see langword="true" /> if the character is a valid Start Name Character type; otherwise, <see langword="false" />. </returns>
		// Token: 0x0600148E RID: 5262 RVA: 0x00081666 File Offset: 0x0007F866
		public static bool IsStartNCNameChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 4) > 0;
		}

		/// <summary>Checks whether the passed-in character is a valid non-colon character type.</summary>
		/// <param name="ch">The character to verify as a non-colon character.</param>
		/// <returns>Returns <see langword="true" /> if the character is a valid non-colon character type; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600148F RID: 5263 RVA: 0x00081679 File Offset: 0x0007F879
		public static bool IsNCNameChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 8) > 0;
		}

		/// <summary>Checks if the passed-in character is a valid XML character.</summary>
		/// <param name="ch">The character to validate.</param>
		/// <returns>
		///     <see langword="true" /> if the passed in character is a valid XML character; otherwise <see langword="false" />.</returns>
		// Token: 0x06001490 RID: 5264 RVA: 0x0008168C File Offset: 0x0007F88C
		public static bool IsXmlChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 16) > 0;
		}

		/// <summary>Checks if the passed-in surrogate pair of characters is a valid XML character.</summary>
		/// <param name="lowChar">The surrogate character to validate.</param>
		/// <param name="highChar">The surrogate character to validate.</param>
		/// <returns>
		///     <see langword="true" /> if the passed in surrogate pair of characters is a valid XML character; otherwise <see langword="false" />.</returns>
		// Token: 0x06001491 RID: 5265 RVA: 0x000816A0 File Offset: 0x0007F8A0
		public static bool IsXmlSurrogatePair(char lowChar, char highChar)
		{
			return XmlCharType.IsHighSurrogate((int)highChar) && XmlCharType.IsLowSurrogate((int)lowChar);
		}

		/// <summary>Returns the passed-in character instance if the character in the argument is a valid public id character, otherwise <see langword="null" />.</summary>
		/// <param name="ch">
		///       <see cref="T:System.Char" /> object to validate.</param>
		/// <returns>Returns the passed-in character if the character is a valid public id character, otherwise <see langword="null" />.</returns>
		// Token: 0x06001492 RID: 5266 RVA: 0x000816B2 File Offset: 0x0007F8B2
		public static bool IsPublicIdChar(char ch)
		{
			return XmlConvert.xmlCharType.IsPubidChar(ch);
		}

		/// <summary>Checks if the passed-in character is a valid XML whitespace character.</summary>
		/// <param name="ch">The character to validate.</param>
		/// <returns>
		///     <see langword="true" /> if the passed in character is a valid XML whitespace character; otherwise <see langword="false" />.</returns>
		// Token: 0x06001493 RID: 5267 RVA: 0x000816BF File Offset: 0x0007F8BF
		public static bool IsWhitespaceChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 1) > 0;
		}

		/// <summary>Converts the <see cref="T:System.Boolean" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Boolean" />, that is, "true" or "false".</returns>
		// Token: 0x06001494 RID: 5268 RVA: 0x000816D2 File Offset: 0x0007F8D2
		public static string ToString(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		/// <summary>Converts the <see cref="T:System.Char" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Char" />.</returns>
		// Token: 0x06001495 RID: 5269 RVA: 0x000816E2 File Offset: 0x0007F8E2
		public static string ToString(char value)
		{
			return value.ToString(null);
		}

		/// <summary>Converts the <see cref="T:System.Decimal" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Decimal" />.</returns>
		// Token: 0x06001496 RID: 5270 RVA: 0x000816EC File Offset: 0x0007F8EC
		public static string ToString(decimal value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.SByte" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="SByte" />.</returns>
		// Token: 0x06001497 RID: 5271 RVA: 0x000816FB File Offset: 0x0007F8FB
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Int16" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Int16" />.</returns>
		// Token: 0x06001498 RID: 5272 RVA: 0x0008170A File Offset: 0x0007F90A
		public static string ToString(short value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Int32" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Int32" />.</returns>
		// Token: 0x06001499 RID: 5273 RVA: 0x00081719 File Offset: 0x0007F919
		public static string ToString(int value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Int64" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Int64" />.</returns>
		// Token: 0x0600149A RID: 5274 RVA: 0x00081728 File Offset: 0x0007F928
		public static string ToString(long value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Byte" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Byte" />.</returns>
		// Token: 0x0600149B RID: 5275 RVA: 0x00081737 File Offset: 0x0007F937
		public static string ToString(byte value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.UInt16" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="UInt16" />.</returns>
		// Token: 0x0600149C RID: 5276 RVA: 0x00081746 File Offset: 0x0007F946
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.UInt32" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="UInt32" />.</returns>
		// Token: 0x0600149D RID: 5277 RVA: 0x00081755 File Offset: 0x0007F955
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.UInt64" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="UInt64" />.</returns>
		// Token: 0x0600149E RID: 5278 RVA: 0x00081764 File Offset: 0x0007F964
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Single" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Single" />.</returns>
		// Token: 0x0600149F RID: 5279 RVA: 0x00081773 File Offset: 0x0007F973
		public static string ToString(float value)
		{
			if (float.IsNegativeInfinity(value))
			{
				return "-INF";
			}
			if (float.IsPositiveInfinity(value))
			{
				return "INF";
			}
			if (XmlConvert.IsNegativeZero((double)value))
			{
				return "-0";
			}
			return value.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Double" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Double" />.</returns>
		// Token: 0x060014A0 RID: 5280 RVA: 0x000817B1 File Offset: 0x0007F9B1
		public static string ToString(double value)
		{
			if (double.IsNegativeInfinity(value))
			{
				return "-INF";
			}
			if (double.IsPositiveInfinity(value))
			{
				return "INF";
			}
			if (XmlConvert.IsNegativeZero(value))
			{
				return "-0";
			}
			return value.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="TimeSpan" />.</returns>
		// Token: 0x060014A1 RID: 5281 RVA: 0x000817F0 File Offset: 0x0007F9F0
		public static string ToString(TimeSpan value)
		{
			return new XsdDuration(value).ToString();
		}

		/// <summary>Converts the <see cref="T:System.DateTime" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="DateTime" /> in the format yyyy-MM-ddTHH:mm:ss where 'T' is a constant literal.</returns>
		// Token: 0x060014A2 RID: 5282 RVA: 0x00081811 File Offset: 0x0007FA11
		[Obsolete("Use XmlConvert.ToString() that takes in XmlDateTimeSerializationMode")]
		public static string ToString(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
		}

		/// <summary>Converts the <see cref="T:System.DateTime" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <param name="format">The format structure that defines how to display the converted string. Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. </param>
		/// <returns>A string representation of the <see langword="DateTime" /> in the specified format.</returns>
		// Token: 0x060014A3 RID: 5283 RVA: 0x0008181E File Offset: 0x0007FA1E
		public static string ToString(DateTime value, string format)
		{
			return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.DateTime" /> to a <see cref="T:System.String" /> using the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> specified.</summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to convert.</param>
		/// <param name="dateTimeOption">One of the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> values that specify how to treat the <see cref="T:System.DateTime" /> value.</param>
		/// <returns>A <see cref="T:System.String" /> equivalent of the <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="dateTimeOption" /> value is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> or <paramref name="dateTimeOption" /> value is <see langword="null" />.</exception>
		// Token: 0x060014A4 RID: 5284 RVA: 0x00081830 File Offset: 0x0007FA30
		public static string ToString(DateTime value, XmlDateTimeSerializationMode dateTimeOption)
		{
			switch (dateTimeOption)
			{
			case XmlDateTimeSerializationMode.Local:
				value = XmlConvert.SwitchToLocalTime(value);
				break;
			case XmlDateTimeSerializationMode.Utc:
				value = XmlConvert.SwitchToUtcTime(value);
				break;
			case XmlDateTimeSerializationMode.Unspecified:
				value = new DateTime(value.Ticks, DateTimeKind.Unspecified);
				break;
			case XmlDateTimeSerializationMode.RoundtripKind:
				break;
			default:
				throw new ArgumentException(Res.GetString("The '{0}' value for the 'dateTimeOption' parameter is not an allowed value for the 'XmlDateTimeSerializationMode' enumeration.", new object[]
				{
					dateTimeOption,
					"dateTimeOption"
				}));
			}
			XsdDateTime xsdDateTime = new XsdDateTime(value, XsdDateTimeFlags.DateTime);
			return xsdDateTime.ToString();
		}

		/// <summary>Converts the supplied <see cref="T:System.DateTimeOffset" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> to be converted.</param>
		/// <returns>A <see cref="T:System.String" /> representation of the supplied <see cref="T:System.DateTimeOffset" />.</returns>
		// Token: 0x060014A5 RID: 5285 RVA: 0x000818B8 File Offset: 0x0007FAB8
		public static string ToString(DateTimeOffset value)
		{
			XsdDateTime xsdDateTime = new XsdDateTime(value);
			return xsdDateTime.ToString();
		}

		/// <summary>Converts the supplied <see cref="T:System.DateTimeOffset" /> to a <see cref="T:System.String" /> in the specified format.</summary>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> to be converted.</param>
		/// <param name="format">The format to which <paramref name="s" /> is converted. The format parameter can be any subset of the W3C Recommendation for the XML dateTime type. (For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.)</param>
		/// <returns>A <see cref="T:System.String" /> representation in the specified format of the supplied <see cref="T:System.DateTimeOffset" />.</returns>
		// Token: 0x060014A6 RID: 5286 RVA: 0x000818DA File Offset: 0x0007FADA
		public static string ToString(DateTimeOffset value, string format)
		{
			return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Guid" /> to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The value to convert. </param>
		/// <returns>A string representation of the <see langword="Guid" />.</returns>
		// Token: 0x060014A7 RID: 5287 RVA: 0x000818E9 File Offset: 0x0007FAE9
		public static string ToString(Guid value)
		{
			return value.ToString();
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Boolean" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="Boolean" /> value, that is, <see langword="true" /> or <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> does not represent a <see langword="Boolean" /> value. </exception>
		// Token: 0x060014A8 RID: 5288 RVA: 0x000818F8 File Offset: 0x0007FAF8
		public static bool ToBoolean(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "1" || s == "true")
			{
				return true;
			}
			if (s == "0" || s == "false")
			{
				return false;
			}
			throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
			{
				s,
				"Boolean"
			}));
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00081968 File Offset: 0x0007FB68
		internal static Exception TryToBoolean(string s, out bool result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "0" || s == "false")
			{
				result = false;
				return null;
			}
			if (s == "1" || s == "true")
			{
				result = true;
				return null;
			}
			result = false;
			return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
			{
				s,
				"Boolean"
			}));
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Char" /> equivalent.</summary>
		/// <param name="s">The string containing a single character to convert. </param>
		/// <returns>A <see langword="Char" /> representing the single character.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="s" /> parameter is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter contains more than one character. </exception>
		// Token: 0x060014AA RID: 5290 RVA: 0x000819DF File Offset: 0x0007FBDF
		public static char ToChar(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(Res.GetString("String must be exactly one character long."));
			}
			return s[0];
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00081A0F File Offset: 0x0007FC0F
		internal static Exception TryToChar(string s, out char result)
		{
			if (!char.TryParse(s, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Char"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Decimal" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="Decimal" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		// Token: 0x060014AC RID: 5292 RVA: 0x00081A3D File Offset: 0x0007FC3D
		public static decimal ToDecimal(string s)
		{
			return decimal.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00081A4C File Offset: 0x0007FC4C
		internal static Exception TryToDecimal(string s, out decimal result)
		{
			if (!decimal.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Decimal"
				}));
			}
			return null;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00081A81 File Offset: 0x0007FC81
		internal static decimal ToInteger(string s)
		{
			return decimal.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00081A8F File Offset: 0x0007FC8F
		internal static Exception TryToInteger(string s, out decimal result)
		{
			if (!decimal.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Integer"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.SByte" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>An <see langword="SByte" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		// Token: 0x060014B0 RID: 5296 RVA: 0x00081AC3 File Offset: 0x0007FCC3
		[CLSCompliant(false)]
		public static sbyte ToSByte(string s)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00081AD1 File Offset: 0x0007FCD1
		internal static Exception TryToSByte(string s, out sbyte result)
		{
			if (!sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"SByte"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Int16" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>An <see langword="Int16" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		// Token: 0x060014B2 RID: 5298 RVA: 0x00081B05 File Offset: 0x0007FD05
		public static short ToInt16(string s)
		{
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00081B13 File Offset: 0x0007FD13
		internal static Exception TryToInt16(string s, out short result)
		{
			if (!short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Int16"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Int32" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>An <see langword="Int32" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x060014B4 RID: 5300 RVA: 0x00081B47 File Offset: 0x0007FD47
		public static int ToInt32(string s)
		{
			return int.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00081B55 File Offset: 0x0007FD55
		internal static Exception TryToInt32(string s, out int result)
		{
			if (!int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Int32"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Int64" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>An <see langword="Int64" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		// Token: 0x060014B6 RID: 5302 RVA: 0x00081B89 File Offset: 0x0007FD89
		public static long ToInt64(string s)
		{
			return long.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00081B97 File Offset: 0x0007FD97
		internal static Exception TryToInt64(string s, out long result)
		{
			if (!long.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Int64"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Byte" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="Byte" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		// Token: 0x060014B8 RID: 5304 RVA: 0x00081BCB File Offset: 0x0007FDCB
		public static byte ToByte(string s)
		{
			return byte.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00081BD9 File Offset: 0x0007FDD9
		internal static Exception TryToByte(string s, out byte result)
		{
			if (!byte.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Byte"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.UInt16" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="UInt16" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		// Token: 0x060014BA RID: 5306 RVA: 0x00081C0D File Offset: 0x0007FE0D
		[CLSCompliant(false)]
		public static ushort ToUInt16(string s)
		{
			return ushort.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00081C1B File Offset: 0x0007FE1B
		internal static Exception TryToUInt16(string s, out ushort result)
		{
			if (!ushort.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"UInt16"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.UInt32" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="UInt32" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		// Token: 0x060014BC RID: 5308 RVA: 0x00081C4F File Offset: 0x0007FE4F
		[CLSCompliant(false)]
		public static uint ToUInt32(string s)
		{
			return uint.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00081C5D File Offset: 0x0007FE5D
		internal static Exception TryToUInt32(string s, out uint result)
		{
			if (!uint.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"UInt32"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.UInt64" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="UInt64" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		// Token: 0x060014BE RID: 5310 RVA: 0x00081C91 File Offset: 0x0007FE91
		[CLSCompliant(false)]
		public static ulong ToUInt64(string s)
		{
			return ulong.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00081C9F File Offset: 0x0007FE9F
		internal static Exception TryToUInt64(string s, out ulong result)
		{
			if (!ulong.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"UInt64"
				}));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Single" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="Single" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		// Token: 0x060014C0 RID: 5312 RVA: 0x00081CD4 File Offset: 0x0007FED4
		public static float ToSingle(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				return float.NegativeInfinity;
			}
			if (s == "INF")
			{
				return float.PositiveInfinity;
			}
			float num = float.Parse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo);
			if (num == 0f && s[0] == '-')
			{
				return --0f;
			}
			return num;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00081D3C File Offset: 0x0007FF3C
		internal static Exception TryToSingle(string s, out float result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				result = float.NegativeInfinity;
				return null;
			}
			if (s == "INF")
			{
				result = float.PositiveInfinity;
				return null;
			}
			if (!float.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Single"
				}));
			}
			if (result == 0f && s[0] == '-')
			{
				result = --0f;
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Double" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="Double" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///         <paramref name="s" /> represents a number less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		// Token: 0x060014C2 RID: 5314 RVA: 0x00081DD0 File Offset: 0x0007FFD0
		public static double ToDouble(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				return double.NegativeInfinity;
			}
			if (s == "INF")
			{
				return double.PositiveInfinity;
			}
			double num = double.Parse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo);
			if (num == 0.0 && s[0] == '-')
			{
				return --0.0;
			}
			return num;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00081E48 File Offset: 0x00080048
		internal static Exception TryToDouble(string s, out double result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				result = double.NegativeInfinity;
				return null;
			}
			if (s == "INF")
			{
				result = double.PositiveInfinity;
				return null;
			}
			if (!double.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Double"
				}));
			}
			if (result == 0.0 && s[0] == '-')
			{
				result = --0.0;
			}
			return null;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00081EEC File Offset: 0x000800EC
		internal static double ToXPathDouble(object o)
		{
			string text = o as string;
			if (text != null)
			{
				text = XmlConvert.TrimString(text);
				double result;
				if (text.Length != 0 && text[0] != '+' && double.TryParse(text, NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out result))
				{
					return result;
				}
				return double.NaN;
			}
			else
			{
				if (o is double)
				{
					return (double)o;
				}
				if (!(o is bool))
				{
					try
					{
						return Convert.ToDouble(o, NumberFormatInfo.InvariantInfo);
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					catch (ArgumentNullException)
					{
					}
					return double.NaN;
				}
				if (!(bool)o)
				{
					return 0.0;
				}
				return 1.0;
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00081FB8 File Offset: 0x000801B8
		internal static string ToXPathString(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return text;
			}
			if (value is double)
			{
				return ((double)value).ToString("R", NumberFormatInfo.InvariantInfo);
			}
			if (!(value is bool))
			{
				return Convert.ToString(value, NumberFormatInfo.InvariantInfo);
			}
			if (!(bool)value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0008201C File Offset: 0x0008021C
		internal static double XPathRound(double value)
		{
			double num = Math.Round(value);
			if (value - num != 0.5)
			{
				return num;
			}
			return num + 1.0;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" /> equivalent.</summary>
		/// <param name="s">The string to convert. The string format must conform to the W3C XML Schema Part 2: Datatypes recommendation for duration.</param>
		/// <returns>A <see langword="TimeSpan" /> equivalent of the string.</returns>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is not in correct format to represent a <see langword="TimeSpan" /> value. </exception>
		// Token: 0x060014C7 RID: 5319 RVA: 0x0008204C File Offset: 0x0008024C
		public static TimeSpan ToTimeSpan(string s)
		{
			XsdDuration xsdDuration;
			try
			{
				xsdDuration = new XsdDuration(s);
			}
			catch (Exception)
			{
				throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"TimeSpan"
				}));
			}
			return xsdDuration.ToTimeSpan();
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0008209C File Offset: 0x0008029C
		internal static Exception TryToTimeSpan(string s, out TimeSpan result)
		{
			XsdDuration xsdDuration;
			Exception ex = XsdDuration.TryParse(s, out xsdDuration);
			if (ex != null)
			{
				result = TimeSpan.MinValue;
				return ex;
			}
			return xsdDuration.TryToTimeSpan(out result);
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x000820CA File Offset: 0x000802CA
		private static string[] AllDateTimeFormats
		{
			get
			{
				if (XmlConvert.s_allDateTimeFormats == null)
				{
					XmlConvert.CreateAllDateTimeFormats();
				}
				return XmlConvert.s_allDateTimeFormats;
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x000820E4 File Offset: 0x000802E4
		private static void CreateAllDateTimeFormats()
		{
			if (XmlConvert.s_allDateTimeFormats == null)
			{
				XmlConvert.s_allDateTimeFormats = new string[]
				{
					"yyyy-MM-ddTHH:mm:ss.FFFFFFFzzzzzz",
					"yyyy-MM-ddTHH:mm:ss.FFFFFFF",
					"yyyy-MM-ddTHH:mm:ss.FFFFFFFZ",
					"HH:mm:ss.FFFFFFF",
					"HH:mm:ss.FFFFFFFZ",
					"HH:mm:ss.FFFFFFFzzzzzz",
					"yyyy-MM-dd",
					"yyyy-MM-ddZ",
					"yyyy-MM-ddzzzzzz",
					"yyyy-MM",
					"yyyy-MMZ",
					"yyyy-MMzzzzzz",
					"yyyy",
					"yyyyZ",
					"yyyyzzzzzz",
					"--MM-dd",
					"--MM-ddZ",
					"--MM-ddzzzzzz",
					"---dd",
					"---ddZ",
					"---ddzzzzzz",
					"--MM--",
					"--MM--Z",
					"--MM--zzzzzz"
				};
			}
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="DateTime" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is an empty string or is not in the correct format. </exception>
		// Token: 0x060014CB RID: 5323 RVA: 0x000821DA File Offset: 0x000803DA
		[Obsolete("Use XmlConvert.ToDateTime() that takes in XmlDateTimeSerializationMode")]
		public static DateTime ToDateTime(string s)
		{
			return XmlConvert.ToDateTime(s, XmlConvert.AllDateTimeFormats);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <param name="format">The format structure to apply to the converted <see langword="DateTime" />. Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. The string is validated against this format. </param>
		/// <returns>A <see langword="DateTime" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> or <paramref name="format" /> is String.Empty -or- 
		///         <paramref name="s" /> does not contain a date and time that corresponds to <paramref name="format" />. </exception>
		// Token: 0x060014CC RID: 5324 RVA: 0x000821E7 File Offset: 0x000803E7
		public static DateTime ToDateTime(string s, string format)
		{
			return DateTime.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <param name="formats">An array containing the format structures to apply to the converted <see langword="DateTime" />. Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. </param>
		/// <returns>A <see langword="DateTime" /> equivalent of the string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> or an element of <paramref name="formats" /> is String.Empty -or- 
		///         <paramref name="s" /> does not contain a date and time that corresponds to any of the elements of <paramref name="formats" />. </exception>
		// Token: 0x060014CD RID: 5325 RVA: 0x000821F6 File Offset: 0x000803F6
		public static DateTime ToDateTime(string s, string[] formats)
		{
			return DateTime.ParseExact(s, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> using the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> specified</summary>
		/// <param name="s">The <see cref="T:System.String" /> value to convert.</param>
		/// <param name="dateTimeOption">One of the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> values that specify whether the date should be converted to local time or preserved as Coordinated Universal Time (UTC), if it is a UTC date.</param>
		/// <returns>A <see cref="T:System.DateTime" /> equivalent of the <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.NullReferenceException">
		///         <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dateTimeOption" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> is an empty string or is not in a valid format.</exception>
		// Token: 0x060014CE RID: 5326 RVA: 0x00082208 File Offset: 0x00080408
		public static DateTime ToDateTime(string s, XmlDateTimeSerializationMode dateTimeOption)
		{
			DateTime dateTime = new XsdDateTime(s, XsdDateTimeFlags.AllXsd);
			switch (dateTimeOption)
			{
			case XmlDateTimeSerializationMode.Local:
				dateTime = XmlConvert.SwitchToLocalTime(dateTime);
				break;
			case XmlDateTimeSerializationMode.Utc:
				dateTime = XmlConvert.SwitchToUtcTime(dateTime);
				break;
			case XmlDateTimeSerializationMode.Unspecified:
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
				break;
			case XmlDateTimeSerializationMode.RoundtripKind:
				break;
			default:
				throw new ArgumentException(Res.GetString("The '{0}' value for the 'dateTimeOption' parameter is not an allowed value for the 'XmlDateTimeSerializationMode' enumeration.", new object[]
				{
					dateTimeOption,
					"dateTimeOption"
				}));
			}
			return dateTime;
		}

		/// <summary>Converts the supplied <see cref="T:System.String" /> to a <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <param name="s">The string to convert.
		///       Note   The string must conform to a subset of the W3C Recommendation for the XML dateTime type. For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.</param>
		/// <returns>The <see cref="T:System.DateTimeOffset" /> equivalent of the supplied string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The argument passed to this method is outside the range of allowable values. For information about allowable values, see <see cref="T:System.DateTimeOffset" />.</exception>
		/// <exception cref="T:System.FormatException">The argument passed to this method does not conform to a subset of the W3C Recommendations for the XML dateTime type. For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.</exception>
		// Token: 0x060014CF RID: 5327 RVA: 0x00082289 File Offset: 0x00080489
		public static DateTimeOffset ToDateTimeOffset(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return new XsdDateTime(s, XsdDateTimeFlags.AllXsd);
		}

		/// <summary>Converts the supplied <see cref="T:System.String" /> to a <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <param name="s">The string to convert.</param>
		/// <param name="format">The format from which <paramref name="s" /> is converted. The format parameter can be any subset of the W3C Recommendation for the XML dateTime type. (For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.) The string <paramref name="s" /> is validated against this format.</param>
		/// <returns>The <see cref="T:System.DateTimeOffset" /> equivalent of the supplied string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="s" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.FormatException">
		///         <paramref name="s" /> or <paramref name="format" /> is an empty string or is not in the specified format.</exception>
		// Token: 0x060014D0 RID: 5328 RVA: 0x000822A9 File Offset: 0x000804A9
		public static DateTimeOffset ToDateTimeOffset(string s, string format)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return DateTimeOffset.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the supplied <see cref="T:System.String" /> to a <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <param name="s">The string to convert.</param>
		/// <param name="formats">An array of formats from which <paramref name="s" /> can be converted. Each format in <paramref name="formats" /> can be any subset of the W3C Recommendation for the XML dateTime type. (For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.) The string <paramref name="s" /> is validated against one of these formats.</param>
		/// <returns>The <see cref="T:System.DateTimeOffset" /> equivalent of the supplied string.</returns>
		// Token: 0x060014D1 RID: 5329 RVA: 0x000822C6 File Offset: 0x000804C6
		public static DateTimeOffset ToDateTimeOffset(string s, string[] formats)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return DateTimeOffset.ParseExact(s, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Guid" /> equivalent.</summary>
		/// <param name="s">The string to convert. </param>
		/// <returns>A <see langword="Guid" /> equivalent of the string.</returns>
		// Token: 0x060014D2 RID: 5330 RVA: 0x000822E3 File Offset: 0x000804E3
		public static Guid ToGuid(string s)
		{
			return new Guid(s);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x000822EC File Offset: 0x000804EC
		internal static Exception TryToGuid(string s, out Guid result)
		{
			Exception result2 = null;
			result = Guid.Empty;
			try
			{
				result = new Guid(s);
			}
			catch (ArgumentException)
			{
				result2 = new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Guid"
				}));
			}
			catch (FormatException)
			{
				result2 = new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Guid"
				}));
			}
			return result2;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0008237C File Offset: 0x0008057C
		private static DateTime SwitchToLocalTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Local);
			case DateTimeKind.Utc:
				return value.ToLocalTime();
			case DateTimeKind.Local:
				return value;
			default:
				return value;
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000823C0 File Offset: 0x000805C0
		private static DateTime SwitchToUtcTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Utc);
			case DateTimeKind.Utc:
				return value;
			case DateTimeKind.Local:
				return value.ToUniversalTime();
			default:
				return value;
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00082404 File Offset: 0x00080604
		internal static Uri ToUri(string s)
		{
			if (s != null && s.Length > 0)
			{
				s = XmlConvert.TrimString(s);
				if (s.Length == 0 || s.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
					{
						s,
						"Uri"
					}));
				}
			}
			Uri result;
			if (!Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out result))
			{
				throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Uri"
				}));
			}
			return result;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0008248C File Offset: 0x0008068C
		internal static Exception TryToUri(string s, out Uri result)
		{
			result = null;
			if (s != null && s.Length > 0)
			{
				s = XmlConvert.TrimString(s);
				if (s.Length == 0 || s.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
					{
						s,
						"Uri"
					}));
				}
			}
			if (!Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
				{
					s,
					"Uri"
				}));
			}
			return null;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00082518 File Offset: 0x00080718
		internal static bool StrEqual(char[] chars, int strPos1, int strLen1, string str2)
		{
			if (strLen1 != str2.Length)
			{
				return false;
			}
			int num = 0;
			while (num < strLen1 && chars[strPos1 + num] == str2[num])
			{
				num++;
			}
			return num == strLen1;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0008254E File Offset: 0x0008074E
		internal static string TrimString(string value)
		{
			return value.Trim(XmlConvert.WhitespaceChars);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0008255B File Offset: 0x0008075B
		internal static string TrimStringStart(string value)
		{
			return value.TrimStart(XmlConvert.WhitespaceChars);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00082568 File Offset: 0x00080768
		internal static string TrimStringEnd(string value)
		{
			return value.TrimEnd(XmlConvert.WhitespaceChars);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00082575 File Offset: 0x00080775
		internal static string[] SplitString(string value)
		{
			return value.Split(XmlConvert.WhitespaceChars, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00082583 File Offset: 0x00080783
		internal static string[] SplitString(string value, StringSplitOptions splitStringOptions)
		{
			return value.Split(XmlConvert.WhitespaceChars, splitStringOptions);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00082591 File Offset: 0x00080791
		internal static bool IsNegativeZero(double value)
		{
			return value == 0.0 && XmlConvert.DoubleToInt64Bits(value) == XmlConvert.DoubleToInt64Bits(--0.0);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000825B8 File Offset: 0x000807B8
		private unsafe static long DoubleToInt64Bits(double value)
		{
			return *(long*)(&value);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x000825BE File Offset: 0x000807BE
		internal static void VerifyCharData(string data, ExceptionType exceptionType)
		{
			XmlConvert.VerifyCharData(data, exceptionType, exceptionType);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x000825C8 File Offset: 0x000807C8
		internal static void VerifyCharData(string data, ExceptionType invCharExceptionType, ExceptionType invSurrogateExceptionType)
		{
			if (data == null || data.Length == 0)
			{
				return;
			}
			int num = 0;
			int length = data.Length;
			for (;;)
			{
				if (num >= length || (XmlConvert.xmlCharType.charProperties[(int)data[num]] & 16) == 0)
				{
					if (num == length)
					{
						break;
					}
					if (!XmlCharType.IsHighSurrogate((int)data[num]))
					{
						goto IL_90;
					}
					if (num + 1 == length)
					{
						goto Block_5;
					}
					if (!XmlCharType.IsLowSurrogate((int)data[num + 1]))
					{
						goto IL_75;
					}
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return;
			Block_5:
			throw XmlConvert.CreateException("The surrogate pair is invalid. Missing a low surrogate character.", invSurrogateExceptionType, 0, num + 1);
			IL_75:
			throw XmlConvert.CreateInvalidSurrogatePairException(data[num + 1], data[num], invSurrogateExceptionType, 0, num + 1);
			IL_90:
			throw XmlConvert.CreateInvalidCharException(data, num, invCharExceptionType);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00082670 File Offset: 0x00080870
		internal static void VerifyCharData(char[] data, int offset, int len, ExceptionType exceptionType)
		{
			if (data == null || len == 0)
			{
				return;
			}
			int num = offset;
			int num2 = offset + len;
			for (;;)
			{
				if (num >= num2 || (XmlConvert.xmlCharType.charProperties[(int)data[num]] & 16) == 0)
				{
					if (num == num2)
					{
						break;
					}
					if (!XmlCharType.IsHighSurrogate((int)data[num]))
					{
						goto IL_78;
					}
					if (num + 1 == num2)
					{
						goto Block_5;
					}
					if (!XmlCharType.IsLowSurrogate((int)data[num + 1]))
					{
						goto IL_63;
					}
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return;
			Block_5:
			throw XmlConvert.CreateException("The surrogate pair is invalid. Missing a low surrogate character.", exceptionType, 0, offset - num + 1);
			IL_63:
			throw XmlConvert.CreateInvalidSurrogatePairException(data[num + 1], data[num], exceptionType, 0, offset - num + 1);
			IL_78:
			throw XmlConvert.CreateInvalidCharException(data, len, num, exceptionType);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00082700 File Offset: 0x00080900
		internal static string EscapeValueForDebuggerDisplay(string value)
		{
			StringBuilder stringBuilder = null;
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c < ' ' || c == '"')
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(value.Length + 4);
					}
					if (i - num > 0)
					{
						stringBuilder.Append(value, num, i - num);
					}
					num = i + 1;
					switch (c)
					{
					case '\t':
						stringBuilder.Append("\\t");
						goto IL_A9;
					case '\n':
						stringBuilder.Append("\\n");
						goto IL_A9;
					case '\v':
					case '\f':
						break;
					case '\r':
						stringBuilder.Append("\\r");
						goto IL_A9;
					default:
						if (c == '"')
						{
							stringBuilder.Append("\\\"");
							goto IL_A9;
						}
						break;
					}
					stringBuilder.Append(c);
				}
				IL_A9:
				i++;
			}
			if (stringBuilder == null)
			{
				return value;
			}
			if (i - num > 0)
			{
				stringBuilder.Append(value, num, i - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000827E3 File Offset: 0x000809E3
		internal static Exception CreateException(string res, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, exceptionType, 0, 0);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x000827EE File Offset: 0x000809EE
		internal static Exception CreateException(string res, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, string.Empty, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res));
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00082812 File Offset: 0x00080A12
		internal static Exception CreateException(string res, string arg, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, arg, exceptionType, 0, 0);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0008281E File Offset: 0x00080A1E
		internal static Exception CreateException(string res, string arg, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, arg, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res, new object[]
			{
				arg
			}));
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00082849 File Offset: 0x00080A49
		internal static Exception CreateException(string res, string[] args, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, args, exceptionType, 0, 0);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00082858 File Offset: 0x00080A58
		internal static Exception CreateException(string res, string[] args, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, args, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res, args));
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00082887 File Offset: 0x00080A87
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi)
		{
			return XmlConvert.CreateInvalidSurrogatePairException(low, hi, ExceptionType.ArgumentException);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00082891 File Offset: 0x00080A91
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi, ExceptionType exceptionType)
		{
			return XmlConvert.CreateInvalidSurrogatePairException(low, hi, exceptionType, 0, 0);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x000828A0 File Offset: 0x00080AA0
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi, ExceptionType exceptionType, int lineNo, int linePos)
		{
			string[] array = new string[2];
			int num = 0;
			uint num2 = (uint)hi;
			array[num] = num2.ToString("X", CultureInfo.InvariantCulture);
			int num3 = 1;
			num2 = (uint)low;
			array[num3] = num2.ToString("X", CultureInfo.InvariantCulture);
			string[] args = array;
			return XmlConvert.CreateException("The surrogate pair (0x{0}, 0x{1}) is invalid. A high surrogate character (0xD800 - 0xDBFF) must always be paired with a low surrogate character (0xDC00 - 0xDFFF).", args, exceptionType, lineNo, linePos);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x000828EF File Offset: 0x00080AEF
		internal static Exception CreateInvalidHighSurrogateCharException(char hi)
		{
			return XmlConvert.CreateInvalidHighSurrogateCharException(hi, ExceptionType.ArgumentException);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x000828F8 File Offset: 0x00080AF8
		internal static Exception CreateInvalidHighSurrogateCharException(char hi, ExceptionType exceptionType)
		{
			return XmlConvert.CreateInvalidHighSurrogateCharException(hi, exceptionType, 0, 0);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00082904 File Offset: 0x00080B04
		internal static Exception CreateInvalidHighSurrogateCharException(char hi, ExceptionType exceptionType, int lineNo, int linePos)
		{
			string res = "Invalid high surrogate character (0x{0}). A high surrogate character must have a value from range (0xD800 - 0xDBFF).";
			uint num = (uint)hi;
			return XmlConvert.CreateException(res, num.ToString("X", CultureInfo.InvariantCulture), exceptionType, lineNo, linePos);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00082931 File Offset: 0x00080B31
		internal static Exception CreateInvalidCharException(char[] data, int length, int invCharPos)
		{
			return XmlConvert.CreateInvalidCharException(data, length, invCharPos, ExceptionType.ArgumentException);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0008293C File Offset: 0x00080B3C
		internal static Exception CreateInvalidCharException(char[] data, int length, int invCharPos, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(data, length, invCharPos), exceptionType, 0, invCharPos + 1);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00082955 File Offset: 0x00080B55
		internal static Exception CreateInvalidCharException(string data, int invCharPos)
		{
			return XmlConvert.CreateInvalidCharException(data, invCharPos, ExceptionType.ArgumentException);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0008295F File Offset: 0x00080B5F
		internal static Exception CreateInvalidCharException(string data, int invCharPos, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(data, invCharPos), exceptionType, 0, invCharPos + 1);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00082977 File Offset: 0x00080B77
		internal static Exception CreateInvalidCharException(char invChar, char nextChar)
		{
			return XmlConvert.CreateInvalidCharException(invChar, nextChar, ExceptionType.ArgumentException);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00082981 File Offset: 0x00080B81
		internal static Exception CreateInvalidCharException(char invChar, char nextChar, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(invChar, nextChar), exceptionType);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00082995 File Offset: 0x00080B95
		internal static Exception CreateInvalidNameCharException(string name, int index, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException((index == 0) ? "Name cannot begin with the '{0}' character, hexadecimal value {1}." : "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, index), exceptionType, 0, index + 1);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x000829B7 File Offset: 0x00080BB7
		internal static ArgumentException CreateInvalidNameArgumentException(string name, string argumentName)
		{
			if (name != null)
			{
				return new ArgumentException(Res.GetString("The empty string '' is not a valid name."), argumentName);
			}
			return new ArgumentNullException(argumentName);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlConvert" /> class. </summary>
		// Token: 0x060014F8 RID: 5368 RVA: 0x0000216B File Offset: 0x0000036B
		public XmlConvert()
		{
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000829D3 File Offset: 0x00080BD3
		// Note: this type is marked as 'beforefieldinit'.
		static XmlConvert()
		{
		}

		// Token: 0x040012B7 RID: 4791
		private static XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x040012B8 RID: 4792
		internal static char[] crt = new char[]
		{
			'\n',
			'\r',
			'\t'
		};

		// Token: 0x040012B9 RID: 4793
		private static readonly int c_EncodedCharLength = 7;

		// Token: 0x040012BA RID: 4794
		private static volatile Regex c_EncodeCharPattern;

		// Token: 0x040012BB RID: 4795
		private static volatile Regex c_DecodeCharPattern;

		// Token: 0x040012BC RID: 4796
		private static volatile string[] s_allDateTimeFormats;

		// Token: 0x040012BD RID: 4797
		internal static readonly char[] WhitespaceChars = new char[]
		{
			' ',
			'\t',
			'\n',
			'\r'
		};
	}
}
