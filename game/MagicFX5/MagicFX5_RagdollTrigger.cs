using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000019 RID: 25
	public class MagicFX5_RagdollTrigger : MonoBehaviour
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000047A7 File Offset: 0x000029A7
		internal void OnEnable()
		{
			base.StartCoroutine(this.Initialize());
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000047B8 File Offset: 0x000029B8
		internal void OnDisable()
		{
			base.StopCoroutine(this.Initialize());
			foreach (Animator animator in this._animators)
			{
				if (animator != null)
				{
					animator.enabled = true;
				}
			}
			if (this.UseAntigravity)
			{
				foreach (Rigidbody rigidbody in this._ragdollRigidbodies)
				{
					if (rigidbody != null)
					{
						rigidbody.useGravity = true;
					}
				}
			}
			base.CancelInvoke("DisableAntigravity");
			base.CancelInvoke("DisableRagdoll");
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000488C File Offset: 0x00002A8C
		private void FixedUpdate()
		{
			if (this.UseAntigravity)
			{
				this.UpdateGravity();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000489C File Offset: 0x00002A9C
		private IEnumerator Initialize()
		{
			yield return null;
			yield return new WaitForSeconds(this.StartDelayRagdoll);
			this._ragdollHips.Clear();
			this._startBonesTransform.Clear();
			this._ragdollRigidbodies.Clear();
			this._animators.Clear();
			this._gravityLiftTime = 0f;
			foreach (Transform transform in this.EffectSettings.Targets)
			{
				Rigidbody[] componentsInChildren = transform.GetComponentsInChildren<Rigidbody>();
				if (componentsInChildren != null && componentsInChildren.Length != 0)
				{
					foreach (Rigidbody rigidbody in componentsInChildren)
					{
						rigidbody.isKinematic = true;
						if (this.UseAntigravity)
						{
							rigidbody.useGravity = false;
						}
						this._ragdollRigidbodies.Add(rigidbody);
						this._startBonesTransform.Add(new MagicFX5_RagdollTrigger.Bone(rigidbody));
					}
					Animator component = transform.GetComponent<Animator>();
					if (component != null)
					{
						this._animators.Add(component);
					}
					SkinnedMeshRenderer componentInChildren = transform.GetComponentInChildren<SkinnedMeshRenderer>();
					if (componentInChildren != null)
					{
						this._ragdollHips.Add(new MagicFX5_RagdollTrigger.Hip(componentInChildren.rootBone.GetComponent<Rigidbody>(), this.AntigravityForceMultiplier, this.AntigravityForceRandomRangePercent));
					}
					else
					{
						this._ragdollHips.Add(new MagicFX5_RagdollTrigger.Hip(componentsInChildren[0], this.AntigravityForceMultiplier, this.AntigravityForceRandomRangePercent));
					}
				}
			}
			yield return null;
			foreach (Rigidbody rigidbody2 in this._ragdollRigidbodies)
			{
				rigidbody2.isKinematic = false;
			}
			yield return null;
			foreach (Animator animator in this._animators)
			{
				animator.enabled = false;
			}
			if (this.UseAntigravity)
			{
				base.Invoke("DisableAntigravity", this.AntigravityLifeTime);
			}
			if (this.RagdollLifeTime > 0f)
			{
				base.Invoke("DisableRagdoll", this.RagdollLifeTime);
			}
			yield break;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000048AC File Offset: 0x00002AAC
		private void DisableAntigravity()
		{
			foreach (Rigidbody rigidbody in this._ragdollRigidbodies)
			{
				rigidbody.useGravity = true;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004900 File Offset: 0x00002B00
		private void DisableRagdoll()
		{
			foreach (Animator animator in this._animators)
			{
				animator.enabled = true;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004954 File Offset: 0x00002B54
		private void UpdateGravity()
		{
			if (this._ragdollHips.Count == 0)
			{
				return;
			}
			this._gravityLiftTime += Time.fixedDeltaTime;
			foreach (MagicFX5_RagdollTrigger.Hip hip in this._ragdollHips)
			{
				float d = this.AntigravityForceCurve.Evaluate(this._gravityLiftTime / this.AntigravityLifeTime);
				hip.HipRigidbody.AddForce(Vector3.up * d * hip.InitialForceMultiplier, ForceMode.Force);
				hip.HipRigidbody.AddTorque(hip.InitialRotationTorque * this.AntigravityTorqueMultiplier);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A18 File Offset: 0x00002C18
		public MagicFX5_RagdollTrigger()
		{
		}

		// Token: 0x040000AD RID: 173
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x040000AE RID: 174
		public float StartDelayRagdoll = 0.05f;

		// Token: 0x040000AF RID: 175
		public float RagdollLifeTime = -1f;

		// Token: 0x040000B0 RID: 176
		[Space]
		public bool UseAntigravity;

		// Token: 0x040000B1 RID: 177
		public float AntigravityLifeTime = 3f;

		// Token: 0x040000B2 RID: 178
		public AnimationCurve AntigravityForceCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x040000B3 RID: 179
		public float AntigravityForceMultiplier = 0.1f;

		// Token: 0x040000B4 RID: 180
		public float AntigravityTorqueMultiplier = 0.1f;

		// Token: 0x040000B5 RID: 181
		[Range(0f, 100f)]
		public float AntigravityForceRandomRangePercent = 25f;

		// Token: 0x040000B6 RID: 182
		private List<MagicFX5_RagdollTrigger.Hip> _ragdollHips = new List<MagicFX5_RagdollTrigger.Hip>();

		// Token: 0x040000B7 RID: 183
		private List<MagicFX5_RagdollTrigger.Bone> _startBonesTransform = new List<MagicFX5_RagdollTrigger.Bone>();

		// Token: 0x040000B8 RID: 184
		private List<Rigidbody> _ragdollRigidbodies = new List<Rigidbody>();

		// Token: 0x040000B9 RID: 185
		private List<Animator> _animators = new List<Animator>();

		// Token: 0x040000BA RID: 186
		private float _gravityLiftTime;

		// Token: 0x02000032 RID: 50
		private struct Bone
		{
			// Token: 0x060000DF RID: 223 RVA: 0x000071C8 File Offset: 0x000053C8
			public Bone(Rigidbody rg)
			{
				this.BoneTransform = rg.transform;
				this.LocalPos = rg.transform.localPosition;
				this.LocalRotation = rg.transform.localRotation;
			}

			// Token: 0x04000177 RID: 375
			public Transform BoneTransform;

			// Token: 0x04000178 RID: 376
			public Vector3 LocalPos;

			// Token: 0x04000179 RID: 377
			public Quaternion LocalRotation;
		}

		// Token: 0x02000033 RID: 51
		private struct Hip
		{
			// Token: 0x060000E0 RID: 224 RVA: 0x000071F8 File Offset: 0x000053F8
			public Hip(Rigidbody rg, float forceMultiplier, float randomRangePercent)
			{
				this.HipRigidbody = rg;
				this.InitialForceMultiplier = forceMultiplier + forceMultiplier * UnityEngine.Random.Range(-randomRangePercent, randomRangePercent) / 100f;
				this.InitialRotationTorque = UnityEngine.Random.insideUnitSphere;
			}

			// Token: 0x0400017A RID: 378
			public Rigidbody HipRigidbody;

			// Token: 0x0400017B RID: 379
			public float InitialForceMultiplier;

			// Token: 0x0400017C RID: 380
			public Vector3 InitialRotationTorque;
		}

		// Token: 0x02000034 RID: 52
		[CompilerGenerated]
		private sealed class <Initialize>d__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000E1 RID: 225 RVA: 0x00007224 File Offset: 0x00005424
			[DebuggerHidden]
			public <Initialize>d__19(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00007233 File Offset: 0x00005433
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x00007238 File Offset: 0x00005438
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MagicFX5_RagdollTrigger magicFX5_RagdollTrigger = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					this.<>2__current = new WaitForSeconds(magicFX5_RagdollTrigger.StartDelayRagdoll);
					this.<>1__state = 2;
					return true;
				case 2:
					this.<>1__state = -1;
					magicFX5_RagdollTrigger._ragdollHips.Clear();
					magicFX5_RagdollTrigger._startBonesTransform.Clear();
					magicFX5_RagdollTrigger._ragdollRigidbodies.Clear();
					magicFX5_RagdollTrigger._animators.Clear();
					magicFX5_RagdollTrigger._gravityLiftTime = 0f;
					foreach (Transform transform in magicFX5_RagdollTrigger.EffectSettings.Targets)
					{
						Rigidbody[] componentsInChildren = transform.GetComponentsInChildren<Rigidbody>();
						if (componentsInChildren != null && componentsInChildren.Length != 0)
						{
							foreach (Rigidbody rigidbody in componentsInChildren)
							{
								rigidbody.isKinematic = true;
								if (magicFX5_RagdollTrigger.UseAntigravity)
								{
									rigidbody.useGravity = false;
								}
								magicFX5_RagdollTrigger._ragdollRigidbodies.Add(rigidbody);
								magicFX5_RagdollTrigger._startBonesTransform.Add(new MagicFX5_RagdollTrigger.Bone(rigidbody));
							}
							Animator component = transform.GetComponent<Animator>();
							if (component != null)
							{
								magicFX5_RagdollTrigger._animators.Add(component);
							}
							SkinnedMeshRenderer componentInChildren = transform.GetComponentInChildren<SkinnedMeshRenderer>();
							if (componentInChildren != null)
							{
								magicFX5_RagdollTrigger._ragdollHips.Add(new MagicFX5_RagdollTrigger.Hip(componentInChildren.rootBone.GetComponent<Rigidbody>(), magicFX5_RagdollTrigger.AntigravityForceMultiplier, magicFX5_RagdollTrigger.AntigravityForceRandomRangePercent));
							}
							else
							{
								magicFX5_RagdollTrigger._ragdollHips.Add(new MagicFX5_RagdollTrigger.Hip(componentsInChildren[0], magicFX5_RagdollTrigger.AntigravityForceMultiplier, magicFX5_RagdollTrigger.AntigravityForceRandomRangePercent));
							}
						}
					}
					this.<>2__current = null;
					this.<>1__state = 3;
					return true;
				case 3:
					this.<>1__state = -1;
					foreach (Rigidbody rigidbody2 in magicFX5_RagdollTrigger._ragdollRigidbodies)
					{
						rigidbody2.isKinematic = false;
					}
					this.<>2__current = null;
					this.<>1__state = 4;
					return true;
				case 4:
					this.<>1__state = -1;
					foreach (Animator animator in magicFX5_RagdollTrigger._animators)
					{
						animator.enabled = false;
					}
					if (magicFX5_RagdollTrigger.UseAntigravity)
					{
						magicFX5_RagdollTrigger.Invoke("DisableAntigravity", magicFX5_RagdollTrigger.AntigravityLifeTime);
					}
					if (magicFX5_RagdollTrigger.RagdollLifeTime > 0f)
					{
						magicFX5_RagdollTrigger.Invoke("DisableRagdoll", magicFX5_RagdollTrigger.RagdollLifeTime);
					}
					return false;
				default:
					return false;
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x060000E4 RID: 228 RVA: 0x000074E0 File Offset: 0x000056E0
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x000074E8 File Offset: 0x000056E8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x060000E6 RID: 230 RVA: 0x000074EF File Offset: 0x000056EF
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400017D RID: 381
			private int <>1__state;

			// Token: 0x0400017E RID: 382
			private object <>2__current;

			// Token: 0x0400017F RID: 383
			public MagicFX5_RagdollTrigger <>4__this;
		}
	}
}
