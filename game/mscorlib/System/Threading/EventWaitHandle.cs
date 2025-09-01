using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	/// <summary>Represents a thread synchronization event.</summary>
	// Token: 0x020002C6 RID: 710
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class EventWaitHandle : WaitHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.EventWaitHandle" /> class, specifying whether the wait handle is initially signaled, and whether it resets automatically or manually.</summary>
		/// <param name="initialState">
		///   <see langword="true" /> to set the initial state to signaled; <see langword="false" /> to set it to nonsignaled.</param>
		/// <param name="mode">One of the <see cref="T:System.Threading.EventResetMode" /> values that determines whether the event resets automatically or manually.</param>
		// Token: 0x06001ED0 RID: 7888 RVA: 0x000727EA File Offset: 0x000709EA
		[SecuritySafeCritical]
		public EventWaitHandle(bool initialState, EventResetMode mode) : this(initialState, mode, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.EventWaitHandle" /> class, specifying whether the wait handle is initially signaled if created as a result of this call, whether it resets automatically or manually, and the name of a system synchronization event.</summary>
		/// <param name="initialState">
		///   <see langword="true" /> to set the initial state to signaled if the named event is created as a result of this call; <see langword="false" /> to set it to nonsignaled.</param>
		/// <param name="mode">One of the <see cref="T:System.Threading.EventResetMode" /> values that determines whether the event resets automatically or manually.</param>
		/// <param name="name">The name of a system-wide synchronization event.</param>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named event cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x06001ED1 RID: 7889 RVA: 0x000727F8 File Offset: 0x000709F8
		[SecurityCritical]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name)
		{
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The name can be no more than 260 characters in length.", new object[]
				{
					name
				}));
			}
			int num;
			SafeWaitHandle safeWaitHandle;
			if (mode != EventResetMode.AutoReset)
			{
				if (mode != EventResetMode.ManualReset)
				{
					throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid.", new object[]
					{
						name
					}));
				}
				safeWaitHandle = new SafeWaitHandle(NativeEventCalls.CreateEvent_internal(true, initialState, name, out num), true);
			}
			else
			{
				safeWaitHandle = new SafeWaitHandle(NativeEventCalls.CreateEvent_internal(false, initialState, name, out num), true);
			}
			if (safeWaitHandle.IsInvalid)
			{
				safeWaitHandle.SetHandleAsInvalid();
				if (name != null && name.Length != 0 && 6 == num)
				{
					throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.", new object[]
					{
						name
					}));
				}
				__Error.WinIOError(num, name);
			}
			base.SetHandleInternal(safeWaitHandle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.EventWaitHandle" /> class, specifying whether the wait handle is initially signaled if created as a result of this call, whether it resets automatically or manually, the name of a system synchronization event, and a Boolean variable whose value after the call indicates whether the named system event was created.</summary>
		/// <param name="initialState">
		///   <see langword="true" /> to set the initial state to signaled if the named event is created as a result of this call; <see langword="false" /> to set it to nonsignaled.</param>
		/// <param name="mode">One of the <see cref="T:System.Threading.EventResetMode" /> values that determines whether the event resets automatically or manually.</param>
		/// <param name="name">The name of a system-wide synchronization event.</param>
		/// <param name="createdNew">When this method returns, contains <see langword="true" /> if a local event was created (that is, if <paramref name="name" /> is <see langword="null" /> or an empty string) or if the specified named system event was created; <see langword="false" /> if the specified named system event already existed. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named event cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x06001ED2 RID: 7890 RVA: 0x000728C3 File Offset: 0x00070AC3
		[SecurityCritical]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew) : this(initialState, mode, name, out createdNew, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.EventWaitHandle" /> class, specifying whether the wait handle is initially signaled if created as a result of this call, whether it resets automatically or manually, the name of a system synchronization event, a Boolean variable whose value after the call indicates whether the named system event was created, and the access control security to be applied to the named event if it is created.</summary>
		/// <param name="initialState">
		///   <see langword="true" /> to set the initial state to signaled if the named event is created as a result of this call; <see langword="false" /> to set it to nonsignaled.</param>
		/// <param name="mode">One of the <see cref="T:System.Threading.EventResetMode" /> values that determines whether the event resets automatically or manually.</param>
		/// <param name="name">The name of a system-wide synchronization event.</param>
		/// <param name="createdNew">When this method returns, contains <see langword="true" /> if a local event was created (that is, if <paramref name="name" /> is <see langword="null" /> or an empty string) or if the specified named system event was created; <see langword="false" /> if the specified named system event already existed. This parameter is passed uninitialized.</param>
		/// <param name="eventSecurity">An <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> object that represents the access control security to be applied to the named system event.</param>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named event cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x06001ED3 RID: 7891 RVA: 0x000728D4 File Offset: 0x00070AD4
		[SecurityCritical]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew, EventWaitHandleSecurity eventSecurity)
		{
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The name can be no more than 260 characters in length.", new object[]
				{
					name
				}));
			}
			bool manual;
			if (mode != EventResetMode.AutoReset)
			{
				if (mode != EventResetMode.ManualReset)
				{
					throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid.", new object[]
					{
						name
					}));
				}
				manual = true;
			}
			else
			{
				manual = false;
			}
			int num;
			SafeWaitHandle safeWaitHandle = new SafeWaitHandle(NativeEventCalls.CreateEvent_internal(manual, initialState, name, out num), true);
			if (safeWaitHandle.IsInvalid)
			{
				safeWaitHandle.SetHandleAsInvalid();
				if (name != null && name.Length != 0 && 6 == num)
				{
					throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.", new object[]
					{
						name
					}));
				}
				__Error.WinIOError(num, name);
			}
			createdNew = (num != 183);
			base.SetHandleInternal(safeWaitHandle);
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000729A0 File Offset: 0x00070BA0
		[SecurityCritical]
		private EventWaitHandle(SafeWaitHandle handle)
		{
			base.SetHandleInternal(handle);
		}

		/// <summary>Opens the specified named synchronization event, if it already exists.</summary>
		/// <param name="name">The name of the system synchronization event to open.</param>
		/// <returns>An  object that represents the named system event.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named system event does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x06001ED5 RID: 7893 RVA: 0x000729AF File Offset: 0x00070BAF
		[SecurityCritical]
		public static EventWaitHandle OpenExisting(string name)
		{
			return EventWaitHandle.OpenExisting(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize);
		}

		/// <summary>Opens the specified named synchronization event, if it already exists, with the desired security access.</summary>
		/// <param name="name">The name of the system synchronization event to open.</param>
		/// <param name="rights">A bitwise combination of the enumeration values that represent the desired security access.</param>
		/// <returns>An object that represents the named system event.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named system event does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists, but the user does not have the desired security access.</exception>
		// Token: 0x06001ED6 RID: 7894 RVA: 0x000729BC File Offset: 0x00070BBC
		[SecurityCritical]
		public static EventWaitHandle OpenExisting(string name, EventWaitHandleRights rights)
		{
			EventWaitHandle result;
			switch (EventWaitHandle.OpenExistingWorker(name, rights, out result))
			{
			case WaitHandle.OpenExistingResult.NameNotFound:
				throw new WaitHandleCannotBeOpenedException();
			case WaitHandle.OpenExistingResult.PathNotFound:
				__Error.WinIOError(3, "");
				return result;
			case WaitHandle.OpenExistingResult.NameInvalid:
				throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.", new object[]
				{
					name
				}));
			default:
				return result;
			}
		}

		/// <summary>Opens the specified named synchronization event, if it already exists, and returns a value that indicates whether the operation succeeded.</summary>
		/// <param name="name">The name of the system synchronization event to open.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Threading.EventWaitHandle" /> object that represents the named synchronization event if the call succeeded, or <see langword="null" /> if the call failed. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the named synchronization event was opened successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists, but the user does not have the desired security access.</exception>
		// Token: 0x06001ED7 RID: 7895 RVA: 0x00072A17 File Offset: 0x00070C17
		[SecurityCritical]
		public static bool TryOpenExisting(string name, out EventWaitHandle result)
		{
			return EventWaitHandle.OpenExistingWorker(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize, out result) == WaitHandle.OpenExistingResult.Success;
		}

		/// <summary>Opens the specified named synchronization event, if it already exists, with the desired security access, and returns a value that indicates whether the operation succeeded.</summary>
		/// <param name="name">The name of the system synchronization event to open.</param>
		/// <param name="rights">A bitwise combination of the enumeration values that represent the desired security access.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Threading.EventWaitHandle" /> object that represents the named synchronization event if the call succeeded, or <see langword="null" /> if the call failed. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the named synchronization event was opened successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named event exists, but the user does not have the desired security access.</exception>
		// Token: 0x06001ED8 RID: 7896 RVA: 0x00072A28 File Offset: 0x00070C28
		[SecurityCritical]
		public static bool TryOpenExisting(string name, EventWaitHandleRights rights, out EventWaitHandle result)
		{
			return EventWaitHandle.OpenExistingWorker(name, rights, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00072A38 File Offset: 0x00070C38
		[SecurityCritical]
		private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, EventWaitHandleRights rights, out EventWaitHandle result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", Environment.GetResourceString("Parameter '{0}' cannot be null."));
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "name");
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The name can be no more than 260 characters in length.", new object[]
				{
					name
				}));
			}
			result = null;
			int num;
			SafeWaitHandle safeWaitHandle = new SafeWaitHandle(NativeEventCalls.OpenEvent_internal(name, rights, out num), true);
			if (safeWaitHandle.IsInvalid)
			{
				if (2 == num || 123 == num)
				{
					return WaitHandle.OpenExistingResult.NameNotFound;
				}
				if (3 == num)
				{
					return WaitHandle.OpenExistingResult.PathNotFound;
				}
				if (name != null && name.Length != 0 && 6 == num)
				{
					return WaitHandle.OpenExistingResult.NameInvalid;
				}
				__Error.WinIOError(num, "");
			}
			result = new EventWaitHandle(safeWaitHandle);
			return WaitHandle.OpenExistingResult.Success;
		}

		/// <summary>Sets the state of the event to nonsignaled, causing threads to block.</summary>
		/// <returns>
		///   <see langword="true" /> if the operation succeeds; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="M:System.Threading.WaitHandle.Close" /> method was previously called on this <see cref="T:System.Threading.EventWaitHandle" />.</exception>
		// Token: 0x06001EDA RID: 7898 RVA: 0x00072AF5 File Offset: 0x00070CF5
		[SecuritySafeCritical]
		public bool Reset()
		{
			bool flag = NativeEventCalls.ResetEvent(this.safeWaitHandle);
			if (!flag)
			{
				throw new IOException();
			}
			return flag;
		}

		/// <summary>Sets the state of the event to signaled, allowing one or more waiting threads to proceed.</summary>
		/// <returns>
		///   <see langword="true" /> if the operation succeeds; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="M:System.Threading.WaitHandle.Close" /> method was previously called on this <see cref="T:System.Threading.EventWaitHandle" />.</exception>
		// Token: 0x06001EDB RID: 7899 RVA: 0x00072B0D File Offset: 0x00070D0D
		[SecuritySafeCritical]
		public bool Set()
		{
			bool flag = NativeEventCalls.SetEvent(this.safeWaitHandle);
			if (!flag)
			{
				throw new IOException();
			}
			return flag;
		}

		/// <summary>Gets an <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> object that represents the access control security for the named system event represented by the current <see cref="T:System.Threading.EventWaitHandle" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> object that represents the access control security for the named system event.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:System.Threading.EventWaitHandle" /> object represents a named system event, and the user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ReadPermissions" />.  
		///  -or-  
		///  The current <see cref="T:System.Threading.EventWaitHandle" /> object represents a named system event, and was not opened with <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ReadPermissions" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Not supported for Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="M:System.Threading.WaitHandle.Close" /> method was previously called on this <see cref="T:System.Threading.EventWaitHandle" />.</exception>
		// Token: 0x06001EDC RID: 7900 RVA: 0x00072B25 File Offset: 0x00070D25
		[SecuritySafeCritical]
		public EventWaitHandleSecurity GetAccessControl()
		{
			return new EventWaitHandleSecurity(this.safeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Sets the access control security for a named system event.</summary>
		/// <param name="eventSecurity">An <see cref="T:System.Security.AccessControl.EventWaitHandleSecurity" /> object that represents the access control security to be applied to the named system event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="eventSecurity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ChangePermissions" />.  
		///  -or-  
		///  The event was not opened with <see cref="F:System.Security.AccessControl.EventWaitHandleRights.ChangePermissions" />.</exception>
		/// <exception cref="T:System.SystemException">The current <see cref="T:System.Threading.EventWaitHandle" /> object does not represent a named system event.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="M:System.Threading.WaitHandle.Close" /> method was previously called on this <see cref="T:System.Threading.EventWaitHandle" />.</exception>
		// Token: 0x06001EDD RID: 7901 RVA: 0x00072B36 File Offset: 0x00070D36
		[SecuritySafeCritical]
		public void SetAccessControl(EventWaitHandleSecurity eventSecurity)
		{
			if (eventSecurity == null)
			{
				throw new ArgumentNullException("eventSecurity");
			}
			eventSecurity.Persist(this.safeWaitHandle);
		}
	}
}
