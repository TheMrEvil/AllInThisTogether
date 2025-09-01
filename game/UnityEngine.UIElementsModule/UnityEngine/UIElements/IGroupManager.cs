using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000039 RID: 57
	internal interface IGroupManager
	{
		// Token: 0x0600015C RID: 348
		IGroupBoxOption GetSelectedOption();

		// Token: 0x0600015D RID: 349
		void OnOptionSelectionChanged(IGroupBoxOption selectedOption);

		// Token: 0x0600015E RID: 350
		void RegisterOption(IGroupBoxOption option);

		// Token: 0x0600015F RID: 351
		void UnregisterOption(IGroupBoxOption option);
	}
}
