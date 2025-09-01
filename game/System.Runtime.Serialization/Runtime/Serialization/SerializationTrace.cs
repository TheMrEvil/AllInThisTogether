using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200012A RID: 298
	internal static class SerializationTrace
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0003CB25 File Offset: 0x0003AD25
		internal static SourceSwitch CodeGenerationSwitch
		{
			get
			{
				return SerializationTrace.CodeGenerationTraceSource.Switch;
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0000A8EE File Offset: 0x00008AEE
		internal static void WriteInstruction(int lineNumber, string instruction)
		{
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0000A8EE File Offset: 0x00008AEE
		internal static void TraceInstruction(string instruction)
		{
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0003CB31 File Offset: 0x0003AD31
		private static TraceSource CodeGenerationTraceSource
		{
			[SecuritySafeCritical]
			get
			{
				if (SerializationTrace.codeGen == null)
				{
					SerializationTrace.codeGen = new TraceSource("System.Runtime.Serialization.CodeGeneration");
				}
				return SerializationTrace.codeGen;
			}
		}

		// Token: 0x0400067E RID: 1662
		[SecurityCritical]
		private static TraceSource codeGen;
	}
}
