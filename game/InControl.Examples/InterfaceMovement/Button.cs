using System;
using UnityEngine;

namespace InterfaceMovement
{
	// Token: 0x02000005 RID: 5
	public class Button : MonoBehaviour
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000026BD File Offset: 0x000008BD
		private void Start()
		{
			this.cachedRenderer = base.GetComponent<Renderer>();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026CC File Offset: 0x000008CC
		private void Update()
		{
			bool flag = base.transform.parent.GetComponent<ButtonManager>().focusedButton == this;
			Color color = this.cachedRenderer.material.color;
			color.a = Mathf.MoveTowards(color.a, flag ? 1f : 0.5f, Time.deltaTime * 3f);
			this.cachedRenderer.material.color = color;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002743 File Offset: 0x00000943
		public Button()
		{
		}

		// Token: 0x0400000B RID: 11
		private Renderer cachedRenderer;

		// Token: 0x0400000C RID: 12
		public Button up;

		// Token: 0x0400000D RID: 13
		public Button down;

		// Token: 0x0400000E RID: 14
		public Button left;

		// Token: 0x0400000F RID: 15
		public Button right;
	}
}
