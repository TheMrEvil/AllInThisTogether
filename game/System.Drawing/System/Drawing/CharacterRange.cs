using System;

namespace System.Drawing
{
	/// <summary>Specifies a range of character positions within a string.</summary>
	// Token: 0x02000052 RID: 82
	public struct CharacterRange
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.CharacterRange" /> structure, specifying a range of character positions within a string.</summary>
		/// <param name="First">The position of the first character in the range. For example, if <paramref name="First" /> is set to 0, the first position of the range is position 0 in the string.</param>
		/// <param name="Length">The number of positions in the range.</param>
		// Token: 0x060003B4 RID: 948 RVA: 0x00009C00 File Offset: 0x00007E00
		public CharacterRange(int First, int Length)
		{
			this.first = First;
			this.length = Length;
		}

		/// <summary>Gets or sets the position in the string of the first character of this <see cref="T:System.Drawing.CharacterRange" />.</summary>
		/// <returns>The first position of this <see cref="T:System.Drawing.CharacterRange" />.</returns>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00009C10 File Offset: 0x00007E10
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00009C18 File Offset: 0x00007E18
		public int First
		{
			get
			{
				return this.first;
			}
			set
			{
				this.first = value;
			}
		}

		/// <summary>Gets or sets the number of positions in this <see cref="T:System.Drawing.CharacterRange" />.</summary>
		/// <returns>The number of positions in this <see cref="T:System.Drawing.CharacterRange" />.</returns>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00009C21 File Offset: 0x00007E21
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00009C29 File Offset: 0x00007E29
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		/// <summary>Gets a value indicating whether this object is equivalent to the specified object.</summary>
		/// <param name="obj">The object to compare to for equality.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the specified object is an instance with the same <see cref="P:System.Drawing.CharacterRange.First" /> and <see cref="P:System.Drawing.CharacterRange.Length" /> value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003B9 RID: 953 RVA: 0x00009C34 File Offset: 0x00007E34
		public override bool Equals(object obj)
		{
			if (!(obj is CharacterRange))
			{
				return false;
			}
			CharacterRange cr = (CharacterRange)obj;
			return this == cr;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		// Token: 0x060003BA RID: 954 RVA: 0x00009C5E File Offset: 0x00007E5E
		public override int GetHashCode()
		{
			return this.first ^ this.length;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.CharacterRange" /> objects. Gets a value indicating whether the <see cref="P:System.Drawing.CharacterRange.First" /> and <see cref="P:System.Drawing.CharacterRange.Length" /> values of the two <see cref="T:System.Drawing.CharacterRange" /> objects are equal.</summary>
		/// <param name="cr1">A <see cref="T:System.Drawing.CharacterRange" /> to compare for equality.</param>
		/// <param name="cr2">A <see cref="T:System.Drawing.CharacterRange" /> to compare for equality.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the two <see cref="T:System.Drawing.CharacterRange" /> objects have the same <see cref="P:System.Drawing.CharacterRange.First" /> and <see cref="P:System.Drawing.CharacterRange.Length" /> values; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003BB RID: 955 RVA: 0x00009C6D File Offset: 0x00007E6D
		public static bool operator ==(CharacterRange cr1, CharacterRange cr2)
		{
			return cr1.first == cr2.first && cr1.length == cr2.length;
		}

		/// <summary>Compares two <see cref="T:System.Drawing.CharacterRange" /> objects. Gets a value indicating whether the <see cref="P:System.Drawing.CharacterRange.First" /> or <see cref="P:System.Drawing.CharacterRange.Length" /> values of the two <see cref="T:System.Drawing.CharacterRange" /> objects are not equal.</summary>
		/// <param name="cr1">A <see cref="T:System.Drawing.CharacterRange" /> to compare for inequality.</param>
		/// <param name="cr2">A <see cref="T:System.Drawing.CharacterRange" /> to compare for inequality.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the either the <see cref="P:System.Drawing.CharacterRange.First" /> or <see cref="P:System.Drawing.CharacterRange.Length" /> values of the two <see cref="T:System.Drawing.CharacterRange" /> objects differ; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003BC RID: 956 RVA: 0x00009C8D File Offset: 0x00007E8D
		public static bool operator !=(CharacterRange cr1, CharacterRange cr2)
		{
			return cr1.first != cr2.first || cr1.length != cr2.length;
		}

		// Token: 0x04000434 RID: 1076
		private int first;

		// Token: 0x04000435 RID: 1077
		private int length;
	}
}
