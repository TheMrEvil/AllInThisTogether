using System;
using UnityEngine;

// Token: 0x02000216 RID: 534
public class UIFollowMouse : MonoBehaviour
{
	// Token: 0x06001691 RID: 5777 RVA: 0x0008EDC8 File Offset: 0x0008CFC8
	private void Awake()
	{
		this.parentCanvas = base.GetComponentInParent<Canvas>();
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x0008EDD8 File Offset: 0x0008CFD8
	private void Update()
	{
		Vector2 v;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this.parentCanvas.transform as RectTransform, Input.mousePosition + this.Offset, this.parentCanvas.worldCamera, out v);
		base.transform.position = this.parentCanvas.transform.TransformPoint(v);
	}

	// Token: 0x06001693 RID: 5779 RVA: 0x0008EE3E File Offset: 0x0008D03E
	public UIFollowMouse()
	{
	}

	// Token: 0x04001624 RID: 5668
	private Canvas parentCanvas;

	// Token: 0x04001625 RID: 5669
	public Vector3 Offset;
}
