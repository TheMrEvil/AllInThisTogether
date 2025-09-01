using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that consists of a single expression.</summary>
	// Token: 0x0200030F RID: 783
	[Serializable]
	public class CodeExpressionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class.</summary>
		// Token: 0x060018EC RID: 6380 RVA: 0x0005F0D5 File Offset: 0x0005D2D5
		public CodeExpressionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class by using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> for the statement.</param>
		// Token: 0x060018ED RID: 6381 RVA: 0x0005FC58 File Offset: 0x0005DE58
		public CodeExpressionStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the expression for the statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression for the statement.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0005FC67 File Offset: 0x0005DE67
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0005FC6F File Offset: 0x0005DE6F
		public CodeExpression Expression
		{
			[CompilerGenerated]
			get
			{
				return this.<Expression>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Expression>k__BackingField = value;
			}
		}

		// Token: 0x04000D8A RID: 3466
		[CompilerGenerated]
		private CodeExpression <Expression>k__BackingField;
	}
}
