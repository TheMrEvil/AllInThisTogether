using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x020002E9 RID: 745
	[Serializable]
	public struct CreateSceneParameters
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0003187C File Offset: 0x0002FA7C
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x00031894 File Offset: 0x0002FA94
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

		// Token: 0x06001E83 RID: 7811 RVA: 0x00031894 File Offset: 0x0002FA94
		public CreateSceneParameters(LocalPhysicsMode physicsMode)
		{
			this.m_LocalPhysicsMode = physicsMode;
		}

		// Token: 0x040009F4 RID: 2548
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;
	}
}
