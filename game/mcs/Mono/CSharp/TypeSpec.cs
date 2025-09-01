using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x020002DE RID: 734
	public class TypeSpec : MemberSpec
	{
		// Token: 0x060022C8 RID: 8904 RVA: 0x000AB6D0 File Offset: 0x000A98D0
		static TypeSpec()
		{
			Assembly assembly = typeof(object).Assembly;
			TypeSpec.TypeBuilder = assembly.GetType("System.Reflection.Emit.TypeBuilder");
			TypeSpec.GenericTypeBuilder = assembly.GetType("System.Reflection.MonoGenericClass");
			if (TypeSpec.GenericTypeBuilder == null)
			{
				TypeSpec.GenericTypeBuilder = assembly.GetType("System.Reflection.Emit.TypeBuilderInstantiation");
			}
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000AB72F File Offset: 0x000A992F
		public TypeSpec(MemberKind kind, TypeSpec declaringType, ITypeDefinition definition, Type info, Modifiers modifiers) : base(kind, declaringType, definition, modifiers)
		{
			this.declaringType = declaringType;
			this.info = info;
			if (definition != null && definition.TypeParametersCount > 0)
			{
				this.state |= MemberSpec.StateFlags.IsGeneric;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x000AB769 File Offset: 0x000A9969
		public override int Arity
		{
			get
			{
				return this.MemberDefinition.TypeParametersCount;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x000AB776 File Offset: 0x000A9976
		// (set) Token: 0x060022CC RID: 8908 RVA: 0x000AB77E File Offset: 0x000A997E
		public virtual TypeSpec BaseType
		{
			get
			{
				return this.base_type;
			}
			set
			{
				this.base_type = value;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x000022F4 File Offset: 0x000004F4
		public virtual BuiltinTypeSpec.Type BuiltinType
		{
			get
			{
				return BuiltinTypeSpec.Type.None;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x000AB787 File Offset: 0x000A9987
		public bool HasDynamicElement
		{
			get
			{
				return (this.state & MemberSpec.StateFlags.HasDynamicElement) > (MemberSpec.StateFlags)0;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000AB798 File Offset: 0x000A9998
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x000AB7EE File Offset: 0x000A99EE
		public virtual IList<TypeSpec> Interfaces
		{
			get
			{
				if ((this.state & MemberSpec.StateFlags.InterfacesImported) == (MemberSpec.StateFlags)0)
				{
					this.state |= MemberSpec.StateFlags.InterfacesImported;
					ImportedTypeDefinition importedTypeDefinition = this.MemberDefinition as ImportedTypeDefinition;
					if (importedTypeDefinition != null && this.Kind != MemberKind.MissingType)
					{
						importedTypeDefinition.DefineInterfaces(this);
					}
				}
				return this.ifaces;
			}
			set
			{
				this.ifaces = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x000AB7F7 File Offset: 0x000A99F7
		public bool IsArray
		{
			get
			{
				return this.Kind == MemberKind.ArrayType;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x000AB808 File Offset: 0x000A9A08
		public bool IsAttribute
		{
			get
			{
				if (!this.IsClass)
				{
					return false;
				}
				TypeSpec typeSpec = this;
				while (typeSpec.BuiltinType != BuiltinTypeSpec.Type.Attribute)
				{
					if (typeSpec.IsGeneric)
					{
						return false;
					}
					typeSpec = typeSpec.base_type;
					if (typeSpec == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x000AB842 File Offset: 0x000A9A42
		public bool IsInterface
		{
			get
			{
				return this.Kind == MemberKind.Interface;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x000AB851 File Offset: 0x000A9A51
		public bool IsClass
		{
			get
			{
				return this.Kind == MemberKind.Class;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x000AB860 File Offset: 0x000A9A60
		public bool IsConstantCompatible
		{
			get
			{
				if ((this.Kind & (MemberKind.Class | MemberKind.Delegate | MemberKind.Enum | MemberKind.Interface | MemberKind.ArrayType)) != (MemberKind)0)
				{
					return true;
				}
				switch (this.BuiltinType)
				{
				case BuiltinTypeSpec.Type.FirstPrimitive:
				case BuiltinTypeSpec.Type.Byte:
				case BuiltinTypeSpec.Type.SByte:
				case BuiltinTypeSpec.Type.Char:
				case BuiltinTypeSpec.Type.Short:
				case BuiltinTypeSpec.Type.UShort:
				case BuiltinTypeSpec.Type.Int:
				case BuiltinTypeSpec.Type.UInt:
				case BuiltinTypeSpec.Type.Long:
				case BuiltinTypeSpec.Type.ULong:
				case BuiltinTypeSpec.Type.Float:
				case BuiltinTypeSpec.Type.Double:
				case BuiltinTypeSpec.Type.Decimal:
				case BuiltinTypeSpec.Type.Dynamic:
					return true;
				}
				return false;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x000AB8D5 File Offset: 0x000A9AD5
		public bool IsDelegate
		{
			get
			{
				return this.Kind == MemberKind.Delegate;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x000022F4 File Offset: 0x000004F4
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x000AB8E4 File Offset: 0x000A9AE4
		public virtual bool IsExpressionTreeType
		{
			get
			{
				return false;
			}
			set
			{
				this.state = (value ? (this.state | MemberSpec.StateFlags.InflatedExpressionType) : (this.state & ~MemberSpec.StateFlags.InflatedExpressionType));
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x000AB909 File Offset: 0x000A9B09
		public bool IsEnum
		{
			get
			{
				return this.Kind == MemberKind.Enum;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x000022F4 File Offset: 0x000004F4
		// (set) Token: 0x060022DB RID: 8923 RVA: 0x000AB918 File Offset: 0x000A9B18
		public virtual bool IsArrayGenericInterface
		{
			get
			{
				return false;
			}
			set
			{
				this.state = (value ? (this.state | MemberSpec.StateFlags.GenericIterateInterface) : (this.state & ~MemberSpec.StateFlags.GenericIterateInterface));
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x000022F4 File Offset: 0x000004F4
		// (set) Token: 0x060022DD RID: 8925 RVA: 0x000AB93D File Offset: 0x000A9B3D
		public virtual bool IsGenericTask
		{
			get
			{
				return false;
			}
			set
			{
				this.state = (value ? (this.state | MemberSpec.StateFlags.GenericTask) : (this.state & ~MemberSpec.StateFlags.GenericTask));
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x000AB964 File Offset: 0x000A9B64
		public bool IsGenericOrParentIsGeneric
		{
			get
			{
				TypeSpec typeSpec = this;
				while (!typeSpec.IsGeneric)
				{
					typeSpec = typeSpec.declaringType;
					if (typeSpec == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x000AB988 File Offset: 0x000A9B88
		public bool IsGenericParameter
		{
			get
			{
				return this.Kind == MemberKind.TypeParameter;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x000022F4 File Offset: 0x000004F4
		// (set) Token: 0x060022E1 RID: 8929 RVA: 0x000AB997 File Offset: 0x000A9B97
		public virtual bool IsNullableType
		{
			get
			{
				return false;
			}
			set
			{
				this.state = (value ? (this.state | MemberSpec.StateFlags.InflatedNullableType) : (this.state & ~MemberSpec.StateFlags.InflatedNullableType));
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000AB9BC File Offset: 0x000A9BBC
		public bool IsNested
		{
			get
			{
				return this.declaringType != null && this.Kind != MemberKind.TypeParameter;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000AB9D8 File Offset: 0x000A9BD8
		public bool IsPointer
		{
			get
			{
				return this.Kind == MemberKind.PointerType;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x0008D40A File Offset: 0x0008B60A
		public bool IsSealed
		{
			get
			{
				return (base.Modifiers & Modifiers.SEALED) > (Modifiers)0;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x000AB9E7 File Offset: 0x000A9BE7
		// (set) Token: 0x060022E6 RID: 8934 RVA: 0x000AB9F8 File Offset: 0x000A9BF8
		public bool IsSpecialRuntimeType
		{
			get
			{
				return (this.state & MemberSpec.StateFlags.SpecialRuntimeType) > (MemberSpec.StateFlags)0;
			}
			set
			{
				this.state = (value ? (this.state | MemberSpec.StateFlags.SpecialRuntimeType) : (this.state & ~MemberSpec.StateFlags.SpecialRuntimeType));
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x000ABA1D File Offset: 0x000A9C1D
		public bool IsStruct
		{
			get
			{
				return this.Kind == MemberKind.Struct;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x000ABA2C File Offset: 0x000A9C2C
		public bool IsStructOrEnum
		{
			get
			{
				return (this.Kind & (MemberKind.Struct | MemberKind.Enum)) > (MemberKind)0;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x000ABA40 File Offset: 0x000A9C40
		public bool IsTypeBuilder
		{
			get
			{
				Type type = this.GetMetaInfo().GetType();
				return type == TypeSpec.TypeBuilder || type == TypeSpec.GenericTypeBuilder;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x000ABA6C File Offset: 0x000A9C6C
		public bool IsUnmanaged
		{
			get
			{
				if (this.IsPointer)
				{
					return ((ElementTypeSpec)this).Element.IsUnmanaged;
				}
				TypeDefinition typeDefinition = this.MemberDefinition as TypeDefinition;
				if (typeDefinition != null)
				{
					return typeDefinition.IsUnmanagedType();
				}
				return this.Kind == MemberKind.Void || (this.Kind != MemberKind.TypeParameter && (!this.IsNested || !base.DeclaringType.IsGenericOrParentIsGeneric) && TypeSpec.IsValueType(this));
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x000ABAE3 File Offset: 0x000A9CE3
		// (set) Token: 0x060022EC RID: 8940 RVA: 0x000ABB08 File Offset: 0x000A9D08
		public MemberCache MemberCache
		{
			get
			{
				if (this.cache == null || (this.state & MemberSpec.StateFlags.PendingMemberCacheMembers) != (MemberSpec.StateFlags)0)
				{
					this.InitializeMemberCache(false);
				}
				return this.cache;
			}
			set
			{
				if (this.cache != null)
				{
					throw new InternalErrorException("Membercache reset");
				}
				this.cache = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x000ABB24 File Offset: 0x000A9D24
		public MemberCache MemberCacheTypes
		{
			get
			{
				if (this.cache == null)
				{
					this.InitializeMemberCache(true);
				}
				return this.cache;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x000ABB3B File Offset: 0x000A9D3B
		public new ITypeDefinition MemberDefinition
		{
			get
			{
				return (ITypeDefinition)this.definition;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x000ABB48 File Offset: 0x000A9D48
		public virtual TypeSpec[] TypeArguments
		{
			get
			{
				return TypeSpec.EmptyTypes;
			}
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000ABB50 File Offset: 0x000A9D50
		public virtual bool AddInterface(TypeSpec iface)
		{
			if ((this.state & MemberSpec.StateFlags.InterfacesExpanded) != (MemberSpec.StateFlags)0)
			{
				throw new InternalErrorException("Modifying expanded interface list");
			}
			if (this.ifaces == null)
			{
				this.ifaces = new List<TypeSpec>
				{
					iface
				};
				return true;
			}
			if (!this.ifaces.Contains(iface))
			{
				this.ifaces.Add(iface);
				return true;
			}
			return false;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000ABBB0 File Offset: 0x000A9DB0
		public bool AddInterfaceDefined(TypeSpec iface)
		{
			if (!this.AddInterface(iface))
			{
				return false;
			}
			if (this.inflated_instances != null)
			{
				InflatedTypeSpec[] array = this.inflated_instances.Values.ToArray<InflatedTypeSpec>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].AddInterface(iface);
				}
			}
			return true;
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000ABBFC File Offset: 0x000A9DFC
		public static TypeSpec[] GetAllTypeArguments(TypeSpec type)
		{
			IList<TypeSpec> list = TypeSpec.EmptyTypes;
			do
			{
				if (type.Arity > 0)
				{
					if (list.Count == 0)
					{
						list = type.TypeArguments;
					}
					else
					{
						List<TypeSpec> list2 = (list as List<TypeSpec>) ?? new List<TypeSpec>(list);
						list2.AddRange(type.TypeArguments);
						list = list2;
					}
				}
				type = type.declaringType;
			}
			while (type != null);
			return (list as TypeSpec[]) ?? ((List<TypeSpec>)list).ToArray();
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000ABC68 File Offset: 0x000A9E68
		public AttributeUsageAttribute GetAttributeUsage(PredefinedAttribute pa)
		{
			if (this.Kind != MemberKind.Class)
			{
				throw new InternalErrorException();
			}
			if (!pa.IsDefined)
			{
				return Attribute.DefaultUsageAttribute;
			}
			AttributeUsageAttribute attributeUsageAttribute = null;
			for (TypeSpec typeSpec = this; typeSpec != null; typeSpec = typeSpec.BaseType)
			{
				attributeUsageAttribute = typeSpec.MemberDefinition.GetAttributeUsage(pa);
				if (attributeUsageAttribute != null)
				{
					break;
				}
			}
			return attributeUsageAttribute;
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000ABCB7 File Offset: 0x000A9EB7
		public virtual Type GetMetaInfo()
		{
			return this.info;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x00005936 File Offset: 0x00003B36
		public virtual TypeSpec GetDefinition()
		{
			return this;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000ABCBF File Offset: 0x000A9EBF
		public sealed override string GetSignatureForDocumentation()
		{
			return this.GetSignatureForDocumentation(false);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000ABCC8 File Offset: 0x000A9EC8
		public virtual string GetSignatureForDocumentation(bool explicitName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsNested)
			{
				stringBuilder.Append(base.DeclaringType.GetSignatureForDocumentation(explicitName));
			}
			else if (this.MemberDefinition.Namespace != null)
			{
				stringBuilder.Append(explicitName ? this.MemberDefinition.Namespace.Replace('.', '#') : this.MemberDefinition.Namespace);
			}
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Append(explicitName ? "#" : ".");
			}
			stringBuilder.Append(this.Name);
			if (this.Arity > 0)
			{
				if (this is InflatedTypeSpec)
				{
					stringBuilder.Append("{");
					for (int i = 0; i < this.Arity; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(this.TypeArguments[i].GetSignatureForDocumentation(explicitName));
					}
					stringBuilder.Append("}");
				}
				else
				{
					stringBuilder.Append("`");
					stringBuilder.Append(this.Arity.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000ABDE4 File Offset: 0x000A9FE4
		public override string GetSignatureForError()
		{
			string text;
			if (this.IsNested)
			{
				text = base.DeclaringType.GetSignatureForError();
			}
			else
			{
				if (this.MemberDefinition is AnonymousTypeClass)
				{
					return ((AnonymousTypeClass)this.MemberDefinition).GetSignatureForError();
				}
				text = this.MemberDefinition.Namespace;
			}
			if (!string.IsNullOrEmpty(text))
			{
				text += ".";
			}
			return text + this.Name + this.GetTypeNameSignature();
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000ABE57 File Offset: 0x000AA057
		public string GetSignatureForErrorIncludingAssemblyName()
		{
			return string.Format("{0} [{1}]", this.GetSignatureForError(), this.MemberDefinition.DeclaringAssembly.FullName);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000ABE79 File Offset: 0x000AA079
		protected virtual string GetTypeNameSignature()
		{
			if (!base.IsGeneric)
			{
				return null;
			}
			return "<" + TypeManager.CSharpName(this.MemberDefinition.TypeParameters) + ">";
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000ABEA4 File Offset: 0x000AA0A4
		public bool ImplementsInterface(TypeSpec iface, bool variantly)
		{
			IList<TypeSpec> interfaces = this.Interfaces;
			if (interfaces != null)
			{
				for (int i = 0; i < interfaces.Count; i++)
				{
					if (TypeSpecComparer.IsEqual(interfaces[i], iface))
					{
						return true;
					}
					if (variantly && TypeSpecComparer.Variant.IsEqual(interfaces[i], iface))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x000ABEF4 File Offset: 0x000AA0F4
		protected virtual void InitializeMemberCache(bool onlyTypes)
		{
			try
			{
				this.MemberDefinition.LoadMembers(this, onlyTypes, ref this.cache);
			}
			catch (Exception exception)
			{
				throw new InternalErrorException(exception, "Unexpected error when loading type `{0}'", new object[]
				{
					this.GetSignatureForError()
				});
			}
			if (onlyTypes)
			{
				this.state |= MemberSpec.StateFlags.PendingMemberCacheMembers;
				return;
			}
			this.state &= ~MemberSpec.StateFlags.PendingMemberCacheMembers;
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000ABF68 File Offset: 0x000AA168
		public static bool IsBaseClass(TypeSpec type, TypeSpec baseClass, bool dynamicIsObject)
		{
			if (dynamicIsObject && baseClass.IsGeneric)
			{
				for (type = type.BaseType; type != null; type = type.BaseType)
				{
					if (TypeSpecComparer.IsEqual(type, baseClass))
					{
						return true;
					}
				}
				return false;
			}
			while (type != null)
			{
				type = type.BaseType;
				if (type == baseClass)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000ABFB4 File Offset: 0x000AA1B4
		public static bool IsReferenceType(TypeSpec t)
		{
			MemberKind kind = t.Kind;
			if (kind <= MemberKind.TypeParameter)
			{
				if (kind != MemberKind.Struct && kind != MemberKind.Enum)
				{
					if (kind != MemberKind.TypeParameter)
					{
						return true;
					}
					return ((TypeParameterSpec)t).IsReferenceType;
				}
			}
			else if (kind != MemberKind.PointerType)
			{
				if (kind == MemberKind.InternalCompilerType)
				{
					return t == InternalType.NullLiteral || t.BuiltinType == BuiltinTypeSpec.Type.Dynamic;
				}
				if (kind != MemberKind.Void)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000AC028 File Offset: 0x000AA228
		public static bool IsNonNullableValueType(TypeSpec t)
		{
			MemberKind kind = t.Kind;
			if (kind != MemberKind.Struct)
			{
				return kind == MemberKind.Enum || (kind == MemberKind.TypeParameter && ((TypeParameterSpec)t).IsValueType);
			}
			return !t.IsNullableType;
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000AC070 File Offset: 0x000AA270
		public static bool IsValueType(TypeSpec t)
		{
			MemberKind kind = t.Kind;
			return kind == MemberKind.Struct || kind == MemberKind.Enum || (kind == MemberKind.TypeParameter && ((TypeParameterSpec)t).IsValueType);
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000AC0AC File Offset: 0x000AA2AC
		public override MemberSpec InflateMember(TypeParameterInflator inflator)
		{
			TypeSpec[] targs = base.IsGeneric ? this.MemberDefinition.TypeParameters : TypeSpec.EmptyTypes;
			if (base.DeclaringType == inflator.TypeInstance)
			{
				return this.MakeGenericType(inflator.Context, targs);
			}
			return new InflatedTypeSpec(inflator.Context, this, inflator.TypeInstance, targs);
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000AC108 File Offset: 0x000AA308
		public InflatedTypeSpec MakeGenericType(IModuleContext context, TypeSpec[] targs)
		{
			if (targs.Length == 0 && !this.IsNested)
			{
				throw new ArgumentException("Empty type arguments for type " + this.GetSignatureForError());
			}
			InflatedTypeSpec inflatedTypeSpec;
			if (this.inflated_instances == null)
			{
				this.inflated_instances = new Dictionary<TypeSpec[], InflatedTypeSpec>(TypeSpecComparer.Default);
				if (this.IsNested)
				{
					inflatedTypeSpec = (this as InflatedTypeSpec);
					if (inflatedTypeSpec != null)
					{
						this.inflated_instances.Add(this.TypeArguments, inflatedTypeSpec);
					}
				}
			}
			if (!this.inflated_instances.TryGetValue(targs, out inflatedTypeSpec))
			{
				if (this.GetDefinition() != this && !this.IsNested)
				{
					throw new InternalErrorException("`{0}' must be type definition or nested non-inflated type to MakeGenericType", new object[]
					{
						this.GetSignatureForError()
					});
				}
				inflatedTypeSpec = new InflatedTypeSpec(context, this, this.declaringType, targs);
				this.inflated_instances.Add(targs, inflatedTypeSpec);
			}
			return inflatedTypeSpec;
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x00005936 File Offset: 0x00003B36
		public virtual TypeSpec Mutate(TypeParameterMutator mutator)
		{
			return this;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x000AC1CC File Offset: 0x000AA3CC
		public override List<MissingTypeSpecReference> ResolveMissingDependencies(MemberSpec caller)
		{
			List<MissingTypeSpecReference> list = null;
			if (this.Kind == MemberKind.MissingType)
			{
				list = new List<MissingTypeSpecReference>();
				list.Add(new MissingTypeSpecReference(this, caller));
				return list;
			}
			foreach (TypeSpec typeSpec in this.TypeArguments)
			{
				if (typeSpec.Kind == MemberKind.MissingType)
				{
					if (list == null)
					{
						list = new List<MissingTypeSpecReference>();
					}
					list.Add(new MissingTypeSpecReference(typeSpec, caller));
				}
			}
			if (this.Interfaces != null)
			{
				foreach (TypeSpec typeSpec2 in this.Interfaces)
				{
					if (typeSpec2.Kind == MemberKind.MissingType)
					{
						if (list == null)
						{
							list = new List<MissingTypeSpecReference>();
						}
						list.Add(new MissingTypeSpecReference(typeSpec2, caller));
					}
				}
			}
			if (this.MemberDefinition.TypeParametersCount > 0)
			{
				TypeParameterSpec[] typeParameters = this.MemberDefinition.TypeParameters;
				for (int i = 0; i < typeParameters.Length; i++)
				{
					List<MissingTypeSpecReference> missingDependencies = typeParameters[i].GetMissingDependencies(this);
					if (missingDependencies != null)
					{
						if (list == null)
						{
							list = new List<MissingTypeSpecReference>();
						}
						list.AddRange(missingDependencies);
					}
				}
			}
			if (list != null || this.BaseType == null)
			{
				return list;
			}
			return this.BaseType.ResolveMissingDependencies(this);
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x000AC30C File Offset: 0x000AA50C
		public void SetMetaInfo(Type info)
		{
			if (this.info != null)
			{
				throw new InternalErrorException("MetaInfo reset");
			}
			this.info = info;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000AC328 File Offset: 0x000AA528
		public void SetExtensionMethodContainer()
		{
			this.modifiers |= Modifiers.METHOD_EXTENSION;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000AC33C File Offset: 0x000AA53C
		public void UpdateInflatedInstancesBaseType()
		{
			if (this.inflated_instances == null)
			{
				return;
			}
			foreach (KeyValuePair<TypeSpec[], InflatedTypeSpec> keyValuePair in this.inflated_instances)
			{
				keyValuePair.Value.BaseType = this.base_type;
			}
		}

		// Token: 0x04000D65 RID: 3429
		protected Type info;

		// Token: 0x04000D66 RID: 3430
		protected MemberCache cache;

		// Token: 0x04000D67 RID: 3431
		protected IList<TypeSpec> ifaces;

		// Token: 0x04000D68 RID: 3432
		private TypeSpec base_type;

		// Token: 0x04000D69 RID: 3433
		private Dictionary<TypeSpec[], InflatedTypeSpec> inflated_instances;

		// Token: 0x04000D6A RID: 3434
		public static readonly TypeSpec[] EmptyTypes = new TypeSpec[0];

		// Token: 0x04000D6B RID: 3435
		private static readonly Type TypeBuilder;

		// Token: 0x04000D6C RID: 3436
		private static readonly Type GenericTypeBuilder;
	}
}
