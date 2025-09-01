using System;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x02000064 RID: 100
	public static class Define
	{
		// Token: 0x02000065 RID: 101
		public enum Result
		{
			// Token: 0x04000248 RID: 584
			None,
			// Token: 0x04000249 RID: 585
			Empty,
			// Token: 0x0400024A RID: 586
			Success,
			// Token: 0x0400024B RID: 587
			Cancel,
			// Token: 0x0400024C RID: 588
			Process,
			// Token: 0x0400024D RID: 589
			Warning = 10000,
			// Token: 0x0400024E RID: 590
			Error = 20000,
			// Token: 0x0400024F RID: 591
			Init_InvalidData = 20100,
			// Token: 0x04000250 RID: 592
			Init_InvalidPaintMap,
			// Token: 0x04000251 RID: 593
			Init_PaintMapNotReadable,
			// Token: 0x04000252 RID: 594
			RenderSetup_Exception = 20200,
			// Token: 0x04000253 RID: 595
			RenderSetup_UnknownError,
			// Token: 0x04000254 RID: 596
			RenderSetup_InvalidSource,
			// Token: 0x04000255 RID: 597
			RenderSetup_NoMeshOnRenderer,
			// Token: 0x04000256 RID: 598
			RenderSetup_InvalidType,
			// Token: 0x04000257 RID: 599
			RenderSetup_Unreadable,
			// Token: 0x04000258 RID: 600
			RenderSetup_Over65535vertices,
			// Token: 0x04000259 RID: 601
			VirtualMesh_UnknownError = 20300,
			// Token: 0x0400025A RID: 602
			VirtualMesh_InvalidSetup,
			// Token: 0x0400025B RID: 603
			VirtualMesh_InvalidRenderData,
			// Token: 0x0400025C RID: 604
			VirtualMesh_ImportError,
			// Token: 0x0400025D RID: 605
			VirtualMesh_SelectionException,
			// Token: 0x0400025E RID: 606
			VirtualMesh_SelectionUnknownError,
			// Token: 0x0400025F RID: 607
			VirtualMesh_InvalidSelection,
			// Token: 0x04000260 RID: 608
			CreateCloth_Exception = 20400,
			// Token: 0x04000261 RID: 609
			CreateCloth_UnknownError,
			// Token: 0x04000262 RID: 610
			CreateCloth_InvalidCloth,
			// Token: 0x04000263 RID: 611
			CreateCloth_InvalidSerializeData,
			// Token: 0x04000264 RID: 612
			CreateCloth_InvalidSetupList,
			// Token: 0x04000265 RID: 613
			CreateCloth_NoRenderer,
			// Token: 0x04000266 RID: 614
			CreateCloth_InvalidPaintMap,
			// Token: 0x04000267 RID: 615
			CreateCloth_PaintMapNotReadable,
			// Token: 0x04000268 RID: 616
			CreateCloth_PaintMapCountMismatch,
			// Token: 0x04000269 RID: 617
			Reduction_Exception = 20500,
			// Token: 0x0400026A RID: 618
			Reduction_UnknownError,
			// Token: 0x0400026B RID: 619
			Reduction_InitError,
			// Token: 0x0400026C RID: 620
			Reduction_SameDistanceException,
			// Token: 0x0400026D RID: 621
			Reduction_SimpleDistanceException,
			// Token: 0x0400026E RID: 622
			Reduction_ShapeDistanceException,
			// Token: 0x0400026F RID: 623
			Reduction_MaxSideLengthZero,
			// Token: 0x04000270 RID: 624
			Reduction_OrganizationError,
			// Token: 0x04000271 RID: 625
			Reduction_StoreVirtualMeshError,
			// Token: 0x04000272 RID: 626
			Reduction_CalcAverageException,
			// Token: 0x04000273 RID: 627
			Optimize_Exception = 20600,
			// Token: 0x04000274 RID: 628
			ProxyMesh_Exception = 20700,
			// Token: 0x04000275 RID: 629
			ProxyMesh_UnknownError,
			// Token: 0x04000276 RID: 630
			ProxyMesh_ApplySelectionError,
			// Token: 0x04000277 RID: 631
			ProxyMesh_ConvertError,
			// Token: 0x04000278 RID: 632
			ProxyMesh_Over32767Vertices,
			// Token: 0x04000279 RID: 633
			ProxyMesh_Over32767Edges,
			// Token: 0x0400027A RID: 634
			ProxyMesh_Over32767Triangles,
			// Token: 0x0400027B RID: 635
			MappingMesh_Exception = 20800,
			// Token: 0x0400027C RID: 636
			MappingMesh_UnknownError,
			// Token: 0x0400027D RID: 637
			MappingMesh_ProxyError,
			// Token: 0x0400027E RID: 638
			ClothInit_Exception = 22000,
			// Token: 0x0400027F RID: 639
			ClothInit_FailedAddRenderer,
			// Token: 0x04000280 RID: 640
			ClothProcess_Exception = 22100,
			// Token: 0x04000281 RID: 641
			ClothProcess_UnknownError,
			// Token: 0x04000282 RID: 642
			ClothProcess_Invalid,
			// Token: 0x04000283 RID: 643
			ClothProcess_InvalidRenderHandleList,
			// Token: 0x04000284 RID: 644
			ClothProcess_GenerateSelectionError,
			// Token: 0x04000285 RID: 645
			Constraint_Exception = 22200,
			// Token: 0x04000286 RID: 646
			Constraint_UnknownError,
			// Token: 0x04000287 RID: 647
			Constraint_CreateDistanceException,
			// Token: 0x04000288 RID: 648
			Constraint_CreateTriangleBendingException,
			// Token: 0x04000289 RID: 649
			Constraint_CreateInertiaException,
			// Token: 0x0400028A RID: 650
			Constraint_CreateSelfCollisionException,
			// Token: 0x0400028B RID: 651
			MagicaMesh_UnknownError = 22500,
			// Token: 0x0400028C RID: 652
			MagicaMesh_Invalid,
			// Token: 0x0400028D RID: 653
			MagicaMesh_InvalidRenderer,
			// Token: 0x0400028E RID: 654
			MagicaMesh_InvalidMeshFilter
		}

		// Token: 0x02000066 RID: 102
		public static class System
		{
			// Token: 0x06000155 RID: 341 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
			// Note: this type is marked as 'beforefieldinit'.
			static System()
			{
			}

			// Token: 0x0400028F RID: 655
			public const float Epsilon = 1E-08f;

			// Token: 0x04000290 RID: 656
			public const int SolverFrequency = 90;

			// Token: 0x04000291 RID: 657
			public const int MaxUpdateCount = 3;

			// Token: 0x04000292 RID: 658
			public const float SameSurfaceAngle = 80f;

			// Token: 0x04000293 RID: 659
			public const bool ReductionEnable = true;

			// Token: 0x04000294 RID: 660
			public const float ReductionSameDistance = 0.001f;

			// Token: 0x04000295 RID: 661
			public const bool ReductionDontMakeLine = true;

			// Token: 0x04000296 RID: 662
			public const float ReductionJoinPositionAdjustment = 1f;

			// Token: 0x04000297 RID: 663
			public const int ReductionMaxStep = 100;

			// Token: 0x04000298 RID: 664
			public const int MaxProxyMeshVertexCount = 32767;

			// Token: 0x04000299 RID: 665
			public const int MaxProxyMeshEdgeCount = 32767;

			// Token: 0x0400029A RID: 666
			public const int MaxProxyMeshTriangleCount = 32767;

			// Token: 0x0400029B RID: 667
			public const float ProxyMeshTrianglePairAngle = 20f;

			// Token: 0x0400029C RID: 668
			public const float FrictionMass = 3f;

			// Token: 0x0400029D RID: 669
			public const float DepthMass = 5f;

			// Token: 0x0400029E RID: 670
			public const float FrictionDampingRate = 0.6f;

			// Token: 0x0400029F RID: 671
			public const float PositionAverageExponent = 0.5f;

			// Token: 0x040002A0 RID: 672
			public const float MaxRealVelocity = 0.5f;

			// Token: 0x040002A1 RID: 673
			public const float TetherCompressionStiffness = 1f;

			// Token: 0x040002A2 RID: 674
			public const float TetherStretchStiffness = 1f;

			// Token: 0x040002A3 RID: 675
			public const float TetherStretchLimit = 0.03f;

			// Token: 0x040002A4 RID: 676
			public const float TetherStiffnessWidth = 0.3f;

			// Token: 0x040002A5 RID: 677
			public const float TetherCompressionVelocityAttenuation = 0.7f;

			// Token: 0x040002A6 RID: 678
			public const float TetherStretchVelocityAttenuation = 0.7f;

			// Token: 0x040002A7 RID: 679
			public const float DistanceVelocityAttenuation = 0.3f;

			// Token: 0x040002A8 RID: 680
			public const float DistanceVerticalStiffness = 1f;

			// Token: 0x040002A9 RID: 681
			public const float DistanceHorizontalStiffness = 0.5f;

			// Token: 0x040002AA RID: 682
			public const float TriangleBendingMaxAngle = 120f;

			// Token: 0x040002AB RID: 683
			public const float VolumeMinAngle = 90f;

			// Token: 0x040002AC RID: 684
			public const float MaxAngleLimit = 179f;

			// Token: 0x040002AD RID: 685
			public const int AngleLimitIteration = 3;

			// Token: 0x040002AE RID: 686
			public const float AngleLimitAttenuation = 0.9f;

			// Token: 0x040002AF RID: 687
			public const float MaxMovementSpeedLimit = 10f;

			// Token: 0x040002B0 RID: 688
			public const float MaxRotationSpeedLimit = 1440f;

			// Token: 0x040002B1 RID: 689
			public const float MaxParticleSpeedLimit = 10f;

			// Token: 0x040002B2 RID: 690
			public const int MaxColliderCount = 32;

			// Token: 0x040002B3 RID: 691
			public const float ColliderCollisionDynamicFrictionRatio = 1f;

			// Token: 0x040002B4 RID: 692
			public const float ColliderCollisionStaticFrictionRatio = 1f;

			// Token: 0x040002B5 RID: 693
			public const float CustomSkinningAngularAttenuation = 1f;

			// Token: 0x040002B6 RID: 694
			public const float CustomSkinningDistanceReduction = 0.6f;

			// Token: 0x040002B7 RID: 695
			public const float CustomSkinningDistancePow = 2f;

			// Token: 0x040002B8 RID: 696
			public const int SelfCollisionSolverIteration = 4;

			// Token: 0x040002B9 RID: 697
			public const float SelfCollisionFixedMass = 100f;

			// Token: 0x040002BA RID: 698
			public const float SelfCollisionFrictionMass = 10f;

			// Token: 0x040002BB RID: 699
			public const float SelfCollisionClothMass = 50f;

			// Token: 0x040002BC RID: 700
			public const float SelfCollisionSCR = 2f;

			// Token: 0x040002BD RID: 701
			public static readonly float SelfCollisionPointTriangleAngleCos = math.cos(math.radians(60f));

			// Token: 0x040002BE RID: 702
			public const int SelfCollisionIntersectDiv = 8;

			// Token: 0x040002BF RID: 703
			public const float SelfCollisionThicknessMin = 0.001f;

			// Token: 0x040002C0 RID: 704
			public const float SelfCollisionThicknessMax = 0.05f;
		}
	}
}
