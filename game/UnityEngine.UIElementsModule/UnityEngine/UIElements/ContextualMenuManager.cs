using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000014 RID: 20
	public abstract class ContextualMenuManager
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003CEC File Offset: 0x00001EEC
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003CF4 File Offset: 0x00001EF4
		internal bool displayMenuHandledOSX
		{
			[CompilerGenerated]
			get
			{
				return this.<displayMenuHandledOSX>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<displayMenuHandledOSX>k__BackingField = value;
			}
		}

		// Token: 0x06000085 RID: 133
		public abstract void DisplayMenuIfEventMatches(EventBase evt, IEventHandler eventHandler);

		// Token: 0x06000086 RID: 134 RVA: 0x00003D00 File Offset: 0x00001F00
		public void DisplayMenu(EventBase triggerEvent, IEventHandler target)
		{
			DropdownMenu menu = new DropdownMenu();
			using (ContextualMenuPopulateEvent pooled = ContextualMenuPopulateEvent.GetPooled(triggerEvent, menu, target, this))
			{
				if (target != null)
				{
					target.SendEvent(pooled);
				}
			}
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
			if (flag)
			{
				this.displayMenuHandledOSX = true;
			}
		}

		// Token: 0x06000087 RID: 135
		protected internal abstract void DoDisplayMenu(DropdownMenu menu, EventBase triggerEvent);

		// Token: 0x06000088 RID: 136 RVA: 0x000020C2 File Offset: 0x000002C2
		protected ContextualMenuManager()
		{
		}

		// Token: 0x04000036 RID: 54
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <displayMenuHandledOSX>k__BackingField;
	}
}
