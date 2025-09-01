using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single successful subexpression capture.</summary>
	// Token: 0x020001E7 RID: 487
	public class Capture
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x000354E3 File Offset: 0x000336E3
		internal Capture(string text, int index, int length)
		{
			this.Text = text;
			this.Index = index;
			this.Length = length;
		}

		/// <summary>The position in the original string where the first character of the captured substring is found.</summary>
		/// <returns>The zero-based starting position in the original string where the captured substring is found.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00035500 File Offset: 0x00033700
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00035508 File Offset: 0x00033708
		public int Index
		{
			[CompilerGenerated]
			get
			{
				return this.<Index>k__BackingField;
			}
			[CompilerGenerated]
			private protected set
			{
				this.<Index>k__BackingField = value;
			}
		}

		/// <summary>Gets the length of the captured substring.</summary>
		/// <returns>The length of the captured substring.</returns>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00035511 File Offset: 0x00033711
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x00035519 File Offset: 0x00033719
		public int Length
		{
			[CompilerGenerated]
			get
			{
				return this.<Length>k__BackingField;
			}
			[CompilerGenerated]
			private protected set
			{
				this.<Length>k__BackingField = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00035522 File Offset: 0x00033722
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x0003552A File Offset: 0x0003372A
		protected internal string Text
		{
			[CompilerGenerated]
			internal get
			{
				return this.<Text>k__BackingField;
			}
			[CompilerGenerated]
			private protected set
			{
				this.<Text>k__BackingField = value;
			}
		}

		/// <summary>Gets the captured substring from the input string.</summary>
		/// <returns>The substring that is captured by the match.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00035533 File Offset: 0x00033733
		public string Value
		{
			get
			{
				return this.Text.Substring(this.Index, this.Length);
			}
		}

		/// <summary>Retrieves the captured substring from the input string by calling the <see cref="P:System.Text.RegularExpressions.Capture.Value" /> property.</summary>
		/// <returns>The substring that was captured by the match.</returns>
		// Token: 0x06000CC1 RID: 3265 RVA: 0x0003554C File Offset: 0x0003374C
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00035554 File Offset: 0x00033754
		internal ReadOnlySpan<char> GetLeftSubstring()
		{
			return this.Text.AsSpan(0, this.Index);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00035568 File Offset: 0x00033768
		internal ReadOnlySpan<char> GetRightSubstring()
		{
			return this.Text.AsSpan(this.Index + this.Length, this.Text.Length - this.Index - this.Length);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal Capture()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007D2 RID: 2002
		[CompilerGenerated]
		private int <Index>k__BackingField;

		// Token: 0x040007D3 RID: 2003
		[CompilerGenerated]
		private int <Length>k__BackingField;

		// Token: 0x040007D4 RID: 2004
		[CompilerGenerated]
		private string <Text>k__BackingField;
	}
}
