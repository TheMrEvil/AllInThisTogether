using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Represents a view of a memory-mapped file as a sequentially accessed stream.</summary>
	// Token: 0x02000339 RID: 825
	public sealed class MemoryMappedViewStream : UnmanagedMemoryStream
	{
		// Token: 0x060018DF RID: 6367 RVA: 0x00053B40 File Offset: 0x00051D40
		[SecurityCritical]
		internal MemoryMappedViewStream(MemoryMappedView view)
		{
			this.m_view = view;
			base.Initialize(this.m_view.ViewHandle, this.m_view.PointerOffset, this.m_view.Size, MemoryMappedFile.GetFileAccess(this.m_view.Access));
		}

		/// <summary>Gets a handle to the view of a memory-mapped file.</summary>
		/// <returns>A wrapper for the operating system's handle to the view of the file. </returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x00053B91 File Offset: 0x00051D91
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

		/// <summary>Sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x060018E1 RID: 6369 RVA: 0x00053BA8 File Offset: 0x00051DA8
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("MemoryMappedViewStreams are fixed length."));
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions] Gets the number of bytes by which the starting position of this view is offset from the beginning of the memory-mapped file.</summary>
		/// <returns>The number of bytes between the starting position of this view and the beginning of the memory-mapped file. </returns>
		/// <exception cref="T:System.InvalidOperationException">The object from which this instance was created is <see langword="null" />. </exception>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00053BB9 File Offset: 0x00051DB9
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

		// Token: 0x060018E3 RID: 6371 RVA: 0x00053BE0 File Offset: 0x00051DE0
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

		/// <summary>Clears all buffers for this stream and causes any buffered data to be written to the underlying file.</summary>
		// Token: 0x060018E4 RID: 6372 RVA: 0x00053C48 File Offset: 0x00051E48
		[SecurityCritical]
		public override void Flush()
		{
			if (!this.CanSeek)
			{
				__Error.StreamIsClosed();
			}
			if (this.m_view != null)
			{
				this.m_view.Flush((IntPtr)base.Capacity);
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0000235B File Offset: 0x0000055B
		internal MemoryMappedViewStream()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000C07 RID: 3079
		private MemoryMappedView m_view;
	}
}
