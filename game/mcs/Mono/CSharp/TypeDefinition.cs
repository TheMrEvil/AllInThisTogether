using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x0200012D RID: 301
	public abstract class TypeDefinition : TypeContainer, ITypeDefinition, IMemberDefinition
	{
		// Token: 0x06000EBB RID: 3771 RVA: 0x0003A788 File Offset: 0x00038988
		protected TypeDefinition(TypeContainer parent, MemberName name, Attributes attrs, MemberKind kind) : base(parent, name, attrs, kind)
		{
			base.PartialContainer = this;
			this.members = new List<MemberCore>();
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0003A7B2 File Offset: 0x000389B2
		public List<FullNamedExpression> BaseTypeExpressions
		{
			get
			{
				return this.type_bases;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0003A7BC File Offset: 0x000389BC
		public override TypeSpec CurrentType
		{
			get
			{
				if (this.current_type == null)
				{
					if (this.IsGenericOrParentIsGeneric)
					{
						TypeSpec[] targs = (this.CurrentTypeParameters == null) ? TypeSpec.EmptyTypes : this.CurrentTypeParameters.Types;
						this.current_type = this.spec.MakeGenericType(this, targs);
					}
					else
					{
						this.current_type = this.spec;
					}
				}
				return this.current_type;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0003A81B File Offset: 0x00038A1B
		public override TypeParameters CurrentTypeParameters
		{
			get
			{
				return base.PartialContainer.MemberName.TypeParameters;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0003A830 File Offset: 0x00038A30
		private int CurrentTypeParametersStartIndex
		{
			get
			{
				int num = this.all_tp_builders.Length;
				if (this.CurrentTypeParameters != null)
				{
					return num - this.CurrentTypeParameters.Count;
				}
				return num;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0003A85D File Offset: 0x00038A5D
		public virtual AssemblyDefinition DeclaringAssembly
		{
			get
			{
				return this.Module.DeclaringAssembly;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0003A85D File Offset: 0x00038A5D
		IAssemblyDefinition ITypeDefinition.DeclaringAssembly
		{
			get
			{
				return this.Module.DeclaringAssembly;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0003A86A File Offset: 0x00038A6A
		public TypeSpec Definition
		{
			get
			{
				return this.spec;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0003A872 File Offset: 0x00038A72
		public bool HasMembersDefined
		{
			get
			{
				return this.members_defined;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0003A87A File Offset: 0x00038A7A
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x0003A88B File Offset: 0x00038A8B
		public bool HasInstanceConstructor
		{
			get
			{
				return (this.caching_flags & MemberCore.Flags.HasInstanceConstructor) > (MemberCore.Flags)0;
			}
			set
			{
				this.caching_flags |= MemberCore.Flags.HasInstanceConstructor;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0003A89F File Offset: 0x00038A9F
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x0003A8B0 File Offset: 0x00038AB0
		public bool HasExplicitLayout
		{
			get
			{
				return (this.caching_flags & MemberCore.Flags.HasExplicitLayout) > (MemberCore.Flags)0;
			}
			set
			{
				this.caching_flags |= MemberCore.Flags.HasExplicitLayout;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0003A8C4 File Offset: 0x00038AC4
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x0003A8D5 File Offset: 0x00038AD5
		public bool HasOperators
		{
			get
			{
				return (this.caching_flags & MemberCore.Flags.HasUserOperators) > (MemberCore.Flags)0;
			}
			set
			{
				this.caching_flags |= MemberCore.Flags.HasUserOperators;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0003A8E9 File Offset: 0x00038AE9
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x0003A8FA File Offset: 0x00038AFA
		public bool HasStructLayout
		{
			get
			{
				return (this.caching_flags & MemberCore.Flags.HasStructLayout) > (MemberCore.Flags)0;
			}
			set
			{
				this.caching_flags |= MemberCore.Flags.HasStructLayout;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0003A90E File Offset: 0x00038B0E
		public TypeSpec[] Interfaces
		{
			get
			{
				return this.iface_exprs;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0003A916 File Offset: 0x00038B16
		public bool IsGenericOrParentIsGeneric
		{
			get
			{
				return this.all_type_parameters != null;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0003A921 File Offset: 0x00038B21
		public bool IsTopLevel
		{
			get
			{
				return !(this.Parent is TypeDefinition);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0003A934 File Offset: 0x00038B34
		public bool IsPartial
		{
			get
			{
				return (base.ModFlags & Modifiers.PARTIAL) > (Modifiers)0;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsCyclicTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0003A945 File Offset: 0x00038B45
		private bool IsPartialPart
		{
			get
			{
				return base.PartialContainer != this;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0003A953 File Offset: 0x00038B53
		public MemberCache MemberCache
		{
			get
			{
				return this.spec.MemberCache;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x0003A960 File Offset: 0x00038B60
		public List<MemberCore> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0003A968 File Offset: 0x00038B68
		string ITypeDefinition.Namespace
		{
			get
			{
				TypeContainer parent = this.Parent;
				while (parent.Kind != MemberKind.Namespace)
				{
					parent = parent.Parent;
				}
				if (parent.MemberName != null)
				{
					return parent.GetSignatureForError();
				}
				return null;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0003A9A2 File Offset: 0x00038BA2
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x0003A9AA File Offset: 0x00038BAA
		public ParametersCompiled PrimaryConstructorParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<PrimaryConstructorParameters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PrimaryConstructorParameters>k__BackingField = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0003A9B3 File Offset: 0x00038BB3
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x0003A9BB File Offset: 0x00038BBB
		public Arguments PrimaryConstructorBaseArguments
		{
			[CompilerGenerated]
			get
			{
				return this.<PrimaryConstructorBaseArguments>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PrimaryConstructorBaseArguments>k__BackingField = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0003A9C4 File Offset: 0x00038BC4
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x0003A9CC File Offset: 0x00038BCC
		public Location PrimaryConstructorBaseArgumentsStart
		{
			[CompilerGenerated]
			get
			{
				return this.<PrimaryConstructorBaseArgumentsStart>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PrimaryConstructorBaseArgumentsStart>k__BackingField = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0003A9D5 File Offset: 0x00038BD5
		public TypeParameters TypeParametersAll
		{
			get
			{
				return this.all_type_parameters;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003A9DD File Offset: 0x00038BDD
		public override string[] ValidAttributeTargets
		{
			get
			{
				if (this.PrimaryConstructorParameters == null)
				{
					return TypeDefinition.attribute_targets;
				}
				return TypeDefinition.attribute_targets_primary;
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003A9F2 File Offset: 0x00038BF2
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003A9FC File Offset: 0x00038BFC
		public void AddMember(MemberCore symbol)
		{
			if (symbol.MemberName.ExplicitInterface != null && this.Kind != MemberKind.Class && this.Kind != MemberKind.Struct)
			{
				base.Report.Error(541, symbol.Location, "`{0}': explicit interface declaration can only be declared in a class or struct", symbol.GetSignatureForError());
			}
			this.AddNameToContainer(symbol, symbol.MemberName.Name);
			this.members.Add(symbol);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003AA6F File Offset: 0x00038C6F
		public override void AddTypeContainer(TypeContainer tc)
		{
			this.AddNameToContainer(tc, tc.MemberName.Basename);
			base.AddTypeContainer(tc);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003AA8A File Offset: 0x00038C8A
		protected override void AddTypeContainerMember(TypeContainer tc)
		{
			this.members.Add(tc);
			if (this.containers == null)
			{
				this.containers = new List<TypeContainer>();
			}
			base.AddTypeContainerMember(tc);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003AAB4 File Offset: 0x00038CB4
		public virtual void AddNameToContainer(MemberCore symbol, string name)
		{
			if (((base.ModFlags | symbol.ModFlags) & Modifiers.COMPILER_GENERATED) != (Modifiers)0)
			{
				return;
			}
			MemberCore memberCore;
			if (!base.PartialContainer.defined_names.TryGetValue(name, out memberCore))
			{
				base.PartialContainer.defined_names.Add(name, symbol);
				return;
			}
			if (symbol.EnableOverloadChecks(memberCore))
			{
				return;
			}
			InterfaceMemberBase interfaceMemberBase = memberCore as InterfaceMemberBase;
			if (interfaceMemberBase != null && interfaceMemberBase.IsExplicitImpl)
			{
				return;
			}
			base.Report.SymbolRelatedToPreviousError(memberCore);
			if ((memberCore.ModFlags & Modifiers.PARTIAL) != (Modifiers)0 && (symbol is ClassOrStruct || symbol is Interface))
			{
				base.Error_MissingPartialModifier(symbol);
				return;
			}
			if (symbol is TypeParameter)
			{
				base.Report.Error(692, symbol.Location, "Duplicate type parameter `{0}'", symbol.GetSignatureForError());
				return;
			}
			base.Report.Error(102, symbol.Location, "The type `{0}' already contains a definition for `{1}'", this.GetSignatureForError(), name);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003AB97 File Offset: 0x00038D97
		public void AddConstructor(Constructor c)
		{
			this.AddConstructor(c, false);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003ABA4 File Offset: 0x00038DA4
		public void AddConstructor(Constructor c, bool isDefault)
		{
			bool flag = (c.ModFlags & Modifiers.STATIC) > (Modifiers)0;
			if (!isDefault)
			{
				this.AddNameToContainer(c, flag ? Constructor.TypeConstructorName : Constructor.ConstructorName);
			}
			if (flag && c.ParameterInfo.IsEmpty)
			{
				base.PartialContainer.has_static_constructor = true;
			}
			else
			{
				base.PartialContainer.HasInstanceConstructor = true;
			}
			this.members.Add(c);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003AC10 File Offset: 0x00038E10
		public bool AddField(FieldBase field)
		{
			this.AddMember(field);
			if ((field.ModFlags & Modifiers.STATIC) != (Modifiers)0)
			{
				return true;
			}
			FieldBase fieldBase = base.PartialContainer.first_nonstatic_field;
			if (fieldBase == null)
			{
				base.PartialContainer.first_nonstatic_field = field;
				return true;
			}
			if (this.Kind == MemberKind.Struct && fieldBase.Parent != field.Parent)
			{
				base.Report.SymbolRelatedToPreviousError(fieldBase.Parent);
				base.Report.Warning(282, 3, field.Location, "struct instance field `{0}' found in different declaration from instance field `{1}'", field.GetSignatureForError(), fieldBase.GetSignatureForError());
			}
			return true;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003ACA6 File Offset: 0x00038EA6
		public void AddIndexer(Indexer i)
		{
			this.members.Add(i);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003ACB4 File Offset: 0x00038EB4
		public void AddOperator(Operator op)
		{
			base.PartialContainer.HasOperators = true;
			this.AddMember(op);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003ACC9 File Offset: 0x00038EC9
		public void AddPartialPart(TypeDefinition part)
		{
			if (this.Kind != MemberKind.Class)
			{
				return;
			}
			if (this.class_partial_parts == null)
			{
				this.class_partial_parts = new List<TypeDefinition>();
			}
			this.class_partial_parts.Add(part);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003ACF8 File Offset: 0x00038EF8
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Target == AttributeTargets.Method)
			{
				foreach (MemberCore memberCore in this.members)
				{
					Constructor constructor = memberCore as Constructor;
					if (constructor != null && constructor.IsPrimaryConstructor)
					{
						constructor.ApplyAttributeBuilder(a, ctor, cdata, pa);
						return;
					}
				}
				throw new InternalErrorException();
			}
			if (this.has_normal_indexers && a.Type == pa.DefaultMember)
			{
				base.Report.Error(646, a.Location, "Cannot specify the `DefaultMember' attribute on type containing an indexer");
				return;
			}
			if (a.Type == pa.Required)
			{
				base.Report.Error(1608, a.Location, "The RequiredAttribute attribute is not permitted on C# types");
				return;
			}
			this.TypeBuilder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0000225C File Offset: 0x0000045C
		public override AttributeTargets AttributeTargets
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0003ADF4 File Offset: 0x00038FF4
		public TypeSpec BaseType
		{
			get
			{
				return this.spec.BaseType;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0003AE01 File Offset: 0x00039001
		protected virtual TypeAttributes TypeAttr
		{
			get
			{
				return ModifiersExtensions.TypeAttr(base.ModFlags, this.IsTopLevel);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0003AE14 File Offset: 0x00039014
		public int TypeParametersCount
		{
			get
			{
				return base.MemberName.Arity;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0003AE21 File Offset: 0x00039021
		TypeParameterSpec[] ITypeDefinition.TypeParameters
		{
			get
			{
				return base.PartialContainer.CurrentTypeParameters.Types;
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003AE33 File Offset: 0x00039033
		public string GetAttributeDefaultMember()
		{
			return this.indexer_name ?? "Item";
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0003AE44 File Offset: 0x00039044
		public bool IsComImport
		{
			get
			{
				return base.OptAttributes != null && base.OptAttributes.Contains(this.Module.PredefinedAttributes.ComImport);
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003AE6C File Offset: 0x0003906C
		public void RegisterFieldForInitialization(MemberCore field, FieldInitializer expression)
		{
			if (this.IsPartialPart)
			{
				base.PartialContainer.RegisterFieldForInitialization(field, expression);
			}
			if ((field.ModFlags & Modifiers.STATIC) != (Modifiers)0)
			{
				if (this.initialized_static_fields == null)
				{
					this.HasStaticFieldInitializer = true;
					this.initialized_static_fields = new List<FieldInitializer>(4);
				}
				this.initialized_static_fields.Add(expression);
				return;
			}
			if (this.Kind == MemberKind.Struct && this.Compiler.Settings.Version != LanguageVersion.Experimental)
			{
				base.Report.Error(573, expression.Location, "'{0}': Structs cannot have instance property or field initializers", this.GetSignatureForError());
			}
			if (this.initialized_fields == null)
			{
				this.initialized_fields = new List<FieldInitializer>(4);
			}
			this.initialized_fields.Add(expression);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003AF28 File Offset: 0x00039128
		public void ResolveFieldInitializers(BlockContext ec)
		{
			if (ec.IsStatic)
			{
				if (this.initialized_static_fields == null)
				{
					return;
				}
				bool flag = !ec.Module.Compiler.Settings.Optimize;
				ExpressionStatement[] array = new ExpressionStatement[this.initialized_static_fields.Count];
				for (int i = 0; i < this.initialized_static_fields.Count; i++)
				{
					FieldInitializer fieldInitializer = this.initialized_static_fields[i];
					ExpressionStatement expressionStatement = fieldInitializer.ResolveStatement(ec);
					if (expressionStatement == null)
					{
						expressionStatement = EmptyExpressionStatement.Instance;
					}
					else if (!fieldInitializer.IsSideEffectFree)
					{
						flag = true;
					}
					array[i] = expressionStatement;
				}
				for (int i = 0; i < this.initialized_static_fields.Count; i++)
				{
					FieldInitializer fieldInitializer2 = this.initialized_static_fields[i];
					if (flag || !fieldInitializer2.IsDefaultInitializer)
					{
						ec.AssignmentInfoOffset += fieldInitializer2.AssignmentOffset;
						ec.CurrentBlock.AddScopeStatement(new StatementExpression(array[i]));
					}
				}
				return;
			}
			else
			{
				if (this.initialized_fields == null)
				{
					return;
				}
				for (int j = 0; j < this.initialized_fields.Count; j++)
				{
					FieldInitializer fieldInitializer3 = this.initialized_fields[j];
					Expression expression = fieldInitializer3.Clone(new CloneContext());
					ExpressionStatement expressionStatement2 = fieldInitializer3.ResolveStatement(ec);
					if (expressionStatement2 == null)
					{
						this.initialized_fields[j] = new FieldInitializer(fieldInitializer3.Field, ErrorExpression.Instance, Location.Null);
					}
					else if (!fieldInitializer3.IsDefaultInitializer || this.Kind == MemberKind.Struct || !ec.Module.Compiler.Settings.Optimize)
					{
						ec.AssignmentInfoOffset += fieldInitializer3.AssignmentOffset;
						ec.CurrentBlock.AddScopeStatement(new StatementExpression(expressionStatement2));
						this.initialized_fields[j] = (FieldInitializer)expression;
					}
				}
				return;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0003B0F3 File Offset: 0x000392F3
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0003B0FB File Offset: 0x000392FB
		public override string DocComment
		{
			get
			{
				return this.comment;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				this.comment += value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003B113 File Offset: 0x00039313
		public PendingImplementation PendingImplementations
		{
			get
			{
				return this.pending;
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003B11C File Offset: 0x0003931C
		public override void GenerateDocComment(DocumentationBuilder builder)
		{
			if (this.IsPartialPart)
			{
				return;
			}
			base.GenerateDocComment(builder);
			foreach (MemberCore memberCore in this.members)
			{
				memberCore.GenerateDocComment(builder);
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003B180 File Offset: 0x00039380
		public TypeSpec GetAttributeCoClass()
		{
			if (base.OptAttributes == null)
			{
				return null;
			}
			Attribute attribute = base.OptAttributes.Search(this.Module.PredefinedAttributes.CoClass);
			if (attribute == null)
			{
				return null;
			}
			return attribute.GetCoClassAttributeValue();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003B1C0 File Offset: 0x000393C0
		public AttributeUsageAttribute GetAttributeUsage(PredefinedAttribute pa)
		{
			Attribute attribute = null;
			if (base.OptAttributes != null)
			{
				attribute = base.OptAttributes.Search(pa);
			}
			if (attribute == null)
			{
				return null;
			}
			return attribute.GetAttributeUsageAttribute();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003B1F0 File Offset: 0x000393F0
		public virtual CompilationSourceFile GetCompilationSourceFile()
		{
			TypeContainer parent = this.Parent;
			CompilationSourceFile compilationSourceFile;
			for (;;)
			{
				compilationSourceFile = (parent as CompilationSourceFile);
				if (compilationSourceFile != null)
				{
					break;
				}
				parent = parent.Parent;
			}
			return compilationSourceFile;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003B218 File Offset: 0x00039418
		public override string GetSignatureForMetadata()
		{
			if (this.Parent is TypeDefinition)
			{
				return this.Parent.GetSignatureForMetadata() + "+" + TypeNameParser.Escape(TypeDefinition.FilterNestedName(base.MemberName.Basename));
			}
			return base.GetSignatureForMetadata();
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003B258 File Offset: 0x00039458
		public virtual void SetBaseTypes(List<FullNamedExpression> baseTypes)
		{
			this.type_bases = baseTypes;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0003B264 File Offset: 0x00039464
		protected virtual TypeSpec[] ResolveBaseTypes(out FullNamedExpression base_class)
		{
			base_class = null;
			if (this.type_bases == null)
			{
				return null;
			}
			int count = this.type_bases.Count;
			TypeSpec[] array = null;
			TypeDefinition.BaseContext baseContext = new TypeDefinition.BaseContext(this);
			int i = 0;
			int num = 0;
			while (i < count)
			{
				FullNamedExpression fullNamedExpression = this.type_bases[i];
				TypeSpec typeSpec = fullNamedExpression.ResolveAsType(baseContext, false);
				if (typeSpec != null)
				{
					if (i == 0 && this.Kind == MemberKind.Class && !typeSpec.IsInterface)
					{
						if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
						{
							base.Report.Error(1965, base.Location, "Class `{0}' cannot derive from the dynamic type", this.GetSignatureForError());
						}
						else
						{
							this.base_type = typeSpec;
							base_class = fullNamedExpression;
						}
					}
					else
					{
						if (array == null)
						{
							array = new TypeSpec[count - i];
						}
						if (typeSpec.IsInterface)
						{
							for (int j = 0; j < num; j++)
							{
								if (typeSpec == array[j])
								{
									base.Report.Error(528, base.Location, "`{0}' is already listed in interface list", typeSpec.GetSignatureForError());
									break;
								}
							}
							if (this.Kind == MemberKind.Interface && !base.IsAccessibleAs(typeSpec))
							{
								base.Report.Error(61, fullNamedExpression.Location, "Inconsistent accessibility: base interface `{0}' is less accessible than interface `{1}'", typeSpec.GetSignatureForError(), this.GetSignatureForError());
							}
						}
						else
						{
							base.Report.SymbolRelatedToPreviousError(typeSpec);
							if (this.Kind != MemberKind.Class)
							{
								base.Report.Error(527, fullNamedExpression.Location, "Type `{0}' in interface list is not an interface", typeSpec.GetSignatureForError());
							}
							else if (base_class != null)
							{
								base.Report.Error(1721, fullNamedExpression.Location, "`{0}': Classes cannot have multiple base classes (`{1}' and `{2}')", new string[]
								{
									this.GetSignatureForError(),
									base_class.GetSignatureForError(),
									typeSpec.GetSignatureForError()
								});
							}
							else
							{
								base.Report.Error(1722, fullNamedExpression.Location, "`{0}': Base class `{1}' must be specified as first", this.GetSignatureForError(), typeSpec.GetSignatureForError());
							}
						}
						array[num++] = typeSpec;
					}
				}
				i++;
			}
			return array;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0003B480 File Offset: 0x00039680
		private void CheckPairedOperators()
		{
			bool flag = false;
			List<Operator.OpType> list = new List<Operator.OpType>();
			for (int i = 0; i < this.members.Count; i++)
			{
				Operator @operator = this.members[i] as Operator;
				if (@operator != null)
				{
					Operator.OpType operatorType = @operator.OperatorType;
					if (operatorType == Operator.OpType.Equality || operatorType == Operator.OpType.Inequality)
					{
						flag = true;
					}
					if (!list.Contains(operatorType))
					{
						Operator.OpType matchingOperator = @operator.GetMatchingOperator();
						if (matchingOperator != Operator.OpType.TOP)
						{
							bool flag2 = false;
							for (int j = 0; j < this.members.Count; j++)
							{
								Operator operator2 = this.members[j] as Operator;
								if (operator2 != null && operator2.OperatorType == matchingOperator && TypeSpecComparer.IsEqual(@operator.ReturnType, operator2.ReturnType) && TypeSpecComparer.Equals(@operator.ParameterTypes, operator2.ParameterTypes))
								{
									list.Add(matchingOperator);
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								base.Report.Error(216, @operator.Location, "The operator `{0}' requires a matching operator `{1}' to also be defined", @operator.GetSignatureForError(), Operator.GetName(matchingOperator));
							}
						}
					}
				}
			}
			if (flag)
			{
				if (!this.HasEquals)
				{
					base.Report.Warning(660, 2, base.Location, "`{0}' defines operator == or operator != but does not override Object.Equals(object o)", this.GetSignatureForError());
				}
				if (!this.HasGetHashCode)
				{
					base.Report.Warning(661, 2, base.Location, "`{0}' defines operator == or operator != but does not override Object.GetHashCode()", this.GetSignatureForError());
				}
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0003B5F8 File Offset: 0x000397F8
		public override void CreateMetadataName(StringBuilder sb)
		{
			if (this.Parent.MemberName != null)
			{
				this.Parent.CreateMetadataName(sb);
				if (sb.Length != 0)
				{
					sb.Append(".");
				}
			}
			sb.Append(base.MemberName.Basename);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0003B644 File Offset: 0x00039844
		private bool CreateTypeBuilder()
		{
			int typeSize = (this.Kind == MemberKind.Struct && this.first_nonstatic_field == null && !(this is StateMachine)) ? 1 : 0;
			TypeDefinition typeDefinition = this.Parent as TypeDefinition;
			if (typeDefinition == null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.CreateMetadataName(stringBuilder);
				this.TypeBuilder = this.Module.CreateBuilder(stringBuilder.ToString(), this.TypeAttr, typeSize);
			}
			else
			{
				this.TypeBuilder = typeDefinition.TypeBuilder.DefineNestedType(TypeDefinition.FilterNestedName(base.MemberName.Basename), this.TypeAttr, null, typeSize);
			}
			if (this.DeclaringAssembly.Importer != null)
			{
				this.DeclaringAssembly.Importer.AddCompiledType(this.TypeBuilder, this.spec);
			}
			this.spec.SetMetaInfo(this.TypeBuilder);
			this.spec.MemberCache = new MemberCache(this);
			TypeParameters typeParameters = null;
			if (typeDefinition != null)
			{
				this.spec.DeclaringType = this.Parent.CurrentType;
				typeDefinition.MemberCache.AddMember(this.spec);
				typeParameters = typeDefinition.all_type_parameters;
			}
			if (base.MemberName.TypeParameters != null || typeParameters != null)
			{
				string[] names = this.CreateTypeParameters(typeParameters);
				this.all_tp_builders = this.TypeBuilder.DefineGenericParameters(names);
				if (this.CurrentTypeParameters != null)
				{
					this.CurrentTypeParameters.Create(this.spec, this.CurrentTypeParametersStartIndex, this);
					this.CurrentTypeParameters.Define(this.all_tp_builders);
				}
			}
			return true;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003B7B4 File Offset: 0x000399B4
		public static string FilterNestedName(string name)
		{
			return name.Replace('.', '_');
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003B7C0 File Offset: 0x000399C0
		private string[] CreateTypeParameters(TypeParameters parentAllTypeParameters)
		{
			int num = 0;
			string[] array;
			if (parentAllTypeParameters != null)
			{
				if (this.CurrentTypeParameters == null)
				{
					this.all_type_parameters = parentAllTypeParameters;
					return parentAllTypeParameters.GetAllNames();
				}
				array = new string[parentAllTypeParameters.Count + this.CurrentTypeParameters.Count];
				this.all_type_parameters = new TypeParameters(array.Length);
				this.all_type_parameters.Add(parentAllTypeParameters);
				num = this.all_type_parameters.Count;
				for (int i = 0; i < num; i++)
				{
					array[i] = this.all_type_parameters[i].MemberName.Name;
				}
			}
			else
			{
				array = new string[this.CurrentTypeParameters.Count];
			}
			for (int j = 0; j < this.CurrentTypeParameters.Count; j++)
			{
				if (this.all_type_parameters != null)
				{
					this.all_type_parameters.Add(base.MemberName.TypeParameters[j]);
				}
				string name = this.CurrentTypeParameters[j].MemberName.Name;
				array[num + j] = name;
				for (int k = 0; k < num + j; k++)
				{
					if (!(array[k] != name))
					{
						TypeParameter typeParameter = this.CurrentTypeParameters[j];
						TypeParameter conflict = this.all_type_parameters[k];
						typeParameter.WarningParentNameConflict(conflict);
					}
				}
			}
			if (this.all_type_parameters == null)
			{
				this.all_type_parameters = this.CurrentTypeParameters;
			}
			return array;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003B914 File Offset: 0x00039B14
		public SourceMethodBuilder CreateMethodSymbolEntry()
		{
			if (this.Module.DeclaringAssembly.SymbolWriter == null || (base.ModFlags & Modifiers.DEBUGGER_HIDDEN) != (Modifiers)0)
			{
				return null;
			}
			CompilationSourceFile compilationSourceFile = this.GetCompilationSourceFile();
			if (compilationSourceFile == null)
			{
				return null;
			}
			return new SourceMethodBuilder(compilationSourceFile.SymbolUnitEntry);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0003B95C File Offset: 0x00039B5C
		public MethodSpec CreateHoistedBaseCallProxy(ResolveContext rc, MethodSpec method)
		{
			Method method2;
			if (this.hoisted_base_call_proxies == null)
			{
				this.hoisted_base_call_proxies = new Dictionary<MethodSpec, Method>();
				method2 = null;
			}
			else
			{
				this.hoisted_base_call_proxies.TryGetValue(method, out method2);
			}
			if (method2 == null)
			{
				string name = CompilerGeneratedContainer.MakeName(method.Name, null, "BaseCallProxy", this.hoisted_base_call_proxies.Count);
				TypeArguments typeArguments = null;
				TypeSpec typeSpec = method.ReturnType;
				TypeSpec[] array = method.Parameters.Types;
				MemberName name2;
				if (method.IsGeneric)
				{
					TypeParameterSpec[] typeParameters = method.GenericDefinition.TypeParameters;
					TypeParameters typeParameters2 = new TypeParameters();
					typeArguments = new TypeArguments(new FullNamedExpression[0]);
					typeArguments.Arguments = new TypeSpec[typeParameters.Length];
					for (int i = 0; i < typeParameters.Length; i++)
					{
						TypeParameterSpec typeParameterSpec = typeParameters[i];
						TypeParameter typeParameter = new TypeParameter(typeParameterSpec, null, new MemberName(typeParameterSpec.Name, base.Location), null);
						typeParameters2.Add(typeParameter);
						typeArguments.Add(new SimpleName(typeParameterSpec.Name, base.Location));
						typeArguments.Arguments[i] = typeParameter.Type;
					}
					name2 = new MemberName(name, typeParameters2, base.Location);
					TypeParameterMutator typeParameterMutator = new TypeParameterMutator(typeParameters, typeParameters2);
					typeSpec = typeParameterMutator.Mutate(typeSpec);
					array = typeParameterMutator.Mutate(array);
				}
				else
				{
					name2 = new MemberName(name);
				}
				Parameter[] array2 = new Parameter[method.Parameters.Count];
				for (int j = 0; j < array2.Length; j++)
				{
					IParameterData parameterData = method.Parameters.FixedParameters[j];
					array2[j] = new Parameter(new TypeExpression(array[j], base.Location), parameterData.Name, parameterData.ModFlags, null, base.Location);
					array2[j].Resolve(this, j);
				}
				ParametersCompiled parametersCompiled = ParametersCompiled.CreateFullyResolved(array2, method.Parameters.Types);
				if (method.Parameters.HasArglist)
				{
					parametersCompiled.FixedParameters[0] = new Parameter(null, "__arglist", Parameter.Modifier.NONE, null, base.Location);
					parametersCompiled.Types[0] = this.Module.PredefinedTypes.RuntimeArgumentHandle.Resolve();
				}
				method2 = new Method(this, new TypeExpression(typeSpec, base.Location), Modifiers.PRIVATE | Modifiers.COMPILER_GENERATED | Modifiers.DEBUGGER_HIDDEN, name2, parametersCompiled, null);
				ToplevelBlock toplevelBlock = new ToplevelBlock(this.Compiler, method2.ParameterInfo, base.Location, (Block.Flags)0)
				{
					IsCompilerGenerated = true
				};
				MethodGroupExpr methodGroupExpr = MethodGroupExpr.CreatePredefined(method, method.DeclaringType, base.Location);
				methodGroupExpr.InstanceExpression = new BaseThis(method.DeclaringType, base.Location);
				if (typeArguments != null)
				{
					methodGroupExpr.SetTypeArguments(rc, typeArguments);
				}
				Invocation expr = new Invocation(methodGroupExpr, toplevelBlock.GetAllParametersArguments());
				Statement s;
				if (method.ReturnType.Kind == MemberKind.Void)
				{
					s = new StatementExpression(expr);
				}
				else
				{
					s = new Return(expr, base.Location);
				}
				toplevelBlock.AddStatement(s);
				method2.Block = toplevelBlock;
				this.members.Add(method2);
				method2.Define();
				method2.PrepareEmit();
				this.hoisted_base_call_proxies.Add(method, method2);
			}
			return method2.Spec;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0003BC5B File Offset: 0x00039E5B
		protected bool DefineBaseTypes()
		{
			return (this.IsPartialPart && this.Kind == MemberKind.Class) || this.DoDefineBaseType();
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0003BC7C File Offset: 0x00039E7C
		private bool DoDefineBaseType()
		{
			this.iface_exprs = this.ResolveBaseTypes(out this.base_type_expr);
			bool flag;
			if (this.IsPartialPart)
			{
				flag = false;
				if (this.base_type_expr != null)
				{
					if (base.PartialContainer.base_type_expr != null && base.PartialContainer.base_type != this.base_type)
					{
						base.Report.SymbolRelatedToPreviousError(this.base_type_expr.Location, "");
						base.Report.Error(263, base.Location, "Partial declarations of `{0}' must not specify different base classes", this.GetSignatureForError());
					}
					else
					{
						base.PartialContainer.base_type_expr = this.base_type_expr;
						base.PartialContainer.base_type = this.base_type;
						flag = true;
					}
				}
				if (this.iface_exprs != null)
				{
					if (base.PartialContainer.iface_exprs == null)
					{
						base.PartialContainer.iface_exprs = this.iface_exprs;
					}
					else
					{
						List<TypeSpec> list = new List<TypeSpec>(base.PartialContainer.iface_exprs);
						foreach (TypeSpec item in this.iface_exprs)
						{
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
						base.PartialContainer.iface_exprs = list.ToArray();
					}
				}
				base.PartialContainer.members.AddRange(this.members);
				if (this.containers != null)
				{
					if (base.PartialContainer.containers == null)
					{
						base.PartialContainer.containers = new List<TypeContainer>();
					}
					base.PartialContainer.containers.AddRange(this.containers);
				}
				if (this.PrimaryConstructorParameters != null)
				{
					if (base.PartialContainer.PrimaryConstructorParameters != null)
					{
						base.Report.Error(8036, base.Location, "Only one part of a partial type can declare primary constructor parameters");
					}
					else
					{
						base.PartialContainer.PrimaryConstructorParameters = this.PrimaryConstructorParameters;
					}
				}
				this.members_defined = (this.members_defined_ok = true);
				this.caching_flags |= MemberCore.Flags.CloseTypeCreated;
			}
			else
			{
				flag = true;
			}
			TypeSpec typeSpec = this.CheckRecursiveDefinition(this);
			if (typeSpec != null)
			{
				base.Report.SymbolRelatedToPreviousError(typeSpec);
				if (this is Interface)
				{
					base.Report.Error(529, base.Location, "Inherited interface `{0}' causes a cycle in the interface hierarchy of `{1}'", this.GetSignatureForError(), typeSpec.GetSignatureForError());
					this.iface_exprs = null;
					base.PartialContainer.iface_exprs = null;
				}
				else
				{
					base.Report.Error(146, base.Location, "Circular base class dependency involving `{0}' and `{1}'", this.GetSignatureForError(), typeSpec.GetSignatureForError());
					this.base_type = null;
					base.PartialContainer.base_type = null;
				}
			}
			if (this.iface_exprs != null)
			{
				if (!this.PrimaryConstructorBaseArgumentsStart.IsNull)
				{
					base.Report.Error(8049, this.PrimaryConstructorBaseArgumentsStart, "Implemented interfaces cannot have arguments");
				}
				foreach (TypeSpec typeSpec2 in this.iface_exprs)
				{
					if (typeSpec2 != null && this.spec.AddInterfaceDefined(typeSpec2))
					{
						this.TypeBuilder.AddInterfaceImplementation(typeSpec2.GetMetaInfo());
					}
				}
			}
			if (this.Kind == MemberKind.Interface)
			{
				this.spec.BaseType = this.Compiler.BuiltinTypes.Object;
				return true;
			}
			if (flag)
			{
				this.SetBaseType();
			}
			if (this.class_partial_parts != null)
			{
				foreach (TypeDefinition typeDefinition in this.class_partial_parts)
				{
					if (typeDefinition.PrimaryConstructorBaseArguments != null)
					{
						this.PrimaryConstructorBaseArguments = typeDefinition.PrimaryConstructorBaseArguments;
					}
					typeDefinition.DoDefineBaseType();
				}
			}
			return true;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0003C014 File Offset: 0x0003A214
		private void SetBaseType()
		{
			if (this.base_type == null)
			{
				this.TypeBuilder.SetParent(null);
				return;
			}
			if (this.spec.BaseType == this.base_type)
			{
				return;
			}
			this.spec.BaseType = this.base_type;
			if (this.IsPartialPart)
			{
				this.spec.UpdateInflatedInstancesBaseType();
			}
			this.TypeBuilder.SetParent(this.base_type.GetMetaInfo());
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003C084 File Offset: 0x0003A284
		public override void ExpandBaseInterfaces()
		{
			if (!this.IsPartialPart)
			{
				this.DoExpandBaseInterfaces();
			}
			base.ExpandBaseInterfaces();
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003C09C File Offset: 0x0003A29C
		public void DoExpandBaseInterfaces()
		{
			if ((this.caching_flags & MemberCore.Flags.InterfacesExpanded) != (MemberCore.Flags)0)
			{
				return;
			}
			this.caching_flags |= MemberCore.Flags.InterfacesExpanded;
			if (this.iface_exprs != null)
			{
				foreach (TypeSpec typeSpec in this.iface_exprs)
				{
					if (typeSpec != null)
					{
						TypeDefinition typeDefinition = typeSpec.MemberDefinition as TypeDefinition;
						if (typeDefinition != null)
						{
							typeDefinition.DoExpandBaseInterfaces();
						}
						if (typeSpec.Interfaces != null)
						{
							foreach (TypeSpec typeSpec2 in typeSpec.Interfaces)
							{
								if (this.spec.AddInterfaceDefined(typeSpec2))
								{
									this.TypeBuilder.AddInterfaceImplementation(typeSpec2.GetMetaInfo());
								}
							}
						}
					}
				}
			}
			if (this.base_type != null)
			{
				TypeDefinition typeDefinition2 = this.base_type.MemberDefinition as TypeDefinition;
				if (typeDefinition2 != null)
				{
					typeDefinition2.DoExpandBaseInterfaces();
				}
				if (this.base_type.Interfaces != null)
				{
					foreach (TypeSpec iface in this.base_type.Interfaces)
					{
						this.spec.AddInterfaceDefined(iface);
					}
				}
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003C1F0 File Offset: 0x0003A3F0
		public override void PrepareEmit()
		{
			if ((this.caching_flags & MemberCore.Flags.CloseTypeCreated) != (MemberCore.Flags)0)
			{
				return;
			}
			foreach (MemberCore memberCore in this.members)
			{
				PropertyBasedMember propertyBasedMember = memberCore as PropertyBasedMember;
				if (propertyBasedMember != null)
				{
					propertyBasedMember.PrepareEmit();
				}
				else
				{
					MethodCore methodCore = memberCore as MethodCore;
					if (methodCore != null)
					{
						methodCore.PrepareEmit();
					}
					else
					{
						Const @const = memberCore as Const;
						if (@const != null)
						{
							@const.DefineValue();
						}
					}
				}
			}
			base.PrepareEmit();
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003C288 File Offset: 0x0003A488
		public override bool CreateContainer()
		{
			if (this.TypeBuilder != null)
			{
				return !this.error;
			}
			if (this.error)
			{
				return false;
			}
			if (this.IsPartialPart)
			{
				this.spec = base.PartialContainer.spec;
				this.TypeBuilder = base.PartialContainer.TypeBuilder;
				this.all_tp_builders = base.PartialContainer.all_tp_builders;
				this.all_type_parameters = base.PartialContainer.all_type_parameters;
			}
			else if (!this.CreateTypeBuilder())
			{
				this.error = true;
				return false;
			}
			return base.CreateContainer();
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0003C316 File Offset: 0x0003A516
		protected override void DoDefineContainer()
		{
			this.DefineBaseTypes();
			this.DoResolveTypeParameters();
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003C328 File Offset: 0x0003A528
		public void SetPredefinedSpec(BuiltinTypeSpec spec)
		{
			spec.SetMetaInfo(this.TypeBuilder);
			spec.MemberCache = this.spec.MemberCache;
			spec.DeclaringType = this.spec.DeclaringType;
			this.spec = spec;
			this.current_type = null;
			if (this.class_partial_parts != null)
			{
				foreach (TypeDefinition typeDefinition in this.class_partial_parts)
				{
					typeDefinition.spec = spec;
				}
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003C3C0 File Offset: 0x0003A5C0
		public override void RemoveContainer(TypeContainer cont)
		{
			base.RemoveContainer(cont);
			this.Members.Remove(cont);
			this.Cache.Remove(cont.MemberName.Basename);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0003C3F0 File Offset: 0x0003A5F0
		protected virtual bool DoResolveTypeParameters()
		{
			TypeParameters typeParameters = base.MemberName.TypeParameters;
			if (typeParameters == null)
			{
				return true;
			}
			TypeDefinition.BaseContext baseContext = new TypeDefinition.BaseContext(this);
			for (int i = 0; i < typeParameters.Count; i++)
			{
				TypeParameter typeParameter = typeParameters[i];
				if (!typeParameter.ResolveConstraints(baseContext))
				{
					this.error = true;
					return false;
				}
				if (this.IsPartialPart)
				{
					TypeParameter typeParameter2 = base.PartialContainer.CurrentTypeParameters[i];
					typeParameter.Create(this.spec, this);
					typeParameter.Define(typeParameter2);
					if (typeParameter.OptAttributes != null)
					{
						if (typeParameter2.OptAttributes == null)
						{
							typeParameter2.OptAttributes = typeParameter.OptAttributes;
						}
						else
						{
							typeParameter2.OptAttributes.Attrs.AddRange(typeParameter.OptAttributes.Attrs);
						}
					}
				}
			}
			if (this.IsPartialPart)
			{
				base.PartialContainer.CurrentTypeParameters.UpdateConstraints(this);
			}
			return true;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003C4D4 File Offset: 0x0003A6D4
		private TypeSpec CheckRecursiveDefinition(TypeDefinition tc)
		{
			if (this.InTransit != null)
			{
				return this.spec;
			}
			this.InTransit = tc;
			if (this.base_type != null)
			{
				TypeDefinition typeDefinition = this.base_type.MemberDefinition as TypeDefinition;
				if (typeDefinition != null && typeDefinition.CheckRecursiveDefinition(this) != null)
				{
					return this.base_type;
				}
			}
			if (this.iface_exprs != null)
			{
				foreach (TypeSpec typeSpec in this.iface_exprs)
				{
					if (typeSpec != null)
					{
						Interface @interface = typeSpec.MemberDefinition as Interface;
						if (@interface != null && @interface.CheckRecursiveDefinition(this) != null)
						{
							return typeSpec;
						}
					}
				}
			}
			if (!this.IsTopLevel && this.Parent.PartialContainer.CheckRecursiveDefinition(this) != null)
			{
				return this.spec;
			}
			this.InTransit = null;
			return null;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003C58E File Offset: 0x0003A78E
		public sealed override bool Define()
		{
			if (this.members_defined)
			{
				return this.members_defined_ok;
			}
			this.members_defined_ok = this.DoDefineMembers();
			this.members_defined = true;
			base.Define();
			return this.members_defined_ok;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0003C5C0 File Offset: 0x0003A7C0
		protected virtual bool DoDefineMembers()
		{
			if (this.iface_exprs != null)
			{
				foreach (TypeSpec typeSpec in this.iface_exprs)
				{
					if (typeSpec != null)
					{
						Interface @interface = typeSpec.MemberDefinition as Interface;
						if (@interface != null)
						{
							@interface.Define();
						}
						ObsoleteAttribute attributeObsolete = typeSpec.GetAttributeObsolete();
						if (attributeObsolete != null && !base.IsObsolete)
						{
							AttributeTester.Report_ObsoleteMessage(attributeObsolete, typeSpec.GetSignatureForError(), base.Location, base.Report);
						}
						if (typeSpec.Arity > 0)
						{
							VarianceDecl.CheckTypeVariance(typeSpec, Variance.Covariant, this);
							if (((InflatedTypeSpec)typeSpec).HasDynamicArgument() && !base.IsCompilerGenerated)
							{
								base.Report.Error(1966, base.Location, "`{0}': cannot implement a dynamic interface `{1}'", this.GetSignatureForError(), typeSpec.GetSignatureForError());
								return false;
							}
						}
						if (typeSpec.IsGenericOrParentIsGeneric)
						{
							foreach (TypeSpec typeSpec2 in this.iface_exprs)
							{
								if (typeSpec2 == typeSpec || typeSpec2 == null)
								{
									break;
								}
								if (TypeSpecComparer.Unify.IsEqual(typeSpec, typeSpec2))
								{
									base.Report.Error(695, base.Location, "`{0}' cannot implement both `{1}' and `{2}' because they may unify for some type parameter substitutions", new string[]
									{
										this.GetSignatureForError(),
										typeSpec2.GetSignatureForError(),
										typeSpec.GetSignatureForError()
									});
								}
							}
						}
					}
				}
				if (this.Kind == MemberKind.Interface)
				{
					foreach (TypeSpec iface in this.spec.Interfaces)
					{
						this.MemberCache.AddInterface(iface);
					}
				}
			}
			if (this.base_type != null)
			{
				if (this.base_type_expr != null)
				{
					ObsoleteAttribute attributeObsolete2 = this.base_type.GetAttributeObsolete();
					if (attributeObsolete2 != null && !base.IsObsolete)
					{
						AttributeTester.Report_ObsoleteMessage(attributeObsolete2, this.base_type.GetSignatureForError(), this.base_type_expr.Location, base.Report);
					}
					if (this.IsGenericOrParentIsGeneric && this.base_type.IsAttribute)
					{
						base.Report.Error(698, this.base_type_expr.Location, "A generic type cannot derive from `{0}' because it is an attribute class", this.base_type.GetSignatureForError());
					}
				}
				ClassOrStruct classOrStruct = this.base_type.MemberDefinition as ClassOrStruct;
				if (classOrStruct != null)
				{
					classOrStruct.Define();
					if (this.HasMembersDefined)
					{
						return true;
					}
				}
			}
			if (this.Kind == MemberKind.Struct || this.Kind == MemberKind.Class)
			{
				this.pending = PendingImplementation.GetPendingImplementations(this);
			}
			int count = this.members.Count;
			for (int k = 0; k < count; k++)
			{
				InterfaceMemberBase interfaceMemberBase = this.members[k] as InterfaceMemberBase;
				if (interfaceMemberBase != null && interfaceMemberBase.IsExplicitImpl)
				{
					try
					{
						interfaceMemberBase.Define();
					}
					catch (Exception e)
					{
						throw new InternalErrorException(interfaceMemberBase, e);
					}
				}
			}
			for (int l = 0; l < count; l++)
			{
				InterfaceMemberBase interfaceMemberBase2 = this.members[l] as InterfaceMemberBase;
				if ((interfaceMemberBase2 == null || !interfaceMemberBase2.IsExplicitImpl) && !(this.members[l] is TypeContainer))
				{
					try
					{
						this.members[l].Define();
					}
					catch (Exception e2)
					{
						throw new InternalErrorException(this.members[l], e2);
					}
				}
			}
			if (this.HasOperators)
			{
				this.CheckPairedOperators();
			}
			if (this.requires_delayed_unmanagedtype_check)
			{
				this.requires_delayed_unmanagedtype_check = false;
				foreach (MemberCore memberCore in this.members)
				{
					Field field = memberCore as Field;
					if (field != null && field.MemberType != null && field.MemberType.IsPointer)
					{
						TypeManager.VerifyUnmanaged(this.Module, field.MemberType, field.Location);
					}
				}
			}
			this.ComputeIndexerName();
			if (this.HasEquals && !this.HasGetHashCode)
			{
				base.Report.Warning(659, 3, base.Location, "`{0}' overrides Object.Equals(object) but does not override Object.GetHashCode()", this.GetSignatureForError());
			}
			if (this.Kind == MemberKind.Interface && this.iface_exprs != null)
			{
				this.MemberCache.RemoveHiddenMembers(this.spec);
			}
			return true;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003CA1C File Offset: 0x0003AC1C
		private void ComputeIndexerName()
		{
			IList<MemberSpec> list = MemberCache.FindMembers(this.spec, MemberCache.IndexerNameAlias, true);
			if (list == null)
			{
				return;
			}
			string text = null;
			foreach (MemberSpec memberSpec in list)
			{
				if (memberSpec.DeclaringType == this.spec)
				{
					this.has_normal_indexers = true;
					if (text == null)
					{
						text = (this.indexer_name = memberSpec.Name);
					}
					else if (memberSpec.Name != text)
					{
						base.Report.Error(668, ((Indexer)memberSpec.MemberDefinition).Location, "Two indexers have different names; the IndexerName attribute must be used with the same name on every indexer within a type");
					}
				}
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003CAD0 File Offset: 0x0003ACD0
		private void EmitIndexerName()
		{
			if (!this.has_normal_indexers)
			{
				return;
			}
			MethodSpec methodSpec = this.Module.PredefinedMembers.DefaultMemberAttributeCtor.Get();
			if (methodSpec == null)
			{
				return;
			}
			AttributeEncoder attributeEncoder = new AttributeEncoder();
			attributeEncoder.Encode(this.GetAttributeDefaultMember());
			attributeEncoder.EncodeEmptyNamedArguments();
			this.TypeBuilder.SetCustomAttribute((ConstructorInfo)methodSpec.GetMetaInfo(), attributeEncoder.ToArray());
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003CB34 File Offset: 0x0003AD34
		public override void VerifyMembers()
		{
			if (!base.IsCompilerGenerated && this.Compiler.Settings.WarningLevel >= 3 && this == base.PartialContainer)
			{
				bool flag = this.Kind == MemberKind.Struct || base.IsExposedFromAssembly();
				foreach (MemberCore memberCore in this.members)
				{
					if (memberCore is Event)
					{
						if (!memberCore.IsUsed && !base.PartialContainer.HasStructLayout)
						{
							base.Report.Warning(67, 3, memberCore.Location, "The event `{0}' is never used", memberCore.GetSignatureForError());
						}
					}
					else
					{
						if ((memberCore.ModFlags & Modifiers.AccessibilityMask) != Modifiers.PRIVATE)
						{
							if (flag)
							{
								continue;
							}
							memberCore.SetIsUsed();
						}
						Field field = memberCore as Field;
						if (field != null)
						{
							if (!memberCore.IsUsed)
							{
								if (!base.PartialContainer.HasStructLayout)
								{
									if ((memberCore.caching_flags & MemberCore.Flags.IsAssigned) == (MemberCore.Flags)0)
									{
										base.Report.Warning(169, 3, memberCore.Location, "The private field `{0}' is never used", memberCore.GetSignatureForError());
									}
									else
									{
										base.Report.Warning(414, 3, memberCore.Location, "The private field `{0}' is assigned but its value is never used", memberCore.GetSignatureForError());
									}
								}
							}
							else if ((field.caching_flags & MemberCore.Flags.IsAssigned) == (MemberCore.Flags)0 && this.Compiler.Settings.WarningLevel >= 4 && field.OptAttributes == null && !base.PartialContainer.HasStructLayout)
							{
								Constant constant = New.Constantify(field.MemberType, field.Location);
								string text;
								if (constant != null)
								{
									text = constant.GetValueAsLiteral();
								}
								else if (TypeSpec.IsReferenceType(field.MemberType))
								{
									text = "null";
								}
								else
								{
									text = null;
								}
								if (text != null)
								{
									text = " `" + text + "'";
								}
								base.Report.Warning(649, 4, field.Location, "Field `{0}' is never assigned to, and will always have its default value{1}", field.GetSignatureForError(), text);
							}
						}
					}
				}
			}
			base.VerifyMembers();
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0003CD74 File Offset: 0x0003AF74
		public override void Emit()
		{
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			if (!base.IsCompilerGenerated)
			{
				if (!this.IsTopLevel)
				{
					bool flag = false;
					MemberSpec memberSpec2;
					MemberSpec memberSpec = MemberCache.FindBaseMember(this, out memberSpec2, ref flag);
					if (memberSpec == null && memberSpec2 == null)
					{
						if ((base.ModFlags & Modifiers.NEW) != (Modifiers)0)
						{
							base.Report.Warning(109, 4, base.Location, "The member `{0}' does not hide an inherited member. The new keyword is not required", this.GetSignatureForError());
						}
					}
					else if ((base.ModFlags & Modifiers.NEW) == (Modifiers)0)
					{
						if (memberSpec2 == null)
						{
							memberSpec2 = memberSpec;
						}
						base.Report.SymbolRelatedToPreviousError(memberSpec2);
						base.Report.Warning(108, 2, base.Location, "`{0}' hides inherited member `{1}'. Use the new keyword if hiding was intended", this.GetSignatureForError(), memberSpec2.GetSignatureForError());
					}
				}
				if (this.base_type != null && this.base_type_expr != null)
				{
					ConstraintChecker.Check(this, this.base_type, this.base_type_expr.Location);
				}
				if (this.iface_exprs != null)
				{
					foreach (TypeSpec typeSpec in this.iface_exprs)
					{
						if (typeSpec != null)
						{
							ConstraintChecker.Check(this, typeSpec, base.Location);
						}
					}
				}
			}
			if (this.all_tp_builders != null)
			{
				int currentTypeParametersStartIndex = this.CurrentTypeParametersStartIndex;
				for (int j = 0; j < this.all_tp_builders.Length; j++)
				{
					if (j < currentTypeParametersStartIndex)
					{
						this.all_type_parameters[j].EmitConstraints(this.all_tp_builders[j]);
					}
					else
					{
						TypeParameter typeParameter = this.CurrentTypeParameters[j - currentTypeParametersStartIndex];
						typeParameter.CheckGenericConstraints(!base.IsObsolete);
						typeParameter.Emit();
					}
				}
			}
			if ((base.ModFlags & Modifiers.COMPILER_GENERATED) != (Modifiers)0 && !this.Parent.IsCompilerGenerated)
			{
				this.Module.PredefinedAttributes.CompilerGenerated.EmitAttribute(this.TypeBuilder);
			}
			base.Emit();
			for (int k = 0; k < this.members.Count; k++)
			{
				MemberCore memberCore = this.members[k];
				if ((memberCore.caching_flags & MemberCore.Flags.CloseTypeCreated) == (MemberCore.Flags)0)
				{
					memberCore.Emit();
				}
			}
			this.EmitIndexerName();
			this.CheckAttributeClsCompliance();
			if (this.pending != null)
			{
				this.pending.VerifyPendingMethods();
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0003CF94 File Offset: 0x0003B194
		private void CheckAttributeClsCompliance()
		{
			if (!this.spec.IsAttribute || !base.IsExposedFromAssembly() || !this.Compiler.Settings.VerifyClsCompliance || !this.IsClsComplianceRequired())
			{
				return;
			}
			foreach (MemberCore memberCore in this.members)
			{
				Constructor constructor = memberCore as Constructor;
				if (constructor != null && constructor.HasCompliantArgs)
				{
					return;
				}
			}
			base.Report.Warning(3015, 1, base.Location, "`{0}' has no accessible constructors which use only CLS-compliant types", this.GetSignatureForError());
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0003D044 File Offset: 0x0003B244
		public sealed override void EmitContainer()
		{
			if ((this.caching_flags & MemberCore.Flags.CloseTypeCreated) != (MemberCore.Flags)0)
			{
				return;
			}
			this.Emit();
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0003D058 File Offset: 0x0003B258
		public override void CloseContainer()
		{
			if ((this.caching_flags & MemberCore.Flags.CloseTypeCreated) != (MemberCore.Flags)0)
			{
				return;
			}
			if (this.spec.BaseType != null)
			{
				TypeContainer typeContainer = this.spec.BaseType.MemberDefinition as TypeContainer;
				if (typeContainer != null)
				{
					typeContainer.CloseContainer();
					if ((this.caching_flags & MemberCore.Flags.CloseTypeCreated) != (MemberCore.Flags)0)
					{
						return;
					}
				}
			}
			try
			{
				this.caching_flags |= MemberCore.Flags.CloseTypeCreated;
				this.TypeBuilder.CreateType();
			}
			catch (TypeLoadException)
			{
			}
			catch (Exception e)
			{
				throw new InternalErrorException(this, e);
			}
			base.CloseContainer();
			this.containers = null;
			this.initialized_fields = null;
			this.initialized_static_fields = null;
			this.type_bases = null;
			base.OptAttributes = null;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0003D118 File Offset: 0x0003B318
		public bool MethodModifiersValid(MemberCore mc)
		{
			bool result = true;
			Modifiers modFlags = mc.ModFlags;
			if ((modFlags & Modifiers.STATIC) != (Modifiers)0 && (modFlags & (Modifiers.ABSTRACT | Modifiers.VIRTUAL | Modifiers.OVERRIDE)) != (Modifiers)0)
			{
				base.Report.Error(112, mc.Location, "A static member `{0}' cannot be marked as override, virtual or abstract", mc.GetSignatureForError());
				result = false;
			}
			if ((modFlags & Modifiers.OVERRIDE) != (Modifiers)0 && (modFlags & (Modifiers.NEW | Modifiers.VIRTUAL)) != (Modifiers)0)
			{
				base.Report.Error(113, mc.Location, "A member `{0}' marked as override cannot be marked as new or virtual", mc.GetSignatureForError());
				result = false;
			}
			if ((modFlags & Modifiers.ABSTRACT) != (Modifiers)0)
			{
				if ((modFlags & Modifiers.EXTERN) != (Modifiers)0)
				{
					base.Report.Error(180, mc.Location, "`{0}' cannot be both extern and abstract", mc.GetSignatureForError());
					result = false;
				}
				if ((modFlags & Modifiers.SEALED) != (Modifiers)0)
				{
					base.Report.Error(502, mc.Location, "`{0}' cannot be both abstract and sealed", mc.GetSignatureForError());
					result = false;
				}
				if ((modFlags & Modifiers.VIRTUAL) != (Modifiers)0)
				{
					base.Report.Error(503, mc.Location, "The abstract method `{0}' cannot be marked virtual", mc.GetSignatureForError());
					result = false;
				}
				if ((base.ModFlags & Modifiers.ABSTRACT) == (Modifiers)0)
				{
					base.Report.SymbolRelatedToPreviousError(this);
					base.Report.Error(513, mc.Location, "`{0}' is abstract but it is declared in the non-abstract class `{1}'", mc.GetSignatureForError(), this.GetSignatureForError());
					result = false;
				}
			}
			if ((modFlags & Modifiers.PRIVATE) != (Modifiers)0 && (modFlags & (Modifiers.ABSTRACT | Modifiers.VIRTUAL | Modifiers.OVERRIDE)) != (Modifiers)0)
			{
				base.Report.Error(621, mc.Location, "`{0}': virtual or abstract members cannot be private", mc.GetSignatureForError());
				result = false;
			}
			if ((modFlags & Modifiers.SEALED) != (Modifiers)0 && (modFlags & Modifiers.OVERRIDE) == (Modifiers)0)
			{
				base.Report.Error(238, mc.Location, "`{0}' cannot be sealed because it is not an override", mc.GetSignatureForError());
				result = false;
			}
			return result;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0003D2C0 File Offset: 0x0003B4C0
		protected override bool VerifyClsCompliance()
		{
			if (!base.VerifyClsCompliance())
			{
				return false;
			}
			if (this.Kind != MemberKind.Delegate)
			{
				this.MemberCache.VerifyClsCompliance(this.Definition, base.Report);
			}
			if (this.BaseType != null && !this.BaseType.IsCLSCompliant())
			{
				base.Report.Warning(3009, 1, base.Location, "`{0}': base type `{1}' is not CLS-compliant", this.GetSignatureForError(), this.BaseType.GetSignatureForError());
			}
			return true;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003D340 File Offset: 0x0003B540
		public bool VerifyImplements(InterfaceMemberBase mb)
		{
			TypeSpec[] interfaces = base.PartialContainer.Interfaces;
			if (interfaces != null)
			{
				foreach (TypeSpec typeSpec in interfaces)
				{
					if (typeSpec == mb.InterfaceType)
					{
						return true;
					}
					IList<TypeSpec> interfaces2 = typeSpec.Interfaces;
					if (interfaces2 != null)
					{
						using (IEnumerator<TypeSpec> enumerator = interfaces2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current == mb.InterfaceType)
								{
									return true;
								}
							}
						}
					}
				}
			}
			base.Report.SymbolRelatedToPreviousError(mb.InterfaceType);
			base.Report.Error(540, mb.Location, "`{0}': containing type does not implement interface `{1}'", mb.GetSignatureForError(), mb.InterfaceType.GetSignatureForError());
			return false;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003D410 File Offset: 0x0003B610
		public bool IsBaseTypeDefinition(TypeSpec baseType)
		{
			if (this.TypeBuilder == null)
			{
				return false;
			}
			TypeSpec baseType2 = this.spec;
			while (baseType2.MemberDefinition != baseType.MemberDefinition)
			{
				baseType2 = baseType2.BaseType;
				if (baseType2 == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003D449 File Offset: 0x0003B649
		public override bool IsClsComplianceRequired()
		{
			if (this.IsPartialPart)
			{
				return base.PartialContainer.IsClsComplianceRequired();
			}
			return base.IsClsComplianceRequired();
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003D465 File Offset: 0x0003B665
		bool ITypeDefinition.IsInternalAsPublic(IAssemblyDefinition assembly)
		{
			return this.Module.DeclaringAssembly == assembly;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual bool IsUnmanagedType()
		{
			return false;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003D475 File Offset: 0x0003B675
		public void LoadMembers(TypeSpec declaringType, bool onlyTypes, ref MemberCache cache)
		{
			throw new NotSupportedException("Not supported for compiled definition " + this.GetSignatureForError());
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0003D48C File Offset: 0x0003B68C
		public override FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
		{
			FullNamedExpression fullNamedExpression;
			if (arity == 0 && this.Cache.TryGetValue(name, out fullNamedExpression) && mode != LookupMode.IgnoreAccessibility)
			{
				return fullNamedExpression;
			}
			fullNamedExpression = null;
			if (arity == 0)
			{
				TypeParameters currentTypeParameters = this.CurrentTypeParameters;
				if (currentTypeParameters != null)
				{
					TypeParameter typeParameter = currentTypeParameters.Find(name);
					if (typeParameter != null)
					{
						fullNamedExpression = new TypeParameterExpr(typeParameter, Location.Null);
					}
				}
			}
			if (fullNamedExpression == null)
			{
				TypeSpec typeSpec = this.LookupNestedTypeInHierarchy(name, arity);
				if (typeSpec != null && (typeSpec.IsAccessible(this) || mode == LookupMode.IgnoreAccessibility))
				{
					fullNamedExpression = new TypeExpression(typeSpec, Location.Null);
				}
				else
				{
					int errors = this.Compiler.Report.Errors;
					fullNamedExpression = this.Parent.LookupNamespaceOrType(name, arity, mode, loc);
					if (errors != this.Compiler.Report.Errors)
					{
						return fullNamedExpression;
					}
				}
			}
			if (arity == 0 && mode == LookupMode.Normal)
			{
				this.Cache[name] = fullNamedExpression;
			}
			return fullNamedExpression;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003D54B File Offset: 0x0003B74B
		private TypeSpec LookupNestedTypeInHierarchy(string name, int arity)
		{
			return MemberCache.FindNestedType(base.PartialContainer.CurrentType, name, arity);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003D55F File Offset: 0x0003B75F
		public void Mark_HasEquals()
		{
			this.cached_method |= TypeDefinition.CachedMethods.Equals;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003D56F File Offset: 0x0003B76F
		public void Mark_HasGetHashCode()
		{
			this.cached_method |= TypeDefinition.CachedMethods.GetHashCode;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003D580 File Offset: 0x0003B780
		public override void WriteDebugSymbol(MonoSymbolFile file)
		{
			if (this.IsPartialPart)
			{
				return;
			}
			foreach (MemberCore memberCore in this.members)
			{
				memberCore.WriteDebugSymbol(file);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0003D5DC File Offset: 0x0003B7DC
		public bool HasEquals
		{
			get
			{
				return (this.cached_method & TypeDefinition.CachedMethods.Equals) > (TypeDefinition.CachedMethods)0;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0003D5E9 File Offset: 0x0003B7E9
		public bool HasGetHashCode
		{
			get
			{
				return (this.cached_method & TypeDefinition.CachedMethods.GetHashCode) > (TypeDefinition.CachedMethods)0;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0003D5F6 File Offset: 0x0003B7F6
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0003D603 File Offset: 0x0003B803
		public bool HasStaticFieldInitializer
		{
			get
			{
				return (this.cached_method & TypeDefinition.CachedMethods.HasStaticFieldInitializer) > (TypeDefinition.CachedMethods)0;
			}
			set
			{
				if (value)
				{
					this.cached_method |= TypeDefinition.CachedMethods.HasStaticFieldInitializer;
					return;
				}
				this.cached_method &= ~TypeDefinition.CachedMethods.HasStaticFieldInitializer;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0003D626 File Offset: 0x0003B826
		public override string DocCommentHeader
		{
			get
			{
				return "T:";
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003D62D File Offset: 0x0003B82D
		// Note: this type is marked as 'beforefieldinit'.
		static TypeDefinition()
		{
		}

		// Token: 0x040006D4 RID: 1748
		private readonly List<MemberCore> members;

		// Token: 0x040006D5 RID: 1749
		protected List<FieldInitializer> initialized_fields;

		// Token: 0x040006D6 RID: 1750
		protected List<FieldInitializer> initialized_static_fields;

		// Token: 0x040006D7 RID: 1751
		private Dictionary<MethodSpec, Method> hoisted_base_call_proxies;

		// Token: 0x040006D8 RID: 1752
		private Dictionary<string, FullNamedExpression> Cache = new Dictionary<string, FullNamedExpression>();

		// Token: 0x040006D9 RID: 1753
		protected FieldBase first_nonstatic_field;

		// Token: 0x040006DA RID: 1754
		protected TypeSpec base_type;

		// Token: 0x040006DB RID: 1755
		private FullNamedExpression base_type_expr;

		// Token: 0x040006DC RID: 1756
		protected TypeSpec[] iface_exprs;

		// Token: 0x040006DD RID: 1757
		protected List<FullNamedExpression> type_bases;

		// Token: 0x040006DE RID: 1758
		private List<TypeDefinition> class_partial_parts;

		// Token: 0x040006DF RID: 1759
		private TypeDefinition InTransit;

		// Token: 0x040006E0 RID: 1760
		public TypeBuilder TypeBuilder;

		// Token: 0x040006E1 RID: 1761
		private GenericTypeParameterBuilder[] all_tp_builders;

		// Token: 0x040006E2 RID: 1762
		private TypeParameters all_type_parameters;

		// Token: 0x040006E3 RID: 1763
		public const string DefaultIndexerName = "Item";

		// Token: 0x040006E4 RID: 1764
		private bool has_normal_indexers;

		// Token: 0x040006E5 RID: 1765
		private string indexer_name;

		// Token: 0x040006E6 RID: 1766
		protected bool requires_delayed_unmanagedtype_check;

		// Token: 0x040006E7 RID: 1767
		private bool error;

		// Token: 0x040006E8 RID: 1768
		private bool members_defined;

		// Token: 0x040006E9 RID: 1769
		private bool members_defined_ok;

		// Token: 0x040006EA RID: 1770
		protected bool has_static_constructor;

		// Token: 0x040006EB RID: 1771
		private TypeDefinition.CachedMethods cached_method;

		// Token: 0x040006EC RID: 1772
		protected TypeSpec spec;

		// Token: 0x040006ED RID: 1773
		private TypeSpec current_type;

		// Token: 0x040006EE RID: 1774
		public int DynamicSitesCounter;

		// Token: 0x040006EF RID: 1775
		public int AnonymousMethodsCounter;

		// Token: 0x040006F0 RID: 1776
		public int MethodGroupsCounter;

		// Token: 0x040006F1 RID: 1777
		private static readonly string[] attribute_targets = new string[]
		{
			"type"
		};

		// Token: 0x040006F2 RID: 1778
		private static readonly string[] attribute_targets_primary = new string[]
		{
			"type",
			"method"
		};

		// Token: 0x040006F3 RID: 1779
		private PendingImplementation pending;

		// Token: 0x040006F4 RID: 1780
		[CompilerGenerated]
		private ParametersCompiled <PrimaryConstructorParameters>k__BackingField;

		// Token: 0x040006F5 RID: 1781
		[CompilerGenerated]
		private Arguments <PrimaryConstructorBaseArguments>k__BackingField;

		// Token: 0x040006F6 RID: 1782
		[CompilerGenerated]
		private Location <PrimaryConstructorBaseArgumentsStart>k__BackingField;

		// Token: 0x02000385 RID: 901
		public struct BaseContext : IMemberContext, IModuleContext
		{
			// Token: 0x060026A7 RID: 9895 RVA: 0x000B6D86 File Offset: 0x000B4F86
			public BaseContext(TypeContainer tc)
			{
				this.tc = tc;
			}

			// Token: 0x170008D4 RID: 2260
			// (get) Token: 0x060026A8 RID: 9896 RVA: 0x000B6D8F File Offset: 0x000B4F8F
			public CompilerContext Compiler
			{
				get
				{
					return this.tc.Compiler;
				}
			}

			// Token: 0x170008D5 RID: 2261
			// (get) Token: 0x060026A9 RID: 9897 RVA: 0x000B6D9C File Offset: 0x000B4F9C
			public TypeSpec CurrentType
			{
				get
				{
					return this.tc.PartialContainer.CurrentType;
				}
			}

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x060026AA RID: 9898 RVA: 0x000B6DAE File Offset: 0x000B4FAE
			public TypeParameters CurrentTypeParameters
			{
				get
				{
					return this.tc.PartialContainer.CurrentTypeParameters;
				}
			}

			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x060026AB RID: 9899 RVA: 0x000B6DC0 File Offset: 0x000B4FC0
			public MemberCore CurrentMemberDefinition
			{
				get
				{
					return this.tc;
				}
			}

			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x060026AC RID: 9900 RVA: 0x000B6DC8 File Offset: 0x000B4FC8
			public bool IsObsolete
			{
				get
				{
					return this.tc.IsObsolete;
				}
			}

			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x060026AD RID: 9901 RVA: 0x000B6DD5 File Offset: 0x000B4FD5
			public bool IsUnsafe
			{
				get
				{
					return this.tc.IsUnsafe;
				}
			}

			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x060026AE RID: 9902 RVA: 0x000B6DE2 File Offset: 0x000B4FE2
			public bool IsStatic
			{
				get
				{
					return this.tc.IsStatic;
				}
			}

			// Token: 0x170008DB RID: 2267
			// (get) Token: 0x060026AF RID: 9903 RVA: 0x000B6DEF File Offset: 0x000B4FEF
			public ModuleContainer Module
			{
				get
				{
					return this.tc.Module;
				}
			}

			// Token: 0x060026B0 RID: 9904 RVA: 0x000B6DFC File Offset: 0x000B4FFC
			public string GetSignatureForError()
			{
				return this.tc.GetSignatureForError();
			}

			// Token: 0x060026B1 RID: 9905 RVA: 0x000055E7 File Offset: 0x000037E7
			public ExtensionMethodCandidates LookupExtensionMethod(string name, int arity)
			{
				return null;
			}

			// Token: 0x060026B2 RID: 9906 RVA: 0x000B6E09 File Offset: 0x000B5009
			public FullNamedExpression LookupNamespaceAlias(string name)
			{
				return this.tc.Parent.LookupNamespaceAlias(name);
			}

			// Token: 0x060026B3 RID: 9907 RVA: 0x000B6E1C File Offset: 0x000B501C
			public FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
			{
				if (arity == 0)
				{
					TypeParameters currentTypeParameters = this.CurrentTypeParameters;
					if (currentTypeParameters != null)
					{
						TypeParameter typeParameter = currentTypeParameters.Find(name);
						if (typeParameter != null)
						{
							return new TypeParameterExpr(typeParameter, loc);
						}
					}
				}
				return this.tc.Parent.LookupNamespaceOrType(name, arity, mode, loc);
			}

			// Token: 0x04000F52 RID: 3922
			private TypeContainer tc;
		}

		// Token: 0x02000386 RID: 902
		[Flags]
		private enum CachedMethods
		{
			// Token: 0x04000F54 RID: 3924
			Equals = 1,
			// Token: 0x04000F55 RID: 3925
			GetHashCode = 2,
			// Token: 0x04000F56 RID: 3926
			HasStaticFieldInitializer = 4
		}
	}
}
