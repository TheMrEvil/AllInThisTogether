using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single capturing group.</summary>
	// Token: 0x020001ED RID: 493
	[Serializable]
	public class Group : Capture
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x00035986 File Offset: 0x00033B86
		internal Group(string text, int[] caps, int capcount, string name) : base(text, (capcount == 0) ? 0 : caps[(capcount - 1) * 2], (capcount == 0) ? 0 : caps[capcount * 2 - 1])
		{
			this._caps = caps;
			this._capcount = capcount;
			this.Name = name;
		}

		/// <summary>Gets a value indicating whether the match is successful.</summary>
		/// <returns>
		///   <see langword="true" /> if the match is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000359BF File Offset: 0x00033BBF
		public bool Success
		{
			get
			{
				return this._capcount != 0;
			}
		}

		/// <summary>Returns the name of the capturing group represented by the current instance.</summary>
		/// <returns>The name of the capturing group represented by the current instance.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000359CA File Offset: 0x00033BCA
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		/// <summary>Gets a collection of all the captures matched by the capturing group, in innermost-leftmost-first order (or innermost-rightmost-first order if the regular expression is modified with the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option). The collection may have zero or more items.</summary>
		/// <returns>The collection of substrings matched by the group.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000359D2 File Offset: 0x00033BD2
		public CaptureCollection Captures
		{
			get
			{
				if (this._capcoll == null)
				{
					this._capcoll = new CaptureCollection(this);
				}
				return this._capcoll;
			}
		}

		/// <summary>Returns a <see langword="Group" /> object equivalent to the one supplied that is safe to share between multiple threads.</summary>
		/// <param name="inner">The input <see cref="T:System.Text.RegularExpressions.Group" /> object.</param>
		/// <returns>A regular expression <see langword="Group" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is <see langword="null" />.</exception>
		// Token: 0x06000CF8 RID: 3320 RVA: 0x000359F0 File Offset: 0x00033BF0
		public static Group Synchronized(Group inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			CaptureCollection captures = inner.Captures;
			if (inner.Success)
			{
				captures.ForceInitialized();
			}
			return inner;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00035A21 File Offset: 0x00033C21
		// Note: this type is marked as 'beforefieldinit'.
		static Group()
		{
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal Group()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007E1 RID: 2017
		internal static readonly Group s_emptyGroup = new Group(string.Empty, Array.Empty<int>(), 0, string.Empty);

		// Token: 0x040007E2 RID: 2018
		internal readonly int[] _caps;

		// Token: 0x040007E3 RID: 2019
		internal int _capcount;

		// Token: 0x040007E4 RID: 2020
		internal CaptureCollection _capcoll;

		// Token: 0x040007E5 RID: 2021
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;
	}
}
