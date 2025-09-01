using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020001C8 RID: 456
public class StatusBar : MonoBehaviour
{
	// Token: 0x17000158 RID: 344
	// (get) Token: 0x060012A0 RID: 4768 RVA: 0x00072B8C File Offset: 0x00070D8C
	private bool shouldShow
	{
		get
		{
			return !UITransitionHelper.InWavePrelim() && (PanelManager.CurPanel == PanelType.Pause || PanelManager.CurPanel == PanelType.Augments || PanelManager.CurPanel == PanelType.GameInvisible);
		}
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x00072BB5 File Offset: 0x00070DB5
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.rect = base.GetComponent<RectTransform>();
		MapManager.OnMapChangeFinished = (Action)Delegate.Combine(MapManager.OnMapChangeFinished, new Action(this.CheckLibraryChange));
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x00072BF0 File Offset: 0x00070DF0
	private void Update()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		this.canvasGroup.UpdateOpacity(this.shouldShow, 1f, false);
		float b = (float)((PanelManager.CurPanel == PanelType.Augments) ? 10 : 44);
		this.rect.anchoredPosition = new Vector2(this.rect.anchoredPosition.x, Mathf.Lerp(this.rect.anchoredPosition.y, b, Time.deltaTime * 10f));
		this.toShowMods.Clear();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in PlayerControl.myInstance.Augment.trees)
		{
			if (keyValuePair.Key.ShouldShowAsStatus(PlayerControl.myInstance))
			{
				this.toShowMods.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
		this.toShowStats.Clear();
		foreach (EntityControl.AppliedStatus appliedStatus in PlayerControl.myInstance.Statuses)
		{
			if (appliedStatus.rootNode.ShowInUI && !this.toShowStats.ContainsKey(appliedStatus.rootNode))
			{
				float item = appliedStatus.Duration;
				float item2 = appliedStatus.baseDuration;
				if (appliedStatus.rootNode.TimeBehaviour == StatusRootNode.TimeoutBehaviour.DecrementStack)
				{
					item = appliedStatus.CurrentTickTime;
					item2 = appliedStatus.rootNode.tickRate;
				}
				this.toShowStats.Add(appliedStatus.rootNode, new ValueTuple<int, float, float>(appliedStatus.Stacks, item, item2));
			}
		}
		this.ClearExpired();
		this.AddAugments();
		this.AddStatuses();
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x00072DD4 File Offset: 0x00070FD4
	private void ClearExpired()
	{
		foreach (KeyValuePair<AugmentRootNode, StatusBoxUI> keyValuePair in this.AugmentDisplays)
		{
			if (!this.toShowMods.ContainsKey(keyValuePair.Key))
			{
				this.AugmentDisplays[keyValuePair.Key].Release();
			}
		}
		foreach (KeyValuePair<StatusRootNode, StatusBoxUI> keyValuePair2 in this.StatusDisplays)
		{
			if (!this.toShowStats.ContainsKey(keyValuePair2.Key))
			{
				this.StatusDisplays[keyValuePair2.Key].Release();
			}
		}
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x00072EB4 File Offset: 0x000710B4
	private void AddAugments()
	{
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in this.toShowMods)
		{
			if (this.AugmentDisplays.ContainsKey(keyValuePair.Key))
			{
				this.AugmentDisplays[keyValuePair.Key].UpdateAugment(keyValuePair.Value);
			}
			else
			{
				this.CreateBox(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x00072F48 File Offset: 0x00071148
	private void AddStatuses()
	{
		foreach (KeyValuePair<StatusRootNode, ValueTuple<int, float, float>> keyValuePair in this.toShowStats)
		{
			if (this.StatusDisplays.ContainsKey(keyValuePair.Key))
			{
				this.StatusDisplays[keyValuePair.Key].UpdateStatus(keyValuePair.Key, keyValuePair.Value.Item1, keyValuePair.Value.Item2, keyValuePair.Value.Item3);
			}
			else
			{
				this.CreateBox(keyValuePair.Key, keyValuePair.Value.Item1, keyValuePair.Value.Item2, keyValuePair.Value.Item3);
			}
		}
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x00073024 File Offset: 0x00071224
	public void CreateBox(AugmentRootNode augment, int stacks)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StatusRef, this.StatusRef.transform.parent);
		gameObject.SetActive(true);
		StatusBoxUI component = gameObject.GetComponent<StatusBoxUI>();
		component.Setup(augment, stacks);
		this.AugmentDisplays.Add(augment, component);
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x00073070 File Offset: 0x00071270
	public void CreateBox(StatusRootNode status, int stacks, float duration, float baseDuration)
	{
		GameObject gameObject = status.IsNegative ? this.StatusNegativeRef : this.StatusRef;
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, gameObject.transform.parent);
		gameObject2.SetActive(true);
		StatusBoxUI component = gameObject2.GetComponent<StatusBoxUI>();
		component.Setup(status, stacks, duration, baseDuration);
		this.StatusDisplays.Add(status, component);
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x000730C8 File Offset: 0x000712C8
	private void CheckLibraryChange()
	{
		if (MapManager.InLobbyScene)
		{
			this.ClearBars();
		}
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x000730D8 File Offset: 0x000712D8
	private void ClearBars()
	{
		foreach (KeyValuePair<AugmentRootNode, StatusBoxUI> keyValuePair in this.AugmentDisplays)
		{
			UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
		}
		foreach (KeyValuePair<StatusRootNode, StatusBoxUI> keyValuePair2 in this.StatusDisplays)
		{
			UnityEngine.Object.Destroy(keyValuePair2.Value.gameObject);
		}
		this.AugmentDisplays.Clear();
		this.StatusDisplays.Clear();
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x00073198 File Offset: 0x00071398
	public StatusBar()
	{
	}

	// Token: 0x040011AB RID: 4523
	public GameObject StatusRef;

	// Token: 0x040011AC RID: 4524
	public GameObject StatusNegativeRef;

	// Token: 0x040011AD RID: 4525
	public RectTransform rect;

	// Token: 0x040011AE RID: 4526
	private CanvasGroup canvasGroup;

	// Token: 0x040011AF RID: 4527
	private Dictionary<AugmentRootNode, StatusBoxUI> AugmentDisplays = new Dictionary<AugmentRootNode, StatusBoxUI>();

	// Token: 0x040011B0 RID: 4528
	private Dictionary<StatusRootNode, StatusBoxUI> StatusDisplays = new Dictionary<StatusRootNode, StatusBoxUI>();

	// Token: 0x040011B1 RID: 4529
	private Dictionary<AugmentRootNode, int> toShowMods = new Dictionary<AugmentRootNode, int>();

	// Token: 0x040011B2 RID: 4530
	[TupleElementNames(new string[]
	{
		"stacks",
		"duration",
		"baseDuration"
	})]
	private Dictionary<StatusRootNode, ValueTuple<int, float, float>> toShowStats = new Dictionary<StatusRootNode, ValueTuple<int, float, float>>();
}
