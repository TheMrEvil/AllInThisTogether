using System;
using System.Collections.Generic;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000BE RID: 190
	public static class LayerMaskExtensions
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x0003965A File Offset: 0x0003785A
		public static bool Contains(LayerMask mask, int layer)
		{
			return mask == (mask | 1 << layer);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00039671 File Offset: 0x00037871
		public static LayerMask Create(params string[] layerNames)
		{
			return LayerMaskExtensions.NamesToMask(layerNames);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00039679 File Offset: 0x00037879
		public static LayerMask Create(params int[] layerNumbers)
		{
			return LayerMaskExtensions.LayerNumbersToMask(layerNumbers);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00039684 File Offset: 0x00037884
		public static LayerMask NamesToMask(params string[] layerNames)
		{
			LayerMask layerMask = 0;
			foreach (string layerName in layerNames)
			{
				layerMask |= 1 << LayerMask.NameToLayer(layerName);
			}
			return layerMask;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000396C8 File Offset: 0x000378C8
		public static LayerMask LayerNumbersToMask(params int[] layerNumbers)
		{
			LayerMask layerMask = 0;
			foreach (int num in layerNumbers)
			{
				layerMask |= 1 << num;
			}
			return layerMask;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00039704 File Offset: 0x00037904
		public static LayerMask Inverse(this LayerMask original)
		{
			return ~original;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00039712 File Offset: 0x00037912
		public static LayerMask AddToMask(this LayerMask original, params string[] layerNames)
		{
			return original | LayerMaskExtensions.NamesToMask(layerNames);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0003972B File Offset: 0x0003792B
		public static LayerMask RemoveFromMask(this LayerMask original, params string[] layerNames)
		{
			return ~(~original | LayerMaskExtensions.NamesToMask(layerNames));
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00039750 File Offset: 0x00037950
		public static string[] MaskToNames(this LayerMask original)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < 32; i++)
			{
				int num = 1 << i;
				if ((original & num) == num)
				{
					string text = LayerMask.LayerToName(i);
					if (!string.IsNullOrEmpty(text))
					{
						list.Add(text);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000397A0 File Offset: 0x000379A0
		public static int[] MaskToNumbers(this LayerMask original)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < 32; i++)
			{
				int num = 1 << i;
				if ((original & num) == num)
				{
					list.Add(i);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000397DF File Offset: 0x000379DF
		public static string MaskToString(this LayerMask original)
		{
			return original.MaskToString(", ");
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000397EC File Offset: 0x000379EC
		public static string MaskToString(this LayerMask original, string delimiter)
		{
			return string.Join(delimiter, original.MaskToNames());
		}
	}
}
