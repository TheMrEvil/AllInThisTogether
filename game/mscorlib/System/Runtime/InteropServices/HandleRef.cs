using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps a managed object holding a handle to a resource that is passed to unmanaged code using platform invoke.</summary>
	// Token: 0x020006C5 RID: 1733
	public readonly struct HandleRef
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleRef" /> class with the object to wrap and a handle to the resource used by unmanaged code.</summary>
		/// <param name="wrapper">A managed object that should not be finalized until the platform invoke call returns.</param>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that indicates a handle to a resource.</param>
		// Token: 0x06003FD7 RID: 16343 RVA: 0x000DFE33 File Offset: 0x000DE033
		public HandleRef(object wrapper, IntPtr handle)
		{
			this._wrapper = wrapper;
			this._handle = handle;
		}

		/// <summary>Gets the object holding the handle to a resource.</summary>
		/// <returns>The object holding the handle to a resource.</returns>
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x000DFE43 File Offset: 0x000DE043
		public object Wrapper
		{
			get
			{
				return this._wrapper;
			}
		}

		/// <summary>Gets the handle to a resource.</summary>
		/// <returns>The handle to a resource.</returns>
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x000DFE4B File Offset: 0x000DE04B
		public IntPtr Handle
		{
			get
			{
				return this._handle;
			}
		}

		/// <summary>Returns the handle to a resource of the specified <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</summary>
		/// <param name="value">The object that needs a handle.</param>
		/// <returns>The handle to a resource of the specified <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</returns>
		// Token: 0x06003FDA RID: 16346 RVA: 0x000DFE4B File Offset: 0x000DE04B
		public static explicit operator IntPtr(HandleRef value)
		{
			return value._handle;
		}

		/// <summary>Returns the internal integer representation of a <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</summary>
		/// <param name="value">A <see cref="T:System.Runtime.InteropServices.HandleRef" /> object to retrieve an internal integer representation from.</param>
		/// <returns>An <see cref="T:System.IntPtr" /> object that represents a <see cref="T:System.Runtime.InteropServices.HandleRef" /> object.</returns>
		// Token: 0x06003FDB RID: 16347 RVA: 0x000DFE4B File Offset: 0x000DE04B
		public static IntPtr ToIntPtr(HandleRef value)
		{
			return value._handle;
		}

		// Token: 0x040029F3 RID: 10739
		private readonly object _wrapper;

		// Token: 0x040029F4 RID: 10740
		private readonly IntPtr _handle;
	}
}
