using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003B6 RID: 950
	public class SmoothPosition : MonoBehaviour
	{
		// Token: 0x06001F5B RID: 8027 RVA: 0x000BB5F0 File Offset: 0x000B97F0
		private void Awake()
		{
			if (this.target == null)
			{
				this.target = base.transform.parent;
			}
			this.tr = base.transform;
			this.currentPosition = base.transform.position;
			this.localPositionOffset = this.tr.localPosition;
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x000BB64A File Offset: 0x000B984A
		private void OnEnable()
		{
			this.ResetCurrentPosition();
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x000BB652 File Offset: 0x000B9852
		private void Update()
		{
			if (this.updateType != SmoothPosition.UpdateType.Update)
			{
				return;
			}
			this.SmoothUpdate();
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000BB663 File Offset: 0x000B9863
		private void LateUpdate()
		{
			if (this.updateType != SmoothPosition.UpdateType.LateUpdate)
			{
				return;
			}
			this.SmoothUpdate();
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000BB675 File Offset: 0x000B9875
		private void FixedUpdate()
		{
			if (this.updateType != SmoothPosition.UpdateType.FixedUpdate)
			{
				return;
			}
			this.SmoothUpdate();
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000BB687 File Offset: 0x000B9887
		private void SmoothUpdate()
		{
			this.currentPosition = this.Smooth(this.currentPosition, this.target.position, this.lerpSpeed);
			this.tr.position = this.currentPosition;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000BB6C0 File Offset: 0x000B98C0
		private Vector3 Smooth(Vector3 _start, Vector3 _target, float _smoothTime)
		{
			Vector3 b = this.tr.localToWorldMatrix * this.localPositionOffset;
			if (this.extrapolatePosition)
			{
				Vector3 b2 = _target - (_start - b);
				_target += b2;
			}
			_target += b;
			SmoothPosition.SmoothType smoothType = this.smoothType;
			Vector3 result;
			if (smoothType != SmoothPosition.SmoothType.Lerp)
			{
				if (smoothType != SmoothPosition.SmoothType.SmoothDamp)
				{
					result = Vector3.zero;
				}
				else
				{
					result = Vector3.SmoothDamp(_start, _target, ref this.refVelocity, this.smoothDampTime);
				}
			}
			else
			{
				result = Vector3.Lerp(_start, _target, Time.deltaTime * _smoothTime);
			}
			if (this.verticalSmoothMultiplier != 1f)
			{
				smoothType = this.verticalSmoothType;
				float y;
				if (smoothType != SmoothPosition.SmoothType.Lerp)
				{
					if (smoothType != SmoothPosition.SmoothType.SmoothDamp)
					{
						y = 0f;
					}
					else
					{
						y = Mathf.SmoothDamp(_start.y, _target.y, ref this.refVelocity.y, this.smoothDampTime * this.verticalSmoothMultiplier);
					}
				}
				else
				{
					y = Mathf.Lerp(_start.y, _target.y, Time.deltaTime * _smoothTime * this.verticalSmoothMultiplier);
				}
				result.y = y;
			}
			return result;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x000BB7DC File Offset: 0x000B99DC
		public void ResetCurrentPosition()
		{
			Vector3 b = this.tr.localToWorldMatrix * this.localPositionOffset;
			this.currentPosition = this.target.position + b;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x000BB821 File Offset: 0x000B9A21
		public SmoothPosition()
		{
		}

		// Token: 0x04001F9F RID: 8095
		public Transform target;

		// Token: 0x04001FA0 RID: 8096
		private Transform tr;

		// Token: 0x04001FA1 RID: 8097
		private Vector3 currentPosition;

		// Token: 0x04001FA2 RID: 8098
		public float lerpSpeed = 20f;

		// Token: 0x04001FA3 RID: 8099
		public float smoothDampTime = 0.02f;

		// Token: 0x04001FA4 RID: 8100
		public float verticalSmoothMultiplier = 1f;

		// Token: 0x04001FA5 RID: 8101
		public bool extrapolatePosition;

		// Token: 0x04001FA6 RID: 8102
		public SmoothPosition.UpdateType updateType;

		// Token: 0x04001FA7 RID: 8103
		public SmoothPosition.SmoothType smoothType;

		// Token: 0x04001FA8 RID: 8104
		public SmoothPosition.SmoothType verticalSmoothType;

		// Token: 0x04001FA9 RID: 8105
		private Vector3 localPositionOffset;

		// Token: 0x04001FAA RID: 8106
		private Vector3 refVelocity;

		// Token: 0x02000699 RID: 1689
		public enum UpdateType
		{
			// Token: 0x04002C40 RID: 11328
			Update,
			// Token: 0x04002C41 RID: 11329
			LateUpdate,
			// Token: 0x04002C42 RID: 11330
			FixedUpdate
		}

		// Token: 0x0200069A RID: 1690
		public enum SmoothType
		{
			// Token: 0x04002C44 RID: 11332
			Lerp,
			// Token: 0x04002C45 RID: 11333
			SmoothDamp
		}
	}
}
