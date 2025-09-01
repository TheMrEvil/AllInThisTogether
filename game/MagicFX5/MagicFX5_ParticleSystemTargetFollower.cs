using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000016 RID: 22
	[ExecuteAlways]
	[RequireComponent(typeof(ParticleSystem))]
	public class MagicFX5_ParticleSystemTargetFollower : MagicFX5_IScriptInstance
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000445D File Offset: 0x0000265D
		internal override void OnEnableExtended()
		{
			this._ps = base.GetComponent<ParticleSystem>();
			this.UpdateParticlePosition();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004471 File Offset: 0x00002671
		internal override void OnDisableExtended()
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004474 File Offset: 0x00002674
		private void UpdateParticlePosition()
		{
			if (this.OverrideTargetFromEffectSettings != null && this.OverrideTargetFromEffectSettings.Targets.Length != 0)
			{
				this.Target = this.OverrideTargetFromEffectSettings.Targets[0];
			}
			if (this.Target == null)
			{
				return;
			}
			this._ps.shape.position = base.transform.InverseTransformPoint(this.Target.position);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000044E8 File Offset: 0x000026E8
		internal override void ManualUpdate()
		{
			this.UpdateParticlePosition();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000044F0 File Offset: 0x000026F0
		private void OnEditorUpdate()
		{
			this.UpdateParticlePosition();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000044F8 File Offset: 0x000026F8
		public MagicFX5_ParticleSystemTargetFollower()
		{
		}

		// Token: 0x040000A0 RID: 160
		public Transform Target;

		// Token: 0x040000A1 RID: 161
		public MagicFX5_EffectSettings OverrideTargetFromEffectSettings;

		// Token: 0x040000A2 RID: 162
		private ParticleSystem _ps;
	}
}
