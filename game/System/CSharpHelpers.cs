using System;
using System.Collections.Generic;
using System.Globalization;

namespace System
{
	// Token: 0x02000140 RID: 320
	internal abstract class CSharpHelpers
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x0001E800 File Offset: 0x0001CA00
		static CSharpHelpers()
		{
			CSharpHelpers.s_fixedStringLookup = new Dictionary<string, object>();
			for (int i = 0; i < CSharpHelpers.s_keywords.Length; i++)
			{
				string[] array = CSharpHelpers.s_keywords[i];
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						CSharpHelpers.s_fixedStringLookup.Add(array[j], null);
					}
				}
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001EB66 File Offset: 0x0001CD66
		public static string CreateEscapedIdentifier(string name)
		{
			if (CSharpHelpers.IsKeyword(name) || CSharpHelpers.IsPrefixTwoUnderscore(name))
			{
				return "@" + name;
			}
			return name;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001EB85 File Offset: 0x0001CD85
		public static bool IsValidLanguageIndependentIdentifier(string value)
		{
			return CSharpHelpers.IsValidTypeNameOrIdentifier(value, false);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001EB8E File Offset: 0x0001CD8E
		internal static bool IsKeyword(string value)
		{
			return CSharpHelpers.s_fixedStringLookup.ContainsKey(value);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001EB9B File Offset: 0x0001CD9B
		internal static bool IsPrefixTwoUnderscore(string value)
		{
			return value.Length >= 3 && (value[0] == '_' && value[1] == '_') && value[2] != '_';
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001EBD0 File Offset: 0x0001CDD0
		internal static bool IsValidTypeNameOrIdentifier(string value, bool isTypeName)
		{
			bool flag = true;
			if (value.Length == 0)
			{
				return false;
			}
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				switch (CharUnicodeInfo.GetUnicodeCategory(c))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.LetterNumber:
					flag = false;
					break;
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.ConnectorPunctuation:
					if (flag && c != '_')
					{
						return false;
					}
					flag = false;
					break;
				case UnicodeCategory.EnclosingMark:
				case UnicodeCategory.OtherNumber:
				case UnicodeCategory.SpaceSeparator:
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Control:
				case UnicodeCategory.Format:
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_88;
				default:
					goto IL_88;
				}
				IL_97:
				i++;
				continue;
				IL_88:
				if (!isTypeName || !CSharpHelpers.IsSpecialTypeChar(c, ref flag))
				{
					return false;
				}
				goto IL_97;
			}
			return true;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001EC88 File Offset: 0x0001CE88
		internal static bool IsSpecialTypeChar(char ch, ref bool nextMustBeStartChar)
		{
			if (ch <= '>')
			{
				switch (ch)
				{
				case '$':
				case '&':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
					break;
				case '%':
				case '\'':
				case '(':
				case ')':
					return false;
				default:
					switch (ch)
					{
					case ':':
					case '<':
					case '>':
						break;
					case ';':
					case '=':
						return false;
					default:
						return false;
					}
					break;
				}
			}
			else if (ch != '[' && ch != ']')
			{
				if (ch != '`')
				{
					return false;
				}
				return true;
			}
			nextMustBeStartChar = true;
			return true;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0000219B File Offset: 0x0000039B
		protected CSharpHelpers()
		{
		}

		// Token: 0x0400053D RID: 1341
		private static Dictionary<string, object> s_fixedStringLookup;

		// Token: 0x0400053E RID: 1342
		private static readonly string[][] s_keywords = new string[][]
		{
			null,
			new string[]
			{
				"as",
				"do",
				"if",
				"in",
				"is"
			},
			new string[]
			{
				"for",
				"int",
				"new",
				"out",
				"ref",
				"try"
			},
			new string[]
			{
				"base",
				"bool",
				"byte",
				"case",
				"char",
				"else",
				"enum",
				"goto",
				"lock",
				"long",
				"null",
				"this",
				"true",
				"uint",
				"void"
			},
			new string[]
			{
				"break",
				"catch",
				"class",
				"const",
				"event",
				"false",
				"fixed",
				"float",
				"sbyte",
				"short",
				"throw",
				"ulong",
				"using",
				"where",
				"while",
				"yield"
			},
			new string[]
			{
				"double",
				"extern",
				"object",
				"params",
				"public",
				"return",
				"sealed",
				"sizeof",
				"static",
				"string",
				"struct",
				"switch",
				"typeof",
				"unsafe",
				"ushort"
			},
			new string[]
			{
				"checked",
				"decimal",
				"default",
				"finally",
				"foreach",
				"partial",
				"private",
				"virtual"
			},
			new string[]
			{
				"abstract",
				"continue",
				"delegate",
				"explicit",
				"implicit",
				"internal",
				"operator",
				"override",
				"readonly",
				"volatile"
			},
			new string[]
			{
				"__arglist",
				"__makeref",
				"__reftype",
				"interface",
				"namespace",
				"protected",
				"unchecked"
			},
			new string[]
			{
				"__refvalue",
				"stackalloc"
			}
		};
	}
}
