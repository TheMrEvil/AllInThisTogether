using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Specifies the name and mode for a code region.</summary>
	// Token: 0x02000326 RID: 806
	[Serializable]
	public class CodeRegionDirective : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class with default values.</summary>
		// Token: 0x060019AE RID: 6574 RVA: 0x0005F624 File Offset: 0x0005D824
		public CodeRegionDirective()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class, specifying its mode and name.</summary>
		/// <param name="regionMode">One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values.</param>
		/// <param name="regionText">The name for the region.</param>
		// Token: 0x060019AF RID: 6575 RVA: 0x00060BF9 File Offset: 0x0005EDF9
		public CodeRegionDirective(CodeRegionMode regionMode, string regionText)
		{
			this.RegionText = regionText;
			this.RegionMode = regionMode;
		}

		/// <summary>Gets or sets the name of the region.</summary>
		/// <returns>The name of the region.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00060C0F File Offset: 0x0005EE0F
		// (set) Token: 0x060019B1 RID: 6577 RVA: 0x00060C20 File Offset: 0x0005EE20
		public string RegionText
		{
			get
			{
				return this._regionText ?? string.Empty;
			}
			set
			{
				this._regionText = value;
			}
		}

		/// <summary>Gets or sets the mode for the region directive.</summary>
		/// <returns>One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values. The default is <see cref="F:System.CodeDom.CodeRegionMode.None" />.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00060C29 File Offset: 0x0005EE29
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x00060C31 File Offset: 0x0005EE31
		public CodeRegionMode RegionMode
		{
			[CompilerGenerated]
			get
			{
				return this.<RegionMode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RegionMode>k__BackingField = value;
			}
		}

		// Token: 0x04000DCE RID: 3534
		private string _regionText;

		// Token: 0x04000DCF RID: 3535
		[CompilerGenerated]
		private CodeRegionMode <RegionMode>k__BackingField;
	}
}
