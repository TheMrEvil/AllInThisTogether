using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
[ExecuteInEditMode]
public class GenreBarPathSegment : MonoBehaviour
{
	// Token: 0x06000F1B RID: 3867 RVA: 0x0005FBFC File Offset: 0x0005DDFC
	private void Update()
	{
		if (this.Start == null || this.End == null)
		{
			return;
		}
		if (this.self == null)
		{
			this.SetupRects();
		}
		this.self.anchoredPosition = new Vector2(this.Start.anchoredPosition.x, 0f);
		this.self.sizeDelta = new Vector2(this.End.anchoredPosition.x - this.Start.anchoredPosition.x, this.self.sizeDelta.y);
		string[] array = new string[8];
		array[0] = "Internal fill: ";
		int num = 1;
		RectTransform end = this.End;
		array[num] = ((end != null) ? end.ToString() : null);
		array[2] = " - ";
		int num2 = 3;
		Vector2 vector = this.End.anchoredPosition;
		array[num2] = vector.x.ToString();
		array[4] = " ( start X ";
		int num3 = 5;
		vector = this.Start.anchoredPosition;
		array[num3] = vector.x.ToString();
		array[6] = ") ";
		int num4 = 7;
		vector = this.self.sizeDelta;
		array[num4] = vector.x.ToString();
		Debug.Log(string.Concat(array));
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x0005FD36 File Offset: 0x0005DF36
	private void SetupRects()
	{
		this.self = base.GetComponent<RectTransform>();
		this.parent = base.transform.parent.GetComponent<RectTransform>();
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x0005FD5A File Offset: 0x0005DF5A
	public GenreBarPathSegment()
	{
	}

	// Token: 0x04000CD5 RID: 3285
	public RectTransform Start;

	// Token: 0x04000CD6 RID: 3286
	public RectTransform End;

	// Token: 0x04000CD7 RID: 3287
	private RectTransform parent;

	// Token: 0x04000CD8 RID: 3288
	private RectTransform self;
}
