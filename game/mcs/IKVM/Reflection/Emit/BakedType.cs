using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000FA RID: 250
	internal sealed class BakedType : TypeInfo
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x0002C9B1 File Offset: 0x0002ABB1
		internal BakedType(TypeBuilder typeBuilder) : base(typeBuilder)
		{
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0002C9BA File Offset: 0x0002ABBA
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.underlyingType.AssemblyQualifiedName;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002C9C7 File Offset: 0x0002ABC7
		public override Type BaseType
		{
			get
			{
				return this.underlyingType.BaseType;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0002C9D4 File Offset: 0x0002ABD4
		internal override TypeName TypeName
		{
			get
			{
				return this.underlyingType.TypeName;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002C9E1 File Offset: 0x0002ABE1
		public override string Name
		{
			get
			{
				return TypeNameParser.Escape(this.underlyingType.__Name);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00009EDD File Offset: 0x000080DD
		public override string FullName
		{
			get
			{
				return base.GetFullName();
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0002C9F3 File Offset: 0x0002ABF3
		public override TypeAttributes Attributes
		{
			get
			{
				return this.underlyingType.Attributes;
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002CA00 File Offset: 0x0002AC00
		public override Type[] __GetDeclaredInterfaces()
		{
			return this.underlyingType.__GetDeclaredInterfaces();
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002CA0D File Offset: 0x0002AC0D
		public override MethodBase[] __GetDeclaredMethods()
		{
			return this.underlyingType.__GetDeclaredMethods();
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002CA1A File Offset: 0x0002AC1A
		public override __MethodImplMap __GetMethodImplMap()
		{
			return this.underlyingType.__GetMethodImplMap();
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002CA27 File Offset: 0x0002AC27
		public override FieldInfo[] __GetDeclaredFields()
		{
			return this.underlyingType.__GetDeclaredFields();
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002CA34 File Offset: 0x0002AC34
		public override EventInfo[] __GetDeclaredEvents()
		{
			return this.underlyingType.__GetDeclaredEvents();
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002CA41 File Offset: 0x0002AC41
		public override PropertyInfo[] __GetDeclaredProperties()
		{
			return this.underlyingType.__GetDeclaredProperties();
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002CA4E File Offset: 0x0002AC4E
		public override Type[] __GetDeclaredTypes()
		{
			return this.underlyingType.__GetDeclaredTypes();
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0002CA5B File Offset: 0x0002AC5B
		public override Type DeclaringType
		{
			get
			{
				return this.underlyingType.DeclaringType;
			}
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002CA68 File Offset: 0x0002AC68
		public override bool __GetLayout(out int packingSize, out int typeSize)
		{
			return this.underlyingType.__GetLayout(out packingSize, out typeSize);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002CA77 File Offset: 0x0002AC77
		public override Type[] GetGenericArguments()
		{
			return this.underlyingType.GetGenericArguments();
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002CA84 File Offset: 0x0002AC84
		internal override Type GetGenericTypeArgument(int index)
		{
			return this.underlyingType.GetGenericTypeArgument(index);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002CA92 File Offset: 0x0002AC92
		public override CustomModifiers[] __GetGenericArgumentsCustomModifiers()
		{
			return this.underlyingType.__GetGenericArgumentsCustomModifiers();
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002CA9F File Offset: 0x0002AC9F
		public override bool IsGenericType
		{
			get
			{
				return this.underlyingType.IsGenericType;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0002CAAC File Offset: 0x0002ACAC
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return this.underlyingType.IsGenericTypeDefinition;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0002CAB9 File Offset: 0x0002ACB9
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.underlyingType.ContainsGenericParameters;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0002CAC6 File Offset: 0x0002ACC6
		public override int MetadataToken
		{
			get
			{
				return this.underlyingType.MetadataToken;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0002CAD3 File Offset: 0x0002ACD3
		public override Module Module
		{
			get
			{
				return this.underlyingType.Module;
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		internal override int GetModuleBuilderToken()
		{
			return this.underlyingType.GetModuleBuilderToken();
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0000212D File Offset: 0x0000032D
		internal override bool IsBaked
		{
			get
			{
				return true;
			}
		}
	}
}
