using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A8 RID: 168
	[Preserve]
	[ES3Properties(new string[]
	{
		"bounciness",
		"friction"
	})]
	public class ES3Type_PhysicsMaterial2D : ES3ObjectType
	{
		// Token: 0x060003AB RID: 939 RVA: 0x000159FD File Offset: 0x00013BFD
		public ES3Type_PhysicsMaterial2D() : base(typeof(PhysicsMaterial2D))
		{
			ES3Type_PhysicsMaterial2D.Instance = this;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00015A18 File Offset: 0x00013C18
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			PhysicsMaterial2D physicsMaterial2D = (PhysicsMaterial2D)obj;
			writer.WriteProperty("bounciness", physicsMaterial2D.bounciness, ES3Type_float.Instance);
			writer.WriteProperty("friction", physicsMaterial2D.friction, ES3Type_float.Instance);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00015A64 File Offset: 0x00013C64
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			PhysicsMaterial2D physicsMaterial2D = (PhysicsMaterial2D)obj;
			foreach (object obj2 in reader.Properties)
			{
				string a = (string)obj2;
				if (!(a == "bounciness"))
				{
					if (!(a == "friction"))
					{
						reader.Skip();
					}
					else
					{
						physicsMaterial2D.friction = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					physicsMaterial2D.bounciness = reader.Read<float>(ES3Type_float.Instance);
				}
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00015B08 File Offset: 0x00013D08
		protected override object ReadObject<T>(ES3Reader reader)
		{
			PhysicsMaterial2D physicsMaterial2D = new PhysicsMaterial2D();
			this.ReadObject<T>(reader, physicsMaterial2D);
			return physicsMaterial2D;
		}

		// Token: 0x040000FB RID: 251
		public static ES3Type Instance;
	}
}
