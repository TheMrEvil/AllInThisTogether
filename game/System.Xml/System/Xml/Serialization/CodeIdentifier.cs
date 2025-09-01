using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	/// <summary>Provides static methods to convert input text into names for code entities.</summary>
	// Token: 0x02000271 RID: 625
	public class CodeIdentifier
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.CodeIdentifier" /> class. </summary>
		// Token: 0x060017BB RID: 6075 RVA: 0x0000216B File Offset: 0x0000036B
		[Obsolete("This class should never get constructed as it contains only static methods.")]
		public CodeIdentifier()
		{
		}

		/// <summary>Produces a Pascal-case string from an input string. </summary>
		/// <param name="identifier">The name of a code entity, such as a method parameter, typically taken from an XML element or attribute name.</param>
		/// <returns>A Pascal-case version of the parameter string.</returns>
		// Token: 0x060017BC RID: 6076 RVA: 0x0008B4C4 File Offset: 0x000896C4
		public static string MakePascal(string identifier)
		{
			identifier = CodeIdentifier.MakeValid(identifier);
			if (identifier.Length <= 2)
			{
				return identifier.ToUpper(CultureInfo.InvariantCulture);
			}
			if (char.IsLower(identifier[0]))
			{
				return char.ToUpper(identifier[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture) + identifier.Substring(1);
			}
			return identifier;
		}

		/// <summary>Produces a camel-case string from an input string. </summary>
		/// <param name="identifier">The name of a code entity, such as a method parameter, typically taken from an XML element or attribute name.</param>
		/// <returns>A camel-case version of the parameter string.</returns>
		// Token: 0x060017BD RID: 6077 RVA: 0x0008B528 File Offset: 0x00089728
		public static string MakeCamel(string identifier)
		{
			identifier = CodeIdentifier.MakeValid(identifier);
			if (identifier.Length <= 2)
			{
				return identifier.ToLower(CultureInfo.InvariantCulture);
			}
			if (char.IsUpper(identifier[0]))
			{
				return char.ToLower(identifier[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture) + identifier.Substring(1);
			}
			return identifier;
		}

		/// <summary>Produces a valid code entity name from an input string. </summary>
		/// <param name="identifier">The name of a code entity, such as a method parameter, typically taken from an XML element or attribute name.</param>
		/// <returns>A string that can be used as a code identifier, such as the name of a method parameter.</returns>
		// Token: 0x060017BE RID: 6078 RVA: 0x0008B58C File Offset: 0x0008978C
		public static string MakeValid(string identifier)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (num < identifier.Length && stringBuilder.Length < 511)
			{
				char c = identifier[num];
				if (CodeIdentifier.IsValid(c))
				{
					if (stringBuilder.Length == 0 && !CodeIdentifier.IsValidStart(c))
					{
						stringBuilder.Append("Item");
					}
					stringBuilder.Append(c);
				}
				num++;
			}
			if (stringBuilder.Length == 0)
			{
				return "Item";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0008B605 File Offset: 0x00089805
		internal static string MakeValidInternal(string identifier)
		{
			if (identifier.Length > 30)
			{
				return "Item";
			}
			return CodeIdentifier.MakeValid(identifier);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0008B61D File Offset: 0x0008981D
		private static bool IsValidStart(char c)
		{
			return char.GetUnicodeCategory(c) != UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0008B62C File Offset: 0x0008982C
		private static bool IsValid(char c)
		{
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.DecimalDigitNumber:
			case UnicodeCategory.ConnectorPunctuation:
				return true;
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.LetterNumber:
			case UnicodeCategory.OtherNumber:
			case UnicodeCategory.SpaceSeparator:
			case UnicodeCategory.LineSeparator:
			case UnicodeCategory.ParagraphSeparator:
			case UnicodeCategory.Control:
			case UnicodeCategory.Format:
			case UnicodeCategory.Surrogate:
			case UnicodeCategory.PrivateUse:
			case UnicodeCategory.DashPunctuation:
			case UnicodeCategory.OpenPunctuation:
			case UnicodeCategory.ClosePunctuation:
			case UnicodeCategory.InitialQuotePunctuation:
			case UnicodeCategory.FinalQuotePunctuation:
			case UnicodeCategory.OtherPunctuation:
			case UnicodeCategory.MathSymbol:
			case UnicodeCategory.CurrencySymbol:
			case UnicodeCategory.ModifierSymbol:
			case UnicodeCategory.OtherSymbol:
			case UnicodeCategory.OtherNotAssigned:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0008B6C5 File Offset: 0x000898C5
		internal static void CheckValidIdentifier(string ident)
		{
			if (!CodeGenerator.IsValidLanguageIndependentIdentifier(ident))
			{
				throw new ArgumentException(Res.GetString("Identifier '{0}' is not CLS-compliant.", new object[]
				{
					ident
				}), "ident");
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0008B6EE File Offset: 0x000898EE
		internal static string GetCSharpName(string name)
		{
			return CodeIdentifier.EscapeKeywords(name.Replace('+', '.'), CodeIdentifier.csharp);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0008B704 File Offset: 0x00089904
		private static int GetCSharpName(Type t, Type[] parameters, int index, StringBuilder sb)
		{
			if (t.DeclaringType != null && t.DeclaringType != t)
			{
				index = CodeIdentifier.GetCSharpName(t.DeclaringType, parameters, index, sb);
				sb.Append(".");
			}
			string name = t.Name;
			int num = name.IndexOf('`');
			if (num < 0)
			{
				num = name.IndexOf('!');
			}
			if (num > 0)
			{
				CodeIdentifier.EscapeKeywords(name.Substring(0, num), CodeIdentifier.csharp, sb);
				sb.Append("<");
				int num2 = int.Parse(name.Substring(num + 1), CultureInfo.InvariantCulture) + index;
				while (index < num2)
				{
					sb.Append(CodeIdentifier.GetCSharpName(parameters[index]));
					if (index < num2 - 1)
					{
						sb.Append(",");
					}
					index++;
				}
				sb.Append(">");
			}
			else
			{
				CodeIdentifier.EscapeKeywords(name, CodeIdentifier.csharp, sb);
			}
			return index;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0008B7E8 File Offset: 0x000899E8
		internal static string GetCSharpName(Type t)
		{
			int num = 0;
			while (t.IsArray)
			{
				t = t.GetElementType();
				num++;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("global::");
			string @namespace = t.Namespace;
			if (@namespace != null && @namespace.Length > 0)
			{
				string[] array = @namespace.Split(new char[]
				{
					'.'
				});
				for (int i = 0; i < array.Length; i++)
				{
					CodeIdentifier.EscapeKeywords(array[i], CodeIdentifier.csharp, stringBuilder);
					stringBuilder.Append(".");
				}
			}
			Type[] parameters = (t.IsGenericType || t.ContainsGenericParameters) ? t.GetGenericArguments() : new Type[0];
			CodeIdentifier.GetCSharpName(t, parameters, 0, stringBuilder);
			for (int j = 0; j < num; j++)
			{
				stringBuilder.Append("[]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0008B8C0 File Offset: 0x00089AC0
		private static void EscapeKeywords(string identifier, CodeDomProvider codeProvider, StringBuilder sb)
		{
			if (identifier == null || identifier.Length == 0)
			{
				return;
			}
			int num = 0;
			while (identifier.EndsWith("[]", StringComparison.Ordinal))
			{
				num++;
				identifier = identifier.Substring(0, identifier.Length - 2);
			}
			if (identifier.Length > 0)
			{
				CodeIdentifier.CheckValidIdentifier(identifier);
				identifier = codeProvider.CreateEscapedIdentifier(identifier);
				sb.Append(identifier);
			}
			for (int i = 0; i < num; i++)
			{
				sb.Append("[]");
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0008B938 File Offset: 0x00089B38
		private static string EscapeKeywords(string identifier, CodeDomProvider codeProvider)
		{
			if (identifier == null || identifier.Length == 0)
			{
				return identifier;
			}
			string[] array = identifier.Split(new char[]
			{
				'.',
				',',
				'<',
				'>'
			});
			StringBuilder stringBuilder = new StringBuilder();
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (num >= 0)
				{
					stringBuilder.Append(identifier.Substring(num, 1));
				}
				num++;
				num += array[i].Length;
				CodeIdentifier.EscapeKeywords(array[i].Trim(), codeProvider, stringBuilder);
			}
			if (stringBuilder.Length != identifier.Length)
			{
				return stringBuilder.ToString();
			}
			return identifier;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0008B9CF File Offset: 0x00089BCF
		// Note: this type is marked as 'beforefieldinit'.
		static CodeIdentifier()
		{
		}

		// Token: 0x04001883 RID: 6275
		internal static CodeDomProvider csharp = new CSharpCodeProvider();

		// Token: 0x04001884 RID: 6276
		internal const int MaxIdentifierLength = 511;
	}
}
