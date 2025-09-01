using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000145 RID: 325
	public abstract class PickUp2Handed : MonoBehaviour
	{
		// Token: 0x06000D15 RID: 3349 RVA: 0x00058FE8 File Offset: 0x000571E8
		private void OnGUI()
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space((float)this.GUIspace);
			if (!this.holding)
			{
				if (GUILayout.Button("Pick Up " + this.obj.name, Array.Empty<GUILayoutOption>()))
				{
					this.interactionSystem.StartInteraction(FullBodyBipedEffector.LeftHand, this.obj, false);
					this.interactionSystem.StartInteraction(FullBodyBipedEffector.RightHand, this.obj, false);
				}
			}
			else
			{
				GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
				if (this.holdingRight && GUILayout.Button("Release Right", Array.Empty<GUILayoutOption>()))
				{
					this.interactionSystem.ResumeInteraction(FullBodyBipedEffector.RightHand);
				}
				if (this.holdingLeft && GUILayout.Button("Release Left", Array.Empty<GUILayoutOption>()))
				{
					this.interactionSystem.ResumeInteraction(FullBodyBipedEffector.LeftHand);
				}
				if (GUILayout.Button("Drop " + this.obj.name, Array.Empty<GUILayoutOption>()))
				{
					this.interactionSystem.ResumeAll();
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndHorizontal();
		}

		// Token: 0x06000D16 RID: 3350
		protected abstract void RotatePivot();

		// Token: 0x06000D17 RID: 3351 RVA: 0x000590F0 File Offset: 0x000572F0
		private void Start()
		{
			InteractionSystem interactionSystem = this.interactionSystem;
			interactionSystem.OnInteractionStart = (InteractionSystem.InteractionDelegate)Delegate.Combine(interactionSystem.OnInteractionStart, new InteractionSystem.InteractionDelegate(this.OnStart));
			InteractionSystem interactionSystem2 = this.interactionSystem;
			interactionSystem2.OnInteractionPause = (InteractionSystem.InteractionDelegate)Delegate.Combine(interactionSystem2.OnInteractionPause, new InteractionSystem.InteractionDelegate(this.OnPause));
			InteractionSystem interactionSystem3 = this.interactionSystem;
			interactionSystem3.OnInteractionResume = (InteractionSystem.InteractionDelegate)Delegate.Combine(interactionSystem3.OnInteractionResume, new InteractionSystem.InteractionDelegate(this.OnDrop));
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00059174 File Offset: 0x00057374
		private void OnPause(FullBodyBipedEffector effectorType, InteractionObject interactionObject)
		{
			if (effectorType != FullBodyBipedEffector.LeftHand)
			{
				return;
			}
			if (interactionObject != this.obj)
			{
				return;
			}
			this.obj.transform.parent = this.interactionSystem.transform;
			Rigidbody component = this.obj.GetComponent<Rigidbody>();
			if (component != null)
			{
				component.isKinematic = true;
			}
			this.pickUpPosition = this.obj.transform.position;
			this.pickUpRotation = this.obj.transform.rotation;
			this.holdWeight = 0f;
			this.holdWeightVel = 0f;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0005920E File Offset: 0x0005740E
		private void OnStart(FullBodyBipedEffector effectorType, InteractionObject interactionObject)
		{
			if (effectorType != FullBodyBipedEffector.LeftHand)
			{
				return;
			}
			if (interactionObject != this.obj)
			{
				return;
			}
			this.RotatePivot();
			this.holdPoint.rotation = this.obj.transform.rotation;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00059248 File Offset: 0x00057448
		private void OnDrop(FullBodyBipedEffector effectorType, InteractionObject interactionObject)
		{
			if (this.holding)
			{
				return;
			}
			if (interactionObject != this.obj)
			{
				return;
			}
			this.obj.transform.parent = null;
			if (this.obj.GetComponent<Rigidbody>() != null)
			{
				this.obj.GetComponent<Rigidbody>().isKinematic = false;
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000592A4 File Offset: 0x000574A4
		private void LateUpdate()
		{
			if (this.holding)
			{
				this.holdWeight = Mathf.SmoothDamp(this.holdWeight, 1f, ref this.holdWeightVel, this.pickUpTime);
				this.obj.transform.position = Vector3.Lerp(this.pickUpPosition, this.holdPoint.position, this.holdWeight);
				this.obj.transform.rotation = Quaternion.Lerp(this.pickUpRotation, this.holdPoint.rotation, this.holdWeight);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00059333 File Offset: 0x00057533
		private bool holding
		{
			get
			{
				return this.holdingLeft || this.holdingRight;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00059345 File Offset: 0x00057545
		private bool holdingLeft
		{
			get
			{
				return this.interactionSystem.IsPaused(FullBodyBipedEffector.LeftHand) && this.interactionSystem.GetInteractionObject(FullBodyBipedEffector.LeftHand) == this.obj;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0005936E File Offset: 0x0005756E
		private bool holdingRight
		{
			get
			{
				return this.interactionSystem.IsPaused(FullBodyBipedEffector.RightHand) && this.interactionSystem.GetInteractionObject(FullBodyBipedEffector.RightHand) == this.obj;
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00059398 File Offset: 0x00057598
		private void OnDestroy()
		{
			if (this.interactionSystem == null)
			{
				return;
			}
			InteractionSystem interactionSystem = this.interactionSystem;
			interactionSystem.OnInteractionStart = (InteractionSystem.InteractionDelegate)Delegate.Remove(interactionSystem.OnInteractionStart, new InteractionSystem.InteractionDelegate(this.OnStart));
			InteractionSystem interactionSystem2 = this.interactionSystem;
			interactionSystem2.OnInteractionPause = (InteractionSystem.InteractionDelegate)Delegate.Remove(interactionSystem2.OnInteractionPause, new InteractionSystem.InteractionDelegate(this.OnPause));
			InteractionSystem interactionSystem3 = this.interactionSystem;
			interactionSystem3.OnInteractionResume = (InteractionSystem.InteractionDelegate)Delegate.Remove(interactionSystem3.OnInteractionResume, new InteractionSystem.InteractionDelegate(this.OnDrop));
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00059429 File Offset: 0x00057629
		protected PickUp2Handed()
		{
		}

		// Token: 0x04000ACA RID: 2762
		public int GUIspace;

		// Token: 0x04000ACB RID: 2763
		public InteractionSystem interactionSystem;

		// Token: 0x04000ACC RID: 2764
		public InteractionObject obj;

		// Token: 0x04000ACD RID: 2765
		public Transform pivot;

		// Token: 0x04000ACE RID: 2766
		public Transform holdPoint;

		// Token: 0x04000ACF RID: 2767
		public float pickUpTime = 0.3f;

		// Token: 0x04000AD0 RID: 2768
		private float holdWeight;

		// Token: 0x04000AD1 RID: 2769
		private float holdWeightVel;

		// Token: 0x04000AD2 RID: 2770
		private Vector3 pickUpPosition;

		// Token: 0x04000AD3 RID: 2771
		private Quaternion pickUpRotation;
	}
}
