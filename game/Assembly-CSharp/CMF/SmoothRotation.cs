using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003B7 RID: 951
	public class SmoothRotation : MonoBehaviour
	{
		// Token: 0x06001F64 RID: 8036 RVA: 0x000BB84A File Offset: 0x000B9A4A
		private void Awake()
		{
			if (this.target == null)
			{
				this.target = base.transform.parent;
			}
			this.tr = base.transform;
			this.currentRotation = base.transform.rotation;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000BB888 File Offset: 0x000B9A88
		private void OnEnable()
		{
			this.ResetCurrentRotation();
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000BB890 File Offset: 0x000B9A90
		private void Update()
		{
			if (this.updateType != SmoothRotation.UpdateType.Update)
			{
				return;
			}
			this.SmoothUpdate();
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000BB8A1 File Offset: 0x000B9AA1
		private void LateUpdate()
		{
			if (this.updateType != SmoothRotation.UpdateType.LateUpdate)
			{
				return;
			}
			this.SmoothUpdate();
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000BB8B3 File Offset: 0x000B9AB3
		private void FixedUpdate()
		{
			if (this.updateType != SmoothRotation.UpdateType.FixedUpdate)
			{
				return;
			}
			this.SmoothUpdate();
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x000BB8C5 File Offset: 0x000B9AC5
		private void SmoothUpdate()
		{
			this.currentRotation = this.Smooth(this.currentRotation, this.target.rotation, this.smoothSpeed);
			this.tr.rotation = this.currentRotation;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x000BB8FC File Offset: 0x000B9AFC
		private Quaternion Smooth(Quaternion _currentRotation, Quaternion _targetRotation, float _smoothSpeed)
		{
			if (this.extrapolateRotation && Quaternion.Angle(_currentRotation, _targetRotation) < 90f)
			{
				Quaternion rhs = _targetRotation * Quaternion.Inverse(_currentRotation);
				_targetRotation *= rhs;
			}
			return Quaternion.Slerp(_currentRotation, _targetRotation, Time.deltaTime * _smoothSpeed);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x000BB943 File Offset: 0x000B9B43
		public void ResetCurrentRotation()
		{
			this.currentRotation = this.target.rotation;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x000BB956 File Offset: 0x000B9B56
		public SmoothRotation()
		{
		}

		// Token: 0x04001FAB RID: 8107
		public Transform target;

		// Token: 0x04001FAC RID: 8108
		private Transform tr;

		// Token: 0x04001FAD RID: 8109
		private Quaternion currentRotation;

		// Token: 0x04001FAE RID: 8110
		public float smoothSpeed = 20f;

		// Token: 0x04001FAF RID: 8111
		public bool extrapolateRotation;

		// Token: 0x04001FB0 RID: 8112
		public SmoothRotation.UpdateType updateType;

		// Token: 0x0200069B RID: 1691
		public enum UpdateType
		{
			// Token: 0x04002C47 RID: 11335
			Update,
			// Token: 0x04002C48 RID: 11336
			LateUpdate,
			// Token: 0x04002C49 RID: 11337
			FixedUpdate
		}
	}
}
