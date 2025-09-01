using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200024A RID: 586
	public sealed class MethodSpec : MemberSpec, IParametersMember, IInterfaceMemberSpec
	{
		// Token: 0x06001D0E RID: 7438 RVA: 0x0008D360 File Offset: 0x0008B560
		public MethodSpec(MemberKind kind, TypeSpec declaringType, IMethodDefinition details, TypeSpec returnType, AParametersCollection parameters, Modifiers modifiers) : base(kind, declaringType, details, modifiers)
		{
			this.parameters = parameters;
			this.returnType = returnType;
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0008D37D File Offset: 0x0008B57D
		public override int Arity
		{
			get
			{
				if (!base.IsGeneric)
				{
					return 0;
				}
				return this.GenericDefinition.TypeParametersCount;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0008D394 File Offset: 0x0008B594
		public TypeParameterSpec[] Constraints
		{
			get
			{
				if (this.constraints == null && base.IsGeneric)
				{
					this.constraints = this.GenericDefinition.TypeParameters;
				}
				return this.constraints;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0008D3BD File Offset: 0x0008B5BD
		public bool IsConstructor
		{
			get
			{
				return this.Kind == MemberKind.Constructor;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0008D3C8 File Offset: 0x0008B5C8
		public new IMethodDefinition MemberDefinition
		{
			get
			{
				return (IMethodDefinition)this.definition;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0008D3D5 File Offset: 0x0008B5D5
		public IGenericMethodDefinition GenericDefinition
		{
			get
			{
				return (IGenericMethodDefinition)this.definition;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0008D3E2 File Offset: 0x0008B5E2
		public bool IsAsync
		{
			get
			{
				return (base.Modifiers & Modifiers.ASYNC) > (Modifiers)0;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0008D3F3 File Offset: 0x0008B5F3
		public bool IsExtensionMethod
		{
			get
			{
				return base.IsStatic && this.parameters.HasExtensionMethodType;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0008D40A File Offset: 0x0008B60A
		public bool IsSealed
		{
			get
			{
				return (base.Modifiers & Modifiers.SEALED) > (Modifiers)0;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0008D418 File Offset: 0x0008B618
		public bool IsVirtual
		{
			get
			{
				return (base.Modifiers & (Modifiers.ABSTRACT | Modifiers.VIRTUAL | Modifiers.OVERRIDE)) > (Modifiers)0;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0008D429 File Offset: 0x0008B629
		public bool IsReservedMethod
		{
			get
			{
				return this.Kind == MemberKind.Operator || base.IsAccessor;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0008D43D File Offset: 0x0008B63D
		TypeSpec IInterfaceMemberSpec.MemberType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0008D445 File Offset: 0x0008B645
		public AParametersCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0008D43D File Offset: 0x0008B63D
		public TypeSpec ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x0008D44D File Offset: 0x0008B64D
		public TypeSpec[] TypeArguments
		{
			get
			{
				return this.targs;
			}
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0008D455 File Offset: 0x0008B655
		public MethodSpec GetGenericMethodDefinition()
		{
			if (!base.IsGeneric && !base.DeclaringType.IsGeneric)
			{
				return this;
			}
			return MemberCache.GetMember<MethodSpec>(this.declaringType, this);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0008D47C File Offset: 0x0008B67C
		public MethodBase GetMetaInfo()
		{
			if (this.inflatedMetaInfo == null)
			{
				if ((this.state & MemberSpec.StateFlags.PendingMetaInflate) != (MemberSpec.StateFlags)0)
				{
					Type metaInfo = base.DeclaringType.GetMetaInfo();
					if (base.DeclaringType.IsTypeBuilder)
					{
						if (this.IsConstructor)
						{
							this.inflatedMetaInfo = TypeBuilder.GetConstructor(metaInfo, (ConstructorInfo)this.MemberDefinition.Metadata);
						}
						else
						{
							this.inflatedMetaInfo = TypeBuilder.GetMethod(metaInfo, (MethodInfo)this.MemberDefinition.Metadata);
						}
					}
					else
					{
						this.inflatedMetaInfo = MethodBase.GetMethodFromHandle(this.MemberDefinition.Metadata.MethodHandle, metaInfo.TypeHandle);
					}
					this.state &= ~MemberSpec.StateFlags.PendingMetaInflate;
				}
				else
				{
					this.inflatedMetaInfo = this.MemberDefinition.Metadata;
				}
			}
			if ((this.state & MemberSpec.StateFlags.PendingMakeMethod) != (MemberSpec.StateFlags)0)
			{
				Type[] array = new Type[this.targs.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.targs[i].GetMetaInfo();
				}
				this.inflatedMetaInfo = ((MethodInfo)this.inflatedMetaInfo).MakeGenericMethod(array);
				this.state &= ~MemberSpec.StateFlags.PendingMakeMethod;
			}
			return this.inflatedMetaInfo;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0008D5B4 File Offset: 0x0008B7B4
		public override string GetSignatureForDocumentation()
		{
			MemberKind kind = this.Kind;
			string text;
			if (kind != MemberKind.Constructor)
			{
				if (kind != MemberKind.Method)
				{
					text = this.Name;
				}
				else if (this.Arity > 0)
				{
					text = this.Name + "``" + this.Arity.ToString();
				}
				else
				{
					text = this.Name;
				}
			}
			else
			{
				text = "#ctor";
			}
			text = base.DeclaringType.GetSignatureForDocumentation() + "." + text + this.parameters.GetSignatureForDocumentation();
			if (this.Kind == MemberKind.Operator)
			{
				Operator.OpType value = Operator.GetType(this.Name).Value;
				if (value == Operator.OpType.Explicit || value == Operator.OpType.Implicit)
				{
					text = text + "~" + this.ReturnType.GetSignatureForDocumentation();
				}
			}
			return text;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0008D678 File Offset: 0x0008B878
		public override string GetSignatureForError()
		{
			string text;
			if (this.IsConstructor)
			{
				text = base.DeclaringType.GetSignatureForError() + "." + base.DeclaringType.Name;
			}
			else if (this.Kind == MemberKind.Operator)
			{
				Operator.OpType value = Operator.GetType(this.Name).Value;
				if (value == Operator.OpType.Implicit || value == Operator.OpType.Explicit)
				{
					text = string.Concat(new string[]
					{
						base.DeclaringType.GetSignatureForError(),
						".",
						Operator.GetName(value),
						" operator ",
						this.returnType.GetSignatureForError()
					});
				}
				else
				{
					text = base.DeclaringType.GetSignatureForError() + ".operator " + Operator.GetName(value);
				}
			}
			else
			{
				if (base.IsAccessor)
				{
					int num = this.Name.IndexOf('_');
					text = this.Name.Substring(num + 1);
					string text2 = this.Name.Substring(0, num);
					if (num == 3)
					{
						int count = this.parameters.Count;
						if (count > 0 && text2 == "get")
						{
							text = "this" + this.parameters.GetSignatureForError("[", "]", count);
						}
						else if (count > 1 && text2 == "set")
						{
							text = "this" + this.parameters.GetSignatureForError("[", "]", count - 1);
						}
					}
					return string.Concat(new string[]
					{
						base.DeclaringType.GetSignatureForError(),
						".",
						text,
						".",
						text2
					});
				}
				text = base.GetSignatureForError();
				if (this.targs != null)
				{
					text = text + "<" + TypeManager.CSharpName(this.targs) + ">";
				}
				else if (base.IsGeneric)
				{
					text = text + "<" + TypeManager.CSharpName(this.GenericDefinition.TypeParameters) + ">";
				}
			}
			return text + this.parameters.GetSignatureForError();
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0008D898 File Offset: 0x0008BA98
		public override MemberSpec InflateMember(TypeParameterInflator inflator)
		{
			MethodSpec methodSpec = (MethodSpec)base.InflateMember(inflator);
			methodSpec.inflatedMetaInfo = null;
			methodSpec.returnType = inflator.Inflate(this.returnType);
			methodSpec.parameters = this.parameters.Inflate(inflator);
			if (base.IsGeneric)
			{
				methodSpec.constraints = TypeParameterSpec.InflateConstraints(inflator, this.Constraints);
			}
			return methodSpec;
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0008D8FC File Offset: 0x0008BAFC
		public MethodSpec MakeGenericMethod(IMemberContext context, params TypeSpec[] targs)
		{
			if (targs == null)
			{
				throw new ArgumentNullException();
			}
			TypeParameterInflator inflator = new TypeParameterInflator(context, base.DeclaringType, this.GenericDefinition.TypeParameters, targs);
			MethodSpec methodSpec = (MethodSpec)base.MemberwiseClone();
			methodSpec.declaringType = inflator.TypeInstance;
			methodSpec.returnType = inflator.Inflate(this.returnType);
			methodSpec.parameters = this.parameters.Inflate(inflator);
			methodSpec.targs = targs;
			methodSpec.constraints = TypeParameterSpec.InflateConstraints(inflator, this.constraints ?? this.GenericDefinition.TypeParameters);
			methodSpec.state |= MemberSpec.StateFlags.PendingMakeMethod;
			return methodSpec;
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x0008D9A4 File Offset: 0x0008BBA4
		public MethodSpec Mutate(TypeParameterMutator mutator)
		{
			TypeSpec[] array = this.TypeArguments;
			if (array != null)
			{
				array = mutator.Mutate(array);
			}
			TypeSpec typeSpec = base.DeclaringType;
			if (base.DeclaringType.IsGenericOrParentIsGeneric)
			{
				typeSpec = mutator.Mutate(typeSpec);
			}
			if (array == this.TypeArguments && typeSpec == base.DeclaringType)
			{
				return this;
			}
			MethodSpec methodSpec = (MethodSpec)base.MemberwiseClone();
			if (typeSpec != base.DeclaringType)
			{
				methodSpec.inflatedMetaInfo = null;
				methodSpec.declaringType = typeSpec;
				methodSpec.state |= MemberSpec.StateFlags.PendingMetaInflate;
			}
			if (array != null)
			{
				methodSpec.targs = array;
				methodSpec.state |= MemberSpec.StateFlags.PendingMakeMethod;
			}
			return methodSpec;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0008DA48 File Offset: 0x0008BC48
		public override List<MissingTypeSpecReference> ResolveMissingDependencies(MemberSpec caller)
		{
			List<MissingTypeSpecReference> list = this.returnType.ResolveMissingDependencies(this);
			TypeSpec[] types = this.parameters.Types;
			for (int i = 0; i < types.Length; i++)
			{
				List<MissingTypeSpecReference> missingDependencies = types[i].GetMissingDependencies(this);
				if (missingDependencies != null)
				{
					if (list == null)
					{
						list = new List<MissingTypeSpecReference>();
					}
					list.AddRange(missingDependencies);
				}
			}
			if (this.Arity > 0)
			{
				TypeParameterSpec[] typeParameters = this.GenericDefinition.TypeParameters;
				for (int i = 0; i < typeParameters.Length; i++)
				{
					List<MissingTypeSpecReference> missingDependencies2 = typeParameters[i].GetMissingDependencies(this);
					if (missingDependencies2 != null)
					{
						if (list == null)
						{
							list = new List<MissingTypeSpecReference>();
						}
						list.AddRange(missingDependencies2);
					}
				}
			}
			return list;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x0008DAE0 File Offset: 0x0008BCE0
		// Note: this type is marked as 'beforefieldinit'.
		static MethodSpec()
		{
		}

		// Token: 0x04000ACB RID: 2763
		private MethodBase inflatedMetaInfo;

		// Token: 0x04000ACC RID: 2764
		private AParametersCollection parameters;

		// Token: 0x04000ACD RID: 2765
		private TypeSpec returnType;

		// Token: 0x04000ACE RID: 2766
		private TypeSpec[] targs;

		// Token: 0x04000ACF RID: 2767
		private TypeParameterSpec[] constraints;

		// Token: 0x04000AD0 RID: 2768
		public static readonly MethodSpec Excluded = new MethodSpec(MemberKind.Method, InternalType.FakeInternalType, null, null, ParametersCompiled.EmptyReadOnlyParameters, (Modifiers)0);
	}
}
