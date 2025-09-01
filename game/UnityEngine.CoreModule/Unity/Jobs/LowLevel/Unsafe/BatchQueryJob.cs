using System;
using Unity.Collections;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000064 RID: 100
	public struct BatchQueryJob<CommandT, ResultT> where CommandT : struct where ResultT : struct
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000036EE File Offset: 0x000018EE
		public BatchQueryJob(NativeArray<CommandT> commands, NativeArray<ResultT> results)
		{
			this.commands = commands;
			this.results = results;
		}

		// Token: 0x04000180 RID: 384
		[ReadOnly]
		internal NativeArray<CommandT> commands;

		// Token: 0x04000181 RID: 385
		internal NativeArray<ResultT> results;
	}
}
