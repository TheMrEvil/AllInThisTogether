using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines flags that apply to type libraries.</summary>
	// Token: 0x020007CC RID: 1996
	[Flags]
	[Serializable]
	public enum LIBFLAGS : short
	{
		/// <summary>The type library is restricted, and should not be displayed to users.</summary>
		// Token: 0x04002D09 RID: 11529
		LIBFLAG_FRESTRICTED = 1,
		/// <summary>The type library describes controls and should not be displayed in type browsers intended for nonvisual objects.</summary>
		// Token: 0x04002D0A RID: 11530
		LIBFLAG_FCONTROL = 2,
		/// <summary>The type library should not be displayed to users, although its use is not restricted. The type library should be used by controls. Hosts should create a new type library that wraps the control with extended properties.</summary>
		// Token: 0x04002D0B RID: 11531
		LIBFLAG_FHIDDEN = 4,
		/// <summary>The type library exists in a persisted form on disk.</summary>
		// Token: 0x04002D0C RID: 11532
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
