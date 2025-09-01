using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000145 RID: 325
public class CodexGridItem : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x06000EB2 RID: 3762 RVA: 0x0005D0A8 File Offset: 0x0005B2A8
	public void Setup(AugmentTree augment)
	{
		if (augment == null)
		{
			this.Title.text = "???";
			this.GroupFader.alpha = 0.5f;
			return;
		}
		foreach (GameObject gameObject in this.AvailableObjects)
		{
			gameObject.SetActive(true);
		}
		this.Title.text = augment.Root.Name;
		this.Icon.sprite = augment.Root.Icon;
		this.augmentRef = augment;
		this.Border.sprite = GameDB.Quality(augment.Root.DisplayQuality).Border;
		this.SetupSearchExtras();
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x0005D17C File Offset: 0x0005B37C
	private void SetupSearchExtras()
	{
		AugmentRootNode root = this.augmentRef.Root;
		List<Node> list = new List<Node>();
		foreach (Node node in this.augmentRef.nodes)
		{
			Logic_Not logic_Not = node as Logic_Not;
			if (logic_Not != null)
			{
				Logic_Ability logic_Ability = logic_Not.Test as Logic_Ability;
				if (logic_Ability != null)
				{
					list.Add(logic_Ability);
				}
			}
		}
		foreach (Node node2 in this.augmentRef.nodes)
		{
			Logic_Ability logic_Ability2 = node2 as Logic_Ability;
			if (logic_Ability2 != null && !list.Contains(logic_Ability2))
			{
				this.extraTerms.Add(logic_Ability2.Graph.Root.Name.ToLower());
			}
		}
		this.extraTerms.Add(root.magicColor.ToString().ToLower());
		foreach (ModFilter modFilter in root.Filters.Filters)
		{
			if (modFilter == ModFilter.Player_Primary)
			{
				this.extraTerms.Add("generator");
			}
			if (modFilter == ModFilter.Player_Secondary)
			{
				this.extraTerms.Add("spender");
			}
			if (modFilter == ModFilter.Player_Movement)
			{
				this.extraTerms.Add("movement");
			}
			if (modFilter == ModFilter.Player_CoreAbility)
			{
				this.extraTerms.Add("signature");
			}
		}
		if (root.modType == ModType.Fountain && root.Tome != null)
		{
			foreach (string item in root.Tome.Root.ShortName.ToLower().Split(' ', StringSplitOptions.None))
			{
				this.extraTerms.Add(item);
			}
		}
		string detail = root.Detail;
		if (detail.Contains("BAR"))
		{
			this.extraTerms.Add("barrier");
		}
		if (detail.Contains("SPD"))
		{
			this.extraTerms.Add("speed");
		}
		if (detail.Contains("HP"))
		{
			this.extraTerms.Add("health");
		}
		if (detail.Contains("DMG"))
		{
			this.extraTerms.Add("damage");
		}
	}

	// Token: 0x06000EB4 RID: 3764 RVA: 0x0005D40C File Offset: 0x0005B60C
	public bool MatchesSearch(List<string> searchTerms)
	{
		if (this.augmentRef == null)
		{
			return false;
		}
		string text = this.augmentRef.Root.Name.ToLower();
		string text2 = this.augmentRef.Root.Detail.ToLower();
		foreach (string value in searchTerms)
		{
			bool flag = false;
			using (List<string>.Enumerator enumerator2 = this.extraTerms.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.Equals(value))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag && !text.Contains(value) && !text2.Contains(value))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x0005D4FC File Offset: 0x0005B6FC
	public void OnClick()
	{
		if (this.augmentRef == null)
		{
			return;
		}
		CodexPanel.instance.GridView.SelectItem(this.augmentRef);
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x0005D522 File Offset: 0x0005B722
	public void OnSelect(BaseEventData ev)
	{
		if (this.augmentRef == null || !InputManager.IsUsingController)
		{
			return;
		}
		CodexPanel.instance.GridView.SelectItem(this.augmentRef);
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x0005D54F File Offset: 0x0005B74F
	public CodexGridItem()
	{
	}

	// Token: 0x04000C49 RID: 3145
	public TextMeshProUGUI Title;

	// Token: 0x04000C4A RID: 3146
	public Image Icon;

	// Token: 0x04000C4B RID: 3147
	public Image Border;

	// Token: 0x04000C4C RID: 3148
	public List<GameObject> AvailableObjects;

	// Token: 0x04000C4D RID: 3149
	public CanvasGroup GroupFader;

	// Token: 0x04000C4E RID: 3150
	public GameObject NewDisplay;

	// Token: 0x04000C4F RID: 3151
	private AugmentTree augmentRef;

	// Token: 0x04000C50 RID: 3152
	private List<string> extraTerms = new List<string>();
}
