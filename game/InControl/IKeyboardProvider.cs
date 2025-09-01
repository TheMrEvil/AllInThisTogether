using System;

namespace InControl
{
	// Token: 0x02000041 RID: 65
	public interface IKeyboardProvider
	{
		// Token: 0x0600034D RID: 845
		void Setup();

		// Token: 0x0600034E RID: 846
		void Reset();

		// Token: 0x0600034F RID: 847
		void Update();

		// Token: 0x06000350 RID: 848
		bool AnyKeyIsPressed();

		// Token: 0x06000351 RID: 849
		bool GetKeyIsPressed(Key control);

		// Token: 0x06000352 RID: 850
		string GetNameForKey(Key control);
	}
}
