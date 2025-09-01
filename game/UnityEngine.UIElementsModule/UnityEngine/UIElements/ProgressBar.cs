using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000160 RID: 352
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class ProgressBar : AbstractProgressBar
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x0002E870 File Offset: 0x0002CA70
		public ProgressBar()
		{
		}

		// Token: 0x02000161 RID: 353
		public new class UxmlFactory : UxmlFactory<ProgressBar, AbstractProgressBar.UxmlTraits>
		{
			// Token: 0x06000B53 RID: 2899 RVA: 0x0002E879 File Offset: 0x0002CA79
			public UxmlFactory()
			{
			}
		}
	}
}
