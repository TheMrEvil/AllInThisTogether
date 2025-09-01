using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x0200013D RID: 317
	public class Const : FieldBase
	{
		// Token: 0x06000FE4 RID: 4068 RVA: 0x00041460 File Offset: 0x0003F660
		public Const(TypeDefinition parent, FullNamedExpression type, Modifiers mod_flags, MemberName name, Attributes attrs) : base(parent, type, mod_flags, Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW, name, attrs)
		{
			base.ModFlags |= Modifiers.STATIC;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00041484 File Offset: 0x0003F684
		public override bool Define()
		{
			if (!base.Define())
			{
				return false;
			}
			if (!this.member_type.IsConstantCompatible)
			{
				Const.Error_InvalidConstantType(this.member_type, base.Location, base.Report);
			}
			FieldAttributes fieldAttributes = FieldAttributes.Static | ModifiersExtensions.FieldAttr(base.ModFlags);
			if (this.member_type.BuiltinType == BuiltinTypeSpec.Type.Decimal)
			{
				fieldAttributes |= FieldAttributes.InitOnly;
			}
			else
			{
				fieldAttributes |= FieldAttributes.Literal;
			}
			this.FieldBuilder = this.Parent.TypeBuilder.DefineField(base.Name, base.MemberType.GetMetaInfo(), fieldAttributes);
			this.spec = new ConstSpec(this.Parent.Definition, this, base.MemberType, this.FieldBuilder, base.ModFlags, this.initializer);
			this.Parent.MemberCache.AddMember(this.spec);
			if ((fieldAttributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope)
			{
				this.Parent.PartialContainer.RegisterFieldForInitialization(this, new FieldInitializer(this, this.initializer, base.Location));
			}
			if (this.declarators != null)
			{
				TypeExpression type = new TypeExpression(base.MemberType, base.TypeExpression.Location);
				foreach (FieldDeclarator fieldDeclarator in this.declarators)
				{
					Const @const = new Const(this.Parent, type, base.ModFlags & ~Modifiers.STATIC, new MemberName(fieldDeclarator.Name.Value, fieldDeclarator.Name.Location), base.OptAttributes);
					@const.initializer = fieldDeclarator.Initializer;
					((ConstInitializer)@const.initializer).Name = fieldDeclarator.Name.Value;
					@const.Define();
					this.Parent.PartialContainer.Members.Add(@const);
				}
			}
			return true;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0004166C File Offset: 0x0003F86C
		public void DefineValue()
		{
			ResolveContext rc = new ResolveContext(this);
			((ConstSpec)this.spec).GetConstant(rc);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00041694 File Offset: 0x0003F894
		public override void Emit()
		{
			Constant constant = ((ConstSpec)this.spec).Value as Constant;
			if (constant.Type.BuiltinType == BuiltinTypeSpec.Type.Decimal)
			{
				this.Module.PredefinedAttributes.DecimalConstant.EmitAttribute(this.FieldBuilder, (decimal)constant.GetValue(), constant.Location);
			}
			else
			{
				this.FieldBuilder.SetConstant(constant.GetValue());
			}
			base.Emit();
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0004170B File Offset: 0x0003F90B
		public static void Error_InvalidConstantType(TypeSpec t, Location loc, Report Report)
		{
			if (t.IsGenericParameter)
			{
				Report.Error(1959, loc, "Type parameter `{0}' cannot be declared const", t.GetSignatureForError());
				return;
			}
			Report.Error(283, loc, "The type `{0}' cannot be declared const", t.GetSignatureForError());
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00041744 File Offset: 0x0003F944
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000730 RID: 1840
		private const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW;
	}
}
