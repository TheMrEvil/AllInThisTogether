using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Jobs
{
	// Token: 0x02000004 RID: 4
	public class EarlyInitHelpers
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
		public static void FlushEarlyInits()
		{
			while (EarlyInitHelpers.s_PendingDelegates != null)
			{
				List<EarlyInitHelpers.EarlyInitFunction> list = EarlyInitHelpers.s_PendingDelegates;
				EarlyInitHelpers.s_PendingDelegates = null;
				for (int i = 0; i < list.Count; i++)
				{
					try
					{
						list[i]();
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
					}
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B4 File Offset: 0x000002B4
		public static void AddEarlyInitFunction(EarlyInitHelpers.EarlyInitFunction f)
		{
			if (EarlyInitHelpers.s_PendingDelegates == null)
			{
				EarlyInitHelpers.s_PendingDelegates = new List<EarlyInitHelpers.EarlyInitFunction>();
			}
			EarlyInitHelpers.s_PendingDelegates.Add(f);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D2 File Offset: 0x000002D2
		public static void JobReflectionDataCreationFailed(Exception ex, Type jobType)
		{
			Debug.LogError(string.Format("Failed to create job reflection data for type ${0}:", jobType));
			Debug.LogException(ex);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EA File Offset: 0x000002EA
		public EarlyInitHelpers()
		{
		}

		// Token: 0x04000001 RID: 1
		private static List<EarlyInitHelpers.EarlyInitFunction> s_PendingDelegates;

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x06000008 RID: 8
		public delegate void EarlyInitFunction();
	}
}
