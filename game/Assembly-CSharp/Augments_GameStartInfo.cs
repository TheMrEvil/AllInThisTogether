using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019B RID: 411
public class Augments_GameStartInfo : MonoBehaviour
{
	// Token: 0x06001134 RID: 4404 RVA: 0x0006A928 File Offset: 0x00068B28
	public void SetupStart()
	{
		this.IsInStart = true;
		GenreTree gameGraph = GameplayManager.instance.GameGraph;
		if (gameGraph == null)
		{
			this.IsInStart = false;
			return;
		}
		if (GameplayManager.IsChallengeActive)
		{
			this.TitleText.text = "Book Club: " + GameplayManager.Challenge.Name;
		}
		else
		{
			this.TitleText.text = gameGraph.Root.Name;
		}
		this.TomeIcon.sprite = gameGraph.Root.Icon;
		this.BackEffects.Play();
		AudioManager.PlaySFX2D(this.SequenceSFX, 1f, 0.1f);
		this.t = this.SequenceDuration;
		this.ClearBindings();
		this.BindingGroup.alpha = 0f;
		if (GameplayManager.BindingLevel > 0)
		{
			this.SetupBindings();
		}
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x0006A9FC File Offset: 0x00068BFC
	public void TickUpdate()
	{
		this.t -= GameplayManager.deltaTime;
		if (this.bindingObjects.Count > 0)
		{
			this.BindingGroup.UpdateOpacity(true, 4f, true);
			float num = 0.1f + (this.SequenceDuration - 2.5f) / (float)Mathf.Max(this.bindingObjects.Count, 3);
			for (int i = 0; i < this.bindingObjects.Count; i++)
			{
				if (this.SequenceDuration - this.t >= (float)i * num)
				{
					RectTransform rectTransform = this.bindingObjects[i];
					rectTransform.sizeDelta = new Vector2(Mathf.Lerp(rectTransform.sizeDelta.x, 120f, Time.deltaTime * 6f), rectTransform.sizeDelta.y);
				}
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.BindingLayout);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.BindingList_2);
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.BindingList_3);
		}
		if (this.t <= 0f)
		{
			this.EndSequence();
		}
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x0006AB04 File Offset: 0x00068D04
	public void EndSequence()
	{
		if (!this.IsInStart)
		{
			return;
		}
		this.IsInStart = false;
		this.BackEffects.Stop();
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x0006AB24 File Offset: 0x00068D24
	private void SetupBindings()
	{
		this.BindingValue.text = GameplayManager.BindingLevel.ToString();
		List<AugmentRootNode> list = new List<AugmentRootNode>();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
		{
			list.Add(keyValuePair.Key);
		}
		for (int i = 0; i < list.Count; i++)
		{
			Transform parent = this.BindingRef.transform.parent;
			if (i >= 12)
			{
				parent = this.BindingList_3.transform;
			}
			else if (i >= 6)
			{
				parent = this.BindingList_2.transform;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BindingRef, parent);
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(0f, component.sizeDelta.y);
			gameObject.GetComponent<AugmentInfoBox>().Setup(list[i], 1, ModType.Binding, null, TextAnchor.UpperCenter, Vector3.zero);
			gameObject.gameObject.SetActive(true);
			this.bindingObjects.Add(component);
		}
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0006AC60 File Offset: 0x00068E60
	private void ClearBindings()
	{
		foreach (RectTransform rectTransform in this.bindingObjects)
		{
			UnityEngine.Object.Destroy(rectTransform.gameObject);
		}
		this.bindingObjects.Clear();
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x0006ACC0 File Offset: 0x00068EC0
	public Augments_GameStartInfo()
	{
	}

	// Token: 0x04000F94 RID: 3988
	public CanvasGroup CGroup;

	// Token: 0x04000F95 RID: 3989
	public TextMeshProUGUI TitleText;

	// Token: 0x04000F96 RID: 3990
	public Image TomeIcon;

	// Token: 0x04000F97 RID: 3991
	public ParticleSystem BackEffects;

	// Token: 0x04000F98 RID: 3992
	public bool IsInStart;

	// Token: 0x04000F99 RID: 3993
	public float SequenceDuration = 2f;

	// Token: 0x04000F9A RID: 3994
	private float t;

	// Token: 0x04000F9B RID: 3995
	public AudioClip SequenceSFX;

	// Token: 0x04000F9C RID: 3996
	public TextMeshProUGUI BindingValue;

	// Token: 0x04000F9D RID: 3997
	public CanvasGroup BindingGroup;

	// Token: 0x04000F9E RID: 3998
	public GameObject BindingRef;

	// Token: 0x04000F9F RID: 3999
	public RectTransform BindingLayout;

	// Token: 0x04000FA0 RID: 4000
	public RectTransform BindingList_2;

	// Token: 0x04000FA1 RID: 4001
	public RectTransform BindingList_3;

	// Token: 0x04000FA2 RID: 4002
	private List<RectTransform> bindingObjects = new List<RectTransform>();
}
