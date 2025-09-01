using System;

namespace Mono.Net.Security
{
	// Token: 0x02000091 RID: 145
	internal class AsyncReadRequest : AsyncReadOrWriteRequest
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00006B02 File Offset: 0x00004D02
		public AsyncReadRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size) : base(parent, sync, buffer, offset, size)
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006B14 File Offset: 0x00004D14
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			ValueTuple<int, bool> valueTuple = base.Parent.ProcessRead(base.UserBuffer);
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
			if (item2 && base.CurrentSize == 0)
			{
				return AsyncOperationStatus.Continue;
			}
			base.UserResult = base.CurrentSize;
			return AsyncOperationStatus.Complete;
		}
	}
}
