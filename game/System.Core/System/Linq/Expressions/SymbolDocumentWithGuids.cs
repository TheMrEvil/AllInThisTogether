using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200029F RID: 671
	internal sealed class SymbolDocumentWithGuids : SymbolDocumentInfo
	{
		// Token: 0x060013ED RID: 5101 RVA: 0x0003D2B0 File Offset: 0x0003B4B0
		internal SymbolDocumentWithGuids(string fileName, ref Guid language) : base(fileName)
		{
			this.Language = language;
			this.DocumentType = SymbolDocumentInfo.DocumentType_Text;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		internal SymbolDocumentWithGuids(string fileName, ref Guid language, ref Guid vendor) : base(fileName)
		{
			this.Language = language;
			this.LanguageVendor = vendor;
			this.DocumentType = SymbolDocumentInfo.DocumentType_Text;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0003D2FC File Offset: 0x0003B4FC
		internal SymbolDocumentWithGuids(string fileName, ref Guid language, ref Guid vendor, ref Guid documentType) : base(fileName)
		{
			this.Language = language;
			this.LanguageVendor = vendor;
			this.DocumentType = documentType;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0003D32A File Offset: 0x0003B52A
		public override Guid Language
		{
			[CompilerGenerated]
			get
			{
				return this.<Language>k__BackingField;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0003D332 File Offset: 0x0003B532
		public override Guid LanguageVendor
		{
			[CompilerGenerated]
			get
			{
				return this.<LanguageVendor>k__BackingField;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0003D33A File Offset: 0x0003B53A
		public override Guid DocumentType
		{
			[CompilerGenerated]
			get
			{
				return this.<DocumentType>k__BackingField;
			}
		}

		// Token: 0x04000A5F RID: 2655
		[CompilerGenerated]
		private readonly Guid <Language>k__BackingField;

		// Token: 0x04000A60 RID: 2656
		[CompilerGenerated]
		private readonly Guid <LanguageVendor>k__BackingField;

		// Token: 0x04000A61 RID: 2657
		[CompilerGenerated]
		private readonly Guid <DocumentType>k__BackingField;
	}
}
