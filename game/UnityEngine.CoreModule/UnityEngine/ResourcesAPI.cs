using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001EA RID: 490
	public class ResourcesAPI
	{
		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x000237D5 File Offset: 0x000219D5
		internal static ResourcesAPI ActiveAPI
		{
			get
			{
				return ResourcesAPI.overrideAPI ?? ResourcesAPI.s_DefaultAPI;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x000237E5 File Offset: 0x000219E5
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x000237EC File Offset: 0x000219EC
		public static ResourcesAPI overrideAPI
		{
			[CompilerGenerated]
			get
			{
				return ResourcesAPI.<overrideAPI>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				ResourcesAPI.<overrideAPI>k__BackingField = value;
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00008CBB File Offset: 0x00006EBB
		protected internal ResourcesAPI()
		{
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000237F4 File Offset: 0x000219F4
		protected internal virtual Object[] FindObjectsOfTypeAll(Type systemTypeInstance)
		{
			return ResourcesAPIInternal.FindObjectsOfTypeAll(systemTypeInstance);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000237FC File Offset: 0x000219FC
		protected internal virtual Shader FindShaderByName(string name)
		{
			return ResourcesAPIInternal.FindShaderByName(name);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00023804 File Offset: 0x00021A04
		protected internal virtual Object Load(string path, Type systemTypeInstance)
		{
			return ResourcesAPIInternal.Load(path, systemTypeInstance);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0002380D File Offset: 0x00021A0D
		protected internal virtual Object[] LoadAll(string path, Type systemTypeInstance)
		{
			return ResourcesAPIInternal.LoadAll(path, systemTypeInstance);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00023818 File Offset: 0x00021A18
		protected internal virtual ResourceRequest LoadAsync(string path, Type systemTypeInstance)
		{
			ResourceRequest resourceRequest = ResourcesAPIInternal.LoadAsyncInternal(path, systemTypeInstance);
			resourceRequest.m_Path = path;
			resourceRequest.m_Type = systemTypeInstance;
			return resourceRequest;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00023841 File Offset: 0x00021A41
		protected internal virtual void UnloadAsset(Object assetToUnload)
		{
			ResourcesAPIInternal.UnloadAsset(assetToUnload);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0002384A File Offset: 0x00021A4A
		// Note: this type is marked as 'beforefieldinit'.
		static ResourcesAPI()
		{
		}

		// Token: 0x040007CB RID: 1995
		private static ResourcesAPI s_DefaultAPI = new ResourcesAPI();

		// Token: 0x040007CC RID: 1996
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static ResourcesAPI <overrideAPI>k__BackingField;
	}
}
