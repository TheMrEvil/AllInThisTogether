using System;
using System.Linq.Expressions;
using System.Reflection.Emit;
using Mono.CSharp.Linq;

namespace Mono.CSharp
{
	// Token: 0x020001C4 RID: 452
	internal abstract class PropertyOrIndexerExpr<T> : MemberExpr, IDynamicAssign, IAssignMethod where T : PropertySpec
	{
		// Token: 0x060017D0 RID: 6096 RVA: 0x00071B06 File Offset: 0x0006FD06
		protected PropertyOrIndexerExpr(Location l)
		{
			this.loc = l;
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060017D1 RID: 6097
		// (set) Token: 0x060017D2 RID: 6098
		protected abstract Arguments Arguments { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00073264 File Offset: 0x00071464
		// (set) Token: 0x060017D4 RID: 6100 RVA: 0x0007326C File Offset: 0x0007146C
		public MethodSpec Getter
		{
			get
			{
				return this.getter;
			}
			set
			{
				this.getter = value;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x00073275 File Offset: 0x00071475
		// (set) Token: 0x060017D6 RID: 6102 RVA: 0x0007327D File Offset: 0x0007147D
		public MethodSpec Setter
		{
			get
			{
				return this.setter;
			}
			set
			{
				this.setter = value;
			}
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00073288 File Offset: 0x00071488
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.eclass == ExprClass.Unresolved)
			{
				base.ResolveConditionalAccessReceiver(ec);
				Expression expression = this.OverloadResolve(ec, null);
				if (expression == null)
				{
					return null;
				}
				if (expression != this)
				{
					return expression.Resolve(ec);
				}
				if (this.conditional_access_receiver)
				{
					this.type = Expression.LiftMemberType(ec, this.type);
					ec.With(ResolveContext.Options.ConditionalAccessReceiver, false);
				}
			}
			if (!this.ResolveGetter(ec))
			{
				return null;
			}
			return this;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000732F4 File Offset: 0x000714F4
		public override Expression DoResolveLValue(ResolveContext rc, Expression right_side)
		{
			if (this.HasConditionalAccess())
			{
				base.Error_NullPropagatingLValue(rc);
			}
			if (right_side == EmptyExpression.OutAccess)
			{
				INamedBlockVariable namedBlockVariable = null;
				if (this.best_candidate != null && rc.CurrentBlock.ParametersBlock.TopBlock.GetLocalName(this.best_candidate.Name, rc.CurrentBlock, ref namedBlockVariable) && namedBlockVariable is RangeVariable)
				{
					rc.Report.Error(1939, this.loc, "A range variable `{0}' may not be passes as `ref' or `out' parameter", this.best_candidate.Name);
				}
				else
				{
					right_side.DoResolveLValue(rc, this);
				}
				return null;
			}
			if (this.eclass == ExprClass.Unresolved)
			{
				Expression expression = this.OverloadResolve(rc, right_side);
				if (expression == null)
				{
					return null;
				}
				if (expression != this)
				{
					return expression.ResolveLValue(rc, right_side);
				}
			}
			else
			{
				base.ResolveInstanceExpression(rc, right_side);
			}
			if (this.best_candidate.HasSet)
			{
				if (!this.best_candidate.Set.IsAccessible(rc) || !this.best_candidate.Set.DeclaringType.IsAccessible(rc))
				{
					if (this.best_candidate.HasDifferentAccessibility)
					{
						rc.Report.SymbolRelatedToPreviousError(this.best_candidate.Set);
						rc.Report.Error(272, this.loc, "The property or indexer `{0}' cannot be used in this context because the set accessor is inaccessible", this.GetSignatureForError());
					}
					else
					{
						rc.Report.SymbolRelatedToPreviousError(this.best_candidate.Set);
						Expression.ErrorIsInaccesible(rc, this.best_candidate.GetSignatureForError(), this.loc);
					}
				}
				if (this.best_candidate.HasDifferentAccessibility)
				{
					base.CheckProtectedMemberAccess(rc, this.best_candidate.Set);
				}
				this.setter = base.CandidateToBaseOverride(rc, this.best_candidate.Set);
				return this;
			}
			if (this.ResolveAutopropertyAssignment(rc, right_side))
			{
				return this;
			}
			rc.Report.Error(200, this.loc, "Property or indexer `{0}' cannot be assigned to (it is read-only)", this.GetSignatureForError());
			return null;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0007350C File Offset: 0x0007170C
		private void EmitConditionalAccess(EmitContext ec, ref CallEmitter call, MethodSpec method, Arguments arguments)
		{
			ec.ConditionalAccess = new ConditionalAccessContext(this.type, ec.DefineLabel());
			call.Emit(ec, method, arguments, this.loc);
			ec.CloseConditionalAccess((method.ReturnType != this.type && this.type.IsNullableType) ? this.type : null);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0007356C File Offset: 0x0007176C
		public virtual void Emit(EmitContext ec, bool leave_copy)
		{
			CallEmitter callEmitter = default(CallEmitter);
			callEmitter.ConditionalAccess = base.ConditionalAccess;
			callEmitter.InstanceExpression = this.InstanceExpression;
			if (this.has_await_arguments)
			{
				callEmitter.HasAwaitArguments = true;
			}
			else
			{
				callEmitter.DuplicateArguments = this.emitting_compound_assignment;
			}
			if (this.conditional_access_receiver)
			{
				this.EmitConditionalAccess(ec, ref callEmitter, this.Getter, this.Arguments);
			}
			else
			{
				callEmitter.Emit(ec, this.Getter, this.Arguments, this.loc);
			}
			if (callEmitter.HasAwaitArguments)
			{
				this.InstanceExpression = callEmitter.InstanceExpression;
				this.Arguments = callEmitter.EmittedArguments;
				this.has_await_arguments = true;
			}
			if (leave_copy)
			{
				ec.Emit(OpCodes.Dup);
				this.temp = new LocalTemporary(base.Type);
				this.temp.Store(ec);
			}
		}

		// Token: 0x060017DB RID: 6107
		public abstract void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound);

		// Token: 0x060017DC RID: 6108 RVA: 0x00073645 File Offset: 0x00071845
		public override void Emit(EmitContext ec)
		{
			this.Emit(ec, false);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0007364F File Offset: 0x0007184F
		protected override FieldExpr EmitToFieldSource(EmitContext ec)
		{
			this.has_await_arguments = true;
			this.Emit(ec, false);
			return null;
		}

		// Token: 0x060017DE RID: 6110
		public abstract Expression MakeAssignExpression(BuilderContext ctx, Expression source);

		// Token: 0x060017DF RID: 6111
		protected abstract Expression OverloadResolve(ResolveContext rc, Expression right_side);

		// Token: 0x060017E0 RID: 6112 RVA: 0x00073664 File Offset: 0x00071864
		private bool ResolveGetter(ResolveContext rc)
		{
			if (!this.best_candidate.HasGet)
			{
				if (this.InstanceExpression != EmptyExpression.Null)
				{
					rc.Report.SymbolRelatedToPreviousError(this.best_candidate);
					rc.Report.Error(154, this.loc, "The property or indexer `{0}' cannot be used in this context because it lacks the `get' accessor", this.best_candidate.GetSignatureForError());
					return false;
				}
			}
			else if (!this.best_candidate.Get.IsAccessible(rc) || !this.best_candidate.Get.DeclaringType.IsAccessible(rc))
			{
				if (this.best_candidate.HasDifferentAccessibility)
				{
					rc.Report.SymbolRelatedToPreviousError(this.best_candidate.Get);
					rc.Report.Error(271, this.loc, "The property or indexer `{0}' cannot be used in this context because the get accessor is inaccessible", TypeManager.CSharpSignature(this.best_candidate));
				}
				else
				{
					rc.Report.SymbolRelatedToPreviousError(this.best_candidate.Get);
					Expression.ErrorIsInaccesible(rc, this.best_candidate.Get.GetSignatureForError(), this.loc);
				}
			}
			if (this.best_candidate.HasDifferentAccessibility)
			{
				base.CheckProtectedMemberAccess(rc, this.best_candidate.Get);
			}
			this.getter = base.CandidateToBaseOverride(rc, this.best_candidate.Get);
			return true;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000022F4 File Offset: 0x000004F4
		protected virtual bool ResolveAutopropertyAssignment(ResolveContext rc, Expression rhs)
		{
			return false;
		}

		// Token: 0x04000985 RID: 2437
		private MethodSpec getter;

		// Token: 0x04000986 RID: 2438
		private MethodSpec setter;

		// Token: 0x04000987 RID: 2439
		protected T best_candidate;

		// Token: 0x04000988 RID: 2440
		protected LocalTemporary temp;

		// Token: 0x04000989 RID: 2441
		protected bool emitting_compound_assignment;

		// Token: 0x0400098A RID: 2442
		protected bool has_await_arguments;
	}
}
