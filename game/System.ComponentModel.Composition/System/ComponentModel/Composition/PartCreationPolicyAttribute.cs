using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies the <see cref="P:System.ComponentModel.Composition.PartCreationPolicyAttribute.CreationPolicy" /> for a part.</summary>
	// Token: 0x02000050 RID: 80
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class PartCreationPolicyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.PartCreationPolicyAttribute" /> class with the specified creation policy.</summary>
		/// <param name="creationPolicy">The creation policy to use.</param>
		// Token: 0x06000220 RID: 544 RVA: 0x000069AB File Offset: 0x00004BAB
		public PartCreationPolicyAttribute(CreationPolicy creationPolicy)
		{
			this.CreationPolicy = creationPolicy;
		}

		/// <summary>Gets or sets a value that indicates the creation policy of the attributed part.</summary>
		/// <returns>One of the <see cref="P:System.ComponentModel.Composition.PartCreationPolicyAttribute.CreationPolicy" /> values that indicates the creation policy of the attributed part. The default is <see cref="F:System.ComponentModel.Composition.CreationPolicy.Any" />.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000069BA File Offset: 0x00004BBA
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000069C2 File Offset: 0x00004BC2
		public CreationPolicy CreationPolicy
		{
			[CompilerGenerated]
			get
			{
				return this.<CreationPolicy>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CreationPolicy>k__BackingField = value;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000069CB File Offset: 0x00004BCB
		// Note: this type is marked as 'beforefieldinit'.
		static PartCreationPolicyAttribute()
		{
		}

		// Token: 0x040000E3 RID: 227
		internal static PartCreationPolicyAttribute Default = new PartCreationPolicyAttribute(CreationPolicy.Any);

		// Token: 0x040000E4 RID: 228
		internal static PartCreationPolicyAttribute Shared = new PartCreationPolicyAttribute(CreationPolicy.Shared);

		// Token: 0x040000E5 RID: 229
		[CompilerGenerated]
		private CreationPolicy <CreationPolicy>k__BackingField;
	}
}
