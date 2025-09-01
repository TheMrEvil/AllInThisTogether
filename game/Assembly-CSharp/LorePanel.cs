using System;
using UnityEngine;

// Token: 0x020001E6 RID: 486
public class LorePanel : MonoBehaviour
{
	// Token: 0x06001469 RID: 5225 RVA: 0x0007F9FD File Offset: 0x0007DBFD
	private void Awake()
	{
		LorePanel.instance = this;
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x0007FA05 File Offset: 0x0007DC05
	public void Open()
	{
		if (PanelManager.CurPanel == PanelType.LoreInfo)
		{
			return;
		}
		PanelManager.instance.PushPanel(PanelType.LoreInfo);
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x0007FA20 File Offset: 0x0007DC20
	public void Load(LibraryInteractivePage page)
	{
		this.FirstTimeGroup.HideImmediate();
		this.Open();
		this.PageDisplay.Load(page.Title, page.Subheading, page.Body, page.Signature);
		this.openTimeAt = Time.realtimeSinceStartup;
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x0007FA6C File Offset: 0x0007DC6C
	public void Load(BookMeshInteraction page)
	{
		this.FirstTimeGroup.HideImmediate();
		this.Open();
		this.PageDisplay.Load("", "", page.Info, "");
		this.openTimeAt = Time.realtimeSinceStartup;
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x0007FAAA File Offset: 0x0007DCAA
	public void Eyeball()
	{
		this.PageDisplay.TurnOnEyeball();
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x0007FAB8 File Offset: 0x0007DCB8
	public void Load(LorePage page)
	{
		this.FirstTimeGroup.HideImmediate();
		this.Open();
		if (page.Type == LorePage.PageType.Explicit)
		{
			this.PageDisplay.Load(page.Title, page.Subheading, page.Body, "");
		}
		else
		{
			if (!UnlockManager.SeenLorePages.Contains(page.UID))
			{
				AudioManager.PlayInterfaceSFX(this.FirstTimeSFX, 1f, 0f);
				this.FirstTimeGroup.ShowImmediate();
			}
			this.Load(page.UID);
		}
		this.openTimeAt = Time.realtimeSinceStartup;
		this.pageRef = page;
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x0007FB54 File Offset: 0x0007DD54
	private void Load(string UID)
	{
		LoreDB.LorePage page = LoreDB.GetPage(UID);
		if (page == null)
		{
			Debug.LogError("Invalid Lore Page UID: " + UID);
			return;
		}
		this.Open();
		this.openTimeAt = Time.realtimeSinceStartup;
		this.PageDisplay.Load(page);
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x0007FB9C File Offset: 0x0007DD9C
	public void Continue()
	{
		if (Time.realtimeSinceStartup - this.openTimeAt < 0.5f)
		{
			return;
		}
		if (PanelManager.CurPanel == PanelType.LoreInfo)
		{
			PanelManager.instance.PopPanel();
		}
		if (this.pageRef != null)
		{
			Action onUse = this.pageRef.OnUse;
			if (onUse == null)
			{
				return;
			}
			onUse();
		}
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x0007FBF3 File Offset: 0x0007DDF3
	public LorePanel()
	{
	}

	// Token: 0x040013AC RID: 5036
	public static LorePanel instance;

	// Token: 0x040013AD RID: 5037
	public LorePageUIDisplay PageDisplay;

	// Token: 0x040013AE RID: 5038
	public AudioClip FirstTimeSFX;

	// Token: 0x040013AF RID: 5039
	public CanvasGroup FirstTimeGroup;

	// Token: 0x040013B0 RID: 5040
	private LorePage pageRef;

	// Token: 0x040013B1 RID: 5041
	private float openTimeAt;
}
