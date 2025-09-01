using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EE RID: 238
	public interface IVisualElementScheduledItem
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000772 RID: 1906
		VisualElement element { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000773 RID: 1907
		bool isActive { get; }

		// Token: 0x06000774 RID: 1908
		void Resume();

		// Token: 0x06000775 RID: 1909
		void Pause();

		// Token: 0x06000776 RID: 1910
		void ExecuteLater(long delayMs);

		// Token: 0x06000777 RID: 1911
		IVisualElementScheduledItem StartingIn(long delayMs);

		// Token: 0x06000778 RID: 1912
		IVisualElementScheduledItem Every(long intervalMs);

		// Token: 0x06000779 RID: 1913
		IVisualElementScheduledItem Until(Func<bool> stopCondition);

		// Token: 0x0600077A RID: 1914
		IVisualElementScheduledItem ForDuration(long durationMs);
	}
}
