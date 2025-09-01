using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QFSW.QC.Utilities
{
	// Token: 0x02000054 RID: 84
	public static class GameObjectExtensions
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00008E6C File Offset: 0x0000706C
		public static GameObject Find(string name, bool includeInactive = false)
		{
			GameObject gameObject;
			if (GameObjectExtensions.GameObjectCache.TryGetValue(name, out gameObject) && gameObject && (gameObject.activeInHierarchy || includeInactive) && gameObject.name == name)
			{
				return gameObject;
			}
			gameObject = GameObject.Find(name);
			if (gameObject)
			{
				GameObject result = GameObjectExtensions.GameObjectCache[name] = gameObject;
				return result;
			}
			if (includeInactive)
			{
				int sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
				for (int i = 0; i < sceneCountInBuildSettings; i++)
				{
					Scene sceneByBuildIndex = SceneManager.GetSceneByBuildIndex(i);
					if (sceneByBuildIndex.isLoaded)
					{
						GameObjectExtensions.RootGameObjectBuffer.Clear();
						sceneByBuildIndex.GetRootGameObjects(GameObjectExtensions.RootGameObjectBuffer);
						foreach (GameObject root in GameObjectExtensions.RootGameObjectBuffer)
						{
							gameObject = GameObjectExtensions.Find(name, root);
							if (gameObject)
							{
								GameObject result = GameObjectExtensions.GameObjectCache[name] = gameObject;
								return result;
							}
						}
					}
				}
				gameObject = (from x in Resources.FindObjectsOfTypeAll<GameObject>()
				where !x.hideFlags.HasFlag(HideFlags.HideInHierarchy)
				select x).FirstOrDefault((GameObject x) => x.name == name);
				if (gameObject)
				{
					return GameObjectExtensions.GameObjectCache[name] = gameObject;
				}
			}
			return null;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009004 File Offset: 0x00007204
		public static GameObject Find(string name, GameObject root)
		{
			if (root.name == name)
			{
				return root;
			}
			for (int i = 0; i < root.transform.childCount; i++)
			{
				GameObject gameObject = GameObjectExtensions.Find(name, root.transform.GetChild(i).gameObject);
				if (gameObject)
				{
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000905A File Offset: 0x0000725A
		// Note: this type is marked as 'beforefieldinit'.
		static GameObjectExtensions()
		{
		}

		// Token: 0x04000135 RID: 309
		private static readonly Dictionary<string, GameObject> GameObjectCache = new Dictionary<string, GameObject>();

		// Token: 0x04000136 RID: 310
		private static readonly List<GameObject> RootGameObjectBuffer = new List<GameObject>();

		// Token: 0x020000A9 RID: 169
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x06000344 RID: 836 RVA: 0x0000C517 File Offset: 0x0000A717
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06000345 RID: 837 RVA: 0x0000C51F File Offset: 0x0000A71F
			internal bool <Find>b__1(GameObject x)
			{
				return x.name == this.name;
			}

			// Token: 0x0400021C RID: 540
			public string name;
		}

		// Token: 0x020000AA RID: 170
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000346 RID: 838 RVA: 0x0000C532 File Offset: 0x0000A732
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000347 RID: 839 RVA: 0x0000C53E File Offset: 0x0000A73E
			public <>c()
			{
			}

			// Token: 0x06000348 RID: 840 RVA: 0x0000C546 File Offset: 0x0000A746
			internal bool <Find>b__2_0(GameObject x)
			{
				return !x.hideFlags.HasFlag(HideFlags.HideInHierarchy);
			}

			// Token: 0x0400021D RID: 541
			public static readonly GameObjectExtensions.<>c <>9 = new GameObjectExtensions.<>c();

			// Token: 0x0400021E RID: 542
			public static Func<GameObject, bool> <>9__2_0;
		}
	}
}
