using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// ScriptableObject で設定したImport設定でImportするツール
/// </summary>
public class CustomTextureAssetImporter : AssetPostprocessor
{
    void OnPreprocessTexture ()
    {
        CustomTextureImportScriptableObject[] importers = GetAllTargetScriptableObject();

        if( importers == null || importers.Length < 1 )
        {
            return;
        }

        CustomTextureImportScriptableObject targetImporter = null; 
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

        var importer = assetImporter as TextureImporter;
        importer.textureType            = targetImporter.ImporterSettings.TextureType;
        importer.alphaIsTransparency    = targetImporter.ImporterSettings.AlphaIsTransparency;
        importer.alphaSource            = targetImporter.ImporterSettings.AlphaSource;
        importer.mipmapEnabled          = targetImporter.ImporterSettings.MipmapEnabled;
        importer.wrapMode               = targetImporter.ImporterSettings.WrapMode;
    }

    /// <summary>
    /// 指定のScriptableObject を全取得
    /// </summary>
    /// <returns></returns>
    private CustomTextureImportScriptableObject[] GetAllTargetScriptableObject() 
    {
        List<CustomTextureImportScriptableObject> ret = new List<CustomTextureImportScriptableObject>();
        var guids = UnityEditor.AssetDatabase.FindAssets ("t:CustomTextureImportScriptableObject");
        if( guids == null || guids.Length < 1)
        {
            return null;
        }

        for (int i = 0; i < guids.Length; i++)
        {
            ret.Add( AssetDatabase.LoadAssetAtPath<CustomTextureImportScriptableObject>(AssetDatabase.GUIDToAssetPath (guids [i])) );
        }

        // Path が長い方からチェック(そのための符号反転)
        ret.Sort( (x,y) =>( -1 * string.Compare( x.Path, y.Path ) ));

        return ret.ToArray();
    }
}