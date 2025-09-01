using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Specifies the version of the assembly being attributed.</summary>
	// Token: 0x02000891 RID: 2193
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="AssemblyVersionAttribute" /> class with the version number of the assembly being attributed.</summary>
		/// <param name="version">The version number of the attributed assembly.</param>
		// Token: 0x06004878 RID: 18552 RVA: 0x000EE19F File Offset: 0x000EC39F
		public AssemblyVersionAttribute(string version)
		{
			this.Version = version;
		}

		/// <summary>Gets the version number of the attributed assembly.</summary>
		/// <returns>A string containing the assembly version number.</returns>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x000EE1AE File Offset: 0x000EC3AE
		public string Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
		}

		// Token: 0x04002E66 RID: 11878
		[CompilerGenerated]
		private readonly string <Version>k__BackingField;
	}
}
