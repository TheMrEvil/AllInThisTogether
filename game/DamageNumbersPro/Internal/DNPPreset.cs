using System;
using TMPro;
using UnityEngine;

namespace DamageNumbersPro.Internal
{
	// Token: 0x02000018 RID: 24
	[CreateAssetMenu(fileName = "Preset", menuName = "TextMeshPro/Preset for DNP", order = -1)]
	public class DNPPreset : ScriptableObject
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00006AA0 File Offset: 0x00004CA0
		public bool IsApplied(DamageNumber dn)
		{
			TMP_Text[] textMeshs = dn.GetTextMeshs();
			if (textMeshs[0] == null)
			{
				dn.GetReferencesIfNecessary();
				textMeshs = dn.GetTextMeshs();
			}
			bool result = true;
			if (this.changeFontAsset)
			{
				foreach (TMP_Text tmp_Text in textMeshs)
				{
					if (this.fontAsset != tmp_Text.font)
					{
						result = false;
					}
				}
			}
			if (this.changeColor)
			{
				foreach (TMP_Text tmp_Text2 in textMeshs)
				{
					if (this.color != tmp_Text2.color || this.enableGradient != tmp_Text2.enableVertexGradient || !this.gradient.Equals(tmp_Text2.colorGradient))
					{
						result = false;
					}
				}
			}
			if (this.changeNumber && (this.enableNumber != dn.enableNumber || !this.numberSettings.Equals(dn.numberSettings) || !this.digitSettings.Equals(dn.digitSettings)))
			{
				result = false;
			}
			if (this.changeLeftText && (this.enableLeftText != dn.enableLeftText || !this.leftTextSettings.Equals(dn.leftTextSettings) || this.leftText != dn.leftText))
			{
				result = false;
			}
			if (this.changeRightText && (this.enableRightText != dn.enableRightText || !this.rightTextSettings.Equals(dn.rightTextSettings) || this.rightText != dn.rightText))
			{
				result = false;
			}
			if (this.hideVerticalTexts && (dn.enableTopText || dn.enableBottomText))
			{
				result = false;
			}
			if (this.changeFadeIn && (this.durationFadeIn != dn.durationFadeIn || this.enableOffsetFadeIn != dn.enableOffsetFadeIn || this.offsetFadeIn != dn.offsetFadeIn || this.enableScaleFadeIn != dn.enableScaleFadeIn || this.scaleFadeIn != dn.scaleFadeIn || this.enableCrossScaleFadeIn != dn.enableCrossScaleFadeIn || this.crossScaleFadeIn != dn.crossScaleFadeIn || this.enableShakeFadeIn != dn.enableShakeFadeIn || this.shakeOffsetFadeIn != dn.shakeOffsetFadeIn || this.shakeFrequencyFadeIn != dn.shakeFrequencyFadeIn))
			{
				result = false;
			}
			if (this.changeFadeOut && (this.durationFadeOut != dn.durationFadeOut || this.enableOffsetFadeOut != dn.enableOffsetFadeOut || this.offsetFadeOut != dn.offsetFadeOut || this.enableScaleFadeOut != dn.enableScaleFadeOut || this.scaleFadeOut != dn.scaleFadeOut || this.enableCrossScaleFadeOut != dn.enableCrossScaleFadeOut || this.crossScaleFadeOut != dn.crossScaleFadeOut || this.enableShakeFadeOut != dn.enableShakeFadeOut || this.shakeOffsetFadeOut != dn.shakeOffsetFadeOut || this.shakeFrequencyFadeOut != dn.shakeFrequencyFadeOut))
			{
				result = false;
			}
			if (this.changeMovement && (this.enableLerp != dn.enableLerp || !this.lerpSettings.Equals(dn.lerpSettings) || this.enableVelocity != dn.enableVelocity || !this.velocitySettings.Equals(dn.velocitySettings) || this.enableShaking != dn.enableShaking || !this.shakeSettings.Equals(dn.shakeSettings) || this.enableFollowing != dn.enableFollowing || !this.followSettings.Equals(dn.followSettings)))
			{
				result = false;
			}
			if (this.changeRotation && (this.enableStartRotation != dn.enableStartRotation || this.minRotation != dn.minRotation || this.maxRotation != dn.maxRotation || this.enableRotateOverTime != dn.enableRotateOverTime || this.minRotationSpeed != dn.minRotationSpeed || this.maxRotationSpeed != dn.maxRotationSpeed || !this.rotateOverTime.Equals(dn.rotateOverTime)))
			{
				result = false;
			}
			if (this.changeScaling && (this.enableScaleByNumber != dn.enableScaleByNumber || !this.scaleByNumberSettings.Equals(dn.scaleByNumberSettings) || this.enableScaleOverTime != dn.enableScaleOverTime || !this.scaleOverTime.Equals(dn.scaleOverTime)))
			{
				result = false;
			}
			if (this.changeSpamControl && (this.enableCombination != dn.enableCombination || !this.combinationSettings.Equals(dn.combinationSettings) || this.enableDestruction != dn.enableDestruction || !this.destructionSettings.Equals(dn.destructionSettings) || this.enableCollision != dn.enableCollision || !this.collisionSettings.Equals(dn.collisionSettings) || this.enablePush != dn.enablePush || !this.pushSettings.Equals(dn.pushSettings)))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007018 File Offset: 0x00005218
		public void Apply(DamageNumber dn)
		{
			TMP_Text[] textMeshs = dn.GetTextMeshs();
			if (this.changeFontAsset)
			{
				TMP_Text[] array = textMeshs;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].font = this.fontAsset;
				}
			}
			if (this.changeColor)
			{
				foreach (TMP_Text tmp_Text in textMeshs)
				{
					tmp_Text.color = this.color;
					tmp_Text.enableVertexGradient = this.enableGradient;
					tmp_Text.colorGradient = this.gradient;
				}
			}
			if (this.changeNumber)
			{
				dn.enableNumber = this.enableNumber;
				dn.numberSettings = this.numberSettings;
				dn.digitSettings = this.digitSettings;
			}
			if (this.changeLeftText)
			{
				dn.enableLeftText = this.enableLeftText;
				dn.leftText = this.leftText;
				dn.leftTextSettings = this.leftTextSettings;
			}
			if (this.changeRightText)
			{
				dn.enableRightText = this.enableRightText;
				dn.rightText = this.rightText;
				dn.rightTextSettings = this.rightTextSettings;
			}
			if (this.hideVerticalTexts)
			{
				dn.enableTopText = (dn.enableBottomText = false);
			}
			if (this.changeFadeIn)
			{
				dn.durationFadeIn = this.durationFadeIn;
				dn.enableOffsetFadeIn = this.enableOffsetFadeIn;
				dn.offsetFadeIn = this.offsetFadeIn;
				dn.enableScaleFadeIn = this.enableScaleFadeIn;
				dn.scaleFadeIn = this.scaleFadeIn;
				dn.enableCrossScaleFadeIn = this.enableCrossScaleFadeIn;
				dn.crossScaleFadeIn = this.crossScaleFadeIn;
				dn.enableShakeFadeIn = this.enableShakeFadeIn;
				dn.shakeOffsetFadeIn = this.shakeOffsetFadeIn;
				dn.shakeFrequencyFadeIn = this.shakeFrequencyFadeIn;
			}
			if (this.changeFadeOut)
			{
				dn.durationFadeOut = this.durationFadeOut;
				dn.enableOffsetFadeOut = this.enableOffsetFadeOut;
				dn.offsetFadeOut = this.offsetFadeOut;
				dn.enableScaleFadeOut = this.enableScaleFadeOut;
				dn.scaleFadeOut = this.scaleFadeOut;
				dn.enableCrossScaleFadeOut = this.enableCrossScaleFadeOut;
				dn.crossScaleFadeOut = this.crossScaleFadeOut;
				dn.enableShakeFadeOut = this.enableShakeFadeOut;
				dn.shakeOffsetFadeOut = this.shakeOffsetFadeOut;
				dn.shakeFrequencyFadeOut = this.shakeFrequencyFadeOut;
			}
			if (this.changeMovement)
			{
				dn.enableLerp = this.enableLerp;
				dn.lerpSettings = this.lerpSettings;
				dn.enableVelocity = this.enableVelocity;
				dn.velocitySettings = this.velocitySettings;
				dn.enableShaking = this.enableShaking;
				dn.shakeSettings = this.shakeSettings;
				dn.enableFollowing = this.enableFollowing;
				dn.followSettings = this.followSettings;
			}
			if (this.changeRotation)
			{
				dn.enableStartRotation = this.enableStartRotation;
				dn.minRotation = this.minRotation;
				dn.maxRotation = this.maxRotation;
				dn.enableRotateOverTime = this.enableRotateOverTime;
				dn.minRotationSpeed = this.minRotationSpeed;
				dn.maxRotationSpeed = this.maxRotationSpeed;
				dn.rotateOverTime = this.rotateOverTime;
			}
			if (this.changeScaling)
			{
				dn.enableScaleByNumber = this.enableScaleByNumber;
				dn.scaleByNumberSettings = this.scaleByNumberSettings;
				dn.enableScaleOverTime = this.enableScaleOverTime;
				dn.scaleOverTime = this.scaleOverTime;
			}
			if (this.changeSpamControl)
			{
				if (dn.spamGroup == null || dn.spamGroup == "")
				{
					dn.spamGroup = this.spamGroup;
				}
				dn.enableCombination = this.enableCombination;
				dn.combinationSettings = this.combinationSettings;
				dn.enableDestruction = this.enableDestruction;
				dn.destructionSettings = this.destructionSettings;
				dn.enableCollision = this.enableCollision;
				dn.collisionSettings = this.collisionSettings;
				dn.enablePush = this.enablePush;
				dn.pushSettings = this.pushSettings;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000073BC File Offset: 0x000055BC
		public void Get(DamageNumber dn)
		{
			TMP_Text[] textMeshs = dn.GetTextMeshs();
			this.changeFontAsset = true;
			foreach (TMP_Text tmp_Text in textMeshs)
			{
				if (tmp_Text != null)
				{
					this.fontAsset = tmp_Text.font;
				}
			}
			this.changeColor = true;
			foreach (TMP_Text tmp_Text2 in textMeshs)
			{
				if (tmp_Text2 != null)
				{
					this.color = tmp_Text2.color;
					this.enableGradient = tmp_Text2.enableVertexGradient;
					this.gradient = tmp_Text2.colorGradient;
				}
			}
			this.changeFadeIn = true;
			this.durationFadeIn = dn.durationFadeIn;
			this.enableOffsetFadeIn = dn.enableOffsetFadeIn;
			this.offsetFadeIn = dn.offsetFadeIn;
			this.enableScaleFadeIn = dn.enableScaleFadeIn;
			this.scaleFadeIn = dn.scaleFadeIn;
			this.enableCrossScaleFadeIn = dn.enableCrossScaleFadeIn;
			this.crossScaleFadeIn = dn.crossScaleFadeIn;
			this.enableShakeFadeIn = dn.enableShakeFadeIn;
			this.shakeOffsetFadeIn = dn.shakeOffsetFadeIn;
			this.shakeFrequencyFadeIn = dn.shakeFrequencyFadeIn;
			this.changeFadeOut = true;
			this.durationFadeOut = dn.durationFadeOut;
			this.enableOffsetFadeOut = dn.enableOffsetFadeOut;
			this.offsetFadeOut = dn.offsetFadeOut;
			this.enableScaleFadeOut = dn.enableScaleFadeOut;
			this.scaleFadeOut = dn.scaleFadeOut;
			this.enableCrossScaleFadeOut = dn.enableCrossScaleFadeOut;
			this.crossScaleFadeOut = dn.crossScaleFadeOut;
			this.enableShakeFadeOut = dn.enableShakeFadeOut;
			this.shakeOffsetFadeOut = dn.shakeOffsetFadeOut;
			this.shakeFrequencyFadeOut = dn.shakeFrequencyFadeOut;
			this.changeMovement = true;
			this.enableLerp = dn.enableLerp;
			this.lerpSettings = dn.lerpSettings;
			this.enableVelocity = dn.enableVelocity;
			this.velocitySettings = dn.velocitySettings;
			this.enableShaking = dn.enableShaking;
			this.shakeSettings = dn.shakeSettings;
			this.enableFollowing = dn.enableFollowing;
			this.followSettings = dn.followSettings;
			this.changeRotation = true;
			this.enableStartRotation = dn.enableStartRotation;
			this.minRotation = dn.minRotation;
			this.maxRotation = dn.maxRotation;
			this.enableRotateOverTime = dn.enableRotateOverTime;
			this.minRotationSpeed = dn.minRotationSpeed;
			this.maxRotationSpeed = dn.maxRotationSpeed;
			this.rotateOverTime = dn.rotateOverTime;
			this.changeScaling = true;
			this.enableScaleByNumber = dn.enableScaleByNumber;
			this.scaleByNumberSettings = dn.scaleByNumberSettings;
			this.enableScaleOverTime = dn.enableScaleOverTime;
			this.scaleOverTime = dn.scaleOverTime;
			this.changeSpamControl = true;
			this.spamGroup = ((dn.spamGroup != "") ? "Default" : "");
			this.enableCombination = dn.enableCombination;
			this.combinationSettings = dn.combinationSettings;
			this.enableDestruction = dn.enableDestruction;
			this.destructionSettings = dn.destructionSettings;
			this.enableCollision = dn.enableCollision;
			this.collisionSettings = dn.collisionSettings;
			this.enablePush = dn.enablePush;
			this.pushSettings = dn.pushSettings;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000076D0 File Offset: 0x000058D0
		public DNPPreset()
		{
		}

		// Token: 0x04000108 RID: 264
		public bool changeFontAsset;

		// Token: 0x04000109 RID: 265
		public TMP_FontAsset fontAsset;

		// Token: 0x0400010A RID: 266
		public bool changeColor;

		// Token: 0x0400010B RID: 267
		public Color color = Color.white;

		// Token: 0x0400010C RID: 268
		public bool enableGradient;

		// Token: 0x0400010D RID: 269
		public VertexGradient gradient = new VertexGradient(Color.white, Color.white, Color.white, Color.white);

		// Token: 0x0400010E RID: 270
		public bool changeNumber;

		// Token: 0x0400010F RID: 271
		public bool enableNumber = true;

		// Token: 0x04000110 RID: 272
		public TextSettings numberSettings = new TextSettings(0f);

		// Token: 0x04000111 RID: 273
		public DigitSettings digitSettings = new DigitSettings(0f);

		// Token: 0x04000112 RID: 274
		public bool changeLeftText;

		// Token: 0x04000113 RID: 275
		public bool enableLeftText = true;

		// Token: 0x04000114 RID: 276
		public string leftText;

		// Token: 0x04000115 RID: 277
		public TextSettings leftTextSettings = new TextSettings(0f);

		// Token: 0x04000116 RID: 278
		public bool changeRightText;

		// Token: 0x04000117 RID: 279
		public bool enableRightText = true;

		// Token: 0x04000118 RID: 280
		public string rightText;

		// Token: 0x04000119 RID: 281
		public TextSettings rightTextSettings = new TextSettings(0f);

		// Token: 0x0400011A RID: 282
		public bool hideVerticalTexts;

		// Token: 0x0400011B RID: 283
		public bool changeFadeIn;

		// Token: 0x0400011C RID: 284
		public float durationFadeIn = 0.2f;

		// Token: 0x0400011D RID: 285
		public bool enableOffsetFadeIn = true;

		// Token: 0x0400011E RID: 286
		[Tooltip("TextA and TextB move together from this offset.")]
		public Vector2 offsetFadeIn = new Vector2(0.5f, 0f);

		// Token: 0x0400011F RID: 287
		public bool enableScaleFadeIn = true;

		// Token: 0x04000120 RID: 288
		[Tooltip("Scales in from this scale.")]
		public Vector2 scaleFadeIn = new Vector2(2f, 2f);

		// Token: 0x04000121 RID: 289
		public bool enableCrossScaleFadeIn;

		// Token: 0x04000122 RID: 290
		[Tooltip("Scales TextA in from this scale and TextB from the inverse of this scale.")]
		public Vector2 crossScaleFadeIn = new Vector2(1f, 1.5f);

		// Token: 0x04000123 RID: 291
		public bool enableShakeFadeIn;

		// Token: 0x04000124 RID: 292
		[Tooltip("Shakes in from this offset.")]
		public Vector2 shakeOffsetFadeIn = new Vector2(0f, 1.5f);

		// Token: 0x04000125 RID: 293
		[Tooltip("Shakes in at this frequency.")]
		public float shakeFrequencyFadeIn = 4f;

		// Token: 0x04000126 RID: 294
		public bool changeFadeOut;

		// Token: 0x04000127 RID: 295
		public float durationFadeOut = 0.2f;

		// Token: 0x04000128 RID: 296
		public bool enableOffsetFadeOut = true;

		// Token: 0x04000129 RID: 297
		[Tooltip("TextA and TextB move apart to this offset.")]
		public Vector2 offsetFadeOut = new Vector2(0.5f, 0f);

		// Token: 0x0400012A RID: 298
		public bool enableScaleFadeOut;

		// Token: 0x0400012B RID: 299
		[Tooltip("Scales out to this scale.")]
		public Vector2 scaleFadeOut = new Vector2(2f, 2f);

		// Token: 0x0400012C RID: 300
		public bool enableCrossScaleFadeOut;

		// Token: 0x0400012D RID: 301
		[Tooltip("Scales TextA out to this scale and TextB to the inverse of this scale.")]
		public Vector2 crossScaleFadeOut = new Vector2(1f, 1.5f);

		// Token: 0x0400012E RID: 302
		public bool enableShakeFadeOut;

		// Token: 0x0400012F RID: 303
		[Tooltip("Shakes out to this offset.")]
		public Vector2 shakeOffsetFadeOut = new Vector2(0f, 1.5f);

		// Token: 0x04000130 RID: 304
		[Tooltip("Shakes out at this frequency.")]
		public float shakeFrequencyFadeOut = 4f;

		// Token: 0x04000131 RID: 305
		public bool changeMovement;

		// Token: 0x04000132 RID: 306
		public bool enableLerp = true;

		// Token: 0x04000133 RID: 307
		public LerpSettings lerpSettings = new LerpSettings(0);

		// Token: 0x04000134 RID: 308
		public bool enableVelocity;

		// Token: 0x04000135 RID: 309
		public VelocitySettings velocitySettings = new VelocitySettings(0f);

		// Token: 0x04000136 RID: 310
		public bool enableShaking;

		// Token: 0x04000137 RID: 311
		[Tooltip("Shake settings during idle.")]
		public ShakeSettings shakeSettings = new ShakeSettings(new Vector2(0.005f, 0.005f));

		// Token: 0x04000138 RID: 312
		public bool enableFollowing;

		// Token: 0x04000139 RID: 313
		public FollowSettings followSettings = new FollowSettings(0f);

		// Token: 0x0400013A RID: 314
		public bool changeRotation;

		// Token: 0x0400013B RID: 315
		public bool enableStartRotation;

		// Token: 0x0400013C RID: 316
		[Tooltip("The minimum z-angle for the random spawn rotation.")]
		public float minRotation = -4f;

		// Token: 0x0400013D RID: 317
		[Tooltip("The maximum z-angle for the random spawn rotation.")]
		public float maxRotation = 4f;

		// Token: 0x0400013E RID: 318
		public bool enableRotateOverTime;

		// Token: 0x0400013F RID: 319
		[Tooltip("The minimum rotation speed for the z-angle.")]
		public float minRotationSpeed = -15f;

		// Token: 0x04000140 RID: 320
		[Tooltip("The maximum rotation speed for the z-angle.")]
		public float maxRotationSpeed = 15f;

		// Token: 0x04000141 RID: 321
		[Tooltip("Defines rotation speed over lifetime.")]
		public AnimationCurve rotateOverTime = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(0.4f, 1f),
			new Keyframe(0.8f, 0f),
			new Keyframe(1f, 0f)
		});

