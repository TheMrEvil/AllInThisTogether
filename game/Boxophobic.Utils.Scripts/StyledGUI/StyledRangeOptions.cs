using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x0200000D RID: 13
	public class StyledRangeOptions : PropertyAttribute
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002445 File Offset: 0x00000645
		public StyledRangeOptions(string display, float min, float max, string[] options)
		{
			this.display = display;
			this.min = min;
			this.max = max;
			this.options = options;
		}

		// Token: 0x0400001E RID: 30
		public string display;

		// Token: 0x0400001F RID: 31
		public float min;

		// Token: 0x04000020 RID: 32
		public float max;

		// Token: 0x04000021 RID: 33
		public string[] options;
	}
}
