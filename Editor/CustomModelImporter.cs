using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// ScriptableObject で設定したImport設定でImportするツール
/// </summary>
public class CustomModelImporter : AssetPostprocessor
{
    void OnPreprocessTexture ()
    {
        CustomFbxImporterScriptableObject[] importers = GetAllTargetScriptableObject();

        if( importers == null || importers.Length < 1 )
        {
            return;
        }

        CustomFbxImporterScriptableObject targetImporter = null; 
        for (int i = 0; i < importers.Length; i++)
        {
            var item = importers[i];
            // 対象のPathチェック
            if( !assetPath.Contains( item.Path))
            {
                continue;
            }

            // 拡張子指定チェック
            bool isTargetExtention = false;
            if( item.ExtentionList != null && item.ExtentionList.Length > 0)
            {
                for (int j = 0; j < item.ExtentionList.Length; j++)
                {
                    isTargetExtention |= assetPath.Contains(item.ExtentionList[j] );
                }
            }
            else
            {
                //指定なし
                isTargetExtention = true;
            }

            if( !isTargetExtention)
            {
                continue;
            }
            targetImporter = item;
            break;
        }

        if( targetImporter == null )
        {
            return;
        }

        var importer = assetImporter as ModelImporter;
		importer.globalScale			= targetImporter.ModelImport.ScaleFactor;
		importer.useFileScale			= targetImporter.ModelImport.UseFileScale;
		importer.meshCompression		= targetImporter.ModelImport.MeshCompression;
		importer.isReadable				= targetImporter.ModelImport.ReadWriteEnabled;
		importer.optimizeMesh			= targetImporter.ModelImport.OptimizeMesh;
		importer.importBlendShapes		= targetImporter.ModelImport.ImportBlendShapes;
		importer.keepQuads				= targetImporter.ModelImport.KeepQuads;
		importer.weldVertices			= targetImporter.ModelImport.WeldticesVer;
		importer.importVisibility		= targetImporter.ModelImport.ImportVisibly;
		importer.importCameras			= targetImporter.ModelImport.ImportCameras;
		importer.importLights			= targetImporter.ModelImport.ImportLights;
		importer.swapUVChannels			= targetImporter.ModelImport.SwapUVs;
		importer.generateSecondaryUV	= targetImporter.ModelImport.GenerateLightmapUVs;

		importer.importNormals			= targetImporter.ModelImport.Normals;
		importer.importTangents			= targetImporter.ModelImport.Tangents;

		importer.importMaterials		= targetImporter.ModelImport.ImportMaterials;
		importer.materialName			= targetImporter.ModelImport.MaterialName;
		importer.materialSearch			= targetImporter.ModelImport.MaterialSearch;

		// Rig
		importer.animationType			= targetImporter.RigImport.AnimationType;
		importer.optimizeGameObjects	= targetImporter.RigImport.OptimizeGameObjects;

		// Animations
		importer.importAnimation		= targetImporter.AnimImport.ImportAnimation;
    }

    /// <summary>
    /// 指定のScriptableObject を全取得
    /// </summary>
    /// <returns></returns>
    private CustomFbxImporterScriptableObject[] GetAllTargetScriptableObject() 
    {
        List<CustomFbxImporterScriptableObject> ret = new List<CustomFbxImporterScriptableObject>();
        var guids = UnityEditor.AssetDatabase.FindAssets ("t:CustomFbxImporterScriptableObject");
        if( guids == null || guids.Length < 1)
        {
            return null;
        }

        for (int i = 0; i < guids.Length; i++)
        {
            ret.Add( AssetDatabase.LoadAssetAtPath<CustomFbxImporterScriptableObject>(AssetDatabase.GUIDToAssetPath (guids [i])) );
        }

        // Path が長い方からチェック(そのための符号反転)
        ret.Sort( (x,y) =>( -1 * string.Compare( x.Path, y.Path ) ));

        return ret.ToArray();
    }
}
