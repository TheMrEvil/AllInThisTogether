using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a <see langword="VT_UNKNOWN" />.</summary>
	// Token: 0x020006DF RID: 1759
	public sealed class UnknownWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.UnknownWrapper" /> class with the object to be wrapped.</summary>
		/// <param name="obj">The object being wrapped.</param>
		// Token: 0x0600404E RID: 16462 RVA: 0x000E0E00 File Offset: 0x000DF000
		public UnknownWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		/// <summary>Gets the object contained by this wrapper.</summary>
		/// <returns>The wrapped object.</returns>
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600404F RID: 16463 RVA: 0x000E0E0F File Offset: 0x000DF00F
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002A21 RID: 10785
		private object m_WrappedObject;
	}
}
