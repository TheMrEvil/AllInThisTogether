using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public class TextureCurve : IDisposable
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001C056 File Offset: 0x0001A256
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0001C05E File Offset: 0x0001A25E
		public int length
		{
			[CompilerGenerated]
			get
			{
				return this.<length>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<length>k__BackingField = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		public Keyframe this[int index]
		{
			get
			{
				return this.m_Curve[index];
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001C075 File Offset: 0x0001A275
		public TextureCurve(AnimationCurve baseCurve, float zeroValue, bool loop, in Vector2 bounds) : this(baseCurve.keys, zeroValue, loop, bounds)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001C088 File Offset: 0x0001A288
		public TextureCurve(Keyframe[] keys, float zeroValue, bool loop, in Vector2 bounds)
		{
			this.m_Curve = new AnimationCurve(keys);
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			Vector2 vector = bounds;
			this.m_Range = vector.magnitude;
			this.length = keys.Length;
			this.SetDirty();
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001C0DC File Offset: 0x0001A2DC
		~TextureCurve()
		{
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001C104 File Offset: 0x0001A304
		[Obsolete("Please use Release() instead.")]
		public void Dispose()
		{
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001C106 File Offset: 0x0001A306
		public void Release()
		{
			CoreUtils.Destroy(this.m_Texture);
			this.m_Texture = null;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001C11A File Offset: 0x0001A31A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDirty()
		{
			this.m_IsCurveDirty = true;
			this.m_IsTextureDirty = true;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001C12A File Offset: 0x0001A32A
		private static GraphicsFormat GetTextureFormat()
		{
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16_SFloat, FormatUsage.SetPixels))
			{
				return GraphicsFormat.R16_SFloat;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R8_UNorm, FormatUsage.SetPixels))
			{
				return GraphicsFormat.R8_UNorm;
			}
			return GraphicsFormat.R8G8B8A8_UNorm;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001C148 File Offset: 0x0001A348
		public Texture2D GetTexture()
		{
			if (this.m_Texture == null)
			{
				this.m_Texture = new Texture2D(128, 1, TextureCurve.GetTextureFormat(), TextureCreationFlags.None);
				this.m_Texture.name = "CurveTexture";
				this.m_Texture.hideFlags = HideFlags.HideAndDontSave;
				this.m_Texture.filterMode = FilterMode.Bilinear;
				this.m_Texture.wrapMode = TextureWrapMode.Clamp;
				this.m_IsTextureDirty = true;
			}
			if (this.m_IsTextureDirty)
			{
				Color[] array = new Color[128];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].r = this.Evaluate((float)i * 0.0078125f);
				}
				this.m_Texture.SetPixels(array);
				this.m_Texture.Apply(false, false);
				this.m_IsTextureDirty = false;
			}
			return this.m_Texture;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001C218 File Offset: 0x0001A418
		public float Evaluate(float time)
		{
			if (this.m_IsCurveDirty)
			{
				this.length = this.m_Curve.length;
			}
			if (this.length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || this.length == 1)
			{
				return this.m_Curve.Evaluate(time);
			}
			if (this.m_IsCurveDirty)
			{
				if (this.m_LoopingCurve == null)
				{
					this.m_LoopingCurve = new AnimationCurve();
				}
				Keyframe key = this.m_Curve[this.length - 1];
				key.time -= this.m_Range;
				Keyframe key2 = this.m_Curve[0];
				key2.time += this.m_Range;
				this.m_LoopingCurve.keys = this.m_Curve.keys;
				this.m_LoopingCurve.AddKey(key);
				this.m_LoopingCurve.AddKey(key2);
				this.m_IsCurveDirty = false;
			}
			return this.m_LoopingCurve.Evaluate(time);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001C315 File Offset: 0x0001A515
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int AddKey(float time, float value)
		{
			int num = this.m_Curve.AddKey(time, value);
			if (num > -1)
			{
				this.SetDirty();
			}
			return num;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001C32E File Offset: 0x0001A52E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int MoveKey(int index, in Keyframe key)
		{
			int result = this.m_Curve.MoveKey(index, key);
			this.SetDirty();
			return result;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001C348 File Offset: 0x0001A548
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveKey(int index)
		{
			this.m_Curve.RemoveKey(index);
			this.SetDirty();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001C35C File Offset: 0x0001A55C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SmoothTangents(int index, float weight)
		{
			this.m_Curve.SmoothTangents(index, weight);
			this.SetDirty();
		}

		// Token: 0x04000385 RID: 901
		private const int k_Precision = 128;

		// Token: 0x04000386 RID: 902
		private const float k_Step = 0.0078125f;

		// Token: 0x04000387 RID: 903
		[CompilerGenerated]
		[SerializeField]
		private int <length>k__BackingField;

		// Token: 0x04000388 RID: 904
		[SerializeField]
		private bool m_Loop;

		// Token: 0x04000389 RID: 905
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x0400038A RID: 906
		[SerializeField]
		private float m_Range;

		// Token: 0x0400038B RID: 907
		[SerializeField]
		private AnimationCurve m_Curve;

		// Token: 0x0400038C RID: 908
		private AnimationCurve m_LoopingCurve;

		// Token: 0x0400038D RID: 909
		private Texture2D m_Texture;

		// Token: 0x0400038E RID: 910
		private bool m_IsCurveDirty;

		// Token: 0x0400038F RID: 911
		private bool m_IsTextureDirty;
	}
}
