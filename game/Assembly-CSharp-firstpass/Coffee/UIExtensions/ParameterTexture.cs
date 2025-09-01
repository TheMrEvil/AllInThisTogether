using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Coffee.UIExtensions
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public class ParameterTexture
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x000287B8 File Offset: 0x000269B8
		public ParameterTexture(int channels, int instanceLimit, string propertyName)
		{
			this._propertyName = propertyName;
			this._channels = ((channels - 1) / 4 + 1) * 4;
			this._instanceLimit = ((instanceLimit - 1) / 2 + 1) * 2;
			this._data = new byte[this._channels * this._instanceLimit];
			this._stack = new Stack<int>(this._instanceLimit);
			for (int i = 1; i < this._instanceLimit + 1; i++)
			{
				this._stack.Push(i);
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00028838 File Offset: 0x00026A38
		public void Register(IParameterTexture target)
		{
			this.Initialize();
			if (target.parameterIndex <= 0 && 0 < this._stack.Count)
			{
				target.parameterIndex = this._stack.Pop();
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00028868 File Offset: 0x00026A68
		public void Unregister(IParameterTexture target)
		{
			if (0 < target.parameterIndex)
			{
				this._stack.Push(target.parameterIndex);
				target.parameterIndex = 0;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002888C File Offset: 0x00026A8C
		public void SetData(IParameterTexture target, int channelId, byte value)
		{
			int num = (target.parameterIndex - 1) * this._channels + channelId;
			if (0 < target.parameterIndex && this._data[num] != value)
			{
				this._data[num] = value;
				this._needUpload = true;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000288CF File Offset: 0x00026ACF
		public void SetData(IParameterTexture target, int channelId, float value)
		{
			this.SetData(target, channelId, (byte)(Mathf.Clamp01(value) * 255f));
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000288E6 File Offset: 0x00026AE6
		public void RegisterMaterial(Material mat)
		{
			if (this._propertyId == 0)
			{
				this._propertyId = Shader.PropertyToID(this._propertyName);
			}
			if (mat)
			{
				mat.SetTexture(this._propertyId, this._texture);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0002891B File Offset: 0x00026B1B
		public float GetNormalizedIndex(IParameterTexture target)
		{
			return ((float)target.parameterIndex - 0.5f) / (float)this._instanceLimit;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00028934 File Offset: 0x00026B34
		private void Initialize()
		{
			if (ParameterTexture.updates == null)
			{
				ParameterTexture.updates = new List<Action>();
				Canvas.willRenderCanvases += delegate()
				{
					int count = ParameterTexture.updates.Count;
					for (int i = 0; i < count; i++)
					{
						ParameterTexture.updates[i]();
					}
				};
			}
			if (!this._texture)
			{
				this._texture = new Texture2D(this._channels / 4, this._instanceLimit, TextureFormat.RGBA32, false, false);
				this._texture.filterMode = FilterMode.Point;
				this._texture.wrapMode = TextureWrapMode.Clamp;
				ParameterTexture.updates.Add(new Action(this.UpdateParameterTexture));
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000289CD File Offset: 0x00026BCD
		private void UpdateParameterTexture()
		{
			if (this._needUpload && this._texture)
			{
				this._needUpload = false;
				this._texture.LoadRawTextureData(this._data);
				this._texture.Apply(false, false);
			}
		}

		// Token: 0x04000525 RID: 1317
		private Texture2D _texture;

		// Token: 0x04000526 RID: 1318
		private bool _needUpload;

		// Token: 0x04000527 RID: 1319
		private int _propertyId;

		// Token: 0x04000528 RID: 1320
		private readonly string _propertyName;

		// Token: 0x04000529 RID: 1321
		private readonly int _channels;

		// Token: 0x0400052A RID: 1322
		private readonly int _instanceLimit;

		// Token: 0x0400052B RID: 1323
		private readonly byte[] _data;

		// Token: 0x0400052C RID: 1324
		private readonly Stack<int> _stack;

		// Token: 0x0400052D RID: 1325
		private static List<Action> updates;

		// Token: 0x020001CC RID: 460
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000FA3 RID: 4003 RVA: 0x00063AE8 File Offset: 0x00061CE8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000FA4 RID: 4004 RVA: 0x00063AF4 File Offset: 0x00061CF4
			public <>c()
			{
			}

			// Token: 0x06000FA5 RID: 4005 RVA: 0x00063AFC File Offset: 0x00061CFC
			internal void <Initialize>b__16_0()
			{
				int count = ParameterTexture.updates.Count;
				for (int i = 0; i < count; i++)
				{
					ParameterTexture.updates[i]();
				}
			}

			// Token: 0x04000DF6 RID: 3574
			public static readonly ParameterTexture.<>c <>9 = new ParameterTexture.<>c();

			// Token: 0x04000DF7 RID: 3575
			public static Canvas.WillRenderCanvases <>9__16_0;
		}
	}
}
