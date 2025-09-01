using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a media type with an additional quality factor used in a Content-Type header.</summary>
	// Token: 0x02000051 RID: 81
	public sealed class MediaTypeWithQualityHeaderValue : MediaTypeHeaderValue
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> class.</summary>
		/// <param name="mediaType">A <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> represented as string to initialize the new instance.</param>
		// Token: 0x0600031A RID: 794 RVA: 0x0000B1A0 File Offset: 0x000093A0
		public MediaTypeWithQualityHeaderValue(string mediaType) : base(mediaType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> class.</summary>
		/// <param name="mediaType">A <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> represented as string to initialize the new instance.</param>
		/// <param name="quality">The quality associated with this header value.</param>
		// Token: 0x0600031B RID: 795 RVA: 0x0000B1A9 File Offset: 0x000093A9
		public MediaTypeWithQualityHeaderValue(string mediaType, double quality) : this(mediaType)
		{
			this.Quality = new double?(quality);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B1BE File Offset: 0x000093BE
		private MediaTypeWithQualityHeaderValue()
		{
		}

		/// <summary>Gets or sets the quality value for the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" />.</summary>
		/// <returns>The quality value for the <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> object.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000B1C6 File Offset: 0x000093C6
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000B1D3 File Offset: 0x000093D3
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

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents media type with quality header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid media type with quality header value information.</exception>
		// Token: 0x0600031F RID: 799 RVA: 0x0000B1E4 File Offset: 0x000093E4
		public new static MediaTypeWithQualityHeaderValue Parse(string input)
		{
			MediaTypeWithQualityHeaderValue result;
			if (MediaTypeWithQualityHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.MediaTypeWithQualityHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000320 RID: 800 RVA: 0x0000B204 File Offset: 0x00009404
		public static bool TryParse(string input, out MediaTypeWithQualityHeaderValue parsedValue)
		{
			Token token;
			if (MediaTypeWithQualityHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B230 File Offset: 0x00009430
		private static bool TryParseElement(Lexer lexer, out MediaTypeWithQualityHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			List<NameValueHeaderValue> parameters = null;
			string media_type;
			Token? token = MediaTypeHeaderValue.TryParseMediaType(lexer, out media_type);
			if (token == null)
			{
				t = Token.Empty;
				return false;
			}
			t = token.Value;
			if (t == Token.Type.SeparatorSemicolon && !NameValueHeaderValue.TryParseParameters(lexer, out parameters, out t))
			{
				return false;
			}
			parsedValue = new MediaTypeWithQualityHeaderValue();
			parsedValue.media_type = media_type;
			parsedValue.parameters = parameters;
			return true;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B2A1 File Offset: 0x000094A1
		internal static bool TryParse(string input, int minimalCount, out List<MediaTypeWithQualityHeaderValue> result)
		{
			return CollectionParser.TryParse<MediaTypeWithQualityHeaderValue>(input, minimalCount, new ElementTryParser<MediaTypeWithQualityHeaderValue>(MediaTypeWithQualityHeaderValue.TryParseElement), out result);
		}
	}
}
