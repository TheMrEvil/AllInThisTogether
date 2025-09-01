using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000010 RID: 16
	public class Compute_DT_EventArgs
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00016A8D File Offset: 0x00014C8D
		public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, float progress)
		{
			this.EventType = type;
			this.ProgressPercentage = progress;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00016AA3 File Offset: 0x00014CA3
		public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, Color[] colors)
		{
			this.EventType = type;
			this.Colors = colors;
		}

		// Token: 0x0400008D RID: 141
		public Compute_DistanceTransform_EventTypes EventType;

		// Token: 0x0400008E RID: 142
		public float ProgressPercentage;

		// Token: 0x0400008F RID: 143
		public Color[] Colors;
	}
}
