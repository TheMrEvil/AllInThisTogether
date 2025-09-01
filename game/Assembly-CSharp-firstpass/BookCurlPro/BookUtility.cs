using System;
using UnityEngine;

namespace BookCurlPro
{
	// Token: 0x02000085 RID: 133
	public static class BookUtility
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x000260BE File Offset: 0x000242BE
		public static void ShowPage(GameObject page)
		{
			CanvasGroup component = page.GetComponent<CanvasGroup>();
			component.alpha = 1f;
			component.blocksRaycasts = true;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000260D7 File Offset: 0x000242D7
		public static void HidePage(GameObject page)
		{
			CanvasGroup component = page.GetComponent<CanvasGroup>();
			component.alpha = 0f;
			component.blocksRaycasts = false;
			page.transform.SetAsFirstSibling();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000260FB File Offset: 0x000242FB
		public static void CopyTransform(Transform from, Transform to)
		{
			to.position = from.position;
			to.rotation = from.rotation;
			to.localScale = from.localScale;
		}
	}
}
