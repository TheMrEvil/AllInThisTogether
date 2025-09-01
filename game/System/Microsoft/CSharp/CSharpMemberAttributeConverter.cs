using System;
using System.CodeDom;
using System.Runtime.CompilerServices;

namespace Microsoft.CSharp
{
	// Token: 0x0200013D RID: 317
	internal sealed class CSharpMemberAttributeConverter : CSharpModifierAttributeConverter
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x0001E60C File Offset: 0x0001C80C
		private CSharpMemberAttributeConverter()
		{
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
		public static CSharpMemberAttributeConverter Default
		{
			[CompilerGenerated]
			get
			{
				return CSharpMemberAttributeConverter.<Default>k__BackingField;
			}
		} = new CSharpMemberAttributeConverter();

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001E6A7 File Offset: 0x0001C8A7
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
			"Protected Internal",
			"Internal",
			"Private"
		};

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0001E6AF File Offset: 0x0001C8AF
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

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00018453 File Offset: 0x00016653
		protected override object DefaultValue
		{
			get
			{
				return MemberAttributes.Private;
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001E6B7 File Offset: 0x0001C8B7
		// Note: this type is marked as 'beforefieldinit'.
		static CSharpMemberAttributeConverter()
		{
		}

		// Token: 0x04000537 RID: 1335
		[CompilerGenerated]
		private static readonly CSharpMemberAttributeConverter <Default>k__BackingField;

		// Token: 0x04000538 RID: 1336
		[CompilerGenerated]
		private readonly string[] <Names>k__BackingField;

		// Token: 0x04000539 RID: 1337
		[CompilerGenerated]
		private readonly object[] <Values>k__BackingField;
	}
}
