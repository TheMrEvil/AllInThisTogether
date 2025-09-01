using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIMSpace.Generating
{
	// Token: 0x0200005E RID: 94
	public static class FGenerators
	{
		// Token: 0x0600037D RID: 893 RVA: 0x00018827 File Offset: 0x00016A27
		public static GameObject InstantiateObject(GameObject obj)
		{
			return UnityEngine.Object.Instantiate<GameObject>(obj);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001882F File Offset: 0x00016A2F
		public static bool CheckIfIsNull(object o)
		{
			return o == null;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00018837 File Offset: 0x00016A37
		public static bool Exists(object o)
		{
			return FGenerators.CheckIfExist_NOTNULL(o);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001883F File Offset: 0x00016A3F
		public static bool NotNull(object o)
		{
			return FGenerators.CheckIfExist_NOTNULL(o);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00018847 File Offset: 0x00016A47
		public static bool IsNull(object o)
		{
			return FGenerators.CheckIfIsNull(o);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001884F File Offset: 0x00016A4F
		public static bool CheckIfExist_NOTNULL(object o)
		{
			return !FGenerators.CheckIfIsNull(o);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001885C File Offset: 0x00016A5C
		public static bool IsChildOf(Transform child, Transform rootParent)
		{
			Transform transform = child;
			while (transform != null && transform != rootParent)
			{
				transform = transform.parent;
			}
			return !(transform == null);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00018892 File Offset: 0x00016A92
		public static void DestroyObject(UnityEngine.Object obj)
		{
			if (obj == null)
			{
				return;
			}
			UnityEngine.Object.Destroy(obj);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000188A4 File Offset: 0x00016AA4
		public static bool RefIsNull(object varMat)
		{
			return FGenerators.CheckIfIsNull(varMat) || varMat == null || varMat == null || varMat.Equals(null);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000188C6 File Offset: 0x00016AC6
		// (set) Token: 0x06000387 RID: 903 RVA: 0x000188CD File Offset: 0x00016ACD
		public static int LatestSeed
		{
			[CompilerGenerated]
			get
			{
				return FGenerators.<LatestSeed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FGenerators.<LatestSeed>k__BackingField = value;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000188D5 File Offset: 0x00016AD5
		public static void SetSeed(int seed)
		{
			FGenerators.random = new System.Random(seed);
			FGenerators.LatestSeed = seed;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000188E8 File Offset: 0x00016AE8
		public static bool GetRandomFlip()
		{
			return FGenerators.GetRandom(0, 2) == 1;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000188F4 File Offset: 0x00016AF4
		public static float GetRandom()
		{
			return (float)FGenerators.random.NextDouble();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00018904 File Offset: 0x00016B04
		public static Vector2 SwampToBeRising(Vector2 minMax)
		{
			if (minMax.y < minMax.x)
			{
				float x = minMax.x;
				minMax.x = minMax.y;
				minMax.y = x;
			}
			return minMax;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001893C File Offset: 0x00016B3C
		public static float GetRandomSwap(float from, float to)
		{
			if (from > to)
			{
				float num = from;
				from = to;
				to = num;
			}
			return FGenerators.GetRandom(from, to);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001894F File Offset: 0x00016B4F
		public static float GetRandom(float plusminus)
		{
			return FGenerators.GetRandom(-plusminus, plusminus);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00018959 File Offset: 0x00016B59
		public static float GetRandom(float from, float to)
		{
			return from + (float)FGenerators.random.NextDouble() * (to - from);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001896C File Offset: 0x00016B6C
		public static Vector3 GetRandom(Vector3 plusMinusRangesPerAxis)
		{
			Vector3 vector = plusMinusRangesPerAxis;
			vector.x = FGenerators.GetRandom(-vector.x, vector.x);
			vector.y = FGenerators.GetRandom(-vector.y, vector.y);
			vector.z = FGenerators.GetRandom(-vector.z, vector.z);
			return vector;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000189C7 File Offset: 0x00016BC7
		public static int GetRandom(int from, int to)
		{
			return FGenerators.random.Next(from, to);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000189D5 File Offset: 0x00016BD5
		public static int GetRandom(MinMax minMax)
		{
			return (int)((float)minMax.Min + (float)FGenerators.random.NextDouble() * (float)(minMax.Max + 1 - minMax.Min));
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000189FC File Offset: 0x00016BFC
		public static void GetIncrementalTo<T>(List<T> list) where T : UnityEngine.Object
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000189FE File Offset: 0x00016BFE
		public static void GetSimilarTo<T>(List<T> list) where T : UnityEngine.Object
		{
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00018A00 File Offset: 0x00016C00
		public static int IndexOfFirstNumber(string name)
		{
			for (int i = 0; i < name.Length; i++)
			{
				int num;
				if (int.TryParse(name[i].ToString(), out num))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00018A3C File Offset: 0x00016C3C
		public static List<T> CopyList<T>(List<T> cellsInstructions)
		{
			List<T> list = new List<T>();
			for (int i = 0; i < cellsInstructions.Count; i++)
			{
				list.Add(cellsInstructions[i]);
			}
			return list;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00018A6E File Offset: 0x00016C6E
		public static bool IsRightMouseButton()
		{
			return Event.current != null && (Event.current.type == EventType.Used && Event.current.button == 1);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00018A97 File Offset: 0x00016C97
		public static string GetLastPath
		{
			get
			{
				return FGenerators.lastPath;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00018A9E File Offset: 0x00016C9E
		public static ScriptableObject GenerateScriptable(ScriptableObject reference, string exampleFilename = "", string playerPrefId = "LastFGenSaveDir")
		{
			return reference;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00018AA1 File Offset: 0x00016CA1
		public static void DrawScriptableModificatorList<T>(List<T> toDraw, GUIStyle style, string title, ref bool foldout, bool newButton = false, bool moveButtons = false, UnityEngine.Object toDirty = null, string first = "[Base]", string defaultFilename = "New Scriptable File") where T : ScriptableObject
		{
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00018AA3 File Offset: 0x00016CA3
		public static void AddScriptableToSimple(ScriptableObject parent, ScriptableObject toAdd, bool reload = true)
		{
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00018AA5 File Offset: 0x00016CA5
		public static bool AssetContainsAsset(UnityEngine.Object subAsset, UnityEngine.Object parentAsset)
		{
			return false;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00018AA8 File Offset: 0x00016CA8
		public static void AddScriptableTo(ScriptableObject toAdd, UnityEngine.Object parentAsset, bool checkIfAlreadyContains = true, bool reload = true)
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00018AAA File Offset: 0x00016CAA
		public static bool IsAssetSaved(UnityEngine.Object asset)
		{
			return false;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00018AB0 File Offset: 0x00016CB0
		public static void SwapElements<T>(List<T> list, int index1, int index2)
		{
			T value = list[index1];
			list[index1] = list[index2];
			list[index2] = value;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00018ADC File Offset: 0x00016CDC
		public static void SwapElements<T>(T[] list, int index1, int index2)
		{
			T t = list[index1];
			list[index1] = list[index2];
			list[index2] = t;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00018B08 File Offset: 0x00016D08
		public static void CheckForNulls<T>(List<T> classes)
		{
			for (int i = classes.Count - 1; i >= 0; i--)
			{
				if (classes[i] == null)
				{
					classes.RemoveAt(i);
				}
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00018B3D File Offset: 0x00016D3D
		public static bool IndexInListRange<T>(List<T> list, int index)
		{
			return list != null && index >= 0 && index < list.Count;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00018B58 File Offset: 0x00016D58
		public static T GetListElementOrNull<T>(this List<T> list, int index) where T : class
		{
			if (list == null)
			{
				return default(T);
			}
			if (index < 0)
			{
				return default(T);
			}
			if (index >= list.Count)
			{
				return default(T);
			}
			if (FGenerators.CheckIfIsNull(list[index]))
			{
				return default(T);
			}
			return list[index];
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00018BB8 File Offset: 0x00016DB8
		public static void AdjustCount<T>(List<T> list, int targetCount, bool addNulls = false) where T : class, new()
		{
			if (list.Count == targetCount)
			{
				return;
			}
			if (list.Count >= targetCount)
			{
				while (list.Count > targetCount)
				{
					list.RemoveAt(list.Count - 1);
				}
				return;
			}
			if (addNulls)
			{
				while (list.Count < targetCount)
				{
					list.Add(default(T));
				}
				return;
			}
			while (list.Count < targetCount)
			{
				list.Add(Activator.CreateInstance<T>());
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00018C22 File Offset: 0x00016E22
		public static float EditorUIScale
		{
			get
			{
				return FGenerators.GetEditorUIScale();
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00018C29 File Offset: 0x00016E29
		public static float GetEditorUIScale()
		{
			return 1f;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00018C30 File Offset: 0x00016E30
		// Note: this type is marked as 'beforefieldinit'.
		static FGenerators()
		{
		}

		// Token: 0x040002F1 RID: 753
		private static System.Random random = new System.Random();

		// Token: 0x040002F2 RID: 754
		[CompilerGenerated]
		private static int <LatestSeed>k__BackingField;

		// Token: 0x040002F3 RID: 755
		public static string lastPath = "";

		// Token: 0x040002F4 RID: 756
		private static float _editorUiScaling;

		// Token: 0x020001B1 RID: 433
		public class DefinedRandom
		{
			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00062CA0 File Offset: 0x00060EA0
			// (set) Token: 0x06000F3B RID: 3899 RVA: 0x00062CA8 File Offset: 0x00060EA8
			public int Seed
			{
				[CompilerGenerated]
				get
				{
					return this.<Seed>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Seed>k__BackingField = value;
				}
			}

			// Token: 0x06000F3C RID: 3900 RVA: 0x00062CB1 File Offset: 0x00060EB1
			public DefinedRandom(int seed)
			{
				this.random = new System.Random(seed);
			}

			// Token: 0x06000F3D RID: 3901 RVA: 0x00062CC5 File Offset: 0x00060EC5
			public float GetRandom()
			{
				return (float)this.random.NextDouble();
			}

			// Token: 0x06000F3E RID: 3902 RVA: 0x00062CD3 File Offset: 0x00060ED3
			public float GetRandom(float from, float to)
			{
				return from + (float)this.random.NextDouble() * (to - from);
			}

			// Token: 0x06000F3F RID: 3903 RVA: 0x00062CE7 File Offset: 0x00060EE7
			public int GetRandom(int from, int to)
			{
				return this.random.Next(from, to);
			}

			// Token: 0x06000F40 RID: 3904 RVA: 0x00062CF6 File Offset: 0x00060EF6
			public int GetRandom(MinMax minMax)
			{
				return (int)((float)minMax.Min + (float)this.random.NextDouble() * (float)(minMax.Max + 1 - minMax.Min));
			}

			// Token: 0x04000D63 RID: 3427
			[CompilerGenerated]
			private int <Seed>k__BackingField;

			// Token: 0x04000D64 RID: 3428
			private System.Random random;
		}
	}
}
