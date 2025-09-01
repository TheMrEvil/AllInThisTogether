using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000069 RID: 105
	[RequireComponent(typeof(EventSystem))]
	public abstract class BaseInputModule : UIBehaviour
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x000193B4 File Offset: 0x000175B4
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x000193BC File Offset: 0x000175BC
		internal bool sendPointerHoverToParent
		{
			get
			{
				return this.m_SendPointerHoverToParent;
			}
			set
			{
				this.m_SendPointerHoverToParent = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x000193C8 File Offset: 0x000175C8
		public BaseInput input
		{
			get
			{
				if (this.m_InputOverride != null)
				{
					return this.m_InputOverride;
				}
				if (this.m_DefaultInput == null)
				{
					foreach (BaseInput baseInput in base.GetComponents<BaseInput>())
					{
						if (baseInput != null && baseInput.GetType() == typeof(BaseInput))
						{
							this.m_DefaultInput = baseInput;
							break;
						}
					}
					if (this.m_DefaultInput == null)
					{
						this.m_DefaultInput = base.gameObject.AddComponent<BaseInput>();
					}
				}
				return this.m_DefaultInput;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001945F File Offset: 0x0001765F
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00019467 File Offset: 0x00017667
		public BaseInput inputOverride
		{
			get
			{
				return this.m_InputOverride;
			}
			set
			{
				this.m_InputOverride = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00019470 File Offset: 0x00017670
		protected EventSystem eventSystem
		{
			get
			{
				return this.m_EventSystem;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00019478 File Offset: 0x00017678
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_EventSystem = base.GetComponent<EventSystem>();
			this.m_EventSystem.UpdateModules();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00019497 File Offset: 0x00017697
		protected override void OnDisable()
		{
			this.m_EventSystem.UpdateModules();
			base.OnDisable();
		}

		// Token: 0x06000608 RID: 1544
		public abstract void Process();

		// Token: 0x06000609 RID: 1545 RVA: 0x000194AC File Offset: 0x000176AC
		protected static RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
		{
			int count = candidates.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(candidates[i].gameObject == null))
				{
					return candidates[i];
				}
			}
			return default(RaycastResult);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000194F4 File Offset: 0x000176F4
		protected static MoveDirection DetermineMoveDirection(float x, float y)
		{
			return BaseInputModule.DetermineMoveDirection(x, y, 0.6f);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00019504 File Offset: 0x00017704
		protected static MoveDirection DetermineMoveDirection(float x, float y, float deadZone)
		{
			if (new Vector2(x, y).sqrMagnitude < deadZone * deadZone)
			{
				return MoveDirection.None;
			}
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x <= 0f)
				{
					return MoveDirection.Left;
				}
				return MoveDirection.Right;
			}
			else
			{
				if (y <= 0f)
				{
					return MoveDirection.Down;
				}
				return MoveDirection.Up;
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001954C File Offset: 0x0001774C
		protected static GameObject FindCommonRoot(GameObject g1, GameObject g2)
		{
			if (g1 == null || g2 == null)
			{
				return null;
			}
			Transform transform = g1.transform;
			while (transform != null)
			{
				Transform transform2 = g2.transform;
				while (transform2 != null)
				{
					if (transform == transform2)
					{
						return transform.gameObject;
					}
					transform2 = transform2.parent;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000195B0 File Offset: 0x000177B0
		protected void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject newEnterTarget)
		{
			if (newEnterTarget == null || currentPointerData.pointerEnter == null)
			{
				int count = currentPointerData.hovered.Count;
				for (int i = 0; i < count; i++)
				{
					currentPointerData.fullyExited = true;
					ExecuteEvents.Execute<IPointerMoveHandler>(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerMoveHandler);
					ExecuteEvents.Execute<IPointerExitHandler>(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerExitHandler);
				}
				currentPointerData.hovered.Clear();
				if (newEnterTarget == null)
				{
					currentPointerData.pointerEnter = null;
					return;
				}
			}
			if (currentPointerData.pointerEnter == newEnterTarget && newEnterTarget)
			{
				if (currentPointerData.IsPointerMoving())
				{
					int count2 = currentPointerData.hovered.Count;
					for (int j = 0; j < count2; j++)
					{
						ExecuteEvents.Execute<IPointerMoveHandler>(currentPointerData.hovered[j], currentPointerData, ExecuteEvents.pointerMoveHandler);
					}
				}
				return;
			}
			GameObject gameObject = BaseInputModule.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
			Component component = (Component)newEnterTarget.GetComponentInParent<IPointerExitHandler>();
			GameObject x = (component != null) ? component.gameObject : null;
			if (currentPointerData.pointerEnter != null)
			{
				Transform transform = currentPointerData.pointerEnter.transform;
				while (transform != null && (!this.m_SendPointerHoverToParent || !(gameObject != null) || !(gameObject.transform == transform)) && (this.m_SendPointerHoverToParent || !(x == transform.gameObject)))
				{
					currentPointerData.fullyExited = (transform.gameObject != gameObject && currentPointerData.pointerEnter != newEnterTarget);
					ExecuteEvents.Execute<IPointerMoveHandler>(transform.gameObject, currentPointerData, ExecuteEvents.pointerMoveHandler);
					ExecuteEvents.Execute<IPointerExitHandler>(transform.gameObject, currentPointerData, ExecuteEvents.pointerExitHandler);
					currentPointerData.hovered.Remove(transform.gameObject);
					if (this.m_SendPointerHoverToParent)
					{
						transform = transform.parent;
					}
					if (gameObject != null && gameObject.transform == transform)
					{
						break;
					}
					if (!this.m_SendPointerHoverToParent)
					{
						transform = transform.parent;
					}
				}
			}
			GameObject pointerEnter = currentPointerData.pointerEnter;
			currentPointerData.pointerEnter = newEnterTarget;
			if (newEnterTarget != null)
			{
				Transform transform2 = newEnterTarget.transform;
				while (transform2 != null)
				{
					currentPointerData.reentered = (transform2.gameObject == gameObject && transform2.gameObject != pointerEnter);
					if (this.m_SendPointerHoverToParent && currentPointerData.reentered)
					{
						break;
					}
					ExecuteEvents.Execute<IPointerEnterHandler>(transform2.gameObject, currentPointerData, ExecuteEvents.pointerEnterHandler);
					ExecuteEvents.Execute<IPointerMoveHandler>(transform2.gameObject, currentPointerData, ExecuteEvents.pointerMoveHandler);
					currentPointerData.hovered.Add(transform2.gameObject);
					if (!this.m_SendPointerHoverToParent && transform2.gameObject.GetComponent<IPointerEnterHandler>() != null)
					{
						break;
					}
					if (this.m_SendPointerHoverToParent)
					{
						transform2 = transform2.parent;
					}
					if (gameObject != null && gameObject.transform == transform2)
					{
						break;
					}
					if (!this.m_SendPointerHoverToParent)
					{
						transform2 = transform2.parent;
					}
				}
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000198B8 File Offset: 0x00017AB8
		protected virtual AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
		{
			if (this.m_AxisEventData == null)
			{
				this.m_AxisEventData = new AxisEventData(this.eventSystem);
			}
			this.m_AxisEventData.Reset();
			this.m_AxisEventData.moveVector = new Vector2(x, y);
			this.m_AxisEventData.moveDir = BaseInputModule.DetermineMoveDirection(x, y, moveDeadZone);
			return this.m_AxisEventData;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00019914 File Offset: 0x00017B14
		protected virtual BaseEventData GetBaseEventData()
		{
			if (this.m_BaseEventData == null)
			{
				this.m_BaseEventData = new BaseEventData(this.eventSystem);
			}
			this.m_BaseEventData.Reset();
			return this.m_BaseEventData;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00019940 File Offset: 0x00017B40
		public virtual bool IsPointerOverGameObject(int pointerId)
		{
			return false;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00019943 File Offset: 0x00017B43
		public virtual bool ShouldActivateModule()
		{
			return base.enabled && base.gameObject.activeInHierarchy;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001995A File Offset: 0x00017B5A
		public virtual void DeactivateModule()
		{
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001995C File Offset: 0x00017B5C
		public virtual void ActivateModule()
		{
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001995E File Offset: 0x00017B5E
		public virtual void UpdateModule()
		{
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00019960 File Offset: 0x00017B60
		public virtual bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00019963 File Offset: 0x00017B63
		public virtual int ConvertUIToolkitPointerId(PointerEventData sourcePointerData)
		{
			if (sourcePointerData.pointerId >= 0)
			{
				return PointerId.touchPointerIdBase + sourcePointerData.pointerId;
			}
			return PointerId.mousePointerId;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00019980 File Offset: 0x00017B80
		protected BaseInputModule()
		{
		}

		// Token: 0x04000203 RID: 515
		[NonSerialized]
		protected List<RaycastResult> m_RaycastResultCache = new List<RaycastResult>();

		// Token: 0x04000204 RID: 516
		[SerializeField]
		private bool m_SendPointerHoverToParent = true;

		// Token: 0x04000205 RID: 517
		private AxisEventData m_AxisEventData;

		// Token: 0x04000206 RID: 518
		private EventSystem m_EventSystem;

		// Token: 0x04000207 RID: 519
		private BaseEventData m_BaseEventData;

		// Token: 0x04000208 RID: 520
		protected BaseInput m_InputOverride;

		// Token: 0x04000209 RID: 521
		private BaseInput m_DefaultInput;
	}
}
