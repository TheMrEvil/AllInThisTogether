using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200006D RID: 109
	[Preserve]
	[ES3Properties(new string[]
	{
		"points",
		"pathCount",
		"paths",
		"density",
		"isTrigger",
		"usedByEffector",
		"offset",
		"sharedMaterial",
		"enabled"
	})]
	public class ES3Type_PolygonCollider2D : ES3ComponentType
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0000D704 File Offset: 0x0000B904
		public ES3Type_PolygonCollider2D() : base(typeof(PolygonCollider2D))
		{
			ES3Type_PolygonCollider2D.Instance = this;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000D71C File Offset: 0x0000B91C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			PolygonCollider2D polygonCollider2D = (PolygonCollider2D)obj;
			writer.WriteProperty("points", polygonCollider2D.points, ES3Type_Vector2Array.Instance);
			writer.WriteProperty("pathCount", polygonCollider2D.pathCount, ES3Type_int.Instance);
			for (int i = 0; i < polygonCollider2D.pathCount; i++)
			{
				writer.WriteProperty("path" + i.ToString(), polygonCollider2D.GetPath(i), ES3Type_Vector2Array.Instance);
			}
			if (polygonCollider2D.attachedRigidbody != null && polygonCollider2D.attachedRigidbody.useAutoMass)
			{
				writer.WriteProperty("density", polygonCollider2D.density, ES3Type_float.Instance);
			}
			writer.WriteProperty("isTrigger", polygonCollider2D.isTrigger, ES3Type_bool.Instance);
			writer.WriteProperty("usedByEffector", polygonCollider2D.usedByEffector, ES3Type_bool.Instance);
			writer.WriteProperty("offset", polygonCollider2D.offset, ES3Type_Vector2.Instance);
			writer.WriteProperty("sharedMaterial", polygonCollider2D.sharedMaterial);
			writer.WriteProperty("enabled", polygonCollider2D.enabled, ES3Type_bool.Instance);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D848 File Offset: 0x0000BA48
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			PolygonCollider2D polygonCollider2D = (PolygonCollider2D)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1924728219U)
				{
					if (num <= 49525662U)
					{
						if (num != 44485181U)
						{
							if (num == 49525662U)
							{
								if (text == "enabled")
								{
									polygonCollider2D.enabled = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "sharedMaterial")
						{
							polygonCollider2D.sharedMaterial = reader.Read<PhysicsMaterial2D>(ES3Type_PhysicsMaterial2D.Instance);
							continue;
						}
					}
					else if (num != 348705738U)
					{
						if (num == 1924728219U)
						{
							if (text == "density")
							{
								polygonCollider2D.density = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "offset")
					{
						polygonCollider2D.offset = reader.Read<Vector2>(ES3Type_Vector2.Instance);
						continue;
					}
				}
				else if (num <= 2631195173U)
				{
					if (num != 2267167091U)
					{
						if (num == 2631195173U)
						{
							if (text == "pathCount")
							{
								int num2 = reader.Read<int>(ES3Type_int.Instance);
								for (int i = 0; i < num2; i++)
								{
									polygonCollider2D.SetPath(i, reader.ReadProperty<Vector2[]>(ES3Type_Vector2Array.Instance));
								}
								continue;
							}
						}
					}
					else if (text == "isTrigger")
					{
						polygonCollider2D.isTrigger = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num != 3063381777U)
				{
					if (num == 3163908038U)
					{
						if (text == "points")
						{
							polygonCollider2D.points = reader.Read<Vector2[]>(ES3Type_Vector2Array.Instance);
							continue;
						}
					}
				}
				else if (text == "usedByEffector")
				{
					polygonCollider2D.usedByEffector = reader.Read<bool>(ES3Type_bool.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000BC RID: 188
		public static ES3Type Instance;
	}
}
