using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to use their standard obfuscation rules for the appropriate assembly type.</summary>
	// Token: 0x020008B3 RID: 2227
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class ObfuscateAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ObfuscateAssemblyAttribute" /> class, specifying whether the assembly to be obfuscated is public or private.</summary>
		/// <param name="assemblyIsPrivate">
		///   <see langword="true" /> if the assembly is used within the scope of one application; otherwise, <see langword="false" />.</param>
		// Token: 0x060049B2 RID: 18866 RVA: 0x000EF16C File Offset: 0x000ED36C
		public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
		{
			this.AssemblyIsPrivate = assemblyIsPrivate;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether the assembly was marked private.</summary>
		/// <returns>
		///   <see langword="true" /> if the assembly was marked private; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x000EF182 File Offset: 0x000ED382
		public bool AssemblyIsPrivate
		{
			[CompilerGenerated]
			get
			{
				return this.<AssemblyIsPrivate>k__BackingField;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove the attribute after processing.</summary>
		/// <returns>
		///   <see langword="true" /> if the obfuscation tool should remove the attribute after processing; otherwise, <see langword="false" />. The default value for this property is <see langword="true" />.</returns>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x000EF18A File Offset: 0x000ED38A
		// (set) Token: 0x060049B5 RID: 18869 RVA: 0x000EF192 File Offset: 0x000ED392
		public bool StripAfterObfuscation
		{
			[CompilerGenerated]
			get
			{
				return this.<StripAfterObfuscation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StripAfterObfuscation>k__BackingField = value;
			}
		} = true;

		// Token: 0x04002EF3 RID: 12019
		[CompilerGenerated]
		private readonly bool <AssemblyIsPrivate>k__BackingField;

		// Token: 0x04002EF4 RID: 12020
		[CompilerGenerated]
		private bool <StripAfterObfuscation>k__BackingField;
	}
}
