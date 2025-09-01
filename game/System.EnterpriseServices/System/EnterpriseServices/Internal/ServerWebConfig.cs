using System;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Creates a Web.config file for a SOAP-enabled COM+ application. Can also add component entries to the file for COM interfaces being published in the application.</summary>
	// Token: 0x0200006A RID: 106
	public class ServerWebConfig : IServerWebConfig
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.ServerWebConfig" /> class.</summary>
		// Token: 0x0600019C RID: 412 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public ServerWebConfig()
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds XML elements to a Web.config file for a COM interface being published in a SOAP-enabled COM+ application.</summary>
		/// <param name="FilePath">The path of the existing Web.config file.</param>
		/// <param name="AssemblyName">The name of the assembly that contains the type being added.</param>
		/// <param name="TypeName">The name of the type being added.</param>
		/// <param name="ProgId">The programmatic identifier for the type being added.</param>
		/// <param name="WkoMode">A string constant that corresponds to the name of a member from the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> enumeration, which indicates how a well-known object is activated.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		// Token: 0x0600019D RID: 413 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void AddElement(string FilePath, string AssemblyName, string TypeName, string ProgId, string WkoMode, out string Error)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a Web.config file for a SOAP-enabled COM+ application so that the file is ready to have XML elements added for COM interfaces being published.</summary>
		/// <param name="FilePath">The folder in which the configuration file should be created.</param>
		/// <param name="FilePrefix">The string value "Web", to which a config extension is added.</param>
		/// <param name="Error">A string to which an error message can be written.</param>
		// Token: 0x0600019E RID: 414 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void Create(string FilePath, string FilePrefix, out string Error)
		{
			throw new NotImplementedException();
		}
	}
}
