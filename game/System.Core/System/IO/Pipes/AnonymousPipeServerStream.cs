using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes a stream around an anonymous pipe, which supports both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x0200033E RID: 830
	public sealed class AnonymousPipeServerStream : PipeStream
	{
		// Token: 0x06001924 RID: 6436 RVA: 0x00054483 File Offset: 0x00052683
		private void Create(PipeDirection direction, HandleInheritability inheritability, int bufferSize)
		{
			this.Create(direction, inheritability, bufferSize, null);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00054490 File Offset: 0x00052690
		private void Create(PipeDirection direction, HandleInheritability inheritability, int bufferSize, PipeSecurity pipeSecurity)
		{
			GCHandle gchandle = default(GCHandle);
			SafePipeHandle safePipeHandle;
			bool flag;
			try
			{
				Interop.Kernel32.SECURITY_ATTRIBUTES secAttrs = PipeStream.GetSecAttrs(inheritability, pipeSecurity, ref gchandle);
				if (direction == PipeDirection.In)
				{
					flag = Interop.Kernel32.CreatePipe(out safePipeHandle, out this._clientHandle, ref secAttrs, bufferSize);
				}
				else
				{
					flag = Interop.Kernel32.CreatePipe(out this._clientHandle, out safePipeHandle, ref secAttrs, bufferSize);
				}
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			if (!flag)
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
			SafePipeHandle handle;
			flag = Interop.Kernel32.DuplicateHandle(Interop.Kernel32.GetCurrentProcess(), safePipeHandle, Interop.Kernel32.GetCurrentProcess(), out handle, 0U, false, 2U);
			if (!flag)
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
			safePipeHandle.Dispose();
			base.InitializeHandle(handle, false, false);
			base.State = PipeState.Connected;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class.</summary>
		// Token: 0x06001926 RID: 6438 RVA: 0x00054544 File Offset: 0x00052744
		public AnonymousPipeServerStream() : this(PipeDirection.Out, HandleInheritability.None, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x06001927 RID: 6439 RVA: 0x0005454F File Offset: 0x0005274F
		public AnonymousPipeServerStream(PipeDirection direction) : this(direction, HandleInheritability.None, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction and inheritability mode.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle can be inherited by child processes. Must be set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="inheritability" /> is not set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x06001928 RID: 6440 RVA: 0x0005455A File Offset: 0x0005275A
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability) : this(direction, inheritability, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class from the specified pipe handles.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="serverSafePipeHandle">A safe handle for the pipe that this <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object will encapsulate.</param>
		/// <param name="clientSafePipeHandle">A safe handle for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="serverSafePipeHandle" /> or <paramref name="clientSafePipeHandle" /> is an invalid handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="serverSafePipeHandle" /> or <paramref name="clientSafePipeHandle" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, has occurred.-or-The stream has been closed.</exception>
		// Token: 0x06001929 RID: 6441 RVA: 0x00054568 File Offset: 0x00052768
		public AnonymousPipeServerStream(PipeDirection direction, SafePipeHandle serverSafePipeHandle, SafePipeHandle clientSafePipeHandle) : base(direction, 0)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipes can only be in one direction.");
			}
			if (serverSafePipeHandle == null)
			{
				throw new ArgumentNullException("serverSafePipeHandle");
			}
			if (clientSafePipeHandle == null)
			{
				throw new ArgumentNullException("clientSafePipeHandle");
			}
			if (serverSafePipeHandle.IsInvalid)
			{
				throw new ArgumentException("Invalid handle.", "serverSafePipeHandle");
			}
			if (clientSafePipeHandle.IsInvalid)
			{
				throw new ArgumentException("Invalid handle.", "clientSafePipeHandle");
			}
			base.ValidateHandleIsPipe(serverSafePipeHandle);
			base.ValidateHandleIsPipe(clientSafePipeHandle);
			base.InitializeHandle(serverSafePipeHandle, true, false);
			this._clientHandle = clientSafePipeHandle;
			this._clientHandleExposed = true;
			base.State = PipeState.Connected;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction, inheritability mode, and buffer size.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle can be inherited by child processes. Must be set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.</param>
		/// <param name="bufferSize">The size of the buffer. This value must be greater than or equal to 0. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="inheritability" /> is not set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.-or-
		///         <paramref name="bufferSize" /> is less than 0.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x0600192A RID: 6442 RVA: 0x00054604 File Offset: 0x00052804
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize) : base(direction, bufferSize)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipes can only be in one direction.");
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", "HandleInheritability.None or HandleInheritability.Inheritable required.");
			}
			this.Create(direction, inheritability, bufferSize);
		}

		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> instance is reclaimed by garbage collection.</summary>
		// Token: 0x0600192B RID: 6443 RVA: 0x00054640 File Offset: 0x00052840
		~AnonymousPipeServerStream()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the connected <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object's handle as a string.</summary>
		/// <returns>A string that represents the connected <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object's handle.</returns>
		// Token: 0x0600192C RID: 6444 RVA: 0x00054670 File Offset: 0x00052870
		public string GetClientHandleAsString()
		{
			this._clientHandleExposed = true;
			GC.SuppressFinalize(this._clientHandle);
			return this._clientHandle.DangerousGetHandle().ToString();
		}

		/// <summary>Gets the safe handle for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object that is currently connected to the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object.</summary>
		/// <returns>A handle for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object that is currently connected to the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object.</returns>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x000546A2 File Offset: 0x000528A2
		public SafePipeHandle ClientSafePipeHandle
		{
			get
			{
				this._clientHandleExposed = true;
				return this._clientHandle;
			}
		}

		/// <summary>Closes the local copy of the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object's handle.</summary>
		// Token: 0x0600192E RID: 6446 RVA: 0x000546B1 File Offset: 0x000528B1
		public void DisposeLocalCopyOfClientHandle()
		{
			if (this._clientHandle != null && !this._clientHandle.IsClosed)
			{
				this._clientHandle.Dispose();
			}
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x000546D4 File Offset: 0x000528D4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._clientHandleExposed && this._clientHandle != null && !this._clientHandle.IsClosed)
				{
					this._clientHandle.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Gets the pipe transmission mode that is supported by the current pipe.</summary>
		/// <returns>The <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> that is supported by the current pipe.</returns>
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x000023D1 File Offset: 0x000005D1
		public override PipeTransmissionMode TransmissionMode
		{
			get
			{
				return PipeTransmissionMode.Byte;
			}
		}

		/// <summary>Sets the reading mode for the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object. For anonymous pipes, transmission mode must be <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" />.</summary>
		/// <returns>The reading mode for the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The transmission mode is not valid. For anonymous pipes, only <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" /> is supported. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set to <see cref="F:System.IO.Pipes.PipeTransmissionMode.Message" />, which is not supported for anonymous pipes.</exception>
		/// <exception cref="T:System.IO.IOException">The connection is broken or another I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x17000456 RID: 1110
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x00054454 File Offset: 0x00052654
		public override PipeTransmissionMode ReadMode
		{
			set
			{
				this.CheckPipePropertyOperations();
				if (value < PipeTransmissionMode.Byte || value > PipeTransmissionMode.Message)
				{
					throw new ArgumentOutOfRangeException("value", "For named pipes, transmission mode can be TransmissionMode.Byte or PipeTransmissionMode.Message. For anonymous pipes, transmission mode can be TransmissionMode.Byte.");
				}
				if (value == PipeTransmissionMode.Message)
				{
					throw new NotSupportedException("Anonymous pipes do not support PipeTransmissionMode.Message ReadMode.");
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction, inheritability mode, buffer size, and pipe security.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="inheritability">One of the enumeration values that determines whether the underlying handle can be inherited by child processes.</param>
		/// <param name="bufferSize">The size of the buffer. This value must be greater than or equal to 0. </param>
		/// <param name="pipeSecurity">An object that determines the access control and audit security for the pipe.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="inheritability" /> is not set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.-or-
		///         <paramref name="bufferSize" /> is less than 0.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x06001932 RID: 6450 RVA: 0x00054724 File Offset: 0x00052924
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize, PipeSecurity pipeSecurity) : base(direction, bufferSize)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipes can only be in one direction.");
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability", "HandleInheritability.None or HandleInheritability.Inheritable required.");
			}
			this.Create(direction, inheritability, bufferSize, pipeSecurity);
		}

		// Token: 0x04000C0F RID: 3087
		private SafePipeHandle _clientHandle;

		// Token: 0x04000C10 RID: 3088
		private bool _clientHandleExposed;
	}
}
