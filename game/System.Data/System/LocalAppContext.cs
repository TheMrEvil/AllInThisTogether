using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000068 RID: 104
	internal class LocalAppContext
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x00010C97 File Offset: 0x0000EE97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00010CB0 File Offset: 0x0000EEB0
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			bool flag;
			AppContext.TryGetSwitch(switchName, out flag);
			if (LocalAppContext.DisableCaching)
			{
				return flag;
			}
			switchValue = (flag ? 1 : -1);
			return flag;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00010CD9 File Offset: 0x0000EED9
		private static bool DisableCaching
		{
			get
			{
				return LazyInitializer.EnsureInitialized<bool>(ref LocalAppContext.s_disableCaching, ref LocalAppContext.s_isDisableCachingInitialized, ref LocalAppContext.s_syncObject, delegate()
				{
					bool result;
					AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out result);
					return result;
				});
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00003D93 File Offset: 0x00001F93
		public LocalAppContext()
		{
		}

		// Token: 0x04000616 RID: 1558
		private static bool s_isDisableCachingInitialized;

		// Token: 0x04000617 RID: 1559
		private static bool s_disableCaching;

		// Token: 0x04000618 RID: 1560
		private static object s_syncObject;

		// Token: 0x02000069 RID: 105
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060004BA RID: 1210 RVA: 0x00010D0E File Offset: 0x0000EF0E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c()
			{
			}

			// Token: 0x060004BC RID: 1212 RVA: 0x00010D1C File Offset: 0x0000EF1C
			internal bool <get_DisableCaching>b__6_0()
			{
				bool result;
				AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out result);
				return result;
			}

			// Token: 0x04000619 RID: 1561
			public static readonly LocalAppContext.<>c <>9 = new LocalAppContext.<>c();

			// Token: 0x0400061A RID: 1562
			public static Func<bool> <>9__6_0;
		}
	}
}
