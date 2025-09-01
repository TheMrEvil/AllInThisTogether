using System;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class DemoUpsellPanel : MonoBehaviour
{
	// Token: 0x060013DE RID: 5086 RVA: 0x0007BC9F File Offset: 0x00079E9F
	public void DoQuit()
	{
		Application.Quit();
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x0007BCA6 File Offset: 0x00079EA6
	public void DiscordButton()
	{
		URLButton.OpenDiscord();
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x0007BCAD File Offset: 0x00079EAD
	public void SteamWishlistButton()
	{
		Application.OpenURL("steam://openurl/https://store.steampowered.com/app/917950/Vellum/");
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x0007BCB9 File Offset: 0x00079EB9
	public DemoUpsellPanel()
	{
	}
}
