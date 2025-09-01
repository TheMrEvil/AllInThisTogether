using System;

namespace System.Xml
{
	/// <summary>Implements a single-threaded <see cref="T:System.Xml.XmlNameTable" />.</summary>
	// Token: 0x020001E7 RID: 487
	public class NameTable : XmlNameTable
	{
		/// <summary>Initializes a new instance of the <see langword="NameTable" /> class.</summary>
		// Token: 0x0600132C RID: 4908 RVA: 0x00070D69 File Offset: 0x0006EF69
		public NameTable()
		{
			this.mask = 31;
			this.entries = new NameTable.Entry[this.mask + 1];
			this.hashCodeRandomizer = Environment.TickCount;
		}

		/// <summary>Atomizes the specified string and adds it to the <see langword="NameTable" />.</summary>
		/// <param name="key">The string to add. </param>
		/// <returns>The atomized string or the existing string if it already exists in the <see langword="NameTable" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="key" /> is <see langword="null" />. </exception>
		// Token: 0x0600132D RID: 4909 RVA: 0x00070D98 File Offset: 0x0006EF98
		public override string Add(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int length = key.Length;
			if (length == 0)
			{
				return string.Empty;
			}
			int num = length + this.hashCodeRandomizer;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7 ^ (int)key[i]);
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && entry.str.Equals(key))
				{
					return entry.str;
				}
			}
			return this.AddEntry(key, num);
		}

		/// <summary>Atomizes the specified string and adds it to the <see langword="NameTable" />.</summary>
		/// <param name="key">The character array containing the string to add. </param>
		/// <param name="start">The zero-based index into the array specifying the first character of the string. </param>
		/// <param name="len">The number of characters in the string. </param>
		/// <returns>The atomized string or the existing string if one already exists in the <see langword="NameTable" />. If <paramref name="len" /> is zero, String.Empty is returned.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">0 &gt; <paramref name="start" />-or- 
		///         <paramref name="start" /> &gt;= <paramref name="key" />.Length -or- 
		///         <paramref name="len" /> &gt;= <paramref name="key" />.Length The above conditions do not cause an exception to be thrown if <paramref name="len" /> =0. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="len" /> &lt; 0. </exception>
		// Token: 0x0600132E RID: 4910 RVA: 0x00070E44 File Offset: 0x0006F044
		public override string Add(char[] key, int start, int len)
		{
			if (len == 0)
			{
				return string.Empty;
			}
			int num = len + this.hashCodeRandomizer;
			num += (num << 7 ^ (int)key[start]);
			int num2 = start + len;
			for (int i = start + 1; i < num2; i++)
			{
				num += (num << 7 ^ (int)key[i]);
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && NameTable.TextEquals(entry.str, key, start, len))
				{
					return entry.str;
				}
			}
			return this.AddEntry(new string(key, start, len), num);
		}

		/// <summary>Gets the atomized string with the specified value.</summary>
		/// <param name="value">The name to find. </param>
		/// <returns>The atomized string object or <see langword="null" /> if the string has not already been atomized.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="value" /> is <see langword="null" />. </exception>
		// Token: 0x0600132F RID: 4911 RVA: 0x00070EE8 File Offset: 0x0006F0E8
		public override string Get(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				return string.Empty;
			}
			int num = value.Length + this.hashCodeRandomizer;
			for (int i = 0; i < value.Length; i++)
			{
				num += (num << 7 ^ (int)value[i]);
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && entry.str.Equals(value))
				{
					return entry.str;
				}
			}
			return null;
		}

		/// <summary>Gets the atomized string containing the same characters as the specified range of characters in the given array.</summary>
		/// <param name="key">The character array containing the name to find. </param>
		/// <param name="start">The zero-based index into the array specifying the first character of the name. </param>
		/// <param name="len">The number of characters in the name. </param>
		/// <returns>The atomized string or <see langword="null" /> if the string has not already been atomized. If <paramref name="len" /> is zero, String.Empty is returned.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">0 &gt; <paramref name="start" />-or- 
		///         <paramref name="start" /> &gt;= <paramref name="key" />.Length -or- 
		///         <paramref name="len" /> &gt;= <paramref name="key" />.Length The above conditions do not cause an exception to be thrown if <paramref name="len" /> =0. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="len" /> &lt; 0. </exception>
		// Token: 0x06001330 RID: 4912 RVA: 0x00070F90 File Offset: 0x0006F190
		public override string Get(char[] key, int start, int len)
		{
			if (len == 0)
			{
				return string.Empty;
			}
			int num = len + this.hashCodeRandomizer;
			num += (num << 7 ^ (int)key[start]);
			int num2 = start + len;
			for (int i = start + 1; i < num2; i++)
			{
				num += (num << 7 ^ (int)key[i]);
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && NameTable.TextEquals(entry.str, key, start, len))
				{
					return entry.str;
				}
			}
			return null;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00071028 File Offset: 0x0006F228
		private string AddEntry(string str, int hashCode)
		{
			int num = hashCode & this.mask;
			NameTable.Entry entry = new NameTable.Entry(str, hashCode, this.entries[num]);
			this.entries[num] = entry;
			int num2 = this.count;
			this.count = num2 + 1;
			if (num2 == this.mask)
			{
				this.Grow();
			}
			return entry.str;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0007107C File Offset: 0x0006F27C
		private void Grow()
		{
			int num = this.mask * 2 + 1;
			NameTable.Entry[] array = this.entries;
			NameTable.Entry[] array2 = new NameTable.Entry[num + 1];
			foreach (NameTable.Entry entry in array)
			{
				while (entry != null)
				{
					int num2 = entry.hashCode & num;
					NameTable.Entry next = entry.next;
					entry.next = array2[num2];
					array2[num2] = entry;
					entry = next;
				}
			}
			this.entries = array2;
			this.mask = num;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000710F0 File Offset: 0x0006F2F0
		private static bool TextEquals(string str1, char[] str2, int str2Start, int str2Length)
		{
			if (str1.Length != str2Length)
			{
				return false;
			}
			for (int i = 0; i < str1.Length; i++)
			{
				if (str1[i] != str2[str2Start + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040010EE RID: 4334
		private NameTable.Entry[] entries;

		// Token: 0x040010EF RID: 4335
		private int count;

		// Token: 0x040010F0 RID: 4336
		private int mask;

		// Token: 0x040010F1 RID: 4337
		private int hashCodeRandomizer;

		// Token: 0x020001E8 RID: 488
		private class Entry
		{
			// Token: 0x06001334 RID: 4916 RVA: 0x0007112A File Offset: 0x0006F32A
			internal Entry(string str, int hashCode, NameTable.Entry next)
			{
				this.str = str;
				this.hashCode = hashCode;
				this.next = next;
			}

			// Token: 0x040010F2 RID: 4338
			internal string str;

			// Token: 0x040010F3 RID: 4339
			internal int hashCode;

			// Token: 0x040010F4 RID: 4340
			internal NameTable.Entry next;
		}
	}
}
