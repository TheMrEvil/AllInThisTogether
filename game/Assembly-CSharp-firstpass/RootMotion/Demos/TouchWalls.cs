using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200014C RID: 332
	public class TouchWalls : MonoBehaviour
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x00059740 File Offset: 0x00057940
		private void Start()
		{
			TouchWalls.EffectorLink[] array = this.effectorLinks;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Initiate(this.interactionSystem);
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00059770 File Offset: 0x00057970
		private void FixedUpdate()
		{
			for (int i = 0; i < this.effectorLinks.Length; i++)
			{
				this.effectorLinks[i].Update(this.interactionSystem);
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000597A4 File Offset: 0x000579A4
		private void OnDestroy()
		{
			if (this.interactionSystem != null)
			{
				for (int i = 0; i < this.effectorLinks.Length; i++)
				{
					this.effectorLinks[i].Destroy(this.interactionSystem);
				}
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000597E5 File Offset: 0x000579E5
		public TouchWalls()
		{
		}

		// Token: 0x04000AE1 RID: 2785
		public InteractionSystem interactionSystem;

		// Token: 0x04000AE2 RID: 2786
		public TouchWalls.EffectorLink[] effectorLinks;

		// Token: 0x02000236 RID: 566
		[Serializable]
		public class EffectorLink
		{
			// Token: 0x060011BB RID: 4539 RVA: 0x0006DF34 File Offset: 0x0006C134
			public void Initiate(InteractionSystem interactionSystem)
			{
				this.raycastDirectionLocal = this.spherecastFrom.InverseTransformDirection(this.interactionObject.transform.position - this.spherecastFrom.position);
				this.raycastDistance = Vector3.Distance(this.spherecastFrom.position, this.interactionObject.transform.position);
				interactionSystem.OnInteractionStart = (InteractionSystem.InteractionDelegate)Delegate.Combine(interactionSystem.OnInteractionStart, new InteractionSystem.InteractionDelegate(this.OnInteractionStart));
				interactionSystem.OnInteractionResume = (InteractionSystem.InteractionDelegate)Delegate.Combine(interactionSystem.OnInteractionResume, new InteractionSystem.InteractionDelegate(this.OnInteractionResume));
				interactionSystem.OnInteractionStop = (InteractionSystem.InteractionDelegate)Delegate.Combine(interactionSystem.OnInteractionStop, new InteractionSystem.InteractionDelegate(this.OnInteractionStop));
				this.hit.normal = Vector3.forward;
				this.targetPosition = this.interactionObject.transform.position;
				this.targetRotation = this.interactionObject.transform.rotation;
				this.initiated = true;
			}

			// Token: 0x060011BC RID: 4540 RVA: 0x0006E044 File Offset: 0x0006C244
			private bool FindWalls(Vector3 direction)
			{
				if (!this.enabled)
				{
					return false;
				}
				bool result = Physics.SphereCast(this.spherecastFrom.position, this.spherecastRadius, direction, out this.hit, this.raycastDistance * this.distanceMlp, this.touchLayers);
				if (this.hit.distance < this.minDistance)
				{
					result = false;
				}
				return result;
			}

			// Token: 0x060011BD RID: 4541 RVA: 0x0006E0A8 File Offset: 0x0006C2A8
			public void Update(InteractionSystem interactionSystem)
			{
				if (!this.initiated)
				{
					return;
				}
				Vector3 vector = this.spherecastFrom.TransformDirection(this.raycastDirectionLocal);
				this.hit.point = this.spherecastFrom.position + vector;
				bool flag = this.FindWalls(vector);
				if (!this.inTouch)
				{
					if (flag && Time.time > this.nextSwitchTime)
					{
						this.interactionObject.transform.parent = null;
						interactionSystem.StartInteraction(this.effectorType, this.interactionObject, true);
						this.nextSwitchTime = Time.time + this.minSwitchTime / interactionSystem.speed;
						this.targetPosition = this.hit.point;
						this.targetRotation = Quaternion.LookRotation(-this.hit.normal);
						this.interactionObject.transform.position = this.targetPosition;
						this.interactionObject.transform.rotation = this.targetRotation;
					}
				}
				else
				{
					if (!flag)
					{
						this.StopTouch(interactionSystem);
					}
					else if (!interactionSystem.IsPaused(this.effectorType) || this.sliding)
					{
						this.targetPosition = this.hit.point;
						this.targetRotation = Quaternion.LookRotation(-this.hit.normal);
					}
					if (Vector3.Distance(this.interactionObject.transform.position, this.hit.point) > this.releaseDistance)
					{
						if (flag)
						{
							this.targetPosition = this.hit.point;
							this.targetRotation = Quaternion.LookRotation(-this.hit.normal);
						}
						else
						{
							this.StopTouch(interactionSystem);
						}
					}
				}
				float b = (!this.inTouch || (interactionSystem.IsPaused(this.effectorType) && this.interactionObject.transform.position == this.targetPosition)) ? 0f : 1f;
				this.speedF = Mathf.Lerp(this.speedF, b, Time.deltaTime * 3f * interactionSystem.speed);
				float t = Time.deltaTime * this.lerpSpeed * this.speedF * interactionSystem.speed;
				this.interactionObject.transform.position = Vector3.Lerp(this.interactionObject.transform.position, this.targetPosition, t);
				this.interactionObject.transform.rotation = Quaternion.Slerp(this.interactionObject.transform.rotation, this.targetRotation, t);
			}

			// Token: 0x060011BE RID: 4542 RVA: 0x0006E338 File Offset: 0x0006C538
			private void StopTouch(InteractionSystem interactionSystem)
			{
				this.interactionObject.transform.parent = interactionSystem.transform;
				this.nextSwitchTime = Time.time + this.minSwitchTime / interactionSystem.speed;
				if (interactionSystem.IsPaused(this.effectorType))
				{
					interactionSystem.ResumeInteraction(this.effectorType);
					return;
				}
				this.speedF = 0f;
				this.targetPosition = this.hit.point;
				this.targetRotation = Quaternion.LookRotation(-this.hit.normal);
			}

			// Token: 0x060011BF RID: 4543 RVA: 0x0006E3C7 File Offset: 0x0006C5C7
			private void OnInteractionStart(FullBodyBipedEffector effectorType, InteractionObject interactionObject)
			{
				if (effectorType != this.effectorType || interactionObject != this.interactionObject)
				{
					return;
				}
				this.inTouch = true;
			}

			// Token: 0x060011C0 RID: 4544 RVA: 0x0006E3E8 File Offset: 0x0006C5E8
			private void OnInteractionResume(FullBodyBipedEffector effectorType, InteractionObject interactionObject)
			{
				if (effectorType != this.effectorType || interactionObject != this.interactionObject)
				{
					return;
				}
				this.inTouch = false;
			}

			// Token: 0x060011C1 RID: 4545 RVA: 0x0006E409 File Offset: 0x0006C609
			private void OnInteractionStop(FullBodyBipedEffector effectorType, InteractionObject interactionObject)
			{
				if (effectorType != this.effectorType || interactionObject != this.interactionObject)
				{
					return;
				}
				this.inTouch = false;
			}

			// Token: 0x060011C2 RID: 4546 RVA: 0x0006E42C File Offset: 0x0006C62C
			public void Destroy(InteractionSystem interactionSystem)
			{
				if (!this.initiated)
				{
					return;
				}
				interactionSystem.OnInteractionStart = (InteractionSystem.InteractionDelegate)Delegate.Remove(interactionSystem.OnInteractionStart, new InteractionSystem.InteractionDelegate(this.OnInteractionStart));
				interactionSystem.OnInteractionResume = (InteractionSystem.InteractionDelegate)Delegate.Remove(interactionSystem.OnInteractionResume, new InteractionSystem.InteractionDelegate(this.OnInteractionResume));
				interactionSystem.OnInteractionStop = (InteractionSystem.InteractionDelegate)Delegate.Remove(interactionSystem.OnInteractionStop, new InteractionSystem.InteractionDelegate(this.OnInteractionStop));
			}

			// Token: 0x060011C3 RID: 4547 RVA: 0x0006E4A8 File Offset: 0x0006C6A8
			public EffectorLink()
			{
			}

			// Token: 0x040010A2 RID: 4258
			public bool enabled = true;

			// Token: 0x040010A3 RID: 4259
			public FullBodyBipedEffector effectorType;

			// Token: 0x040010A4 RID: 4260
			public InteractionObject interactionObject;

			// Token: 0x040010A5 RID: 4261
			public Transform spherecastFrom;

			// Token: 0x040010A6 RID: 4262
			public float spherecastRadius = 0.1f;

			// Token: 0x040010A7 RID: 4263
			public float minDistance = 0.3f;

			// Token: 0x040010A8 RID: 4264
			public float distanceMlp = 1f;

			// Token: 0x040010A9 RID: 4265
			public LayerMask touchLayers;

			// Token: 0x040010AA RID: 4266
			public float lerpSpeed = 10f;

			// Token: 0x040010AB RID: 4267
			public float minSwitchTime = 0.2f;

			// Token: 0x040010AC RID: 4268
			public float releaseDistance = 0.4f;

			// Token: 0x040010AD RID: 4269
			public bool sliding;

			// Token: 0x040010AE RID: 4270
			private Vector3 raycastDirectionLocal;

			// Token: 0x040010AF RID: 4271
			private float raycastDistance;

			// Token: 0x040010B0 RID: 4272
			private bool inTouch;

			// Token: 0x040010B1 RID: 4273
			private RaycastHit hit;

			// Token: 0x040010B2 RID: 4274
			private Vector3 targetPosition;

			// Token: 0x040010B3 RID: 4275
			private Quaternion targetRotation;

			// Token: 0x040010B4 RID: 4276
			private bool initiated;

			// Token: 0x040010B5 RID: 4277
			private float nextSwitchTime;

			// Token: 0x040010B6 RID: 4278
			private float speedF;
		}
	}
}
