using System;
using UnityEngine;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x02000008 RID: 8
	public class Demo : MonoBehaviour
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000032DD File Offset: 0x000014DD
		public void ChangeTooltipSmoothing(float smoothAmount)
		{
			this.controller.tooltipController.toolTipSmoothing = smoothAmount;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000032F0 File Offset: 0x000014F0
		public void ChangeParallaxStrength(float strength)
		{
			this.controller.parallaxStrength = strength;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000032FE File Offset: 0x000014FE
		public void ChangeToolTipDelay(float delayAmount)
		{
			this.controller.tooltipController.popUpDelay = delayAmount;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003311 File Offset: 0x00001511
		public void DisableAllCursors()
		{
			this.controller.cursorRect.gameObject.SetActive(false);
			this.controller.cursorObjectPlayer1.SetActive(false);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000333C File Offset: 0x0000153C
		public void SelectNextPlayer()
		{
			this.DisableAllCursors();
			this.DisablePlayerBlocks();
			if (this.controller.currentPlayerActive == 0)
			{
				this.controller.ChangeActivePlayer(1);
				this.player2Block.SetActive(true);
				return;
			}
			if (this.controller.currentPlayerActive == 1)
			{
				this.controller.ChangeActivePlayer(2);
				this.player3Block.SetActive(true);
				return;
			}
			if (this.controller.currentPlayerActive == 2)
			{
				this.controller.ChangeActivePlayer(3);
				this.player4Block.SetActive(true);
				return;
			}
			if (this.controller.currentPlayerActive == 3)
			{
				this.controller.ChangeActivePlayer(0);
				this.player1Block.SetActive(true);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000033EF File Offset: 0x000015EF
		private void DisablePlayerBlocks()
		{
			this.player1Block.SetActive(false);
			this.player2Block.SetActive(false);
			this.player3Block.SetActive(false);
			this.player4Block.SetActive(false);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003421 File Offset: 0x00001621
		public void EnableMultiCursors()
		{
			this.DisablePlayerBlocks();
			this.DisableAllCursors();
			this.controller.ChangeActivePlayer(0);
			this.player1Block.SetActive(true);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003448 File Offset: 0x00001648
		public void ChangeCursorTints(int tintColorIndex)
		{
			if (tintColorIndex == 0)
			{
				this.controller.tint = new Color(1f, 1f, 1f);
			}
			else if (tintColorIndex == 1)
			{
				this.controller.tint = new Color(0.38f, 0.74f, 1f);
			}
			else if (tintColorIndex == 2)
			{
				this.controller.tint = new Color(1f, 0.69f, 0.29f);
			}
			else if (tintColorIndex == 3)
			{
				this.controller.tint = new Color(1f, 0.26f, 0.26f);
			}
			else if (tintColorIndex == 4)
			{
				this.controller.tint = new Color(0.65f, 0.31f, 1f);
			}
			else if (tintColorIndex == 5)
			{
				this.controller.tint = new Color(0.32f, 0.9f, 0.34f);
			}
			this.controller.cursorObjectPlayer1.SetActive(false);
			this.controller.cursorObjectPlayer1.SetActive(true);
			this.controller.cursorObjectPlayer1.GetComponent<CursorTint>().SetColor(this.controller.tint);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003578 File Offset: 0x00001778
		public void LoadOnlineDocumentation()
		{
			Application.OpenURL("http://cursorcontrollerpro.slimui.com/documentation/");
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003584 File Offset: 0x00001784
		public Demo()
		{
		}

		// Token: 0x0400004C RID: 76
		public CursorController controller;

		// Token: 0x0400004D RID: 77
		[Header("MULTI-CURSOR WINDOW")]
		public GameObject player1Block;

		// Token: 0x0400004E RID: 78
		public GameObject player2Block;

		// Token: 0x0400004F RID: 79
		public GameObject player3Block;

		// Token: 0x04000050 RID: 80
		public GameObject player4Block;
	}
}
