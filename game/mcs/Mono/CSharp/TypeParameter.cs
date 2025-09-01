using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x0200021F RID: 543
	public class TypeParameter : MemberCore, ITypeDefinition, IMemberDefinition
	{
		// Token: 0x06001B77 RID: 7031 RVA: 0x00085654 File Offset: 0x00083854
		public TypeParameter(int index, MemberName name, Constraints constraints, Attributes attrs, Variance Variance) : base(null, name, attrs)
		{
			this.constraints = constraints;
			this.spec = new TypeParameterSpec(null, index, this, SpecialConstraint.None, Variance, null);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0008567C File Offset: 0x0008387C
		public TypeParameter(MemberName name, Attributes attrs, VarianceDecl variance) : base(null, name, attrs)
		{
			Variance variance2 = (variance == null) ? Variance.None : variance.Variance;
			this.spec = new TypeParameterSpec(null, -1, this, SpecialConstraint.None, variance2, null);
			this.VarianceDecl = variance;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000856B8 File Offset: 0x000838B8
		public TypeParameter(TypeParameterSpec spec, TypeSpec parentSpec, MemberName name, Attributes attrs) : base(null, name, attrs)
		{
			this.spec = new TypeParameterSpec(parentSpec, spec.DeclaredPosition, spec.MemberDefinition, spec.SpecialConstraint, spec.Variance, null)
			{
				BaseType = spec.BaseType,
				InterfacesDefined = spec.InterfacesDefined,
				TypeArguments = spec.TypeArguments
			};
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x00085718 File Offset: 0x00083918
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.GenericParameter;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0008571F File Offset: 0x0008391F
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x00085727 File Offset: 0x00083927
		public Constraints Constraints
		{
			get
			{
				return this.constraints;
			}
			set
			{
				this.constraints = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0003A85D File Offset: 0x00038A5D
		public IAssemblyDefinition DeclaringAssembly
		{
			get
			{
				return this.Module.DeclaringAssembly;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00085730 File Offset: 0x00083930
		public override string DocCommentHeader
		{
			get
			{
				throw new InvalidOperationException("Unexpected attempt to get doc comment from " + base.GetType());
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsComImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsPartial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x00085747 File Offset: 0x00083947
		public bool IsMethodTypeParameter
		{
			get
			{
				return this.spec.IsMethodOwned;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x000022F4 File Offset: 0x000004F4
		bool ITypeDefinition.IsCyclicTypeForwarder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0003F215 File Offset: 0x0003D415
		public string Name
		{
			get
			{
				return base.MemberName.Name;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x000055E7 File Offset: 0x000037E7
		public string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00085754 File Offset: 0x00083954
		public TypeParameterSpec Type
		{
			get
			{
				return this.spec;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x000022F4 File Offset: 0x000004F4
		public int TypeParametersCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x000055E7 File Offset: 0x000037E7
		public TypeParameterSpec[] TypeParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0008575C File Offset: 0x0008395C
		public override string[] ValidAttributeTargets
		{
			get
			{
				return TypeParameter.attribute_target;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x00085763 File Offset: 0x00083963
		public Variance Variance
		{
			get
			{
				return this.spec.Variance;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00085770 File Offset: 0x00083970
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x00085778 File Offset: 0x00083978
		public VarianceDecl VarianceDecl
		{
			[CompilerGenerated]
			get
			{
				return this.<VarianceDecl>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<VarianceDecl>k__BackingField = value;
			}
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00085784 File Offset: 0x00083984
		public bool AddPartialConstraints(TypeDefinition part, TypeParameter tp)
		{
			if (this.builder == null)
			{
				throw new InvalidOperationException();
			}
			if (tp.constraints == null)
			{
				return true;
			}
			tp.spec.DeclaringType = part.Definition;
			if (!tp.ResolveConstraints(part))
			{
				return false;
			}
			if (this.constraints != null)
			{
				return this.spec.HasSameConstraintsDefinition(tp.Type);
			}
			this.spec.SpecialConstraint = tp.spec.SpecialConstraint;
			this.spec.InterfacesDefined = tp.spec.InterfacesDefined;
			this.spec.TypeArguments = tp.spec.TypeArguments;
			this.spec.BaseType = tp.spec.BaseType;
			return true;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00085838 File Offset: 0x00083A38
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			this.builder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00085851 File Offset: 0x00083A51
		public void CheckGenericConstraints(bool obsoleteCheck)
		{
			if (this.constraints != null)
			{
				this.constraints.CheckGenericConstraints(this, obsoleteCheck);
			}
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00085868 File Offset: 0x00083A68
		public TypeParameter CreateHoistedCopy(TypeSpec declaringSpec)
		{
			return new TypeParameter(this.spec, declaringSpec, base.MemberName, null);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool Define()
		{
			return true;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0008587D File Offset: 0x00083A7D
		public void Create(TypeSpec declaringType, TypeContainer parent)
		{
			if (this.builder != null)
			{
				throw new InternalErrorException();
			}
			this.Parent = parent;
			this.spec.DeclaringType = declaringType;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000858A0 File Offset: 0x00083AA0
		public void Define(GenericTypeParameterBuilder type)
		{
			this.builder = type;
			this.spec.SetMetaInfo(type);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000858B5 File Offset: 0x00083AB5
		public void Define(TypeParameter tp)
		{
			this.builder = tp.builder;
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000858C4 File Offset: 0x00083AC4
		public void EmitConstraints(GenericTypeParameterBuilder builder)
		{
			GenericParameterAttributes genericParameterAttributes = GenericParameterAttributes.None;
			if (this.spec.Variance == Variance.Contravariant)
			{
				genericParameterAttributes |= GenericParameterAttributes.Contravariant;
			}
			else if (this.spec.Variance == Variance.Covariant)
			{
				genericParameterAttributes |= GenericParameterAttributes.Covariant;
			}
			if (this.spec.HasSpecialClass)
			{
				genericParameterAttributes |= GenericParameterAttributes.ReferenceTypeConstraint;
			}
			else if (this.spec.HasSpecialStruct)
			{
				genericParameterAttributes |= (GenericParameterAttributes.NotNullableValueTypeConstraint | GenericParameterAttributes.DefaultConstructorConstraint);
			}
			if (this.spec.HasSpecialConstructor)
			{
				genericParameterAttributes |= GenericParameterAttributes.DefaultConstructorConstraint;
			}
			if (this.spec.BaseType.BuiltinType != BuiltinTypeSpec.Type.Object)
			{
				builder.SetBaseTypeConstraint(this.spec.BaseType.GetMetaInfo());
			}
			if (this.spec.InterfacesDefined != null)
			{
				builder.SetInterfaceConstraints((from l in this.spec.InterfacesDefined
				select l.GetMetaInfo()).ToArray<Type>());
			}
			if (this.spec.TypeArguments != null)
			{
				List<Type> list = new List<Type>(this.spec.TypeArguments.Length);
				foreach (TypeSpec typeSpec in this.spec.TypeArguments)
				{
					if (typeSpec.BuiltinType != BuiltinTypeSpec.Type.Object && typeSpec.BuiltinType != BuiltinTypeSpec.Type.ValueType)
					{
						list.Add(typeSpec.GetMetaInfo());
					}
				}
				builder.SetInterfaceConstraints(list.ToArray());
			}
			builder.SetGenericParameterAttributes(genericParameterAttributes);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00085A16 File Offset: 0x00083C16
		public override void Emit()
		{
			this.EmitConstraints(this.builder);
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			base.Emit();
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00085A40 File Offset: 0x00083C40
		public void ErrorInvalidVariance(IMemberContext mc, Variance expected)
		{
			base.Report.SymbolRelatedToPreviousError(mc.CurrentMemberDefinition);
			string text = (this.Variance == Variance.Contravariant) ? "contravariant" : "covariant";
			string text2;
			if (expected != Variance.Contravariant)
			{
				if (expected != Variance.Covariant)
				{
					text2 = "invariantly";
				}
				else
				{
					text2 = "covariantly";
				}
			}
			else
			{
				text2 = "contravariantly";
			}
			Delegate @delegate = mc as Delegate;
			string text3 = (@delegate != null) ? @delegate.Parameters.GetSignatureForError() : "";
			base.Report.Error(1961, base.Location, "The {2} type parameter `{0}' must be {3} valid on `{1}{4}'", new string[]
			{
				this.GetSignatureForError(),
				mc.GetSignatureForError(),
				text,
				text2,
				text3
			});
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000055E7 File Offset: 0x000037E7
		public TypeSpec GetAttributeCoClass()
		{
			return null;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0000225C File Offset: 0x0000045C
		public string GetAttributeDefaultMember()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0000225C File Offset: 0x0000045C
		public AttributeUsageAttribute GetAttributeUsage(PredefinedAttribute pa)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override string GetSignatureForDocumentation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0003F215 File Offset: 0x0003D415
		public override string GetSignatureForError()
		{
			return base.MemberName.Name;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00085AF0 File Offset: 0x00083CF0
		bool ITypeDefinition.IsInternalAsPublic(IAssemblyDefinition assembly)
		{
			return this.spec.MemberDefinition.DeclaringAssembly == assembly;
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00085B05 File Offset: 0x00083D05
		public void LoadMembers(TypeSpec declaringType, bool onlyTypes, ref MemberCache cache)
		{
			throw new NotSupportedException("Not supported for compiled definition");
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00085B14 File Offset: 0x00083D14
		public bool ResolveConstraints(IMemberContext context)
		{
			if (this.constraints != null)
			{
				return this.constraints.Resolve(context, this);
			}
			if (this.spec.BaseType == null)
			{
				this.spec.BaseType = context.Module.Compiler.BuiltinTypes.Object;
			}
			return true;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsClsComplianceRequired()
		{
			return false;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00085B65 File Offset: 0x00083D65
		public new void VerifyClsCompliance()
		{
			if (this.constraints != null)
			{
				this.constraints.VerifyClsCompliance(base.Report);
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00085B80 File Offset: 0x00083D80
		public void WarningParentNameConflict(TypeParameter conflict)
		{
			conflict.Report.SymbolRelatedToPreviousError(conflict.Location, null);
			conflict.Report.Warning(693, 3, base.Location, "Type parameter `{0}' has the same name as the type parameter from outer type `{1}'", this.GetSignatureForError(), conflict.CurrentType.GetSignatureForError());
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00085BCC File Offset: 0x00083DCC
		// Note: this type is marked as 'beforefieldinit'.
		static TypeParameter()
		{
		}

		// Token: 0x04000A43 RID: 2627
		private static readonly string[] attribute_target = new string[]
		{
			"type parameter"
		};

		// Token: 0x04000A44 RID: 2628
		private Constraints constraints;

		// Token: 0x04000A45 RID: 2629
		private GenericTypeParameterBuilder builder;

		// Token: 0x04000A46 RID: 2630
		private readonly TypeParameterSpec spec;

		// Token: 0x04000A47 RID: 2631
		[CompilerGenerated]
		private VarianceDecl <VarianceDecl>k__BackingField;

		// Token: 0x020003C0 RID: 960
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002743 RID: 10051 RVA: 0x000BBED9 File Offset: 0x000BA0D9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002744 RID: 10052 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x06002745 RID: 10053 RVA: 0x000BBEE5 File Offset: 0x000BA0E5
			internal Type <EmitConstraints>b__52_0(TypeSpec l)
			{
				return l.GetMetaInfo();
			}

			// Token: 0x040010AC RID: 4268
			public static readonly TypeParameter.<>c <>9 = new TypeParameter.<>c();

			// Token: 0x040010AD RID: 4269
			public static Func<TypeSpec, Type> <>9__52_0;
		}
	}
}
