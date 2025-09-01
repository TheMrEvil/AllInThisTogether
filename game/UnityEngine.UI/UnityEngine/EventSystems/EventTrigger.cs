using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000065 RID: 101
	[AddComponentMenu("Event/Event Trigger")]
	public class EventTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00018C8A File Offset: 0x00016E8A
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00018C92 File Offset: 0x00016E92
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use triggers instead (UnityUpgradable) -> triggers", true)]
		public List<EventTrigger.Entry> delegates
		{
			get
			{
				return this.triggers;
			}
			set
			{
				this.triggers = value;
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00018C9B File Offset: 0x00016E9B
		protected EventTrigger()
		{
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00018CA3 File Offset: 0x00016EA3
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00018CBE File Offset: 0x00016EBE
		public List<EventTrigger.Entry> triggers
		{
			get
			{
				if (this.m_Delegates == null)
				{
					this.m_Delegates = new List<EventTrigger.Entry>();
				}
				return this.m_Delegates;
			}
			set
			{
				this.m_Delegates = value;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00018CC8 File Offset: 0x00016EC8
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			int count = this.triggers.Count;
			int i = 0;
			int count2 = this.triggers.Count;
			while (i < count2)
			{
				EventTrigger.Entry entry = this.triggers[i];
				if (entry.eventID == id && entry.callback != null)
				{
					entry.callback.Invoke(eventData);
				}
				i++;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018D23 File Offset: 0x00016F23
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00018D2D File Offset: 0x00016F2D
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00018D37 File Offset: 0x00016F37
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drag, eventData);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018D41 File Offset: 0x00016F41
		public virtual void OnDrop(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drop, eventData);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00018D4B File Offset: 0x00016F4B
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerDown, eventData);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00018D55 File Offset: 0x00016F55
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerUp, eventData);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00018D5F File Offset: 0x00016F5F
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00018D69 File Offset: 0x00016F69
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Select, eventData);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00018D74 File Offset: 0x00016F74
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Deselect, eventData);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00018D7F File Offset: 0x00016F7F
		public virtual void OnScroll(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Scroll, eventData);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00018D89 File Offset: 0x00016F89
		public virtual void OnMove(AxisEventData eventData)
		{
			this.Execute(EventTriggerType.Move, eventData);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00018D94 File Offset: 0x00016F94
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.UpdateSelected, eventData);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00018D9E File Offset: 0x00016F9E
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.InitializePotentialDrag, eventData);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018DA9 File Offset: 0x00016FA9
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.BeginDrag, eventData);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018DB4 File Offset: 0x00016FB4
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.EndDrag, eventData);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018DBF File Offset: 0x00016FBF
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Submit, eventData);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00018DCA File Offset: 0x00016FCA
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Cancel, eventData);
		}

		// Token: 0x040001DD RID: 477
		[FormerlySerializedAs("delegates")]
		[SerializeField]
		private List<EventTrigger.Entry> m_Delegates;

		// Token: 0x020000C3 RID: 195
		[Serializable]
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
			// Token: 0x06000743 RID: 1859 RVA: 0x0001C7D7 File Offset: 0x0001A9D7
			public TriggerEvent()
			{
			}
		}

		// Token: 0x020000C4 RID: 196
		[Serializable]
		public class Entry
		{
			// Token: 0x06000744 RID: 1860 RVA: 0x0001C7DF File Offset: 0x0001A9DF
			public Entry()
			{
			}

			// Token: 0x04000345 RID: 837
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x04000346 RID: 838
			public EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
		}
	}
}
