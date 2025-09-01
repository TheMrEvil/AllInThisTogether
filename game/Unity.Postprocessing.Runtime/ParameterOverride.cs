using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000047 RID: 71
	public abstract class ParameterOverride
	{
		// Token: 0x060000EE RID: 238
		internal abstract void Interp(ParameterOverride from, ParameterOverride to, float t);

		// Token: 0x060000EF RID: 239
		public abstract int GetHash();

		// Token: 0x060000F0 RID: 240 RVA: 0x0000AF6D File Offset: 0x0000916D
		public T GetValue<T>()
		{
			return ((ParameterOverride<T>)this).value;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000AF7A File Offset: 0x0000917A
		protected internal virtual void OnEnable()
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000AF7C File Offset: 0x0000917C
		protected internal virtual void OnDisable()
		{
		}

		// Token: 0x060000F3 RID: 243
		internal abstract void SetValue(ParameterOverride parameter);

		// Token: 0x060000F4 RID: 244 RVA: 0x0000AF7E File Offset: 0x0000917E
		protected ParameterOverride()
		{
		}

		// Token: 0x04000164 RID: 356
		public bool overrideState;
	}
}
