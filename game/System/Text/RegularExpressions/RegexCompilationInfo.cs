using System;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions
{
	/// <summary>Provides information about a regular expression that is used to compile a regular expression to a stand-alone assembly.</summary>
	// Token: 0x020001FF RID: 511
	[Serializable]
	public class RegexCompilationInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexCompilationInfo" /> class that contains information about a regular expression to be included in an assembly.</summary>
		/// <param name="pattern">The regular expression to compile.</param>
		/// <param name="options">The regular expression options to use when compiling the regular expression.</param>
		/// <param name="name">The name of the type that represents the compiled regular expression.</param>
		/// <param name="fullnamespace">The namespace to which the new type belongs.</param>
		/// <param name="ispublic">
		///   <see langword="true" /> to make the compiled regular expression publicly visible; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="fullnamespace" /> is <see langword="null" />.</exception>
		// Token: 0x06000DEC RID: 3564 RVA: 0x0003A448 File Offset: 0x00038648
		public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic) : this(pattern, options, name, fullnamespace, ispublic, Regex.s_defaultMatchTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexCompilationInfo" /> class that contains information about a regular expression with a specified time-out value to be included in an assembly.</summary>
		/// <param name="pattern">The regular expression to compile.</param>
		/// <param name="options">The regular expression options to use when compiling the regular expression.</param>
		/// <param name="name">The name of the type that represents the compiled regular expression.</param>
		/// <param name="fullnamespace">The namespace to which the new type belongs.</param>
		/// <param name="ispublic">
		///   <see langword="true" /> to make the compiled regular expression publicly visible; otherwise, <see langword="false" />.</param>
		/// <param name="matchTimeout">The default time-out interval for the regular expression.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="fullnamespace" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="matchTimeout" /> is negative, zero, or greater than approximately 24 days.</exception>
		// Token: 0x06000DED RID: 3565 RVA: 0x0003A45C File Offset: 0x0003865C
		public RegexCompilationInfo(string pattern, RegexOptions options, string name, string fullnamespace, bool ispublic, TimeSpan matchTimeout)
		{
			this.Pattern = pattern;
			this.Name = name;
			this.Namespace = fullnamespace;
			this.Options = options;
			this.IsPublic = ispublic;
			this.MatchTimeout = matchTimeout;
		}

		/// <summary>Gets or sets a value that indicates whether the compiled regular expression has public visibility.</summary>
		/// <returns>
		///   <see langword="true" /> if the regular expression has public visibility; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0003A491 File Offset: 0x00038691
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0003A499 File Offset: 0x00038699
		public bool IsPublic
		{
			[CompilerGenerated]
			get
			{
				return this.<IsPublic>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsPublic>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the regular expression's default time-out interval.</summary>
		/// <returns>The default maximum time interval that can elapse in a pattern-matching operation before a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> is thrown, or <see cref="F:System.Text.RegularExpressions.Regex.InfiniteMatchTimeout" /> if time-outs are disabled.</returns>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003A4A2 File Offset: 0x000386A2
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0003A4AA File Offset: 0x000386AA
		public TimeSpan MatchTimeout
		{
			get
			{
				return this._matchTimeout;
			}
			set
			{
				Regex.ValidateMatchTimeout(value);
				this._matchTimeout = value;
			}
		}

		/// <summary>Gets or sets the name of the type that represents the compiled regular expression.</summary>
		/// <returns>The name of the new type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value for this property is an empty string.</exception>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003A4B9 File Offset: 0x000386B9
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0003A4C1 File Offset: 0x000386C1
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Name");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.Format("Argument {0} cannot be zero-length.", "Name"), "Name");
				}
				this._name = value;
			}
		}

		/// <summary>Gets or sets the namespace to which the new type belongs.</summary>
		/// <returns>The namespace of the new type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is <see langword="null" />.</exception>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0003A4FA File Offset: 0x000386FA
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0003A502 File Offset: 0x00038702
		public string Namespace
		{
			get
			{
				return this._nspace;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Namespace");
				}
				this._nspace = value;
			}
		}

		/// <summary>Gets or sets the options to use when compiling the regular expression.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0003A51A File Offset: 0x0003871A
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0003A522 File Offset: 0x00038722
		public RegexOptions Options
		{
			[CompilerGenerated]
			get
			{
				return this.<Options>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Options>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the regular expression to compile.</summary>
		/// <returns>The regular expression to compile.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for this property is <see langword="null" />.</exception>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0003A52B File Offset: 0x0003872B
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0003A533 File Offset: 0x00038733
		public string Pattern
		{
			get
			{
				return this._pattern;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Pattern");
				}
				this._pattern = value;
			}
		}

		// Token: 0x040008A3 RID: 2211
		private string _pattern;

		// Token: 0x040008A4 RID: 2212
		private string _name;

		// Token: 0x040008A5 RID: 2213
		private string _nspace;

		// Token: 0x040008A6 RID: 2214
		private TimeSpan _matchTimeout;

		// Token: 0x040008A7 RID: 2215
		[CompilerGenerated]
		private bool <IsPublic>k__BackingField;

		// Token: 0x040008A8 RID: 2216
		[CompilerGenerated]
		private RegexOptions <Options>k__BackingField;
	}
}
