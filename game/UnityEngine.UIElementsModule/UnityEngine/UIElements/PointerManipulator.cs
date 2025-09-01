using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000065 RID: 101
	public abstract class PointerManipulator : MouseManipulator
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		protected bool CanStartManipulation(IPointerEvent e)
		{
			foreach (ManipulatorActivationFilter manipulatorActivationFilter in base.activators)
			{
				bool flag = manipulatorActivationFilter.Matches(e);
				if (flag)
				{
					this.m_CurrentPointerId = e.pointerId;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000AA54 File Offset: 0x00008C54
		protected bool CanStopManipulation(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && e.pointerId == this.m_CurrentPointerId;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000AA80 File Offset: 0x00008C80
		protected PointerManipulator()
		{
		}

		// Token: 0x04000151 RID: 337
		private int m_CurrentPointerId;
	}
}
