using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that consists of a binary operation between two expressions.</summary>
	// Token: 0x020002F9 RID: 761
	[Serializable]
	public class CodeBinaryOperatorExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class.</summary>
		// Token: 0x06001859 RID: 6233 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeBinaryOperatorExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class using the specified parameters.</summary>
		/// <param name="left">The <see cref="T:System.CodeDom.CodeExpression" /> on the left of the operator.</param>
		/// <param name="op">A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> indicating the type of operator.</param>
		/// <param name="right">The <see cref="T:System.CodeDom.CodeExpression" /> on the right of the operator.</param>
		// Token: 0x0600185A RID: 6234 RVA: 0x0005F3E0 File Offset: 0x0005D5E0
		public CodeBinaryOperatorExpression(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			this.Right = right;
			this.Operator = op;
			this.Left = left;
		}

		/// <summary>Gets or sets the code expression on the right of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the right operand.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0005F3FD File Offset: 0x0005D5FD
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x0005F405 File Offset: 0x0005D605
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

		/// <summary>Gets or sets the code expression on the left of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the left operand.</returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0005F40E File Offset: 0x0005D60E
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x0005F416 File Offset: 0x0005D616
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

		/// <summary>Gets or sets the operator in the binary operator expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> that indicates the type of operator in the expression.</returns>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0005F41F File Offset: 0x0005D61F
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x0005F427 File Offset: 0x0005D627
		public CodeBinaryOperatorType Operator
		{
			[CompilerGenerated]
			get
			{
				return this.<Operator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Operator>k__BackingField = value;
			}
		}

		// Token: 0x04000D56 RID: 3414
		[CompilerGenerated]
		private CodeExpression <Right>k__BackingField;

		// Token: 0x04000D57 RID: 3415
		[CompilerGenerated]
		private CodeExpression <Left>k__BackingField;

		// Token: 0x04000D58 RID: 3416
		[CompilerGenerated]
		private CodeBinaryOperatorType <Operator>k__BackingField;
	}
}
