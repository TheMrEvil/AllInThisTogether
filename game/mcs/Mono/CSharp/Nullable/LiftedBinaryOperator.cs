using System;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp.Nullable
{
	// Token: 0x02000303 RID: 771
	internal class LiftedBinaryOperator : Expression
	{
		// Token: 0x06002480 RID: 9344 RVA: 0x000AE656 File Offset: 0x000AC856
		public LiftedBinaryOperator(Binary b)
		{
			this.Binary = b;
			this.loc = b.Location;
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x000AE671 File Offset: 0x000AC871
		// (set) Token: 0x06002482 RID: 9346 RVA: 0x000AE679 File Offset: 0x000AC879
		public Binary Binary
		{
			[CompilerGenerated]
			get
			{
				return this.<Binary>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Binary>k__BackingField = value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x000AE682 File Offset: 0x000AC882
		// (set) Token: 0x06002484 RID: 9348 RVA: 0x000AE68A File Offset: 0x000AC88A
		public Expression Left
		{
			[CompilerGenerated]
			get
			{
				return this.<Left>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Left>k__BackingField = value;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x000AE693 File Offset: 0x000AC893
		// (set) Token: 0x06002486 RID: 9350 RVA: 0x000AE69B File Offset: 0x000AC89B
		public Expression Right
		{
			[CompilerGenerated]
			get
			{
				return this.<Right>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Right>k__BackingField = value;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002487 RID: 9351 RVA: 0x000AE6A4 File Offset: 0x000AC8A4
		// (set) Token: 0x06002488 RID: 9352 RVA: 0x000AE6AC File Offset: 0x000AC8AC
		public Unwrap UnwrapLeft
		{
			[CompilerGenerated]
			get
			{
				return this.<UnwrapLeft>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnwrapLeft>k__BackingField = value;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002489 RID: 9353 RVA: 0x000AE6B5 File Offset: 0x000AC8B5
		// (set) Token: 0x0600248A RID: 9354 RVA: 0x000AE6BD File Offset: 0x000AC8BD
		public Unwrap UnwrapRight
		{
			[CompilerGenerated]
			get
			{
				return this.<UnwrapRight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnwrapRight>k__BackingField = value;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x000AE6C6 File Offset: 0x000AC8C6
		// (set) Token: 0x0600248C RID: 9356 RVA: 0x000AE6CE File Offset: 0x000AC8CE
		public MethodSpec UserOperator
		{
			[CompilerGenerated]
			get
			{
				return this.<UserOperator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UserOperator>k__BackingField = value;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x000AE6D8 File Offset: 0x000AC8D8
		private bool IsBitwiseBoolean
		{
			get
			{
				return (this.Binary.Oper == Binary.Operator.BitwiseAnd || this.Binary.Oper == Binary.Operator.BitwiseOr) && ((this.UnwrapLeft != null && this.UnwrapLeft.Type.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive) || (this.UnwrapRight != null && this.UnwrapRight.Type.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive));
			}
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000AE745 File Offset: 0x000AC945
		public override bool ContainsEmitWithAwait()
		{
			return this.Left.ContainsEmitWithAwait() || this.Right.ContainsEmitWithAwait();
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000AE764 File Offset: 0x000AC964
		public override Expression CreateExpressionTree(ResolveContext rc)
		{
			if (this.UserOperator != null)
			{
				Arguments arguments = new Arguments(2);
				arguments.Add(new Argument(this.Binary.Left));
				arguments.Add(new Argument(this.Binary.Right));
				return new UserOperatorCall(this.UserOperator, arguments, new Func<ResolveContext, Expression, Expression>(this.Binary.CreateExpressionTree), this.loc).CreateExpressionTree(rc);
			}
			return this.Binary.CreateExpressionTree(rc);
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000AE7E4 File Offset: 0x000AC9E4
		protected override Expression DoResolve(ResolveContext rc)
		{
			if (rc.IsRuntimeBinder)
			{
				if (this.UnwrapLeft == null && !this.Left.Type.IsNullableType)
				{
					this.Left = this.LiftOperand(rc, this.Left);
				}
				if (this.UnwrapRight == null && !this.Right.Type.IsNullableType)
				{
					this.Right = this.LiftOperand(rc, this.Right);
				}
			}
			else
			{
				if (this.UnwrapLeft == null && this.Left != null && this.Left.Type.IsNullableType)
				{
					this.Left = Unwrap.CreateUnwrapped(this.Left);
					this.UnwrapLeft = (this.Left as Unwrap);
				}
				if (this.UnwrapRight == null && this.Right != null && this.Right.Type.IsNullableType)
				{
					this.Right = Unwrap.CreateUnwrapped(this.Right);
					this.UnwrapRight = (this.Right as Unwrap);
				}
			}
			this.type = this.Binary.Type;
			this.eclass = this.Binary.eclass;
			return this;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000AE90C File Offset: 0x000ACB0C
		private Expression LiftOperand(ResolveContext rc, Expression expr)
		{
			TypeSpec typeSpec;
			if (expr.IsNull)
			{
				typeSpec = (this.Left.IsNull ? this.Right.Type : this.Left.Type);
			}
			else
			{
				typeSpec = expr.Type;
			}
			if (!typeSpec.IsNullableType)
			{
				typeSpec = NullableInfo.MakeType(rc.Module, typeSpec);
			}
			return Wrap.Create(expr, typeSpec);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000AE96C File Offset: 0x000ACB6C
		public override void Emit(EmitContext ec)
		{
			if (this.IsBitwiseBoolean && this.UserOperator == null)
			{
				this.EmitBitwiseBoolean(ec);
				return;
			}
			if ((this.Binary.Oper & Binary.Operator.EqualityMask) != (Binary.Operator)0)
			{
				this.EmitEquality(ec);
				return;
			}
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			if (ec.HasSet(BuilderContext.Options.AsyncBody) && this.Right.ContainsEmitWithAwait())
			{
				this.Left = this.Left.EmitToField(ec);
				this.Right = this.Right.EmitToField(ec);
			}
			if (this.UnwrapLeft != null)
			{
				this.UnwrapLeft.EmitCheck(ec);
			}
			if (this.UnwrapRight != null && !this.Binary.Left.Equals(this.Binary.Right))
			{
				this.UnwrapRight.EmitCheck(ec);
				if (this.UnwrapLeft != null)
				{
					ec.Emit(OpCodes.And);
				}
			}
			ec.Emit(OpCodes.Brfalse, label);
			if (this.UserOperator != null)
			{
				Arguments arguments = new Arguments(2);
				arguments.Add(new Argument(this.Left));
				arguments.Add(new Argument(this.Right));
				default(CallEmitter).EmitPredefined(ec, this.UserOperator, arguments, false, null);
			}
			else
			{
				this.Binary.EmitOperator(ec, this.Left, this.Right);
			}
			if (this.type.IsNullableType)
			{
				ec.Emit(OpCodes.Newobj, NullableInfo.GetConstructor(this.type));
			}
			ec.Emit(OpCodes.Br_S, label2);
			ec.MarkLabel(label);
			if ((this.Binary.Oper & Binary.Operator.ComparisonMask) != (Binary.Operator)0)
			{
				ec.EmitInt(0);
			}
			else
			{
				LiftedNull.Create(this.type, this.loc).Emit(ec);
			}
			ec.MarkLabel(label2);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000AEB38 File Offset: 0x000ACD38
		private void EmitBitwiseBoolean(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			Label label3 = ec.DefineLabel();
			Label label4 = ec.DefineLabel();
			bool flag = this.Binary.Oper == Binary.Operator.BitwiseOr;
			if (this.UnwrapLeft != null && this.UnwrapRight != null)
			{
				if (ec.HasSet(BuilderContext.Options.AsyncBody) && this.Binary.Right.ContainsEmitWithAwait())
				{
					this.Left = this.Left.EmitToField(ec);
					this.Right = this.Right.EmitToField(ec);
				}
				else
				{
					this.UnwrapLeft.Store(ec);
					this.UnwrapRight.Store(ec);
				}
				this.Left.Emit(ec);
				ec.Emit(OpCodes.Brtrue_S, label2);
				this.Right.Emit(ec);
				ec.Emit(OpCodes.Brtrue_S, label);
				this.UnwrapLeft.EmitCheck(ec);
				ec.Emit(OpCodes.Brfalse_S, label2);
				ec.MarkLabel(label);
				if (flag)
				{
					this.UnwrapRight.Load(ec);
				}
				else
				{
					this.UnwrapLeft.Load(ec);
				}
				ec.Emit(OpCodes.Br_S, label3);
				ec.MarkLabel(label2);
				if (flag)
				{
					this.UnwrapLeft.Load(ec);
				}
				else
				{
					this.UnwrapRight.Load(ec);
				}
				ec.MarkLabel(label3);
				return;
			}
			if (this.UnwrapLeft == null)
			{
				if (this.Left is BoolConstant)
				{
					this.UnwrapRight.Store(ec);
					ec.EmitInt(flag ? 1 : 0);
					ec.Emit(OpCodes.Newobj, NullableInfo.GetConstructor(this.type));
				}
				else if (this.Left.IsNull)
				{
					this.UnwrapRight.Emit(ec);
					ec.Emit(flag ? OpCodes.Brfalse_S : OpCodes.Brtrue_S, label4);
					this.UnwrapRight.Load(ec);
					ec.Emit(OpCodes.Br_S, label3);
					ec.MarkLabel(label4);
					LiftedNull.Create(this.type, this.loc).Emit(ec);
				}
				else
				{
					this.Left.Emit(ec);
					ec.Emit(flag ? OpCodes.Brfalse_S : OpCodes.Brtrue_S, label2);
					ec.EmitInt(flag ? 1 : 0);
					ec.Emit(OpCodes.Newobj, NullableInfo.GetConstructor(this.type));
					ec.Emit(OpCodes.Br_S, label3);
					ec.MarkLabel(label2);
					this.UnwrapRight.Original.Emit(ec);
				}
			}
			else
			{
				this.UnwrapLeft.Store(ec);
				if (this.Right is BoolConstant)
				{
					ec.EmitInt(flag ? 1 : 0);
					ec.Emit(OpCodes.Newobj, NullableInfo.GetConstructor(this.type));
				}
				else if (this.Right.IsNull)
				{
					this.UnwrapLeft.Emit(ec);
					ec.Emit(flag ? OpCodes.Brfalse_S : OpCodes.Brtrue_S, label4);
					this.UnwrapLeft.Load(ec);
					ec.Emit(OpCodes.Br_S, label3);
					ec.MarkLabel(label4);
					LiftedNull.Create(this.type, this.loc).Emit(ec);
				}
				else
				{
					this.Right.Emit(ec);
					ec.Emit(flag ? OpCodes.Brfalse_S : OpCodes.Brtrue_S, label2);
					ec.EmitInt(flag ? 1 : 0);
					ec.Emit(OpCodes.Newobj, NullableInfo.GetConstructor(this.type));
					ec.Emit(OpCodes.Br_S, label3);
					ec.MarkLabel(label2);
					this.UnwrapLeft.Load(ec);
				}
			}
			ec.MarkLabel(label3);
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000AEECC File Offset: 0x000AD0CC
		private void EmitEquality(EmitContext ec)
		{
			if (this.UnwrapLeft != null && this.Binary.Right.IsNull)
			{
				this.UnwrapLeft.EmitCheck(ec);
				if (this.Binary.Oper == Binary.Operator.Equality)
				{
					ec.EmitInt(0);
					ec.Emit(OpCodes.Ceq);
				}
				return;
			}
			if (this.UnwrapRight != null && this.Binary.Left.IsNull)
			{
				this.UnwrapRight.EmitCheck(ec);
				if (this.Binary.Oper == Binary.Operator.Equality)
				{
					ec.EmitInt(0);
					ec.Emit(OpCodes.Ceq);
				}
				return;
			}
			Label label = ec.DefineLabel();
			Label label2 = ec.DefineLabel();
			if (this.UserOperator != null)
			{
				Expression expr = this.Left;
				if (this.UnwrapLeft != null)
				{
					this.UnwrapLeft.EmitCheck(ec);
				}
				else if (!(this.Left is VariableReference))
				{
					this.Left.Emit(ec);
					LocalTemporary localTemporary = new LocalTemporary(this.Left.Type);
					localTemporary.Store(ec);
					expr = localTemporary;
				}
				if (this.UnwrapRight != null)
				{
					this.UnwrapRight.EmitCheck(ec);
					if (this.UnwrapLeft != null)
					{
						ec.Emit(OpCodes.Bne_Un, label);
						Label label3 = ec.DefineLabel();
						this.UnwrapLeft.EmitCheck(ec);
						ec.Emit(OpCodes.Brtrue, label3);
						if (this.Binary.Oper == Binary.Operator.Equality)
						{
							ec.EmitInt(1);
						}
						else
						{
							ec.EmitInt(0);
						}
						ec.Emit(OpCodes.Br, label2);
						ec.MarkLabel(label3);
					}
					else
					{
						ec.Emit(OpCodes.Brfalse, label);
					}
				}
				else
				{
					ec.Emit(OpCodes.Brfalse, label);
				}
				Arguments arguments = new Arguments(2);
				arguments.Add(new Argument(expr));
				arguments.Add(new Argument(this.Right));
				default(CallEmitter).EmitPredefined(ec, this.UserOperator, arguments, false, null);
			}
			else
			{
				if (ec.HasSet(BuilderContext.Options.AsyncBody) && this.Binary.Right.ContainsEmitWithAwait())
				{
					this.Left = this.Left.EmitToField(ec);
					this.Right = this.Right.EmitToField(ec);
				}
				this.Left.Emit(ec);
				this.Right.Emit(ec);
				ec.Emit(OpCodes.Bne_Un_S, label);
				if (this.UnwrapLeft != null)
				{
					this.UnwrapLeft.EmitCheck(ec);
				}
				if (this.UnwrapRight != null)
				{
					this.UnwrapRight.EmitCheck(ec);
				}
				if (this.UnwrapLeft != null && this.UnwrapRight != null)
				{
					if (this.Binary.Oper == Binary.Operator.Inequality)
					{
						ec.Emit(OpCodes.Xor);
					}
					else
					{
						ec.Emit(OpCodes.Ceq);
					}
				}
				else if (this.Binary.Oper == Binary.Operator.Inequality)
				{
					ec.EmitInt(0);
					ec.Emit(OpCodes.Ceq);
				}
			}
			ec.Emit(OpCodes.Br_S, label2);
			ec.MarkLabel(label);
			if (this.Binary.Oper == Binary.Operator.Inequality)
			{
				ec.EmitInt(1);
			}
			else
			{
				ec.EmitInt(0);
			}
			ec.MarkLabel(label2);
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000AF1E7 File Offset: 0x000AD3E7
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.Binary.FlowAnalysis(fc);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000AF1F5 File Offset: 0x000AD3F5
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return this.Binary.MakeExpression(ctx, this.Left, this.Right);
		}

		// Token: 0x04000D95 RID: 3477
		[CompilerGenerated]
		private Binary <Binary>k__BackingField;

		// Token: 0x04000D96 RID: 3478
		[CompilerGenerated]
		private Expression <Left>k__BackingField;

		// Token: 0x04000D97 RID: 3479
		[CompilerGenerated]
		private Expression <Right>k__BackingField;

		// Token: 0x04000D98 RID: 3480
		[CompilerGenerated]
		private Unwrap <UnwrapLeft>k__BackingField;

		// Token: 0x04000D99 RID: 3481
		[CompilerGenerated]
		private Unwrap <UnwrapRight>k__BackingField;

		// Token: 0x04000D9A RID: 3482
		[CompilerGenerated]
		private MethodSpec <UserOperator>k__BackingField;
	}
}
