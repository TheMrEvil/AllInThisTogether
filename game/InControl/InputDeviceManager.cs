using System;
using System.Collections.Generic;

namespace InControl
{
	// Token: 0x02000037 RID: 55
	public abstract class InputDeviceManager
	{
		// Token: 0x06000274 RID: 628
		public abstract void Update(ulong updateTick, float deltaTime);

		// Token: 0x06000275 RID: 629 RVA: 0x00008503 File Offset: 0x00006703
		public virtual void Destroy()
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008505 File Offset: 0x00006705
		protected InputDeviceManager()
		{
		}

		// Token: 0x04000292 RID: 658
		protected readonly List<InputDevice> devices = new List<InputDevice>();
	}
}
