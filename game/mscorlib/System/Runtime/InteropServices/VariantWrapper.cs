using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Marshals data of type <see langword="VT_VARIANT | VT_BYREF" /> from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x020006E0 RID: 1760
	public sealed class VariantWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> class for the specified <see cref="T:System.Object" /> parameter.</summary>
		/// <param name="obj">The object to marshal.</param>
		// Token: 0x06004050 RID: 16464 RVA: 0x000E0E17 File Offset: 0x000DF017
		public VariantWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		/// <summary>Gets the object wrapped by the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> object.</summary>
		/// <returns>The object wrapped by the <see cref="T:System.Runtime.InteropServices.VariantWrapper" /> object.</returns>
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06004051 RID: 16465 RVA: 0x000E0E26 File Offset: 0x000DF026
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002A22 RID: 10786
		private object m_WrappedObject;
	}
}
