using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001E0 RID: 480
	public class PointerArithmetic : Expression
	{
		// Token: 0x0600191A RID: 6426 RVA: 0x0007C0DB File Offset: 0x0007A2DB
		public PointerArithmetic(Binary.Operator op, Expression l, Expression r, TypeSpec t, Location loc)
		{
			this.type = t;
			this.loc = loc;
			this.left = l;
			this.right = r;
			this.op = op;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override bool ContainsEmitWithAwait()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00075589 File Offset: 0x00073789
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			base.Error_PointerInsideExpressionTree(ec);
			return null;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0007C108 File Offset: 0x0007A308
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.eclass = ExprClass.Variable;
			PointerContainer pointerContainer = this.left.Type as PointerContainer;
			if (pointerContainer != null && pointerContainer.Element.Kind == MemberKind.Void)
			{
				base.Error_VoidPointerOperation(ec);
				return null;
			}
			return this;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0007C14C File Offset: 0x0007A34C
		public override void Emit(EmitContext ec)
		{
			TypeSpec type = this.left.Type;
			TypeSpec typeSpec;
			if (TypeManager.HasElementType(type))
			{
				typeSpec = TypeManager.GetElementType(type);
			}
			else
			{
				FieldExpr fieldExpr = this.left as FieldExpr;
				if (fieldExpr != null)
				{
					typeSpec = ((FixedFieldSpec)fieldExpr.Spec).ElementType;
				}
				else
				{
					typeSpec = type;
				}
			}
			int size = BuiltinTypeSpec.GetSize(typeSpec);
			TypeSpec type2 = this.right.Type;
			if ((this.op & Binary.Operator.SubtractionMask) != (Binary.Operator)0 && type2.IsPointer)
			{
				this.left.Emit(ec);
				this.right.Emit(ec);
				ec.Emit(OpCodes.Sub);
				if (size != 1)
				{
					if (size == 0)
					{
						ec.Emit(OpCodes.Sizeof, typeSpec);
					}
					else
					{
						ec.EmitInt(size);
					}
					ec.Emit(OpCodes.Div);
				}
				ec.Emit(OpCodes.Conv_I8);
				return;
			}
			Constant constant = this.left as Constant;
			if (constant != null)
			{
				if (constant.IsDefaultValue)
				{
					this.left = EmptyExpression.Null;
				}
				else
				{
					constant = null;
				}
			}
			this.left.Emit(ec);
			Constant constant2 = this.right as Constant;
			if (constant2 != null)
			{
				if (constant2.IsDefaultValue)
				{
					return;
				}
				if (size != 0)
				{
					this.right = new IntConstant(ec.BuiltinTypes, size, this.right.Location);
				}
				else
				{
					this.right = new SizeOf(new TypeExpression(typeSpec, this.right.Location), this.right.Location);
				}
				ResolveContext rc = new ResolveContext(ec.MemberContext, ResolveContext.Options.UnsafeScope);
				this.right = new Binary(Binary.Operator.Multiply, this.right, constant2).Resolve(rc);
				if (this.right == null)
				{
					return;
				}
			}
			this.right.Emit(ec);
			switch (type2.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
				ec.Emit(OpCodes.Conv_I);
				break;
			case BuiltinTypeSpec.Type.UInt:
				ec.Emit(OpCodes.Conv_U);
				break;
			}
			if (constant2 == null && size != 1)
			{
				if (size == 0)
				{
					ec.Emit(OpCodes.Sizeof, typeSpec);
				}
				else
				{
					ec.EmitInt(size);
				}
				if (type2.BuiltinType == BuiltinTypeSpec.Type.Long || type2.BuiltinType == BuiltinTypeSpec.Type.ULong)
				{
					ec.Emit(OpCodes.Conv_I8);
				}
				Binary.EmitOperatorOpcode(ec, Binary.Operator.Multiply, type2, this.right);
			}
			if (constant == null)
			{
				if (type2.BuiltinType == BuiltinTypeSpec.Type.Long)
				{
					ec.Emit(OpCodes.Conv_I);
				}
				else if (type2.BuiltinType == BuiltinTypeSpec.Type.ULong)
				{
					ec.Emit(OpCodes.Conv_U);
				}
				Binary.EmitOperatorOpcode(ec, this.op, type, this.right);
			}
		}

		// Token: 0x040009C0 RID: 2496
		private Expression left;

		// Token: 0x040009C1 RID: 2497
		private Expression right;

		// Token: 0x040009C2 RID: 2498
		private readonly Binary.Operator op;
	}
}
