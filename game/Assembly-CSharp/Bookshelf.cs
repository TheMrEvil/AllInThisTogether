using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200021D RID: 541
public class Bookshelf : MonoBehaviour
{
	// Token: 0x060016B6 RID: 5814 RVA: 0x0008F707 File Offset: 0x0008D907
	private void Start()
	{
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x0008F70C File Offset: 0x0008D90C
	public void Generate(BookshelfData data = null)
	{
		if (this.Inactive)
		{
			return;
		}
		if (data == null)
		{
			return;
		}
		this.Clear();
		foreach (Bookshelf.Shelf s in this.Shelves)
		{
			this.GenerateShelf(s, data);
		}
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x0008F77C File Offset: 0x0008D97C
	public void Clear()
	{
		foreach (Bookshelf.Shelf shelf in this.Shelves)
		{
			if (!(shelf.ShelfObj == null))
			{
				for (int i = shelf.ShelfObj.childCount - 1; i >= 0; i--)
				{
					Transform child = shelf.ShelfObj.GetChild(i);
					if (child != null)
					{
						UnityEngine.Object.DestroyImmediate(child.gameObject);
					}
				}
			}
		}
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x0008F810 File Offset: 0x0008DA10
	private void GenerateShelf(Bookshelf.Shelf s, BookshelfData data)
	{
		float num = s.Width / 2f;
		BookshelfData.ShelfElement element;
		for (float num2 = -num; num2 < num; num2 += element.Width)
		{
			float availableWidth = num - num2;
			element = data.GetElement(availableWidth, this.IsLowDetail, null);
			if (element == null)
			{
				break;
			}
			this.SpawnShelfElement(element, s.ShelfObj, num2);
		}
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x0008F860 File Offset: 0x0008DA60
	private void SpawnShelfElement(BookshelfData.ShelfElement obj, Transform shelf, float offset)
	{
		float x = offset + obj.Width / 2f;
		if (obj.Prefab == null)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(obj.Prefab, shelf);
		gameObject.transform.localPosition = new Vector3(x, 0f, 0f);
		gameObject.transform.localRotation = Quaternion.identity;
		if (obj.CanRotate)
		{
			gameObject.transform.localEulerAngles = new Vector3(0f, (float)UnityEngine.Random.Range(0, 360), 0f);
		}
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x0008F8F1 File Offset: 0x0008DAF1
	private void OnDrawGizmos()
	{
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x0008F8F3 File Offset: 0x0008DAF3
	public Bookshelf()
	{
	}

	// Token: 0x04001645 RID: 5701
	public List<Bookshelf.Shelf> Shelves;

	// Token: 0x04001646 RID: 5702
	public bool IsLowDetail;

	// Token: 0x04001647 RID: 5703
	public bool Inactive;

	// Token: 0x020005FB RID: 1531
	[Serializable]
	public class Shelf
	{
		// Token: 0x060026B2 RID: 9906 RVA: 0x000D3FE0 File Offset: 0x000D21E0
		public Shelf()
		{
		}

		// Token: 0x0400295E RID: 10590
		public Transform ShelfObj;

		// Token: 0x0400295F RID: 10591
		public float Width;
	}
}
