using System;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003D1 RID: 977
	[CreateAssetMenu(menuName = "Music System/Probability Setting")]
	public class VMS_StemProbability : ScriptableObject
	{
		// Token: 0x06002004 RID: 8196 RVA: 0x000BE436 File Offset: 0x000BC636
		public VMS_StemProbability()
		{
		}

		// Token: 0x0400203F RID: 8255
		public float melodyProbability = 0.5f;

		// Token: 0x04002040 RID: 8256
		public float rhythmProbability = 0.5f;

		// Token: 0x04002041 RID: 8257
		public float bassProbability = 0.5f;

		// Token: 0x04002042 RID: 8258
		public float drumsProbability = 1f;
	}
}
