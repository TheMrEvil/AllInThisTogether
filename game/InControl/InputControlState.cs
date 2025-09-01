using System;

namespace InControl
{
	// Token: 0x02000029 RID: 41
	public struct InputControlState
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00005ABC File Offset: 0x00003CBC
		public void Reset()
		{
			this.State = false;
			this.Value = 0f;
			this.RawValue = 0f;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005ADB File Offset: 0x00003CDB
		public void Set(float value)
		{
			this.Value = value;
			this.State = Utility.IsNotZero(value);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public void Set(float value, float threshold)
		{
			this.Value = value;
			this.State = Utility.AbsoluteIsOverThreshold(value, threshold);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005B06 File Offset: 0x00003D06
		public void Set(bool state)
		{
			this.State = state;
			this.Value = (state ? 1f : 0f);
			this.RawValue = this.Value;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005B30 File Offset: 0x00003D30
		public static implicit operator bool(InputControlState state)
		{
			return state.State;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005B38 File Offset: 0x00003D38
		public static implicit operator float(InputControlState state)
		{
			return state.Value;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005B40 File Offset: 0x00003D40
		public static bool operator ==(InputControlState a, InputControlState b)
		{
			return a.State == b.State && Utility.Approximately(a.Value, b.Value);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005B63 File Offset: 0x00003D63
		public static bool operator !=(InputControlState a, InputControlState b)
		{
			return a.State != b.State || !Utility.Approximately(a.Value, b.Value);
		}

		// Token: 0x04000159 RID: 345
		public bool State;

		// Token: 0x0400015A RID: 346
		public float Value;

		// Token: 0x0400015B RID: 347
		public float RawValue;
	}
}
