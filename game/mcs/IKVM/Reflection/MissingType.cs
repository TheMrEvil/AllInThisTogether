using System;

namespace IKVM.Reflection
{
	// Token: 0x02000046 RID: 70
	internal sealed class MissingType : Type
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00009E10 File Offset: 0x00008010
		internal MissingType(Module module, Type declaringType, string ns, string name)
		{
			this.module = module;
			this.declaringType = declaringType;
			this.ns = ns;
			this.name = name;
			base.MarkKnownType(ns, name);
			if (WindowsRuntimeProjection.IsProjectedValueType(ns, name, module))
			{
				this.typeFlags |= Type.TypeFlags.ValueType;
				return;
			}
			if (WindowsRuntimeProjection.IsProjectedReferenceType(ns, name, module))
			{
				this.typeFlags |= Type.TypeFlags.NotValueType;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00009E80 File Offset: 0x00008080
		internal override MethodBase FindMethod(string name, MethodSignature signature)
		{
			MethodInfo methodInfo = new MissingMethod(this, name, signature);
			if (name == ".ctor")
			{
				return new ConstructorInfoImpl(methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00009EAB File Offset: 0x000080AB
		internal override FieldInfo FindField(string name, FieldSignature signature)
		{
			return new MissingField(this, name, signature);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindNestedType(TypeName name)
		{
			return null;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000055E7 File Offset: 0x000037E7
		internal override Type FindNestedTypeIgnoreCase(TypeName lowerCaseName)
		{
			return null;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool __IsMissing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00009EB5 File Offset: 0x000080B5
		public override Type DeclaringType
		{
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00009EBD File Offset: 0x000080BD
		internal override TypeName TypeName
		{
			get
			{
				return new TypeName(this.ns, this.name);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00009ED0 File Offset: 0x000080D0
		public override string Name
		{
			get
			{
				return TypeNameParser.Escape(this.name);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00009EDD File Offset: 0x000080DD
		public override string FullName
		{
			get
			{
				return base.GetFullName();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00009EE5 File Offset: 0x000080E5
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00009EED File Offset: 0x000080ED
		public override int MetadataToken
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00009EF8 File Offset: 0x000080F8
		public override bool IsValueType
		{
			get
			{
				Type.TypeFlags typeFlags = this.typeFlags & (Type.TypeFlags.ValueType | Type.TypeFlags.NotValueType);
				if (typeFlags == Type.TypeFlags.ValueType)
				{
					return true;
				}
				if (typeFlags != Type.TypeFlags.NotValueType)
				{
					if (typeFlags == (Type.TypeFlags.ValueType | Type.TypeFlags.NotValueType))
					{
						if (WindowsRuntimeProjection.IsProjectedValueType(this.ns, this.name, this.module))
						{
							this.typeFlags &= ~Type.TypeFlags.NotValueType;
							return true;
						}
						if (WindowsRuntimeProjection.IsProjectedReferenceType(this.ns, this.name, this.module))
						{
							this.typeFlags &= ~Type.TypeFlags.ValueType;
							return false;
						}
					}
					if (this.module.universe.ResolveMissingTypeIsValueType(this))
					{
						this.typeFlags |= Type.TypeFlags.ValueType;
					}
					else
					{
						this.typeFlags |= Type.TypeFlags.NotValueType;
					}
					return (this.typeFlags & Type.TypeFlags.ValueType) > Type.TypeFlags.ContainsMissingType_Unknown;
				}
				return false;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00009FBA File Offset: 0x000081BA
		public override Type BaseType
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00009FBA File Offset: 0x000081BA
		public override TypeAttributes Attributes
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00009FBA File Offset: 0x000081BA
		public override Type[] __GetDeclaredTypes()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00009FBA File Offset: 0x000081BA
		public override Type[] __GetDeclaredInterfaces()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009FBA File Offset: 0x000081BA
		public override MethodBase[] __GetDeclaredMethods()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009FBA File Offset: 0x000081BA
		public override __MethodImplMap __GetMethodImplMap()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00009FBA File Offset: 0x000081BA
		public override FieldInfo[] __GetDeclaredFields()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009FBA File Offset: 0x000081BA
		public override EventInfo[] __GetDeclaredEvents()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009FBA File Offset: 0x000081BA
		public override PropertyInfo[] __GetDeclaredProperties()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009FBA File Offset: 0x000081BA
		public override CustomModifiers __GetCustomModifiers()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009FBA File Offset: 0x000081BA
		public override Type[] GetGenericArguments()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00009FBA File Offset: 0x000081BA
		public override CustomModifiers[] __GetGenericArgumentsCustomModifiers()
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00009FBA File Offset: 0x000081BA
		public override bool __GetLayout(out int packingSize, out int typeSize)
		{
			throw new MissingMemberException(this);
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009FBA File Offset: 0x000081BA
		public override bool IsGenericType
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00009FBA File Offset: 0x000081BA
		public override bool IsGenericTypeDefinition
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00009FC4 File Offset: 0x000081C4
		internal override Type GetGenericTypeArgument(int index)
		{
			if (this.typeArgs == null)
			{
				this.typeArgs = new Type[index + 1];
			}
			else if (this.typeArgs.Length <= index)
			{
				Array.Resize<Type>(ref this.typeArgs, index + 1);
			}
			Type result;
			if ((result = this.typeArgs[index]) == null)
			{
				result = (this.typeArgs[index] = new MissingTypeParameter(this, index));
			}
			return result;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00005936 File Offset: 0x00003B36
		internal override Type BindTypeParameters(IGenericBinder binder)
		{
			return this;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000A020 File Offset: 0x00008220
		internal override Type SetMetadataTokenForMissing(int token, int flags)
		{
			this.token = token;
			this.flags = flags;
			return this;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000A031 File Offset: 0x00008231
		internal override Type SetCyclicTypeForwarder()
		{
			this.cyclicTypeForwarder = true;
			return this;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00009FBA File Offset: 0x000081BA
		internal override bool IsBaked
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000A03B File Offset: 0x0000823B
		public override bool __IsTypeForwarder
		{
			get
			{
				return (this.flags & 2097152) != 0;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000A04C File Offset: 0x0000824C
		public override bool __IsCyclicTypeForwarder
		{
			get
			{
				return this.cyclicTypeForwarder;
			}
		}

		// Token: 0x04000173 RID: 371
		private readonly Module module;

		// Token: 0x04000174 RID: 372
		private readonly Type declaringType;

		// Token: 0x04000175 RID: 373
		private readonly string ns;

		// Token: 0x04000176 RID: 374
		private readonly string name;

		// Token: 0x04000177 RID: 375
		private Type[] typeArgs;

		// Token: 0x04000178 RID: 376
		private int token;

		// Token: 0x04000179 RID: 377
		private int flags;

		// Token: 0x0400017A RID: 378
		private bool cyclicTypeForwarder;
	}
}
