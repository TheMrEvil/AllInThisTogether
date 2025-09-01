using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B9 RID: 697
	[HelpURL("UIE-tss")]
	[Serializable]
	public class ThemeStyleSheet : StyleSheet
	{
		// Token: 0x0600179C RID: 6044 RVA: 0x00062CBF File Offset: 0x00060EBF
		internal override void OnEnable()
		{
			base.isDefaultStyleSheet = true;
			base.OnEnable();
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00062CD1 File Offset: 0x00060ED1
		public ThemeStyleSheet()
		{
		}
	}
}
