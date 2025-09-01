using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020002D8 RID: 728
	public abstract class FieldBase : MemberBase
	{
		// Token: 0x0600229B RID: 8859 RVA: 0x000AA739 File Offset: 0x000A8939
		protected FieldBase(TypeDefinition parent, FullNamedExpression type, Modifiers mod, Modifiers allowed_mod, MemberName name, Attributes attrs) : base(parent, type, mod, allowed_mod | Modifiers.ABSTRACT, Modifiers.PRIVATE, name, attrs)
		{
			if ((mod & Modifiers.ABSTRACT) != (Modifiers)0)
			{
				base.Report.Error(681, base.Location, "The modifier 'abstract' is not valid on fields. Try using a property instead");
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x000AA76F File Offset: 0x000A896F
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Field;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x000AA776 File Offset: 0x000A8976
		public List<FieldDeclarator> Declarators
		{
			get
			{
				return this.declarators;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x000AA77E File Offset: 0x000A897E
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x000AA786 File Offset: 0x000A8986
		public Expression Initializer
		{
			get
			{
				return this.initializer;
			}
			set
			{
				this.initializer = value;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x0003F215 File Offset: 0x0003D415
		public string Name
		{
			get
			{
				return base.MemberName.Name;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x000AA78F File Offset: 0x000A898F
		public FieldSpec Spec
		{
			get
			{
				return this.spec;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000AA797 File Offset: 0x000A8997
		public override string[] ValidAttributeTargets
		{
			get
			{
				return FieldBase.attribute_targets;
			}
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000AA79E File Offset: 0x000A899E
		public void AddDeclarator(FieldDeclarator declarator)
		{
			if (this.declarators == null)
			{
				this.declarators = new List<FieldDeclarator>(2);
			}
			this.declarators.Add(declarator);
			this.Parent.AddNameToContainer(this, declarator.Name.Value);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000AA7D8 File Offset: 0x000A89D8
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Type == pa.FieldOffset)
			{
				this.status |= FieldBase.Status.HAS_OFFSET;
				if (!this.Parent.PartialContainer.HasExplicitLayout)
				{
					base.Report.Error(636, base.Location, "The FieldOffset attribute can only be placed on members of types marked with the StructLayout(LayoutKind.Explicit)");
					return;
				}
				if ((base.ModFlags & Modifiers.STATIC) != (Modifiers)0 || this is Const)
				{
					base.Report.Error(637, base.Location, "The FieldOffset attribute is not allowed on static or const fields");
					return;
				}
			}
			if (a.Type == pa.FixedBuffer)
			{
				base.Report.Error(1716, base.Location, "Do not use 'System.Runtime.CompilerServices.FixedBuffer' attribute. Use the 'fixed' field modifier instead");
				return;
			}
			if (a.HasSecurityAttribute)
			{
				a.Error_InvalidSecurityParent();
				return;
			}
			if (a.Type == pa.Dynamic)
			{
				a.Error_MisusedDynamicAttribute();
				return;
			}
			this.FieldBuilder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000AA8D8 File Offset: 0x000A8AD8
		public void SetCustomAttribute(MethodSpec ctor, byte[] data)
		{
			this.FieldBuilder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), data);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000AA8F4 File Offset: 0x000A8AF4
		protected override bool CheckBase()
		{
			if (!base.CheckBase())
			{
				return false;
			}
			bool flag = false;
			MemberSpec memberSpec2;
			MemberSpec memberSpec = MemberCache.FindBaseMember(this, out memberSpec2, ref flag);
			if (memberSpec == null)
			{
				memberSpec = memberSpec2;
			}
			if (memberSpec == null)
			{
				if ((base.ModFlags & Modifiers.NEW) != (Modifiers)0)
				{
					base.Report.Warning(109, 4, base.Location, "The member `{0}' does not hide an inherited member. The new keyword is not required", this.GetSignatureForError());
				}
			}
			else
			{
				if ((base.ModFlags & (Modifiers.NEW | Modifiers.OVERRIDE | Modifiers.BACKING_FIELD)) == (Modifiers)0)
				{
					base.Report.SymbolRelatedToPreviousError(memberSpec);
					base.Report.Warning(108, 2, base.Location, "`{0}' hides inherited member `{1}'. Use the new keyword if hiding was intended", this.GetSignatureForError(), memberSpec.GetSignatureForError());
				}
				if (memberSpec.IsAbstract)
				{
					base.Report.SymbolRelatedToPreviousError(memberSpec);
					base.Report.Error(533, base.Location, "`{0}' hides inherited abstract member `{1}'", this.GetSignatureForError(), memberSpec.GetSignatureForError());
				}
			}
			return true;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000AA9CA File Offset: 0x000A8BCA
		public virtual Constant ConvertInitializer(ResolveContext rc, Constant expr)
		{
			return expr.ConvertImplicitly(base.MemberType);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000AA9D8 File Offset: 0x000A8BD8
		protected override void DoMemberTypeDependentChecks()
		{
			base.DoMemberTypeDependentChecks();
			if (base.MemberType.IsGenericParameter)
			{
				return;
			}
			if (base.MemberType.IsStatic)
			{
				FieldBase.Error_VariableOfStaticClass(base.Location, this.GetSignatureForError(), base.MemberType, base.Report);
			}
			if (!base.IsCompilerGenerated)
			{
				this.CheckBase();
			}
			base.IsTypePermitted();
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x000AAA38 File Offset: 0x000A8C38
		public override string DocCommentHeader
		{
			get
			{
				return "F:";
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000AAA40 File Offset: 0x000A8C40
		public override void Emit()
		{
			if (this.member_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.FieldBuilder);
			}
			else if (!this.Parent.IsCompilerGenerated && this.member_type.HasDynamicElement)
			{
				this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.FieldBuilder, this.member_type, base.Location);
			}
			if ((base.ModFlags & Modifiers.COMPILER_GENERATED) != (Modifiers)0 && !this.Parent.IsCompilerGenerated)
			{
				this.Module.PredefinedAttributes.CompilerGenerated.EmitAttribute(this.FieldBuilder);
			}
			if ((base.ModFlags & Modifiers.DEBUGGER_HIDDEN) != (Modifiers)0)
			{
				this.Module.PredefinedAttributes.DebuggerBrowsable.EmitAttribute(this.FieldBuilder, DebuggerBrowsableState.Never);
			}
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			if ((this.status & FieldBase.Status.HAS_OFFSET) == (FieldBase.Status)0 && (base.ModFlags & (Modifiers.STATIC | Modifiers.BACKING_FIELD)) == (Modifiers)0 && this.Parent.PartialContainer.HasExplicitLayout)
			{
				base.Report.Error(625, base.Location, "`{0}': Instance field types marked with StructLayout(LayoutKind.Explicit) must have a FieldOffset attribute", this.GetSignatureForError());
			}
			ConstraintChecker.Check(this, this.member_type, this.type_expr.Location);
			base.Emit();
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000AAB96 File Offset: 0x000A8D96
		public static void Error_VariableOfStaticClass(Location loc, string variable_name, TypeSpec static_class, Report Report)
		{
			Report.SymbolRelatedToPreviousError(static_class);
			Report.Error(723, loc, "`{0}': cannot declare variables of static types", variable_name);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000AABB4 File Offset: 0x000A8DB4
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance())
			{
				return false;
			}
			if (!base.MemberType.IsCLSCompliant() || this is FixedField)
			{
				base.Report.Warning(3003, 1, base.Location, "Type of `{0}' is not CLS-compliant", this.GetSignatureForError());
			}
			return true;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000AAC03 File Offset: 0x000A8E03
		// Note: this type is marked as 'beforefieldinit'.
		static FieldBase()
		{
		}

		// Token: 0x04000D56 RID: 3414
		protected FieldBuilder FieldBuilder;

		// Token: 0x04000D57 RID: 3415
		protected FieldSpec spec;

		// Token: 0x04000D58 RID: 3416
		public FieldBase.Status status;

		// Token: 0x04000D59 RID: 3417
		protected Expression initializer;

		// Token: 0x04000D5A RID: 3418
		protected List<FieldDeclarator> declarators;

		// Token: 0x04000D5B RID: 3419
		private static readonly string[] attribute_targets = new string[]
		{
			"field"
		};

		// Token: 0x02000403 RID: 1027
		[Flags]
		public enum Status : byte
		{
			// Token: 0x04001169 RID: 4457
			HAS_OFFSET = 4
		}
	}
}
