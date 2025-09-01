using System;
using System.CodeDom;
using System.Runtime.CompilerServices;

namespace Microsoft.VisualBasic
{
	// Token: 0x02000137 RID: 311
	internal sealed class VBMemberAttributeConverter : VBModifierAttributeConverter
	{
		// Token: 0x060007B2 RID: 1970 RVA: 0x000183A8 File Offset: 0x000165A8
		private VBMemberAttributeConverter()
		{
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001843C File Offset: 0x0001663C
		public static VBMemberAttributeConverter Default
		{
			[CompilerGenerated]
			get
			{
				return VBMemberAttributeConverter.<Default>k__BackingField;
			}
		} = new VBMemberAttributeConverter();

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00018443 File Offset: 0x00016643
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
			"Protected",
			"Protected Friend",
			"Friend",
			"Private"
		};

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001844B File Offset: 0x0001664B
		protected override object[] Values
		{
			[CompilerGenerated]
			get
			{
				return this.<Values>k__BackingField;
			}
		} = new object[]
		{
			MemberAttributes.Public,
			MemberAttributes.Family,
			MemberAttributes.FamilyOrAssembly,
			MemberAttributes.Assembly,
			MemberAttributes.Private
		};

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00018453 File Offset: 0x00016653
		protected override object DefaultValue
		{
			get
			{
				return MemberAttributes.Private;
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001845F File Offset: 0x0001665F
		// Note: this type is marked as 'beforefieldinit'.
		static VBMemberAttributeConverter()
		{
		}

		// Token: 0x0400051F RID: 1311
		[CompilerGenerated]
		private static readonly VBMemberAttributeConverter <Default>k__BackingField;

		// Token: 0x04000520 RID: 1312
		[CompilerGenerated]
		private readonly string[] <Names>k__BackingField;

		// Token: 0x04000521 RID: 1313
		[CompilerGenerated]
		private readonly object[] <Values>k__BackingField;
	}
}
