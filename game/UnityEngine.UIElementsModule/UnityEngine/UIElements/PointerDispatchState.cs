using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000064 RID: 100
	internal class PointerDispatchState
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000A6C0 File Offset: 0x000088C0
		public PointerDispatchState()
		{
			this.Reset();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000A70C File Offset: 0x0000890C
		internal void Reset()
		{
			for (int i = 0; i < this.m_PointerCapture.Length; i++)
			{
				this.m_PendingPointerCapture[i] = null;
				this.m_PointerCapture[i] = null;
				this.m_ShouldSendCompatibilityMouseEvents[i] = true;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000A750 File Offset: 0x00008950
		public IEventHandler GetCapturingElement(int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId];
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A76C File Offset: 0x0000896C
		public bool HasPointerCapture(IEventHandler handler, int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId] == handler;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A78C File Offset: 0x0000898C
		public void CapturePointer(IEventHandler handler, int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId && this.m_PendingPointerCapture[pointerId] != handler && GUIUtility.hotControl != 0;
			if (flag)
			{
				Debug.LogWarning("Should not be capturing when there is a hotcontrol");
			}
			else
			{
				this.m_PendingPointerCapture[pointerId] = handler;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000A7D3 File Offset: 0x000089D3
		public void ReleasePointer(int pointerId)
		{
			this.m_PendingPointerCapture[pointerId] = null;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A7E0 File Offset: 0x000089E0
		public void ReleasePointer(IEventHandler handler, int pointerId)
		{
			bool flag = handler == this.m_PendingPointerCapture[pointerId];
			if (flag)
			{
				this.m_PendingPointerCapture[pointerId] = null;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A808 File Offset: 0x00008A08
		public void ProcessPointerCapture(int pointerId)
		{
			bool flag = this.m_PointerCapture[pointerId] == this.m_PendingPointerCapture[pointerId];
			if (!flag)
			{
				bool flag2 = this.m_PointerCapture[pointerId] != null;
				if (flag2)
				{
					using (PointerCaptureOutEvent pooled = PointerCaptureEventBase<PointerCaptureOutEvent>.GetPooled(this.m_PointerCapture[pointerId], this.m_PendingPointerCapture[pointerId], pointerId))
					{
						this.m_PointerCapture[pointerId].SendEvent(pooled);
					}
					bool flag3 = pointerId == PointerId.mousePointerId;
					if (flag3)
					{
						using (MouseCaptureOutEvent pooled2 = PointerCaptureEventBase<MouseCaptureOutEvent>.GetPooled(this.m_PointerCapture[pointerId], this.m_PendingPointerCapture[pointerId], pointerId))
						{
							this.m_PointerCapture[pointerId].SendEvent(pooled2);
						}
					}
				}
				bool flag4 = this.m_PendingPointerCapture[pointerId] != null;
				if (flag4)
				{
					using (PointerCaptureEvent pooled3 = PointerCaptureEventBase<PointerCaptureEvent>.GetPooled(this.m_PendingPointerCapture[pointerId], this.m_PointerCapture[pointerId], pointerId))
					{
						this.m_PendingPointerCapture[pointerId].SendEvent(pooled3);
					}
					bool flag5 = pointerId == PointerId.mousePointerId;
					if (flag5)
					{
						using (MouseCaptureEvent pooled4 = PointerCaptureEventBase<MouseCaptureEvent>.GetPooled(this.m_PendingPointerCapture[pointerId], this.m_PointerCapture[pointerId], pointerId))
						{
							this.m_PendingPointerCapture[pointerId].SendEvent(pooled4);
						}
					}
				}
				this.m_PointerCapture[pointerId] = this.m_PendingPointerCapture[pointerId];
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000A99C File Offset: 0x00008B9C
		public void ActivateCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = true;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		public void PreventCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = false;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		public bool ShouldSendCompatibilityMouseEvents(IPointerEvent evt)
		{
			return evt.isPrimary && this.m_ShouldSendCompatibilityMouseEvents[evt.pointerId];
		}

		// Token: 0x0400014E RID: 334
		private IEventHandler[] m_PendingPointerCapture = new IEventHandler[PointerId.maxPointers];

		// Token: 0x0400014F RID: 335
		private IEventHandler[] m_PointerCapture = new IEventHandler[PointerId.maxPointers];

		// Token: 0x04000150 RID: 336
		private bool[] m_ShouldSendCompatibilityMouseEvents = new bool[PointerId.maxPointers];
	}
}
