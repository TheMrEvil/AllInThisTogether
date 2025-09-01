using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies a particular type library and provides localization support for member names.</summary>
	// Token: 0x020007CD RID: 1997
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		/// <summary>Represents a globally unique library ID of a type library.</summary>
		// Token: 0x04002D0D RID: 11533
		public Guid guid;

		/// <summary>Represents a locale ID of a type library.</summary>
		// Token: 0x04002D0E RID: 11534
		public int lcid;

		/// <summary>Represents the target hardware platform of a type library.</summary>
		// Token: 0x04002D0F RID: 11535
		public SYSKIND syskind;

		/// <summary>Represents the major version number of a type library.</summary>
		// Token: 0x04002D10 RID: 11536
		public short wMajorVerNum;

		/// <summary>Represents the minor version number of a type library.</summary>
		// Token: 0x04002D11 RID: 11537
		public short wMinorVerNum;

		/// <summary>Represents library flags.</summary>
		// Token: 0x04002D12 RID: 11538
		public LIBFLAGS wLibFlags;
	}
}
