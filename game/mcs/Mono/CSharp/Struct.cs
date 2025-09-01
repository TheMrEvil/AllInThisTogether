using System;
using System.Runtime.InteropServices;

namespace Mono.CSharp
{
	// Token: 0x02000130 RID: 304
	public sealed class Struct : ClassOrStruct
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x0003E264 File Offset: 0x0003C464
		public Struct(TypeContainer parent, MemberName name, Modifiers mod, Attributes attrs) : base(parent, name, attrs, MemberKind.Struct)
		{
			Modifiers def_access = base.IsTopLevel ? Modifiers.INTERNAL : Modifiers.PRIVATE;
			base.ModFlags = (ModifiersExtensions.Check(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.UNSAFE, mod, def_access, base.Location, base.Report) | Modifiers.SEALED);
			this.spec = new TypeSpec(this.Kind, null, this, null, base.ModFlags);
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00008E8B File Offset: 0x0000708B
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Struct;
			}
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0003E2C8 File Offset: 0x0003C4C8
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003E2D4 File Offset: 0x0003C4D4
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
			if (a.Type == pa.StructLayout)
			{
				Constant namedValue = a.GetNamedValue("CharSet");
				if (namedValue == null)
				{
					return;
				}
				for (int i = 0; i < base.Members.Count; i++)
				{
					FixedField fixedField = base.Members[i] as FixedField;
					if (fixedField != null)
					{
						fixedField.CharSetValue = new CharSet?((CharSet)Enum.Parse(typeof(CharSet), namedValue.GetValue().ToString()));
					}
				}
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003E368 File Offset: 0x0003C568
		private bool CheckStructCycles()
		{
			if (this.InTransit)
			{
				return false;
			}
			this.InTransit = true;
			foreach (MemberCore memberCore in base.Members)
			{
				Field field = memberCore as Field;
				if (field != null)
				{
					TypeSpec memberType = field.Spec.MemberType;
					if (memberType.IsStruct && !(memberType is BuiltinTypeSpec))
					{
						TypeSpec[] typeArguments = memberType.TypeArguments;
						for (int i = 0; i < typeArguments.Length; i++)
						{
							if (!Struct.CheckFieldTypeCycle(typeArguments[i]))
							{
								base.Report.Error(523, field.Location, "Struct member `{0}' of type `{1}' causes a cycle in the struct layout", field.GetSignatureForError(), memberType.GetSignatureForError());
								break;
							}
						}
						if ((!field.IsStatic || memberType != this.CurrentType) && !Struct.CheckFieldTypeCycle(memberType))
						{
							base.Report.Error(523, field.Location, "Struct member `{0}' of type `{1}' causes a cycle in the struct layout", field.GetSignatureForError(), memberType.GetSignatureForError());
							break;
						}
					}
				}
			}
			this.InTransit = false;
			return true;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003E490 File Offset: 0x0003C690
		private static bool CheckFieldTypeCycle(TypeSpec ts)
		{
			Struct @struct = ts.MemberDefinition as Struct;
			return @struct == null || @struct.CheckStructCycles();
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003E4B4 File Offset: 0x0003C6B4
		protected override bool DoDefineMembers()
		{
			bool result = base.DoDefineMembers();
			if (base.PrimaryConstructorParameters != null || (this.initialized_fields != null && !this.HasUserDefaultConstructor()))
			{
				this.generated_primary_constructor = this.DefineDefaultConstructor(false);
				this.generated_primary_constructor.Define();
			}
			return result;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0003E4ED File Offset: 0x0003C6ED
		public override void Emit()
		{
			this.CheckStructCycles();
			base.Emit();
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003E4FC File Offset: 0x0003C6FC
		private bool HasUserDefaultConstructor()
		{
			foreach (MemberCore memberCore in base.PartialContainer.Members)
			{
				Constructor constructor = memberCore as Constructor;
				if (constructor != null && !constructor.IsStatic && constructor.ParameterInfo.IsEmpty)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0003E574 File Offset: 0x0003C774
		public override bool IsUnmanagedType()
		{
			if (this.has_unmanaged_check_done)
			{
				return this.is_unmanaged;
			}
			if (this.requires_delayed_unmanagedtype_check)
			{
				return true;
			}
			TypeDefinition partialContainer = this.Parent.PartialContainer;
			if (partialContainer != null && partialContainer.IsGenericOrParentIsGeneric)
			{
				this.has_unmanaged_check_done = true;
				return false;
			}
			if (this.first_nonstatic_field != null)
			{
				this.requires_delayed_unmanagedtype_check = true;
				foreach (MemberCore memberCore in base.Members)
				{
					Field field = memberCore as Field;
					if (field != null && !field.IsStatic)
					{
						TypeSpec memberType = field.MemberType;
						if (memberType == null)
						{
							return true;
						}
						if (!memberType.IsUnmanaged)
						{
							this.has_unmanaged_check_done = true;
							return false;
						}
					}
				}
				this.has_unmanaged_check_done = true;
			}
			this.is_unmanaged = true;
			return true;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003E650 File Offset: 0x0003C850
		protected override TypeSpec[] ResolveBaseTypes(out FullNamedExpression base_class)
		{
			TypeSpec[] result = base.ResolveBaseTypes(out base_class);
			this.base_type = this.Compiler.BuiltinTypes.ValueType;
			return result;
		}

		// Token: 0x040006FC RID: 1788
		private bool is_unmanaged;

		// Token: 0x040006FD RID: 1789
		private bool has_unmanaged_check_done;

		// Token: 0x040006FE RID: 1790
		private bool InTransit;

		// Token: 0x040006FF RID: 1791
		private const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.UNSAFE;
	}
}
