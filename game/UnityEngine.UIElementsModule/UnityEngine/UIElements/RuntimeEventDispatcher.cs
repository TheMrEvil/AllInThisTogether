using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024D RID: 589
	internal static class RuntimeEventDispatcher
	{
		// Token: 0x060011E8 RID: 4584 RVA: 0x0004688C File Offset: 0x00044A8C
		public static EventDispatcher Create()
		{
			return EventDispatcher.CreateForRuntime(new List<IEventDispatchingStrategy>
			{
				new PointerCaptureDispatchingStrategy(),
				new MouseCaptureDispatchingStrategy(),
				new KeyboardEventDispatchingStrategy(),
				new PointerEventDispatchingStrategy(),
				new MouseEventDispatchingStrategy(),
				new NavigationEventDispatchingStrategy(),
				new DefaultDispatchingStrategy()
			});
		}
	}
}
