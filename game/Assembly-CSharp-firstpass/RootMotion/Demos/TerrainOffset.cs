using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000128 RID: 296
	public class TerrainOffset : MonoBehaviour
	{
		// Token: 0x06000C9C RID: 3228 RVA: 0x0005522C File Offset: 0x0005342C
		private void LateUpdate()
		{
			Vector3 vector = base.transform.rotation * this.raycastOffset;
			Vector3 groundHeightOffset = this.GetGroundHeightOffset(base.transform.position + vector);
			this.offset = Vector3.Lerp(this.offset, groundHeightOffset, Time.deltaTime * this.lerpSpeed);
			Vector3 vector2 = base.transform.position + new Vector3(vector.x, 0f, vector.z);
			this.aimIK.solver.transform.LookAt(vector2);
			this.aimIK.solver.IKPosition = vector2 + this.offset;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000552E0 File Offset: 0x000534E0
		private Vector3 GetGroundHeightOffset(Vector3 worldPosition)
		{
			Debug.DrawRay(worldPosition, Vector3.down * this.raycastOffset.y * 2f, Color.green);
			if (Physics.Raycast(worldPosition, Vector3.down, out this.hit, this.raycastOffset.y * 2f))
			{
				return Mathf.Clamp(this.hit.point.y - base.transform.position.y, this.min, this.max) * Vector3.up;
			}
			return Vector3.zero;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00055380 File Offset: 0x00053580
		public TerrainOffset()
		{
		}

		// Token: 0x040009E7 RID: 2535
		public AimIK aimIK;

		// Token: 0x040009E8 RID: 2536
		public Vector3 raycastOffset = new Vector3(0f, 2f, 1.5f);

		// Token: 0x040009E9 RID: 2537
		public LayerMask raycastLayers;

		// Token: 0x040009EA RID: 2538
		public float min = -2f;

		// Token: 0x040009EB RID: 2539
		public float max = 2f;

		// Token: 0x040009EC RID: 2540
		public float lerpSpeed = 10f;

		// Token: 0x040009ED RID: 2541
		private RaycastHit hit;

		// Token: 0x040009EE RID: 2542
		private Vector3 offset;
	}
}
