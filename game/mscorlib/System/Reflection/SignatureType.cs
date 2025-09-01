using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020008C8 RID: 2248
	internal abstract class SignatureType : Type
	{
		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x000040F7 File Offset: 0x000022F7
		public sealed override bool IsSignatureType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06004A7A RID: 19066
		public abstract override bool IsTypeDefinition { get; }

		// Token: 0x06004A7B RID: 19067
		protected abstract override bool HasElementTypeImpl();

		// Token: 0x06004A7C RID: 19068
		protected abstract override bool IsArrayImpl();

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06004A7D RID: 19069
		public abstract override bool IsSZArray { get; }

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06004A7E RID: 19070
		public abstract override bool IsVariableBoundArray { get; }

		// Token: 0x06004A7F RID: 19071
		protected abstract override bool IsByRefImpl();

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06004A80 RID: 19072
		public abstract override bool IsByRefLike { get; }

		// Token: 0x06004A81 RID: 19073
		protected abstract override bool IsPointerImpl();

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x000EF938 File Offset: 0x000EDB38
		public sealed override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition || this.IsConstructedGenericType;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004A83 RID: 19075
		public abstract override bool IsGenericTypeDefinition { get; }

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004A84 RID: 19076
		public abstract override bool IsConstructedGenericType { get; }

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004A85 RID: 19077
		public abstract override bool IsGenericParameter { get; }

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004A86 RID: 19078
		public abstract override bool IsGenericTypeParameter { get; }

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004A87 RID: 19079
		public abstract override bool IsGenericMethodParameter { get; }

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004A88 RID: 19080
		public abstract override bool ContainsGenericParameters { get; }

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x0004720C File Offset: 0x0004540C
		public sealed override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x000EF94A File Offset: 0x000EDB4A
		public sealed override Type MakeArrayType()
		{
			return new SignatureArrayType(this, 1, false);
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x000EF954 File Offset: 0x000EDB54
		public sealed override Type MakeArrayType(int rank)
		{
			if (rank <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			return new SignatureArrayType(this, rank, true);
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x000EF968 File Offset: 0x000EDB68
		public sealed override Type MakeByRefType()
		{
			return new SignatureByRefType(this);
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x000EF970 File Offset: 0x000EDB70
		public sealed override Type MakePointerType()
		{
			return new SignaturePointerType(this);
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x000EF984 File Offset: 0x000EDB84
		public sealed override Type GetElementType()
		{
			return this.ElementType;
		}

		// Token: 0x06004A90 RID: 19088
		public abstract override int GetArrayRank();

		// Token: 0x06004A91 RID: 19089
		public abstract override Type GetGenericTypeDefinition();

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004A92 RID: 19090
		public abstract override Type[] GenericTypeArguments { get; }

		// Token: 0x06004A93 RID: 19091
		public abstract override Type[] GetGenericArguments();

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06004A94 RID: 19092
		public abstract override int GenericParameterPosition { get; }

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06004A95 RID: 19093
		internal abstract SignatureType ElementType { get; }

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06004A96 RID: 19094 RVA: 0x0000270D File Offset: 0x0000090D
		public sealed override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06004A97 RID: 19095
		public abstract override string Name { get; }

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06004A98 RID: 19096
		public abstract override string Namespace { get; }

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06004A99 RID: 19097 RVA: 0x0000AF5E File Offset: 0x0000915E
		public sealed override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x0000AF5E File Offset: 0x0000915E
		public sealed override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A9B RID: 19099
		public abstract override string ToString();

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06004A9C RID: 19100 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Assembly Assembly
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06004A9D RID: 19101 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Module Module
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06004A9E RID: 19102 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type ReflectedType
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06004A9F RID: 19103 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type BaseType
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type[] GetInterfaces()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsAssignableFrom(Type c)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06004AA2 RID: 19106 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override int MetadataToken
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type DeclaringType
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004AA5 RID: 19109 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override MethodBase DeclaringMethod
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type[] GetGenericParameterConstraints()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004AA7 RID: 19111 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsEnumDefined(object value)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override string GetEnumName(object value)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override string[] GetEnumNames()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type GetEnumUnderlyingType()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Array GetEnumValues()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004AAD RID: 19117 RVA: 0x000EF98C File Offset: 0x000EDB8C
		public sealed override Guid GUID
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override TypeCode GetTypeCodeImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override TypeAttributes GetAttributeFlagsImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override MethodInfo GetMethodImpl(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override MemberInfo[] GetDefaultMembers()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override EventInfo[] GetEvents()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override bool IsCOMObjectImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override bool IsPrimitiveImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004ACB RID: 19147 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x000EF9A4 File Offset: 0x000EDBA4
		public sealed override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override bool IsContextfulImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsEnum
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsEquivalentTo(Type other)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsInstanceOfType(object o)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override bool IsMarshalByRefImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004AD3 RID: 19155 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsSecurityCritical
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004AD4 RID: 19156 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsSecurityTransparent
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06004AD6 RID: 19158 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsSerializable
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override bool IsSubclassOf(Type c)
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x000EF978 File Offset: 0x000EDB78
		protected sealed override bool IsValueTypeImpl()
		{
			throw new NotSupportedException("This method is not supported on signature types.");
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06004AD9 RID: 19161 RVA: 0x000EF978 File Offset: 0x000EDB78
		public sealed override StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06004ADA RID: 19162 RVA: 0x000EF9BC File Offset: 0x000EDBBC
		public sealed override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException("This method is not supported on signature types.");
			}
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x000EF9D3 File Offset: 0x000EDBD3
		protected SignatureType()
		{
		}
	}
}
