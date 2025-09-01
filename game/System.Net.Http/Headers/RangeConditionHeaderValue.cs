using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents an If-Range header value which can either be a date/time or an entity-tag value.</summary>
	// Token: 0x02000064 RID: 100
	public class RangeConditionHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> class.</summary>
		/// <param name="date">A date value used to initialize the new instance.</param>
		// Token: 0x0600037C RID: 892 RVA: 0x0000C09B File Offset: 0x0000A29B
		public RangeConditionHeaderValue(DateTimeOffset date)
		{
			this.Date = new DateTimeOffset?(date);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> class.</summary>
		/// <param name="entityTag">An <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object used to initialize the new instance.</param>
		// Token: 0x0600037D RID: 893 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		public RangeConditionHeaderValue(EntityTagHeaderValue entityTag)
		{
			if (entityTag == null)
			{
				throw new ArgumentNullException("entityTag");
			}
			this.EntityTag = entityTag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> class.</summary>
		/// <param name="entityTag">An entity tag represented as a string used to initialize the new instance.</param>
		// Token: 0x0600037E RID: 894 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public RangeConditionHeaderValue(string entityTag) : this(new EntityTagHeaderValue(entityTag))
		{
		}

		/// <summary>Gets the date from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>The date from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000C0DA File Offset: 0x0000A2DA
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000C0E2 File Offset: 0x0000A2E2
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

		/// <summary>Gets the entity tag from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>The entity tag from the <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000C0EB File Offset: 0x0000A2EB
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
		public EntityTagHeaderValue EntityTag
		{
			[CompilerGenerated]
			get
			{
				return this.<EntityTag>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EntityTag>k__BackingField = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x06000383 RID: 899 RVA: 0x00006AEE File Offset: 0x00004CEE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000384 RID: 900 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		public override bool Equals(object obj)
		{
			RangeConditionHeaderValue rangeConditionHeaderValue = obj as RangeConditionHeaderValue;
			if (rangeConditionHeaderValue == null)
			{
				return false;
			}
			if (this.EntityTag == null)
			{
				return this.Date == rangeConditionHeaderValue.Date;
			}
			return this.EntityTag.Equals(rangeConditionHeaderValue.EntityTag);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06000385 RID: 901 RVA: 0x0000C170 File Offset: 0x0000A370
		public override int GetHashCode()
		{
			if (this.EntityTag == null)
			{
				return this.Date.GetHashCode();
			}
			return this.EntityTag.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents range condition header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid range Condition header value information.</exception>
		// Token: 0x06000386 RID: 902 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
		public static RangeConditionHeaderValue Parse(string input)
		{
			RangeConditionHeaderValue result;
			if (RangeConditionHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000387 RID: 903 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public static bool TryParse(string input, out RangeConditionHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan(false);
			bool isWeak;
			if (token == Token.Type.Token)
			{
				if (lexer.GetStringValue(token) != "W")
				{
					DateTimeOffset date;
					if (!Lexer.TryGetDateValue(input, out date))
					{
						return false;
					}
					parsedValue = new RangeConditionHeaderValue(date);
					return true;
				}
				else
				{
					if (lexer.PeekChar() != 47)
					{
						return false;
					}
					isWeak = true;
					lexer.EatChar();
					token = lexer.Scan(false);
				}
			}
			else
			{
				isWeak = false;
			}
			if (token != Token.Type.QuotedString)
			{
				return false;
			}
			if (lexer.Scan(false) != Token.Type.End)
			{
				return false;
			}
			parsedValue = new RangeConditionHeaderValue(new EntityTagHeaderValue
			{
				Tag = lexer.GetStringValue(token),
				IsWeak = isWeak
			});
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.RangeConditionHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06000388 RID: 904 RVA: 0x0000C278 File Offset: 0x0000A478
		public override string ToString()
		{
			if (this.EntityTag != null)
			{
				return this.EntityTag.ToString();
			}
			return this.Date.Value.ToString("r", CultureInfo.InvariantCulture);
		}

		// Token: 0x04000144 RID: 324
		[CompilerGenerated]
		private DateTimeOffset? <Date>k__BackingField;

		// Token: 0x04000145 RID: 325
		[CompilerGenerated]
		private EntityTagHeaderValue <EntityTag>k__BackingField;
	}
}
