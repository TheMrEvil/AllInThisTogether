using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x020001C9 RID: 457
	public class Enum : TypeDefinition
	{
		// Token: 0x0600180C RID: 6156 RVA: 0x00073E94 File Offset: 0x00072094
		public Enum(TypeContainer parent, FullNamedExpression type, Modifiers mod_flags, MemberName name, Attributes attrs) : base(parent, name, attrs, MemberKind.Enum)
		{
			this.underlying_type_expr = type;
			Modifiers def_access = base.IsTopLevel ? Modifiers.INTERNAL : Modifiers.PRIVATE;
			base.ModFlags = ModifiersExtensions.Check(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW, mod_flags, def_access, base.Location, base.Report);
			this.spec = new EnumSpec(null, this, null, null, base.ModFlags);
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0000C9C5 File Offset: 0x0000ABC5
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Enum;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00073EF5 File Offset: 0x000720F5
		public FullNamedExpression BaseTypeExpression
		{
			get
			{
				return this.underlying_type_expr;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00068400 File Offset: 0x00066600
		protected override TypeAttributes TypeAttr
		{
			get
			{
				return base.TypeAttr | TypeAttributes.NotPublic | TypeAttributes.Sealed;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00073EFD File Offset: 0x000720FD
		public TypeSpec UnderlyingType
		{
			get
			{
				return ((EnumSpec)this.spec).UnderlyingType;
			}
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00073F0F File Offset: 0x0007210F
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00073F18 File Offset: 0x00072118
		public void AddEnumMember(EnumMember em)
		{
			if (em.Name == Enum.UnderlyingValueField)
			{
				base.Report.Error(76, em.Location, "An item in an enumeration cannot have an identifier `{0}'", Enum.UnderlyingValueField);
				return;
			}
			base.AddMember(em);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00073F51 File Offset: 0x00072151
		public void Error_UnderlyingType(Location loc)
		{
			base.Report.Error(1008, loc, "Type byte, sbyte, short, ushort, int, uint, long or ulong expected");
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00073F6C File Offset: 0x0007216C
		protected override void DoDefineContainer()
		{
			TypeSpec typeSpec;
			if (this.underlying_type_expr != null)
			{
				typeSpec = this.underlying_type_expr.ResolveAsType(this, false);
				if (!EnumSpec.IsValidUnderlyingType(typeSpec))
				{
					this.Error_UnderlyingType(this.underlying_type_expr.Location);
					typeSpec = null;
				}
			}
			else
			{
				typeSpec = null;
			}
			if (typeSpec == null)
			{
				typeSpec = this.Compiler.BuiltinTypes.Int;
			}
			((EnumSpec)this.spec).UnderlyingType = typeSpec;
			this.TypeBuilder.DefineField(Enum.UnderlyingValueField, this.UnderlyingType.GetMetaInfo(), FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
			base.DefineBaseTypes();
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00073FFC File Offset: 0x000721FC
		protected override bool DoDefineMembers()
		{
			for (int i = 0; i < base.Members.Count; i++)
			{
				EnumMember enumMember = (EnumMember)base.Members[i];
				if (enumMember.Initializer == null)
				{
					EnumMember enumMember2 = enumMember;
					enumMember2.Initializer = new Enum.ImplicitInitializer(enumMember2, (i == 0) ? null : ((EnumMember)base.Members[i - 1]));
				}
				enumMember.Define();
			}
			return true;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsUnmanagedType()
		{
			return true;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00074066 File Offset: 0x00072266
		protected override TypeSpec[] ResolveBaseTypes(out FullNamedExpression base_class)
		{
			this.base_type = this.Compiler.BuiltinTypes.Enum;
			base_class = null;
			return null;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00074084 File Offset: 0x00072284
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance())
			{
				return false;
			}
			switch (this.UnderlyingType.BuiltinType)
			{
			case BuiltinTypeSpec.Type.UShort:
			case BuiltinTypeSpec.Type.UInt:
			case BuiltinTypeSpec.Type.ULong:
				base.Report.Warning(3009, 1, base.Location, "`{0}': base type `{1}' is not CLS-compliant", this.GetSignatureForError(), this.UnderlyingType.GetSignatureForError());
				break;
			}
			return true;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000740F3 File Offset: 0x000722F3
		// Note: this type is marked as 'beforefieldinit'.
		static Enum()
		{
		}

		// Token: 0x0400098E RID: 2446
		public static readonly string UnderlyingValueField = "value__";

		// Token: 0x0400098F RID: 2447
		private const Modifiers AllowedModifiers = Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW;

		// Token: 0x04000990 RID: 2448
		private readonly FullNamedExpression underlying_type_expr;

		// Token: 0x020003AF RID: 943
		private sealed class ImplicitInitializer : Expression
		{
			// Token: 0x0600270A RID: 9994 RVA: 0x000BADBD File Offset: 0x000B8FBD
			public ImplicitInitializer(EnumMember current, EnumMember prev)
			{
				this.current = current;
				this.prev = prev;
			}

			// Token: 0x0600270B RID: 9995 RVA: 0x000022F4 File Offset: 0x000004F4
			public override bool ContainsEmitWithAwait()
			{
				return false;
			}

			// Token: 0x0600270C RID: 9996 RVA: 0x000BADD3 File Offset: 0x000B8FD3
			public override Expression CreateExpressionTree(ResolveContext ec)
			{
				throw new NotSupportedException("Missing Resolve call");
			}

			// Token: 0x0600270D RID: 9997 RVA: 0x000BADE0 File Offset: 0x000B8FE0
			protected override Expression DoResolve(ResolveContext rc)
			{
				if (this.prev == null)
				{
					return New.Constantify(this.current.Parent.Definition, base.Location);
				}
				EnumConstant enumConstant = ((ConstSpec)this.prev.Spec).GetConstant(rc) as EnumConstant;
				Expression result;
				try
				{
					result = enumConstant.Increment();
				}
				catch (OverflowException)
				{
					rc.Report.Error(543, this.current.Location, "The enumerator value `{0}' is outside the range of enumerator underlying type `{1}'", this.current.GetSignatureForError(), ((Enum)this.current.Parent).UnderlyingType.GetSignatureForError());
					result = New.Constantify(this.current.Parent.Definition, this.current.Location);
				}
				return result;
			}

			// Token: 0x0600270E RID: 9998 RVA: 0x000BADD3 File Offset: 0x000B8FD3
			public override void Emit(EmitContext ec)
			{
				throw new NotSupportedException("Missing Resolve call");
			}

			// Token: 0x04001063 RID: 4195
			private readonly EnumMember prev;

			// Token: 0x04001064 RID: 4196
			private readonly EnumMember current;
		}
	}
}
