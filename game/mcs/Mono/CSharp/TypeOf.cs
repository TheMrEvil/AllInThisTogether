using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001F3 RID: 499
	public class TypeOf : Expression
	{
		// Token: 0x06001A05 RID: 6661 RVA: 0x0007FD97 File Offset: 0x0007DF97
		public TypeOf(FullNamedExpression queried_type, Location l)
		{
			this.QueriedType = queried_type;
			this.loc = l;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0007FDAD File Offset: 0x0007DFAD
		public TypeOf(TypeSpec type, Location loc)
		{
			this.typearg = type;
			this.loc = loc;
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsSideEffectFree
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0007FDC3 File Offset: 0x0007DFC3
		public TypeSpec TypeArgument
		{
			get
			{
				return this.typearg;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0007FDCB File Offset: 0x0007DFCB
		public FullNamedExpression TypeExpression
		{
			get
			{
				return this.QueriedType;
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0007FDD4 File Offset: 0x0007DFD4
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			TypeOf typeOf = (TypeOf)t;
			if (this.QueriedType != null)
			{
				typeOf.QueriedType = (FullNamedExpression)this.QueriedType.Clone(clonectx);
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0007FE08 File Offset: 0x0007E008
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(this));
			arguments.Add(new Argument(new TypeOf(new TypeExpression(this.type, this.loc), this.loc)));
			return base.CreateExpressionFactoryCall(ec, "Constant", arguments);
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0007FE5C File Offset: 0x0007E05C
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.eclass != ExprClass.Unresolved)
			{
				return this;
			}
			if (this.typearg == null)
			{
				using (ec.Set(ResolveContext.Options.UnsafeScope))
				{
					this.typearg = this.QueriedType.ResolveAsType(ec, true);
				}
				if (this.typearg == null)
				{
					return null;
				}
				if (this.typearg.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					ec.Report.Error(1962, this.QueriedType.Location, "The typeof operator cannot be used on the dynamic type");
				}
			}
			this.type = ec.BuiltinTypes.Type;
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0007FF08 File Offset: 0x0007E108
		private static bool ContainsDynamicType(TypeSpec type)
		{
			if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				return true;
			}
			ElementTypeSpec elementTypeSpec = type as ElementTypeSpec;
			if (elementTypeSpec != null)
			{
				return TypeOf.ContainsDynamicType(elementTypeSpec.Element);
			}
			TypeSpec[] typeArguments = type.TypeArguments;
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (TypeOf.ContainsDynamicType(typeArguments[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0007FF5C File Offset: 0x0007E15C
		public override void EncodeAttributeValue(IMemberContext rc, AttributeEncoder enc, TypeSpec targetType, TypeSpec parameterType)
		{
			if (targetType != this.type)
			{
				enc.Encode(this.type);
			}
			if (this.typearg is InflatedTypeSpec)
			{
				TypeSpec declaringType = this.typearg;
				while (!InflatedTypeSpec.ContainsTypeParameter(declaringType))
				{
					declaringType = declaringType.DeclaringType;
					if (declaringType == null)
					{
						goto IL_6D;
					}
				}
				rc.Module.Compiler.Report.Error(416, this.loc, "`{0}': an attribute argument cannot use type parameters", this.typearg.GetSignatureForError());
				return;
			}
			IL_6D:
			if (TypeOf.ContainsDynamicType(this.typearg))
			{
				Attribute.Error_AttributeArgumentIsDynamic(rc, this.loc);
				return;
			}
			enc.EncodeTypeName(this.typearg);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0007FFFC File Offset: 0x0007E1FC
		public override void Emit(EmitContext ec)
		{
			ec.Emit(OpCodes.Ldtoken, this.typearg);
			MethodSpec methodSpec = ec.Module.PredefinedMembers.TypeGetTypeFromHandle.Resolve(this.loc);
			if (methodSpec != null)
			{
				ec.Emit(OpCodes.Call, methodSpec);
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00080045 File Offset: 0x0007E245
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009E1 RID: 2529
		private FullNamedExpression QueriedType;

		// Token: 0x040009E2 RID: 2530
		private TypeSpec typearg;
	}
}
