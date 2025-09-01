using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000179 RID: 377
public class PostGame_RaidEncounter : MonoBehaviour
{
	// Token: 0x0600100E RID: 4110 RVA: 0x00064E20 File Offset: 0x00063020
	public void Setup(RaidDB.Encounter encounter, bool completed, bool hasNext, int attempts)
	{
		this.encounter = encounter;
		foreach (Image image in this.Icons)
		{
			image.sprite = this.encounter.Icon;
		}
		this.GreyscaleIcon.SetActive(!completed);
		this.AttemptCounter.SetActive(attempts > 0);
		this.AttemptText.text = attempts.ToString();
		this.Checkbox.SetActive(completed);
		this.FillDisplay.SetActive(hasNext);
		this.ProgressFill.fillAmount = (float)(completed ? 1 : 0);
		base.GetComponent<UIPingable>().ContextData = encounter.ShortName;
		this.EncounterName.text = encounter.ShortName;
		base.StartCoroutine("RebuildLayout");
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x00064F0C File Offset: 0x0006310C
	private IEnumerator RebuildLayout()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.EncounterName.transform.parent.GetComponent<RectTransform>());
		yield break;
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x00064F1B File Offset: 0x0006311B
	public PostGame_RaidEncounter()
	{
	}

	// Token: 0x04000E22 RID: 3618
	public List<Image> Icons;

	// Token: 0x04000E23 RID: 3619
	public GameObject GreyscaleIcon;

	// Token: 0x04000E24 RID: 3620
	public Image Border;

	// Token: 0x04000E25 RID: 3621
	public GameObject Checkbox;

	// Token: 0x04000E26 RID: 3622
	public GameObject FillDisplay;

	// Token: 0x04000E27 RID: 3623
	public Image ProgressFill;

	// Token: 0x04000E28 RID: 3624
	public TextMeshProUGUI EncounterName;

	// Token: 0x04000E29 RID: 3625
	public GameObject AttemptCounter;

	// Token: 0x04000E2A RID: 3626
	public TextMeshProUGUI AttemptText;

	// Token: 0x04000E2B RID: 3627
	public RectTransform TooltipAnchor;

	// Token: 0x04000E2C RID: 3628
	private RaidDB.Encounter encounter;

	// Token: 0x02000559 RID: 1369
	[CompilerGenerated]
	private sealed class <RebuildLayout>d__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002492 RID: 9362 RVA: 0x000CE809 File Offset: 0x000CCA09
		[DebuggerHidden]
		public <RebuildLayout>d__12(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000CE818 File Offset: 0x000CCA18
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000CE81C File Offset: 0x000CCA1C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PostGame_RaidEncounter postGame_RaidEncounter = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			LayoutRebuilder.ForceRebuildLayoutImmediate(postGame_RaidEncounter.EncounterName.transform.parent.GetComponent<RectTransform>());
			return false;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x000CE87E File Offset: 0x000CCA7E
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000CE886 File Offset: 0x000CCA86
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x000CE88D File Offset: 0x000CCA8D
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026D2 RID: 9938
		private int <>1__state;

		// Token: 0x040026D3 RID: 9939
		private object <>2__current;

		// Token: 0x040026D4 RID: 9940
		public PostGame_RaidEncounter <>4__this;
	}
}
