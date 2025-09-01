using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a simple assignment statement.</summary>
	// Token: 0x020002F2 RID: 754
	[Serializable]
	public class CodeAssignStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class.</summary>
		// Token: 0x06001821 RID: 6177 RVA: 0x0005F0D5 File Offset: 0x0005D2D5
		public CodeAssignStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class using the specified expressions.</summary>
		/// <param name="left">The variable to assign to.</param>
		/// <param name="right">The value to assign.</param>
		// Token: 0x06001822 RID: 6178 RVA: 0x0005F0DD File Offset: 0x0005D2DD
		public CodeAssignStatement(CodeExpression left, CodeExpression right)
		{
			this.Left = left;
			this.Right = right;
		}

		/// <summary>Gets or sets the expression representing the object or reference to assign to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign to.</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0005F0F3 File Offset: 0x0005D2F3
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x0005F0FB File Offset: 0x0005D2FB
		public CodeExpression Left
		{
			[CompilerGenerated]
			get
			{
				return this.<Left>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Left>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the expression representing the object or reference to assign.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0005F104 File Offset: 0x0005D304
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x0005F10C File Offset: 0x0005D30C
		public CodeExpression Right
		{
			[CompilerGenerated]
			get
			{
				return this.<Right>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Right>k__BackingField = value;
			}
		}

		// Token: 0x04000D4D RID: 3405
		[CompilerGenerated]
		private CodeExpression <Left>k__BackingField;

		// Token: 0x04000D4E RID: 3406
		[CompilerGenerated]
		private CodeExpression <Right>k__BackingField;
	}
}
