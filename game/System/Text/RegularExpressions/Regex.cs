using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents an immutable regular expression.</summary>
	// Token: 0x020001F5 RID: 501
	public class Regex : ISerializable
	{
		/// <summary>Gets or sets the maximum number of entries in the current static cache of compiled regular expressions.</summary>
		/// <returns>The maximum number of entries in the static cache.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.</exception>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000365B8 File Offset: 0x000347B8
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x000365C0 File Offset: 0x000347C0
		public static int CacheSize
		{
			get
			{
				return Regex.s_cacheSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				Dictionary<Regex.CachedCodeEntryKey, Regex.CachedCodeEntry> obj = Regex.s_cache;
				lock (obj)
				{
					Regex.s_cacheSize = value;
					while (Regex.s_cacheCount > Regex.s_cacheSize)
					{
						Regex.CachedCodeEntry cachedCodeEntry = Regex.s_cacheLast;
						if (Regex.s_cacheCount >= 10)
						{
							Regex.s_cache.Remove(cachedCodeEntry.Key);
						}
						Regex.s_cacheLast = cachedCodeEntry.Next;
						if (cachedCodeEntry.Next != null)
						{
							cachedCodeEntry.Next.Previous = null;
						}
						else
						{
							Regex.s_cacheFirst = null;
						}
						Regex.s_cacheCount--;
					}
				}
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00036670 File Offset: 0x00034870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Regex.CachedCodeEntry GetCachedCode(Regex.CachedCodeEntryKey key, bool isToAdd)
		{
			Regex.CachedCodeEntry cachedCodeEntry = Regex.s_cacheFirst;
			if (cachedCodeEntry != null && cachedCodeEntry.Key == key)
			{
				return cachedCodeEntry;
			}
			if (Regex.s_cacheSize == 0)
			{
				return null;
			}
			return this.GetCachedCodeEntryInternal(key, isToAdd);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000366A8 File Offset: 0x000348A8
		private Regex.CachedCodeEntry GetCachedCodeEntryInternal(Regex.CachedCodeEntryKey key, bool isToAdd)
		{
			Dictionary<Regex.CachedCodeEntryKey, Regex.CachedCodeEntry> obj = Regex.s_cache;
			Regex.CachedCodeEntry result;
			lock (obj)
			{
				Regex.CachedCodeEntry cachedCodeEntry = Regex.LookupCachedAndPromote(key);
				if (cachedCodeEntry == null && isToAdd && Regex.s_cacheSize != 0)
				{
					cachedCodeEntry = new Regex.CachedCodeEntry(key, this.capnames, this.capslist, this._code, this.caps, this.capsize, this._runnerref, this._replref);
					if (Regex.s_cacheFirst != null)
					{
						Regex.s_cacheFirst.Next = cachedCodeEntry;
						cachedCodeEntry.Previous = Regex.s_cacheFirst;
					}
					Regex.s_cacheFirst = cachedCodeEntry;
					Regex.s_cacheCount++;
					if (Regex.s_cacheCount >= 10)
					{
						if (Regex.s_cacheCount == 10)
						{
							this.FillCacheDictionary();
						}
						else
						{
							Regex.s_cache.Add(key, cachedCodeEntry);
						}
					}
					if (Regex.s_cacheLast == null)
					{
						Regex.s_cacheLast = cachedCodeEntry;
					}
					else if (Regex.s_cacheCount > Regex.s_cacheSize)
					{
						Regex.CachedCodeEntry cachedCodeEntry2 = Regex.s_cacheLast;
						if (Regex.s_cacheCount >= 10)
						{
							Regex.s_cache.Remove(cachedCodeEntry2.Key);
						}
						cachedCodeEntry2.Next.Previous = null;
						Regex.s_cacheLast = cachedCodeEntry2.Next;
						Regex.s_cacheCount--;
					}
				}
				result = cachedCodeEntry;
			}
			return result;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000367F4 File Offset: 0x000349F4
		private void FillCacheDictionary()
		{
			Regex.s_cache.Clear();
			for (Regex.CachedCodeEntry previous = Regex.s_cacheFirst; previous != null; previous = previous.Previous)
			{
				Regex.s_cache.Add(previous.Key, previous);
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0003682E File Offset: 0x00034A2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryGetCacheValue(Regex.CachedCodeEntryKey key, out Regex.CachedCodeEntry entry)
		{
			if (Regex.s_cacheCount >= 10)
			{
				return Regex.s_cache.TryGetValue(key, out entry);
			}
			return Regex.TryGetCacheValueSmall(key, out entry);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0003684D File Offset: 0x00034A4D
		private static bool TryGetCacheValueSmall(Regex.CachedCodeEntryKey key, out Regex.CachedCodeEntry entry)
		{
			Regex.CachedCodeEntry cachedCodeEntry = Regex.s_cacheFirst;
			for (entry = ((cachedCodeEntry != null) ? cachedCodeEntry.Previous : null); entry != null; entry = entry.Previous)
			{
				if (entry.Key == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00036884 File Offset: 0x00034A84
		private static Regex.CachedCodeEntry LookupCachedAndPromote(Regex.CachedCodeEntryKey key)
		{
			Regex.CachedCodeEntry cachedCodeEntry = Regex.s_cacheFirst;
			if (cachedCodeEntry != null && cachedCodeEntry.Key == key)
			{
				return Regex.s_cacheFirst;
			}
			Regex.CachedCodeEntry cachedCodeEntry2;
			if (Regex.TryGetCacheValue(key, out cachedCodeEntry2))
			{
				if (Regex.s_cacheLast == cachedCodeEntry2)
				{
					Regex.s_cacheLast = cachedCodeEntry2.Next;
				}
				else
				{
					cachedCodeEntry2.Previous.Next = cachedCodeEntry2.Next;
				}
				cachedCodeEntry2.Next.Previous = cachedCodeEntry2.Previous;
				Regex.s_cacheFirst.Next = cachedCodeEntry2;
				cachedCodeEntry2.Previous = Regex.s_cacheFirst;
				cachedCodeEntry2.Next = null;
				Regex.s_cacheFirst = cachedCodeEntry2;
			}
			return cachedCodeEntry2;
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D67 RID: 3431 RVA: 0x00036915 File Offset: 0x00034B15
		public static bool IsMatch(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string, using the specified matching options.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> value.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D68 RID: 3432 RVA: 0x00036924 File Offset: 0x00034B24
		public static bool IsMatch(string input, string pattern, RegexOptions options)
		{
			return Regex.IsMatch(input, pattern, options, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Indicates whether the specified regular expression finds a match in the specified input string, using the specified matching options and time-out interval.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> value.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		// Token: 0x06000D69 RID: 3433 RVA: 0x00036933 File Offset: 0x00034B33
		public static bool IsMatch(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).IsMatch(input);
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in a specified input string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D6A RID: 3434 RVA: 0x00036944 File Offset: 0x00034B44
		public bool IsMatch(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.IsMatch(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in the specified input string, beginning at the specified starting position in the string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="startat">The character position at which to start the search.</param>
		/// <returns>
		///   <see langword="true" /> if the regular expression finds a match; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D6B RID: 3435 RVA: 0x0003696C File Offset: 0x00034B6C
		public bool IsMatch(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(true, -1, input, 0, input.Length, startat) == null;
		}

		/// <summary>Searches the specified input string for the first occurrence of the specified regular expression.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D6C RID: 3436 RVA: 0x00036990 File Offset: 0x00034B90
		public static Match Match(string input, string pattern)
		{
			return Regex.Match(input, pattern, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Searches the input string for the first occurrence of the specified regular expression, using the specified matching options.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D6D RID: 3437 RVA: 0x0003699F File Offset: 0x00034B9F
		public static Match Match(string input, string pattern, RegexOptions options)
		{
			return Regex.Match(input, pattern, options, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Searches the input string for the first occurrence of the specified regular expression, using the specified matching options and time-out interval.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D6E RID: 3438 RVA: 0x000369AE File Offset: 0x00034BAE
		public static Match Match(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Match(input);
		}

		/// <summary>Searches the specified input string for the first occurrence of the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D6F RID: 3439 RVA: 0x000369BF File Offset: 0x00034BBF
		public Match Match(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Match(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="startat">The zero-based character position at which to start the search.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D70 RID: 3440 RVA: 0x000369E7 File Offset: 0x00034BE7
		public Match Match(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(false, -1, input, 0, input.Length, startat);
		}

		/// <summary>Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position and searching only the specified number of characters.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="beginning">The zero-based character position in the input string that defines the leftmost position to be searched.</param>
		/// <param name="length">The number of characters in the substring to include in the search.</param>
		/// <returns>An object that contains information about the match.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="beginning" /> is less than zero or greater than the length of <paramref name="input" />.  
		/// -or-  
		/// <paramref name="length" /> is less than zero or greater than the length of <paramref name="input" />.  
		/// -or-  
		/// <paramref name="beginning" /><see langword="+" /><paramref name="length" /><see langword="-1" /> identifies a position that is outside the range of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D71 RID: 3441 RVA: 0x00036A08 File Offset: 0x00034C08
		public Match Match(string input, int beginning, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Run(false, -1, input, beginning, length, this.UseOptionR() ? (beginning + length) : beginning);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		// Token: 0x06000D72 RID: 3442 RVA: 0x00036A31 File Offset: 0x00034C31
		public static MatchCollection Matches(string input, string pattern)
		{
			return Regex.Matches(input, pattern, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression, using the specified matching options.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		// Token: 0x06000D73 RID: 3443 RVA: 0x00036A40 File Offset: 0x00034C40
		public static MatchCollection Matches(string input, string pattern, RegexOptions options)
		{
			return Regex.Matches(input, pattern, options, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression, using the specified matching options and time-out interval.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		// Token: 0x06000D74 RID: 3444 RVA: 0x00036A4F File Offset: 0x00034C4F
		public static MatchCollection Matches(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Matches(input);
		}

		/// <summary>Searches the specified input string for all occurrences of a regular expression.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		// Token: 0x06000D75 RID: 3445 RVA: 0x00036A60 File Offset: 0x00034C60
		public MatchCollection Matches(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Matches(input, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Searches the specified input string for all occurrences of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="startat">The character position in the input string at which to start the search.</param>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		// Token: 0x06000D76 RID: 3446 RVA: 0x00036A88 File Offset: 0x00034C88
		public MatchCollection Matches(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return new MatchCollection(this, input, 0, input.Length, startat);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D77 RID: 3447 RVA: 0x00036AA7 File Offset: 0x00034CA7
		public static string Replace(string input, string pattern, string replacement)
		{
			return Regex.Replace(input, pattern, replacement, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Specified options modify the matching operation.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D78 RID: 3448 RVA: 0x00036AB7 File Offset: 0x00034CB7
		public static string Replace(string input, string pattern, string replacement, RegexOptions options)
		{
			return Regex.Replace(input, pattern, replacement, options, Regex.s_defaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D79 RID: 3449 RVA: 0x00036AC7 File Offset: 0x00034CC7
		public static string Replace(string input, string pattern, string replacement, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, replacement);
		}

		/// <summary>In a specified input string, replaces all strings that match a regular expression pattern with a specified replacement string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D7A RID: 3450 RVA: 0x00036ADA File Offset: 0x00034CDA
		public string Replace(string input, string replacement)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, -1, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="count">The maximum number of times the replacement can occur.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D7B RID: 3451 RVA: 0x00036B04 File Offset: 0x00034D04
		public string Replace(string input, string replacement, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, replacement, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="replacement">The replacement string.</param>
		/// <param name="count">Maximum number of times the replacement can occur.</param>
		/// <param name="startat">The character position in the input string where the search begins.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="replacement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D7C RID: 3452 RVA: 0x00036B30 File Offset: 0x00034D30
		public string Replace(string input, string replacement, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			return RegexReplacement.GetOrCreate(this._replref, replacement, this.caps, this.capsize, this.capnames, this.roptions).Replace(this, input, count, startat);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D7D RID: 3453 RVA: 0x00036B87 File Offset: 0x00034D87
		public static string Replace(string input, string pattern, MatchEvaluator evaluator)
		{
			return Regex.Replace(input, pattern, evaluator, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. Specified options modify the matching operation.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D7E RID: 3454 RVA: 0x00036B97 File Offset: 0x00034D97
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options)
		{
			return Regex.Replace(input, pattern, evaluator, options, Regex.s_defaultMatchTimeout);
		}

		/// <summary>In a specified input string, replaces all substrings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="options">A bitwise combination of enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If <paramref name="pattern" /> is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" />, <paramref name="pattern" />, or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D7F RID: 3455 RVA: 0x00036BA7 File Offset: 0x00034DA7
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Replace(input, evaluator);
		}

		/// <summary>In a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D80 RID: 3456 RVA: 0x00036BBA File Offset: 0x00034DBA
		public string Replace(string input, MatchEvaluator evaluator)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, evaluator, -1, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="count">The maximum number of times the replacement will occur.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D81 RID: 3457 RVA: 0x00036BE4 File Offset: 0x00034DE4
		public string Replace(string input, MatchEvaluator evaluator, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Replace(input, evaluator, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>In a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <param name="input">The string to search for a match.</param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="count">The maximum number of times the replacement will occur.</param>
		/// <param name="startat">The character position in the input string where the search begins.</param>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="evaluator" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D82 RID: 3458 RVA: 0x00036C0E File Offset: 0x00034E0E
		public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Regex.Replace(evaluator, this, input, count, startat);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00036C2C File Offset: 0x00034E2C
		private static string Replace(MatchEvaluator evaluator, Regex regex, string input, int count, int startat)
		{
			if (evaluator == null)
			{
				throw new ArgumentNullException("evaluator");
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than -1.");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			if (count == 0)
			{
				return input;
			}
			Match match = regex.Match(input, startat);
			if (!match.Success)
			{
				return input;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			if (!regex.RightToLeft)
			{
				int num = 0;
				do
				{
					if (match.Index != num)
					{
						stringBuilder.Append(input, num, match.Index - num);
					}
					num = match.Index + match.Length;
					stringBuilder.Append(evaluator(match));
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num < input.Length)
				{
					stringBuilder.Append(input, num, input.Length - num);
				}
			}
			else
			{
				List<string> list = new List<string>();
				int num2 = input.Length;
				do
				{
					if (match.Index + match.Length != num2)
					{
						list.Add(input.Substring(match.Index + match.Length, num2 - match.Index - match.Length));
					}
					num2 = match.Index;
					list.Add(evaluator(match));
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				if (num2 > 0)
				{
					stringBuilder.Append(input, 0, num2);
				}
				for (int i = list.Count - 1; i >= 0; i--)
				{
					stringBuilder.Append(list[i]);
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a regular expression pattern.</summary>
		/// <param name="input">The string to split.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D84 RID: 3460 RVA: 0x00036DC4 File Offset: 0x00034FC4
		public static string[] Split(string input, string pattern)
		{
			return Regex.Split(input, pattern, RegexOptions.None, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a specified regular expression pattern. Specified options modify the matching operation.</summary>
		/// <param name="input">The string to split.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D85 RID: 3461 RVA: 0x00036DD3 File Offset: 0x00034FD3
		public static string[] Split(string input, string pattern, RegexOptions options)
		{
			return Regex.Split(input, pattern, options, Regex.s_defaultMatchTimeout);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a specified regular expression pattern. Additional parameters specify options that modify the matching operation and a time-out interval if no match is found.</summary>
		/// <param name="input">The string to split.</param>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that provide options for matching.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <returns>A string array.</returns>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> or <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D86 RID: 3462 RVA: 0x00036DE2 File Offset: 0x00034FE2
		public static string[] Split(string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
		{
			return new Regex(pattern, options, matchTimeout, true).Split(input);
		}

		/// <summary>Splits an input string into an array of substrings at the positions defined by a regular expression pattern specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <param name="input">The string to split.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D87 RID: 3463 RVA: 0x00036DF3 File Offset: 0x00034FF3
		public string[] Split(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Split(input, 0, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Splits an input string a specified maximum number of times into an array of substrings, at the positions defined by a regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <param name="input">The string to be split.</param>
		/// <param name="count">The maximum number of times the split can occur.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D88 RID: 3464 RVA: 0x00036E1C File Offset: 0x0003501C
		public string[] Split(string input, int count)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Regex.Split(this, input, count, this.UseOptionR() ? input.Length : 0);
		}

		/// <summary>Splits an input string a specified maximum number of times into an array of substrings, at the positions defined by a regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor. The search for the regular expression pattern starts at a specified character position in the input string.</summary>
		/// <param name="input">The string to be split.</param>
		/// <param name="count">The maximum number of times the split can occur.</param>
		/// <param name="startat">The character position in the input string where the search will begin.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
		// Token: 0x06000D89 RID: 3465 RVA: 0x00036E45 File Offset: 0x00035045
		public string[] Split(string input, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Regex.Split(this, input, count, startat);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00036E60 File Offset: 0x00035060
		private static string[] Split(Regex regex, string input, int count, int startat)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than -1.");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			if (count == 1)
			{
				return new string[]
				{
					input
				};
			}
			count--;
			Match match = regex.Match(input, startat);
			if (!match.Success)
			{
				return new string[]
				{
					input
				};
			}
			List<string> list = new List<string>();
			if (!regex.RightToLeft)
			{
				int num = 0;
				do
				{
					list.Add(input.Substring(num, match.Index - num));
					num = match.Index + match.Length;
					for (int i = 1; i < match.Groups.Count; i++)
					{
						if (match.IsMatched(i))
						{
							list.Add(match.Groups[i].ToString());
						}
					}
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				list.Add(input.Substring(num, input.Length - num));
			}
			else
			{
				int num2 = input.Length;
				do
				{
					list.Add(input.Substring(match.Index + match.Length, num2 - match.Index - match.Length));
					num2 = match.Index;
					for (int j = 1; j < match.Groups.Count; j++)
					{
						if (match.IsMatched(j))
						{
							list.Add(match.Groups[j].ToString());
						}
					}
					if (--count == 0)
					{
						break;
					}
					match = match.NextMatch();
				}
				while (match.Success);
				list.Add(input.Substring(0, num2));
				list.Reverse(0, list.Count);
			}
			return list.ToArray();
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0003701C File Offset: 0x0003521C
		static Regex()
		{
			Regex.s_defaultMatchTimeout = Regex.InitDefaultMatchTimeout();
		}

		/// <summary>Gets the time-out interval of the current instance.</summary>
		/// <returns>The maximum time interval that can elapse in a pattern-matching operation before a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> is thrown, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> if time-outs are disabled.</returns>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0003706C File Offset: 0x0003526C
		public TimeSpan MatchTimeout
		{
			get
			{
				return this.internalMatchTimeout;
			}
		}

		/// <summary>Checks whether a time-out interval is within an acceptable range.</summary>
		/// <param name="matchTimeout">The time-out interval to check.</param>
		// Token: 0x06000D8D RID: 3469 RVA: 0x00037074 File Offset: 0x00035274
		protected internal static void ValidateMatchTimeout(TimeSpan matchTimeout)
		{
			if (Regex.InfiniteMatchTimeout == matchTimeout)
			{
				return;
			}
			if (TimeSpan.Zero < matchTimeout && matchTimeout <= Regex.s_maximumMatchTimeout)
			{
				return;
			}
			throw new ArgumentOutOfRangeException("matchTimeout");
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000370AC File Offset: 0x000352AC
		private static TimeSpan InitDefaultMatchTimeout()
		{
			object data = AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
			if (data == null)
			{
				return Regex.InfiniteMatchTimeout;
			}
			if (data is TimeSpan)
			{
				TimeSpan timeSpan = (TimeSpan)data;
				try
				{
					Regex.ValidateMatchTimeout(timeSpan);
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new ArgumentOutOfRangeException(SR.Format("AppDomain data '{0}' contains an invalid value or object for specifying a default matching timeout for System.Text.RegularExpressions.Regex.", "REGEX_DEFAULT_MATCH_TIMEOUT", timeSpan));
				}
				return timeSpan;
			}
			throw new InvalidCastException(SR.Format("AppDomain data '{0}' contains an invalid value or object for specifying a default matching timeout for System.Text.RegularExpressions.Regex.", "REGEX_DEFAULT_MATCH_TIMEOUT", data));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class.</summary>
		// Token: 0x06000D8F RID: 3471 RVA: 0x0003712C File Offset: 0x0003532C
		protected Regex()
		{
			this.internalMatchTimeout = Regex.s_defaultMatchTimeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression.</summary>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.</exception>
		// Token: 0x06000D90 RID: 3472 RVA: 0x0003713F File Offset: 0x0003533F
		public Regex(string pattern) : this(pattern, RegexOptions.None, Regex.s_defaultMatchTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression, with options that modify the pattern.</summary>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> contains an invalid flag.</exception>
		// Token: 0x06000D91 RID: 3473 RVA: 0x0003714F File Offset: 0x0003534F
		public Regex(string pattern, RegexOptions options) : this(pattern, options, Regex.s_defaultMatchTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression, with options that modify the pattern and a value that specifies how long a pattern matching method should attempt a match before it times out.</summary>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
		/// <param name="matchTimeout">A time-out interval, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> to indicate that the method should not time out.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> value.  
		/// -or-  
		/// <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		// Token: 0x06000D92 RID: 3474 RVA: 0x0003715F File Offset: 0x0003535F
		public Regex(string pattern, RegexOptions options, TimeSpan matchTimeout) : this(pattern, options, matchTimeout, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class by using serialized data.</summary>
		/// <param name="info">The object that contains a serialized pattern and <see cref="T:System.Text.RegularExpressions.RegexOptions" /> information.</param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">The pattern that <paramref name="info" /> contains is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="info" /> contains an invalid <see cref="T:System.Text.RegularExpressions.RegexOptions" /> flag.</exception>
		// Token: 0x06000D93 RID: 3475 RVA: 0x0003716B File Offset: 0x0003536B
		protected Regex(SerializationInfo info, StreamingContext context) : this(info.GetString("pattern"), (RegexOptions)info.GetInt32("options"))
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.RegularExpressions.Regex" /> object.</summary>
		/// <param name="si">The object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. This parameter is reserved for future use.</param>
		// Token: 0x06000D94 RID: 3476 RVA: 0x00011F54 File Offset: 0x00010154
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00037190 File Offset: 0x00035390
		private Regex(string pattern, RegexOptions options, TimeSpan matchTimeout, bool addToCache)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			if (options < RegexOptions.None || options >> 10 != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & RegexOptions.ECMAScript) != RegexOptions.None && (options & ~(RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ECMAScript | RegexOptions.CultureInvariant)) != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			Regex.ValidateMatchTimeout(matchTimeout);
			this.pattern = pattern;
			this.roptions = options;
			this.internalMatchTimeout = matchTimeout;
			string cultureKey = ((options & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture.ToString() : CultureInfo.CurrentCulture.ToString();
			Regex.CachedCodeEntryKey key = new Regex.CachedCodeEntryKey(options, cultureKey, pattern);
			Regex.CachedCodeEntry cachedCode = this.GetCachedCode(key, false);
			if (cachedCode == null)
			{
				RegexTree regexTree = RegexParser.Parse(pattern, this.roptions);
				this.capnames = regexTree.CapNames;
				this.capslist = regexTree.CapsList;
				this._code = RegexWriter.Write(regexTree);
				this.caps = this._code.Caps;
				this.capsize = this._code.CapSize;
				this.InitializeReferences();
				if (addToCache)
				{
					cachedCode = this.GetCachedCode(key, true);
				}
			}
			else
			{
				this.caps = cachedCode.Caps;
				this.capnames = cachedCode.Capnames;
				this.capslist = cachedCode.Capslist;
				this.capsize = cachedCode.Capsize;
				this._code = cachedCode.Code;
				this.factory = cachedCode.Factory;
				this._runnerref = cachedCode.Runnerref;
				this._replref = cachedCode.ReplRef;
				this._refsInitialized = true;
			}
			if (this.UseOptionC() && this.factory == null)
			{
				this.factory = this.Compile(this._code, this.roptions);
				if (addToCache && cachedCode != null)
				{
					cachedCode.AddCompiled(this.factory);
				}
				this._code = null;
			}
		}

		/// <summary>Gets or sets a dictionary that maps numbered capturing groups to their index values.</summary>
		/// <returns>A dictionary that maps numbered capturing groups to their index values.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Text.RegularExpressions.Regex.Caps" /> property in a set operation is <see langword="null" />.</exception>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00037347 File Offset: 0x00035547
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0003734F File Offset: 0x0003554F
		[CLSCompliant(false)]
		protected IDictionary Caps
		{
			get
			{
				return this.caps;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.caps = ((value as Hashtable) ?? new Hashtable(value));
			}
		}

		/// <summary>Gets or sets a dictionary that maps named capturing groups to their index values.</summary>
		/// <returns>A dictionary that maps named capturing groups to their index values.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Text.RegularExpressions.Regex.CapNames" /> property in a set operation is <see langword="null" />.</exception>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00037375 File Offset: 0x00035575
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0003737D File Offset: 0x0003557D
		[CLSCompliant(false)]
		protected IDictionary CapNames
		{
			get
			{
				return this.capnames;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.capnames = ((value as Hashtable) ?? new Hashtable(value));
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x000373A3 File Offset: 0x000355A3
		[MethodImpl(MethodImplOptions.NoInlining)]
		private RegexRunnerFactory Compile(RegexCode code, RegexOptions roptions)
		{
			return RegexCompiler.Compile(code, roptions);
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects to a named assembly.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile.</param>
		/// <param name="assemblyname">The file name of the assembly.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.  
		///  -or-  
		///  The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: Creating an assembly of compiled regular expressions is not supported.</exception>
		// Token: 0x06000D9B RID: 3483 RVA: 0x000373AC File Offset: 0x000355AC
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname)
		{
			throw new PlatformNotSupportedException("This platform does not support writing compiled regular expressions to an assembly.");
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects to a named assembly with the specified attributes.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile.</param>
		/// <param name="assemblyname">The file name of the assembly.</param>
		/// <param name="attributes">An array that defines the attributes to apply to the assembly.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.  
		///  -or-  
		///  The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: Creating an assembly of compiled regular expressions is not supported.</exception>
		// Token: 0x06000D9C RID: 3484 RVA: 0x000373AC File Offset: 0x000355AC
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes)
		{
			throw new PlatformNotSupportedException("This platform does not support writing compiled regular expressions to an assembly.");
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects and a specified resource file to a named assembly with the specified attributes.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile.</param>
		/// <param name="assemblyname">The file name of the assembly.</param>
		/// <param name="attributes">An array that defines the attributes to apply to the assembly.</param>
		/// <param name="resourceFile">The name of the Win32 resource file to include in the assembly.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.  
		///  -or-  
		///  The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The <paramref name="resourceFile" /> parameter designates an invalid Win32 resource file.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file designated by the <paramref name="resourceFile" /> parameter cannot be found.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">.NET Core only: Creating an assembly of compiled regular expressions is not supported.</exception>
		// Token: 0x06000D9D RID: 3485 RVA: 0x000373AC File Offset: 0x000355AC
		public static void CompileToAssembly(RegexCompilationInfo[] regexinfos, AssemblyName assemblyname, CustomAttributeBuilder[] attributes, string resourceFile)
		{
			throw new PlatformNotSupportedException("This platform does not support writing compiled regular expressions to an assembly.");
		}

		/// <summary>Escapes a minimal set of characters (\, *, +, ?, |, {, [, (,), ^, $, ., #, and white space) by replacing them with their escape codes. This instructs the regular expression engine to interpret these characters literally rather than as metacharacters.</summary>
		/// <param name="str">The input string that contains the text to convert.</param>
		/// <returns>A string of characters with metacharacters converted to their escaped form.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x06000D9E RID: 3486 RVA: 0x000373B8 File Offset: 0x000355B8
		public static string Escape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return RegexParser.Escape(str);
		}

		/// <summary>Converts any escaped characters in the input string.</summary>
		/// <param name="str">The input string containing the text to convert.</param>
		/// <returns>A string of characters with any escaped characters converted to their unescaped form.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="str" /> includes an unrecognized escape sequence.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x06000D9F RID: 3487 RVA: 0x000373CE File Offset: 0x000355CE
		public static string Unescape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return RegexParser.Unescape(str);
		}

		/// <summary>Gets the options that were passed into the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>One or more members of the <see cref="T:System.Text.RegularExpressions.RegexOptions" /> enumeration that represent options that were passed to the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x000373E4 File Offset: 0x000355E4
		public RegexOptions Options
		{
			get
			{
				return this.roptions;
			}
		}

		/// <summary>Gets a value that indicates whether the regular expression searches from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> if the regular expression searches from right to left; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x000373EC File Offset: 0x000355EC
		public bool RightToLeft
		{
			get
			{
				return this.UseOptionR();
			}
		}

		/// <summary>Returns the regular expression pattern that was passed into the <see langword="Regex" /> constructor.</summary>
		/// <returns>The <paramref name="pattern" /> parameter that was passed into the <see langword="Regex" /> constructor.</returns>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x000373F4 File Offset: 0x000355F4
		public override string ToString()
		{
			return this.pattern;
		}

		/// <summary>Returns an array of capturing group names for the regular expression.</summary>
		/// <returns>A string array of group names.</returns>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x000373FC File Offset: 0x000355FC
		public string[] GetGroupNames()
		{
			string[] array;
			if (this.capslist == null)
			{
				int num = this.capsize;
				array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = Convert.ToString(i, CultureInfo.InvariantCulture);
				}
			}
			else
			{
				array = new string[this.capslist.Length];
				Array.Copy(this.capslist, 0, array, 0, this.capslist.Length);
			}
			return array;
		}

		/// <summary>Returns an array of capturing group numbers that correspond to group names in an array.</summary>
		/// <returns>An integer array of group numbers.</returns>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x00037460 File Offset: 0x00035660
		public int[] GetGroupNumbers()
		{
			int[] array;
			if (this.caps == null)
			{
				array = new int[this.capsize];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i;
				}
			}
			else
			{
				array = new int[this.caps.Count];
				IDictionaryEnumerator enumerator = this.caps.GetEnumerator();
				while (enumerator.MoveNext())
				{
					array[(int)enumerator.Value] = (int)enumerator.Key;
				}
			}
			return array;
		}

		/// <summary>Gets the group name that corresponds to the specified group number.</summary>
		/// <param name="i">The group number to convert to the corresponding group name.</param>
		/// <returns>A string that contains the group name associated with the specified group number. If there is no group name that corresponds to <paramref name="i" />, the method returns <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x06000DA5 RID: 3493 RVA: 0x000374D8 File Offset: 0x000356D8
		public string GroupNameFromNumber(int i)
		{
			if (this.capslist == null)
			{
				if (i >= 0 && i < this.capsize)
				{
					return i.ToString(CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
			else
			{
				if (this.caps != null && !this.caps.TryGetValue(i, out i))
				{
					return string.Empty;
				}
				if (i >= 0 && i < this.capslist.Length)
				{
					return this.capslist[i];
				}
				return string.Empty;
			}
		}

		/// <summary>Returns the group number that corresponds to the specified group name.</summary>
		/// <param name="name">The group name to convert to the corresponding group number.</param>
		/// <returns>The group number that corresponds to the specified group name, or -1 if <paramref name="name" /> is not a valid group name.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06000DA6 RID: 3494 RVA: 0x00037550 File Offset: 0x00035750
		public int GroupNumberFromName(string name)
		{
			int num = -1;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.capnames != null)
			{
				if (!this.capnames.TryGetValue(name, out num))
				{
					return -1;
				}
				return num;
			}
			else
			{
				num = 0;
				foreach (char c in name)
				{
					if (c > '9' || c < '0')
					{
						return -1;
					}
					num *= 10;
					num += (int)(c - '0');
				}
				if (num >= 0 && num < this.capsize)
				{
					return num;
				}
				return -1;
			}
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <exception cref="T:System.NotSupportedException">References have already been initialized.</exception>
		// Token: 0x06000DA7 RID: 3495 RVA: 0x000375CC File Offset: 0x000357CC
		protected void InitializeReferences()
		{
			if (this._refsInitialized)
			{
				throw new NotSupportedException("This operation is only allowed once per object.");
			}
			this._refsInitialized = true;
			this._runnerref = new ExclusiveReference();
			this._replref = new WeakReference<RegexReplacement>(null);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00037600 File Offset: 0x00035800
		internal Match Run(bool quick, int prevlen, string input, int beginning, int length, int startat)
		{
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat", "Start index cannot be less than 0 or greater than input length.");
			}
			if (length < 0 || length > input.Length)
			{
				throw new ArgumentOutOfRangeException("length", "Length cannot be less than 0 or exceed input length.");
			}
			RegexRunner regexRunner = this._runnerref.Get();
			if (regexRunner == null)
			{
				if (this.factory != null)
				{
					regexRunner = this.factory.CreateInstance();
				}
				else
				{
					regexRunner = new RegexInterpreter(this._code, this.UseOptionInvariant() ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
				}
			}
			Match result;
			try
			{
				result = regexRunner.Scan(this, input, beginning, beginning + length, startat, prevlen, quick, this.internalMatchTimeout);
			}
			finally
			{
				this._runnerref.Release(regexRunner);
			}
			return result;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.Compiled" /> option; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DA9 RID: 3497 RVA: 0x000376CC File Offset: 0x000358CC
		protected bool UseOptionC()
		{
			return Environment.GetEnvironmentVariable("MONO_REGEX_COMPILED_ENABLE") != null && (this.roptions & RegexOptions.Compiled) > RegexOptions.None;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DAA RID: 3498 RVA: 0x000376E7 File Offset: 0x000358E7
		protected internal bool UseOptionR()
		{
			return (this.roptions & RegexOptions.RightToLeft) > RegexOptions.None;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000376F5 File Offset: 0x000358F5
		internal bool UseOptionInvariant()
		{
			return (this.roptions & RegexOptions.CultureInvariant) > RegexOptions.None;
		}

		// Token: 0x04000803 RID: 2051
		private const int CacheDictionarySwitchLimit = 10;

		// Token: 0x04000804 RID: 2052
		private static int s_cacheSize = 15;

		// Token: 0x04000805 RID: 2053
		private static readonly Dictionary<Regex.CachedCodeEntryKey, Regex.CachedCodeEntry> s_cache = new Dictionary<Regex.CachedCodeEntryKey, Regex.CachedCodeEntry>(Regex.s_cacheSize);

		// Token: 0x04000806 RID: 2054
		private static int s_cacheCount = 0;

		// Token: 0x04000807 RID: 2055
		private static Regex.CachedCodeEntry s_cacheFirst;

		// Token: 0x04000808 RID: 2056
		private static Regex.CachedCodeEntry s_cacheLast;

		// Token: 0x04000809 RID: 2057
		private static readonly TimeSpan s_maximumMatchTimeout = TimeSpan.FromMilliseconds(2147483646.0);

		// Token: 0x0400080A RID: 2058
		private const string DefaultMatchTimeout_ConfigKeyName = "REGEX_DEFAULT_MATCH_TIMEOUT";

		// Token: 0x0400080B RID: 2059
		internal static readonly TimeSpan s_defaultMatchTimeout;

		/// <summary>Specifies that a pattern-matching operation should not time out.</summary>
		// Token: 0x0400080C RID: 2060
		public static readonly TimeSpan InfiniteMatchTimeout = Timeout.InfiniteTimeSpan;

		/// <summary>The maximum amount of time that can elapse in a pattern-matching operation before the operation times out.</summary>
		// Token: 0x0400080D RID: 2061
		protected internal TimeSpan internalMatchTimeout;

		// Token: 0x0400080E RID: 2062
		internal const int MaxOptionShift = 10;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x0400080F RID: 2063
		protected internal string pattern;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04000810 RID: 2064
		protected internal RegexOptions roptions;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04000811 RID: 2065
		protected internal RegexRunnerFactory factory;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04000812 RID: 2066
		protected internal Hashtable caps;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04000813 RID: 2067
		protected internal Hashtable capnames;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04000814 RID: 2068
		protected internal string[] capslist;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		// Token: 0x04000815 RID: 2069
		protected internal int capsize;

		// Token: 0x04000816 RID: 2070
		internal ExclusiveReference _runnerref;

		// Token: 0x04000817 RID: 2071
		internal WeakReference<RegexReplacement> _replref;

		// Token: 0x04000818 RID: 2072
		internal RegexCode _code;

		// Token: 0x04000819 RID: 2073
		internal bool _refsInitialized;

		// Token: 0x020001F6 RID: 502
		internal readonly struct CachedCodeEntryKey : IEquatable<Regex.CachedCodeEntryKey>
		{
			// Token: 0x06000DAC RID: 3500 RVA: 0x00037706 File Offset: 0x00035906
			public CachedCodeEntryKey(RegexOptions options, string cultureKey, string pattern)
			{
				this._options = options;
				this._cultureKey = cultureKey;
				this._pattern = pattern;
			}

			// Token: 0x06000DAD RID: 3501 RVA: 0x0003771D File Offset: 0x0003591D
			public override bool Equals(object obj)
			{
				return obj is Regex.CachedCodeEntryKey && this.Equals((Regex.CachedCodeEntryKey)obj);
			}

			// Token: 0x06000DAE RID: 3502 RVA: 0x00037735 File Offset: 0x00035935
			public bool Equals(Regex.CachedCodeEntryKey other)
			{
				return this._pattern.Equals(other._pattern) && this._options == other._options && this._cultureKey.Equals(other._cultureKey);
			}

			// Token: 0x06000DAF RID: 3503 RVA: 0x0003776B File Offset: 0x0003596B
			public static bool operator ==(Regex.CachedCodeEntryKey left, Regex.CachedCodeEntryKey right)
			{
				return left.Equals(right);
			}

			// Token: 0x06000DB0 RID: 3504 RVA: 0x00037775 File Offset: 0x00035975
			public static bool operator !=(Regex.CachedCodeEntryKey left, Regex.CachedCodeEntryKey right)
			{
				return !left.Equals(right);
			}

			// Token: 0x06000DB1 RID: 3505 RVA: 0x00037782 File Offset: 0x00035982
			public override int GetHashCode()
			{
				return (int)(this._options ^ (RegexOptions)this._cultureKey.GetHashCode() ^ (RegexOptions)this._pattern.GetHashCode());
			}

			// Token: 0x0400081A RID: 2074
			private readonly RegexOptions _options;

			// Token: 0x0400081B RID: 2075
			private readonly string _cultureKey;

			// Token: 0x0400081C RID: 2076
			private readonly string _pattern;
		}

		// Token: 0x020001F7 RID: 503
		internal sealed class CachedCodeEntry
		{
			// Token: 0x06000DB2 RID: 3506 RVA: 0x000377A4 File Offset: 0x000359A4
			public CachedCodeEntry(Regex.CachedCodeEntryKey key, Hashtable capnames, string[] capslist, RegexCode code, Hashtable caps, int capsize, ExclusiveReference runner, WeakReference<RegexReplacement> replref)
			{
				this.Key = key;
				this.Capnames = capnames;
				this.Capslist = capslist;
				this.Code = code;
				this.Caps = caps;
				this.Capsize = capsize;
				this.Runnerref = runner;
				this.ReplRef = replref;
			}

			// Token: 0x06000DB3 RID: 3507 RVA: 0x000377F4 File Offset: 0x000359F4
			public void AddCompiled(RegexRunnerFactory factory)
			{
				this.Factory = factory;
				this.Code = null;
			}

			// Token: 0x0400081D RID: 2077
			public Regex.CachedCodeEntry Next;

			// Token: 0x0400081E RID: 2078
			public Regex.CachedCodeEntry Previous;

			// Token: 0x0400081F RID: 2079
			public readonly Regex.CachedCodeEntryKey Key;

			// Token: 0x04000820 RID: 2080
			public RegexCode Code;

			// Token: 0x04000821 RID: 2081
			public readonly Hashtable Caps;

			// Token: 0x04000822 RID: 2082
			public readonly Hashtable Capnames;

			// Token: 0x04000823 RID: 2083
			public readonly string[] Capslist;

			// Token: 0x04000824 RID: 2084
			public RegexRunnerFactory Factory;

			// Token: 0x04000825 RID: 2085
			public readonly int Capsize;

			// Token: 0x04000826 RID: 2086
			public readonly ExclusiveReference Runnerref;

			// Token: 0x04000827 RID: 2087
			public readonly WeakReference<RegexReplacement> ReplRef;
		}
	}
}
