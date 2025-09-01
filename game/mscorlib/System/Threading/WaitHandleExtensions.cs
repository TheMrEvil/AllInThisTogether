using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	/// <summary>Provides convenience methods to for working with a safe handle for a wait handle.</summary>
	// Token: 0x020002EC RID: 748
	public static class WaitHandleExtensions
	{
		/// <summary>Gets the safe handle for a native operating system wait handle.</summary>
		/// <param name="waitHandle">A native operating system handle.</param>
		/// <returns>The safe wait handle that wraps the native operating system wait handle.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="waitHandle" /> is <see langword="null" />.</exception>
		// Token: 0x0600209B RID: 8347 RVA: 0x000768AF File Offset: 0x00074AAF
		[SecurityCritical]
		public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			return waitHandle.SafeWaitHandle;
		}

		/// <summary>Sets a safe handle for a native operating system wait handle.</summary>
		/// <param name="waitHandle">A wait handle that encapsulates an operating system-specific object that waits for exclusive access to a shared resource.</param>
		/// <param name="value">The safe handle to wrap the operating system handle.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="waitHandle" /> is <see langword="null" />.</exception>
		// Token: 0x0600209C RID: 8348 RVA: 0x000768C5 File Offset: 0x00074AC5
		[SecurityCritical]
		public static void SetSafeWaitHandle(this WaitHandle waitHandle, SafeWaitHandle value)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			waitHandle.SafeWaitHandle = value;
		}
	}
}
