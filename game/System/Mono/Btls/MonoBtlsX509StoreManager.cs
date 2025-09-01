using System;
using System.IO;
using Mono.Security.X509;

namespace Mono.Btls
{
	// Token: 0x0200010C RID: 268
	internal static class MonoBtlsX509StoreManager
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x0001174C File Offset: 0x0000F94C
		private static void Initialize()
		{
			if (MonoBtlsX509StoreManager.initialized)
			{
				return;
			}
			try
			{
				MonoBtlsX509StoreManager.DoInitialize();
			}
			catch (Exception arg)
			{
				Console.Error.WriteLine("MonoBtlsX509StoreManager.Initialize() threw exception: {0}", arg);
			}
			finally
			{
				MonoBtlsX509StoreManager.initialized = true;
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000117A0 File Offset: 0x0000F9A0
		private static void DoInitialize()
		{
			string newCurrentUserPath = X509StoreManager.NewCurrentUserPath;
			MonoBtlsX509StoreManager.userTrustedRootPath = Path.Combine(newCurrentUserPath, "Trust");
			MonoBtlsX509StoreManager.userIntermediateCAPath = Path.Combine(newCurrentUserPath, "CA");
			MonoBtlsX509StoreManager.userUntrustedPath = Path.Combine(newCurrentUserPath, "Disallowed");
			string newLocalMachinePath = X509StoreManager.NewLocalMachinePath;
			MonoBtlsX509StoreManager.machineTrustedRootPath = Path.Combine(newLocalMachinePath, "Trust");
			MonoBtlsX509StoreManager.machineIntermediateCAPath = Path.Combine(newLocalMachinePath, "CA");
			MonoBtlsX509StoreManager.machineUntrustedPath = Path.Combine(newLocalMachinePath, "Disallowed");
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00011818 File Offset: 0x0000FA18
		public static bool HasStore(MonoBtlsX509StoreType type)
		{
			string storePath = MonoBtlsX509StoreManager.GetStorePath(type);
			return storePath != null && Directory.Exists(storePath);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00011838 File Offset: 0x0000FA38
		public static string GetStorePath(MonoBtlsX509StoreType type)
		{
			MonoBtlsX509StoreManager.Initialize();
			switch (type)
			{
			case MonoBtlsX509StoreType.MachineTrustedRoots:
				return MonoBtlsX509StoreManager.machineTrustedRootPath;
			case MonoBtlsX509StoreType.MachineIntermediateCA:
				return MonoBtlsX509StoreManager.machineIntermediateCAPath;
			case MonoBtlsX509StoreType.MachineUntrusted:
				return MonoBtlsX509StoreManager.machineUntrustedPath;
			case MonoBtlsX509StoreType.UserTrustedRoots:
				return MonoBtlsX509StoreManager.userTrustedRootPath;
			case MonoBtlsX509StoreType.UserIntermediateCA:
				return MonoBtlsX509StoreManager.userIntermediateCAPath;
			case MonoBtlsX509StoreType.UserUntrusted:
				return MonoBtlsX509StoreManager.userUntrustedPath;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000451 RID: 1105
		private static bool initialized;

		// Token: 0x04000452 RID: 1106
		private static string machineTrustedRootPath;

		// Token: 0x04000453 RID: 1107
		private static string machineIntermediateCAPath;

		// Token: 0x04000454 RID: 1108
		private static string machineUntrustedPath;

		// Token: 0x04000455 RID: 1109
		private static string userTrustedRootPath;

		// Token: 0x04000456 RID: 1110
		private static string userIntermediateCAPath;

		// Token: 0x04000457 RID: 1111
		private static string userUntrustedPath;
	}
}
