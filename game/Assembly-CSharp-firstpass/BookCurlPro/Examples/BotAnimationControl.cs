using System;
using UnityEngine;

namespace BookCurlPro.Examples
{
	// Token: 0x02000089 RID: 137
	public class BotAnimationControl : MonoBehaviour
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x000265DB File Offset: 0x000247DB
		private void Start()
		{
			if (!this.anim)
			{
				this.anim = base.GetComponent<Animator>();
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000265F6 File Offset: 0x000247F6
		public void Close()
		{
			this.anim.ResetTrigger("Open");
			this.anim.ResetTrigger("Close");
			this.anim.SetTrigger("Close");
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00026628 File Offset: 0x00024828
		public void Open()
		{
			this.anim.ResetTrigger("Close");
			this.anim.ResetTrigger("Open");
			this.anim.SetTrigger("Open");
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0002665A File Offset: 0x0002485A
		private void Update()
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002665C File Offset: 0x0002485C
		public BotAnimationControl()
		{
		}

		// Token: 0x040004C0 RID: 1216
		public Animator anim;
	}
}
