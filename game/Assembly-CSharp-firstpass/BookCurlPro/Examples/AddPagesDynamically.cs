using System;
using UnityEngine;

namespace BookCurlPro.Examples
{
	// Token: 0x02000088 RID: 136
	public class AddPagesDynamically : MonoBehaviour
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x000264F0 File Offset: 0x000246F0
		public void AddPaper()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.FrontPagePrefab);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.BackPagePrefab);
			gameObject.transform.SetParent(this.book.transform, false);
			gameObject2.transform.SetParent(this.book.transform, false);
			Paper paper = new Paper();
			paper.Front = gameObject;
			paper.Back = gameObject2;
			Paper[] array = new Paper[this.book.papers.Length + 1];
			for (int i = 0; i < this.book.papers.Length; i++)
			{
				array[i] = this.book.papers[i];
			}
			array[array.Length - 1] = paper;
			this.book.papers = array;
			this.book.EndFlippingPaper = this.book.papers.Length - 1;
			this.book.UpdatePages();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000265D3 File Offset: 0x000247D3
		public AddPagesDynamically()
		{
		}

		// Token: 0x040004BD RID: 1213
		public GameObject FrontPagePrefab;

		// Token: 0x040004BE RID: 1214
		public GameObject BackPagePrefab;

		// Token: 0x040004BF RID: 1215
		public BookPro book;
	}
}
