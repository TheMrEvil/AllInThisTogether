using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000159 RID: 345
	public abstract class CharacterAnimationBase : MonoBehaviour
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x0005A480 File Offset: 0x00058680
		public virtual Vector3 GetPivotPoint()
		{
			return base.transform.position;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0005A48D File Offset: 0x0005868D
		public virtual bool animationGrounded
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0005A490 File Offset: 0x00058690
		public float GetAngleFromForward(Vector3 worldDirection)
		{
			Vector3 vector = base.transform.InverseTransformDirection(worldDirection);
			return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0005A4C4 File Offset: 0x000586C4
		protected virtual void Start()
		{
			if (base.transform.parent.GetComponent<CharacterBase>() == null)
			{
				Debug.LogWarning("Animation controllers should be parented to character controllers!", base.transform);
			}
			this.lastPosition = base.transform.position;
			this.localPosition = base.transform.localPosition;
			this.lastRotation = base.transform.rotation;
			this.localRotation = base.transform.localRotation;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0005A53D File Offset: 0x0005873D
		protected virtual void LateUpdate()
		{
			if (this.animatePhysics)
			{
				return;
			}
			this.SmoothFollow();
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0005A54E File Offset: 0x0005874E
		protected virtual void FixedUpdate()
		{
			if (!this.animatePhysics)
			{
				return;
			}
			this.SmoothFollow();
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0005A560 File Offset: 0x00058760
		private void SmoothFollow()
		{
			if (this.smoothFollow)
			{
				base.transform.position = Vector3.Lerp(this.lastPosition, base.transform.parent.TransformPoint(this.localPosition), Time.deltaTime * this.smoothFollowSpeed);
				base.transform.rotation = Quaternion.Lerp(this.lastRotation, base.transform.parent.rotation * this.localRotation, Time.deltaTime * this.smoothFollowSpeed);
			}
			else
			{
				base.transform.localPosition = this.localPosition;
				base.transform.localRotation = this.localRotation;
			}
			this.lastPosition = base.transform.position;
			this.lastRotation = base.transform.rotation;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0005A630 File Offset: 0x00058830
		protected CharacterAnimationBase()
		{
		}

		// Token: 0x04000B1F RID: 2847
		public bool smoothFollow = true;

		// Token: 0x04000B20 RID: 2848
		public float smoothFollowSpeed = 20f;

		// Token: 0x04000B21 RID: 2849
		protected bool animatePhysics;

		// Token: 0x04000B22 RID: 2850
		private Vector3 lastPosition;

		// Token: 0x04000B23 RID: 2851
		private Vector3 localPosition;

		// Token: 0x04000B24 RID: 2852
		private Quaternion localRotation;

		// Token: 0x04000B25 RID: 2853
		private Quaternion lastRotation;
	}
}
