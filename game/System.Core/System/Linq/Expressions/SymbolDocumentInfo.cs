using System;
using System.Dynamic.Utils;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Stores information necessary to emit debugging symbol information for a source file, in particular the file name and unique language identifier.</summary>
	// Token: 0x0200029E RID: 670
	public class SymbolDocumentInfo
	{
		// Token: 0x060013E6 RID: 5094 RVA: 0x0003D244 File Offset: 0x0003B444
		internal SymbolDocumentInfo(string fileName)
		{
			ContractUtils.RequiresNotNull(fileName, "fileName");
			this.FileName = fileName;
		}

		/// <summary>The source file name.</summary>
		/// <returns>The string representing the source file name.</returns>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0003D25E File Offset: 0x0003B45E
		public string FileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FileName>k__BackingField;
			}
		}

		/// <summary>Returns the language's unique identifier, if any.</summary>
		/// <returns>The language's unique identifier</returns>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0003D266 File Offset: 0x0003B466
		public virtual Guid Language
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>Returns the language vendor's unique identifier, if any.</summary>
		/// <returns>The language vendor's unique identifier.</returns>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0003D266 File Offset: 0x0003B466
		public virtual Guid LanguageVendor
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>Returns the document type's unique identifier, if any. Defaults to the GUID for a text file.</summary>
		/// <returns>The document type's unique identifier.</returns>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0003D26D File Offset: 0x0003B46D
		public virtual Guid DocumentType
		{
			get
			{
				return SymbolDocumentInfo.DocumentType_Text;
			}
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0003D274 File Offset: 0x0003B474
		// Note: this type is marked as 'beforefieldinit'.
		static SymbolDocumentInfo()
		{
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0000235B File Offset: 0x0000055B
		internal SymbolDocumentInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A5D RID: 2653
		[CompilerGenerated]
		private readonly string <FileName>k__BackingField;

		// Token: 0x04000A5E RID: 2654
		internal static readonly Guid DocumentType_Text = new Guid(1518771467, 26129, 4563, 189, 42, 0, 0, 248, 8, 73, 189);
	}
}
