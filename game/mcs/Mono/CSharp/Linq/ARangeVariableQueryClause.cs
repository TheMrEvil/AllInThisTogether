using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002EB RID: 747
	public abstract class ARangeVariableQueryClause : AQueryClause
	{
		// Token: 0x060023F9 RID: 9209 RVA: 0x000AD392 File Offset: 0x000AB592
		protected ARangeVariableQueryClause(QueryBlock block, RangeVariable identifier, Expression expr, Location loc) : base(block, expr, loc)
		{
			this.identifier = identifier;
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000AD3A5 File Offset: 0x000AB5A5
		public RangeVariable Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000AD3AD File Offset: 0x000AB5AD
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x000AD3B5 File Offset: 0x000AB5B5
		public FullNamedExpression IdentifierType
		{
			[CompilerGenerated]
			get
			{
				return this.<IdentifierType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IdentifierType>k__BackingField = value;
			}
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000AD3BE File Offset: 0x000AB5BE
		protected Invocation CreateCastExpression(Expression lSide)
		{
			return new AQueryClause.QueryExpressionInvocation(new AQueryClause.QueryExpressionAccess(lSide, "Cast", new TypeArguments(new FullNamedExpression[]
			{
				this.IdentifierType
			}), this.loc), null);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000AD3EB File Offset: 0x000AB5EB
		protected override Parameter CreateChildrenParameters(Parameter parameter)
		{
			return new QueryBlock.TransparentParameter(parameter.Clone(), this.GetIntoVariable());
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000AD400 File Offset: 0x000AB600
		protected static Expression CreateRangeVariableType(ResolveContext rc, Parameter parameter, RangeVariable name, Expression init)
		{
			return new NewAnonymousType(new List<AnonymousTypeParameter>(2)
			{
				new AnonymousTypeParameter(new ARangeVariableQueryClause.RangeParameterReference(parameter), parameter.Name, parameter.Location),
				new ARangeVariableQueryClause.RangeAnonymousTypeParameter(init, name)
			}, rc.MemberContext.CurrentMemberDefinition.Parent, name.Location);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000AD3A5 File Offset: 0x000AB5A5
		protected virtual RangeVariable GetIntoVariable()
		{
			return this.identifier;
		}

		// Token: 0x04000D81 RID: 3457
		protected RangeVariable identifier;

		// Token: 0x04000D82 RID: 3458
		[CompilerGenerated]
		private FullNamedExpression <IdentifierType>k__BackingField;

		// Token: 0x0200040D RID: 1037
		private sealed class RangeAnonymousTypeParameter : AnonymousTypeParameter
		{
			// Token: 0x06002853 RID: 10323 RVA: 0x000BF3E2 File Offset: 0x000BD5E2
			public RangeAnonymousTypeParameter(Expression initializer, RangeVariable parameter) : base(initializer, parameter.Name, parameter.Location)
			{
			}

			// Token: 0x06002854 RID: 10324 RVA: 0x000BF3F7 File Offset: 0x000BD5F7
			protected override void Error_InvalidInitializer(ResolveContext ec, string initializer)
			{
				ec.Report.Error(1932, this.loc, "A range variable `{0}' cannot be initialized with `{1}'", this.Name, initializer);
			}
		}

		// Token: 0x0200040E RID: 1038
		private class RangeParameterReference : ParameterReference
		{
			// Token: 0x06002855 RID: 10325 RVA: 0x000BF41B File Offset: 0x000BD61B
			public RangeParameterReference(Parameter p) : base(null, p.Location)
			{
				this.parameter = p;
			}

			// Token: 0x06002856 RID: 10326 RVA: 0x000BF431 File Offset: 0x000BD631
			protected override Expression DoResolve(ResolveContext ec)
			{
				this.pi = ec.CurrentBlock.ParametersBlock.GetParameterInfo(this.parameter);
				return base.DoResolve(ec);
			}

			// Token: 0x0400118F RID: 4495
			private Parameter parameter;
		}
	}
}
