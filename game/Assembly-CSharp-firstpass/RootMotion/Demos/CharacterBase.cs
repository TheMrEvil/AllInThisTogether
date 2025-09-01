using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200015C RID: 348
	public abstract class CharacterBase : MonoBehaviour
	{
		// Token: 0x06000D6E RID: 3438
		public abstract void Move(Vector3 deltaPosition, Quaternion deltaRotation);

		// Token: 0x06000D6F RID: 3439 RVA: 0x0005AAE4 File Offset: 0x00058CE4
		protected Vector3 GetGravity()
		{
			if (this.gravityTarget != null)
			{
				return (this.gravityTarget.position - base.transform.position).normalized * Physics.gravity.magnitude;
			}
			return Physics.gravity;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0005AB3C File Offset: 0x00058D3C
		protected virtual void Start()
		{
			this.capsule = (base.GetComponent<Collider>() as CapsuleCollider);
			this.r = base.GetComponent<Rigidbody>();
			this.originalHeight = this.capsule.height;
			this.originalCenter = this.capsule.center;
			this.zeroFrictionMaterial = new PhysicMaterial();
			this.zeroFrictionMaterial.dynamicFriction = 0f;
			this.zeroFrictionMaterial.staticFriction = 0f;
			this.zeroFrictionMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
			this.zeroFrictionMaterial.bounciness = 0f;
			this.zeroFrictionMaterial.bounceCombine = PhysicMaterialCombine.Minimum;
			this.highFrictionMaterial = new PhysicMaterial();
			this.r.constraints = RigidbodyConstraints.FreezeRotation;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected virtual RaycastHit GetSpherecastHit()
		{
			Vector3 up = base.transform.up;
			Ray ray = new Ray(this.r.position + up * this.airborneThreshold, -up);
			RaycastHit result = default(RaycastHit);
			result.point = base.transform.position - base.transform.transform.up * this.airborneThreshold;
			result.normal = base.transform.up;
			Physics.SphereCast(ray, this.spherecastRadius, out result, this.airborneThreshold * 2f, this.groundLayers);
			return result;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0005ACA8 File Offset: 0x00058EA8
		public float GetAngleFromForward(Vector3 worldDirection)
		{
			Vector3 vector = base.transform.InverseTransformDirection(worldDirection);
			return Mathf.Atan2(vector.x, vector.z) * 57.29578f;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0005ACDC File Offset: 0x00058EDC
		protected void RigidbodyRotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
			Vector3 point2 = base.transform.position - point;
			this.r.MovePosition(point + quaternion * point2);
			this.r.MoveRotation(quaternion * base.transform.rotation);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0005AD38 File Offset: 0x00058F38
		protected void ScaleCapsule(float mlp)
		{
			if (this.capsule.height != this.originalHeight * mlp)
			{
				this.capsule.height = Mathf.MoveTowards(this.capsule.height, this.originalHeight * mlp, Time.deltaTime * 4f);
				this.capsule.center = Vector3.MoveTowards(this.capsule.center, this.originalCenter * mlp, Time.deltaTime * 2f);
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0005ADBA File Offset: 0x00058FBA
		protected void HighFriction()
		{
			this.capsule.material = this.highFrictionMaterial;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0005ADCD File Offset: 0x00058FCD
		protected void ZeroFriction()
		{
			this.capsule.material = this.zeroFrictionMaterial;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0005ADE0 File Offset: 0x00058FE0
		protected float GetSlopeDamper(Vector3 velocity, Vector3 groundNormal)
		{
			float num = 90f - Vector3.Angle(velocity, groundNormal);
			num -= this.slopeStartAngle;
			float num2 = this.slopeEndAngle - this.slopeStartAngle;
			return 1f - Mathf.Clamp(num / num2, 0f, 1f);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0005AE2A File Offset: 0x0005902A
		protected CharacterBase()
		{
		}

		// Token: 0x04000B36 RID: 2870
		[Header("Base Parameters")]
		[Tooltip("If specified, will use the direction from the character to this Transform as the gravity vector instead of Physics.gravity. Physics.gravity.magnitude will be used as the magnitude of the gravity vector.")]
		public Transform gravityTarget;

		// Token: 0x04000B37 RID: 2871
		[Tooltip("Multiplies gravity applied to the character even if 'Individual Gravity' is unchecked.")]
		public float gravityMultiplier = 2f;

		// Token: 0x04000B38 RID: 2872
		public float airborneThreshold = 0.6f;

		// Token: 0x04000B39 RID: 2873
		public float slopeStartAngle = 50f;

		// Token: 0x04000B3A RID: 2874
		public float slopeEndAngle = 85f;

		// Token: 0x04000B3B RID: 2875
		public float spherecastRadius = 0.1f;

		// Token: 0x04000B3C RID: 2876
		public LayerMask groundLayers;

		// Token: 0x04000B3D RID: 2877
		private PhysicMaterial zeroFrictionMaterial;

		// Token: 0x04000B3E RID: 2878
		private PhysicMaterial highFrictionMaterial;

		// Token: 0x04000B3F RID: 2879
		protected Rigidbody r;

		// Token: 0x04000B40 RID: 2880
		protected const float half = 0.5f;

		// Token: 0x04000B41 RID: 2881
		protected float originalHeight;

		// Token: 0x04000B42 RID: 2882
		protected Vector3 originalCenter;

		// Token: 0x04000B43 RID: 2883
		protected CapsuleCollider capsule;
	}
}
