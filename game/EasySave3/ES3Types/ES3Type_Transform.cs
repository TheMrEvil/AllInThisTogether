using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000075 RID: 117
	[Preserve]
	[ES3Properties(new string[]
	{
		"localPosition",
		"localRotation",
		"localScale",
		"parent",
		"siblingIndex"
	})]
	public class ES3Type_Transform : ES3ComponentType
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000F108 File Offset: 0x0000D308
		public ES3Type_Transform() : base(typeof(Transform))
		{
			ES3Type_Transform.Instance = this;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000F120 File Offset: 0x0000D320
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Transform transform = (Transform)obj;
			writer.WritePropertyByRef("parent", transform.parent);
			writer.WriteProperty("localPosition", transform.localPosition);
			writer.WriteProperty("localRotation", transform.localRotation);
			writer.WriteProperty("localScale", transform.localScale);
			writer.WriteProperty("siblingIndex", transform.GetSiblingIndex());
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Transform transform = (Transform)obj;
			CharacterController component = transform.gameObject.GetComponent<CharacterController>();
			bool enabled = false;
			if (component != null)
			{
				enabled = component.enabled;
				component.enabled = false;
			}
			foreach (object obj2 in reader.Properties)
			{
				string a = (string)obj2;
				if (!(a == "parent"))
				{
					if (!(a == "localPosition"))
					{
						if (!(a == "localRotation"))
						{
							if (!(a == "localScale"))
							{
								if (!(a == "siblingIndex"))
								{
									reader.Skip();
								}
								else
								{
									transform.SetSiblingIndex(reader.Read<int>());
								}
							}
							else
							{
								transform.localScale = reader.Read<Vector3>();
							}
						}
						else
						{
							transform.localRotation = reader.Read<Quaternion>();
						}
					}
					else
					{
						transform.localPosition = reader.Read<Vector3>();
					}
				}
				else
				{
					transform.SetParent(reader.Read<Transform>());
				}
			}
			if (component != null)
			{
				component.enabled = enabled;
			}
		}

		// Token: 0x040000C4 RID: 196
		public static int countRead;

		// Token: 0x040000C5 RID: 197
		public static ES3Type Instance;
	}
}
