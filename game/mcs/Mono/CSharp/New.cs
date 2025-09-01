using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020001E8 RID: 488
	public class New : ExpressionStatement, IMemoryLocation
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x0007DB73 File Offset: 0x0007BD73
		public New(Expression requested_type, Arguments arguments, Location l)
		{
			this.RequestedType = requested_type;
			this.arguments = arguments;
			this.loc = l;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x0007DB90 File Offset: 0x0007BD90
		public Arguments Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0007DB98 File Offset: 0x0007BD98
		public bool IsGeneratedStructConstructor
		{
			get
			{
				return this.arguments == null && this.method == null && this.type.IsStruct && base.GetType() == typeof(New);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x0007DBCB File Offset: 0x0007BDCB
		public Expression TypeExpression
		{
			get
			{
				return this.RequestedType;
			}
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0007DBD4 File Offset: 0x0007BDD4
		public static Constant Constantify(TypeSpec t, Location loc)
		{
			switch (t.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
				return new BoolConstant(t, false, loc);
			case BuiltinTypeSpec.Type.Byte:
				return new ByteConstant(t, 0, loc);
			case BuiltinTypeSpec.Type.SByte:
				return new SByteConstant(t, 0, loc);
			case BuiltinTypeSpec.Type.Char:
				return new CharConstant(t, '\0', loc);
			case BuiltinTypeSpec.Type.Short:
				return new ShortConstant(t, 0, loc);
			case BuiltinTypeSpec.Type.UShort:
				return new UShortConstant(t, 0, loc);
			case BuiltinTypeSpec.Type.Int:
				return new IntConstant(t, 0, loc);
			case BuiltinTypeSpec.Type.UInt:
				return new UIntConstant(t, 0U, loc);
			case BuiltinTypeSpec.Type.Long:
				return new LongConstant(t, 0L, loc);
			case BuiltinTypeSpec.Type.ULong:
				return new ULongConstant(t, 0UL, loc);
			case BuiltinTypeSpec.Type.Float:
				return new FloatConstant(t, 0.0, loc);
			case BuiltinTypeSpec.Type.Double:
				return new DoubleConstant(t, 0.0, loc);
			case BuiltinTypeSpec.Type.Decimal:
				return new DecimalConstant(t, 0m, loc);
			default:
				if (t.IsEnum)
				{
					return new EnumConstant(New.Constantify(EnumSpec.GetUnderlyingType(t), loc), t);
				}
				if (t.IsNullableType)
				{
					return LiftedNull.Create(t, loc);
				}
				return null;
			}
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0007DCE0 File Offset: 0x0007BEE0
		public override bool ContainsEmitWithAwait()
		{
			return this.arguments != null && this.arguments.ContainsEmitWithAwait();
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0007DCF8 File Offset: 0x0007BEF8
		public Expression CheckComImport(ResolveContext ec)
		{
			if (!this.type.IsInterface)
			{
				return null;
			}
			TypeSpec attributeCoClass = this.type.MemberDefinition.GetAttributeCoClass();
			if (attributeCoClass == null)
			{
				return null;
			}
			New expr = new New(new TypeExpression(attributeCoClass, this.loc), this.arguments, this.loc);
			return new Cast(new TypeExpression(this.type, this.loc), expr, this.loc).Resolve(ec);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0007DD6C File Offset: 0x0007BF6C
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments;
			if (this.method == null)
			{
				arguments = new Arguments(1);
				arguments.Add(new Argument(new TypeOf(this.type, this.loc)));
			}
			else
			{
				arguments = Arguments.CreateForExpressionTree(ec, this.arguments, new Expression[]
				{
					new TypeOfMethod(this.method, this.loc)
				});
			}
			return base.CreateExpressionFactoryCall(ec, "New", arguments);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0007DDDC File Offset: 0x0007BFDC
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.type = this.RequestedType.ResolveAsType(ec, false);
			if (this.type == null)
			{
				return null;
			}
			this.eclass = ExprClass.Value;
			if (this.type.IsPointer)
			{
				ec.Report.Error(1919, this.loc, "Unsafe type `{0}' cannot be used in an object creation expression", this.type.GetSignatureForError());
				return null;
			}
			if (this.arguments == null)
			{
				Constant constant = New.Constantify(this.type, this.RequestedType.Location);
				if (constant != null)
				{
					return ReducedExpression.Create(constant, this);
				}
			}
			if (this.type.IsDelegate)
			{
				return new NewDelegate(this.type, this.arguments, this.loc).Resolve(ec);
			}
			TypeParameterSpec typeParameterSpec = this.type as TypeParameterSpec;
			if (typeParameterSpec != null)
			{
				if ((typeParameterSpec.SpecialConstraint & (SpecialConstraint.Constructor | SpecialConstraint.Struct)) == SpecialConstraint.None && !TypeSpec.IsValueType(typeParameterSpec))
				{
					ec.Report.Error(304, this.loc, "Cannot create an instance of the variable type `{0}' because it does not have the new() constraint", this.type.GetSignatureForError());
				}
				if (this.arguments != null && this.arguments.Count != 0)
				{
					ec.Report.Error(417, this.loc, "`{0}': cannot provide arguments when creating an instance of a variable type", this.type.GetSignatureForError());
				}
				return this;
			}
			if (this.type.IsStatic)
			{
				ec.Report.SymbolRelatedToPreviousError(this.type);
				ec.Report.Error(712, this.loc, "Cannot create an instance of the static class `{0}'", this.type.GetSignatureForError());
				return null;
			}
			if (this.type.IsInterface || this.type.IsAbstract)
			{
				if (!TypeManager.IsGenericType(this.type))
				{
					this.RequestedType = this.CheckComImport(ec);
					if (this.RequestedType != null)
					{
						return this.RequestedType;
					}
				}
				ec.Report.SymbolRelatedToPreviousError(this.type);
				ec.Report.Error(144, this.loc, "Cannot create an instance of the abstract class or interface `{0}'", this.type.GetSignatureForError());
				return null;
			}
			bool flag;
			if (this.arguments != null)
			{
				this.arguments.Resolve(ec, out flag);
			}
			else
			{
				flag = false;
			}
			this.method = Expression.ConstructorLookup(ec, this.type, ref this.arguments, this.loc);
			if (flag)
			{
				this.arguments.Insert(0, new Argument(new TypeOf(this.type, this.loc).Resolve(ec), Argument.AType.DynamicTypeName));
				return new DynamicConstructorBinder(this.type, this.arguments, this.loc).Resolve(ec);
			}
			return this;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0007E064 File Offset: 0x0007C264
		private void DoEmitTypeParameter(EmitContext ec)
		{
			MethodSpec methodSpec = ec.Module.PredefinedMembers.ActivatorCreateInstance.Resolve(this.loc);
			if (methodSpec == null)
			{
				return;
			}
			MethodSpec methodSpec2 = methodSpec.MakeGenericMethod(ec.MemberContext, new TypeSpec[]
			{
				this.type
			});
			ec.Emit(OpCodes.Call, methodSpec2);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0007E0BC File Offset: 0x0007C2BC
		public virtual bool Emit(EmitContext ec, IMemoryLocation target)
		{
			bool isStructOrEnum = this.type.IsStructOrEnum;
			VariableReference variableReference = target as VariableReference;
			if (target != null && isStructOrEnum && (variableReference != null || this.method == null))
			{
				target.AddressOf(ec, AddressOp.Store);
			}
			else if (variableReference != null && variableReference.IsRef)
			{
				variableReference.EmitLoad(ec);
			}
			if (this.arguments != null)
			{
				if (ec.HasSet(BuilderContext.Options.AsyncBody) && this.arguments.Count > ((this is NewInitialize) ? 0 : 1) && this.arguments.ContainsEmitWithAwait())
				{
					this.arguments = this.arguments.Emit(ec, false, true);
				}
				this.arguments.Emit(ec);
			}
			if (isStructOrEnum)
			{
				if (this.method == null)
				{
					ec.Emit(OpCodes.Initobj, this.type);
					return false;
				}
				if (variableReference != null)
				{
					ec.MarkCallEntry(this.loc);
					ec.Emit(OpCodes.Call, this.method);
					return false;
				}
			}
			if (this.type is TypeParameterSpec)
			{
				this.DoEmitTypeParameter(ec);
				return true;
			}
			ec.MarkCallEntry(this.loc);
			ec.Emit(OpCodes.Newobj, this.method);
			return true;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0007E1D8 File Offset: 0x0007C3D8
		public override void Emit(EmitContext ec)
		{
			LocalTemporary localTemporary = null;
			if (this.method == null && this.type.IsStructOrEnum)
			{
				localTemporary = new LocalTemporary(this.type);
			}
			if (!this.Emit(ec, localTemporary))
			{
				localTemporary.Emit(ec);
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0007E21C File Offset: 0x0007C41C
		public override void EmitStatement(EmitContext ec)
		{
			LocalTemporary target = null;
			if (this.method == null && TypeSpec.IsValueType(this.type))
			{
				target = new LocalTemporary(this.type);
			}
			if (this.Emit(ec, target))
			{
				ec.Emit(OpCodes.Pop);
			}
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0007E261 File Offset: 0x0007C461
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			if (this.arguments != null)
			{
				this.arguments.FlowAnalysis(fc, null);
			}
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0007E278 File Offset: 0x0007C478
		public void AddressOf(EmitContext ec, AddressOp mode)
		{
			this.EmitAddressOf(ec, mode);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0007E284 File Offset: 0x0007C484
		protected virtual IMemoryLocation EmitAddressOf(EmitContext ec, AddressOp mode)
		{
			LocalTemporary localTemporary = new LocalTemporary(this.type);
			if (this.type is TypeParameterSpec)
			{
				this.DoEmitTypeParameter(ec);
				localTemporary.Store(ec);
				localTemporary.AddressOf(ec, mode);
				return localTemporary;
			}
			localTemporary.AddressOf(ec, AddressOp.Store);
			if (this.method == null)
			{
				ec.Emit(OpCodes.Initobj, this.type);
			}
			else
			{
				if (this.arguments != null)
				{
					this.arguments.Emit(ec);
				}
				ec.Emit(OpCodes.Call, this.method);
			}
			localTemporary.AddressOf(ec, mode);
			return localTemporary;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0007E314 File Offset: 0x0007C514
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			New @new = (New)t;
			@new.RequestedType = this.RequestedType.Clone(clonectx);
			if (this.arguments != null)
			{
				@new.arguments = this.arguments.Clone(clonectx);
			}
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0007E354 File Offset: 0x0007C554
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return Expression.New((ConstructorInfo)this.method.GetMetaInfo(), Arguments.MakeExpression(this.arguments, ctx));
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0007E377 File Offset: 0x0007C577
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009CD RID: 2509
		protected Arguments arguments;

		// Token: 0x040009CE RID: 2510
		protected Expression RequestedType;

		// Token: 0x040009CF RID: 2511
		protected MethodSpec method;
	}
}
