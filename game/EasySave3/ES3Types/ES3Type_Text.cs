using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace ES3Types
{
	// Token: 0x02000074 RID: 116
	[Preserve]
	[ES3Properties(new string[]
	{
		"font",
		"text",
		"supportRichText",
		"resizeTextForBestFit",
		"resizeTextMinSize",
		"resizeTextMaxSize",
		"alignment",
		"alignByGeometry",
		"fontSize",
		"horizontalOverflow",
		"verticalOverflow",
		"lineSpacing",
		"fontStyle",
		"onCullStateChanged",
		"maskable",
		"color",
		"raycastTarget",
		"material",
		"useGUILayout",
		"enabled",
		"tag",
		"name",
		"hideFlags"
	})]
	public class ES3Type_Text : ES3ComponentType
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		public ES3Type_Text() : base(typeof(Text))
		{
			ES3Type_Text.Instance = this;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000E8E8 File Offset: 0x0000CAE8
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			Text text = (Text)obj;
			writer.WriteProperty("text", text.text);
			writer.WriteProperty("supportRichText", text.supportRichText);
			writer.WriteProperty("resizeTextForBestFit", text.resizeTextForBestFit);
			writer.WriteProperty("resizeTextMinSize", text.resizeTextMinSize);
			writer.WriteProperty("resizeTextMaxSize", text.resizeTextMaxSize);
			writer.WriteProperty("alignment", text.alignment);
			writer.WriteProperty("alignByGeometry", text.alignByGeometry);
			writer.WriteProperty("fontSize", text.fontSize);
			writer.WriteProperty("horizontalOverflow", text.horizontalOverflow);
			writer.WriteProperty("verticalOverflow", text.verticalOverflow);
			writer.WriteProperty("lineSpacing", text.lineSpacing);
			writer.WriteProperty("fontStyle", text.fontStyle);
			writer.WriteProperty("onCullStateChanged", text.onCullStateChanged);
			writer.WriteProperty("maskable", text.maskable);
			writer.WriteProperty("color", text.color);
			writer.WriteProperty("raycastTarget", text.raycastTarget);
			if (text.material.name.Contains("Default"))
			{
				writer.WriteProperty("material", null);
			}
			else
			{
				writer.WriteProperty("material", text.material);
			}
			writer.WriteProperty("useGUILayout", text.useGUILayout);
			writer.WriteProperty("enabled", text.enabled);
			writer.WriteProperty("hideFlags", text.hideFlags);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000EACC File Offset: 0x0000CCCC
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			Text text = (Text)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text2 = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 1889725101U)
				{
					if (num <= 659427984U)
					{
						if (num <= 455389033U)
						{
							if (num != 49525662U)
							{
								if (num != 435358413U)
								{
									if (num == 455389033U)
									{
										if (text2 == "verticalOverflow")
										{
											text.verticalOverflow = reader.Read<VerticalWrapMode>();
											continue;
										}
									}
								}
								else if (text2 == "supportRichText")
								{
									text.supportRichText = reader.Read<bool>();
									continue;
								}
							}
							else if (text2 == "enabled")
							{
								text.enabled = reader.Read<bool>();
								continue;
							}
						}
						else if (num != 465631631U)
						{
							if (num != 576466009U)
							{
								if (num == 659427984U)
								{
									if (text2 == "font")
									{
										text.font = reader.Read<Font>();
										continue;
									}
								}
							}
							else if (text2 == "m_TextCacheForLayout")
							{
								reader.SetPrivateField("m_TextCacheForLayout", reader.Read<TextGenerator>(), text);
								continue;
							}
						}
						else if (text2 == "onCullStateChanged")
						{
							text.onCullStateChanged = reader.Read<MaskableGraphic.CullStateChangedEvent>();
							continue;
						}
					}
					else if (num <= 1418927036U)
					{
						if (num != 1002274016U)
						{
							if (num != 1031692888U)
							{
								if (num == 1418927036U)
								{
									if (text2 == "m_LastTrackedFont")
									{
										reader.SetPrivateField("m_LastTrackedFont", reader.Read<Font>(), text);
										continue;
									}
								}
							}
							else if (text2 == "color")
							{
								text.color = reader.Read<Color>();
								continue;
							}
						}
						else if (text2 == "m_Material")
						{
							reader.SetPrivateField("m_Material", reader.Read<Material>(), text);
							continue;
						}
					}
					else if (num <= 1570416393U)
					{
						if (num != 1455915036U)
						{
							if (num == 1570416393U)
							{
								if (text2 == "raycastTarget")
								{
									text.raycastTarget = reader.Read<bool>();
									continue;
								}
							}
						}
						else if (text2 == "m_TextCache")
						{
							reader.SetPrivateField("m_TextCache", reader.Read<TextGenerator>(), text);
							continue;
						}
					}
					else if (num != 1784211997U)
					{
						if (num == 1889725101U)
						{
							if (text2 == "resizeTextMaxSize")
							{
								text.resizeTextMaxSize = reader.Read<int>();
								continue;
							}
						}
					}
					else if (text2 == "useGUILayout")
					{
						text.useGUILayout = reader.Read<bool>();
						continue;
					}
				}
				else if (num <= 2891645794U)
				{
					if (num <= 2154374041U)
					{
						if (num != 2055430583U)
						{
							if (num != 2094909700U)
							{
								if (num == 2154374041U)
								{
									if (text2 == "maskable")
									{
										text.maskable = reader.Read<bool>();
										continue;
									}
								}
							}
							else if (text2 == "lineSpacing")
							{
								text.lineSpacing = reader.Read<float>();
								continue;
							}
						}
						else if (text2 == "horizontalOverflow")
						{
							text.horizontalOverflow = reader.Read<HorizontalWrapMode>();
							continue;
						}
					}
					else if (num <= 2507921237U)
					{
						if (num != 2476765927U)
						{
							if (num == 2507921237U)
							{
								if (text2 == "fontStyle")
								{
									text.fontStyle = reader.Read<FontStyle>();
									continue;
								}
							}
						}
						else if (text2 == "resizeTextMinSize")
						{
							text.resizeTextMinSize = reader.Read<int>();
							continue;
						}
					}
					else if (num != 2834400513U)
					{
						if (num == 2891645794U)
						{
							if (text2 == "resizeTextForBestFit")
							{
								text.resizeTextForBestFit = reader.Read<bool>();
								continue;
							}
						}
					}
					else if (text2 == "fontSize")
					{
						text.fontSize = reader.Read<int>();
						continue;
					}
				}
				else if (num <= 3538210912U)
				{
					if (num != 2984076638U)
					{
						if (num != 3185987134U)
						{
							if (num == 3538210912U)
							{
								if (text2 == "material")
								{
									text.material = reader.Read<Material>();
									continue;
								}
							}
						}
						else if (text2 == "text")
						{
							text.text = reader.Read<string>();
							continue;
						}
					}
					else if (text2 == "alignment")
					{
						text.alignment = reader.Read<TextAnchor>();
						continue;
					}
				}
				else if (num <= 3944566772U)
				{
					if (num != 3559542335U)
					{
						if (num == 3944566772U)
						{
							if (text2 == "hideFlags")
							{
								text.hideFlags = reader.Read<HideFlags>();
								continue;
							}
						}
					}
					else if (text2 == "alignByGeometry")
					{
						text.alignByGeometry = reader.Read<bool>();
						continue;
					}
				}
				else if (num != 4194726424U)
				{
					if (num == 4220859070U)
					{
						if (text2 == "m_Text")
						{
							reader.SetPrivateField("m_Text", reader.Read<string>(), text);
							continue;
						}
					}
				}
				else if (text2 == "m_FontData")
				{
					reader.SetPrivateField("m_FontData", reader.Read<FontData>(), text);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000C3 RID: 195
		public static ES3Type Instance;
	}
}
