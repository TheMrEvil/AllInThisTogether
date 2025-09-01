using System;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015A RID: 346
public class KeywordBoxUI : MonoBehaviour
{
	// Token: 0x06000F32 RID: 3890 RVA: 0x0006052C File Offset: 0x0005E72C
	private void Setup(GameDB.Parsable parsable, EntityControl owner)
	{
		this._parsable = parsable;
		this.Title.text = this._parsable.GetReplacementText("", true);
		string text = TextParser.AugmentDetail(this._parsable.Description, null, owner, false);
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		this.Detail.GetComponent<TextAnimator_TMP>().SetText(text, false);
		this.Icon.sprite = this._parsable.Icon;
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x000605A8 File Offset: 0x0005E7A8
	private void Setup(AugmentTree augment, EntityControl owner)
	{
		this._tree = augment;
		this.Title.text = this._tree.Root.Name;
		string text = TextParser.AugmentDetail(this._tree.Root.Detail, augment, owner, false);
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		this.Detail.GetComponent<TextAnimator_TMP>().SetText(text, false);
		this.Icon.sprite = this._tree.Root.Icon;
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x0006062B File Offset: 0x0005E82B
	public bool Matches(GameDB.Parsable p)
	{
		return this._parsable.ID == p.ID;
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x00060644 File Offset: 0x0005E844
	public static void CreateBox(GameDB.Parsable parsable, RectTransform parent, ref List<KeywordBoxUI> boxes, EntityControl owner)
	{
		if (AugmentsPanel.instance == null)
		{
			return;
		}
		using (List<KeywordBoxUI>.Enumerator enumerator = boxes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Matches(parsable))
				{
					return;
				}
			}
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(AugmentsPanel.instance.KeywordPrefab, parent);
		gameObject.transform.localScale = Vector3.one;
		KeywordBoxUI component = gameObject.GetComponent<KeywordBoxUI>();
		component.Setup(parsable, owner);
		boxes.Add(component);
		foreach (GameDB.Parsable parsable2 in TextParser.GetKeywords(parsable.Description, owner))
		{
			KeywordBoxUI.CreateBox(parsable2, parent, ref boxes, owner);
		}
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00060724 File Offset: 0x0005E924
	public static void CreateBox(AugmentTree augment, RectTransform parent, ref List<KeywordBoxUI> boxes, EntityControl owner)
	{
		if (AugmentsPanel.instance == null)
		{
			return;
		}
		using (List<KeywordBoxUI>.Enumerator enumerator = boxes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current._tree == augment)
				{
					return;
				}
			}
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(AugmentsPanel.instance.KeywordPrefab, parent);
		gameObject.transform.localScale = Vector3.one;
		KeywordBoxUI component = gameObject.GetComponent<KeywordBoxUI>();
		component.Setup(augment, owner);
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(augment.Root.Detail, owner))
		{
			KeywordBoxUI.CreateBox(parsable, parent, ref boxes, owner);
		}
		boxes.Add(component);
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00060810 File Offset: 0x0005EA10
	public KeywordBoxUI()
	{
	}

	// Token: 0x04000CF2 RID: 3314
	public TextMeshProUGUI Title;

	// Token: 0x04000CF3 RID: 3315
	public TextMeshProUGUI Detail;

	// Token: 0x04000CF4 RID: 3316
	public Image Icon;

	// Token: 0x04000CF5 RID: 3317
	internal GameDB.Parsable _parsable;

	// Token: 0x04000CF6 RID: 3318
	internal AugmentTree _tree;
}
