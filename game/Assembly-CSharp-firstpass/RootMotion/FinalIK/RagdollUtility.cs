using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000109 RID: 265
	public class RagdollUtility : MonoBehaviour
	{
		// Token: 0x06000BD0 RID: 3024 RVA: 0x0004FB86 File Offset: 0x0004DD86
		public void EnableRagdoll()
		{
			if (this.isRagdoll)
			{
				return;
			}
			base.StopAllCoroutines();
			this.enableRagdollFlag = true;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0004FB9E File Offset: 0x0004DD9E
		public void DisableRagdoll()
		{
			if (!this.isRagdoll)
			{
				return;
			}
			this.StoreLocalState();
			base.StopAllCoroutines();
			base.StartCoroutine(this.DisableRagdollSmooth());
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0004FBC4 File Offset: 0x0004DDC4
		public void Start()
		{
			this.animator = base.GetComponent<Animator>();
			this.allIKComponents = base.GetComponentsInChildren<IK>();
			this.disabledIKComponents = new bool[this.allIKComponents.Length];
			this.fixTransforms = new bool[this.allIKComponents.Length];
			if (this.ik != null)
			{
				IKSolver iksolver = this.ik.GetIKSolver();
				iksolver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(iksolver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterLastIK));
			}
			Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
			int num = (componentsInChildren[0].gameObject == base.gameObject) ? 1 : 0;
			this.rigidbones = new RagdollUtility.Rigidbone[(num == 0) ? componentsInChildren.Length : (componentsInChildren.Length - 1)];
			for (int i = 0; i < this.rigidbones.Length; i++)
			{
				this.rigidbones[i] = new RagdollUtility.Rigidbone(componentsInChildren[i + num]);
			}
			Transform[] componentsInChildren2 = base.GetComponentsInChildren<Transform>();
			this.children = new RagdollUtility.Child[componentsInChildren2.Length - 1];
			for (int j = 0; j < this.children.Length; j++)
			{
				this.children[j] = new RagdollUtility.Child(componentsInChildren2[j + 1]);
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0004FCE9 File Offset: 0x0004DEE9
		private IEnumerator DisableRagdollSmooth()
		{
			for (int i = 0; i < this.rigidbones.Length; i++)
			{
				this.rigidbones[i].r.isKinematic = true;
			}
			for (int j = 0; j < this.allIKComponents.Length; j++)
			{
				this.allIKComponents[j].fixTransforms = this.fixTransforms[j];
				if (this.disabledIKComponents[j])
				{
					this.allIKComponents[j].enabled = true;
				}
			}
			this.animator.updateMode = this.animatorUpdateMode;
			this.animator.enabled = true;
			while (this.ragdollWeight > 0f)
			{
				this.ragdollWeight = Mathf.SmoothDamp(this.ragdollWeight, 0f, ref this.ragdollWeightV, this.ragdollToAnimationTime);
				if (this.ragdollWeight < 0.001f)
				{
					this.ragdollWeight = 0f;
				}
				yield return null;
			}
			yield return null;
			yield break;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0004FCF8 File Offset: 0x0004DEF8
		private void Update()
		{
			if (!this.isRagdoll)
			{
				return;
			}
			if (!this.applyIkOnRagdoll)
			{
				bool flag = false;
				for (int i = 0; i < this.allIKComponents.Length; i++)
				{
					if (this.allIKComponents[i].enabled)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					for (int j = 0; j < this.allIKComponents.Length; j++)
					{
						this.disabledIKComponents[j] = false;
					}
				}
				for (int k = 0; k < this.allIKComponents.Length; k++)
				{
					if (this.allIKComponents[k].enabled)
					{
						this.allIKComponents[k].enabled = false;
						this.disabledIKComponents[k] = true;
					}
				}
				return;
			}
			bool flag2 = false;
			for (int l = 0; l < this.allIKComponents.Length; l++)
			{
				if (this.disabledIKComponents[l])
				{
					flag2 = true;
					break;
				}
			}
			if (flag2)
			{
				for (int m = 0; m < this.allIKComponents.Length; m++)
				{
					if (this.disabledIKComponents[m])
					{
						this.allIKComponents[m].enabled = true;
					}
				}
				for (int n = 0; n < this.allIKComponents.Length; n++)
				{
					this.disabledIKComponents[n] = false;
				}
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0004FE1A File Offset: 0x0004E01A
		private void FixedUpdate()
		{
			if (this.isRagdoll && this.applyIkOnRagdoll)
			{
				this.FixTransforms(1f);
			}
			this.fixedFrame = true;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0004FE40 File Offset: 0x0004E040
		private void LateUpdate()
		{
			if (this.animator.updateMode != AnimatorUpdateMode.AnimatePhysics || (this.animator.updateMode == AnimatorUpdateMode.AnimatePhysics && this.fixedFrame))
			{
				this.AfterAnimation();
			}
			this.fixedFrame = false;
			if (!this.ikUsed)
			{
				this.OnFinalPose();
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0004FE8C File Offset: 0x0004E08C
		private void AfterLastIK()
		{
			if (this.ikUsed)
			{
				this.OnFinalPose();
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0004FE9C File Offset: 0x0004E09C
		private void AfterAnimation()
		{
			if (this.isRagdoll)
			{
				this.StoreLocalState();
				return;
			}
			this.FixTransforms(this.ragdollWeight);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0004FEB9 File Offset: 0x0004E0B9
		private void OnFinalPose()
		{
			if (!this.isRagdoll)
			{
				this.RecordVelocities();
			}
			if (this.enableRagdollFlag)
			{
				this.RagdollEnabler();
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0004FED8 File Offset: 0x0004E0D8
		private void RagdollEnabler()
		{
			this.StoreLocalState();
			for (int i = 0; i < this.allIKComponents.Length; i++)
			{
				this.disabledIKComponents[i] = false;
			}
			if (!this.applyIkOnRagdoll)
			{
				for (int j = 0; j < this.allIKComponents.Length; j++)
				{
					if (this.allIKComponents[j].enabled)
					{
						this.allIKComponents[j].enabled = false;
						this.disabledIKComponents[j] = true;
					}
				}
			}
			this.animatorUpdateMode = this.animator.updateMode;
			this.animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
			this.animator.enabled = false;
			for (int k = 0; k < this.rigidbones.Length; k++)
			{
				this.rigidbones[k].WakeUp(this.applyVelocity, this.applyAngularVelocity);
			}
			for (int l = 0; l < this.fixTransforms.Length; l++)
			{
				this.fixTransforms[l] = this.allIKComponents[l].fixTransforms;
				this.allIKComponents[l].fixTransforms = false;
			}
			this.ragdollWeight = 1f;
			this.ragdollWeightV = 0f;
			this.enableRagdollFlag = false;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0004FFF0 File Offset: 0x0004E1F0
		private bool isRagdoll
		{
			get
			{
				return !this.rigidbones[0].r.isKinematic && !this.animator.enabled;
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00050018 File Offset: 0x0004E218
		private void RecordVelocities()
		{
			RagdollUtility.Rigidbone[] array = this.rigidbones;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].RecordVelocity();
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00050044 File Offset: 0x0004E244
		private bool ikUsed
		{
			get
			{
				if (this.ik == null)
				{
					return false;
				}
				if (this.ik.enabled && this.ik.GetIKSolver().IKPositionWeight > 0f)
				{
					return true;
				}
				foreach (IK ik in this.allIKComponents)
				{
					if (ik.enabled && ik.GetIKSolver().IKPositionWeight > 0f)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000500C0 File Offset: 0x0004E2C0
		private void StoreLocalState()
		{
			RagdollUtility.Child[] array = this.children;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].StoreLocalState();
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000500EC File Offset: 0x0004E2EC
		private void FixTransforms(float weight)
		{
			RagdollUtility.Child[] array = this.children;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FixTransform(weight);
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00050117 File Offset: 0x0004E317
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolver iksolver = this.ik.GetIKSolver();
				iksolver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(iksolver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterLastIK));
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00050154 File Offset: 0x0004E354
		public RagdollUtility()
		{
		}

		// Token: 0x04000936 RID: 2358
		[Tooltip("If you have multiple IK components, then this should be the one that solves last each frame.")]
		public IK ik;

		// Token: 0x04000937 RID: 2359
		[Tooltip("How long does it take to blend from ragdoll to animation?")]
		public float ragdollToAnimationTime = 0.2f;

		// Token: 0x04000938 RID: 2360
		[Tooltip("If true, IK can be used on top of physical ragdoll simulation.")]
		public bool applyIkOnRagdoll;

		// Token: 0x04000939 RID: 2361
		[Tooltip("How much velocity transfer from animation to ragdoll?")]
		public float applyVelocity = 1f;

		// Token: 0x0400093A RID: 2362
		[Tooltip("How much angular velocity to transfer from animation to ragdoll?")]
		public float applyAngularVelocity = 1f;

		// Token: 0x0400093B RID: 2363
		private Animator animator;

		// Token: 0x0400093C RID: 2364
		private RagdollUtility.Rigidbone[] rigidbones = new RagdollUtility.Rigidbone[0];

		// Token: 0x0400093D RID: 2365
		private RagdollUtility.Child[] children = new RagdollUtility.Child[0];

		// Token: 0x0400093E RID: 2366
		private bool enableRagdollFlag;

		// Token: 0x0400093F RID: 2367
		private AnimatorUpdateMode animatorUpdateMode;

		// Token: 0x04000940 RID: 2368
		private IK[] allIKComponents = new IK[0];

		// Token: 0x04000941 RID: 2369
		private bool[] fixTransforms = new bool[0];

		// Token: 0x04000942 RID: 2370
		private float ragdollWeight;

		// Token: 0x04000943 RID: 2371
		private float ragdollWeightV;

		// Token: 0x04000944 RID: 2372
		private bool fixedFrame;

		// Token: 0x04000945 RID: 2373
		private bool[] disabledIKComponents = new bool[0];

		// Token: 0x02000213 RID: 531
		public class Rigidbone
		{
			// Token: 0x0600112F RID: 4399 RVA: 0x0006BBA8 File Offset: 0x00069DA8
			public Rigidbone(Rigidbody r)
			{
				this.r = r;
				this.t = r.transform;
				this.joint = this.t.GetComponent<Joint>();
				this.collider = this.t.GetComponent<Collider>();
				if (this.joint != null)
				{
					this.c = this.joint.connectedBody;
					this.updateAnchor = (this.c != null);
				}
				this.lastPosition = this.t.position;
				this.lastRotation = this.t.rotation;
			}

			// Token: 0x06001130 RID: 4400 RVA: 0x0006BC44 File Offset: 0x00069E44
			public void RecordVelocity()
			{
				this.deltaPosition = this.t.position - this.lastPosition;
				this.lastPosition = this.t.position;
				this.deltaRotation = QuaTools.FromToRotation(this.lastRotation, this.t.rotation);
				this.lastRotation = this.t.rotation;
				this.deltaTime = Time.deltaTime;
			}

			// Token: 0x06001131 RID: 4401 RVA: 0x0006BCB8 File Offset: 0x00069EB8
			public void WakeUp(float velocityWeight, float angularVelocityWeight)
			{
				if (this.updateAnchor)
				{
					this.joint.connectedAnchor = this.t.InverseTransformPoint(this.c.position);
				}
				this.r.isKinematic = false;
				if (velocityWeight != 0f)
				{
					this.r.velocity = this.deltaPosition / this.deltaTime * velocityWeight;
				}
				if (angularVelocityWeight != 0f)
				{
					float num = 0f;
					Vector3 vector = Vector3.zero;
					this.deltaRotation.ToAngleAxis(out num, out vector);
					num *= 0.017453292f;
					num /= this.deltaTime;
					vector *= num * angularVelocityWeight;
					this.r.angularVelocity = Vector3.ClampMagnitude(vector, this.r.maxAngularVelocity);
				}
				this.r.WakeUp();
			}

			// Token: 0x04000FDA RID: 4058
			public Rigidbody r;

			// Token: 0x04000FDB RID: 4059
			public Transform t;

			// Token: 0x04000FDC RID: 4060
			public Collider collider;

			// Token: 0x04000FDD RID: 4061
			public Joint joint;

			// Token: 0x04000FDE RID: 4062
			public Rigidbody c;

			// Token: 0x04000FDF RID: 4063
			public bool updateAnchor;

			// Token: 0x04000FE0 RID: 4064
			public Vector3 deltaPosition;

			// Token: 0x04000FE1 RID: 4065
			public Quaternion deltaRotation;

			// Token: 0x04000FE2 RID: 4066
			public float deltaTime;

			// Token: 0x04000FE3 RID: 4067
			public Vector3 lastPosition;

			// Token: 0x04000FE4 RID: 4068
			public Quaternion lastRotation;
		}

		// Token: 0x02000214 RID: 532
		public class Child
		{
			// Token: 0x06001132 RID: 4402 RVA: 0x0006BD89 File Offset: 0x00069F89
			public Child(Transform transform)
			{
				this.t = transform;
				this.localPosition = this.t.localPosition;
				this.localRotation = this.t.localRotation;
			}

			// Token: 0x06001133 RID: 4403 RVA: 0x0006BDBC File Offset: 0x00069FBC
			public void FixTransform(float weight)
			{
				if (weight <= 0f)
				{
					return;
				}
				if (weight >= 1f)
				{
					this.t.localPosition = this.localPosition;
					this.t.localRotation = this.localRotation;
					return;
				}
				this.t.localPosition = Vector3.Lerp(this.t.localPosition, this.localPosition, weight);
				this.t.localRotation = Quaternion.Lerp(this.t.localRotation, this.localRotation, weight);
			}

			// Token: 0x06001134 RID: 4404 RVA: 0x0006BE41 File Offset: 0x0006A041
			public void StoreLocalState()
			{
				this.localPosition = this.t.localPosition;
				this.localRotation = this.t.localRotation;
			}

			// Token: 0x04000FE5 RID: 4069
			public Transform t;

			// Token: 0x04000FE6 RID: 4070
			public Vector3 localPosition;

			// Token: 0x04000FE7 RID: 4071
			public Quaternion localRotation;
		}

		// Token: 0x02000215 RID: 533
		[CompilerGenerated]
		private sealed class <DisableRagdollSmooth>d__21 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06001135 RID: 4405 RVA: 0x0006BE65 File Offset: 0x0006A065
			[DebuggerHidden]
			public <DisableRagdollSmooth>d__21(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06001136 RID: 4406 RVA: 0x0006BE74 File Offset: 0x0006A074
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001137 RID: 4407 RVA: 0x0006BE78 File Offset: 0x0006A078
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				RagdollUtility ragdollUtility = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					for (int i = 0; i < ragdollUtility.rigidbones.Length; i++)
					{
						ragdollUtility.rigidbones[i].r.isKinematic = true;
					}
					for (int j = 0; j < ragdollUtility.allIKComponents.Length; j++)
					{
						ragdollUtility.allIKComponents[j].fixTransforms = ragdollUtility.fixTransforms[j];
						if (ragdollUtility.disabledIKComponents[j])
						{
							ragdollUtility.allIKComponents[j].enabled = true;
						}
					}
					ragdollUtility.animator.updateMode = ragdollUtility.animatorUpdateMode;
					ragdollUtility.animator.enabled = true;
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				if (ragdollUtility.ragdollWeight <= 0f)
				{
					this.<>2__current = null;
					this.<>1__state = 2;
					return true;
				}
				ragdollUtility.ragdollWeight = Mathf.SmoothDamp(ragdollUtility.ragdollWeight, 0f, ref ragdollUtility.ragdollWeightV, ragdollUtility.ragdollToAnimationTime);
				if (ragdollUtility.ragdollWeight < 0.001f)
				{
					ragdollUtility.ragdollWeight = 0f;
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x06001138 RID: 4408 RVA: 0x0006BFA9 File Offset: 0x0006A1A9
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001139 RID: 4409 RVA: 0x0006BFB1 File Offset: 0x0006A1B1
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x0600113A RID: 4410 RVA: 0x0006BFB8 File Offset: 0x0006A1B8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000FE8 RID: 4072
			private int <>1__state;

			// Token: 0x04000FE9 RID: 4073
			private object <>2__current;

			// Token: 0x04000FEA RID: 4074
			public RagdollUtility <>4__this;
		}
	}
}
