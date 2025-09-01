using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000036 RID: 54
	public class EnvironmentReflectionProperty : Property<EnvironmentReflection>
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003644 File Offset: 0x00001844
		public EnvironmentReflectionProperty(Uniform uniform, params string[] keywords) : base(uniform, keywords)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000364E File Offset: 0x0000184E
		public override EnvironmentReflection GetValue(Material material)
		{
			return (EnvironmentReflection)material.GetInt(this._uniform.id);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003664 File Offset: 0x00001864
		public override void SetValue(Material material, EnvironmentReflection environmentReflection)
		{
			if (material.shader.name.Contains(Properties.shaderVariantSimpleName))
			{
				environmentReflection = ((environmentReflection >= EnvironmentReflection.Ambient) ? EnvironmentReflection.Ambient : EnvironmentReflection.Off);
			}
			material.SetInt(this._uniform.id, (int)environmentReflection);
			base.SetKeyword(material, environmentReflection > EnvironmentReflection.Off, (int)environmentReflection);
		}
	}
}
