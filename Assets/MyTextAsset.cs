using UnityEngine;  
using UnityEditor;  
using System.IO;  
  
[System.Serializable]  
public class MyTextAsset  
{  
    public Object textAsset;  
      
    private string text = string.Empty;  
    private TextAsset asset = null;  
    public string Text  
    {  

        get  
        {  
            if (textAsset is DefaultAsset)  
            {  
                if (string.IsNullOrEmpty(text))  
                {  
                    text = File.ReadAllText(AssetDatabase.GetAssetPath(textAsset));  
                }  
                return text;  
            }  
            else if (textAsset is TextAsset)  
            {  
                if (asset == null)  
                {  
                    asset = textAsset as TextAsset;  
                }  
                return asset.text;  
            }  
            else  
            {  
                return null;  
            }  
        }  
    }  
}  