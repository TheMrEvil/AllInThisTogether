using System;
using UnityEngine;

namespace JesseStiller.TerrainFormerExtension
{
	// Token: 0x0200002B RID: 43
	public class TerrainSetNeighbours : MonoBehaviour
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x000084F4 File Offset: 0x000066F4
		private void Awake()
		{
			base.GetComponent<Terrain>().SetNeighbors(this.leftTerrain, this.topTerrain, this.rightTerrain, this.bottomTerrain);
			UnityEngine.Object.Destroy(this);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000851F File Offset: 0x0000671F
		public void SetNeighbours(Terrain leftTerrain, Terrain topTerrain, Terrain rightTerrain, Terrain bottomTerrain)
		{
			this.leftTerrain = leftTerrain;
			this.topTerrain = topTerrain;
			this.rightTerrain = rightTerrain;
			this.bottomTerrain = bottomTerrain;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000853E File Offset: 0x0000673E
		public TerrainSetNeighbours()
		{
		}

		// Token: 0x0400016F RID: 367
		[SerializeField]
		private Terrain leftTerrain;

		// Token: 0x04000170 RID: 368
		[SerializeField]
		private Terrain topTerrain;

		// Token: 0x04000171 RID: 369
		[SerializeField]
		private Terrain rightTerrain;

		// Token: 0x04000172 RID: 370
		[SerializeField]
		private Terrain bottomTerrain;
	}
}
