using System;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000218 RID: 536
public class URLButton : MonoBehaviour
{
	// Token: 0x060016A4 RID: 5796 RVA: 0x0008F512 File Offset: 0x0008D712
	private void Awake()
	{
		if (string.IsNullOrEmpty(this.URL))
		{
			return;
		}
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x0008F53E File Offset: 0x0008D73E
	private void OnClick()
	{
		Application.OpenURL(this.URL);
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x0008F54B File Offset: 0x0008D74B
	public static void OpenDiscord()
	{
		Application.OpenURL("https://discord.com/invite/3MYqK2a9G9");
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x0008F557 File Offset: 0x0008D757
	public void OpenSteamPage()
	{
		SteamFriends.ActivateGameOverlayToStore(new AppId_t(917950U), EOverlayToStoreFlag.k_EOverlayToStoreFlag_None);
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x0008F569 File Offset: 0x0008D769
	public URLButton()
	{
	}

	// Token: 0x04001637 RID: 5687
	public const string DISCORD = "https://discord.com/invite/3MYqK2a9G9";

	// Token: 0x04001638 RID: 5688
	public string URL;
}
