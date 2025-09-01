using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021F RID: 543
	public sealed class PointerStationaryEvent : PointerEventBase<PointerStationaryEvent>
	{
		// Token: 0x060010C1 RID: 4289 RVA: 0x00042EEC File Offset: 0x000410EC
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00042EFD File Offset: 0x000410FD
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
			((IPointerEventInternal)this).triggeredByOS = true;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00042F18 File Offset: 0x00041118
		public PointerStationaryEvent()
		{
			this.LocalInit();
		}
	}
}
