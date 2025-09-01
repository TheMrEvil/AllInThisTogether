using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003CC RID: 972
	public class VMS_Sequencer
	{
		// Token: 0x06001FFF RID: 8191 RVA: 0x000BDE64 File Offset: 0x000BC064
		public VMS_SequencerEvent GetSequencerEvents(VMS_SceneData sceneData, VMS_SequencerSettings sequencerSettings)
		{
			VMS_Sequencer.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.sceneData = sceneData;
			VMS_SequencerEvent vms_SequencerEvent = this.sequencerHistory[0];
			VMS_SequencerEvent vms_SequencerEvent2 = new VMS_SequencerEvent
			{
				section = vms_SequencerEvent.section,
				subSection = vms_SequencerEvent.subSection
			};
			if (CS$<>8__locals1.sceneData.clipSetHasChanged)
			{
				this.sequencerEvents = 0;
				if (sequencerSettings.resetSubSectionOnSetChange)
				{
					vms_SequencerEvent2.subSection = 0;
				}
			}
			if (CS$<>8__locals1.sceneData.clipSet.manualEventList == null || this.sequencerEvents >= CS$<>8__locals1.sceneData.clipSet.manualEventList.events.Length)
			{
				if (CS$<>8__locals1.sceneData.clipSetHasChanged && sequencerSettings.resetDrumBreakOnSetChange)
				{
					this.drumPlays = 0;
				}
				if (this.sectionPlays > sequencerSettings.sectionRepeats)
				{
					int sections = CS$<>8__locals1.sceneData.clipSet.setInfo.sections;
					vms_SequencerEvent2.section = UnityEngine.Random.Range(0, sections);
					if (vms_SequencerEvent2.section == vms_SequencerEvent.section)
					{
						vms_SequencerEvent2.section++;
						if (vms_SequencerEvent2.section >= sections)
						{
							vms_SequencerEvent2.section = 0;
						}
					}
					this.sectionPlays = 0;
				}
				vms_SequencerEvent2.melody.section = vms_SequencerEvent2.section;
				vms_SequencerEvent2.rhythm.section = vms_SequencerEvent2.section;
				vms_SequencerEvent2.bass.section = vms_SequencerEvent2.section;
				vms_SequencerEvent2.drums.section = vms_SequencerEvent2.section;
				vms_SequencerEvent2.ambience.section = vms_SequencerEvent2.section;
				vms_SequencerEvent2.ambience.playStatus = VMS_StemPlayStatus.PlayStem;
				if (sequencerSettings.randomiseAmbience)
				{
					vms_SequencerEvent2.ambience.section = UnityEngine.Random.Range(0, CS$<>8__locals1.sceneData.clipSet.setInfo.sections);
				}
				if (sequencerSettings.randomiseDrums)
				{
					vms_SequencerEvent2.drums.section = UnityEngine.Random.Range(0, CS$<>8__locals1.sceneData.clipSet.setInfo.sections);
				}
				bool flag = this.currentSubSection == 0;
				int num = 0;
				if (flag)
				{
					if (UnityEngine.Random.value < CS$<>8__locals1.sceneData.clipSet.stemProbability.melodyProbability)
					{
						vms_SequencerEvent2.melody.playStatus = VMS_StemPlayStatus.PlayStem;
						num++;
					}
					if (UnityEngine.Random.value < CS$<>8__locals1.sceneData.clipSet.stemProbability.rhythmProbability)
					{
						vms_SequencerEvent2.rhythm.playStatus = VMS_StemPlayStatus.PlayStem;
						num++;
					}
					if (UnityEngine.Random.value < CS$<>8__locals1.sceneData.clipSet.stemProbability.bassProbability)
					{
						vms_SequencerEvent2.bass.playStatus = VMS_StemPlayStatus.PlayStem;
						num++;
					}
				}
				if (!flag && !CS$<>8__locals1.sceneData.clipSetHasChanged)
				{
					if (vms_SequencerEvent.melody.playStatus == VMS_StemPlayStatus.PlayStem || UnityEngine.Random.value < CS$<>8__locals1.sceneData.clipSet.stemProbability.melodyProbability)
					{
						vms_SequencerEvent2.melody.playStatus = VMS_StemPlayStatus.PlayStem;
						num++;
					}
					if (vms_SequencerEvent.rhythm.playStatus == VMS_StemPlayStatus.PlayStem || UnityEngine.Random.value < CS$<>8__locals1.sceneData.clipSet.stemProbability.rhythmProbability)
					{
						vms_SequencerEvent2.rhythm.playStatus = VMS_StemPlayStatus.PlayStem;
						num++;
					}
					if (vms_SequencerEvent.bass.playStatus == VMS_StemPlayStatus.PlayStem || UnityEngine.Random.value < CS$<>8__locals1.sceneData.clipSet.stemProbability.bassProbability)
					{
						vms_SequencerEvent2.bass.playStatus = VMS_StemPlayStatus.PlayStem;
						num++;
					}
				}
				if (UnityEngine.Random.value < CS$<>8__locals1.sceneData.clipSet.stemProbability.drumsProbability)
				{
					vms_SequencerEvent2.drums.playStatus = VMS_StemPlayStatus.PlayStem;
				}
				if (sequencerSettings.drumBreakGap > 0 && this.drumPlays > sequencerSettings.drumBreakGap && num > sequencerSettings.minDrumBreakStems)
				{
					vms_SequencerEvent2.drums.playStatus = VMS_StemPlayStatus.DoNotPlayStem;
				}
			}
			else
			{
				vms_SequencerEvent2 = CS$<>8__locals1.sceneData.clipSet.manualEventList.events[this.sequencerEvents];
			}
			if (vms_SequencerEvent2.melody.playStatus == VMS_StemPlayStatus.DoNotPlayStem && vms_SequencerEvent.melody.playStatus == VMS_StemPlayStatus.PlayStem)
			{
				vms_SequencerEvent2.melody.section = -1;
			}
			if (vms_SequencerEvent2.rhythm.playStatus == VMS_StemPlayStatus.DoNotPlayStem && vms_SequencerEvent.rhythm.playStatus == VMS_StemPlayStatus.PlayStem)
			{
				vms_SequencerEvent2.rhythm.section = -1;
			}
			if (vms_SequencerEvent2.bass.playStatus == VMS_StemPlayStatus.DoNotPlayStem && vms_SequencerEvent.bass.playStatus == VMS_StemPlayStatus.PlayStem)
			{
				vms_SequencerEvent2.bass.section = -1;
			}
			if (vms_SequencerEvent2.drums.playStatus == VMS_StemPlayStatus.DoNotPlayStem && vms_SequencerEvent.drums.playStatus == VMS_StemPlayStatus.PlayStem)
			{
				this.drumPlays = 0;
				vms_SequencerEvent2.drums.section = -1;
			}
			vms_SequencerEvent2.subSection = this.currentSubSection;
			this.<GetSequencerEvents>g__IncrementPlayStats|6_0(vms_SequencerEvent2, ref CS$<>8__locals1);
			this.<GetSequencerEvents>g__AddToSequencerHistory|6_1(vms_SequencerEvent2, ref CS$<>8__locals1);
			return vms_SequencerEvent2;
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x000BE304 File Offset: 0x000BC504
		public VMS_Sequencer()
		{
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000BE320 File Offset: 0x000BC520
		[CompilerGenerated]
		private void <GetSequencerEvents>g__IncrementPlayStats|6_0(VMS_SequencerEvent nextSequencerEvent, ref VMS_Sequencer.<>c__DisplayClass6_0 A_2)
		{
			if (nextSequencerEvent.drums.playStatus == VMS_StemPlayStatus.PlayStem)
			{
				this.drumPlays++;
			}
			this.currentSubSection++;
			if (this.currentSubSection >= A_2.sceneData.clipSet.setInfo.subSections)
			{
				this.currentSubSection = 0;
				this.sectionPlays++;
			}
			this.sequencerEvents++;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000BE398 File Offset: 0x000BC598
		[CompilerGenerated]
		private void <GetSequencerEvents>g__AddToSequencerHistory|6_1(VMS_SequencerEvent sequencerEvent, ref VMS_Sequencer.<>c__DisplayClass6_0 A_2)
		{
			for (int i = this.sequencerLogs - 2; i >= 0; i--)
			{
				this.sequencerHistory[i + 1] = this.sequencerHistory[i];
			}
			this.sequencerHistory[0] = sequencerEvent;
			if (this.sequencerLogs < this.sequencerHistory.Length)
			{
				this.sequencerLogs++;
			}
		}

		// Token: 0x04002024 RID: 8228
		private VMS_SequencerEvent[] sequencerHistory = new VMS_SequencerEvent[10];

		// Token: 0x04002025 RID: 8229
		private int sequencerEvents;

		// Token: 0x04002026 RID: 8230
		private int sequencerLogs = 1;

		// Token: 0x04002027 RID: 8231
		private int sectionPlays;

		// Token: 0x04002028 RID: 8232
		private int currentSubSection;

		// Token: 0x04002029 RID: 8233
		private int drumPlays;

		// Token: 0x020006A3 RID: 1699
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass6_0
		{
			// Token: 0x04002C74 RID: 11380
			public VMS_Sequencer <>4__this;

			// Token: 0x04002C75 RID: 11381
			public VMS_SceneData sceneData;
		}
	}
}
