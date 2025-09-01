using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E8 RID: 744
	[Serializable]
	public struct LoadSceneParameters
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x00031814 File Offset: 0x0002FA14
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x0003182C File Offset: 0x0002FA2C
		public LoadSceneMode loadSceneMode
		{
			get
			{
				return this.m_LoadSceneMode;
			}
			set
			{
				this.m_LoadSceneMode = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x00031838 File Offset: 0x0002FA38
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x00031850 File Offset: 0x0002FA50
		public LocalPhysicsMode localPhysicsMode
		{
			get
			{
				return this.m_LocalPhysicsMode;
			}
			set
			{
				this.m_LocalPhysicsMode = value;
			}
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0003185A File Offset: 0x0002FA5A
		public LoadSceneParameters(LoadSceneMode mode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = LocalPhysicsMode.None;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0003186B File Offset: 0x0002FA6B
		public LoadSceneParameters(LoadSceneMode mode, LocalPhysicsMode physicsMode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = physicsMode;
		}

		// Token: 0x040009F2 RID: 2546
		[SerializeField]
		private LoadSceneMode m_LoadSceneMode;

		// Token: 0x040009F3 RID: 2547
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;
	}
}
