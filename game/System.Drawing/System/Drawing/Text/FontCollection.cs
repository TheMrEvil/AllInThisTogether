using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Text
{
	/// <summary>Provides a base class for installed and private font collections.</summary>
	// Token: 0x020000B1 RID: 177
	public abstract class FontCollection : IDisposable
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x00017B65 File Offset: 0x00015D65
		internal FontCollection()
		{
			this._nativeFontCollection = IntPtr.Zero;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Text.FontCollection" />.</summary>
		// Token: 0x06000A65 RID: 2661 RVA: 0x00017B78 File Offset: 0x00015D78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Text.FontCollection" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000A66 RID: 2662 RVA: 0x000049FE File Offset: 0x00002BFE
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets the array of <see cref="T:System.Drawing.FontFamily" /> objects associated with this <see cref="T:System.Drawing.Text.FontCollection" />.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects.</returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00017B88 File Offset: 0x00015D88
		public FontFamily[] Families
		{
			get
			{
				int num = 0;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetFontCollectionFamilyCount(new HandleRef(this, this._nativeFontCollection), out num));
				IntPtr[] array = new IntPtr[num];
				int num2 = 0;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetFontCollectionFamilyList(new HandleRef(this, this._nativeFontCollection), num, array, out num2));
				FontFamily[] array2 = new FontFamily[num2];
				for (int i = 0; i < num2; i++)
				{
					IntPtr fntfamily;
					GDIPlus.GdipCloneFontFamily(new HandleRef(null, array[i]), out fntfamily);
					array2[i] = new FontFamily(fntfamily);
				}
				return array2;
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000A68 RID: 2664 RVA: 0x00017C08 File Offset: 0x00015E08
		~FontCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x0400064D RID: 1613
		internal IntPtr _nativeFontCollection;
	}
}
