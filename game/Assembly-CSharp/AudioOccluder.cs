using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200002A RID: 42
[RequireComponent(typeof(AudioLowPassFilter))]
public class AudioOccluder : MonoBehaviour
{
	// Token: 0x06000153 RID: 339 RVA: 0x0000DF38 File Offset: 0x0000C138
	private void Awake()
	{
		this.filter = base.GetComponent<AudioLowPassFilter>();
		this.checkInterval = UnityEngine.Random.Range(0.075f, 0.2f);
		base.InvokeRepeating("CheckOcclusion", this.checkInterval, this.checkInterval);
		base.StartCoroutine(this.UpdateFilter());
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0000DF8C File Offset: 0x0000C18C
	private void CheckOcclusion()
	{
		Vector3 position = base.transform.position;
		if (PlayerControl.MyCamera != null)
		{
			position = PlayerControl.MyCamera.transform.position;
		}
		else
		{
			if (this.mCamera == null)
			{
				Camera main = Camera.main;
				this.mCamera = ((main != null) ? main.transform : null);
			}
			if (this.mCamera != null)
			{
				position = this.mCamera.position;
			}
		}
		Vector3 position2 = base.transform.position;
		Ray ray = new Ray(position2, (position - position2).normalized);
		float num = Vector3.Distance(base.transform.position, position);
		if (Physics.Raycast(ray, out this.hit, num, this.RayMask))
		{
			this.wantFilter = this.FilterCurve.Evaluate(this.hit.distance / num);
			UnityEngine.Debug.DrawRay(ray.origin, ray.direction * this.hit.distance, Color.red, this.checkInterval);
			return;
		}
		this.wantFilter = this.FilterCurve.Evaluate(1f);
		UnityEngine.Debug.DrawRay(ray.origin, ray.direction * num, Color.green, this.checkInterval);
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000E0DA File Offset: 0x0000C2DA
	private IEnumerator UpdateFilter()
	{
		for (;;)
		{
			this.filter.cutoffFrequency = Mathf.Lerp(this.filter.cutoffFrequency, this.wantFilter, Time.deltaTime * 2f);
			yield return true;
		}
		yield break;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x0000E0E9 File Offset: 0x0000C2E9
	public AudioOccluder()
	{
	}

	// Token: 0x04000177 RID: 375
	public LayerMask RayMask;

	// Token: 0x04000178 RID: 376
	private AudioLowPassFilter filter;

	// Token: 0x04000179 RID: 377
	private float checkInterval = 0.1f;

	// Token: 0x0400017A RID: 378
	private float wantFilter = 5000f;

	// Token: 0x0400017B RID: 379
	public AnimationCurve FilterCurve;

	// Token: 0x0400017C RID: 380
	private RaycastHit hit;

	// Token: 0x0400017D RID: 381
	private Transform mCamera;

	// Token: 0x020003FA RID: 1018
	[CompilerGenerated]
	private sealed class <UpdateFilter>d__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600208C RID: 8332 RVA: 0x000C08BE File Offset: 0x000BEABE
		[DebuggerHidden]
		public <UpdateFilter>d__9(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000C08CD File Offset: 0x000BEACD
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000C08D0 File Offset: 0x000BEAD0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AudioOccluder audioOccluder = this;
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
			audioOccluder.filter.cutoffFrequency = Mathf.Lerp(audioOccluder.filter.cutoffFrequency, audioOccluder.wantFilter, Time.deltaTime * 2f);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x000C0944 File Offset: 0x000BEB44
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000C094C File Offset: 0x000BEB4C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x000C0953 File Offset: 0x000BEB53
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002119 RID: 8473
		private int <>1__state;

		// Token: 0x0400211A RID: 8474
		private object <>2__current;

		// Token: 0x0400211B RID: 8475
		public AudioOccluder <>4__this;
	}
}
