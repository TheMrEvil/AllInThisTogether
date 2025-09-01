using System;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003D4 RID: 980
	[CreateAssetMenu(menuName = "Music System/Volume Profile Set")]
	public class VMS_VolumeProfileSet : ScriptableObject
	{
		// Token: 0x06002005 RID: 8197 RVA: 0x000BE46A File Offset: 0x000BC66A
		public VMS_VolumeProfileSet()
		{
		}

		// Token: 0x0400204E RID: 8270
		public VolumeProfile gameplayVolume;

		// Token: 0x0400204F RID: 8271
		public VolumeProfile menuVolume;
	}
}
