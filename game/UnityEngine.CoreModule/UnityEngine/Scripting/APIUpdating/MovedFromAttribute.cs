using System;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x020002E0 RID: 736
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public class MovedFromAttribute : Attribute
	{
		// Token: 0x06001E08 RID: 7688 RVA: 0x00030DCC File Offset: 0x0002EFCC
		public MovedFromAttribute(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.data.Set(autoUpdateAPI, sourceNamespace, sourceAssembly, sourceClassName);
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00030DE7 File Offset: 0x0002EFE7
		public MovedFromAttribute(string sourceNamespace)
		{
			this.data.Set(true, sourceNamespace, null, null);
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x00030E04 File Offset: 0x0002F004
		internal bool AffectsAPIUpdater
		{
			get
			{
				return !this.data.classHasChanged && !this.data.assemblyHasChanged;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x00030E34 File Offset: 0x0002F034
		public bool IsInDifferentAssembly
		{
			get
			{
				return this.data.assemblyHasChanged;
			}
		}

		// Token: 0x040009DE RID: 2526
		internal MovedFromAttributeData data;
	}
}
