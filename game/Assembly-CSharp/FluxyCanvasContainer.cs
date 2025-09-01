using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Fluxy;
using UnityEngine;

// Token: 0x02000197 RID: 407
[RequireComponent(typeof(FluxyContainer))]
public class FluxyCanvasContainer : MonoBehaviour
{
	// Token: 0x1700013D RID: 317
	// (get) Token: 0x0600111C RID: 4380 RVA: 0x0006A384 File Offset: 0x00068584
	public float Opacity
	{
		get
		{
			if (this.canvasGrps == null || this.canvasGrps.Length == 0)
			{
				return 1f;
			}
			float num = 1f;
			CanvasGroup[] array = this.canvasGrps;
			for (int i = 0; i < array.Length; i++)
			{
				num = Mathf.Min(array[i].alpha, num);
			}
			return num;
		}
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x0006A3D4 File Offset: 0x000685D4
	private void Awake()
	{
		UIFluidManager.AddContainer(this);
		this.container = base.GetComponent<FluxyContainer>();
		this.canvasGrps = base.GetComponentsInParent<CanvasGroup>();
		this.container.opacity = this.Opacity;
		this.fluidMat = base.GetComponent<MeshRenderer>().material;
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x0006A421 File Offset: 0x00068621
	private void Update()
	{
		this.container.opacity = this.Opacity;
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0006A434 File Offset: 0x00068634
	public void AddTarget(FluxyTarget target)
	{
		this.container.targets = this.container.targets.Concat(new FluxyTarget[]
		{
			target
		}).ToArray<FluxyTarget>();
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0006A460 File Offset: 0x00068660
	public void DisableContainer()
	{
		this.container.Clear();
		this.container.enabled = false;
		base.enabled = false;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x0006A480 File Offset: 0x00068680
	public void EnableContainer()
	{
		base.enabled = true;
		this.ResetContainer();
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0006A48F File Offset: 0x0006868F
	public void ResetContainer()
	{
		if (this.isResetting)
		{
			return;
		}
		this.isResetting = true;
		this.container.Clear();
		this.container.enabled = false;
		base.StartCoroutine(this.DelayUpdateShape());
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0006A4C5 File Offset: 0x000686C5
	private IEnumerator DelayUpdateShape()
	{
		yield return new WaitForSeconds(0.5f);
		this.container.enabled = true;
		this.isResetting = false;
		yield break;
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0006A4D4 File Offset: 0x000686D4
	private void OnDisable()
	{
		this.isResetting = false;
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x0006A4DD File Offset: 0x000686DD
	public FluxyCanvasContainer()
	{
	}

	// Token: 0x04000F7B RID: 3963
	public UIFluidType FluidType;

	// Token: 0x04000F7C RID: 3964
	private CanvasGroup[] canvasGrps;

	// Token: 0x04000F7D RID: 3965
	private FluxyContainer container;

	// Token: 0x04000F7E RID: 3966
	private bool isResetting;

	// Token: 0x04000F7F RID: 3967
	private Material fluidMat;

	// Token: 0x02000567 RID: 1383
	[CompilerGenerated]
	private sealed class <DelayUpdateShape>d__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024C7 RID: 9415 RVA: 0x000CF5B9 File Offset: 0x000CD7B9
		[DebuggerHidden]
		public <DelayUpdateShape>d__13(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000CF5C8 File Offset: 0x000CD7C8
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000CF5CC File Offset: 0x000CD7CC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			FluxyCanvasContainer fluxyCanvasContainer = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			fluxyCanvasContainer.container.enabled = true;
			fluxyCanvasContainer.isResetting = false;
			return false;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x000CF62B File Offset: 0x000CD82B
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000CF633 File Offset: 0x000CD833
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x000CF63A File Offset: 0x000CD83A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002709 RID: 9993
		private int <>1__state;

		// Token: 0x0400270A RID: 9994
		private object <>2__current;

		// Token: 0x0400270B RID: 9995
		public FluxyCanvasContainer <>4__this;
	}
}
