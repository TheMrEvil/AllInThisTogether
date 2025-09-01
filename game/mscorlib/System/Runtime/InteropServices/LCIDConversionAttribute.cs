using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that a method's unmanaged signature expects a locale identifier (LCID) parameter.</summary>
	// Token: 0x020006ED RID: 1773
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class LCIDConversionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="LCIDConversionAttribute" /> class with the position of the LCID in the unmanaged signature.</summary>
		/// <param name="lcid">Indicates the position of the LCID argument in the unmanaged signature, where 0 is the first argument.</param>
		// Token: 0x06004070 RID: 16496 RVA: 0x000E0FCE File Offset: 0x000DF1CE
		public LCIDConversionAttribute(int lcid)
		{
			this._val = lcid;
		}

		/// <summary>Gets the position of the LCID argument in the unmanaged signature.</summary>
		/// <returns>The position of the LCID argument in the unmanaged signature, where 0 is the first argument.</returns>
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x000E0FDD File Offset: 0x000DF1DD
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A3C RID: 10812
		internal int _val;
	}
}
