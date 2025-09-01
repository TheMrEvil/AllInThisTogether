using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000030 RID: 48
	internal sealed class PreferenceDictionary : ScriptableObject, ISerializationCallbackReceiver, IHasDefault
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00016204 File Offset: 0x00014404
		public void OnBeforeSerialize()
		{
			this.m_Bool_keys = this.m_Bool.Keys.ToList<string>();
			this.m_Int_keys = this.m_Int.Keys.ToList<string>();
			this.m_Float_keys = this.m_Float.Keys.ToList<string>();
			this.m_String_keys = this.m_String.Keys.ToList<string>();
			this.m_Color_keys = this.m_Color.Keys.ToList<string>();
			this.m_Material_keys = this.m_Material.Keys.ToList<string>();
			this.m_Bool_values = this.m_Bool.Values.ToList<bool>();
			this.m_Int_values = this.m_Int.Values.ToList<int>();
			this.m_Float_values = this.m_Float.Values.ToList<float>();
			this.m_String_values = this.m_String.Values.ToList<string>();
			this.m_Color_values = this.m_Color.Values.ToList<Color>();
			this.m_Material_values = this.m_Material.Values.ToList<Material>();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0001631C File Offset: 0x0001451C
		public void OnAfterDeserialize()
		{
			for (int i = 0; i < this.m_Bool_keys.Count; i++)
			{
				this.m_Bool.Add(this.m_Bool_keys[i], this.m_Bool_values[i]);
			}
			for (int j = 0; j < this.m_Int_keys.Count; j++)
			{
				this.m_Int.Add(this.m_Int_keys[j], this.m_Int_values[j]);
			}
			for (int k = 0; k < this.m_Float_keys.Count; k++)
			{
				this.m_Float.Add(this.m_Float_keys[k], this.m_Float_values[k]);
			}
			for (int l = 0; l < this.m_String_keys.Count; l++)
			{
				this.m_String.Add(this.m_String_keys[l], this.m_String_values[l]);
			}
			for (int m = 0; m < this.m_Color_keys.Count; m++)
			{
				this.m_Color.Add(this.m_Color_keys[m], this.m_Color_values[m]);
			}
			for (int n = 0; n < this.m_Material_keys.Count; n++)
			{
				this.m_Material.Add(this.m_Material_keys[n], this.m_Material_values[n]);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0001648C File Offset: 0x0001468C
		public void SetDefaultValues()
		{
			this.m_Bool.Clear();
			this.m_Int.Clear();
			this.m_Float.Clear();
			this.m_String.Clear();
			this.m_Color.Clear();
			this.m_Material.Clear();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000164DC File Offset: 0x000146DC
		public bool HasKey(string key)
		{
			return this.m_Bool.ContainsKey(key) || this.m_Int.ContainsKey(key) || this.m_Float.ContainsKey(key) || this.m_String.ContainsKey(key) || this.m_Color.ContainsKey(key) || this.m_Material.ContainsKey(key);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00016540 File Offset: 0x00014740
		public bool HasKey<T>(string key)
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == typeof(int))
			{
				return this.m_Int.ContainsKey(key);
			}
			if (typeFromHandle == typeof(float))
			{
				return this.m_Float.ContainsKey(key);
			}
			if (typeFromHandle == typeof(bool))
			{
				return this.m_Bool.ContainsKey(key);
			}
			if (typeFromHandle == typeof(string))
			{
				return this.m_String.ContainsKey(key);
			}
			if (typeFromHandle == typeof(Color))
			{
				return this.m_Color.ContainsKey(key);
			}
			if (typeFromHandle == typeof(Material))
			{
				return this.m_Material.ContainsKey(key);
			}
			Debug.LogWarning(string.Format("HasKey<{0}>({1}) not valid preference type.", typeof(T).ToString(), key));
			return false;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00016634 File Offset: 0x00014834
		public void DeleteKey(string key)
		{
			if (this.m_Bool.ContainsKey(key))
			{
				this.m_Bool.Remove(key);
			}
			if (this.m_Int.ContainsKey(key))
			{
				this.m_Int.Remove(key);
			}
			if (this.m_Float.ContainsKey(key))
			{
				this.m_Float.Remove(key);
			}
			if (this.m_String.ContainsKey(key))
			{
				this.m_String.Remove(key);
			}
			if (this.m_Color.ContainsKey(key))
			{
				this.m_Color.Remove(key);
			}
			if (this.m_Material.ContainsKey(key))
			{
				this.m_Material.Remove(key);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000166E4 File Offset: 0x000148E4
		public T Get<T>(string key, T fallback = default(T))
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == typeof(int))
			{
				if (this.m_Int.ContainsKey(key))
				{
					return (T)((object)this.GetInt(key, 0));
				}
			}
			else if (typeFromHandle == typeof(float))
			{
				if (this.m_Float.ContainsKey(key))
				{
					return (T)((object)this.GetFloat(key, 0f));
				}
			}
			else if (typeFromHandle == typeof(bool))
			{
				if (this.m_Bool.ContainsKey(key))
				{
					return (T)((object)this.GetBool(key, false));
				}
			}
			else if (typeFromHandle == typeof(string))
			{
				if (this.m_String.ContainsKey(key))
				{
					return (T)((object)this.GetString(key, null));
				}
			}
			else if (typeFromHandle == typeof(Color))
			{
				if (this.m_Color.ContainsKey(key))
				{
					return (T)((object)this.GetColor(key, default(Color)));
				}
			}
			else if (typeFromHandle == typeof(Material))
			{
				if (this.m_Material.ContainsKey(key))
				{
					return (T)((object)this.GetMaterial(key, null));
				}
			}
			else
			{
				Debug.LogWarning(string.Format("Get<{0}>({1}) not valid preference type.", typeof(T).ToString(), key));
			}
			return fallback;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0001685C File Offset: 0x00014A5C
		public void Set<T>(string key, T value)
		{
			object obj = value;
			if (value is int)
			{
				this.SetInt(key, (int)obj);
				return;
			}
			if (value is float)
			{
				this.SetFloat(key, (float)obj);
				return;
			}
			if (value is bool)
			{
				this.SetBool(key, (bool)obj);
				return;
			}
			if (value is string)
			{
				this.SetString(key, (string)obj);
				return;
			}
			if (value is Color)
			{
				this.SetColor(key, (Color)obj);
				return;
			}
			if (value is Material)
			{
				this.SetMaterial(key, (Material)obj);
				return;
			}
			Debug.LogWarning(string.Format("Set<{0}>({1}, {2}) not valid preference type.", typeof(T).ToString(), key, value.ToString()));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00016940 File Offset: 0x00014B40
		public bool GetBool(string key, bool fallback = false)
		{
			bool result;
			if (this.m_Bool.TryGetValue(key, out result))
			{
				return result;
			}
			return fallback;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00016960 File Offset: 0x00014B60
		public int GetInt(string key, int fallback = 0)
		{
			int result;
			if (this.m_Int.TryGetValue(key, out result))
			{
				return result;
			}
			return fallback;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00016980 File Offset: 0x00014B80
		public float GetFloat(string key, float fallback = 0f)
		{
			float result;
			if (this.m_Float.TryGetValue(key, out result))
			{
				return result;
			}
			return fallback;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000169A0 File Offset: 0x00014BA0
		public string GetString(string key, string fallback = null)
		{
			string result;
			if (this.m_String.TryGetValue(key, out result))
			{
				return result;
			}
			return fallback;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000169C0 File Offset: 0x00014BC0
		public Color GetColor(string key, Color fallback = default(Color))
		{
			Color result;
			if (this.m_Color.TryGetValue(key, out result))
			{
				return result;
			}
			return fallback;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000169E0 File Offset: 0x00014BE0
		public Material GetMaterial(string key, Material fallback = null)
		{
			Material result;
			if (this.m_Material.TryGetValue(key, out result))
			{
				return result;
			}
			return fallback;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00016A00 File Offset: 0x00014C00
		public void SetBool(string key, bool value)
		{
			this.m_Bool[key] = value;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00016A0F File Offset: 0x00014C0F
		public void SetInt(string key, int value)
		{
			this.m_Int[key] = value;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00016A1E File Offset: 0x00014C1E
		public void SetFloat(string key, float value)
		{
			this.m_Float[key] = value;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00016A2D File Offset: 0x00014C2D
		public void SetString(string key, string value)
		{
			this.m_String[key] = value;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00016A3C File Offset: 0x00014C3C
		public void SetColor(string key, Color value)
		{
			this.m_Color[key] = value;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00016A4B File Offset: 0x00014C4B
		public void SetMaterial(string key, Material value)
		{
			this.m_Material[key] = value;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00016A5A File Offset: 0x00014C5A
		public Dictionary<string, bool> GetBoolDictionary()
		{
			return this.m_Bool;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00016A62 File Offset: 0x00014C62
		public Dictionary<string, int> GetIntDictionary()
		{
			return this.m_Int;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00016A6A File Offset: 0x00014C6A
		public Dictionary<string, float> GetFloatDictionary()
		{
			return this.m_Float;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00016A72 File Offset: 0x00014C72
		public Dictionary<string, string> GetStringDictionary()
		{
			return this.m_String;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00016A7A File Offset: 0x00014C7A
		public Dictionary<string, Color> GetColorDictionary()
		{
			return this.m_Color;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00016A82 File Offset: 0x00014C82
		public Dictionary<string, Material> GetMaterialDictionary()
		{
			return this.m_Material;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00016A8A File Offset: 0x00014C8A
		public void Clear()
		{
			this.m_Bool.Clear();
			this.m_Int.Clear();
			this.m_Float.Clear();
			this.m_String.Clear();
			this.m_Color.Clear();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00016AC4 File Offset: 0x00014CC4
		public PreferenceDictionary()
		{
		}

		// Token: 0x040000A4 RID: 164
		private Dictionary<string, bool> m_Bool = new Dictionary<string, bool>();

		// Token: 0x040000A5 RID: 165
		private Dictionary<string, int> m_Int = new Dictionary<string, int>();

		// Token: 0x040000A6 RID: 166
		private Dictionary<string, float> m_Float = new Dictionary<string, float>();

		// Token: 0x040000A7 RID: 167
		private Dictionary<string, string> m_String = new Dictionary<string, string>();

		// Token: 0x040000A8 RID: 168
		private Dictionary<string, Color> m_Color = new Dictionary<string, Color>();

		// Token: 0x040000A9 RID: 169
		private Dictionary<string, Material> m_Material = new Dictionary<string, Material>();

		// Token: 0x040000AA RID: 170
		[SerializeField]
		private List<string> m_Bool_keys;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private List<string> m_Int_keys;

		// Token: 0x040000AC RID: 172
		[SerializeField]
		private List<string> m_Float_keys;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		private List<string> m_String_keys;

		// Token: 0x040000AE RID: 174
		[SerializeField]
		private List<string> m_Color_keys;

		// Token: 0x040000AF RID: 175
		[SerializeField]
		private List<string> m_Material_keys;

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		private List<bool> m_Bool_values;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		private List<int> m_Int_values;

		// Token: 0x040000B2 RID: 178
		[SerializeField]
		private List<float> m_Float_values;

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		private List<string> m_String_values;

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		private List<Color> m_Color_values;

		// Token: 0x040000B5 RID: 181
		[SerializeField]
		private List<Material> m_Material_values;
	}
}
