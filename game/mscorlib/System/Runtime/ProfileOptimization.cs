using System;
using System.Security;

namespace System.Runtime
{
	/// <summary>Improves the startup performance of application domains in applications that require the just-in-time (JIT) compiler by performing background compilation of methods that are likely to be executed, based on profiles created during previous compilations.</summary>
	// Token: 0x0200054F RID: 1359
	public static class ProfileOptimization
	{
		// Token: 0x060035AF RID: 13743 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal static void InternalSetProfileRoot(string directoryPath)
		{
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal static void InternalStartProfile(string profile, IntPtr ptrNativeAssemblyLoadContext)
		{
		}

		/// <summary>Enables optimization profiling for the current application domain, and sets the folder where the optimization profile files are stored. On a single-core computer, the method is ignored.</summary>
		/// <param name="directoryPath">The full path to the folder where profile files are stored for the current application domain.</param>
		// Token: 0x060035B1 RID: 13745 RVA: 0x000C1FCC File Offset: 0x000C01CC
		[SecurityCritical]
		public static void SetProfileRoot(string directoryPath)
		{
			ProfileOptimization.InternalSetProfileRoot(directoryPath);
		}

		/// <summary>Starts just-in-time (JIT) compilation of the methods that were previously recorded in the specified profile file, on a background thread. Starts the process of recording current method use, which later overwrites the specified profile file.</summary>
		/// <param name="profile">The file name of the profile to use.</param>
		// Token: 0x060035B2 RID: 13746 RVA: 0x000C1FD4 File Offset: 0x000C01D4
		[SecurityCritical]
		public static void StartProfile(string profile)
		{
			ProfileOptimization.InternalStartProfile(profile, IntPtr.Zero);
		}
	}
}
