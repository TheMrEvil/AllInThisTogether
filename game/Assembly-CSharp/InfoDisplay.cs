using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Coffee.UIExtensions;
using TMPro;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class InfoDisplay : MonoBehaviour
{
	// Token: 0x060012F0 RID: 4848 RVA: 0x00074980 File Offset: 0x00072B80
	public static void Init()
	{
		InfoDisplay.instances = new Dictionary<InfoArea, InfoDisplay>();
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x0007498C File Offset: 0x00072B8C
	private void Awake()
	{
		if (!InfoDisplay.instances.ContainsKey(this.DisplayArea))
		{
			InfoDisplay.instances.Add(this.DisplayArea, this);
		}
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.dissolve = base.GetComponentInChildren<UIDissolve>();
		if (this.dissolve != null)
		{
			this.dissolve.effectFactor = 1f;
		}
		this.canvasGroup.alpha = 0f;
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x00074A02 File Offset: 0x00072C02
	public static void SetText(string text, float displayDuration, InfoArea displayArea)
	{
		if (!InfoDisplay.instances.ContainsKey(displayArea))
		{
			return;
		}
		InfoDisplay.instances[displayArea].DoSetText(text, displayDuration);
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x00074A24 File Offset: 0x00072C24
	public static void Reset()
	{
		if (InfoDisplay.instances == null)
		{
			return;
		}
		foreach (KeyValuePair<InfoArea, InfoDisplay> keyValuePair in InfoDisplay.instances)
		{
			keyValuePair.Value.ResetState();
		}
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x00074A84 File Offset: 0x00072C84
	private void ResetState()
	{
		base.StopAllCoroutines();
		this.canvasGroup.alpha = 0f;
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x00074A9C File Offset: 0x00072C9C
	private void DoSetText(string text, float displayDuration)
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.DisplaySequence(displayDuration, text));
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x00074AB3 File Offset: 0x00072CB3
	public static void ReleaseText(InfoArea displayArea)
	{
		if (!InfoDisplay.instances.ContainsKey(displayArea))
		{
			return;
		}
		InfoDisplay.instances[displayArea].Release();
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x00074AD3 File Offset: 0x00072CD3
	private void Release()
	{
		base.StopAllCoroutines();
		if (this.canvasGroup.alpha > 0f)
		{
			base.StartCoroutine(this.FadeOut());
		}
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x00074AFA File Offset: 0x00072CFA
	private IEnumerator DisplaySequence(float duration, string textVal)
	{
		if (this.dissolve != null)
		{
			while (this.dissolve.effectFactor < 1f)
			{
				this.dissolve.effectFactor += Time.deltaTime;
				yield return true;
			}
		}
		if (textVal != this.displayText.text)
		{
			this.displayText.text = textVal;
		}
		this.InDissolve = base.StartCoroutine(this.DissolveIn());
		while (this.canvasGroup.alpha < 1f)
		{
			this.canvasGroup.alpha += Time.deltaTime * 2f;
			yield return true;
		}
		if (duration > 0f)
		{
			yield return new WaitForSeconds(duration);
			base.StartCoroutine(this.FadeOut());
		}
		yield break;
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x00074B17 File Offset: 0x00072D17
	private IEnumerator FadeOut()
	{
		if (this.InDissolve != null)
		{
			base.StopCoroutine(this.InDissolve);
		}
		base.StartCoroutine(this.DissolveOut());
		while (this.canvasGroup.alpha > 0f)
		{
			this.canvasGroup.alpha -= Time.deltaTime;
			yield return true;
		}
		this.canvasGroup.alpha = 0f;
		yield break;
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x00074B26 File Offset: 0x00072D26
	private IEnumerator DissolveIn()
	{
		if (this.dissolve == null)
		{
			yield break;
		}
		yield return new WaitForSeconds(0.25f);
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			this.dissolve.effectFactor = 1f - t;
			yield return true;
		}
		yield break;
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x00074B35 File Offset: 0x00072D35
	private IEnumerator DissolveOut()
	{
		if (this.dissolve == null)
		{
			yield break;
		}
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			this.dissolve.effectFactor = t;
			yield return true;
		}
		yield break;
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x00074B44 File Offset: 0x00072D44
	public InfoDisplay()
	{
	}

	// Token: 0x04001210 RID: 4624
	private CanvasGroup canvasGroup;

	// Token: 0x04001211 RID: 4625
	public TextMeshProUGUI displayText;

	// Token: 0x04001212 RID: 4626
	private UIDissolve dissolve;

	// Token: 0x04001213 RID: 4627
	public InfoArea DisplayArea;

	// Token: 0x04001214 RID: 4628
	private Coroutine seq;

	// Token: 0x04001215 RID: 4629
	public static Dictionary<InfoArea, InfoDisplay> instances;

	// Token: 0x04001216 RID: 4630
	private Coroutine InDissolve;

	// Token: 0x0200058D RID: 1421
	[CompilerGenerated]
	private sealed class <DisplaySequence>d__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600255D RID: 9565 RVA: 0x000D134E File Offset: 0x000CF54E
		[DebuggerHidden]
		public <DisplaySequence>d__15(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000D135D File Offset: 0x000CF55D
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000D1360 File Offset: 0x000CF560
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			InfoDisplay infoDisplay = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				if (!(infoDisplay.dissolve != null))
				{
					goto IL_82;
				}
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_F8;
			case 3:
				this.<>1__state = -1;
				infoDisplay.StartCoroutine(infoDisplay.FadeOut());
				return false;
			default:
				return false;
			}
			if (infoDisplay.dissolve.effectFactor < 1f)
			{
				infoDisplay.dissolve.effectFactor += Time.deltaTime;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			IL_82:
			if (textVal != infoDisplay.displayText.text)
			{
				infoDisplay.displayText.text = textVal;
			}
			infoDisplay.InDissolve = infoDisplay.StartCoroutine(infoDisplay.DissolveIn());
			IL_F8:
			if (infoDisplay.canvasGroup.alpha < 1f)
			{
				infoDisplay.canvasGroup.alpha += Time.deltaTime * 2f;
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			if (duration > 0f)
			{
				this.<>2__current = new WaitForSeconds(duration);
				this.<>1__state = 3;
				return true;
			}
			return false;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000D14B3 File Offset: 0x000CF6B3
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000D14BB File Offset: 0x000CF6BB
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000D14C2 File Offset: 0x000CF6C2
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040027A3 RID: 10147
		private int <>1__state;

		// Token: 0x040027A4 RID: 10148
		private object <>2__current;

		// Token: 0x040027A5 RID: 10149
		public InfoDisplay <>4__this;

		// Token: 0x040027A6 RID: 10150
		public string textVal;

		// Token: 0x040027A7 RID: 10151
		public float duration;
	}

	// Token: 0x0200058E RID: 1422
	[CompilerGenerated]
	private sealed class <FadeOut>d__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002563 RID: 9571 RVA: 0x000D14CA File Offset: 0x000CF6CA
		[DebuggerHidden]
		public <FadeOut>d__16(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000D14D9 File Offset: 0x000CF6D9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x000D14DC File Offset: 0x000CF6DC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			InfoDisplay infoDisplay = this;
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
				if (infoDisplay.InDissolve != null)
				{
					infoDisplay.StopCoroutine(infoDisplay.InDissolve);
				}
				infoDisplay.StartCoroutine(infoDisplay.DissolveOut());
			}
			if (infoDisplay.canvasGroup.alpha <= 0f)
			{
				infoDisplay.canvasGroup.alpha = 0f;
				return false;
			}
			infoDisplay.canvasGroup.alpha -= Time.deltaTime;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x000D1580 File Offset: 0x000CF780
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000D1588 File Offset: 0x000CF788
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x000D158F File Offset: 0x000CF78F
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040027A8 RID: 10152
		private int <>1__state;

		// Token: 0x040027A9 RID: 10153
		private object <>2__current;

		// Token: 0x040027AA RID: 10154
		public InfoDisplay <>4__this;
	}

	// Token: 0x0200058F RID: 1423
	[CompilerGenerated]
	private sealed class <DissolveIn>d__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002569 RID: 9577 RVA: 0x000D1597 File Offset: 0x000CF797
		[DebuggerHidden]
		public <DissolveIn>d__17(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000D15A6 File Offset: 0x000CF7A6
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000D15A8 File Offset: 0x000CF7A8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			InfoDisplay infoDisplay = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				if (infoDisplay.dissolve == null)
				{
					return false;
				}
				this.<>2__current = new WaitForSeconds(0.25f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t >= 1f)
			{
				return false;
			}
			t += Time.deltaTime;
			infoDisplay.dissolve.effectFactor = 1f - t;
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x000D166E File Offset: 0x000CF86E
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000D1676 File Offset: 0x000CF876
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x000D167D File Offset: 0x000CF87D
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040027AB RID: 10155
		private int <>1__state;

		// Token: 0x040027AC RID: 10156
		private object <>2__current;

		// Token: 0x040027AD RID: 10157
		public InfoDisplay <>4__this;

		// Token: 0x040027AE RID: 10158
		private float <t>5__2;
	}

	// Token: 0x02000590 RID: 1424
	[CompilerGenerated]
	private sealed class <DissolveOut>d__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600256F RID: 9583 RVA: 0x000D1685 File Offset: 0x000CF885
		[DebuggerHidden]
		public <DissolveOut>d__18(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000D1694 File Offset: 0x000CF894
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000D1698 File Offset: 0x000CF898
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			InfoDisplay infoDisplay = this;
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
				if (infoDisplay.dissolve == null)
				{
					return false;
				}
				t = 0f;
			}
			if (t >= 1f)
			{
				return false;
			}
			t += Time.deltaTime;
			infoDisplay.dissolve.effectFactor = t;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x000D172D File Offset: 0x000CF92D
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000D1735 File Offset: 0x000CF935
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x000D173C File Offset: 0x000CF93C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040027AF RID: 10159
		private int <>1__state;

		// Token: 0x040027B0 RID: 10160
		private object <>2__current;

		// Token: 0x040027B1 RID: 10161
		public InfoDisplay <>4__this;

		// Token: 0x040027B2 RID: 10162
		private float <t>5__2;
	}
}
