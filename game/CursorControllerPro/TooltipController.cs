using System;
using UnityEngine;
using UnityEngine.UI;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x0200000E RID: 14
	public class TooltipController : MonoBehaviour
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000038B3 File Offset: 0x00001AB3
		private void Start()
		{
			this.cursorController = base.GetComponent<CursorController>();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000038C4 File Offset: 0x00001AC4
		public void ToolTipPopUpDelay()
		{
			if (this.timer < this.popUpDelay && this.countTimer)
			{
				this.timer += Time.deltaTime;
				return;
			}
			if (this.timer >= this.popUpDelay)
			{
				this.tooltipRect.GetComponent<Animator>().SetBool("Show", true);
				this.countTimer = false;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003925 File Offset: 0x00001B25
		public void ToolTipPositions()
		{
			this.tooltipRect.transform.localPosition = Vector3.SmoothDamp(this.tooltipRect.transform.localPosition, this.toolTipPosition, ref this.toolTipV, this.toolTipSmoothing);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003960 File Offset: 0x00001B60
		public void ToolTipBoundaries(RectTransform rect)
		{
			if (rect.anchoredPosition.x <= this.cursorController.xMin + (float)(this.toolTipWidth + 70))
			{
				this.tooFarLeft = true;
				if (rect.anchoredPosition.y <= this.cursorController.yMin + (float)(this.toolTipHeight + 55))
				{
					this.toolTipPosition = new Vector3((float)this.toolTipOffsetX, (float)(-(float)this.toolTipOffsetY), 100f);
				}
				else
				{
					this.toolTipPosition = new Vector3((float)this.toolTipOffsetX, (float)this.toolTipOffsetY, 100f);
				}
			}
			else
			{
				this.tooFarLeft = false;
			}
			if (rect.anchoredPosition.x >= this.cursorController.xMax - (float)(this.toolTipWidth + 70))
			{
				this.tooFarRight = true;
				if (rect.anchoredPosition.y <= this.cursorController.yMin + (float)(this.toolTipHeight + 55))
				{
					this.toolTipPosition = new Vector3((float)(-(float)this.toolTipOffsetX), (float)(-(float)this.toolTipOffsetY), 100f);
				}
				else
				{
					this.toolTipPosition = new Vector3((float)(-(float)this.toolTipOffsetX), (float)this.toolTipOffsetY, 100f);
				}
			}
			else
			{
				this.tooFarRight = false;
			}
			if (rect.anchoredPosition.y <= this.cursorController.yMin + (float)(this.toolTipHeight + 55))
			{
				if (!this.tooFarRight)
				{
					this.toolTipPosition = new Vector3((float)this.toolTipOffsetX, (float)(-(float)this.toolTipOffsetY), 100f);
					return;
				}
			}
			else if (!this.tooFarRight)
			{
				this.toolTipPosition = new Vector3((float)this.toolTipOffsetX, (float)this.toolTipOffsetY, 100f);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003B0A File Offset: 0x00001D0A
		public void UpdateTooltipText(string title, string body)
		{
			this.tooltipTitle1.text = title;
			this.tooltipBody1.text = body;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003B24 File Offset: 0x00001D24
		public void ShowTooltip()
		{
			if (this.cursorController.canMoveCursor)
			{
				this.countTimer = true;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003B3C File Offset: 0x00001D3C
		public void HideTooltip()
		{
			if (this.tooltipRect.GetComponent<Animator>().runtimeAnimatorController != null)
			{
				this.tooltipRect.GetComponent<Animator>().SetBool("Show", false);
			}
			this.countTimer = false;
			this.timer = 0f;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003B8C File Offset: 0x00001D8C
		public TooltipController()
		{
		}

		// Token: 0x04000068 RID: 104
		private CursorController cursorController;

		// Token: 0x04000069 RID: 105
		[Header("TOOLTIPS")]
		[Tooltip("The Player 1 Rect Transform holding the position of the tool tips.")]
		public RectTransform tooltipRect;

		// Token: 0x0400006A RID: 106
		public Text tooltipTitle1;

		// Token: 0x0400006B RID: 107
		public Text tooltipBody1;

		// Token: 0x0400006C RID: 108
		[Tooltip("The 'Width' of the Tooltip game object. This is how Console Cursors determines the size boundaries at runtime.")]
		public int toolTipWidth = 770;

		// Token: 0x0400006D RID: 109
		[Tooltip("The 'Height' of the Tooltip game object. This is how Console Cursors determines the size boundaries at runtime.")]
		public int toolTipHeight = 245;

		// Token: 0x0400006E RID: 110
		[Tooltip("The Pos X of the game object 'container' that is used to re-position the tooltip so it always stays visible on the screen. Adjusting this value will adjust how far left or right the tooltip positions itself when re-aligning.")]
		public int toolTipOffsetX = 468;

		// Token: 0x0400006F RID: 111
		[Tooltip("The Pos Y of the game object 'container' that is used to re-position the tooltip so it always stays visible on the screen. Adjusting this value will adjust how far left or right the tooltip positions itself when re-aligning.")]
		public int toolTipOffsetY = -206;

		// Token: 0x04000070 RID: 112
		[HideInInspector]
		public bool tooFarRight;

		// Token: 0x04000071 RID: 113
		[HideInInspector]
		public bool tooFarLeft;

		// Token: 0x04000072 RID: 114
		[Range(0f, 0.2f)]
		public float toolTipSmoothing = 0.07f;

		// Token: 0x04000073 RID: 115
		private Vector3 toolTipV = Vector3.zero;

		// Token: 0x04000074 RID: 116
		private Vector3 toolTipPosition = new Vector3(0f, 0f, 0f);

		// Token: 0x04000075 RID: 117
		[Tooltip("The delay before the tooltip appears over a button")]
		public float popUpDelay = 0.35f;

		// Token: 0x04000076 RID: 118
		[HideInInspector]
		public float timer;

		// Token: 0x04000077 RID: 119
		[HideInInspector]
		public bool countTimer;
	}
}
