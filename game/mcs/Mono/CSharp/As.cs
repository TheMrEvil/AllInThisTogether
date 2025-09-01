using System;
using System.Reflection.Emit;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020001D8 RID: 472
	public class As : Probe
	{
		// Token: 0x060018A5 RID: 6309 RVA: 0x00075F1A File Offset: 0x0007411A
		public As(Expression expr, Expression probe_type, Location l) : base(expr, probe_type, l)
		{
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0007723D File Offset: 0x0007543D
		protected override string OperatorName
		{
			get
			{
				return "as";
			}
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00077244 File Offset: 0x00075444
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments args = Arguments.CreateForExpressionTree(ec, null, new Expression[]
			{
				this.expr.CreateExpressionTree(ec),
				new TypeOf(this.probe_type_expr, this.loc)
			});
			return base.CreateExpressionFactoryCall(ec, "TypeAs", args);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00077290 File Offset: 0x00075490
		public override void Emit(EmitContext ec)
		{
			this.expr.Emit(ec);
			ec.Emit(OpCodes.Isinst, this.type);
			if (TypeManager.IsGenericParameter(this.type) || this.type.IsNullableType)
			{
				ec.Emit(OpCodes.Unbox_Any, this.type);
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000772E8 File Offset: 0x000754E8
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (base.ResolveCommon(ec) == null)
			{
				return null;
			}
			this.type = this.probe_type_expr;
			this.eclass = ExprClass.Value;
			TypeSpec type = this.expr.Type;
			if (!TypeSpec.IsReferenceType(this.type) && !this.type.IsNullableType)
			{
				if (TypeManager.IsGenericParameter(this.type))
				{
					ec.Report.Error(413, this.loc, "The `as' operator cannot be used with a non-reference type parameter `{0}'. Consider adding `class' or a reference type constraint", this.probe_type_expr.GetSignatureForError());
				}
				else
				{
					ec.Report.Error(77, this.loc, "The `as' operator cannot be used with a non-nullable value type `{0}'", this.type.GetSignatureForError());
				}
				return null;
			}
			if (this.expr.IsNull && this.type.IsNullableType)
			{
				return LiftedNull.CreateFromExpression(ec, this);
			}
			if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				return this;
			}
			Expression expression = Convert.ImplicitConversionStandard(ec, this.expr, this.type, this.loc);
			if (expression != null)
			{
				expression = EmptyCast.Create(expression, this.type);
				return ReducedExpression.Create(expression, this).Resolve(ec);
			}
			if (Convert.ExplicitReferenceConversionExists(type, this.type))
			{
				if (TypeManager.IsGenericParameter(type))
				{
					this.expr = new BoxedCast(this.expr, type);
				}
				return this;
			}
			if (InflatedTypeSpec.ContainsTypeParameter(type) || InflatedTypeSpec.ContainsTypeParameter(this.type))
			{
				this.expr = new BoxedCast(this.expr, type);
				return this;
			}
			if (type != InternalType.ErrorType)
			{
				ec.Report.Error(39, this.loc, "Cannot convert type `{0}' to `{1}' via a built-in conversion", type.GetSignatureForError(), this.type.GetSignatureForError());
			}
			return null;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0007747D File Offset: 0x0007567D
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
