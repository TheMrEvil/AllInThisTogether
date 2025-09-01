using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000159 RID: 345
public class InfoTooltip : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000F2F RID: 3887 RVA: 0x000604ED File Offset: 0x0005E6ED
	public void OnPointerEnter(PointerEventData e)
	{
		Tooltip.Show(this.AnchorPos.position, this.AnchorType, this);
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00060506 File Offset: 0x0005E706
	public void OnPointerExit(PointerEventData e)
	{
		Tooltip.Release();
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x0006050D File Offset: 0x0005E70D
	public InfoTooltip()
	{
	}

	// Token: 0x04000CED RID: 3309
	public Transform AnchorPos;

	// Token: 0x04000CEE RID: 3310
	public TextAnchor AnchorType;

	// Token: 0x04000CEF RID: 3311
	public Vector2 Size = new Vector2(400f, 300f);

	// Token: 0x04000CF0 RID: 3312
	public string Title;

	// Token: 0x04000CF1 RID: 3313
	[TextArea(5, 10)]
	public string Detail;
}
