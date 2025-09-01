using System;
using System.Reflection;
using System.Reflection.Emit;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x02000277 RID: 631
	public abstract class PropertyBase : PropertyBasedMember
	{
		// Token: 0x06001EE6 RID: 7910 RVA: 0x000984FE File Offset: 0x000966FE
		protected PropertyBase(TypeDefinition parent, FullNamedExpression type, Modifiers mod_flags, Modifiers allowed_mod, MemberName name, Attributes attrs) : base(parent, type, mod_flags, allowed_mod, name, attrs)
		{
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0009850F File Offset: 0x0009670F
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Property;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x00098516 File Offset: 0x00096716
		public PropertyBase.PropertyMethod AccessorFirst
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0009851E File Offset: 0x0009671E
		public PropertyBase.PropertyMethod AccessorSecond
		{
			get
			{
				if (this.first != this.get)
				{
					return this.get;
				}
				return this.set;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0009853B File Offset: 0x0009673B
		public override Variance ExpectedMemberTypeVariance
		{
			get
			{
				if (this.get != null && this.set != null)
				{
					return Variance.None;
				}
				if (this.set != null)
				{
					return Variance.Contravariant;
				}
				return Variance.Covariant;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0009855A File Offset: 0x0009675A
		// (set) Token: 0x06001EEC RID: 7916 RVA: 0x00098562 File Offset: 0x00096762
		public PropertyBase.PropertyMethod Get
		{
			get
			{
				return this.get;
			}
			set
			{
				this.get = value;
				if (this.first == null)
				{
					this.first = value;
				}
				this.Parent.AddNameToContainer(this.get, this.get.MemberName.Basename);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x0009859B File Offset: 0x0009679B
		// (set) Token: 0x06001EEE RID: 7918 RVA: 0x000985A3 File Offset: 0x000967A3
		public PropertyBase.PropertyMethod Set
		{
			get
			{
				return this.set;
			}
			set
			{
				this.set = value;
				if (this.first == null)
				{
					this.first = value;
				}
				this.Parent.AddNameToContainer(this.set, this.set.MemberName.Basename);
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x000985DC File Offset: 0x000967DC
		public override string[] ValidAttributeTargets
		{
			get
			{
				return PropertyBase.attribute_targets;
			}
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000985E4 File Offset: 0x000967E4
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
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
			this.PropertyBuilder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00098634 File Offset: 0x00096834
		private void CheckMissingAccessor(MemberKind kind, ParametersCompiled parameters, bool get)
		{
			if (this.IsExplicitImpl)
			{
				MemberFilter filter;
				if (kind == MemberKind.Indexer)
				{
					filter = new MemberFilter(MemberCache.IndexerNameAlias, 0, kind, parameters, null);
				}
				else
				{
					filter = new MemberFilter(base.MemberName.Name, 0, kind, null, null);
				}
				PropertySpec propertySpec = MemberCache.FindMember(this.InterfaceType, filter, BindingRestriction.DeclaredOnly) as PropertySpec;
				if (propertySpec == null)
				{
					return;
				}
				MethodSpec methodSpec = get ? propertySpec.Get : propertySpec.Set;
				if (methodSpec != null)
				{
					base.Report.SymbolRelatedToPreviousError(methodSpec);
					base.Report.Error(551, base.Location, "Explicit interface implementation `{0}' is missing accessor `{1}'", this.GetSignatureForError(), methodSpec.GetSignatureForError());
				}
			}
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x000986D8 File Offset: 0x000968D8
		protected override bool CheckOverrideAgainstBase(MemberSpec base_member)
		{
			bool flag = base.CheckOverrideAgainstBase(base_member);
			PropertySpec propertySpec = (PropertySpec)base_member;
			if (this.Get == null)
			{
				if ((base.ModFlags & Modifiers.SEALED) != (Modifiers)0 && propertySpec.HasGet && !propertySpec.Get.IsAccessible(this))
				{
					base.Report.SymbolRelatedToPreviousError(propertySpec);
					base.Report.Error(545, base.Location, "`{0}': cannot override because `{1}' does not have accessible get accessor", this.GetSignatureForError(), propertySpec.GetSignatureForError());
					flag = false;
				}
			}
			else if (!propertySpec.HasGet)
			{
				if (flag)
				{
					base.Report.SymbolRelatedToPreviousError(propertySpec);
					base.Report.Error(545, this.Get.Location, "`{0}': cannot override because `{1}' does not have an overridable get accessor", this.Get.GetSignatureForError(), propertySpec.GetSignatureForError());
					flag = false;
				}
			}
			else if (this.Get.HasCustomAccessModifier || propertySpec.HasDifferentAccessibility)
			{
				if (!propertySpec.Get.IsAccessible(this))
				{
					base.Report.Error(115, this.Get.Location, "`{0}' is marked as an override but no accessible `get' accessor found to override", this.GetSignatureForError());
					flag = false;
				}
				else if (!InterfaceMemberBase.CheckAccessModifiers(this.Get, propertySpec.Get))
				{
					base.Error_CannotChangeAccessModifiers(this.Get, propertySpec.Get);
					flag = false;
				}
			}
			if (this.Set == null)
			{
				if (propertySpec.HasSet)
				{
					if ((base.ModFlags & Modifiers.SEALED) != (Modifiers)0 && !propertySpec.Set.IsAccessible(this))
					{
						base.Report.SymbolRelatedToPreviousError(propertySpec);
						base.Report.Error(546, base.Location, "`{0}': cannot override because `{1}' does not have accessible set accessor", this.GetSignatureForError(), propertySpec.GetSignatureForError());
						flag = false;
					}
					if ((base.ModFlags & Modifiers.AutoProperty) != (Modifiers)0)
					{
						base.Report.Error(8080, base.Location, "`{0}': Auto-implemented properties must override all accessors of the overridden property", this.GetSignatureForError());
						flag = false;
					}
				}
			}
			else if (!propertySpec.HasSet)
			{
				if (flag)
				{
					base.Report.SymbolRelatedToPreviousError(propertySpec);
					base.Report.Error(546, this.Set.Location, "`{0}': cannot override because `{1}' does not have an overridable set accessor", this.Set.GetSignatureForError(), propertySpec.GetSignatureForError());
					flag = false;
				}
			}
			else if (this.Set.HasCustomAccessModifier || propertySpec.HasDifferentAccessibility)
			{
				if (!propertySpec.Set.IsAccessible(this))
				{
					base.Report.Error(115, this.Set.Location, "`{0}' is marked as an override but no accessible `set' accessor found to override", this.GetSignatureForError());
					flag = false;
				}
				else if (!InterfaceMemberBase.CheckAccessModifiers(this.Set, propertySpec.Set))
				{
					base.Error_CannotChangeAccessModifiers(this.Set, propertySpec.Set);
					flag = false;
				}
			}
			if ((this.Set == null || !this.Set.HasCustomAccessModifier) && (this.Get == null || !this.Get.HasCustomAccessModifier) && !InterfaceMemberBase.CheckAccessModifiers(this, propertySpec))
			{
				base.Error_CannotChangeAccessModifiers(this, propertySpec);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000989BB File Offset: 0x00096BBB
		protected override void DoMemberTypeDependentChecks()
		{
			base.DoMemberTypeDependentChecks();
			base.IsTypePermitted();
			if (base.MemberType.IsStatic)
			{
				base.Error_StaticReturnType();
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000989DC File Offset: 0x00096BDC
		protected override void DoMemberTypeIndependentChecks()
		{
			base.DoMemberTypeIndependentChecks();
			if (this.AccessorSecond != null)
			{
				if ((this.Get.ModFlags & Modifiers.AccessibilityMask) != (Modifiers)0 && (this.Set.ModFlags & Modifiers.AccessibilityMask) != (Modifiers)0)
				{
					base.Report.Error(274, base.Location, "`{0}': Cannot specify accessibility modifiers for both accessors of the property or indexer", this.GetSignatureForError());
					return;
				}
			}
			else if ((base.ModFlags & Modifiers.OVERRIDE) == (Modifiers)0 && ((this.Get == null && (this.Set.ModFlags & Modifiers.AccessibilityMask) != (Modifiers)0) || (this.Set == null && (this.Get.ModFlags & Modifiers.AccessibilityMask) != (Modifiers)0)))
			{
				base.Report.Error(276, base.Location, "`{0}': accessibility modifiers on accessors may only be used if the property or indexer has both a get and a set accessor", this.GetSignatureForError());
			}
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00098A9E File Offset: 0x00096C9E
		protected bool DefineAccessors()
		{
			this.first.Define(this.Parent);
			if (this.AccessorSecond != null)
			{
				this.AccessorSecond.Define(this.Parent);
			}
			return true;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00098ACC File Offset: 0x00096CCC
		protected void DefineBuilders(MemberKind kind, ParametersCompiled parameters)
		{
			this.PropertyBuilder = this.Parent.TypeBuilder.DefineProperty(base.GetFullName(base.MemberName), PropertyAttributes.None, base.MemberType.GetMetaInfo(), null, null, parameters.GetMetaInfo(), null, null);
			PropertySpec propertySpec;
			if (kind == MemberKind.Indexer)
			{
				propertySpec = new IndexerSpec(this.Parent.Definition, this, base.MemberType, parameters, this.PropertyBuilder, base.ModFlags);
			}
			else
			{
				propertySpec = new PropertySpec(kind, this.Parent.Definition, this, base.MemberType, this.PropertyBuilder, base.ModFlags);
			}
			if (this.Get != null)
			{
				propertySpec.Get = this.Get.Spec;
				this.Parent.MemberCache.AddMember(this, this.Get.Spec.Name, this.Get.Spec);
			}
			else
			{
				this.CheckMissingAccessor(kind, parameters, true);
			}
			if (this.Set != null)
			{
				propertySpec.Set = this.Set.Spec;
				this.Parent.MemberCache.AddMember(this, this.Set.Spec.Name, this.Set.Spec);
			}
			else
			{
				this.CheckMissingAccessor(kind, parameters, false);
			}
			this.Parent.MemberCache.AddMember(this, this.PropertyBuilder.Name, propertySpec);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00098C20 File Offset: 0x00096E20
		public override void Emit()
		{
			base.CheckReservedNameConflict("get_", (this.get == null) ? null : this.get.Spec);
			base.CheckReservedNameConflict("set_", (this.set == null) ? null : this.set.Spec);
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			if (this.member_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.PropertyBuilder);
			}
			else if (this.member_type.HasDynamicElement)
			{
				this.Module.PredefinedAttributes.Dynamic.EmitAttribute(this.PropertyBuilder, this.member_type, base.Location);
			}
			ConstraintChecker.Check(this, this.member_type, this.type_expr.Location);
			this.first.Emit(this.Parent);
			if (this.AccessorSecond != null)
			{
				this.AccessorSecond.Emit(this.Parent);
			}
			base.Emit();
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x00098D2A File Offset: 0x00096F2A
		public override bool IsUsed
		{
			get
			{
				return this.IsExplicitImpl || (this.Get.IsUsed | this.Set.IsUsed);
			}
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00098D50 File Offset: 0x00096F50
		public override void PrepareEmit()
		{
			this.AccessorFirst.PrepareEmit();
			if (this.AccessorSecond != null)
			{
				this.AccessorSecond.PrepareEmit();
			}
			if (this.get != null)
			{
				MethodBuilder methodBuilder = this.Get.Spec.GetMetaInfo() as MethodBuilder;
				if (methodBuilder != null)
				{
					this.PropertyBuilder.SetGetMethod(methodBuilder);
				}
			}
			if (this.set != null)
			{
				MethodBuilder methodBuilder2 = this.Set.Spec.GetMetaInfo() as MethodBuilder;
				if (methodBuilder2 != null)
				{
					this.PropertyBuilder.SetSetMethod(methodBuilder2);
				}
			}
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00098DD5 File Offset: 0x00096FD5
		protected override void SetMemberName(MemberName new_name)
		{
			base.SetMemberName(new_name);
			if (this.Get != null)
			{
				this.Get.UpdateName(this);
			}
			if (this.Set != null)
			{
				this.Set.UpdateName(this);
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00098E06 File Offset: 0x00097006
		public override void WriteDebugSymbol(MonoSymbolFile file)
		{
			if (this.get != null)
			{
				this.get.WriteDebugSymbol(file);
			}
			if (this.set != null)
			{
				this.set.WriteDebugSymbol(file);
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x00098E30 File Offset: 0x00097030
		public override string DocCommentHeader
		{
			get
			{
				return "P:";
			}
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00098E37 File Offset: 0x00097037
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyBase()
		{
		}

		// Token: 0x04000B61 RID: 2913
		private static readonly string[] attribute_targets = new string[]
		{
			"property"
		};

		// Token: 0x04000B62 RID: 2914
		private PropertyBase.PropertyMethod get;

		// Token: 0x04000B63 RID: 2915
		private PropertyBase.PropertyMethod set;

		// Token: 0x04000B64 RID: 2916
		private PropertyBase.PropertyMethod first;

		// Token: 0x04000B65 RID: 2917
		private PropertyBuilder PropertyBuilder;

		// Token: 0x020003DA RID: 986
		public class GetMethod : PropertyBase.PropertyMethod
		{
			// Token: 0x0600278F RID: 10127 RVA: 0x000BC4EC File Offset: 0x000BA6EC
			public GetMethod(PropertyBase method, Modifiers modifiers, Attributes attrs, Location loc) : base(method, "get_", modifiers, attrs, loc)
			{
			}

			// Token: 0x06002790 RID: 10128 RVA: 0x000BC500 File Offset: 0x000BA700
			public override void Define(TypeContainer parent)
			{
				base.Define(parent);
				base.Spec = new MethodSpec(MemberKind.Method, parent.PartialContainer.Definition, this, this.ReturnType, this.ParameterInfo, base.ModFlags);
				this.method_data = new MethodData(this.method, base.ModFlags, this.flags, this);
				this.method_data.Define(parent.PartialContainer, this.method.GetFullName(base.MemberName));
			}

			// Token: 0x170008EB RID: 2283
			// (get) Token: 0x06002791 RID: 10129 RVA: 0x000BC57F File Offset: 0x000BA77F
			public override TypeSpec ReturnType
			{
				get
				{
					return this.method.MemberType;
				}
			}

			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x06002792 RID: 10130 RVA: 0x000BC58C File Offset: 0x000BA78C
			public override ParametersCompiled ParameterInfo
			{
				get
				{
					return ParametersCompiled.EmptyReadOnlyParameters;
				}
			}

			// Token: 0x170008ED RID: 2285
			// (get) Token: 0x06002793 RID: 10131 RVA: 0x000BC593 File Offset: 0x000BA793
			public override string[] ValidAttributeTargets
			{
				get
				{
					return PropertyBase.GetMethod.attribute_targets;
				}
			}

			// Token: 0x06002794 RID: 10132 RVA: 0x000BC59A File Offset: 0x000BA79A
			// Note: this type is marked as 'beforefieldinit'.
			static GetMethod()
			{
			}

			// Token: 0x04001105 RID: 4357
			private static readonly string[] attribute_targets = new string[]
			{
				"method",
				"return"
			};

			// Token: 0x04001106 RID: 4358
			public const string Prefix = "get_";
		}

		// Token: 0x020003DB RID: 987
		public class SetMethod : PropertyBase.PropertyMethod
		{
			// Token: 0x06002795 RID: 10133 RVA: 0x000BC5B7 File Offset: 0x000BA7B7
			public SetMethod(PropertyBase method, Modifiers modifiers, ParametersCompiled parameters, Attributes attrs, Location loc) : base(method, "set_", modifiers, attrs, loc)
			{
				this.parameters = parameters;
			}

			// Token: 0x06002796 RID: 10134 RVA: 0x000BC5D1 File Offset: 0x000BA7D1
			protected override void ApplyToExtraTarget(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
			{
				if (a.Target == AttributeTargets.Parameter)
				{
					this.parameters[this.parameters.Count - 1].ApplyAttributeBuilder(a, ctor, cdata, pa);
					return;
				}
				base.ApplyToExtraTarget(a, ctor, cdata, pa);
			}

			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x06002797 RID: 10135 RVA: 0x000BC60E File Offset: 0x000BA80E
			public override ParametersCompiled ParameterInfo
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x06002798 RID: 10136 RVA: 0x000BC618 File Offset: 0x000BA818
			public override void Define(TypeContainer parent)
			{
				this.parameters.Resolve(this);
				base.Define(parent);
				base.Spec = new MethodSpec(MemberKind.Method, parent.PartialContainer.Definition, this, this.ReturnType, this.ParameterInfo, base.ModFlags);
				this.method_data = new MethodData(this.method, base.ModFlags, this.flags, this);
				this.method_data.Define(parent.PartialContainer, this.method.GetFullName(base.MemberName));
			}

			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x06002799 RID: 10137 RVA: 0x000BC6A4 File Offset: 0x000BA8A4
			public override TypeSpec ReturnType
			{
				get
				{
					return this.Parent.Compiler.BuiltinTypes.Void;
				}
			}

			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x0600279A RID: 10138 RVA: 0x000BC6BB File Offset: 0x000BA8BB
			public override string[] ValidAttributeTargets
			{
				get
				{
					return PropertyBase.SetMethod.attribute_targets;
				}
			}

			// Token: 0x0600279B RID: 10139 RVA: 0x000BC6C2 File Offset: 0x000BA8C2
			// Note: this type is marked as 'beforefieldinit'.
			static SetMethod()
			{
			}

			// Token: 0x04001107 RID: 4359
			private static readonly string[] attribute_targets = new string[]
			{
				"method",
				"param",
				"return"
			};

			// Token: 0x04001108 RID: 4360
			public const string Prefix = "set_";

			// Token: 0x04001109 RID: 4361
			protected ParametersCompiled parameters;
		}

		// Token: 0x020003DC RID: 988
		public abstract class PropertyMethod : AbstractPropertyEventMethod
		{
			// Token: 0x0600279C RID: 10140 RVA: 0x000BC6E8 File Offset: 0x000BA8E8
			public PropertyMethod(PropertyBase method, string prefix, Modifiers modifiers, Attributes attrs, Location loc) : base(method, prefix, attrs, loc)
			{
				this.method = method;
				base.ModFlags = ModifiersExtensions.Check(Modifiers.AccessibilityMask, modifiers, (Modifiers)0, loc, base.Report);
				base.ModFlags |= (method.ModFlags & (Modifiers.STATIC | Modifiers.UNSAFE));
			}

			// Token: 0x0600279D RID: 10141 RVA: 0x000BC738 File Offset: 0x000BA938
			public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
			{
				if (a.Type == pa.MethodImpl)
				{
					this.method.is_external_implementation = a.IsInternalCall();
				}
				base.ApplyAttributeBuilder(a, ctor, cdata, pa);
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x0600279E RID: 10142 RVA: 0x0008DC6A File Offset: 0x0008BE6A
			public override AttributeTargets AttributeTargets
			{
				get
				{
					return AttributeTargets.Method;
				}
			}

			// Token: 0x0600279F RID: 10143 RVA: 0x000BC76A File Offset: 0x000BA96A
			public override bool IsClsComplianceRequired()
			{
				return this.method.IsClsComplianceRequired();
			}

			// Token: 0x060027A0 RID: 10144 RVA: 0x000BC778 File Offset: 0x000BA978
			public virtual void Define(TypeContainer parent)
			{
				TypeDefinition partialContainer = parent.PartialContainer;
				if ((base.ModFlags & Modifiers.AccessibilityMask) == (Modifiers)0)
				{
					base.ModFlags |= this.method.ModFlags;
					this.flags = this.method.flags;
				}
				else
				{
					this.CheckModifiers(base.ModFlags);
					base.ModFlags |= (this.method.ModFlags & ~(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL));
					base.ModFlags |= Modifiers.PROPERTY_CUSTOM;
					if (partialContainer.Kind == MemberKind.Interface)
					{
						base.Report.Error(275, base.Location, "`{0}': accessibility modifiers may not be used on accessors in an interface", this.GetSignatureForError());
					}
					else if ((base.ModFlags & Modifiers.PRIVATE) != (Modifiers)0)
					{
						if ((this.method.ModFlags & Modifiers.ABSTRACT) != (Modifiers)0)
						{
							base.Report.Error(442, base.Location, "`{0}': abstract properties cannot have private accessors", this.GetSignatureForError());
						}
						base.ModFlags &= ~Modifiers.VIRTUAL;
					}
					this.flags = (ModifiersExtensions.MethodAttr(base.ModFlags) | MethodAttributes.SpecialName);
				}
				base.CheckAbstractAndExtern(this.block != null);
				base.CheckProtectedModifier();
				if (this.block != null)
				{
					if (this.block.IsIterator)
					{
						Iterator.CreateIterator(this, this.Parent.PartialContainer, base.ModFlags);
					}
					if (this.Compiler.Settings.WriteMetadataOnly)
					{
						this.block = null;
					}
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x060027A1 RID: 10145 RVA: 0x000BC8ED File Offset: 0x000BAAED
			public bool HasCustomAccessModifier
			{
				get
				{
					return (base.ModFlags & Modifiers.PROPERTY_CUSTOM) > (Modifiers)0;
				}
			}

			// Token: 0x170008F3 RID: 2291
			// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000BC8FE File Offset: 0x000BAAFE
			public PropertyBase Property
			{
				get
				{
					return this.method;
				}
			}

			// Token: 0x060027A3 RID: 10147 RVA: 0x000BC906 File Offset: 0x000BAB06
			public override ObsoleteAttribute GetAttributeObsolete()
			{
				return this.method.GetAttributeObsolete();
			}

			// Token: 0x060027A4 RID: 10148 RVA: 0x000BC913 File Offset: 0x000BAB13
			public override string GetSignatureForError()
			{
				return this.method.GetSignatureForError() + "." + this.prefix.Substring(0, 3);
			}

			// Token: 0x060027A5 RID: 10149 RVA: 0x000BC938 File Offset: 0x000BAB38
			private void CheckModifiers(Modifiers modflags)
			{
				if (!ModifiersExtensions.IsRestrictedModifier(modflags & Modifiers.AccessibilityMask, this.method.ModFlags & Modifiers.AccessibilityMask))
				{
					base.Report.Error(273, base.Location, "The accessibility modifier of the `{0}' accessor must be more restrictive than the modifier of the property or indexer `{1}'", this.GetSignatureForError(), this.method.GetSignatureForError());
				}
			}

			// Token: 0x0400110A RID: 4362
			private const Modifiers AllowedModifiers = Modifiers.AccessibilityMask;

			// Token: 0x0400110B RID: 4363
			protected readonly PropertyBase method;

			// Token: 0x0400110C RID: 4364
			protected MethodAttributes flags;
		}
	}
}
