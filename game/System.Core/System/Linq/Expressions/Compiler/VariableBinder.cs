using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002D2 RID: 722
	internal sealed class VariableBinder : ExpressionVisitor
	{
		// Token: 0x060015FC RID: 5628 RVA: 0x0004A2AD File Offset: 0x000484AD
		internal static AnalyzedTree Bind(LambdaExpression lambda)
		{
			VariableBinder variableBinder = new VariableBinder();
			variableBinder.Visit(lambda);
			return variableBinder._tree;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0004A2C1 File Offset: 0x000484C1
		private VariableBinder()
		{
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0004A2F8 File Offset: 0x000484F8
		public override Expression Visit(Expression node)
		{
			if (!this._guard.TryEnterOnCurrentStack())
			{
				return this._guard.RunOnEmptyStack<VariableBinder, Expression, Expression>((VariableBinder @this, Expression e) => @this.Visit(e), this, node);
			}
			return base.Visit(node);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0004A346 File Offset: 0x00048546
		protected internal override Expression VisitConstant(ConstantExpression node)
		{
			if (this._inQuote)
			{
				return node;
			}
			if (ILGen.CanEmitConstant(node.Value, node.Type))
			{
				return node;
			}
			this._constants.Peek().AddReference(node.Value, node.Type);
			return node;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0004A384 File Offset: 0x00048584
		protected internal override Expression VisitUnary(UnaryExpression node)
		{
			if (node.NodeType == ExpressionType.Quote)
			{
				bool inQuote = this._inQuote;
				this._inQuote = true;
				this.Visit(node.Operand);
				this._inQuote = inQuote;
			}
			else
			{
				this.Visit(node.Operand);
			}
			return node;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0004A3D0 File Offset: 0x000485D0
		protected internal override Expression VisitLambda<T>(Expression<T> node)
		{
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, true));
			this._constants.Push(this._tree.Constants[node] = new BoundConstants());
			base.Visit(this.MergeScopes(node));
			this._constants.Pop();
			this._scopes.Pop();
			return node;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0004A450 File Offset: 0x00048650
		protected internal override Expression VisitInvocation(InvocationExpression node)
		{
			LambdaExpression lambdaOperand = node.LambdaOperand;
			if (lambdaOperand != null)
			{
				this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(lambdaOperand, false));
				base.Visit(this.MergeScopes(lambdaOperand));
				this._scopes.Pop();
				int i = 0;
				int argumentCount = node.ArgumentCount;
				while (i < argumentCount)
				{
					this.Visit(node.GetArgument(i));
					i++;
				}
				return node;
			}
			return base.VisitInvocation(node);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0004A4D4 File Offset: 0x000486D4
		protected internal override Expression VisitBlock(BlockExpression node)
		{
			if (node.Variables.Count == 0)
			{
				base.Visit(node.Expressions);
				return node;
			}
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, false));
			base.Visit(this.MergeScopes(node));
			this._scopes.Pop();
			return node;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0004A540 File Offset: 0x00048740
		protected override CatchBlock VisitCatchBlock(CatchBlock node)
		{
			if (node.Variable == null)
			{
				this.Visit(node.Filter);
				this.Visit(node.Body);
				return node;
			}
			this._scopes.Push(this._tree.Scopes[node] = new CompilerScope(node, false));
			this.Visit(node.Filter);
			this.Visit(node.Body);
			this._scopes.Pop();
			return node;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0004A5C0 File Offset: 0x000487C0
		private ReadOnlyCollection<Expression> MergeScopes(Expression node)
		{
			LambdaExpression lambdaExpression = node as LambdaExpression;
			ReadOnlyCollection<Expression> readOnlyCollection;
			if (lambdaExpression != null)
			{
				readOnlyCollection = new ReadOnlyCollection<Expression>(new Expression[]
				{
					lambdaExpression.Body
				});
			}
			else
			{
				readOnlyCollection = ((BlockExpression)node).Expressions;
			}
			CompilerScope compilerScope = this._scopes.Peek();
			while (readOnlyCollection.Count == 1 && readOnlyCollection[0].NodeType == ExpressionType.Block)
			{
				BlockExpression blockExpression = (BlockExpression)readOnlyCollection[0];
				if (blockExpression.Variables.Count > 0)
				{
					foreach (ParameterExpression key in blockExpression.Variables)
					{
						if (compilerScope.Definitions.ContainsKey(key))
						{
							return readOnlyCollection;
						}
					}
					if (compilerScope.MergedScopes == null)
					{
						compilerScope.MergedScopes = new HashSet<BlockExpression>(ReferenceEqualityComparer<object>.Instance);
					}
					compilerScope.MergedScopes.Add(blockExpression);
					foreach (ParameterExpression key2 in blockExpression.Variables)
					{
						compilerScope.Definitions.Add(key2, VariableStorageKind.Local);
					}
				}
				readOnlyCollection = blockExpression.Expressions;
			}
			return readOnlyCollection;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0004A714 File Offset: 0x00048914
		protected internal override Expression VisitParameter(ParameterExpression node)
		{
			this.Reference(node, VariableStorageKind.Local);
			CompilerScope compilerScope = null;
			foreach (CompilerScope compilerScope2 in this._scopes)
			{
				if (compilerScope2.IsMethod || compilerScope2.Definitions.ContainsKey(node))
				{
					compilerScope = compilerScope2;
					break;
				}
			}
			if (compilerScope.ReferenceCount == null)
			{
				compilerScope.ReferenceCount = new Dictionary<ParameterExpression, int>();
			}
			Helpers.IncrementCount<ParameterExpression>(node, compilerScope.ReferenceCount);
			return node;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0004A7A4 File Offset: 0x000489A4
		protected internal override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
		{
			foreach (ParameterExpression node2 in node.Variables)
			{
				this.Reference(node2, VariableStorageKind.Hoisted);
			}
			return node;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0004A7F4 File Offset: 0x000489F4
		private void Reference(ParameterExpression node, VariableStorageKind storage)
		{
			CompilerScope compilerScope = null;
			foreach (CompilerScope compilerScope2 in this._scopes)
			{
				if (compilerScope2.Definitions.ContainsKey(node))
				{
					compilerScope = compilerScope2;
					break;
				}
				compilerScope2.NeedsClosure = true;
				if (compilerScope2.IsMethod)
				{
					storage = VariableStorageKind.Hoisted;
				}
			}
			if (compilerScope == null)
			{
				throw Error.UndefinedVariable(node.Name, node.Type, this.CurrentLambdaName);
			}
			if (storage == VariableStorageKind.Hoisted)
			{
				if (node.IsByRef)
				{
					throw Error.CannotCloseOverByRef(node.Name, this.CurrentLambdaName);
				}
				compilerScope.Definitions[node] = VariableStorageKind.Hoisted;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0004A8AC File Offset: 0x00048AAC
		private string CurrentLambdaName
		{
			get
			{
				foreach (CompilerScope compilerScope in this._scopes)
				{
					LambdaExpression lambdaExpression = compilerScope.Node as LambdaExpression;
					if (lambdaExpression != null)
					{
						return lambdaExpression.Name;
					}
				}
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x04000B30 RID: 2864
		private readonly AnalyzedTree _tree = new AnalyzedTree();

		// Token: 0x04000B31 RID: 2865
		private readonly Stack<CompilerScope> _scopes = new Stack<CompilerScope>();

		// Token: 0x04000B32 RID: 2866
		private readonly Stack<BoundConstants> _constants = new Stack<BoundConstants>();

		// Token: 0x04000B33 RID: 2867
		private readonly StackGuard _guard = new StackGuard();

		// Token: 0x04000B34 RID: 2868
		private bool _inQuote;

		// Token: 0x020002D3 RID: 723
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600160A RID: 5642 RVA: 0x0004A918 File Offset: 0x00048B18
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600160B RID: 5643 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x0600160C RID: 5644 RVA: 0x0004A924 File Offset: 0x00048B24
			internal Expression <Visit>b__7_0(VariableBinder @this, Expression e)
			{
				return @this.Visit(e);
			}

			// Token: 0x04000B35 RID: 2869
			public static readonly VariableBinder.<>c <>9 = new VariableBinder.<>c();

			// Token: 0x04000B36 RID: 2870
			public static Func<VariableBinder, Expression, Expression> <>9__7_0;
		}
	}
}
