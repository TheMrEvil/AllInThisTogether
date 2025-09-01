using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021E RID: 542
	public sealed class PointerMoveEvent : PointerEventBase<PointerMoveEvent>
	{
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00042D60 File Offset: 0x00040F60
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x00042D68 File Offset: 0x00040F68
		internal bool isHandledByDraggable
		{
			[CompilerGenerated]
			get
			{
				return this.<isHandledByDraggable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<isHandledByDraggable>k__BackingField = value;
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00042D71 File Offset: 0x00040F71
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00042D82 File Offset: 0x00040F82
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
			((IPointerEventInternal)this).triggeredByOS = true;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
			this.isHandledByDraggable = false;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00042DA5 File Offset: 0x00040FA5
		public PointerMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00042DB8 File Offset: 0x00040FB8
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				bool flag2 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseDown;
				if (flag2)
				{
					using (MouseDownEvent pooled = MouseDownEvent.GetPooled(this))
					{
						pooled.target = base.target;
						pooled.target.SendEvent(pooled);
					}
				}
				else
				{
					bool flag3 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseUp;
					if (flag3)
					{
						using (MouseUpEvent pooled2 = MouseUpEvent.GetPooled(this))
						{
							pooled2.target = base.target;
							pooled2.target.SendEvent(pooled2);
						}
					}
					else
					{
						using (MouseMoveEvent pooled3 = MouseMoveEvent.GetPooled(this))
						{
							pooled3.target = base.target;
							pooled3.target.SendEvent(pooled3);
						}
					}
				}
			}
			base.PostDispatch(panel);
		}

		// Token: 0x04000766 RID: 1894
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <isHandledByDraggable>k__BackingField;
	}
}
