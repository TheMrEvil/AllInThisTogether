using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000035 RID: 53
	internal class TextInfo
	{
		// Token: 0x0600015A RID: 346 RVA: 0x0001A1B4 File Offset: 0x000183B4
		public TextInfo()
		{
			this.textElementInfo = new TextElementInfo[4];
			this.wordInfo = new WordInfo[1];
			this.lineInfo = new LineInfo[1];
			this.pageInfo = new PageInfo[1];
			this.linkInfo = new LinkInfo[0];
			this.meshInfo = new MeshInfo[1];
			this.materialCount = 0;
			this.isDirty = true;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0001A220 File Offset: 0x00018420
		internal void Clear()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.pageCount = 0;
			this.spriteCount = 0;
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].vertexCount = 0;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0001A28C File Offset: 0x0001848C
		internal void ClearMeshInfo(bool updateMesh)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(updateMesh);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0001A2C4 File Offset: 0x000184C4
		internal void ClearLineInfo()
		{
			bool flag = this.lineInfo == null;
			if (flag)
			{
				this.lineInfo = new LineInfo[1];
			}
			for (int i = 0; i < this.lineInfo.Length; i++)
			{
				this.lineInfo[i].characterCount = 0;
				this.lineInfo[i].spaceCount = 0;
				this.lineInfo[i].wordCount = 0;
				this.lineInfo[i].controlCharacterCount = 0;
				this.lineInfo[i].ascender = TextInfo.s_InfinityVectorNegative.x;
				this.lineInfo[i].baseline = 0f;
				this.lineInfo[i].descender = TextInfo.s_InfinityVectorPositive.x;
				this.lineInfo[i].maxAdvance = 0f;
				this.lineInfo[i].marginLeft = 0f;
				this.lineInfo[i].marginRight = 0f;
				this.lineInfo[i].lineExtents.min = TextInfo.s_InfinityVectorPositive;
				this.lineInfo[i].lineExtents.max = TextInfo.s_InfinityVectorNegative;
				this.lineInfo[i].width = 0f;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0001A42C File Offset: 0x0001862C
		internal static void Resize<T>(ref T[] array, int size)
		{
			int newSize = (size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size);
			Array.Resize<T>(ref array, newSize);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0001A45C File Offset: 0x0001865C
		internal static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
		{
			if (isBlockAllocated)
			{
				size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			}
			bool flag = size == array.Length;
			if (!flag)
			{
				Array.Resize<T>(ref array, size);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0001A49D File Offset: 0x0001869D
		// Note: this type is marked as 'beforefieldinit'.
		static TextInfo()
		{
		}

		// Token: 0x04000261 RID: 609
		private static Vector2 s_InfinityVectorPositive = new Vector2(32767f, 32767f);

		// Token: 0x04000262 RID: 610
		private static Vector2 s_InfinityVectorNegative = new Vector2(-32767f, -32767f);

		// Token: 0x04000263 RID: 611
		public int characterCount;

		// Token: 0x04000264 RID: 612
		public int spriteCount;

		// Token: 0x04000265 RID: 613
		public int spaceCount;

		// Token: 0x04000266 RID: 614
		public int wordCount;

		// Token: 0x04000267 RID: 615
		public int linkCount;

		// Token: 0x04000268 RID: 616
		public int lineCount;

		// Token: 0x04000269 RID: 617
		public int pageCount;

		// Token: 0x0400026A RID: 618
		public int materialCount;

		// Token: 0x0400026B RID: 619
		public TextElementInfo[] textElementInfo;

		// Token: 0x0400026C RID: 620
		public WordInfo[] wordInfo;

		// Token: 0x0400026D RID: 621
		public LinkInfo[] linkInfo;

		// Token: 0x0400026E RID: 622
		public LineInfo[] lineInfo;

		// Token: 0x0400026F RID: 623
		public PageInfo[] pageInfo;

		// Token: 0x04000270 RID: 624
		public MeshInfo[] meshInfo;

		// Token: 0x04000271 RID: 625
		public bool isDirty;
	}
}
