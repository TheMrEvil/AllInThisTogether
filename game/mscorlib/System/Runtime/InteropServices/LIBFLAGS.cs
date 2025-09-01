using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.LIBFLAGS" /> instead.</summary>
	// Token: 0x02000746 RID: 1862
	[Obsolete]
	[Flags]
	[Serializable]
	public enum LIBFLAGS : short
	{
		/// <summary>The type library is restricted, and should not be displayed to users.</summary>
		// Token: 0x04002BC7 RID: 11207
		LIBFLAG_FRESTRICTED = 1,
		/// <summary>The type library describes controls, and should not be displayed in type browsers intended for nonvisual objects.</summary>
		// Token: 0x04002BC8 RID: 11208
		LIBFLAG_FCONTROL = 2,
		/// <summary>The type library should not be displayed to users, although its use is not restricted. Should be used by controls. Hosts should create a new type library that wraps the control with extended properties.</summary>
		// Token: 0x04002BC9 RID: 11209
		LIBFLAG_FHIDDEN = 4,
		/// <summary>The type library exists in a persisted form on disk.</summary>
		// Token: 0x04002BCA RID: 11210
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
