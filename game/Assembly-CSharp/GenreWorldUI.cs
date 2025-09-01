using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B4 RID: 436
public class GenreWorldUI : MonoBehaviour
{
	// Token: 0x06001203 RID: 4611 RVA: 0x0006FBF0 File Offset: 0x0006DDF0
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.canvasGroup.alpha = 0f;
		this.rect = base.GetComponent<RectTransform>();
		this.canvas = base.GetComponentInParent<Canvas>().GetComponentInParent<RectTransform>();
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x0006FC2B File Offset: 0x0006DE2B
	private void Update()
	{
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x0006FC30 File Offset: 0x0006DE30
	private void UpdateVotePips()
	{
		if (this.selected == null)
		{
			return;
		}
		int votes = VoteManager.GetVotes(0);
		for (int i = 0; i < this.VotePips.Count; i++)
		{
			this.VotePips[i].SetActive(i < votes);
		}
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x0006FC80 File Offset: 0x0006DE80
	public void SetupDisplay(GenreBookOption genre)
	{
		this.selected = genre;
		if (this.selected == null || genre.Genre == null)
		{
			return;
		}
		GenreRootNode genreRootNode = genre.Genre.RootNode as GenreRootNode;
		this.GenreIcon.sprite = genreRootNode.Icon;
		this.TitleText.text = genreRootNode.Name;
		this.DetailText.text = TextParser.AugmentDetail(genreRootNode.Detail, null, null, false);
		foreach (GameObject gameObject in this.VotePips)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x0006FD44 File Offset: 0x0006DF44
	public GenreWorldUI()
	{
	}

	// Token: 0x040010CB RID: 4299
	private CanvasGroup canvasGroup;

	// Token: 0x040010CC RID: 4300
	private RectTransform canvas;

	// Token: 0x040010CD RID: 4301
	private RectTransform rect;

	// Token: 0x040010CE RID: 4302
	public Image GenreIcon;

	// Token: 0x040010CF RID: 4303
	public TextMeshProUGUI TitleText;

	// Token: 0x040010D0 RID: 4304
	public TextMeshProUGUI DetailText;

	// Token: 0x040010D1 RID: 4305
	public TextMeshProUGUI ContextKey;

	// Token: 0x040010D2 RID: 4306
	private GenreBookOption selected;

	// Token: 0x040010D3 RID: 4307
	[Header("Voting")]
	public CanvasGroup VoteGroup;

	// Token: 0x040010D4 RID: 4308
	public List<GameObject> VotePips;
}
