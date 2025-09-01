using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CA RID: 458
	public abstract class CommandEventBase<T> : EventBase<T>, ICommandEvent where T : CommandEventBase<T>, new()
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0003D2C0 File Offset: 0x0003B4C0
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x0003D2FF File Offset: 0x0003B4FF
		public string commandName
		{
			get
			{
				bool flag = this.m_CommandName == null && base.imguiEvent != null;
				string commandName;
				if (flag)
				{
					commandName = base.imguiEvent.commandName;
				}
				else
				{
					commandName = this.m_CommandName;
				}
				return commandName;
			}
			protected set
			{
				this.m_CommandName = value;
			}
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003D309 File Offset: 0x0003B509
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003D31A File Offset: 0x0003B51A
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
			this.commandName = null;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003D330 File Offset: 0x0003B530
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003D358 File Offset: 0x0003B558
		public static T GetPooled(string commandName)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.commandName = commandName;
			return pooled;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003D37E File Offset: 0x0003B57E
		protected CommandEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x040006C3 RID: 1731
		private string m_CommandName;
	}
}
