using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000086 RID: 134
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
	public class GenerateHLSL : Attribute
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x00014340 File Offset: 0x00012540
		public GenerateHLSL(PackingRules rules = PackingRules.Exact, bool needAccessors = true, bool needSetters = false, bool needParamDebug = false, int paramDefinesStart = 1, bool omitStructDeclaration = false, bool containsPackedFields = false, bool generateCBuffer = false, int constantRegister = -1, [CallerFilePath] string sourcePath = null)
		{
			this.sourcePath = sourcePath;
			this.packingRules = rules;
			this.needAccessors = needAccessors;
			this.needSetters = needSetters;
			this.needParamDebug = needParamDebug;
			this.paramDefinesStart = paramDefinesStart;
			this.omitStructDeclaration = omitStructDeclaration;
			this.containsPackedFields = containsPackedFields;
			this.generateCBuffer = generateCBuffer;
			this.constantRegister = constantRegister;
		}

		// Token: 0x040002C1 RID: 705
		public PackingRules packingRules;

		// Token: 0x040002C2 RID: 706
		public bool containsPackedFields;

		// Token: 0x040002C3 RID: 707
		public bool needAccessors;

		// Token: 0x040002C4 RID: 708
		public bool needSetters;

		// Token: 0x040002C5 RID: 709
		public bool needParamDebug;

		// Token: 0x040002C6 RID: 710
		public int paramDefinesStart;

		// Token: 0x040002C7 RID: 711
		public bool omitStructDeclaration;

		// Token: 0x040002C8 RID: 712
		public bool generateCBuffer;

		// Token: 0x040002C9 RID: 713
		public int constantRegister;

		// Token: 0x040002CA RID: 714
		public string sourcePath;
	}
}
