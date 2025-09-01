using System;

namespace System
{
	/// <summary>Describes the console key that was pressed, including the character represented by the console key and the state of the SHIFT, ALT, and CTRL modifier keys.</summary>
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public readonly struct ConsoleKeyInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ConsoleKeyInfo" /> structure using the specified character, console key, and modifier keys.</summary>
		/// <param name="keyChar">The Unicode character that corresponds to the <paramref name="key" /> parameter.</param>
		/// <param name="key">The console key that corresponds to the <paramref name="keyChar" /> parameter.</param>
		/// <param name="shift">
		///   <see langword="true" /> to indicate that a SHIFT key was pressed; otherwise, <see langword="false" />.</param>
		/// <param name="alt">
		///   <see langword="true" /> to indicate that an ALT key was pressed; otherwise, <see langword="false" />.</param>
		/// <param name="control">
		///   <see langword="true" /> to indicate that a CTRL key was pressed; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The numeric value of the <paramref name="key" /> parameter is less than 0 or greater than 255.</exception>
		// Token: 0x06001339 RID: 4921 RVA: 0x0004D888 File Offset: 0x0004BA88
		public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control)
		{
			if (key < (ConsoleKey)0 || key > (ConsoleKey)255)
			{
				throw new ArgumentOutOfRangeException("key", "Console key values must be between 0 and 255 inclusive.");
			}
			this._keyChar = keyChar;
			this._key = key;
			this._mods = (ConsoleModifiers)0;
			if (shift)
			{
				this._mods |= ConsoleModifiers.Shift;
			}
			if (alt)
			{
				this._mods |= ConsoleModifiers.Alt;
			}
			if (control)
			{
				this._mods |= ConsoleModifiers.Control;
			}
		}

		/// <summary>Gets the Unicode character represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>An object that corresponds to the console key represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0004D8FB File Offset: 0x0004BAFB
		public char KeyChar
		{
			get
			{
				return this._keyChar;
			}
		}

		/// <summary>Gets the console key represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>A value that identifies the console key that was pressed.</returns>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0004D903 File Offset: 0x0004BB03
		public ConsoleKey Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Gets a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values that specifies one or more modifier keys pressed simultaneously with the console key.</summary>
		/// <returns>A bitwise combination of the enumeration values. There is no default value.</returns>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0004D90B File Offset: 0x0004BB0B
		public ConsoleModifiers Modifiers
		{
			get
			{
				return this._mods;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <param name="value">An object to compare to the current <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.ConsoleKeyInfo" /> object and is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600133D RID: 4925 RVA: 0x0004D913 File Offset: 0x0004BB13
		public override bool Equals(object value)
		{
			return value is ConsoleKeyInfo && this.Equals((ConsoleKeyInfo)value);
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.ConsoleKeyInfo" /> object is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <param name="obj">An object to compare to the current <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600133E RID: 4926 RVA: 0x0004D92B File Offset: 0x0004BB2B
		public bool Equals(ConsoleKeyInfo obj)
		{
			return obj._keyChar == this._keyChar && obj._key == this._key && obj._mods == this._mods;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.ConsoleKeyInfo" /> objects are equal.</summary>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600133F RID: 4927 RVA: 0x0004D959 File Offset: 0x0004BB59
		public static bool operator ==(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.ConsoleKeyInfo" /> objects are not equal.</summary>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001340 RID: 4928 RVA: 0x0004D963 File Offset: 0x0004BB63
		public static bool operator !=(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return !(a == b);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001341 RID: 4929 RVA: 0x0004D96F File Offset: 0x0004BB6F
		public override int GetHashCode()
		{
			return (int)((ConsoleKey)this._keyChar | (int)this._key << 16 | (ConsoleKey)((int)this._mods << 24));
		}

		// Token: 0x04001439 RID: 5177
		private readonly char _keyChar;

		// Token: 0x0400143A RID: 5178
		private readonly ConsoleKey _key;

		// Token: 0x0400143B RID: 5179
		private readonly ConsoleModifiers _mods;
	}
}
