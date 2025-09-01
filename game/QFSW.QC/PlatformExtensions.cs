using System;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000029 RID: 41
	public static class PlatformExtensions
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00003F14 File Offset: 0x00002114
		public static Platform ToPlatform(this RuntimePlatform pl)
		{
			return (Platform)(1L << (int)pl);
		}
	}
}
