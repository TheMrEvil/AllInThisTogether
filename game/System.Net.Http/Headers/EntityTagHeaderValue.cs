using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents an entity-tag header value.</summary>
	// Token: 0x0200003B RID: 59
	public class EntityTagHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> class.</summary>
		/// <param name="tag">A string that contains an <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.</param>
		// Token: 0x0600020F RID: 527 RVA: 0x00008912 File Offset: 0x00006B12
		public EntityTagHeaderValue(string tag)
		{
			Parser.Token.CheckQuotedString(tag);
			this.Tag = tag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> class.</summary>
		/// <param name="tag">A string that contains an  <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.</param>
		/// <param name="isWeak">A value that indicates if this entity-tag header is a weak validator. If the entity-tag header is weak validator, then <paramref name="isWeak" /> should be set to <see langword="true" />. If the entity-tag header is a strong validator, then <paramref name="isWeak" /> should be set to <see langword="false" />.</param>
		// Token: 0x06000210 RID: 528 RVA: 0x00008927 File Offset: 0x00006B27
		public EntityTagHeaderValue(string tag, bool isWeak) : this(tag)
		{
			this.IsWeak = isWeak;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000022B8 File Offset: 0x000004B8
		internal EntityTagHeaderValue()
		{
		}

		/// <summary>Gets the entity-tag header value.</summary>
		/// <returns>Returns <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" />.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008937 File Offset: 0x00006B37
		public static EntityTagHeaderValue Any
		{
			get
			{
				return EntityTagHeaderValue.any;
			}
		}

		/// <summary>Gets whether the entity-tag is prefaced by a weakness indicator.</summary>
		/// <returns>
		///   <see langword="true" /> if the entity-tag is prefaced by a weakness indicator; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000893E File Offset: 0x00006B3E
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00008946 File Offset: 0x00006B46
		public bool IsWeak
		{
			[CompilerGenerated]
			get
			{
				return this.<IsWeak>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<IsWeak>k__BackingField = value;
			}
		}

		/// <summary>Gets the opaque quoted string.</summary>
		/// <returns>An opaque quoted string.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000894F File Offset: 0x00006B4F
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00008957 File Offset: 0x00006B57
		public string Tag
		{
			[CompilerGenerated]
			get
			{
				return this.<Tag>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Tag>k__BackingField = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x06000217 RID: 535 RVA: 0x00006AEE File Offset: 0x00004CEE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000218 RID: 536 RVA: 0x00008960 File Offset: 0x00006B60
		public override bool Equals(object obj)
		{
			EntityTagHeaderValue entityTagHeaderValue = obj as EntityTagHeaderValue;
			return entityTagHeaderValue != null && entityTagHeaderValue.Tag == this.Tag && string.Equals(entityTagHeaderValue.Tag, this.Tag, StringComparison.Ordinal);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06000219 RID: 537 RVA: 0x000089A0 File Offset: 0x00006BA0
		public override int GetHashCode()
		{
			return this.IsWeak.GetHashCode() ^ this.Tag.GetHashCode();
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents entity tag header value information.</param>
		/// <returns>An <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid entity tag header value information.</exception>
		// Token: 0x0600021A RID: 538 RVA: 0x000089C8 File Offset: 0x00006BC8
		public static EntityTagHeaderValue Parse(string input)
		{
			EntityTagHeaderValue result;
			if (EntityTagHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600021B RID: 539 RVA: 0x000089E8 File Offset: 0x00006BE8
		public static bool TryParse(string input, out EntityTagHeaderValue parsedValue)
		{
			Token token;
			if (EntityTagHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008A14 File Offset: 0x00006C14
		private static bool TryParseElement(Lexer lexer, out EntityTagHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			bool isWeak = false;
			if (t == Token.Type.Token)
			{
				string stringValue = lexer.GetStringValue(t);
				if (stringValue == "*")
				{
					parsedValue = EntityTagHeaderValue.any;
					t = lexer.Scan(false);
					return true;
				}
				if (stringValue != "W" || lexer.PeekChar() != 47)
				{
					return false;
				}
				isWeak = true;
				lexer.EatChar();
				t = lexer.Scan(false);
			}
			if (t != Token.Type.QuotedString)
			{
				return false;
			}
			parsedValue = new EntityTagHeaderValue();
			parsedValue.Tag = lexer.GetStringValue(t);
			parsedValue.IsWeak = isWeak;
			t = lexer.Scan(false);
			return true;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008ADF File Offset: 0x00006CDF
		internal static bool TryParse(string input, int minimalCount, out List<EntityTagHeaderValue> result)
		{
			return CollectionParser.TryParse<EntityTagHeaderValue>(input, minimalCount, new ElementTryParser<EntityTagHeaderValue>(EntityTagHeaderValue.TryParseElement), out result);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.EntityTagHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x0600021E RID: 542 RVA: 0x00008AF5 File Offset: 0x00006CF5
		public override string ToString()
		{
			if (!this.IsWeak)
			{
				return this.Tag;
			}
			return "W/" + this.Tag;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008B16 File Offset: 0x00006D16
		// Note: this type is marked as 'beforefieldinit'.
		static EntityTagHeaderValue()
		{
		}

		// Token: 0x040000F1 RID: 241
		private static readonly EntityTagHeaderValue any = new EntityTagHeaderValue
		{
			Tag = "*"
		};

		// Token: 0x040000F2 RID: 242
		[CompilerGenerated]
		private bool <IsWeak>k__BackingField;

		// Token: 0x040000F3 RID: 243
		[CompilerGenerated]
		private string <Tag>k__BackingField;
	}
}
