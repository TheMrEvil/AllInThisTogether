using System;
using UnityEngine;

namespace QFSW.QC.Extras
{
	// Token: 0x02000010 RID: 16
	public static class TimeCommands
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003714 File Offset: 0x00001914
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000371B File Offset: 0x0000191B
		private static float TimeScale
		{
			get
			{
				return Time.timeScale;
			}
			set
			{
				Time.timeScale = value;
			}
		}
	}
}
