using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QFSW.QC.UI
{
	// Token: 0x02000005 RID: 5
	[DisallowMultipleComponent]
	public class ResizableUI : MonoBehaviour, IDragHandler, IEventSystemHandler
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000023E0 File Offset: 0x000005E0
		public void OnDrag(PointerEventData eventData)
		{
			Vector2 vector = (this._resizeRoot.offsetMin + this._minSize) * this._resizeCanvas.scaleFactor;
			Vector2 vector2 = this._lockInScreen ? new Vector2((float)Screen.width, (float)Screen.height) : new Vector2(float.PositiveInfinity, float.PositiveInfinity);
			Vector2 delta = eventData.delta;
			Vector2 position = eventData.position;
			Vector2 vector3 = position - delta;
			Vector2 a = new Vector2(Mathf.Clamp(position.x, vector.x, vector2.x), Mathf.Clamp(position.y, vector.y, vector2.y));
			Vector2 b = new Vector2(Mathf.Clamp(vector3.x, vector.x, vector2.x), Mathf.Clamp(vector3.y, vector.y, vector2.y));
			Vector2 a2 = a - b;
			this._resizeRoot.offsetMax += a2 / this._resizeCanvas.scaleFactor;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024F1 File Offset: 0x000006F1
		public ResizableUI()
		{
		}

		// Token: 0x04000012 RID: 18
		[SerializeField]
		private RectTransform _resizeRoot;

		// Token: 0x04000013 RID: 19
		[SerializeField]
		private Canvas _resizeCanvas;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		private bool _lockInScreen = true;

		// Token: 0x04000015 RID: 21
		[SerializeField]
		private Vector2 _minSize;
	}
}
