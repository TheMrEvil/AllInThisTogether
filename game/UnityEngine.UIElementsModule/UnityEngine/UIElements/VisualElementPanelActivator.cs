using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F1 RID: 241
	internal class VisualElementPanelActivator
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0001B938 File Offset: 0x00019B38
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x0001B940 File Offset: 0x00019B40
		public bool isActive
		{
			[CompilerGenerated]
			get
			{
				return this.<isActive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isActive>k__BackingField = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0001B949 File Offset: 0x00019B49
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x0001B951 File Offset: 0x00019B51
		public bool isDetaching
		{
			[CompilerGenerated]
			get
			{
				return this.<isDetaching>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isDetaching>k__BackingField = value;
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001B95A File Offset: 0x00019B5A
		public VisualElementPanelActivator(IVisualElementPanelActivatable activatable)
		{
			this.m_Activatable = activatable;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001B96C File Offset: 0x00019B6C
		public void SetActive(bool action)
		{
			bool flag = this.isActive != action;
			if (flag)
			{
				this.isActive = action;
				bool isActive = this.isActive;
				if (isActive)
				{
					this.m_Activatable.element.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnEnter), TrickleDown.NoTrickleDown);
					this.m_Activatable.element.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnLeave), TrickleDown.NoTrickleDown);
					this.SendActivation();
				}
				else
				{
					this.m_Activatable.element.UnregisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnEnter), TrickleDown.NoTrickleDown);
					this.m_Activatable.element.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnLeave), TrickleDown.NoTrickleDown);
					this.SendDeactivation();
				}
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001BA30 File Offset: 0x00019C30
		public void SendActivation()
		{
			bool flag = this.m_Activatable.CanBeActivated();
			if (flag)
			{
				this.m_Activatable.OnPanelActivate();
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001BA5C File Offset: 0x00019C5C
		public void SendDeactivation()
		{
			bool flag = this.m_Activatable.CanBeActivated();
			if (flag)
			{
				this.m_Activatable.OnPanelDeactivate();
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001BA88 File Offset: 0x00019C88
		private void OnEnter(AttachToPanelEvent evt)
		{
			bool isActive = this.isActive;
			if (isActive)
			{
				this.SendActivation();
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001BAAC File Offset: 0x00019CAC
		private void OnLeave(DetachFromPanelEvent evt)
		{
			bool isActive = this.isActive;
			if (isActive)
			{
				this.isDetaching = true;
				try
				{
					this.SendDeactivation();
				}
				finally
				{
					this.isDetaching = false;
				}
			}
		}

		// Token: 0x04000307 RID: 775
		private IVisualElementPanelActivatable m_Activatable;

		// Token: 0x04000308 RID: 776
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isActive>k__BackingField;

		// Token: 0x04000309 RID: 777
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <isDetaching>k__BackingField;
	}
}
