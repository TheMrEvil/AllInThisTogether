using System;

namespace Mono.Net
{
	// Token: 0x02000080 RID: 128
	internal class CFProxy
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00005684 File Offset: 0x00003884
		static CFProxy()
		{
			IntPtr handle = CFObject.dlopen("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", 0);
			CFProxy.kCFProxyAutoConfigurationJavaScriptKey = CFObject.GetCFObjectHandle(handle, "kCFProxyAutoConfigurationJavaScriptKey");
			CFProxy.kCFProxyAutoConfigurationURLKey = CFObject.GetCFObjectHandle(handle, "kCFProxyAutoConfigurationURLKey");
			CFProxy.kCFProxyHostNameKey = CFObject.GetCFObjectHandle(handle, "kCFProxyHostNameKey");
			CFProxy.kCFProxyPasswordKey = CFObject.GetCFObjectHandle(handle, "kCFProxyPasswordKey");
			CFProxy.kCFProxyPortNumberKey = CFObject.GetCFObjectHandle(handle, "kCFProxyPortNumberKey");
			CFProxy.kCFProxyTypeKey = CFObject.GetCFObjectHandle(handle, "kCFProxyTypeKey");
			CFProxy.kCFProxyUsernameKey = CFObject.GetCFObjectHandle(handle, "kCFProxyUsernameKey");
			CFProxy.kCFProxyTypeAutoConfigurationURL = CFObject.GetCFObjectHandle(handle, "kCFProxyTypeAutoConfigurationURL");
			CFProxy.kCFProxyTypeAutoConfigurationJavaScript = CFObject.GetCFObjectHandle(handle, "kCFProxyTypeAutoConfigurationJavaScript");
			CFProxy.kCFProxyTypeFTP = CFObject.GetCFObjectHandle(handle, "kCFProxyTypeFTP");
			CFProxy.kCFProxyTypeHTTP = CFObject.GetCFObjectHandle(handle, "kCFProxyTypeHTTP");
			CFProxy.kCFProxyTypeHTTPS = CFObject.GetCFObjectHandle(handle, "kCFProxyTypeHTTPS");
			CFProxy.kCFProxyTypeSOCKS = CFObject.GetCFObjectHandle(handle, "kCFProxyTypeSOCKS");
			CFObject.dlclose(handle);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005771 File Offset: 0x00003971
		internal CFProxy(CFDictionary settings)
		{
			this.settings = settings;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005780 File Offset: 0x00003980
		private static CFProxyType CFProxyTypeToEnum(IntPtr type)
		{
			if (type == CFProxy.kCFProxyTypeAutoConfigurationJavaScript)
			{
				return CFProxyType.AutoConfigurationJavaScript;
			}
			if (type == CFProxy.kCFProxyTypeAutoConfigurationURL)
			{
				return CFProxyType.AutoConfigurationUrl;
			}
			if (type == CFProxy.kCFProxyTypeFTP)
			{
				return CFProxyType.FTP;
			}
			if (type == CFProxy.kCFProxyTypeHTTP)
			{
				return CFProxyType.HTTP;
			}
			if (type == CFProxy.kCFProxyTypeHTTPS)
			{
				return CFProxyType.HTTPS;
			}
			if (type == CFProxy.kCFProxyTypeSOCKS)
			{
				return CFProxyType.SOCKS;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeAutoConfigurationJavaScript, 0) == 0)
			{
				return CFProxyType.AutoConfigurationJavaScript;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeAutoConfigurationURL, 0) == 0)
			{
				return CFProxyType.AutoConfigurationUrl;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeFTP, 0) == 0)
			{
				return CFProxyType.FTP;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeHTTP, 0) == 0)
			{
				return CFProxyType.HTTP;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeHTTPS, 0) == 0)
			{
				return CFProxyType.HTTPS;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeSOCKS, 0) == 0)
			{
				return CFProxyType.SOCKS;
			}
			return CFProxyType.None;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00005848 File Offset: 0x00003A48
		public IntPtr AutoConfigurationJavaScript
		{
			get
			{
				if (CFProxy.kCFProxyAutoConfigurationJavaScriptKey == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return this.settings[CFProxy.kCFProxyAutoConfigurationJavaScriptKey];
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00005871 File Offset: 0x00003A71
		public IntPtr AutoConfigurationUrl
		{
			get
			{
				if (CFProxy.kCFProxyAutoConfigurationURLKey == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return this.settings[CFProxy.kCFProxyAutoConfigurationURLKey];
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000589A File Offset: 0x00003A9A
		public string HostName
		{
			get
			{
				if (CFProxy.kCFProxyHostNameKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyHostNameKey]);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000058C4 File Offset: 0x00003AC4
		public string Password
		{
			get
			{
				if (CFProxy.kCFProxyPasswordKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyPasswordKey]);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000058EE File Offset: 0x00003AEE
		public int Port
		{
			get
			{
				if (CFProxy.kCFProxyPortNumberKey == IntPtr.Zero)
				{
					return 0;
				}
				return CFNumber.AsInt32(this.settings[CFProxy.kCFProxyPortNumberKey]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005918 File Offset: 0x00003B18
		public CFProxyType ProxyType
		{
			get
			{
				if (CFProxy.kCFProxyTypeKey == IntPtr.Zero)
				{
					return CFProxyType.None;
				}
				return CFProxy.CFProxyTypeToEnum(this.settings[CFProxy.kCFProxyTypeKey]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00005942 File Offset: 0x00003B42
		public string Username
		{
			get
			{
				if (CFProxy.kCFProxyUsernameKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyUsernameKey]);
			}
		}

		// Token: 0x040001D9 RID: 473
		private static IntPtr kCFProxyAutoConfigurationJavaScriptKey;

		// Token: 0x040001DA RID: 474
		private static IntPtr kCFProxyAutoConfigurationURLKey;

		// Token: 0x040001DB RID: 475
		private static IntPtr kCFProxyHostNameKey;

		// Token: 0x040001DC RID: 476
		private static IntPtr kCFProxyPasswordKey;

		// Token: 0x040001DD RID: 477
		private static IntPtr kCFProxyPortNumberKey;

		// Token: 0x040001DE RID: 478
		private static IntPtr kCFProxyTypeKey;

		// Token: 0x040001DF RID: 479
		private static IntPtr kCFProxyUsernameKey;

		// Token: 0x040001E0 RID: 480
		private static IntPtr kCFProxyTypeAutoConfigurationURL;

		// Token: 0x040001E1 RID: 481
		private static IntPtr kCFProxyTypeAutoConfigurationJavaScript;

		// Token: 0x040001E2 RID: 482
		private static IntPtr kCFProxyTypeFTP;

		// Token: 0x040001E3 RID: 483
		private static IntPtr kCFProxyTypeHTTP;

		// Token: 0x040001E4 RID: 484
		private static IntPtr kCFProxyTypeHTTPS;

		// Token: 0x040001E5 RID: 485
		private static IntPtr kCFProxyTypeSOCKS;

		// Token: 0x040001E6 RID: 486
		private CFDictionary settings;
	}
}
