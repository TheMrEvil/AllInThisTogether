using System;

namespace System.Drawing
{
	/// <summary>Specifies that, when interpreting <see cref="T:System.Drawing.ToolboxBitmapAttribute" /> declarations, the assembly should look for the indicated resources in the same assembly, but with the <see cref="P:System.Drawing.Configuration.SystemDrawingSection.BitmapSuffix" /> configuration value appended to the declared file name.</summary>
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Assembly)]
	public class BitmapSuffixInSameAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.BitmapSuffixInSameAssemblyAttribute" /> class.</summary>
		// Token: 0x06000045 RID: 69 RVA: 0x00002064 File Offset: 0x00000264
		public BitmapSuffixInSameAssemblyAttribute()
		{
		}
	}
}
