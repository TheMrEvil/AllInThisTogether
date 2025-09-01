using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

namespace Fluxy
{
	// Token: 0x0200000C RID: 12
	[AddComponentMenu("Physics/FluXY/Solver", 800)]
	public class FluxySolver : MonoBehaviour
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003A RID: 58 RVA: 0x00003B98 File Offset: 0x00001D98
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public event FluxySolver.SolverCallback OnStep
		{
			[CompilerGenerated]
			add
			{
				FluxySolver.SolverCallback solverCallback = this.OnStep;
				FluxySolver.SolverCallback solverCallback2;
				do
				{
					solverCallback2 = solverCallback;
					FluxySolver.SolverCallback value2 = (FluxySolver.SolverCallback)Delegate.Combine(solverCallback2, value);
					solverCallback = Interlocked.CompareExchange<FluxySolver.SolverCallback>(ref this.OnStep, value2, solverCallback2);
				}
				while (solverCallback != solverCallback2);
			}
			[CompilerGenerated]
			remove
			{
				FluxySolver.SolverCallback solverCallback = this.OnStep;
				FluxySolver.SolverCallback solverCallback2;
				do
				{
					solverCallback2 = solverCallback;
					FluxySolver.SolverCallback value2 = (FluxySolver.SolverCallback)Delegate.Remove(solverCallback2, value);
					solverCallback = Interlocked.CompareExchange<FluxySolver.SolverCallback>(ref this.OnStep, value2, solverCallback2);
				}
				while (solverCallback != solverCallback2);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003C05 File Offset: 0x00001E05
		public FluxyStorage.Framebuffer framebuffer
		{
			get
			{
				if (!(this.storage != null))
				{
					return null;
				}
				return this.storage.GetFramebuffer(this.framebufferID);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003C28 File Offset: 0x00001E28
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00003C30 File Offset: 0x00001E30
		public Texture2D velocityReadbackTexture
		{
			[CompilerGenerated]
			get
			{
				return this.<velocityReadbackTexture>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<velocityReadbackTexture>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003C39 File Offset: 0x00001E39
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00003C41 File Offset: 0x00001E41
		public Texture2D densityReadbackTexture
		{
			[CompilerGenerated]
			get
			{
				return this.<densityReadbackTexture>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<densityReadbackTexture>k__BackingField = value;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003C4A File Offset: 0x00001E4A
		private void OnEnable()
		{
			this.lodGroup = base.GetComponent<LODGroup>();
			this.visibleLOD = this.GetCurrentLOD(FluxySolver.LODCam);
			this.UpdateFramebuffer();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003C6F File Offset: 0x00001E6F
		private void OnDisable()
		{
			this.DisposeOfFramebuffer();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003C77 File Offset: 0x00001E77
		private void OnValidate()
		{
			this.UpdateFramebuffer();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003C7F File Offset: 0x00001E7F
		public bool IsFull()
		{
			return this.containers.Count >= 16;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003C93 File Offset: 0x00001E93
		public bool RegisterContainer(FluxyContainer container)
		{
			if (this.IsFull())
			{
				return false;
			}
			if (!this.containers.Contains(container))
			{
				this.containers.Add(container);
				this.tilesDirty = true;
			}
			return true;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003CC1 File Offset: 0x00001EC1
		public void UnregisterContainer(FluxyContainer container)
		{
			if (this.containers.Contains(container))
			{
				this.containers.Remove(container);
				this.tilesDirty = true;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003CE5 File Offset: 0x00001EE5
		public int GetContainerID(FluxyContainer container)
		{
			return this.containers.IndexOf(container);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public Vector4 GetContainerUVRect(FluxyContainer container)
		{
			int num = this.containers.IndexOf(container);
			if (num >= 0)
			{
				return this.rects[num + 1];
			}
			return Vector4.zero;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003D28 File Offset: 0x00001F28
		private void UpdateFramebuffer()
		{
			if (!this.visible)
			{
				if (!this.disposeWhenCulled)
				{
					return;
				}
				this.DisposeOfFramebuffer();
			}
			else if (this.framebufferID < 0)
			{
				if (this.storage != null)
				{
					this.framebufferID = this.storage.RequestFramebuffer(this.desiredResolution / (this.visibleLOD + 1), this.densitySupersampling);
				}
			}
			else
			{
				FluxyStorage.Framebuffer framebuffer = this.framebuffer;
				if (framebuffer != null)
				{
					framebuffer.desiredResolution = this.desiredResolution / (this.visibleLOD + 1);
					framebuffer.stateSupersampling = this.densitySupersampling;
					this.storage.ResizeStorage();
				}
			}
			FluxyStorage.Framebuffer framebuffer2 = this.framebuffer;
			if (framebuffer2 != null)
			{
				this.velocityReadbackTexture = new Texture2D(framebuffer2.velocityA.width, framebuffer2.velocityA.height, TextureFormat.RGBAHalf, false);
				this.densityReadbackTexture = new Texture2D(framebuffer2.stateA.width, framebuffer2.stateA.height, TextureFormat.RGBAHalf, false);
				Color[] pixels = this.velocityReadbackTexture.GetPixels();
				for (int i = 0; i < pixels.Length; i++)
				{
					pixels[i] = new Color(0f, 0f, 0f, 0f);
				}
				this.velocityReadbackTexture.SetPixels(pixels);
				this.velocityReadbackTexture.Apply();
				pixels = this.densityReadbackTexture.GetPixels();
				for (int j = 0; j < pixels.Length; j++)
				{
					pixels[j] = new Color(0f, 0f, 0f, 0f);
				}
				this.densityReadbackTexture.SetPixels(pixels);
				this.densityReadbackTexture.Apply();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003EC0 File Offset: 0x000020C0
		private void DisposeOfFramebuffer()
		{
			if (this.storage != null && this.framebufferID >= 0)
			{
				this.storage.DisposeFramebuffer(this.framebufferID, true);
				this.framebufferID = -1;
			}
			UnityEngine.Object.Destroy(this.velocityReadbackTexture);
			UnityEngine.Object.Destroy(this.densityReadbackTexture);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003F14 File Offset: 0x00002114
		private int GetCurrentLOD(Camera cam = null)
		{
			this.visible = true;
			if (this.lodGroup == null || cam == null)
			{
				return 0;
			}
			float magnitude = (base.transform.position - cam.transform.position).magnitude;
			float size = this.lodGroup.size;
			float num = FluxyUtils.RelativeScreenHeight(cam, magnitude / QualitySettings.lodBias, size);
			LOD[] lods = this.lodGroup.GetLODs();
			for (int i = 0; i < lods.Length; i++)
			{
				if (num >= lods[i].screenRelativeTransitionHeight)
				{
					return i;
				}
			}
			this.visible = false;
			return this.lodGroup.lodCount;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003FC4 File Offset: 0x000021C4
		private void UpdateLOD()
		{
			int currentLOD = this.GetCurrentLOD(FluxySolver.LODCam);
			if (this.visibleLOD != currentLOD)
			{
				this.visibleLOD = currentLOD;
				this.UpdateFramebuffer();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003FF4 File Offset: 0x000021F4
		protected virtual void SimulationStep(FluxyStorage.Framebuffer fb, float deltaTime)
		{
			if (fb == null)
			{
				return;
			}
			FluxySolver.SolverCallback onStep = this.OnStep;
			if (onStep != null)
			{
				onStep(this);
			}
			fb.velocityA.filterMode = FilterMode.Point;
			fb.stateA.filterMode = FilterMode.Point;
			this.simulationMaterial.SetFloat("_DeltaTime", deltaTime);
			this.simulationMaterial.SetTexture("_Velocity", fb.velocityA);
			this.simulationMaterial.SetTexture("_State", fb.stateB);
			Graphics.Blit(fb.stateA, fb.stateB, this.simulationMaterial, 0);
			Graphics.Blit(fb.velocityA, fb.velocityB, this.simulationMaterial, 1);
			Graphics.Blit(fb.stateB, fb.stateA, this.simulationMaterial, 2);
			Graphics.Blit(fb.velocityB, fb.velocityA, this.simulationMaterial, 3);
			Graphics.Blit(fb.stateA, fb.stateB, this.simulationMaterial, 4);
			fb.stateB.filterMode = FilterMode.Bilinear;
			this.UpdateExternalForces(fb.velocityA, fb.velocityB);
			fb.stateB.filterMode = FilterMode.Point;
			Graphics.Blit(fb.velocityB, fb.velocityA, this.simulationMaterial, 5);
			if (this.pressureSolver == FluxySolver.PressureSolver.Separable)
			{
				Graphics.Blit(fb.velocityA, fb.velocityB, this.simulationMaterial, 11);
				this.simulationMaterial.SetVector("axis", Vector2.right);
				Graphics.Blit(fb.velocityB, fb.velocityA, this.simulationMaterial, 6);
				this.simulationMaterial.SetVector("axis", Vector2.up);
				Graphics.Blit(fb.velocityA, fb.velocityB, this.simulationMaterial, 6);
			}
			else
			{
				for (int i = 0; i < this.pressureIterations; i++)
				{
					Graphics.Blit(fb.velocityA, fb.velocityB, this.simulationMaterial, 10);
					Graphics.Blit(fb.velocityB, fb.velocityA, this.simulationMaterial, 10);
				}
				Graphics.Blit(fb.velocityA, fb.velocityB);
			}
			Graphics.Blit(fb.velocityB, fb.velocityA, this.simulationMaterial, 7);
			fb.velocityA.filterMode = FilterMode.Bilinear;
			fb.stateA.filterMode = FilterMode.Bilinear;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004234 File Offset: 0x00002434
		private void UpdateExternalForces(RenderTexture source, RenderTexture dest)
		{
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = dest;
			GL.Clear(false, true, Color.clear);
			this.simulationMaterial.SetTexture("_MainTex", source);
			for (int i = 0; i < this.containers.Count; i++)
			{
				int num = i + 1;
				int index = this.indices[num];
				this.simulationMaterial.SetInt("_TileIndex", num);
				this.simulationMaterial.SetFloat("_NormalScale", this.containers[index].normalScale);
				this.simulationMaterial.SetVector("_NormalTiling", this.containers[index].normalTiling);
				this.simulationMaterial.SetTexture("_Normals", this.containers[index].surfaceNormals);
				if (this.simulationMaterial.SetPass(12))
				{
					GL.PushMatrix();
					GL.LoadProjectionMatrix(Matrix4x4.Ortho(0f, 1f, 0f, 1f, -1f, 1f));
					Graphics.DrawMeshNow(this.containers[index].containerMesh, this.containers[index].transform.localToWorldMatrix);
					GL.PopMatrix();
				}
			}
			this.simulationMaterial.SetTexture("_MainTex", null);
			RenderTexture.active = active;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004390 File Offset: 0x00002590
		public void ClearContainer(int id)
		{
			FluxyStorage.Framebuffer framebuffer = this.framebuffer;
			if (framebuffer != null && this.simulationMaterial != null && id >= 0 && id < this.indices.Length)
			{
				this.UpdateTileData();
				int num = id + 1;
				int index = this.indices[num];
				this.simulationMaterial.SetInt("_TileIndex", num);
				this.simulationMaterial.SetColor("_ClearColor", this.containers[index].clearColor);
				Graphics.Blit(this.containers[index].clearTexture, framebuffer.stateA, this.simulationMaterial, 9);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004430 File Offset: 0x00002630
		public void UpdateTileData()
		{
			if (this.tilesDirty)
			{
				this.rects[0] = new Vector4(-1f, -1f, 3f, 3f);
				for (int i = 0; i < this.containers.Count; i++)
				{
					this.rects[i + 1] = new Vector4(0f, 0f, this.containers[i].size.x * 1024f, this.containers[i].size.y * 1024f);
					this.indices[i + 1] = i;
				}
				Vector2 vector = RectPacking.Pack(this.rects, this.indices, 1, this.containers.Count, 0);
				float d = Mathf.Max(vector.x, vector.y);
				for (int j = 0; j < this.containers.Count; j++)
				{
					this.rects[j + 1] /= d;
					float num = 32f;
					this.rects[j + 1].x = (float)Mathf.FloorToInt(this.rects[j + 1].x * num) / num;
					this.rects[j + 1].y = (float)Mathf.FloorToInt(this.rects[j + 1].y * num) / num;
					this.rects[j + 1].z = (float)Mathf.FloorToInt(this.rects[j + 1].z * num) / num;
					this.rects[j + 1].w = (float)Mathf.FloorToInt(this.rects[j + 1].w * num) / num;
				}
				Shader.SetGlobalVectorArray("_TileData", this.rects);
				this.tilesDirty = false;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004638 File Offset: 0x00002838
		private void UpdateContainerTransforms(FluxyStorage.Framebuffer fb)
		{
			for (int i = 0; i < this.containers.Count; i++)
			{
				int num = i + 1;
				int index = this.indices[num];
				this.containers[index].UpdateTransform();
				this.containers[index].UpdateMaterial(num, fb);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000468C File Offset: 0x0000288C
		private void UpdateContainers(FluxyStorage.Framebuffer fb, float deltaTime)
		{
			if (fb == null)
			{
				return;
			}
			for (int i = 0; i < this.containers.Count; i++)
			{
				int num = i + 1;
				int index = this.indices[num];
				this.simulationMaterial.SetInt("_TileIndex", num);
				if (this.containers[index].opacity > 0f)
				{
					Graphics.Blit(null, fb.tileID, this.simulationMaterial, 8);
					this.dissipation[num] = this.containers[index].dissipation;
					this.turbulence[num] = this.containers[index].turbulence;
					this.adhesion[num] = this.containers[index].adhesion;
					this.surfaceTension[num] = this.containers[index].surfaceTension;
					this.pressure[num] = this.containers[index].pressure;
					this.viscosity[num] = Mathf.Pow(1f - Mathf.Clamp01(this.containers[index].viscosity), deltaTime);
					this.wrapmode[num] = this.containers[index].boundaries;
					this.densityFalloff[num] = this.containers[index].edgeFalloff;
					Vector3 a = this.containers[index].UpdateVelocityAndGetAcceleration();
					this.externalForce[num] = this.containers[index].gravity + this.containers[index].externalForce - a * this.containers[index].accelerationScale;
					Vector4[] array = this.buoyancy;
					int num2 = num;
					FluxyContainer fluxyContainer = this.containers[index];
					Vector3 vector = Vector3.up;
					array[num2] = fluxyContainer.TransformWorldVectorToUVSpace(vector, this.rects[num]) * this.containers[index].buoyancy;
					Vector4[] array2 = this.offsets;
					int num3 = num;
					FluxyContainer fluxyContainer2 = this.containers[index];
					vector = this.containers[index].velocity * deltaTime;
					array2[num3] = fluxyContainer2.TransformWorldVectorToUVSpace(vector, this.rects[num]) * (1f - this.containers[index].velocityScale) + this.containers[index].positionOffset * deltaTime;
				}
			}
			this.simulationMaterial.SetFloatArray("_Pressure", this.pressure);
			this.simulationMaterial.SetFloatArray("_Viscosity", this.viscosity);
			this.simulationMaterial.SetFloatArray("_VortConf", this.turbulence);
			this.simulationMaterial.SetFloatArray("_Adhesion", this.adhesion);
			this.simulationMaterial.SetFloatArray("_SurfaceTension", this.surfaceTension);
			this.simulationMaterial.SetVectorArray("_Dissipation", this.dissipation);
			this.simulationMaterial.SetVectorArray("_ExternalForce", this.externalForce);
			this.simulationMaterial.SetVectorArray("_Buoyancy", this.buoyancy);
			this.simulationMaterial.SetVectorArray("_WrapMode", this.wrapmode);
			this.simulationMaterial.SetVectorArray("_EdgeFalloff", this.densityFalloff);
			this.simulationMaterial.SetVectorArray("_Offsets", this.offsets);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004A20 File Offset: 0x00002C20
		private void Splat(FluxyStorage.Framebuffer fb)
		{
			if (fb == null || this.simulationMaterial == null)
			{
				return;
			}
			Shader.SetGlobalTexture("_TileID", fb.tileID);
			for (int i = 0; i < this.containers.Count; i++)
			{
				int num = i + 1;
				int index = this.indices[num];
				if (this.containers[index].opacity > 0f)
				{
					for (int j = 0; j < this.containers[index].targets.Length; j++)
					{
						if (this.containers[index].targets[j] != null)
						{
							this.containers[index].targets[j].Splat(this.containers[index], fb, num, this.rects[num]);
						}
					}
					FluxyTargetProvider fluxyTargetProvider;
					if (this.containers[index].TryGetComponent<FluxyTargetProvider>(out fluxyTargetProvider))
					{
						List<FluxyTarget> targets = fluxyTargetProvider.GetTargets();
						for (int k = 0; k < targets.Count; k++)
						{
							if (targets[k] != null)
							{
								targets[k].Splat(this.containers[index], fb, num, this.rects[num]);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004B72 File Offset: 0x00002D72
		private void VelocityReadback(FluxyStorage.Framebuffer fb)
		{
			if (this.velocityReadbackTexture != null)
			{
				AsyncGPUReadback.Request(fb.velocityA, 0, TextureFormat.RGBAHalf, delegate(AsyncGPUReadbackRequest request)
				{
					if (request.hasError)
					{
						Debug.LogError("GPU readback error.");
						return;
					}
					if (this.velocityReadbackTexture != null)
					{
						this.velocityReadbackTexture.LoadRawTextureData<float>(request.GetData<float>(0));
						this.velocityReadbackTexture.Apply();
					}
				});
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004B9D File Offset: 0x00002D9D
		private void DensityReadback(FluxyStorage.Framebuffer fb)
		{
			if (this.densityReadbackTexture != null)
			{
				AsyncGPUReadback.Request(fb.stateA, 0, TextureFormat.RGBAHalf, delegate(AsyncGPUReadbackRequest request)
				{
					if (request.hasError)
					{
						Debug.LogError("GPU readback error.");
						return;
					}
					if (this.densityReadbackTexture != null)
					{
						this.densityReadbackTexture.LoadRawTextureData<float>(request.GetData<float>(0));
						this.densityReadbackTexture.Apply();
					}
				});
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public void UpdateSolver(float deltaTime)
		{
			if (this.storage != null && deltaTime > 0f)
			{
				this.UpdateLOD();
				this.UpdateTileData();
				FluxyStorage.Framebuffer framebuffer = this.framebuffer;
				this.UpdateContainerTransforms(framebuffer);
				if (this.visible && this.simulationMaterial != null)
				{
					this.Splat(framebuffer);
					int num = 0;
					while (deltaTime > 0f && (float)num++ < this.maxSteps)
					{
						float num2 = Mathf.Min(deltaTime, this.maxTimestep);
						deltaTime -= num2;
						this.UpdateContainers(framebuffer, num2);
						this.SimulationStep(framebuffer, num2);
					}
					if ((this.readable & FluxySolver.ReadbackMode.Density) != FluxySolver.ReadbackMode.None)
					{
						this.DensityReadback(framebuffer);
					}
					if ((this.readable & FluxySolver.ReadbackMode.Velocity) != FluxySolver.ReadbackMode.None)
					{
						this.VelocityReadback(framebuffer);
					}
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004C86 File Offset: 0x00002E86
		protected virtual void LateUpdate()
		{
			if (!this.IsActive)
			{
				return;
			}
			this.UpdateSolver(Time.deltaTime);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004C9C File Offset: 0x00002E9C
		public FluxySolver()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004DA8 File Offset: 0x00002FA8
		[CompilerGenerated]
		private void <VelocityReadback>b__67_0(AsyncGPUReadbackRequest request)
		{
			if (request.hasError)
			{
				Debug.LogError("GPU readback error.");
				return;
			}
			if (this.velocityReadbackTexture != null)
			{
				this.velocityReadbackTexture.LoadRawTextureData<float>(request.GetData<float>(0));
				this.velocityReadbackTexture.Apply();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004DF8 File Offset: 0x00002FF8
		[CompilerGenerated]
		private void <DensityReadback>b__68_0(AsyncGPUReadbackRequest request)
		{
			if (request.hasError)
			{
				Debug.LogError("GPU readback error.");
				return;
			}
			if (this.densityReadbackTexture != null)
			{
				this.densityReadbackTexture.LoadRawTextureData<float>(request.GetData<float>(0));
				this.densityReadbackTexture.Apply();
			}
		}

		// Token: 0x04000048 RID: 72
		private const int MAX_TILES = 17;

		// Token: 0x04000049 RID: 73
		[Header("Storage")]
		[Tooltip("Storage used to store and manage simulation buffers.")]
		public FluxyStorage storage;

		// Token: 0x0400004A RID: 74
		[Tooltip("Desired buffer resolution.")]
		[Delayed]
		[Min(16f)]
		public int desiredResolution = 128;

		// Token: 0x0400004B RID: 75
		[Tooltip("Supersampling used by density buffer. Eg. a value of 4 will use a density buffer that's 4 times the size of the velocity buffer.")]
		[Range(1f, 8f)]
		public int densitySupersampling = 2;

		// Token: 0x0400004C RID: 76
		[Tooltip("Dispose of this solver's buffers when culled by LOD.")]
		public bool disposeWhenCulled;

		// Token: 0x0400004D RID: 77
		[Tooltip("Allows this solver's data to be read back to the CPU.")]
		public FluxySolver.ReadbackMode readable;

		// Token: 0x0400004E RID: 78
		[Header("Simulation")]
		[Tooltip("Material used to update fluid simulation.")]
		public Material simulationMaterial;

		// Token: 0x0400004F RID: 79
		[Tooltip("Maximum amount of time advanced in a single simulation step.")]
		[Min(0.0001f)]
		public float maxTimestep = 0.008f;

		// Token: 0x04000050 RID: 80
		[Tooltip("Maximum amount of simulation steps taken in a single frame.")]
		[Min(1f)]
		public float maxSteps = 4f;

		// Token: 0x04000051 RID: 81
		[Tooltip("Type of pressure solver used: traditional, iterative Jacobi or separable poisson filter.")]
		public FluxySolver.PressureSolver pressureSolver;

		// Token: 0x04000052 RID: 82
		[Tooltip("Amount of iterations when the iterative pressure solver is being used.")]
		[Range(0f, 32f)]
		public int pressureIterations = 3;

		// Token: 0x04000053 RID: 83
		public bool IsActive = true;

		// Token: 0x04000054 RID: 84
		private LODGroup lodGroup;

		// Token: 0x04000055 RID: 85
		private int visibleLOD;

		// Token: 0x04000056 RID: 86
		private bool visible = true;

		// Token: 0x04000057 RID: 87
		private List<FluxyContainer> containers = new List<FluxyContainer>();

		// Token: 0x04000058 RID: 88
		private int framebufferID = -1;

		// Token: 0x04000059 RID: 89
		private bool tilesDirty;

		// Token: 0x0400005A RID: 90
		private int[] indices = new int[17];

		// Token: 0x0400005B RID: 91
		private Vector4[] rects = new Vector4[17];

		// Token: 0x0400005C RID: 92
		private Vector4[] externalForce = new Vector4[17];

		// Token: 0x0400005D RID: 93
		private Vector4[] buoyancy = new Vector4[17];

		// Token: 0x0400005E RID: 94
		private Vector4[] dissipation = new Vector4[17];

		// Token: 0x0400005F RID: 95
		private float[] pressure = new float[17];

		// Token: 0x04000060 RID: 96
		private float[] viscosity = new float[17];

		// Token: 0x04000061 RID: 97
		private float[] turbulence = new float[17];

		// Token: 0x04000062 RID: 98
		private float[] adhesion = new float[17];

		// Token: 0x04000063 RID: 99
		private float[] surfaceTension = new float[17];

		// Token: 0x04000064 RID: 100
		private Vector4[] wrapmode = new Vector4[17];

		// Token: 0x04000065 RID: 101
		private Vector4[] densityFalloff = new Vector4[17];

		// Token: 0x04000066 RID: 102
		private Vector4[] offsets = new Vector4[17];

		// Token: 0x04000067 RID: 103
		[CompilerGenerated]
		private FluxySolver.SolverCallback OnStep;

		// Token: 0x04000068 RID: 104
		[CompilerGenerated]
		private Texture2D <velocityReadbackTexture>k__BackingField;

		// Token: 0x04000069 RID: 105
		[CompilerGenerated]
		private Texture2D <densityReadbackTexture>k__BackingField;

		// Token: 0x0400006A RID: 106
		public static Camera LODCam;

		// Token: 0x02000027 RID: 39
		public enum PressureSolver
		{
			// Token: 0x040000E6 RID: 230
			Separable,
			// Token: 0x040000E7 RID: 231
			Iterative
		}

		// Token: 0x02000028 RID: 40
		[Flags]
		public enum ReadbackMode
		{
			// Token: 0x040000E9 RID: 233
			None = 0,
			// Token: 0x040000EA RID: 234
			Density = 1,
			// Token: 0x040000EB RID: 235
			Velocity = 2
		}

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x060000AF RID: 175
		public delegate void SolverCallback(FluxySolver solver);
	}
}
