using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace ES3Types
{
	// Token: 0x0200006F RID: 111
	[Preserve]
	[ES3Properties(new string[]
	{
		"texture",
		"uvRect",
		"onCullStateChanged",
		"maskable",
		"color",
		"raycastTarget",
		"useLegacyMeshGeneration",
		"material",
		"useGUILayout",
		"enabled",
		"hideFlags"
	})]
	public class ES3Type_RawImage : ES3ComponentType
	{
		// Token: 0x06000308 RID: 776 RVA: 0x0000DAC1 File Offset: 0x0000BCC1
		public ES3Type_RawImage() : base(typeof(RawImage))
		{
			ES3Type_RawImage.Instance = this;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000DADC File Offset: 0x0000BCDC
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			RawImage rawImage = (RawImage)obj;
			writer.WritePropertyByRef("texture", rawImage.texture);
			writer.WriteProperty("uvRect", rawImage.uvRect, ES3Type_Rect.Instance);
			writer.WriteProperty("onCullStateChanged", rawImage.onCullStateChanged);
			writer.WriteProperty("maskable", rawImage.maskable, ES3Type_bool.Instance);
			writer.WriteProperty("color", rawImage.color, ES3Type_Color.Instance);
			writer.WriteProperty("raycastTarget", rawImage.raycastTarget, ES3Type_bool.Instance);
			writer.WritePrivateProperty("useLegacyMeshGeneration", rawImage);
			if (rawImage.material.name.Contains("Default"))
			{
				writer.WriteProperty("material", null);
			}
			else
			{
				writer.WriteProperty("material", rawImage.material);
			}
			writer.WriteProperty("useGUILayout", rawImage.useGUILayout, ES3Type_bool.Instance);
			writer.WriteProperty("enabled", rawImage.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("hideFlags", rawImage.hideFlags);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			RawImage rawImage = (RawImage)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1031692888U)
				{
					if (num <= 465631631U)
					{
						if (num != 49525662U)
						{
							if (num == 465631631U)
							{
								if (text == "onCullStateChanged")
								{
									rawImage.onCullStateChanged = reader.Read<MaskableGraphic.CullStateChangedEvent>();
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							rawImage.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 886554752U)
					{
						if (num != 1013213428U)
						{
							if (num == 1031692888U)
							{
								if (text == "color")
								{
									rawImage.color = reader.Read<Color>(ES3Type_Color.Instance);
									continue;
								}
							}
						}
						else if (text == "texture")
						{
							rawImage.texture = reader.Read<Texture>(ES3Type_Texture.Instance);
							continue;
						}
					}
					else if (text == "useLegacyMeshGeneration")
					{
						reader.SetPrivateProperty("useLegacyMeshGeneration", reader.Read<bool>(), rawImage);
						continue;
					}
				}
				else if (num <= 1784211997U)
				{
					if (num != 1570416393U)
					{
						if (num != 1753367520U)
						{
							if (num == 1784211997U)
							{
								if (text == "useGUILayout")
								{
									rawImage.useGUILayout = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "uvRect")
						{
							rawImage.uvRect = reader.Read<Rect>(ES3Type_Rect.Instance);
							continue;
						}
					}
					else if (text == "raycastTarget")
					{
						rawImage.raycastTarget = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num != 2154374041U)
				{
					if (num != 3538210912U)
					{
						if (num == 3944566772U)
						{
							if (text == "hideFlags")
							{
								rawImage.hideFlags = reader.Read<HideFlags>();
								continue;
							}
						}
					}
					else if (text == "material")
					{
						rawImage.material = reader.Read<Material>(ES3Type_Material.Instance);
						continue;
					}
				}
				else if (text == "maskable")
				{
					rawImage.maskable = reader.Read<bool>(ES3Type_bool.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000BE RID: 190
		public static ES3Type Instance;
	}
}
