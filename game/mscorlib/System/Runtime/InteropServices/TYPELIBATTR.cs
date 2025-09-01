using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPELIBATTR" /> instead.</summary>
	// Token: 0x02000751 RID: 1873
	[Obsolete]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		/// <summary>Represents a globally unique library ID of a type library.</summary>
		// Token: 0x04002C01 RID: 11265
		public Guid guid;

		/// <summary>Represents a locale ID of a type library.</summary>
		// Token: 0x04002C02 RID: 11266
		public int lcid;

		/// <summary>Represents the target hardware platform of a type library.</summary>
		// Token: 0x04002C03 RID: 11267
		public SYSKIND syskind;

		/// <summary>Represents the major version number of a type library.</summary>
		// Token: 0x04002C04 RID: 11268
		public short wMajorVerNum;

		/// <summary>Represents the minor version number of a type library.</summary>
		// Token: 0x04002C05 RID: 11269
		public short wMinorVerNum;

		/// <summary>Represents library flags.</summary>
		// Token: 0x04002C06 RID: 11270
		public LIBFLAGS wLibFlags;
	}
}
