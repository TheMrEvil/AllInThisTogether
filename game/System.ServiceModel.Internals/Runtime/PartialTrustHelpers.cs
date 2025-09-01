using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime
{
	// Token: 0x02000026 RID: 38
	internal static class PartialTrustHelpers
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000568C File Offset: 0x0000388C
		internal static bool ShouldFlowSecurityContext
		{
			[SecurityCritical]
			get
			{
				return SecurityManager.CurrentThreadRequiresSecurityContextCapture();
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005693 File Offset: 0x00003893
		[SecurityCritical]
		internal static bool IsInFullTrust()
		{
			return true;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005698 File Offset: 0x00003898
		[SecurityCritical]
		internal static bool IsTypeAptca(Type type)
		{
			Assembly assembly = type.Assembly;
			return PartialTrustHelpers.IsAssemblyAptca(assembly) || !PartialTrustHelpers.IsAssemblySigned(assembly);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000056BF File Offset: 0x000038BF
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void DemandForFullTrust()
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000056C1 File Offset: 0x000038C1
		[SecurityCritical]
		private static bool IsAssemblyAptca(Assembly assembly)
		{
			if (PartialTrustHelpers.aptca == null)
			{
				PartialTrustHelpers.aptca = typeof(AllowPartiallyTrustedCallersAttribute);
			}
			return assembly.GetCustomAttributes(PartialTrustHelpers.aptca, false).Length != 0;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000056F0 File Offset: 0x000038F0
		[SecurityCritical]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool IsAssemblySigned(Assembly assembly)
		{
			byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
			return publicKeyToken != null & publicKeyToken.Length != 0;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005713 File Offset: 0x00003913
		[SecurityCritical]
		internal static bool CheckAppDomainPermissions(PermissionSet permissions)
		{
			return true;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005716 File Offset: 0x00003916
		[SecurityCritical]
		internal static bool HasEtwPermissions()
		{
			return true;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005719 File Offset: 0x00003919
		internal static bool AppDomainFullyTrusted
		{
			[SecuritySafeCritical]
			get
			{
				return true;
			}
		}

		// Token: 0x040000C4 RID: 196
		[SecurityCritical]
		private static Type aptca;

		// Token: 0x040000C5 RID: 197
		[SecurityCritical]
		private static volatile bool checkedForFullTrust;

		// Token: 0x040000C6 RID: 198
		[SecurityCritical]
		private static bool inFullTrust;
	}
}
