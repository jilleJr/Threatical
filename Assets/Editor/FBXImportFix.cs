using UnityEditor;

public class FBXImportFix : AssetPostprocessor {

	void OnPreprocessModel() {
		(assetImporter as ModelImporter).globalScale = 100f;
	}


}
