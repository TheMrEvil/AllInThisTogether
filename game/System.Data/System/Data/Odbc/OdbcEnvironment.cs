using System;
using System.Threading;

namespace System.Data.Odbc
{
	// Token: 0x020002E5 RID: 741
	internal sealed class OdbcEnvironment
	{
		// Token: 0x060020F5 RID: 8437 RVA: 0x00003D93 File Offset: 0x00001F93
		private OdbcEnvironment()
		{
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0009A3F4 File Offset: 0x000985F4
		internal static OdbcEnvironmentHandle GetGlobalEnvironmentHandle()
		{
			OdbcEnvironmentHandle odbcEnvironmentHandle = OdbcEnvironment.s_globalEnvironmentHandle as OdbcEnvironmentHandle;
			if (odbcEnvironmentHandle == null)
			{
				object obj = OdbcEnvironment.s_globalEnvironmentHandleLock;
				lock (obj)
				{
					odbcEnvironmentHandle = (OdbcEnvironment.s_globalEnvironmentHandle as OdbcEnvironmentHandle);
					if (odbcEnvironmentHandle == null)
					{
						odbcEnvironmentHandle = new OdbcEnvironmentHandle();
						OdbcEnvironment.s_globalEnvironmentHandle = odbcEnvironmentHandle;
					}
				}
			}
			return odbcEnvironmentHandle;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0009A458 File Offset: 0x00098658
		internal static void ReleaseObjectPool()
		{
			object obj = Interlocked.Exchange(ref OdbcEnvironment.s_globalEnvironmentHandle, null);
			if (obj != null)
			{
				(obj as OdbcEnvironmentHandle).Dispose();
			}
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0009A47F File Offset: 0x0009867F
		// Note: this type is marked as 'beforefieldinit'.
		static OdbcEnvironment()
		{
		}

		// Token: 0x040017B4 RID: 6068
		private static object s_globalEnvironmentHandle;

		// Token: 0x040017B5 RID: 6069
		private static object s_globalEnvironmentHandleLock = new object();
	}
}
