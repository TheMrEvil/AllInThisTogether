using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x0200001D RID: 29
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Wiggle", fileName = "Wiggle Behavior")]
	[EffectInfo("wiggle", EffectCategory.Behaviors)]
	[DefaultValue("baseAmplitude", 4.74f)]
	[DefaultValue("baseFrequency", 7.82f)]
	[DefaultValue("baseWaveSize", 0.551f)]
	public sealed class WiggleBehavior : BehaviorScriptableSine
	{
		// Token: 0x06000067 RID: 103 RVA: 0x0000396C File Offset: 0x00001B6C
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.directions = new Vector3[23];
			for (int i = 0; i < 23; i++)
			{
				this.directions[i] = TextUtilities.fakeRandoms[UnityEngine.Random.Range(0, 24)] * Mathf.Sign(Mathf.Sin((float)i));
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000039C8 File Offset: 0x00001BC8
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.indexCache = character.index % 23;
			character.current.positions.MoveChar(this.directions[this.indexCache] * Mathf.Sin(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize) * this.amplitude * character.uniformIntensity);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003A46 File Offset: 0x00001C46
		public WiggleBehavior()
		{
		}

		// Token: 0x0400005D RID: 93
		private const int maxDirections = 23;

		// Token: 0x0400005E RID: 94
		private Vector3[] directions;

		// Token: 0x0400005F RID: 95
		private int indexCache;
	}
}
