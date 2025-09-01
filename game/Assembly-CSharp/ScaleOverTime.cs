using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200023D RID: 573
public class ScaleOverTime : MonoBehaviour
{
	// Token: 0x06001763 RID: 5987 RVA: 0x00093888 File Offset: 0x00091A88
	private void Start()
	{
		if (this.ScaleOnStart)
		{
			this.BeginScale();
		}
	}

	// Token: 0x06001764 RID: 5988 RVA: 0x00093898 File Offset: 0x00091A98
	public void BeginScale()
	{
		base.StopAllCoroutines();
		this.isScalingUp = true;
		base.StartCoroutine(this.Scale());
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x000938B4 File Offset: 0x00091AB4
	public void EndScale()
	{
		base.StopAllCoroutines();
		this.isScalingUp = false;
		base.StartCoroutine(this.Scale());
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000938D0 File Offset: 0x00091AD0
	private IEnumerator Scale()
	{
		float time = 0f;
		float duration = this.isScalingUp ? this.ScaleInTime : this.ScaleOutTime;
		AnimationCurve curve = this.isScalingUp ? this.ScaleIn : this.ScaleOut;
		while (time < duration)
		{
			base.transform.localScale = curve.Evaluate(time / duration) * Vector3.one;
			time += Time.deltaTime;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000938E0 File Offset: 0x00091AE0
	public ScaleOverTime()
	{
	}

	// Token: 0x04001717 RID: 5911
	public AnimationCurve ScaleIn = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001718 RID: 5912
	public float ScaleInTime = 1f;

	// Token: 0x04001719 RID: 5913
	public AnimationCurve ScaleOut = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 0f)
	});

	// Token: 0x0400171A RID: 5914
	public float ScaleOutTime = 1f;

	// Token: 0x0400171B RID: 5915
	public bool ScaleOnStart = true;

	// Token: 0x0400171C RID: 5916
	private bool isScalingUp = true;

	// Token: 0x02000601 RID: 1537
	[CompilerGenerated]
	private sealed class <Scale>d__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026CB RID: 9931 RVA: 0x000D426F File Offset: 0x000D246F
		[DebuggerHidden]
		public <Scale>d__9(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000D427E File Offset: 0x000D247E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000D4280 File Offset: 0x000D2480
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ScaleOverTime scaleOverTime = this;
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
				time = 0f;
				duration = (scaleOverTime.isScalingUp ? scaleOverTime.ScaleInTime : scaleOverTime.ScaleOutTime);
				curve = (scaleOverTime.isScalingUp ? scaleOverTime.ScaleIn : scaleOverTime.ScaleOut);
			}
			if (time >= duration)
			{
				return false;
			}
			scaleOverTime.transform.localScale = curve.Evaluate(time / duration) * Vector3.one;
			time += Time.deltaTime;
			this.<>2__current = null;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x000D4358 File Offset: 0x000D2558
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000D4360 File Offset: 0x000D2560
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x000D4367 File Offset: 0x000D2567
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002975 RID: 10613
		private int <>1__state;

		// Token: 0x04002976 RID: 10614
		private object <>2__current;

		// Token: 0x04002977 RID: 10615
		public ScaleOverTime <>4__this;

		// Token: 0x04002978 RID: 10616
		private float <time>5__2;

		// Token: 0x04002979 RID: 10617
		private float <duration>5__3;

		// Token: 0x0400297A RID: 10618
		private AnimationCurve <curve>5__4;
	}
}
