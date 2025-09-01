using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System.Threading
{
	/// <summary>Represents an I/O handle that is bound to the system thread pool and enables low-level components to receive notifications for asynchronous I/O operations.</summary>
	// Token: 0x020002B5 RID: 693
	public sealed class ThreadPoolBoundHandle : IDisposable, IDeferredDisposable
	{
		// Token: 0x06001E53 RID: 7763 RVA: 0x000175B9 File Offset: 0x000157B9
		static ThreadPoolBoundHandle()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00070522 File Offset: 0x0006E722
		private ThreadPoolBoundHandle(SafeHandle handle, SafeThreadPoolIOHandle threadPoolHandle)
		{
			this._threadPoolHandle = threadPoolHandle;
			this._handle = handle;
		}

		/// <summary>Gets the bound operating system handle.</summary>
		/// <returns>An object that holds the bound operating system handle.</returns>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x00070538 File Offset: 0x0006E738
		public SafeHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		/// <summary>Returns a <see cref="T:System.Threading.ThreadPoolBoundHandle" /> for the specified handle, which is bound to the system thread pool.</summary>
		/// <param name="handle">An object that holds the operating system handle. The handle must have been opened for overlapped I/O in unmanaged code.</param>
		/// <returns>A <see cref="T:System.Threading.ThreadPoolBoundHandle" /> for <paramref name="handle" />, which is bound to the system thread pool.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="handle" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> has been disposed.  
		/// -or-  
		/// <paramref name="handle" /> does not refer to a valid I/O handle.  
		/// -or-  
		/// <paramref name="handle" /> refers to a handle that has not been opened for overlapped I/O.  
		/// -or-  
		/// <paramref name="handle" /> refers to a handle that has already been bound.</exception>
		// Token: 0x06001E56 RID: 7766 RVA: 0x00070540 File Offset: 0x0006E740
		public static ThreadPoolBoundHandle BindHandle(SafeHandle handle)
		{
			if (handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			if (handle.IsClosed || handle.IsInvalid)
			{
				throw new ArgumentException("'handle' has been disposed or is an invalid handle.", "handle");
			}
			IntPtr pfnio = AddrofIntrinsics.AddrOf<Interop.NativeIoCompletionCallback>(new Interop.NativeIoCompletionCallback(ThreadPoolBoundHandle.OnNativeIOCompleted));
			SafeThreadPoolIOHandle safeThreadPoolIOHandle = Interop.mincore.CreateThreadpoolIo(handle, pfnio, IntPtr.Zero, IntPtr.Zero);
			if (!safeThreadPoolIOHandle.IsInvalid)
			{
				return new ThreadPoolBoundHandle(handle, safeThreadPoolIOHandle);
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 6)
			{
				throw new ArgumentException("'handle' has been disposed or is an invalid handle.", "handle");
			}
			if (lastWin32Error == 87)
			{
				throw new ArgumentException("'handle' has already been bound to the thread pool, or was not opened for asynchronous I/O.", "handle");
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
		}

		/// <summary>Returns an unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying a delegate that is invoked when the asynchronous I/O operation is complete, a user-provided object that supplies context, and managed objects that serve as buffers.</summary>
		/// <param name="callback">A delegate that represents the callback method to invoke when the asynchronous I/O operation completes.</param>
		/// <param name="state">A user-provided object that distinguishes this <see cref="T:System.Threading.NativeOverlapped" /> instance from other <see cref="T:System.Threading.NativeOverlapped" /> instances.</param>
		/// <param name="pinData">An object or array of objects that represent the input or output buffer for the operation, or <see langword="null" />. Each object represents a buffer, such an array of bytes.</param>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This method was called after the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> object was disposed.</exception>
		// Token: 0x06001E57 RID: 7767 RVA: 0x000705E8 File Offset: 0x0006E7E8
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.AddRef();
			NativeOverlapped* result;
			try
			{
				Win32ThreadPoolNativeOverlapped* ptr = Win32ThreadPoolNativeOverlapped.Allocate(callback, state, pinData, null);
				ptr->Data._boundHandle = this;
				Interop.mincore.StartThreadpoolIo(this._threadPoolHandle);
				result = Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(ptr);
			}
			catch
			{
				this.Release();
				throw;
			}
			return result;
		}

		/// <summary>Returns an unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure using the callback state and buffers associated with the specified <see cref="T:System.Threading.PreAllocatedOverlapped" /> object.</summary>
		/// <param name="preAllocated">An object from which to create the <see cref="T:System.Threading.NativeOverlapped" /> pointer.</param>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="preAllocated" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="preAllocated" /> is currently in use for another I/O operation.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This method was called after the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> was disposed.  
		///  -or-  
		///  This method was called after <paramref name="preAllocated" /> was disposed.</exception>
		// Token: 0x06001E58 RID: 7768 RVA: 0x0007064C File Offset: 0x0006E84C
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped(PreAllocatedOverlapped preAllocated)
		{
			if (preAllocated == null)
			{
				throw new ArgumentNullException("preAllocated");
			}
			bool flag = false;
			bool flag2 = false;
			NativeOverlapped* result;
			try
			{
				flag = this.AddRef();
				flag2 = preAllocated.AddRef();
				Win32ThreadPoolNativeOverlapped.OverlappedData data = preAllocated._overlapped->Data;
				if (data._boundHandle != null)
				{
					throw new ArgumentException("'preAllocated' is already in use.", "preAllocated");
				}
				data._boundHandle = this;
				Interop.mincore.StartThreadpoolIo(this._threadPoolHandle);
				result = Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(preAllocated._overlapped);
			}
			catch
			{
				if (flag2)
				{
					preAllocated.Release();
				}
				if (flag)
				{
					this.Release();
				}
				throw;
			}
			return result;
		}

		/// <summary>Frees the memory associated with a <see cref="T:System.Threading.NativeOverlapped" /> structure allocated by the <see cref="Overload:System.Threading.ThreadPoolBoundHandle.AllocateNativeOverlapped" /> method.</summary>
		/// <param name="overlapped">An unmanaged pointer to the <see cref="T:System.Threading.NativeOverlapped" /> structure structure to be freed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="overlapped" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This method was called after the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> object was disposed.</exception>
		// Token: 0x06001E59 RID: 7769 RVA: 0x000706E4 File Offset: 0x0006E8E4
		[CLSCompliant(false)]
		public unsafe void FreeNativeOverlapped(NativeOverlapped* overlapped)
		{
			if (overlapped == null)
			{
				throw new ArgumentNullException("overlapped");
			}
			Win32ThreadPoolNativeOverlapped* overlapped2 = Win32ThreadPoolNativeOverlapped.FromNativeOverlapped(overlapped);
			Win32ThreadPoolNativeOverlapped.OverlappedData overlappedData = ThreadPoolBoundHandle.GetOverlappedData(overlapped2, this);
			if (!overlappedData._completed)
			{
				Interop.mincore.CancelThreadpoolIo(this._threadPoolHandle);
				this.Release();
			}
			overlappedData._boundHandle = null;
			overlappedData._completed = false;
			if (overlappedData._preAllocated != null)
			{
				overlappedData._preAllocated.Release();
				return;
			}
			Win32ThreadPoolNativeOverlapped.Free(overlapped2);
		}

		/// <summary>Returns the user-provided object that was specified when the <see cref="T:System.Threading.NativeOverlapped" /> instance was allocated by calling the <see cref="M:System.Threading.ThreadPoolBoundHandle.AllocateNativeOverlapped(System.Threading.IOCompletionCallback,System.Object,System.Object)" /> method.</summary>
		/// <param name="overlapped">An unmanaged pointer to the <see cref="T:System.Threading.NativeOverlapped" /> structure from which to return the associated user-provided object.</param>
		/// <returns>A user-provided object that distinguishes this <see cref="T:System.Threading.NativeOverlapped" /> instance from other <see cref="T:System.Threading.NativeOverlapped" /> instances, or <see langword="null" /> if one was not specified when the intstance was allocated by calling the <see cref="Overload:System.Threading.ThreadPoolBoundHandle.AllocateNativeOverlapped" /> method.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="overlapped" /> is <see langword="null" />.</exception>
		// Token: 0x06001E5A RID: 7770 RVA: 0x00070751 File Offset: 0x0006E951
		[CLSCompliant(false)]
		public unsafe static object GetNativeOverlappedState(NativeOverlapped* overlapped)
		{
			if (overlapped == null)
			{
				throw new ArgumentNullException("overlapped");
			}
			return ThreadPoolBoundHandle.GetOverlappedData(Win32ThreadPoolNativeOverlapped.FromNativeOverlapped(overlapped), null)._state;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00070774 File Offset: 0x0006E974
		private unsafe static Win32ThreadPoolNativeOverlapped.OverlappedData GetOverlappedData(Win32ThreadPoolNativeOverlapped* overlapped, ThreadPoolBoundHandle expectedBoundHandle)
		{
			Win32ThreadPoolNativeOverlapped.OverlappedData data = overlapped->Data;
			if (data._boundHandle == null)
			{
				throw new ArgumentException("'overlapped' has already been freed.", "overlapped");
			}
			if (expectedBoundHandle != null && data._boundHandle != expectedBoundHandle)
			{
				throw new ArgumentException("'overlapped' was not allocated by this ThreadPoolBoundHandle instance.", "overlapped");
			}
			return data;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x000707C0 File Offset: 0x0006E9C0
		[NativeCallable(CallingConvention = CallingConvention.StdCall)]
		private unsafe static void OnNativeIOCompleted(IntPtr instance, IntPtr context, IntPtr overlappedPtr, uint ioResult, UIntPtr numberOfBytesTransferred, IntPtr ioPtr)
		{
			ThreadPoolCallbackWrapper threadPoolCallbackWrapper = ThreadPoolCallbackWrapper.Enter();
			Win32ThreadPoolNativeOverlapped* ptr = (Win32ThreadPoolNativeOverlapped*)((void*)overlappedPtr);
			ThreadPoolBoundHandle boundHandle = ptr->Data._boundHandle;
			if (boundHandle == null)
			{
				throw new InvalidOperationException("'overlapped' has already been freed.");
			}
			boundHandle.Release();
			Win32ThreadPoolNativeOverlapped.CompleteWithCallback(ioResult, (uint)numberOfBytesTransferred, ptr);
			threadPoolCallbackWrapper.Exit(true);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0007080E File Offset: 0x0006EA0E
		private bool AddRef()
		{
			return this._lifetime.AddRef(this);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0007081C File Offset: 0x0006EA1C
		private void Release()
		{
			this._lifetime.Release(this);
		}

		/// <summary>Releases all unmanaged resources used by the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> instance.</summary>
		// Token: 0x06001E5F RID: 7775 RVA: 0x0007082A File Offset: 0x0006EA2A
		public void Dispose()
		{
			this._lifetime.Dispose(this);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x00070840 File Offset: 0x0006EA40
		~ThreadPoolBoundHandle()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose();
			}
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00070880 File Offset: 0x0006EA80
		void IDeferredDisposable.OnFinalRelease(bool disposed)
		{
			if (disposed)
			{
				this._threadPoolHandle.Dispose();
			}
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x000173AD File Offset: 0x000155AD
		internal ThreadPoolBoundHandle()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001AA0 RID: 6816
		private readonly SafeHandle _handle;

		// Token: 0x04001AA1 RID: 6817
		private readonly SafeThreadPoolIOHandle _threadPoolHandle;

		// Token: 0x04001AA2 RID: 6818
		private DeferredDisposableLifetime<ThreadPoolBoundHandle> _lifetime;
	}
}
