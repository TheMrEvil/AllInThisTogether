using System;
using System.Collections.Generic;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003C5 RID: 965
	[CreateAssetMenu(menuName = "Music System/Clip Set")]
	public class VMS_ClipSet : ScriptableObject
	{
		// Token: 0x06001FC6 RID: 8134 RVA: 0x000BCEF0 File Offset: 0x000BB0F0
		public AudioClip GetClip(VMS_StemType stemType, int sectionNum, int subSectionNum)
		{
			Action<VMS_GetClipEvent> onGetClip = VMS_ClipSet.OnGetClip;
			if (onGetClip != null)
			{
				onGetClip(new VMS_GetClipEvent(this, stemType, sectionNum, subSectionNum));
			}
			VMS_ClipSet.fallBackCounter++;
			if (VMS_ClipSet.fallBackCounter >= 10)
			{
				if (VMS_Player.instance != null && VMS_Player.instance.logWarnings)
				{
					Debug.LogWarning("The fallback limit was reached and no clip was selected. Check that the final fallback does not, itself, fall back to another clip set or that a clip set is not falling back to its own clip set.");
				}
				return null;
			}
			int num = sectionNum;
			if (num == -1)
			{
				AudioClip audioClip = this.stemSets[(int)stemType].endingClip;
				if (audioClip == null && this.fallbackClipSet != null)
				{
					audioClip = this.fallbackClipSet.GetClip(stemType, sectionNum, subSectionNum);
				}
				Action<VMS_GetClipEvent> onGetClip2 = VMS_ClipSet.OnGetClip;
				if (onGetClip2 != null)
				{
					onGetClip2(new VMS_GetClipEvent(this, stemType, sectionNum, subSectionNum));
				}
				VMS_ClipSet.fallBackCounter = 0;
				return audioClip;
			}
			if (num >= this.stemSets[(int)stemType].sectionClips.Length)
			{
				num = this.stemSets[(int)stemType].sectionClips.Length - 1;
			}
			AudioClip audioClip2 = this.stemSets[(int)stemType].sectionClips[num].GetSectionClip(subSectionNum);
			if (audioClip2 == null && this.fallbackClipSet != null)
			{
				audioClip2 = this.fallbackClipSet.GetClip(stemType, sectionNum, subSectionNum);
			}
			VMS_ClipSet.fallBackCounter = 0;
			return audioClip2;
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000BD018 File Offset: 0x000BB218
		public List<AudioClip> GetAllClips()
		{
			List<AudioClip> list = new List<AudioClip>();
			foreach (VMS_ClipSet.MusicClipStemSet musicClipStemSet in this.stemSets)
			{
				foreach (VMS_ClipSet.SectionClip sectionClip in musicClipStemSet.sectionClips)
				{
					if (sectionClip.GetSectionClip(0) != null)
					{
						list.Add(sectionClip.GetSectionClip(0));
					}
					if (sectionClip.GetSectionClip(1) != null)
					{
						list.Add(sectionClip.GetSectionClip(1));
					}
				}
				if (musicClipStemSet.endingClip != null)
				{
					list.Add(musicClipStemSet.endingClip);
				}
			}
			return list;
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000BD0C3 File Offset: 0x000BB2C3
		private void OnEnable()
		{
			this.loaded = false;
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000BD0CC File Offset: 0x000BB2CC
		public VMS_ClipSet()
		{
		}

		// Token: 0x04001FEF RID: 8175
		public static Action<VMS_GetClipEvent> OnGetClip;

		// Token: 0x04001FF0 RID: 8176
		private static int fallBackCounter;

		// Token: 0x04001FF1 RID: 8177
		public Color clipSetColor;

		// Token: 0x04001FF2 RID: 8178
		[HideInInspector]
		public bool loaded;

		// Token: 0x04001FF3 RID: 8179
		[Space(10f)]
		[Tooltip("Forces the player to stop and restart when switching to or from this clip set. Use for special sets, such as Boss music.")]
		public bool restartOnly;

		// Token: 0x04001FF4 RID: 8180
		public VMS_StemProbability stemProbability;

		// Token: 0x04001FF5 RID: 8181
		public VMS_VolumeProfileSet volumeProfile;

		// Token: 0x04001FF6 RID: 8182
		public VMS_ClipSetInfo setInfo;

		// Token: 0x04001FF7 RID: 8183
		[Tooltip("The clip set that will be used if a clip is missing or intentionally not set. Clip Set fallback is recursive up to a limit of 10 layers, after which null will be returned. Leave this blank to stop further fallback at this Clip Set.")]
		public VMS_ClipSet fallbackClipSet;

		// Token: 0x04001FF8 RID: 8184
		[Tooltip("If set, the sequencer will follow a user-definable pattern of events when the set is first played. Once it runs out of manual events it will select random events as normal. Leave empty to disable.")]
		public VMS_EventList manualEventList;

		// Token: 0x04001FF9 RID: 8185
		[Space(10f)]
		[SerializeField]
		private VMS_ClipSet.MusicClipStemSet[] stemSets = new VMS_ClipSet.MusicClipStemSet[]
		{
			new VMS_ClipSet.MusicClipStemSet("Melody Clips"),
			new VMS_ClipSet.MusicClipStemSet("Rhythm Clips"),
			new VMS_ClipSet.MusicClipStemSet("Bass Clips"),
			new VMS_ClipSet.MusicClipStemSet("Drum Clips"),
			new VMS_ClipSet.MusicClipStemSet("Ambience Clips")
		};

		// Token: 0x0200069F RID: 1695
		[Serializable]
		private class MusicClipStemSet
		{
			// Token: 0x06002830 RID: 10288 RVA: 0x000D7FF8 File Offset: 0x000D61F8
			public MusicClipStemSet(string name)
			{
				this.name = name;
			}

			// Token: 0x04002C62 RID: 11362
			[HideInInspector]
			public string name;

			// Token: 0x04002C63 RID: 11363
			public VMS_ClipSet.SectionClip[] sectionClips = new VMS_ClipSet.SectionClip[]
			{
				new VMS_ClipSet.SectionClip("Section 1"),
				new VMS_ClipSet.SectionClip("Section 2"),
				new VMS_ClipSet.SectionClip("Section 3"),
				new VMS_ClipSet.SectionClip("Section 4")
			};

			// Token: 0x04002C64 RID: 11364
			public AudioClip endingClip;
		}

		// Token: 0x020006A0 RID: 1696
		[Serializable]
		private class SectionClip
		{
			// Token: 0x06002831 RID: 10289 RVA: 0x000D8052 File Offset: 0x000D6252
			public SectionClip(string name)
			{
				this.name = name;
			}

			// Token: 0x06002832 RID: 10290 RVA: 0x000D8061 File Offset: 0x000D6261
			public AudioClip GetSectionClip(int subSectionNumber)
			{
				if (subSectionNumber != 0)
				{
					return this.b;
				}
				return this.a;
			}

			// Token: 0x04002C65 RID: 11365
			[HideInInspector]
			public string name;

			// Token: 0x04002C66 RID: 11366
			[SerializeField]
			private AudioClip a;

			// Token: 0x04002C67 RID: 11367
			[SerializeField]
			private AudioClip b;
		}
	}
}
