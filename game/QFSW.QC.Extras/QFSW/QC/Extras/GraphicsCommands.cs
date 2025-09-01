using System;
using UnityEngine;

namespace QFSW.QC.Extras
{
	// Token: 0x0200000B RID: 11
	public static class GraphicsCommands
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002E6A File Offset: 0x0000106A
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002E71 File Offset: 0x00001071
		private static int MaxFPS
		{
			get
			{
				return Application.targetFrameRate;
			}
			set
			{
				Application.targetFrameRate = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002E79 File Offset: 0x00001079
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002E83 File Offset: 0x00001083
		private static bool VSync
		{
			get
			{
				return QualitySettings.vSyncCount > 0;
			}
			set
			{
				QualitySettings.vSyncCount = (value ? 1 : 0);
			}
		}
	}
}
