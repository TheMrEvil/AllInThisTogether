using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001FF RID: 511
	internal class IndexerExpr : PropertyOrIndexerExpr<IndexerSpec>, OverloadResolver.IBaseMembersProvider
	{
		// Token: 0x06001A87 RID: 6791 RVA: 0x00081D6C File Offset: 0x0007FF6C
		public IndexerExpr(IList<MemberSpec> indexers, TypeSpec queriedType, ElementAccess ea) : this(indexers, queriedType, ea.Expr, ea.Arguments, ea.Location)
		{
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00081D88 File Offset: 0x0007FF88
		public IndexerExpr(IList<MemberSpec> indexers, TypeSpec queriedType, Expression instance, Arguments args, Location loc) : base(loc)
		{
			this.indexers = indexers;
			this.queried_type = queriedType;
			this.InstanceExpression = instance;
			this.arguments = args;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x00081DAF File Offset: 0x0007FFAF
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x00081DB7 File Offset: 0x0007FFB7
		protected override Arguments Arguments
		{
			get
			{
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00081DC0 File Offset: 0x0007FFC0
		protected override TypeSpec DeclaringType
		{
			get
			{
				return this.best_candidate.DeclaringType;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x00081DCD File Offset: 0x0007FFCD
		public override string KindName
		{
			get
			{
				return "indexer";
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0007F6BB File Offset: 0x0007D8BB
		public override string Name
		{
			get
			{
				return "this";
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00081DD4 File Offset: 0x0007FFD4
		public override bool ContainsEmitWithAwait()
		{
			return base.ContainsEmitWithAwait() || this.arguments.ContainsEmitWithAwait();
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00081DEC File Offset: 0x0007FFEC
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (base.ConditionalAccess)
			{
				base.Error_NullShortCircuitInsideExpressionTree(ec);
			}
			Arguments args = Arguments.CreateForExpressionTree(ec, this.arguments, new Expression[]
			{
				this.InstanceExpression.CreateExpressionTree(ec),
				new TypeOfMethod(base.Getter, this.loc)
			});
			return base.CreateExpressionFactoryCall(ec, "Call", args);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00081E4C File Offset: 0x0008004C
		public override void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			LocalTemporary localTemporary = null;
			if (isCompound)
			{
				this.emitting_compound_assignment = true;
				if (source is DynamicExpressionStatement)
				{
					this.Emit(ec, false);
				}
				else
				{
					source.Emit(ec);
				}
				this.emitting_compound_assignment = false;
				if (this.has_await_arguments)
				{
					localTemporary = new LocalTemporary(base.Type);
					localTemporary.Store(ec);
					this.arguments.Add(new Argument(localTemporary));
					if (leave_copy)
					{
						this.temp = localTemporary;
					}
					this.has_await_arguments = false;
				}
				else
				{
					this.arguments = null;
					if (leave_copy)
					{
						ec.Emit(OpCodes.Dup);
						this.temp = new LocalTemporary(base.Type);
						this.temp.Store(ec);
					}
				}
			}
			else
			{
				if (leave_copy)
				{
					if (ec.HasSet(BuilderContext.Options.AsyncBody) && (this.arguments.ContainsEmitWithAwait() || source.ContainsEmitWithAwait()))
					{
						source = source.EmitToField(ec);
					}
					else
					{
						this.temp = new LocalTemporary(base.Type);
						source.Emit(ec);
						this.temp.Store(ec);
						source = this.temp;
					}
				}
				this.arguments.Add(new Argument(source));
			}
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = this.InstanceExpression;
			if (this.arguments == null)
			{
				callEmitter.InstanceExpressionOnStack = true;
			}
			callEmitter.Emit(ec, base.Setter, this.arguments, this.loc);
			if (this.temp != null)
			{
				this.temp.Emit(ec);
				this.temp.Release(ec);
			}
			else if (leave_copy)
			{
				source.Emit(ec);
			}
			if (localTemporary != null)
			{
				localTemporary.Release(ec);
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00081FE1 File Offset: 0x000801E1
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			base.FlowAnalysis(fc);
			this.arguments.FlowAnalysis(fc, null);
			if (this.conditional_access_receiver)
			{
				fc.ConditionalAccessEnd();
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00082005 File Offset: 0x00080205
		public override string GetSignatureForError()
		{
			return this.best_candidate.GetSignatureForError();
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00082014 File Offset: 0x00080214
		public override Expression MakeAssignExpression(BuilderContext ctx, Expression source)
		{
			Expression[] second = new Expression[]
			{
				source.MakeExpression(ctx)
			};
			return Arguments.MakeExpression(this.arguments, ctx).Concat(second).First<Expression>();
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0008204C File Offset: 0x0008024C
		public override Expression MakeExpression(BuilderContext ctx)
		{
			Expression[] array = Arguments.MakeExpression(this.arguments, ctx);
			return Expression.Call(this.InstanceExpression.MakeExpression(ctx), (MethodInfo)base.Getter.GetMetaInfo(), array);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00082088 File Offset: 0x00080288
		protected override Expression OverloadResolve(ResolveContext rc, Expression right_side)
		{
			if (this.best_candidate != null)
			{
				return this;
			}
			this.eclass = ExprClass.IndexerAccess;
			bool flag;
			this.arguments.Resolve(rc, out flag);
			if (this.indexers == null && this.InstanceExpression.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				flag = true;
			}
			else
			{
				OverloadResolver overloadResolver = new OverloadResolver(this.indexers, OverloadResolver.Restrictions.None, this.loc);
				overloadResolver.BaseMembersProvider = this;
				overloadResolver.InstanceQualifier = this;
				this.best_candidate = overloadResolver.ResolveMember<IndexerSpec>(rc, ref this.arguments);
				if (this.best_candidate != null)
				{
					this.type = overloadResolver.BestCandidateReturnType;
				}
				else if (!overloadResolver.BestCandidateIsDynamic)
				{
					return null;
				}
			}
			if (flag)
			{
				Arguments arguments = new Arguments(this.arguments.Count + 1);
				if (base.IsBase)
				{
					rc.Report.Error(1972, this.loc, "The indexer base access cannot be dynamically dispatched. Consider casting the dynamic arguments or eliminating the base access");
				}
				else
				{
					arguments.Add(new Argument(this.InstanceExpression));
				}
				arguments.AddRange(this.arguments);
				this.best_candidate = null;
				return new DynamicIndexBinder(arguments, this.loc);
			}
			if (right_side != null)
			{
				base.ResolveInstanceExpression(rc, right_side);
			}
			return this;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000821A8 File Offset: 0x000803A8
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			IndexerExpr indexerExpr = (IndexerExpr)t;
			if (this.arguments != null)
			{
				indexerExpr.arguments = this.arguments.Clone(clonectx);
			}
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000821D6 File Offset: 0x000803D6
		public void SetConditionalAccessReceiver()
		{
			this.conditional_access_receiver = true;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000821DF File Offset: 0x000803DF
		public override void SetTypeArguments(ResolveContext ec, TypeArguments ta)
		{
			Expression.Error_TypeArgumentsCannotBeUsed(ec, "indexer", this.GetSignatureForError(), this.loc);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000821F8 File Offset: 0x000803F8
		IList<MemberSpec> OverloadResolver.IBaseMembersProvider.GetBaseMembers(TypeSpec baseType)
		{
			if (baseType != null)
			{
				return MemberCache.FindMembers(baseType, MemberCache.IndexerNameAlias, false);
			}
			return null;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0008220C File Offset: 0x0008040C
		IParametersMember OverloadResolver.IBaseMembersProvider.GetOverrideMemberParameters(MemberSpec member)
		{
			if (this.queried_type == member.DeclaringType)
			{
				return null;
			}
			MemberFilter filter = new MemberFilter(MemberCache.IndexerNameAlias, 0, MemberKind.Indexer, ((IndexerSpec)member).Parameters, null);
			return MemberCache.FindMember(this.queried_type, filter, BindingRestriction.InstanceOnly | BindingRestriction.OverrideOnly) as IParametersMember;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000055E7 File Offset: 0x000037E7
		MethodGroupExpr OverloadResolver.IBaseMembersProvider.LookupExtensionMethod(ResolveContext rc)
		{
			return null;
		}

		// Token: 0x040009F4 RID: 2548
		private IList<MemberSpec> indexers;

		// Token: 0x040009F5 RID: 2549
		private Arguments arguments;

		// Token: 0x040009F6 RID: 2550
		private TypeSpec queried_type;
	}
}
