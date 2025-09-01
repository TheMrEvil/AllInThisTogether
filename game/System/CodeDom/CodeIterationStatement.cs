using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a <see langword="for" /> statement, or a loop through a block of statements, using a test expression as a condition for continuing to loop.</summary>
	// Token: 0x02000313 RID: 787
	[Serializable]
	public class CodeIterationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class.</summary>
		// Token: 0x060018FF RID: 6399 RVA: 0x0005FD3D File Offset: 0x0005DF3D
		public CodeIterationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class using the specified parameters.</summary>
		/// <param name="initStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the loop initialization statement.</param>
		/// <param name="testExpression">A <see cref="T:System.CodeDom.CodeExpression" /> containing the expression to test for exit condition.</param>
		/// <param name="incrementStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the per-cycle increment statement.</param>
		/// <param name="statements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements within the loop.</param>
		// Token: 0x06001900 RID: 6400 RVA: 0x0005FD50 File Offset: 0x0005DF50
		public CodeIterationStatement(CodeStatement initStatement, CodeExpression testExpression, CodeStatement incrementStatement, params CodeStatement[] statements)
		{
			this.InitStatement = initStatement;
			this.TestExpression = testExpression;
			this.IncrementStatement = incrementStatement;
			this.Statements.AddRange(statements);
		}

		/// <summary>Gets or sets the loop initialization statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the loop initialization statement.</returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0005FD85 File Offset: 0x0005DF85
		// (set) Token: 0x06001902 RID: 6402 RVA: 0x0005FD8D File Offset: 0x0005DF8D
		public CodeStatement InitStatement
		{
			[CompilerGenerated]
			get
			{
				return this.<InitStatement>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InitStatement>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the expression to test as the condition that continues the loop.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to test.</returns>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0005FD96 File Offset: 0x0005DF96
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x0005FD9E File Offset: 0x0005DF9E
		public CodeExpression TestExpression
		{
			[CompilerGenerated]
			get
			{
				return this.<TestExpression>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TestExpression>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the statement that is called after each loop cycle.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the per cycle increment statement.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0005FDA7 File Offset: 0x0005DFA7
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x0005FDAF File Offset: 0x0005DFAF
		public CodeStatement IncrementStatement
		{
			[CompilerGenerated]
			get
			{
				return this.<IncrementStatement>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IncrementStatement>k__BackingField = value;
			}
		}

		/// <summary>Gets the collection of statements to be executed within the loop.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statements within the loop.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0005FDB8 File Offset: 0x0005DFB8
		public CodeStatementCollection Statements
		{
			[CompilerGenerated]
			get
			{
				return this.<Statements>k__BackingField;
			}
		} = new CodeStatementCollection();

		// Token: 0x04000D90 RID: 3472
		[CompilerGenerated]
		private CodeStatement <InitStatement>k__BackingField;

		// Token: 0x04000D91 RID: 3473
		[CompilerGenerated]
		private CodeExpression <TestExpression>k__BackingField;

		// Token: 0x04000D92 RID: 3474
		[CompilerGenerated]
		private CodeStatement <IncrementStatement>k__BackingField;

		// Token: 0x04000D93 RID: 3475
		[CompilerGenerated]
		private readonly CodeStatementCollection <Statements>k__BackingField;
	}
}
