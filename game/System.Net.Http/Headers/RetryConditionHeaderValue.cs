using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents a Retry-After header value which can either be a date/time or a timespan value.</summary>
	// Token: 0x02000067 RID: 103
	public class RetryConditionHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> class.</summary>
		/// <param name="date">The date and time offset used to initialize the new instance.</param>
		// Token: 0x0600039E RID: 926 RVA: 0x0000C8F1 File Offset: 0x0000AAF1
		public RetryConditionHeaderValue(DateTimeOffset date)
		{
			this.Date = new DateTimeOffset?(date);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> class.</summary>
		/// <param name="delta">The delta, in seconds, used to initialize the new instance.</param>
		// Token: 0x0600039F RID: 927 RVA: 0x0000C905 File Offset: 0x0000AB05
		public RetryConditionHeaderValue(TimeSpan delta)
		{
			if (delta.TotalSeconds > 4294967295.0)
			{
				throw new ArgumentOutOfRangeException("delta");
			}
			this.Delta = new TimeSpan?(delta);
		}

		/// <summary>Gets the date and time offset from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>The date and time offset from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000C936 File Offset: 0x0000AB36
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000C93E File Offset: 0x0000AB3E
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

		/// <summary>Gets the delta in seconds from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>The delta in seconds from the <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000C947 File Offset: 0x0000AB47
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000C94F File Offset: 0x0000AB4F
		public TimeSpan? Delta
		{
			[CompilerGenerated]
			get
			{
				return this.<Delta>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Delta>k__BackingField = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x060003A4 RID: 932 RVA: 0x00006AEE File Offset: 0x00004CEE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003A5 RID: 933 RVA: 0x0000C958 File Offset: 0x0000AB58
		public override bool Equals(object obj)
		{
			RetryConditionHeaderValue retryConditionHeaderValue = obj as RetryConditionHeaderValue;
			return retryConditionHeaderValue != null && retryConditionHeaderValue.Date == this.Date && retryConditionHeaderValue.Delta == this.Delta;
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x060003A6 RID: 934 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		public override int GetHashCode()
		{
			return this.Date.GetHashCode() ^ this.Delta.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents retry condition header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid retry condition header value information.</exception>
		// Token: 0x060003A7 RID: 935 RVA: 0x0000CA30 File Offset: 0x0000AC30
		public static RetryConditionHeaderValue Parse(string input)
		{
			RetryConditionHeaderValue result;
			if (RetryConditionHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003A8 RID: 936 RVA: 0x0000CA50 File Offset: 0x0000AC50
		public static bool TryParse(string input, out RetryConditionHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan(false);
			if (token != Token.Type.Token)
			{
				return false;
			}
			TimeSpan? timeSpan = lexer.TryGetTimeSpanValue(token);
			if (timeSpan != null)
			{
				if (lexer.Scan(false) != Token.Type.End)
				{
					return false;
				}
				parsedValue = new RetryConditionHeaderValue(timeSpan.Value);
			}
			else
			{
				DateTimeOffset date;
				if (!Lexer.TryGetDateValue(input, out date))
				{
					return false;
				}
				parsedValue = new RetryConditionHeaderValue(date);
			}
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RetryConditionHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060003A9 RID: 937 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		public override string ToString()
		{
			if (this.Delta == null)
			{
				return this.Date.Value.ToString("r", CultureInfo.InvariantCulture);
			}
			return this.Delta.Value.TotalSeconds.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0400014A RID: 330
		[CompilerGenerated]
		private DateTimeOffset? <Date>k__BackingField;

		// Token: 0x0400014B RID: 331
		[CompilerGenerated]
		private TimeSpan? <Delta>k__BackingField;
	}
}
