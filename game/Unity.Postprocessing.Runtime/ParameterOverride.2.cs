using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public class ParameterOverride<T> : ParameterOverride
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x0000AF88 File Offset: 0x00009188
		public ParameterOverride() : this(default(T), false)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000AFA5 File Offset: 0x000091A5
		public ParameterOverride(T value) : this(value, false)
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000AFAF File Offset: 0x000091AF
		public ParameterOverride(T value, bool overrideState)
		{
			this.value = value;
			this.overrideState = overrideState;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000AFC5 File Offset: 0x000091C5
		internal override void Interp(ParameterOverride from, ParameterOverride to, float t)
		{
			this.Interp(from.GetValue<T>(), to.GetValue<T>(), t);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000AFDA File Offset: 0x000091DA
		public virtual void Interp(T from, T to, float t)
		{
			this.value = ((t > 0f) ? to : from);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000AFEE File Offset: 0x000091EE
		public void Override(T x)
		{
			this.overrideState = true;
			this.value = x;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000AFFE File Offset: 0x000091FE
		internal override void SetValue(ParameterOverride parameter)
		{
			this.value = parameter.GetValue<T>();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000B00C File Offset: 0x0000920C
		public override int GetHash()
		{
			return (17 * 23 + this.overrideState.GetHashCode()) * 23 + this.value.GetHashCode();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000B034 File Offset: 0x00009234
		public static implicit operator T(ParameterOverride<T> prop)
		{
			return prop.value;
		}

		// Token: 0x04000165 RID: 357
		public T value;
	}
}
