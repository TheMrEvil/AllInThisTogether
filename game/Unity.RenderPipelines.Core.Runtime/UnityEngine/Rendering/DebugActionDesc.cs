using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000069 RID: 105
	internal class DebugActionDesc
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		public DebugActionDesc()
		{
		}

		// Token: 0x04000236 RID: 566
		public string axisTrigger = "";

		// Token: 0x04000237 RID: 567
		public List<string[]> buttonTriggerList = new List<string[]>();

		// Token: 0x04000238 RID: 568
		public List<KeyCode[]> keyTriggerList = new List<KeyCode[]>();

		// Token: 0x04000239 RID: 569
		public DebugActionRepeatMode repeatMode;

		// Token: 0x0400023A RID: 570
		public float repeatDelay;
	}
}
