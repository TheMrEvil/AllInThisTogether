using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a name/value pair used in various headers as defined in RFC 2616.</summary>
	// Token: 0x02000052 RID: 82
	public class NameValueHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		// Token: 0x06000323 RID: 803 RVA: 0x0000B2B7 File Offset: 0x000094B7
		public NameValueHeaderValue(string name) : this(name, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> class.</summary>
		/// <param name="name">The header name.</param>
		/// <param name="value">The header value.</param>
		// Token: 0x06000324 RID: 804 RVA: 0x0000B2C1 File Offset: 0x000094C1
		public NameValueHeaderValue(string name, string value)
		{
			Parser.Token.Check(name);
			this.Name = name;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object used to initialize the new instance.</param>
		// Token: 0x06000325 RID: 805 RVA: 0x0000B2DD File Offset: 0x000094DD
		protected internal NameValueHeaderValue(NameValueHeaderValue source)
		{
			this.Name = source.Name;
			this.value = source.value;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000022B8 File Offset: 0x000004B8
		internal NameValueHeaderValue()
		{
		}

		/// <summary>Gets the header name.</summary>
		/// <returns>The header name.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000B2FD File Offset: 0x000094FD
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000B305 File Offset: 0x00009505
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Name>k__BackingField = value;
			}
		}

		/// <summary>Gets the header value.</summary>
		/// <returns>The header value.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000B30E File Offset: 0x0000950E
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000B318 File Offset: 0x00009518
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					Lexer lexer = new Lexer(value);
					Token token = lexer.Scan(false);
					if (lexer.Scan(false) != Token.Type.End || (token != Token.Type.Token && token != Token.Type.QuotedString))
					{
						throw new FormatException();
					}
					value = lexer.GetStringValue(token);
				}
				this.value = value;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B371 File Offset: 0x00009571
		internal static NameValueHeaderValue Create(string name, string value)
		{
			return new NameValueHeaderValue
			{
				Name = name,
				value = value
			};
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x0600032C RID: 812 RVA: 0x0000B386 File Offset: 0x00009586
		object ICloneable.Clone()
		{
			return new NameValueHeaderValue(this);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x0600032D RID: 813 RVA: 0x0000B390 File Offset: 0x00009590
		public override int GetHashCode()
		{
			int num = this.Name.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(this.value))
			{
				num ^= this.value.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600032E RID: 814 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public override bool Equals(object obj)
		{
			NameValueHeaderValue nameValueHeaderValue = obj as NameValueHeaderValue;
			if (nameValueHeaderValue == null || !string.Equals(nameValueHeaderValue.Name, this.Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.value))
			{
				return string.IsNullOrEmpty(nameValueHeaderValue.value);
			}
			return string.Equals(nameValueHeaderValue.value, this.value, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents name value header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid name value header value information.</exception>
		// Token: 0x0600032F RID: 815 RVA: 0x0000B428 File Offset: 0x00009628
		public static NameValueHeaderValue Parse(string input)
		{
			NameValueHeaderValue result;
			if (NameValueHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B447 File Offset: 0x00009647
		internal static bool TryParsePragma(string input, int minimalCount, out List<NameValueHeaderValue> result)
		{
			return CollectionParser.TryParse<NameValueHeaderValue>(input, minimalCount, new ElementTryParser<NameValueHeaderValue>(NameValueHeaderValue.TryParseElement), out result);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B460 File Offset: 0x00009660
		internal static bool TryParseParameters(Lexer lexer, out List<NameValueHeaderValue> result, out Token t)
		{
			List<NameValueHeaderValue> list = new List<NameValueHeaderValue>();
			result = null;
			for (;;)
			{
				Token token = lexer.Scan(false);
				if (token != Token.Type.Token)
				{
					break;
				}
				string text = null;
				t = lexer.Scan(false);
				if (t == Token.Type.SeparatorEqual)
				{
					t = lexer.Scan(false);
					if (t != Token.Type.Token && t != Token.Type.QuotedString)
					{
						return false;
					}
					text = lexer.GetStringValue(t);
					t = lexer.Scan(false);
				}
				list.Add(new NameValueHeaderValue
				{
					Name = lexer.GetStringValue(token),
					value = text
				});
				if (t != Token.Type.SeparatorSemicolon)
				{
					goto Block_5;
				}
			}
			t = Token.Empty;
			return false;
			Block_5:
			result = list;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06000332 RID: 818 RVA: 0x0000B52A File Offset: 0x0000972A
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.value))
			{
				return this.Name;
			}
			return this.Name + "=" + this.value;
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.NameValueHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000333 RID: 819 RVA: 0x0000B558 File Offset: 0x00009758
		public static bool TryParse(string input, out NameValueHeaderValue parsedValue)
		{
			Token token;
			if (NameValueHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000B584 File Offset: 0x00009784
		private static bool TryParseElement(Lexer lexer, out NameValueHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			parsedValue = new NameValueHeaderValue
			{
				Name = lexer.GetStringValue(t)
			};
			t = lexer.Scan(false);
			if (t == Token.Type.SeparatorEqual)
			{
				t = lexer.Scan(false);
				if (t != Token.Type.Token && t != Token.Type.QuotedString)
				{
					return false;
				}
				parsedValue.value = lexer.GetStringValue(t);
				t = lexer.Scan(false);
			}
			return true;
		}

		// Token: 0x04000137 RID: 311
		internal string value;

		// Token: 0x04000138 RID: 312
		[CompilerGenerated]
		private string <Name>k__BackingField;
	}
}
