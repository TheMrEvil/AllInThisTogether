using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

namespace Fluxy
{
	// Token: 0x0200000B RID: 11
	[AddComponentMenu("Physics/FluXY/Container", 800)]
	[ExecuteInEditMode]
	[ExecutionOrder(9998)]
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class FluxyContainer : MonoBehaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000019 RID: 25 RVA: 0x00002490 File Offset: 0x00000690
		// (remove) Token: 0x0600001A RID: 26 RVA: 0x000024C8 File Offset: 0x000006C8
		public event FluxyContainer.ContainerCallback OnFrameEnded
		{
			[CompilerGenerated]
			add
			{
				FluxyContainer.ContainerCallback containerCallback = this.OnFrameEnded;
				FluxyContainer.ContainerCallback containerCallback2;
				do
				{
					containerCallback2 = containerCallback;
					FluxyContainer.ContainerCallback value2 = (FluxyContainer.ContainerCallback)Delegate.Combine(containerCallback2, value);
					containerCallback = Interlocked.CompareExchange<FluxyContainer.ContainerCallback>(ref this.OnFrameEnded, value2, containerCallback2);
				}
				while (containerCallback != containerCallback2);
			}
			[CompilerGenerated]
			remove
			{
				FluxyContainer.ContainerCallback containerCallback = this.OnFrameEnded;
				FluxyContainer.ContainerCallback containerCallback2;
				do
				{
					containerCallback2 = containerCallback;
					FluxyContainer.ContainerCallback value2 = (FluxyContainer.ContainerCallback)Delegate.Remove(containerCallback2, value);
					containerCallback = Interlocked.CompareExchange<FluxyContainer.ContainerCallback>(ref this.OnFrameEnded, value2, containerCallback2);
				}
				while (containerCallback != containerCallback2);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024FD File Offset: 0x000006FD
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002505 File Offset: 0x00000705
		public FluxySolver solver
		{
			get
			{
				return this.m_Solver;
			}
			set
			{
				this.m_Solver = value;
				this.SetSolver(this.m_Solver, true);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000251B File Offset: 0x0000071B
		public Vector3 velocity
		{
			get
			{
				return this.m_Velocity;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002523 File Offset: 0x00000723
		public Vector3 angularVelocity
		{
			get
			{
				return this.m_AngularVelocity;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000252B File Offset: 0x0000072B
		public Renderer containerRenderer
		{
			get
			{
				return this.m_Renderer;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002533 File Offset: 0x00000733
		public Mesh containerMesh
		{
			get
			{
				if (!(this.customMesh != null))
				{
					return this.proceduralMesh;
				}
				return this.customMesh;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002550 File Offset: 0x00000750
		protected virtual void OnEnable()
		{
			this.m_Renderer = base.GetComponent<Renderer>();
			this.m_Filter = base.GetComponent<MeshFilter>();
			this.propertyBlock = new MaterialPropertyBlock();
			this.UpdateContainerShape();
			this.SetSolver(this.m_Solver, this.m_Solver == null);
			this.oldPosition = base.transform.position;
			this.oldRotation = base.transform.rotation;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025C0 File Offset: 0x000007C0
		protected virtual void OnDisable()
		{
			this.Clear();
			UnityEngine.Object.DestroyImmediate(this.proceduralMesh);
			this.SetSolver(null, false);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025DB File Offset: 0x000007DB
		protected virtual void Start()
		{
			if (Application.isPlaying)
			{
				this.Clear();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025EA File Offset: 0x000007EA
		protected virtual void OnValidate()
		{
			this.subdivisions.x = Mathf.Max(1, this.subdivisions.x);
			this.subdivisions.y = Mathf.Max(1, this.subdivisions.y);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002624 File Offset: 0x00000824
		public virtual void UpdateContainerShape()
		{
			switch (this.containerShape)
			{
			case FluxyContainer.ContainerShape.Plane:
				this.BuildPlaneMesh();
				return;
			case FluxyContainer.ContainerShape.Volume:
				this.BuildVolumeMesh();
				return;
			case FluxyContainer.ContainerShape.Custom:
				this.BuildCustomMesh();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002660 File Offset: 0x00000860
		protected void BuildPlaneMesh()
		{
			if (this.proceduralMesh == null)
			{
				this.proceduralMesh = new Mesh();
				this.proceduralMesh.name = "FluidContainer";
				if (this.m_Filter != null)
				{
					this.m_Filter.sharedMesh = this.proceduralMesh;
				}
			}
			this.proceduralMesh.Clear();
			this.subdivisions.x = Mathf.Max(1, this.subdivisions.x);
			this.subdivisions.y = Mathf.Max(1, this.subdivisions.y);
			Vector2 vector = new Vector2(1f / (float)this.subdivisions.x, 1f / (float)this.subdivisions.y);
			int num = (this.subdivisions.x + 1) * (this.subdivisions.y + 1);
			int num2 = this.subdivisions.x * this.subdivisions.y * 2;
			if (num > 65535)
			{
				this.proceduralMesh.indexFormat = IndexFormat.UInt32;
			}
			else
			{
				this.proceduralMesh.indexFormat = IndexFormat.UInt16;
			}
			this.vertices = new Vector3[num];
			this.normals = new Vector3[num];
			this.tangents = new Vector4[num];
			this.uvs = new Vector2[num];
			this.triangles = new int[num2 * 3];
			for (int i = 0; i < this.subdivisions.y + 1; i++)
			{
				for (int j = 0; j < this.subdivisions.x + 1; j++)
				{
					int num3 = i * (this.subdivisions.x + 1) + j;
					this.vertices[num3] = new Vector3((vector.x * (float)j - 0.5f) * this.size.x, (vector.y * (float)i - 0.5f) * this.size.y, 0f);
					this.normals[num3] = -Vector3.forward;
					this.tangents[num3] = new Vector4(1f, 0f, 0f, -1f);
					this.uvs[num3] = new Vector3((float)j / (float)this.subdivisions.x, (float)i / (float)this.subdivisions.y);
				}
			}
			for (int k = 0; k < this.subdivisions.y; k++)
			{
				for (int l = 0; l < this.subdivisions.x; l++)
				{
					int num4 = k * (this.subdivisions.x + 1) + l;
					int num5 = (k * this.subdivisions.x + l) * 6;
					this.triangles[num5] = num4 + this.subdivisions.x + 1;
					this.triangles[num5 + 1] = num4 + 1;
					this.triangles[num5 + 2] = num4;
					this.triangles[num5 + 3] = num4 + this.subdivisions.x + 2;
					this.triangles[num5 + 4] = num4 + 1;
					this.triangles[num5 + 5] = num4 + this.subdivisions.x + 1;
				}
			}
			this.proceduralMesh.SetVertices(this.vertices);
			this.proceduralMesh.SetNormals(this.normals);
			this.proceduralMesh.SetTangents(this.tangents);
			this.proceduralMesh.SetUVs(0, this.uvs);
			this.proceduralMesh.SetIndices(this.triangles, MeshTopology.Triangles, 0);
			this.proceduralMesh.RecalculateNormals();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A18 File Offset: 0x00000C18
		protected void BuildVolumeMesh()
		{
			if (this.proceduralMesh == null)
			{
				this.proceduralMesh = new Mesh();
				this.proceduralMesh.name = "FluidContainer";
				base.GetComponent<MeshFilter>().sharedMesh = this.proceduralMesh;
			}
			this.proceduralMesh.Clear();
			int num = 24;
			int num2 = 8;
			this.proceduralMesh.indexFormat = IndexFormat.UInt32;
			this.tangents = null;
			this.uvs = new Vector2[num];
			this.triangles = new int[num2 * 3];
			Vector3[] array = new Vector3[8];
			float x = this.size.x;
			float y = this.size.y;
			float z = this.size.z;
			array[0] = new Vector3(-x * 0.5f, -y * 0.5f, z * 0.5f);
			array[1] = new Vector3(x * 0.5f, -y * 0.5f, z * 0.5f);
			array[2] = new Vector3(x * 0.5f, -y * 0.5f, -z * 0.5f);
			array[3] = new Vector3(-x * 0.5f, -y * 0.5f, -z * 0.5f);
			array[4] = new Vector3(-x * 0.5f, y * 0.5f, z * 0.5f);
			array[5] = new Vector3(x * 0.5f, y * 0.5f, z * 0.5f);
			array[6] = new Vector3(x * 0.5f, y * 0.5f, -z * 0.5f);
			array[7] = new Vector3(-x * 0.5f, y * 0.5f, -z * 0.5f);
			this.vertices = new Vector3[]
			{
				array[0],
				array[1],
				array[2],
				array[3],
				array[7],
				array[4],
				array[0],
				array[3],
				array[4],
				array[5],
				array[1],
				array[0],
				array[6],
				array[7],
				array[3],
				array[2],
				array[5],
				array[6],
				array[2],
				array[1],
				array[7],
				array[6],
				array[5],
				array[4]
			};
			Vector3 vector = -Vector3.up;
			Vector3 vector2 = -Vector3.down;
			Vector3 vector3 = -Vector3.forward;
			Vector3 vector4 = -Vector3.back;
			Vector3 vector5 = -Vector3.left;
			Vector3 vector6 = -Vector3.right;
			this.normals = new Vector3[]
			{
				vector2,
				vector2,
				vector2,
				vector2,
				vector5,
				vector5,
				vector5,
				vector5,
				vector3,
				vector3,
				vector3,
				vector3,
				vector4,
				vector4,
				vector4,
				vector4,
				vector6,
				vector6,
				vector6,
				vector6,
				vector,
				vector,
				vector,
				vector
			};
			Vector2 vector7 = new Vector2(0f, 0f);
			Vector2 vector8 = new Vector2(1f, 0f);
			Vector2 vector9 = new Vector2(0f, 1f);
			Vector2 vector10 = new Vector2(1f, 1f);
			this.uvs = new Vector2[]
			{
				vector10,
				vector9,
				vector7,
				vector8,
				vector10,
				vector9,
				vector7,
				vector8,
				vector10,
				vector9,
				vector7,
				vector8,
				vector10,
				vector9,
				vector7,
				vector8,
				vector10,
				vector9,
				vector7,
				vector8,
				vector10,
				vector9,
				vector7,
				vector8
			};
			this.triangles = new int[]
			{
				0,
				1,
				3,
				1,
				2,
				3,
				4,
				5,
				7,
				5,
				6,
				7,
				8,
				9,
				11,
				9,
				10,
				11,
				12,
				13,
				15,
				13,
				14,
				15,
				16,
				17,
				19,
				17,
				18,
				19,
				20,
				21,
				23,
				21,
				22,
				23
			};
			this.proceduralMesh.SetVertices(this.vertices);
			this.proceduralMesh.SetNormals(this.normals);
			this.proceduralMesh.SetUVs(0, this.uvs);
			this.proceduralMesh.SetIndices(this.triangles, MeshTopology.Triangles, 0);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000302C File Offset: 0x0000122C
		protected void BuildCustomMesh()
		{
			if (this.proceduralMesh != null)
			{
				UnityEngine.Object.DestroyImmediate(this.proceduralMesh);
			}
			if (this.customMesh != null)
			{
				base.GetComponent<MeshFilter>().sharedMesh = this.customMesh;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003068 File Offset: 0x00001268
		private void SetSolver(FluxySolver newSolver, bool setMember)
		{
			if (this.m_Solver != null)
			{
				this.m_Solver.UnregisterContainer(this);
			}
			if (newSolver == null)
			{
				newSolver = FluXYSolverFinder.GetSolver(this.SolverGroup);
			}
			if (setMember)
			{
				this.m_Solver = newSolver;
			}
			if (this.m_Solver != null && base.isActiveAndEnabled)
			{
				this.m_Solver.RegisterContainer(this);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000030D4 File Offset: 0x000012D4
		public virtual void Clear()
		{
			if (this.m_Solver != null)
			{
				int containerID = this.m_Solver.GetContainerID(this);
				this.m_Solver.ClearContainer(containerID);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003108 File Offset: 0x00001308
		public Vector3 TransformWorldVectorToUVSpace(in Vector3 vector, in Vector4 uvRect)
		{
			if (Mathf.Abs(this.size.x) < 1E-05f || Mathf.Abs(this.size.y) < 1E-05f)
			{
				return Vector3.zero;
			}
			Vector3 result = base.transform.InverseTransformVector(vector);
			result.x *= uvRect.z / this.size.x;
			result.y *= uvRect.w / this.size.y;
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003198 File Offset: 0x00001398
		public Vector3 TransformUVVectorToWorldSpace(in Vector3 vector, in Vector4 uvRect)
		{
			if (Mathf.Abs(uvRect.z) < 1E-05f || Mathf.Abs(uvRect.w) < 1E-05f)
			{
				return Vector3.zero;
			}
			Vector3 vector2 = vector;
			vector2.x *= this.size.x / uvRect.z;
			vector2.y *= this.size.y / uvRect.w;
			return base.transform.TransformVector(vector2);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000321C File Offset: 0x0000141C
		public Vector3 TransformWorldPointToUVSpace(in Vector3 point, in Vector4 uvRect)
		{
			Vector3 result = base.transform.InverseTransformPoint(point);
			result.x += this.size.x * 0.5f;
			result.y += this.size.y * 0.5f;
			result.x *= uvRect.z / this.size.x;
			result.y *= uvRect.w / this.size.y;
			result.x += uvRect.x;
			result.y += uvRect.y;
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000032D0 File Offset: 0x000014D0
		public virtual void UpdateTransform()
		{
			if (this.lookAt != null)
			{
				if (this.lookAtMode == FluxyContainer.LookAtMode.LookAt)
				{
					base.transform.rotation = Quaternion.LookRotation(base.transform.position - this.lookAt.position, Vector3.up);
				}
				else
				{
					base.transform.rotation = Quaternion.LookRotation(this.lookAt.forward, this.lookAt.up);
				}
			}
			Shader.SetGlobalVector("_ContainerSize", this.size);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003360 File Offset: 0x00001560
		public virtual void UpdateMaterial(int tile, FluxyStorage.Framebuffer fb)
		{
			if (this.m_Renderer == null || this.m_Renderer.sharedMaterial == null || fb == null)
			{
				return;
			}
			this.containerRenderer.GetPropertyBlock(this.propertyBlock);
			this.propertyBlock.SetInt("_TileIndex", tile);
			this.propertyBlock.SetTexture("_MainTex", fb.stateA);
			this.propertyBlock.SetTexture("_Velocity", fb.velocityA);
			this.propertyBlock.SetFloat("_Opacity", this.opacity);
			this.oldOpacity = this.opacity;
			if (this.lightSource != null && this.lightSource.isActiveAndEnabled && this.lightSource.type == LightType.Directional)
			{
				this.m_Renderer.sharedMaterial.EnableKeyword("_LIGHTSOURCE_DIRECTIONAL");
				this.m_Renderer.sharedMaterial.DisableKeyword("_LIGHTSOURCE_POINT");
				this.m_Renderer.sharedMaterial.DisableKeyword("_LIGHTSOURCE_NONE");
				this.propertyBlock.SetVector("_LightVector", base.transform.InverseTransformDirection(this.lightSource.transform.forward));
				this.propertyBlock.SetVector("_LightColor", this.lightSource.color * this.lightSource.intensity);
			}
			else if (this.lightSource != null && this.lightSource.isActiveAndEnabled && this.lightSource.type == LightType.Point)
			{
				this.m_Renderer.sharedMaterial.DisableKeyword("_LIGHTSOURCE_DIRECTIONAL");
				this.m_Renderer.sharedMaterial.EnableKeyword("_LIGHTSOURCE_POINT");
				this.m_Renderer.sharedMaterial.DisableKeyword("_LIGHTSOURCE_NONE");
				Vector4 value = base.transform.InverseTransformPoint(this.lightSource.transform.position);
				value.w = 1f / Mathf.Max(this.lightSource.range * this.lightSource.range, 1E-05f);
				this.propertyBlock.SetVector("_LightVector", value);
				this.propertyBlock.SetVector("_LightColor", this.lightSource.color * this.lightSource.intensity);
			}
			else
			{
				this.m_Renderer.sharedMaterial.DisableKeyword("_LIGHTSOURCE_DIRECTIONAL");
				this.m_Renderer.sharedMaterial.DisableKeyword("_LIGHTSOURCE_POINT");
				this.m_Renderer.sharedMaterial.EnableKeyword("_LIGHTSOURCE_NONE");
				this.propertyBlock.SetVector("_LightColor", Color.white);
			}
			this.containerRenderer.SetPropertyBlock(this.propertyBlock);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003648 File Offset: 0x00001848
		public virtual Vector4 ProjectTarget(in Vector3 targetPosition, Vector2 projectionSize, float aspectRatio, bool scaleWithDistance = true)
		{
			Vector3 projectionOrigin = this.GetProjectionOrigin(targetPosition);
			Ray ray = new Ray(projectionOrigin, targetPosition - projectionOrigin);
			MeshCollider meshCollider;
			if (base.TryGetComponent<MeshCollider>(out meshCollider) && meshCollider.enabled)
			{
				RaycastHit raycastHit;
				if (meshCollider.Raycast(ray, out raycastHit, float.PositiveInfinity))
				{
					float num = 1f;
					if (scaleWithDistance)
					{
						ray = new Ray(projectionOrigin, targetPosition + base.transform.right * 0.01f - projectionOrigin);
						RaycastHit raycastHit2;
						if (meshCollider.Raycast(ray, out raycastHit2, float.PositiveInfinity))
						{
							num = Vector3.Distance(raycastHit.point, raycastHit2.point) / 0.01f;
						}
					}
					return new Vector4(raycastHit.textureCoord.x - 0.5f, raycastHit.textureCoord.y - 0.5f, projectionSize.x * num * aspectRatio, projectionSize.y * num);
				}
			}
			else
			{
				Plane plane = new Plane(base.transform.forward, base.transform.position);
				float distance;
				if (plane.Raycast(ray, out distance))
				{
					Vector3 point = ray.GetPoint(distance);
					Vector2 vector = base.transform.InverseTransformPoint(point) / this.size;
					float num2 = 1f;
					if (scaleWithDistance)
					{
						ray = new Ray(projectionOrigin, targetPosition + base.transform.right - projectionOrigin);
						float distance2;
						if (plane.Raycast(ray, out distance2))
						{
							Vector3 point2 = ray.GetPoint(distance2);
							Vector2 b = base.transform.InverseTransformPoint(point2) / this.size;
							num2 = Vector2.Distance(vector, b);
						}
					}
					return new Vector4(vector.x, vector.y, projectionSize.x * num2 * aspectRatio, projectionSize.y * num2);
				}
			}
			return Vector4.zero;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000383B File Offset: 0x00001A3B
		private Vector3 GetProjectionOrigin(in Vector3 targetPosition)
		{
			if (this.projectFrom != null)
			{
				return this.projectFrom.position;
			}
			return targetPosition + base.transform.forward;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003870 File Offset: 0x00001A70
		public Vector3 UpdateVelocityAndGetAcceleration()
		{
			this.m_Velocity = (base.transform.position - this.oldPosition) / Time.deltaTime;
			Quaternion quaternion = base.transform.rotation * Quaternion.Inverse(this.oldRotation);
			this.m_AngularVelocity = new Vector3(quaternion.x, quaternion.y, quaternion.z) * 2f / Time.deltaTime;
			return (this.m_Velocity - this.oldVelocity) / Time.deltaTime;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000390B File Offset: 0x00001B0B
		private void ResetVelocityAndAcceleration()
		{
			this.oldVelocity = this.m_Velocity;
			this.oldRotation = base.transform.rotation;
			this.oldPosition = base.transform.position;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000393B File Offset: 0x00001B3B
		protected virtual void LateUpdate()
		{
			this.ResetVelocityAndAcceleration();
			this.UpdateOpacity();
			FluxyContainer.ContainerCallback onFrameEnded = this.OnFrameEnded;
			if (onFrameEnded == null)
			{
				return;
			}
			onFrameEnded(this);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000395C File Offset: 0x00001B5C
		private void UpdateOpacity()
		{
			if (this.oldOpacity == this.opacity)
			{
				return;
			}
			this.oldOpacity = this.opacity;
			this.propertyBlock.SetFloat("_Opacity", this.opacity);
			this.containerRenderer.SetPropertyBlock(this.propertyBlock);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000039AC File Offset: 0x00001BAC
		public Vector3 GetVelocityAt(Vector3 worldPosition)
		{
			if (this.solver != null && (this.solver.readable & FluxySolver.ReadbackMode.Velocity) != FluxySolver.ReadbackMode.None && this.solver.framebuffer != null)
			{
				Vector4 containerUVRect = this.solver.GetContainerUVRect(this);
				Vector3 vector = this.TransformWorldPointToUVSpace(worldPosition, containerUVRect);
				Color pixelBilinear = this.solver.velocityReadbackTexture.GetPixelBilinear(vector.x, vector.y);
				Vector3 vector2 = new Vector3(pixelBilinear.r, pixelBilinear.g, 0f);
				return this.TransformUVVectorToWorldSpace(vector2, containerUVRect);
			}
			return Vector3.zero;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003A40 File Offset: 0x00001C40
		public Vector4 GetDensityAt(Vector3 worldPosition)
		{
			if (this.solver != null && (this.solver.readable & FluxySolver.ReadbackMode.Density) != FluxySolver.ReadbackMode.None && this.solver.framebuffer != null)
			{
				Vector4 containerUVRect = this.solver.GetContainerUVRect(this);
				Vector3 vector = this.TransformWorldPointToUVSpace(worldPosition, containerUVRect);
				return this.solver.densityReadbackTexture.GetPixelBilinear(vector.x, vector.y);
			}
			return Vector4.zero;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003AB6 File Offset: 0x00001CB6
		protected virtual void OnDrawGizmosSelected()
		{
			if (this.customMesh == null)
			{
				Gizmos.matrix = base.transform.localToWorldMatrix;
				Gizmos.DrawWireCube(Vector3.zero, this.size);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public FluxyContainer()
		{
		}

		// Token: 0x04000019 RID: 25
		[Tooltip("Shape of the container: can be flat, can be a volume, or can be a custom mesh.")]
		public FluxyContainer.ContainerShape containerShape;

		// Token: 0x0400001A RID: 26
		[Tooltip("Amount of subdivisions in the plane mesh.")]
		public Vector2Int subdivisions = Vector2Int.one;

		// Token: 0x0400001B RID: 27
		[Tooltip("Custom mesh used by the container. If null, a subdivided plane will be used instead.")]
		public Mesh customMesh;

		// Token: 0x0400001C RID: 28
		[Tooltip("Size of the container in local space.")]
		public Vector3 size = Vector3.one;

		// Token: 0x0400001D RID: 29
		[Tooltip("Method using for facing the lookAt transform: look to it, or copy its orientation.")]
		public FluxyContainer.LookAtMode lookAtMode;

		// Token: 0x0400001E RID: 30
		[Tooltip("Transform that the container should be facing at all times. If unused, you can set the container's rotation manually.")]
		public Transform lookAt;

		// Token: 0x0400001F RID: 31
		[Tooltip("Transform used as raycasting origin for splatting targets onto this container. If unused, simple planar projection will be used instead.")]
		public Transform projectFrom;

		// Token: 0x04000020 RID: 32
		[Range(0f, 1f)]
		[Tooltip("Container material opacity")]
		public float opacity = 1f;

		// Token: 0x04000021 RID: 33
		[Tooltip("Texture used for clearing the container's density buffer.")]
		public Texture2D clearTexture;

		// Token: 0x04000022 RID: 34
		[Tooltip("Color used to tint the clear texture.")]
		public Color clearColor;

		// Token: 0x04000023 RID: 35
		[Tooltip("Normal map used to determine container surface normal.")]
		public Texture2D surfaceNormals;

		// Token: 0x04000024 RID: 36
		[Tooltip("Tiling of surface normal map.")]
		public Vector2 normalTiling = Vector2.one;

		// Token: 0x04000025 RID: 37
		[Range(0f, 1f)]
		[Tooltip("Intensity of the surface normals.")]
		public float normalScale = 1f;

		// Token: 0x04000026 RID: 38
		[Tooltip("Falloff controls for density and velocity near container edges.")]
		public FluxyContainer.EdgeFalloff edgeFalloff;

		// Token: 0x04000027 RID: 39
		[Tooltip("Falloff controls for density and velocity near container edges.")]
		public FluxyContainer.BoundaryConditions boundaries;

		// Token: 0x04000028 RID: 40
		[Range(0f, 1f)]
		[Tooltip("Scale (0%-100%) of container's velocity. If set to zero, containers will be regarded as static.")]
		public float velocityScale = 1f;

		// Token: 0x04000029 RID: 41
		[Range(0f, 1f)]
		[Tooltip("Scale (0%-100%) of container's acceleration. This controls how much world-space inertia affects the fluid.")]
		public float accelerationScale = 1f;

		// Token: 0x0400002A RID: 42
		[Tooltip("Local-space positional offset applied to the fluid.")]
		public Vector2 positionOffset = Vector2.zero;

		// Token: 0x0400002B RID: 43
		[Tooltip("World-space gravity applied to the fluid.")]
		public Vector3 gravity = Vector3.zero;

		// Token: 0x0400002C RID: 44
		[Tooltip("World-space external force applied to the fluid.")]
		public Vector3 externalForce = Vector3.zero;

		// Token: 0x0400002D RID: 45
		[Tooltip("Lightsource used for volume rendering.")]
		public Light lightSource;

		// Token: 0x0400002E RID: 46
		[Tooltip("List of targets that should be splatted onto this container.")]
		[NonReorderable]
		public FluxyTarget[] targets;

		// Token: 0x0400002F RID: 47
		[Range(0f, 1f)]
		[Tooltip("Scales fluid pressure.")]
		public float pressure = 1f;

		// Token: 0x04000030 RID: 48
		[Range(0f, 1f)]
		[Tooltip("Scales fluid viscosity.")]
		public float viscosity;

		// Token: 0x04000031 RID: 49
		[Tooltip("Amount of turbulence (vorticity) in the fluid.")]
		public float turbulence = 5f;

		// Token: 0x04000032 RID: 50
		[Range(0f, 1f)]
		[Tooltip("Amount of adhesion to the container's surface.")]
		public float adhesion;

		// Token: 0x04000033 RID: 51
		[Range(0f, 1f)]
		[Tooltip("Amount of surface tension. Higher values will make the fluid tend to form round shapes.")]
		public float surfaceTension;

		// Token: 0x04000034 RID: 52
		[Tooltip("Upwards buoyant force applied to fluid. It is directly proportional to the contents the density buffer's alpha channel (temperature).")]
		public float buoyancy = 1f;

		// Token: 0x04000035 RID: 53
		[Tooltip("Amount of density dissipated per second.")]
		public Vector4 dissipation = Vector4.zero;

		// Token: 0x04000036 RID: 54
		[SerializeField]
		[HideInInspector]
		private FluxySolver m_Solver;

		// Token: 0x04000037 RID: 55
		private Renderer m_Renderer;

		// Token: 0x04000038 RID: 56
		private MeshFilter m_Filter;

		// Token: 0x04000039 RID: 57
		private MaterialPropertyBlock propertyBlock;

		// Token: 0x0400003A RID: 58
		private Vector3 m_Velocity;

		// Token: 0x0400003B RID: 59
		private Vector3 m_AngularVelocity;

		// Token: 0x0400003C RID: 60
		private Vector3 oldPosition;

		// Token: 0x0400003D RID: 61
		private Quaternion oldRotation;

		// Token: 0x0400003E RID: 62
		private Vector3 oldVelocity;

		// Token: 0x0400003F RID: 63
		private float oldOpacity;

		// Token: 0x04000040 RID: 64
		protected Mesh proceduralMesh;

		// Token: 0x04000041 RID: 65
		protected Vector3[] vertices;

		// Token: 0x04000042 RID: 66
		protected Vector3[] normals;

		// Token: 0x04000043 RID: 67
		protected Vector4[] tangents;

		// Token: 0x04000044 RID: 68
		protected Vector2[] uvs;

		// Token: 0x04000045 RID: 69
		protected int[] triangles;

		// Token: 0x04000046 RID: 70
		[CompilerGenerated]
		private FluxyContainer.ContainerCallback OnFrameEnded;

		// Token: 0x04000047 RID: 71
		public FluXYGroup SolverGroup;

		// Token: 0x02000022 RID: 34
		[Serializable]
		public struct BoundaryConditions
		{
			// Token: 0x060000A8 RID: 168 RVA: 0x00006E72 File Offset: 0x00005072
			public static implicit operator Vector4(FluxyContainer.BoundaryConditions b)
			{
				return new Vector4((float)((b.horizontalBoundary == FluxyContainer.BoundaryConditions.BoundaryType.Periodic) ? 1 : 0), (float)((b.verticalBoundary == FluxyContainer.BoundaryConditions.BoundaryType.Periodic) ? 1 : 0), (float)((b.horizontalBoundary == FluxyContainer.BoundaryConditions.BoundaryType.Solid) ? 1 : 0), (float)((b.verticalBoundary == FluxyContainer.BoundaryConditions.BoundaryType.Solid) ? 1 : 0));
			}

			// Token: 0x040000D8 RID: 216
			public FluxyContainer.BoundaryConditions.BoundaryType horizontalBoundary;

			// Token: 0x040000D9 RID: 217
			public FluxyContainer.BoundaryConditions.BoundaryType verticalBoundary;

			// Token: 0x02000032 RID: 50
			public enum BoundaryType
			{
				// Token: 0x04000100 RID: 256
				Open,
				// Token: 0x04000101 RID: 257
				Solid,
				// Token: 0x04000102 RID: 258
				Periodic
			}
		}

		// Token: 0x02000023 RID: 35
		[Serializable]
		public struct EdgeFalloff
		{
			// Token: 0x060000A9 RID: 169 RVA: 0x00006EB1 File Offset: 0x000050B1
			public static implicit operator Vector4(FluxyContainer.EdgeFalloff d)
			{
				return new Vector4(d.densityEdgeWidth, d.densityFalloffRate, d.velocityEdgeWidth, d.velocityFalloffRate);
			}

			// Token: 0x040000DA RID: 218
			[Min(0f)]
			public float densityEdgeWidth;

			// Token: 0x040000DB RID: 219
			[Min(0f)]
			public float densityFalloffRate;

			// Token: 0x040000DC RID: 220
			[Min(0f)]
			public float velocityEdgeWidth;

			// Token: 0x040000DD RID: 221
			[Min(0f)]
			public float velocityFalloffRate;
		}

		// Token: 0x02000024 RID: 36
		public enum LookAtMode
		{
			// Token: 0x040000DF RID: 223
			LookAt,
			// Token: 0x040000E0 RID: 224
			CopyOrientation
		}

		// Token: 0x02000025 RID: 37
		public enum ContainerShape
		{
			// Token: 0x040000E2 RID: 226
			Plane,
			// Token: 0x040000E3 RID: 227
			Volume,
			// Token: 0x040000E4 RID: 228
			Custom
		}

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x060000AB RID: 171
		public delegate void ContainerCallback(FluxyContainer container);
	}
}
