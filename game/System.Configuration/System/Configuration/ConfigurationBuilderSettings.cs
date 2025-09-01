using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a group of configuration elements that configure the providers for the <see langword="&lt;configBuilders&gt;" /> configuration section.</summary>
	// Token: 0x0200008E RID: 142
	public class ConfigurationBuilderSettings : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationBuilderSettings" /> class.</summary>
		// Token: 0x06000493 RID: 1171 RVA: 0x00003518 File Offset: 0x00001718
		public ConfigurationBuilderSettings()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection of <see cref="T:System.Configuration.ConfigurationBuilderSettings" /> objects that represent the properties of configuration builders.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationBuilder" /> objects.</returns>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00003527 File Offset: 0x00001727
		public ProviderSettingsCollection Builders
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
