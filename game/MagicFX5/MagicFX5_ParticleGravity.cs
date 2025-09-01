using System;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

namespace MagicFX5
{
	// Token: 0x02000014 RID: 20
	public class MagicFX5_ParticleGravity : MonoBehaviour
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00004093 File Offset: 0x00002293
		private void Awake()
		{
			this._ps = base.GetComponent<ParticleSystem>();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000040A1 File Offset: 0x000022A1
		private void OnEnable()
		{
			this._leftTime = 0f;
			this._leftAffectedParticles = 0;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000040B8 File Offset: 0x000022B8
		private void OnParticleUpdateJobScheduled()
		{
			this._leftTime += Time.deltaTime;
			if (this._leftTime < this.Delay)
			{
				return;
			}
			if (this.ForceLifeTime > 0f && this._leftTime - this.Delay > this.ForceLifeTime)
			{
				return;
			}
			float num = (this.ForceLifeTime > 0f) ? this.ForceCurve.Evaluate((this._leftTime - this.Delay) / this.ForceLifeTime) : 1f;
			this._job.CurrentForce = Time.deltaTime * this.Force * num;
			this._job.TargetPosition = this.Target.position;
			this._job.ForceByDistanceRemap = this.ForceByDistanceRemap;
			this._job.UseStopDistance = this.UseStopDistance;
			this._job.StopDistance = this.StopDistance;
			this._job.UseRotation = this.UseRotation;
			this._job.RotationForceAxis = this.RotationForceAxis;
			this._job.RotationSpeed = this.RotationSpeed;
			this._job.AffectParticlesSequentially = this.AffectParticlesSequentially;
			this._job.AffectParticlesPerFrame = this.AffectParticlesPerFrame;
			this._job.LeftAffectedParticles = this._leftAffectedParticles;
			this._leftAffectedParticles += this.AffectParticlesPerFrame;
			this._job.Schedule(this._ps, default(JobHandle));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004234 File Offset: 0x00002434
		public MagicFX5_ParticleGravity()
		{
		}

		// Token: 0x0400007F RID: 127
		public Transform Target;

		// Token: 0x04000080 RID: 128
		public AnimationCurve ForceCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 1f);

		// Token: 0x04000081 RID: 129
		public float Force = 1f;

		// Token: 0x04000082 RID: 130
		public float ForceLifeTime = -1f;

		// Token: 0x04000083 RID: 131
		public bool UseRotation;

		// Token: 0x04000084 RID: 132
		public Vector3 RotationForceAxis = Vector3.up;

		// Token: 0x04000085 RID: 133
		public float RotationSpeed = 1f;

		// Token: 0x04000086 RID: 134
		public Vector2 ForceByDistanceRemap = new Vector2(0f, 1f);

		// Token: 0x04000087 RID: 135
		public bool UseStopDistance;

		// Token: 0x04000088 RID: 136
		public float StopDistance = 0.5f;

		// Token: 0x04000089 RID: 137
		public float Delay;

		// Token: 0x0400008A RID: 138
		[Space]
		public bool AffectParticlesSequentially;

		// Token: 0x0400008B RID: 139
		public int AffectParticlesPerFrame = 1;

		// Token: 0x0400008C RID: 140
		private ParticleSystem _ps;

		// Token: 0x0400008D RID: 141
		private MagicFX5_ParticleGravity.UpdateParticlesJob _job;

		// Token: 0x0400008E RID: 142
		private float _leftTime;

		// Token: 0x0400008F RID: 143
		private int _leftAffectedParticles;

		// Token: 0x0200002F RID: 47
		private struct UpdateParticlesJob : IJobParticleSystem
		{
			// Token: 0x060000D8 RID: 216 RVA: 0x00006C18 File Offset: 0x00004E18
			public void Execute(ParticleSystemJobData particles)
			{
				int count = particles.count;
				ParticleSystemNativeArray3 velocities = particles.velocities;
				ParticleSystemNativeArray3 positions = particles.positions;
				int num = this.AffectParticlesSequentially ? (this.LeftAffectedParticles + this.AffectParticlesPerFrame) : count;
				if (num > count)
				{
					num = count;
				}
				for (int i = 0; i < num; i++)
				{
					float num2 = Vector3.Distance(this.TargetPosition, positions[i]);
					Vector3 a = Vector3.Normalize(this.TargetPosition - positions[i]);
					if (this.UseStopDistance && num2 < this.StopDistance)
					{
						velocities[i] = a * 0.0001f;
					}
					else
					{
						Vector3 b = a * this.CurrentForce * this.ForceByDistanceRemap.y;
						if (this.UseRotation)
						{
							Vector3 b2 = Vector3.Cross(this.TargetPosition - positions[i], this.RotationForceAxis).normalized * this.CurrentForce * this.RotationSpeed;
							ref ParticleSystemNativeArray3 ptr = ref velocities;
							int index = i;
							ptr[index] += b;
							ptr = ref velocities;
							index = i;
							ptr[index] += b2;
						}
						else
						{
							ref ParticleSystemNativeArray3 ptr = ref velocities;
							int index = i;
							ptr[index] += b;
						}
					}
				}
			}

			// Token: 0x04000161 RID: 353
			public float CurrentForce;

			// Token: 0x04000162 RID: 354
			public Vector3 TargetPosition;

			// Token: 0x04000163 RID: 355
			public Vector2 ForceByDistanceRemap;

			// Token: 0x04000164 RID: 356
			public bool UseStopDistance;

			// Token: 0x04000165 RID: 357
			public float StopDistance;

			// Token: 0x04000166 RID: 358
			public bool UseRotation;

			// Token: 0x04000167 RID: 359
			public Vector3 RotationForceAxis;

			// Token: 0x04000168 RID: 360
			public float RotationSpeed;

			// Token: 0x04000169 RID: 361
			public bool AffectParticlesSequentially;

			// Token: 0x0400016A RID: 362
			public int AffectParticlesPerFrame;

			// Token: 0x0400016B RID: 363
			public int LeftAffectedParticles;
		}
	}
}
