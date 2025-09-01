using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class represents a configuration element for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
	// Token: 0x020002AB RID: 683
	[MonoTODO]
	public sealed class ExtendedProtectionPolicyElement : ConfigurationElement
	{
		// Token: 0x06001541 RID: 5441 RVA: 0x00055B80 File Offset: 0x00053D80
		static ExtendedProtectionPolicyElement()
		{
			Type typeFromHandle = typeof(ExtendedProtectionPolicyElement);
			ExtendedProtectionPolicyElement.custom_service_names = ConfigUtil.BuildProperty(typeFromHandle, "CustomServiceNames");
			ExtendedProtectionPolicyElement.policy_enforcement = ConfigUtil.BuildProperty(typeFromHandle, "PolicyEnforcement");
			ExtendedProtectionPolicyElement.protection_scenario = ConfigUtil.BuildProperty(typeFromHandle, "ProtectionScenario");
			foreach (ConfigurationProperty property in new ConfigurationProperty[]
			{
				ExtendedProtectionPolicyElement.custom_service_names,
				ExtendedProtectionPolicyElement.policy_enforcement,
				ExtendedProtectionPolicyElement.protection_scenario
			})
			{
				ExtendedProtectionPolicyElement.properties.Add(property);
			}
		}

		/// <summary>Gets or sets the custom Service Provider Name (SPN) list used to match against a client's SPN for this configuration policy element.</summary>
		/// <returns>A collection that includes the custom SPN list used to match against a client's SPN.</returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x00055C0C File Offset: 0x00053E0C
		[ConfigurationProperty("customServiceNames")]
		public ServiceNameElementCollection CustomServiceNames
		{
			get
			{
				return (ServiceNameElementCollection)base[ExtendedProtectionPolicyElement.custom_service_names];
			}
		}

		/// <summary>Gets or sets the policy enforcement value for this configuration policy element.</summary>
		/// <returns>One of the enumeration values that indicates when the extended protection policy should be enforced.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x00055C1E File Offset: 0x00053E1E
		// (set) Token: 0x06001544 RID: 5444 RVA: 0x00055C30 File Offset: 0x00053E30
		[ConfigurationProperty("policyEnforcement")]
		public PolicyEnforcement PolicyEnforcement
		{
			get
			{
				return (PolicyEnforcement)base[ExtendedProtectionPolicyElement.policy_enforcement];
			}
			set
			{
				base[ExtendedProtectionPolicyElement.policy_enforcement] = value;
			}
		}

		/// <summary>Gets or sets the kind of protection enforced by the extended protection policy for this configuration policy element.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x00055C43 File Offset: 0x00053E43
		// (set) Token: 0x06001546 RID: 5446 RVA: 0x00055C55 File Offset: 0x00053E55
		[ConfigurationProperty("protectionScenario", DefaultValue = ProtectionScenario.TransportSelected)]
		public ProtectionScenario ProtectionScenario
		{
			get
			{
				return (ProtectionScenario)base[ExtendedProtectionPolicyElement.protection_scenario];
			}
			set
			{
				base[ExtendedProtectionPolicyElement.protection_scenario] = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00055C68 File Offset: 0x00053E68
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ExtendedProtectionPolicyElement.properties;
			}
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement.BuildPolicy" /> method builds a new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance based on the properties set on the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class.</summary>
		/// <returns>A new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance that represents the extended protection policy created.</returns>
		// Token: 0x06001548 RID: 5448 RVA: 0x0000829A File Offset: 0x0000649A
		public ExtendedProtectionPolicy BuildPolicy()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class.</summary>
		// Token: 0x06001549 RID: 5449 RVA: 0x00031238 File Offset: 0x0002F438
		public ExtendedProtectionPolicyElement()
		{
		}

		// Token: 0x04000C00 RID: 3072
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000C01 RID: 3073
		private static ConfigurationProperty custom_service_names;

		// Token: 0x04000C02 RID: 3074
		private static ConfigurationProperty policy_enforcement;

		// Token: 0x04000C03 RID: 3075
		private static ConfigurationProperty protection_scenario;
	}
}
