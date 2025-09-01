using System;

namespace InControl
{
	// Token: 0x02000024 RID: 36
	public interface IInputControl
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600014B RID: 331
		bool HasChanged { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600014C RID: 332
		bool IsPressed { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600014D RID: 333
		bool WasPressed { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600014E RID: 334
		bool WasReleased { get; }

		// Token: 0x0600014F RID: 335
		void ClearInputState();
	}
}
