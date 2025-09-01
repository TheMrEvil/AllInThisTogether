using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using QFSW.QC.Utilities;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

// Token: 0x02000226 RID: 550
public static class ExtensionMethods
{
	// Token: 0x060016D7 RID: 5847 RVA: 0x00090F84 File Offset: 0x0008F184
	public static bool IsValid(this Vector3 p)
	{
		return p != p.INVALID();
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x00090F92 File Offset: 0x0008F192
	public static Vector3 INVALID(this Vector3 p)
	{
		return Vector3.up * -1024f;
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x00090FA4 File Offset: 0x0008F1A4
	public unsafe static bool AnyFlagsMatch<[IsUnmanaged] TEnum>(this TEnum e, TEnum test) where TEnum : struct, ValueType, Enum
	{
		ulong num = 0UL;
		ulong num2 = 0UL;
		UnsafeUtility.CopyStructureToPtr<TEnum>(ref e, (void*)(&num));
		UnsafeUtility.CopyStructureToPtr<TEnum>(ref test, (void*)(&num2));
		if ((num & num2) != 0UL)
		{
			return true;
		}
		for (ulong num3 = 1UL; num3 != 0UL; num3 <<= 1)
		{
			if ((num & num3) != 0UL && (num2 & num3) != 0UL)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x00090FEC File Offset: 0x0008F1EC
	public unsafe static bool HasFlagUnsafe<[IsUnmanaged] TEnum>(this TEnum lhs, TEnum rhs) where TEnum : struct, ValueType, Enum
	{
		ulong num = 0UL;
		UnsafeUtility.CopyStructureToPtr<TEnum>(ref lhs, (void*)(&num));
		ulong num2 = 0UL;
		UnsafeUtility.CopyStructureToPtr<TEnum>(ref rhs, (void*)(&num2));
		return (num & num2) > 0UL;
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x0009101C File Offset: 0x0008F21C
	public static void SetAlpha(this SpriteRenderer sr, float alpha)
	{
		Color color = sr.color;
		color.a = alpha;
		sr.color = color;
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x0009103F File Offset: 0x0008F23F
	public static void HideImmediate(this CanvasGroup cg)
	{
		cg.alpha = 0f;
		cg.blocksRaycasts = false;
		cg.interactable = false;
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x0009105A File Offset: 0x0008F25A
	public static void ShowImmediate(this CanvasGroup cg)
	{
		cg.alpha = 1f;
		cg.blocksRaycasts = true;
		cg.interactable = true;
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x00091078 File Offset: 0x0008F278
	public static void UpdateOpacity(this CanvasGroup cg, bool shouldShow, float fadeSpeed, bool ignoreInteractions = false)
	{
		if (!shouldShow || (cg.alpha >= 1f && (cg.interactable || ignoreInteractions)))
		{
			if (!shouldShow && (cg.alpha > 0f || (cg.interactable && !ignoreInteractions)))
			{
				cg.alpha -= fadeSpeed * Time.unscaledDeltaTime;
				if (ignoreInteractions)
				{
					return;
				}
				cg.interactable = false;
				cg.blocksRaycasts = false;
			}
			return;
		}
		cg.alpha += fadeSpeed * Time.unscaledDeltaTime;
		if (ignoreInteractions)
		{
			return;
		}
		cg.interactable = true;
		cg.blocksRaycasts = true;
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x00091108 File Offset: 0x0008F308
	public static void SetOpacity(this ParticleSystem particleSystem, float opacity)
	{
		ParticleSystem.MainModule main = particleSystem.main;
		ParticleSystem.MinMaxGradient startColor = particleSystem.main.startColor;
		Color color = startColor.color;
		color.a = opacity;
		startColor.color = color;
		main.startColor = startColor;
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x0009114B File Offset: 0x0008F34B
	public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
	{
		return new UnityWebRequestAwaiter(asyncOp);
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x00091154 File Offset: 0x0008F354
	public static string ToDetailedString(this Vector3 v)
	{
		return string.Concat(new string[]
		{
			v.x.ToInvariantStr(),
			"|",
			v.y.ToInvariantStr(),
			"|",
			v.z.ToInvariantStr()
		});
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x000911A6 File Offset: 0x0008F3A6
	public static string ToSimpleString(this Vector3 v)
	{
		return string.Format("{0}|{1}|{2}", (int)v.x, (int)v.y, (int)v.z);
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x000911D8 File Offset: 0x0008F3D8
	public static Vector3 ToVector3(this string str)
	{
		string text = str.Replace("\"", "");
		if (string.IsNullOrEmpty(text))
		{
			return Vector3.zero;
		}
		Vector3 zero = Vector3.zero;
		if (str.Contains("|"))
		{
			string[] array = text.Split('|', StringSplitOptions.None);
			try
			{
				zero.x = float.Parse(array[0], CultureInfo.InvariantCulture);
				zero.y = float.Parse(array[1], CultureInfo.InvariantCulture);
				zero.z = float.Parse(array[2], CultureInfo.InvariantCulture);
				return zero;
			}
			catch (Exception message)
			{
				UnityEngine.Debug.LogError("Error parsing Vector3: " + str);
				UnityEngine.Debug.LogError(message);
				throw;
			}
		}
		UnityEngine.Debug.LogError("Invalid Vector3 Format to parse: " + str);
		return zero;
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x00091298 File Offset: 0x0008F498
	public static bool ValidIndex(this IList list, int index)
	{
		return index >= 0 && index < list.Count;
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x000912A9 File Offset: 0x0008F4A9
	public static Vector3 Multiply(this Vector3 v, Vector3 v2)
	{
		return new Vector3(v.x * v2.x, v.y * v2.y, v.z * v2.z);
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x000912D8 File Offset: 0x0008F4D8
	public static int TryParseInt(this string str)
	{
		int result = 0;
		int.TryParse(str, out result);
		return result;
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x000912F4 File Offset: 0x0008F4F4
	public static float TryParseFloat(this string str)
	{
		float result = 0f;
		try
		{
			result = float.Parse(str, CultureInfo.InvariantCulture);
		}
		catch (Exception message)
		{
			UnityEngine.Debug.LogError("Couldn't parse " + str);
			UnityEngine.Debug.LogError(message);
			throw;
		}
		return result;
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x00091340 File Offset: 0x0008F540
	public static void Invoke(this MonoBehaviour mb, Action f, float delay)
	{
		mb.StartCoroutine(ExtensionMethods.InvokeRoutine(f, delay));
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x00091350 File Offset: 0x0008F550
	private static IEnumerator InvokeRoutine(Action f, float delay)
	{
		yield return new WaitForSeconds(delay);
		f();
		yield break;
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x00091368 File Offset: 0x0008F568
	public static Vector3 TransformDirection(this Vector3 inVector, Vector3 up, Vector3 forward)
	{
		Vector3 vector = Vector3.Cross(forward, up);
		Matrix4x4 matrix4x = new Matrix4x4(new Vector4(vector.x, up.x, forward.x, 0f), new Vector4(vector.y, up.y, forward.y, 0f), new Vector4(vector.z, up.z, forward.z, 0f), new Vector4(0f, 0f, 0f, 1f));
		return matrix4x.MultiplyVector(inVector);
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x000913FC File Offset: 0x0008F5FC
	public static string GetRelativePath(this Transform child, Transform root)
	{
		if (child == root)
		{
			return "";
		}
		string text = child.name;
		Transform transform = child;
		int num = 32;
		int num2 = 0;
		while (transform.parent != null && transform.parent != root && num2 < num)
		{
			num2++;
			transform = transform.parent;
			text = transform.name + "/" + text;
		}
		return text;
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x00091468 File Offset: 0x0008F668
	public static Transform FindInInstance(this Transform sourceChild, Transform sourceRoot, Transform instanceRoot)
	{
		if (sourceChild == null || sourceRoot == null || instanceRoot == null)
		{
			UnityEngine.Debug.LogError("FindInInstance: One or more parameters are null.");
			return null;
		}
		string relativePath = sourceChild.GetRelativePath(sourceRoot);
		if (string.IsNullOrEmpty(relativePath))
		{
			return instanceRoot;
		}
		Transform transform = instanceRoot.Find(relativePath);
		if (transform == null)
		{
			UnityEngine.Debug.LogWarning("FindInInstance: Could not find transform at relative path '" + relativePath + "' in the instance.");
		}
		return transform;
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x000914D4 File Offset: 0x0008F6D4
	public static T ConvertToEnum<T>(this string str) where T : Enum
	{
		string key = str.Replace(" ", "").TrimQuotes().ToLower();
		if (ExtensionMethods.EnumDict == null)
		{
			ExtensionMethods.EnumDict = new Dictionary<Type, Dictionary<string, int>>();
		}
		if (!ExtensionMethods.EnumDict.ContainsKey(typeof(T)))
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Array values = Enum.GetValues(typeof(T));
			string str2 = "";
			foreach (object obj in values)
			{
				dictionary.Add(obj.ToString().ToLower(), (int)obj);
				str2 = str2 + obj.ToString().ToLower() + ", ";
			}
			ExtensionMethods.EnumDict.Add(typeof(T), dictionary);
		}
		return (T)((object)ExtensionMethods.EnumDict[typeof(T)][key]);
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x000915EC File Offset: 0x0008F7EC
	public static string TrimQuotes(this string str)
	{
		if (str == null)
		{
			return str;
		}
		if (str.Length > 0 && str[0] == '"')
		{
			str = str.Substring(1, str.Length - 1);
		}
		if (str.Length > 0 && str[str.Length - 1] == '"')
		{
			str = str.Substring(0, str.Length - 1);
		}
		return str;
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x00091650 File Offset: 0x0008F850
	public static T GetCopyOf<T>(this Component comp, T other) where T : Component
	{
		Type type = comp.GetType();
		if (type != other.GetType())
		{
			return default(T);
		}
		BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		foreach (PropertyInfo propertyInfo in type.GetProperties(bindingAttr))
		{
			if (propertyInfo.CanWrite)
			{
				try
				{
					propertyInfo.SetValue(comp, propertyInfo.GetValue(other, null), null);
				}
				catch
				{
				}
			}
		}
		foreach (FieldInfo fieldInfo in type.GetFields(bindingAttr))
		{
			fieldInfo.SetValue(comp, fieldInfo.GetValue(other));
		}
		return comp as T;
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x0009171C File Offset: 0x0008F91C
	public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
	{
		return go.AddComponent<T>().GetCopyOf(toAdd);
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x00091730 File Offset: 0x0008F930
	public static List<TKey> GetKeys<TKey, TValue>(this Dictionary<TKey, TValue> dict)
	{
		List<TKey> list = new List<TKey>();
		foreach (KeyValuePair<TKey, TValue> keyValuePair in dict)
		{
			list.Add(keyValuePair.Key);
		}
		return list;
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x0009178C File Offset: 0x0008F98C
	public static List<T> GetAllComponents<T>(this GameObject go)
	{
		List<T> list = new List<T>();
		foreach (T t in go.GetComponents<T>())
		{
			if (t != null)
			{
				list.Add(t);
			}
		}
		foreach (T t2 in go.GetComponentsInChildren<T>(true))
		{
			if (t2 != null && !list.Contains(t2))
			{
				list.Add(t2);
			}
		}
		return list;
	}

	// Token: 0x060016F3 RID: 5875 RVA: 0x00091808 File Offset: 0x0008FA08
	public static T GetComponentSelfOrChild<T>(this GameObject go)
	{
		T component = go.GetComponent<T>();
		if (component != null)
		{
			return component;
		}
		return go.GetComponentInChildren<T>();
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x0009182C File Offset: 0x0008FA2C
	public static Quaternion SmoothRotate(this Quaternion start, Quaternion to, float speed, float angleLimit)
	{
		if (Quaternion.Angle(start, to) > angleLimit)
		{
			return Quaternion.RotateTowards(start, to, speed * Time.deltaTime * 57.29578f);
		}
		return Quaternion.Lerp(start, to, Time.deltaTime * speed);
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x0009185B File Offset: 0x0008FA5B
	public static bool IsNan(this Quaternion q)
	{
		return float.IsNaN(q.x) || float.IsNaN(q.y) || float.IsNaN(q.z) || float.IsNaN(q.w);
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x00091894 File Offset: 0x0008FA94
	public static T GetOrAddComponent<T>(this GameObject go) where T : Component
	{
		T t = go.GetComponent<T>();
		if (t == null || t.Equals(null))
		{
			t = go.AddComponent<T>();
		}
		return t;
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x000918CC File Offset: 0x0008FACC
	public static List<T> Clone<T>(this List<T> list)
	{
		if (list == null)
		{
			return null;
		}
		return new List<T>(list);
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x000918DC File Offset: 0x0008FADC
	public static void Remove<T>(this List<T> list, List<T> toRemove)
	{
		foreach (T item in toRemove)
		{
			list.Remove(item);
		}
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x0009192C File Offset: 0x0008FB2C
	public static string GetTypeHierarchy(this Type targetType)
	{
		string text = string.Empty;
		Type type = targetType;
		while (type != null)
		{
			text = (string.IsNullOrEmpty(text) ? type.Name : (type.Name + "::" + text));
			type = type.BaseType;
		}
		return text;
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x00091978 File Offset: 0x0008FB78
	public static List<T> GetTypes<T>(this T e) where T : Enum
	{
		List<T> list = new List<T>();
		foreach (object obj in Enum.GetValues(typeof(T)))
		{
			T item = (T)((object)obj);
			list.Add(item);
		}
		return list;
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x000919E4 File Offset: 0x0008FBE4
	public static void Move<T>(this List<T> list, T item, int index)
	{
		list.Remove(item);
		list.Insert(index, item);
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x000919F8 File Offset: 0x0008FBF8
	public static AudioClip GetRandomClip(this List<AudioClip> clips, int ignoreID = -1)
	{
		if (clips.Count == 0)
		{
			return null;
		}
		int num = UnityEngine.Random.Range(0, clips.Count);
		if (clips.Count > 1 && ignoreID >= 0 && ignoreID < clips.Count)
		{
			int num2 = 0;
			while (num == ignoreID && num2 < 10)
			{
				num2++;
				num = UnityEngine.Random.Range(0, clips.Count);
			}
		}
		return clips[num];
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x00091A58 File Offset: 0x0008FC58
	public static void Shuffle<T>(this IList<T> list, System.Random rng = null)
	{
		if (rng == null)
		{
			rng = new System.Random();
		}
		int i = list.Count;
		while (i > 1)
		{
			i--;
			int num = rng.Next(i + 1);
			int index = num;
			int index2 = i;
			T value = list[i];
			T value2 = list[num];
			list[index] = value;
			list[index2] = value2;
		}
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x00091ABC File Offset: 0x0008FCBC
	public static T GetRandomElement<T>(this IList<T> list, System.Random rng = null)
	{
		if (list == null || list.Count <= 0)
		{
			return default(T);
		}
		if (rng == null)
		{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		return list[rng.Next(list.Count)];
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x00091B07 File Offset: 0x0008FD07
	public static float FollowWorldObject(this RectTransform rect, Transform WorldTransform, RectTransform canvas, int index, float edgeOffset = 0f)
	{
		return rect.FollowWorldPoint(WorldTransform.position, canvas, index, edgeOffset);
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x00091B1C File Offset: 0x0008FD1C
	public static float FollowWorldPoint(this RectTransform rect, Vector3 WorldPoint, RectTransform canvas, int index, float edgeOffset = 0f)
	{
		if (PlayerControl.MyCamera == null)
		{
			return 1f;
		}
		Vector3 vector = PlayerControl.MyCamera.WorldToViewportPoint(WorldPoint);
		float num = (float)index * 0.03f;
		vector = new Vector3(Mathf.Clamp(vector.x, 0f, 1f), Mathf.Clamp(vector.y, 0f, 1f), vector.z);
		Transform transform = PlayerControl.MyCamera.transform;
		float num2 = Vector3.Dot(transform.forward, (WorldPoint - transform.position).normalized);
		if (vector.z < 0f)
		{
			vector.x = (float)((vector.x > 0.5f) ? 0 : 1);
			vector.y = 0.3f + num;
		}
		else if (num2 < 0.5f + num && (vector.x == 0f || vector.x == 1f))
		{
			vector.y = Mathf.Lerp(0.3f + num, vector.y, (num2 - (0.3f + num)) / 0.2f);
		}
		Vector2 sizeDelta = canvas.sizeDelta;
		Vector2 vector2 = new Vector2(vector.x * sizeDelta.x - sizeDelta.x * 0.5f, vector.y * sizeDelta.y - sizeDelta.y * 0.5f);
		if (edgeOffset > 0f)
		{
			float num3 = edgeOffset * (sizeDelta.x / 2f);
			float num4 = edgeOffset * (sizeDelta.y / 2f);
			vector2.x = Mathf.Clamp(vector2.x, -sizeDelta.x * 0.5f + num3, sizeDelta.x * 0.5f - num3);
			vector2.y = Mathf.Clamp(vector2.y, -sizeDelta.y * 0.5f + num4, sizeDelta.y * 0.5f - num4);
		}
		float result = Mathf.Min(Mathf.Min(Mathf.Min(vector.x, vector.y) * 10f, 1f), Mathf.Min((1f - Mathf.Max(vector.x, vector.y)) * 10f, 1f));
		rect.anchoredPosition = vector2;
		return result;
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x00091D6C File Offset: 0x0008FF6C
	public static string GetString(this AnimationCurve curve)
	{
		string text = "";
		text = string.Concat(new string[]
		{
			text,
			((int)curve.preWrapMode).ToString(),
			"|",
			((int)curve.postWrapMode).ToString(),
			"|",
			curve.length.ToString(),
			"|"
		});
		for (int i = 0; i < curve.keys.Length; i++)
		{
			Keyframe keyframe = curve.keys[i];
			text = string.Concat(new string[]
			{
				text,
				keyframe.time.ToInvariantStr(),
				",",
				keyframe.value.ToInvariantStr(),
				",",
				((int)keyframe.weightedMode).ToString(),
				",",
				keyframe.inWeight.ToInvariantStr(),
				",",
				keyframe.inTangent.ToInvariantStr(),
				",",
				keyframe.outWeight.ToInvariantStr(),
				",",
				keyframe.outTangent.ToInvariantStr(),
				"|"
			});
		}
		return text.Substring(0, text.Length - 1);
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x00091ECC File Offset: 0x000900CC
	public static void LoadFromString(this AnimationCurve curve, string str)
	{
		string[] array = str.Split('|', StringSplitOptions.None);
		if (array.Length < 3)
		{
			return;
		}
		NumberStyles style = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
		CultureInfo invariantCulture = CultureInfo.InvariantCulture;
		int preWrapMode;
		int.TryParse(array[0], out preWrapMode);
		int postWrapMode;
		int.TryParse(array[1], out postWrapMode);
		int num;
		int.TryParse(array[2], out num);
		curve.preWrapMode = (WrapMode)preWrapMode;
		curve.postWrapMode = (WrapMode)postWrapMode;
		array = array.SubArray(3, array.Length - 3);
		List<Keyframe> list = new List<Keyframe>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string[] array3 = array2[i].Split(',', StringSplitOptions.None);
			Keyframe item = default(Keyframe);
			if (array3.Length == 7)
			{
				float num2 = 0f;
				int weightedMode = 0;
				float.TryParse(array3[0], style, invariantCulture, out num2);
				item.time = num2;
				float.TryParse(array3[1], style, invariantCulture, out num2);
				item.value = num2;
				int.TryParse(array3[2], style, invariantCulture, out weightedMode);
				item.weightedMode = (WeightedMode)weightedMode;
				float.TryParse(array3[3], style, invariantCulture, out num2);
				item.inWeight = num2;
				float.TryParse(array3[4], style, invariantCulture, out num2);
				item.inTangent = num2;
				float.TryParse(array3[5], style, invariantCulture, out num2);
				item.outWeight = num2;
				float.TryParse(array3[6], style, invariantCulture, out num2);
				item.outTangent = num2;
				list.Add(item);
			}
		}
		curve.keys = list.ToArray();
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x00092035 File Offset: 0x00090235
	public static string ToInvariantStr(this float f)
	{
		return f.ToString("0.#####", CultureInfo.InvariantCulture);
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x00092048 File Offset: 0x00090248
	public static void UpdateOpacity(this PostProcessVolume volume, bool shouldShow, float speed, float targetWeight = 1f)
	{
		if (volume.weight > 0f && !shouldShow)
		{
			volume.weight -= Time.deltaTime * speed;
			return;
		}
		if (volume.weight < targetWeight && shouldShow)
		{
			volume.weight += Time.deltaTime * speed * targetWeight;
		}
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x0009209D File Offset: 0x0009029D
	public static bool IsList(this Type type)
	{
		return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x000920BE File Offset: 0x000902BE
	public static string NetworkCompress(this string str)
	{
		return Convert.ToBase64String(ExtensionMethods.Compress(Encoding.UTF8.GetBytes(str)));
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x000920D8 File Offset: 0x000902D8
	public static string NetworkDecompress(this string str)
	{
		byte[] bytes = ExtensionMethods.Decompress(Convert.FromBase64String(str));
		return Encoding.UTF8.GetString(bytes);
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x000920FC File Offset: 0x000902FC
	private static byte[] Compress(byte[] bytes)
	{
		byte[] result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BrotliStream brotliStream = new BrotliStream(memoryStream, CompressionLevel.Optimal))
			{
				brotliStream.Write(bytes, 0, bytes.Length);
			}
			result = memoryStream.ToArray();
		}
		return result;
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x00092160 File Offset: 0x00090360
	private static byte[] Decompress(byte[] bytes)
	{
		byte[] result;
		using (MemoryStream memoryStream = new MemoryStream(bytes))
		{
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (BrotliStream brotliStream = new BrotliStream(memoryStream, CompressionMode.Decompress))
				{
					brotliStream.CopyTo(memoryStream2);
				}
				result = memoryStream2.ToArray();
			}
		}
		return result;
	}

	// Token: 0x0600170A RID: 5898 RVA: 0x000921DC File Offset: 0x000903DC
	public static T Next<T>(this T src) where T : Enum
	{
		T[] array = (T[])Enum.GetValues(src.GetType());
		int num = Array.IndexOf<T>(array, src) + 1;
		if (array.Length != num)
		{
			return array[num];
		}
		return array[0];
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x00092220 File Offset: 0x00090420
	public static void ResetLocalTransform(this Transform t)
	{
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
		t.localScale = Vector3.one;
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x00092244 File Offset: 0x00090444
	public static InputActions.InputAction GetUIBinding(this PlayerAbilityType pType)
	{
		InputActions.InputAction result;
		switch (pType)
		{
		case PlayerAbilityType.Primary:
			result = InputActions.InputAction.Ability_Primary;
			break;
		case PlayerAbilityType.Secondary:
			result = InputActions.InputAction.Ability_Secondary;
			break;
		case PlayerAbilityType.Utility:
			result = InputActions.InputAction.Ability_Signature;
			break;
		case PlayerAbilityType.Movement:
			result = InputActions.InputAction.Ability_Movement;
			break;
		case PlayerAbilityType.Ghost:
			result = InputActions.InputAction.Ability_Primary;
			break;
		default:
			result = InputActions.InputAction.Jump;
			break;
		}
		return result;
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x0009228A File Offset: 0x0009048A
	public static float AtMost(this float f, float Max)
	{
		return Mathf.Min(f, Max);
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x00092293 File Offset: 0x00090493
	public static float AtLeast(this float f, float Min)
	{
		return Mathf.Max(f, Min);
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x0009229C File Offset: 0x0009049C
	public static int AtMost(this int f, int Max)
	{
		return Mathf.Min(f, Max);
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x000922A5 File Offset: 0x000904A5
	public static int AtLeast(this int f, int Min)
	{
		return Mathf.Max(f, Min);
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x000922B0 File Offset: 0x000904B0
	public static string ToNetString(this List<string> list)
	{
		string text = "";
		foreach (string str in list)
		{
			text = text + str + "|";
		}
		if (text.Length > 1)
		{
			string text2 = text;
			int length = text2.Length - 1 - 0;
			text = text2.Substring(0, length);
		}
		return text;
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x00092328 File Offset: 0x00090528
	public static void FromNetString(this List<string> list, string input)
	{
		string[] array = input.Split('|', StringSplitOptions.None);
		list.Clear();
		foreach (string item in array)
		{
			list.Add(item);
		}
	}

	// Token: 0x06001713 RID: 5907 RVA: 0x00092360 File Offset: 0x00090560
	public static void SetNavigation(this Selectable b, Selectable target, UIDirection dir, bool biDirectional = false)
	{
		if (target == null)
		{
			return;
		}
		Navigation navigation = b.navigation;
		Navigation navigation2 = target.navigation;
		switch (dir)
		{
		case UIDirection.Up:
			navigation.selectOnUp = target;
			if (biDirectional)
			{
				navigation2.selectOnDown = b;
			}
			break;
		case UIDirection.Down:
			navigation.selectOnDown = target;
			if (biDirectional)
			{
				navigation2.selectOnUp = b;
			}
			break;
		case UIDirection.Left:
			navigation.selectOnLeft = target;
			if (biDirectional)
			{
				navigation2.selectOnRight = b;
			}
			break;
		case UIDirection.Right:
			navigation.selectOnRight = target;
			if (biDirectional)
			{
				navigation2.selectOnLeft = b;
			}
			break;
		}
		b.navigation = navigation;
		if (biDirectional)
		{
			target.navigation = navigation2;
		}
	}

	// Token: 0x06001714 RID: 5908 RVA: 0x00092400 File Offset: 0x00090600
	public static int CountNewLines(this string input)
	{
		int num = 0;
		for (int i = 0; i < input.Length; i++)
		{
			if (input[i] == '\n')
			{
				num++;
			}
			else if (input[i] == '\r' && i + 1 < input.Length && input[i + 1] == '\n')
			{
				i++;
				num++;
			}
		}
		return num;
	}

	// Token: 0x06001715 RID: 5909 RVA: 0x00092460 File Offset: 0x00090660
	public static string ToRomanNumeral(this int number)
	{
		if (number < 0 || number > 3999)
		{
			return "NA";
		}
		if (number < 1)
		{
			return string.Empty;
		}
		if (number >= 1000)
		{
			return "M" + (number - 1000).ToRomanNumeral();
		}
		if (number >= 900)
		{
			return "CM" + (number - 900).ToRomanNumeral();
		}
		if (number >= 500)
		{
			return "D" + (number - 500).ToRomanNumeral();
		}
		if (number >= 400)
		{
			return "CD" + (number - 400).ToRomanNumeral();
		}
		if (number >= 100)
		{
			return "C" + (number - 100).ToRomanNumeral();
		}
		if (number >= 90)
		{
			return "XC" + (number - 90).ToRomanNumeral();
		}
		if (number >= 50)
		{
			return "L" + (number - 50).ToRomanNumeral();
		}
		if (number >= 40)
		{
			return "XL" + (number - 40).ToRomanNumeral();
		}
		if (number >= 10)
		{
			return "X" + (number - 10).ToRomanNumeral();
		}
		if (number >= 9)
		{
			return "IX" + (number - 9).ToRomanNumeral();
		}
		if (number >= 5)
		{
			return "V" + (number - 5).ToRomanNumeral();
		}
		if (number >= 4)
		{
			return "IV" + (number - 4).ToRomanNumeral();
		}
		if (number >= 1)
		{
			return "I" + (number - 1).ToRomanNumeral();
		}
		return "NA";
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x000925E5 File Offset: 0x000907E5
	public static string ToCommaSeparatedString(this int value)
	{
		if (value < 1000)
		{
			return value.ToString();
		}
		return string.Format("{0:N0}", value);
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x00092608 File Offset: 0x00090808
	public static Vector3 ClosestPointOnLine(Vector3 startPos, Vector3 end, Vector3 playerPt)
	{
		Vector3 vector = end - startPos;
		Vector3 lhs = playerPt - startPos;
		float magnitude = vector.magnitude;
		vector.Normalize();
		float num = Vector3.Dot(lhs, vector);
		num = Mathf.Clamp(num, 0f, magnitude);
		return startPos + vector * num;
	}

	// Token: 0x040016E8 RID: 5864
	public static Dictionary<Type, Dictionary<string, int>> EnumDict;

	// Token: 0x020005FE RID: 1534
	[CompilerGenerated]
	private sealed class <InvokeRoutine>d__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026BA RID: 9914 RVA: 0x000D40A0 File Offset: 0x000D22A0
		[DebuggerHidden]
		public <InvokeRoutine>d__18(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000D40AF File Offset: 0x000D22AF
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000D40B4 File Offset: 0x000D22B4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(delay);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			f();
			return false;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000D4105 File Offset: 0x000D2305
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000D410D File Offset: 0x000D230D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000D4114 File Offset: 0x000D2314
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002966 RID: 10598
		private int <>1__state;

		// Token: 0x04002967 RID: 10599
		private object <>2__current;

		// Token: 0x04002968 RID: 10600
		public float delay;

		// Token: 0x04002969 RID: 10601
		public Action f;
	}
}
