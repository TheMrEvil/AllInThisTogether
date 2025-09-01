using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.VisualBasic
{
	// Token: 0x02000139 RID: 313
	internal sealed class VBTypeAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x00018558 File Offset: 0x00016758
		private VBTypeAttributeConverter()
		{
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x000185A5 File Offset: 0x000167A5
		public static VBTypeAttributeConverter Default
		{
			[CompilerGenerated]
			get
			{
				return VBTypeAttributeConverter.<Default>k__BackingField;
			}
		} = new VBTypeAttributeConverter();

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x000185AC File Offset: 0x000167AC
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
			"Friend"
		};

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x000185B4 File Offset: 0x000167B4
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x000185BC File Offset: 0x000167BC
		protected override object DefaultValue
		{
			get
			{
				return TypeAttributes.Public;
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000185C4 File Offset: 0x000167C4
		// Note: this type is marked as 'beforefieldinit'.
		static VBTypeAttributeConverter()
		{
		}

		// Token: 0x04000522 RID: 1314
		[CompilerGenerated]
		private static readonly VBTypeAttributeConverter <Default>k__BackingField;

		// Token: 0x04000523 RID: 1315
		[CompilerGenerated]
		private readonly string[] <Names>k__BackingField;

		// Token: 0x04000524 RID: 1316
		[CompilerGenerated]
		private readonly object[] <Values>k__BackingField;
	}
}
