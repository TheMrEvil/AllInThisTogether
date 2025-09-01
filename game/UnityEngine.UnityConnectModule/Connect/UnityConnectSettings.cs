using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Connect
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/UnityConnect/UnityConnectSettings.h")]
	internal class UnityConnectSettings : Object
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13
		// (set) Token: 0x0600000E RID: 14
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern bool testMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15
		// (set) Token: 0x06000010 RID: 16
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern string eventUrl { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17
		// (set) Token: 0x06000012 RID: 18
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern string eventOldUrl { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19
		// (set) Token: 0x06000014 RID: 20
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern string configUrl { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21
		// (set) Token: 0x06000016 RID: 22
		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		public static extern int testInitMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000017 RID: 23 RVA: 0x00002066 File Offset: 0x00000266
		public UnityConnectSettings()
		{
		}
	}
}
