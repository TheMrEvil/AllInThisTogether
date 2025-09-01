using System;

namespace UnityEngine.Rendering.LookDev
{
	// Token: 0x020000EA RID: 234
	public class StageRuntimeInterface
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x0001E84C File Offset: 0x0001CA4C
		public StageRuntimeInterface(Func<bool, GameObject> AddGameObject, Func<Camera> GetCamera, Func<Light> GetSunLight)
		{
			this.m_AddGameObject = AddGameObject;
			this.m_GetCamera = GetCamera;
			this.m_GetSunLight = GetSunLight;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001E869 File Offset: 0x0001CA69
		public GameObject AddGameObject(bool persistent = false)
		{
			Func<bool, GameObject> addGameObject = this.m_AddGameObject;
			if (addGameObject == null)
			{
				return null;
			}
			return addGameObject(persistent);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001E87D File Offset: 0x0001CA7D
		public Camera camera
		{
			get
			{
				Func<Camera> getCamera = this.m_GetCamera;
				if (getCamera == null)
				{
					return null;
				}
				return getCamera();
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001E890 File Offset: 0x0001CA90
		public Light sunLight
		{
			get
			{
				Func<Light> getSunLight = this.m_GetSunLight;
				if (getSunLight == null)
				{
					return null;
				}
				return getSunLight();
			}
		}

		// Token: 0x040003D1 RID: 977
		private Func<bool, GameObject> m_AddGameObject;

		// Token: 0x040003D2 RID: 978
		private Func<Camera> m_GetCamera;

		// Token: 0x040003D3 RID: 979
		private Func<Light> m_GetSunLight;

		// Token: 0x040003D4 RID: 980
		public object SRPData;
	}
}
