using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents a wrapper class for handle resources.</summary>
	// Token: 0x02000715 RID: 1813
	[SecurityCritical]
	public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> class with the specified invalid handle value.</summary>
		/// <param name="invalidHandleValue">The value of an invalid handle (usually 0 or -1).</param>
		/// <exception cref="T:System.TypeLoadException">The derived class resides in an assembly without unmanaged code access permission.</exception>
		// Token: 0x060040C9 RID: 16585 RVA: 0x000E165C File Offset: 0x000DF85C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandle(IntPtr invalidHandleValue)
		{
			this.handle = invalidHandleValue;
			this._isClosed = false;
		}

		/// <summary>Frees all resources associated with the handle.</summary>
		// Token: 0x060040CA RID: 16586 RVA: 0x000E1674 File Offset: 0x000DF874
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x000E16A4 File Offset: 0x000DF8A4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		private void Cleanup()
		{
			if (this.IsClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this.IsInvalid)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!this.ReleaseHandle())
			{
				CriticalHandle.FireCustomerDebugProbe();
			}
			Marshal.SetLastWin32Error(lastWin32Error);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x00004BF9 File Offset: 0x00002DF9
		private static void FireCustomerDebugProbe()
		{
		}

		/// <summary>Sets the handle to the specified pre-existing handle.</summary>
		/// <param name="handle">The pre-existing handle to use.</param>
		// Token: 0x060040CD RID: 16589 RVA: 0x000E16DC File Offset: 0x000DF8DC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		/// <summary>Gets a value indicating whether the handle is closed.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is closed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060040CE RID: 16590 RVA: 0x000E16E5 File Offset: 0x000DF8E5
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._isClosed;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the handle value is invalid.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060040CF RID: 16591
		public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get; }

		/// <summary>Marks the handle for releasing and freeing resources.</summary>
		// Token: 0x060040D0 RID: 16592 RVA: 0x000E16ED File Offset: 0x000DF8ED
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.InteropServices.CriticalHandle" />.</summary>
		// Token: 0x060040D1 RID: 16593 RVA: 0x000E16ED File Offset: 0x000DF8ED
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> class specifying whether to perform a normal dispose operation.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> for a normal dispose operation; <see langword="false" /> to finalize the handle.</param>
		// Token: 0x060040D2 RID: 16594 RVA: 0x000E16F6 File Offset: 0x000DF8F6
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		protected virtual void Dispose(bool disposing)
		{
			this.Cleanup();
		}

		/// <summary>Marks a handle as invalid.</summary>
		// Token: 0x060040D3 RID: 16595 RVA: 0x000E16FE File Offset: 0x000DF8FE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void SetHandleAsInvalid()
		{
			this._isClosed = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, executes the code required to free the handle.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is released successfully; otherwise, in the event of a catastrophic failure, <see langword="false" />. In this case, it generates a releaseHandleFailed Managed Debugging Assistant.</returns>
		// Token: 0x060040D4 RID: 16596
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		/// <summary>Specifies the handle to be wrapped.</summary>
		// Token: 0x04002AF8 RID: 11000
		protected IntPtr handle;

		// Token: 0x04002AF9 RID: 11001
		private bool _isClosed;
	}
}
