using System;

namespace Mono.Net.Security
{
	// Token: 0x02000092 RID: 146
	internal class AsyncWriteRequest : AsyncReadOrWriteRequest
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00006B02 File Offset: 0x00004D02
		public AsyncWriteRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size) : base(parent, sync, buffer, offset, size)
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006B9C File Offset: 0x00004D9C
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			if (base.UserBuffer.Size == 0)
			{
				base.UserResult = base.CurrentSize;
				return AsyncOperationStatus.Complete;
			}
			ValueTuple<int, bool> valueTuple = base.Parent.ProcessWrite(base.UserBuffer);
			int item = valueTuple.Item1;
			bool item2 = valueTuple.Item2;
			if (item < 0)
			{
				base.UserResult = -1;
				return AsyncOperationStatus.Complete;
			}
			base.CurrentSize += item;
			base.UserBuffer.Offset += item;
			base.UserBuffer.Size -= item;
			if (item2)
			{
				return AsyncOperationStatus.Continue;
			}
			base.UserResult = base.CurrentSize;
			return AsyncOperationStatus.Complete;
		}
	}
}
