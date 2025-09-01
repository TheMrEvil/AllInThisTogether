using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B9 RID: 185
	[Preserve]
	[ES3Properties(new string[]
	{
		"texture",
		"rect",
		"pivot",
		"pixelsPerUnit",
		"border"
	})]
	public class ES3Type_Sprite : ES3UnityObjectType
	{
		// Token: 0x060003DC RID: 988 RVA: 0x00018A41 File Offset: 0x00016C41
		public ES3Type_Sprite() : base(typeof(Sprite))
		{
			ES3Type_Sprite.Instance = this;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00018A5C File Offset: 0x00016C5C
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Sprite sprite = (Sprite)obj;
			writer.WriteProperty("texture", sprite.texture, ES3Type_Texture2D.Instance);
			writer.WriteProperty("rect", sprite.rect, ES3Type_Rect.Instance);
			writer.WriteProperty("pivot", new Vector2(sprite.pivot.x / (float)sprite.texture.width, sprite.pivot.y / (float)sprite.texture.height), ES3Type_Vector2.Instance);
			writer.WriteProperty("pixelsPerUnit", sprite.pixelsPerUnit, ES3Type_float.Instance);
			writer.WriteProperty("border", sprite.border, ES3Type_Vector4.Instance);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00018B24 File Offset: 0x00016D24
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				reader.Skip();
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00018B7C File Offset: 0x00016D7C
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			Texture2D texture = null;
			Rect rect = Rect.zero;
			Vector2 pivot = Vector2.zero;
			float pixelsPerUnit = 0f;
			Vector4 border = Vector4.zero;
			foreach (object obj in reader.Properties)
			{
				string a = (string)obj;
				if (!(a == "texture"))
				{
					if (!(a == "textureRect") && !(a == "rect"))
					{
						if (!(a == "pivot"))
						{
							if (!(a == "pixelsPerUnit"))
							{
								if (!(a == "border"))
								{
									reader.Skip();
								}
								else
								{
									border = reader.Read<Vector4>(ES3Type_Vector4.Instance);
								}
							}
							else
							{
								pixelsPerUnit = reader.Read<float>(ES3Type_float.Instance);
							}
						}
						else
						{
							pivot = reader.Read<Vector2>(ES3Type_Vector2.Instance);
						}
					}
					else
					{
						rect = reader.Read<Rect>(ES3Type_Rect.Instance);
					}
				}
				else
				{
					texture = reader.Read<Texture2D>(ES3Type_Texture2D.Instance);
				}
			}
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, 0U, SpriteMeshType.Tight, border);
		}

		// Token: 0x0400010C RID: 268
		public static ES3Type Instance;
	}
}
