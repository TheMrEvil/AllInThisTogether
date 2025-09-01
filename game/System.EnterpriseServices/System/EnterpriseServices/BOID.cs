using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Represents the unit of work associated with a transaction. This structure is used in <see cref="T:System.EnterpriseServices.XACTTRANSINFO" />.</summary>
	// Token: 0x02000013 RID: 19
	[ComVisible(false)]
	public struct BOID
	{
		/// <summary>Represents an array that contains the data.</summary>
		// Token: 0x04000046 RID: 70
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] rgb;
	}
}
