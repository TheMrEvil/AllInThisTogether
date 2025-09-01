using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies a source <see cref="T:System.Type" /> in another assembly.</summary>
	// Token: 0x02000809 RID: 2057
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class TypeForwardedFromAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TypeForwardedFromAttribute" /> class.</summary>
		/// <param name="assemblyFullName">The source <see cref="T:System.Type" /> in another assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFullName" /> is <see langword="null" /> or empty.</exception>
		// Token: 0x06004626 RID: 17958 RVA: 0x000E5957 File Offset: 0x000E3B57
		public TypeForwardedFromAttribute(string assemblyFullName)
		{
			if (string.IsNullOrEmpty(assemblyFullName))
			{
				throw new ArgumentNullException("assemblyFullName");
			}
			this.AssemblyFullName = assemblyFullName;
		}

		/// <summary>Gets the assembly-qualified name of the source type.</summary>
		/// <returns>The assembly-qualified name of the source type.</returns>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x000E5979 File Offset: 0x000E3B79
		public string AssemblyFullName
		{
			[CompilerGenerated]
			get
			{
				return this.<AssemblyFullName>k__BackingField;
			}
		}

		// Token: 0x04002D43 RID: 11587
		[CompilerGenerated]
		private readonly string <AssemblyFullName>k__BackingField;
	}
}
