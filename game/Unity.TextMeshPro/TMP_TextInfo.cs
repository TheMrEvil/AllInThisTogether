using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public class TMP_TextInfo
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x000353EC File Offset: 0x000335EC
		public TMP_TextInfo()
		{
			this.characterInfo = new TMP_CharacterInfo[8];
			this.wordInfo = new TMP_WordInfo[16];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00035448 File Offset: 0x00033648
		internal TMP_TextInfo(int characterCount)
		{
			this.characterInfo = new TMP_CharacterInfo[characterCount];
			this.wordInfo = new TMP_WordInfo[16];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000354A4 File Offset: 0x000336A4
		public TMP_TextInfo(TMP_Text textComponent)
		{
			this.textComponent = textComponent;
			this.characterInfo = new TMP_CharacterInfo[8];
			this.wordInfo = new TMP_WordInfo[4];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
			this.meshInfo[0].mesh = textComponent.mesh;
			this.materialCount = 1;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00035524 File Offset: 0x00033724
		public void Clear()
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

		// Token: 0x0600056E RID: 1390 RVA: 0x00035588 File Offset: 0x00033788
		internal void ClearAllData()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.pageCount = 0;
			this.spriteCount = 0;
			this.characterInfo = new TMP_CharacterInfo[4];
			this.wordInfo = new TMP_WordInfo[1];
			this.lineInfo = new TMP_LineInfo[1];
			this.pageInfo = new TMP_PageInfo[1];
			this.linkInfo = new TMP_LinkInfo[0];
			this.materialCount = 0;
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00035618 File Offset: 0x00033818
		public void ClearMeshInfo(bool updateMesh)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(updateMesh);
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0003564C File Offset: 0x0003384C
		public void ClearAllMeshInfo()
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(true);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00035680 File Offset: 0x00033880
		public void ResetVertexLayout(bool isVolumetric)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].ResizeMeshInfo(0, isVolumetric);
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000356B4 File Offset: 0x000338B4
		public void ClearUnusedVertices(MaterialReference[] materials)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				int startIndex = 0;
				this.meshInfo[i].ClearUnusedVertices(startIndex);
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000356E8 File Offset: 0x000338E8
		public void ClearLineInfo()
		{
			if (this.lineInfo == null)
			{
				this.lineInfo = new TMP_LineInfo[2];
			}
			int num = this.lineInfo.Length;
			for (int i = 0; i < num; i++)
			{
				this.lineInfo[i].characterCount = 0;
				this.lineInfo[i].spaceCount = 0;
				this.lineInfo[i].wordCount = 0;
				this.lineInfo[i].controlCharacterCount = 0;
				this.lineInfo[i].width = 0f;
				this.lineInfo[i].ascender = TMP_TextInfo.k_InfinityVectorNegative.x;
				this.lineInfo[i].descender = TMP_TextInfo.k_InfinityVectorPositive.x;
				this.lineInfo[i].marginLeft = 0f;
				this.lineInfo[i].marginRight = 0f;
				this.lineInfo[i].lineExtents.min = TMP_TextInfo.k_InfinityVectorPositive;
				this.lineInfo[i].lineExtents.max = TMP_TextInfo.k_InfinityVectorNegative;
				this.lineInfo[i].maxAdvance = 0f;
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00035830 File Offset: 0x00033A30
		internal void ClearPageInfo()
		{
			if (this.pageInfo == null)
			{
				this.pageInfo = new TMP_PageInfo[2];
			}
			int num = this.pageInfo.Length;
			for (int i = 0; i < num; i++)
			{
				this.pageInfo[i].firstCharacterIndex = 0;
				this.pageInfo[i].lastCharacterIndex = 0;
				this.pageInfo[i].ascender = -32767f;
				this.pageInfo[i].baseLine = 0f;
				this.pageInfo[i].descender = 32767f;
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000358CC File Offset: 0x00033ACC
		public TMP_MeshInfo[] CopyMeshInfoVertexData()
		{
			if (this.m_CachedMeshInfo == null || this.m_CachedMeshInfo.Length != this.meshInfo.Length)
			{
				this.m_CachedMeshInfo = new TMP_MeshInfo[this.meshInfo.Length];
				for (int i = 0; i < this.m_CachedMeshInfo.Length; i++)
				{
					int num = this.meshInfo[i].vertices.Length;
					this.m_CachedMeshInfo[i].vertices = new Vector3[num];
					this.m_CachedMeshInfo[i].uvs0 = new Vector2[num];
					this.m_CachedMeshInfo[i].uvs2 = new Vector2[num];
					this.m_CachedMeshInfo[i].colors32 = new Color32[num];
				}
			}
			for (int j = 0; j < this.m_CachedMeshInfo.Length; j++)
			{
				int num2 = this.meshInfo[j].vertices.Length;
				if (this.m_CachedMeshInfo[j].vertices.Length != num2)
				{
					this.m_CachedMeshInfo[j].vertices = new Vector3[num2];
					this.m_CachedMeshInfo[j].uvs0 = new Vector2[num2];
					this.m_CachedMeshInfo[j].uvs2 = new Vector2[num2];
					this.m_CachedMeshInfo[j].colors32 = new Color32[num2];
				}
				Array.Copy(this.meshInfo[j].vertices, this.m_CachedMeshInfo[j].vertices, num2);
				Array.Copy(this.meshInfo[j].uvs0, this.m_CachedMeshInfo[j].uvs0, num2);
				Array.Copy(this.meshInfo[j].uvs2, this.m_CachedMeshInfo[j].uvs2, num2);
				Array.Copy(this.meshInfo[j].colors32, this.m_CachedMeshInfo[j].colors32, num2);
			}
			return this.m_CachedMeshInfo;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00035AD4 File Offset: 0x00033CD4
		public static void Resize<T>(ref T[] array, int size)
		{
			int newSize = (size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size);
			Array.Resize<T>(ref array, newSize);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00035B00 File Offset: 0x00033D00
		public static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
		{
			if (isBlockAllocated)
			{
				size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			}
			if (size == array.Length)
			{
				return;
			}
			Array.Resize<T>(ref array, size);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00035B2D File Offset: 0x00033D2D
		// Note: this type is marked as 'beforefieldinit'.
		static TMP_TextInfo()
		{
		}

		// Token: 0x0400052B RID: 1323
		internal static Vector2 k_InfinityVectorPositive = new Vector2(32767f, 32767f);

		// Token: 0x0400052C RID: 1324
		internal static Vector2 k_InfinityVectorNegative = new Vector2(-32767f, -32767f);

		// Token: 0x0400052D RID: 1325
		public TMP_Text textComponent;

		// Token: 0x0400052E RID: 1326
		public int characterCount;

		// Token: 0x0400052F RID: 1327
		public int spriteCount;

		// Token: 0x04000530 RID: 1328
		public int spaceCount;

		// Token: 0x04000531 RID: 1329
		public int wordCount;

		// Token: 0x04000532 RID: 1330
		public int linkCount;

		// Token: 0x04000533 RID: 1331
		public int lineCount;

		// Token: 0x04000534 RID: 1332
		public int pageCount;

		// Token: 0x04000535 RID: 1333
		public int materialCount;

		// Token: 0x04000536 RID: 1334
		public TMP_CharacterInfo[] characterInfo;

		// Token: 0x04000537 RID: 1335
		public TMP_WordInfo[] wordInfo;

		// Token: 0x04000538 RID: 1336
		public TMP_LinkInfo[] linkInfo;

		// Token: 0x04000539 RID: 1337
		public TMP_LineInfo[] lineInfo;

		// Token: 0x0400053A RID: 1338
		public TMP_PageInfo[] pageInfo;

		// Token: 0x0400053B RID: 1339
		public TMP_MeshInfo[] meshInfo;

		// Token: 0x0400053C RID: 1340
		private TMP_MeshInfo[] m_CachedMeshInfo;
	}
}
