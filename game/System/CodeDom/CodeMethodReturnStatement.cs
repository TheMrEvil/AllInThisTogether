using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a return value statement.</summary>
	// Token: 0x0200031B RID: 795
	[Serializable]
	public class CodeMethodReturnStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class.</summary>
		// Token: 0x06001946 RID: 6470 RVA: 0x0005F0D5 File Offset: 0x0005D2D5
		public CodeMethodReturnStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the return value.</param>
		// Token: 0x06001947 RID: 6471 RVA: 0x000603D2 File Offset: 0x0005E5D2
		public CodeMethodReturnStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the return value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value to return for the return statement, or <see langword="null" /> if the statement is part of a subroutine.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x000603E1 File Offset: 0x0005E5E1
		// (set) Token: 0x06001949 RID: 6473 RVA: 0x000603E9 File Offset: 0x0005E5E9
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

		// Token: 0x04000DB5 RID: 3509
		[CompilerGenerated]
		private CodeExpression <Expression>k__BackingField;
	}
}
