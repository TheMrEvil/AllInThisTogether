using System;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Defines a static <see cref="M:System.EnterpriseServices.Internal.ClientRemotingConfig.Write(System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String)" /> method that creates a client remoting configuration file for a client type library.</summary>
	// Token: 0x02000059 RID: 89
	public class ClientRemotingConfig
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.ClientRemotingConfig" /> class.</summary>
		// Token: 0x06000158 RID: 344 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public ClientRemotingConfig()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a client remoting configuration file for a client type library in a SOAP-enabled COM+ application.</summary>
		/// <param name="DestinationDirectory">The folder in which to create the configuration file.</param>
		/// <param name="VRoot">The name of the virtual root.</param>
		/// <param name="BaseUrl">The base URL that contains the virtual root.</param>
		/// <param name="AssemblyName">The display name of the assembly that contains common language runtime (CLR) metadata corresponding to the type library.</param>
		/// <param name="TypeName">The fully qualified name of the assembly that contains CLR metadata corresponding to the type library.</param>
		/// <param name="ProgId">The programmatic identifier of the class.</param>
		/// <param name="Mode">The activation mode.</param>
		/// <param name="Transport">Not used. Specify <see langword="null" /> for this parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the client remoting configuration file was successfully created; otherwise <see langword="false" />.</returns>
		// Token: 0x06000159 RID: 345 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static bool Write(string DestinationDirectory, string VRoot, string BaseUrl, string AssemblyName, string TypeName, string ProgId, string Mode, string Transport)
		{
			throw new NotImplementedException();
		}
	}
}
