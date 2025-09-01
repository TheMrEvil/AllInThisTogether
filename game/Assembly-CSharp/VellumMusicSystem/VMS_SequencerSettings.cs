using System;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003CE RID: 974
	[CreateAssetMenu(menuName = "Music System/Sequencer Settings Profile")]
	public class VMS_SequencerSettings : ScriptableObject
	{
		// Token: 0x06002003 RID: 8195 RVA: 0x000BE3FD File Offset: 0x000BC5FD
		public VMS_SequencerSettings()
		{
		}

		// Token: 0x04002031 RID: 8241
		[Tooltip("How many times can each section be repeated? (measured in sections, not sub-sections). Setting to Zero will cause the section to change every time.")]
		public int sectionRepeats;

		// Token: 0x04002032 RID: 8242
		[Tooltip("Resets the current sub section when changing set. When checked, new sets will always start with Sub Section A.")]
		public bool resetSubSectionOnSetChange = true;

		// Token: 0x04002033 RID: 8243
		[Space(10f)]
		[Tooltip("Allow the Drums to play using random sections.")]
		public bool randomiseDrums = true;

		// Token: 0x04002034 RID: 8244
		[Tooltip("How many times can the drums repeat themselves (measured in sections, not sub-sections). Setting to Zero will cause the drums to change every section.")]
		public int maxRandomDrumRepeats;

		// Token: 0x04002035 RID: 8245
		[Space(10f)]
		[Tooltip("How many sections will feature drums before taking a break during combat. Select 0 to disable the Drum Break (meaning that drums will always play in combat)")]
		public int drumBreakGap = 4;

		// Token: 0x04002036 RID: 8246
		[Tooltip("If the intensity changes, should the drum break counter be reset? Setting to true prevents a drum break from immeditately following an intensity change.")]
		public bool resetDrumBreakOnSetChange = true;

		// Token: 0x04002037 RID: 8247
		[Tooltip("The minimum number of stems that must be playing to allow a drum break.")]
		public int minDrumBreakStems = 2;

		// Token: 0x04002038 RID: 8248
		[Space(10f)]
		[Tooltip("Allow the Ambience to play using random sections.")]
		public bool randomiseAmbience = true;

		// Token: 0x04002039 RID: 8249
		[Tooltip("How many times can the ambience repeat itself (measured in sections, not sub-sections). Setting to Zero will cause the ambience to change every section.")]
		public int maxRandomAmbienceRepeats = 1;
	}
}
