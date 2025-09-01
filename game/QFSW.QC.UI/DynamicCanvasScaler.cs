using System;
using UnityEngine;
using UnityEngine.UI;

namespace QFSW.QC.UI
{
	// Token: 0x02000004 RID: 4
	[ExecuteInEditMode]
	public class DynamicCanvasScaler : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002240 File Offset: 0x00000440
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002248 File Offset: 0x00000448
		public float RectMagnification
		{
			get
			{
				return this._rectMagnification;
			}
			set
			{
				if (value > 0f)
				{
					this._rectMagnification = value;
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002259 File Offset: 0x00000459
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002261 File Offset: 0x00000461
		public float ZoomMagnification
		{
			get
			{
				return this._zoomMagnification;
			}
			set
			{
				if (value > 0f)
				{
					this._zoomMagnification = value;
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002272 File Offset: 0x00000472
		private float RootScaler
		{
			get
			{
				return this._rectMagnification / this._zoomMagnification;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002281 File Offset: 0x00000481
		private void OnEnable()
		{
			this._lastScaler = this.RootScaler;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002290 File Offset: 0x00000490
		private void Update()
		{
			if (this._scaler && this._uiRoot && this.RootScaler != this._lastScaler)
			{
				Rect rect = new Rect(this._uiRoot.offsetMin.x / this._lastScaler, this._uiRoot.offsetMin.y / this._lastScaler, this._uiRoot.offsetMax.x / this._lastScaler, this._uiRoot.offsetMax.y / this._lastScaler);
				this._lastScaler = this.RootScaler;
				this._scaler.referenceResolution = this._referenceResolution / this._zoomMagnification;
				this._uiRoot.offsetMin = new Vector2(rect.x, rect.y) * this.RootScaler;
				this._uiRoot.offsetMax = new Vector2(rect.width, rect.height) * this.RootScaler;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023AB File Offset: 0x000005AB
		public DynamicCanvasScaler()
		{
		}

		// Token: 0x0400000C RID: 12
		[Range(0.5f, 2f)]
		[SerializeField]
		private float _rectMagnification = 1f;

		// Token: 0x0400000D RID: 13
		[Range(0.5f, 2f)]
		[SerializeField]
		private float _zoomMagnification = 1f;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		private CanvasScaler _scaler;

		// Token: 0x0400000F RID: 15
		[SerializeField]
		private RectTransform _uiRoot;

		// Token: 0x04000010 RID: 16
		[SerializeField]
		private Vector2 _referenceResolution = new Vector2(1920f, 1080f);

		// Token: 0x04000011 RID: 17
		private float _lastScaler;
	}
}
