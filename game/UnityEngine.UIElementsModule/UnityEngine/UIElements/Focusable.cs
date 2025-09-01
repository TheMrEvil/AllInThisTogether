using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002E RID: 46
	public abstract class Focusable : CallbackEventHandler
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00005BFC File Offset: 0x00003DFC
		protected Focusable()
		{
			this.focusable = true;
			this.tabIndex = 0;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000115 RID: 277
		public abstract FocusController focusController { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005C1D File Offset: 0x00003E1D
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00005C25 File Offset: 0x00003E25
		public bool focusable
		{
			[CompilerGenerated]
			get
			{
				return this.<focusable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<focusable>k__BackingField = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005C2E File Offset: 0x00003E2E
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005C36 File Offset: 0x00003E36
		public int tabIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<tabIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<tabIndex>k__BackingField = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005C40 File Offset: 0x00003E40
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005C58 File Offset: 0x00003E58
		public bool delegatesFocus
		{
			get
			{
				return this.m_DelegatesFocus;
			}
			set
			{
				this.m_DelegatesFocus = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005C64 File Offset: 0x00003E64
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00005C7C File Offset: 0x00003E7C
		internal bool excludeFromFocusRing
		{
			get
			{
				return this.m_ExcludeFromFocusRing;
			}
			set
			{
				bool flag = !((VisualElement)this).isCompositeRoot;
				if (flag)
				{
					throw new InvalidOperationException("excludeFromFocusRing should only be set on composite roots.");
				}
				this.m_ExcludeFromFocusRing = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005CAF File Offset: 0x00003EAF
		public virtual bool canGrabFocus
		{
			get
			{
				return this.focusable;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public virtual void Focus()
		{
			bool flag = this.focusController != null;
			if (flag)
			{
				bool canGrabFocus = this.canGrabFocus;
				if (canGrabFocus)
				{
					Focusable focusDelegate = this.GetFocusDelegate();
					this.focusController.SwitchFocus(focusDelegate, this != focusDelegate, DispatchMode.Default);
				}
				else
				{
					this.focusController.SwitchFocus(null, false, DispatchMode.Default);
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005D10 File Offset: 0x00003F10
		public virtual void Blur()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.Blur(this, false, DispatchMode.Default);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005D28 File Offset: 0x00003F28
		internal void BlurImmediately()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.Blur(this, false, DispatchMode.Immediate);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005D40 File Offset: 0x00003F40
		private Focusable GetFocusDelegate()
		{
			Focusable focusable = this;
			while (focusable != null && focusable.delegatesFocus)
			{
				focusable = Focusable.GetFirstFocusableChild(focusable as VisualElement);
			}
			return focusable;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005D78 File Offset: 0x00003F78
		private static Focusable GetFirstFocusableChild(VisualElement ve)
		{
			int childCount = ve.hierarchy.childCount;
			int i = 0;
			while (i < childCount)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = visualElement.canGrabFocus && visualElement.tabIndex >= 0;
				if (!flag)
				{
					bool flag2 = visualElement.hierarchy.parent != null && visualElement == visualElement.hierarchy.parent.contentContainer;
					bool flag3 = !visualElement.isCompositeRoot && !flag2;
					if (flag3)
					{
						Focusable firstFocusableChild = Focusable.GetFirstFocusableChild(visualElement);
						bool flag4 = firstFocusableChild != null;
						if (flag4)
						{
							return firstFocusableChild;
						}
					}
					i++;
					continue;
				}
				return visualElement;
			}
			return null;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005E46 File Offset: 0x00004046
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			this.ProcessEvent(evt);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005E59 File Offset: 0x00004059
		internal override void ExecuteDefaultActionDisabled(EventBase evt)
		{
			base.ExecuteDefaultActionDisabled(evt);
			this.ProcessEvent(evt);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005E6C File Offset: 0x0000406C
		private void ProcessEvent(EventBase evt)
		{
			bool flag = evt != null && evt.target == evt.leafTarget;
			if (flag)
			{
				FocusController focusController = this.focusController;
				if (focusController != null)
				{
					focusController.SwitchFocusOnEvent(evt);
				}
			}
		}

		// Token: 0x0400007F RID: 127
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <focusable>k__BackingField;

		// Token: 0x04000080 RID: 128
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <tabIndex>k__BackingField;

		// Token: 0x04000081 RID: 129
		private bool m_DelegatesFocus;

		// Token: 0x04000082 RID: 130
		private bool m_ExcludeFromFocusRing;

		// Token: 0x04000083 RID: 131
		internal bool isIMGUIContainer = false;
	}
}
