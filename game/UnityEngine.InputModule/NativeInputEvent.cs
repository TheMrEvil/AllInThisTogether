using System;
using System.Runtime.InteropServices;

namespace UnityEngineInternal.Input
{
	// Token: 0x02000005 RID: 5
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 20)]
	internal struct NativeInputEvent
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		public NativeInputEvent(NativeInputEventType type, int sizeInBytes, int deviceId, double time)
		{
			this.type = type;
			this.sizeInBytes = (ushort)sizeInBytes;
			this.deviceId = (ushort)deviceId;
			this.eventId = 0;
			this.time = time;
		}

		// Token: 0x0400000C RID: 12
		public const int structSize = 20;

		// Token: 0x0400000D RID: 13
		[FieldOffset(0)]
		public NativeInputEventType type;

		// Token: 0x0400000E RID: 14
		[FieldOffset(4)]
		public ushort sizeInBytes;

		// Token: 0x0400000F RID: 15
		[FieldOffset(6)]
		public ushort deviceId;

		// Token: 0x04000010 RID: 16
		[FieldOffset(8)]
		public double time;

		// Token: 0x04000011 RID: 17
		[FieldOffset(16)]
		public int eventId;
	}
}
