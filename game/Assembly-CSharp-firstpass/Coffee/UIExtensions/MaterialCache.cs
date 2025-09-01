using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Coffee.UIExtensions
{
	// Token: 0x02000096 RID: 150
	public class MaterialCache
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0002850A File Offset: 0x0002670A
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x00028512 File Offset: 0x00026712
		public ulong hash
		{
			[CompilerGenerated]
			get
			{
				return this.<hash>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<hash>k__BackingField = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0002851B File Offset: 0x0002671B
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00028523 File Offset: 0x00026723
		public int referenceCount
		{
			[CompilerGenerated]
			get
			{
				return this.<referenceCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<referenceCount>k__BackingField = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0002852C File Offset: 0x0002672C
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00028534 File Offset: 0x00026734
		public Texture texture
		{
			[CompilerGenerated]
			get
			{
				return this.<texture>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<texture>k__BackingField = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0002853D File Offset: 0x0002673D
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x00028545 File Offset: 0x00026745
		public Material material
		{
			[CompilerGenerated]
			get
			{
				return this.<material>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<material>k__BackingField = value;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00028550 File Offset: 0x00026750
		public static MaterialCache Register(ulong hash, Texture texture, Func<Material> onCreateMaterial)
		{
			MaterialCache materialCache = MaterialCache.materialCaches.FirstOrDefault((MaterialCache x) => x.hash == hash);
			if (materialCache != null && materialCache.material)
			{
				if (materialCache.material)
				{
					MaterialCache materialCache2 = materialCache;
					int referenceCount = materialCache2.referenceCount;
					materialCache2.referenceCount = referenceCount + 1;
				}
				else
				{
					MaterialCache.materialCaches.Remove(materialCache);
					materialCache = null;
				}
			}
			if (materialCache == null)
			{
				materialCache = new MaterialCache
				{
					hash = hash,
					material = onCreateMaterial(),
					referenceCount = 1
				};
				MaterialCache.materialCaches.Add(materialCache);
			}
			return materialCache;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000285F4 File Offset: 0x000267F4
		public static MaterialCache Register(ulong hash, Func<Material> onCreateMaterial)
		{
			MaterialCache materialCache = MaterialCache.materialCaches.FirstOrDefault((MaterialCache x) => x.hash == hash);
			if (materialCache != null)
			{
				MaterialCache materialCache2 = materialCache;
				int referenceCount = materialCache2.referenceCount;
				materialCache2.referenceCount = referenceCount + 1;
			}
			if (materialCache == null)
			{
				materialCache = new MaterialCache
				{
					hash = hash,
					material = onCreateMaterial(),
					referenceCount = 1
				};
				MaterialCache.materialCaches.Add(materialCache);
			}
			return materialCache;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002866C File Offset: 0x0002686C
		public static void Unregister(MaterialCache cache)
		{
			if (cache == null)
			{
				return;
			}
			int referenceCount = cache.referenceCount;
			cache.referenceCount = referenceCount - 1;
			if (cache.referenceCount <= 0)
			{
				MaterialCache.materialCaches.Remove(cache);
				cache.material = null;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000286A9 File Offset: 0x000268A9
		public MaterialCache()
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000286B1 File Offset: 0x000268B1
		// Note: this type is marked as 'beforefieldinit'.
		static MaterialCache()
		{
		}

		// Token: 0x0400051A RID: 1306
		[CompilerGenerated]
		private ulong <hash>k__BackingField;

		// Token: 0x0400051B RID: 1307
		[CompilerGenerated]
		private int <referenceCount>k__BackingField;

		// Token: 0x0400051C RID: 1308
		[CompilerGenerated]
		private Texture <texture>k__BackingField;

		// Token: 0x0400051D RID: 1309
		[CompilerGenerated]
		private Material <material>k__BackingField;

		// Token: 0x0400051E RID: 1310
		public static List<MaterialCache> materialCaches = new List<MaterialCache>();

		// Token: 0x020001CA RID: 458
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x06000F9F RID: 3999 RVA: 0x00063AB8 File Offset: 0x00061CB8
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x06000FA0 RID: 4000 RVA: 0x00063AC0 File Offset: 0x00061CC0
			internal bool <Register>b__0(MaterialCache x)
			{
				return x.hash == this.hash;
			}

			// Token: 0x04000DF4 RID: 3572
			public ulong hash;
		}

		// Token: 0x020001CB RID: 459
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x06000FA1 RID: 4001 RVA: 0x00063AD0 File Offset: 0x00061CD0
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x06000FA2 RID: 4002 RVA: 0x00063AD8 File Offset: 0x00061CD8
			internal bool <Register>b__0(MaterialCache x)
			{
				return x.hash == this.hash;
			}

			// Token: 0x04000DF5 RID: 3573
			public ulong hash;
		}
	}
}
