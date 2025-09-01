using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000140 RID: 320
public class RewardItem : MonoBehaviour
{
	// Token: 0x06000EA2 RID: 3746 RVA: 0x0005CCC0 File Offset: 0x0005AEC0
	public void Setup(string label, Sprite icon, int value)
	{
		this.Opacity.alpha = 0f;
		this.TitleText.text = label;
		this.Icon.sprite = icon;
		this.ValueText.text = "+" + value.ToString();
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x0005CD11 File Offset: 0x0005AF11
	public void Reveal()
	{
		base.StartCoroutine("FadeIn");
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x0005CD1F File Offset: 0x0005AF1F
	private IEnumerator FadeIn()
	{
		while (this.Opacity.alpha < 1f)
		{
			this.Opacity.alpha += Time.deltaTime * 2f;
			yield return true;
		}
		yield break;
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x0005CD2E File Offset: 0x0005AF2E
	public RewardItem()
	{
	}

	// Token: 0x04000C32 RID: 3122
	public TextMeshProUGUI TitleText;

	// Token: 0x04000C33 RID: 3123
	public TextMeshProUGUI ValueText;

	// Token: 0x04000C34 RID: 3124
	public Image Icon;

	// Token: 0x04000C35 RID: 3125
	public CanvasGroup Opacity;

	// Token: 0x0200053C RID: 1340
	[CompilerGenerated]
	private sealed class <FadeIn>d__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x000CD0DF File Offset: 0x000CB2DF
		[DebuggerHidden]
		public <FadeIn>d__6(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000CD0EE File Offset: 0x000CB2EE
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000CD0F0 File Offset: 0x000CB2F0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			RewardItem rewardItem = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
			}
			if (rewardItem.Opacity.alpha >= 1f)
			{
				return false;
			}
			rewardItem.Opacity.alpha += Time.deltaTime * 2f;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x000CD169 File Offset: 0x000CB369
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000CD171 File Offset: 0x000CB371
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x000CD178 File Offset: 0x000CB378
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400266A RID: 9834
		private int <>1__state;

		// Token: 0x0400266B RID: 9835
		private object <>2__current;

		// Token: 0x0400266C RID: 9836
		public RewardItem <>4__this;
	}
}
