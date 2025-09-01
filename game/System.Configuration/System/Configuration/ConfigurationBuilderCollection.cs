using System;
using System.Configuration.Provider;
using System.Reflection;
using Unity;

namespace System.Configuration
{
	/// <summary>Maintains a collection of <see cref="T:System.Configuration.ConfigurationBuilder" /> objects by name.</summary>
	// Token: 0x0200008D RID: 141
	[DefaultMember("Item")]
	public class ConfigurationBuilderCollection : ProviderCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationBuilderCollection" /> class.</summary>
		// Token: 0x06000492 RID: 1170 RVA: 0x00003518 File Offset: 0x00001718
		public ConfigurationBuilderCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
