using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	/// <summary>Specifies patch band information for targeted patching of the .NET Framework.</summary>
	// Token: 0x0200054C RID: 1356
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTargetedPatchBandAttribute : Attribute
	{
		/// <summary>Gets the patch band.</summary>
		/// <returns>The patch band information.</returns>
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060035A7 RID: 13735 RVA: 0x000C1F5D File Offset: 0x000C015D
		public string TargetedPatchBand
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetedPatchBand>k__BackingField;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.AssemblyTargetedPatchBandAttribute" /> class.</summary>
		/// <param name="targetedPatchBand">The patch band.</param>
		// Token: 0x060035A8 RID: 13736 RVA: 0x000C1F65 File Offset: 0x000C0165
		public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
		{
			this.TargetedPatchBand = targetedPatchBand;
		}

		// Token: 0x04002503 RID: 9475
		[CompilerGenerated]
		private readonly string <TargetedPatchBand>k__BackingField;
	}
}
