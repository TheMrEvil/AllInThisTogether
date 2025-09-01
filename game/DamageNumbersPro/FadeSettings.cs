using System;
using UnityEngine;

namespace DamageNumbersPro
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public struct FadeSettings
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002859 File Offset: 0x00000A59
		public FadeSettings(Vector2 newScale)
		{
			this.fadeDuration = 0.2f;
			this.positionOffset = new Vector2(0.5f, 0f);
			this.scaleOffset = Vector2.one;
			this.scale = newScale;
		}

		// Token: 0x0400002D RID: 45
		[Tooltip("The duration that the fade takes.")]
		public float fadeDuration;

		// Token: 0x0400002E RID: 46
		[Space(8f)]
		[Tooltip("Moves TextA and TextB in opposite directions based on this offset.")]
		public Vector2 positionOffset;

		// Token: 0x0400002F RID: 47
		[Tooltip("Scales TextA and divides the scale of TextB by this scale.")]
		public Vector2 scaleOffset;

		// Token: 0x04000030 RID: 48
		[Tooltip("Scales both TextA and TextB by this scale.")]
		public Vector2 scale;
	}
}
