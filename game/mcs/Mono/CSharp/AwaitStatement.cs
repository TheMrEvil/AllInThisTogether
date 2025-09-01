using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Mono.CSharp.Linq;

namespace Mono.CSharp
{
	// Token: 0x0200011A RID: 282
	public class AwaitStatement : YieldStatement<AsyncInitializer>
	{
		// Token: 0x06000DD5 RID: 3541 RVA: 0x000332D9 File Offset: 0x000314D9
		public AwaitStatement(Expression expr, Location loc) : base(expr, loc)
		{
			this.unwind_protect = true;
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x000332EA File Offset: 0x000314EA
		private bool IsDynamic
		{
			get
			{
				return this.awaiter_definition == null;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x000332F5 File Offset: 0x000314F5
		public TypeSpec ResultType
		{
			get
			{
				return this.result_type;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00033300 File Offset: 0x00031500
		protected override void DoEmit(EmitContext ec)
		{
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				this.GetResultExpression(ec).Emit(ec);
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00033344 File Offset: 0x00031544
		public Expression GetResultExpression(EmitContext ec)
		{
			FieldExpr fieldExpr = new FieldExpr(this.awaiter, this.loc);
			fieldExpr.InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, this.loc);
			if (this.IsDynamic)
			{
				ResolveContext rc = new ResolveContext(ec.MemberContext);
				return new Invocation(new MemberAccess(fieldExpr, "GetResult"), new Arguments(0)).Resolve(rc);
			}
			MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(this.awaiter_definition.GetResult, fieldExpr.Type, this.loc);
			methodGroupExpr.InstanceExpression = fieldExpr;
			return new AwaitStatement.GetResultInvocation(methodGroupExpr, new Arguments(0));
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x000333DC File Offset: 0x000315DC
		public void EmitPrologue(EmitContext ec)
		{
			this.awaiter = ((AsyncTaskStorey)this.machine_initializer.Storey).AddAwaiter(this.expr.Type);
			FieldExpr fieldExpr = new FieldExpr(this.awaiter, this.loc);
			fieldExpr.InstanceExpression = new CompilerGeneratedThis(ec.CurrentType, this.loc);
			Label label = ec.DefineLabel();
			using (ec.With(BuilderContext.Options.OmitDebugInfo, true))
			{
				fieldExpr.EmitAssign(ec, this.expr, false, false);
				Expression expression;
				if (this.IsDynamic)
				{
					ResolveContext rc = new ResolveContext(ec.MemberContext);
					Arguments arguments = new Arguments(1);
					arguments.Add(new Argument(fieldExpr));
					expression = new DynamicMemberBinder("IsCompleted", arguments, this.loc).Resolve(rc);
					arguments = new Arguments(1);
					arguments.Add(new Argument(expression));
					expression = new DynamicConversion(ec.Module.Compiler.BuiltinTypes.Bool, CSharpBinderFlags.None, arguments, this.loc).Resolve(rc);
				}
				else
				{
					PropertyExpr propertyExpr = PropertyExpr.CreatePredefined(this.awaiter_definition.IsCompleted, this.loc);
					propertyExpr.InstanceExpression = fieldExpr;
					expression = propertyExpr;
				}
				expression.EmitBranchable(ec, label, true);
			}
			base.DoEmit(ec);
			ec.AssertEmptyStack();
			AsyncTaskStorey asyncTaskStorey = (AsyncTaskStorey)this.machine_initializer.Storey;
			if (this.IsDynamic)
			{
				asyncTaskStorey.EmitAwaitOnCompletedDynamic(ec, fieldExpr);
			}
			else
			{
				asyncTaskStorey.EmitAwaitOnCompleted(ec, fieldExpr);
			}
			this.machine_initializer.EmitLeave(ec, this.unwind_protect);
			ec.MarkLabel(this.resume_point);
			ec.MarkLabel(label);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0003358C File Offset: 0x0003178C
		public void EmitStatement(EmitContext ec)
		{
			this.EmitPrologue(ec);
			this.DoEmit(ec);
			this.awaiter.IsAvailableForReuse = true;
			if (this.ResultType.Kind != MemberKind.Void)
			{
				ec.Emit(OpCodes.Pop);
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000335C5 File Offset: 0x000317C5
		private void Error_WrongAwaiterPattern(ResolveContext rc, TypeSpec awaiter)
		{
			rc.Report.Error(4011, this.loc, "The awaiter type `{0}' must have suitable IsCompleted and GetResult members", awaiter.GetSignatureForError());
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000335E8 File Offset: 0x000317E8
		public override bool Resolve(BlockContext bc)
		{
			if (bc.CurrentBlock is QueryBlock)
			{
				bc.Report.Error(1995, this.loc, "The `await' operator may only be used in a query expression within the first collection expression of the initial `from' clause or within the collection expression of a `join' clause");
				return false;
			}
			if (!base.Resolve(bc))
			{
				return false;
			}
			this.type = this.expr.Type;
			Arguments arguments = new Arguments(0);
			if (this.type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				this.result_type = this.type;
				this.expr = new Invocation(new MemberAccess(this.expr, "GetAwaiter"), arguments).Resolve(bc);
				return true;
			}
			Expression expression = new AwaitStatement.AwaitableMemberAccess(this.expr).Resolve(bc);
			if (expression == null)
			{
				return false;
			}
			SessionReportPrinter sessionReportPrinter = new SessionReportPrinter();
			ReportPrinter printer = bc.Report.SetPrinter(sessionReportPrinter);
			expression = new Invocation(expression, arguments).Resolve(bc);
			bc.Report.SetPrinter(printer);
			if (sessionReportPrinter.ErrorsCount > 0 || !MemberAccess.IsValidDotExpression(expression.Type))
			{
				bc.Report.Error(1986, this.expr.Location, "The `await' operand type `{0}' must have suitable GetAwaiter method", this.expr.Type.GetSignatureForError());
				return false;
			}
			TypeSpec typeSpec = expression.Type;
			this.awaiter_definition = bc.Module.GetAwaiter(typeSpec);
			if (!this.awaiter_definition.IsValidPattern)
			{
				this.Error_WrongAwaiterPattern(bc, typeSpec);
				return false;
			}
			if (!this.awaiter_definition.INotifyCompletion)
			{
				bc.Report.Error(4027, this.loc, "The awaiter type `{0}' must implement interface `{1}'", typeSpec.GetSignatureForError(), bc.Module.PredefinedTypes.INotifyCompletion.GetSignatureForError());
				return false;
			}
			this.expr = expression;
			this.result_type = this.awaiter_definition.GetResult.ReturnType;
			return true;
		}

		// Token: 0x0400066E RID: 1646
		private Field awaiter;

		// Token: 0x0400066F RID: 1647
		private AwaiterDefinition awaiter_definition;

		// Token: 0x04000670 RID: 1648
		private TypeSpec type;

		// Token: 0x04000671 RID: 1649
		private TypeSpec result_type;

		// Token: 0x02000382 RID: 898
		public sealed class AwaitableMemberAccess : MemberAccess
		{
			// Token: 0x060026A0 RID: 9888 RVA: 0x000B6CAA File Offset: 0x000B4EAA
			public AwaitableMemberAccess(Expression expr) : base(expr, "GetAwaiter")
			{
			}

			// Token: 0x170008D3 RID: 2259
			// (get) Token: 0x060026A1 RID: 9889 RVA: 0x000B6CB8 File Offset: 0x000B4EB8
			// (set) Token: 0x060026A2 RID: 9890 RVA: 0x000B6CC0 File Offset: 0x000B4EC0
			public bool ProbingMode
			{
				[CompilerGenerated]
				get
				{
					return this.<ProbingMode>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ProbingMode>k__BackingField = value;
				}
			}

			// Token: 0x060026A3 RID: 9891 RVA: 0x000B6CC9 File Offset: 0x000B4EC9
			public override void Error_TypeDoesNotContainDefinition(ResolveContext rc, TypeSpec type, string name)
			{
				this.Error_OperatorCannotBeApplied(rc, type);
			}

			// Token: 0x060026A4 RID: 9892 RVA: 0x000B6CD4 File Offset: 0x000B4ED4
			protected override void Error_OperatorCannotBeApplied(ResolveContext rc, TypeSpec type)
			{
				if (this.ProbingMode)
				{
					return;
				}
				Invocation invocation = base.LeftExpression as Invocation;
				if (invocation != null && invocation.MethodGroup != null && (invocation.MethodGroup.BestCandidate.Modifiers & Modifiers.ASYNC) != (Modifiers)0)
				{
					rc.Report.Error(4008, this.loc, "Cannot await void method `{0}'. Consider changing method return type to `Task'", invocation.GetSignatureForError());
					return;
				}
				if (type != InternalType.ErrorType)
				{
					rc.Report.Error(4001, this.loc, "Cannot await `{0}' expression", type.GetSignatureForError());
				}
			}

			// Token: 0x04000F4D RID: 3917
			[CompilerGenerated]
			private bool <ProbingMode>k__BackingField;
		}

		// Token: 0x02000383 RID: 899
		private sealed class GetResultInvocation : Invocation
		{
			// Token: 0x060026A5 RID: 9893 RVA: 0x000B6D64 File Offset: 0x000B4F64
			public GetResultInvocation(MethodGroupExpr mge, Arguments arguments) : base(null, arguments)
			{
				this.mg = mge;
				this.type = this.mg.BestCandidateReturnType;
			}

			// Token: 0x060026A6 RID: 9894 RVA: 0x00005936 File Offset: 0x00003B36
			public override Expression EmitToField(EmitContext ec)
			{
				return this;
			}
		}
	}
}
