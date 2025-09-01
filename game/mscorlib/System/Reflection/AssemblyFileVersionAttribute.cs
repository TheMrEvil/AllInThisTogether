using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Instructs a compiler to use a specific version number for the Win32 file version resource. The Win32 file version is not required to be the same as the assembly's version number.</summary>
	// Token: 0x02000886 RID: 2182
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFileVersionAttribute" /> class, specifying the file version.</summary>
		/// <param name="version">The file version.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="version" /> is <see langword="null" />.</exception>
		// Token: 0x0600485F RID: 18527 RVA: 0x000EE08D File Offset: 0x000EC28D
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.Version = version;
		}

		/// <summary>Gets the Win32 file version resource name.</summary>
		/// <returns>A string containing the file version resource name.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06004860 RID: 18528 RVA: 0x000EE0AA File Offset: 0x000EC2AA
		public string Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
		}

		// Token: 0x04002E54 RID: 11860
		[CompilerGenerated]
		private readonly string <Version>k__BackingField;
	}
}
