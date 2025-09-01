using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000012 RID: 18
	internal class ClickDetector
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003871 File Offset: 0x00001A71
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003878 File Offset: 0x00001A78
		internal static int s_DoubleClickTime
		{
			[CompilerGenerated]
			get
			{
				return ClickDetector.<s_DoubleClickTime>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				ClickDetector.<s_DoubleClickTime>k__BackingField = value;
			}
		} = -1;

		// Token: 0x06000079 RID: 121 RVA: 0x00003880 File Offset: 0x00001A80
		public ClickDetector()
		{
			this.m_ClickStatus = new List<ClickDetector.ButtonClickStatus>(PointerId.maxPointers);
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				this.m_ClickStatus.Add(new ClickDetector.ButtonClickStatus());
			}
			bool flag = ClickDetector.s_DoubleClickTime == -1;
			if (flag)
			{
				ClickDetector.s_DoubleClickTime = Event.GetDoubleClickTime();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000038E8 File Offset: 0x00001AE8
		private void StartClickTracking(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				VisualElement visualElement = evt.target as VisualElement;
				bool flag2 = visualElement != buttonClickStatus.m_Target;
				if (flag2)
				{
					buttonClickStatus.Reset();
				}
				buttonClickStatus.m_Target = visualElement;
				bool flag3 = evt.timestamp - buttonClickStatus.m_LastPointerDownTime > (long)ClickDetector.s_DoubleClickTime;
				if (flag3)
				{
					buttonClickStatus.m_ClickCount = 1;
				}
				else
				{
					buttonClickStatus.m_ClickCount++;
				}
				buttonClickStatus.m_LastPointerDownTime = evt.timestamp;
				buttonClickStatus.m_PointerDownPosition = pointerEvent.position;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000399C File Offset: 0x00001B9C
		private void SendClickEvent(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				VisualElement visualElement = evt.target as VisualElement;
				bool flag2 = visualElement != null && ClickDetector.ContainsPointer(visualElement, pointerEvent.position);
				if (flag2)
				{
					bool flag3 = buttonClickStatus.m_Target != null && buttonClickStatus.m_ClickCount > 0;
					if (flag3)
					{
						VisualElement visualElement2 = buttonClickStatus.m_Target.FindCommonAncestor(evt.target as VisualElement);
						bool flag4 = visualElement2 != null;
						if (flag4)
						{
							using (ClickEvent pooled = ClickEvent.GetPooled(evt as PointerUpEvent, buttonClickStatus.m_ClickCount))
							{
								pooled.target = visualElement2;
								visualElement2.SendEvent(pooled);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003A88 File Offset: 0x00001C88
		private void CancelClickTracking(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
				buttonClickStatus.Reset();
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public void ProcessEvent(EventBase evt)
		{
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag = pointerEvent == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<PointerDownEvent>.TypeId() && pointerEvent.button == 0;
				if (flag2)
				{
					this.StartClickTracking(evt);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<PointerMoveEvent>.TypeId();
					if (flag3)
					{
						bool flag4 = pointerEvent.button == 0 && (pointerEvent.pressedButtons & 1) == 1;
						if (flag4)
						{
							this.StartClickTracking(evt);
						}
						else
						{
							bool flag5 = pointerEvent.button == 0 && (pointerEvent.pressedButtons & 1) == 0;
							if (flag5)
							{
								this.SendClickEvent(evt);
							}
							else
							{
								ClickDetector.ButtonClickStatus buttonClickStatus = this.m_ClickStatus[pointerEvent.pointerId];
								bool flag6 = buttonClickStatus.m_Target != null;
								if (flag6)
								{
									buttonClickStatus.m_LastPointerDownTime = 0L;
								}
							}
						}
					}
					else
					{
						bool flag7 = evt.eventTypeId == EventBase<PointerCancelEvent>.TypeId();
						if (flag7)
						{
							this.CancelClickTracking(evt);
						}
						else
						{
							bool flag8 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() && pointerEvent.button == 0;
							if (flag8)
							{
								this.SendClickEvent(evt);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003BF4 File Offset: 0x00001DF4
		private static bool ContainsPointer(VisualElement element, Vector2 position)
		{
			bool flag = !element.worldBound.Contains(position) || element.panel == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				VisualElement visualElement = element.panel.Pick(position);
				result = (element == visualElement || element.Contains(visualElement));
			}
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003C48 File Offset: 0x00001E48
		internal void Cleanup(List<VisualElement> elements)
		{
			foreach (ClickDetector.ButtonClickStatus buttonClickStatus in this.m_ClickStatus)
			{
				bool flag = buttonClickStatus.m_Target == null;
				if (!flag)
				{
					bool flag2 = elements.Contains(buttonClickStatus.m_Target);
					if (flag2)
					{
						buttonClickStatus.Reset();
					}
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003CC0 File Offset: 0x00001EC0
		// Note: this type is marked as 'beforefieldinit'.
		static ClickDetector()
		{
		}

		// Token: 0x04000030 RID: 48
		private List<ClickDetector.ButtonClickStatus> m_ClickStatus;

		// Token: 0x04000031 RID: 49
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static int <s_DoubleClickTime>k__BackingField;

		// Token: 0x02000013 RID: 19
		private class ButtonClickStatus
		{
			// Token: 0x06000081 RID: 129 RVA: 0x00003CC8 File Offset: 0x00001EC8
			public void Reset()
			{
				this.m_Target = null;
				this.m_ClickCount = 0;
				this.m_LastPointerDownTime = 0L;
				this.m_PointerDownPosition = Vector3.zero;
			}

			// Token: 0x06000082 RID: 130 RVA: 0x000020C2 File Offset: 0x000002C2
			public ButtonClickStatus()
			{
			}

			// Token: 0x04000032 RID: 50
			public VisualElement m_Target;

			// Token: 0x04000033 RID: 51
			public Vector3 m_PointerDownPosition;

			// Token: 0x04000034 RID: 52
			public long m_LastPointerDownTime;

			// Token: 0x04000035 RID: 53
			public int m_ClickCount;
		}
	}
}
