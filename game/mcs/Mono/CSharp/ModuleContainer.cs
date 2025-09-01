using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000298 RID: 664
	public sealed class ModuleContainer : TypeContainer
	{
		// Token: 0x06001FF7 RID: 8183 RVA: 0x0009EAB0 File Offset: 0x0009CCB0
		public ModuleContainer.PatternMatchingHelper CreatePatterMatchingHelper()
		{
			if (this.pmh == null)
			{
				this.pmh = new ModuleContainer.PatternMatchingHelper(this);
				this.pmh.CreateContainer();
				this.pmh.DefineContainer();
				this.pmh.Define();
				base.AddCompilerGeneratedClass(this.pmh);
			}
			return this.pmh;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0009EB08 File Offset: 0x0009CD08
		public ModuleContainer(CompilerContext context) : base(null, MemberName.Null, null, (MemberKind)0)
		{
			this.context = context;
			this.caching_flags &= ~(MemberCore.Flags.Obsolete_Undetected | MemberCore.Flags.Excluded_Undetected);
			this.containers = new List<TypeContainer>();
			this.anonymous_types = new Dictionary<int, List<AnonymousTypeClass>>();
			this.global_ns = new GlobalRootNamespace();
			this.alias_ns = new Dictionary<string, RootNamespace>();
			this.array_types = new Dictionary<ArrayContainer.TypeRankPair, ArrayContainer>();
			this.pointer_types = new Dictionary<TypeSpec, PointerContainer>();
			this.reference_types = new Dictionary<TypeSpec, ReferenceContainer>();
			this.attrs_cache = new Dictionary<TypeSpec, MethodSpec>();
			this.awaiters = new Dictionary<TypeSpec, AwaiterDefinition>();
			this.type_info_cache = new Dictionary<TypeSpec, TypeInfo>();
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x0009EBAA File Offset: 0x0009CDAA
		public Dictionary<ArrayContainer.TypeRankPair, ArrayContainer> ArrayTypesCache
		{
			get
			{
				return this.array_types;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x0009EBB2 File Offset: 0x0009CDB2
		public Dictionary<TypeSpec, MethodSpec> AttributeConstructorCache
		{
			get
			{
				return this.attrs_cache;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0000212D File Offset: 0x0000032D
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Assembly;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x0009EBBA File Offset: 0x0009CDBA
		public ModuleBuilder Builder
		{
			get
			{
				return this.builder;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x0009EBC2 File Offset: 0x0009CDC2
		public override CompilerContext Compiler
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x0009EBCA File Offset: 0x0009CDCA
		// (set) Token: 0x06001FFF RID: 8191 RVA: 0x0009EBD2 File Offset: 0x0009CDD2
		public int CounterAnonymousTypes
		{
			[CompilerGenerated]
			get
			{
				return this.<CounterAnonymousTypes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CounterAnonymousTypes>k__BackingField = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x0009EBDB File Offset: 0x0009CDDB
		public AssemblyDefinition DeclaringAssembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0009EBE3 File Offset: 0x0009CDE3
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x0009EBEB File Offset: 0x0009CDEB
		public DocumentationBuilder DocumentationBuilder
		{
			[CompilerGenerated]
			get
			{
				return this.<DocumentationBuilder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DocumentationBuilder>k__BackingField = value;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x0000225C File Offset: 0x0000045C
		public override string DocCommentHeader
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x0009EBF4 File Offset: 0x0009CDF4
		// (set) Token: 0x06002005 RID: 8197 RVA: 0x0009EBFC File Offset: 0x0009CDFC
		public Evaluator Evaluator
		{
			[CompilerGenerated]
			get
			{
				return this.<Evaluator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Evaluator>k__BackingField = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x0009EC05 File Offset: 0x0009CE05
		public bool HasDefaultCharSet
		{
			get
			{
				return this.DefaultCharSet != null;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x0009EC12 File Offset: 0x0009CE12
		// (set) Token: 0x06002008 RID: 8200 RVA: 0x0009EC1A File Offset: 0x0009CE1A
		public bool HasExtensionMethod
		{
			get
			{
				return this.has_extenstion_method;
			}
			set
			{
				this.has_extenstion_method = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x0009EC23 File Offset: 0x0009CE23
		// (set) Token: 0x0600200A RID: 8202 RVA: 0x0009EC2B File Offset: 0x0009CE2B
		public bool HasTypesFullyDefined
		{
			[CompilerGenerated]
			get
			{
				return this.<HasTypesFullyDefined>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HasTypesFullyDefined>k__BackingField = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x0009EC34 File Offset: 0x0009CE34
		public RootNamespace GlobalRootNamespace
		{
			get
			{
				return this.global_ns;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x00005936 File Offset: 0x00003B36
		public override ModuleContainer Module
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x0009EC3C File Offset: 0x0009CE3C
		public Dictionary<TypeSpec, PointerContainer> PointerTypesCache
		{
			get
			{
				return this.pointer_types;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x0009EC44 File Offset: 0x0009CE44
		public PredefinedAttributes PredefinedAttributes
		{
			get
			{
				return this.predefined_attributes;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x0009EC4C File Offset: 0x0009CE4C
		public PredefinedMembers PredefinedMembers
		{
			get
			{
				return this.predefined_members;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x0009EC54 File Offset: 0x0009CE54
		public PredefinedTypes PredefinedTypes
		{
			get
			{
				return this.predefined_types;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x0009EC5C File Offset: 0x0009CE5C
		public Dictionary<TypeSpec, ReferenceContainer> ReferenceTypesCache
		{
			get
			{
				return this.reference_types;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x0009EC64 File Offset: 0x0009CE64
		public Dictionary<TypeSpec, TypeInfo> TypeInfoCache
		{
			get
			{
				return this.type_info_cache;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x0009EC6C File Offset: 0x0009CE6C
		public override string[] ValidAttributeTargets
		{
			get
			{
				return ModuleContainer.attribute_targets;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x0009EC73 File Offset: 0x0009CE73
		// (set) Token: 0x06002015 RID: 8213 RVA: 0x0009EC7B File Offset: 0x0009CE7B
		public Dictionary<string, string> GetResourceStrings
		{
			[CompilerGenerated]
			get
			{
				return this.<GetResourceStrings>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GetResourceStrings>k__BackingField = value;
			}
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0009EC84 File Offset: 0x0009CE84
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0009EC90 File Offset: 0x0009CE90
		public void AddAnonymousType(AnonymousTypeClass type)
		{
			List<AnonymousTypeClass> list;
			if (!this.anonymous_types.TryGetValue(type.Parameters.Count, out list) && list == null)
			{
				list = new List<AnonymousTypeClass>();
				this.anonymous_types.Add(type.Parameters.Count, list);
			}
			list.Add(type);
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0009ECDE File Offset: 0x0009CEDE
		public void AddAttribute(Attribute attr, IMemberContext context)
		{
			attr.AttachTo(this, context);
			if (this.attributes == null)
			{
				this.attributes = new Attributes(attr);
				return;
			}
			this.attributes.AddAttribute(attr);
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00039EBF File Offset: 0x000380BF
		public override void AddTypeContainer(TypeContainer tc)
		{
			this.AddTypeContainerMember(tc);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0009ED0C File Offset: 0x0009CF0C
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Target == AttributeTargets.Assembly)
			{
				this.assembly.ApplyAttributeBuilder(a, ctor, cdata, pa);
				return;
			}
			if (a.Type == pa.DefaultCharset)
			{
				switch (a.GetCharSetValue())
				{
				case CharSet.None:
				case CharSet.Ansi:
					break;
				case CharSet.Unicode:
					this.DefaultCharSet = new CharSet?(CharSet.Unicode);
					this.DefaultCharSetType = TypeAttributes.UnicodeClass;
					break;
				case CharSet.Auto:
					this.DefaultCharSet = new CharSet?(CharSet.Auto);
					this.DefaultCharSetType = TypeAttributes.AutoClass;
					break;
				default:
					base.Report.Error(1724, a.Location, "Value specified for the argument to `{0}' is not valid", a.GetSignatureForError());
					break;
				}
			}
			else if (a.Type == pa.CLSCompliant)
			{
				Attribute clscompliantAttribute = this.DeclaringAssembly.CLSCompliantAttribute;
				if (clscompliantAttribute == null)
				{
					base.Report.Warning(3012, 1, a.Location, "You must specify the CLSCompliant attribute on the assembly, not the module, to enable CLS compliance checking");
				}
				else if (this.DeclaringAssembly.IsCLSCompliant != a.GetBoolean())
				{
					base.Report.SymbolRelatedToPreviousError(clscompliantAttribute.Location, clscompliantAttribute.GetSignatureForError());
					base.Report.Warning(3017, 1, a.Location, "You cannot specify the CLSCompliant attribute on a module that differs from the CLSCompliant attribute on the assembly");
					return;
				}
			}
			this.builder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0009EE68 File Offset: 0x0009D068
		public override void CloseContainer()
		{
			if (this.anonymous_types != null)
			{
				foreach (KeyValuePair<int, List<AnonymousTypeClass>> keyValuePair in this.anonymous_types)
				{
					foreach (AnonymousTypeClass anonymousTypeClass in keyValuePair.Value)
					{
						anonymousTypeClass.CloseContainer();
					}
				}
			}
			base.CloseContainer();
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0009EF04 File Offset: 0x0009D104
		public TypeBuilder CreateBuilder(string name, TypeAttributes attr, int typeSize)
		{
			return this.builder.DefineType(name, attr, null, typeSize);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0009EF18 File Offset: 0x0009D118
		public RootNamespace CreateRootNamespace(string alias)
		{
			if (alias == this.global_ns.Alias)
			{
				RootNamespace.Error_GlobalNamespaceRedefined(base.Report, Location.Null);
				return this.global_ns;
			}
			RootNamespace rootNamespace;
			if (!this.alias_ns.TryGetValue(alias, out rootNamespace))
			{
				rootNamespace = new RootNamespace(alias);
				this.alias_ns.Add(alias, rootNamespace);
			}
			return rootNamespace;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0009EF74 File Offset: 0x0009D174
		public void Create(AssemblyDefinition assembly, ModuleBuilder moduleBuilder)
		{
			this.assembly = assembly;
			this.builder = moduleBuilder;
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0009EF84 File Offset: 0x0009D184
		public override bool Define()
		{
			this.DefineContainer();
			this.ExpandBaseInterfaces();
			base.Define();
			this.HasTypesFullyDefined = true;
			return true;
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0009EFA2 File Offset: 0x0009D1A2
		public override bool DefineContainer()
		{
			this.DefineNamespace();
			return base.DefineContainer();
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0009EFB0 File Offset: 0x0009D1B0
		public void EnableRedefinition()
		{
			this.is_defined = false;
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x0009EFBC File Offset: 0x0009D1BC
		public override void EmitContainer()
		{
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			if (this.Compiler.Settings.Unsafe && !this.assembly.IsSatelliteAssembly)
			{
				PredefinedAttribute unverifiableCode = this.PredefinedAttributes.UnverifiableCode;
				if (unverifiableCode.IsDefined)
				{
					unverifiableCode.EmitAttribute(this.builder);
				}
			}
			foreach (TypeContainer typeContainer in this.containers)
			{
				typeContainer.PrepareEmit();
			}
			base.EmitContainer();
			if (this.Compiler.Report.Errors == 0 && !this.Compiler.Settings.WriteMetadataOnly)
			{
				this.VerifyMembers();
			}
			if (this.anonymous_types != null)
			{
				foreach (KeyValuePair<int, List<AnonymousTypeClass>> keyValuePair in this.anonymous_types)
				{
					foreach (AnonymousTypeClass anonymousTypeClass in keyValuePair.Value)
					{
						anonymousTypeClass.EmitContainer();
					}
				}
			}
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0009F114 File Offset: 0x0009D314
		public override void GenerateDocComment(DocumentationBuilder builder)
		{
			foreach (TypeContainer typeContainer in this.containers)
			{
				typeContainer.GenerateDocComment(builder);
			}
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x0009F168 File Offset: 0x0009D368
		public AnonymousTypeClass GetAnonymousType(IList<AnonymousTypeParameter> parameters)
		{
			List<AnonymousTypeClass> list;
			if (!this.anonymous_types.TryGetValue(parameters.Count, out list))
			{
				return null;
			}
			foreach (AnonymousTypeClass anonymousTypeClass in list)
			{
				int num = 0;
				while (num < parameters.Count && parameters[num].Equals(anonymousTypeClass.Parameters[num]))
				{
					num++;
				}
				if (num == parameters.Count)
				{
					return anonymousTypeClass;
				}
			}
			return null;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0009F204 File Offset: 0x0009D404
		public AwaiterDefinition GetAwaiter(TypeSpec type)
		{
			AwaiterDefinition awaiterDefinition;
			if (this.awaiters.TryGetValue(type, out awaiterDefinition))
			{
				return awaiterDefinition;
			}
			awaiterDefinition = new AwaiterDefinition();
			awaiterDefinition.IsCompleted = (MemberCache.FindMember(type, MemberFilter.Property("IsCompleted", this.Compiler.BuiltinTypes.Bool), BindingRestriction.InstanceOnly) as PropertySpec);
			awaiterDefinition.GetResult = (MemberCache.FindMember(type, MemberFilter.Method("GetResult", 0, ParametersCompiled.EmptyReadOnlyParameters, null), BindingRestriction.InstanceOnly) as MethodSpec);
			PredefinedType inotifyCompletion = this.PredefinedTypes.INotifyCompletion;
			awaiterDefinition.INotifyCompletion = (!inotifyCompletion.Define() || type.ImplementsInterface(inotifyCompletion.TypeSpec, false));
			this.awaiters.Add(type, awaiterDefinition);
			return awaiterDefinition;
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x0009F2B0 File Offset: 0x0009D4B0
		public override void GetCompletionStartingWith(string prefix, List<string> results)
		{
			string[] varNames = this.Evaluator.GetVarNames();
			results.AddRange(from l in varNames
			where l.StartsWith(prefix)
			select l);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0009F2F0 File Offset: 0x0009D4F0
		public RootNamespace GetRootNamespace(string name)
		{
			RootNamespace result;
			this.alias_ns.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x0009F30D File Offset: 0x0009D50D
		public override string GetSignatureForError()
		{
			return "<module>";
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x0009F314 File Offset: 0x0009D514
		public Binary.PredefinedOperator[] GetPredefinedEnumAritmeticOperators(TypeSpec enumType, bool nullable)
		{
			Binary.Operator @operator = (Binary.Operator)0;
			TypeSpec typeSpec;
			if (nullable)
			{
				typeSpec = NullableInfo.GetEnumUnderlyingType(this, enumType);
				@operator = Binary.Operator.NullableMask;
			}
			else
			{
				typeSpec = EnumSpec.GetUnderlyingType(enumType);
			}
			return new Binary.PredefinedOperator[]
			{
				new Binary.PredefinedOperator(enumType, typeSpec, @operator | Binary.Operator.AdditionMask | Binary.Operator.SubtractionMask | Binary.Operator.DecomposedMask, enumType),
				new Binary.PredefinedOperator(typeSpec, enumType, @operator | Binary.Operator.AdditionMask | Binary.Operator.SubtractionMask | Binary.Operator.DecomposedMask, enumType),
				new Binary.PredefinedOperator(enumType, @operator | Binary.Operator.SubtractionMask, typeSpec)
			};
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x0009F390 File Offset: 0x0009D590
		public void InitializePredefinedTypes()
		{
			this.predefined_attributes = new PredefinedAttributes(this);
			this.predefined_types = new PredefinedTypes(this);
			this.predefined_members = new PredefinedMembers(this);
			this.OperatorsBinaryEqualityLifted = Binary.CreateEqualityLiftedOperatorsTable(this);
			this.OperatorsBinaryLifted = Binary.CreateStandardLiftedOperatorsTable(this);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x0009F3CE File Offset: 0x0009D5CE
		public override bool IsClsComplianceRequired()
		{
			return this.DeclaringAssembly.IsCLSCompliant;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x0009F3DC File Offset: 0x0009D5DC
		public Attribute ResolveAssemblyAttribute(PredefinedAttribute a_type)
		{
			Attribute attribute = base.OptAttributes.Search("assembly", a_type);
			if (attribute != null)
			{
				attribute.Resolve();
			}
			return attribute;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0009F406 File Offset: 0x0009D606
		public void SetDeclaringAssembly(AssemblyDefinition assembly)
		{
			this.assembly = assembly;
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0009F410 File Offset: 0x0009D610
		public void LoadGetResourceStrings(List<string> fileNames)
		{
			foreach (string text in fileNames)
			{
				if (!File.Exists(text))
				{
					base.Report.Error(1566, "Error reading resource file `{0}'", text);
					break;
				}
				foreach (string text2 in File.ReadAllLines(text))
				{
					if (this.GetResourceStrings == null)
					{
						this.GetResourceStrings = new Dictionary<string, string>();
					}
					string text3 = text2.Trim();
					if (text3.Length != 0 && text3[0] != '#' && text3[0] != ';')
					{
						int num = text3.IndexOf('=');
						if (num >= 0)
						{
							string key = text3.Substring(0, num).Trim();
							string value = text3.Substring(num + 1).Trim();
							this.GetResourceStrings[key] = value;
						}
					}
				}
			}
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x0009F51C File Offset: 0x0009D71C
		// Note: this type is marked as 'beforefieldinit'.
		static ModuleContainer()
		{
		}

		// Token: 0x04000BF3 RID: 3059
		private ModuleContainer.PatternMatchingHelper pmh;

		// Token: 0x04000BF4 RID: 3060
		public CharSet? DefaultCharSet;

		// Token: 0x04000BF5 RID: 3061
		public TypeAttributes DefaultCharSetType;

		// Token: 0x04000BF6 RID: 3062
		private readonly Dictionary<int, List<AnonymousTypeClass>> anonymous_types;

		// Token: 0x04000BF7 RID: 3063
		private readonly Dictionary<ArrayContainer.TypeRankPair, ArrayContainer> array_types;

		// Token: 0x04000BF8 RID: 3064
		private readonly Dictionary<TypeSpec, PointerContainer> pointer_types;

		// Token: 0x04000BF9 RID: 3065
		private readonly Dictionary<TypeSpec, ReferenceContainer> reference_types;

		// Token: 0x04000BFA RID: 3066
		private readonly Dictionary<TypeSpec, MethodSpec> attrs_cache;

		// Token: 0x04000BFB RID: 3067
		private readonly Dictionary<TypeSpec, AwaiterDefinition> awaiters;

		// Token: 0x04000BFC RID: 3068
		private readonly Dictionary<TypeSpec, TypeInfo> type_info_cache;

		// Token: 0x04000BFD RID: 3069
		private AssemblyDefinition assembly;

		// Token: 0x04000BFE RID: 3070
		private readonly CompilerContext context;

		// Token: 0x04000BFF RID: 3071
		private readonly RootNamespace global_ns;

		// Token: 0x04000C00 RID: 3072
		private readonly Dictionary<string, RootNamespace> alias_ns;

		// Token: 0x04000C01 RID: 3073
		private ModuleBuilder builder;

		// Token: 0x04000C02 RID: 3074
		private bool has_extenstion_method;

		// Token: 0x04000C03 RID: 3075
		private PredefinedAttributes predefined_attributes;

		// Token: 0x04000C04 RID: 3076
		private PredefinedTypes predefined_types;

		// Token: 0x04000C05 RID: 3077
		private PredefinedMembers predefined_members;

		// Token: 0x04000C06 RID: 3078
		public Binary.PredefinedOperator[] OperatorsBinaryEqualityLifted;

		// Token: 0x04000C07 RID: 3079
		public Binary.PredefinedOperator[] OperatorsBinaryLifted;

		// Token: 0x04000C08 RID: 3080
		private static readonly string[] attribute_targets = new string[]
		{
			"assembly",
			"module"
		};

		// Token: 0x04000C09 RID: 3081
		[CompilerGenerated]
		private int <CounterAnonymousTypes>k__BackingField;

		// Token: 0x04000C0A RID: 3082
		[CompilerGenerated]
		private DocumentationBuilder <DocumentationBuilder>k__BackingField;

		// Token: 0x04000C0B RID: 3083
		[CompilerGenerated]
		private Evaluator <Evaluator>k__BackingField;

		// Token: 0x04000C0C RID: 3084
		[CompilerGenerated]
		private bool <HasTypesFullyDefined>k__BackingField;

		// Token: 0x04000C0D RID: 3085
		[CompilerGenerated]
		private Dictionary<string, string> <GetResourceStrings>k__BackingField;

		// Token: 0x020003EE RID: 1006
		public sealed class PatternMatchingHelper : CompilerGeneratedContainer
		{
			// Token: 0x060027D5 RID: 10197 RVA: 0x000BD144 File Offset: 0x000BB344
			public PatternMatchingHelper(ModuleContainer module) : base(module, new MemberName("<PatternMatchingHelper>", Location.Null), Modifiers.INTERNAL | Modifiers.STATIC | Modifiers.DEBUGGER_HIDDEN)
			{
			}

			// Token: 0x17000900 RID: 2304
			// (get) Token: 0x060027D6 RID: 10198 RVA: 0x000BD161 File Offset: 0x000BB361
			// (set) Token: 0x060027D7 RID: 10199 RVA: 0x000BD169 File Offset: 0x000BB369
			public Method NumberMatcher
			{
				[CompilerGenerated]
				get
				{
					return this.<NumberMatcher>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<NumberMatcher>k__BackingField = value;
				}
			}

			// Token: 0x060027D8 RID: 10200 RVA: 0x000BD172 File Offset: 0x000BB372
			protected override bool DoDefineMembers()
			{
				if (!base.DoDefineMembers())
				{
					return false;
				}
				this.NumberMatcher = this.GenerateNumberMatcher();
				return true;
			}

			// Token: 0x060027D9 RID: 10201 RVA: 0x000BD18C File Offset: 0x000BB38C
			private Method GenerateNumberMatcher()
			{
				Location location = base.Location;
				ParametersCompiled parametersCompiled = ParametersCompiled.CreateFullyResolved(new Parameter[]
				{
					new Parameter(new TypeExpression(this.Compiler.BuiltinTypes.Object, location), "obj", Parameter.Modifier.NONE, null, location),
					new Parameter(new TypeExpression(this.Compiler.BuiltinTypes.Object, location), "value", Parameter.Modifier.NONE, null, location),
					new Parameter(new TypeExpression(this.Compiler.BuiltinTypes.Bool, location), "enumType", Parameter.Modifier.NONE, null, location)
				}, new BuiltinTypeSpec[]
				{
					this.Compiler.BuiltinTypes.Object,
					this.Compiler.BuiltinTypes.Object,
					this.Compiler.BuiltinTypes.Bool
				});
				Method method = new Method(this, new TypeExpression(this.Compiler.BuiltinTypes.Bool, location), Modifiers.PUBLIC | Modifiers.STATIC | Modifiers.DEBUGGER_HIDDEN, new MemberName("NumberMatcher", location), parametersCompiled, null);
				parametersCompiled[0].Resolve(method, 0);
				parametersCompiled[1].Resolve(method, 1);
				parametersCompiled[2].Resolve(method, 2);
				ToplevelBlock toplevelBlock = new ToplevelBlock(this.Compiler, parametersCompiled, location, (Block.Flags)0);
				method.Block = toplevelBlock;
				Arguments arguments = new Arguments(2);
				arguments.Add(new Argument(toplevelBlock.GetParameterReference(0, location)));
				arguments.Add(new Argument(toplevelBlock.GetParameterReference(1, location)));
				If s = new If(toplevelBlock.GetParameterReference(2, location), new Return(new Invocation(new SimpleName("Equals", location), arguments), location), location);
				toplevelBlock.AddStatement(s);
				If s2 = new If(new Binary(Binary.Operator.LogicalOr, new Is(toplevelBlock.GetParameterReference(0, location), new TypeExpression(this.Compiler.BuiltinTypes.Enum, location), location), new Binary(Binary.Operator.Equality, toplevelBlock.GetParameterReference(0, location), new NullLiteral(location))), new Return(new BoolLiteral(this.Compiler.BuiltinTypes, false, location), location), location);
				toplevelBlock.AddStatement(s2);
				MemberAccess expr = new MemberAccess(new QualifiedAliasMember("global", "System", location), "Convert", location);
				Block parent = toplevelBlock;
				Location location2 = location;
				ExplicitBlock explicitBlock = new ExplicitBlock(parent, location2, location2);
				LocalVariable li = LocalVariable.CreateCompilerGenerated(this.Compiler.BuiltinTypes.Object, toplevelBlock, location);
				Arguments arguments2 = new Arguments(1);
				arguments2.Add(new Argument(toplevelBlock.GetParameterReference(1, location)));
				Invocation expr2 = new Invocation(new MemberAccess(expr, "GetTypeCode", location), arguments2);
				Arguments arguments3 = new Arguments(1);
				arguments3.Add(new Argument(toplevelBlock.GetParameterReference(0, location)));
				arguments3.Add(new Argument(expr2));
				Invocation source = new Invocation(new MemberAccess(expr, "ChangeType", location), arguments3);
				explicitBlock.AddStatement(new StatementExpression(new SimpleAssign(new LocalVariableReference(li, location), source, location)));
				Arguments arguments4 = new Arguments(1);
				arguments4.Add(new Argument(toplevelBlock.GetParameterReference(1, location)));
				Invocation expr3 = new Invocation(new MemberAccess(new LocalVariableReference(li, location), "Equals"), arguments4);
				explicitBlock.AddStatement(new Return(expr3, location));
				Block parent2 = toplevelBlock;
				Location location3 = location;
				ExplicitBlock explicitBlock2 = new ExplicitBlock(parent2, location3, location3);
				explicitBlock2.AddStatement(new Return(new BoolLiteral(this.Compiler.BuiltinTypes, false, location), location));
				toplevelBlock.AddStatement(new TryCatch(explicitBlock, new List<Catch>
				{
					new Catch(explicitBlock2, location)
				}, location, false));
				method.Define();
				method.PrepareEmit();
				base.AddMember(method);
				return method;
			}

			// Token: 0x0400112A RID: 4394
			[CompilerGenerated]
			private Method <NumberMatcher>k__BackingField;
		}

		// Token: 0x020003EF RID: 1007
		[CompilerGenerated]
		private sealed class <>c__DisplayClass98_0
		{
			// Token: 0x060027DA RID: 10202 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c__DisplayClass98_0()
			{
			}

			// Token: 0x060027DB RID: 10203 RVA: 0x000BD50C File Offset: 0x000BB70C
			internal bool <GetCompletionStartingWith>b__0(string l)
			{
				return l.StartsWith(this.prefix);
			}

			// Token: 0x0400112B RID: 4395
			public string prefix;
		}
	}
}
