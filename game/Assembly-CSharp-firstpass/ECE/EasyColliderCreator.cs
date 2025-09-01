using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECE
{
	// Token: 0x02000069 RID: 105
	public class EasyColliderCreator
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x000207B0 File Offset: 0x0001E9B0
		private EasyColliderCreator.BestFitSphere CalculateBestFitSphere(List<Vector3> localVertices)
		{
			int count = localVertices.Count;
			float num3;
			float num2;
			float num = num2 = (num3 = 0f);
			foreach (Vector3 vector in localVertices)
			{
				num += vector.x;
				num3 += vector.y;
				num2 += vector.z;
			}
			num *= 1f / (float)count;
			num3 *= 1f / (float)count;
			num2 *= 1f / (float)count;
			Vector3 zero = Vector3.zero;
			Matrix4x4 zero2 = Matrix4x4.zero;
			zero2.m33 = 1f;
			foreach (Vector3 vector2 in localVertices)
			{
				ref Matrix4x4 ptr = ref zero2;
				ptr[0, 0] = ptr[0, 0] + 2f * (vector2.x * (vector2.x - num)) / (float)count;
				ptr = ref zero2;
				ptr[0, 1] = ptr[0, 1] + 2f * (vector2.x * (vector2.y - num3)) / (float)count;
				ptr = ref zero2;
				ptr[0, 2] = ptr[0, 2] + 2f * (vector2.x * (vector2.z - num2)) / (float)count;
				ptr = ref zero2;
				ptr[1, 0] = ptr[1, 0] + 2f * (vector2.y * (vector2.x - num)) / (float)count;
				ptr = ref zero2;
				ptr[1, 1] = ptr[1, 1] + 2f * (vector2.y * (vector2.y - num3)) / (float)count;
				ptr = ref zero2;
				ptr[1, 2] = ptr[1, 2] + 2f * (vector2.y * (vector2.z - num2)) / (float)count;
				ptr = ref zero2;
				ptr[2, 0] = ptr[2, 0] + 2f * (vector2.z * (vector2.x - num)) / (float)count;
				ptr = ref zero2;
				ptr[2, 1] = ptr[2, 1] + 2f * (vector2.z * (vector2.y - num3)) / (float)count;
				ptr = ref zero2;
				ptr[2, 2] = ptr[2, 2] + 2f * (vector2.z * (vector2.z - num2)) / (float)count;
				float num4 = vector2.x * vector2.x;
				float num5 = vector2.y * vector2.y;
				float num6 = vector2.z * vector2.z;
				zero.x += (num4 + num5 + num6) * (vector2.x - num) / (float)count;
				zero.y += (num4 + num5 + num6) * (vector2.y - num3) / (float)count;
				zero.z += (num4 + num5 + num6) * (vector2.z - num2) / (float)count;
			}
			Vector3 vector3 = (zero2.transpose * zero2).inverse * zero2.transpose * zero;
			float num7 = 0f;
			foreach (Vector3 vector4 in localVertices)
			{
				num7 += Mathf.Pow(vector4.x - vector3.x, 2f) + Mathf.Pow(vector4.y - vector3.y, 2f) + Mathf.Pow(vector4.z - vector3.z, 2f);
			}
			num7 = Mathf.Sqrt(num7 / (float)localVertices.Count);
			return new EasyColliderCreator.BestFitSphere(vector3, num7);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00020C00 File Offset: 0x0001EE00
		public BoxColliderData CalculateBox(List<Vector3> worldVertices, Transform attachTo, bool isRotated = false)
		{
			if (isRotated && worldVertices.Count < 3)
			{
				return new BoxColliderData();
			}
			if (worldVertices.Count < 2)
			{
				return new BoxColliderData();
			}
			Quaternion q = Quaternion.identity;
			List<Vector3> list = new List<Vector3>();
			Matrix4x4 matrix;
			if (isRotated && worldVertices.Count >= 3)
			{
				Vector3 vector = worldVertices[1] - worldVertices[0];
				Vector3 upwards = Vector3.Cross(vector, worldVertices[2] - worldVertices[1]);
				q = Quaternion.LookRotation(vector, upwards);
				matrix = Matrix4x4.TRS(attachTo.position, q, Vector3.one);
				for (int i = 0; i < worldVertices.Count; i++)
				{
					list.Add(matrix.inverse.MultiplyPoint3x4(worldVertices[i]));
				}
			}
			else
			{
				list = this.ToLocalVerts(attachTo, worldVertices);
				matrix = attachTo.localToWorldMatrix;
			}
			BoxColliderData boxColliderData = this.CalculateBoxLocal(list);
			boxColliderData.ColliderType = (isRotated ? CREATE_COLLIDER_TYPE.ROTATED_BOX : CREATE_COLLIDER_TYPE.BOX);
			boxColliderData.Matrix = matrix;
			return boxColliderData;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00020CF0 File Offset: 0x0001EEF0
		public BoxColliderData CalculateBoxLocal(List<Vector3> vertices)
		{
			float num3;
			float num2;
			float num = num2 = (num3 = float.PositiveInfinity);
			float num6;
			float num5;
			float num4 = num5 = (num6 = float.NegativeInfinity);
			foreach (Vector3 vector in vertices)
			{
				num = ((vector.x < num) ? vector.x : num);
				num4 = ((vector.x > num4) ? vector.x : num4);
				num3 = ((vector.y < num3) ? vector.y : num3);
				num6 = ((vector.y > num6) ? vector.y : num6);
				num2 = ((vector.z < num2) ? vector.z : num2);
				num5 = ((vector.z > num5) ? vector.z : num5);
			}
			Vector3 a = new Vector3(num4, num6, num5);
			Vector3 b = new Vector3(num, num3, num2);
			Vector3 size = a - b;
			Vector3 center = (a + b) / 2f;
			return new BoxColliderData
			{
				Center = center,
				ColliderType = CREATE_COLLIDER_TYPE.BOX,
				IsValid = true,
				Size = size
			};
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00020E30 File Offset: 0x0001F030
		public CapsuleColliderData CalculateCapsuleBestFit(List<Vector3> worldVertices, Transform attachTo, bool isRotated)
		{
			if (worldVertices.Count >= 3)
			{
				Quaternion q = Quaternion.identity;
				List<Vector3> list = new List<Vector3>();
				Matrix4x4 matrix;
				if (isRotated)
				{
					Vector3 vector = worldVertices[1] - worldVertices[0];
					Vector3 upwards = Vector3.Cross(vector, worldVertices[2] - worldVertices[1]);
					q = Quaternion.LookRotation(vector, upwards);
					matrix = Matrix4x4.TRS(attachTo.position, q, Vector3.one);
					for (int i = 0; i < worldVertices.Count; i++)
					{
						list.Add(matrix.inverse.MultiplyPoint3x4(worldVertices[i]));
					}
				}
				else
				{
					list = this.ToLocalVerts(attachTo, worldVertices);
					matrix = attachTo.localToWorldMatrix;
				}
				CapsuleColliderData capsuleColliderData = this.CalculateCapsuleBestFitLocal(list);
				capsuleColliderData.ColliderType = (isRotated ? CREATE_COLLIDER_TYPE.ROTATED_CAPSULE : CREATE_COLLIDER_TYPE.CAPSULE);
				capsuleColliderData.Matrix = matrix;
				return capsuleColliderData;
			}
			return new CapsuleColliderData();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00020F04 File Offset: 0x0001F104
		public CapsuleColliderData CalculateCapsuleBestFitLocal(List<Vector3> localVertices)
		{
			if (localVertices.Count < 3)
			{
				Debug.LogWarning("EasyColliderCreator: Too few vertices passed to calculate a best fit capsule collider.");
				return new CapsuleColliderData();
			}
			Vector3 vector = localVertices[0];
			Vector3 vector2 = localVertices[1];
			float height = Vector3.Distance(vector, vector2);
			float num = Mathf.Abs(vector2.x - vector.x);
			float num2 = Mathf.Abs(vector2.y - vector.y);
			float num3 = Mathf.Abs(vector2.z - vector.z);
			localVertices.RemoveAt(1);
			localVertices.RemoveAt(0);
			EasyColliderCreator.BestFitSphere bestFitSphere = this.CalculateBestFitSphere(localVertices);
			Vector3 center = bestFitSphere.Center;
			int direction;
			if (num > num2 && num > num3)
			{
				direction = 0;
				center.x = (vector2.x + vector.x) / 2f;
			}
			else if (num2 > num && num2 > num3)
			{
				direction = 1;
				center.y = (vector2.y + vector.y) / 2f;
			}
			else
			{
				direction = 2;
				center.z = (vector2.z + vector.z) / 2f;
			}
			return new CapsuleColliderData
			{
				Center = center,
				ColliderType = CREATE_COLLIDER_TYPE.CAPSULE,
				Direction = direction,
				Height = height,
				IsValid = true,
				Radius = bestFitSphere.Radius
			};
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00021048 File Offset: 0x0001F248
		public CapsuleColliderData CalculateCapsuleMinMax(List<Vector3> worldVertices, Transform attachTo, CAPSULE_COLLIDER_METHOD method, bool isRotated)
		{
			if (isRotated && worldVertices.Count < 3)
			{
				return new CapsuleColliderData();
			}
			if (worldVertices.Count < 2)
			{
				return new CapsuleColliderData();
			}
			List<Vector3> list = new List<Vector3>();
			Matrix4x4 matrix;
			if (isRotated && worldVertices.Count >= 3)
			{
				Vector3 vector = worldVertices[1] - worldVertices[0];
				Vector3 upwards = Vector3.Cross(vector, worldVertices[2] - worldVertices[1]);
				Quaternion q = Quaternion.LookRotation(vector, upwards);
				matrix = Matrix4x4.TRS(attachTo.position, q, Vector3.one);
				for (int i = 0; i < worldVertices.Count; i++)
				{
					list.Add(matrix.inverse.MultiplyPoint3x4(worldVertices[i]));
				}
			}
			else
			{
				list = this.ToLocalVerts(attachTo.transform, worldVertices);
				matrix = attachTo.localToWorldMatrix;
			}
			CapsuleColliderData capsuleColliderData = this.CalculateCapsuleMinMaxLocal(list, method);
			capsuleColliderData.ColliderType = (isRotated ? CREATE_COLLIDER_TYPE.ROTATED_CAPSULE : CREATE_COLLIDER_TYPE.CAPSULE);
			capsuleColliderData.Matrix = matrix;
			return capsuleColliderData;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0002113C File Offset: 0x0001F33C
		public CapsuleColliderData CalculateCapsuleMinMaxLocal(List<Vector3> localVertices, CAPSULE_COLLIDER_METHOD method)
		{
			Vector3 vector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			Vector3 vector2 = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
			foreach (Vector3 vector3 in localVertices)
			{
				vector.x = ((vector3.x < vector.x) ? vector3.x : vector.x);
				vector.y = ((vector3.y < vector.y) ? vector3.y : vector.y);
				vector.z = ((vector3.z < vector.z) ? vector3.z : vector.z);
				vector2.x = ((vector3.x > vector2.x) ? vector3.x : vector2.x);
				vector2.y = ((vector3.y > vector2.y) ? vector3.y : vector2.y);
				vector2.z = ((vector3.z > vector2.z) ? vector3.z : vector2.z);
			}
			float num = vector2.x - vector.x;
			float num2 = vector2.y - vector.y;
			float num3 = vector2.z - vector.z;
			Vector3 vector4 = (vector2 + vector) / 2f;
			int num4 = 0;
			float num5 = 0f;
			if (num > num2 && num > num3)
			{
				num4 = 0;
				num5 = num;
			}
			else if (num2 > num && num2 > num3)
			{
				num4 = 1;
				num5 = num2;
			}
			else
			{
				num4 = 2;
				num5 = num3;
			}
			float num6 = float.NegativeInfinity;
			Vector3 a = Vector3.zero;
			using (List<Vector3>.Enumerator enumerator = localVertices.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					a = enumerator.Current;
					if (num4 == 0)
					{
						a.x = vector4.x;
					}
					else if (num4 == 1)
					{
						a.y = vector4.y;
					}
					else if (num4 == 2)
					{
						a.z = vector4.z;
					}
					float num7 = Vector3.Distance(a, vector4);
					if (num7 > num6)
					{
						num6 = num7;
					}
				}
			}
			if (method == CAPSULE_COLLIDER_METHOD.MinMaxPlusRadius)
			{
				num5 += num6;
			}
			else if (method == CAPSULE_COLLIDER_METHOD.MinMaxPlusDiameter)
			{
				num5 += num6 * 2f;
			}
			return new CapsuleColliderData
			{
				Center = vector4,
				ColliderType = CREATE_COLLIDER_TYPE.CAPSULE,
				Direction = num4,
				Height = num5,
				IsValid = true,
				Radius = num6
			};
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000213F0 File Offset: 0x0001F5F0
		public MeshColliderData CalculateCylinderCollider(List<Vector3> worldVertices, Transform attachTo, int numberOfSides = 12, CYLINDER_ORIENTATION orientation = CYLINDER_ORIENTATION.Automatic, float cylinderOffset = 0f)
		{
			MeshColliderData meshColliderData = new MeshColliderData();
			List<Vector3> vertices = this.ToLocalVerts(attachTo, worldVertices);
			EasyColliderQuickHull easyColliderQuickHull = EasyColliderQuickHull.CalculateHull(this.CalculateCylinderPointsLocal(vertices, attachTo, numberOfSides, orientation, cylinderOffset));
			meshColliderData.ColliderType = CREATE_COLLIDER_TYPE.CONVEX_MESH;
			meshColliderData.ConvexMesh = easyColliderQuickHull.Result;
			if (easyColliderQuickHull.Result != null)
			{
				meshColliderData.IsValid = true;
			}
			meshColliderData.Matrix = attachTo.transform.localToWorldMatrix;
			return meshColliderData;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0002145C File Offset: 0x0001F65C
		public MeshColliderData CalculateCylinderColliderLocal(List<Vector3> vertices, int numberOfSides = 12, CYLINDER_ORIENTATION orientation = CYLINDER_ORIENTATION.Automatic, float cylinderOffset = 0f)
		{
			MeshColliderData meshColliderData = new MeshColliderData();
			EasyColliderQuickHull easyColliderQuickHull = EasyColliderQuickHull.CalculateHull(this.CalculateCylinderPointsLocal(vertices, null, numberOfSides, orientation, cylinderOffset));
			meshColliderData.ColliderType = CREATE_COLLIDER_TYPE.CONVEX_MESH;
			meshColliderData.ConvexMesh = easyColliderQuickHull.Result;
			if (easyColliderQuickHull.Result != null)
			{
				meshColliderData.IsValid = true;
			}
			meshColliderData.Matrix = default(Matrix4x4);
			return meshColliderData;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000214B8 File Offset: 0x0001F6B8
		public MeshColliderData CalculateMeshColliderQuickHullLocal(List<Vector3> localVertices)
		{
			MeshColliderData meshColliderData = new MeshColliderData();
			EasyColliderQuickHull easyColliderQuickHull = EasyColliderQuickHull.CalculateHull(localVertices);
			meshColliderData.ConvexMesh = easyColliderQuickHull.Result;
			if (easyColliderQuickHull.Result != null)
			{
				meshColliderData.ColliderType = CREATE_COLLIDER_TYPE.CONVEX_MESH;
				meshColliderData.IsValid = true;
			}
			return meshColliderData;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000214FC File Offset: 0x0001F6FC
		public SphereColliderData CalculateSphereBestFit(List<Vector3> worldVertices, Transform attachTo)
		{
			if (worldVertices.Count < 2)
			{
				return new SphereColliderData();
			}
			List<Vector3> localVertices = this.ToLocalVerts(attachTo, worldVertices);
			SphereColliderData sphereColliderData = this.CalculateSphereBestFitLocal(localVertices);
			sphereColliderData.Matrix = attachTo.localToWorldMatrix;
			return sphereColliderData;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00021534 File Offset: 0x0001F734
		public SphereColliderData CalculateSphereBestFitLocal(List<Vector3> localVertices)
		{
			EasyColliderCreator.BestFitSphere bestFitSphere = this.CalculateBestFitSphere(localVertices);
			return new SphereColliderData
			{
				Center = bestFitSphere.Center,
				ColliderType = CREATE_COLLIDER_TYPE.SPHERE,
				IsValid = true,
				Radius = bestFitSphere.Radius
			};
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00021574 File Offset: 0x0001F774
		public SphereColliderData CalculateSphereDistance(List<Vector3> worldVertices, Transform attachTo)
		{
			if (worldVertices.Count < 2)
			{
				return new SphereColliderData();
			}
			List<Vector3> localVertices = this.ToLocalVerts(attachTo, worldVertices);
			SphereColliderData sphereColliderData = this.CalculateSphereDistanceLocal(localVertices);
			sphereColliderData.Matrix = attachTo.localToWorldMatrix;
			return sphereColliderData;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000215AC File Offset: 0x0001F7AC
		public SphereColliderData CalculateSphereDistanceLocal(List<Vector3> localVertices)
		{
			bool flag = false;
			double num = (double)Time.realtimeSinceStartup;
			double num2 = 0.10000000149011612;
			Vector3 vector = Vector3.zero;
			Vector3 b = Vector3.zero;
			float num3 = float.NegativeInfinity;
			for (int i = 0; i < localVertices.Count; i++)
			{
				for (int j = i + 1; j < localVertices.Count; j++)
				{
					float num4 = Vector3.Distance(localVertices[i], localVertices[j]);
					if (num4 > num3)
					{
						num3 = num4;
						vector = localVertices[i];
						b = localVertices[j];
					}
				}
				if ((double)Time.realtimeSinceStartup - num > num2)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				Vector3 vector2 = Vector3.zero;
				foreach (Vector3 b2 in localVertices)
				{
					vector2 += b2;
				}
				vector2 /= (float)localVertices.Count;
				foreach (Vector3 vector3 in localVertices)
				{
					float num4 = Vector3.Distance(vector3, vector2);
					if (num4 > num3)
					{
						vector = vector3;
						num3 = num4;
					}
				}
				num3 = float.NegativeInfinity;
				foreach (Vector3 vector4 in localVertices)
				{
					float num4 = Vector3.Distance(vector4, vector);
					if (num4 > num3)
					{
						num3 = num4;
						b = vector4;
					}
				}
			}
			return new SphereColliderData
			{
				Center = (vector + b) / 2f,
				ColliderType = CREATE_COLLIDER_TYPE.SPHERE,
				IsValid = true,
				Radius = num3 / 2f
			};
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0002179C File Offset: 0x0001F99C
		public SphereColliderData CalculateSphereMinMax(List<Vector3> worldVertices, Transform attachTo)
		{
			if (worldVertices.Count < 2)
			{
				return new SphereColliderData();
			}
			List<Vector3> localVertices = this.ToLocalVerts(attachTo, worldVertices);
			SphereColliderData sphereColliderData = this.CalculateSphereMinMaxLocal(localVertices);
			sphereColliderData.Matrix = attachTo.localToWorldMatrix;
			return sphereColliderData;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000217D4 File Offset: 0x0001F9D4
		public SphereColliderData CalculateSphereMinMaxLocal(List<Vector3> localVertices)
		{
			float num3;
			float num2;
			float num = num2 = (num3 = float.PositiveInfinity);
			float num6;
			float num5;
			float num4 = num5 = (num6 = float.NegativeInfinity);
			for (int i = 0; i < localVertices.Count; i++)
			{
				num = ((localVertices[i].x < num) ? localVertices[i].x : num);
				num4 = ((localVertices[i].x > num4) ? localVertices[i].x : num4);
				num3 = ((localVertices[i].y < num3) ? localVertices[i].y : num3);
				num6 = ((localVertices[i].y > num6) ? localVertices[i].y : num6);
				num2 = ((localVertices[i].z < num2) ? localVertices[i].z : num2);
				num5 = ((localVertices[i].z > num5) ? localVertices[i].z : num5);
			}
			Vector3 vector = (new Vector3(num, num3, num2) + new Vector3(num4, num6, num5)) / 2f;
			float num7 = 0f;
			foreach (Vector3 a in localVertices)
			{
				float num8 = Vector3.Distance(a, vector);
				if (num8 > num7)
				{
					num7 = num8;
				}
			}
			return new SphereColliderData
			{
				Center = vector,
				ColliderType = CREATE_COLLIDER_TYPE.SPHERE,
				IsValid = true,
				Radius = num7
			};
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00021980 File Offset: 0x0001FB80
		private BoxCollider CreateBoxCollider(BoxColliderData data, EasyColliderProperties properties, bool postProcess = true)
		{
			BoxCollider boxCollider = properties.AttachTo.AddComponent<BoxCollider>();
			boxCollider.size = data.Size;
			boxCollider.center = data.Center;
			this.PostColliderCreation(boxCollider, properties, postProcess);
			return boxCollider;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000219BC File Offset: 0x0001FBBC
		public BoxCollider CreateBoxCollider(List<Vector3> vertices, EasyColliderProperties properties, bool isLocal = false)
		{
			if (vertices.Count >= 2)
			{
				BoxColliderData data;
				if (properties.Orientation == COLLIDER_ORIENTATION.ROTATED)
				{
					if (vertices.Count < 3)
					{
						Debug.LogWarning("Easy Collider Editor: Creating a Rotated Box Collider requires at least 3 points to be selected.");
						return null;
					}
					GameObject gameObject = this.CreateGameObjectOrientation(vertices, properties.AttachTo, "Rotated Box Collider");
					if (gameObject != null)
					{
						gameObject.layer = properties.Layer;
						properties.AttachTo = gameObject;
					}
					data = this.CalculateBox(vertices, properties.AttachTo.transform, true);
				}
				else if (!isLocal)
				{
					data = this.CalculateBox(vertices, properties.AttachTo.transform, false);
				}
				else
				{
					data = this.CalculateBoxLocal(vertices);
				}
				return this.CreateBoxCollider(data, properties, true);
			}
			return null;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00021A68 File Offset: 0x0001FC68
		private CapsuleCollider CreateCapsuleCollider(CapsuleColliderData data, EasyColliderProperties properties, bool postProcess = true)
		{
			CapsuleCollider capsuleCollider = properties.AttachTo.AddComponent<CapsuleCollider>();
			capsuleCollider.direction = data.Direction;
			capsuleCollider.height = data.Height;
			capsuleCollider.center = data.Center;
			capsuleCollider.radius = data.Radius;
			this.PostColliderCreation(capsuleCollider, properties, true);
			return capsuleCollider;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00021ABC File Offset: 0x0001FCBC
		public CapsuleCollider CreateCapsuleCollider_BestFit(List<Vector3> worldVertices, EasyColliderProperties properties)
		{
			if (worldVertices.Count >= 3)
			{
				CapsuleColliderData data = new CapsuleColliderData();
				if (properties.Orientation == COLLIDER_ORIENTATION.ROTATED)
				{
					GameObject gameObject = this.CreateGameObjectOrientation(worldVertices, properties.AttachTo, "Rotated Capsule Collider");
					if (gameObject != null)
					{
						properties.AttachTo = gameObject;
						gameObject.layer = properties.Layer;
					}
					data = this.CalculateCapsuleBestFit(worldVertices, properties.AttachTo.transform, true);
				}
				else
				{
					data = this.CalculateCapsuleBestFit(worldVertices, properties.AttachTo.transform, false);
				}
				return this.CreateCapsuleCollider(data, properties, true);
			}
			return null;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00021B48 File Offset: 0x0001FD48
		public CapsuleCollider CreateCapsuleCollider_MinMax(List<Vector3> worldVertices, EasyColliderProperties properties, CAPSULE_COLLIDER_METHOD method, bool isLocal = false)
		{
			CapsuleColliderData data;
			if (properties.Orientation == COLLIDER_ORIENTATION.ROTATED && worldVertices.Count >= 3)
			{
				GameObject gameObject = this.CreateGameObjectOrientation(worldVertices, properties.AttachTo, "Rotated Capsule Collider");
				if (gameObject != null)
				{
					properties.AttachTo = gameObject;
					gameObject.layer = properties.AttachTo.layer;
				}
				data = this.CalculateCapsuleMinMax(worldVertices, properties.AttachTo.transform, method, true);
			}
			else if (!isLocal)
			{
				data = this.CalculateCapsuleMinMax(worldVertices, properties.AttachTo.transform, method, false);
			}
			else
			{
				data = this.CalculateCapsuleMinMaxLocal(worldVertices, method);
			}
			return this.CreateCapsuleCollider(data, properties, true);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		public MeshCollider CreateConvexMeshCollider(Mesh mesh, GameObject attachToObject, EasyColliderProperties properties, bool postProcess = true)
		{
			MeshCollider meshCollider = attachToObject.AddComponent<MeshCollider>();
			meshCollider.sharedMesh = mesh;
			meshCollider.cookingOptions = (MeshColliderCookingOptions.CookForFasterSimulation | MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices);
			meshCollider.convex = true;
			this.PostColliderCreation(meshCollider, properties, postProcess);
			return meshCollider;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00021C18 File Offset: 0x0001FE18
		private SphereCollider CreateSphereCollider(SphereColliderData data, EasyColliderProperties properties, bool postProcess = true)
		{
			SphereCollider sphereCollider = properties.AttachTo.AddComponent<SphereCollider>();
			sphereCollider.radius = data.Radius;
			sphereCollider.center = data.Center;
			this.PostColliderCreation(sphereCollider, properties, postProcess);
			return sphereCollider;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00021C54 File Offset: 0x0001FE54
		public SphereCollider CreateSphereCollider_BestFit(List<Vector3> worldVertices, EasyColliderProperties properties)
		{
			if (worldVertices.Count >= 2)
			{
				SphereColliderData data = this.CalculateSphereBestFit(worldVertices, properties.AttachTo.transform);
				return this.CreateSphereCollider(data, properties, true);
			}
			return null;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00021C88 File Offset: 0x0001FE88
		public SphereCollider CreateSphereCollider_Distance(List<Vector3> worldVertices, EasyColliderProperties properties)
		{
			if (worldVertices.Count >= 2)
			{
				SphereColliderData data = this.CalculateSphereDistance(worldVertices, properties.AttachTo.transform);
				return this.CreateSphereCollider(data, properties, true);
			}
			return null;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00021CBC File Offset: 0x0001FEBC
		public SphereCollider CreateSphereCollider_MinMax(List<Vector3> worldVertices, EasyColliderProperties properties, bool isLocal = false)
		{
			if (worldVertices.Count < 2)
			{
				return null;
			}
			if (!isLocal)
			{
				SphereColliderData data = this.CalculateSphereMinMax(worldVertices, properties.AttachTo.transform);
				return this.CreateSphereCollider(data, properties, true);
			}
			SphereColliderData data2 = this.CalculateSphereMinMaxLocal(worldVertices);
			return this.CreateSphereCollider(data2, properties, true);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00021D05 File Offset: 0x0001FF05
		public void PostColliderCreationProcess(Collider createdCollider, EasyColliderProperties properties)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00021D08 File Offset: 0x0001FF08
		public List<Vector3> CalculateCylinderPointsLocal(List<Vector3> vertices, Transform attachTo, int numberOfSides, CYLINDER_ORIENTATION orientation, float cylinderOffset)
		{
			BoxColliderData boxColliderData = this.CalculateBoxLocal(vertices);
			float num = 0f;
			int num2 = 0;
			if (orientation == CYLINDER_ORIENTATION.Automatic)
			{
				num = Mathf.Max(Mathf.Max(boxColliderData.Size.x, boxColliderData.Size.y), boxColliderData.Size.z);
				num2 = ((num == boxColliderData.Size.x) ? 0 : ((num == boxColliderData.Size.y) ? 1 : 2));
			}
			else
			{
				num = boxColliderData.Size[orientation - CYLINDER_ORIENTATION.LocalX];
				num2 = orientation - CYLINDER_ORIENTATION.LocalX;
			}
			float num3 = 0f;
			Vector3 zero = Vector3.zero;
			foreach (Vector3 vector in vertices)
			{
				zero.x = ((num2 == 0) ? boxColliderData.Center.x : vector.x);
				zero.y = ((num2 == 1) ? boxColliderData.Center.y : vector.y);
				zero.z = ((num2 == 2) ? boxColliderData.Center.z : vector.z);
				float num4 = Vector3.Distance(zero, boxColliderData.Center);
				if (num4 > num3)
				{
					num3 = num4;
				}
			}
			float num5 = num / 2f;
			float num6 = 360f / (float)numberOfSides;
			Vector3 center;
			Vector3 vector2 = center = boxColliderData.Center;
			vector2.x = ((num2 == 0) ? (vector2.x + num5) : vector2.x);
			vector2.y = ((num2 == 1) ? (vector2.y + num5) : vector2.y);
			vector2.z = ((num2 == 2) ? (vector2.z + num5) : vector2.z);
			center.x = ((num2 == 0) ? (center.x - num5) : center.x);
			center.y = ((num2 == 1) ? (center.y - num5) : center.y);
			center.z = ((num2 == 2) ? (center.z - num5) : center.z);
			List<Vector3> list = new List<Vector3>();
			for (float num7 = 0f + cylinderOffset; num7 < 360f + cylinderOffset; num7 += num6)
			{
				float num8 = num3 * Mathf.Sin(num7 * 0.017453292f);
				float num9 = num3 * Mathf.Cos(num7 * 0.017453292f);
				if (num2 == 0)
				{
					vector2.y = num8 + boxColliderData.Center.y;
					vector2.z = num9 + boxColliderData.Center.z;
					center.y = num8 + boxColliderData.Center.y;
					center.z = num9 + boxColliderData.Center.z;
				}
				else if (num2 == 1)
				{
					vector2.x = num8 + boxColliderData.Center.x;
					vector2.z = num9 + boxColliderData.Center.z;
					center.x = num8 + boxColliderData.Center.x;
					center.z = num9 + boxColliderData.Center.z;
				}
				else
				{
					vector2.y = num8 + boxColliderData.Center.y;
					vector2.x = num9 + boxColliderData.Center.x;
					center.y = num8 + boxColliderData.Center.y;
					center.x = num9 + boxColliderData.Center.x;
				}
				list.Add(vector2);
				list.Add(center);
			}
			return list;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00022094 File Offset: 0x00020294
		private GameObject CreateGameObjectOrientation(List<Vector3> worldVertices, GameObject parent, string name)
		{
			GameObject gameObject = new GameObject(name);
			if (worldVertices.Count >= 3)
			{
				Vector3 vector = worldVertices[1] - worldVertices[0];
				Vector3 upwards = Vector3.Cross(vector, worldVertices[2] - worldVertices[1]);
				gameObject.transform.rotation = Quaternion.LookRotation(vector, upwards);
				gameObject.transform.SetParent(parent.transform);
				gameObject.transform.localPosition = Vector3.zero;
				return gameObject;
			}
			return null;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00022118 File Offset: 0x00020318
		private void DebugDrawPoint(Vector3 worldLoc, Color color, float dist = 0.01f)
		{
			Debug.DrawLine(worldLoc - Vector3.up * dist, worldLoc + Vector3.up * dist, color, 0.01f, false);
			Debug.DrawLine(worldLoc - Vector3.left * dist, worldLoc + Vector3.left * dist, color, 0.01f, false);
			Debug.DrawLine(worldLoc - Vector3.forward * dist, worldLoc + Vector3.forward * dist, color, 0.01f, false);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000221AF File Offset: 0x000203AF
		private void PostColliderCreation(Collider collider, EasyColliderProperties properties, bool postProcess = true)
		{
			this.SetPropertiesOnCollider(collider, properties);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000221B9 File Offset: 0x000203B9
		private void SetPropertiesOnCollider(Collider collider, EasyColliderProperties properties)
		{
			if (collider != null)
			{
				collider.isTrigger = properties.IsTrigger;
				collider.sharedMaterial = properties.PhysicMaterial;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000221DC File Offset: 0x000203DC
		private List<Vector3> ToLocalVerts(Transform transform, List<Vector3> worldVertices)
		{
			List<Vector3> list = new List<Vector3>(worldVertices.Count);
			foreach (Vector3 position in worldVertices)
			{
				list.Add(transform.InverseTransformPoint(position));
			}
			return list;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00022240 File Offset: 0x00020440
		public EasyColliderCreator()
		{
		}

		// Token: 0x020001C0 RID: 448
		private struct BestFitSphere
		{
			// Token: 0x06000F8A RID: 3978 RVA: 0x000637FA File Offset: 0x000619FA
			public BestFitSphere(Vector3 center, float radius)
			{
				this.Center = center;
				this.Radius = radius;
			}

			// Token: 0x04000DC3 RID: 3523
			public Vector3 Center;

			// Token: 0x04000DC4 RID: 3524
			public float Radius;
		}
	}
}
