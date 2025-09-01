using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Interface definition for creating and deleting Internet Information Services (IIS) 6.0 virtual roots.</summary>
	// Token: 0x02000060 RID: 96
	[Guid("d8013ef0-730b-45e2-ba24-874b7242c425")]
	public interface IComSoapIISVRoot
	{
		/// <summary>Creates an Internet Information Services (IIS) virtual root.</summary>
		/// <param name="RootWeb">The root Web server.</param>
		/// <param name="PhysicalDirectory">The physical path of the virtual root, which corresponds to <paramref name="PhysicalPath" /> from the <see cref="M:System.EnterpriseServices.Internal.IComSoapPublisher.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" /> method.</param>
		/// <param name="VirtualDirectory">The name of the virtual root, which corresponds to <paramref name="VirtualRoot" /> from the <see cref="M:System.EnterpriseServices.Internal.IComSoapPublisher.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" /> method.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		// Token: 0x0600016F RID: 367
		[DispId(1)]
		void Create([MarshalAs(UnmanagedType.BStr)] string RootWeb, [MarshalAs(UnmanagedType.BStr)] string PhysicalDirectory, [MarshalAs(UnmanagedType.BStr)] string VirtualDirectory, [MarshalAs(UnmanagedType.BStr)] out string Error);

		/// <summary>Deletes an Internet Information Services (IIS) virtual root.</summary>
		/// <param name="RootWeb">The root Web server.</param>
		/// <param name="PhysicalDirectory">The physical path of the virtual root.</param>
		/// <param name="VirtualDirectory">The name of the virtual root.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		// Token: 0x06000170 RID: 368
		[DispId(2)]
		void Delete([MarshalAs(UnmanagedType.BStr)] string RootWeb, [MarshalAs(UnmanagedType.BStr)] string PhysicalDirectory, [MarshalAs(UnmanagedType.BStr)] string VirtualDirectory, [MarshalAs(UnmanagedType.BStr)] out string Error);
	}
}
