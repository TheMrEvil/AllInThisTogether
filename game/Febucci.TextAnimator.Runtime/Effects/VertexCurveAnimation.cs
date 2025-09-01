using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000023 RID: 35
	[Preserve]
	[CreateAssetMenu(fileName = "Vertex Curve Animation", menuName = "Text Animator/Animations/Special/Vertex Curve Animation")]
	[EffectInfo("", EffectCategory.All)]
	public sealed class VertexCurveAnimation : AnimationScriptableBase
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000040F9 File Offset: 0x000022F9
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004101 File Offset: 0x00002301
		public AnimationData[] VertexData
		{
			get
			{
				return this.animationPerVertexData;
			}
			set
			{
				this.animationPerVertexData = value;
				this.ClampVertexDataArray();
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004110 File Offset: 0x00002310
		public override void ResetContext(TAnimCore animator)
		{
			this.weightMult = 1f;
			this.timeSpeed = 1f;
			this.ClampVertexDataArray();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004130 File Offset: 0x00002330
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "f")
			{
				this.timeSpeed = modifier.value;
				return;
			}
			if (!(name == "a"))
			{
				return;
			}
			this.weightMult = modifier.value;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004178 File Offset: 0x00002378
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.timePassed = this.timeMode.GetTime(animator.time.timeSinceStart * this.timeSpeed, character.passedTime * this.timeSpeed, character.index);
			if (this.timePassed < 0f)
			{
				return;
			}
			float num = this.weightMult * this.emissionCurve.Evaluate(this.timePassed);
			for (byte b = 0; b < 4; b += 1)
			{
				if (this.animationPerVertexData[(int)b].TryCalculatingMatrix(character, this.timePassed, num, out this.matrix, out this.offset))
				{
					character.current.positions[(int)b] = this.matrix.MultiplyPoint3x4(character.current.positions[(int)b] - this.offset) + this.offset;
				}
				if (this.animationPerVertexData[(int)b].TryCalculatingColor(character, this.timePassed, num, out this.color))
				{
					character.current.colors[(int)b] = Color32.LerpUnclamped(character.current.colors[(int)b], this.color, Mathf.Clamp01(num));
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000042B5 File Offset: 0x000024B5
		public override float GetMaxDuration()
		{
			return this.emissionCurve.GetMaxDuration();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000042C2 File Offset: 0x000024C2
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000042C8 File Offset: 0x000024C8
		private void ClampVertexDataArray()
		{
			for (int i = 0; i < this.animationPerVertexData.Length; i++)
			{
				if (this.animationPerVertexData[i] == null)
				{
					this.animationPerVertexData[i] = new AnimationData();
				}
			}
			if (this.animationPerVertexData.Length != 4)
			{
				Debug.LogError("Vertex data array must have four vertices. Clamping/Resizing to four.");
				AnimationData[] array = new AnimationData[4];
				for (int j = 0; j < array.Length; j++)
				{
					if (j < this.animationPerVertexData.Length)
					{
						array[j] = this.animationPerVertexData[j];
					}
					else
					{
						array[j] = new AnimationData();
					}
				}
				this.animationPerVertexData = array;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004350 File Offset: 0x00002550
		private void OnValidate()
		{
			this.ClampVertexDataArray();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004358 File Offset: 0x00002558
		public VertexCurveAnimation()
		{
		}

		// Token: 0x04000072 RID: 114
		public TimeMode timeMode = new TimeMode(true);

		// Token: 0x04000073 RID: 115
		[EmissionCurveProperty]
		public EmissionCurve emissionCurve = new EmissionCurve();

		// Token: 0x04000074 RID: 116
		[SerializeField]
		private AnimationData[] animationPerVertexData = new AnimationData[4];

		// Token: 0x04000075 RID: 117
		private float timeSpeed;

		// Token: 0x04000076 RID: 118
		private float weightMult;

		// Token: 0x04000077 RID: 119
		private Matrix4x4 matrix;

		// Token: 0x04000078 RID: 120
		private Vector3 offset;

		// Token: 0x04000079 RID: 121
		private Vector3 movement;

		// Token: 0x0400007A RID: 122
		private Vector2 scale;

		// Token: 0x0400007B RID: 123
		private Quaternion rot;

		// Token: 0x0400007C RID: 124
		private Color32 color;

		// Token: 0x0400007D RID: 125
		private float timePassed;
	}
}
