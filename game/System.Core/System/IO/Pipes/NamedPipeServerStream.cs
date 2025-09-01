using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes a <see cref="T:System.IO.Stream" /> around a named pipe, supporting both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x02000343 RID: 835
	public sealed class NamedPipeServerStream : PipeStream
	{
		// Token: 0x0600194F RID: 6479 RVA: 0x00054D8C File Offset: 0x00052F8C
		private void Create(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, HandleInheritability inheritability)
		{
			this.Create(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, null, inheritability, (PipeAccessRights)0);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00054DB0 File Offset: 0x00052FB0
		private void Create(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability, PipeAccessRights additionalAccessRights)
		{
			string fullPath = Path.GetFullPath("\\\\.\\pipe\\" + pipeName);
			if (string.Equals(fullPath, "\\\\.\\pipe\\anonymous", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentOutOfRangeException("pipeName", "The pipeName \\\"anonymous\\\" is reserved.");
			}
			if (base.IsCurrentUserOnly)
			{
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					SecurityIdentifier owner = current.Owner;
					PipeAccessRule rule = new PipeAccessRule(owner, PipeAccessRights.FullControl, AccessControlType.Allow);
					pipeSecurity = new PipeSecurity();
					pipeSecurity.AddAccessRule(rule);
					pipeSecurity.SetOwner(owner);
				}
				options &= ~PipeOptions.CurrentUserOnly;
			}
			int openMode = (int)(direction | ((maxNumberOfServerInstances == 1) ? ((PipeDirection)524288) : ((PipeDirection)0)) | (PipeDirection)options | (PipeDirection)additionalAccessRights);
			int pipeMode = (int)((int)transmissionMode << 2 | (int)transmissionMode << 1);
			if (maxNumberOfServerInstances == -1)
			{
				maxNumberOfServerInstances = 255;
			}
			GCHandle gchandle = default(GCHandle);
			try
			{
				Interop.Kernel32.SECURITY_ATTRIBUTES secAttrs = PipeStream.GetSecAttrs(inheritability, pipeSecurity, ref gchandle);
				SafePipeHandle safePipeHandle = Interop.Kernel32.CreateNamedPipe(fullPath, openMode, pipeMode, maxNumberOfServerInstances, outBufferSize, inBufferSize, 0, ref secAttrs);
				if (safePipeHandle.IsInvalid)
				{
					throw Win32Marshal.GetExceptionForLastWin32Error("");
				}
				base.InitializeHandle(safePipeHandle, false, (options & PipeOptions.Asynchronous) > PipeOptions.None);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
		}

		/// <summary>Waits for a client to connect to this <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> object.</summary>
		/// <exception cref="T:System.InvalidOperationException">A pipe connection has already been established.-or-The pipe handle has not been set.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe connection has been broken.</exception>
		// Token: 0x06001951 RID: 6481 RVA: 0x00054EEC File Offset: 0x000530EC
		public void WaitForConnection()
		{
			this.CheckConnectOperationsServerWithHandle();
			if (base.IsAsync)
			{
				this.WaitForConnectionCoreAsync(CancellationToken.None).GetAwaiter().GetResult();
				return;
			}
			if (!Interop.Kernel32.ConnectNamedPipe(base.InternalHandle, IntPtr.Zero))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 535)
				{
					throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
				}
				if (lastWin32Error == 535 && base.State == PipeState.Connected)
				{
					throw new InvalidOperationException("Already in a connected state.");
				}
			}
			base.State = PipeState.Connected;
		}

		/// <summary>Asynchronously waits for a client to connect to this <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> object and monitors cancellation requests.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous wait operation.</returns>
		// Token: 0x06001952 RID: 6482 RVA: 0x00054F70 File Offset: 0x00053170
		public Task WaitForConnectionAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (!base.IsAsync)
			{
				return Task.Factory.StartNew(delegate(object s)
				{
					((NamedPipeServerStream)s).WaitForConnection();
				}, this, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			return this.WaitForConnectionCoreAsync(cancellationToken);
		}

		/// <summary>Disconnects the current connection.</summary>
		/// <exception cref="T:System.InvalidOperationException">No pipe connections have been made yet.-or-The connected pipe has already disconnected.-or-The pipe handle has not been set.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x06001953 RID: 6483 RVA: 0x00054FCE File Offset: 0x000531CE
		public void Disconnect()
		{
			this.CheckDisconnectOperations();
			if (!Interop.Kernel32.DisconnectNamedPipe(base.InternalHandle))
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
			base.State = PipeState.Disconnected;
		}

		/// <summary>Gets the user name of the client on the other end of the pipe.</summary>
		/// <returns>The user name of the client on the other end of the pipe.</returns>
		/// <exception cref="T:System.InvalidOperationException">No pipe connections have been made yet.-or-The connected pipe has already disconnected.-or-The pipe handle has not been set.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe connection has been broken.-or-The user name of the client is longer than 19 characters.</exception>
		// Token: 0x06001954 RID: 6484 RVA: 0x00054FF8 File Offset: 0x000531F8
		public string GetImpersonationUserName()
		{
			base.CheckWriteOperations();
			StringBuilder stringBuilder = new StringBuilder(514);
			if (!Interop.Kernel32.GetNamedPipeHandleState(base.InternalHandle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, stringBuilder, stringBuilder.Capacity))
			{
				throw base.WinIOError(Marshal.GetLastWin32Error());
			}
			return stringBuilder.ToString();
		}

		/// <summary>Calls a delegate while impersonating the client.</summary>
		/// <param name="impersonationWorker">The delegate that specifies a method to call.</param>
		/// <exception cref="T:System.InvalidOperationException">No pipe connections have been made yet.-or-The connected pipe has already disconnected.-or-The pipe handle has not been set.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe connection has been broken.-or-An I/O error occurred.</exception>
		// Token: 0x06001955 RID: 6485 RVA: 0x00055050 File Offset: 0x00053250
		public void RunAsClient(PipeStreamImpersonationWorker impersonationWorker)
		{
			base.CheckWriteOperations();
			NamedPipeServerStream.ExecuteHelper executeHelper = new NamedPipeServerStream.ExecuteHelper(impersonationWorker, base.InternalHandle);
			RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(NamedPipeServerStream.tryCode, NamedPipeServerStream.cleanupCode, executeHelper);
			if (executeHelper._impersonateErrorCode != 0)
			{
				throw base.WinIOError(executeHelper._impersonateErrorCode);
			}
			if (executeHelper._revertImpersonateErrorCode != 0)
			{
				throw base.WinIOError(executeHelper._revertImpersonateErrorCode);
			}
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000550AC File Offset: 0x000532AC
		private static void ImpersonateAndTryCode(object helper)
		{
			NamedPipeServerStream.ExecuteHelper executeHelper = (NamedPipeServerStream.ExecuteHelper)helper;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (Interop.Advapi32.ImpersonateNamedPipeClient(executeHelper._handle))
				{
					executeHelper._mustRevert = true;
				}
				else
				{
					executeHelper._impersonateErrorCode = Marshal.GetLastWin32Error();
				}
			}
			if (executeHelper._mustRevert)
			{
				executeHelper._userCode();
			}
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0005510C File Offset: 0x0005330C
		private static void RevertImpersonationOnBackout(object helper, bool exceptionThrown)
		{
			NamedPipeServerStream.ExecuteHelper executeHelper = (NamedPipeServerStream.ExecuteHelper)helper;
			if (executeHelper._mustRevert && !Interop.Advapi32.RevertToSelf())
			{
				executeHelper._revertImpersonateErrorCode = Marshal.GetLastWin32Error();
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0005513C File Offset: 0x0005333C
		private Task WaitForConnectionCoreAsync(CancellationToken cancellationToken)
		{
			this.CheckConnectOperationsServerWithHandle();
			if (!base.IsAsync)
			{
				throw new InvalidOperationException("Pipe is not opened in asynchronous mode.");
			}
			ConnectionCompletionSource connectionCompletionSource = new ConnectionCompletionSource(this);
			if (!Interop.Kernel32.ConnectNamedPipe(base.InternalHandle, connectionCompletionSource.Overlapped))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 535)
				{
					if (lastWin32Error != 997)
					{
						connectionCompletionSource.ReleaseResources();
						throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
					}
				}
				else
				{
					connectionCompletionSource.ReleaseResources();
					if (base.State == PipeState.Connected)
					{
						throw new InvalidOperationException("Already in a connected state.");
					}
					connectionCompletionSource.SetCompletedSynchronously();
					return Task.CompletedTask;
				}
			}
			connectionCompletionSource.RegisterForCancellation(cancellationToken);
			return connectionCompletionSource.Task;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000551D9 File Offset: 0x000533D9
		private void CheckConnectOperationsServerWithHandle()
		{
			if (base.InternalHandle == null)
			{
				throw new InvalidOperationException("Pipe handle has not been set.  Did your PipeStream implementation call InitializeHandle?");
			}
			this.CheckConnectOperationsServer();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class with the specified pipe name.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="pipeName" /> contains a colon (":").</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows Millennium Edition, Windows 98, or Windows 95, which are not supported. </exception>
		/// <exception cref="T:System.IO.IOException">The maximum number of server instances has been exceeded.</exception>
		// Token: 0x0600195A RID: 6490 RVA: 0x000551F4 File Offset: 0x000533F4
		public NamedPipeServerStream(string pipeName) : this(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.None, 0, 0, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class with the specified pipe name and pipe direction.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="pipeName" /> contains a colon (":").</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows Millennium Edition, Windows 98, or Windows 95, which are not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The maximum number of server instances has been exceeded.</exception>
		// Token: 0x0600195B RID: 6491 RVA: 0x00055210 File Offset: 0x00053410
		public NamedPipeServerStream(string pipeName, PipeDirection direction) : this(pipeName, direction, 1, PipeTransmissionMode.Byte, PipeOptions.None, 0, 0, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class with the specified pipe name, pipe direction, and maximum number of server instances.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="maxNumberOfServerInstances">The maximum number of server instances that share the same name. You can pass <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" /> for this value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-A non-negative number is required.-or-
		///         <paramref name="maxNumberofServerInstances" /> is less than -1 or greater than 254 (-1 indicates <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" />)-or-
		///
		///         <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" /> is required.-or-Access rights is limited to the <see cref="F:System.IO.Pipes.PipeAccessRights.ChangePermissions" /> , <see cref="F:System.IO.Pipes.PipeAccessRights.TakeOwnership" /> , and <see cref="F:System.IO.Pipes.PipeAccessRights.AccessSystemSecurity" /> flags.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="pipeName" /> contains a colon (":").</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows Millennium Edition, Windows 98, or Windows 95, which are not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The maximum number of server instances has been exceeded.</exception>
		// Token: 0x0600195C RID: 6492 RVA: 0x0005522C File Offset: 0x0005342C
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances) : this(pipeName, direction, maxNumberOfServerInstances, PipeTransmissionMode.Byte, PipeOptions.None, 0, 0, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class with the specified pipe name, pipe direction, maximum number of server instances, and transmission mode.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="maxNumberOfServerInstances">The maximum number of server instances that share the same name. You can pass <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" /> for this value.</param>
		/// <param name="transmissionMode">One of the enumeration values that determines the transmission mode of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-
		///         <paramref name="maxNumberofServerInstances" /> is less than -1 or greater than 254 (-1 indicates <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" />)</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="pipeName" /> contains a colon (":").</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows Millennium Edition, Windows 98, or Windows 95, which are not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The maximum number of server instances has been exceeded.</exception>
		// Token: 0x0600195D RID: 6493 RVA: 0x00055248 File Offset: 0x00053448
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, PipeOptions.None, 0, 0, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class with the specified pipe name, pipe direction, maximum number of server instances, transmission mode, and pipe options.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="maxNumberOfServerInstances">The maximum number of server instances that share the same name. You can pass <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" /> for this value.</param>
		/// <param name="transmissionMode">One of the enumeration values that determines the transmission mode of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-
		///         <paramref name="maxNumberofServerInstances" /> is less than -1 or greater than 254 (-1 indicates <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" />)-or-
		///         <paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="pipeName" /> contains a colon (":").</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows Millennium Edition, Windows 98, or Windows 95, which are not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The maximum number of server instances has been exceeded.</exception>
		// Token: 0x0600195E RID: 6494 RVA: 0x00055264 File Offset: 0x00053464
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, 0, 0, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class with the specified pipe name, pipe direction, maximum number of server instances, transmission mode, pipe options, and recommended in and out buffer sizes.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="maxNumberOfServerInstances">The maximum number of server instances that share the same name. You can pass <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" /> for this value.</param>
		/// <param name="transmissionMode">One of the enumeration values that determines the transmission mode of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="inBufferSize">A positive value greater than 0 that indicates the input buffer size.</param>
		/// <param name="outBufferSize">A positive value greater than 0 that indicates the output buffer size.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-
		///         <paramref name="maxNumberofServerInstances" /> is less than -1 or greater than 254 (-1 indicates <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" />)-or-
		///         <paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-
		///         <paramref name="inBufferSize" /> is negative.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="pipeName" /> contains a colon (":").</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows Millennium Edition, Windows 98, or Windows 95, which are not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The maximum number of server instances has been exceeded.</exception>
		// Token: 0x0600195F RID: 6495 RVA: 0x00055284 File Offset: 0x00053484
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, HandleInheritability.None)
		{
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000552A4 File Offset: 0x000534A4
		private NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, HandleInheritability inheritability) : base(direction, transmissionMode, outBufferSize)
		{
			if (pipeName == null)
			{
				throw new ArgumentNullException("pipeName");
			}
			if (pipeName.Length == 0)
			{
				throw new ArgumentException("pipeName cannot be an empty string.");
			}
			if ((options & (PipeOptions)536870911) != PipeOptions.None)
			{
				throw new ArgumentOutOfRangeException("options", "options contains an invalid flag.");
			}
			if (inBufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("inBufferSize", "Non negative number is required.");
			}
			if ((maxNumberOfServerInstances < 1 || maxNumberOfServerInstances > 254) && maxNumberOfServerInstances != -1)
			{
				throw new ArgumentOutOfRangeException("maxNumberOfServerInstances", "maxNumberOfServerInstances must either be a value between 1 and 254, or NamedPipeServerStream.MaxAllowedServerInstances (to obtain the maximum number allowed by system resources).");
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", "HandleInheritability.None or HandleInheritability.Inheritable required.");
			}
			if ((options & PipeOptions.CurrentUserOnly) != PipeOptions.None)
			{
				base.IsCurrentUserOnly = true;
			}
			this.Create(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, inheritability);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class from the specified pipe handle.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="isAsync">
		///       <see langword="true" /> to indicate that the handle was opened asynchronously; otherwise, <see langword="false" />.</param>
		/// <param name="isConnected">
		///       <see langword="true" /> to indicate that the pipe is connected; otherwise, <see langword="false" />.</param>
		/// <param name="safePipeHandle">A safe handle for the pipe that this <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> object will encapsulate.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="safePipeHandle" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="safePipeHandle" /> is an invalid handle.</exception>
		/// <exception cref="T:System.IO.IOException">
		///         <paramref name="safePipeHandle" /> is not a valid pipe handle.-or-The maximum number of server instances has been exceeded.</exception>
		// Token: 0x06001961 RID: 6497 RVA: 0x0005536C File Offset: 0x0005356C
		public NamedPipeServerStream(PipeDirection direction, bool isAsync, bool isConnected, SafePipeHandle safePipeHandle) : base(direction, PipeTransmissionMode.Byte, 0)
		{
			if (safePipeHandle == null)
			{
				throw new ArgumentNullException("safePipeHandle");
			}
			if (safePipeHandle.IsInvalid)
			{
				throw new ArgumentException("Invalid handle.", "safePipeHandle");
			}
			base.ValidateHandleIsPipe(safePipeHandle);
			base.InitializeHandle(safePipeHandle, true, isAsync);
			if (isConnected)
			{
				base.State = PipeState.Connected;
			}
		}

		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> instance is reclaimed by garbage collection.</summary>
		// Token: 0x06001962 RID: 6498 RVA: 0x000553C8 File Offset: 0x000535C8
		~NamedPipeServerStream()
		{
			this.Dispose(false);
		}

		/// <summary>Asynchronously waits for a client to connect to this <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> object.</summary>
		/// <returns>A task that represents the asynchronous wait operation.</returns>
		// Token: 0x06001963 RID: 6499 RVA: 0x000553F8 File Offset: 0x000535F8
		public Task WaitForConnectionAsync()
		{
			return this.WaitForConnectionAsync(CancellationToken.None);
		}

		/// <summary>Begins an asynchronous operation to wait for a client to connect.</summary>
		/// <param name="callback">The method to call when a client connects to the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> object.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous request from other requests.</param>
		/// <returns>An object that references the asynchronous request.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe was not opened asynchronously.-or-A pipe connection has already been established.-or-The pipe handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe connection has been broken.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x06001964 RID: 6500 RVA: 0x00055405 File Offset: 0x00053605
		public IAsyncResult BeginWaitForConnection(AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.WaitForConnectionAsync(), callback, state);
		}

		/// <summary>Ends an asynchronous operation to wait for a client to connect.</summary>
		/// <param name="asyncResult">The pending asynchronous request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe was not opened asynchronously.-or-The pipe handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe connection has been broken.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x06001965 RID: 6501 RVA: 0x00055414 File Offset: 0x00053614
		public void EndWaitForConnection(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0005541C File Offset: 0x0005361C
		private void CheckConnectOperationsServer()
		{
			if (base.State == PipeState.Closed)
			{
				throw Error.GetPipeNotOpen();
			}
			if (base.InternalHandle != null && base.InternalHandle.IsClosed)
			{
				throw Error.GetPipeNotOpen();
			}
			if (base.State == PipeState.Broken)
			{
				throw new IOException("Pipe is broken.");
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0005545C File Offset: 0x0005365C
		private void CheckDisconnectOperations()
		{
			if (base.State == PipeState.WaitingToConnect)
			{
				throw new InvalidOperationException("Pipe hasn't been connected yet.");
			}
			if (base.State == PipeState.Disconnected)
			{
				throw new InvalidOperationException("Already in a disconnected state.");
			}
			if (base.InternalHandle == null)
			{
				throw new InvalidOperationException("Pipe handle has not been set.  Did your PipeStream implementation call InitializeHandle?");
			}
			if (base.State == PipeState.Closed || (base.InternalHandle != null && base.InternalHandle.IsClosed))
			{
				throw Error.GetPipeNotOpen();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class with the specified pipe name, pipe direction, maximum number of server instances, transmission mode, pipe options, recommended in and out buffer sizes, and pipe security.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="maxNumberOfServerInstances">The maximum number of server instances that share the same name. You can pass <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" /> for this value.</param>
		/// <param name="transmissionMode">One of the enumeration values that determines the transmission mode of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="inBufferSize">A positive value greater than 0 that indicates the input buffer size.</param>
		/// <param name="outBufferSize">A positive value greater than 0 that indicates the output buffer size.</param>
		/// <param name="pipeSecurity">An object that determines the access control and audit security for the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-
		///         <paramref name="maxNumberofServerInstances" />  is less than -1 or greater than 254 (-1 indicates <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" />)-or-
		///         <paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-
		///         <paramref name="inBufferSize" /> is negative.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="pipeName" /> contains a colon (":").</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows Millennium Edition, Windows 98, or Windows 95, which are not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The maximum number of server instances has been exceeded.</exception>
		// Token: 0x06001968 RID: 6504 RVA: 0x000554C8 File Offset: 0x000536C8
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class with the specified pipe name, pipe direction, maximum number of server instances, transmission mode, pipe options, recommended in and out buffer sizes, pipe security, and inheritability mode.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="maxNumberOfServerInstances">The maximum number of server instances that share the same name. You can pass <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" /> for this value.</param>
		/// <param name="transmissionMode">One of the enumeration values that determines the transmission mode of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="inBufferSize">A positive value greater than 0 that indicates the input buffer size.</param>
		/// <param name="outBufferSize">A positive value greater than 0 that indicates the output buffer size.</param>
		/// <param name="pipeSecurity">An object that determines the access control and audit security for the pipe.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle can be inherited by child processes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-
		///         <paramref name="maxNumberofServerInstances" /> is less than -1 or greater than 254 (-1 indicates <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" />)-or-
		///         <paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-
		///         <paramref name="inBufferSize" /> is negative.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> value.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="pipeName" /> contains a colon (":").</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows Millennium Edition, Windows 98, or Windows 95, which are not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The maximum number of server instances has been exceeded.</exception>
		// Token: 0x06001969 RID: 6505 RVA: 0x000554E8 File Offset: 0x000536E8
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability) : this(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, inheritability)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> class with the specified pipe name, pipe direction, maximum number of server instances, transmission mode, pipe options, recommended in and out buffer sizes, pipe security, inheritability mode, and pipe access rights.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="maxNumberOfServerInstances">The maximum number of server instances that share the same name. You can pass <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" /> for this value.</param>
		/// <param name="transmissionMode">One of the enumeration values that determines the transmission mode of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="inBufferSize">The input buffer size.</param>
		/// <param name="outBufferSize">The output buffer size.</param>
		/// <param name="pipeSecurity">An object that determines the access control and audit security for the pipe.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle can be inherited by child processes.</param>
		/// <param name="additionalAccessRights">One of the enumeration values that specifies the access rights of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-
		///         <paramref name="maxNumberofServerInstances" /> is less than -1 or greater than 254 (-1 indicates <see cref="F:System.IO.Pipes.NamedPipeServerStream.MaxAllowedServerInstances" />)-or-
		///         <paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-
		///         <paramref name="inBufferSize" /> is negative.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> value.-or-
		///         <paramref name="additionalAccessRights" /> is not a valid <see cref="T:System.IO.Pipes.PipeAccessRights" /> value.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="pipeName" /> contains a colon (":").</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system is Windows Millennium Edition, Windows 98, or Windows 95, which are not supported.</exception>
		/// <exception cref="T:System.IO.IOException">The maximum number of server instances has been exceeded.</exception>
		// Token: 0x0600196A RID: 6506 RVA: 0x00055508 File Offset: 0x00053708
		public NamedPipeServerStream(string pipeName, PipeDirection direction, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeOptions options, int inBufferSize, int outBufferSize, PipeSecurity pipeSecurity, HandleInheritability inheritability, PipeAccessRights additionalAccessRights) : base(direction, transmissionMode, outBufferSize)
		{
			if (pipeName == null)
			{
				throw new ArgumentNullException("pipeName");
			}
			if (pipeName.Length == 0)
			{
				throw new ArgumentException("pipeName cannot be an empty string.");
			}
			if ((options & (PipeOptions)1073741823) != PipeOptions.None)
			{
				throw new ArgumentOutOfRangeException("options", "options contains an invalid flag.");
			}
			if (inBufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("inBufferSize", "Non negative number is required.");
			}
			if ((maxNumberOfServerInstances < 1 || maxNumberOfServerInstances > 254) && maxNumberOfServerInstances != -1)
			{
				throw new ArgumentOutOfRangeException("maxNumberOfServerInstances", "maxNumberOfServerInstances must either be a value between 1 and 254, or NamedPipeServerStream.MaxAllowedServerInstances (to obtain the maximum number allowed by system resources).");
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", "HandleInheritability.None or HandleInheritability.Inheritable required.");
			}
			if ((additionalAccessRights & ~(PipeAccessRights.ChangePermissions | PipeAccessRights.TakeOwnership | PipeAccessRights.AccessSystemSecurity)) != (PipeAccessRights)0)
			{
				throw new ArgumentOutOfRangeException("additionalAccessRights", "additionalAccessRights is limited to the PipeAccessRights.ChangePermissions, PipeAccessRights.TakeOwnership, and PipeAccessRights.AccessSystemSecurity flags when creating NamedPipeServerStreams.");
			}
			this.Create(pipeName, direction, maxNumberOfServerInstances, transmissionMode, options, inBufferSize, outBufferSize, pipeSecurity, inheritability, additionalAccessRights);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000555DB File Offset: 0x000537DB
		// Note: this type is marked as 'beforefieldinit'.
		static NamedPipeServerStream()
		{
		}

		// Token: 0x04000C1D RID: 3101
		private static RuntimeHelpers.TryCode tryCode = new RuntimeHelpers.TryCode(NamedPipeServerStream.ImpersonateAndTryCode);

		// Token: 0x04000C1E RID: 3102
		private static RuntimeHelpers.CleanupCode cleanupCode = new RuntimeHelpers.CleanupCode(NamedPipeServerStream.RevertImpersonationOnBackout);

		/// <summary>Represents the maximum number of server instances that the system resources allow.</summary>
		// Token: 0x04000C1F RID: 3103
		public const int MaxAllowedServerInstances = -1;

		// Token: 0x02000344 RID: 836
		internal class ExecuteHelper
		{
			// Token: 0x0600196C RID: 6508 RVA: 0x000555FF File Offset: 0x000537FF
			internal ExecuteHelper(PipeStreamImpersonationWorker userCode, SafePipeHandle handle)
			{
				this._userCode = userCode;
				this._handle = handle;
			}

			// Token: 0x04000C20 RID: 3104
			internal PipeStreamImpersonationWorker _userCode;

			// Token: 0x04000C21 RID: 3105
			internal SafePipeHandle _handle;

			// Token: 0x04000C22 RID: 3106
			internal bool _mustRevert;

			// Token: 0x04000C23 RID: 3107
			internal int _impersonateErrorCode;

			// Token: 0x04000C24 RID: 3108
			internal int _revertImpersonateErrorCode;
		}

		// Token: 0x02000345 RID: 837
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600196D RID: 6509 RVA: 0x00055615 File Offset: 0x00053815
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600196E RID: 6510 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x0600196F RID: 6511 RVA: 0x00055621 File Offset: 0x00053821
			internal void <WaitForConnectionAsync>b__3_0(object s)
			{
				((NamedPipeServerStream)s).WaitForConnection();
			}

			// Token: 0x04000C25 RID: 3109
			public static readonly NamedPipeServerStream.<>c <>9 = new NamedPipeServerStream.<>c();

			// Token: 0x04000C26 RID: 3110
			public static Action<object> <>9__3_0;
		}
	}
}
