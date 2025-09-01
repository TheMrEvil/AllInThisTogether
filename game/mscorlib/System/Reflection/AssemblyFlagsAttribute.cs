using System;

namespace System.Reflection
{
	/// <summary>Specifies a bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags for an assembly, describing just-in-time (JIT) compiler options, whether the assembly is retargetable, and whether it has a full or tokenized public key. This class cannot be inherited.</summary>
	// Token: 0x02000887 RID: 2183
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFlagsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> class with the specified combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an unsigned integer value.</summary>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an unsigned integer value, representing just-in-time (JIT) compiler options, longevity, whether an assembly is retargetable, and whether it has a full or tokenized public key.</param>
		// Token: 0x06004861 RID: 18529 RVA: 0x000EE0B2 File Offset: 0x000EC2B2
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public AssemblyFlagsAttribute(uint flags)
		{
			this._flags = (AssemblyNameFlags)flags;
		}

		/// <summary>Gets an unsigned integer value representing the combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags specified when this attribute instance was created.</summary>
		/// <returns>An unsigned integer value representing a bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x000EE0C1 File Offset: 0x000EC2C1
		[CLSCompliant(false)]
		[Obsolete("This property has been deprecated. Please use AssemblyFlags instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public uint Flags
		{
			get
			{
				return (uint)this._flags;
			}
		}

		/// <summary>Gets an integer value representing the combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags specified when this attribute instance was created.</summary>
		/// <returns>An integer value representing a bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags.</returns>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x000EE0C1 File Offset: 0x000EC2C1
		public int AssemblyFlags
		{
			get
			{
				return (int)this._flags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> class with the specified combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an integer value.</summary>
		/// <param name="assemblyFlags">A bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an integer value, representing just-in-time (JIT) compiler options, longevity, whether an assembly is retargetable, and whether it has a full or tokenized public key.</param>
		// Token: 0x06004864 RID: 18532 RVA: 0x000EE0B2 File Offset: 0x000EC2B2
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public AssemblyFlagsAttribute(int assemblyFlags)
		{
			this._flags = (AssemblyNameFlags)assemblyFlags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> class with the specified combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags.</summary>
		/// <param name="assemblyFlags">A bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags representing just-in-time (JIT) compiler options, longevity, whether an assembly is retargetable, and whether it has a full or tokenized public key.</param>
		// Token: 0x06004865 RID: 18533 RVA: 0x000EE0B2 File Offset: 0x000EC2B2
		public AssemblyFlagsAttribute(AssemblyNameFlags assemblyFlags)
		{
			this._flags = assemblyFlags;
		}

		// Token: 0x04002E55 RID: 11861
		private AssemblyNameFlags _flags;
	}
}
