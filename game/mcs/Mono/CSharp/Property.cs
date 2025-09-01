using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000278 RID: 632
	public class Property : PropertyBase
	{
		// Token: 0x06001EFE RID: 7934 RVA: 0x00098E4C File Offset: 0x0009704C
		public Property(TypeDefinition parent, FullNamedExpression type, Modifiers mod, MemberName name, Attributes attrs) : base(parent, type, mod, (parent.PartialContainer.Kind == MemberKind.Interface) ? (Modifiers.NEW | Modifiers.UNSAFE) : ((parent.PartialContainer.Kind == MemberKind.Struct) ? (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.STATIC | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE) : (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.STATIC | Modifiers.VIRTUAL | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE)), name, attrs)
		{
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00098E9D File Offset: 0x0009709D
		// (set) Token: 0x06001F00 RID: 7936 RVA: 0x00098EA5 File Offset: 0x000970A5
		public Property.BackingFieldDeclaration BackingField
		{
			[CompilerGenerated]
			get
			{
				return this.<BackingField>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BackingField>k__BackingField = value;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00098EAE File Offset: 0x000970AE
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x00098EB6 File Offset: 0x000970B6
		public Expression Initializer
		{
			[CompilerGenerated]
			get
			{
				return this.<Initializer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Initializer>k__BackingField = value;
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00098EBF File Offset: 0x000970BF
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00098EC8 File Offset: 0x000970C8
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Target == AttributeTargets.Field)
			{
				this.BackingField.ApplyAttributeBuilder(a, ctor, cdata, pa);
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00098EF4 File Offset: 0x000970F4
		private void CreateAutomaticProperty()
		{
			this.BackingField = new Property.BackingFieldDeclaration(this, this.Initializer == null && base.Set == null);
			if (!this.BackingField.Define())
			{
				return;
			}
			if (this.Initializer != null)
			{
				this.BackingField.Initializer = this.Initializer;
				this.Parent.RegisterFieldForInitialization(this.BackingField, new FieldInitializer(this.BackingField, this.Initializer, base.Location));
				this.BackingField.ModFlags |= Modifiers.READONLY;
			}
			this.Parent.PartialContainer.Members.Add(this.BackingField);
			FieldExpr fieldExpr = new FieldExpr(this.BackingField, base.Location);
			if ((this.BackingField.ModFlags & Modifiers.STATIC) == (Modifiers)0)
			{
				fieldExpr.InstanceExpression = new CompilerGeneratedThis(this.Parent.CurrentType, base.Location);
			}
			base.Get.Block = new ToplevelBlock(this.Compiler, ParametersCompiled.EmptyReadOnlyParameters, Location.Null, (Block.Flags)0);
			Return s = new Return(fieldExpr, base.Get.Location);
			base.Get.Block.AddStatement(s);
			base.Get.ModFlags |= Modifiers.COMPILER_GENERATED;
			if (base.Set != null)
			{
				base.Set.Block = new ToplevelBlock(this.Compiler, base.Set.ParameterInfo, Location.Null, (Block.Flags)0);
				Assign expr = new SimpleAssign(fieldExpr, new SimpleName("value", Location.Null), Location.Null);
				base.Set.Block.AddStatement(new StatementExpression(expr, base.Set.Location));
				base.Set.ModFlags |= Modifiers.COMPILER_GENERATED;
			}
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000990C0 File Offset: 0x000972C0
		public override bool Define()
		{
			if (!base.Define())
			{
				return false;
			}
			this.flags |= (MethodAttributes.HideBySig | MethodAttributes.SpecialName);
			bool flag = base.AccessorFirst.Block == null && (base.AccessorSecond == null || base.AccessorSecond.Block == null) && (base.ModFlags & (Modifiers.ABSTRACT | Modifiers.EXTERN)) == (Modifiers)0;
			if (this.Initializer != null)
			{
				if (!flag)
				{
					base.Report.Error(8050, base.Location, "`{0}': Only auto-implemented properties can have initializers", this.GetSignatureForError());
				}
				if (this.IsInterface)
				{
					base.Report.Error(8052, base.Location, "`{0}': Properties inside interfaces cannot have initializers", this.GetSignatureForError());
				}
				if (this.Compiler.Settings.Version < LanguageVersion.V_6)
				{
					base.Report.FeatureIsNotAvailable(this.Compiler, base.Location, "auto-implemented property initializer");
				}
			}
			if (flag)
			{
				base.ModFlags |= Modifiers.AutoProperty;
				if (base.Get == null)
				{
					base.Report.Error(8051, base.Location, "Auto-implemented property `{0}' must have get accessor", this.GetSignatureForError());
					return false;
				}
				if (this.Compiler.Settings.Version < LanguageVersion.V_3 && this.Initializer == null)
				{
					base.Report.FeatureIsNotAvailable(this.Compiler, base.Location, "auto-implemented properties");
				}
				this.CreateAutomaticProperty();
			}
			if (!base.DefineAccessors())
			{
				return false;
			}
			if (base.AccessorSecond == null)
			{
				PropertyBase.PropertyMethod propertyMethod;
				if (base.AccessorFirst is PropertyBase.GetMethod)
				{
					propertyMethod = new PropertyBase.SetMethod(this, (Modifiers)0, ParametersCompiled.EmptyReadOnlyParameters, null, base.Location);
				}
				else
				{
					propertyMethod = new PropertyBase.GetMethod(this, (Modifiers)0, null, base.Location);
				}
				this.Parent.AddNameToContainer(propertyMethod, propertyMethod.MemberName.Basename);
			}
			if (!this.CheckBase())
			{
				return false;
			}
			base.DefineBuilders(MemberKind.Property, ParametersCompiled.EmptyReadOnlyParameters);
			return true;
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00099294 File Offset: 0x00097494
		public override void Emit()
		{
			if ((base.AccessorFirst.ModFlags & (Modifiers.STATIC | Modifiers.COMPILER_GENERATED)) == Modifiers.COMPILER_GENERATED && this.Parent.PartialContainer.HasExplicitLayout)
			{
				base.Report.Error(842, base.Location, "Automatically implemented property `{0}' cannot be used inside a type with an explicit StructLayout attribute", this.GetSignatureForError());
			}
			base.Emit();
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x000992F2 File Offset: 0x000974F2
		public override string[] ValidAttributeTargets
		{
			get
			{
				if (base.Get == null || (base.Get.ModFlags & Modifiers.COMPILER_GENERATED) == (Modifiers)0)
				{
					return base.ValidAttributeTargets;
				}
				return Property.attribute_target_auto;
			}
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0009931B File Offset: 0x0009751B
		// Note: this type is marked as 'beforefieldinit'.
		static Property()
		{
		}

		// Token: 0x04000B66 RID: 2918
		private static readonly string[] attribute_target_auto = new string[]
		{
			"property",
			"field"
		};

		// Token: 0x04000B67 RID: 2919
		[CompilerGenerated]
		private Property.BackingFieldDeclaration <BackingField>k__BackingField;

		// Token: 0x04000B68 RID: 2920
		[CompilerGenerated]
		private Expression <Initializer>k__BackingField;

		// Token: 0x020003DD RID: 989
		public sealed class BackingFieldDeclaration : Field
		{
			// Token: 0x060027A6 RID: 10150 RVA: 0x000BC98C File Offset: 0x000BAB8C
			public BackingFieldDeclaration(Property p, bool readOnly) : base(p.Parent, p.type_expr, Modifiers.PRIVATE | Modifiers.COMPILER_GENERATED | Modifiers.BACKING_FIELD | Modifiers.DEBUGGER_HIDDEN | (p.ModFlags & (Modifiers.STATIC | Modifiers.UNSAFE)), new MemberName("<" + p.GetFullName(p.MemberName) + ">k__BackingField", p.Location), null)
			{
				this.property = p;
				if (readOnly)
				{
					base.ModFlags |= Modifiers.READONLY;
				}
			}

			// Token: 0x170008F4 RID: 2292
			// (get) Token: 0x060027A7 RID: 10151 RVA: 0x000BCA00 File Offset: 0x000BAC00
			public Property OriginalProperty
			{
				get
				{
					return this.property;
				}
			}

			// Token: 0x060027A8 RID: 10152 RVA: 0x000BCA08 File Offset: 0x000BAC08
			public override string GetSignatureForError()
			{
				return this.property.GetSignatureForError();
			}

			// Token: 0x0400110D RID: 4365
			private readonly Property property;

			// Token: 0x0400110E RID: 4366
			private const Modifiers DefaultModifiers = Modifiers.PRIVATE | Modifiers.COMPILER_GENERATED | Modifiers.BACKING_FIELD | Modifiers.DEBUGGER_HIDDEN;
		}
	}
}
