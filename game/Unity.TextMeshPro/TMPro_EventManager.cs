using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200000F RID: 15
	public static class TMPro_EventManager
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00016965 File Offset: 0x00014B65
		public static void ON_MATERIAL_PROPERTY_CHANGED(bool isChanged, Material mat)
		{
			TMPro_EventManager.MATERIAL_PROPERTY_EVENT.Call(isChanged, mat);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00016973 File Offset: 0x00014B73
		public static void ON_FONT_PROPERTY_CHANGED(bool isChanged, UnityEngine.Object obj)
		{
			TMPro_EventManager.FONT_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00016981 File Offset: 0x00014B81
		public static void ON_SPRITE_ASSET_PROPERTY_CHANGED(bool isChanged, UnityEngine.Object obj)
		{
			TMPro_EventManager.SPRITE_ASSET_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0001698F File Offset: 0x00014B8F
		public static void ON_TEXTMESHPRO_PROPERTY_CHANGED(bool isChanged, UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXTMESHPRO_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0001699D File Offset: 0x00014B9D
		public static void ON_DRAG_AND_DROP_MATERIAL_CHANGED(GameObject sender, Material currentMaterial, Material newMaterial)
		{
			TMPro_EventManager.DRAG_AND_DROP_MATERIAL_EVENT.Call(sender, currentMaterial, newMaterial);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000169AC File Offset: 0x00014BAC
		public static void ON_TEXT_STYLE_PROPERTY_CHANGED(bool isChanged)
		{
			TMPro_EventManager.TEXT_STYLE_PROPERTY_EVENT.Call(isChanged);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000169B9 File Offset: 0x00014BB9
		public static void ON_COLOR_GRADIENT_PROPERTY_CHANGED(UnityEngine.Object obj)
		{
			TMPro_EventManager.COLOR_GRADIENT_PROPERTY_EVENT.Call(obj);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000169C6 File Offset: 0x00014BC6
		public static void ON_TEXT_CHANGED(UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXT_CHANGED_EVENT.Call(obj);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000169D3 File Offset: 0x00014BD3
		public static void ON_TMP_SETTINGS_CHANGED()
		{
			TMPro_EventManager.TMP_SETTINGS_PROPERTY_EVENT.Call();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000169DF File Offset: 0x00014BDF
		public static void ON_RESOURCES_LOADED()
		{
			TMPro_EventManager.RESOURCE_LOAD_EVENT.Call();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000169EB File Offset: 0x00014BEB
		public static void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(bool isChanged, UnityEngine.Object obj)
		{
			TMPro_EventManager.TEXTMESHPRO_UGUI_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000169F9 File Offset: 0x00014BF9
		public static void ON_COMPUTE_DT_EVENT(object Sender, Compute_DT_EventArgs e)
		{
			TMPro_EventManager.COMPUTE_DT_EVENT.Call(Sender, e);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00016A08 File Offset: 0x00014C08
		// Note: this type is marked as 'beforefieldinit'.
		static TMPro_EventManager()
		{
		}

		// Token: 0x04000081 RID: 129
		public static readonly FastAction<object, Compute_DT_EventArgs> COMPUTE_DT_EVENT = new FastAction<object, Compute_DT_EventArgs>();

		// Token: 0x04000082 RID: 130
		public static readonly FastAction<bool, Material> MATERIAL_PROPERTY_EVENT = new FastAction<bool, Material>();

		// Token: 0x04000083 RID: 131
		public static readonly FastAction<bool, UnityEngine.Object> FONT_PROPERTY_EVENT = new FastAction<bool, UnityEngine.Object>();

		// Token: 0x04000084 RID: 132
		public static readonly FastAction<bool, UnityEngine.Object> SPRITE_ASSET_PROPERTY_EVENT = new FastAction<bool, UnityEngine.Object>();

		// Token: 0x04000085 RID: 133
		public static readonly FastAction<bool, UnityEngine.Object> TEXTMESHPRO_PROPERTY_EVENT = new FastAction<bool, UnityEngine.Object>();

		// Token: 0x04000086 RID: 134
		public static readonly FastAction<GameObject, Material, Material> DRAG_AND_DROP_MATERIAL_EVENT = new FastAction<GameObject, Material, Material>();

		// Token: 0x04000087 RID: 135
		public static readonly FastAction<bool> TEXT_STYLE_PROPERTY_EVENT = new FastAction<bool>();

		// Token: 0x04000088 RID: 136
		public static readonly FastAction<UnityEngine.Object> COLOR_GRADIENT_PROPERTY_EVENT = new FastAction<UnityEngine.Object>();

		// Token: 0x04000089 RID: 137
		public static readonly FastAction TMP_SETTINGS_PROPERTY_EVENT = new FastAction();

		// Token: 0x0400008A RID: 138
		public static readonly FastAction RESOURCE_LOAD_EVENT = new FastAction();

		// Token: 0x0400008B RID: 139
		public static readonly FastAction<bool, UnityEngine.Object> TEXTMESHPRO_UGUI_PROPERTY_EVENT = new FastAction<bool, UnityEngine.Object>();

		// Token: 0x0400008C RID: 140
		public static readonly FastAction<UnityEngine.Object> TEXT_CHANGED_EVENT = new FastAction<UnityEngine.Object>();
	}
}
