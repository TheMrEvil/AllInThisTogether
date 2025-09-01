using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000DA RID: 218
	public sealed class EnumBuilder : TypeInfo
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x000233F0 File Offset: 0x000215F0
		internal EnumBuilder(TypeBuilder typeBuilder, FieldBuilder fieldBuilder) : base(typeBuilder)
		{
			this.typeBuilder = typeBuilder;
			this.fieldBuilder = fieldBuilder;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00023407 File Offset: 0x00021607
		internal override TypeName TypeName
		{
			get
			{
				return this.typeBuilder.TypeName;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00023414 File Offset: 0x00021614
		public override string Name
		{
			get
			{
				return this.typeBuilder.Name;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00023421 File Offset: 0x00021621
		public override string FullName
		{
			get
			{
				return this.typeBuilder.FullName;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0002342E File Offset: 0x0002162E
		public override Type BaseType
		{
			get
			{
				return this.typeBuilder.BaseType;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0002343B File Offset: 0x0002163B
		public override TypeAttributes Attributes
		{
			get
			{
				return this.typeBuilder.Attributes;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00023448 File Offset: 0x00021648
		public override Module Module
		{
			get
			{
				return this.typeBuilder.Module;
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00023455 File Offset: 0x00021655
		public FieldBuilder DefineLiteral(string literalName, object literalValue)
		{
			FieldBuilder fieldBuilder = this.typeBuilder.DefineField(literalName, this.typeBuilder, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			fieldBuilder.SetConstant(literalValue);
			return fieldBuilder;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00023472 File Offset: 0x00021672
		public Type CreateType()
		{
			return this.typeBuilder.CreateType();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002347F File Offset: 0x0002167F
		public TypeInfo CreateTypeInfo()
		{
			return this.typeBuilder.CreateTypeInfo();
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002348C File Offset: 0x0002168C
		public TypeToken TypeToken
		{
			get
			{
				return this.typeBuilder.TypeToken;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00023499 File Offset: 0x00021699
		public FieldBuilder UnderlyingField
		{
			get
			{
				return this.fieldBuilder;
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000234A1 File Offset: 0x000216A1
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.typeBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x000234B0 File Offset: 0x000216B0
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.typeBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000234BE File Offset: 0x000216BE
		public override Type GetEnumUnderlyingType()
		{
			return this.fieldBuilder.FieldType;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000234CB File Offset: 0x000216CB
		internal override bool IsBaked
		{
			get
			{
				return this.typeBuilder.IsBaked;
			}
		}

		// Token: 0x0400042F RID: 1071
		private readonly TypeBuilder typeBuilder;

		// Token: 0x04000430 RID: 1072
		private readonly FieldBuilder fieldBuilder;
	}
}
