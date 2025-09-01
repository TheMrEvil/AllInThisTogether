using System;
using UnityEngine;

namespace TeleportFX
{
	// Token: 0x02000007 RID: 7
	[AddComponentMenu("")]
	internal class TeleportFX_Settings : MonoBehaviour
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002CEC File Offset: 0x00000EEC
		public TeleportFX_Settings()
		{
		}

		// Token: 0x04000033 RID: 51
		public GameObject[] LightObjects;

		// Token: 0x04000034 RID: 52
		[Space]
		public Shader Shader;

		// Token: 0x04000035 RID: 53
		[Space]
		public bool UseVertexTeleportation;

		// Token: 0x04000036 RID: 54
		public float VertexTeleportationTime = 1f;

		// Token: 0x04000037 RID: 55
		public AnimationCurve VertexTeleporationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x04000038 RID: 56
		[Space]
		public bool UseDissolveByTime;

		// Token: 0x04000039 RID: 57
		public float CutoutTime = 1f;

		// Token: 0x0400003A RID: 58
		public AnimationCurve CutoutCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x0400003B RID: 59
		[Space]
		public bool UseDissolveByHeight;

		// Token: 0x0400003C RID: 60
		public Transform DissolveAnchor;

		// Token: 0x0400003D RID: 61
		public float DissolveByHeightDuration = 2f;

		// Token: 0x0400003E RID: 62
		[Space]
		public bool UseVertexPositionAsUV;

		// Token: 0x0400003F RID: 63
		public bool OverrideTexture;

		// Token: 0x04000040 RID: 64
		public Texture2D NoiseTexture;

		// Token: 0x04000041 RID: 65
		public Vector2 NoiseStrength = new Vector2(1f, 0f);

		// Token: 0x04000042 RID: 66
		public Vector2 NoiseScale = Vector2.one;

		// Token: 0x04000043 RID: 67
		[ColorUsage(true, true)]
		public Color DissolveColor1 = Color.yellow;

		// Token: 0x04000044 RID: 68
		[ColorUsage(true, true)]
		public Color DissolveColor2 = Color.red;

		// Token: 0x04000045 RID: 69
		[ColorUsage(true, true)]
		public Color DissolveColor3 = Color.black;

		// Token: 0x04000046 RID: 70
		public Vector3 DissolveThresold = new Vector3(0.75f, 0.75f, 0.9f);
	}
}
