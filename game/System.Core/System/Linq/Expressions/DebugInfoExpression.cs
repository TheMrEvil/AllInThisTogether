using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Emits or clears a sequence point for debug information. This allows the debugger to highlight the correct source code when debugging.</summary>
	// Token: 0x02000243 RID: 579
	[DebuggerTypeProxy(typeof(Expression.DebugInfoExpressionProxy))]
	public class DebugInfoExpression : Expression
	{
		// Token: 0x06000FC5 RID: 4037 RVA: 0x000358EF File Offset: 0x00033AEF
		internal DebugInfoExpression(SymbolDocumentInfo document)
		{
			this.Document = document;
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="P:System.Linq.Expressions.DebugInfoExpression.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x000358FE File Offset: 0x00033AFE
		public sealed override Type Type
		{
			get
			{
				return typeof(void);
			}
		}

		/// <summary>Returns the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.ExpressionType" /> that represents this expression.</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003590A File Offset: 0x00033B0A
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.DebugInfo;
			}
		}

		/// <summary>Gets the start line of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</summary>
		/// <returns>The number of the start line of the code that was used to generate the wrapped expression.</returns>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public virtual int StartLine
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Gets the start column of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</summary>
		/// <returns>The number of the start column of the code that was used to generate the wrapped expression.</returns>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public virtual int StartColumn
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Gets the end line of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</summary>
		/// <returns>The number of the end line of the code that was used to generate the wrapped expression.</returns>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public virtual int EndLine
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Gets the end column of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</summary>
		/// <returns>The number of the end column of the code that was used to generate the wrapped expression.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public virtual int EndColumn
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Gets the <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that represents the source file.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that represents the source file.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0003590E File Offset: 0x00033B0E
		public SymbolDocumentInfo Document
		{
			[CompilerGenerated]
			get
			{
				return this.<Document>k__BackingField;
			}
		}

		/// <summary>Gets the value to indicate if the <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> is for clearing a sequence point.</summary>
		/// <returns>True if the <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> is for clearing a sequence point, otherwise false.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public virtual bool IsClear
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x06000FCE RID: 4046 RVA: 0x00035916 File Offset: 0x00033B16
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDebugInfo(this);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0000235B File Offset: 0x0000055B
		internal DebugInfoExpression()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000969 RID: 2409
		[CompilerGenerated]
		private readonly SymbolDocumentInfo <Document>k__BackingField;
	}
}
