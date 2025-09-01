using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Specifies the build configuration, such as retail or debug, for an assembly.</summary>
	// Token: 0x0200087F RID: 2175
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyConfigurationAttribute" /> class.</summary>
		/// <param name="configuration">The assembly configuration.</param>
		// Token: 0x06004853 RID: 18515 RVA: 0x000EE003 File Offset: 0x000EC203
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.Configuration = configuration;
		}

		/// <summary>Gets assembly configuration information.</summary>
		/// <returns>A string containing the assembly configuration information.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06004854 RID: 18516 RVA: 0x000EE012 File Offset: 0x000EC212
		public string Configuration
		{
			[CompilerGenerated]
			get
			{
				return this.<Configuration>k__BackingField;
			}
		}

		// Token: 0x04002E4B RID: 11851
		[CompilerGenerated]
		private readonly string <Configuration>k__BackingField;
	}
}
