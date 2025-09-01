using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DF RID: 479
public class ErrorPrompt : MonoBehaviour
{
	// Token: 0x06001402 RID: 5122 RVA: 0x0007C803 File Offset: 0x0007AA03
	private void Awake()
	{
		ErrorPrompt.instance = this;
		ErrorPrompt.IsInPrompt = false;
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.CheckInputMethod));
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x0007C831 File Offset: 0x0007AA31
	public static void OpenPrompt(string description, Action responseAction = null)
	{
		ErrorPrompt.instance.OnActivate();
		ErrorPrompt.instance.OnResponse = responseAction;
		ErrorPrompt.instance.BodyText.text = description;
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x0007C858 File Offset: 0x0007AA58
	public void DebugActivate()
	{
		ErrorPrompt.OpenPrompt("Test Error", null);
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x0007C868 File Offset: 0x0007AA68
	private void OnActivate()
	{
		this.BlurBack.SetActive(true);
		ErrorPrompt.IsInPrompt = true;
		this.Fader.UpdateOpacity(true, 1f, false);
		this.Fader.blocksRaycasts = true;
		this.CheckInputMethod();
		base.StartCoroutine("RebuildLayout");
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x0007C8B7 File Offset: 0x0007AAB7
	private IEnumerator RebuildLayout()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.LayoutGroup);
		yield break;
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x0007C8C8 File Offset: 0x0007AAC8
	private void CheckInputMethod()
	{
		foreach (GameObject gameObject in this.ControllerPrompts)
		{
			gameObject.SetActive(InputManager.IsUsingController);
		}
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x0007C920 File Offset: 0x0007AB20
	private void Update()
	{
		this.Fader.UpdateOpacity(ErrorPrompt.IsInPrompt, 4f, false);
		bool flag = ErrorPrompt.IsInPrompt || this.Fader.alpha > 0f;
		if (this.BlurBack.activeSelf != flag)
		{
			this.BlurBack.gameObject.SetActive(flag);
		}
		if (ErrorPrompt.IsInPrompt && InputManager.UIAct.UIBack.WasPressed)
		{
			this.Close();
		}
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x0007C99D File Offset: 0x0007AB9D
	public void Close()
	{
		if (!ErrorPrompt.IsInPrompt)
		{
			return;
		}
		ErrorPrompt.IsInPrompt = false;
		this.Fader.blocksRaycasts = false;
		Action onResponse = this.OnResponse;
		if (onResponse == null)
		{
			return;
		}
		onResponse();
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x0007C9C9 File Offset: 0x0007ABC9
	public ErrorPrompt()
	{
	}

	// Token: 0x04001324 RID: 4900
	public static ErrorPrompt instance;

	// Token: 0x04001325 RID: 4901
	public static bool IsInPrompt;

	// Token: 0x04001326 RID: 4902
	public CanvasGroup Fader;

	// Token: 0x04001327 RID: 4903
	public TextMeshProUGUI BodyText;

	// Token: 0x04001328 RID: 4904
	public List<GameObject> ControllerPrompts;

	// Token: 0x04001329 RID: 4905
	public GameObject BlurBack;

	// Token: 0x0400132A RID: 4906
	public RectTransform LayoutGroup;

	// Token: 0x0400132B RID: 4907
	private Action OnResponse;

	// Token: 0x020005A9 RID: 1449
	[CompilerGenerated]
	private sealed class <RebuildLayout>d__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025AB RID: 9643 RVA: 0x000D2039 File Offset: 0x000D0239
		[DebuggerHidden]
		public <RebuildLayout>d__12(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000D2048 File Offset: 0x000D0248
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000D204C File Offset: 0x000D024C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ErrorPrompt errorPrompt = this;
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
			LayoutRebuilder.ForceRebuildLayoutImmediate(errorPrompt.LayoutGroup);
			return false;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x000D209F File Offset: 0x000D029F
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000D20A7 File Offset: 0x000D02A7
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x000D20AE File Offset: 0x000D02AE
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002819 RID: 10265
		private int <>1__state;

		// Token: 0x0400281A RID: 10266
		private object <>2__current;

		// Token: 0x0400281B RID: 10267
		public ErrorPrompt <>4__this;
	}
}
