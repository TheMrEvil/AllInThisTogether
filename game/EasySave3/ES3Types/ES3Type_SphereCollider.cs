using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000073 RID: 115
	[Preserve]
	[ES3Properties(new string[]
	{
		"center",
		"radius",
		"enabled",
		"isTrigger",
		"contactOffset",
		"sharedMaterial"
	})]
	public class ES3Type_SphereCollider : ES3ComponentType
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000E6E5 File Offset: 0x0000C8E5
		public ES3Type_SphereCollider() : base(typeof(SphereCollider))
		{
			ES3Type_SphereCollider.Instance = this;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E700 File Offset: 0x0000C900
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SphereCollider sphereCollider = (SphereCollider)obj;
			writer.WriteProperty("center", sphereCollider.center, ES3Type_Vector3.Instance);
			writer.WriteProperty("radius", sphereCollider.radius, ES3Type_float.Instance);
			writer.WriteProperty("enabled", sphereCollider.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("isTrigger", sphereCollider.isTrigger, ES3Type_bool.Instance);
			writer.WriteProperty("contactOffset", sphereCollider.contactOffset, ES3Type_float.Instance);
			writer.WritePropertyByRef("material", sphereCollider.sharedMaterial);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E7AC File Offset: 0x0000C9AC
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SphereCollider sphereCollider = (SphereCollider)obj;
			foreach (object obj2 in reader.Properties)
			{
				string a = (string)obj2;
				if (!(a == "center"))
				{
					if (!(a == "radius"))
					{
						if (!(a == "enabled"))
						{
							if (!(a == "isTrigger"))
							{
								if (!(a == "contactOffset"))
								{
									if (!(a == "material"))
									{
										reader.Skip();
									}
									else
									{
										sphereCollider.sharedMaterial = reader.Read<PhysicMaterial>();
									}
								}
								else
								{
									sphereCollider.contactOffset = reader.Read<float>(ES3Type_float.Instance);
								}
							}
							else
							{
								sphereCollider.isTrigger = reader.Read<bool>(ES3Type_bool.Instance);
							}
						}
						else
						{
							sphereCollider.enabled = reader.Read<bool>(ES3Type_bool.Instance);
						}
					}
					else
					{
						sphereCollider.radius = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					sphereCollider.center = reader.Read<Vector3>(ES3Type_Vector3.Instance);
				}
			}
		}

		// Token: 0x040000C2 RID: 194
		public static ES3Type Instance;
	}
}
