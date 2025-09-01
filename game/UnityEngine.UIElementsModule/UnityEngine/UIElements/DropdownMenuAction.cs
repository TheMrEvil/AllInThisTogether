using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000023 RID: 35
	public class DropdownMenuAction : DropdownMenuItem
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000050CC File Offset: 0x000032CC
		public string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000050D4 File Offset: 0x000032D4
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000050DC File Offset: 0x000032DC
		public DropdownMenuAction.Status status
		{
			[CompilerGenerated]
			get
			{
				return this.<status>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<status>k__BackingField = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000050E5 File Offset: 0x000032E5
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000050ED File Offset: 0x000032ED
		public DropdownMenuEventInfo eventInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<eventInfo>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<eventInfo>k__BackingField = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000050F6 File Offset: 0x000032F6
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x000050FE File Offset: 0x000032FE
		public object userData
		{
			[CompilerGenerated]
			get
			{
				return this.<userData>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<userData>k__BackingField = value;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005108 File Offset: 0x00003308
		public static DropdownMenuAction.Status AlwaysEnabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Normal;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000511C File Offset: 0x0000331C
		public static DropdownMenuAction.Status AlwaysDisabled(DropdownMenuAction a)
		{
			return DropdownMenuAction.Status.Disabled;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000512F File Offset: 0x0000332F
		public DropdownMenuAction(string actionName, Action<DropdownMenuAction> actionCallback, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			this.name = actionName;
			this.actionCallback = actionCallback;
			this.actionStatusCallback = actionStatusCallback;
			this.userData = userData;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005157 File Offset: 0x00003357
		public void UpdateActionStatus(DropdownMenuEventInfo eventInfo)
		{
			this.eventInfo = eventInfo;
			Func<DropdownMenuAction, DropdownMenuAction.Status> func = this.actionStatusCallback;
			this.status = ((func != null) ? func(this) : DropdownMenuAction.Status.Hidden);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000517C File Offset: 0x0000337C
		public void Execute()
		{
			Action<DropdownMenuAction> action = this.actionCallback;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x0400005B RID: 91
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string <name>k__BackingField;

		// Token: 0x0400005C RID: 92
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private DropdownMenuAction.Status <status>k__BackingField;

		// Token: 0x0400005D RID: 93
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DropdownMenuEventInfo <eventInfo>k__BackingField;

		// Token: 0x0400005E RID: 94
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private object <userData>k__BackingField;

		// Token: 0x0400005F RID: 95
		private readonly Action<DropdownMenuAction> actionCallback;

		// Token: 0x04000060 RID: 96
		private readonly Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback;

		// Token: 0x02000024 RID: 36
		[Flags]
		public enum Status
		{
			// Token: 0x04000062 RID: 98
			None = 0,
			// Token: 0x04000063 RID: 99
			Normal = 1,
			// Token: 0x04000064 RID: 100
			Disabled = 2,
			// Token: 0x04000065 RID: 101
			Checked = 4,
			// Token: 0x04000066 RID: 102
			Hidden = 8
		}
	}
}
