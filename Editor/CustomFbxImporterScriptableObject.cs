using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class CustomFbxImporterScriptableObject : ScriptableObject
{
    //-------------------------------------
    // 定数関連
    //-------------------------------------
    #region ===== CONSTS =====

    const string DEFAULT_SCRIPTABLEOBJECT_GENERATED_PATH = "Assets/CustomAssetImportPreset/ScriptableObjects/";
    const string DEFAULT_SCRIPTABLEOBJECT_FILENAME = "CustomFbxImporter.asset";

    #endregion //) ===== CONSTS =====

	/// <summary>
	/// Model 関連のimport設定
	/// </summary>
    [System.Serializable]
    public class ModelImportSettings
    {
		[Header("Meshes")]
        public float ScaleFactor = 1.0f;
		public bool UseFileScale = true;
		public ModelImporterMeshCompression MeshCompression = ModelImporterMeshCompression.Off;
		public bool ReadWriteEnabled = true;
		public bool OptimizeMesh = true;
		public bool ImportBlendShapes = true;
		// public bool GenerateCollider = false;
		public bool KeepQuads = false;
		public bool WeldticesVer = true;
		public bool ImportVisibly = true;
		public bool ImportCameras = true;
		public bool ImportLights = true;
		public bool SwapUVs = false;
		public bool GenerateLightmapUVs = false;

		[Header("Normal & Tangents ")]
		public ModelImporterNormals Normals = ModelImporterNormals.Import;
		public ModelImporterTangents Tangents = ModelImporterTangents.Import;

		[Header("Materials")]
		public bool ImportMaterials = true;
		public ModelImporterMaterialName MaterialName		=  ModelImporterMaterialName.BasedOnTextureName;
		public ModelImporterMaterialSearch MaterialSearch	=  ModelImporterMaterialSearch.RecursiveUp;
    }

	/// <summary>
	/// Rig関連のimport設定
	/// </summary>
    [System.Serializable]
    public class RigImportSettings
    {
        public ModelImporterAnimationType AnimationType = ModelImporterAnimationType.Generic;
		public bool OptimizeGameObjects = false;
    }

	/// <summary>
	/// Animation関連のimport設定
	/// </summary>
    [System.Serializable]
    public class AnimImportSettings
    {
		public bool ImportAnimation = true;
    }

	 //-------------------------------------
    // MemberVariables
    //-------------------------------------
    #region ===== MEMBER_VARIABLES =====
    
    /// <summary>
    ///  対象のPath
    /// </summary>
    [Tooltip("対象のpath")]
    public string Path = "Assets/";

    /// <summary>
    /// 対象となるテクスチャの拡張子リスト
    /// </summary>
    [Tooltip("対象となる拡張子")]
    public string[] ExtentionList = new string[]{".png"};

    /// <summary>
    /// Import設定
    /// </summary>
    public ModelImportSettings ModelImport = new ModelImportSettings();
	public RigImportSettings RigImport = new RigImportSettings();
	public AnimImportSettings AnimImport = new AnimImportSettings();

    #endregion //) ===== MEMBER_VARIABLES =====
    
}
