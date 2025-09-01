using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QFSW.QC.UI
{
	// Token: 0x02000006 RID: 6
	[ExecuteInEditMode]
	public class ZoomUIController : MonoBehaviour
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002500 File Offset: 0x00000700
		private float ClampAndSnapZoom(float zoom)
		{
			return Mathf.Round(Mathf.Min(this._maxZoom, Mathf.Max(this._minZoom, zoom)) / this._zoomIncrement) * this._zoomIncrement;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000252C File Offset: 0x0000072C
		public void ZoomUp()
		{
			this._scaler.ZoomMagnification = this.ClampAndSnapZoom(this._scaler.ZoomMagnification + this._zoomIncrement);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002551 File Offset: 0x00000751
		public void ZoomDown()
		{
			this._scaler.ZoomMagnification = this.ClampAndSnapZoom(this._scaler.ZoomMagnification - this._zoomIncrement);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002578 File Offset: 0x00000778
		private void Update()
		{
			if (this._quantumConsole && this._quantumConsole.KeyConfig)
			{
				if (this._quantumConsole.KeyConfig.ZoomInKey.IsPressed())
				{
					this.ZoomUp();
				}
				if (this._quantumConsole.KeyConfig.ZoomOutKey.IsPressed())
				{
					this.ZoomDown();
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025E0 File Offset: 0x000007E0
		private void LateUpdate()
		{
			if (this._scaler && this._text)
			{
				float zoomMagnification = this._scaler.ZoomMagnification;
				if (zoomMagnification != this._lastZoom)
				{
					this._lastZoom = zoomMagnification;
					int num = Mathf.RoundToInt(100f * zoomMagnification);
					this._text.text = string.Format("{0}%", num);
				}
			}
			if (this._zoomDownBtn)
			{
				this._zoomDownBtn.interactable = (this._lastZoom > this._minZoom);
			}
			if (this._zoomUpBtn)
			{
				this._zoomUpBtn.interactable = (this._lastZoom < this._maxZoom);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002697 File Offset: 0x00000897
		public ZoomUIController()
		{
		}

		// Token: 0x04000016 RID: 22
		[SerializeField]
		private float _zoomIncrement = 0.1f;

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private float _minZoom = 0.1f;

		// Token: 0x04000018 RID: 24
		[SerializeField]
		private float _maxZoom = 2f;

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private Button _zoomDownBtn;

		// Token: 0x0400001A RID: 26
		[SerializeField]
		private Button _zoomUpBtn;

		// Token: 0x0400001B RID: 27
		[SerializeField]
		private DynamicCanvasScaler _scaler;

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private QuantumConsole _quantumConsole;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		private TextMeshProUGUI _text;

		// Token: 0x0400001E RID: 30
		private float _lastZoom = -1f;
	}
}
