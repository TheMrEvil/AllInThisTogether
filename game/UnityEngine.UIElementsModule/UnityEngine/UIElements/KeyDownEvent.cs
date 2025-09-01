using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EF RID: 495
	public class KeyDownEvent : KeyboardEventBase<KeyDownEvent>
	{
		// Token: 0x06000F93 RID: 3987 RVA: 0x0003FD00 File Offset: 0x0003DF00
		internal void GetEquivalentImguiEvent(Event outImguiEvent)
		{
			bool flag = base.imguiEvent != null;
			if (flag)
			{
				outImguiEvent.CopyFrom(base.imguiEvent);
			}
			else
			{
				outImguiEvent.type = EventType.KeyDown;
				outImguiEvent.modifiers = base.modifiers;
				outImguiEvent.character = base.character;
				outImguiEvent.keyCode = base.keyCode;
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003FD5D File Offset: 0x0003DF5D
		public KeyDownEvent()
		{
		}
	}
}
