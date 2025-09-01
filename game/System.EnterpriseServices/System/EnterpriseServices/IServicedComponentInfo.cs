using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Implemented by the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class to obtain information about the component via the <see cref="M:System.EnterpriseServices.IServicedComponentInfo.GetComponentInfo(System.Int32@,System.String[]@)" /> method.</summary>
	// Token: 0x0200002A RID: 42
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8165B19E-8D3A-4d0b-80C8-97DE310DB583")]
	public interface IServicedComponentInfo
	{
		/// <summary>Obtains certain information about the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class instance.</summary>
		/// <param name="infoMask">A bitmask where 0x00000001 is a key for the serviced component's process ID, 0x00000002 is a key for the application domain ID, and 0x00000004 is a key for the serviced component's remote URI.</param>
		/// <param name="infoArray">A string array that may contain any or all of the following, in order: the serviced component's process ID, the application domain ID, and the serviced component's remote URI.</param>
		// Token: 0x06000088 RID: 136
		void GetComponentInfo(ref int infoMask, out string[] infoArray);
	}
}
