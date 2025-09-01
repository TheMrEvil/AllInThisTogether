using System;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003C6 RID: 966
	[CreateAssetMenu(menuName = "Music System/Clip Set Info")]
	public class VMS_ClipSetInfo : ScriptableObject
	{
		// Token: 0x06001FCA RID: 8138 RVA: 0x000BD12C File Offset: 0x000BB32C
		public double TailTime()
		{
			return this.BarTime(this.tailBars);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000BD13A File Offset: 0x000BB33A
		public double IntroTime()
		{
			return this.BarTime(this.introBars);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000BD148 File Offset: 0x000BB348
		public double BarTime(int numBars)
		{
			return 60.0 / (double)this.bpm * (double)this.beatsPerBar * (double)numBars;
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000BD166 File Offset: 0x000BB366
		public double BeatTime()
		{
			return 60.0 / (double)this.bpm;
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000BD179 File Offset: 0x000BB379
		public VMS_ClipSetInfo()
		{
		}

		// Token: 0x04001FFA RID: 8186
		[Header("Tempo Information")]
		[Space(10f)]
		[SerializeField]
		private int bpm = 130;

		// Token: 0x04001FFB RID: 8187
		[SerializeField]
		private int beatsPerBar = 4;

		// Token: 0x04001FFC RID: 8188
		public int sections = 4;

		// Token: 0x04001FFD RID: 8189
		public int subSections = 2;

		// Token: 0x04001FFE RID: 8190
		[SerializeField]
		private int tailBars = 2;

		// Token: 0x04001FFF RID: 8191
		[SerializeField]
		private int introBars = 1;

		// Token: 0x04002000 RID: 8192
		[Header("Clip Data")]
		[Space(10f)]
		public VMS_SequencerSettings sequencerSettingsProfile;
	}
}
