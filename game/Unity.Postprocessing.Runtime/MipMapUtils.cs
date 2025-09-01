using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public static class MipMapUtils
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static float CalculateMipMapBias(float renderWidth, float displayWidth, float mipmapBiasOverride)
	{
		return (Mathf.Log(renderWidth / displayWidth, 2f) - 1f) * mipmapBiasOverride;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002067 File Offset: 0x00000267
	public static void OnMipMapSingleTexture(Texture texture, float renderWidth, float displayWidth, float mipmapBiasOverride)
	{
		MipMapUtils._IsReset = false;
		MipMapUtils.OnMipMapSingleTexture(texture, MipMapUtils.CalculateMipMapBias(renderWidth, displayWidth, mipmapBiasOverride));
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000207D File Offset: 0x0000027D
	public static void OnMipMapSingleTexture(Texture texture, float mapmapBias)
	{
		MipMapUtils._IsReset = false;
		texture.mipMapBias = mapmapBias;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
	public static void OnMipMapAllTextures(float renderWidth, float displayWidth, float mipmapBiasOverride)
	{
		MipMapUtils._IsReset = false;
		MipMapUtils.OnMipMapAllTextures(MipMapUtils.CalculateMipMapBias(renderWidth, displayWidth, mipmapBiasOverride));
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020A4 File Offset: 0x000002A4
	public static void OnMipMapAllTextures(float mapmapBias)
	{
		MipMapUtils._IsReset = false;
		Texture[] array = Resources.FindObjectsOfTypeAll(typeof(Texture)) as Texture[];
		for (int i = 0; i < array.Length; i++)
		{
			array[i].mipMapBias = mapmapBias;
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020E4 File Offset: 0x000002E4
	public static void OnResetAllMipMaps()
	{
		if (!MipMapUtils._IsReset)
		{
			MipMapUtils._IsReset = true;
			Texture[] array = Resources.FindObjectsOfTypeAll(typeof(Texture)) as Texture[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].mipMapBias = 0f;
			}
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
	public static void AutoUpdateMipMaps(float renderWidth, float displayWidth, float mipMapBiasOverride, float updateFrequency, ref float prevMipMapBias, ref float mipMapTimer, ref ulong previousLength)
	{
		mipMapTimer += Time.deltaTime;
		MipMapUtils._IsReset = false;
		if (mipMapTimer > updateFrequency)
		{
			mipMapTimer = 0f;
			float num = MipMapUtils.CalculateMipMapBias(renderWidth, displayWidth, mipMapBiasOverride);
			if (previousLength != Texture.currentTextureMemory || prevMipMapBias != num)
			{
				prevMipMapBias = num;
				previousLength = Texture.currentTextureMemory;
				MipMapUtils.OnMipMapAllTextures(num);
			}
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002188 File Offset: 0x00000388
	// Note: this type is marked as 'beforefieldinit'.
	static MipMapUtils()
	{
	}

	// Token: 0x04000016 RID: 22
	private static bool _IsReset = true;
}
