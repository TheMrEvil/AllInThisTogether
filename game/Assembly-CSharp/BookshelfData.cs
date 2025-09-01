using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class BookshelfData : ScriptableObject
{
	// Token: 0x06000219 RID: 537 RVA: 0x0001310C File Offset: 0x0001130C
	public BookshelfData.ShelfElement GetElement(float availableWidth, bool lowDetail, List<BookshelfData.ShelfElement> ignore = null)
	{
		List<BookshelfData.ShelfElement> list = new List<BookshelfData.ShelfElement>();
		foreach (BookshelfData.ShelfElement shelfElement in this.Elements)
		{
			if ((!lowDetail || shelfElement.LowDetail) && shelfElement.Width <= availableWidth)
			{
				list.Add(shelfElement);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00013198 File Offset: 0x00011398
	public BookshelfData()
	{
	}

	// Token: 0x04000217 RID: 535
	public List<BookshelfData.ShelfElement> Elements;

	// Token: 0x02000434 RID: 1076
	[Serializable]
	public class ShelfElement
	{
		// Token: 0x0600210C RID: 8460 RVA: 0x000C21D5 File Offset: 0x000C03D5
		public ShelfElement()
		{
		}

		// Token: 0x04002173 RID: 8563
		public GameObject Prefab;

		// Token: 0x04002174 RID: 8564
		public float Width = 0.5f;

		// Token: 0x04002175 RID: 8565
		public bool CanRotate;

		// Token: 0x04002176 RID: 8566
		public bool LowDetail = true;
	}
}
