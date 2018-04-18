using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 
/// </summary>
public class CustomTextureImportScriptableObject : ScriptableObject
{
    //-------------------------------------
    // 定数関連
    //-------------------------------------
    #region ===== CONSTS =====

    const string DEFAULT_SCRIPTABLEOBJECT_GENERATED_PATH = "Assets/CustomAssetImportPreset/ScriptableObjects/";
    const string DEFAULT_SCRIPTABLEOBJECT_FILENAME = "CustomTexImporter.asset";

    #endregion //) ===== CONSTS =====

    [System.Serializable]
    public class ImportSettings
    {
        //-------------------------------------
        // MemberVariables
        //-------------------------------------
        #region ===== MEMBER_VARIABLES =====

        public TextureImporterType TextureType = TextureImporterType.Default;

        public bool sRGBTexture = true;
        public TextureImporterAlphaSource AlphaSource = TextureImporterAlphaSource.None;
        public bool AlphaIsTransparency = true;
        
        public bool MipmapEnabled = false;
        public bool Lightmap = false;
        public bool Normalmap = false;
        public bool LinearTexture = false;
        public TextureWrapMode WrapMode = TextureWrapMode.Repeat;

        #endregion //) ===== MEMBER_VARIABLES =====
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
    public ImportSettings ImporterSettings = new ImportSettings();

    #endregion //) ===== MEMBER_VARIABLES =====


    [MenuItem("Assets/UniLibTool/CreateCustomTexImporterObject")]
    public static void CreateTargetObject()
    {
        CustomTextureImportScriptableObject targetObject = CreateInstance<CustomTextureImportScriptableObject> ();
        if( !System.IO.Directory.Exists(DEFAULT_SCRIPTABLEOBJECT_GENERATED_PATH))
        {
            System.IO.Directory.CreateDirectory(DEFAULT_SCRIPTABLEOBJECT_GENERATED_PATH);
        }
        string path = AssetDatabase.GenerateUniqueAssetPath( DEFAULT_SCRIPTABLEOBJECT_GENERATED_PATH+DEFAULT_SCRIPTABLEOBJECT_FILENAME);
        AssetDatabase.CreateAsset(targetObject, path);
        AssetDatabase.Refresh ();
    }
}
