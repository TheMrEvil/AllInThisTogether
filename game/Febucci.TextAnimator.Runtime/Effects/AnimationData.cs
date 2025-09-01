using System;
using Febucci.UI.Core;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class AnimationData
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004384 File Offset: 0x00002584
		public bool TryCalculatingMatrix(CharacterData character, float timePassed, float weight, out Matrix4x4 matrix, out Vector3 offset)
		{
			matrix = default(Matrix4x4);
			if (!this.movementX.enabled && !this.movementY.enabled && !this.movementZ.enabled && !this.rotX.enabled && !this.rotY.enabled && !this.rotZ.enabled && !this.scaleX.enabled && !this.scaleY.enabled)
			{
				offset = Vector2.zero;
				return false;
			}
			offset = (character.current.positions[0] + character.current.positions[2]) / 2f;
			this.rot = Quaternion.Euler(Mathf.LerpUnclamped(0f, this.rotX.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(0f, this.rotY.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(0f, this.rotZ.Evaluate(timePassed, character.index), weight));
			this.movement = new Vector3(Mathf.LerpUnclamped(0f, this.movementX.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(0f, this.movementY.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(0f, this.movementZ.Evaluate(timePassed, character.index), weight));
			this.scale = new Vector2(Mathf.LerpUnclamped(1f, this.scaleX.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(1f, this.scaleY.Evaluate(timePassed, character.index), weight));
			matrix.SetTRS(this.movement, this.rot, this.scale);
			return true;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004574 File Offset: 0x00002774
		public bool TryCalculatingColor(CharacterData character, float timePassed, float weight, out Color32 color)
		{
			if (!this.colorCurve.enabled)
			{
				color = Color.white;
				return false;
			}
			color = this.colorCurve.Evaluate(timePassed, character.index);
			return true;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000045B0 File Offset: 0x000027B0
		public AnimationData()
		{
		}

		// Token: 0x0400007E RID: 126
		[FloatCurveProperty]
		public FloatCurve movementX = new FloatCurve(1f, 0f, 0f);

		// Token: 0x0400007F RID: 127
		[FloatCurveProperty]
		public FloatCurve movementY = new FloatCurve(1f, 0f, 0f);

		// Token: 0x04000080 RID: 128
		[FloatCurveProperty]
		public FloatCurve movementZ = new FloatCurve(1f, 0f, 0f);

		// Token: 0x04000081 RID: 129
		[FloatCurveProperty]
		public FloatCurve scaleX = new FloatCurve(2f, 0f, 1f);

		// Token: 0x04000082 RID: 130
		[FloatCurveProperty]
		public FloatCurve scaleY = new FloatCurve(2f, 0f, 1f);

		// Token: 0x04000083 RID: 131
		[FloatCurveProperty]
		public FloatCurve rotX = new FloatCurve(45f, 0f, 0f);

		// Token: 0x04000084 RID: 132
		[FloatCurveProperty]
		public FloatCurve rotY = new FloatCurve(45f, 0f, 0f);

		// Token: 0x04000085 RID: 133
		[FloatCurveProperty]
		public FloatCurve rotZ = new FloatCurve(45f, 0f, 0f);

		// Token: 0x04000086 RID: 134
		[ColorCurveProperty]
		public ColorCurve colorCurve = new ColorCurve(0f);

		// Token: 0x04000087 RID: 135
		private Vector3 movement;

		// Token: 0x04000088 RID: 136
		private Vector2 scale;

		// Token: 0x04000089 RID: 137
		private Quaternion rot;
	}
}
