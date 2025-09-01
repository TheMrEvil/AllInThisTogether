using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	/// <summary>Encapsulates operating system-specific objects that wait for exclusive access to shared resources.</summary>
	// Token: 0x020002EA RID: 746
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class WaitHandle : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandle" /> class.</summary>
		// Token: 0x06002073 RID: 8307 RVA: 0x00075FEB File Offset: 0x000741EB
		protected WaitHandle()
		{
			this.Init();
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00075FF9 File Offset: 0x000741F9
		[SecuritySafeCritical]
		private void Init()
		{
			this.safeWaitHandle = null;
			this.waitHandle = WaitHandle.InvalidHandle;
			this.hasThreadAffinity = false;
		}

		/// <summary>Gets or sets the native operating system handle.</summary>
		/// <returns>An <see langword="IntPtr" /> representing the native operating system handle. The default is the value of the <see cref="F:System.Threading.WaitHandle.InvalidHandle" /> field.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x00076016 File Offset: 0x00074216
		// (set) Token: 0x06002076 RID: 8310 RVA: 0x00076038 File Offset: 0x00074238
		[Obsolete("Use the SafeWaitHandle property instead.")]
		public virtual IntPtr Handle
		{
			[SecuritySafeCritical]
			get
			{
				if (this.safeWaitHandle != null)
				{
					return this.safeWaitHandle.DangerousGetHandle();
				}
				return WaitHandle.InvalidHandle;
			}
			[SecurityCritical]
			set
			{
				if (value == WaitHandle.InvalidHandle)
				{
					if (this.safeWaitHandle != null)
					{
						this.safeWaitHandle.SetHandleAsInvalid();
						this.safeWaitHandle = null;
					}
				}
				else
				{
					this.safeWaitHandle = new SafeWaitHandle(value, true);
				}
				this.waitHandle = value;
			}
		}

		/// <summary>Gets or sets the native operating system handle.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SafeHandles.SafeWaitHandle" /> representing the native operating system handle.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x0007608A File Offset: 0x0007428A
		// (set) Token: 0x06002078 RID: 8312 RVA: 0x000760B4 File Offset: 0x000742B4
		public SafeWaitHandle SafeWaitHandle
		{
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				if (this.safeWaitHandle == null)
				{
					this.safeWaitHandle = new SafeWaitHandle(WaitHandle.InvalidHandle, false);
				}
				return this.safeWaitHandle;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SecurityCritical]
			set
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (value == null)
					{
						this.safeWaitHandle = null;
						this.waitHandle = WaitHandle.InvalidHandle;
					}
					else
					{
						this.safeWaitHandle = value;
						this.waitHandle = this.safeWaitHandle.DangerousGetHandle();
					}
				}
			}
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x00076110 File Offset: 0x00074310
		[SecurityCritical]
		internal void SetHandleInternal(SafeWaitHandle handle)
		{
			this.safeWaitHandle = handle;
			this.waitHandle = handle.DangerousGetHandle();
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.WaitHandle" /> receives a signal, using a 32-bit signed integer to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance receives a signal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600207A RID: 8314 RVA: 0x00076127 File Offset: 0x00074327
		public virtual bool WaitOne(int millisecondsTimeout, bool exitContext)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.WaitOne((long)millisecondsTimeout, exitContext);
		}

		/// <summary>Blocks the current thread until the current instance receives a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance receives a signal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600207B RID: 8315 RVA: 0x0007614C File Offset: 0x0007434C
		public virtual bool WaitOne(TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.WaitOne(num, exitContext);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.WaitHandle" /> receives a signal.</summary>
		/// <returns>
		///   <see langword="true" /> if the current instance receives a signal. If the current instance is never signaled, <see cref="M:System.Threading.WaitHandle.WaitOne(System.Int32,System.Boolean)" /> never returns.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600207C RID: 8316 RVA: 0x0007618D File Offset: 0x0007438D
		public virtual bool WaitOne()
		{
			return this.WaitOne(-1, false);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.WaitHandle" /> receives a signal, using a 32-bit signed integer to specify the time interval in milliseconds.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance receives a signal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600207D RID: 8317 RVA: 0x00076197 File Offset: 0x00074397
		public virtual bool WaitOne(int millisecondsTimeout)
		{
			return this.WaitOne(millisecondsTimeout, false);
		}

		/// <summary>Blocks the current thread until the current instance receives a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if the current instance receives a signal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600207E RID: 8318 RVA: 0x000761A1 File Offset: 0x000743A1
		public virtual bool WaitOne(TimeSpan timeout)
		{
			return this.WaitOne(timeout, false);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000761AB File Offset: 0x000743AB
		[SecuritySafeCritical]
		private bool WaitOne(long timeout, bool exitContext)
		{
			return WaitHandle.InternalWaitOne(this.safeWaitHandle, timeout, this.hasThreadAffinity, exitContext);
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000761C4 File Offset: 0x000743C4
		[SecurityCritical]
		internal static bool InternalWaitOne(SafeHandle waitableSafeHandle, long millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			if (waitableSafeHandle == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			int num = WaitHandle.WaitOneNative(waitableSafeHandle, (uint)millisecondsTimeout, hasThreadAffinity, exitContext);
			if (num == 128)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			return num != 258 && num != int.MaxValue;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00076214 File Offset: 0x00074414
		[SecurityCritical]
		internal bool WaitOneWithoutFAS()
		{
			if (this.safeWaitHandle == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			long num = -1L;
			int num2 = WaitHandle.WaitOneNative(this.safeWaitHandle, (uint)num, this.hasThreadAffinity, false);
			if (num2 == 128)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			return num2 != 258 && num2 != int.MaxValue;
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using an <see cref="T:System.Int32" /> value to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object (duplicates).</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> when every element in <paramref name="waitHandles" /> has received a signal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />.  
		///  -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array is <see langword="null" />.  
		///  -or-  
		///  <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later.</exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">The <paramref name="waitHandles" /> array contains elements that are duplicates.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.  
		///  -or-  
		///  The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element.</exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06002082 RID: 8322 RVA: 0x00076278 File Offset: 0x00074478
		[SecuritySafeCritical]
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("The waitHandles parameter cannot be null."));
			}
			if (waitHandles.Length == 0)
			{
				throw new ArgumentNullException(Environment.GetResourceString("Waithandle array may not be empty."));
			}
			if (waitHandles.Length > 64)
			{
				throw new NotSupportedException(Environment.GetResourceString("The number of WaitHandles must be less than or equal to 64."));
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			WaitHandle[] array = new WaitHandle[waitHandles.Length];
			for (int i = 0; i < waitHandles.Length; i++)
			{
				WaitHandle waitHandle = waitHandles[i];
				if (waitHandle == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("At least one element in the specified array was null."));
				}
				if (RemotingServices.IsTransparentProxy(waitHandle))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Cannot wait on a transparent proxy."));
				}
				array[i] = waitHandle;
			}
			int num = WaitHandle.WaitMultiple(array, millisecondsTimeout, exitContext, true);
			if (128 <= num && 128 + array.Length > num)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			GC.KeepAlive(array);
			return num != 258 && num != int.MaxValue;
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> value to specify the time interval, and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds, to wait indefinitely.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> when every element in <paramref name="waitHandles" /> has received a signal; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />.  
		///  -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array is <see langword="null" />.  
		///  -or-  
		///  <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later.</exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">The <paramref name="waitHandles" /> array contains elements that are duplicates.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.  
		///  -or-  
		///  The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element.</exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06002083 RID: 8323 RVA: 0x00076364 File Offset: 0x00074564
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return WaitHandle.WaitAll(waitHandles, (int)num, exitContext);
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object.</param>
		/// <returns>
		///   <see langword="true" /> when every element in <paramref name="waitHandles" /> has received a signal; otherwise the method never returns.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />. -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array are <see langword="null" />.  
		///  -or-  
		///  <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later.</exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.  
		///
		///
		///
		///
		///  The <paramref name="waitHandles" /> array contains elements that are duplicates.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.  
		///  -or-  
		///  The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element.</exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06002084 RID: 8324 RVA: 0x000763A6 File Offset: 0x000745A6
		public static bool WaitAll(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitAll(waitHandles, -1, true);
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using an <see cref="T:System.Int32" /> value to specify the time interval.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object (duplicates).</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> when every element in <paramref name="waitHandles" /> has received a signal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />.  
		///  -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array is <see langword="null" />.  
		///  -or-  
		///  <paramref name="waitHandles" /> is an array with no elements.</exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.  
		///
		///
		///
		///
		///  The <paramref name="waitHandles" /> array contains elements that are duplicates.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.  
		///  -or-  
		///  The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06002085 RID: 8325 RVA: 0x000763B0 File Offset: 0x000745B0
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitAll(waitHandles, millisecondsTimeout, true);
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> value to specify the time interval.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds, to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> when every element in <paramref name="waitHandles" /> has received a signal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />.  
		///  -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array is <see langword="null" />.  
		///  -or-  
		///  <paramref name="waitHandles" /> is an array with no elements.</exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.  
		///
		///
		///
		///
		///  The <paramref name="waitHandles" /> array contains elements that are duplicates.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.  
		///  -or-  
		///  The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06002086 RID: 8326 RVA: 0x000763BA File Offset: 0x000745BA
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitAll(waitHandles, timeout, true);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a 32-bit signed integer to specify the time interval, and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, <see langword="false" />.</param>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="millisecondsTimeout" /> has passed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />.  
		///  -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.</exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06002087 RID: 8327 RVA: 0x000763C4 File Offset: 0x000745C4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecuritySafeCritical]
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("The waitHandles parameter cannot be null."));
			}
			if (waitHandles.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Waithandle array may not be empty."));
			}
			if (64 < waitHandles.Length)
			{
				throw new NotSupportedException(Environment.GetResourceString("The number of WaitHandles must be less than or equal to 64."));
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			WaitHandle[] array = new WaitHandle[waitHandles.Length];
			for (int i = 0; i < waitHandles.Length; i++)
			{
				WaitHandle waitHandle = waitHandles[i];
				if (waitHandle == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("At least one element in the specified array was null."));
				}
				if (RemotingServices.IsTransparentProxy(waitHandle))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Cannot wait on a transparent proxy."));
				}
				array[i] = waitHandle;
			}
			int num = WaitHandle.WaitMultiple(array, millisecondsTimeout, exitContext, false);
			if (128 <= num && 128 + array.Length > num)
			{
				int num2 = num - 128;
				if (0 <= num2 && num2 < array.Length)
				{
					WaitHandle.ThrowAbandonedMutexException(num2, array[num2]);
				}
				else
				{
					WaitHandle.ThrowAbandonedMutexException();
				}
			}
			GC.KeepAlive(array);
			return num;
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, <see langword="false" />.</param>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="timeout" /> has passed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />.  
		///  -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.</exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06002088 RID: 8328 RVA: 0x000764C0 File Offset: 0x000746C0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return WaitHandle.WaitAny(waitHandles, (int)num, exitContext);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="timeout" /> has passed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />.  
		///  -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06002089 RID: 8329 RVA: 0x00076502 File Offset: 0x00074702
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitAny(waitHandles, timeout, true);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait.</param>
		/// <returns>The array index of the object that satisfied the wait.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />.  
		///  -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.</exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600208A RID: 8330 RVA: 0x0007650C File Offset: 0x0007470C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitAny(waitHandles, -1, true);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a 32-bit signed integer to specify the time interval.</summary>
		/// <param name="waitHandles">A <see langword="WaitHandle" /> array containing the objects for which the current instance will wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="millisecondsTimeout" /> has passed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is <see langword="null" />.  
		///  -or-  
		///  One or more of the objects in the <paramref name="waitHandles" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600208B RID: 8331 RVA: 0x00076516 File Offset: 0x00074716
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitAny(waitHandles, millisecondsTimeout, true);
		}

		/// <summary>Signals one <see cref="T:System.Threading.WaitHandle" /> and waits on another.</summary>
		/// <param name="toSignal">The <see cref="T:System.Threading.WaitHandle" /> to signal.</param>
		/// <param name="toWaitOn">The <see cref="T:System.Threading.WaitHandle" /> to wait on.</param>
		/// <returns>
		///   <see langword="true" /> if both the signal and the wait complete successfully; if the wait does not complete, the method does not return.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toSignal" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="toWaitOn" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The method was called on a thread that has <see cref="T:System.STAThreadAttribute" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="toSignal" /> is a semaphore, and it already has a full count.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x0600208C RID: 8332 RVA: 0x00076520 File Offset: 0x00074720
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn)
		{
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, -1, false);
		}

		/// <summary>Signals one <see cref="T:System.Threading.WaitHandle" /> and waits on another, specifying the time-out interval as a <see cref="T:System.TimeSpan" /> and specifying whether to exit the synchronization domain for the context before entering the wait.</summary>
		/// <param name="toSignal">The <see cref="T:System.Threading.WaitHandle" /> to signal.</param>
		/// <param name="toWaitOn">The <see cref="T:System.Threading.WaitHandle" /> to wait on.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the interval to wait. If the value is -1, the wait is infinite.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if both the signal and the wait completed successfully, or <see langword="false" /> if the signal completed but the wait timed out.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toSignal" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="toWaitOn" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The method was called on a thread that has <see cref="T:System.STAThreadAttribute" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="toSignal" /> is a semaphore, and it already has a full count.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> evaluates to a negative number of milliseconds other than -1.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x0600208D RID: 8333 RVA: 0x0007652C File Offset: 0x0007472C
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, (int)num, exitContext);
		}

		/// <summary>Signals one <see cref="T:System.Threading.WaitHandle" /> and waits on another, specifying a time-out interval as a 32-bit signed integer and specifying whether to exit the synchronization domain for the context before entering the wait.</summary>
		/// <param name="toSignal">The <see cref="T:System.Threading.WaitHandle" /> to signal.</param>
		/// <param name="toWaitOn">The <see cref="T:System.Threading.WaitHandle" /> to wait on.</param>
		/// <param name="millisecondsTimeout">An integer that represents the interval to wait. If the value is <see cref="F:System.Threading.Timeout.Infinite" />, that is, -1, the wait is infinite.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if both the signal and the wait completed successfully, or <see langword="false" /> if the signal completed but the wait timed out.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toSignal" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="toWaitOn" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The method is called on a thread that has <see cref="T:System.STAThreadAttribute" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.WaitHandle" /> cannot be signaled because it would exceed its maximum count.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x0600208E RID: 8334 RVA: 0x00076570 File Offset: 0x00074770
		[SecuritySafeCritical]
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, int millisecondsTimeout, bool exitContext)
		{
			if (toSignal == null)
			{
				throw new ArgumentNullException("toSignal");
			}
			if (toWaitOn == null)
			{
				throw new ArgumentNullException("toWaitOn");
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			int num = WaitHandle.SignalAndWaitOne(toSignal.safeWaitHandle, toWaitOn.safeWaitHandle, millisecondsTimeout, toWaitOn.hasThreadAffinity, exitContext);
			if (2147483647 != num && toSignal.hasThreadAffinity)
			{
				Thread.EndCriticalRegion();
				Thread.EndThreadAffinity();
			}
			if (128 == num)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			if (298 == num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The WaitHandle cannot be signaled because it would exceed its maximum count."));
			}
			if (299 == num)
			{
				throw new ApplicationException("Attempt to release mutex not owned by caller");
			}
			return num == 0;
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00076628 File Offset: 0x00074828
		private static void ThrowAbandonedMutexException()
		{
			throw new AbandonedMutexException();
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0007662F File Offset: 0x0007482F
		private static void ThrowAbandonedMutexException(int location, WaitHandle handle)
		{
			throw new AbandonedMutexException(location, handle);
		}

		/// <summary>Releases all resources held by the current <see cref="T:System.Threading.WaitHandle" />.</summary>
		// Token: 0x06002091 RID: 8337 RVA: 0x00076638 File Offset: 0x00074838
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Threading.WaitHandle" />, and optionally releases the managed resources.</summary>
		/// <param name="explicitDisposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002092 RID: 8338 RVA: 0x00076647 File Offset: 0x00074847
		[SecuritySafeCritical]
		protected virtual void Dispose(bool explicitDisposing)
		{
			if (this.safeWaitHandle != null)
			{
				this.safeWaitHandle.Close();
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.WaitHandle" /> class.</summary>
		// Token: 0x06002093 RID: 8339 RVA: 0x00076638 File Offset: 0x00074838
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00076660 File Offset: 0x00074860
		private unsafe static int WaitOneNative(SafeHandle waitableSafeHandle, uint millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			bool flag = false;
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			int result;
			try
			{
				waitableSafeHandle.DangerousAddRef(ref flag);
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				if (synchronizationContext != null && synchronizationContext.IsWaitNotificationRequired())
				{
					result = synchronizationContext.Wait(new IntPtr[]
					{
						waitableSafeHandle.DangerousGetHandle()
					}, false, (int)millisecondsTimeout);
				}
				else
				{
					IntPtr intPtr = waitableSafeHandle.DangerousGetHandle();
					result = WaitHandle.Wait_internal(&intPtr, 1, false, (int)millisecondsTimeout);
				}
			}
			finally
			{
				if (flag)
				{
					waitableSafeHandle.DangerousRelease();
				}
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
			}
			return result;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000766E4 File Offset: 0x000748E4
		private unsafe static int WaitMultiple(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext, bool WaitAll)
		{
			if (waitHandles.Length > 64)
			{
				return int.MaxValue;
			}
			int num = -1;
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			int result;
			try
			{
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				for (int i = 0; i < waitHandles.Length; i++)
				{
					try
					{
					}
					finally
					{
						bool flag = false;
						waitHandles[i].SafeWaitHandle.DangerousAddRef(ref flag);
						num = i;
					}
				}
				if (synchronizationContext != null && synchronizationContext.IsWaitNotificationRequired())
				{
					IntPtr[] array = new IntPtr[waitHandles.Length];
					for (int j = 0; j < waitHandles.Length; j++)
					{
						array[j] = waitHandles[j].SafeWaitHandle.DangerousGetHandle();
					}
					result = synchronizationContext.Wait(array, false, millisecondsTimeout);
				}
				else
				{
					IntPtr* ptr = stackalloc IntPtr[checked(unchecked((UIntPtr)waitHandles.Length) * (UIntPtr)sizeof(IntPtr))];
					for (int k = 0; k < waitHandles.Length; k++)
					{
						ptr[k] = waitHandles[k].SafeWaitHandle.DangerousGetHandle();
					}
					result = WaitHandle.Wait_internal(ptr, waitHandles.Length, WaitAll, millisecondsTimeout);
				}
			}
			finally
			{
				for (int l = num; l >= 0; l--)
				{
					waitHandles[l].SafeWaitHandle.DangerousRelease();
				}
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
			}
			return result;
		}

		// Token: 0x06002096 RID: 8342
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern int Wait_internal(IntPtr* handles, int numHandles, bool waitAll, int ms);

		// Token: 0x06002097 RID: 8343 RVA: 0x00076810 File Offset: 0x00074A10
		private static int SignalAndWaitOne(SafeWaitHandle waitHandleToSignal, SafeWaitHandle waitHandleToWaitOn, int millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			bool flag = false;
			bool flag2 = false;
			int result;
			try
			{
				waitHandleToSignal.DangerousAddRef(ref flag);
				waitHandleToWaitOn.DangerousAddRef(ref flag2);
				result = WaitHandle.SignalAndWait_Internal(waitHandleToSignal.DangerousGetHandle(), waitHandleToWaitOn.DangerousGetHandle(), millisecondsTimeout);
			}
			finally
			{
				if (flag)
				{
					waitHandleToSignal.DangerousRelease();
				}
				if (flag2)
				{
					waitHandleToWaitOn.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002098 RID: 8344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SignalAndWait_Internal(IntPtr toSignal, IntPtr toWaitOn, int ms);

		// Token: 0x06002099 RID: 8345 RVA: 0x0007686C File Offset: 0x00074A6C
		internal static int ToTimeoutMilliseconds(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", "Number must be either non-negative and less than or equal to Int32.MaxValue or -1.");
			}
			return (int)num;
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000768A2 File Offset: 0x00074AA2
		// Note: this type is marked as 'beforefieldinit'.
		static WaitHandle()
		{
		}

		/// <summary>Indicates that a <see cref="M:System.Threading.WaitHandle.WaitAny(System.Threading.WaitHandle[],System.Int32,System.Boolean)" /> operation timed out before any of the wait handles were signaled. This field is constant.</summary>
		// Token: 0x04001B5A RID: 7002
		public const int WaitTimeout = 258;

		// Token: 0x04001B5B RID: 7003
		private const int MAX_WAITHANDLES = 64;

		// Token: 0x04001B5C RID: 7004
		private IntPtr waitHandle;

		// Token: 0x04001B5D RID: 7005
		[SecurityCritical]
		internal volatile SafeWaitHandle safeWaitHandle;

		// Token: 0x04001B5E RID: 7006
		internal bool hasThreadAffinity;

		// Token: 0x04001B5F RID: 7007
		private const int WAIT_OBJECT_0 = 0;

		// Token: 0x04001B60 RID: 7008
		private const int WAIT_ABANDONED = 128;

		// Token: 0x04001B61 RID: 7009
		private const int WAIT_FAILED = 2147483647;

		// Token: 0x04001B62 RID: 7010
		private const int ERROR_TOO_MANY_POSTS = 298;

		// Token: 0x04001B63 RID: 7011
		private const int ERROR_NOT_OWNED_BY_CALLER = 299;

		/// <summary>Represents an invalid native operating system handle. This field is read-only.</summary>
		// Token: 0x04001B64 RID: 7012
		protected static readonly IntPtr InvalidHandle = (IntPtr)(-1);

		// Token: 0x04001B65 RID: 7013
		internal const int MaxWaitHandles = 64;

		// Token: 0x020002EB RID: 747
		internal enum OpenExistingResult
		{
			// Token: 0x04001B67 RID: 7015
			Success,
			// Token: 0x04001B68 RID: 7016
			NameNotFound,
			// Token: 0x04001B69 RID: 7017
			PathNotFound,
			// Token: 0x04001B6A RID: 7018
			NameInvalid
		}
	}
}
