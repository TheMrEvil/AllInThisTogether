using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Holds the public GUIDs for language types to be used with the symbol store.</summary>
	// Token: 0x020009E1 RID: 2529
	[ComVisible(true)]
	public class SymLanguageType
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SymbolStore.SymLanguageType" /> class.</summary>
		// Token: 0x06005A8D RID: 23181 RVA: 0x0000259F File Offset: 0x0000079F
		public SymLanguageType()
		{
		}

		/// <summary>Specifies the GUID of the Basic language type to be used with the symbol store.</summary>
		// Token: 0x040037CA RID: 14282
		public static readonly Guid Basic;

		/// <summary>Specifies the GUID of the C language type to be used with the symbol store.</summary>
		// Token: 0x040037CB RID: 14283
		public static readonly Guid C;

		/// <summary>Specifies the GUID of the Cobol language type to be used with the symbol store.</summary>
		// Token: 0x040037CC RID: 14284
		public static readonly Guid Cobol;

		/// <summary>Specifies the GUID of the C++ language type to be used with the symbol store.</summary>
		// Token: 0x040037CD RID: 14285
		public static readonly Guid CPlusPlus;

		/// <summary>Specifies the GUID of the C# language type to be used with the symbol store.</summary>
		// Token: 0x040037CE RID: 14286
		public static readonly Guid CSharp;

		/// <summary>Specifies the GUID of the ILAssembly language type to be used with the symbol store.</summary>
		// Token: 0x040037CF RID: 14287
		public static readonly Guid ILAssembly;

		/// <summary>Specifies the GUID of the Java language type to be used with the symbol store.</summary>
		// Token: 0x040037D0 RID: 14288
		public static readonly Guid Java;

		/// <summary>Specifies the GUID of the JScript language type to be used with the symbol store.</summary>
		// Token: 0x040037D1 RID: 14289
		public static readonly Guid JScript;

		/// <summary>Specifies the GUID of the C++ language type to be used with the symbol store.</summary>
		// Token: 0x040037D2 RID: 14290
		public static readonly Guid MCPlusPlus;

		/// <summary>Specifies the GUID of the Pascal language type to be used with the symbol store.</summary>
		// Token: 0x040037D3 RID: 14291
		public static readonly Guid Pascal;

		/// <summary>Specifies the GUID of the SMC language type to be used with the symbol store.</summary>
		// Token: 0x040037D4 RID: 14292
		public static readonly Guid SMC;
	}
}
