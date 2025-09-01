using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides a managed representation of a Win32 OVERLAPPED structure, including methods to transfer information from an <see cref="T:System.Threading.Overlapped" /> instance to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</summary>
	// Token: 0x020002F5 RID: 757
	[ComVisible(true)]
	public class Overlapped
	{
		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Threading.Overlapped" /> class.</summary>
		// Token: 0x060020F4 RID: 8436 RVA: 0x0000259F File Offset: 0x0000079F
		public Overlapped()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Overlapped" /> class with the specified file position, the 32-bit integer handle to an event that is signaled when the I/O operation is complete, and an interface through which to return the results of the operation.</summary>
		/// <param name="offsetLo">The low word of the file position at which to start the transfer.</param>
		/// <param name="offsetHi">The high word of the file position at which to start the transfer.</param>
		/// <param name="hEvent">The handle to an event that is signaled when the I/O operation is complete.</param>
		/// <param name="ar">An object that implements the <see cref="T:System.IAsyncResult" /> interface and provides status information on the I/O operation.</param>
		// Token: 0x060020F5 RID: 8437 RVA: 0x00076F91 File Offset: 0x00075191
		[Obsolete("Not 64bit compatible.  Please use the constructor that takes IntPtr for the event handle")]
		public Overlapped(int offsetLo, int offsetHi, int hEvent, IAsyncResult ar)
		{
			this.offsetL = offsetLo;
			this.offsetH = offsetHi;
			this.evt = hEvent;
			this.ares = ar;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Overlapped" /> class with the specified file position, the handle to an event that is signaled when the I/O operation is complete, and an interface through which to return the results of the operation.</summary>
		/// <param name="offsetLo">The low word of the file position at which to start the transfer.</param>
		/// <param name="offsetHi">The high word of the file position at which to start the transfer.</param>
		/// <param name="hEvent">The handle to an event that is signaled when the I/O operation is complete.</param>
		/// <param name="ar">An object that implements the <see cref="T:System.IAsyncResult" /> interface and provides status information on the I/O operation.</param>
		// Token: 0x060020F6 RID: 8438 RVA: 0x00076FB6 File Offset: 0x000751B6
		public Overlapped(int offsetLo, int offsetHi, IntPtr hEvent, IAsyncResult ar)
		{
			this.offsetL = offsetLo;
			this.offsetH = offsetHi;
			this.evt_ptr = hEvent;
			this.ares = ar;
		}

		/// <summary>Frees the unmanaged memory associated with a native overlapped structure allocated by the <see cref="Overload:System.Threading.Overlapped.Pack" /> method.</summary>
		/// <param name="nativeOverlappedPtr">A pointer to the <see cref="T:System.Threading.NativeOverlapped" /> structure to be freed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nativeOverlappedPtr" /> is <see langword="null" />.</exception>
		// Token: 0x060020F7 RID: 8439 RVA: 0x00076FDB File Offset: 0x000751DB
		[CLSCompliant(false)]
		public unsafe static void Free(NativeOverlapped* nativeOverlappedPtr)
		{
			if ((IntPtr)((void*)nativeOverlappedPtr) == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			Marshal.FreeHGlobal((IntPtr)((void*)nativeOverlappedPtr));
		}

		/// <summary>Unpacks the specified unmanaged <see cref="T:System.Threading.NativeOverlapped" /> structure into a managed <see cref="T:System.Threading.Overlapped" /> object.</summary>
		/// <param name="nativeOverlappedPtr">An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</param>
		/// <returns>An <see cref="T:System.Threading.Overlapped" /> object containing the information unpacked from the native structure.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nativeOverlappedPtr" /> is <see langword="null" />.</exception>
		// Token: 0x060020F8 RID: 8440 RVA: 0x00077008 File Offset: 0x00075208
		[CLSCompliant(false)]
		public unsafe static Overlapped Unpack(NativeOverlapped* nativeOverlappedPtr)
		{
			if ((IntPtr)((void*)nativeOverlappedPtr) == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			return new Overlapped
			{
				offsetL = nativeOverlappedPtr->OffsetLow,
				offsetH = nativeOverlappedPtr->OffsetHigh,
				evt = (int)nativeOverlappedPtr->EventHandle
			};
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying the delegate to be invoked when the asynchronous I/O operation is complete.</summary>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> has already been packed.</exception>
		// Token: 0x060020F9 RID: 8441 RVA: 0x00077060 File Offset: 0x00075260
		[MonoTODO("Security - we need to propagate the call stack")]
		[Obsolete("Use Pack(iocb, userData) instead")]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb)
		{
			NativeOverlapped* ptr = (NativeOverlapped*)((void*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped))));
			ptr->OffsetLow = this.offsetL;
			ptr->OffsetHigh = this.offsetH;
			ptr->EventHandle = (IntPtr)this.evt;
			return ptr;
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying a delegate that is invoked when the asynchronous I/O operation is complete and a managed object that serves as a buffer.</summary>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <param name="userData">An object or array of objects representing the input or output buffer for the operation. Each object represents a buffer, for example an array of bytes.</param>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> has already been packed.</exception>
		// Token: 0x060020FA RID: 8442 RVA: 0x000770B4 File Offset: 0x000752B4
		[ComVisible(false)]
		[CLSCompliant(false)]
		[MonoTODO("handle userData")]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			NativeOverlapped* ptr = (NativeOverlapped*)((void*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped))));
			ptr->OffsetLow = this.offsetL;
			ptr->OffsetHigh = this.offsetH;
			ptr->EventHandle = this.evt_ptr;
			return ptr;
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure specifying the delegate to invoke when the asynchronous I/O operation is complete. Does not propagate the calling stack.</summary>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> has already been packed.</exception>
		// Token: 0x060020FB RID: 8443 RVA: 0x00077100 File Offset: 0x00075300
		[Obsolete("Use UnsafePack(iocb, userData) instead")]
		[CLSCompliant(false)]
		[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb)
		{
			return this.Pack(iocb);
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying the delegate to invoke when the asynchronous I/O operation is complete and the managed object that serves as a buffer. Does not propagate the calling stack.</summary>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <param name="userData">An object or array of objects representing the input or output buffer for the operation. Each object represents a buffer, for example an array of bytes.</param>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> is already packed.</exception>
		// Token: 0x060020FC RID: 8444 RVA: 0x00077109 File Offset: 0x00075309
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			return this.Pack(iocb, userData);
		}

		/// <summary>Gets or sets the object that provides status information on the I/O operation.</summary>
		/// <returns>An object that implements the <see cref="T:System.IAsyncResult" /> interface.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x00077113 File Offset: 0x00075313
		// (set) Token: 0x060020FE RID: 8446 RVA: 0x0007711B File Offset: 0x0007531B
		public IAsyncResult AsyncResult
		{
			get
			{
				return this.ares;
			}
			set
			{
				this.ares = value;
			}
		}

		/// <summary>Gets or sets the 32-bit integer handle to a synchronization event that is signaled when the I/O operation is complete.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the handle of the synchronization event.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x00077124 File Offset: 0x00075324
		// (set) Token: 0x06002100 RID: 8448 RVA: 0x0007712C File Offset: 0x0007532C
		[Obsolete("Not 64bit compatible.  Use EventHandleIntPtr instead.")]
		public int EventHandle
		{
			get
			{
				return this.evt;
			}
			set
			{
				this.evt = value;
			}
		}

		/// <summary>Gets or sets the handle to the synchronization event that is signaled when the I/O operation is complete.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> representing the handle of the event.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x00077135 File Offset: 0x00075335
		// (set) Token: 0x06002102 RID: 8450 RVA: 0x0007713D File Offset: 0x0007533D
		[ComVisible(false)]
		public IntPtr EventHandleIntPtr
		{
			get
			{
				return this.evt_ptr;
			}
			set
			{
				this.evt_ptr = value;
			}
		}

		/// <summary>Gets or sets the high-order word of the file position at which to start the transfer. The file position is a byte offset from the start of the file.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the high word of the file position.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x00077146 File Offset: 0x00075346
		// (set) Token: 0x06002104 RID: 8452 RVA: 0x0007714E File Offset: 0x0007534E
		public int OffsetHigh
		{
			get
			{
				return this.offsetH;
			}
			set
			{
				this.offsetH = value;
			}
		}

		/// <summary>Gets or sets the low-order word of the file position at which to start the transfer. The file position is a byte offset from the start of the file.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the low word of the file position.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x00077157 File Offset: 0x00075357
		// (set) Token: 0x06002106 RID: 8454 RVA: 0x0007715F File Offset: 0x0007535F
		public int OffsetLow
		{
			get
			{
				return this.offsetL;
			}
			set
			{
				this.offsetL = value;
			}
		}

		// Token: 0x04001B72 RID: 7026
		private IAsyncResult ares;

		// Token: 0x04001B73 RID: 7027
		private int offsetL;

		// Token: 0x04001B74 RID: 7028
		private int offsetH;

		// Token: 0x04001B75 RID: 7029
		private int evt;

		// Token: 0x04001B76 RID: 7030
		private IntPtr evt_ptr;
	}
}
