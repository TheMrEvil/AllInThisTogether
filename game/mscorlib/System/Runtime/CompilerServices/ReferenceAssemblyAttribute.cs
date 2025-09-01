using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Identifies an assembly as a reference assembly, which contains metadata but no executable code.</summary>
	// Token: 0x02000800 RID: 2048
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[Serializable]
	public sealed class ReferenceAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" /> class.</summary>
		// Token: 0x06004611 RID: 17937 RVA: 0x00002050 File Offset: 0x00000250
		public ReferenceAssemblyAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" /> class by using the specified description.</summary>
		/// <param name="description">The description of the reference assembly.</param>
		// Token: 0x06004612 RID: 17938 RVA: 0x000E582E File Offset: 0x000E3A2E
		public ReferenceAssemblyAttribute(string description)
		{
			this.Description = description;
		}

		/// <summary>Gets the description of the reference assembly.</summary>
		/// <returns>The description of the reference assembly.</returns>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06004613 RID: 17939 RVA: 0x000E583D File Offset: 0x000E3A3D
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
		}

		// Token: 0x04002D3C RID: 11580
		[CompilerGenerated]
		private readonly string <Description>k__BackingField;
	}
}
