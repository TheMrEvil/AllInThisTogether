using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class LorePage : DiageticOption
{
	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x060009CB RID: 2507 RVA: 0x0004100F File Offset: 0x0003F20F
	public bool IsActive
	{
		get
		{
			return this.isActive;
		}
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00041017 File Offset: 0x0003F217
	protected override void Awake()
	{
		this.PageObject.gameObject.SetActive(false);
		base.Awake();
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x00041030 File Offset: 0x0003F230
	private void Start()
	{
		if (this.ShowOnSpawn)
		{
			this.Show();
		}
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x00041040 File Offset: 0x0003F240
	public void Show()
	{
		if (this.RequireInteraction)
		{
			this.Activate();
			return;
		}
		this.Reveal();
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00041058 File Offset: 0x0003F258
	private void Update()
	{
		if (!this.isActive || this.RequireInteraction || PlayerControl.myInstance == null)
		{
			return;
		}
		if (Vector3.Distance(PlayerControl.myInstance.movement.GetPosition(), base.transform.position) < this.InteractDistance)
		{
			this.Consume();
		}
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x000410B0 File Offset: 0x0003F2B0
	public override void Activate()
	{
		base.Activate();
		this.Reveal();
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x000410BE File Offset: 0x0003F2BE
	public override void Deactivate()
	{
		base.Deactivate();
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x000410C8 File Offset: 0x0003F2C8
	private void Reveal()
	{
		this.BurstFX.Play();
		this.PageObject.gameObject.SetActive(true);
		bool flag = !string.IsNullOrEmpty(this.UID) && UnlockManager.SeenLorePages.Contains(this.UID);
		this.Indicator.gameObject.SetActive(!flag);
		if (this.Type == LorePage.PageType.ID)
		{
			this.SetupVisuals();
		}
		foreach (ParticleSystem particleSystem in this.ParticleFX)
		{
			particleSystem.Play();
		}
		this.Projector.Show();
		this.isActive = true;
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0004118C File Offset: 0x0003F38C
	private void SetupVisuals()
	{
		LoreDB.LorePage page = LoreDB.GetPage(this.UID);
		if (page == null)
		{
			return;
		}
		LoreDB.CharacterInfo characterInfo = page.CharacterInfo;
		if (characterInfo == null)
		{
			return;
		}
		this.ActivateVisuals(characterInfo.Character, characterInfo.Era);
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x000411C8 File Offset: 0x0003F3C8
	private void ActivateVisuals(LoreDB.Character character, LoreDB.Era era)
	{
		foreach (LorePage.PageVisuals pageVisuals in this.Visuals)
		{
			if (pageVisuals.Character != character || pageVisuals.Era != era)
			{
				pageVisuals.VFXRef.gameObject.SetActive(false);
			}
			else
			{
				pageVisuals.VFXRef.gameObject.SetActive(true);
				pageVisuals.VFXRef.Play();
			}
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00041258 File Offset: 0x0003F458
	public override void Select()
	{
		this.Deactivate();
		this.Consume();
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x00041268 File Offset: 0x0003F468
	public void Consume()
	{
		if (!this.isActive)
		{
			return;
		}
		foreach (ParticleSystem particleSystem in this.ParticleFX)
		{
			particleSystem.Stop();
		}
		foreach (LorePage.PageVisuals pageVisuals in this.Visuals)
		{
			if (pageVisuals.VFXRef.gameObject.activeSelf)
			{
				pageVisuals.VFXRef.Stop();
			}
		}
		this.Projector.Hide();
		this.Indicator.Deactivate();
		this.Indicator.gameObject.SetActive(false);
		this.PageObject.SetActive(false);
		this.BurstFX.Play();
		this.isActive = false;
		Action onActivate = this.OnActivate;
		if (onActivate != null)
		{
			onActivate();
		}
		LorePanel.instance.Load(this);
		if (!string.IsNullOrEmpty(this.UID))
		{
			UnlockManager.SaveSeenLorePage(this.UID);
		}
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x00041394 File Offset: 0x0003F594
	public void PageCompleted()
	{
		Action onUse = this.OnUse;
		if (onUse != null)
		{
			onUse();
		}
		UnityEngine.Object.Destroy(base.gameObject, 3f);
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x000413B7 File Offset: 0x0003F5B7
	public LorePage()
	{
	}

	// Token: 0x04000828 RID: 2088
	public LorePage.PageType Type;

	// Token: 0x04000829 RID: 2089
	public string Title;

	// Token: 0x0400082A RID: 2090
	[TextArea(3, 4)]
	public string Subheading;

	// Token: 0x0400082B RID: 2091
	[TextArea(5, 8)]
	public string Body;

	// Token: 0x0400082C RID: 2092
	public string UID;

	// Token: 0x0400082D RID: 2093
	public List<LorePage.PageVisuals> Visuals = new List<LorePage.PageVisuals>();

	// Token: 0x0400082E RID: 2094
	public ProjectorScale Projector;

	// Token: 0x0400082F RID: 2095
	public EntityIndicator Indicator;

	// Token: 0x04000830 RID: 2096
	public GameObject PageObject;

	// Token: 0x04000831 RID: 2097
	public List<ParticleSystem> ParticleFX;

	// Token: 0x04000832 RID: 2098
	public ParticleSystem BurstFX;

	// Token: 0x04000833 RID: 2099
	public bool ShowOnSpawn;

	// Token: 0x04000834 RID: 2100
	public bool RequireInteraction;

	// Token: 0x04000835 RID: 2101
	private bool isActive;

	// Token: 0x04000836 RID: 2102
	public Action OnActivate;

	// Token: 0x04000837 RID: 2103
	public Action OnUse;

	// Token: 0x020004CE RID: 1230
	public enum PageType
	{
		// Token: 0x04002466 RID: 9318
		Explicit,
		// Token: 0x04002467 RID: 9319
		ID
	}

	// Token: 0x020004CF RID: 1231
	[Serializable]
	public class PageVisuals
	{
		// Token: 0x060022E2 RID: 8930 RVA: 0x000C7F7C File Offset: 0x000C617C
		public PageVisuals()
		{
		}

		// Token: 0x04002468 RID: 9320
		public LoreDB.Character Character;

		// Token: 0x04002469 RID: 9321
		public LoreDB.Era Era;

		// Token: 0x0400246A RID: 9322
		public ParticleSystem VFXRef;
	}
}
