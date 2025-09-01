using System;
using UnityEngine;
using UnityEngine.UI;

namespace BookCurlPro.Examples
{
	// Token: 0x0200008A RID: 138
	public class FastForwardFlipper : MonoBehaviour
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00026664 File Offset: 0x00024864
		public void GotoPage()
		{
			int num = int.Parse(this.pageNumInputField.text);
			if (num < 0)
			{
				num = 0;
			}
			if (num > this.flipper.ControledBook.papers.Length * 2)
			{
				num = this.flipper.ControledBook.papers.Length * 2 - 1;
			}
			this.flipper.enabled = true;
			this.flipper.PageFlipTime = 0.2f;
			this.flipper.TimeBetweenPages = 0f;
			this.flipper.StartFlipping((num + 1) / 2);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000266F2 File Offset: 0x000248F2
		public FastForwardFlipper()
		{
		}

		// Token: 0x040004C1 RID: 1217
		public AutoFlip flipper;

		// Token: 0x040004C2 RID: 1218
		private BookPro book;

		// Token: 0x040004C3 RID: 1219
		public InputField pageNumInputField;
	}
}
