using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B9 RID: 441
public class ManaDisplay : MonoBehaviour
{
	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06001224 RID: 4644 RVA: 0x000705A8 File Offset: 0x0006E7A8
	private bool ShouldShow
	{
		get
		{
			return !UITransitionHelper.InWavePrelim() && PanelManager.CurPanel != PanelType.Augments && PanelManager.CurPanel != PanelType.Pause && (!TutorialManager.InTutorial || TutorialManager.CurrentStep >= TutorialStep.Generator_Mana) && (!PlayerNook.IsInEditMode || !MapManager.InLobbyScene);
		}
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x000705F4 File Offset: 0x0006E7F4
	private void Awake()
	{
		ManaDisplay.instance = this;
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.startGroupPos = this.GroupTransform.localPosition;
		PlayerControl.LocalPlayerSpawned = (Action)Delegate.Combine(PlayerControl.LocalPlayerSpawned, new Action(this.PlayerSpawned));
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x00070644 File Offset: 0x0006E844
	private void PlayerSpawned()
	{
		this.ManaChanged();
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x0007064C File Offset: 0x0006E84C
	public void ManaChanged()
	{
		List<Mana> tempMana = PlayerControl.myInstance.Mana.TempMana;
		List<Mana> coreMana = PlayerControl.myInstance.Mana.CoreMana;
		Mathf.CeilToInt((float)tempMana.Count / 5f);
		Mathf.CeilToInt((float)coreMana.Count / 5f);
		List<List<Mana>> list = this.CollectManaGroups(tempMana);
		List<List<Mana>> list2 = this.CollectManaGroups(coreMana);
		if (this.TempGroups.Count == 0)
		{
			this.AddGroup(true);
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (this.TempGroups.Count <= i)
			{
				this.AddGroup(true);
			}
			this.TempGroups[i].UpdateDisplay(list[i]);
		}
		if (list.Count == 0)
		{
			this.TempGroups[0].UpdateDisplay(this.emptyList);
		}
		for (int j = 0; j < list2.Count; j++)
		{
			if (this.CoreGroups.Count <= j)
			{
				this.AddGroup(false);
			}
			this.CoreGroups[j].UpdateDisplay(list2[j]);
		}
		for (int k = this.CoreGroups.Count - 1; k >= list2.Count; k--)
		{
			this.RemoveGroup(this.CoreGroups[k], false);
		}
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x000707A4 File Offset: 0x0006E9A4
	public void CastFailed(PlayerAbilityType abilityType, CastFailedReason reason, int cost)
	{
		if (reason != CastFailedReason.Mana)
		{
			return;
		}
		if (Time.realtimeSinceStartup - this.animCD < 0.1f)
		{
			this.animCD = Time.realtimeSinceStartup;
			return;
		}
		if (this.NotEnoughManaSFX != null)
		{
			AudioManager.PlayInterfaceSFX(this.NotEnoughManaSFX, 1f, 0f);
		}
		this.animCD = Time.realtimeSinceStartup;
		int num = 0;
		foreach (ManaGroup manaGroup in this.TempGroups)
		{
			foreach (ManaPip manaPip in manaGroup.Mana)
			{
				num++;
			}
		}
		foreach (ManaGroup manaGroup2 in this.CoreGroups)
		{
			for (int i = 0; i < manaGroup2.Mana.Count; i++)
			{
				ManaPip manaPip2 = manaGroup2.Mana[i];
				num++;
				if (!manaPip2.mana.IsAvailable)
				{
					manaPip2.PlayAnim("Pulse_Empty");
					if (num >= cost)
					{
						return;
					}
				}
			}
		}
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x0007090C File Offset: 0x0006EB0C
	private List<List<Mana>> CollectManaGroups(List<Mana> manas)
	{
		List<List<Mana>> list = new List<List<Mana>>();
		List<Mana> list2 = new List<Mana>();
		for (int i = 0; i < manas.Count; i++)
		{
			if (list2.Count >= 5)
			{
				list.Add(list2.Clone<Mana>());
				list2.Clear();
			}
			list2.Add(manas[i]);
		}
		if (list2.Count > 0)
		{
			list.Add(list2.Clone<Mana>());
		}
		return list;
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x00070974 File Offset: 0x0006EB74
	public ManaGroup AddGroup(bool temp)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GroupRef, this.GroupRef.transform.parent);
		gameObject.SetActive(true);
		ManaGroup component = gameObject.GetComponent<ManaGroup>();
		if (temp)
		{
			this.TempGroups.Add(component);
		}
		else
		{
			this.CoreGroups.Add(component);
		}
		return component;
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x000709C7 File Offset: 0x0006EBC7
	public void RemoveGroup(ManaGroup grp, bool temp)
	{
		if (temp)
		{
			this.TempGroups.Remove(grp);
		}
		else
		{
			this.CoreGroups.Remove(grp);
		}
		UnityEngine.Object.Destroy(grp.gameObject);
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x000709F4 File Offset: 0x0006EBF4
	public void ManaUsed(ManaPip pip)
	{
		if (GameHUD.Mode == GameHUD.HUDMode.Off)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UsedVFX.gameObject, this.UsedVFX.transform.parent);
		gameObject.transform.position = pip.rect.position;
		gameObject.SetActive(true);
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x00070A48 File Offset: 0x0006EC48
	public void ManaRegen(ManaPip pip, bool ignoreFX = false)
	{
		if (ignoreFX)
		{
			return;
		}
		AudioManager.PlayInterfaceSFX(this.RegenSFX, 0.5f, 0f);
		if (GameHUD.Mode == GameHUD.HUDMode.Off)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RegenVFX.gameObject, pip.transform);
		gameObject.transform.position = pip.rect.position;
		gameObject.SetActive(true);
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x00070AA9 File Offset: 0x0006ECA9
	public void ManaRechargePartial(ManaPip pip)
	{
		if (GameHUD.Mode == GameHUD.HUDMode.Off)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RegenSmallVFX.gameObject, pip.transform);
		gameObject.transform.position = pip.rect.position;
		gameObject.SetActive(true);
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x00070AE8 File Offset: 0x0006ECE8
	public void ManaTransform(ManaPip pip, MagicColor e, bool ignoreFX = false)
	{
		if (GameHUD.Mode == GameHUD.HUDMode.Off || ignoreFX)
		{
			return;
		}
		ManaDisplay.ColorFX colorFX = this.GetColorFX(e);
		ParticleSystem particleSystem = (colorFX != null) ? colorFX.fx : null;
		if (particleSystem == null)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(particleSystem.gameObject, particleSystem.transform.parent);
		gameObject.transform.position = pip.rect.position;
		gameObject.SetActive(true);
		AudioManager.PlayInterfaceSFX(this.ManaTransformSFX, 1f, 0f);
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x00070B68 File Offset: 0x0006ED68
	public void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		this.canvasGroup.UpdateOpacity(this.ShouldShow, 4f, true);
		float fieldOfView = PlayerControl.MyCamera.fieldOfView;
		Vector3 vector = this.startGroupPos + new Vector3(this.FovHorizontalOffset.Evaluate(fieldOfView), this.FovVerticalOffset.Evaluate(fieldOfView), 0f);
		if (this.GroupTransform.localPosition != vector)
		{
			this.GroupTransform.localPosition = vector;
		}
		for (int i = 0; i < this.TempGroups.Count; i++)
		{
			this.TempGroups[i].UpdatePosition(i);
		}
		for (int j = 0; j < this.CoreGroups.Count; j++)
		{
			this.CoreGroups[j].UpdatePosition(this.TempGroups.Count + j);
		}
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x00070C50 File Offset: 0x0006EE50
	private ManaDisplay.ColorFX GetColorFX(MagicColor e)
	{
		foreach (ManaDisplay.ColorFX colorFX in this.ColorSwapEffects)
		{
			if (colorFX.color == e)
			{
				return colorFX;
			}
		}
		return null;
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x00070CAC File Offset: 0x0006EEAC
	public ManaDisplay()
	{
	}

	// Token: 0x0400110E RID: 4366
	public static ManaDisplay instance;

	// Token: 0x0400110F RID: 4367
	public RectTransform GroupTransform;

	// Token: 0x04001110 RID: 4368
	public AnimationCurve FovHorizontalOffset;

	// Token: 0x04001111 RID: 4369
	public AnimationCurve FovVerticalOffset;

	// Token: 0x04001112 RID: 4370
	public ParticleSystem UsedVFX;

	// Token: 0x04001113 RID: 4371
	public ParticleSystem RegenSmallVFX;

	// Token: 0x04001114 RID: 4372
	public ParticleSystem RegenVFX;

	// Token: 0x04001115 RID: 4373
	public List<ManaDisplay.ColorFX> ColorSwapEffects;

	// Token: 0x04001116 RID: 4374
	public AudioClip RegenSFX;

	// Token: 0x04001117 RID: 4375
	public AudioClip NotEnoughManaSFX;

	// Token: 0x04001118 RID: 4376
	public AudioClip ManaTransformSFX;

	// Token: 0x04001119 RID: 4377
	private Vector3 startGroupPos;

	// Token: 0x0400111A RID: 4378
	public GameObject GroupRef;

	// Token: 0x0400111B RID: 4379
	public List<ManaGroup> TempGroups;

	// Token: 0x0400111C RID: 4380
	public List<ManaGroup> CoreGroups;

	// Token: 0x0400111D RID: 4381
	private CanvasGroup canvasGroup;

	// Token: 0x0400111E RID: 4382
	[Header("Mana Pips")]
	public float CDAlpha = 0.4f;

	// Token: 0x0400111F RID: 4383
	private float animCD;

	// Token: 0x04001120 RID: 4384
	private List<Mana> emptyList = new List<Mana>();

	// Token: 0x0200057E RID: 1406
	[Serializable]
	public class ColorFX
	{
		// Token: 0x0600251F RID: 9503 RVA: 0x000D092C File Offset: 0x000CEB2C
		public ColorFX()
		{
		}

		// Token: 0x04002767 RID: 10087
		public MagicColor color;

		// Token: 0x04002768 RID: 10088
		public ParticleSystem fx;
	}
}
