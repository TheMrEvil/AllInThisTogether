using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200005F RID: 95
	[Preserve]
	[ES3Properties(new string[]
	{
		"center",
		"size",
		"enabled",
		"isTrigger",
		"contactOffset",
		"sharedMaterial"
	})]
	public class ES3Type_BoxCollider : ES3ComponentType
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000ABF3 File Offset: 0x00008DF3
		public ES3Type_BoxCollider() : base(typeof(BoxCollider))
		{
			ES3Type_BoxCollider.Instance = this;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000AC0C File Offset: 0x00008E0C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			BoxCollider boxCollider = (BoxCollider)obj;
			writer.WriteProperty("center", boxCollider.center);
			writer.WriteProperty("size", boxCollider.size);
			writer.WriteProperty("enabled", boxCollider.enabled);
			writer.WriteProperty("isTrigger", boxCollider.isTrigger);
			writer.WriteProperty("contactOffset", boxCollider.contactOffset);
			writer.WritePropertyByRef("material", boxCollider.sharedMaterial);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			BoxCollider boxCollider = (BoxCollider)obj;
			foreach (object obj2 in reader.Properties)
			{
				string a = (string)obj2;
				if (!(a == "center"))
				{
					if (!(a == "size"))
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
										boxCollider.sharedMaterial = reader.Read<PhysicMaterial>();
									}
								}
								else
								{
									boxCollider.contactOffset = reader.Read<float>();
								}
							}
							else
							{
								boxCollider.isTrigger = reader.Read<bool>();
							}
						}
						else
						{
							boxCollider.enabled = reader.Read<bool>();
						}
					}
					else
					{
						boxCollider.size = reader.Read<Vector3>();
					}
				}
				else
				{
					boxCollider.center = reader.Read<Vector3>();
				}
			}
		}

		// Token: 0x040000AE RID: 174
		public static ES3Type Instance;
	}
}
