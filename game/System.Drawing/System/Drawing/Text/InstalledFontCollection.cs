using System;

namespace System.Drawing.Text
{
	/// <summary>Represents the fonts installed on the system. This class cannot be inherited.</summary>
	// Token: 0x020000B4 RID: 180
	public sealed class InstalledFontCollection : FontCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Text.InstalledFontCollection" /> class.</summary>
		// Token: 0x06000A69 RID: 2665 RVA: 0x00017C38 File Offset: 0x00015E38
		public InstalledFontCollection()
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipNewInstalledFontCollection(out this._nativeFontCollection));
		}
	}
}
