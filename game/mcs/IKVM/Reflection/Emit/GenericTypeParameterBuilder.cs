using System;
using IKVM.Reflection.Metadata;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F8 RID: 248
	public sealed class GenericTypeParameterBuilder : TypeInfo
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0002B44C File Offset: 0x0002964C
		internal GenericTypeParameterBuilder(string name, TypeBuilder type, int position) : this(name, type, null, position, 19)
		{
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002B45A File Offset: 0x0002965A
		internal GenericTypeParameterBuilder(string name, MethodBuilder method, int position) : this(name, null, method, position, 30)
		{
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002B468 File Offset: 0x00029668
		private GenericTypeParameterBuilder(string name, TypeBuilder type, MethodBuilder method, int position, byte sigElementType) : base(sigElementType)
		{
			this.name = name;
			this.type = type;
			this.method = method;
			this.position = position;
			GenericParamTable.Record newRecord = default(GenericParamTable.Record);
			newRecord.Number = (short)position;
			newRecord.Flags = 0;
			newRecord.Owner = ((type != null) ? type.MetadataToken : method.MetadataToken);
			newRecord.Name = this.ModuleBuilder.Strings.Add(name);
			this.paramPseudoIndex = this.ModuleBuilder.GenericParam.AddRecord(newRecord);
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x000055E7 File Offset: 0x000037E7
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0001971B File Offset: 0x0001791B
		public override bool IsValueType
		{
			get
			{
				return (this.GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) > GenericParameterAttributes.None;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0002B501 File Offset: 0x00029701
		public override Type BaseType
		{
			get
			{
				return this.baseType;
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override Type[] __GetDeclaredInterfaces()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0000212D File Offset: 0x0000032D
		public override TypeAttributes Attributes
		{
			get
			{
				return TypeAttributes.Public;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00019885 File Offset: 0x00017A85
		public override string Namespace
		{
			get
			{
				return this.DeclaringType.Namespace;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0002B509 File Offset: 0x00029709
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x000055E7 File Offset: 0x000037E7
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000197CD File Offset: 0x000179CD
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0002B511 File Offset: 0x00029711
		private ModuleBuilder ModuleBuilder
		{
			get
			{
				if (!(this.type != null))
				{
					return this.method.ModuleBuilder;
				}
				return this.type.ModuleBuilder;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0002B538 File Offset: 0x00029738
		public override Module Module
		{
			get
			{
				return this.ModuleBuilder;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002B540 File Offset: 0x00029740
		public override int GenericParameterPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0002B548 File Offset: 0x00029748
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0002B550 File Offset: 0x00029750
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override Type[] GetGenericParameterConstraints()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00023DF4 File Offset: 0x00021FF4
		public override CustomModifiers[] __GetGenericParameterConstraintCustomModifiers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0002B558 File Offset: 0x00029758
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				this.CheckBaked();
				return this.attr;
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002B566 File Offset: 0x00029766
		internal override void CheckBaked()
		{
			if (this.type != null)
			{
				this.type.CheckBaked();
				return;
			}
			this.method.CheckBaked();
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002B590 File Offset: 0x00029790
		private void AddConstraint(Type type)
		{
			GenericParamConstraintTable.Record newRecord = default(GenericParamConstraintTable.Record);
			newRecord.Owner = this.paramPseudoIndex;
			newRecord.Constraint = this.ModuleBuilder.GetTypeTokenForMemberRef(type);
			this.ModuleBuilder.GenericParamConstraint.AddRecord(newRecord);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002B5D7 File Offset: 0x000297D7
		public void SetBaseTypeConstraint(Type baseTypeConstraint)
		{
			this.baseType = baseTypeConstraint;
			this.AddConstraint(baseTypeConstraint);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002B5E8 File Offset: 0x000297E8
		public void SetInterfaceConstraints(params Type[] interfaceConstraints)
		{
			foreach (Type type in interfaceConstraints)
			{
				this.AddConstraint(type);
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002B610 File Offset: 0x00029810
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.attr = genericParameterAttributes;
			this.ModuleBuilder.GenericParam.PatchAttribute(this.paramPseudoIndex, genericParameterAttributes);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002B630 File Offset: 0x00029830
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.ModuleBuilder.SetCustomAttribute(42 << 24 | this.paramPseudoIndex, customBuilder);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002B64A File Offset: 0x0002984A
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0002B659 File Offset: 0x00029859
		public override int MetadataToken
		{
			get
			{
				this.CheckBaked();
				return 42 << 24 | this.paramPseudoIndex;
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002B670 File Offset: 0x00029870
		internal override int GetModuleBuilderToken()
		{
			if (this.typeToken == 0)
			{
				ByteBuffer bb = new ByteBuffer(5);
				Signature.WriteTypeSpec(this.ModuleBuilder, bb, this);
				this.typeToken = (452984832 | this.ModuleBuilder.TypeSpec.AddRecord(this.ModuleBuilder.Blobs.Add(bb)));
			}
			return this.typeToken;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002B6CC File Offset: 0x000298CC
		internal override Type BindTypeParameters(IGenericBinder binder)
		{
			if (this.type != null)
			{
				return binder.BindTypeParameter(this);
			}
			return binder.BindMethodParameter(this);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002B6EB File Offset: 0x000298EB
		internal override int GetCurrentToken()
		{
			if (this.ModuleBuilder.IsSaved)
			{
				return 42 << 24 | this.Module.GenericParam.GetIndexFixup()[this.paramPseudoIndex - 1] + 1;
			}
			return 42 << 24 | this.paramPseudoIndex;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002B728 File Offset: 0x00029928
		internal override bool IsBaked
		{
			get
			{
				return (this.type ?? this.method).IsBaked;
			}
		}

		// Token: 0x040005F9 RID: 1529
		private readonly string name;

		// Token: 0x040005FA RID: 1530
		private readonly TypeBuilder type;

		// Token: 0x040005FB RID: 1531
		private readonly MethodBuilder method;

		// Token: 0x040005FC RID: 1532
		private readonly int paramPseudoIndex;

		// Token: 0x040005FD RID: 1533
		private readonly int position;

		// Token: 0x040005FE RID: 1534
		private int typeToken;

		// Token: 0x040005FF RID: 1535
		private Type baseType;

		// Token: 0x04000600 RID: 1536
		private GenericParameterAttributes attr;
	}
}
