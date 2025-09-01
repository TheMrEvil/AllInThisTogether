using System;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003C7 RID: 967
	[CreateAssetMenu(menuName = "Music System/Manual Events List")]
	public class VMS_EventList : ScriptableObject
	{
		// Token: 0x06001FCF RID: 8143 RVA: 0x000BD1AF File Offset: 0x000BB3AF
		public VMS_EventList()
		{
		}

		// Token: 0x04002001 RID: 8193
		public VMS_SequencerEvent[] events;
	}
}
