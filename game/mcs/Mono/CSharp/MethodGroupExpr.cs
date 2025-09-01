using System;
using System.Collections.Generic;
using System.Linq;

namespace Mono.CSharp
{
	// Token: 0x020001BE RID: 446
	public class MethodGroupExpr : MemberExpr, OverloadResolver.IBaseMembersProvider
	{
		// Token: 0x06001747 RID: 5959 RVA: 0x0006F1B6 File Offset: 0x0006D3B6
		public MethodGroupExpr(IList<MemberSpec> mi, TypeSpec type, Location loc)
		{
			this.Methods = mi;
			this.loc = loc;
			this.type = InternalType.MethodGroup;
			this.eclass = ExprClass.MethodGroup;
			this.queried_type = type;
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0006F1E5 File Offset: 0x0006D3E5
		public MethodGroupExpr(MethodSpec m, TypeSpec type, Location loc) : this(new MemberSpec[]
		{
			m
		}, type, loc)
		{
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x0006F1F9 File Offset: 0x0006D3F9
		public MethodSpec BestCandidate
		{
			get
			{
				return this.best_candidate;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x0006F201 File Offset: 0x0006D401
		public TypeSpec BestCandidateReturnType
		{
			get
			{
				return this.best_candidate_return;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x0006F209 File Offset: 0x0006D409
		public IList<MemberSpec> Candidates
		{
			get
			{
				return this.Methods;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0006F211 File Offset: 0x0006D411
		protected override TypeSpec DeclaringType
		{
			get
			{
				return this.queried_type;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x0006F219 File Offset: 0x0006D419
		public bool IsConditionallyExcluded
		{
			get
			{
				return this.Methods == MethodGroupExpr.Excluded;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0006F228 File Offset: 0x0006D428
		public override bool IsInstance
		{
			get
			{
				return this.best_candidate != null && !this.best_candidate.IsStatic;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x0006F242 File Offset: 0x0006D442
		public override bool IsSideEffectFree
		{
			get
			{
				return this.InstanceExpression == null || this.InstanceExpression.IsSideEffectFree;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0006F259 File Offset: 0x0006D459
		public override bool IsStatic
		{
			get
			{
				return this.best_candidate != null && this.best_candidate.IsStatic;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x0006F270 File Offset: 0x0006D470
		public override string KindName
		{
			get
			{
				return "method";
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x0006F277 File Offset: 0x0006D477
		public override string Name
		{
			get
			{
				if (this.best_candidate != null)
				{
					return this.best_candidate.Name;
				}
				return this.Methods.First<MemberSpec>().Name;
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0006F29D File Offset: 0x0006D49D
		public static MethodGroupExpr CreatePredefined(MethodSpec best, TypeSpec queriedType, Location loc)
		{
			return new MethodGroupExpr(best, queriedType, loc)
			{
				best_candidate = best,
				best_candidate_return = best.ReturnType
			};
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0006F2BA File Offset: 0x0006D4BA
		public override string GetSignatureForError()
		{
			if (this.best_candidate != null)
			{
				return this.best_candidate.GetSignatureForError();
			}
			return this.Methods.First<MemberSpec>().GetSignatureForError();
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0006F2E0 File Offset: 0x0006D4E0
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.best_candidate == null)
			{
				ec.Report.Error(1953, this.loc, "An expression tree cannot contain an expression with method group");
				return null;
			}
			if (this.IsConditionallyExcluded)
			{
				ec.Report.Error(765, this.loc, "Partial methods with only a defining declaration or removed conditional methods cannot be used in an expression tree");
			}
			if (base.ConditionalAccess)
			{
				base.Error_NullShortCircuitInsideExpressionTree(ec);
			}
			return new TypeOfMethod(this.best_candidate, this.loc);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0006F355 File Offset: 0x0006D555
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.eclass = ExprClass.MethodGroup;
			if (this.InstanceExpression != null)
			{
				this.InstanceExpression = this.InstanceExpression.Resolve(ec);
				if (this.InstanceExpression == null)
				{
					return null;
				}
			}
			return this;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0000225C File Offset: 0x0000045C
		public override void Emit(EmitContext ec)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0006F384 File Offset: 0x0006D584
		public void EmitCall(EmitContext ec, Arguments arguments, bool statement)
		{
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.InstanceExpression = this.InstanceExpression;
			callEmitter.ConditionalAccess = base.ConditionalAccess;
			if (statement)
			{
				callEmitter.EmitStatement(ec, this.best_candidate, arguments, this.loc);
				return;
			}
			callEmitter.Emit(ec, this.best_candidate, arguments, this.loc);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0006F3E4 File Offset: 0x0006D5E4
		public void EmitCall(EmitContext ec, Arguments arguments, TypeSpec conditionalAccessReceiver, bool statement)
		{
			ec.ConditionalAccess = new ConditionalAccessContext(conditionalAccessReceiver, ec.DefineLabel())
			{
				Statement = statement
			};
			this.EmitCall(ec, arguments, statement);
			ec.CloseConditionalAccess((!statement && this.best_candidate_return != conditionalAccessReceiver && conditionalAccessReceiver.IsNullableType) ? conditionalAccessReceiver : null);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0006F434 File Offset: 0x0006D634
		public override void Error_ValueCannotBeConverted(ResolveContext ec, TypeSpec target, bool expl)
		{
			ec.Report.Error(428, this.loc, "Cannot convert method group `{0}' to non-delegate type `{1}'. Consider using parentheses to invoke the method", this.Name, target.GetSignatureForError());
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0006F460 File Offset: 0x0006D660
		public bool HasAccessibleCandidate(ResolveContext rc)
		{
			using (IEnumerator<MemberSpec> enumerator = this.Candidates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsAccessible(rc))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0006F4B4 File Offset: 0x0006D6B4
		public static bool IsExtensionMethodArgument(Expression expr)
		{
			return !(expr is TypeExpr) && !(expr is BaseThis);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0006F4CC File Offset: 0x0006D6CC
		public virtual MethodGroupExpr OverloadResolve(ResolveContext ec, ref Arguments args, OverloadResolver.IErrorHandler cerrors, OverloadResolver.Restrictions restr)
		{
			if (this.best_candidate != null && this.best_candidate.Kind == MemberKind.Destructor)
			{
				return this;
			}
			OverloadResolver overloadResolver = new OverloadResolver(this.Methods, this.type_arguments, restr, this.loc);
			if ((restr & OverloadResolver.Restrictions.NoBaseMembers) == OverloadResolver.Restrictions.None)
			{
				overloadResolver.BaseMembersProvider = this;
				overloadResolver.InstanceQualifier = this;
			}
			if (cerrors != null)
			{
				overloadResolver.CustomErrors = cerrors;
			}
			this.best_candidate = overloadResolver.ResolveMember<MethodSpec>(ec, ref args);
			if (this.best_candidate == null)
			{
				if (!overloadResolver.BestCandidateIsDynamic)
				{
					return null;
				}
				if (this.simple_name != null && ec.IsStatic)
				{
					this.InstanceExpression = base.ProbeIdenticalTypeName(ec, this.InstanceExpression, this.simple_name);
				}
				return this;
			}
			else
			{
				if (overloadResolver.BestCandidateNewMethodGroup != null)
				{
					return overloadResolver.BestCandidateNewMethodGroup;
				}
				if (this.best_candidate.Kind == MemberKind.Method && (restr & OverloadResolver.Restrictions.ProbingOnly) == OverloadResolver.Restrictions.None)
				{
					if (this.InstanceExpression != null)
					{
						if (this.best_candidate.IsExtensionMethod && args[0].Expr == this.InstanceExpression)
						{
							this.InstanceExpression = null;
						}
						else
						{
							if (this.simple_name != null && this.best_candidate.IsStatic)
							{
								this.InstanceExpression = base.ProbeIdenticalTypeName(ec, this.InstanceExpression, this.simple_name);
							}
							this.InstanceExpression.Resolve(ec, ResolveFlags.VariableOrValue | ResolveFlags.Type | ResolveFlags.MethodGroup);
						}
					}
					base.ResolveInstanceExpression(ec, null);
				}
				MethodSpec methodSpec = base.CandidateToBaseOverride(ec, this.best_candidate);
				if (methodSpec == this.best_candidate)
				{
					this.best_candidate_return = overloadResolver.BestCandidateReturnType;
				}
				else
				{
					this.best_candidate = methodSpec;
					this.best_candidate_return = this.best_candidate.ReturnType;
				}
				if (this.best_candidate.IsGeneric && (restr & OverloadResolver.Restrictions.ProbingOnly) == OverloadResolver.Restrictions.None && TypeParameterSpec.HasAnyTypeParameterConstrained(this.best_candidate.GenericDefinition))
				{
					ConstraintChecker constraintChecker = new ConstraintChecker(ec);
					constraintChecker.CheckAll(this.best_candidate.GetGenericMethodDefinition(), this.best_candidate.TypeArguments, this.best_candidate.Constraints, this.loc);
				}
				if (this.best_candidate.IsVirtual && (this.best_candidate.DeclaringType.Modifiers & Modifiers.PROTECTED) != (Modifiers)0 && this.best_candidate.MemberDefinition.IsImported && !this.best_candidate.DeclaringType.IsAccessible(ec))
				{
					ec.Report.SymbolRelatedToPreviousError(this.best_candidate);
					Expression.ErrorIsInaccesible(ec, this.best_candidate.GetSignatureForError(), this.loc);
				}
				if (this.best_candidate_return.Kind == MemberKind.Void && this.best_candidate.IsConditionallyExcluded(ec))
				{
					this.Methods = MethodGroupExpr.Excluded;
				}
				return this;
			}
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0006F750 File Offset: 0x0006D950
		public override MemberExpr ResolveMemberAccess(ResolveContext ec, Expression left, SimpleName original)
		{
			FieldExpr fieldExpr = left as FieldExpr;
			if (fieldExpr != null)
			{
				fieldExpr.Spec.MemberDefinition.SetIsAssigned();
			}
			this.simple_name = original;
			return base.ResolveMemberAccess(ec, left, original);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0006F787 File Offset: 0x0006D987
		public override void SetTypeArguments(ResolveContext ec, TypeArguments ta)
		{
			this.type_arguments = ta;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0006F790 File Offset: 0x0006D990
		public virtual IList<MemberSpec> GetBaseMembers(TypeSpec baseType)
		{
			if (baseType != null)
			{
				return MemberCache.FindMembers(baseType, this.Methods[0].Name, false);
			}
			return null;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0006F7AF File Offset: 0x0006D9AF
		public IParametersMember GetOverrideMemberParameters(MemberSpec member)
		{
			if (this.queried_type == member.DeclaringType)
			{
				return null;
			}
			return MemberCache.FindMember(this.queried_type, new MemberFilter((MethodSpec)member), BindingRestriction.InstanceOnly | BindingRestriction.OverrideOnly) as IParametersMember;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0006F7E0 File Offset: 0x0006D9E0
		public virtual MethodGroupExpr LookupExtensionMethod(ResolveContext rc)
		{
			if (this.InstanceExpression == null || this.InstanceExpression.eclass == ExprClass.Type)
			{
				return null;
			}
			if (!MethodGroupExpr.IsExtensionMethodArgument(this.InstanceExpression))
			{
				return null;
			}
			int arity = (this.type_arguments == null) ? 0 : this.type_arguments.Count;
			ExtensionMethodCandidates extensionMethodCandidates = rc.LookupExtensionMethod(this.Methods[0].Name, arity);
			if (extensionMethodCandidates == null)
			{
				return null;
			}
			ExtensionMethodGroupExpr extensionMethodGroupExpr = new ExtensionMethodGroupExpr(extensionMethodCandidates, this.InstanceExpression, this.loc);
			extensionMethodGroupExpr.SetTypeArguments(rc, this.type_arguments);
			return extensionMethodGroupExpr;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0006F868 File Offset: 0x0006DA68
		// Note: this type is marked as 'beforefieldinit'.
		static MethodGroupExpr()
		{
		}

		// Token: 0x0400096B RID: 2411
		private static readonly MemberSpec[] Excluded = new MemberSpec[0];

		// Token: 0x0400096C RID: 2412
		protected IList<MemberSpec> Methods;

		// Token: 0x0400096D RID: 2413
		private MethodSpec best_candidate;

		// Token: 0x0400096E RID: 2414
		private TypeSpec best_candidate_return;

		// Token: 0x0400096F RID: 2415
		protected TypeArguments type_arguments;

		// Token: 0x04000970 RID: 2416
		private SimpleName simple_name;

		// Token: 0x04000971 RID: 2417
		protected TypeSpec queried_type;
	}
}
