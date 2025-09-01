using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000AD RID: 173
	[Preserve]
	[ES3Properties(new string[]
	{
		"anchorMin",
		"anchorMax",
		"anchoredPosition",
		"sizeDelta",
		"pivot",
		"offsetMin",
		"offsetMax",
		"localPosition",
		"localRotation",
		"localScale",
		"parent",
		"hideFlags"
	})]
	public class ES3Type_RectTransform : ES3ComponentType
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x00015D08 File Offset: 0x00013F08
		public ES3Type_RectTransform() : base(typeof(RectTransform))
		{
			ES3Type_RectTransform.Instance = this;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00015D20 File Offset: 0x00013F20
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			RectTransform rectTransform = (RectTransform)obj;
			writer.WritePropertyByRef("parent", rectTransform.parent);
			writer.WriteProperty("anchorMin", rectTransform.anchorMin, ES3Type_Vector2.Instance);
			writer.WriteProperty("anchorMax", rectTransform.anchorMax, ES3Type_Vector2.Instance);
			writer.WriteProperty("anchoredPosition", rectTransform.anchoredPosition, ES3Type_Vector2.Instance);
			writer.WriteProperty("sizeDelta", rectTransform.sizeDelta, ES3Type_Vector2.Instance);
			writer.WriteProperty("pivot", rectTransform.pivot, ES3Type_Vector2.Instance);
			writer.WriteProperty("offsetMin", rectTransform.offsetMin, ES3Type_Vector2.Instance);
			writer.WriteProperty("offsetMax", rectTransform.offsetMax, ES3Type_Vector2.Instance);
			writer.WriteProperty("localPosition", rectTransform.localPosition, ES3Type_Vector3.Instance);
			writer.WriteProperty("localRotation", rectTransform.localRotation, ES3Type_Quaternion.Instance);
			writer.WriteProperty("localScale", rectTransform.localScale, ES3Type_Vector3.Instance);
			writer.WriteProperty("hideFlags", rectTransform.hideFlags);
			writer.WriteProperty("siblingIndex", rectTransform.GetSiblingIndex());
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00015E80 File Offset: 0x00014080
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			if (obj.GetType() == typeof(Transform))
			{
				obj = ((Transform)obj).gameObject.AddComponent<RectTransform>();
			}
			RectTransform rectTransform = (RectTransform)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 3411781026U)
				{
					if (num <= 1458260364U)
					{
						if (num != 49430524U)
						{
							if (num != 583395338U)
							{
								if (num == 1458260364U)
								{
									if (text == "hierarchyCapacity")
									{
										rectTransform.hierarchyCapacity = reader.Read<int>(ES3Type_int.Instance);
										continue;
									}
								}
							}
							else if (text == "localScale")
							{
								rectTransform.localScale = reader.Read<Vector3>(ES3Type_Vector3.Instance);
								continue;
							}
						}
						else if (text == "offsetMax")
						{
							rectTransform.offsetMax = reader.Read<Vector2>(ES3Type_Vector2.Instance);
							continue;
						}
					}
					else if (num <= 2931884202U)
					{
						if (num != 2175575855U)
						{
							if (num == 2931884202U)
							{
								if (text == "sizeDelta")
								{
									rectTransform.sizeDelta = reader.Read<Vector2>(ES3Type_Vector2.Instance);
									continue;
								}
							}
						}
						else if (text == "siblingIndex")
						{
							rectTransform.SetSiblingIndex(reader.Read<int>());
							continue;
						}
					}
					else if (num != 3123707722U)
					{
						if (num == 3411781026U)
						{
							if (text == "anchorMax")
							{
								rectTransform.anchorMax = reader.Read<Vector2>(ES3Type_Vector2.Instance);
								continue;
							}
						}
					}
					else if (text == "anchoredPosition")
					{
						rectTransform.anchoredPosition = reader.Read<Vector2>(ES3Type_Vector2.Instance);
						continue;
					}
				}
				else if (num <= 3757480667U)
				{
					if (num != 3578277288U)
					{
						if (num != 3727349614U)
						{
							if (num == 3757480667U)
							{
								if (text == "localPosition")
								{
									rectTransform.localPosition = reader.Read<Vector3>(ES3Type_Vector3.Instance);
									continue;
								}
							}
						}
						else if (text == "localRotation")
						{
							rectTransform.localRotation = reader.Read<Quaternion>(ES3Type_Quaternion.Instance);
							continue;
						}
					}
					else if (text == "anchorMin")
					{
						rectTransform.anchorMin = reader.Read<Vector2>(ES3Type_Vector2.Instance);
						continue;
					}
				}
				else if (num <= 3944566772U)
				{
					if (num != 3939368189U)
					{
						if (num == 3944566772U)
						{
							if (text == "hideFlags")
							{
								rectTransform.hideFlags = reader.Read<HideFlags>();
								continue;
							}
						}
					}
					else if (text == "parent")
					{
						rectTransform.SetParent(reader.Read<Transform>(ES3Type_Transform.Instance));
						continue;
					}
				}
				else if (num != 4043783774U)
				{
					if (num == 4226831639U)
					{
						if (text == "pivot")
						{
							rectTransform.pivot = reader.Read<Vector2>(ES3Type_Vector2.Instance);
							continue;
						}
					}
				}
				else if (text == "offsetMin")
				{
					rectTransform.offsetMin = reader.Read<Vector2>(ES3Type_Vector2.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x04000100 RID: 256
		public static ES3Type Instance;
	}
}
