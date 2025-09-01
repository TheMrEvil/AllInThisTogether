using System;

namespace System.Xml
{
	/// <summary>Table of atomized string objects.</summary>
	// Token: 0x0200023C RID: 572
	public abstract class XmlNameTable
	{
		/// <summary>When overridden in a derived class, gets the atomized string containing the same characters as the specified range of characters in the given array.</summary>
		/// <param name="array">The character array containing the name to look up. </param>
		/// <param name="offset">The zero-based index into the array specifying the first character of the name. </param>
		/// <param name="length">The number of characters in the name. </param>
		/// <returns>The atomized string or <see langword="null" /> if the string has not already been atomized. If <paramref name="length" /> is zero, String.Empty is returned.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">0 &gt; <paramref name="offset" />-or- 
		///         <paramref name="offset" /> &gt;= <paramref name="array" />.Length -or- 
		///         <paramref name="length" /> &gt; <paramref name="array" />.Length The above conditions do not cause an exception to be thrown if <paramref name="length" /> =0. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="length" /> &lt; 0. </exception>
		// Token: 0x06001572 RID: 5490
		public abstract string Get(char[] array, int offset, int length);

		/// <summary>When overridden in a derived class, gets the atomized string containing the same value as the specified string.</summary>
		/// <param name="array">The name to look up. </param>
		/// <returns>The atomized string or <see langword="null" /> if the string has not already been atomized.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="array" /> is <see langword="null" />. </exception>
		// Token: 0x06001573 RID: 5491
		public abstract string Get(string array);

		/// <summary>When overridden in a derived class, atomizes the specified string and adds it to the <see langword="XmlNameTable" />.</summary>
		/// <param name="array">The character array containing the name to add. </param>
		/// <param name="offset">Zero-based index into the array specifying the first character of the name. </param>
		/// <param name="length">The number of characters in the name. </param>
		/// <returns>The new atomized string or the existing one if it already exists. If length is zero, String.Empty is returned.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">0 &gt; <paramref name="offset" />-or- 
		///         <paramref name="offset" /> &gt;= <paramref name="array" />.Length -or- 
		///         <paramref name="length" /> &gt; <paramref name="array" />.Length The above conditions do not cause an exception to be thrown if <paramref name="length" /> =0. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="length" /> &lt; 0. </exception>
		// Token: 0x06001574 RID: 5492
		public abstract string Add(char[] array, int offset, int length);

		/// <summary>When overridden in a derived class, atomizes the specified string and adds it to the <see langword="XmlNameTable" />.</summary>
		/// <param name="array">The name to add. </param>
		/// <returns>The new atomized string or the existing one if it already exists.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="array" /> is <see langword="null" />. </exception>
		// Token: 0x06001575 RID: 5493
		public abstract string Add(string array);

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlNameTable" /> class. </summary>
		// Token: 0x06001576 RID: 5494 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlNameTable()
		{
		}
	}
}
