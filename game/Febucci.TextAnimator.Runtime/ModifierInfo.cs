using System;

namespace Febucci.UI
{
	// Token: 0x02000007 RID: 7
	public struct ModifierInfo : IEquatable<ModifierInfo>
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002384 File Offset: 0x00000584
		public ModifierInfo(string name, float value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002394 File Offset: 0x00000594
		public bool Equals(ModifierInfo other)
		{
			return this.value.Equals(other.value) && this.name.Equals(other.name);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023BC File Offset: 0x000005BC
		public override string ToString()
		{
			return string.Format("{0}={1}", this.name, this.value);
		}

		// Token: 0x04000016 RID: 22
		public string name;

		// Token: 0x04000017 RID: 23
		public float value;
	}
}
