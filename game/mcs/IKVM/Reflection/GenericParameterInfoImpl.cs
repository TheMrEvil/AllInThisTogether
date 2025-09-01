using System;

namespace IKVM.Reflection
{
	// Token: 0x02000031 RID: 49
	internal sealed class GenericParameterInfoImpl : ParameterInfo
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00007D79 File Offset: 0x00005F79
		internal GenericParameterInfoImpl(GenericMethodInstance method, ParameterInfo parameterInfo)
		{
			this.method = method;
			this.parameterInfo = parameterInfo;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00007D8F File Offset: 0x00005F8F
		public override string Name
		{
			get
			{
				return this.parameterInfo.Name;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00007D9C File Offset: 0x00005F9C
		public override Type ParameterType
		{
			get
			{
				return this.parameterInfo.ParameterType.BindTypeParameters(this.method);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public override ParameterAttributes Attributes
		{
			get
			{
				return this.parameterInfo.Attributes;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00007DC1 File Offset: 0x00005FC1
		public override int Position
		{
			get
			{
				return this.parameterInfo.Position;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00007DCE File Offset: 0x00005FCE
		public override object RawDefaultValue
		{
			get
			{
				return this.parameterInfo.RawDefaultValue;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007DDC File Offset: 0x00005FDC
		public override CustomModifiers __GetCustomModifiers()
		{
			return this.parameterInfo.__GetCustomModifiers().Bind(this.method);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007E02 File Offset: 0x00006002
		public override bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
		{
			return this.parameterInfo.__TryGetFieldMarshal(out fieldMarshal);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007E10 File Offset: 0x00006010
		public override MemberInfo Member
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00007E18 File Offset: 0x00006018
		public override int MetadataToken
		{
			get
			{
				return this.parameterInfo.MetadataToken;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007E25 File Offset: 0x00006025
		internal override Module Module
		{
			get
			{
				return this.method.Module;
			}
		}

		// Token: 0x04000140 RID: 320
		private readonly GenericMethodInstance method;

		// Token: 0x04000141 RID: 321
		private readonly ParameterInfo parameterInfo;
	}
}
