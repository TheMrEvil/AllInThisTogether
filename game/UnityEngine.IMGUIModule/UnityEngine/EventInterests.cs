using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	internal struct EventInterests
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000373E File Offset: 0x0000193E
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00003746 File Offset: 0x00001946
		public bool wantsMouseMove
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<wantsMouseMove>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<wantsMouseMove>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000374F File Offset: 0x0000194F
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003757 File Offset: 0x00001957
		public bool wantsMouseEnterLeaveWindow
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<wantsMouseEnterLeaveWindow>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<wantsMouseEnterLeaveWindow>k__BackingField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003760 File Offset: 0x00001960
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003768 File Offset: 0x00001968
		public bool wantsLessLayoutEvents
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<wantsLessLayoutEvents>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<wantsLessLayoutEvents>k__BackingField = value;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003774 File Offset: 0x00001974
		public bool WantsEvent(EventType type)
		{
			bool result;
			if (type != EventType.MouseMove)
			{
				result = (type - EventType.MouseEnterWindow > 1 || this.wantsMouseEnterLeaveWindow);
			}
			else
			{
				result = this.wantsMouseMove;
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000037AC File Offset: 0x000019AC
		public bool WantsLayoutPass(EventType type)
		{
			bool flag = !this.wantsLessLayoutEvents;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				switch (type)
				{
				case EventType.MouseDown:
				case EventType.MouseUp:
					return this.wantsMouseMove;
				case EventType.MouseMove:
				case EventType.MouseDrag:
				case EventType.ScrollWheel:
					goto IL_6C;
				case EventType.KeyDown:
				case EventType.KeyUp:
					return GUIUtility.textFieldInput;
				case EventType.Repaint:
					break;
				default:
					if (type != EventType.ExecuteCommand)
					{
						if (type - EventType.MouseEnterWindow > 1)
						{
							goto IL_6C;
						}
						return this.wantsMouseEnterLeaveWindow;
					}
					break;
				}
				return true;
				IL_6C:
				result = false;
			}
			return result;
		}

		// Token: 0x0400004F RID: 79
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <wantsMouseMove>k__BackingField;

		// Token: 0x04000050 RID: 80
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <wantsMouseEnterLeaveWindow>k__BackingField;

		// Token: 0x04000051 RID: 81
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <wantsLessLayoutEvents>k__BackingField;
	}
}
