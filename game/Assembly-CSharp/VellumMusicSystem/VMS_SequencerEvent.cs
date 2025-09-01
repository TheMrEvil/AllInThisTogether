using System;
using UnityEngine;

namespace VellumMusicSystem
{
	// Token: 0x020003CD RID: 973
	[Serializable]
	public struct VMS_SequencerEvent
	{
		// Token: 0x0400202A RID: 8234
		[HideInInspector]
		public int section;

		// Token: 0x0400202B RID: 8235
		public int subSection;

		// Token: 0x0400202C RID: 8236
		public VMS_StemEvent melody;

		// Token: 0x0400202D RID: 8237
		public VMS_StemEvent rhythm;

		// Token: 0x0400202E RID: 8238
		public VMS_StemEvent bass;

		// Token: 0x0400202F RID: 8239
		public VMS_StemEvent drums;

		// Token: 0x04002030 RID: 8240
		public VMS_StemEvent ambience;
	}
}
