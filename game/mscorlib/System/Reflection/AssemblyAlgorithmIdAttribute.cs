using System;
using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Specifies an algorithm to hash all files in an assembly. This class cannot be inherited.</summary>
	// Token: 0x0200087D RID: 2173
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyAlgorithmIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> class with the specified hash algorithm, using one of the members of <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> to represent the hash algorithm.</summary>
		/// <param name="algorithmId">A member of <see langword="AssemblyHashAlgorithm" /> that represents the hash algorithm.</param>
		// Token: 0x0600484E RID: 18510 RVA: 0x000EDFD5 File Offset: 0x000EC1D5
		public AssemblyAlgorithmIdAttribute(AssemblyHashAlgorithm algorithmId)
		{
			this.AlgorithmId = algorithmId;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> class with the specified hash algorithm, using an unsigned integer to represent the hash algorithm.</summary>
		/// <param name="algorithmId">An unsigned integer representing the hash algorithm.</param>
		// Token: 0x0600484F RID: 18511 RVA: 0x000EDFD5 File Offset: 0x000EC1D5
		[CLSCompliant(false)]
		public AssemblyAlgorithmIdAttribute(uint algorithmId)
		{
			this.AlgorithmId = algorithmId;
		}

		/// <summary>Gets the hash algorithm of an assembly manifest's contents.</summary>
		/// <returns>An unsigned integer representing the assembly hash algorithm.</returns>
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x000EDFE4 File Offset: 0x000EC1E4
		[CLSCompliant(false)]
		public uint AlgorithmId
		{
			[CompilerGenerated]
			get
			{
				return this.<AlgorithmId>k__BackingField;
			}
		}

		// Token: 0x04002E49 RID: 11849
		[CompilerGenerated]
		private readonly uint <AlgorithmId>k__BackingField;
	}
}
