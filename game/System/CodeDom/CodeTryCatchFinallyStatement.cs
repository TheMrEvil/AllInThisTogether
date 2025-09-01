using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a <see langword="try" /> block with any number of <see langword="catch" /> clauses and, optionally, a <see langword="finally" /> block.</summary>
	// Token: 0x02000331 RID: 817
	[Serializable]
	public class CodeTryCatchFinallyStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class.</summary>
		// Token: 0x060019E5 RID: 6629 RVA: 0x00060E98 File Offset: 0x0005F098
		public CodeTryCatchFinallyStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try and catch clauses.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try.</param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch.</param>
		// Token: 0x060019E6 RID: 6630 RVA: 0x00060EC4 File Offset: 0x0005F0C4
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try, catch clauses, and finally statements.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try.</param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch.</param>
		/// <param name="finallyStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the finally statements to use.</param>
		// Token: 0x060019E7 RID: 6631 RVA: 0x00060F10 File Offset: 0x0005F110
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses, CodeStatement[] finallyStatements)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
			this.FinallyStatements.AddRange(finallyStatements);
		}

		/// <summary>Gets the statements to try.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements to try.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00060F68 File Offset: 0x0005F168
		public CodeStatementCollection TryStatements
		{
			[CompilerGenerated]
			get
			{
				return this.<TryStatements>k__BackingField;
			}
		} = new CodeStatementCollection();

		/// <summary>Gets the catch clauses to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> that indicates the catch clauses to use.</returns>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00060F70 File Offset: 0x0005F170
		public CodeCatchClauseCollection CatchClauses
		{
			[CompilerGenerated]
			get
			{
				return this.<CatchClauses>k__BackingField;
			}
		} = new CodeCatchClauseCollection();

		/// <summary>Gets the finally statements to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the finally statements.</returns>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00060F78 File Offset: 0x0005F178
		public CodeStatementCollection FinallyStatements
		{
			[CompilerGenerated]
			get
			{
				return this.<FinallyStatements>k__BackingField;
			}
		} = new CodeStatementCollection();

		// Token: 0x04000DDF RID: 3551
		[CompilerGenerated]
		private readonly CodeStatementCollection <TryStatements>k__BackingField;

		// Token: 0x04000DE0 RID: 3552
		[CompilerGenerated]
		private readonly CodeCatchClauseCollection <CatchClauses>k__BackingField;

		// Token: 0x04000DE1 RID: 3553
		[CompilerGenerated]
		private readonly CodeStatementCollection <FinallyStatements>k__BackingField;
	}
}
