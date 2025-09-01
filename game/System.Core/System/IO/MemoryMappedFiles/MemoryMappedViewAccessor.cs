using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Represents a randomly accessed view of a memory-mapped file.</summary>
	// Token: 0x02000338 RID: 824
	public sealed class MemoryMappedViewAccessor : UnmanagedMemoryAccessor
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x00053A08 File Offset: 0x00051C08
		[SecurityCritical]
		internal MemoryMappedViewAccessor(MemoryMappedView view)
		{
			this.m_view = view;
			base.Initialize(this.m_view.ViewHandle, this.m_view.PointerOffset, this.m_view.Size, MemoryMappedFile.GetFileAccess(this.m_view.Access));
		}

		/// <summary>Gets a handle to the view of a memory-mapped file.</summary>
		/// <returns>A wrapper for the operating system's handle to the view of the file. </returns>
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x00053A59 File Offset: 0x00051C59
		public SafeMemoryMappedViewHandle SafeMemoryMappedViewHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.m_view == null)
				{
					return null;
				}
				return this.m_view.ViewHandle;
			}
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions] Gets the number of bytes by which the starting position of this view is offset from the beginning of the memory-mapped file. </summary>
		/// <returns>The number of bytes between the starting position of this view and the beginning of the memory-mapped file. </returns>
		/// <exception cref="T:System.InvalidOperationException">The object from which this instance was created is <see langword="null" />. </exception>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00053A70 File Offset: 0x00051C70
		public long PointerOffset
		{
			get
			{
				if (this.m_view == null)
				{
					throw new InvalidOperationException(SR.GetString("The underlying MemoryMappedView object is null."));
				}
				return this.m_view.PointerOffset;
			}
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00053A98 File Offset: 0x00051C98
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_view != null && !this.m_view.IsClosed)
				{
					this.Flush();
				}
			}
			finally
			{
				try
				{
					if (this.m_view != null)
					{
						this.m_view.Dispose();
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		/// <summary>Clears all buffers for this view and causes any buffered data to be written to the underlying file.</summary>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the accessor was closed.</exception>
		// Token: 0x060018DD RID: 6365 RVA: 0x00053B00 File Offset: 0x00051D00
		[SecurityCritical]
		public void Flush()
		{
			if (!base.IsOpen)
			{
				throw new ObjectDisposedException("MemoryMappedViewAccessor", SR.GetString("Cannot access a closed accessor."));
			}
			if (this.m_view != null)
			{
				this.m_view.Flush((IntPtr)base.Capacity);
			}
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0000235B File Offset: 0x0000055B
		internal MemoryMappedViewAccessor()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000C06 RID: 3078
		private MemoryMappedView m_view;
	}
}
