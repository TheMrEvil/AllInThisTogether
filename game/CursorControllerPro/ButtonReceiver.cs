using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x02000005 RID: 5
	public class ButtonReceiver : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002131 File Offset: 0x00000331
		private void Start()
		{
			this.controllerObj = GameObject.Find("CursorControl");
			this.cursorController = this.controllerObj.GetComponent<CursorController>();
			this.tooltipController = this.controllerObj.GetComponent<TooltipController>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002168 File Offset: 0x00000368
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.cursorController.FadeIn();
			this.cursorController.HoverSpeed();
			if (this.hasTooltip)
			{
				this.cursorController.tooltipController.ShowTooltip();
			}
			this.tooltipController.UpdateTooltipText(this.title, this.body);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021BA File Offset: 0x000003BA
		public void OnPointerExit(PointerEventData eventData)
		{
			this.cursorController.FadeOut();
			this.cursorController.NormalSpeed();
			if (this.hasTooltip)
			{
				this.cursorController.tooltipController.HideTooltip();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021EA File Offset: 0x000003EA
		public ButtonReceiver()
		{
		}

		// Token: 0x0400000B RID: 11
		private GameObject controllerObj;

		// Token: 0x0400000C RID: 12
		private CursorController cursorController;

		// Token: 0x0400000D RID: 13
		private TooltipController tooltipController;

		// Token: 0x0400000E RID: 14
		[Header("TOOLTIP")]
		public bool hasTooltip;

		// Token: 0x0400000F RID: 15
		public string title = "Tooltip";

		// Token: 0x04000010 RID: 16
		public string body = "Tooltip information goes here.";
	}
}
