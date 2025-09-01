using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000220 RID: 544
	[DebuggerDisplay("{DisplayDebugInfo()}")]
	public class TypeParameterSpec : TypeSpec
	{
		// Token: 0x06001BA4 RID: 7076 RVA: 0x00085BE1 File Offset: 0x00083DE1
		public TypeParameterSpec(TypeSpec declaringType, int index, ITypeDefinition definition, SpecialConstraint spec, Variance variance, Type info) : base(MemberKind.TypeParameter, declaringType, definition, info, Modifiers.PUBLIC)
		{
			this.variance = variance;
			this.spec = spec;
			this.state &= ~MemberSpec.StateFlags.Obsolete_Undetected;
			this.tp_pos = index;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00085C19 File Offset: 0x00083E19
		public TypeParameterSpec(int index, ITypeDefinition definition, SpecialConstraint spec, Variance variance, Type info) : this(null, index, definition, spec, variance, info)
		{
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x00085C29 File Offset: 0x00083E29
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x00085C31 File Offset: 0x00083E31
		public int DeclaredPosition
		{
			get
			{
				return this.tp_pos;
			}
			set
			{
				this.tp_pos = value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x00085C3A File Offset: 0x00083E3A
		public bool HasSpecialConstructor
		{
			get
			{
				return (this.spec & SpecialConstraint.Constructor) > SpecialConstraint.None;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00085C47 File Offset: 0x00083E47
		public bool HasSpecialClass
		{
			get
			{
				return (this.spec & SpecialConstraint.Class) > SpecialConstraint.None;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00085C54 File Offset: 0x00083E54
		public bool HasSpecialStruct
		{
			get
			{
				return (this.spec & SpecialConstraint.Struct) > SpecialConstraint.None;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00085C62 File Offset: 0x00083E62
		public bool HasAnyTypeConstraint
		{
			get
			{
				return (this.spec & (SpecialConstraint.Class | SpecialConstraint.Struct)) != SpecialConstraint.None || this.ifaces != null || this.targs != null || this.HasTypeConstraint;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00085C88 File Offset: 0x00083E88
		public bool HasTypeConstraint
		{
			get
			{
				BuiltinTypeSpec.Type builtinType = this.BaseType.BuiltinType;
				return builtinType != BuiltinTypeSpec.Type.Object && builtinType != BuiltinTypeSpec.Type.ValueType;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x00085CB0 File Offset: 0x00083EB0
		public override IList<TypeSpec> Interfaces
		{
			get
			{
				if ((this.state & MemberSpec.StateFlags.InterfacesExpanded) == (MemberSpec.StateFlags)0)
				{
					if (this.ifaces != null)
					{
						if (this.ifaces_defined == null)
						{
							this.ifaces_defined = this.ifaces.ToArray<TypeSpec>();
						}
						for (int i = 0; i < this.ifaces_defined.Length; i++)
						{
							TypeSpec typeSpec = this.ifaces_defined[i];
							TypeDefinition typeDefinition = typeSpec.MemberDefinition as TypeDefinition;
							if (typeDefinition != null)
							{
								typeDefinition.DoExpandBaseInterfaces();
							}
							if (typeSpec.Interfaces != null)
							{
								for (int j = 0; j < typeSpec.Interfaces.Count; j++)
								{
									TypeSpec iface = typeSpec.Interfaces[j];
									this.AddInterface(iface);
								}
							}
						}
					}
					else if (this.ifaces_defined == null)
					{
						this.ifaces_defined = ((this.ifaces == null) ? TypeSpec.EmptyTypes : this.ifaces.ToArray<TypeSpec>());
					}
					if (this.BaseType != null)
					{
						TypeDefinition typeDefinition2 = this.BaseType.MemberDefinition as TypeDefinition;
						if (typeDefinition2 != null)
						{
							typeDefinition2.DoExpandBaseInterfaces();
						}
						if (this.BaseType.Interfaces != null)
						{
							foreach (TypeSpec iface2 in this.BaseType.Interfaces)
							{
								this.AddInterface(iface2);
							}
						}
					}
					this.state |= MemberSpec.StateFlags.InterfacesExpanded;
				}
				return this.ifaces;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00085E1C File Offset: 0x0008401C
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x00085E57 File Offset: 0x00084057
		public TypeSpec[] InterfacesDefined
		{
			get
			{
				if (this.ifaces_defined == null)
				{
					this.ifaces_defined = ((this.ifaces == null) ? TypeSpec.EmptyTypes : this.ifaces.ToArray<TypeSpec>());
				}
				if (this.ifaces_defined.Length != 0)
				{
					return this.ifaces_defined;
				}
				return null;
			}
			set
			{
				this.ifaces_defined = value;
				if (value != null && value.Length != 0)
				{
					this.ifaces = new List<TypeSpec>(value);
				}
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00085E73 File Offset: 0x00084073
		public bool IsConstrained
		{
			get
			{
				return this.spec != SpecialConstraint.None || this.ifaces != null || this.targs != null || this.HasTypeConstraint;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x00085E98 File Offset: 0x00084098
		public new bool IsReferenceType
		{
			get
			{
				if ((this.spec & (SpecialConstraint.Class | SpecialConstraint.Struct)) != SpecialConstraint.None)
				{
					return (this.spec & SpecialConstraint.Class) > SpecialConstraint.None;
				}
				if (this.HasTypeConstraint && TypeSpec.IsReferenceType(this.BaseType))
				{
					return true;
				}
				if (this.targs != null)
				{
					foreach (TypeSpec typeSpec in this.targs)
					{
						TypeParameterSpec typeParameterSpec = typeSpec as TypeParameterSpec;
						if ((typeParameterSpec == null || (typeParameterSpec.spec & (SpecialConstraint.Class | SpecialConstraint.Struct)) == SpecialConstraint.None) && TypeSpec.IsReferenceType(typeSpec))
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x00085F14 File Offset: 0x00084114
		public new bool IsValueType
		{
			get
			{
				if (this.HasSpecialStruct)
				{
					return true;
				}
				if (this.targs != null)
				{
					TypeSpec[] array = this.targs;
					for (int i = 0; i < array.Length; i++)
					{
						if (TypeSpec.IsValueType(array[i]))
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0006771B File Offset: 0x0006591B
		public override string Name
		{
			get
			{
				return this.definition.Name;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x00085F55 File Offset: 0x00084155
		public bool IsMethodOwned
		{
			get
			{
				return base.DeclaringType == null;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00085F60 File Offset: 0x00084160
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x00085F68 File Offset: 0x00084168
		public SpecialConstraint SpecialConstraint
		{
			get
			{
				return this.spec;
			}
			set
			{
				this.spec = value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00085F71 File Offset: 0x00084171
		// (set) Token: 0x06001BB8 RID: 7096 RVA: 0x00085F79 File Offset: 0x00084179
		public new TypeSpec[] TypeArguments
		{
			get
			{
				return this.targs;
			}
			set
			{
				this.targs = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x00085F82 File Offset: 0x00084182
		public Variance Variance
		{
			get
			{
				return this.variance;
			}
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00085F8C File Offset: 0x0008418C
		public string DisplayDebugInfo()
		{
			string signatureForError = this.GetSignatureForError();
			if (!this.IsMethodOwned)
			{
				return signatureForError + "!";
			}
			return signatureForError + "!!";
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00085FC0 File Offset: 0x000841C0
		public TypeSpec GetEffectiveBase()
		{
			if (this.HasSpecialStruct)
			{
				return this.BaseType;
			}
			if (this.BaseType != null && this.targs == null)
			{
				if (!this.BaseType.IsStruct)
				{
					return this.BaseType;
				}
				return this.BaseType.BaseType;
			}
			else
			{
				if (this.effective_base != null)
				{
					return this.effective_base;
				}
				TypeSpec[] array = new TypeSpec[this.HasTypeConstraint ? (this.targs.Length + 1) : this.targs.Length];
				for (int i = 0; i < this.targs.Length; i++)
				{
					TypeSpec typeSpec = this.targs[i];
					if (typeSpec.IsStruct)
					{
						array[i] = typeSpec.BaseType;
					}
					else
					{
						TypeParameterSpec typeParameterSpec = typeSpec as TypeParameterSpec;
						array[i] = ((typeParameterSpec != null) ? typeParameterSpec.GetEffectiveBase() : typeSpec);
					}
				}
				if (this.HasTypeConstraint)
				{
					TypeSpec[] array2 = array;
					array2[array2.Length - 1] = this.BaseType;
				}
				return this.effective_base = Convert.FindMostEncompassedType(array);
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000860A8 File Offset: 0x000842A8
		public override string GetSignatureForDocumentation(bool explicitName)
		{
			if (explicitName)
			{
				return this.Name;
			}
			return (this.IsMethodOwned ? "``" : "`") + this.DeclaredPosition;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000860D8 File Offset: 0x000842D8
		public override string GetSignatureForError()
		{
			return this.Name;
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000860E0 File Offset: 0x000842E0
		public bool HasSameConstraintsDefinition(TypeParameterSpec other)
		{
			return this.spec == other.spec && this.BaseType == other.BaseType && TypeSpecComparer.Override.IsSame(this.InterfacesDefined, other.InterfacesDefined) && TypeSpecComparer.Override.IsSame(this.targs, other.targs);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00086138 File Offset: 0x00084338
		public bool HasSameConstraintsImplementation(TypeParameterSpec other)
		{
			if (this.spec != other.spec)
			{
				return false;
			}
			if (!TypeSpecComparer.Override.IsEqual(this.BaseType, other.BaseType))
			{
				bool flag = false;
				if (other.targs != null)
				{
					foreach (TypeSpec b in other.targs)
					{
						if (TypeSpecComparer.Override.IsEqual(this.BaseType, b))
						{
							flag = true;
							break;
						}
					}
				}
				else if (this.targs != null)
				{
					TypeSpec[] array = this.targs;
					for (int i = 0; i < array.Length; i++)
					{
						if (TypeSpecComparer.Override.IsEqual(array[i], other.BaseType))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			if (this.InterfacesDefined != null)
			{
				foreach (TypeSpec a in this.Interfaces)
				{
					bool flag = false;
					if (other.InterfacesDefined != null)
					{
						foreach (TypeSpec b2 in other.Interfaces)
						{
							if (TypeSpecComparer.Override.IsEqual(a, b2))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						if (other.targs != null)
						{
							foreach (TypeSpec b3 in other.targs)
							{
								if (TypeSpecComparer.Override.IsEqual(a, b3))
								{
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							return false;
						}
					}
				}
			}
			if (other.InterfacesDefined != null)
			{
				foreach (TypeSpec b4 in other.Interfaces)
				{
					bool flag = false;
					if (this.InterfacesDefined != null)
					{
						using (IEnumerator<TypeSpec> enumerator2 = this.Interfaces.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (TypeSpecComparer.Override.IsEqual(enumerator2.Current, b4))
								{
									flag = true;
									break;
								}
							}
							goto IL_1EE;
						}
						goto IL_1C1;
					}
					goto IL_1C1;
					IL_1EE:
					if (!flag)
					{
						return false;
					}
					continue;
					IL_1C1:
					if (this.targs != null)
					{
						TypeSpec[] array = this.targs;
						for (int i = 0; i < array.Length; i++)
						{
							if (TypeSpecComparer.Override.IsEqual(array[i], b4))
							{
								flag = true;
								break;
							}
						}
						goto IL_1EE;
					}
					goto IL_1EE;
				}
			}
			if (this.targs != null)
			{
				foreach (TypeSpec typeSpec in this.targs)
				{
					bool flag = false;
					if (other.targs != null)
					{
						foreach (TypeSpec b5 in other.targs)
						{
							if (TypeSpecComparer.Override.IsEqual(typeSpec, b5))
							{
								flag = true;
								break;
							}
						}
					}
					if (other.InterfacesDefined != null && !flag)
					{
						using (IEnumerator<TypeSpec> enumerator = other.Interfaces.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (TypeSpecComparer.Override.IsEqual(enumerator.Current, typeSpec))
								{
									flag = true;
									break;
								}
							}
						}
					}
					if (!flag)
					{
						flag = TypeSpecComparer.Override.IsEqual(typeSpec, other.BaseType);
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			if (other.targs != null)
			{
				foreach (TypeSpec typeSpec2 in other.targs)
				{
					if (typeSpec2.IsGenericParameter)
					{
						if (this.targs == null)
						{
							return false;
						}
						bool flag = false;
						TypeSpec[] array2 = this.targs;
						for (int j = 0; j < array2.Length; j++)
						{
							if (TypeSpecComparer.Override.IsEqual(array2[j], typeSpec2))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000864C8 File Offset: 0x000846C8
		public static TypeParameterSpec[] InflateConstraints(TypeParameterInflator inflator, TypeParameterSpec[] tparams)
		{
			return TypeParameterSpec.InflateConstraints<TypeParameterInflator>(tparams, (TypeParameterInflator l) => l, inflator);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000864F0 File Offset: 0x000846F0
		public static TypeParameterSpec[] InflateConstraints<T>(TypeParameterSpec[] tparams, Func<T, TypeParameterInflator> inflatorFactory, T arg)
		{
			TypeParameterSpec[] array = null;
			TypeParameterInflator? typeParameterInflator = null;
			for (int i = 0; i < tparams.Length; i++)
			{
				TypeParameterSpec typeParameterSpec = tparams[i];
				if (typeParameterSpec.HasTypeConstraint || typeParameterSpec.InterfacesDefined != null || typeParameterSpec.TypeArguments != null)
				{
					if (array == null)
					{
						array = new TypeParameterSpec[tparams.Length];
						Array.Copy(tparams, array, array.Length);
					}
					if (typeParameterInflator == null)
					{
						typeParameterInflator = new TypeParameterInflator?(inflatorFactory(arg));
					}
					array[i] = (TypeParameterSpec)array[i].InflateMember(typeParameterInflator.Value);
				}
			}
			if (array == null)
			{
				array = tparams;
			}
			return array;
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0008657C File Offset: 0x0008477C
		public void InflateConstraints(TypeParameterInflator inflator, TypeParameterSpec tps)
		{
			tps.BaseType = inflator.Inflate(this.BaseType);
			TypeSpec[] interfacesDefined = this.InterfacesDefined;
			if (interfacesDefined != null)
			{
				tps.ifaces_defined = new TypeSpec[interfacesDefined.Length];
				for (int i = 0; i < interfacesDefined.Length; i++)
				{
					tps.ifaces_defined[i] = inflator.Inflate(interfacesDefined[i]);
				}
			}
			else if (this.ifaces_defined == TypeSpec.EmptyTypes)
			{
				tps.ifaces_defined = TypeSpec.EmptyTypes;
			}
			IList<TypeSpec> interfaces = this.Interfaces;
			if (interfaces != null)
			{
				tps.ifaces = new List<TypeSpec>(interfaces.Count);
				for (int j = 0; j < interfaces.Count; j++)
				{
					tps.ifaces.Add(inflator.Inflate(interfaces[j]));
				}
				tps.state |= MemberSpec.StateFlags.InterfacesExpanded;
			}
			if (this.targs != null)
			{
				tps.targs = new TypeSpec[this.targs.Length];
				for (int k = 0; k < this.targs.Length; k++)
				{
					tps.targs[k] = inflator.Inflate(this.targs[k]);
				}
			}
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00086690 File Offset: 0x00084890
		public override MemberSpec InflateMember(TypeParameterInflator inflator)
		{
			TypeParameterSpec typeParameterSpec = (TypeParameterSpec)base.MemberwiseClone();
			this.InflateConstraints(inflator, typeParameterSpec);
			return typeParameterSpec;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000866B4 File Offset: 0x000848B4
		protected override void InitializeMemberCache(bool onlyTypes)
		{
			this.cache = new MemberCache();
			if (this.BaseType.BuiltinType != BuiltinTypeSpec.Type.Object && this.BaseType.BuiltinType != BuiltinTypeSpec.Type.ValueType)
			{
				this.cache.AddBaseType(this.BaseType);
			}
			if (this.InterfacesDefined != null)
			{
				foreach (TypeSpec iface in this.InterfacesDefined)
				{
					this.cache.AddInterface(iface);
				}
			}
			if (this.targs != null)
			{
				foreach (TypeSpec typeSpec in this.targs)
				{
					TypeParameterSpec typeParameterSpec = typeSpec as TypeParameterSpec;
					TypeSpec typeSpec2;
					IList<TypeSpec> list;
					if (typeParameterSpec != null)
					{
						typeSpec2 = typeParameterSpec.GetEffectiveBase();
						list = typeParameterSpec.InterfacesDefined;
					}
					else
					{
						typeSpec2 = typeSpec;
						list = typeSpec.Interfaces;
					}
					if (typeSpec2 != null && typeSpec2.BuiltinType != BuiltinTypeSpec.Type.Object && typeSpec2.BuiltinType != BuiltinTypeSpec.Type.ValueType && !typeSpec2.IsStructOrEnum)
					{
						this.cache.AddBaseType(typeSpec2);
					}
					if (list != null)
					{
						foreach (TypeSpec iface2 in list)
						{
							this.cache.AddInterface(iface2);
						}
					}
				}
			}
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x000867FC File Offset: 0x000849FC
		public bool IsConvertibleToInterface(TypeSpec iface)
		{
			if (this.Interfaces != null)
			{
				using (IEnumerator<TypeSpec> enumerator = this.Interfaces.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == iface)
						{
							return true;
						}
					}
				}
			}
			if (this.TypeArguments != null)
			{
				foreach (TypeSpec typeSpec in this.TypeArguments)
				{
					TypeParameterSpec typeParameterSpec = typeSpec as TypeParameterSpec;
					if (typeParameterSpec != null)
					{
						if (typeParameterSpec.IsConvertibleToInterface(iface))
						{
							return true;
						}
					}
					else if (typeSpec.ImplementsInterface(iface, false))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0008689C File Offset: 0x00084A9C
		public static bool HasAnyTypeParameterTypeConstrained(IGenericMethodDefinition md)
		{
			TypeParameterSpec[] typeParameters = md.TypeParameters;
			for (int i = 0; i < md.TypeParametersCount; i++)
			{
				if (typeParameters[i].HasAnyTypeConstraint)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000868D0 File Offset: 0x00084AD0
		public static bool HasAnyTypeParameterConstrained(IGenericMethodDefinition md)
		{
			TypeParameterSpec[] typeParameters = md.TypeParameters;
			for (int i = 0; i < md.TypeParametersCount; i++)
			{
				if (typeParameters[i].IsConstrained)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00086904 File Offset: 0x00084B04
		public bool HasDependencyOn(TypeSpec type)
		{
			if (this.TypeArguments != null)
			{
				foreach (TypeSpec typeSpec in this.TypeArguments)
				{
					if (TypeSpecComparer.Override.IsEqual(typeSpec, type))
					{
						return true;
					}
					TypeParameterSpec typeParameterSpec = typeSpec as TypeParameterSpec;
					if (typeParameterSpec != null && typeParameterSpec.HasDependencyOn(type))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00086953 File Offset: 0x00084B53
		public override TypeSpec Mutate(TypeParameterMutator mutator)
		{
			return mutator.Mutate(this);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0008695C File Offset: 0x00084B5C
		// Note: this type is marked as 'beforefieldinit'.
		static TypeParameterSpec()
		{
		}

		// Token: 0x04000A48 RID: 2632
		public new static readonly TypeParameterSpec[] EmptyTypes = new TypeParameterSpec[0];

		// Token: 0x04000A49 RID: 2633
		private Variance variance;

		// Token: 0x04000A4A RID: 2634
		private SpecialConstraint spec;

		// Token: 0x04000A4B RID: 2635
		private int tp_pos;

		// Token: 0x04000A4C RID: 2636
		private TypeSpec[] targs;

		// Token: 0x04000A4D RID: 2637
		private TypeSpec[] ifaces_defined;

		// Token: 0x04000A4E RID: 2638
		private TypeSpec effective_base;

		// Token: 0x020003C1 RID: 961
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002746 RID: 10054 RVA: 0x000BBEED File Offset: 0x000BA0ED
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002747 RID: 10055 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x06002748 RID: 10056 RVA: 0x0004D50E File Offset: 0x0004B70E
			internal TypeParameterInflator <InflateConstraints>b__51_0(TypeParameterInflator l)
			{
				return l;
			}

			// Token: 0x040010AE RID: 4270
			public static readonly TypeParameterSpec.<>c <>9 = new TypeParameterSpec.<>c();

			// Token: 0x040010AF RID: 4271
			public static Func<TypeParameterInflator, TypeParameterInflator> <>9__51_0;
		}
	}
}
