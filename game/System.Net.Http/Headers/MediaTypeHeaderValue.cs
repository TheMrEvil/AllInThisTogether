using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a media type used in a Content-Type header as defined in the RFC 2616.</summary>
	// Token: 0x0200004F RID: 79
	public class MediaTypeHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> class.</summary>
		/// <param name="mediaType">The source represented as a string to initialize the new instance.</param>
		// Token: 0x06000308 RID: 776 RVA: 0x0000AE3F File Offset: 0x0000903F
		public MediaTypeHeaderValue(string mediaType)
		{
			this.MediaType = mediaType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object used to initialize the new instance.</param>
		// Token: 0x06000309 RID: 777 RVA: 0x0000AE50 File Offset: 0x00009050
		protected MediaTypeHeaderValue(MediaTypeHeaderValue source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.media_type = source.media_type;
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue source2 in source.parameters)
				{
					this.Parameters.Add(new NameValueHeaderValue(source2));
				}
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000022B8 File Offset: 0x000004B8
		internal MediaTypeHeaderValue()
		{
		}

		/// <summary>Gets or sets the character set.</summary>
		/// <returns>The character set.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000AED8 File Offset: 0x000090D8
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000AF25 File Offset: 0x00009125
		public string CharSet
		{
			get
			{
				if (this.parameters == null)
				{
					return null;
				}
				NameValueHeaderValue nameValueHeaderValue = this.parameters.Find((NameValueHeaderValue l) => string.Equals(l.Name, "charset", StringComparison.OrdinalIgnoreCase));
				if (nameValueHeaderValue == null)
				{
					return null;
				}
				return nameValueHeaderValue.Value;
			}
			set
			{
				if (this.parameters == null)
				{
					this.parameters = new List<NameValueHeaderValue>();
				}
				this.parameters.SetValue("charset", value);
			}
		}

		/// <summary>Gets or sets the media-type header value.</summary>
		/// <returns>The media-type header value.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000AF4B File Offset: 0x0000914B
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000AF54 File Offset: 0x00009154
		public string MediaType
		{
			get
			{
				return this.media_type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("MediaType");
				}
				string text;
				Token? token = MediaTypeHeaderValue.TryParseMediaType(new Lexer(value), out text);
				if (token == null || token.Value.Kind != Token.Type.End)
				{
					throw new FormatException();
				}
				this.media_type = text;
			}
		}

		/// <summary>Gets or sets the media-type header value parameters.</summary>
		/// <returns>The media-type header value parameters.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public ICollection<NameValueHeaderValue> Parameters
		{
			get
			{
				List<NameValueHeaderValue> result;
				if ((result = this.parameters) == null)
				{
					result = (this.parameters = new List<NameValueHeaderValue>());
				}
				return result;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x06000310 RID: 784 RVA: 0x0000AFCD File Offset: 0x000091CD
		object ICloneable.Clone()
		{
			return new MediaTypeHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000311 RID: 785 RVA: 0x0000AFD8 File Offset: 0x000091D8
		public override bool Equals(object obj)
		{
			MediaTypeHeaderValue mediaTypeHeaderValue = obj as MediaTypeHeaderValue;
			return mediaTypeHeaderValue != null && string.Equals(mediaTypeHeaderValue.media_type, this.media_type, StringComparison.OrdinalIgnoreCase) && mediaTypeHeaderValue.parameters.SequenceEqual(this.parameters);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06000312 RID: 786 RVA: 0x0000B018 File Offset: 0x00009218
		public override int GetHashCode()
		{
			return this.media_type.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate<NameValueHeaderValue>(this.parameters);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents media type header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid media type header value information.</exception>
		// Token: 0x06000313 RID: 787 RVA: 0x0000B038 File Offset: 0x00009238
		public static MediaTypeHeaderValue Parse(string input)
		{
			MediaTypeHeaderValue result;
			if (MediaTypeHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06000314 RID: 788 RVA: 0x0000B057 File Offset: 0x00009257
		public override string ToString()
		{
			if (this.parameters == null)
			{
				return this.media_type;
			}
			return this.media_type + this.parameters.ToString<NameValueHeaderValue>();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.MediaTypeHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000315 RID: 789 RVA: 0x0000B080 File Offset: 0x00009280
		public static bool TryParse(string input, out MediaTypeHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			List<NameValueHeaderValue> list = null;
			string text;
			Token? token = MediaTypeHeaderValue.TryParseMediaType(lexer, out text);
			if (token == null)
			{
				return false;
			}
			Token.Type kind = token.Value.Kind;
			if (kind != Token.Type.End)
			{
				if (kind != Token.Type.SeparatorSemicolon)
				{
					return false;
				}
				Token token2;
				if (!NameValueHeaderValue.TryParseParameters(lexer, out list, out token2) || token2 != Token.Type.End)
				{
					return false;
				}
			}
			parsedValue = new MediaTypeHeaderValue
			{
				media_type = text,
				parameters = list
			};
			return true;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B0FC File Offset: 0x000092FC
		internal static Token? TryParseMediaType(Lexer lexer, out string media)
		{
			media = null;
			Token token = lexer.Scan(false);
			if (token != Token.Type.Token)
			{
				return null;
			}
			if (lexer.Scan(false) != Token.Type.SeparatorSlash)
			{
				return null;
			}
			Token token2 = lexer.Scan(false);
			if (token2 != Token.Type.Token)
			{
				return null;
			}
			media = lexer.GetStringValue(token) + "/" + lexer.GetStringValue(token2);
			return new Token?(lexer.Scan(false));
		}

		// Token: 0x04000133 RID: 307
		internal List<NameValueHeaderValue> parameters;

		// Token: 0x04000134 RID: 308
		internal string media_type;

		// Token: 0x02000050 RID: 80
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000317 RID: 791 RVA: 0x0000B181 File Offset: 0x00009381
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000318 RID: 792 RVA: 0x000022B8 File Offset: 0x000004B8
			public <>c()
			{
			}

			// Token: 0x06000319 RID: 793 RVA: 0x0000B18D File Offset: 0x0000938D
			internal bool <get_CharSet>b__6_0(NameValueHeaderValue l)
			{
				return string.Equals(l.Name, "charset", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000135 RID: 309
			public static readonly MediaTypeHeaderValue.<>c <>9 = new MediaTypeHeaderValue.<>c();

			// Token: 0x04000136 RID: 310
			public static Predicate<NameValueHeaderValue> <>9__6_0;
		}
	}
}
