using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	// Token: 0x02000490 RID: 1168
	[NativeClass("ScriptingCoverage")]
	[NativeType("Runtime/Scripting/ScriptingCoverage.h")]
	public static class Coverage
	{
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002949 RID: 10569
		// (set) Token: 0x0600294A RID: 10570
		public static extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600294B RID: 10571
		[FreeFunction("ScriptingCoverageGetCoverageForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CoveredSequencePoint[] GetSequencePointsFor_Internal(MethodBase method);

		// Token: 0x0600294C RID: 10572
		[FreeFunction("ScriptingCoverageResetForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetFor_Internal(MethodBase method);

		// Token: 0x0600294D RID: 10573 RVA: 0x00044684 File Offset: 0x00042884
		[FreeFunction("ScriptingCoverageGetStatsForMethodInfoObject", ThrowsException = true)]
		private static CoveredMethodStats GetStatsFor_Internal(MethodBase method)
		{
			CoveredMethodStats result;
			Coverage.GetStatsFor_Internal_Injected(method, out result);
			return result;
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x0004469C File Offset: 0x0004289C
		public static CoveredSequencePoint[] GetSequencePointsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetSequencePointsFor_Internal(method);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000446C8 File Offset: 0x000428C8
		public static CoveredMethodStats GetStatsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetStatsFor_Internal(method);
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000446F4 File Offset: 0x000428F4
		public static CoveredMethodStats[] GetStatsFor(MethodBase[] methods)
		{
			bool flag = methods == null;
			if (flag)
			{
				throw new ArgumentNullException("methods");
			}
			CoveredMethodStats[] array = new CoveredMethodStats[methods.Length];
			for (int i = 0; i < methods.Length; i++)
			{
				array[i] = Coverage.GetStatsFor(methods[i]);
			}
			return array;
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x00044748 File Offset: 0x00042948
		public static CoveredMethodStats[] GetStatsFor(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentNullException("type");
			}
			return Coverage.GetStatsFor(type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).OfType<MethodBase>().ToArray<MethodBase>());
		}

		// Token: 0x06002952 RID: 10578
		[FreeFunction("ScriptingCoverageGetStatsForAllCoveredMethodsFromScripting", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern CoveredMethodStats[] GetStatsForAllCoveredMethods();

		// Token: 0x06002953 RID: 10579 RVA: 0x00044784 File Offset: 0x00042984
		public static void ResetFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			Coverage.ResetFor_Internal(method);
		}

		// Token: 0x06002954 RID: 10580
		[FreeFunction("ScriptingCoverageResetAllFromScripting", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetAll();

		// Token: 0x06002955 RID: 10581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStatsFor_Internal_Injected(MethodBase method, out CoveredMethodStats ret);
	}
}
