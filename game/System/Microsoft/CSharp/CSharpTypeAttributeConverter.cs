using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.CSharp
{
	// Token: 0x0200013F RID: 319
	internal sealed class CSharpTypeAttributeConverter : CSharpModifierAttributeConverter
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x0001E788 File Offset: 0x0001C988
		private CSharpTypeAttributeConverter()
		{
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001E7D5 File Offset: 0x0001C9D5
		public static CSharpTypeAttributeConverter Default
		{
			[CompilerGenerated]
			get
			{
				return CSharpTypeAttributeConverter.<Default>k__BackingField;
			}
		} = new CSharpTypeAttributeConverter();

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001E7DC File Offset: 0x0001C9DC
		protected override string[] Names
		{
			[CompilerGenerated]
			get
			{
				return this.<Names>k__BackingField;
			}
		} = new string[]
		{
			"Public",
			"Internal"
		};

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
		protected override object[] Values
		{
			[CompilerGenerated]
			get
			{
				return this.<Values>k__BackingField;
			}
		} = new object[]
		{
			TypeAttributes.Public,
			TypeAttributes.NotPublic
		};

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001E7EC File Offset: 0x0001C9EC
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.NotPublic;
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001E7F4 File Offset: 0x0001C9F4
		// Note: this type is marked as 'beforefieldinit'.
		static CSharpTypeAttributeConverter()
		{
		}

		// Token: 0x0400053A RID: 1338
		[CompilerGenerated]
		private static readonly CSharpTypeAttributeConverter <Default>k__BackingField;

		// Token: 0x0400053B RID: 1339
		[CompilerGenerated]
		private readonly string[] <Names>k__BackingField;

		// Token: 0x0400053C RID: 1340
		[CompilerGenerated]
		private readonly object[] <Values>k__BackingField;
	}
}
