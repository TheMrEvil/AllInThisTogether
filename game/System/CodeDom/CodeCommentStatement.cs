using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement consisting of a single comment.</summary>
	// Token: 0x02000300 RID: 768
	[Serializable]
	public class CodeCommentStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class.</summary>
		// Token: 0x0600188E RID: 6286 RVA: 0x0005F0D5 File Offset: 0x0005D2D5
		public CodeCommentStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified comment.</summary>
		/// <param name="comment">A <see cref="T:System.CodeDom.CodeComment" /> that indicates the comment.</param>
		// Token: 0x0600188F RID: 6287 RVA: 0x0005F6DD File Offset: 0x0005D8DD
		public CodeCommentStatement(CodeComment comment)
		{
			this.Comment = comment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified text as contents.</summary>
		/// <param name="text">The contents of the comment.</param>
		// Token: 0x06001890 RID: 6288 RVA: 0x0005F6EC File Offset: 0x0005D8EC
		public CodeCommentStatement(string text)
		{
			this.Comment = new CodeComment(text);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment.</param>
		/// <param name="docComment">
		///   <see langword="true" /> if the comment is a documentation comment; otherwise, <see langword="false" />.</param>
		// Token: 0x06001891 RID: 6289 RVA: 0x0005F700 File Offset: 0x0005D900
		public CodeCommentStatement(string text, bool docComment)
		{
			this.Comment = new CodeComment(text, docComment);
		}

		/// <summary>Gets or sets the contents of the comment.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeComment" /> that indicates the comment.</returns>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0005F715 File Offset: 0x0005D915
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x0005F71D File Offset: 0x0005D91D
		public CodeComment Comment
		{
			[CompilerGenerated]
			get
			{
				return this.<Comment>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Comment>k__BackingField = value;
			}
		}

		// Token: 0x04000D75 RID: 3445
		[CompilerGenerated]
		private CodeComment <Comment>k__BackingField;
	}
}
