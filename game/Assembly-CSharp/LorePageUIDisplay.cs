using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Febucci.UI;
using FIMSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FD RID: 509
public class LorePageUIDisplay : MonoBehaviour
{
	// Token: 0x060015B3 RID: 5555 RVA: 0x00088E94 File Offset: 0x00087094
	public void Load(LoreDB.LorePage page)
	{
		string text = page.CharacterInfo.Signature;
		if (text.Length > 0)
		{
			text = string.Concat(new string[]
			{
				"<color=",
				page.CharacterInfo.TextColor.ColorToHex(true),
				">",
				text,
				"</color>"
			});
		}
		if (page.Character == LoreDB.Character.Tome)
		{
			text = string.Concat(new string[]
			{
				"<color=",
				page.CharacterInfo.TextColor.ColorToHex(true),
				">",
				page.Signature,
				"</color>"
			});
		}
		this.Load(page.Title, "", page.Body, text);
		this.SetPageNumber(page.PageNumber, 0);
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x00088F64 File Offset: 0x00087164
	public void Load(string title, string subheading, string detail, string signature = "")
	{
		this.EyeballIcon.SetActive(false);
		this.BottomLine.SetActive(false);
		this.TitleText.text = title;
		this.BaseDisplay.SetActive(true);
		this.UnavailableText.SetActive(false);
		this.SubheadingText.gameObject.SetActive(subheading.Length > 0);
		this.SubheadingText.text = subheading;
		this.DetailText.gameObject.SetActive(detail.Length > 0);
		this.DetailText.text = detail;
		this.PageNumberText.gameObject.SetActive(false);
		this.SignatureText.gameObject.SetActive(signature.Length > 0);
		this.SignatureText.GetComponent<TextAnimator_TMP>().SetText(signature);
		base.StartCoroutine("Rebuild");
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x00089040 File Offset: 0x00087240
	public void TurnOnEyeball()
	{
		this.TitleText.text = "";
		this.EyeballIcon.SetActive(true);
		this.BottomLine.SetActive(true);
		base.StartCoroutine("Rebuild");
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x00089078 File Offset: 0x00087278
	public void SetPageNumber(int number, int total = 0)
	{
		if (number <= 0)
		{
			this.PageNumberText.gameObject.SetActive(false);
			return;
		}
		this.PageNumberText.gameObject.SetActive(true);
		this.PageNumberText.text = ((total > 0) ? string.Format("p. {0} / {1}", number, total) : string.Format("p. {0}", number));
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x000890E4 File Offset: 0x000872E4
	public void ShowAsUnavailable(LoreDB.LorePage page)
	{
		this.BaseDisplay.SetActive(false);
		this.UnavailableText.SetActive(true);
		this.EyeballIcon.SetActive(false);
		this.BottomLine.SetActive(false);
		this.TitleText.text = page.Title;
		this.PageNumberText.gameObject.SetActive(false);
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x00089143 File Offset: 0x00087343
	private IEnumerator Rebuild()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rect);
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rect);
		yield break;
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x00089152 File Offset: 0x00087352
	public LorePageUIDisplay()
	{
	}

	// Token: 0x0400155E RID: 5470
	public TextMeshProUGUI TitleText;

	// Token: 0x0400155F RID: 5471
	public GameObject EyeballIcon;

	// Token: 0x04001560 RID: 5472
	public TextMeshProUGUI SubheadingText;

	// Token: 0x04001561 RID: 5473
	public TextMeshProUGUI DetailText;

	// Token: 0x04001562 RID: 5474
	public TextMeshProUGUI SignatureText;

	// Token: 0x04001563 RID: 5475
	public TextMeshProUGUI PageNumberText;

	// Token: 0x04001564 RID: 5476
	public GameObject BottomLine;

	// Token: 0x04001565 RID: 5477
	public RectTransform rect;

	// Token: 0x04001566 RID: 5478
	[Header("Unavailable Display")]
	public GameObject BaseDisplay;

	// Token: 0x04001567 RID: 5479
	public GameObject UnavailableText;

	// Token: 0x020005E0 RID: 1504
	[CompilerGenerated]
	private sealed class <Rebuild>d__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002666 RID: 9830 RVA: 0x000D3862 File Offset: 0x000D1A62
		[DebuggerHidden]
		public <Rebuild>d__15(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000D3871 File Offset: 0x000D1A71
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000D3874 File Offset: 0x000D1A74
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			LorePageUIDisplay lorePageUIDisplay = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				LayoutRebuilder.ForceRebuildLayoutImmediate(lorePageUIDisplay.rect);
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			LayoutRebuilder.ForceRebuildLayoutImmediate(lorePageUIDisplay.rect);
			return false;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x000D38D2 File Offset: 0x000D1AD2
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000D38DA File Offset: 0x000D1ADA
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000D38E1 File Offset: 0x000D1AE1
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028F1 RID: 10481
		private int <>1__state;

		// Token: 0x040028F2 RID: 10482
		private object <>2__current;

		// Token: 0x040028F3 RID: 10483
		public LorePageUIDisplay <>4__this;
	}
}
