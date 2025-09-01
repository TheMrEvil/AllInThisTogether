using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A4 RID: 932
	public class CameraDistanceRaycaster : MonoBehaviour
	{
		// Token: 0x06001EC6 RID: 7878 RVA: 0x000B7E50 File Offset: 0x000B6050
		private void Awake()
		{
			this.tr = base.transform;
			this.ignoreListLayers = new int[this.ignoreList.Length];
			this.ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
			if (this.layerMask == (this.layerMask | 1 << this.ignoreRaycastLayer))
			{
				this.layerMask ^= 1 << this.ignoreRaycastLayer;
			}
			if (this.cameraTransform == null)
			{
				Debug.LogWarning("No camera transform has been assigned.", this);
			}
			if (this.cameraTargetTransform == null)
			{
				Debug.LogWarning("No camera target transform has been assigned.", this);
			}
			if (this.cameraTransform == null || this.cameraTargetTransform == null)
			{
				base.enabled = false;
				return;
			}
			this.currentDistance = (this.cameraTargetTransform.position - this.tr.position).magnitude;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000B7F54 File Offset: 0x000B6154
		private void LateUpdate()
		{
			if (this.ignoreListLayers.Length != this.ignoreList.Length)
			{
				this.ignoreListLayers = new int[this.ignoreList.Length];
			}
			for (int i = 0; i < this.ignoreList.Length; i++)
			{
				this.ignoreListLayers[i] = this.ignoreList[i].gameObject.layer;
				this.ignoreList[i].gameObject.layer = this.ignoreRaycastLayer;
			}
			float cameraDistance = this.GetCameraDistance();
			for (int j = 0; j < this.ignoreList.Length; j++)
			{
				this.ignoreList[j].gameObject.layer = this.ignoreListLayers[j];
			}
			this.currentDistance = Mathf.Lerp(this.currentDistance, cameraDistance, Time.deltaTime * this.smoothingFactor);
			this.cameraTransform.position = this.tr.position + (this.cameraTargetTransform.position - this.tr.position).normalized * this.currentDistance;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x000B8068 File Offset: 0x000B6268
		private float GetCameraDistance()
		{
			Vector3 direction = this.cameraTargetTransform.position - this.tr.position;
			RaycastHit raycastHit;
			if (this.castType == CameraDistanceRaycaster.CastType.Raycast)
			{
				if (Physics.Raycast(new Ray(this.tr.position, direction), out raycastHit, direction.magnitude + this.minimumDistanceFromObstacles, this.layerMask, QueryTriggerInteraction.Ignore))
				{
					if (raycastHit.distance - this.minimumDistanceFromObstacles < 0f)
					{
						return raycastHit.distance;
					}
					return raycastHit.distance - this.minimumDistanceFromObstacles;
				}
			}
			else if (Physics.SphereCast(new Ray(this.tr.position, direction), this.spherecastRadius, out raycastHit, direction.magnitude, this.layerMask, QueryTriggerInteraction.Ignore))
			{
				return raycastHit.distance;
			}
			return direction.magnitude;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x000B813B File Offset: 0x000B633B
		public CameraDistanceRaycaster()
		{
		}

		// Token: 0x04001F0B RID: 7947
		public Transform cameraTransform;

		// Token: 0x04001F0C RID: 7948
		public Transform cameraTargetTransform;

		// Token: 0x04001F0D RID: 7949
		private Transform tr;

		// Token: 0x04001F0E RID: 7950
		public CameraDistanceRaycaster.CastType castType;

		// Token: 0x04001F0F RID: 7951
		public LayerMask layerMask = -1;

		// Token: 0x04001F10 RID: 7952
		private int ignoreRaycastLayer;

		// Token: 0x04001F11 RID: 7953
		public Collider[] ignoreList;

		// Token: 0x04001F12 RID: 7954
		private int[] ignoreListLayers;

		// Token: 0x04001F13 RID: 7955
		private float currentDistance;

		// Token: 0x04001F14 RID: 7956
		public float minimumDistanceFromObstacles = 0.1f;

		// Token: 0x04001F15 RID: 7957
		public float smoothingFactor = 25f;

		// Token: 0x04001F16 RID: 7958
		public float spherecastRadius = 0.2f;

		// Token: 0x02000692 RID: 1682
		public enum CastType
		{
			// Token: 0x04002C24 RID: 11300
			Raycast,
			// Token: 0x04002C25 RID: 11301
			Spherecast
		}
	}
}
