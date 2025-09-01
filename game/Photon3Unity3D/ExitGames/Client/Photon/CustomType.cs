using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200002B RID: 43
	internal class CustomType
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		public CustomType(Type type, byte code, SerializeMethod serializeFunction, DeserializeMethod deserializeFunction)
		{
			this.Type = type;
			this.Code = code;
			this.SerializeFunction = serializeFunction;
			this.DeserializeFunction = deserializeFunction;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000D2F3 File Offset: 0x0000B4F3
		public CustomType(Type type, byte code, SerializeStreamMethod serializeFunction, DeserializeStreamMethod deserializeFunction)
		{
			this.Type = type;
			this.Code = code;
			this.SerializeStreamFunction = serializeFunction;
			this.DeserializeStreamFunction = deserializeFunction;
		}

		// Token: 0x04000172 RID: 370
		public readonly byte Code;

		// Token: 0x04000173 RID: 371
		public readonly Type Type;

		// Token: 0x04000174 RID: 372
		public readonly SerializeMethod SerializeFunction;

		// Token: 0x04000175 RID: 373
		public readonly DeserializeMethod DeserializeFunction;

		// Token: 0x04000176 RID: 374
		public readonly SerializeStreamMethod SerializeStreamFunction;

		// Token: 0x04000177 RID: 375
		public readonly DeserializeStreamMethod DeserializeStreamFunction;
	}
}
