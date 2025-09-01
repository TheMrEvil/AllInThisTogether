using System;

namespace UnityEngine.Scripting.APIUpdating
{
	// Token: 0x020002DF RID: 735
	internal struct MovedFromAttributeData
	{
		// Token: 0x06001E07 RID: 7687 RVA: 0x00030D74 File Offset: 0x0002EF74
		public void Set(bool autoUpdateAPI, string sourceNamespace = null, string sourceAssembly = null, string sourceClassName = null)
		{
			this.className = sourceClassName;
			this.classHasChanged = (this.className != null);
			this.nameSpace = sourceNamespace;
			this.nameSpaceHasChanged = (this.nameSpace != null);
			this.assembly = sourceAssembly;
			this.assemblyHasChanged = (this.assembly != null);
			this.autoUdpateAPI = autoUpdateAPI;
		}

		// Token: 0x040009D7 RID: 2519
		public string className;

		// Token: 0x040009D8 RID: 2520
		public string nameSpace;

		// Token: 0x040009D9 RID: 2521
		public string assembly;

		// Token: 0x040009DA RID: 2522
		public bool classHasChanged;

		// Token: 0x040009DB RID: 2523
		public bool nameSpaceHasChanged;

		// Token: 0x040009DC RID: 2524
		public bool assemblyHasChanged;

		// Token: 0x040009DD RID: 2525
		public bool autoUdpateAPI;
	}
}
