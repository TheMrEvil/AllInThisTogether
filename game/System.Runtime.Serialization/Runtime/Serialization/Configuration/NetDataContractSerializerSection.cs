using System;
using System.Configuration;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure serialization by the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" />.</summary>
	// Token: 0x020001A5 RID: 421
	public sealed class NetDataContractSerializerSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Configuration.NetDataContractSerializerSection" /> class.</summary>
		// Token: 0x06001539 RID: 5433 RVA: 0x00053DA2 File Offset: 0x00051FA2
		public NetDataContractSerializerSection()
		{
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0005414C File Offset: 0x0005234C
		[SecurityCritical]
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static bool TryUnsafeGetSection(out NetDataContractSerializerSection section)
		{
			section = (NetDataContractSerializerSection)ConfigurationManager.GetSection(ConfigurationStrings.NetDataContractSerializerSectionPath);
			return section != null;
		}

		/// <summary>Gets a value that indicates whether unsafe type forwarding is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if unsafe type forwarding is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00054164 File Offset: 0x00052364
		[ConfigurationProperty("enableUnsafeTypeForwarding", DefaultValue = false)]
		public bool EnableUnsafeTypeForwarding
		{
			get
			{
				return (bool)base["enableUnsafeTypeForwarding"];
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00054178 File Offset: 0x00052378
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("enableUnsafeTypeForwarding", typeof(bool), false, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04000A85 RID: 2693
		private ConfigurationPropertyCollection properties;
	}
}
