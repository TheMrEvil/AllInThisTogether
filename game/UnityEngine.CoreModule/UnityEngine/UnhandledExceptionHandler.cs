using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000222 RID: 546
	[NativeHeader("PlatformDependent/iPhonePlayer/IOSScriptBindings.h")]
	internal sealed class UnhandledExceptionHandler
	{
		// Token: 0x06001793 RID: 6035 RVA: 0x00026315 File Offset: 0x00024515
		[RequiredByNativeCode]
		private static void RegisterUECatcher()
		{
			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
			{
				Debug.LogException(e.ExceptionObject as Exception);
			};
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00002072 File Offset: 0x00000272
		public UnhandledExceptionHandler()
		{
		}

		// Token: 0x02000223 RID: 547
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001795 RID: 6037 RVA: 0x00026342 File Offset: 0x00024542
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001796 RID: 6038 RVA: 0x00002072 File Offset: 0x00000272
			public <>c()
			{
			}

			// Token: 0x06001797 RID: 6039 RVA: 0x0002634E File Offset: 0x0002454E
			internal void <RegisterUECatcher>b__0_0(object sender, UnhandledExceptionEventArgs e)
			{
				Debug.LogException(e.ExceptionObject as Exception);
			}

			// Token: 0x04000815 RID: 2069
			public static readonly UnhandledExceptionHandler.<>c <>9 = new UnhandledExceptionHandler.<>c();

			// Token: 0x04000816 RID: 2070
			public static UnhandledExceptionEventHandler <>9__0_0;
		}
	}
}
