using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000716 RID: 1814
	internal sealed class LinuxNetworkChange : INetworkChange, IDisposable
	{
		// Token: 0x14000073 RID: 115
		// (add) Token: 0x060039F6 RID: 14838 RVA: 0x000C94BF File Offset: 0x000C76BF
		// (remove) Token: 0x060039F7 RID: 14839 RVA: 0x000C94C8 File Offset: 0x000C76C8
		public event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				this.Register(value);
			}
			remove
			{
				this.Unregister(value);
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060039F8 RID: 14840 RVA: 0x000C94D1 File Offset: 0x000C76D1
		// (remove) Token: 0x060039F9 RID: 14841 RVA: 0x000C94DA File Offset: 0x000C76DA
		public event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				this.Register(value);
			}
			remove
			{
				this.Unregister(value);
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060039FA RID: 14842 RVA: 0x000C94E3 File Offset: 0x000C76E3
		public bool HasRegisteredEvents
		{
			get
			{
				return this.AddressChanged != null || this.AvailabilityChanged != null;
			}
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose()
		{
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000C94F8 File Offset: 0x000C76F8
		private bool EnsureSocket()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this.nl_sock != null)
				{
					return true;
				}
				IntPtr preexistingHandle = LinuxNetworkChange.CreateNLSocket();
				if (preexistingHandle.ToInt64() == -1L)
				{
					return false;
				}
				SafeSocketHandle safe_handle = new SafeSocketHandle(preexistingHandle, true);
				this.nl_sock = new Socket(AddressFamily.Unspecified, SocketType.Raw, ProtocolType.Udp, safe_handle);
				this.nl_args = new SocketAsyncEventArgs();
				this.nl_args.SetBuffer(new byte[8192], 0, 8192);
				this.nl_args.Completed += this.OnDataAvailable;
				this.nl_sock.ReceiveAsync(this.nl_args);
			}
			return true;
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000C95C4 File Offset: 0x000C77C4
		private void MaybeCloseSocket()
		{
			if (this.nl_sock == null || this.AvailabilityChanged != null || this.AddressChanged != null)
			{
				return;
			}
			LinuxNetworkChange.CloseNLSocket(this.nl_sock.Handle);
			GC.SuppressFinalize(this.nl_sock);
			this.nl_sock = null;
			this.nl_args = null;
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000C9614 File Offset: 0x000C7814
		private bool GetAvailability()
		{
			foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback && networkInterface.OperationalStatus == OperationalStatus.Up)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000C9650 File Offset: 0x000C7850
		private void OnAvailabilityChanged(object unused)
		{
			NetworkAvailabilityChangedEventHandler availabilityChanged = this.AvailabilityChanged;
			if (availabilityChanged != null)
			{
				availabilityChanged(null, new NetworkAvailabilityEventArgs(this.GetAvailability()));
			}
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000C967C File Offset: 0x000C787C
		private void OnAddressChanged(object unused)
		{
			NetworkAddressChangedEventHandler addressChanged = this.AddressChanged;
			if (addressChanged != null)
			{
				addressChanged(null, EventArgs.Empty);
			}
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000C96A0 File Offset: 0x000C78A0
		private void OnEventDue(object unused)
		{
			object @lock = this._lock;
			LinuxNetworkChange.EventType eventType;
			lock (@lock)
			{
				eventType = this.pending_events;
				this.pending_events = (LinuxNetworkChange.EventType)0;
				this.timer.Change(-1, -1);
			}
			if ((eventType & LinuxNetworkChange.EventType.Availability) != (LinuxNetworkChange.EventType)0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnAvailabilityChanged));
			}
			if ((eventType & LinuxNetworkChange.EventType.Address) != (LinuxNetworkChange.EventType)0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.OnAddressChanged));
			}
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000C9724 File Offset: 0x000C7924
		private void QueueEvent(LinuxNetworkChange.EventType type)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this.timer == null)
				{
					this.timer = new Timer(new TimerCallback(this.OnEventDue));
				}
				if (this.pending_events == (LinuxNetworkChange.EventType)0)
				{
					this.timer.Change(150, -1);
				}
				this.pending_events |= type;
			}
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000C97A8 File Offset: 0x000C79A8
		private unsafe void OnDataAvailable(object sender, SocketAsyncEventArgs args)
		{
			if (this.nl_sock == null)
			{
				return;
			}
			byte[] array;
			byte* value;
			if ((array = args.Buffer) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			LinuxNetworkChange.EventType eventType = LinuxNetworkChange.ReadEvents(this.nl_sock.Handle, new IntPtr((void*)value), args.BytesTransferred, 8192);
			array = null;
			this.nl_sock.ReceiveAsync(this.nl_args);
			if (eventType != (LinuxNetworkChange.EventType)0)
			{
				this.QueueEvent(eventType);
			}
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000C981B File Offset: 0x000C7A1B
		private void Register(NetworkAddressChangedEventHandler d)
		{
			this.EnsureSocket();
			this.AddressChanged = (NetworkAddressChangedEventHandler)Delegate.Combine(this.AddressChanged, d);
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000C983B File Offset: 0x000C7A3B
		private void Register(NetworkAvailabilityChangedEventHandler d)
		{
			this.EnsureSocket();
			this.AvailabilityChanged = (NetworkAvailabilityChangedEventHandler)Delegate.Combine(this.AvailabilityChanged, d);
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000C985C File Offset: 0x000C7A5C
		private void Unregister(NetworkAddressChangedEventHandler d)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.AddressChanged = (NetworkAddressChangedEventHandler)Delegate.Remove(this.AddressChanged, d);
				this.MaybeCloseSocket();
			}
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x000C98B4 File Offset: 0x000C7AB4
		private void Unregister(NetworkAvailabilityChangedEventHandler d)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this.AvailabilityChanged = (NetworkAvailabilityChangedEventHandler)Delegate.Remove(this.AvailabilityChanged, d);
				this.MaybeCloseSocket();
			}
		}

		// Token: 0x06003A08 RID: 14856
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CreateNLSocket();

		// Token: 0x06003A09 RID: 14857
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern LinuxNetworkChange.EventType ReadEvents(IntPtr sock, IntPtr buffer, int count, int size);

		// Token: 0x06003A0A RID: 14858
		[DllImport("MonoPosixHelper", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CloseNLSocket(IntPtr sock);

		// Token: 0x06003A0B RID: 14859 RVA: 0x000C990C File Offset: 0x000C7B0C
		public LinuxNetworkChange()
		{
		}

		// Token: 0x0400221E RID: 8734
		private object _lock = new object();

		// Token: 0x0400221F RID: 8735
		private Socket nl_sock;

		// Token: 0x04002220 RID: 8736
		private SocketAsyncEventArgs nl_args;

		// Token: 0x04002221 RID: 8737
		private LinuxNetworkChange.EventType pending_events;

		// Token: 0x04002222 RID: 8738
		private Timer timer;

		// Token: 0x04002223 RID: 8739
		private NetworkAddressChangedEventHandler AddressChanged;

		// Token: 0x04002224 RID: 8740
		private NetworkAvailabilityChangedEventHandler AvailabilityChanged;

		// Token: 0x02000717 RID: 1815
		[Flags]
		private enum EventType
		{
			// Token: 0x04002226 RID: 8742
			Availability = 1,
			// Token: 0x04002227 RID: 8743
			Address = 2
		}
	}
}
