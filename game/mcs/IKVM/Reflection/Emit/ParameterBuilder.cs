using System;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000EF RID: 239
	public sealed class ParameterBuilder
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0002A788 File Offset: 0x00028988
		internal ParameterBuilder(ModuleBuilder moduleBuilder, int sequence, ParameterAttributes attribs, string name)
		{
			this.moduleBuilder = moduleBuilder;
			this.flags = (short)attribs;
			this.sequence = (short)sequence;
			this.nameIndex = ((name == null) ? 0 : moduleBuilder.Strings.Add(name));
			this.name = name;
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002A7D4 File Offset: 0x000289D4
		internal int PseudoToken
		{
			get
			{
				if (this.lazyPseudoToken == 0)
				{
					this.lazyPseudoToken = this.moduleBuilder.AllocPseudoToken();
				}
				return this.lazyPseudoToken;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0002A7F5 File Offset: 0x000289F5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0002A7FD File Offset: 0x000289FD
		public int Position
		{
			get
			{
				return (int)this.sequence;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0002A805 File Offset: 0x00028A05
		public int Attributes
		{
			get
			{
				return (int)this.flags;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002A80D File Offset: 0x00028A0D
		public bool IsIn
		{
			get
			{
				return (this.flags & 1) != 0;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002A81A File Offset: 0x00028A1A
		public bool IsOut
		{
			get
			{
				return (this.flags & 2) != 0;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002A827 File Offset: 0x00028A27
		public bool IsOptional
		{
			get
			{
				return (this.flags & 16) != 0;
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002A835 File Offset: 0x00028A35
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002A844 File Offset: 0x00028A44
		public void SetCustomAttribute(CustomAttributeBuilder customAttributeBuilder)
		{
			switch (customAttributeBuilder.KnownCA)
			{
			case KnownCA.MarshalAsAttribute:
				FieldMarshal.SetMarshalAsAttribute(this.moduleBuilder, this.PseudoToken, customAttributeBuilder);
				this.flags |= 8192;
				return;
			case KnownCA.InAttribute:
				this.flags |= 1;
				return;
			case KnownCA.OutAttribute:
				this.flags |= 2;
				return;
			case KnownCA.OptionalAttribute:
				this.flags |= 16;
				return;
			}
			this.moduleBuilder.SetCustomAttribute(this.PseudoToken, customAttributeBuilder);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002A8DF File Offset: 0x00028ADF
		public void SetConstant(object defaultValue)
		{
			this.flags |= 4096;
			this.moduleBuilder.AddConstant(this.PseudoToken, defaultValue);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002A906 File Offset: 0x00028B06
		internal void WriteParamRecord(MetadataWriter mw)
		{
			mw.Write(this.flags);
			mw.Write(this.sequence);
			mw.WriteStringIndex(this.nameIndex);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002A92C File Offset: 0x00028B2C
		internal void FixupToken(int parameterToken)
		{
			if (this.lazyPseudoToken != 0)
			{
				this.moduleBuilder.RegisterTokenFixup(this.lazyPseudoToken, parameterToken);
			}
		}

		// Token: 0x040005DD RID: 1501
		private readonly ModuleBuilder moduleBuilder;

		// Token: 0x040005DE RID: 1502
		private short flags;

		// Token: 0x040005DF RID: 1503
		private readonly short sequence;

		// Token: 0x040005E0 RID: 1504
		private readonly int nameIndex;

		// Token: 0x040005E1 RID: 1505
		private readonly string name;

		// Token: 0x040005E2 RID: 1506
		private int lazyPseudoToken;
	}
}
