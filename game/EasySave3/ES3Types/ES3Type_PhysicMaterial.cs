using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A6 RID: 166
	[Preserve]
	[ES3Properties(new string[]
	{
		"dynamicFriction",
		"staticFriction",
		"bounciness",
		"frictionCombine",
		"bounceCombine"
	})]
	public class ES3Type_PhysicMaterial : ES3ObjectType
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x00015818 File Offset: 0x00013A18
		public ES3Type_PhysicMaterial() : base(typeof(PhysicMaterial))
		{
			ES3Type_PhysicMaterial.Instance = this;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00015830 File Offset: 0x00013A30
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			PhysicMaterial physicMaterial = (PhysicMaterial)obj;
			writer.WriteProperty("dynamicFriction", physicMaterial.dynamicFriction, ES3Type_float.Instance);
			writer.WriteProperty("staticFriction", physicMaterial.staticFriction, ES3Type_float.Instance);
			writer.WriteProperty("bounciness", physicMaterial.bounciness, ES3Type_float.Instance);
			writer.WriteProperty("frictionCombine", physicMaterial.frictionCombine);
			writer.WriteProperty("bounceCombine", physicMaterial.bounceCombine);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000158C4 File Offset: 0x00013AC4
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			PhysicMaterial physicMaterial = (PhysicMaterial)obj;
			foreach (object obj2 in reader.Properties)
			{
				string a = (string)obj2;
				if (!(a == "dynamicFriction"))
				{
					if (!(a == "staticFriction"))
					{
						if (!(a == "bounciness"))
						{
							if (!(a == "frictionCombine"))
							{
								if (!(a == "bounceCombine"))
								{
									reader.Skip();
								}
								else
								{
									physicMaterial.bounceCombine = reader.Read<PhysicMaterialCombine>();
								}
							}
							else
							{
								physicMaterial.frictionCombine = reader.Read<PhysicMaterialCombine>();
							}
						}
						else
						{
							physicMaterial.bounciness = reader.Read<float>(ES3Type_float.Instance);
						}
					}
					else
					{
						physicMaterial.staticFriction = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					physicMaterial.dynamicFriction = reader.Read<float>(ES3Type_float.Instance);
				}
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000159C4 File Offset: 0x00013BC4
		protected override object ReadObject<T>(ES3Reader reader)
		{
			PhysicMaterial physicMaterial = new PhysicMaterial();
			this.ReadObject<T>(reader, physicMaterial);
			return physicMaterial;
		}

		// Token: 0x040000F9 RID: 249
		public static ES3Type Instance;
	}
}
