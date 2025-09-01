using System;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000DD RID: 221
	public class ES3GlobalReferences : ScriptableObject
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001E12A File Offset: 0x0001C32A
		public static ES3GlobalReferences Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001E12D File Offset: 0x0001C32D
		public UnityEngine.Object Get(long id)
		{
			return null;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001E130 File Offset: 0x0001C330
		public long GetOrAdd(UnityEngine.Object obj)
		{
			return -1L;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001E134 File Offset: 0x0001C334
		public void RemoveInvalidKeys()
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001E136 File Offset: 0x0001C336
		public ES3GlobalReferences()
		{
		}
	}
}
