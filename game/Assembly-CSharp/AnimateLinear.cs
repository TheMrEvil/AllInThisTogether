using System;
using UnityEngine;

// Token: 0x0200021A RID: 538
public class AnimateLinear : MonoBehaviour
{
	// Token: 0x060016AD RID: 5805 RVA: 0x0008F5A4 File Offset: 0x0008D7A4
	private void Awake()
	{
		this.startPos = base.transform.localPosition;
		if (this.animateAtStart)
		{
			this.animationDirection = 1;
		}
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x0008F5C8 File Offset: 0x0008D7C8
	private void Update()
	{
		if (this.animationDirection != 0)
		{
			this.t += Time.deltaTime * (float)this.animationDirection / this.animDuration;
			base.transform.localPosition = Vector3.Lerp(this.startPos, this.startPos + this.animOffset, this.moveCurve.Evaluate(this.t));
			if (this.t >= 1f && this.animationDirection > 0)
			{
				this.animationDirection = 0;
				return;
			}
			if (this.t <= 0f && this.animationDirection < 0)
			{
				this.animationDirection = 0;
			}
		}
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x0008F674 File Offset: 0x0008D874
	public void PlayForward()
	{
		this.animationDirection = 1;
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x0008F67D File Offset: 0x0008D87D
	public void PlayReverse()
	{
		this.animationDirection = -1;
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x0008F686 File Offset: 0x0008D886
	public AnimateLinear()
	{
	}

	// Token: 0x0400163C RID: 5692
	public AnimationCurve moveCurve;

	// Token: 0x0400163D RID: 5693
	public Vector3 animOffset;

	// Token: 0x0400163E RID: 5694
	public float animDuration;

	// Token: 0x0400163F RID: 5695
	private Vector3 startPos;

	// Token: 0x04001640 RID: 5696
	public bool animateAtStart;

	// Token: 0x04001641 RID: 5697
	private int animationDirection;

	// Token: 0x04001642 RID: 5698
	private float t;
}
