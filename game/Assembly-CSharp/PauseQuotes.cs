using System;
using TMPro;
using UnityEngine;

// Token: 0x02000174 RID: 372
public class PauseQuotes : MonoBehaviour
{
	// Token: 0x06000FE6 RID: 4070 RVA: 0x00064238 File Offset: 0x00062438
	public void SetupQuote(string ID)
	{
		LoreDB.PauseQuote pauseQuote = LoreDB.GetPauseQuote(ID);
		if (pauseQuote == null)
		{
			this.DetailBlock.text = "";
			return;
		}
		string text;
		if (WaveManager.instance.AppendixLevel > 0)
		{
			text = pauseQuote.Appendix;
		}
		else
		{
			text = pauseQuote.GetText(WaveManager.CurrentWave + 1);
		}
		this.Apply(pauseQuote, text);
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x00064294 File Offset: 0x00062494
	public void Apply(LoreDB.PauseQuote quote, string text)
	{
		if (text.Contains("$boss$"))
		{
			text = text.Replace("$boss$", AIManager.GetNextBossName());
		}
		this.DetailBlock.text = text;
		TextMeshProUGUI detailBlock = this.DetailBlock;
		TextAlignmentOptions alignment;
		switch (quote.Alignment)
		{
		case LoreDB.PauseQuote.TextAlign.Left:
			alignment = TextAlignmentOptions.Left;
			break;
		case LoreDB.PauseQuote.TextAlign.Center:
			alignment = TextAlignmentOptions.Center;
			break;
		case LoreDB.PauseQuote.TextAlign.Right:
			alignment = TextAlignmentOptions.Right;
			break;
		default:
			alignment = TextAlignmentOptions.Center;
			break;
		}
		detailBlock.alignment = alignment;
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x00064313 File Offset: 0x00062513
	public PauseQuotes()
	{
	}

	// Token: 0x04000E02 RID: 3586
	public TextMeshProUGUI DetailBlock;
}
