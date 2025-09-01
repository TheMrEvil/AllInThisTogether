using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022A RID: 554
	public class TooltipEvent : EventBase<TooltipEvent>
	{
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x000433A2 File Offset: 0x000415A2
		// (set) Token: 0x060010E1 RID: 4321 RVA: 0x000433AA File Offset: 0x000415AA
		public string tooltip
		{
			[CompilerGenerated]
			get
			{
				return this.<tooltip>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<tooltip>k__BackingField = value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x000433B3 File Offset: 0x000415B3
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x000433BB File Offset: 0x000415BB
		public Rect rect
		{
			[CompilerGenerated]
			get
			{
				return this.<rect>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<rect>k__BackingField = value;
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000433C4 File Offset: 0x000415C4
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x000433D8 File Offset: 0x000415D8
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
			this.rect = default(Rect);
			this.tooltip = string.Empty;
			base.ignoreCompositeRoots = true;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00043414 File Offset: 0x00041614
		internal static TooltipEvent GetPooled(string tooltip, Rect rect)
		{
			TooltipEvent pooled = EventBase<TooltipEvent>.GetPooled();
			pooled.tooltip = tooltip;
			pooled.rect = rect;
			return pooled;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0004343D File Offset: 0x0004163D
		public TooltipEvent()
		{
			this.LocalInit();
		}

		// Token: 0x04000771 RID: 1905
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <tooltip>k__BackingField;

		// Token: 0x04000772 RID: 1906
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Rect <rect>k__BackingField;
	}
}
