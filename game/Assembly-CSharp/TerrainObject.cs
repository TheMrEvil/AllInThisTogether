using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class TerrainObject : MonoBehaviour
{
	// Token: 0x06000C13 RID: 3091 RVA: 0x0004E6D1 File Offset: 0x0004C8D1
	private void Awake()
	{
		TerrainObject.terrainObjects.Add(base.gameObject);
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x0004E6E4 File Offset: 0x0004C8E4
	public static bool IsTerrainObject(GameObject obj)
	{
		return TerrainObject.terrainObjects.Contains(obj);
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0004E6F1 File Offset: 0x0004C8F1
	private void OnDestroy()
	{
		TerrainObject.terrainObjects.Remove(base.gameObject);
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x0004E704 File Offset: 0x0004C904
	public TerrainObject()
	{
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x0004E70C File Offset: 0x0004C90C
	// Note: this type is marked as 'beforefieldinit'.
	static TerrainObject()
	{
	}

	// Token: 0x040009D0 RID: 2512
	public static HashSet<GameObject> terrainObjects = new HashSet<GameObject>();
}
