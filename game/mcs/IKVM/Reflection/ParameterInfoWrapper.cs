using System;

namespace IKVM.Reflection
{
	// Token: 0x02000051 RID: 81
	internal sealed class ParameterInfoWrapper : ParameterInfo
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000B0C6 File Offset: 0x000092C6
		internal ParameterInfoWrapper(MemberInfo member, ParameterInfo forward)
		{
			this.member = member;
			this.forward = forward;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000B0DC File Offset: 0x000092DC
		public override string Name
		{
			get
			{
				return this.forward.Name;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000B0E9 File Offset: 0x000092E9
		public override Type ParameterType
		{
			get
			{
				return this.forward.ParameterType;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000B0F6 File Offset: 0x000092F6
		public override ParameterAttributes Attributes
		{
			get
			{
				return this.forward.Attributes;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000B103 File Offset: 0x00009303
		public override int Position
		{
			get
			{
				return this.forward.Position;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000B110 File Offset: 0x00009310
		public override object RawDefaultValue
		{
			get
			{
				return this.forward.RawDefaultValue;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000B11D File Offset: 0x0000931D
		public override CustomModifiers __GetCustomModifiers()
		{
			return this.forward.__GetCustomModifiers();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000B12A File Offset: 0x0000932A
		public override bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
		{
			return this.forward.__TryGetFieldMarshal(out fieldMarshal);
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000B138 File Offset: 0x00009338
		public override MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000B140 File Offset: 0x00009340
		public override int MetadataToken
		{
			get
			{
				return this.forward.MetadataToken;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000B14D File Offset: 0x0000934D
		internal override Module Module
		{
			get
			{
				return this.member.Module;
			}
		}

		// Token: 0x040001B4 RID: 436
		private readonly MemberInfo member;

		// Token: 0x040001B5 RID: 437
		private readonly ParameterInfo forward;
	}
}
