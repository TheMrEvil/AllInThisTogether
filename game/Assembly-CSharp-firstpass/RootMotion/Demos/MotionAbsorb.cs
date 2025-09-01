using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000141 RID: 321
	public class MotionAbsorb : OffsetModifier
	{
		// Token: 0x06000D05 RID: 3333 RVA: 0x000587A7 File Offset: 0x000569A7
		protected override void Start()
		{
			base.Start();
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterIK));
			this.initialMode = this.mode;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000587E8 File Offset: 0x000569E8
		private void OnCollisionEnter(Collision c)
		{
			if (this.timer > 0f)
			{
				return;
			}
			this.timer = 1f;
			for (int i = 0; i < this.absorbers.Length; i++)
			{
				this.absorbers[i].SetToBone(this.ik.solver, this.mode);
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00058840 File Offset: 0x00056A40
		protected override void OnModifyOffset()
		{
			if (this.timer <= 0f)
			{
				return;
			}
			this.mode = this.initialMode;
			this.timer -= Time.deltaTime * this.falloffSpeed;
			this.w = this.falloff.Evaluate(this.timer);
			if (this.mode == MotionAbsorb.Mode.Position)
			{
				for (int i = 0; i < this.absorbers.Length; i++)
				{
					this.absorbers[i].UpdateEffectorWeights(this.w * this.weight);
				}
				return;
			}
			for (int j = 0; j < this.absorbers.Length; j++)
			{
				this.absorbers[j].SetPosition(this.w * this.weight);
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000588FC File Offset: 0x00056AFC
		private void AfterIK()
		{
			if (this.timer <= 0f)
			{
				return;
			}
			if (this.mode == MotionAbsorb.Mode.Position)
			{
				return;
			}
			for (int i = 0; i < this.absorbers.Length; i++)
			{
				this.absorbers[i].SetRotation(this.w * this.weight);
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00058950 File Offset: 0x00056B50
		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterIK));
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0005899D File Offset: 0x00056B9D
		public MotionAbsorb()
		{
		}

		// Token: 0x04000AA9 RID: 2729
		[Tooltip("Use either effector position, position weight, rotation, rotationWeight or positionOffset and rotating the bone directly.")]
		public MotionAbsorb.Mode mode;

		// Token: 0x04000AAA RID: 2730
		[Tooltip("Array containing the absorbers")]
		public MotionAbsorb.Absorber[] absorbers;

		// Token: 0x04000AAB RID: 2731
		[Tooltip("Weight falloff curve (how fast will the effect reduce after impact)")]
		public AnimationCurve falloff;

		// Token: 0x04000AAC RID: 2732
		[Tooltip("How fast will the impact fade away. (if 1, effect lasts for 1 second)")]
		public float falloffSpeed = 1f;

		// Token: 0x04000AAD RID: 2733
		private float timer;

		// Token: 0x04000AAE RID: 2734
		private float w;

		// Token: 0x04000AAF RID: 2735
		private MotionAbsorb.Mode initialMode;

		// Token: 0x02000231 RID: 561
		[Serializable]
		public enum Mode
		{
			// Token: 0x04001091 RID: 4241
			Position,
			// Token: 0x04001092 RID: 4242
			PositionOffset
		}

		// Token: 0x02000232 RID: 562
		[Serializable]
		public class Absorber
		{
			// Token: 0x060011A9 RID: 4521 RVA: 0x0006DC00 File Offset: 0x0006BE00
			public void SetToBone(IKSolverFullBodyBiped solver, MotionAbsorb.Mode mode)
			{
				this.e = solver.GetEffector(this.effector);
				if (mode == MotionAbsorb.Mode.Position)
				{
					this.e.position = this.e.bone.position;
					this.e.rotation = this.e.bone.rotation;
					return;
				}
				if (mode != MotionAbsorb.Mode.PositionOffset)
				{
					return;
				}
				this.position = this.e.bone.position;
				this.rotation = this.e.bone.rotation;
			}

			// Token: 0x060011AA RID: 4522 RVA: 0x0006DC8A File Offset: 0x0006BE8A
			public void UpdateEffectorWeights(float w)
			{
				this.e.positionWeight = w * this.weight;
				this.e.rotationWeight = w * this.weight;
			}

			// Token: 0x060011AB RID: 4523 RVA: 0x0006DCB4 File Offset: 0x0006BEB4
			public void SetPosition(float w)
			{
				this.e.positionOffset += (this.position - this.e.bone.position) * w * this.weight;
			}

			// Token: 0x060011AC RID: 4524 RVA: 0x0006DD03 File Offset: 0x0006BF03
			public void SetRotation(float w)
			{
				this.e.bone.rotation = Quaternion.Slerp(this.e.bone.rotation, this.rotation, w * this.weight);
			}

			// Token: 0x060011AD RID: 4525 RVA: 0x0006DD38 File Offset: 0x0006BF38
			public Absorber()
			{
			}

			// Token: 0x04001093 RID: 4243
			[Tooltip("The type of effector (hand, foot, shoulder...) - this is just an enum")]
			public FullBodyBipedEffector effector;

			// Token: 0x04001094 RID: 4244
			[Tooltip("How much should motion be absorbed on this effector")]
			public float weight = 1f;

			// Token: 0x04001095 RID: 4245
			private Vector3 position;

			// Token: 0x04001096 RID: 4246
			private Quaternion rotation = Quaternion.identity;

			// Token: 0x04001097 RID: 4247
			private IKEffector e;
		}
	}
}
