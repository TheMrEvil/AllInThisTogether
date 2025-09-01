using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x0200003A RID: 58
	public static class FEngineering
	{
		// Token: 0x06000103 RID: 259 RVA: 0x0000A472 File Offset: 0x00008672
		public static bool VIsZero(this Vector3 vec)
		{
			return vec.sqrMagnitude == 0f;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000A485 File Offset: 0x00008685
		public static bool VIsSame(this Vector3 vec1, Vector3 vec2)
		{
			return vec1.x == vec2.x && vec1.y == vec2.y && vec1.z == vec2.z;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public static Vector3 TransformVector(this Quaternion parentRot, Vector3 parentLossyScale, Vector3 childLocalPos)
		{
			return parentRot * Vector3.Scale(childLocalPos, parentLossyScale);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public static Vector3 TransformInDirection(this Quaternion childRotation, Vector3 parentLossyScale, Vector3 childLocalPos)
		{
			return childRotation * Vector3.Scale(childLocalPos, new Vector3((float)((parentLossyScale.x > 0f) ? 1 : -1), (float)((parentLossyScale.y > 0f) ? 1 : -1), (float)((parentLossyScale.y > 0f) ? 1 : -1)));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000A51C File Offset: 0x0000871C
		public static Vector3 InverseTransformVector(this Quaternion tRotation, Vector3 tLossyScale, Vector3 worldPos)
		{
			worldPos = Quaternion.Inverse(tRotation) * worldPos;
			return new Vector3(worldPos.x / tLossyScale.x, worldPos.y / tLossyScale.y, worldPos.z / tLossyScale.z);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000A558 File Offset: 0x00008758
		public static Vector3 VAxis2DLimit(this Transform parent, Vector3 parentPos, Vector3 childPos, int axis = 3)
		{
			if (axis == 3)
			{
				FEngineering.axis2DProjection.SetNormalAndPosition(parent.forward, parentPos);
			}
			else if (axis == 2)
			{
				FEngineering.axis2DProjection.SetNormalAndPosition(parent.up, parentPos);
			}
			else
			{
				FEngineering.axis2DProjection.SetNormalAndPosition(parent.right, parentPos);
			}
			return FEngineering.axis2DProjection.normal * FEngineering.axis2DProjection.GetDistanceToPoint(childPos);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000A5BE File Offset: 0x000087BE
		public static Quaternion QToLocal(this Quaternion parentRotation, Quaternion worldRotation)
		{
			return Quaternion.Inverse(parentRotation) * worldRotation;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000A5CC File Offset: 0x000087CC
		public static Quaternion QToWorld(this Quaternion parentRotation, Quaternion localRotation)
		{
			return parentRotation * localRotation;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000A5D5 File Offset: 0x000087D5
		public static Quaternion QRotateChild(this Quaternion offset, Quaternion parentRot, Quaternion childLocalRot)
		{
			return offset * parentRot * childLocalRot;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000A5E4 File Offset: 0x000087E4
		public static Quaternion ClampRotation(this Vector3 current, Vector3 bounds)
		{
			FEngineering.WrapVector(current);
			if (current.x < -bounds.x)
			{
				current.x = -bounds.x;
			}
			else if (current.x > bounds.x)
			{
				current.x = bounds.x;
			}
			if (current.y < -bounds.y)
			{
				current.y = -bounds.y;
			}
			else if (current.y > bounds.y)
			{
				current.y = bounds.y;
			}
			if (current.z < -bounds.z)
			{
				current.z = -bounds.z;
			}
			else if (current.z > bounds.z)
			{
				current.z = bounds.z;
			}
			return Quaternion.Euler(current);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000A6AC File Offset: 0x000088AC
		public static Vector3 QToAngularVelocity(this Quaternion deltaRotation, bool fix = false)
		{
			float num;
			Vector3 vector;
			deltaRotation.ToAngleAxis(out num, out vector);
			if (num == 0f)
			{
				return Vector3.zero;
			}
			num = Mathf.DeltaAngle(0f, num);
			vector *= num * 0.017453292f;
			if (fix)
			{
				vector /= Time.fixedDeltaTime;
			}
			if (float.IsNaN(vector.x))
			{
				return Vector3.zero;
			}
			if (float.IsNaN(vector.y))
			{
				return Vector3.zero;
			}
			if (float.IsNaN(vector.z))
			{
				return Vector3.zero;
			}
			return vector;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000A737 File Offset: 0x00008937
		public static Vector3 QToAngularVelocity(this Quaternion currentRotation, Quaternion targetRotation, bool fix = false)
		{
			return (targetRotation * Quaternion.Inverse(currentRotation)).QToAngularVelocity(fix);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000A74B File Offset: 0x0000894B
		public static bool QIsZero(this Quaternion rot)
		{
			return rot.x == 0f && rot.y == 0f && rot.z == 0f;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000A77C File Offset: 0x0000897C
		public static bool QIsSame(this Quaternion rot1, Quaternion rot2)
		{
			return rot1.x == rot2.x && rot1.y == rot2.y && rot1.z == rot2.z && rot1.w == rot2.w;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000A7CA File Offset: 0x000089CA
		public static float WrapAngle(float angle)
		{
			angle %= 360f;
			if (angle > 180f)
			{
				return angle - 360f;
			}
			return angle;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000A7E6 File Offset: 0x000089E6
		public static Vector3 WrapVector(Vector3 angles)
		{
			return new Vector3(FEngineering.WrapAngle(angles.x), FEngineering.WrapAngle(angles.y), FEngineering.WrapAngle(angles.z));
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000A80E File Offset: 0x00008A0E
		public static float UnwrapAngle(float angle)
		{
			if (angle >= 0f)
			{
				return angle;
			}
			angle = -angle % 360f;
			return 360f - angle;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000A82B File Offset: 0x00008A2B
		public static Vector3 UnwrapVector(Vector3 angles)
		{
			return new Vector3(FEngineering.UnwrapAngle(angles.x), FEngineering.UnwrapAngle(angles.y), FEngineering.UnwrapAngle(angles.z));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000A853 File Offset: 0x00008A53
		public static Quaternion SmoothDampRotation(this Quaternion current, Quaternion target, ref Quaternion velocityRef, float duration, float delta)
		{
			return current.SmoothDampRotation(target, ref velocityRef, duration, float.PositiveInfinity, delta);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000A868 File Offset: 0x00008A68
		public static Quaternion SmoothDampRotation(this Quaternion current, Quaternion target, ref Quaternion velocityRef, float duration, float maxSpeed, float delta)
		{
			float num = (Quaternion.Dot(current, target) > 0f) ? 1f : -1f;
			target.x *= num;
			target.y *= num;
			target.z *= num;
			target.w *= num;
			Vector4 normalized = new Vector4(Mathf.SmoothDamp(current.x, target.x, ref velocityRef.x, duration, maxSpeed, delta), Mathf.SmoothDamp(current.y, target.y, ref velocityRef.y, duration, maxSpeed, delta), Mathf.SmoothDamp(current.z, target.z, ref velocityRef.z, duration, maxSpeed, delta), Mathf.SmoothDamp(current.w, target.w, ref velocityRef.w, duration, maxSpeed, delta)).normalized;
			Vector4 vector = Vector4.Project(new Vector4(velocityRef.x, velocityRef.y, velocityRef.z, velocityRef.w), normalized);
			velocityRef.x -= vector.x;
			velocityRef.y -= vector.y;
			velocityRef.z -= vector.z;
			velocityRef.w -= vector.w;
			return new Quaternion(normalized.x, normalized.y, normalized.z, normalized.w);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public static float PerlinNoise3D(float x, float y, float z)
		{
			y += 1f;
			z += 2f;
			float num = Mathf.Sin(3.1415927f * Mathf.PerlinNoise(x, y));
			float num2 = Mathf.Sin(3.1415927f * Mathf.PerlinNoise(x, z));
			float num3 = Mathf.Sin(3.1415927f * Mathf.PerlinNoise(y, z));
			float num4 = Mathf.Sin(3.1415927f * Mathf.PerlinNoise(y, x));
			float num5 = Mathf.Sin(3.1415927f * Mathf.PerlinNoise(z, x));
			float num6 = Mathf.Sin(3.1415927f * Mathf.PerlinNoise(z, y));
			return num * num2 * num3 * num4 * num5 * num6;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000AA5C File Offset: 0x00008C5C
		public static float PerlinNoise3D(Vector3 pos)
		{
			return FEngineering.PerlinNoise3D(pos.x, pos.y, pos.z);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000AA75 File Offset: 0x00008C75
		public static bool SameDirection(this float a, float b)
		{
			return (a > 0f && b > 0f) || (a < 0f && b < 0f);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000AA9C File Offset: 0x00008C9C
		public static float PointDisperse01(int index, int baseV = 2)
		{
			float num = 0f;
			float num2 = 1f / (float)baseV;
			int i = index;
			while (i > 0)
			{
				num += num2 * (float)(i % baseV);
				i = Mathf.FloorToInt((float)(i / baseV));
				num2 /= (float)baseV;
			}
			return num;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000AADC File Offset: 0x00008CDC
		public static float PointDisperse(int index, int baseV = 2)
		{
			float num = 0f;
			float num2 = 1f / (float)baseV;
			int i = index;
			while (i > 0)
			{
				num += num2 * (float)(i % baseV);
				i = Mathf.FloorToInt((float)(i / baseV));
				num2 /= (float)baseV;
			}
			return num - 0.5f;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000AB20 File Offset: 0x00008D20
		public static float GetScaler(this Transform transform)
		{
			float result;
			if (transform.lossyScale.x > transform.lossyScale.y)
			{
				if (transform.lossyScale.y > transform.lossyScale.z)
				{
					result = transform.lossyScale.y;
				}
				else
				{
					result = transform.lossyScale.z;
				}
			}
			else
			{
				result = transform.lossyScale.x;
			}
			return result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000AB86 File Offset: 0x00008D86
		public static Vector3 PosFromMatrix(this Matrix4x4 m)
		{
			return m.GetColumn(3);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000AB95 File Offset: 0x00008D95
		public static Quaternion RotFromMatrix(this Matrix4x4 m)
		{
			return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		public static Vector3 ScaleFromMatrix(this Matrix4x4 m)
		{
			return new Vector3(m.GetColumn(0).magnitude, m.GetColumn(1).magnitude, m.GetColumn(2).magnitude);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000ABFA File Offset: 0x00008DFA
		public static Bounds TransformBounding(Bounds b, Transform by)
		{
			return FEngineering.TransformBounding(b, by.localToWorldMatrix);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000AC08 File Offset: 0x00008E08
		public static Bounds TransformBounding(Bounds b, Matrix4x4 mx)
		{
			Vector3 vector = mx.MultiplyPoint(b.min);
			Vector3 point = mx.MultiplyPoint(b.max);
			Vector3 point2 = mx.MultiplyPoint(new Vector3(b.max.x, b.center.y, b.min.z));
			Vector3 point3 = mx.MultiplyPoint(new Vector3(b.min.x, b.center.y, b.max.z));
			b = new Bounds(vector, Vector3.zero);
			b.Encapsulate(vector);
			b.Encapsulate(point);
			b.Encapsulate(point2);
			b.Encapsulate(point3);
			return b;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		public static Bounds RotateBoundsByMatrix(this Bounds b, Quaternion rotation)
		{
			if (rotation.QIsZero())
			{
				return b;
			}
			Matrix4x4 matrix4x = Matrix4x4.Rotate(rotation);
			Bounds result = default(Bounds);
			Vector3 point = matrix4x.MultiplyPoint(new Vector3(b.max.x, b.min.y, b.max.z));
			Vector3 point2 = matrix4x.MultiplyPoint(new Vector3(b.max.x, b.min.y, b.min.z));
			Vector3 point3 = matrix4x.MultiplyPoint(new Vector3(b.min.x, b.min.y, b.min.z));
			Vector3 point4 = matrix4x.MultiplyPoint(new Vector3(b.min.x, b.min.y, b.max.z));
			result.Encapsulate(point);
			result.Encapsulate(point2);
			result.Encapsulate(point3);
			result.Encapsulate(point4);
			Vector3 point5 = matrix4x.MultiplyPoint(new Vector3(b.max.x, b.max.y, b.max.z));
			Vector3 point6 = matrix4x.MultiplyPoint(new Vector3(b.max.x, b.max.y, b.min.z));
			Vector3 point7 = matrix4x.MultiplyPoint(new Vector3(b.min.x, b.max.y, b.min.z));
			Vector3 point8 = matrix4x.MultiplyPoint(new Vector3(b.min.x, b.max.y, b.max.z));
			result.Encapsulate(point5);
			result.Encapsulate(point6);
			result.Encapsulate(point7);
			result.Encapsulate(point8);
			return result;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public static Bounds RotateLocalBounds(this Bounds b, Quaternion rotation)
		{
			float num = Quaternion.Angle(rotation, Quaternion.identity);
			if (num > 45f && num < 135f)
			{
				b.size = new Vector3(b.size.z, b.size.y, b.size.x);
			}
			if (num < 315f && num > 225f)
			{
				b.size = new Vector3(b.size.z, b.size.y, b.size.x);
			}
			return b;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000AF5C File Offset: 0x0000915C
		public static int[] GetLayermaskValues(int mask, int optionsCount)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < optionsCount; i++)
			{
				int num = 1 << i;
				if ((mask & num) != 0)
				{
					list.Add(i);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000AF94 File Offset: 0x00009194
		public static LayerMask GetLayerMaskUsingPhysicsProjectSettingsMatrix(int maskForLayer)
		{
			LayerMask layerMask = 0;
			for (int i = 0; i < 32; i++)
			{
				if (!Physics.GetIgnoreLayerCollision(maskForLayer, i))
				{
					layerMask |= 1 << i;
				}
			}
			return layerMask;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000AFD4 File Offset: 0x000091D4
		public static PhysicMaterial PMSliding
		{
			get
			{
				if (FEngineering._slidingMat)
				{
					return FEngineering._slidingMat;
				}
				FEngineering._slidingMat = new PhysicMaterial("Slide");
				FEngineering._slidingMat.frictionCombine = PhysicMaterialCombine.Minimum;
				FEngineering._slidingMat.dynamicFriction = 0f;
				FEngineering._slidingMat.staticFriction = 0f;
				return FEngineering._slidingMat;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000B030 File Offset: 0x00009230
		public static PhysicMaterial PMFrict
		{
			get
			{
				if (FEngineering._frictMat)
				{
					return FEngineering._frictMat;
				}
				FEngineering._frictMat = new PhysicMaterial("Friction");
				FEngineering._frictMat.frictionCombine = PhysicMaterialCombine.Maximum;
				FEngineering._frictMat.dynamicFriction = 10f;
				FEngineering._frictMat.staticFriction = 10f;
				return FEngineering._frictMat;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000B08C File Offset: 0x0000928C
		public static PhysicsMaterial2D PMSliding2D
		{
			get
			{
				if (FEngineering._slidingMat2D)
				{
					return FEngineering._slidingMat2D;
				}
				FEngineering._slidingMat2D = new PhysicsMaterial2D("Slide2D");
				FEngineering._slidingMat2D.friction = 0f;
				return FEngineering._slidingMat2D;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000B0C3 File Offset: 0x000092C3
		public static PhysicsMaterial2D PMFrict2D
		{
			get
			{
				if (FEngineering._frictMat2D)
				{
					return FEngineering._frictMat2D;
				}
				FEngineering._frictMat2D = new PhysicsMaterial2D("Friction2D");
				FEngineering._frictMat2D.friction = 5f;
				return FEngineering._frictMat2D;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000B0FA File Offset: 0x000092FA
		public static float DistanceTo_2D(Vector3 aPos, Vector3 bPos)
		{
			return Vector2.Distance(new Vector2(aPos.x, aPos.z), new Vector2(bPos.x, bPos.z));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000B123 File Offset: 0x00009323
		public static float DistanceTo_2DSqrt(Vector3 aPos, Vector3 bPos)
		{
			return Vector2.SqrMagnitude(new Vector2(aPos.x, aPos.z) - new Vector2(bPos.x, bPos.z));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000B154 File Offset: 0x00009354
		public static Vector2 GetAngleDirection2D(float angle)
		{
			float f = angle * 0.017453292f;
			return new Vector2(Mathf.Sin(f), Mathf.Cos(f));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000B17C File Offset: 0x0000937C
		public static Vector3 GetAngleDirection(float angle)
		{
			float f = angle * 0.017453292f;
			return new Vector3(Mathf.Sin(f), 0f, Mathf.Cos(f));
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000B1A7 File Offset: 0x000093A7
		public static Vector3 GetAngleDirectionXZ(float angle)
		{
			return FEngineering.GetAngleDirection(angle);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public static Vector3 GetAngleDirectionZX(float angle)
		{
			float f = angle * 0.017453292f;
			return new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000B1DC File Offset: 0x000093DC
		public static Vector3 GetAngleDirectionXY(float angle, float radOffset = 0f, float secAxisRadOffset = 0f)
		{
			float num = angle * 0.017453292f;
			return new Vector3(Mathf.Sin(num + radOffset), Mathf.Cos(num + secAxisRadOffset), 0f);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000B20C File Offset: 0x0000940C
		public static Vector3 GetAngleDirectionYX(float angle, float firstAxisRadOffset = 0f, float secAxisRadOffset = 0f)
		{
			float num = angle * 0.017453292f;
			return new Vector3(Mathf.Cos(num + secAxisRadOffset), Mathf.Sin(num + firstAxisRadOffset), 0f);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000B23C File Offset: 0x0000943C
		public static Vector3 GetAngleDirectionYZ(float angle)
		{
			float f = angle * 0.017453292f;
			return new Vector3(0f, Mathf.Sin(f), Mathf.Cos(f));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000B268 File Offset: 0x00009468
		public static Vector3 GetAngleDirectionZY(float angle)
		{
			float f = angle * 0.017453292f;
			return new Vector3(0f, Mathf.Cos(f), Mathf.Sin(f));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000B293 File Offset: 0x00009493
		public static Vector3 V2ToV3TopDown(Vector2 v)
		{
			return new Vector3(v.x, 0f, v.y);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000B2AB File Offset: 0x000094AB
		public static Vector2 V3ToV2(Vector3 a)
		{
			return new Vector2(a.x, a.z);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000B2BE File Offset: 0x000094BE
		public static Vector2 V3TopDownDiff(Vector3 target, Vector3 me)
		{
			return FEngineering.V3ToV2(target) - FEngineering.V3ToV2(me);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000B2D1 File Offset: 0x000094D1
		public static float GetAngleDeg(Vector3 v)
		{
			return FEngineering.GetAngleDeg(v.x, v.z);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000B2E4 File Offset: 0x000094E4
		public static float GetAngleDeg(Vector2 v)
		{
			return FEngineering.GetAngleDeg(v.x, v.y);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000B2F7 File Offset: 0x000094F7
		public static float GetAngleDeg(float x, float z)
		{
			return FEngineering.GetAngleRad(x, z) * 57.29578f;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000B306 File Offset: 0x00009506
		public static float GetAngleRad(float x, float z)
		{
			return Mathf.Atan2(x, z);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000B30F File Offset: 0x0000950F
		public static float Rnd(float val, int dec = 0)
		{
			if (dec <= 0)
			{
				return Mathf.Round(val);
			}
			return (float)Math.Round((double)val, dec);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000B328 File Offset: 0x00009528
		internal static float ManhattanTopDown2D(Vector3 probePos, Vector3 worldPosition)
		{
			float num = probePos.x - worldPosition.x;
			if (num < 0f)
			{
				num = -num;
			}
			float num2 = probePos.z - worldPosition.z;
			if (num2 < 0f)
			{
				num2 = -num2;
			}
			return num + num2;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000B36C File Offset: 0x0000956C
		internal static bool IsInSqureBounds2D(Vector3 probePos, Vector3 boundsPos, float boundsRange)
		{
			return boundsRange > 0f && (probePos.x > boundsPos.x - boundsRange && probePos.x < boundsPos.x + boundsRange && probePos.z > boundsPos.z - boundsRange && probePos.z < boundsPos.z + boundsRange);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000B3C8 File Offset: 0x000095C8
		internal static bool IsInSqureBounds2D(Vector3 boundsAPos, float boundsAHalfRange, Vector3 boundsBPos, float boundsBHRange)
		{
			return boundsAPos.x - boundsAHalfRange <= boundsBPos.x + boundsBHRange && boundsAPos.x + boundsAHalfRange >= boundsBPos.x - boundsBHRange && boundsAPos.z - boundsAHalfRange <= boundsBPos.z + boundsBHRange && boundsAPos.z + boundsAHalfRange >= boundsBPos.z - boundsBHRange;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000B422 File Offset: 0x00009622
		internal static Vector3 GetDirectionTowards(Vector3 me, Vector3 target)
		{
			return new Vector3(target.x - me.x, 0f, target.z - me.z);
		}

		// Token: 0x040001C4 RID: 452
		private static Plane axis2DProjection;

		// Token: 0x040001C5 RID: 453
		private static PhysicMaterial _slidingMat;

		// Token: 0x040001C6 RID: 454
		private static PhysicMaterial _frictMat;

		// Token: 0x040001C7 RID: 455
		private static PhysicsMaterial2D _slidingMat2D;

		// Token: 0x040001C8 RID: 456
		private static PhysicsMaterial2D _frictMat2D;
	}
}
