using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000304 RID: 772
	internal class GradientSettingsAtlas : IDisposable
	{
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x00068310 File Offset: 0x00066510
		internal int length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x00068328 File Offset: 0x00066528
		// (set) Token: 0x06001984 RID: 6532 RVA: 0x00068330 File Offset: 0x00066530
		private protected bool disposed
		{
			[CompilerGenerated]
			protected get
			{
				return this.<disposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<disposed>k__BackingField = value;
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00068339 File Offset: 0x00066539
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0006834C File Offset: 0x0006654C
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_Atlas);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00068383 File Offset: 0x00066583
		public GradientSettingsAtlas(int length = 4096)
		{
			this.m_Length = length;
			this.m_ElemWidth = 3;
			this.Reset();
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000683A4 File Offset: 0x000665A4
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator = new BestFitAllocator((uint)this.m_Length);
				UIRUtility.Destroy(this.m_Atlas);
				this.m_RawAtlas = default(GradientSettingsAtlas.RawTexture);
				this.MustCommit = false;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x000683F8 File Offset: 0x000665F8
		public Texture2D atlas
		{
			get
			{
				return this.m_Atlas;
			}
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00068410 File Offset: 0x00066610
		public Alloc Add(int count)
		{
			Debug.Assert(count > 0);
			bool disposed = this.disposed;
			Alloc result;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				result = default(Alloc);
			}
			else
			{
				Alloc alloc = this.m_Allocator.Allocate((uint)count);
				result = alloc;
			}
			return result;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00068458 File Offset: 0x00066658
		public void Remove(Alloc alloc)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Allocator.Free(alloc);
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00068488 File Offset: 0x00066688
		public void Write(Alloc alloc, GradientSettings[] settings, GradientRemap remap)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_RawAtlas.rgba == null;
				if (flag)
				{
					this.m_RawAtlas = new GradientSettingsAtlas.RawTexture
					{
						rgba = new Color32[this.m_ElemWidth * this.m_Length],
						width = this.m_ElemWidth,
						height = this.m_Length
					};
					int num = this.m_ElemWidth * this.m_Length;
					for (int i = 0; i < num; i++)
					{
						this.m_RawAtlas.rgba[i] = Color.black;
					}
				}
				GradientSettingsAtlas.s_MarkerWrite.Begin();
				int num2 = (int)alloc.start;
				int j = 0;
				int num3 = settings.Length;
				while (j < num3)
				{
					int num4 = 0;
					GradientSettings gradientSettings = settings[j];
					Debug.Assert(remap == null || num2 == remap.destIndex);
					bool flag2 = gradientSettings.gradientType == GradientType.Radial;
					if (flag2)
					{
						Vector2 vector = gradientSettings.radialFocus;
						vector += Vector2.one;
						vector /= 2f;
						vector.y = 1f - vector.y;
						this.m_RawAtlas.WriteRawFloat4Packed(0.003921569f, (float)gradientSettings.addressMode / 255f, vector.x, vector.y, num4++, num2);
					}
					else
					{
						bool flag3 = gradientSettings.gradientType == GradientType.Linear;
						if (flag3)
						{
							this.m_RawAtlas.WriteRawFloat4Packed(0f, (float)gradientSettings.addressMode / 255f, 0f, 0f, num4++, num2);
						}
					}
					Vector2Int vector2Int = new Vector2Int(gradientSettings.location.x, gradientSettings.location.y);
					Vector2 vector2 = new Vector2((float)(gradientSettings.location.width - 1), (float)(gradientSettings.location.height - 1));
					bool flag4 = remap != null;
					if (flag4)
					{
						vector2Int = new Vector2Int(remap.location.x, remap.location.y);
						vector2 = new Vector2((float)(remap.location.width - 1), (float)(remap.location.height - 1));
					}
					this.m_RawAtlas.WriteRawInt2Packed(vector2Int.x, vector2Int.y, num4++, num2);
					this.m_RawAtlas.WriteRawInt2Packed((int)vector2.x, (int)vector2.y, num4++, num2);
					remap = ((remap != null) ? remap.next : null);
					num2++;
					j++;
				}
				this.MustCommit = true;
				GradientSettingsAtlas.s_MarkerWrite.End();
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x0006875F File Offset: 0x0006695F
		// (set) Token: 0x0600198E RID: 6542 RVA: 0x00068767 File Offset: 0x00066967
		public bool MustCommit
		{
			[CompilerGenerated]
			get
			{
				return this.<MustCommit>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MustCommit>k__BackingField = value;
			}
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00068770 File Offset: 0x00066970
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = !this.MustCommit;
				if (!flag)
				{
					this.PrepareAtlas();
					GradientSettingsAtlas.s_MarkerCommit.Begin();
					this.m_Atlas.SetPixels32(this.m_RawAtlas.rgba);
					this.m_Atlas.Apply();
					GradientSettingsAtlas.s_MarkerCommit.End();
					this.MustCommit = false;
				}
			}
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000687EC File Offset: 0x000669EC
		private void PrepareAtlas()
		{
			bool flag = this.m_Atlas != null;
			if (!flag)
			{
				this.m_Atlas = new Texture2D(this.m_ElemWidth, this.m_Length, TextureFormat.ARGB32, 0, true)
				{
					hideFlags = HideFlags.HideAndDontSave,
					name = "GradientSettings " + GradientSettingsAtlas.s_TextureCounter++.ToString(),
					filterMode = FilterMode.Point
				};
			}
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0006885D File Offset: 0x00066A5D
		// Note: this type is marked as 'beforefieldinit'.
		static GradientSettingsAtlas()
		{
		}

		// Token: 0x04000B06 RID: 2822
		private static ProfilerMarker s_MarkerWrite = new ProfilerMarker("UIR.GradientSettingsAtlas.Write");

		// Token: 0x04000B07 RID: 2823
		private static ProfilerMarker s_MarkerCommit = new ProfilerMarker("UIR.GradientSettingsAtlas.Commit");

		// Token: 0x04000B08 RID: 2824
		private readonly int m_Length;

		// Token: 0x04000B09 RID: 2825
		private readonly int m_ElemWidth;

		// Token: 0x04000B0A RID: 2826
		private BestFitAllocator m_Allocator;

		// Token: 0x04000B0B RID: 2827
		private Texture2D m_Atlas;

		// Token: 0x04000B0C RID: 2828
		private GradientSettingsAtlas.RawTexture m_RawAtlas;

		// Token: 0x04000B0D RID: 2829
		private static int s_TextureCounter;

		// Token: 0x04000B0E RID: 2830
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposed>k__BackingField;

		// Token: 0x04000B0F RID: 2831
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <MustCommit>k__BackingField;

		// Token: 0x02000305 RID: 773
		private struct RawTexture
		{
			// Token: 0x06001992 RID: 6546 RVA: 0x00068880 File Offset: 0x00066A80
			public void WriteRawInt2Packed(int v0, int v1, int destX, int destY)
			{
				byte b = (byte)(v0 / 255);
				byte g = (byte)(v0 - (int)(b * byte.MaxValue));
				byte b2 = (byte)(v1 / 255);
				byte a = (byte)(v1 - (int)(b2 * byte.MaxValue));
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(b, g, b2, a);
			}

			// Token: 0x06001993 RID: 6547 RVA: 0x000688DC File Offset: 0x00066ADC
			public void WriteRawFloat4Packed(float f0, float f1, float f2, float f3, int destX, int destY)
			{
				byte r = (byte)(f0 * 255f + 0.5f);
				byte g = (byte)(f1 * 255f + 0.5f);
				byte b = (byte)(f2 * 255f + 0.5f);
				byte a = (byte)(f3 * 255f + 0.5f);
				int num = destY * this.width + destX;
				this.rgba[num] = new Color32(r, g, b, a);
			}

			// Token: 0x04000B10 RID: 2832
			public Color32[] rgba;

			// Token: 0x04000B11 RID: 2833
			public int width;

			// Token: 0x04000B12 RID: 2834
			public int height;
		}
	}
}
