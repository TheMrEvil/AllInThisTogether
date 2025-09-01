using System;

namespace InControl
{
	// Token: 0x02000014 RID: 20
	public class KeyBindingSourceListener : BindingSourceListener
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002A44 File Offset: 0x00000C44
		public void Reset()
		{
			this.detectFound.Clear();
			this.detectPhase = 0;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002A58 File Offset: 0x00000C58
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeKeys)
			{
				return null;
			}
			if (this.detectFound.IncludeCount > 0 && !this.detectFound.IsPressed && this.detectPhase == 2)
			{
				BindingSource result = new KeyBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			KeyCombo keyCombo = KeyCombo.Detect(listenOptions.IncludeModifiersAsFirstClassKeys);
			if (keyCombo.IncludeCount > 0)
			{
				if (this.detectPhase == 1)
				{
					this.detectFound = keyCombo;
					this.detectPhase = 2;
				}
			}
			else if (this.detectPhase == 0)
			{
				this.detectPhase = 1;
			}
			return null;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public KeyBindingSourceListener()
		{
		}

		// Token: 0x040000C6 RID: 198
		private KeyCombo detectFound;

		// Token: 0x040000C7 RID: 199
		private int detectPhase;
	}
}
