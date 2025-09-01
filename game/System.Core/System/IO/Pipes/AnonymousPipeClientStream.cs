using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes the client side of an anonymous pipe stream, which supports both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x0200033D RID: 829
	public sealed class AnonymousPipeClientStream : PipeStream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> class with the specified string representation of the pipe handle.</summary>
		/// <param name="pipeHandleAsString">A string that represents the pipe handle.</param>
		/// <exception cref="T:System.IO.IOException">
		///         <paramref name="pipeHandleAsString" /> is not a valid pipe handle.</exception>
		// Token: 0x0600191D RID: 6429 RVA: 0x00054331 File Offset: 0x00052531
		public AnonymousPipeClientStream(string pipeHandleAsString) : this(PipeDirection.In, pipeHandleAsString)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> class with the specified pipe direction and a string representation of the pipe handle.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="pipeHandleAsString">A string that represents the pipe handle.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="pipeHandleAsString" /> is an invalid handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="pipeHandleAsString" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x0600191E RID: 6430 RVA: 0x0005433C File Offset: 0x0005253C
		public AnonymousPipeClientStream(PipeDirection direction, string pipeHandleAsString) : base(direction, 0)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipes can only be in one direction.");
			}
			if (pipeHandleAsString == null)
			{
				throw new ArgumentNullException("pipeHandleAsString");
			}
			long value = 0L;
			if (!long.TryParse(pipeHandleAsString, out value))
			{
				throw new ArgumentException("Invalid handle.", "pipeHandleAsString");
			}
			SafePipeHandle safePipeHandle = new SafePipeHandle((IntPtr)value, true);
			if (safePipeHandle.IsInvalid)
			{
				throw new ArgumentException("Invalid handle.", "pipeHandleAsString");
			}
			this.Init(direction, safePipeHandle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> class from the specified handle.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="safePipeHandle">A safe handle for the pipe that this <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object will encapsulate.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="safePipeHandle " />is not a valid handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="safePipeHandle" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, has occurred.-or-The stream has been closed.</exception>
		// Token: 0x0600191F RID: 6431 RVA: 0x000543B8 File Offset: 0x000525B8
		public AnonymousPipeClientStream(PipeDirection direction, SafePipeHandle safePipeHandle) : base(direction, 0)
		{
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipes can only be in one direction.");
			}
			if (safePipeHandle == null)
			{
				throw new ArgumentNullException("safePipeHandle");
			}
			if (safePipeHandle.IsInvalid)
			{
				throw new ArgumentException("Invalid handle.", "safePipeHandle");
			}
			this.Init(direction, safePipeHandle);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0005440A File Offset: 0x0005260A
		private void Init(PipeDirection direction, SafePipeHandle safePipeHandle)
		{
			base.ValidateHandleIsPipe(safePipeHandle);
			base.InitializeHandle(safePipeHandle, true, false);
			base.State = PipeState.Connected;
		}

		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> instance is reclaimed by garbage collection.</summary>
		// Token: 0x06001921 RID: 6433 RVA: 0x00054424 File Offset: 0x00052624
		~AnonymousPipeClientStream()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the pipe transmission mode supported by the current pipe.</summary>
		/// <returns>The <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> supported by the current pipe.</returns>
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x000023D1 File Offset: 0x000005D1
		public override PipeTransmissionMode TransmissionMode
		{
			get
			{
				return PipeTransmissionMode.Byte;
			}
		}

		/// <summary>Sets the reading mode for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object.</summary>
		/// <returns>The <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The transmission mode is not valid. For anonymous pipes, only <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" /> is supported.</exception>
		/// <exception cref="T:System.NotSupportedException">The transmission mode is <see cref="F:System.IO.Pipes.PipeTransmissionMode.Message" />.</exception>
		/// <exception cref="T:System.IO.IOException">The connection is broken or another I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x17000453 RID: 1107
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x00054454 File Offset: 0x00052654
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
	}
}
