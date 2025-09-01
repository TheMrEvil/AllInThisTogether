using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents authentication information in Authorization, ProxyAuthorization, WWW-Authenticate, and Proxy-Authenticate header values.</summary>
	// Token: 0x02000034 RID: 52
	public class AuthenticationHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> class.</summary>
		/// <param name="scheme">The scheme to use for authorization.</param>
		// Token: 0x0600019A RID: 410 RVA: 0x00006AA6 File Offset: 0x00004CA6
		public AuthenticationHeaderValue(string scheme) : this(scheme, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> class.</summary>
		/// <param name="scheme">The scheme to use for authorization.</param>
		/// <param name="parameter">The credentials containing the authentication information of the user agent for the resource being requested.</param>
		// Token: 0x0600019B RID: 411 RVA: 0x00006AB0 File Offset: 0x00004CB0
		public AuthenticationHeaderValue(string scheme, string parameter)
		{
			Parser.Token.Check(scheme);
			this.Scheme = scheme;
			this.Parameter = parameter;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000022B8 File Offset: 0x000004B8
		private AuthenticationHeaderValue()
		{
		}

		/// <summary>Gets the credentials containing the authentication information of the user agent for the resource being requested.</summary>
		/// <returns>The credentials containing the authentication information.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006ACC File Offset: 0x00004CCC
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public string Parameter
		{
			[CompilerGenerated]
			get
			{
				return this.<Parameter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Parameter>k__BackingField = value;
			}
		}

		/// <summary>Gets the scheme to use for authorization.</summary>
		/// <returns>The scheme to use for authorization.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006ADD File Offset: 0x00004CDD
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00006AE5 File Offset: 0x00004CE5
		public string Scheme
		{
			[CompilerGenerated]
			get
			{
				return this.<Scheme>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Scheme>k__BackingField = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x060001A1 RID: 417 RVA: 0x00006AEE File Offset: 0x00004CEE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001A2 RID: 418 RVA: 0x00006AF8 File Offset: 0x00004CF8
		public override bool Equals(object obj)
		{
			AuthenticationHeaderValue authenticationHeaderValue = obj as AuthenticationHeaderValue;
			return authenticationHeaderValue != null && string.Equals(authenticationHeaderValue.Scheme, this.Scheme, StringComparison.OrdinalIgnoreCase) && authenticationHeaderValue.Parameter == this.Parameter;
		}

		/// <summary>Serves as a hash function for an  <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x060001A3 RID: 419 RVA: 0x00006B38 File Offset: 0x00004D38
		public override int GetHashCode()
		{
			int num = this.Scheme.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(this.Parameter))
			{
				num ^= this.Parameter.ToLowerInvariant().GetHashCode();
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents authentication header value information.</param>
		/// <returns>An <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid authentication header value information.</exception>
		// Token: 0x060001A4 RID: 420 RVA: 0x00006B78 File Offset: 0x00004D78
		public static AuthenticationHeaderValue Parse(string input)
		{
			AuthenticationHeaderValue result;
			if (AuthenticationHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001A5 RID: 421 RVA: 0x00006B98 File Offset: 0x00004D98
		public static bool TryParse(string input, out AuthenticationHeaderValue parsedValue)
		{
			Token token;
			if (AuthenticationHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006BC4 File Offset: 0x00004DC4
		internal static bool TryParse(string input, int minimalCount, out List<AuthenticationHeaderValue> result)
		{
			return CollectionParser.TryParse<AuthenticationHeaderValue>(input, minimalCount, new ElementTryParser<AuthenticationHeaderValue>(AuthenticationHeaderValue.TryParseElement), out result);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006BDC File Offset: 0x00004DDC
		private static bool TryParseElement(Lexer lexer, out AuthenticationHeaderValue parsedValue, out Token t)
		{
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				parsedValue = null;
				return false;
			}
			parsedValue = new AuthenticationHeaderValue();
			parsedValue.Scheme = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.Token)
			{
				parsedValue.Parameter = lexer.GetRemainingStringValue(t.StartPosition);
				t = new Token(Token.Type.End, 0, 0);
			}
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.AuthenticationHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060001A8 RID: 424 RVA: 0x00006C60 File Offset: 0x00004E60
		public override string ToString()
		{
			if (this.Parameter == null)
			{
				return this.Scheme;
			}
			return this.Scheme + " " + this.Parameter;
		}

		// Token: 0x040000D9 RID: 217
		[CompilerGenerated]
		private string <Parameter>k__BackingField;

		// Token: 0x040000DA RID: 218
		[CompilerGenerated]
		private string <Scheme>k__BackingField;
	}
}
