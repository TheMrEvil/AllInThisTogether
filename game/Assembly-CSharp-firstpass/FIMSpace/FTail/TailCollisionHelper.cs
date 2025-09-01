using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FIMSpace.FTail
{
	// Token: 0x02000068 RID: 104
	[AddComponentMenu("FImpossible Creations/Hidden/Tail Collision Helper")]
	public class TailCollisionHelper : MonoBehaviour
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00020418 File Offset: 0x0001E618
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00020420 File Offset: 0x0001E620
		internal Rigidbody RigBody
		{
			[CompilerGenerated]
			get
			{
				return this.<RigBody>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RigBody>k__BackingField = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00020429 File Offset: 0x0001E629
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00020431 File Offset: 0x0001E631
		internal Rigidbody2D RigBody2D
		{
			[CompilerGenerated]
			get
			{
				return this.<RigBody2D>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RigBody2D>k__BackingField = value;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0002043C File Offset: 0x0001E63C
		internal TailCollisionHelper Init(bool addRigidbody = true, float mass = 1f, bool kinematic = false)
		{
			if (this.TailCollider2D == null)
			{
				if (addRigidbody)
				{
					Rigidbody rigidbody = base.GetComponent<Rigidbody>();
					if (!rigidbody)
					{
						rigidbody = base.gameObject.AddComponent<Rigidbody>();
					}
					rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
					rigidbody.useGravity = false;
					rigidbody.isKinematic = kinematic;
					rigidbody.constraints = RigidbodyConstraints.FreezeAll;
					rigidbody.mass = mass;
					this.RigBody = rigidbody;
				}
				else
				{
					this.RigBody = base.GetComponent<Rigidbody>();
					if (this.RigBody)
					{
						this.RigBody.mass = mass;
					}
				}
			}
			else if (addRigidbody)
			{
				Rigidbody2D rigidbody2D = base.GetComponent<Rigidbody2D>();
				if (!rigidbody2D)
				{
					rigidbody2D = base.gameObject.AddComponent<Rigidbody2D>();
				}
				rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
				rigidbody2D.gravityScale = 0f;
				rigidbody2D.isKinematic = kinematic;
				rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
				rigidbody2D.mass = mass;
				this.RigBody2D = rigidbody2D;
			}
			else
			{
				this.RigBody2D = base.GetComponent<Rigidbody2D>();
				if (this.RigBody2D)
				{
					this.RigBody2D.mass = mass;
				}
			}
			return this;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00020544 File Offset: 0x0001E744
		private void OnCollisionEnter(Collision collision)
		{
			if (this.ParentTail == null)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
			TailCollisionHelper component = collision.transform.GetComponent<TailCollisionHelper>();
			if (component)
			{
				if (!this.ParentTail.CollideWithOtherTails)
				{
					return;
				}
				if (component.ParentTail == this.ParentTail)
				{
					return;
				}
			}
			if (this.ParentTail._TransformsGhostChain.Contains(collision.transform))
			{
				return;
			}
			if (this.ParentTail.IgnoredColliders.Contains(collision.collider))
			{
				return;
			}
			this.ParentTail.CollisionDetection(this.Index, collision);
			this.previousCollision = collision.transform;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000205EC File Offset: 0x0001E7EC
		private void OnCollisionExit(Collision collision)
		{
			if (collision.transform == this.previousCollision)
			{
				this.ParentTail.ExitCollision(this.Index);
				this.previousCollision = null;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0002061C File Offset: 0x0001E81C
		private void OnTriggerEnter(Collider other)
		{
			if (other.isTrigger)
			{
				return;
			}
			if (this.ParentTail.IgnoreMeshColliders && other is MeshCollider)
			{
				return;
			}
			if (other is CharacterController)
			{
				return;
			}
			if (this.ParentTail._TransformsGhostChain.Contains(other.transform))
			{
				return;
			}
			if (this.ParentTail.IgnoredColliders.Contains(other))
			{
				return;
			}
			if (!this.ParentTail.CollideWithOtherTails && other.transform.GetComponent<TailCollisionHelper>())
			{
				return;
			}
			this.ParentTail.AddCollider(other);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000206AA File Offset: 0x0001E8AA
		private void OnTriggerExit(Collider other)
		{
			if (this.ParentTail.IncludedColliders.Contains(other) && !this.ParentTail.DynamicAlwaysInclude.Contains(other))
			{
				this.ParentTail.IncludedColliders.Remove(other);
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000206E4 File Offset: 0x0001E8E4
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.isTrigger)
			{
				return;
			}
			if (other is CompositeCollider2D)
			{
				return;
			}
			if (other is TilemapCollider2D)
			{
				return;
			}
			if (other is EdgeCollider2D)
			{
				return;
			}
			if (this.ParentTail._TransformsGhostChain.Contains(other.transform))
			{
				return;
			}
			if (this.ParentTail.IgnoredColliders2D.Contains(other))
			{
				return;
			}
			if (!this.ParentTail.CollideWithOtherTails && other.transform.GetComponent<TailCollisionHelper>())
			{
				return;
			}
			this.ParentTail.AddCollider(other);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0002076E File Offset: 0x0001E96E
		private void OnTriggerExit2D(Collider2D other)
		{
			if (this.ParentTail.IncludedColliders2D.Contains(other) && !this.ParentTail.DynamicAlwaysInclude.Contains(other))
			{
				this.ParentTail.IncludedColliders2D.Remove(other);
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000207A8 File Offset: 0x0001E9A8
		public TailCollisionHelper()
		{
		}

		// Token: 0x0400040F RID: 1039
		public TailAnimator2 ParentTail;

		// Token: 0x04000410 RID: 1040
		public Collider TailCollider;

		// Token: 0x04000411 RID: 1041
		public Collider2D TailCollider2D;

		// Token: 0x04000412 RID: 1042
		public int Index;

		// Token: 0x04000413 RID: 1043
		[CompilerGenerated]
		private Rigidbody <RigBody>k__BackingField;

		// Token: 0x04000414 RID: 1044
		[CompilerGenerated]
		private Rigidbody2D <RigBody2D>k__BackingField;

		// Token: 0x04000415 RID: 1045
		private Transform previousCollision;
	}
}
