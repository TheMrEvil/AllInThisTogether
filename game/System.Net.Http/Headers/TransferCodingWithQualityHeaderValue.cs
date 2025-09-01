using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents an Accept-Encoding header value.with optional quality factor.</summary>
	// Token: 0x0200006A RID: 106
	public sealed class TransferCodingWithQualityHeaderValue : TransferCodingHeaderValue
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		// Token: 0x060003C6 RID: 966 RVA: 0x0000D02F File Offset: 0x0000B22F
		public TransferCodingWithQualityHeaderValue(string value) : base(value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> class.</summary>
		/// <param name="value">A string used to initialize the new instance.</param>
		/// <param name="quality">A value for the quality factor.</param>
		// Token: 0x060003C7 RID: 967 RVA: 0x0000D038 File Offset: 0x0000B238
		public TransferCodingWithQualityHeaderValue(string value, double quality) : this(value)
		{
			this.Quality = new double?(quality);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000D04D File Offset: 0x0000B24D
		private TransferCodingWithQualityHeaderValue()
		{
		}

		/// <summary>Gets the quality factor from the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" />.</summary>
		/// <returns>The quality factor from the <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" />.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000D055 File Offset: 0x0000B255
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000D062 File Offset: 0x0000B262
		public double? Quality
		{
			get
			{
				return QualityValue.GetValue(this.parameters);
			}
			set
			{
				QualityValue.SetValue(ref this.parameters, value);
			}
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents transfer-coding value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid transfer-coding with quality header value information.</exception>
		// Token: 0x060003CB RID: 971 RVA: 0x0000D070 File Offset: 0x0000B270
		public new static TransferCodingWithQualityHeaderValue Parse(string input)
		{
			TransferCodingWithQualityHeaderValue result;
			if (TransferCodingWithQualityHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.TransferCodingWithQualityHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003CC RID: 972 RVA: 0x0000D090 File Offset: 0x0000B290
		public static bool TryParse(string input, out TransferCodingWithQualityHeaderValue parsedValue)
		{
			Token token;
			if (TransferCodingWithQualityHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		internal static bool TryParse(string input, int minimalCount, out List<TransferCodingWithQualityHeaderValue> result)
		{
			return CollectionParser.TryParse<TransferCodingWithQualityHeaderValue>(input, minimalCount, new ElementTryParser<TransferCodingWithQualityHeaderValue>(TransferCodingWithQualityHeaderValue.TryParseElement), out result);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		private static bool TryParseElement(Lexer lexer, out TransferCodingWithQualityHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			TransferCodingWithQualityHeaderValue transferCodingWithQualityHeaderValue = new TransferCodingWithQualityHeaderValue();
			transferCodingWithQualityHeaderValue.value = lexer.GetStringValue(t);
			t = lexer.Scan(false);
			if (t == Token.Type.SeparatorSemicolon && (!NameValueHeaderValue.TryParseParameters(lexer, out transferCodingWithQualityHeaderValue.parameters, out t) || t != Token.Type.End))
			{
				return false;
			}
			parsedValue = transferCodingWithQualityHeaderValue;
			return true;
		}
	}
}
