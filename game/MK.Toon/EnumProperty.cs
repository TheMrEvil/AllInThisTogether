using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000031 RID: 49
	public class EnumProperty<T> : Property<T> where T : Enum
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00003481 File Offset: 0x00001681
		public EnumProperty(Uniform uniform, params string[] keywords) : base(uniform, keywords)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000348B File Offset: 0x0000168B
		public override T GetValue(Material material)
		{
			return (T)((object)material.GetInt(this._uniform.id));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000034A8 File Offset: 0x000016A8
		public override void SetValue(Material material, T value)
		{
			material.SetInt(this._uniform.id, (int)((object)value));
			base.SetKeyword(material, (int)((object)value) != 0, (int)((object)value));
		}
	}
}
