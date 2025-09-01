using System;
using System.Reflection.Emit;

namespace Mono.CSharp.Nullable
{
	// Token: 0x02000301 RID: 769
	public class LiftedConversion : Expression, IMemoryLocation
	{
		// Token: 0x0600246F RID: 9327 RVA: 0x000AE270 File Offset: 0x000AC470
		public LiftedConversion(Expression expr, Unwrap unwrap, TypeSpec type)
		{
			this.expr = expr;
			this.unwrap = unwrap;
			this.loc = expr.Location;
			this.type = type;
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000AE299 File Offset: 0x000AC499
		public LiftedConversion(Expression expr, Expression unwrap, TypeSpec type) : this(expr, unwrap as Unwrap, type)
		{
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x000AE2A9 File Offset: 0x000AC4A9
		public override bool IsNull
		{
			get
			{
				return this.expr.IsNull;
			}
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000AE2B6 File Offset: 0x000AC4B6
		public override bool ContainsEmitWithAwait()
		{
			return this.unwrap.ContainsEmitWithAwait();
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000AE2C3 File Offset: 0x000AC4C3
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return this.expr.CreateExpressionTree(ec);
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000AE2D4 File Offset: 0x000AC4D4
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.unwrap != null)
			{
				if (this.type.IsNullableType)
				{
					if (!this.expr.Type.IsNullableType)
					{
						this.expr = Wrap.Create(this.expr, this.type);
						if (this.expr == null)
						{
							return null;
						}
					}
					this.null_value = LiftedNull.Create(this.type, this.loc);
				}
				else if (TypeSpec.IsValueType(this.type))
				{
					this.null_value = LiftedNull.Create(this.type, this.loc);
				}
				else
				{
					this.null_value = new NullConstant(this.type, this.loc);
				}
				this.eclass = ExprClass.Value;
				return this;
			}
			if (this.type.IsNullableType)
			{
				return Wrap.Create(this.expr, this.type);
			}
			return this.expr;
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000AE3B0 File Offset: 0x000AC5B0
		public override void Emit(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			this.unwrap.EmitCheck(ec);
			ec.Emit(OpCodes.Brfalse, label);
			this.expr.Emit(ec);
			ec.Emit(OpCodes.Br, label2);
			ec.MarkLabel(label);
			this.null_value.Emit(ec);
			ec.MarkLabel(label2);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000AE415 File Offset: 0x000AC615
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysis(fc);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000AE423 File Offset: 0x000AC623
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			this.unwrap.AddressOf(ec, mode);
		}

		// Token: 0x04000D90 RID: 3472
		private Expression expr;

		// Token: 0x04000D91 RID: 3473
		private Expression null_value;

		// Token: 0x04000D92 RID: 3474
		private Unwrap unwrap;
	}
}
