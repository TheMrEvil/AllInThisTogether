using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class AbilityGroupBar : MonoBehaviour
{
	// Token: 0x06000F40 RID: 3904 RVA: 0x00060B60 File Offset: 0x0005ED60
	private void Awake()
	{
		this.BoxRef.SetActive(false);
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00060B70 File Offset: 0x0005ED70
	public void Setup(PlayerAbilityType abilityType)
	{
		this.ClearBoxes();
		TextMeshProUGUI title = this.Title;
		string text;
		switch (abilityType)
		{
		case PlayerAbilityType.Primary:
			text = "Generator";
			goto IL_48;
		case PlayerAbilityType.Secondary:
			text = "Spender";
			goto IL_48;
		case PlayerAbilityType.Movement:
			text = "Movement";
			goto IL_48;
		}
		text = "NA";
		IL_48:
		title.text = text;
		this.BindIcon.Binding = abilityType.GetUIBinding();
		this.BindIcon.UpdateIcon();
		this.AbilityType = abilityType;
		List<AbilityTree> playerAbilities = GraphDB.GetPlayerAbilities(abilityType);
		for (int i = playerAbilities.Count - 1; i >= 0; i--)
		{
			if (playerAbilities[i].Root.Usage.AbilityMetadata.Locked)
			{
				playerAbilities.RemoveAt(i);
			}
		}
		if (playerAbilities[0].Root.Usage.AbilityMetadata.ManaCost > 0f)
		{
			playerAbilities.Sort((AbilityTree x, AbilityTree y) => x.Root.Usage.AbilityMetadata.ManaCost.CompareTo(y.Root.Usage.AbilityMetadata.ManaCost));
		}
		playerAbilities.Sort((AbilityTree x, AbilityTree y) => UnlockManager.IsAbilityUnlocked(y).CompareTo(UnlockManager.IsAbilityUnlocked(x)));
		AbilityBoxUI abilityBoxUI = null;
		Ability ability = PlayerControl.myInstance.actions.GetAbility(abilityType);
		AbilityTree y2 = (ability != null) ? ability.AbilityTree : null;
		foreach (AbilityTree abilityTree in playerAbilities)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BoxRef, this.BoxRef.transform.parent);
			gameObject.SetActive(true);
			AbilityBoxUI component = gameObject.GetComponent<AbilityBoxUI>();
			component.Setup(abilityType, abilityTree, this);
			this.boxes.Add(component);
			component.EquippedDisplay.SetActive(abilityTree == y2);
			if (abilityTree == y2)
			{
				abilityBoxUI = component;
			}
		}
		UISelector.SetupGridListNav<AbilityBoxUI>(this.boxes, 3, null, null, false);
		int num = 9 - this.boxes.Count;
		for (int j = 0; j < num; j++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.FillBox, this.FillBox.transform.parent);
			gameObject2.gameObject.SetActive(true);
			this.fills.Add(gameObject2);
		}
		if (abilityBoxUI != null)
		{
			this.Select(abilityBoxUI);
		}
		this.Refresh();
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x00060DDC File Offset: 0x0005EFDC
	public void Select(AbilityBoxUI selected)
	{
		AbilityPanel.instance.Select(selected);
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x00060DEC File Offset: 0x0005EFEC
	public void Refresh()
	{
		foreach (AbilityBoxUI abilityBoxUI in this.boxes)
		{
			abilityBoxUI.Refresh();
		}
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x00060E3C File Offset: 0x0005F03C
	public void UpdateLockInfo()
	{
		foreach (AbilityBoxUI abilityBoxUI in this.boxes)
		{
			abilityBoxUI.UpdateLockedState();
		}
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x00060E8C File Offset: 0x0005F08C
	public void UpdateEquipped(AbilityBoxUI item)
	{
		foreach (AbilityBoxUI abilityBoxUI in this.boxes)
		{
			abilityBoxUI.EquippedDisplay.SetActive(item == abilityBoxUI);
		}
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x00060EEC File Offset: 0x0005F0EC
	public void OnEquipped(AbilityTree tree)
	{
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00060EF0 File Offset: 0x0005F0F0
	private void ClearBoxes()
	{
		foreach (AbilityBoxUI abilityBoxUI in this.boxes)
		{
			UnityEngine.Object.Destroy(abilityBoxUI.gameObject);
		}
		foreach (GameObject gameObject in this.fills)
		{
			UnityEngine.Object.Destroy(gameObject.gameObject);
		}
		this.fills.Clear();
		this.boxes.Clear();
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x00060FA0 File Offset: 0x0005F1A0
	public AbilityGroupBar()
	{
	}

	// Token: 0x04000D0B RID: 3339
	public TextMeshProUGUI Title;

	// Token: 0x04000D0C RID: 3340
	public GameObject BoxRef;

	// Token: 0x04000D0D RID: 3341
	public PlayerAbilityType AbilityType;

	// Token: 0x04000D0E RID: 3342
	public UIBindingDisplay BindIcon;

	// Token: 0x04000D0F RID: 3343
	public GameObject FillBox;

	// Token: 0x04000D10 RID: 3344
	[NonSerialized]
	public List<AbilityBoxUI> boxes = new List<AbilityBoxUI>();

	// Token: 0x04000D11 RID: 3345
	private List<GameObject> fills = new List<GameObject>();

	// Token: 0x0200054F RID: 1359
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002460 RID: 9312 RVA: 0x000CDF14 File Offset: 0x000CC114
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000CDF20 File Offset: 0x000CC120
		public <>c()
		{
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000CDF28 File Offset: 0x000CC128
		internal int <Setup>b__8_0(AbilityTree x, AbilityTree y)
		{
			return x.Root.Usage.AbilityMetadata.ManaCost.CompareTo(y.Root.Usage.AbilityMetadata.ManaCost);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000CDF5C File Offset: 0x000CC15C
		internal int <Setup>b__8_1(AbilityTree x, AbilityTree y)
		{
			return UnlockManager.IsAbilityUnlocked(y).CompareTo(UnlockManager.IsAbilityUnlocked(x));
		}

		// Token: 0x040026AE RID: 9902
		public static readonly AbilityGroupBar.<>c <>9 = new AbilityGroupBar.<>c();

		// Token: 0x040026AF RID: 9903
		public static Comparison<AbilityTree> <>9__8_0;

		// Token: 0x040026B0 RID: 9904
		public static Comparison<AbilityTree> <>9__8_1;
	}
}
