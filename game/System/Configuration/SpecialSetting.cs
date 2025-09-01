using System;

namespace System.Configuration
{
	/// <summary>Specifies the special setting category of a application settings property.</summary>
	// Token: 0x020001DA RID: 474
	public enum SpecialSetting
	{
		/// <summary>The configuration property represents a connection string, typically for a data store or network resource.</summary>
		// Token: 0x040007BA RID: 1978
		ConnectionString,
		/// <summary>The configuration property represents a Uniform Resource Locator (URL) to a Web service.</summary>
		// Token: 0x040007BB RID: 1979
		WebServiceUrl
	}
}
