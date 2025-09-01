using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BookCurlPro
{
	// Token: 0x02000081 RID: 129
	[RequireComponent(typeof(BookPro))]
	public class AutoFlip : MonoBehaviour
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x00024618 File Offset: 0x00022818
		private void Start()
		{
			if (!this.ControledBook)
			{
				this.ControledBook = base.GetComponent<BookPro>();
			}
			if (this.AutoStartFlip)
			{
				this.StartFlipping(this.ControledBook.EndFlippingPaper + 1);
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00024650 File Offset: 0x00022850
		public void FlipRightPage()
		{
			if (this.isPageFlipping)
			{
				return;
			}
			if (this.ControledBook.CurrentPaper >= this.ControledBook.papers.Length)
			{
				return;
			}
			this.ControledBook.interactable = false;
			this.isPageFlipping = true;
			PageFlipper.FlipPage(this.ControledBook, this.PageFlipTime, FlipMode.RightToLeft, delegate
			{
				this.isPageFlipping = false;
			});
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000246B4 File Offset: 0x000228B4
		public void FlipLeftPage()
		{
			if (this.isPageFlipping)
			{
				return;
			}
			if (this.ControledBook.CurrentPaper <= 0)
			{
				return;
			}
			this.ControledBook.interactable = false;
			this.isPageFlipping = true;
			PageFlipper.FlipPage(this.ControledBook, this.PageFlipTime, FlipMode.LeftToRight, delegate
			{
				this.isPageFlipping = false;
			});
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0002470C File Offset: 0x0002290C
		public void StartFlipping(int target)
		{
			this.isBookInteractable = this.ControledBook.interactable;
			this.ControledBook.interactable = false;
			this.flippingStarted = true;
			this.elapsedTime = 0f;
			this.nextPageCountDown = 0f;
			this.targetPaper = target;
			if (target > this.ControledBook.CurrentPaper)
			{
				this.Mode = FlipMode.RightToLeft;
				return;
			}
			if (target < this.ControledBook.currentPaper)
			{
				this.Mode = FlipMode.LeftToRight;
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00024788 File Offset: 0x00022988
		private void Update()
		{
			if (this.flippingStarted)
			{
				this.elapsedTime += Time.deltaTime;
				if (this.elapsedTime > this.DelayBeforeStart)
				{
					if (this.nextPageCountDown < 0f)
					{
						if ((this.ControledBook.CurrentPaper < this.targetPaper && this.Mode == FlipMode.RightToLeft) || (this.ControledBook.CurrentPaper > this.targetPaper && this.Mode == FlipMode.LeftToRight))
						{
							this.isPageFlipping = true;
							PageFlipper.FlipPage(this.ControledBook, this.PageFlipTime, this.Mode, delegate
							{
								this.isPageFlipping = false;
							});
						}
						else
						{
							this.flippingStarted = false;
							this.ControledBook.interactable = this.isBookInteractable;
							base.enabled = false;
						}
						this.nextPageCountDown = this.PageFlipTime + this.TimeBetweenPages + Time.deltaTime;
					}
					this.nextPageCountDown -= Time.deltaTime;
				}
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00024880 File Offset: 0x00022A80
		public AutoFlip()
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000248A5 File Offset: 0x00022AA5
		[CompilerGenerated]
		private void <FlipRightPage>b__12_0()
		{
			this.isPageFlipping = false;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000248AE File Offset: 0x00022AAE
		[CompilerGenerated]
		private void <FlipLeftPage>b__13_0()
		{
			this.isPageFlipping = false;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000248B7 File Offset: 0x00022AB7
		[CompilerGenerated]
		private void <Update>b__16_0()
		{
			this.isPageFlipping = false;
		}

		// Token: 0x0400047E RID: 1150
		public BookPro ControledBook;

		// Token: 0x0400047F RID: 1151
		public FlipMode Mode;

		// Token: 0x04000480 RID: 1152
		public float PageFlipTime = 1f;

		// Token: 0x04000481 RID: 1153
		public float DelayBeforeStart;

		// Token: 0x04000482 RID: 1154
		public float TimeBetweenPages = 5f;

		// Token: 0x04000483 RID: 1155
		public bool AutoStartFlip = true;

		// Token: 0x04000484 RID: 1156
		private bool flippingStarted;

		// Token: 0x04000485 RID: 1157
		private bool isPageFlipping;

		// Token: 0x04000486 RID: 1158
		private float elapsedTime;

		// Token: 0x04000487 RID: 1159
		private float nextPageCountDown;

		// Token: 0x04000488 RID: 1160
		private bool isBookInteractable;

		// Token: 0x04000489 RID: 1161
		private int targetPaper;
	}
}
