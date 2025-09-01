using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class NookDB : ScriptableObject
{
	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000299 RID: 665 RVA: 0x0001685C File Offset: 0x00014A5C
	public List<NookDB.NookObject> AllObjects
	{
		get
		{
			List<NookDB.NookObject> list = new List<NookDB.NookObject>();
			list.AddRange(this.Furniture);
			list.AddRange(this.SmallItems);
			list.AddRange(this.Furnishings);
			list.AddRange(this.Utility);
			list.AddRange(this.Treasures);
			list.AddRange(this.Knowledge);
			return list;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600029A RID: 666 RVA: 0x000168B6 File Offset: 0x00014AB6
	public static NookDB DB
	{
		get
		{
			if (NookDB._db == null)
			{
				NookDB._db = Resources.Load<NookDB>("NookDB");
			}
			return NookDB._db;
		}
	}

	// Token: 0x0600029B RID: 667 RVA: 0x000168DC File Offset: 0x00014ADC
	public static NookDB.NookObject GetItem(string id)
	{
		return NookDB.DB.AllObjects.Find((NookDB.NookObject x) => x.ID == id);
	}

	// Token: 0x0600029C RID: 668 RVA: 0x00016914 File Offset: 0x00014B14
	public static AudioClip GetPlacementSound(NookItem item)
	{
		foreach (NookDB.NookItemSounds nookItemSounds in NookDB.DB.ItemSounds)
		{
			if (nookItemSounds.Material == item.Material)
			{
				foreach (NookDB.NookItemSounds.Sounds sounds in nookItemSounds.Placements)
				{
					if (sounds.MinSize <= item.Size)
					{
						return sounds.Clips.GetRandomClip(-1);
					}
				}
			}
		}
		return null;
	}

	// Token: 0x0600029D RID: 669 RVA: 0x000169D0 File Offset: 0x00014BD0
	public static bool AllowedDrops()
	{
		return GameStats.GetTomeStat(NookDB.DB.DropRequirement, GameStats.Stat.TomesWon, 0) > 0;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x000169E8 File Offset: 0x00014BE8
	public static List<NookDB.NookObject> GetAvailableDrops()
	{
		List<NookDB.NookObject> list = new List<NookDB.NookObject>();
		foreach (NookDB.NookObject nookObject in NookDB.DB.AllObjects)
		{
			if (nookObject.UnlockedBy == Unlockable.UnlockType.Drop && !UnlockManager.IsNookItemUnlocked(nookObject))
			{
				list.Add(nookObject);
			}
		}
		return list;
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00016A58 File Offset: 0x00014C58
	public NookDB()
	{
	}

	// Token: 0x04000285 RID: 645
	public List<NookDB.NookObject> Furniture;

	// Token: 0x04000286 RID: 646
	public List<NookDB.NookObject> Furnishings;

	// Token: 0x04000287 RID: 647
	public List<NookDB.NookObject> SmallItems;

	// Token: 0x04000288 RID: 648
	public List<NookDB.NookObject> Utility;

	// Token: 0x04000289 RID: 649
	public List<NookDB.NookObject> Treasures;

	// Token: 0x0400028A RID: 650
	public List<NookDB.NookObject> Knowledge;

	// Token: 0x0400028B RID: 651
	public int NookLimit = 75;

	// Token: 0x0400028C RID: 652
	public Material HilightMaterial;

	// Token: 0x0400028D RID: 653
	public Material MovementMaterial;

	// Token: 0x0400028E RID: 654
	public Material InvalidMaterial;

	// Token: 0x0400028F RID: 655
	public AudioClip RemoveSound;

	// Token: 0x04000290 RID: 656
	public List<NookDB.NookItemSounds> ItemSounds;

	// Token: 0x04000291 RID: 657
	public GenreTree DropRequirement;

	// Token: 0x04000292 RID: 658
	private static NookDB _db;

	// Token: 0x02000453 RID: 1107
	[Serializable]
	public class NookObject : Unlockable
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x000C2D3B File Offset: 0x000C0F3B
		public override string GUID
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x000C2D43 File Offset: 0x000C0F43
		public override string UnlockName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x000C2D4B File Offset: 0x000C0F4B
		public override string CategoryName
		{
			get
			{
				return "Nook Item";
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x000C2D52 File Offset: 0x000C0F52
		public override UnlockCategory Type
		{
			get
			{
				return UnlockCategory.NookItem;
			}
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000C2D56 File Offset: 0x000C0F56
		public NookObject()
		{
		}

		// Token: 0x04002209 RID: 8713
		public string Name;

		// Token: 0x0400220A RID: 8714
		public string ID;

		// Token: 0x0400220B RID: 8715
		public Sprite Icon;

		// Token: 0x0400220C RID: 8716
		public GameObject Prefab;
	}

	// Token: 0x02000454 RID: 1108
	[Serializable]
	public class NookItemSounds
	{
		// Token: 0x06002159 RID: 8537 RVA: 0x000C2D5E File Offset: 0x000C0F5E
		public NookItemSounds()
		{
		}

		// Token: 0x0400220D RID: 8717
		public NookItem.ItemMaterial Material;

		// Token: 0x0400220E RID: 8718
		public List<NookDB.NookItemSounds.Sounds> Placements = new List<NookDB.NookItemSounds.Sounds>();

		// Token: 0x020006BA RID: 1722
		[Serializable]
		public class Sounds
		{
			// Token: 0x06002858 RID: 10328 RVA: 0x000D8702 File Offset: 0x000D6902
			public Sounds()
			{
			}

			// Token: 0x04002CCA RID: 11466
			public float MinSize;

			// Token: 0x04002CCB RID: 11467
			public List<AudioClip> Clips = new List<AudioClip>();
		}
	}

	// Token: 0x02000455 RID: 1109
	[CompilerGenerated]
	private sealed class <>c__DisplayClass18_0
	{
		// Token: 0x0600215A RID: 8538 RVA: 0x000C2D71 File Offset: 0x000C0F71
		public <>c__DisplayClass18_0()
		{
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000C2D79 File Offset: 0x000C0F79
		internal bool <GetItem>b__0(NookDB.NookObject x)
		{
			return x.ID == this.id;
		}

		// Token: 0x0400220F RID: 8719
		public string id;
	}
}
