using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000087 RID: 135
	[Preserve]
	[ES3Properties(new string[]
	{
		"enabled",
		"multiplier"
	})]
	public class ES3Type_ExternalForcesModule : ES3Type
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0001079C File Offset: 0x0000E99C
		public ES3Type_ExternalForcesModule() : base(typeof(ParticleSystem.ExternalForcesModule))
		{
			ES3Type_ExternalForcesModule.Instance = this;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.ExternalForcesModule externalForcesModule = (ParticleSystem.ExternalForcesModule)obj;
			writer.WriteProperty("enabled", externalForcesModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("multiplier", externalForcesModule.multiplier, ES3Type_float.Instance);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010800 File Offset: 0x0000EA00
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.ExternalForcesModule externalForcesModule = default(ParticleSystem.ExternalForcesModule);
			this.ReadInto<T>(reader, externalForcesModule);
			return externalForcesModule;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010828 File Offset: 0x0000EA28
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.ExternalForcesModule externalForcesModule = (ParticleSystem.ExternalForcesModule)obj;
			string a;
			while ((a = reader.ReadPropertyName()) != null)
			{
				if (!(a == "enabled"))
				{
					if (!(a == "multiplier"))
					{
						reader.Skip();
					}
					else
					{
						externalForcesModule.multiplier = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					externalForcesModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000D7 RID: 215
		public static ES3Type Instance;
	}
}
