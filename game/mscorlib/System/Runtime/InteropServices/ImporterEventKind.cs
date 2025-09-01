using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the callbacks that the type library importer makes when importing a type library.</summary>
	// Token: 0x02000745 RID: 1861
	[ComVisible(true)]
	[Serializable]
	public enum ImporterEventKind
	{
		/// <summary>Specifies that the event is invoked when a type has been imported.</summary>
		// Token: 0x04002BC3 RID: 11203
		NOTIF_TYPECONVERTED,
		/// <summary>Specifies that the event is invoked when a warning occurs during conversion.</summary>
		// Token: 0x04002BC4 RID: 11204
		NOTIF_CONVERTWARNING,
		/// <summary>This property is not supported in this version of the .NET Framework.</summary>
		// Token: 0x04002BC5 RID: 11205
		ERROR_REFTOINVALIDTYPELIB
	}
}
