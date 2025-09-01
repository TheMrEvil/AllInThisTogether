using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000147 RID: 327
public class CodexLorePageSelector : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x06000EC3 RID: 3779 RVA: 0x0005D873 File Offset: 0x0005BA73
	public void Setup(string PageID)
	{
		this.Setup(LoreDB.GetPage(PageID));
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0005D884 File Offset: 0x0005BA84
	public void Setup(LoreDB.LorePage page)
	{
		this.pageRef = page;
		this.Title.text = page.Title;
		this.UnlockText.text = page.GetUnlockInfo();
		this.PageNumber.text = ((page.PageNumber == 0) ? "" : string.Format("p. {0}", page.PageNumber));
		this.HasSeenPage = (CodexLorePanel.instance.AlwaysAvailablePages.Contains(page.UID) || UnlockManager.SeenLorePages.Contains(page.UID));
		this.TitleGroup.alpha = (this.HasSeenPage ? 1f : 0.5f);
		this.Checkmark.SetActive(this.HasSeenPage);
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0005D949 File Offset: 0x0005BB49
	public void OnSelect(BaseEventData eventData)
	{
		if (!this.HasSeenPage)
		{
			CodexLorePanel.instance.PageDisplay.ShowAsUnavailable(this.pageRef);
			return;
		}
		CodexLorePanel.instance.PageDisplay.Load(this.pageRef);
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0005D97E File Offset: 0x0005BB7E
	public CodexLorePageSelector()
	{
	}

	// Token: 0x04000C60 RID: 3168
	public TextMeshProUGUI Title;

	// Token: 0x04000C61 RID: 3169
	public TextMeshProUGUI UnlockText;

	// Token: 0x04000C62 RID: 3170
	public TextMeshProUGUI PageNumber;

	// Token: 0x04000C63 RID: 3171
	public GameObject Checkmark;

	// Token: 0x04000C64 RID: 3172
	public CanvasGroup TitleGroup;

	// Token: 0x04000C65 RID: 3173
	public bool HasSeenPage;

	// Token: 0x04000C66 RID: 3174
	private LoreDB.LorePage pageRef;
}
