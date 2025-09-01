using System;

namespace VellumMusicSystem
{
	// Token: 0x020003C8 RID: 968
	public struct VMS_GetClipEvent
	{
		// Token: 0x06001FD0 RID: 8144 RVA: 0x000BD1B7 File Offset: 0x000BB3B7
		public VMS_GetClipEvent(VMS_ClipSet clipSet, VMS_StemType stemType, int section, int subSection)
		{
			this.clipSet = clipSet;
			this.stemType = stemType;
			this.section = section;
			this.subSection = subSection;
		}

		// Token: 0x04002002 RID: 8194
		public VMS_ClipSet clipSet;

		// Token: 0x04002003 RID: 8195
		public VMS_StemType stemType;

		// Token: 0x04002004 RID: 8196
		public int section;

		// Token: 0x04002005 RID: 8197
		public int subSection;
	}
}
