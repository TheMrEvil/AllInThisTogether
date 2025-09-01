using System;

namespace Mono.CSharp
{
	// Token: 0x0200016C RID: 364
	internal abstract class DynamicMemberAssignable : DynamicExpressionStatement, IDynamicBinder, IAssignMethod
	{
		// Token: 0x060011A3 RID: 4515 RVA: 0x00048E3E File Offset: 0x0004703E
		protected DynamicMemberAssignable(Arguments args, Location loc) : base(null, args, loc)
		{
			this.binder = this;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00048E50 File Offset: 0x00047050
		public Expression CreateCallSiteBinder(ResolveContext ec, Arguments args)
		{
			return this.CreateCallSiteBinder(ec, args, false);
		}

		// Token: 0x060011A5 RID: 4517
		protected abstract Expression CreateCallSiteBinder(ResolveContext ec, Arguments args, bool isSet);

		// Token: 0x060011A6 RID: 4518 RVA: 0x00048E5B File Offset: 0x0004705B
		protected virtual Arguments CreateSetterArguments(ResolveContext rc, Expression rhs)
		{
			Arguments arguments = new Arguments(base.Arguments.Count + 1);
			arguments.AddRange(base.Arguments);
			arguments.Add(new Argument(rhs));
			return arguments;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00048E88 File Offset: 0x00047088
		public override Expression DoResolveLValue(ResolveContext rc, Expression right_side)
		{
			if (right_side == EmptyExpression.OutAccess)
			{
				right_side.DoResolveLValue(rc, this);
				return null;
			}
			if (base.DoResolveCore(rc))
			{
				this.setter_args = this.CreateSetterArguments(rc, right_side);
				this.setter = this.CreateCallSiteBinder(rc, this.setter_args, true);
			}
			this.eclass = ExprClass.Variable;
			return this;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00048EDB File Offset: 0x000470DB
		public override void Emit(EmitContext ec)
		{
			if (this.binder_expr == null)
			{
				base.EmitCall(ec, this.setter, base.Arguments, false);
				return;
			}
			base.Emit(ec);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00048F01 File Offset: 0x00047101
		public override void EmitStatement(EmitContext ec)
		{
			if (this.binder_expr == null)
			{
				base.EmitCall(ec, this.setter, base.Arguments, true);
				return;
			}
			base.EmitStatement(ec);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public void Emit(EmitContext ec, bool leave_copy)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00048F27 File Offset: 0x00047127
		public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
		{
			base.EmitCall(ec, this.setter, this.setter_args, !leave_copy);
		}

		// Token: 0x0400078B RID: 1931
		private Expression setter;

		// Token: 0x0400078C RID: 1932
		private Arguments setter_args;
	}
}
