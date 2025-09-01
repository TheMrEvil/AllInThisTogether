using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C3 RID: 451
	public abstract class MouseCaptureEventBase<T> : PointerCaptureEventBase<T>, IMouseCaptureEvent where T : MouseCaptureEventBase<T>, new()
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0003D0A3 File Offset: 0x0003B2A3
		public new IEventHandler relatedTarget
		{
			get
			{
				return base.relatedTarget;
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003D0AC File Offset: 0x0003B2AC
		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget)
		{
			return PointerCaptureEventBase<T>.GetPooled(target, relatedTarget, 0);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003D0C8 File Offset: 0x0003B2C8
		protected override void Init()
		{
			base.Init();
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003D0D2 File Offset: 0x0003B2D2
		protected MouseCaptureEventBase()
		{
		}
	}
}
