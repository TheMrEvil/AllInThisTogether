using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000062 RID: 98
	[Preserve]
	[ES3Properties(new string[]
	{
		"center",
		"radius",
		"height",
		"direction",
		"enabled",
		"isTrigger",
		"contactOffset",
		"sharedMaterial"
	})]
	public class ES3Type_CapsuleCollider : ES3ComponentType
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0000BA24 File Offset: 0x00009C24
		public ES3Type_CapsuleCollider() : base(typeof(CapsuleCollider))
		{
			ES3Type_CapsuleCollider.Instance = this;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000BA3C File Offset: 0x00009C3C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			writer.WriteProperty("center", capsuleCollider.center, ES3Type_Vector3.Instance);
			writer.WriteProperty("radius", capsuleCollider.radius, ES3Type_float.Instance);
			writer.WriteProperty("height", capsuleCollider.height, ES3Type_float.Instance);
			writer.WriteProperty("direction", capsuleCollider.direction, ES3Type_int.Instance);
			writer.WriteProperty("enabled", capsuleCollider.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("isTrigger", capsuleCollider.isTrigger, ES3Type_bool.Instance);
			writer.WriteProperty("contactOffset", capsuleCollider.contactOffset, ES3Type_float.Instance);
			writer.WritePropertyByRef("material", capsuleCollider.sharedMaterial);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000BB20 File Offset: 0x00009D20
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			CapsuleCollider capsuleCollider = (CapsuleCollider)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2267167091U)
				{
					if (num <= 93078660U)
					{
						if (num != 49525662U)
						{
							if (num == 93078660U)
							{
								if (text == "center")
								{
									capsuleCollider.center = reader.Read<Vector3>(ES3Type_Vector3.Instance);
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							capsuleCollider.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 230313139U)
					{
						if (num == 2267167091U)
						{
							if (text == "isTrigger")
							{
								capsuleCollider.isTrigger = reader.Read<bool>(ES3Type_bool.Instance);
								continue;
							}
						}
					}
					else if (text == "radius")
					{
						capsuleCollider.radius = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 3538210912U)
				{
					if (num != 2750587184U)
					{
						if (num == 3538210912U)
						{
							if (text == "material")
							{
								capsuleCollider.sharedMaterial = reader.Read<PhysicMaterial>();
								continue;
							}
						}
					}
					else if (text == "contactOffset")
					{
						capsuleCollider.contactOffset = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 3585981250U)
				{
					if (num == 3748513642U)
					{
						if (text == "direction")
						{
							capsuleCollider.direction = reader.Read<int>(ES3Type_int.Instance);
							continue;
						}
					}
				}
				else if (text == "height")
				{
					capsuleCollider.height = reader.Read<float>(ES3Type_float.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000B1 RID: 177
		public static ES3Type Instance;
	}
}
