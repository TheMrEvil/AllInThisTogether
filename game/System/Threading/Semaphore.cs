using System;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	/// <summary>Limits the number of threads that can access a resource or pool of resources concurrently.</summary>
	// Token: 0x0200017E RID: 382
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class Semaphore : WaitHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Semaphore" /> class, specifying the initial number of entries and the maximum number of concurrent entries.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
		/// <param name="maximumCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCount" /> is greater than <paramref name="maximumCount" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumCount" /> is less than 1.  
		/// -or-  
		/// <paramref name="initialCount" /> is less than 0.</exception>
		// Token: 0x06000A3F RID: 2623 RVA: 0x0002CD12 File Offset: 0x0002AF12
		[SecuritySafeCritical]
		public Semaphore(int initialCount, int maximumCount) : this(initialCount, maximumCount, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Semaphore" /> class, specifying the initial number of entries and the maximum number of concurrent entries, and optionally specifying the name of a system semaphore object.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
		/// <param name="maximumCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
		/// <param name="name">The name of a named system semaphore object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCount" /> is greater than <paramref name="maximumCount" />.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumCount" /> is less than 1.  
		/// -or-  
		/// <paramref name="initialCount" /> is less than 0.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists and has access control security, and the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		// Token: 0x06000A40 RID: 2624 RVA: 0x0002CD20 File Offset: 0x0002AF20
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Semaphore(int initialCount, int maximumCount, string name)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount", SR.GetString("Non-negative number required."));
			}
			if (maximumCount < 1)
			{
				throw new ArgumentOutOfRangeException("maximumCount", SR.GetString("Positive number required."));
			}
			if (initialCount > maximumCount)
			{
				throw new ArgumentException(SR.GetString("The initial count for the semaphore must be greater than or equal to zero and less than the maximum count."));
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(SR.GetString("The name can be no more than 260 characters in length."));
			}
			int num;
			SafeWaitHandle safeWaitHandle = new SafeWaitHandle(Semaphore.CreateSemaphore_internal(initialCount, maximumCount, name, out num), true);
			if (safeWaitHandle.IsInvalid)
			{
				if (name != null && name.Length != 0 && 6 == num)
				{
					throw new WaitHandleCannotBeOpenedException(SR.GetString("A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.", new object[]
					{
						name
					}));
				}
				InternalResources.WinIOError(num, "");
			}
			base.SafeWaitHandle = safeWaitHandle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Semaphore" /> class, specifying the initial number of entries and the maximum number of concurrent entries, optionally specifying the name of a system semaphore object, and specifying a variable that receives a value indicating whether a new system semaphore was created.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be satisfied concurrently.</param>
		/// <param name="maximumCount">The maximum number of requests for the semaphore that can be satisfied concurrently.</param>
		/// <param name="name">The name of a named system semaphore object.</param>
		/// <param name="createdNew">When this method returns, contains <see langword="true" /> if a local semaphore was created (that is, if <paramref name="name" /> is <see langword="null" /> or an empty string) or if the specified named system semaphore was created; <see langword="false" /> if the specified named system semaphore already existed. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCount" /> is greater than <paramref name="maximumCount" />.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumCount" /> is less than 1.  
		/// -or-  
		/// <paramref name="initialCount" /> is less than 0.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists and has access control security, and the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		// Token: 0x06000A41 RID: 2625 RVA: 0x0002CDED File Offset: 0x0002AFED
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Semaphore(int initialCount, int maximumCount, string name, out bool createdNew) : this(initialCount, maximumCount, name, out createdNew, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Semaphore" /> class, specifying the initial number of entries and the maximum number of concurrent entries, optionally specifying the name of a system semaphore object, specifying a variable that receives a value indicating whether a new system semaphore was created, and specifying security access control for the system semaphore.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be satisfied concurrently.</param>
		/// <param name="maximumCount">The maximum number of requests for the semaphore that can be satisfied concurrently.</param>
		/// <param name="name">The name of a named system semaphore object.</param>
		/// <param name="createdNew">When this method returns, contains <see langword="true" /> if a local semaphore was created (that is, if <paramref name="name" /> is <see langword="null" /> or an empty string) or if the specified named system semaphore was created; <see langword="false" /> if the specified named system semaphore already existed. This parameter is passed uninitialized.</param>
		/// <param name="semaphoreSecurity">A <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> object that represents the access control security to be applied to the named system semaphore.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="initialCount" /> is greater than <paramref name="maximumCount" />.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maximumCount" /> is less than 1.  
		/// -or-  
		/// <paramref name="initialCount" /> is less than 0.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists and has access control security, and the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.FullControl" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		// Token: 0x06000A42 RID: 2626 RVA: 0x0002CDFC File Offset: 0x0002AFFC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Semaphore(int initialCount, int maximumCount, string name, out bool createdNew, SemaphoreSecurity semaphoreSecurity)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount", SR.GetString("Non-negative number required."));
			}
			if (maximumCount < 1)
			{
				throw new ArgumentOutOfRangeException("maximumCount", SR.GetString("Non-negative number required."));
			}
			if (initialCount > maximumCount)
			{
				throw new ArgumentException(SR.GetString("The initial count for the semaphore must be greater than or equal to zero and less than the maximum count."));
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(SR.GetString("The name can be no more than 260 characters in length."));
			}
			int num;
			SafeWaitHandle safeWaitHandle = new SafeWaitHandle(Semaphore.CreateSemaphore_internal(initialCount, maximumCount, name, out num), true);
			if (safeWaitHandle.IsInvalid)
			{
				if (name != null && name.Length != 0 && 6 == num)
				{
					throw new WaitHandleCannotBeOpenedException(SR.GetString("A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.", new object[]
					{
						name
					}));
				}
				InternalResources.WinIOError(num, "");
			}
			createdNew = (num != 183);
			base.SafeWaitHandle = safeWaitHandle;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002CED7 File Offset: 0x0002B0D7
		private Semaphore(SafeWaitHandle handle)
		{
			base.SafeWaitHandle = handle;
		}

		/// <summary>Opens the specified named semaphore, if it already exists.</summary>
		/// <param name="name">The name of the system semaphore to open.</param>
		/// <returns>An object that represents the named system semaphore.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x06000A44 RID: 2628 RVA: 0x0002CEE6 File Offset: 0x0002B0E6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static Semaphore OpenExisting(string name)
		{
			return Semaphore.OpenExisting(name, SemaphoreRights.Modify | SemaphoreRights.Synchronize);
		}

		/// <summary>Opens the specified named semaphore, if it already exists, with the desired security access.</summary>
		/// <param name="name">The name of the system semaphore to open.</param>
		/// <param name="rights">A bitwise combination of the enumeration values that represent the desired security access.</param>
		/// <returns>An object that represents the named system semaphore.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named semaphore does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists, but the user does not have the desired security access rights.</exception>
		// Token: 0x06000A45 RID: 2629 RVA: 0x0002CEF4 File Offset: 0x0002B0F4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static Semaphore OpenExisting(string name, SemaphoreRights rights)
		{
			Semaphore result;
			switch (Semaphore.OpenExistingWorker(name, rights, out result))
			{
			case Semaphore.OpenExistingResult.NameNotFound:
				throw new WaitHandleCannotBeOpenedException();
			case Semaphore.OpenExistingResult.PathNotFound:
				InternalResources.WinIOError(3, string.Empty);
				return result;
			case Semaphore.OpenExistingResult.NameInvalid:
				throw new WaitHandleCannotBeOpenedException(SR.GetString("A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.", new object[]
				{
					name
				}));
			default:
				return result;
			}
		}

		/// <summary>Opens the specified named semaphore, if it already exists, and returns a value that indicates whether the operation succeeded.</summary>
		/// <param name="name">The name of the system semaphore to open.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Threading.Semaphore" /> object that represents the named semaphore if the call succeeded, or <see langword="null" /> if the call failed. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the named semaphore was opened successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x06000A46 RID: 2630 RVA: 0x0002CF4F File Offset: 0x0002B14F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool TryOpenExisting(string name, out Semaphore result)
		{
			return Semaphore.OpenExistingWorker(name, SemaphoreRights.Modify | SemaphoreRights.Synchronize, out result) == Semaphore.OpenExistingResult.Success;
		}

		/// <summary>Opens the specified named semaphore, if it already exists, with the desired security access, and returns a value that indicates whether the operation succeeded.</summary>
		/// <param name="name">The name of the system semaphore to open.</param>
		/// <param name="rights">A bitwise combination of the enumeration values that represent the desired security access.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Threading.Semaphore" /> object that represents the named semaphore if the call succeeded, or <see langword="null" /> if the call failed. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the named semaphore was opened successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named semaphore exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x06000A47 RID: 2631 RVA: 0x0002CF60 File Offset: 0x0002B160
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool TryOpenExisting(string name, SemaphoreRights rights, out Semaphore result)
		{
			return Semaphore.OpenExistingWorker(name, rights, out result) == Semaphore.OpenExistingResult.Success;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002CF70 File Offset: 0x0002B170
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static Semaphore.OpenExistingResult OpenExistingWorker(string name, SemaphoreRights rights, out Semaphore result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument {0} cannot be null or zero-length.", new object[]
				{
					"name"
				}), "name");
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(SR.GetString("The name can be no more than 260 characters in length."));
			}
			result = null;
			int num;
			SafeWaitHandle safeWaitHandle = new SafeWaitHandle(Semaphore.OpenSemaphore_internal(name, rights, out num), true);
			if (safeWaitHandle.IsInvalid)
			{
				if (2 == num || 123 == num)
				{
					return Semaphore.OpenExistingResult.NameNotFound;
				}
				if (3 == num)
				{
					return Semaphore.OpenExistingResult.PathNotFound;
				}
				if (name != null && name.Length != 0 && 6 == num)
				{
					return Semaphore.OpenExistingResult.NameInvalid;
				}
				InternalResources.WinIOError(num, "");
			}
			result = new Semaphore(safeWaitHandle);
			return Semaphore.OpenExistingResult.Success;
		}

		/// <summary>Exits the semaphore and returns the previous count.</summary>
		/// <returns>The count on the semaphore before the <see cref="Overload:System.Threading.Semaphore.Release" /> method was called.</returns>
		/// <exception cref="T:System.Threading.SemaphoreFullException">The semaphore count is already at the maximum value.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred with a named semaphore.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current semaphore represents a named system semaphore, but the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.Modify" />.  
		///  -or-  
		///  The current semaphore represents a named system semaphore, but it was not opened with <see cref="F:System.Security.AccessControl.SemaphoreRights.Modify" />.</exception>
		// Token: 0x06000A49 RID: 2633 RVA: 0x0002D027 File Offset: 0x0002B227
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[PrePrepareMethod]
		public int Release()
		{
			return this.Release(1);
		}

		/// <summary>Exits the semaphore a specified number of times and returns the previous count.</summary>
		/// <param name="releaseCount">The number of times to exit the semaphore.</param>
		/// <returns>The count on the semaphore before the <see cref="Overload:System.Threading.Semaphore.Release" /> method was called.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="releaseCount" /> is less than 1.</exception>
		/// <exception cref="T:System.Threading.SemaphoreFullException">The semaphore count is already at the maximum value.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred with a named semaphore.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current semaphore represents a named system semaphore, but the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.Modify" /> rights.  
		///  -or-  
		///  The current semaphore represents a named system semaphore, but it was not opened with <see cref="F:System.Security.AccessControl.SemaphoreRights.Modify" /> rights.</exception>
		// Token: 0x06000A4A RID: 2634 RVA: 0x0002D030 File Offset: 0x0002B230
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public int Release(int releaseCount)
		{
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", SR.GetString("Non-negative number required."));
			}
			int result;
			if (!Semaphore.ReleaseSemaphore_internal(base.SafeWaitHandle.DangerousGetHandle(), releaseCount, out result))
			{
				throw new SemaphoreFullException();
			}
			return result;
		}

		/// <summary>Gets the access control security for a named system semaphore.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> object that represents the access control security for the named system semaphore.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:System.Threading.Semaphore" /> object represents a named system semaphore, and the user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.ReadPermissions" /> rights.  
		///  -or-  
		///  The current <see cref="T:System.Threading.Semaphore" /> object represents a named system semaphore and was not opened with <see cref="F:System.Security.AccessControl.SemaphoreRights.ReadPermissions" /> rights.</exception>
		/// <exception cref="T:System.NotSupportedException">Not supported for Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x06000A4B RID: 2635 RVA: 0x0002D072 File Offset: 0x0002B272
		public SemaphoreSecurity GetAccessControl()
		{
			return new SemaphoreSecurity(base.SafeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Sets the access control security for a named system semaphore.</summary>
		/// <param name="semaphoreSecurity">A <see cref="T:System.Security.AccessControl.SemaphoreSecurity" /> object that represents the access control security to be applied to the named system semaphore.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="semaphoreSecurity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have <see cref="F:System.Security.AccessControl.SemaphoreRights.ChangePermissions" /> rights.  
		///  -or-  
		///  The semaphore was not opened with <see cref="F:System.Security.AccessControl.SemaphoreRights.ChangePermissions" /> rights.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Threading.Semaphore" /> object does not represent a named system semaphore.</exception>
		// Token: 0x06000A4C RID: 2636 RVA: 0x0002D081 File Offset: 0x0002B281
		public void SetAccessControl(SemaphoreSecurity semaphoreSecurity)
		{
			if (semaphoreSecurity == null)
			{
				throw new ArgumentNullException("semaphoreSecurity");
			}
			semaphoreSecurity.Persist(base.SafeWaitHandle);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002D0A0 File Offset: 0x0002B2A0
		internal unsafe static IntPtr CreateSemaphore_internal(int initialCount, int maximumCount, string name, out int errorCode)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Semaphore.CreateSemaphore_icall(initialCount, maximumCount, ptr, (name != null) ? name.Length : 0, out errorCode);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002D0D4 File Offset: 0x0002B2D4
		private unsafe static IntPtr OpenSemaphore_internal(string name, SemaphoreRights rights, out int errorCode)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Semaphore.OpenSemaphore_icall(ptr, (name != null) ? name.Length : 0, rights, out errorCode);
		}

		// Token: 0x06000A4F RID: 2639
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr CreateSemaphore_icall(int initialCount, int maximumCount, char* name, int name_length, out int errorCode);

		// Token: 0x06000A50 RID: 2640
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr OpenSemaphore_icall(char* name, int name_length, SemaphoreRights rights, out int errorCode);

		// Token: 0x06000A51 RID: 2641
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ReleaseSemaphore_internal(IntPtr handle, int releaseCount, out int previousCount);

		// Token: 0x040006D6 RID: 1750
		private const int MAX_PATH = 260;

		// Token: 0x0200017F RID: 383
		private new enum OpenExistingResult
		{
			// Token: 0x040006D8 RID: 1752
			Success,
			// Token: 0x040006D9 RID: 1753
			NameNotFound,
			// Token: 0x040006DA RID: 1754
			PathNotFound,
			// Token: 0x040006DB RID: 1755
			NameInvalid
		}
	}
}
