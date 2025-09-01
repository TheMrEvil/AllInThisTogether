using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000043 RID: 67
	public static class ComponentSingleton<TType> where TType : Component
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000CD14 File Offset: 0x0000AF14
		public static TType instance
		{
			get
			{
				if (ComponentSingleton<TType>.s_Instance == null)
				{
					GameObject gameObject = new GameObject("Default " + typeof(TType).Name);
					gameObject.hideFlags = HideFlags.HideAndDontSave;
					gameObject.SetActive(false);
					ComponentSingleton<TType>.s_Instance = gameObject.AddComponent<TType>();
				}
				return ComponentSingleton<TType>.s_Instance;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000CD6F File Offset: 0x0000AF6F
		public static void Release()
		{
			if (ComponentSingleton<TType>.s_Instance != null)
			{
				CoreUtils.Destroy(ComponentSingleton<TType>.s_Instance.gameObject);
				ComponentSingleton<TType>.s_Instance = default(TType);
			}
		}

		// Token: 0x040001AD RID: 429
		private static TType s_Instance;
	}
}
