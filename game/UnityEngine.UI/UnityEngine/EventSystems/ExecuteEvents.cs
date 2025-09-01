using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000067 RID: 103
	public static class ExecuteEvents
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x00018DD5 File Offset: 0x00016FD5
		public static T ValidateEventData<T>(BaseEventData data) where T : class
		{
			if (!(data is T))
			{
				throw new ArgumentException(string.Format("Invalid type: {0} passed to event expecting {1}", data.GetType(), typeof(T)));
			}
			return data as T;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00018E14 File Offset: 0x00017014
		private static void Execute(IPointerMoveHandler handler, BaseEventData eventData)
		{
			handler.OnPointerMove(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00018E22 File Offset: 0x00017022
		private static void Execute(IPointerEnterHandler handler, BaseEventData eventData)
		{
			handler.OnPointerEnter(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00018E30 File Offset: 0x00017030
		private static void Execute(IPointerExitHandler handler, BaseEventData eventData)
		{
			handler.OnPointerExit(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018E3E File Offset: 0x0001703E
		private static void Execute(IPointerDownHandler handler, BaseEventData eventData)
		{
			handler.OnPointerDown(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00018E4C File Offset: 0x0001704C
		private static void Execute(IPointerUpHandler handler, BaseEventData eventData)
		{
			handler.OnPointerUp(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00018E5A File Offset: 0x0001705A
		private static void Execute(IPointerClickHandler handler, BaseEventData eventData)
		{
			handler.OnPointerClick(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00018E68 File Offset: 0x00017068
		private static void Execute(IInitializePotentialDragHandler handler, BaseEventData eventData)
		{
			handler.OnInitializePotentialDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00018E76 File Offset: 0x00017076
		private static void Execute(IBeginDragHandler handler, BaseEventData eventData)
		{
			handler.OnBeginDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00018E84 File Offset: 0x00017084
		private static void Execute(IDragHandler handler, BaseEventData eventData)
		{
			handler.OnDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00018E92 File Offset: 0x00017092
		private static void Execute(IEndDragHandler handler, BaseEventData eventData)
		{
			handler.OnEndDrag(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00018EA0 File Offset: 0x000170A0
		private static void Execute(IDropHandler handler, BaseEventData eventData)
		{
			handler.OnDrop(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00018EAE File Offset: 0x000170AE
		private static void Execute(IScrollHandler handler, BaseEventData eventData)
		{
			handler.OnScroll(ExecuteEvents.ValidateEventData<PointerEventData>(eventData));
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00018EBC File Offset: 0x000170BC
		private static void Execute(IUpdateSelectedHandler handler, BaseEventData eventData)
		{
			handler.OnUpdateSelected(eventData);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00018EC5 File Offset: 0x000170C5
		private static void Execute(ISelectHandler handler, BaseEventData eventData)
		{
			handler.OnSelect(eventData);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00018ECE File Offset: 0x000170CE
		private static void Execute(IDeselectHandler handler, BaseEventData eventData)
		{
			handler.OnDeselect(eventData);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00018ED7 File Offset: 0x000170D7
		private static void Execute(IMoveHandler handler, BaseEventData eventData)
		{
			handler.OnMove(ExecuteEvents.ValidateEventData<AxisEventData>(eventData));
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00018EE5 File Offset: 0x000170E5
		private static void Execute(ISubmitHandler handler, BaseEventData eventData)
		{
			handler.OnSubmit(eventData);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00018EEE File Offset: 0x000170EE
		private static void Execute(ICancelHandler handler, BaseEventData eventData)
		{
			handler.OnCancel(eventData);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00018EF7 File Offset: 0x000170F7
		public static ExecuteEvents.EventFunction<IPointerMoveHandler> pointerMoveHandler
		{
			get
			{
				return ExecuteEvents.s_PointerMoveHandler;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00018EFE File Offset: 0x000170FE
		public static ExecuteEvents.EventFunction<IPointerEnterHandler> pointerEnterHandler
		{
			get
			{
				return ExecuteEvents.s_PointerEnterHandler;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00018F05 File Offset: 0x00017105
		public static ExecuteEvents.EventFunction<IPointerExitHandler> pointerExitHandler
		{
			get
			{
				return ExecuteEvents.s_PointerExitHandler;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00018F0C File Offset: 0x0001710C
		public static ExecuteEvents.EventFunction<IPointerDownHandler> pointerDownHandler
		{
			get
			{
				return ExecuteEvents.s_PointerDownHandler;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00018F13 File Offset: 0x00017113
		public static ExecuteEvents.EventFunction<IPointerUpHandler> pointerUpHandler
		{
			get
			{
				return ExecuteEvents.s_PointerUpHandler;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00018F1A File Offset: 0x0001711A
		public static ExecuteEvents.EventFunction<IPointerClickHandler> pointerClickHandler
		{
			get
			{
				return ExecuteEvents.s_PointerClickHandler;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00018F21 File Offset: 0x00017121
		public static ExecuteEvents.EventFunction<IInitializePotentialDragHandler> initializePotentialDrag
		{
			get
			{
				return ExecuteEvents.s_InitializePotentialDragHandler;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00018F28 File Offset: 0x00017128
		public static ExecuteEvents.EventFunction<IBeginDragHandler> beginDragHandler
		{
			get
			{
				return ExecuteEvents.s_BeginDragHandler;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00018F2F File Offset: 0x0001712F
		public static ExecuteEvents.EventFunction<IDragHandler> dragHandler
		{
			get
			{
				return ExecuteEvents.s_DragHandler;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00018F36 File Offset: 0x00017136
		public static ExecuteEvents.EventFunction<IEndDragHandler> endDragHandler
		{
			get
			{
				return ExecuteEvents.s_EndDragHandler;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00018F3D File Offset: 0x0001713D
		public static ExecuteEvents.EventFunction<IDropHandler> dropHandler
		{
			get
			{
				return ExecuteEvents.s_DropHandler;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00018F44 File Offset: 0x00017144
		public static ExecuteEvents.EventFunction<IScrollHandler> scrollHandler
		{
			get
			{
				return ExecuteEvents.s_ScrollHandler;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00018F4B File Offset: 0x0001714B
		public static ExecuteEvents.EventFunction<IUpdateSelectedHandler> updateSelectedHandler
		{
			get
			{
				return ExecuteEvents.s_UpdateSelectedHandler;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00018F52 File Offset: 0x00017152
		public static ExecuteEvents.EventFunction<ISelectHandler> selectHandler
		{
			get
			{
				return ExecuteEvents.s_SelectHandler;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00018F59 File Offset: 0x00017159
		public static ExecuteEvents.EventFunction<IDeselectHandler> deselectHandler
		{
			get
			{
				return ExecuteEvents.s_DeselectHandler;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00018F60 File Offset: 0x00017160
		public static ExecuteEvents.EventFunction<IMoveHandler> moveHandler
		{
			get
			{
				return ExecuteEvents.s_MoveHandler;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00018F67 File Offset: 0x00017167
		public static ExecuteEvents.EventFunction<ISubmitHandler> submitHandler
		{
			get
			{
				return ExecuteEvents.s_SubmitHandler;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00018F6E File Offset: 0x0001716E
		public static ExecuteEvents.EventFunction<ICancelHandler> cancelHandler
		{
			get
			{
				return ExecuteEvents.s_CancelHandler;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00018F78 File Offset: 0x00017178
		private static void GetEventChain(GameObject root, IList<Transform> eventChain)
		{
			eventChain.Clear();
			if (root == null)
			{
				return;
			}
			Transform transform = root.transform;
			while (transform != null)
			{
				eventChain.Add(transform);
				transform = transform.parent;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00018FB8 File Offset: 0x000171B8
		public static bool Execute<T>(GameObject target, BaseEventData eventData, ExecuteEvents.EventFunction<T> functor) where T : IEventSystemHandler
		{
			List<IEventSystemHandler> list = CollectionPool<List<IEventSystemHandler>, IEventSystemHandler>.Get();
			ExecuteEvents.GetEventList<T>(target, list);
			int count = list.Count;
			int i = 0;
			while (i < count)
			{
				T handler;
				try
				{
					handler = (T)((object)list[i]);
				}
				catch (Exception innerException)
				{
					IEventSystemHandler eventSystemHandler = list[i];
					Debug.LogException(new Exception(string.Format("Type {0} expected {1} received.", typeof(T).Name, eventSystemHandler.GetType().Name), innerException));
					goto IL_78;
				}
				goto IL_66;
				IL_78:
				i++;
				continue;
				IL_66:
				try
				{
					functor(handler, eventData);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
				goto IL_78;
			}
			int count2 = list.Count;
			CollectionPool<List<IEventSystemHandler>, IEventSystemHandler>.Release(list);
			return count2 > 0;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00019070 File Offset: 0x00017270
		public static GameObject ExecuteHierarchy<T>(GameObject root, BaseEventData eventData, ExecuteEvents.EventFunction<T> callbackFunction) where T : IEventSystemHandler
		{
			ExecuteEvents.GetEventChain(root, ExecuteEvents.s_InternalTransformList);
			int count = ExecuteEvents.s_InternalTransformList.Count;
			for (int i = 0; i < count; i++)
			{
				Transform transform = ExecuteEvents.s_InternalTransformList[i];
				if (ExecuteEvents.Execute<T>(transform.gameObject, eventData, callbackFunction))
				{
					return transform.gameObject;
				}
			}
			return null;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000190C4 File Offset: 0x000172C4
		private static bool ShouldSendToComponent<T>(Component component) where T : IEventSystemHandler
		{
			if (!(component is T))
			{
				return false;
			}
			Behaviour behaviour = component as Behaviour;
			return !(behaviour != null) || behaviour.isActiveAndEnabled;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000190F8 File Offset: 0x000172F8
		private static void GetEventList<T>(GameObject go, IList<IEventSystemHandler> results) where T : IEventSystemHandler
		{
			if (results == null)
			{
				throw new ArgumentException("Results array is null", "results");
			}
			if (go == null || !go.activeInHierarchy)
			{
				return;
			}
			List<Component> list = CollectionPool<List<Component>, Component>.Get();
			go.GetComponents<Component>(list);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				if (ExecuteEvents.ShouldSendToComponent<T>(list[i]))
				{
					results.Add(list[i] as IEventSystemHandler);
				}
			}
			CollectionPool<List<Component>, Component>.Release(list);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00019170 File Offset: 0x00017370
		public static bool CanHandleEvent<T>(GameObject go) where T : IEventSystemHandler
		{
			List<IEventSystemHandler> list = CollectionPool<List<IEventSystemHandler>, IEventSystemHandler>.Get();
			ExecuteEvents.GetEventList<T>(go, list);
			int count = list.Count;
			CollectionPool<List<IEventSystemHandler>, IEventSystemHandler>.Release(list);
			return count != 0;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001919C File Offset: 0x0001739C
		public static GameObject GetEventHandler<T>(GameObject root) where T : IEventSystemHandler
		{
			if (root == null)
			{
				return null;
			}
			Transform transform = root.transform;
			while (transform != null)
			{
				if (ExecuteEvents.CanHandleEvent<T>(transform.gameObject))
				{
					return transform.gameObject;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000191E4 File Offset: 0x000173E4
		// Note: this type is marked as 'beforefieldinit'.
		static ExecuteEvents()
		{
		}

		// Token: 0x040001F0 RID: 496
		private static readonly ExecuteEvents.EventFunction<IPointerMoveHandler> s_PointerMoveHandler = new ExecuteEvents.EventFunction<IPointerMoveHandler>(ExecuteEvents.Execute);

		// Token: 0x040001F1 RID: 497
		private static readonly ExecuteEvents.EventFunction<IPointerEnterHandler> s_PointerEnterHandler = new ExecuteEvents.EventFunction<IPointerEnterHandler>(ExecuteEvents.Execute);

		// Token: 0x040001F2 RID: 498
		private static readonly ExecuteEvents.EventFunction<IPointerExitHandler> s_PointerExitHandler = new ExecuteEvents.EventFunction<IPointerExitHandler>(ExecuteEvents.Execute);

		// Token: 0x040001F3 RID: 499
		private static readonly ExecuteEvents.EventFunction<IPointerDownHandler> s_PointerDownHandler = new ExecuteEvents.EventFunction<IPointerDownHandler>(ExecuteEvents.Execute);

		// Token: 0x040001F4 RID: 500
		private static readonly ExecuteEvents.EventFunction<IPointerUpHandler> s_PointerUpHandler = new ExecuteEvents.EventFunction<IPointerUpHandler>(ExecuteEvents.Execute);

		// Token: 0x040001F5 RID: 501
		private static readonly ExecuteEvents.EventFunction<IPointerClickHandler> s_PointerClickHandler = new ExecuteEvents.EventFunction<IPointerClickHandler>(ExecuteEvents.Execute);

		// Token: 0x040001F6 RID: 502
		private static readonly ExecuteEvents.EventFunction<IInitializePotentialDragHandler> s_InitializePotentialDragHandler = new ExecuteEvents.EventFunction<IInitializePotentialDragHandler>(ExecuteEvents.Execute);

		// Token: 0x040001F7 RID: 503
		private static readonly ExecuteEvents.EventFunction<IBeginDragHandler> s_BeginDragHandler = new ExecuteEvents.EventFunction<IBeginDragHandler>(ExecuteEvents.Execute);

		// Token: 0x040001F8 RID: 504
		private static readonly ExecuteEvents.EventFunction<IDragHandler> s_DragHandler = new ExecuteEvents.EventFunction<IDragHandler>(ExecuteEvents.Execute);

		// Token: 0x040001F9 RID: 505
		private static readonly ExecuteEvents.EventFunction<IEndDragHandler> s_EndDragHandler = new ExecuteEvents.EventFunction<IEndDragHandler>(ExecuteEvents.Execute);

		// Token: 0x040001FA RID: 506
		private static readonly ExecuteEvents.EventFunction<IDropHandler> s_DropHandler = new ExecuteEvents.EventFunction<IDropHandler>(ExecuteEvents.Execute);

		// Token: 0x040001FB RID: 507
		private static readonly ExecuteEvents.EventFunction<IScrollHandler> s_ScrollHandler = new ExecuteEvents.EventFunction<IScrollHandler>(ExecuteEvents.Execute);

		// Token: 0x040001FC RID: 508
		private static readonly ExecuteEvents.EventFunction<IUpdateSelectedHandler> s_UpdateSelectedHandler = new ExecuteEvents.EventFunction<IUpdateSelectedHandler>(ExecuteEvents.Execute);

		// Token: 0x040001FD RID: 509
		private static readonly ExecuteEvents.EventFunction<ISelectHandler> s_SelectHandler = new ExecuteEvents.EventFunction<ISelectHandler>(ExecuteEvents.Execute);

		// Token: 0x040001FE RID: 510
		private static readonly ExecuteEvents.EventFunction<IDeselectHandler> s_DeselectHandler = new ExecuteEvents.EventFunction<IDeselectHandler>(ExecuteEvents.Execute);

		// Token: 0x040001FF RID: 511
		private static readonly ExecuteEvents.EventFunction<IMoveHandler> s_MoveHandler = new ExecuteEvents.EventFunction<IMoveHandler>(ExecuteEvents.Execute);

		// Token: 0x04000200 RID: 512
		private static readonly ExecuteEvents.EventFunction<ISubmitHandler> s_SubmitHandler = new ExecuteEvents.EventFunction<ISubmitHandler>(ExecuteEvents.Execute);

		// Token: 0x04000201 RID: 513
		private static readonly ExecuteEvents.EventFunction<ICancelHandler> s_CancelHandler = new ExecuteEvents.EventFunction<ICancelHandler>(ExecuteEvents.Execute);

		// Token: 0x04000202 RID: 514
		private static readonly List<Transform> s_InternalTransformList = new List<Transform>(30);

		// Token: 0x020000C5 RID: 197
		// (Invoke) Token: 0x06000746 RID: 1862
		public delegate void EventFunction<T1>(T1 handler, BaseEventData eventData);
	}
}
