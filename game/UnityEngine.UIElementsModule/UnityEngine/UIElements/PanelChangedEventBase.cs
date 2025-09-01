using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000210 RID: 528
	public abstract class PanelChangedEventBase<T> : EventBase<T>, IPanelChangedEvent where T : PanelChangedEventBase<T>, new()
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x000418E7 File Offset: 0x0003FAE7
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x000418EF File Offset: 0x0003FAEF
		public IPanel originPanel
		{
			[CompilerGenerated]
			get
			{
				return this.<originPanel>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<originPanel>k__BackingField = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x000418F8 File Offset: 0x0003FAF8
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x00041900 File Offset: 0x0003FB00
		public IPanel destinationPanel
		{
			[CompilerGenerated]
			get
			{
				return this.<destinationPanel>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<destinationPanel>k__BackingField = value;
			}
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00041909 File Offset: 0x0003FB09
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0004191A File Offset: 0x0003FB1A
		private void LocalInit()
		{
			this.originPanel = null;
			this.destinationPanel = null;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00041930 File Offset: 0x0003FB30
		public static T GetPooled(IPanel originPanel, IPanel destinationPanel)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.originPanel = originPanel;
			pooled.destinationPanel = destinationPanel;
			return pooled;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00041963 File Offset: 0x0003FB63
		protected PanelChangedEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x0400073B RID: 1851
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private IPanel <originPanel>k__BackingField;

		// Token: 0x0400073C RID: 1852
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private IPanel <destinationPanel>k__BackingField;
	}
}
