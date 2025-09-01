using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>A synchronization primitive that can also be used for interprocess synchronization.</summary>
	// Token: 0x020002F3 RID: 755
	[ComVisible(true)]
	public sealed class Mutex : WaitHandle
	{
		// Token: 0x060020D9 RID: 8409
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr CreateMutex_icall(bool initiallyOwned, char* name, int name_length, out bool created);

		// Token: 0x060020DA RID: 8410
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr OpenMutex_icall(char* name, int name_length, MutexRights rights, out MonoIOError error);

		// Token: 0x060020DB RID: 8411
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ReleaseMutex_internal(IntPtr handle);

		// Token: 0x060020DC RID: 8412 RVA: 0x00076C40 File Offset: 0x00074E40
		private unsafe static IntPtr CreateMutex_internal(bool initiallyOwned, string name, out bool created)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Mutex.CreateMutex_icall(initiallyOwned, ptr, (name != null) ? name.Length : 0, out created);
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00076C74 File Offset: 0x00074E74
		private unsafe static IntPtr OpenMutex_internal(string name, MutexRights rights, out MonoIOError error)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Mutex.OpenMutex_icall(ptr, (name != null) ? name.Length : 0, rights, out error);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00076CA5 File Offset: 0x00074EA5
		private Mutex(IntPtr handle)
		{
			this.Handle = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with default properties.</summary>
		// Token: 0x060020DF RID: 8415 RVA: 0x00076CB4 File Offset: 0x00074EB4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Mutex()
		{
			bool flag;
			this.Handle = Mutex.CreateMutex_internal(false, null, out flag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with a Boolean value that indicates whether the calling thread should have initial ownership of the mutex.</summary>
		/// <param name="initiallyOwned">
		///   <see langword="true" /> to give the calling thread initial ownership of the mutex; otherwise, <see langword="false" />.</param>
		// Token: 0x060020E0 RID: 8416 RVA: 0x00076CD8 File Offset: 0x00074ED8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public Mutex(bool initiallyOwned)
		{
			bool flag;
			this.Handle = Mutex.CreateMutex_internal(initiallyOwned, null, out flag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with a Boolean value that indicates whether the calling thread should have initial ownership of the mutex, and a string that is the name of the mutex.</summary>
		/// <param name="initiallyOwned">
		///   <see langword="true" /> to give the calling thread initial ownership of the named system mutex if the named system mutex is created as a result of this call; otherwise, <see langword="false" />.</param>
		/// <param name="name">The name of the <see cref="T:System.Threading.Mutex" />. If the value is <see langword="null" />, the <see cref="T:System.Threading.Mutex" /> is unnamed.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named mutex cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x060020E1 RID: 8417 RVA: 0x00076CFC File Offset: 0x00074EFC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public Mutex(bool initiallyOwned, string name)
		{
			bool flag;
			this.Handle = Mutex.CreateMutex_internal(initiallyOwned, name, out flag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with a Boolean value that indicates whether the calling thread should have initial ownership of the mutex, a string that is the name of the mutex, and a Boolean value that, when the method returns, indicates whether the calling thread was granted initial ownership of the mutex.</summary>
		/// <param name="initiallyOwned">
		///   <see langword="true" /> to give the calling thread initial ownership of the named system mutex if the named system mutex is created as a result of this call; otherwise, <see langword="false" />.</param>
		/// <param name="name">The name of the <see cref="T:System.Threading.Mutex" />. If the value is <see langword="null" />, the <see cref="T:System.Threading.Mutex" /> is unnamed.</param>
		/// <param name="createdNew">When this method returns, contains a Boolean that is <see langword="true" /> if a local mutex was created (that is, if <paramref name="name" /> is <see langword="null" /> or an empty string) or if the specified named system mutex was created; <see langword="false" /> if the specified named system mutex already existed. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named mutex cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x060020E2 RID: 8418 RVA: 0x00076D1E File Offset: 0x00074F1E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public Mutex(bool initiallyOwned, string name, out bool createdNew)
		{
			this.Handle = Mutex.CreateMutex_internal(initiallyOwned, name, out createdNew);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Mutex" /> class with a Boolean value that indicates whether the calling thread should have initial ownership of the mutex, a string that is the name of the mutex, a Boolean variable that, when the method returns, indicates whether the calling thread was granted initial ownership of the mutex, and the access control security to be applied to the named mutex.</summary>
		/// <param name="initiallyOwned">
		///   <see langword="true" /> to give the calling thread initial ownership of the named system mutex if the named system mutex is created as a result of this call; otherwise, <see langword="false" />.</param>
		/// <param name="name">The name of the system mutex. If the value is <see langword="null" />, the <see cref="T:System.Threading.Mutex" /> is unnamed.</param>
		/// <param name="createdNew">When this method returns, contains a Boolean that is <see langword="true" /> if a local mutex was created (that is, if <paramref name="name" /> is <see langword="null" /> or an empty string) or if the specified named system mutex was created; <see langword="false" /> if the specified named system mutex already existed. This parameter is passed uninitialized.</param>
		/// <param name="mutexSecurity">A <see cref="T:System.Security.AccessControl.MutexSecurity" /> object that represents the access control security to be applied to the named system mutex.</param>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists and has access control security, but the user does not have <see cref="F:System.Security.AccessControl.MutexRights.FullControl" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named mutex cannot be created, perhaps because a wait handle of a different type has the same name.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is longer than 260 characters.</exception>
		// Token: 0x060020E3 RID: 8419 RVA: 0x00076D1E File Offset: 0x00074F1E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MonoTODO("Use MutexSecurity in CreateMutex_internal")]
		public Mutex(bool initiallyOwned, string name, out bool createdNew, MutexSecurity mutexSecurity)
		{
			this.Handle = Mutex.CreateMutex_internal(initiallyOwned, name, out createdNew);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.MutexSecurity" /> object that represents the access control security for the named mutex.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.MutexSecurity" /> object that represents the access control security for the named mutex.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The current <see cref="T:System.Threading.Mutex" /> object represents a named system mutex, but the user does not have <see cref="F:System.Security.AccessControl.MutexRights.ReadPermissions" />.  
		///  -or-  
		///  The current <see cref="T:System.Threading.Mutex" /> object represents a named system mutex, and was not opened with <see cref="F:System.Security.AccessControl.MutexRights.ReadPermissions" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Not supported for Windows 98 or Windows Millennium Edition.</exception>
		// Token: 0x060020E4 RID: 8420 RVA: 0x00076D34 File Offset: 0x00074F34
		public MutexSecurity GetAccessControl()
		{
			return new MutexSecurity(base.SafeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Opens the specified named mutex, if it already exists.</summary>
		/// <param name="name">The name of the system mutex to open.</param>
		/// <returns>An object that represents the named system mutex.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named mutex does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x060020E5 RID: 8421 RVA: 0x00076D43 File Offset: 0x00074F43
		public static Mutex OpenExisting(string name)
		{
			return Mutex.OpenExisting(name, MutexRights.Modify | MutexRights.Synchronize);
		}

		/// <summary>Opens the specified named mutex, if it already exists, with the desired security access.</summary>
		/// <param name="name">The name of the system mutex to open.</param>
		/// <param name="rights">A bitwise combination of the enumeration values that represent the desired security access.</param>
		/// <returns>An object that represents the named system mutex.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.WaitHandleCannotBeOpenedException">The named mutex does not exist.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists, but the user does not have the desired security access.</exception>
		// Token: 0x060020E6 RID: 8422 RVA: 0x00076D50 File Offset: 0x00074F50
		public static Mutex OpenExisting(string name, MutexRights rights)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0 || name.Length > 260)
			{
				throw new ArgumentException("name", Locale.GetText("Invalid length [1-260]."));
			}
			MonoIOError monoIOError;
			IntPtr intPtr = Mutex.OpenMutex_internal(name, rights, out monoIOError);
			if (!(intPtr == (IntPtr)null))
			{
				return new Mutex(intPtr);
			}
			if (monoIOError == MonoIOError.ERROR_FILE_NOT_FOUND)
			{
				throw new WaitHandleCannotBeOpenedException(Locale.GetText("Named Mutex handle does not exist: ") + name);
			}
			if (monoIOError == MonoIOError.ERROR_ACCESS_DENIED)
			{
				throw new UnauthorizedAccessException();
			}
			throw new IOException(Locale.GetText("Win32 IO error: ") + monoIOError.ToString());
		}

		/// <summary>Opens the specified named mutex, if it already exists, and returns a value that indicates whether the operation succeeded.</summary>
		/// <param name="name">The name of the system mutex to open.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Threading.Mutex" /> object that represents the named mutex if the call succeeded, or <see langword="null" /> if the call failed. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the named mutex was opened successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x060020E7 RID: 8423 RVA: 0x00076DF8 File Offset: 0x00074FF8
		public static bool TryOpenExisting(string name, out Mutex result)
		{
			return Mutex.TryOpenExisting(name, MutexRights.Modify | MutexRights.Synchronize, out result);
		}

		/// <summary>Opens the specified named mutex, if it already exists, with the desired security access, and returns a value that indicates whether the operation succeeded.</summary>
		/// <param name="name">The name of the system mutex to open.</param>
		/// <param name="rights">A bitwise combination of the enumeration values that represent the desired security access.</param>
		/// <param name="result">When this method returns, contains a <see cref="T:System.Threading.Mutex" /> object that represents the named mutex if the call succeeded, or <see langword="null" /> if the call failed. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the named mutex was opened successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.  
		/// -or-  
		/// <paramref name="name" /> is longer than 260 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">A Win32 error occurred.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The named mutex exists, but the user does not have the security access required to use it.</exception>
		// Token: 0x060020E8 RID: 8424 RVA: 0x00076E08 File Offset: 0x00075008
		public static bool TryOpenExisting(string name, MutexRights rights, out Mutex result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0 || name.Length > 260)
			{
				throw new ArgumentException("name", Locale.GetText("Invalid length [1-260]."));
			}
			MonoIOError monoIOError;
			IntPtr intPtr = Mutex.OpenMutex_internal(name, rights, out monoIOError);
			if (intPtr == (IntPtr)null)
			{
				result = null;
				return false;
			}
			result = new Mutex(intPtr);
			return true;
		}

		/// <summary>Releases the <see cref="T:System.Threading.Mutex" /> once.</summary>
		/// <exception cref="T:System.ApplicationException">The calling thread does not own the mutex.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x060020E9 RID: 8425 RVA: 0x00076E74 File Offset: 0x00075074
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void ReleaseMutex()
		{
			if (!Mutex.ReleaseMutex_internal(this.Handle))
			{
				throw new ApplicationException("Mutex is not owned");
			}
		}

		/// <summary>Sets the access control security for a named system mutex.</summary>
		/// <param name="mutexSecurity">A <see cref="T:System.Security.AccessControl.MutexSecurity" /> object that represents the access control security to be applied to the named system mutex.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mutexSecurity" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The user does not have <see cref="F:System.Security.AccessControl.MutexRights.ChangePermissions" />.  
		///  -or-  
		///  The mutex was not opened with <see cref="F:System.Security.AccessControl.MutexRights.ChangePermissions" />.</exception>
		/// <exception cref="T:System.SystemException">The current <see cref="T:System.Threading.Mutex" /> object does not represent a named system mutex.</exception>
		// Token: 0x060020EA RID: 8426 RVA: 0x00076E8E File Offset: 0x0007508E
		public void SetAccessControl(MutexSecurity mutexSecurity)
		{
			if (mutexSecurity == null)
			{
				throw new ArgumentNullException("mutexSecurity");
			}
			mutexSecurity.PersistModifications(base.SafeWaitHandle);
		}
	}
}