		// Token: 0x04000142 RID: 322
		public bool changeScaling;

		// Token: 0x04000143 RID: 323
		public bool enableScaleByNumber;

		// Token: 0x04000144 RID: 324
		public ScaleByNumberSettings scaleByNumberSettings = new ScaleByNumberSettings(0f);

		// Token: 0x04000145 RID: 325
		public bool enableScaleOverTime;

		// Token: 0x04000146 RID: 326
		[Tooltip("Will scale over it's lifetime using this curve.")]
		public AnimationCurve scaleOverTime = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 0.7f)
		});

		// Token: 0x04000147 RID: 327
		public bool changeSpamControl;

		// Token: 0x04000148 RID: 328
		public string spamGroup = "";

		// Token: 0x04000149 RID: 329
		public bool enableCombination;

		// Token: 0x0400014A RID: 330
		public CombinationSettings combinationSettings = new CombinationSettings(0f);

		// Token: 0x0400014B RID: 331
		public bool enableDestruction;

		// Token: 0x0400014C RID: 332
		public DestructionSettings destructionSettings = new DestructionSettings(0f);

		// Token: 0x0400014D RID: 333
		public bool enableCollision;

		// Token: 0x0400014E RID: 334
		public CollisionSettings collisionSettings = new CollisionSettings(0f);

		// Token: 0x0400014F RID: 335
		public bool enablePush;

		// Token: 0x04000150 RID: 336
		public PushSettings pushSettings = new PushSettings(0f);
	}
}
