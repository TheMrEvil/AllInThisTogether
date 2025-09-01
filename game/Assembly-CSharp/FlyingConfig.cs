using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class FlyingConfig : MonoBehaviour
{
	// Token: 0x0600097A RID: 2426 RVA: 0x0003F7E0 File Offset: 0x0003D9E0
	private void OnDrawGizmos()
	{
		if (!this.DebugViewArea)
		{
			return;
		}
		Gizmos.color = new Color(0.25f, 1f, 1f, 0.3f);
		Gizmos.DrawCube(this.Offset + Vector3.up * this.gridSize.y / 2f, this.gridSize);
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0003F849 File Offset: 0x0003DA49
	public FlyingConfig()
	{
	}

	// Token: 0x040007D5 RID: 2005
	public int cellSize = 1;

	// Token: 0x040007D6 RID: 2006
	public float minSize = 3f;

	// Token: 0x040007D7 RID: 2007
	public Vector3 gridSize;

	// Token: 0x040007D8 RID: 2008
	public Vector3 Offset;

	// Token: 0x040007D9 RID: 2009
	public Terrain TerrainTest;

	// Token: 0x040007DA RID: 2010
	public bool DebugViewArea;
}
