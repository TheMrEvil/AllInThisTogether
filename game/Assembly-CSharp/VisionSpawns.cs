using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000D3 RID: 211
public class VisionSpawns : MonoBehaviour
{
	// Token: 0x060009A6 RID: 2470 RVA: 0x00040494 File Offset: 0x0003E694
	public void LoadPoints()
	{
		this.points = new List<NavVisionPoint>();
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			this.AddVisionPoint(transform.position);
		}
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x00040500 File Offset: 0x0003E700
	private void AddVisionPoint(Vector3 position)
	{
		NavVisionPoint item = new NavVisionPoint(position, this.mask, 120f);
		this.points.Add(item);
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0004052C File Offset: 0x0003E72C
	public GameObject GetDeletePoint(Vector3 position)
	{
		Transform transform = null;
		float num = 10f;
		foreach (object obj in base.transform)
		{
			Transform transform2 = (Transform)obj;
			float num2 = Vector3.Distance(transform2.position, position);
			if (num2 <= num)
			{
				num = num2;
				transform = transform2;
			}
		}
		if (transform == null)
		{
			return null;
		}
		return transform.gameObject;
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x000405B4 File Offset: 0x0003E7B4
	private void OnDrawGizmos()
	{
		if (this.points == null || !this.SpawnMode)
		{
			return;
		}
		Color blue = Color.blue;
		blue.a = 0.33f;
		Gizmos.color = blue;
		foreach (NavVisionPoint navVisionPoint in this.points)
		{
			Gizmos.DrawSphere(navVisionPoint.Position, 4f);
		}
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00040638 File Offset: 0x0003E838
	[ContextMenu("Clear Vision Points")]
	public void ClearChildren()
	{
		int num = 25000;
		int num2 = 0;
		while (base.transform.childCount > 0 && num2 < num)
		{
			UnityEngine.Object.DestroyImmediate(base.transform.GetChild(0).gameObject);
			num2++;
		}
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0004067C File Offset: 0x0003E87C
	[ContextMenu("Debug Setup")]
	public void SetupSimple()
	{
		Scene_Settings scene_Settings = UnityEngine.Object.FindObjectOfType<Scene_Settings>();
		Terrain terrain = (scene_Settings != null) ? scene_Settings.SceneTerrain : null;
		if (terrain == null)
		{
			Debug.LogError("No terrain was set up in Scene_Settings!");
			return;
		}
		Vector3 size = terrain.terrainData.size;
		int num = 0;
		for (float num2 = terrain.transform.position.x; num2 < size.x / 2f; num2 += 8f)
		{
			for (float num3 = terrain.transform.position.z; num3 < size.z / 2f; num3 += UnityEngine.Random.Range(6.5f, 9.5f))
			{
				RaycastHit raycastHit;
				NavMeshHit navMeshHit;
				if (Physics.Raycast(new Ray(new Vector3(num2 + UnityEngine.Random.Range(-1.5f, 1.5f), 100f, num3), Vector3.down), out raycastHit, 101f, this.mask) && raycastHit.distance >= 75f && NavMesh.SamplePosition(raycastHit.point, out navMeshHit, 2f, -1))
				{
					this.SpawnPt(raycastHit.point + Vector3.up * 1f);
					num++;
				}
			}
		}
		if (num > 0)
		{
			this.LoadPoints();
		}
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x000407C3 File Offset: 0x0003E9C3
	private void SpawnPt(Vector3 pos)
	{
		GameObject gameObject = new GameObject("-");
		gameObject.transform.SetParent(base.transform);
		gameObject.hideFlags = HideFlags.HideInHierarchy;
		gameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x000407F8 File Offset: 0x0003E9F8
	private string ChildInfo()
	{
		return base.transform.childCount.ToString() + " Vision Points";
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x00040822 File Offset: 0x0003EA22
	public VisionSpawns()
	{
	}

	// Token: 0x04000805 RID: 2053
	public bool SpawnMode;

	// Token: 0x04000806 RID: 2054
	public bool Batch;

	// Token: 0x04000807 RID: 2055
	[Header("V to Spawn | E to Destroy - At Mouse Position")]
	public LayerMask mask;

	// Token: 0x04000808 RID: 2056
	[Header("$ChildInfo")]
	public string InfoText = "";

	// Token: 0x04000809 RID: 2057
	[NonSerialized]
	private List<NavVisionPoint> points = new List<NavVisionPoint>();
}
