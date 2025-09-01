using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace QFSW.QC.UI
{
	// Token: 0x02000003 RID: 3
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	public class DraggableUI : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020DC File Offset: 0x000002DC
		public void OnPointerDown(PointerEventData eventData)
		{
			this._isDragging = (this._quantumConsole && this._quantumConsole.KeyConfig && this._quantumConsole.KeyConfig.DragConsoleKey.IsHeld());
			if (this._isDragging)
			{
				this._onBeginDrag.Invoke();
				this._lastPos = eventData.position;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002148 File Offset: 0x00000348
		public void LateUpdate()
		{
			if (this._isDragging)
			{
				Transform transform = this._dragRoot;
				if (!transform)
				{
					transform = (base.transform as RectTransform);
				}
				Vector2 mousePosition = InputHelper.GetMousePosition();
				Vector2 v = mousePosition - this._lastPos;
				this._lastPos = mousePosition;
				if (this._lockInScreen)
				{
					Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
					if (mousePosition.x <= 0f || mousePosition.x >= vector.x)
					{
						v.x = 0f;
					}
					if (mousePosition.y <= 0f || mousePosition.y >= vector.y)
					{
						v.y = 0f;
					}
				}
				transform.Translate(v);
				this._onDrag.Invoke();
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002215 File Offset: 0x00000415
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this._isDragging)
			{
				this._isDragging = false;
				this._onEndDrag.Invoke();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002231 File Offset: 0x00000431
		public DraggableUI()
		{
		}

		// Token: 0x04000004 RID: 4
		[SerializeField]
		private RectTransform _dragRoot;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		private QuantumConsole _quantumConsole;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		private bool _lockInScreen = true;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private UnityEvent _onBeginDrag;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		private UnityEvent _onDrag;

		// Token: 0x04000009 RID: 9
		[SerializeField]
		private UnityEvent _onEndDrag;

		// Token: 0x0400000A RID: 10
		private Vector2 _lastPos;

		// Token: 0x0400000B RID: 11
		private bool _isDragging;
	}
}
