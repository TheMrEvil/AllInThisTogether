using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class PickupManager : MonoBehaviour
{
	// Token: 0x060008EF RID: 2287 RVA: 0x0003C9A4 File Offset: 0x0003ABA4
	private void Awake()
	{
		PickupManager.instance = this;
		PickupManager.Pickups = new List<PickupObj>();
		PickupManager.Ghosts = new List<GhostPlayerDisplay>();
		PickupManager.ClosestGhost = null;
		GameDB.SetInstance(this.DB);
		PlayerDB.SetInstance(this.PDB);
		MetaDB.SetInstance(this.MDB);
		LoreDB.SetInstance(this.LDB);
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0003CA00 File Offset: 0x0003AC00
	private void Update()
	{
		for (int i = PickupManager.Pickups.Count - 1; i >= 0; i--)
		{
			PickupManager.Pickups[i].TickUpdate(GameplayManager.deltaTime);
		}
		this.GetClosestGhost();
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0003CA40 File Offset: 0x0003AC40
	private void GetClosestGhost()
	{
		PickupManager.ClosestGhost = null;
		if (PlayerControl.myInstance == null || PlayerControl.myInstance.IsDead)
		{
			return;
		}
		float num = 5f;
		GhostPlayerDisplay closestGhost = null;
		Vector3 position = PlayerControl.myInstance.movement.GetPosition();
		foreach (GhostPlayerDisplay ghostPlayerDisplay in PickupManager.Ghosts)
		{
			float num2 = Vector3.Distance(ghostPlayerDisplay.transform.position, position) + 0.1f;
			if (num2 < num)
			{
				num = num2;
				closestGhost = ghostPlayerDisplay;
			}
		}
		PickupManager.ClosestGhost = closestGhost;
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0003CAF0 File Offset: 0x0003ACF0
	public void TrySpawnPickups(EntityControl control)
	{
		if (PickupManager.Pickups.Count >= 100)
		{
			return;
		}
		float num = 1f;
		List<GameDB.PickupOption> pickupOptions = GameDB.GetPickupOptions(control);
		if (pickupOptions.Count == 0)
		{
			return;
		}
		float num2 = 100f;
		foreach (GameDB.PickupOption pickupOption in pickupOptions)
		{
			float num3 = UnityEngine.Random.Range(0f, num2);
			AIControl aicontrol = control as AIControl;
			if (aicontrol != null && aicontrol.PointValue > 0f)
			{
				num3 /= Mathf.Sqrt((control as AIControl).PointValue);
			}
			if (num3 < num * num2 * (pickupOption.SpawnWeight / 100f))
			{
				int num4 = Mathf.RoundToInt(pickupOption.DropStackCurve.Evaluate(UnityEngine.Random.Range(0f, 1f)));
				for (int i = 0; i < num4; i++)
				{
					this.SpawnPickup(pickupOption, control.display.CenterOfMass.position);
				}
			}
		}
		AudioManager.PlayClipAtPoint(this.SpawnPickupClips.GetRandomClip(-1), control.display.CenterOfMass.position, 1f, UnityEngine.Random.Range(0.95f, 1.05f), 0.95f, 3f, 25f).outputAudioMixerGroup = AudioManager.instance.SFXGroup;
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0003CC58 File Offset: 0x0003AE58
	public void SpawnPickup(GameDB.PickupOption option, Vector3 pos)
	{
		ActionPool.SpawnObject(option.Ref, pos, Quaternion.identity);
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0003CC6C File Offset: 0x0003AE6C
	public static void RegisterPickup(PickupObj obj)
	{
		if (PickupManager.Pickups.Contains(obj))
		{
			return;
		}
		PickupManager.Pickups.Add(obj);
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0003CC87 File Offset: 0x0003AE87
	public static void UnregisterPickup(PickupObj obj)
	{
		PickupManager.Pickups.Remove(obj);
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x0003CC95 File Offset: 0x0003AE95
	public static void RegisterGhost(GhostPlayerDisplay obj)
	{
		if (PickupManager.Ghosts.Contains(obj))
		{
			return;
		}
		PickupManager.Ghosts.Add(obj);
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x0003CCB0 File Offset: 0x0003AEB0
	public static void UnregisterGhost(GhostPlayerDisplay obj)
	{
		PickupManager.Ghosts.Remove(obj);
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x0003CCBE File Offset: 0x0003AEBE
	public PickupManager()
	{
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x0003CCC6 File Offset: 0x0003AEC6
	// Note: this type is marked as 'beforefieldinit'.
	static PickupManager()
	{
	}

	// Token: 0x0400077D RID: 1917
	public static PickupManager instance;

	// Token: 0x0400077E RID: 1918
	public const int PICKUP_DISTANCE = 7;

	// Token: 0x0400077F RID: 1919
	public const int GHOST_DISTANCE = 5;

	// Token: 0x04000780 RID: 1920
	public const int MAX_PICKUPS = 100;

	// Token: 0x04000781 RID: 1921
	public GameDB DB;

	// Token: 0x04000782 RID: 1922
	public PlayerDB PDB;

	// Token: 0x04000783 RID: 1923
	public MetaDB MDB;

	// Token: 0x04000784 RID: 1924
	public LoreDB LDB;

	// Token: 0x04000785 RID: 1925
	public static List<PickupObj> Pickups = new List<PickupObj>();

	// Token: 0x04000786 RID: 1926
	public static List<GhostPlayerDisplay> Ghosts = new List<GhostPlayerDisplay>();

	// Token: 0x04000787 RID: 1927
	public static GhostPlayerDisplay ClosestGhost;

	// Token: 0x04000788 RID: 1928
	public List<AudioClip> SpawnPickupClips;
}
