using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[Obsolete("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.", true)]
	[ExcludeFromPreset]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class ProceduralMaterial : Material
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static void FeatureRemoved()
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
		internal ProceduralMaterial() : base(null)
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		public ProceduralPropertyDescription[] GetProceduralPropertyDescriptions()
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002050 File Offset: 0x00000250
		public bool HasProceduralProperty(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		public bool GetProceduralBoolean(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002050 File Offset: 0x00000250
		public bool IsProceduralPropertyVisible(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000206E File Offset: 0x0000026E
		public void SetProceduralBoolean(string inputName, bool value)
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002050 File Offset: 0x00000250
		public float GetProceduralFloat(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000206E File Offset: 0x0000026E
		public void SetProceduralFloat(string inputName, float value)
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002050 File Offset: 0x00000250
		public Vector4 GetProceduralVector(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000206E File Offset: 0x0000026E
		public void SetProceduralVector(string inputName, Vector4 value)
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002050 File Offset: 0x00000250
		public Color GetProceduralColor(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000206E File Offset: 0x0000026E
		public void SetProceduralColor(string inputName, Color value)
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002050 File Offset: 0x00000250
		public int GetProceduralEnum(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000206E File Offset: 0x0000026E
		public void SetProceduralEnum(string inputName, int value)
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002050 File Offset: 0x00000250
		public Texture2D GetProceduralTexture(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000206E File Offset: 0x0000026E
		public void SetProceduralTexture(string inputName, Texture2D value)
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002050 File Offset: 0x00000250
		public string GetProceduralString(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000206E File Offset: 0x0000026E
		public void SetProceduralString(string inputName, string value)
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002050 File Offset: 0x00000250
		public bool IsProceduralPropertyCached(string inputName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000206E File Offset: 0x0000026E
		public void CacheProceduralProperty(string inputName, bool value)
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000206E File Offset: 0x0000026E
		public void ClearCache()
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000206E File Offset: 0x0000026E
		public ProceduralCacheSize cacheSize
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
			set
			{
				ProceduralMaterial.FeatureRemoved();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000206E File Offset: 0x0000026E
		public int animationUpdateRate
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
			set
			{
				ProceduralMaterial.FeatureRemoved();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000206E File Offset: 0x0000026E
		public void RebuildTextures()
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000206E File Offset: 0x0000026E
		public void RebuildTexturesImmediately()
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002050 File Offset: 0x00000250
		public bool isProcessing
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000206E File Offset: 0x0000026E
		public static void StopRebuilds()
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002050 File Offset: 0x00000250
		public bool isCachedDataAvailable
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000206E File Offset: 0x0000026E
		public bool isLoadTimeGenerated
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
			set
			{
				ProceduralMaterial.FeatureRemoved();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002050 File Offset: 0x00000250
		public ProceduralLoadingBehavior loadingBehavior
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002050 File Offset: 0x00000250
		public static bool isSupported
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000206E File Offset: 0x0000026E
		public static ProceduralProcessorUsage substanceProcessorUsage
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
			set
			{
				ProceduralMaterial.FeatureRemoved();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000206E File Offset: 0x0000026E
		public string preset
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
			set
			{
				ProceduralMaterial.FeatureRemoved();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002050 File Offset: 0x00000250
		public Texture[] GetGeneratedTextures()
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002050 File Offset: 0x00000250
		public ProceduralTexture GetGeneratedTexture(string textureName)
		{
			throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000206E File Offset: 0x0000026E
		public bool isReadable
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
			set
			{
				ProceduralMaterial.FeatureRemoved();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000206E File Offset: 0x0000026E
		public void FreezeAndReleaseSourceData()
		{
			ProceduralMaterial.FeatureRemoved();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002050 File Offset: 0x00000250
		public bool isFrozen
		{
			get
			{
				throw new Exception("Built-in support for Substance Designer materials has been removed from Unity. To continue using Substance Designer materials, you will need to install Allegorithmic's external importer from the Asset Store.");
			}
		}
	}
}
