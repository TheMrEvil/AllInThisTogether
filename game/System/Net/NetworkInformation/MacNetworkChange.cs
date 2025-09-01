using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Util;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000711 RID: 1809
	internal sealed class MacNetworkChange : INetworkChange, IDisposable
	{
		// Token: 0x060039D9 RID: 14809
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlopen(string path, int mode);

		// Token: 0x060039DA RID: 14810
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern IntPtr dlsym(IntPtr handle, string symbol);

		// Token: 0x060039DB RID: 14811
		[DllImport("/usr/lib/libSystem.dylib")]
		private static extern int dlclose(IntPtr handle);

		// Token: 0x060039DC RID: 14812
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRelease(IntPtr handle);

		// Token: 0x060039DD RID: 14813
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFRunLoopGetMain();

		// Token: 0x060039DE RID: 14814
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern IntPtr SCNetworkReachabilityCreateWithAddress(IntPtr allocator, ref MacNetworkChange.sockaddr_in sockaddr);

		// Token: 0x060039DF RID: 14815
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityGetFlags(IntPtr reachability, out MacNetworkChange.NetworkReachabilityFlags flags);

		// Token: 0x060039E0 RID: 14816
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilitySetCallback(IntPtr reachability, MacNetworkChange.SCNetworkReachabilityCallback callback, ref MacNetworkChange.SCNetworkReachabilityContext context);

		// Token: 0x060039E1 RID: 14817
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityScheduleWithRunLoop(IntPtr reachability, IntPtr runLoop, IntPtr runLoopMode);

		// Token: 0x060039E2 RID: 14818
		[DllImport("/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration")]
		private static extern bool SCNetworkReachabilityUnscheduleFromRunLoop(IntPtr reachability, IntPtr runLoop, IntPtr runLoopMode);

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060039E3 RID: 14819 RVA: 0x000C90E8 File Offset: 0x000C72E8
		// (remove) Token: 0x060039E4 RID: 14820 RVA: 0x000C9120 File Offset: 0x000C7320
		private event NetworkAddressChangedEventHandler networkAddressChanged
		{
			[CompilerGenerated]
			add
			{
				NetworkAddressChangedEventHandler networkAddressChangedEventHandler = this.networkAddressChanged;
				NetworkAddressChangedEventHandler networkAddressChangedEventHandler2;
				do
				{
					networkAddressChangedEventHandler2 = networkAddressChangedEventHandler;
					NetworkAddressChangedEventHandler value2 = (NetworkAddressChangedEventHandler)Delegate.Combine(networkAddressChangedEventHandler2, value);
					networkAddressChangedEventHandler = Interlocked.CompareExchange<NetworkAddressChangedEventHandler>(ref this.networkAddressChanged, value2, networkAddressChangedEventHandler2);
				}
				while (networkAddressChangedEventHandler != networkAddressChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				NetworkAddressChangedEventHandler networkAddressChangedEventHandler = this.networkAddressChanged;
				NetworkAddressChangedEventHandler networkAddressChangedEventHandler2;
				do
				{
					networkAddressChangedEventHandler2 = networkAddressChangedEventHandler;
					NetworkAddressChangedEventHandler value2 = (NetworkAddressChangedEventHandler)Delegate.Remove(networkAddressChangedEventHandler2, value);
					networkAddressChangedEventHandler = Interlocked.CompareExchange<NetworkAddressChangedEventHandler>(ref this.networkAddressChanged, value2, networkAddressChangedEventHandler2);
				}
				while (networkAddressChangedEventHandler != networkAddressChangedEventHandler2);
			}
		}

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x060039E5 RID: 14821 RVA: 0x000C9158 File Offset: 0x000C7358
		// (remove) Token: 0x060039E6 RID: 14822 RVA: 0x000C9190 File Offset: 0x000C7390
		private event NetworkAvailabilityChangedEventHandler networkAvailabilityChanged
		{
			[CompilerGenerated]
			add
			{
				NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler = this.networkAvailabilityChanged;
				NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler2;
				do
				{
					networkAvailabilityChangedEventHandler2 = networkAvailabilityChangedEventHandler;
					NetworkAvailabilityChangedEventHandler value2 = (NetworkAvailabilityChangedEventHandler)Delegate.Combine(networkAvailabilityChangedEventHandler2, value);
					networkAvailabilityChangedEventHandler = Interlocked.CompareExchange<NetworkAvailabilityChangedEventHandler>(ref this.networkAvailabilityChanged, value2, networkAvailabilityChangedEventHandler2);
				}
				while (networkAvailabilityChangedEventHandler != networkAvailabilityChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler = this.networkAvailabilityChanged;
				NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler2;
				do
				{
					networkAvailabilityChangedEventHandler2 = networkAvailabilityChangedEventHandler;
					NetworkAvailabilityChangedEventHandler value2 = (NetworkAvailabilityChangedEventHandler)Delegate.Remove(networkAvailabilityChangedEventHandler2, value);
					networkAvailabilityChangedEventHandler = Interlocked.CompareExchange<NetworkAvailabilityChangedEventHandler>(ref this.networkAvailabilityChanged, value2, networkAvailabilityChangedEventHandler2);
				}
				while (networkAvailabilityChangedEventHandler != networkAvailabilityChangedEventHandler2);
			}
		}

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x060039E7 RID: 14823 RVA: 0x000C91C5 File Offset: 0x000C73C5
		// (remove) Token: 0x060039E8 RID: 14824 RVA: 0x000C91DA File Offset: 0x000C73DA
		public event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				value(null, EventArgs.Empty);
				this.networkAddressChanged += value;
			}
			remove
			{
				this.networkAddressChanged -= value;
			}
		}

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x060039E9 RID: 14825 RVA: 0x000C91E3 File Offset: 0x000C73E3
		// (remove) Token: 0x060039EA RID: 14826 RVA: 0x000C91FE File Offset: 0x000C73FE
		public event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				value(null, new NetworkAvailabilityEventArgs(this.IsAvailable));
				this.networkAvailabilityChanged += value;
			}
			remove
			{
				this.networkAvailabilityChanged -= value;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000C9207 File Offset: 0x000C7407
		private bool IsAvailable
		{
			get
			{
				return (this.flags & MacNetworkChange.NetworkReachabilityFlags.Reachable) != MacNetworkChange.NetworkReachabilityFlags.None && (this.flags & MacNetworkChange.NetworkReachabilityFlags.ConnectionRequired) == MacNetworkChange.NetworkReachabilityFlags.None;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000C9220 File Offset: 0x000C7420
		public bool HasRegisteredEvents
		{
			get
			{
				return this.networkAddressChanged != null || this.networkAvailabilityChanged != null;
			}
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000C9238 File Offset: 0x000C7438
		public MacNetworkChange()
		{
			MacNetworkChange.sockaddr_in sockaddr_in = MacNetworkChange.sockaddr_in.Create();
			this.handle = MacNetworkChange.SCNetworkReachabilityCreateWithAddress(IntPtr.Zero, ref sockaddr_in);
			if (this.handle == IntPtr.Zero)
			{
				throw new Exception("SCNetworkReachabilityCreateWithAddress returned NULL");
			}
			this.callback = new MacNetworkChange.SCNetworkReachabilityCallback(MacNetworkChange.HandleCallback);
			MacNetworkChange.SCNetworkReachabilityContext scnetworkReachabilityContext = new MacNetworkChange.SCNetworkReachabilityContext
			{
				info = GCHandle.ToIntPtr(GCHandle.Alloc(this))
			};
			MacNetworkChange.SCNetworkReachabilitySetCallback(this.handle, this.callback, ref scnetworkReachabilityContext);
			this.scheduledWithRunLoop = (this.LoadRunLoopMode() && MacNetworkChange.SCNetworkReachabilityScheduleWithRunLoop(this.handle, MacNetworkChange.CFRunLoopGetMain(), this.runLoopMode));
			MacNetworkChange.SCNetworkReachabilityGetFlags(this.handle, out this.flags);
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000C92FC File Offset: 0x000C74FC
		private bool LoadRunLoopMode()
		{
			IntPtr value = MacNetworkChange.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (value == IntPtr.Zero)
			{
				return false;
			}
			try
			{
				this.runLoopMode = MacNetworkChange.dlsym(value, "kCFRunLoopDefaultMode");
				if (this.runLoopMode != IntPtr.Zero)
				{
					this.runLoopMode = Marshal.ReadIntPtr(this.runLoopMode);
					return this.runLoopMode != IntPtr.Zero;
				}
			}
			finally
			{
				MacNetworkChange.dlclose(value);
			}
			return false;
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000C938C File Offset: 0x000C758C
		public void Dispose()
		{
			lock (this)
			{
				if (!(this.handle == IntPtr.Zero))
				{
					if (this.scheduledWithRunLoop)
					{
						MacNetworkChange.SCNetworkReachabilityUnscheduleFromRunLoop(this.handle, MacNetworkChange.CFRunLoopGetMain(), this.runLoopMode);
					}
					MacNetworkChange.CFRelease(this.handle);
					this.handle = IntPtr.Zero;
					this.callback = null;
					this.flags = MacNetworkChange.NetworkReachabilityFlags.None;
					this.scheduledWithRunLoop = false;
				}
			}
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x000C9420 File Offset: 0x000C7620
		[MonoPInvokeCallback(typeof(MacNetworkChange.SCNetworkReachabilityCallback))]
		private static void HandleCallback(IntPtr reachability, MacNetworkChange.NetworkReachabilityFlags flags, IntPtr info)
		{
			if (info == IntPtr.Zero)
			{
				return;
			}
			MacNetworkChange macNetworkChange = GCHandle.FromIntPtr(info).Target as MacNetworkChange;
			if (macNetworkChange == null || macNetworkChange.flags == flags)
			{
				return;
			}
			macNetworkChange.flags = flags;
			NetworkAddressChangedEventHandler networkAddressChangedEventHandler = macNetworkChange.networkAddressChanged;
			if (networkAddressChangedEventHandler != null)
			{
				networkAddressChangedEventHandler(null, EventArgs.Empty);
			}
			NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler = macNetworkChange.networkAvailabilityChanged;
			if (networkAvailabilityChangedEventHandler != null)
			{
				networkAvailabilityChangedEventHandler(null, new NetworkAvailabilityEventArgs(macNetworkChange.IsAvailable));
			}
		}

		// Token: 0x04002201 RID: 8705
		private const string DL_LIB = "/usr/lib/libSystem.dylib";

		// Token: 0x04002202 RID: 8706
		private const string CORE_SERVICES_LIB = "/System/Library/Frameworks/SystemConfiguration.framework/SystemConfiguration";

		// Token: 0x04002203 RID: 8707
		private const string CORE_FOUNDATION_LIB = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";

		// Token: 0x04002204 RID: 8708
		private IntPtr handle;

		// Token: 0x04002205 RID: 8709
		private IntPtr runLoopMode;

		// Token: 0x04002206 RID: 8710
		private MacNetworkChange.SCNetworkReachabilityCallback callback;

		// Token: 0x04002207 RID: 8711
		private bool scheduledWithRunLoop;

		// Token: 0x04002208 RID: 8712
		private MacNetworkChange.NetworkReachabilityFlags flags;

		// Token: 0x04002209 RID: 8713
		[CompilerGenerated]
		private NetworkAddressChangedEventHandler networkAddressChanged;

		// Token: 0x0400220A RID: 8714
		[CompilerGenerated]
		private NetworkAvailabilityChangedEventHandler networkAvailabilityChanged;

		// Token: 0x02000712 RID: 1810
		// (Invoke) Token: 0x060039F2 RID: 14834
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void SCNetworkReachabilityCallback(IntPtr target, MacNetworkChange.NetworkReachabilityFlags flags, IntPtr info);

		// Token: 0x02000713 RID: 1811
		[StructLayout(LayoutKind.Explicit, Size = 28)]
		private struct sockaddr_in
		{
			// Token: 0x060039F5 RID: 14837 RVA: 0x000C9498 File Offset: 0x000C7698
			public static MacNetworkChange.sockaddr_in Create()
			{
				return new MacNetworkChange.sockaddr_in
				{
					sin_len = 28,
					sin_family = 2
				};
			}

			// Token: 0x0400220B RID: 8715
			[FieldOffset(0)]
			public byte sin_len;

			// Token: 0x0400220C RID: 8716
			[FieldOffset(1)]
			public byte sin_family;
		}

		// Token: 0x02000714 RID: 1812
		private struct SCNetworkReachabilityContext
		{
			// Token: 0x0400220D RID: 8717
			public IntPtr version;

			// Token: 0x0400220E RID: 8718
			public IntPtr info;

			// Token: 0x0400220F RID: 8719
			public IntPtr retain;

			// Token: 0x04002210 RID: 8720
			public IntPtr release;

			// Token: 0x04002211 RID: 8721
			public IntPtr copyDescription;
		}

		// Token: 0x02000715 RID: 1813
		[Flags]
		private enum NetworkReachabilityFlags
		{
			// Token: 0x04002213 RID: 8723
			None = 0,
			// Token: 0x04002214 RID: 8724
			TransientConnection = 1,
			// Token: 0x04002215 RID: 8725
			Reachable = 2,
			// Token: 0x04002216 RID: 8726
			ConnectionRequired = 4,
			// Token: 0x04002217 RID: 8727
			ConnectionOnTraffic = 8,
			// Token: 0x04002218 RID: 8728
			InterventionRequired = 16,
			// Token: 0x04002219 RID: 8729
			ConnectionOnDemand = 32,
			// Token: 0x0400221A RID: 8730
			IsLocalAddress = 65536,
			// Token: 0x0400221B RID: 8731
			IsDirect = 131072,
			// Token: 0x0400221C RID: 8732
			IsWWAN = 262144,
			// Token: 0x0400221D RID: 8733
			ConnectionAutomatic = 8
		}
	}
}
