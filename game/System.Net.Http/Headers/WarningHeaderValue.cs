using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a warning value used by the Warning header.</summary>
	// Token: 0x0200006C RID: 108
	public class WarningHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> class.</summary>
		/// <param name="code">The specific warning code.</param>
		/// <param name="agent">The host that attached the warning.</param>
		/// <param name="text">A quoted-string containing the warning text.</param>
		// Token: 0x060003E3 RID: 995 RVA: 0x0000D4C7 File Offset: 0x0000B6C7
		public WarningHeaderValue(int code, string agent, string text)
		{
			if (!WarningHeaderValue.IsCodeValid(code))
			{
				throw new ArgumentOutOfRangeException("code");
			}
			Parser.Uri.Check(agent);
			Parser.Token.CheckQuotedString(text);
			this.Code = code;
			this.Agent = agent;
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> class.</summary>
		/// <param name="code">The specific warning code.</param>
		/// <param name="agent">The host that attached the warning.</param>
		/// <param name="text">A quoted-string containing the warning text.</param>
		/// <param name="date">The date/time stamp of the warning.</param>
		// Token: 0x060003E4 RID: 996 RVA: 0x0000D503 File Offset: 0x0000B703
		public WarningHeaderValue(int code, string agent, string text, DateTimeOffset date) : this(code, agent, text)
		{
			this.Date = new DateTimeOffset?(date);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000022B8 File Offset: 0x000004B8
		private WarningHeaderValue()
		{
		}

		/// <summary>Gets the host that attached the warning.</summary>
		/// <returns>The host that attached the warning.</returns>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000D51B File Offset: 0x0000B71B
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000D523 File Offset: 0x0000B723
		public string Agent
		{
			[CompilerGenerated]
			get
			{
				return this.<Agent>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Agent>k__BackingField = value;
			}
		}

		/// <summary>Gets the specific warning code.</summary>
		/// <returns>The specific warning code.</returns>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000D52C File Offset: 0x0000B72C
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000D534 File Offset: 0x0000B734
		public int Code
		{
			[CompilerGenerated]
			get
			{
				return this.<Code>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Code>k__BackingField = value;
			}
		}

		/// <summary>Gets the date/time stamp of the warning.</summary>
		/// <returns>The date/time stamp of the warning.</returns>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000D53D File Offset: 0x0000B73D
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x0000D545 File Offset: 0x0000B745
		public DateTimeOffset? Date
		{
			[CompilerGenerated]
			get
			{
				return this.<Date>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Date>k__BackingField = value;
			}
		}

		/// <summary>Gets a quoted-string containing the warning text.</summary>
		/// <returns>A quoted-string containing the warning text.</returns>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000D54E File Offset: 0x0000B74E
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000D556 File Offset: 0x0000B756
		public string Text
		{
			[CompilerGenerated]
			get
			{
				return this.<Text>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Text>k__BackingField = value;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000D55F File Offset: 0x0000B75F
		private static bool IsCodeValid(int code)
		{
			return code >= 0 && code < 1000;
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> instance.</summary>
		/// <returns>Returns a copy of the current instance.</returns>
		// Token: 0x060003EF RID: 1007 RVA: 0x00006AEE File Offset: 0x00004CEE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000D570 File Offset: 0x0000B770
		public override bool Equals(object obj)
		{
			WarningHeaderValue warningHeaderValue = obj as WarningHeaderValue;
			return warningHeaderValue != null && (this.Code == warningHeaderValue.Code && string.Equals(warningHeaderValue.Agent, this.Agent, StringComparison.OrdinalIgnoreCase) && this.Text == warningHeaderValue.Text) && this.Date == warningHeaderValue.Date;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000D600 File Offset: 0x0000B800
		public override int GetHashCode()
		{
			return this.Code.GetHashCode() ^ this.Agent.ToLowerInvariant().GetHashCode() ^ this.Text.GetHashCode() ^ this.Date.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents authentication header value information.</param>
		/// <returns>Returns a <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid authentication header value information.</exception>
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000D650 File Offset: 0x0000B850
		public static WarningHeaderValue Parse(string input)
		{
			WarningHeaderValue result;
			if (WarningHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000D670 File Offset: 0x0000B870
		public static bool TryParse(string input, out WarningHeaderValue parsedValue)
		{
			Token token;
			if (WarningHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000D69C File Offset: 0x0000B89C
		internal static bool TryParse(string input, int minimalCount, out List<WarningHeaderValue> result)
		{
			return CollectionParser.TryParse<WarningHeaderValue>(input, minimalCount, new ElementTryParser<WarningHeaderValue>(WarningHeaderValue.TryParseElement), out result);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		private static bool TryParseElement(Lexer lexer, out WarningHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			int code;
			if (!lexer.TryGetNumericValue(t, out code) || !WarningHeaderValue.IsCodeValid(code))
			{
				return false;
			}
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			Token token = t;
			if (lexer.PeekChar() == 58)
			{
				lexer.EatChar();
				token = lexer.Scan(false);
				if (token != Token.Type.Token)
				{
					return false;
				}
			}
			WarningHeaderValue warningHeaderValue = new WarningHeaderValue();
			warningHeaderValue.Code = code;
			warningHeaderValue.Agent = lexer.GetStringValue(t, token);
			t = lexer.Scan(false);
			if (t != Token.Type.QuotedString)
			{
				return false;
			}
			warningHeaderValue.Text = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.QuotedString)
			{
				DateTimeOffset value;
				if (!lexer.TryGetDateValue(t, out value))
				{
					return false;
				}
				warningHeaderValue.Date = new DateTimeOffset?(value);
				t = lexer.Scan(false);
			}
			parsedValue = warningHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.WarningHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public override string ToString()
		{
			string text = string.Concat(new string[]
			{
				this.Code.ToString("000"),
				" ",
				this.Agent,
				" ",
				this.Text
			});
			if (this.Date != null)
			{
				text = text + " \"" + this.Date.Value.ToString("r", CultureInfo.InvariantCulture) + "\"";
			}
			return text;
		}

		// Token: 0x04000154 RID: 340
		[CompilerGenerated]
		private string <Agent>k__BackingField;

		// Token: 0x04000155 RID: 341
		[CompilerGenerated]
		private int <Code>k__BackingField;

		// Token: 0x04000156 RID: 342
		[CompilerGenerated]
		private DateTimeOffset? <Date>k__BackingField;

		// Token: 0x04000157 RID: 343
		[CompilerGenerated]
		private string <Text>k__BackingField;
	}
}
