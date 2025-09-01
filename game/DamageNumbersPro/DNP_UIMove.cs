using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class DNP_UIMove : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205E File Offset: 0x0000025E
	private void FixedUpdate()
	{
		this.rectTransform.anchoredPosition = Vector2.Lerp(this.fromPosition, this.toPosition, Mathf.Sin(Time.time * this.frequency) * 0.5f + 0.5f);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002099 File Offset: 0x00000299
	public DNP_UIMove()
	{
	}

	// Token: 0x04000001 RID: 1
	public Vector2 fromPosition;

	// Token: 0x04000002 RID: 2
	public Vector2 toPosition;

	// Token: 0x04000003 RID: 3
	public float frequency = 4f;

	// Token: 0x04000004 RID: 4
	private RectTransform rectTransform;
}
