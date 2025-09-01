using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the callbacks that the type library exporter makes when exporting a type library.</summary>
	// Token: 0x0200073C RID: 1852
	[ComVisible(true)]
	[Serializable]
	public enum ExporterEventKind
	{
		/// <summary>Specifies that the event is invoked when a type has been exported.</summary>
		// Token: 0x04002BBB RID: 11195
		NOTIF_TYPECONVERTED,
		/// <summary>Specifies that the event is invoked when a warning occurs during conversion.</summary>
		// Token: 0x04002BBC RID: 11196
		NOTIF_CONVERTWARNING,
		/// <summary>This value is not supported in this version of the .NET Framework.</summary>
		// Token: 0x04002BBD RID: 11197
		ERROR_REFTOINVALIDASSEMBLY
	}
}
