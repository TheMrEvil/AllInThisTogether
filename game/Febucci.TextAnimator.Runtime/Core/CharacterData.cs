using System;
using UnityEngine;

namespace Febucci.UI.Core
{
	// Token: 0x02000039 RID: 57
	public struct CharacterData
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000680C File Offset: 0x00004A0C
		public void ResetInfo(int i)
		{
			this.index = i;
			this.wordIndex = -1;
			this.isVisible = true;
			if (!this.info.initialized)
			{
				this.source.positions = new Vector3[4];
				this.source.colors = new Color32[4];
				this.current.positions = new Vector3[4];
				this.current.colors = new Color32[4];
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006880 File Offset: 0x00004A80
		public void ResetAnimation()
		{
			for (int i = 0; i < this.source.positions.Length; i++)
			{
				this.current.positions[i] = this.source.positions[i];
				this.current.colors[i] = this.source.colors[i];
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000068EC File Offset: 0x00004AEC
		public void Hide()
		{
			byte b = 0;
			while ((int)b < this.source.positions.Length)
			{
				this.current.positions[(int)b] = Vector3.zero;
				b += 1;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006928 File Offset: 0x00004B28
		public void UpdateIntensity(float referenceFontSize)
		{
			this.uniformIntensity = this.info.pointSize / referenceFontSize;
		}

		// Token: 0x040000DD RID: 221
		public CharInfo info;

		// Token: 0x040000DE RID: 222
		public int index;

		// Token: 0x040000DF RID: 223
		public int wordIndex;

		// Token: 0x040000E0 RID: 224
		public bool isVisible;

		// Token: 0x040000E1 RID: 225
		public float passedTime;

		// Token: 0x040000E2 RID: 226
		public float uniformIntensity;

		// Token: 0x040000E3 RID: 227
		public MeshData source;

		// Token: 0x040000E4 RID: 228
		public MeshData current;
	}
}
