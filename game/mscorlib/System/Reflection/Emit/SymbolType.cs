using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200090D RID: 2317
	[StructLayout(LayoutKind.Sequential)]
	internal abstract class SymbolType : TypeInfo
	{
		// Token: 0x06004E5E RID: 20062 RVA: 0x00057EED File Offset: 0x000560ED
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004E5F RID: 20063 RVA: 0x000F6440 File Offset: 0x000F4640
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
			}
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x000F6440 File Offset: 0x000F4640
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004E61 RID: 20065 RVA: 0x000F6454 File Offset: 0x000F4654
		public override Module Module
		{
			get
			{
				Type baseType = this.m_baseType;
				while (baseType is SymbolType)
				{
					baseType = ((SymbolType)baseType).m_baseType;
				}
				return baseType.Module;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004E62 RID: 20066 RVA: 0x000F6484 File Offset: 0x000F4684
		public override Assembly Assembly
		{
			get
			{
				Type baseType = this.m_baseType;
				while (baseType is SymbolType)
				{
					baseType = ((SymbolType)baseType).m_baseType;
				}
				return baseType.Assembly;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004E63 RID: 20067 RVA: 0x000F6440 File Offset: 0x000F4640
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x000F64B4 File Offset: 0x000F46B4
		public override string Namespace
		{
			get
			{
				return this.m_baseType.Namespace;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004E65 RID: 20069 RVA: 0x000F64C1 File Offset: 0x000F46C1
		public override Type BaseType
		{
			get
			{
				return typeof(Array);
			}
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x000F6440 File Offset: 0x000F4640
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x000F6440 File Offset: 0x000F4640
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x000F6440 File Offset: 0x000F4640
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x000F6440 File Offset: 0x000F4640
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x000F6440 File Offset: 0x000F4640
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x000F6440 File Offset: 0x000F4640
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x000F6440 File Offset: 0x000F4640
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x000F6440 File Offset: 0x000F4640
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x000F6440 File Offset: 0x000F4640
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x000F6440 File Offset: 0x000F4640
		public override EventInfo[] GetEvents()
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x000F6440 File Offset: 0x000F4640
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x000F6440 File Offset: 0x000F4640
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x000F6440 File Offset: 0x000F4640
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x000F6440 File Offset: 0x000F4640
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x000F6440 File Offset: 0x000F4640
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x000F6440 File Offset: 0x000F4640
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x000F6440 File Offset: 0x000F4640
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x000F6440 File Offset: 0x000F4640
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x000F64D0 File Offset: 0x000F46D0
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			Type baseType = this.m_baseType;
			while (baseType is SymbolType)
			{
				baseType = ((SymbolType)baseType).m_baseType;
			}
			return baseType.Attributes;
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsValueTypeImpl()
		{
			return false;
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004E7C RID: 20092 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x000F6500 File Offset: 0x000F4700
		public override Type GetElementType()
		{
			return this.m_baseType;
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x000F6508 File Offset: 0x000F4708
		protected override bool HasElementTypeImpl()
		{
			return this.m_baseType != null;
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x000F6440 File Offset: 0x000F4640
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x000F6440 File Offset: 0x000F4640
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x000F6440 File Offset: 0x000F4640
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("Not supported in a non-reflected type."));
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x000F6516 File Offset: 0x000F4716
		internal SymbolType(Type elementType)
		{
			this.m_baseType = elementType;
		}

		// Token: 0x06004E83 RID: 20099
		internal abstract string FormatName(string elementName);

		// Token: 0x06004E84 RID: 20100 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x000F6525 File Offset: 0x000F4725
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x000F652E File Offset: 0x000F472E
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x000F6541 File Offset: 0x000F4741
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x000F6549 File Offset: 0x000F4749
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x000F6551 File Offset: 0x000F4751
		public override string ToString()
		{
			return this.FormatName(this.m_baseType.ToString());
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06004E8C RID: 20108 RVA: 0x000F6564 File Offset: 0x000F4764
		public override string AssemblyQualifiedName
		{
			get
			{
				string text = this.FormatName(this.m_baseType.FullName);
				if (text == null)
				{
					return null;
				}
				return text + ", " + this.m_baseType.Assembly.FullName;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06004E8D RID: 20109 RVA: 0x000F65A3 File Offset: 0x000F47A3
		public override string FullName
		{
			get
			{
				return this.FormatName(this.m_baseType.FullName);
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004E8E RID: 20110 RVA: 0x000F65B6 File Offset: 0x000F47B6
		public override string Name
		{
			get
			{
				return this.FormatName(this.m_baseType.Name);
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004E8F RID: 20111 RVA: 0x0000270D File Offset: 0x0000090D
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004E90 RID: 20112 RVA: 0x000F65C9 File Offset: 0x000F47C9
		internal override bool IsUserType
		{
			get
			{
				return this.m_baseType.IsUserType;
			}
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x000F65D6 File Offset: 0x000F47D6
		internal override Type RuntimeResolve()
		{
			return this.InternalResolve();
		}

		// Token: 0x040030D0 RID: 12496
		internal Type m_baseType;
	}
}
