using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Creates and deletes Internet Information Services (IIS) 6.0 virtual roots.</summary>
	// Token: 0x02000063 RID: 99
	[Guid("d8013ef1-730b-45e2-ba24-874b7242c425")]
	public class IISVirtualRoot : IComSoapIISVRoot
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.IISVirtualRoot" /> class.</summary>
		// Token: 0x0600017F RID: 383 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public IISVirtualRoot()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an Internet Information Services (IIS) virtual root.</summary>
		/// <param name="RootWeb">A string with the value <c>"IIS://localhost/W3SVC/1/ROOT"</c> representing the root Web server.</param>
		/// <param name="inPhysicalDirectory">The physical path of the virtual root, which corresponds to <paramref name="PhysicalPath" /> from the <see cref="M:System.EnterpriseServices.Internal.Publish.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" /> method.</param>
		/// <param name="VirtualDirectory">The name of the virtual root, which corresponds to <paramref name="VirtualRoot" /> from <see cref="M:System.EnterpriseServices.Internal.Publish.CreateVirtualRoot(System.String,System.String,System.String@,System.String@,System.String@,System.String@)" />.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		// Token: 0x06000180 RID: 384 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void Create(string RootWeb, string inPhysicalDirectory, string VirtualDirectory, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes an Internet Information Services (IIS) virtual root.</summary>
		/// <param name="RootWeb">The root Web server, as specified by <paramref name="RootWebServer" /> from the <see cref="M:System.EnterpriseServices.Internal.IComSoapPublisher.DeleteVirtualRoot(System.String,System.String,System.String@)" /> method.</param>
		/// <param name="PhysicalDirectory">The physical path of the virtual root.</param>
		/// <param name="VirtualDirectory">The name of the virtual root.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		// Token: 0x06000181 RID: 385 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void Delete(string RootWeb, string PhysicalDirectory, string VirtualDirectory, out string Error)
		{
			throw new NotImplementedException();
		}
	}
}
