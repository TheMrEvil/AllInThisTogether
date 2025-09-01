using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000232 RID: 562
	public sealed class Security
	{
		// Token: 0x06001801 RID: 6145 RVA: 0x0002700C File Offset: 0x0002520C
		[Obsolete("This was an internal method which is no longer used", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData, string authorizationKey)
		{
			return null;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00027020 File Offset: 0x00025220
		[Obsolete("This was an internal method which is no longer used", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData)
		{
			return null;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00027034 File Offset: 0x00025234
		[ExcludeFromDocs]
		[Obsolete("Security.PrefetchSocketPolicy is no longer supported, since the Unity Web Player is no longer supported by Unity.", true)]
		public static bool PrefetchSocketPolicy(string ip, int atPort)
		{
			int timeout = 3000;
			return Security.PrefetchSocketPolicy(ip, atPort, timeout);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00027054 File Offset: 0x00025254
		[Obsolete("Security.PrefetchSocketPolicy is no longer supported, since the Unity Web Player is no longer supported by Unity.", true)]
		public static bool PrefetchSocketPolicy(string ip, int atPort, [UnityEngine.Internal.DefaultValue("3000")] int timeout)
		{
			return false;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00002072 File Offset: 0x00000272
		public Security()
		{
		}
	}
}
