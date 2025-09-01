using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes a <see cref="T:System.IO.Stream" /> around a named pipe, which supports both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x02000341 RID: 833
	public sealed class NamedPipeClientStream : PipeStream
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x000547DC File Offset: 0x000529DC
		private bool TryConnect(int timeout, CancellationToken cancellationToken)
		{
			Interop.Kernel32.SECURITY_ATTRIBUTES secAttrs = PipeStream.GetSecAttrs(this._inheritability);
			int num = (int)(this._pipeOptions & ~PipeOptions.CurrentUserOnly);
			if (this._impersonationLevel != TokenImpersonationLevel.None)
			{
				num |= 1048576;
				num |= this._impersonationLevel - TokenImpersonationLevel.Anonymous << 16;
			}
			int num2 = this._access;
			if ((PipeDirection.In & this._direction) != (PipeDirection)0)
			{
				num2 |= int.MinValue;
			}
			if ((PipeDirection.Out & this._direction) != (PipeDirection)0)
			{
				num2 |= 1073741824;
			}
			SafePipeHandle safePipeHandle = Interop.Kernel32.CreateNamedPipeClient(this._normalizedPipePath, num2, FileShare.None, ref secAttrs, FileMode.Open, num, IntPtr.Zero);
			if (safePipeHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 231 && lastWin32Error != 2)
				{
					throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
				}
				if (!Interop.Kernel32.WaitNamedPipe(this._normalizedPipePath, timeout))
				{
					lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 2 || lastWin32Error == 121)
					{
						return false;
					}
					throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
				}
				else
				{
					safePipeHandle = Interop.Kernel32.CreateNamedPipeClient(this._normalizedPipePath, num2, FileShare.None, ref secAttrs, FileMode.Open, num, IntPtr.Zero);
					if (safePipeHandle.IsInvalid)
					{
						lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 231)
						{
							return false;
						}
						throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
					}
				}
			}
			base.InitializeHandle(safePipeHandle, false, (this._pipeOptions & PipeOptions.Asynchronous) > PipeOptions.None);
			base.State = PipeState.Connected;
			this.ValidateRemotePipeUser();
			return true;
		}

		/// <summary>Gets the number of server instances that share the same pipe name.</summary>
		/// <returns>The number of server instances that share the same pipe name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe handle has not been set.-or-The current <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> object has not yet connected to a <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> object.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or an I/O error occurred.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying pipe handle is closed.</exception>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00054924 File Offset: 0x00052B24
		public int NumberOfServerInstances
		{
			get
			{
				this.CheckPipePropertyOperations();
				int result;
				if (!Interop.Kernel32.GetNamedPipeHandleState(base.InternalHandle, IntPtr.Zero, out result, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0))
				{
					throw base.WinIOError(Marshal.GetLastWin32Error());
				}
				return result;
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00054968 File Offset: 0x00052B68
		private void ValidateRemotePipeUser()
		{
			if (!base.IsCurrentUserOnly)
			{
				return;
			}
			IdentityReference owner = base.GetAccessControl().GetOwner(typeof(SecurityIdentifier));
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				SecurityIdentifier owner2 = current.Owner;
				if (owner != owner2)
				{
					base.State = PipeState.Closed;
					throw new UnauthorizedAccessException("Could not connect to the pipe because it was not owned by the current user.");
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe name.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".</exception>
		// Token: 0x0600193B RID: 6459 RVA: 0x000549D8 File Offset: 0x00052BD8
		public NamedPipeClientStream(string pipeName) : this(".", pipeName, PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".</exception>
		// Token: 0x0600193C RID: 6460 RVA: 0x000549EA File Offset: 0x00052BEA
		public NamedPipeClientStream(string serverName, string pipeName) : this(serverName, pipeName, PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.</exception>
		// Token: 0x0600193D RID: 6461 RVA: 0x000549F8 File Offset: 0x00052BF8
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction) : this(serverName, pipeName, direction, PipeOptions.None, TokenImpersonationLevel.None, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction and pipe options.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-
		///         <paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.</exception>
		// Token: 0x0600193E RID: 6462 RVA: 0x00054A06 File Offset: 0x00052C06
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options) : this(serverName, pipeName, direction, options, TokenImpersonationLevel.None, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction, pipe options, and security impersonation level.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="impersonationLevel">One of the enumeration values that determines the security impersonation level.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-
		///         <paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-
		///         <paramref name="impersonationLevel" /> is not a valid <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.</exception>
		// Token: 0x0600193F RID: 6463 RVA: 0x00054A15 File Offset: 0x00052C15
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel) : this(serverName, pipeName, direction, options, impersonationLevel, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction, pipe options, security impersonation level, and inheritability mode.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="impersonationLevel">One of the enumeration values that determines the security impersonation level.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle will be inheritable by child processes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-
		///         <paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-
		///         <paramref name="impersonationLevel" /> is not a valid <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> value.</exception>
		// Token: 0x06001940 RID: 6464 RVA: 0x00054A28 File Offset: 0x00052C28
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability) : base(direction, 0)
		{
			if (pipeName == null)
			{
				throw new ArgumentNullException("pipeName");
			}
			if (serverName == null)
			{
				throw new ArgumentNullException("serverName", "serverName cannot be null. Use \\\".\\\" for current machine.");
			}
			if (pipeName.Length == 0)
			{
				throw new ArgumentException("pipeName cannot be an empty string.");
			}
			if (serverName.Length == 0)
			{
				throw new ArgumentException("serverName cannot be an empty string.  Use \\\\\\\".\\\\\\\" for current machine.");
			}
			if ((options & (PipeOptions)536870911) != PipeOptions.None)
			{
				throw new ArgumentOutOfRangeException("options", "options contains an invalid flag.");
			}
			if (impersonationLevel < TokenImpersonationLevel.None || impersonationLevel > TokenImpersonationLevel.Delegation)
			{
				throw new ArgumentOutOfRangeException("impersonationLevel", "TokenImpersonationLevel.None, TokenImpersonationLevel.Anonymous, TokenImpersonationLevel.Identification, TokenImpersonationLevel.Impersonation or TokenImpersonationLevel.Delegation required.");
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", "HandleInheritability.None or HandleInheritability.Inheritable required.");
			}
			if ((options & PipeOptions.CurrentUserOnly) != PipeOptions.None)
			{
				base.IsCurrentUserOnly = true;
			}
			this._normalizedPipePath = PipeStream.GetPipePath(serverName, pipeName);
			this._direction = direction;
			this._inheritability = inheritability;
			this._impersonationLevel = impersonationLevel;
			this._pipeOptions = options;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class for the specified pipe handle with the specified pipe direction.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.</param>
		/// <param name="isAsync">
		///       <see langword="true" /> to indicate that the handle was opened asynchronously; otherwise, <see langword="false" />.</param>
		/// <param name="isConnected">
		///       <see langword="true" /> to indicate that the pipe is connected; otherwise, <see langword="false" />.</param>
		/// <param name="safePipeHandle">A safe handle for the pipe that this <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> object will encapsulate.</param>
		/// <exception cref="T:System.IO.IOException">
		///         <paramref name="safePipeHandle" /> is not a valid handle.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="safePipeHandle" /> is not a valid handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="safePipeHandle" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.</exception>
		/// <exception cref="T:System.IO.IOException">The stream has been closed. </exception>
		// Token: 0x06001941 RID: 6465 RVA: 0x00054B10 File Offset: 0x00052D10
		public NamedPipeClientStream(PipeDirection direction, bool isAsync, bool isConnected, SafePipeHandle safePipeHandle) : base(direction, 0)
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

		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> instance is reclaimed by garbage collection.</summary>
		// Token: 0x06001942 RID: 6466 RVA: 0x00054B6C File Offset: 0x00052D6C
		~NamedPipeClientStream()
		{
			this.Dispose(false);
		}

		/// <summary>Connects to a waiting server with an infinite time-out value.</summary>
		/// <exception cref="T:System.InvalidOperationException">The client is already connected.</exception>
		// Token: 0x06001943 RID: 6467 RVA: 0x00054B9C File Offset: 0x00052D9C
		public void Connect()
		{
			this.Connect(-1);
		}

		/// <summary>Connects to a waiting server within the specified time-out period.</summary>
		/// <param name="timeout">The number of milliseconds to wait for the server to respond before the connection times out.</param>
		/// <exception cref="T:System.TimeoutException">Could not connect to the server within the specified <paramref name="timeout" /> period.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="timeout" /> is less than 0 and not set to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The client is already connected.</exception>
		/// <exception cref="T:System.IO.IOException">The server is connected to another client and the time-out period has expired.</exception>
		// Token: 0x06001944 RID: 6468 RVA: 0x00054BA5 File Offset: 0x00052DA5
		public void Connect(int timeout)
		{
			this.CheckConnectOperationsClient();
			if (timeout < 0 && timeout != -1)
			{
				throw new ArgumentOutOfRangeException("timeout", "Timeout must be non-negative or equal to -1 (Timeout.Infinite)");
			}
			this.ConnectInternal(timeout, CancellationToken.None, Environment.TickCount);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00054BD8 File Offset: 0x00052DD8
		private void ConnectInternal(int timeout, CancellationToken cancellationToken, int startTime)
		{
			int num = 0;
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int num2 = timeout - num;
				if (cancellationToken.CanBeCanceled && num2 > 50)
				{
					num2 = 50;
				}
				if (this.TryConnect(num2, cancellationToken))
				{
					break;
				}
				spinWait.SpinOnce();
				if (timeout != -1 && (num = Environment.TickCount - startTime) >= timeout)
				{
					goto Block_5;
				}
			}
			return;
			Block_5:
			throw new TimeoutException();
		}

		/// <summary>Asynchronously connects to a waiting server with an infinite timeout period.</summary>
		/// <returns>A task that represents the asynchronous connect operation.</returns>
		// Token: 0x06001946 RID: 6470 RVA: 0x00054C32 File Offset: 0x00052E32
		public Task ConnectAsync()
		{
			return this.ConnectAsync(-1, CancellationToken.None);
		}

		/// <summary>Asynchronously connects to a waiting server within the specified timeout period.</summary>
		/// <param name="timeout">The number of milliseconds to wait for the server to respond before the connection times out.</param>
		/// <returns>A task that represents the asynchronous connect operation.</returns>
		// Token: 0x06001947 RID: 6471 RVA: 0x00054C40 File Offset: 0x00052E40
		public Task ConnectAsync(int timeout)
		{
			return this.ConnectAsync(timeout, CancellationToken.None);
		}

		/// <summary>Asynchronously connects to a waiting server and monitors cancellation requests.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous connect operation.</returns>
		// Token: 0x06001948 RID: 6472 RVA: 0x00054C4E File Offset: 0x00052E4E
		public Task ConnectAsync(CancellationToken cancellationToken)
		{
			return this.ConnectAsync(-1, cancellationToken);
		}

		/// <summary>Asynchronously connects to a waiting server within the specified timeout period and monitors cancellation requests.</summary>
		/// <param name="timeout">The number of milliseconds to wait for the server to respond before the connection times out.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous connect operation.</returns>
		// Token: 0x06001949 RID: 6473 RVA: 0x00054C58 File Offset: 0x00052E58
		public Task ConnectAsync(int timeout, CancellationToken cancellationToken)
		{
			this.CheckConnectOperationsClient();
			if (timeout < 0 && timeout != -1)
			{
				throw new ArgumentOutOfRangeException("timeout", "Timeout must be non-negative or equal to -1 (Timeout.Infinite)");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			int startTime = Environment.TickCount;
			return Task.Run(delegate()
			{
				this.ConnectInternal(timeout, cancellationToken, startTime);
			}, cancellationToken);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00054CE3 File Offset: 0x00052EE3
		protected internal override void CheckPipePropertyOperations()
		{
			base.CheckPipePropertyOperations();
			if (base.State == PipeState.WaitingToConnect)
			{
				throw new InvalidOperationException("Pipe hasn't been connected yet.");
			}
			if (base.State == PipeState.Broken)
			{
				throw new IOException("Pipe is broken.");
			}
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00054D12 File Offset: 0x00052F12
		private void CheckConnectOperationsClient()
		{
			if (base.State == PipeState.Connected)
			{
				throw new InvalidOperationException("Already in a connected state.");
			}
			if (base.State == PipeState.Closed)
			{
				throw Error.GetPipeNotOpen();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe options, security impersonation level, and inheritability mode.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="desiredAccessRights">One of the enumeration values that specifies the desired access rights of the pipe.</param>
		/// <param name="options">One of the enumeration values that determines how to open or create the pipe.</param>
		/// <param name="impersonationLevel">One of the enumeration values that determines the security impersonation level.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle will be inheritable by child processes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeName" /> or <paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="pipeName" /> is set to "anonymous".-or-
		///         <paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-
		///         <paramref name="impersonationLevel" /> is not a valid <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.-or-
		///         <paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> value.</exception>
		// Token: 0x0600194C RID: 6476 RVA: 0x00054D37 File Offset: 0x00052F37
		public NamedPipeClientStream(string serverName, string pipeName, PipeAccessRights desiredAccessRights, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability) : this(serverName, pipeName, (PipeDirection)(desiredAccessRights & (PipeAccessRights.ReadData | PipeAccessRights.WriteData)), options, impersonationLevel, inheritability)
		{
			if ((desiredAccessRights & ~(PipeAccessRights.ReadData | PipeAccessRights.WriteData | PipeAccessRights.ReadAttributes | PipeAccessRights.WriteAttributes | PipeAccessRights.ReadExtendedAttributes | PipeAccessRights.WriteExtendedAttributes | PipeAccessRights.CreateNewInstance | PipeAccessRights.Delete | PipeAccessRights.ReadPermissions | PipeAccessRights.ChangePermissions | PipeAccessRights.TakeOwnership | PipeAccessRights.Synchronize | PipeAccessRights.AccessSystemSecurity)) != (PipeAccessRights)0)
			{
				throw new ArgumentOutOfRangeException("desiredAccessRights", "Invalid PipeAccessRights flag.");
			}
			this._access = (int)desiredAccessRights;
		}

		// Token: 0x04000C12 RID: 3090
		private const int CancellationCheckInterval = 50;

		// Token: 0x04000C13 RID: 3091
		private readonly string _normalizedPipePath;

		// Token: 0x04000C14 RID: 3092
		private readonly TokenImpersonationLevel _impersonationLevel;

		// Token: 0x04000C15 RID: 3093
		private readonly PipeOptions _pipeOptions;

		// Token: 0x04000C16 RID: 3094
		private readonly HandleInheritability _inheritability;

		// Token: 0x04000C17 RID: 3095
		private readonly PipeDirection _direction;

		// Token: 0x04000C18 RID: 3096
		private int _access;

		// Token: 0x02000342 RID: 834
		[CompilerGenerated]
		private sealed class <>c__DisplayClass24_0
		{
			// Token: 0x0600194D RID: 6477 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass24_0()
			{
			}

			// Token: 0x0600194E RID: 6478 RVA: 0x00054D6A File Offset: 0x00052F6A
			internal void <ConnectAsync>b__0()
			{
				this.<>4__this.ConnectInternal(this.timeout, this.cancellationToken, this.startTime);
			}

			// Token: 0x04000C19 RID: 3097
			public NamedPipeClientStream <>4__this;

			// Token: 0x04000C1A RID: 3098
			public int timeout;

			// Token: 0x04000C1B RID: 3099
			public CancellationToken cancellationToken;

			// Token: 0x04000C1C RID: 3100
			public int startTime;
		}
	}
}
