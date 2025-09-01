using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Fluxy
{
	// Token: 0x02000010 RID: 16
	[AddComponentMenu("Physics/FluXY/Target", 800)]
	[ExecutionOrder(9999)]
	public class FluxyTarget : MonoBehaviour
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000068 RID: 104 RVA: 0x00005360 File Offset: 0x00003560
		// (remove) Token: 0x06000069 RID: 105 RVA: 0x00005398 File Offset: 0x00003598
		public event FluxyTarget.SplatCallback OnSplat
		{
			[CompilerGenerated]
			add
			{
				FluxyTarget.SplatCallback splatCallback = this.OnSplat;
				FluxyTarget.SplatCallback splatCallback2;
				do
				{
					splatCallback2 = splatCallback;
					FluxyTarget.SplatCallback value2 = (FluxyTarget.SplatCallback)Delegate.Combine(splatCallback2, value);
					splatCallback = Interlocked.CompareExchange<FluxyTarget.SplatCallback>(ref this.OnSplat, value2, splatCallback2);
				}
				while (splatCallback != splatCallback2);
			}
			[CompilerGenerated]
			remove
			{
				FluxyTarget.SplatCallback splatCallback = this.OnSplat;
				FluxyTarget.SplatCallback splatCallback2;
				do
				{
					splatCallback2 = splatCallback;
					FluxyTarget.SplatCallback value2 = (FluxyTarget.SplatCallback)Delegate.Remove(splatCallback2, value);
					splatCallback = Interlocked.CompareExchange<FluxyTarget.SplatCallback>(ref this.OnSplat, value2, splatCallback2);
				}
				while (splatCallback != splatCallback2);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000053CD File Offset: 0x000035CD
		public Vector3 velocity
		{
			get
			{
				return (base.transform.position - this.oldPosition) / Time.deltaTime;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000053F0 File Offset: 0x000035F0
		public Vector3 angularVelocity
		{
			get
			{
				Quaternion quaternion = base.transform.rotation * Quaternion.Inverse(this.oldRotation);
				return new Vector3(quaternion.x, quaternion.y, quaternion.z) * 2f / Time.deltaTime;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005444 File Offset: 0x00003644
		public void OnEnable()
		{
			this.SetOldState();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000544C File Offset: 0x0000364C
		private void Update()
		{
			if (this.rateOverTime > 0f)
			{
				this.timeAccumulator += Time.deltaTime * this.rateOverTime;
				this.timeSplats = Mathf.FloorToInt(this.timeAccumulator);
				this.timeAccumulator -= (float)this.timeSplats;
				return;
			}
			this.timeAccumulator = 0f;
			this.timeSplats = 0;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000054B7 File Offset: 0x000036B7
		private void LateUpdate()
		{
			this.SetOldState();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000054BF File Offset: 0x000036BF
		private void SetOldState()
		{
			this.oldPosition = base.transform.position;
			this.oldRotation = base.transform.rotation;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000054E4 File Offset: 0x000036E4
		private float GetAspectRatio()
		{
			if (this.densityTexture != null)
			{
				return (float)this.densityTexture.width / (float)this.densityTexture.height;
			}
			if (this.velocityTexture != null)
			{
				return (float)this.velocityTexture.width / (float)this.velocityTexture.height;
			}
			return 1f;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005548 File Offset: 0x00003748
		public virtual void Splat(FluxyContainer container, FluxyStorage.Framebuffer fb, in int tileIndex, in Vector4 rect)
		{
			if (this.splatMaterial != null && base.isActiveAndEnabled)
			{
				Quaternion lhs = Quaternion.Inverse(container.transform.rotation);
				Vector3 lossyScale = base.transform.lossyScale;
				float d = Mathf.Max(Mathf.Max(lossyScale.x, lossyScale.y), lossyScale.z);
				Vector3 vector = this.velocity - container.velocity * container.velocityScale;
				Vector3 a = container.TransformWorldVectorToUVSpace(vector, rect);
				vector = this.angularVelocity;
				float z = container.TransformWorldVectorToUVSpace(vector, rect).z;
				Vector3 angularVelocity = container.angularVelocity;
				float num = z - container.TransformWorldVectorToUVSpace(angularVelocity, rect).z * container.velocityScale;
				float magnitude = a.magnitude;
				if (magnitude > 1E-05f)
				{
					a /= magnitude;
					a *= Mathf.Min(magnitude, this.maxRelativeVelocity);
				}
				Vector4 value = Vector3.Scale(a, this.velocityScale) + this.force;
				value.w = this.velocityWeight;
				num = Mathf.Clamp(num, -this.maxRelativeAngularVelocity, this.maxRelativeAngularVelocity) * this.angularVelocityScale;
				num += this.torque;
				this.splatMaterial.SetInt("_TileIndex", tileIndex);
				this.splatMaterial.SetInt("_SrcBlend", (int)this.srcBlend);
				this.splatMaterial.SetInt("_DstBlend", (int)this.dstBlend);
				this.splatMaterial.SetInt("_BlendOp", (int)this.blendOp);
				this.splatMaterial.SetTexture("_Noise", this.noiseTexture);
				Vector3 v = new Vector3(this.velocityNoise, this.velocityNoiseOffset, this.velocityNoiseTiling);
				Vector3 v2 = new Vector3(this.densityNoise, this.densityNoiseOffset, this.densityNoiseTiling);
				float aspectRatio = this.GetAspectRatio();
				int num2;
				if (this.rateOverDistance > 0f)
				{
					this.distanceAccumulator += Vector3.Distance(base.transform.position, this.oldPosition) * this.rateOverDistance;
					num2 = Mathf.FloorToInt(this.distanceAccumulator);
					this.distanceAccumulator -= (float)num2;
				}
				else
				{
					this.distanceAccumulator = 0f;
					num2 = 0;
				}
				int num3 = this.rateOverSteps + this.timeSplats + num2;
				Color linear = new Color(this.color.r, this.color.g, this.color.b, this.color.a * this.densityWeight);
				if (QualitySettings.activeColorSpace == ColorSpace.Linear)
				{
					linear = linear.linear;
				}
				for (int i = 1; i <= num3; i++)
				{
					float t = (float)i / (float)num3;
					Vector4 b = UnityEngine.Random.insideUnitCircle * this.positionRandomness;
					float num4 = UnityEngine.Random.Range(-this.scaleRandomness, this.scaleRandomness) * 0.5f;
					float num5 = UnityEngine.Random.Range(-this.rotationRandomness, this.rotationRandomness) * 3.1415927f;
					float num6 = this.rotation;
					if (!this.overrideRotation)
					{
						num6 = -(lhs * Quaternion.Lerp(this.oldRotation, base.transform.rotation, t)).eulerAngles.z;
					}
					this.splatMaterial.SetFloat("_SplatRotation", num6 * 0.017453292f + num5);
					Vector2 vector2 = this.scale + new Vector2(num4, num4);
					if (this.scaleWithTransform)
					{
						vector2 *= d;
					}
					Vector4 a2;
					if (this.overridePosition)
					{
						a2 = new Vector4(this.position.x, this.position.y, vector2.x * aspectRatio, vector2.y);
					}
					else
					{
						Vector3 vector3 = Vector3.Lerp(this.oldPosition, base.transform.position, t);
						a2 = container.ProjectTarget(vector3, vector2, aspectRatio, this.scaleWithDistance);
					}
					this.splatMaterial.SetVector("_SplatTransform", a2 + b);
					this.splatMaterial.SetVector("_DensityNoiseParams", v2);
					this.splatMaterial.SetVector("_SplatWeights", linear);
					Graphics.Blit(this.densityTexture, fb.stateA, this.splatMaterial, 0);
					this.splatMaterial.SetVector("_VelocityNoiseParams", v);
					this.splatMaterial.SetFloat("_AngularVelocity", num);
					this.splatMaterial.SetVector("_SplatWeights", value);
					this.splatMaterial.SetTexture("_Velocity", this.velocityTexture);
					Graphics.Blit(this.densityTexture, fb.velocityA, this.splatMaterial, 1);
				}
				FluxyTarget.SplatCallback onSplat = this.OnSplat;
				if (onSplat == null)
				{
					return;
				}
				onSplat(this, container, fb, rect);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005A24 File Offset: 0x00003C24
		public FluxyTarget()
		{
		}

		// Token: 0x04000075 RID: 117
		[Tooltip("Material used to splat this target's velocity and density onto containers.")]
		public Material splatMaterial;

		// Token: 0x04000076 RID: 118
		[Tooltip("Amount of splats per update step.")]
		[Min(0f)]
		[FormerlySerializedAs("temporalSamples")]
		public int rateOverSteps = 1;

		// Token: 0x04000077 RID: 119
		[Tooltip("Amount of splats per second.")]
		[Min(0f)]
		public float rateOverTime;

		// Token: 0x04000078 RID: 120
		[Tooltip("Amount of splats per distance unit.")]
		[Min(0f)]
		public float rateOverDistance;

		// Token: 0x04000079 RID: 121
		[Tooltip("Manually override target splat position.")]
		public bool overridePosition;

		// Token: 0x0400007A RID: 122
		[Tooltip("Coordinate to splat this target at when overriding splat position.")]
		public Vector2 position = Vector2.zero;

		// Token: 0x0400007B RID: 123
		[Tooltip("Randomization applied to the splat position.")]
		[Range(0f, 1f)]
		public float positionRandomness;

		// Token: 0x0400007C RID: 124
		[Tooltip("Manually override target splat rotation.")]
		public bool overrideRotation = true;

		// Token: 0x0400007D RID: 125
		[Tooltip("Rotation of the target's shape when splatted.")]
		public float rotation;

		// Token: 0x0400007E RID: 126
		[Tooltip("Randomization applied to the splat rotation.")]
		[Range(0f, 1f)]
		public float rotationRandomness;

		// Token: 0x0400007F RID: 127
		[Tooltip("Scales splat size based on distance from the target to the container's surface.")]
		public bool scaleWithDistance = true;

		// Token: 0x04000080 RID: 128
		[Tooltip("Scales splat based on maximum transform scale value.")]
		public bool scaleWithTransform;

		// Token: 0x04000081 RID: 129
		[Tooltip("Scale of the target's shape when splatted.")]
		public Vector2 scale = new Vector2(0.1f, 0.1f);

		// Token: 0x04000082 RID: 130
		[Tooltip("Randomization applied to the splat size.")]
		[Range(0f, 1f)]
		[FormerlySerializedAs("sizeRandomness")]
		public float scaleRandomness;

		// Token: 0x04000083 RID: 131
		[Range(0f, 1f)]
		public float velocityWeight = 1f;

		// Token: 0x04000084 RID: 132
		[Tooltip("Texture defining the target's splat shape.")]
		public Texture velocityTexture;

		// Token: 0x04000085 RID: 133
		[Min(0f)]
		[Tooltip("Maximum relative velocity between a container and this target.")]
		public float maxRelativeVelocity = 8f;

		// Token: 0x04000086 RID: 134
		[Tooltip("Local-space scale applied to this target's velocity vector.")]
		public Vector3 velocityScale = Vector3.one;

		// Token: 0x04000087 RID: 135
		[Min(0f)]
		[Tooltip("Maximum relative angular velocity between a container and this target.")]
		public float maxRelativeAngularVelocity = 12f;

		// Token: 0x04000088 RID: 136
		[Tooltip("Scale applied to this target's angular velocity.")]
		public float angularVelocityScale = 1f;

		// Token: 0x04000089 RID: 137
		[Tooltip("Local-space force applied by this target, regardless of its velocity")]
		public Vector3 force = Vector3.zero;

		// Token: 0x0400008A RID: 138
		[Tooltip("Local-space torque applied by this target, regardless of its angular velocity")]
		public float torque;

		// Token: 0x0400008B RID: 139
		[Range(0f, 1f)]
		public float densityWeight = 1f;

		// Token: 0x0400008C RID: 140
		[Tooltip("Texture defining the target's splat shape.")]
		public Texture densityTexture;

		// Token: 0x0400008D RID: 141
		[Tooltip("Blend mode used for source fragments.")]
		public BlendMode srcBlend = BlendMode.SrcAlpha;

		// Token: 0x0400008E RID: 142
		[Tooltip("Blend mode used for destination fragments.")]
		public BlendMode dstBlend = BlendMode.OneMinusSrcAlpha;

		// Token: 0x0400008F RID: 143
		[Tooltip("Blend operation used when splatting density.")]
		public BlendOp blendOp;

		// Token: 0x04000090 RID: 144
		[Tooltip("Color splatted by this target onto the container's density buffer.")]
		public Color color = Color.white;

		// Token: 0x04000091 RID: 145
		[Tooltip("Texture used to generate density and velocity noise.")]
		public Texture noiseTexture;

		// Token: 0x04000092 RID: 146
		[Min(0f)]
		[Tooltip("Amount of scalar noise modulating density.")]
		public float densityNoise;

		// Token: 0x04000093 RID: 147
		[Min(0f)]
		[Tooltip("Non-zero values animate noise by offsetting it.")]
		public float densityNoiseOffset;

		// Token: 0x04000094 RID: 148
		[Min(0f)]
		[Tooltip("Tiling scale of density noise.")]
		public float densityNoiseTiling = 1f;

		// Token: 0x04000095 RID: 149
		[Min(0f)]
		[Tooltip("Amount of curl noise added to velocity.")]
		public float velocityNoise;

		// Token: 0x04000096 RID: 150
		[Min(0f)]
		[Tooltip("Non-zero values animate noise by offsetting it.")]
		public float velocityNoiseOffset;

		// Token: 0x04000097 RID: 151
		[Min(0f)]
		[Tooltip("Tiling scale of velocity noise.")]
		public float velocityNoiseTiling = 1f;

		// Token: 0x04000098 RID: 152
		private Vector3 oldPosition;

		// Token: 0x04000099 RID: 153
		private Quaternion oldRotation;

		// Token: 0x0400009A RID: 154
		private float timeAccumulator;

		// Token: 0x0400009B RID: 155
		private float distanceAccumulator;

		// Token: 0x0400009C RID: 156
		private int timeSplats;

		// Token: 0x0400009D RID: 157
		[CompilerGenerated]
		private FluxyTarget.SplatCallback OnSplat;

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x060000B5 RID: 181
		public delegate void SplatCallback(FluxyTarget target, FluxyContainer container, FluxyStorage.Framebuffer fb, in Vector4 rect);
	}
}
