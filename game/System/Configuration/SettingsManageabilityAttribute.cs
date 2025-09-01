using System;

namespace System.Configuration
{
	/// <summary>Specifies special services for application settings properties. This class cannot be inherited.</summary>
	// Token: 0x020001CB RID: 459
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsManageabilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsManageabilityAttribute" /> class.</summary>
		/// <param name="manageability">A <see cref="T:System.Configuration.SettingsManageability" /> value that enumerates the services being requested.</param>
		// Token: 0x06000BF7 RID: 3063 RVA: 0x00031E58 File Offset: 0x00030058
		public SettingsManageabilityAttribute(SettingsManageability manageability)
		{
			this.manageability = manageability;
		}

		/// <summary>Gets the set of special services that have been requested.</summary>
		/// <returns>A value that results from using the logical <see langword="OR" /> operator to combine all the <see cref="T:System.Configuration.SettingsManageability" /> enumeration values corresponding to the requested services.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00031E67 File Offset: 0x00030067
		public SettingsManageability Manageability
		{
			get
			{
				return this.manageability;
			}
		}

		// Token: 0x0400079C RID: 1948
		private SettingsManageability manageability;
	}
}
