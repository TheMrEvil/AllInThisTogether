using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200012A RID: 298
	public class MechSpider : MonoBehaviour
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000556D8 File Offset: 0x000538D8
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x000556E0 File Offset: 0x000538E0
		public Vector3 velocity
		{
			[CompilerGenerated]
			get
			{
				return this.<velocity>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<velocity>k__BackingField = value;
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000556E9 File Offset: 0x000538E9
		private void Start()
		{
			this.lastPosition = base.transform.position;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000556FC File Offset: 0x000538FC
		private void Update()
		{
			this.velocity = (base.transform.position - this.lastPosition) / Time.deltaTime;
			this.lastPosition = base.transform.position;
			Vector3 legsPlaneNormal = this.GetLegsPlaneNormal();
			Quaternion lhs = Quaternion.FromToRotation(base.transform.up, legsPlaneNormal);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, lhs * base.transform.rotation, Time.deltaTime * this.rootRotationSpeed);
			Vector3 a = Vector3.Project(this.GetLegCentroid() + base.transform.up * this.height * this.scale - base.transform.position, base.transform.up);
			base.transform.position += a * Time.deltaTime * (this.rootPositionSpeed * this.scale);
			if (Physics.Raycast(base.transform.position + base.transform.up * this.raycastHeight * this.scale, -base.transform.up, out this.rootHit, this.raycastHeight * this.scale + this.raycastDistance * this.scale, this.raycastLayers))
			{
				this.rootHit.distance = this.rootHit.distance - (this.raycastHeight * this.scale + this.minHeight * this.scale);
				if (this.rootHit.distance < 0f)
				{
					Vector3 b = base.transform.position - base.transform.up * this.rootHit.distance;
					base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * this.rootPositionSpeed * this.scale);
				}
			}
			this.sine += Time.deltaTime * this.breatheSpeed;
			if (this.sine >= 6.2831855f)
			{
				this.sine -= 6.2831855f;
			}
			float d = Mathf.Sin(this.sine) * this.breatheMagnitude * this.scale;
			Vector3 b2 = base.transform.up * d;
			this.body.transform.position = base.transform.position + b2;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000559AC File Offset: 0x00053BAC
		private Vector3 GetLegCentroid()
		{
			Vector3 vector = Vector3.zero;
			float d = 1f / (float)this.legs.Length;
			for (int i = 0; i < this.legs.Length; i++)
			{
				vector += this.legs[i].position * d;
			}
			return vector;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00055A00 File Offset: 0x00053C00
		private Vector3 GetLegsPlaneNormal()
		{
			Vector3 vector = base.transform.up;
			if (this.legRotationWeight <= 0f)
			{
				return vector;
			}
			float t = 1f / Mathf.Lerp((float)this.legs.Length, 1f, this.legRotationWeight);
			for (int i = 0; i < this.legs.Length; i++)
			{
				Vector3 vector2 = this.legs[i].position - (base.transform.position - base.transform.up * this.height * this.scale);
				Vector3 up = base.transform.up;
				Vector3 fromDirection = vector2;
				Vector3.OrthoNormalize(ref up, ref fromDirection);
				Quaternion quaternion = Quaternion.FromToRotation(fromDirection, vector2);
				quaternion = Quaternion.Lerp(Quaternion.identity, quaternion, t);
				vector = quaternion * vector;
			}
			return vector;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00055AE4 File Offset: 0x00053CE4
		public MechSpider()
		{
		}

		// Token: 0x04000A02 RID: 2562
		public LayerMask raycastLayers;

		// Token: 0x04000A03 RID: 2563
		public float scale = 1f;

		// Token: 0x04000A04 RID: 2564
		public Transform body;

		// Token: 0x04000A05 RID: 2565
		public MechSpiderLeg[] legs;

		// Token: 0x04000A06 RID: 2566
		public float legRotationWeight = 1f;

		// Token: 0x04000A07 RID: 2567
		public float rootPositionSpeed = 5f;

		// Token: 0x04000A08 RID: 2568
		public float rootRotationSpeed = 30f;

		// Token: 0x04000A09 RID: 2569
		public float breatheSpeed = 2f;

		// Token: 0x04000A0A RID: 2570
		public float breatheMagnitude = 0.2f;

		// Token: 0x04000A0B RID: 2571
		public float height = 3.5f;

		// Token: 0x04000A0C RID: 2572
		public float minHeight = 2f;

		// Token: 0x04000A0D RID: 2573
		public float raycastHeight = 10f;

		// Token: 0x04000A0E RID: 2574
		public float raycastDistance = 5f;

		// Token: 0x04000A0F RID: 2575
		[CompilerGenerated]
		private Vector3 <velocity>k__BackingField;

		// Token: 0x04000A10 RID: 2576
		private Vector3 lastPosition;

		// Token: 0x04000A11 RID: 2577
		private Vector3 defaultBodyLocalPosition;

		// Token: 0x04000A12 RID: 2578
		private float sine;

		// Token: 0x04000A13 RID: 2579
		private RaycastHit rootHit;
	}
}
