using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents a wrapper class for operating system handles. This class must be inherited.</summary>
	// Token: 0x0200071D RID: 1821
	[SecurityCritical]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class with the specified invalid handle value.</summary>
		/// <param name="invalidHandleValue">The value of an invalid handle (usually 0 or -1).  Your implementation of <see cref="P:System.Runtime.InteropServices.SafeHandle.IsInvalid" /> should return <see langword="true" /> for this value.</param>
		/// <param name="ownsHandle">
		///   <see langword="true" /> to reliably let <see cref="T:System.Runtime.InteropServices.SafeHandle" /> release the handle during the finalization phase; otherwise, <see langword="false" /> (not recommended).</param>
		/// <exception cref="T:System.TypeLoadException">The derived class resides in an assembly without unmanaged code access permission.</exception>
		// Token: 0x060040ED RID: 16621 RVA: 0x000E17D4 File Offset: 0x000DF9D4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
		{
			this.handle = invalidHandleValue;
			this._state = 4;
			this._ownsHandle = ownsHandle;
			if (!ownsHandle)
			{
				GC.SuppressFinalize(this);
			}
			this._fullyInitialized = true;
		}

		/// <summary>Frees all resources associated with the handle.</summary>
		// Token: 0x060040EE RID: 16622 RVA: 0x000E1804 File Offset: 0x000DFA04
		[SecuritySafeCritical]
		~SafeHandle()
		{
			this.Dispose(false);
		}

		/// <summary>Sets the handle to the specified pre-existing handle.</summary>
		/// <param name="handle">The pre-existing handle to use.</param>
		// Token: 0x060040EF RID: 16623 RVA: 0x000E1834 File Offset: 0x000DFA34
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		/// <summary>Returns the value of the <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> field.</summary>
		/// <returns>An <see langword="IntPtr" /> representing the value of the <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> field. If the handle has been marked invalid with <see cref="M:System.Runtime.InteropServices.SafeHandle.SetHandleAsInvalid" />, this method still returns the original handle value, which can be a stale value.</returns>
		// Token: 0x060040F0 RID: 16624 RVA: 0x000E183D File Offset: 0x000DFA3D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		/// <summary>Gets a value indicating whether the handle is closed.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is closed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x000E1845 File Offset: 0x000DFA45
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return (this._state & 1) == 1;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the handle value is invalid.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle value is invalid; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060040F2 RID: 16626
		public abstract bool IsInvalid { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get; }

		/// <summary>Marks the handle for releasing and freeing resources.</summary>
		// Token: 0x060040F3 RID: 16627 RVA: 0x000E1852 File Offset: 0x000DFA52
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class.</summary>
		// Token: 0x060040F4 RID: 16628 RVA: 0x000E1852 File Offset: 0x000DFA52
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class specifying whether to perform a normal dispose operation.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> for a normal dispose operation; <see langword="false" /> to finalize the handle.</param>
		// Token: 0x060040F5 RID: 16629 RVA: 0x000E185B File Offset: 0x000DFA5B
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalDispose();
				return;
			}
			this.InternalFinalize();
		}

		/// <summary>When overridden in a derived class, executes the code required to free the handle.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is released successfully; otherwise, in the event of a catastrophic failure, <see langword="false" />. In this case, it generates a releaseHandleFailed Managed Debugging Assistant.</returns>
		// Token: 0x060040F6 RID: 16630
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		/// <summary>Marks a handle as no longer used.</summary>
		// Token: 0x060040F7 RID: 16631 RVA: 0x000E1870 File Offset: 0x000DFA70
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void SetHandleAsInvalid()
		{
			try
			{
			}
			finally
			{
				int state;
				int value;
				do
				{
					state = this._state;
					value = (state | 1);
				}
				while (Interlocked.CompareExchange(ref this._state, value, state) != state);
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Manually increments the reference counter on <see cref="T:System.Runtime.InteropServices.SafeHandle" /> instances.</summary>
		/// <param name="success">
		///   <see langword="true" /> if the reference counter was successfully incremented; otherwise, <see langword="false" />.</param>
		// Token: 0x060040F8 RID: 16632 RVA: 0x000E18B4 File Offset: 0x000DFAB4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void DangerousAddRef(ref bool success)
		{
			try
			{
			}
			finally
			{
				if (!this._fullyInitialized)
				{
					throw new InvalidOperationException();
				}
				for (;;)
				{
					int state = this._state;
					if ((state & 1) != 0)
					{
						break;
					}
					int value = state + 4;
					if (Interlocked.CompareExchange(ref this._state, value, state) == state)
					{
						goto Block_5;
					}
				}
				throw new ObjectDisposedException(null, "Safe handle has been closed");
				Block_5:
				success = true;
			}
		}

		/// <summary>Manually decrements the reference counter on a <see cref="T:System.Runtime.InteropServices.SafeHandle" /> instance.</summary>
		// Token: 0x060040F9 RID: 16633 RVA: 0x000E1914 File Offset: 0x000DFB14
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void DangerousRelease()
		{
			this.DangerousReleaseInternal(false);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000E191D File Offset: 0x000DFB1D
		private void InternalDispose()
		{
			if (!this._fullyInitialized)
			{
				throw new InvalidOperationException();
			}
			this.DangerousReleaseInternal(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000E193A File Offset: 0x000DFB3A
		private void InternalFinalize()
		{
			if (this._fullyInitialized)
			{
				this.DangerousReleaseInternal(true);
			}
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x000E194C File Offset: 0x000DFB4C
		private void DangerousReleaseInternal(bool dispose)
		{
			try
			{
			}
			finally
			{
				if (!this._fullyInitialized)
				{
					throw new InvalidOperationException();
				}
				bool flag;
				for (;;)
				{
					int state = this._state;
					if (dispose && (state & 2) != 0)
					{
						break;
					}
					if ((state & 2147483644) == 0)
					{
						goto Block_6;
					}
					flag = ((state & 2147483644) == 4 && (state & 1) == 0 && this._ownsHandle && !this.IsInvalid);
					int num = state - 4;
					if ((state & 2147483644) == 4)
					{
						num |= 1;
					}
					if (dispose)
					{
						num |= 2;
					}
					if (Interlocked.CompareExchange(ref this._state, num, state) == state)
					{
						goto IL_9A;
					}
				}
				flag = false;
				goto IL_9A;
				Block_6:
				throw new ObjectDisposedException(null, "Safe handle has been closed");
				IL_9A:
				if (flag)
				{
					this.ReleaseHandle();
				}
			}
		}

		/// <summary>Specifies the handle to be wrapped.</summary>
		// Token: 0x04002B02 RID: 11010
		protected IntPtr handle;

		// Token: 0x04002B03 RID: 11011
		private int _state;

		// Token: 0x04002B04 RID: 11012
		private bool _ownsHandle;

		// Token: 0x04002B05 RID: 11013
		private bool _fullyInitialized;

		// Token: 0x04002B06 RID: 11014
		private const int RefCount_Mask = 2147483644;

		// Token: 0x04002B07 RID: 11015
		private const int RefCount_One = 4;

		// Token: 0x0200071E RID: 1822
		private enum State
		{
			// Token: 0x04002B09 RID: 11017
			Closed = 1,
			// Token: 0x04002B0A RID: 11018
			Disposed
		}
	}
}
