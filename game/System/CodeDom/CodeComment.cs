using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a comment.</summary>
	// Token: 0x020002FF RID: 767
	[Serializable]
	public class CodeComment : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class.</summary>
		// Token: 0x06001887 RID: 6279 RVA: 0x0005F685 File Offset: 0x0005D885
		public CodeComment()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class with the specified text as contents.</summary>
		/// <param name="text">The contents of the comment.</param>
		// Token: 0x06001888 RID: 6280 RVA: 0x0005F68D File Offset: 0x0005D88D
		public CodeComment(string text)
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment.</param>
		/// <param name="docComment">
		///   <see langword="true" /> if the comment is a documentation comment; otherwise, <see langword="false" />.</param>
		// Token: 0x06001889 RID: 6281 RVA: 0x0005F69C File Offset: 0x0005D89C
		public CodeComment(string text, bool docComment)
		{
			this.Text = text;
			this.DocComment = docComment;
		}

		/// <summary>Gets or sets a value that indicates whether the comment is a documentation comment.</summary>
		/// <returns>
		///   <see langword="true" /> if the comment is a documentation comment; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0005F6B2 File Offset: 0x0005D8B2
		// (set) Token: 0x0600188B RID: 6283 RVA: 0x0005F6BA File Offset: 0x0005D8BA
		public bool DocComment
		{
			[CompilerGenerated]
			get
			{
				return this.<DocComment>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DocComment>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the text of the comment.</summary>
		/// <returns>A string containing the comment text.</returns>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x0005F6C3 File Offset: 0x0005D8C3
		// (set) Token: 0x0600188D RID: 6285 RVA: 0x0005F6D4 File Offset: 0x0005D8D4
		public string Text
		{
			get
			{
				return this._text ?? string.Empty;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x04000D73 RID: 3443
		private string _text;

		// Token: 0x04000D74 RID: 3444
		[CompilerGenerated]
		private bool <DocComment>k__BackingField;
	}
}
