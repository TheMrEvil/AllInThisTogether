using System;
using UnityEngine;

namespace BookCurlPro
{
	// Token: 0x02000086 RID: 134
	public class PageFlipper : MonoBehaviour
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x00026124 File Offset: 0x00024324
		public static void FlipPage(BookPro book, float duration, FlipMode mode, Action OnComplete)
		{
			PageFlipper pageFlipper = book.GetComponent<PageFlipper>();
			if (!pageFlipper)
			{
				pageFlipper = book.gameObject.AddComponent<PageFlipper>();
			}
			pageFlipper.enabled = true;
			pageFlipper.book = book;
			pageFlipper.isFlipping = true;
			pageFlipper.duration = duration - Time.deltaTime;
			pageFlipper.finish = OnComplete;
			pageFlipper.xc = (book.EndBottomLeft.x + book.EndBottomRight.x) / 2f;
			pageFlipper.pageWidth = (book.EndBottomRight.x - book.EndBottomLeft.x) / 2f;
			pageFlipper.pageHeight = Mathf.Abs(book.EndBottomRight.y);
			pageFlipper.flipMode = mode;
			pageFlipper.elapsedTime = 0f;
			float num;
			if (mode == FlipMode.RightToLeft)
			{
				num = pageFlipper.xc + pageFlipper.pageWidth * 0.99f;
				float y = -pageFlipper.pageHeight / (pageFlipper.pageWidth * pageFlipper.pageWidth) * (num - pageFlipper.xc) * (num - pageFlipper.xc);
				book.DragRightPageToPoint(new Vector3(num, y, 0f));
				return;
			}
			num = pageFlipper.xc - pageFlipper.pageWidth * 0.99f;
			float y2 = -pageFlipper.pageHeight / (pageFlipper.pageWidth * pageFlipper.pageWidth) * (num - pageFlipper.xc) * (num - pageFlipper.xc);
			book.DragLeftPageToPoint(new Vector3(num, y2, 0f));
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00026284 File Offset: 0x00024484
		private void Update()
		{
			if (this.isFlipping)
			{
				this.elapsedTime += Time.deltaTime;
				if (this.elapsedTime < this.duration)
				{
					if (this.flipMode == FlipMode.RightToLeft)
					{
						float num = this.xc + (0.5f - this.elapsedTime / this.duration) * 2f * this.pageWidth;
						float y = -this.pageHeight / (this.pageWidth * this.pageWidth) * (num - this.xc) * (num - this.xc);
						this.book.UpdateBookRTLToPoint(new Vector3(num, y, 0f));
						return;
					}
					float num2 = this.xc - (0.5f - this.elapsedTime / this.duration) * 2f * this.pageWidth;
					float y2 = -this.pageHeight / (this.pageWidth * this.pageWidth) * (num2 - this.xc) * (num2 - this.xc);
					this.book.UpdateBookLTRToPoint(new Vector3(num2, y2, 0f));
					return;
				}
				else
				{
					this.book.Flip();
					this.isFlipping = false;
					base.enabled = false;
					if (this.finish != null)
					{
						this.finish();
					}
				}
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000263C3 File Offset: 0x000245C3
		public PageFlipper()
		{
		}

		// Token: 0x040004AD RID: 1197
		public float duration;

		// Token: 0x040004AE RID: 1198
		public BookPro book;

		// Token: 0x040004AF RID: 1199
		private bool isFlipping;

		// Token: 0x040004B0 RID: 1200
		private Action finish;

		// Token: 0x040004B1 RID: 1201
		private float elapsedTime;

		// Token: 0x040004B2 RID: 1202
		private float xc;

		// Token: 0x040004B3 RID: 1203
		private float pageWidth;

		// Token: 0x040004B4 RID: 1204
		private float pageHeight;

		// Token: 0x040004B5 RID: 1205
		private FlipMode flipMode;
	}
}
