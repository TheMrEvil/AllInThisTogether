using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Represents a structure used in the <see cref="T:System.EnterpriseServices.ITransaction" /> interface.</summary>
	// Token: 0x02000056 RID: 86
	[ComVisible(false)]
	public struct XACTTRANSINFO
	{
		/// <summary>Specifies zero. This field is reserved.</summary>
		// Token: 0x040000B0 RID: 176
		public int grfRMSupported;

		/// <summary>Specifies zero. This field is reserved.</summary>
		// Token: 0x040000B1 RID: 177
		public int grfRMSupportedRetaining;

		/// <summary>Represents a bitmask that indicates which <see langword="grfTC" /> flags this transaction implementation supports.</summary>
		// Token: 0x040000B2 RID: 178
		public int grfTCSupported;

		/// <summary>Specifies zero. This field is reserved.</summary>
		// Token: 0x040000B3 RID: 179
		public int grfTCSupportedRetaining;

		/// <summary>Specifies zero. This field is reserved.</summary>
		// Token: 0x040000B4 RID: 180
		public int isoFlags;

		/// <summary>Represents the isolation level associated with this transaction object. ISOLATIONLEVEL_UNSPECIFIED indicates that no isolation level was specified.</summary>
		// Token: 0x040000B5 RID: 181
		public int isoLevel;

		/// <summary>Represents the unit of work associated with this transaction.</summary>
		// Token: 0x040000B6 RID: 182
		public BOID uow;
	}
}
