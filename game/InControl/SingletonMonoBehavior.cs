using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200006F RID: 111
	public abstract class SingletonMonoBehavior<TComponent> : MonoBehaviour where TComponent : MonoBehaviour
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00012394 File Offset: 0x00010594
		public static TComponent Instance
		{
			get
			{
				object obj = SingletonMonoBehavior<TComponent>.lockObject;
				TComponent result;
				lock (obj)
				{
					if (SingletonMonoBehavior<TComponent>.hasInstance)
					{
						result = SingletonMonoBehavior<TComponent>.instance;
					}
					else
					{
						SingletonMonoBehavior<TComponent>.instance = SingletonMonoBehavior<TComponent>.FindFirstInstance();
						if (SingletonMonoBehavior<TComponent>.instance == null)
						{
							string str = "The instance of singleton component ";
							Type typeFromHandle = typeof(TComponent);
							throw new Exception(str + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + " was requested, but it doesn't appear to exist in the scene.");
						}
						SingletonMonoBehavior<TComponent>.hasInstance = true;
						SingletonMonoBehavior<TComponent>.instanceId = SingletonMonoBehavior<TComponent>.instance.GetInstanceID();
						result = SingletonMonoBehavior<TComponent>.instance;
					}
				}
				return result;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00012444 File Offset: 0x00010644
		protected bool EnforceSingleton
		{
			get
			{
				if (base.GetInstanceID() == SingletonMonoBehavior<TComponent>.Instance.GetInstanceID())
				{
					return false;
				}
				if (Application.isPlaying)
				{
					base.enabled = false;
				}
				return true;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00012470 File Offset: 0x00010670
		protected bool IsTheSingleton
		{
			get
			{
				object obj = SingletonMonoBehavior<TComponent>.lockObject;
				bool result;
				lock (obj)
				{
					result = (base.GetInstanceID() == SingletonMonoBehavior<TComponent>.instanceId);
				}
				return result;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000124B8 File Offset: 0x000106B8
		protected bool IsNotTheSingleton
		{
			get
			{
				object obj = SingletonMonoBehavior<TComponent>.lockObject;
				bool result;
				lock (obj)
				{
					result = (base.GetInstanceID() != SingletonMonoBehavior<TComponent>.instanceId);
				}
				return result;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00012504 File Offset: 0x00010704
		private static TComponent[] FindInstances()
		{
			TComponent[] array = UnityEngine.Object.FindObjectsOfType<TComponent>();
			Array.Sort<TComponent>(array, (TComponent a, TComponent b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
			return array;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00012530 File Offset: 0x00010730
		private static TComponent FindFirstInstance()
		{
			TComponent[] array = SingletonMonoBehavior<TComponent>.FindInstances();
			if (array.Length == 0)
			{
				return default(TComponent);
			}
			return array[0];
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00012558 File Offset: 0x00010758
		protected virtual void Awake()
		{
			if (Application.isPlaying && SingletonMonoBehavior<TComponent>.Instance)
			{
				if (base.GetInstanceID() != SingletonMonoBehavior<TComponent>.instanceId)
				{
					base.enabled = false;
				}
				foreach (TComponent tcomponent in from o in SingletonMonoBehavior<TComponent>.FindInstances()
				where o.GetInstanceID() != SingletonMonoBehavior<TComponent>.instanceId
				select o)
				{
					tcomponent.enabled = false;
				}
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000125F8 File Offset: 0x000107F8
		protected virtual void OnDestroy()
		{
			object obj = SingletonMonoBehavior<TComponent>.lockObject;
			lock (obj)
			{
				if (base.GetInstanceID() == SingletonMonoBehavior<TComponent>.instanceId)
				{
					SingletonMonoBehavior<TComponent>.hasInstance = false;
				}
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00012644 File Offset: 0x00010844
		protected SingletonMonoBehavior()
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001264C File Offset: 0x0001084C
		// Note: this type is marked as 'beforefieldinit'.
		static SingletonMonoBehavior()
		{
		}

		// Token: 0x04000414 RID: 1044
		private static TComponent instance;

		// Token: 0x04000415 RID: 1045
		private static bool hasInstance;

		// Token: 0x04000416 RID: 1046
		private static int instanceId;

		// Token: 0x04000417 RID: 1047
		private static readonly object lockObject = new object();

		// Token: 0x0200021A RID: 538
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000924 RID: 2340 RVA: 0x00052E58 File Offset: 0x00051058
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000925 RID: 2341 RVA: 0x00052E64 File Offset: 0x00051064
			public <>c()
			{
			}

			// Token: 0x06000926 RID: 2342 RVA: 0x00052E6C File Offset: 0x0005106C
			internal int <FindInstances>b__12_0(TComponent a, TComponent b)
			{
				return a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex());
			}

			// Token: 0x06000927 RID: 2343 RVA: 0x00052EA1 File Offset: 0x000510A1
			internal bool <Awake>b__14_0(TComponent o)
			{
				return o.GetInstanceID() != SingletonMonoBehavior<TComponent>.instanceId;
			}

			// Token: 0x0400049B RID: 1179
			public static readonly SingletonMonoBehavior<TComponent>.<>c <>9 = new SingletonMonoBehavior<TComponent>.<>c();

			// Token: 0x0400049C RID: 1180
			public static Comparison<TComponent> <>9__12_0;

			// Token: 0x0400049D RID: 1181
			public static Func<TComponent, bool> <>9__14_0;
		}
	}
}
