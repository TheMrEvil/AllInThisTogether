using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002EA RID: 746
	public abstract class AQueryClause : ShimExpression
	{
		// Token: 0x060023EF RID: 9199 RVA: 0x000AD1F3 File Offset: 0x000AB3F3
		protected AQueryClause(QueryBlock block, Expression expr, Location loc) : base(expr)
		{
			this.block = block;
			this.loc = loc;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000AD20C File Offset: 0x000AB40C
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
			base.CloneTo(clonectx, target);
			AQueryClause aqueryClause = (AQueryClause)target;
			if (this.block != null)
			{
				aqueryClause.block = (QueryBlock)clonectx.LookupBlock(this.block);
			}
			if (this.next != null)
			{
				aqueryClause.next = (AQueryClause)this.next.Clone(clonectx);
			}
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000AD266 File Offset: 0x000AB466
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this.expr.Resolve(ec);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000AD274 File Offset: 0x000AB474
		public virtual Expression BuildQueryClause(ResolveContext ec, Expression lSide, Parameter parameter)
		{
			Arguments arguments = null;
			this.CreateArguments(ec, parameter, ref arguments);
			lSide = this.CreateQueryExpression(lSide, arguments);
			if (this.next != null)
			{
				parameter = this.CreateChildrenParameters(parameter);
				Select select = this.next as Select;
				if (select == null || select.IsRequired(parameter))
				{
					return this.next.BuildQueryClause(ec, lSide, parameter);
				}
				if (this.next.next != null)
				{
					return this.next.next.BuildQueryClause(ec, lSide, parameter);
				}
			}
			return lSide;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000AD2F1 File Offset: 0x000AB4F1
		protected virtual Parameter CreateChildrenParameters(Parameter parameter)
		{
			return parameter.Clone();
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000AD2FC File Offset: 0x000AB4FC
		protected virtual void CreateArguments(ResolveContext ec, Parameter parameter, ref Arguments args)
		{
			args = new Arguments(2);
			LambdaExpression lambdaExpression = new LambdaExpression(this.loc);
			this.block.SetParameter(parameter);
			lambdaExpression.Block = this.block;
			lambdaExpression.Block.AddStatement(new ContextualReturn(this.expr));
			args.Add(new Argument(lambdaExpression));
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000AD358 File Offset: 0x000AB558
		protected Invocation CreateQueryExpression(Expression lSide, Arguments arguments)
		{
			return new AQueryClause.QueryExpressionInvocation(new AQueryClause.QueryExpressionAccess(lSide, this.MethodName, this.loc), arguments);
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060023F6 RID: 9206
		protected abstract string MethodName { get; }

		// Token: 0x17000835 RID: 2101
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x000AD372 File Offset: 0x000AB572
		public AQueryClause Next
		{
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000AD37B File Offset: 0x000AB57B
		public AQueryClause Tail
		{
			get
			{
				if (this.next != null)
				{
					return this.next.Tail;
				}
				return this;
			}
		}

		// Token: 0x04000D7F RID: 3455
		public AQueryClause next;

		// Token: 0x04000D80 RID: 3456
		public QueryBlock block;

		// Token: 0x0200040B RID: 1035
		protected class QueryExpressionAccess : MemberAccess
		{
			// Token: 0x06002849 RID: 10313 RVA: 0x000BF1FF File Offset: 0x000BD3FF
			public QueryExpressionAccess(Expression expr, string methodName, Location loc) : base(expr, methodName, loc)
			{
			}

			// Token: 0x0600284A RID: 10314 RVA: 0x00080F8A File Offset: 0x0007F18A
			public QueryExpressionAccess(Expression expr, string methodName, TypeArguments typeArguments, Location loc) : base(expr, methodName, typeArguments, loc)
			{
			}

			// Token: 0x0600284B RID: 10315 RVA: 0x000BF20A File Offset: 0x000BD40A
			public override void Error_TypeDoesNotContainDefinition(ResolveContext ec, TypeSpec type, string name)
			{
				ec.Report.Error(1935, this.loc, "An implementation of `{0}' query expression pattern could not be found. Are you missing `System.Linq' using directive or `System.Core.dll' assembly reference?", name);
			}
		}

		// Token: 0x0200040C RID: 1036
		protected class QueryExpressionInvocation : Invocation, OverloadResolver.IErrorHandler
		{
			// Token: 0x0600284C RID: 10316 RVA: 0x000BF228 File Offset: 0x000BD428
			public QueryExpressionInvocation(AQueryClause.QueryExpressionAccess expr, Arguments arguments) : base(expr, arguments)
			{
			}

			// Token: 0x0600284D RID: 10317 RVA: 0x000BF232 File Offset: 0x000BD432
			protected override MethodGroupExpr DoResolveOverload(ResolveContext ec)
			{
				return this.mg.OverloadResolve(ec, ref this.arguments, this, OverloadResolver.Restrictions.None);
			}

			// Token: 0x0600284E RID: 10318 RVA: 0x000BF248 File Offset: 0x000BD448
			protected override Expression DoResolveDynamic(ResolveContext ec, Expression memberExpr)
			{
				ec.Report.Error(1979, this.loc, "Query expressions with a source or join sequence of type `dynamic' are not allowed");
				return null;
			}

			// Token: 0x0600284F RID: 10319 RVA: 0x000BF268 File Offset: 0x000BD468
			bool OverloadResolver.IErrorHandler.AmbiguousCandidates(ResolveContext ec, MemberSpec best, MemberSpec ambiguous)
			{
				ec.Report.SymbolRelatedToPreviousError(best);
				ec.Report.SymbolRelatedToPreviousError(ambiguous);
				ec.Report.Error(1940, this.loc, "Ambiguous implementation of the query pattern `{0}' for source type `{1}'", best.Name, this.mg.InstanceExpression.GetSignatureForError());
				return true;
			}

			// Token: 0x06002850 RID: 10320 RVA: 0x000022F4 File Offset: 0x000004F4
			bool OverloadResolver.IErrorHandler.ArgumentMismatch(ResolveContext rc, MemberSpec best, Argument arg, int index)
			{
				return false;
			}

			// Token: 0x06002851 RID: 10321 RVA: 0x000022F4 File Offset: 0x000004F4
			bool OverloadResolver.IErrorHandler.NoArgumentMatch(ResolveContext rc, MemberSpec best)
			{
				return false;
			}

			// Token: 0x06002852 RID: 10322 RVA: 0x000BF2C0 File Offset: 0x000BD4C0
			bool OverloadResolver.IErrorHandler.TypeInferenceFailed(ResolveContext rc, MemberSpec best)
			{
				TypeSpec typeSpec = ((MethodSpec)best).Parameters.ExtensionMethodType;
				if (typeSpec != null)
				{
					Argument argument = this.arguments[0];
					if (TypeManager.IsGenericType(typeSpec) && InflatedTypeSpec.ContainsTypeParameter(typeSpec))
					{
						TypeInferenceContext typeInferenceContext = new TypeInferenceContext(typeSpec.TypeArguments);
						typeInferenceContext.OutputTypeInference(rc, argument.Expr, typeSpec);
						if (typeInferenceContext.FixAllTypes(rc))
						{
							typeSpec = typeSpec.GetDefinition().MakeGenericType(rc, typeInferenceContext.InferredTypeArguments);
						}
					}
					if (!Convert.ImplicitConversionExists(rc, argument.Expr, typeSpec))
					{
						rc.Report.Error(1936, this.loc, "An implementation of `{0}' query expression pattern for source type `{1}' could not be found", best.Name, argument.Type.GetSignatureForError());
						return true;
					}
				}
				if (best.Name == "SelectMany")
				{
					rc.Report.Error(1943, this.loc, "An expression type is incorrect in a subsequent `from' clause in a query expression with source type `{0}'", this.arguments[0].GetSignatureForError());
				}
				else
				{
					rc.Report.Error(1942, this.loc, "An expression type in `{0}' clause is incorrect. Type inference failed in the call to `{1}'", best.Name.ToLowerInvariant(), best.Name);
				}
				return true;
			}
		}
	}
}
