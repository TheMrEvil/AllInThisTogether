using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000082 RID: 130
	public abstract class RenderPipelineResources : ScriptableObject
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0001432C File Offset: 0x0001252C
		protected virtual string packagePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001432F File Offset: 0x0001252F
		internal string packagePath_Internal
		{
			get
			{
				return this.packagePath;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00014337 File Offset: 0x00012537
		protected RenderPipelineResources()
		{
		}
	}
}
