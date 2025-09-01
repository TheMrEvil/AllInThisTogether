using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the value of the Content-Range header.</summary>
	// Token: 0x0200003A RID: 58
	public class ContentRangeHeaderValue : ICloneable
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00008437 File Offset: 0x00006637
		private ContentRangeHeaderValue()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> class.</summary>
		/// <param name="length">The starting or ending point of the range, in bytes.</param>
		// Token: 0x060001FC RID: 508 RVA: 0x0000844A File Offset: 0x0000664A
		public ContentRangeHeaderValue(long length)
		{
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this.Length = new long?(length);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> class.</summary>
		/// <param name="from">The position, in bytes, at which to start sending data.</param>
		/// <param name="to">The position, in bytes, at which to stop sending data.</param>
		// Token: 0x060001FD RID: 509 RVA: 0x00008479 File Offset: 0x00006679
		public ContentRangeHeaderValue(long from, long to)
		{
			if (from < 0L || from > to)
			{
				throw new ArgumentOutOfRangeException("from");
			}
			this.From = new long?(from);
			this.To = new long?(to);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> class.</summary>
		/// <param name="from">The position, in bytes, at which to start sending data.</param>
		/// <param name="to">The position, in bytes, at which to stop sending data.</param>
		/// <param name="length">The starting or ending point of the range, in bytes.</param>
		// Token: 0x060001FE RID: 510 RVA: 0x000084B8 File Offset: 0x000066B8
		public ContentRangeHeaderValue(long from, long to, long length) : this(from, to)
		{
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (to > length)
			{
				throw new ArgumentOutOfRangeException("to");
			}
			this.Length = new long?(length);
		}

		/// <summary>Gets the position at which to start sending data.</summary>
		/// <returns>The position, in bytes, at which to start sending data.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000084ED File Offset: 0x000066ED
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000084F5 File Offset: 0x000066F5
		public long? From
		{
			[CompilerGenerated]
			get
			{
				return this.<From>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<From>k__BackingField = value;
			}
		}

		/// <summary>Gets whether the Content-Range header has a length specified.</summary>
		/// <returns>
		///   <see langword="true" /> if the Content-Range has a length specified; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008500 File Offset: 0x00006700
		public bool HasLength
		{
			get
			{
				return this.Length != null;
			}
		}

		/// <summary>Gets whether the Content-Range has a range specified.</summary>
		/// <returns>
		///   <see langword="true" /> if the Content-Range has a range specified; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000851C File Offset: 0x0000671C
		public bool HasRange
		{
			get
			{
				return this.From != null;
			}
		}

		/// <summary>Gets the length of the full entity-body.</summary>
		/// <returns>The length of the full entity-body.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008537 File Offset: 0x00006737
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000853F File Offset: 0x0000673F
		public long? Length
		{
			[CompilerGenerated]
			get
			{
				return this.<Length>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Length>k__BackingField = value;
			}
		}

		/// <summary>Gets the position at which to stop sending data.</summary>
		/// <returns>The position at which to stop sending data.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008548 File Offset: 0x00006748
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00008550 File Offset: 0x00006750
		public long? To
		{
			[CompilerGenerated]
			get
			{
				return this.<To>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<To>k__BackingField = value;
			}
		}

		/// <summary>The range units used.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains range units.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008559 File Offset: 0x00006759
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00008561 File Offset: 0x00006761
		public string Unit
		{
			get
			{
				return this.unit;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Unit");
				}
				Parser.Token.Check(value);
				this.unit = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x06000209 RID: 521 RVA: 0x00006AEE File Offset: 0x00004CEE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified Object is equal to the current <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600020A RID: 522 RVA: 0x00008580 File Offset: 0x00006780
		public override bool Equals(object obj)
		{
			ContentRangeHeaderValue contentRangeHeaderValue = obj as ContentRangeHeaderValue;
			if (contentRangeHeaderValue == null)
			{
				return false;
			}
			long? num = contentRangeHeaderValue.Length;
			long? num2 = this.Length;
			if (num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null))
			{
				num2 = contentRangeHeaderValue.From;
				num = this.From;
				if (num2.GetValueOrDefault() == num.GetValueOrDefault() & num2 != null == (num != null))
				{
					num = contentRangeHeaderValue.To;
					num2 = this.To;
					if (num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null))
					{
						return string.Equals(contentRangeHeaderValue.unit, this.unit, StringComparison.OrdinalIgnoreCase);
					}
				}
			}
			return false;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x0600020B RID: 523 RVA: 0x00008640 File Offset: 0x00006840
		public override int GetHashCode()
		{
			return this.Unit.GetHashCode() ^ this.Length.GetHashCode() ^ this.From.GetHashCode() ^ this.To.GetHashCode() ^ this.unit.ToLowerInvariant().GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents content range header value information.</param>
		/// <returns>An <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid content range header value information.</exception>
		// Token: 0x0600020C RID: 524 RVA: 0x000086A8 File Offset: 0x000068A8
		public static ContentRangeHeaderValue Parse(string input)
		{
			ContentRangeHeaderValue result;
			if (ContentRangeHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600020D RID: 525 RVA: 0x000086C8 File Offset: 0x000068C8
		public static bool TryParse(string input, out ContentRangeHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan(false);
			if (token != Token.Type.Token)
			{
				return false;
			}
			ContentRangeHeaderValue contentRangeHeaderValue = new ContentRangeHeaderValue();
			contentRangeHeaderValue.unit = lexer.GetStringValue(token);
			token = lexer.Scan(false);
			if (token != Token.Type.Token)
			{
				return false;
			}
			if (!lexer.IsStarStringValue(token))
			{
				long value;
				if (!lexer.TryGetNumericValue(token, out value))
				{
					string stringValue = lexer.GetStringValue(token);
					if (stringValue.Length < 3)
					{
						return false;
					}
					string[] array = stringValue.Split('-', StringSplitOptions.None);
					if (array.Length != 2)
					{
						return false;
					}
					if (!long.TryParse(array[0], NumberStyles.None, CultureInfo.InvariantCulture, out value))
					{
						return false;
					}
					contentRangeHeaderValue.From = new long?(value);
					if (!long.TryParse(array[1], NumberStyles.None, CultureInfo.InvariantCulture, out value))
					{
						return false;
					}
					contentRangeHeaderValue.To = new long?(value);
				}
				else
				{
					contentRangeHeaderValue.From = new long?(value);
					token = lexer.Scan(true);
					if (token != Token.Type.SeparatorDash)
					{
						return false;
					}
					token = lexer.Scan(false);
					if (!lexer.TryGetNumericValue(token, out value))
					{
						return false;
					}
					contentRangeHeaderValue.To = new long?(value);
				}
			}
			token = lexer.Scan(false);
			if (token != Token.Type.SeparatorSlash)
			{
				return false;
			}
			token = lexer.Scan(false);
			if (!lexer.IsStarStringValue(token))
			{
				long value2;
				if (!lexer.TryGetNumericValue(token, out value2))
				{
					return false;
				}
				contentRangeHeaderValue.Length = new long?(value2);
			}
			token = lexer.Scan(false);
			if (token != Token.Type.End)
			{
				return false;
			}
			parsedValue = contentRangeHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ContentRangeHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x0600020E RID: 526 RVA: 0x00008834 File Offset: 0x00006A34
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.unit);
			stringBuilder.Append(" ");
			if (this.From == null)
			{
				stringBuilder.Append("*");
			}
			else
			{
				stringBuilder.Append(this.From.Value.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append("-");
				stringBuilder.Append(this.To.Value.ToString(CultureInfo.InvariantCulture));
			}
			stringBuilder.Append("/");
			stringBuilder.Append((this.Length == null) ? "*" : this.Length.Value.ToString(CultureInfo.InvariantCulture));
			return stringBuilder.ToString();
		}

		// Token: 0x040000ED RID: 237
		private string unit = "bytes";

		// Token: 0x040000EE RID: 238
		[CompilerGenerated]
		private long? <From>k__BackingField;

		// Token: 0x040000EF RID: 239
		[CompilerGenerated]
		private long? <Length>k__BackingField;

		// Token: 0x040000F0 RID: 240
		[CompilerGenerated]
		private long? <To>k__BackingField;
	}
}
