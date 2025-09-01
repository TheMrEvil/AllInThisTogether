using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro
{
	// Token: 0x0200004D RID: 77
	public class TMP_ScrollbarEventHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00024A86 File Offset: 0x00022C86
		public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log("Scrollbar click...");
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00024A92 File Offset: 0x00022C92
		public void OnSelect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar selected");
			this.isSelected = true;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00024AA5 File Offset: 0x00022CA5
		public void OnDeselect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar De-Selected");
			this.isSelected = false;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00024AB8 File Offset: 0x00022CB8
		public TMP_ScrollbarEventHandler()
		{
		}

		// Token: 0x0400032E RID: 814
		public bool isSelected;
	}
}
