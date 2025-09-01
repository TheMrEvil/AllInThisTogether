using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000224 RID: 548
	public class InflatedTypeSpec : TypeSpec
	{
		// Token: 0x06001BDA RID: 7130 RVA: 0x00086D60 File Offset: 0x00084F60
		public InflatedTypeSpec(IModuleContext context, TypeSpec openType, TypeSpec declaringType, TypeSpec[] targs) : base(openType.Kind, declaringType, openType.MemberDefinition, null, openType.Modifiers)
		{
			if (targs == null)
			{
				throw new ArgumentNullException("targs");
			}
			this.state &= ~(MemberSpec.StateFlags.Obsolete_Undetected | MemberSpec.StateFlags.Obsolete | MemberSpec.StateFlags.CLSCompliant_Undetected | MemberSpec.StateFlags.CLSCompliant | MemberSpec.StateFlags.MissingDependency_Undetected | MemberSpec.StateFlags.MissingDependency | MemberSpec.StateFlags.HasDynamicElement);
			this.state |= (openType.state & (MemberSpec.StateFlags.Obsolete_Undetected | MemberSpec.StateFlags.Obsolete | MemberSpec.StateFlags.CLSCompliant_Undetected | MemberSpec.StateFlags.CLSCompliant | MemberSpec.StateFlags.MissingDependency_Undetected | MemberSpec.StateFlags.MissingDependency | MemberSpec.StateFlags.HasDynamicElement));
			this.context = context;
			this.open_type = openType;
			this.targs = targs;
			foreach (TypeSpec typeSpec in targs)
			{
				if (typeSpec.HasDynamicElement || typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					this.state |= MemberSpec.StateFlags.HasDynamicElement;
					break;
				}
			}
			if (this.open_type.Kind == MemberKind.MissingType)
			{
				base.MemberCache = MemberCache.Empty;
			}
			if ((this.open_type.Modifiers & Modifiers.COMPILER_GENERATED) != (Modifiers)0)
			{
				this.state |= MemberSpec.StateFlags.ConstraintsChecked;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00086E4B File Offset: 0x0008504B
		public override TypeSpec BaseType
		{
			get
			{
				if (this.cache == null || (this.state & MemberSpec.StateFlags.PendingBaseTypeInflate) != (MemberSpec.StateFlags)0)
				{
					this.InitializeMemberCache(true);
				}
				return base.BaseType;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x00086E70 File Offset: 0x00085070
		public TypeParameterSpec[] Constraints
		{
			get
			{
				if (this.constraints == null)
				{
					this.constraints = TypeParameterSpec.InflateConstraints<InflatedTypeSpec>(base.MemberDefinition.TypeParameters, (InflatedTypeSpec l) => l.CreateLocalInflator(this.context), this);
				}
				return this.constraints;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00086EA3 File Offset: 0x000850A3
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x00086EB4 File Offset: 0x000850B4
		public bool HasConstraintsChecked
		{
			get
			{
				return (this.state & MemberSpec.StateFlags.ConstraintsChecked) > (MemberSpec.StateFlags)0;
			}
			set
			{
				this.state = (value ? (this.state | MemberSpec.StateFlags.ConstraintsChecked) : (this.state & ~MemberSpec.StateFlags.ConstraintsChecked));
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00086ED9 File Offset: 0x000850D9
		public override IList<TypeSpec> Interfaces
		{
			get
			{
				if (this.cache == null)
				{
					this.InitializeMemberCache(true);
				}
				return base.Interfaces;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00086EF0 File Offset: 0x000850F0
		public override bool IsExpressionTreeType
		{
			get
			{
				return (this.open_type.state & MemberSpec.StateFlags.InflatedExpressionType) > (MemberSpec.StateFlags)0;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x00086F06 File Offset: 0x00085106
		public override bool IsArrayGenericInterface
		{
			get
			{
				return (this.open_type.state & MemberSpec.StateFlags.GenericIterateInterface) > (MemberSpec.StateFlags)0;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x00086F1C File Offset: 0x0008511C
		public override bool IsGenericTask
		{
			get
			{
				return (this.open_type.state & MemberSpec.StateFlags.GenericTask) > (MemberSpec.StateFlags)0;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x00086F32 File Offset: 0x00085132
		public override bool IsNullableType
		{
			get
			{
				return (this.open_type.state & MemberSpec.StateFlags.InflatedNullableType) > (MemberSpec.StateFlags)0;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00086F48 File Offset: 0x00085148
		public override TypeSpec[] TypeArguments
		{
			get
			{
				return this.targs;
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00086F50 File Offset: 0x00085150
		public override bool AddInterface(TypeSpec iface)
		{
			iface = this.CreateLocalInflator(this.context).Inflate(iface);
			return iface != null && base.AddInterface(iface);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00086F80 File Offset: 0x00085180
		public static bool ContainsTypeParameter(TypeSpec type)
		{
			if (type.Kind == MemberKind.TypeParameter)
			{
				return true;
			}
			ElementTypeSpec elementTypeSpec = type as ElementTypeSpec;
			if (elementTypeSpec != null)
			{
				return InflatedTypeSpec.ContainsTypeParameter(elementTypeSpec.Element);
			}
			TypeSpec[] typeArguments = type.TypeArguments;
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (InflatedTypeSpec.ContainsTypeParameter(typeArguments[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00086FD4 File Offset: 0x000851D4
		public TypeParameterInflator CreateLocalInflator(IModuleContext context)
		{
			TypeSpec[] array = this.targs;
			TypeParameterSpec[] tparams;
			if (base.IsNested)
			{
				List<TypeSpec> list = null;
				List<TypeParameterSpec> list2 = null;
				TypeSpec declaringType = base.DeclaringType;
				do
				{
					if (declaringType.TypeArguments.Length != 0)
					{
						if (list == null)
						{
							list = new List<TypeSpec>();
							list2 = new List<TypeParameterSpec>();
							if (this.targs.Length != 0)
							{
								list.AddRange(this.targs);
								list2.AddRange(this.open_type.MemberDefinition.TypeParameters);
							}
						}
						list2.AddRange(declaringType.MemberDefinition.TypeParameters);
						list.AddRange(declaringType.TypeArguments);
					}
					declaringType = declaringType.DeclaringType;
				}
				while (declaringType != null);
				if (list != null)
				{
					array = list.ToArray();
					tparams = list2.ToArray();
				}
				else if (this.targs.Length == 0)
				{
					tparams = TypeParameterSpec.EmptyTypes;
				}
				else
				{
					tparams = this.open_type.MemberDefinition.TypeParameters;
				}
			}
			else if (this.targs.Length == 0)
			{
				tparams = TypeParameterSpec.EmptyTypes;
			}
			else
			{
				tparams = this.open_type.MemberDefinition.TypeParameters;
			}
			return new TypeParameterInflator(context, this, tparams, array);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000870D4 File Offset: 0x000852D4
		private Type CreateMetaInfo()
		{
			List<Type> list = new List<Type>();
			TypeSpec typeSpec = this;
			TypeSpec typeSpec2 = typeSpec;
			do
			{
				if (typeSpec.GetDefinition().IsGeneric)
				{
					List<Type> list2 = list;
					int index = 0;
					IEnumerable<Type> collection;
					if (typeSpec.TypeArguments == TypeSpec.EmptyTypes)
					{
						collection = from l in typeSpec.MemberDefinition.TypeParameters
						select l.GetMetaInfo();
					}
					else
					{
						collection = from l in typeSpec.TypeArguments
						select l.GetMetaInfo();
					}
					list2.InsertRange(index, collection);
				}
				typeSpec2 = typeSpec2.GetDefinition();
				typeSpec = typeSpec.DeclaringType;
			}
			while (typeSpec != null);
			return typeSpec2.GetMetaInfo().MakeGenericType(list.ToArray());
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0008718C File Offset: 0x0008538C
		public override ObsoleteAttribute GetAttributeObsolete()
		{
			return this.open_type.GetAttributeObsolete();
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0008719C File Offset: 0x0008539C
		protected override bool IsNotCLSCompliant(out bool attrValue)
		{
			if (base.IsNotCLSCompliant(out attrValue))
			{
				return true;
			}
			TypeSpec[] typeArguments = this.TypeArguments;
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i].MemberDefinition.CLSAttributeValue == false)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000871F2 File Offset: 0x000853F2
		public override TypeSpec GetDefinition()
		{
			return this.open_type;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000871FA File Offset: 0x000853FA
		public override Type GetMetaInfo()
		{
			if (this.info == null)
			{
				this.info = this.CreateMetaInfo();
			}
			return this.info;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00087216 File Offset: 0x00085416
		public override string GetSignatureForError()
		{
			if (this.IsNullableType)
			{
				return this.targs[0].GetSignatureForError() + "?";
			}
			return base.GetSignatureForError();
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x0008723E File Offset: 0x0008543E
		protected override string GetTypeNameSignature()
		{
			if (this.targs.Length == 0 || base.MemberDefinition is AnonymousTypeClass)
			{
				return null;
			}
			return "<" + TypeManager.CSharpName(this.targs) + ">";
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00087274 File Offset: 0x00085474
		public bool HasDynamicArgument()
		{
			for (int i = 0; i < this.targs.Length; i++)
			{
				TypeSpec typeSpec = this.targs[i];
				if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					return true;
				}
				if (typeSpec is InflatedTypeSpec)
				{
					if (((InflatedTypeSpec)typeSpec).HasDynamicArgument())
					{
						return true;
					}
				}
				else if (typeSpec.IsArray)
				{
					while (typeSpec.IsArray)
					{
						typeSpec = ((ArrayContainer)typeSpec).Element;
					}
					if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000872EC File Offset: 0x000854EC
		protected override void InitializeMemberCache(bool onlyTypes)
		{
			if (this.cache == null)
			{
				MemberCache cache = onlyTypes ? this.open_type.MemberCacheTypes : this.open_type.MemberCache;
				if (this.cache == null)
				{
					this.cache = new MemberCache(cache);
				}
			}
			TypeParameterInflator inflator = this.CreateLocalInflator(this.context);
			if ((this.state & MemberSpec.StateFlags.PendingMemberCacheMembers) == (MemberSpec.StateFlags)0)
			{
				this.open_type.MemberCacheTypes.InflateTypes(this.cache, inflator);
				if (this.open_type.Interfaces != null)
				{
					this.ifaces = new List<TypeSpec>(this.open_type.Interfaces.Count);
					foreach (TypeSpec type in this.open_type.Interfaces)
					{
						TypeSpec typeSpec = inflator.Inflate(type);
						if (typeSpec != null)
						{
							base.AddInterface(typeSpec);
						}
					}
				}
				if (this.open_type.BaseType == null)
				{
					if (base.IsClass)
					{
						this.state |= MemberSpec.StateFlags.PendingBaseTypeInflate;
					}
				}
				else
				{
					this.BaseType = inflator.Inflate(this.open_type.BaseType);
				}
			}
			else if ((this.state & MemberSpec.StateFlags.PendingBaseTypeInflate) != (MemberSpec.StateFlags)0)
			{
				if (this.open_type.BaseType == null)
				{
					return;
				}
				this.BaseType = inflator.Inflate(this.open_type.BaseType);
				this.state &= ~MemberSpec.StateFlags.PendingBaseTypeInflate;
			}
			if (onlyTypes)
			{
				this.state |= MemberSpec.StateFlags.PendingMemberCacheMembers;
				return;
			}
			TypeDefinition typeDefinition = this.open_type.MemberDefinition as TypeDefinition;
			if (typeDefinition != null && !typeDefinition.HasMembersDefined)
			{
				return;
			}
			if ((this.state & MemberSpec.StateFlags.PendingBaseTypeInflate) != (MemberSpec.StateFlags)0)
			{
				this.BaseType = inflator.Inflate(this.open_type.BaseType);
				this.state &= ~MemberSpec.StateFlags.PendingBaseTypeInflate;
			}
			this.state &= ~MemberSpec.StateFlags.PendingMemberCacheMembers;
			this.open_type.MemberCache.InflateMembers(this.cache, this.open_type, inflator);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00087508 File Offset: 0x00085708
		public override TypeSpec Mutate(TypeParameterMutator mutator)
		{
			TypeSpec[] array = this.TypeArguments;
			if (array != null)
			{
				array = mutator.Mutate(array);
			}
			TypeSpec typeSpec = base.DeclaringType;
			if (base.IsNested && base.DeclaringType.IsGenericOrParentIsGeneric)
			{
				typeSpec = mutator.Mutate(typeSpec);
			}
			if (array == this.TypeArguments && typeSpec == base.DeclaringType)
			{
				return this;
			}
			InflatedTypeSpec inflatedTypeSpec = (InflatedTypeSpec)base.MemberwiseClone();
			if (typeSpec != base.DeclaringType)
			{
				inflatedTypeSpec.declaringType = typeSpec;
				inflatedTypeSpec.state |= MemberSpec.StateFlags.PendingMetaInflate;
			}
			if (array != null)
			{
				inflatedTypeSpec.targs = array;
				inflatedTypeSpec.info = null;
			}
			return inflatedTypeSpec;
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0008759F File Offset: 0x0008579F
		[CompilerGenerated]
		private TypeParameterInflator <get_Constraints>b__8_0(InflatedTypeSpec l)
		{
			return l.CreateLocalInflator(this.context);
		}

		// Token: 0x04000A57 RID: 2647
		private TypeSpec[] targs;

		// Token: 0x04000A58 RID: 2648
		private TypeParameterSpec[] constraints;

		// Token: 0x04000A59 RID: 2649
		private readonly TypeSpec open_type;

		// Token: 0x04000A5A RID: 2650
		private readonly IModuleContext context;

		// Token: 0x020003C2 RID: 962
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002749 RID: 10057 RVA: 0x000BBEF9 File Offset: 0x000BA0F9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600274A RID: 10058 RVA: 0x00002CCC File Offset: 0x00000ECC
			public <>c()
			{
			}

			// Token: 0x0600274B RID: 10059 RVA: 0x000BBEE5 File Offset: 0x000BA0E5
			internal Type <CreateMetaInfo>b__27_0(TypeSpec l)
			{
				return l.GetMetaInfo();
			}

			// Token: 0x0600274C RID: 10060 RVA: 0x000BBEE5 File Offset: 0x000BA0E5
			internal Type <CreateMetaInfo>b__27_1(TypeParameterSpec l)
			{
				return l.GetMetaInfo();
			}

			// Token: 0x040010B0 RID: 4272
			public static readonly InflatedTypeSpec.<>c <>9 = new InflatedTypeSpec.<>c();

			// Token: 0x040010B1 RID: 4273
			public static Func<TypeSpec, Type> <>9__27_0;

			// Token: 0x040010B2 RID: 4274
			public static Func<TypeParameterSpec, Type> <>9__27_1;
		}
	}
}
