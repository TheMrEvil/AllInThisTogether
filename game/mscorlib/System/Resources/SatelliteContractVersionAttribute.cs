using System;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	/// <summary>Instructs a <see cref="T:System.Resources.ResourceManager" /> object to ask for a particular version of a satellite assembly.</summary>
	// Token: 0x02000861 RID: 2145
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> class.</summary>
		/// <param name="version">A string that specifies the version of the satellite assemblies to load.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="version" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004759 RID: 18265 RVA: 0x000E87A0 File Offset: 0x000E69A0
		public SatelliteContractVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.Version = version;
		}

		/// <summary>Gets the version of the satellite assemblies with the required resources.</summary>
		/// <returns>A string that contains the version of the satellite assemblies with the required resources.</returns>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600475A RID: 18266 RVA: 0x000E87BD File Offset: 0x000E69BD
		public string Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
		}

		// Token: 0x04002DC7 RID: 11719
		[CompilerGenerated]
		private readonly string <Version>k__BackingField;
	}
}
