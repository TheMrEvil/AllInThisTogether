using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ColorPaletteAttribute : Attribute
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002497 File Offset: 0x00000697
		public ColorPaletteAttribute()
		{
			this.PaletteName = null;
			this.ShowAlpha = true;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000024AD File Offset: 0x000006AD
		public ColorPaletteAttribute(string paletteName)
		{
			this.PaletteName = paletteName;
			this.ShowAlpha = true;
		}

		// Token: 0x04000032 RID: 50
		public string PaletteName;

		// Token: 0x04000033 RID: 51
		public bool ShowAlpha;
	}
}
