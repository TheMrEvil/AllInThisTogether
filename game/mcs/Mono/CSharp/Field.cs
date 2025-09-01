using System;

namespace Mono.CSharp
{
	// Token: 0x020002DC RID: 732
	public class Field : FieldBase
	{
		// Token: 0x060022C0 RID: 8896 RVA: 0x000AB31C File Offset: 0x000A951C
		public Field(TypeDefinition parent, FullNamedExpression type, Modifiers mod, MemberName name, Attributes attrs) : base(parent, type, mod, Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.STATIC | Modifiers.READONLY | Modifiers.VOLATILE | Modifiers.UNSAFE, name, attrs)
		{
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000AB330 File Offset: 0x000A9530
		private bool CanBeVolatile()
		{
			switch (base.MemberType.BuiltinType)
			{
			case BuiltinTypeSpec.Type.FirstPrimitive:
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
			case BuiltinTypeSpec.Type.Int:
			case BuiltinTypeSpec.Type.UInt:
			case BuiltinTypeSpec.Type.Float:
			case BuiltinTypeSpec.Type.IntPtr:
			case BuiltinTypeSpec.Type.UIntPtr:
				return true;
			}
			if (TypeSpec.IsReferenceType(base.MemberType))
			{
				return true;
			}
			if (base.MemberType.IsPointer)
			{
				return true;
			}
			if (base.MemberType.IsEnum)
			{
				switch (EnumSpec.GetUnderlyingType(base.MemberType).BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
				case BuiltinTypeSpec.Type.SByte:
				case BuiltinTypeSpec.Type.Short:
				case BuiltinTypeSpec.Type.UShort:
				case BuiltinTypeSpec.Type.Int:
				case BuiltinTypeSpec.Type.UInt:
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000AB3F8 File Offset: 0x000A95F8
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000AB404 File Offset: 0x000A9604
		public override bool Define()
		{
			if (!base.Define())
			{
				return false;
			}
			Type[] requiredCustomModifiers = null;
			if ((base.ModFlags & Modifiers.VOLATILE) != (Modifiers)0)
			{
				TypeSpec typeSpec = this.Module.PredefinedTypes.IsVolatile.Resolve();
				if (typeSpec != null)
				{
					requiredCustomModifiers = new Type[]
					{
						typeSpec.GetMetaInfo()
					};
				}
			}
			this.FieldBuilder = this.Parent.TypeBuilder.DefineField(base.Name, this.member_type.GetMetaInfo(), requiredCustomModifiers, null, ModifiersExtensions.FieldAttr(base.ModFlags));
			this.spec = new FieldSpec(this.Parent.Definition, this, base.MemberType, this.FieldBuilder, base.ModFlags);
			if ((base.ModFlags & Modifiers.BACKING_FIELD) == (Modifiers)0 || this.Parent.Kind == MemberKind.Struct)
			{
				this.Parent.MemberCache.AddMember(this.spec);
			}
			if (this.initializer != null)
			{
				this.Parent.RegisterFieldForInitialization(this, new FieldInitializer(this, this.initializer, base.TypeExpression.Location));
			}
			if (this.declarators != null)
			{
				foreach (FieldDeclarator fieldDeclarator in this.declarators)
				{
					Field field = new Field(this.Parent, fieldDeclarator.GetFieldTypeExpression(this), base.ModFlags, new MemberName(fieldDeclarator.Name.Value, fieldDeclarator.Name.Location), base.OptAttributes);
					if (fieldDeclarator.Initializer != null)
					{
						field.initializer = fieldDeclarator.Initializer;
					}
					field.Define();
					this.Parent.PartialContainer.Members.Add(field);
				}
			}
			return true;
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000AB5CC File Offset: 0x000A97CC
		protected override void DoMemberTypeDependentChecks()
		{
			if ((base.ModFlags & Modifiers.BACKING_FIELD) != (Modifiers)0)
			{
				return;
			}
			base.DoMemberTypeDependentChecks();
			if ((base.ModFlags & Modifiers.VOLATILE) != (Modifiers)0)
			{
				if (!this.CanBeVolatile())
				{
					base.Report.Error(677, base.Location, "`{0}': A volatile field cannot be of the type `{1}'", this.GetSignatureForError(), base.MemberType.GetSignatureForError());
				}
				if ((base.ModFlags & Modifiers.READONLY) != (Modifiers)0)
				{
					base.Report.Error(678, base.Location, "`{0}': A field cannot be both volatile and readonly", this.GetSignatureForError());
				}
			}
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000AB65F File Offset: 0x000A985F
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance())
			{
				return false;
			}
			if ((base.ModFlags & Modifiers.VOLATILE) != (Modifiers)0)
			{
				base.Report.Warning(3026, 1, base.Location, "CLS-compliant field `{0}' cannot be volatile", this.GetSignatureForError());
			}
			return true;
		}

		// Token: 0x04000D64 RID: 3428
		private const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.STATIC | Modifiers.READONLY | Modifiers.VOLATILE | Modifiers.UNSAFE;
	}
}
