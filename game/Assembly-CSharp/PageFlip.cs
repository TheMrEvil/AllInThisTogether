using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001BD RID: 445
public class PageFlip : MonoBehaviour
{
	// Token: 0x06001253 RID: 4691 RVA: 0x000715C0 File Offset: 0x0006F7C0
	private void Awake()
	{
		PageFlip.instance = this;
		this.image = base.GetComponent<Image>();
		this.mat = new Material(this.image.material);
		this.image.material = this.mat;
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.Curl));
		this.image.enabled = false;
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x00071632 File Offset: 0x0006F832
	public void PlaySketchIntro()
	{
		AudioManager.PlayInterfaceSFX(this.SketchIntro, 1f, 1f);
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x00071649 File Offset: 0x0006F849
	public void DoFlipInstant()
	{
		this.Save();
		this.Curl(true);
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x00071658 File Offset: 0x0006F858
	public void Save()
	{
		this.image.enabled = false;
		this.cam = PlayerControl.MyCamera;
		if (TutorialManager.InTutorial && LibraryManager.instance != null)
		{
			this.cam = LibraryManager.instance.LoadingCamera;
		}
		if (this.cam == null)
		{
			this.cam = Camera.main;
		}
		if (this.cam == null)
		{
			return;
		}
		this.mat.SetFloat("flip_time", 0f);
		this.mat.SetFloat("_MaskFade", 0f);
		base.StartCoroutine("SaveDelayed");
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x000716FE File Offset: 0x0006F8FE
	private IEnumerator SaveDelayed()
	{
		if (this.renderTexture != null)
		{
			this.renderTexture.Release();
		}
		this.renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
		this.renderTexture.width = Screen.width;
		this.renderTexture.height = Screen.height;
		this.mat.SetTexture("_SourceTex", this.renderTexture);
		this.cam.targetTexture = this.renderTexture;
		this.cam.Render();
		this.cam.targetTexture = null;
		this.image.enabled = true;
		yield return true;
		yield break;
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x00071710 File Offset: 0x0006F910
	public void Curl()
	{
		bool flag = MapManager.InLobbyScene && LibraryManager.WantAntechamberSpawn;
		this.Curl(flag);
		if (flag)
		{
			PostFXManager.UpdateSketch(0f);
		}
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x00071744 File Offset: 0x0006F944
	public void Curl(bool noSketch)
	{
		if (!CanvasController.instance.wantGameVisible)
		{
			return;
		}
		this.image.enabled = true;
		AudioManager.PlaySFX2D(this.FlipClips.GetRandomClip(-1), 1f, 0.1f);
		base.StartCoroutine("CurlSequence", noSketch);
	}

	// Token: 0x0600125A RID: 4698 RVA: 0x00071797 File Offset: 0x0006F997
	private IEnumerator CurlSequence(bool noSketch)
	{
		yield return true;
		yield return true;
		float t = 0f;
		while (t < 1f)
		{
			yield return true;
			t += Time.deltaTime;
			this.mat.SetFloat("flip_time", t);
		}
		this.mat.SetFloat("flip_time", 1f);
		this.EndCurl();
		if (!noSketch)
		{
			PostFXManager.instance.ReleaseSketch();
		}
		yield break;
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000717AD File Offset: 0x0006F9AD
	private void EndCurl()
	{
		this.image.enabled = false;
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x000717BB File Offset: 0x0006F9BB
	public PageFlip()
	{
	}

	// Token: 0x04001143 RID: 4419
	public static PageFlip instance;

	// Token: 0x04001144 RID: 4420
	private Image image;

	// Token: 0x04001145 RID: 4421
	private Material mat;

	// Token: 0x04001146 RID: 4422
	public RenderTexture renderTexture;

	// Token: 0x04001147 RID: 4423
	public AudioClip SketchIntro;

	// Token: 0x04001148 RID: 4424
	public List<AudioClip> FlipClips;

	// Token: 0x04001149 RID: 4425
	private Camera cam;

	// Token: 0x02000581 RID: 1409
	[CompilerGenerated]
	private sealed class <SaveDelayed>d__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002527 RID: 9511 RVA: 0x000D0AD3 File Offset: 0x000CECD3
		[DebuggerHidden]
		public <SaveDelayed>d__11(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000D0AE2 File Offset: 0x000CECE2
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000D0AE4 File Offset: 0x000CECE4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PageFlip pageFlip = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				if (pageFlip.renderTexture != null)
				{
					pageFlip.renderTexture.Release();
				}
				pageFlip.renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
				pageFlip.renderTexture.width = Screen.width;
				pageFlip.renderTexture.height = Screen.height;
				pageFlip.mat.SetTexture("_SourceTex", pageFlip.renderTexture);
				pageFlip.cam.targetTexture = pageFlip.renderTexture;
				pageFlip.cam.Render();
				pageFlip.cam.targetTexture = null;
				pageFlip.image.enabled = true;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			return false;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x000D0BC8 File Offset: 0x000CEDC8
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000D0BD0 File Offset: 0x000CEDD0
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x000D0BD7 File Offset: 0x000CEDD7
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002774 RID: 10100
		private int <>1__state;

		// Token: 0x04002775 RID: 10101
		private object <>2__current;

		// Token: 0x04002776 RID: 10102
		public PageFlip <>4__this;
	}

	// Token: 0x02000582 RID: 1410
	[CompilerGenerated]
	private sealed class <CurlSequence>d__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600252D RID: 9517 RVA: 0x000D0BDF File Offset: 0x000CEDDF
		[DebuggerHidden]
		public <CurlSequence>d__14(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000D0BEE File Offset: 0x000CEDEE
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x000D0BF0 File Offset: 0x000CEDF0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PageFlip pageFlip = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 3:
				this.<>1__state = -1;
				t += Time.deltaTime;
				pageFlip.mat.SetFloat("flip_time", t);
				break;
			default:
				return false;
			}
			if (t >= 1f)
			{
				pageFlip.mat.SetFloat("flip_time", 1f);
				pageFlip.EndCurl();
				if (!noSketch)
				{
					PostFXManager.instance.ReleaseSketch();
				}
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 3;
			return true;
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x000D0CEE File Offset: 0x000CEEEE
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000D0CF6 File Offset: 0x000CEEF6
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x000D0CFD File Offset: 0x000CEEFD
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002777 RID: 10103
		private int <>1__state;

		// Token: 0x04002778 RID: 10104
		private object <>2__current;

		// Token: 0x04002779 RID: 10105
		public PageFlip <>4__this;

		// Token: 0x0400277A RID: 10106
		public bool noSketch;

		// Token: 0x0400277B RID: 10107
		private float <t>5__2;
	}
}
