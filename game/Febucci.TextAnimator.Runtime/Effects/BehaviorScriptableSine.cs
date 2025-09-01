using System;
using Febucci.UI.Core;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001F RID: 31
	public abstract class BehaviorScriptableSine : BehaviorScriptableBase
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003A60 File Offset: 0x00001C60
		public override void ResetContext(TAnimCore animator)
		{
			this.amplitude = this.baseAmplitude;
			this.frequency = this.baseFrequency;
			this.waveSize = this.baseWaveSize;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003A88 File Offset: 0x00001C88
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "a")
			{
				this.amplitude = this.baseAmplitude * modifier.value;
				return;
			}
			if (name == "f")
			{
				this.frequency = this.baseFrequency * modifier.value;
				return;
			}
			if (!(name == "w"))
			{
				return;
			}
			this.waveSize = this.baseWaveSize * modifier.value;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003AFF File Offset: 0x00001CFF
		protected BehaviorScriptableSine()
		{
		}

		// Token: 0x04000060 RID: 96
		public float baseAmplitude = 1f;

		// Token: 0x04000061 RID: 97
		public float baseFrequency = 1f;

		// Token: 0x04000062 RID: 98
		[Range(0f, 1f)]
		public float baseWaveSize = 0.2f;

		// Token: 0x04000063 RID: 99
		protected float amplitude;

		// Token: 0x04000064 RID: 100
		protected float frequency;

		// Token: 0x04000065 RID: 101
		protected float waveSize;
	}
}
