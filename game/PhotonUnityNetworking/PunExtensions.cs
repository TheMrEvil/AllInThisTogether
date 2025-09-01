using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x0200001D RID: 29
	public static class PunExtensions
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00008F30 File Offset: 0x00007130
		public static ParameterInfo[] GetCachedParemeters(this MethodInfo mo)
		{
			ParameterInfo[] parameters;
			if (!PunExtensions.ParametersOfMethods.TryGetValue(mo, out parameters))
			{
				parameters = mo.GetParameters();
				PunExtensions.ParametersOfMethods[mo] = parameters;
			}
			return parameters;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008F60 File Offset: 0x00007160
		public static PhotonView[] GetPhotonViewsInChildren(this GameObject go)
		{
			return go.GetComponentsInChildren<PhotonView>(true);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008F69 File Offset: 0x00007169
		public static PhotonView GetPhotonView(this GameObject go)
		{
			return go.GetComponent<PhotonView>();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008F74 File Offset: 0x00007174
		public static bool AlmostEquals(this Vector3 target, Vector3 second, float sqrMagnitudePrecision)
		{
			return (target - second).sqrMagnitude < sqrMagnitudePrecision;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008F94 File Offset: 0x00007194
		public static bool AlmostEquals(this Vector2 target, Vector2 second, float sqrMagnitudePrecision)
		{
			return (target - second).sqrMagnitude < sqrMagnitudePrecision;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008FB3 File Offset: 0x000071B3
		public static bool AlmostEquals(this Quaternion target, Quaternion second, float maxAngle)
		{
			return Quaternion.Angle(target, second) < maxAngle;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008FBF File Offset: 0x000071BF
		public static bool AlmostEquals(this float target, float second, float floatDiff)
		{
			return Mathf.Abs(target - second) < floatDiff;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008FCC File Offset: 0x000071CC
		public static bool CheckIsAssignableFrom(this Type to, Type from)
		{
			return to.IsAssignableFrom(from);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008FD5 File Offset: 0x000071D5
		public static bool CheckIsInterface(this Type to)
		{
			return to.IsInterface;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008FDD File Offset: 0x000071DD
		// Note: this type is marked as 'beforefieldinit'.
		static PunExtensions()
		{
		}

		// Token: 0x040000B9 RID: 185
		public static Dictionary<MethodInfo, ParameterInfo[]> ParametersOfMethods = new Dictionary<MethodInfo, ParameterInfo[]>();
	}
}
