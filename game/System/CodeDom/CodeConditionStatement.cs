using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a conditional branch statement, typically represented as an <see langword="if" /> statement.</summary>
	// Token: 0x02000303 RID: 771
	[Serializable]
	public class CodeConditionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class.</summary>
		// Token: 0x060018A7 RID: 6311 RVA: 0x0005F881 File Offset: 0x0005DA81
		public CodeConditionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to evaluate.</param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is <see langword="true" />.</param>
		// Token: 0x060018A8 RID: 6312 RVA: 0x0005F89F File Offset: 0x0005DA9F
		public CodeConditionStatement(CodeExpression condition, params CodeStatement[] trueStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the condition to evaluate.</param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is <see langword="true" />.</param>
		/// <param name="falseStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is <see langword="false" />.</param>
		// Token: 0x060018A9 RID: 6313 RVA: 0x0005F8D0 File Offset: 0x0005DAD0
		public CodeConditionStatement(CodeExpression condition, CodeStatement[] trueStatements, CodeStatement[] falseStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
			this.FalseStatements.AddRange(falseStatements);
		}

		/// <summary>Gets or sets the expression to evaluate <see langword="true" /> or <see langword="false" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> to evaluate <see langword="true" /> or <see langword="false" />.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0005F90D File Offset: 0x0005DB0D
		// (set) Token: 0x060018AB RID: 6315 RVA: 0x0005F915 File Offset: 0x0005DB15
		public CodeExpression Condition
		{
			[CompilerGenerated]
			get
			{
				return this.<Condition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Condition>k__BackingField = value;
			}
		}

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to <see langword="true" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to <see langword="true" />.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005F91E File Offset: 0x0005DB1E
		public CodeStatementCollection TrueStatements
		{
			[CompilerGenerated]
			get
			{
				return this.<TrueStatements>k__BackingField;
			}
		} = new CodeStatementCollection();

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to <see langword="false" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to <see langword="false" />.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0005F926 File Offset: 0x0005DB26
		public CodeStatementCollection FalseStatements
		{
			[CompilerGenerated]
			get
			{
				return this.<FalseStatements>k__BackingField;
			}
		} = new CodeStatementCollection();

		// Token: 0x04000D7B RID: 3451
		[CompilerGenerated]
		private CodeExpression <Condition>k__BackingField;

		// Token: 0x04000D7C RID: 3452
		[CompilerGenerated]
		private readonly CodeStatementCollection <TrueStatements>k__BackingField;

		// Token: 0x04000D7D RID: 3453
		[CompilerGenerated]
		private readonly CodeStatementCollection <FalseStatements>k__BackingField;
	}
}
