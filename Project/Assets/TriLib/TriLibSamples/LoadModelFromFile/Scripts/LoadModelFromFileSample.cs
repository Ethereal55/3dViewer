#pragma warning disable 649
using TriLibCore.General;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace TriLibCore.Samples
{
    /// Represents a sample that loads the "TriLibSample.obj" Model from the "Models" folder.
    public class LoadModelFromFileSample : MonoBehaviour
    {

#if UNITY_EDITOR
        /// The Model asset used to locate the filename when running in Unity Editor.
        [SerializeField]
        private Object ModelAsset;
#endif

        /// Returns the path to the "TriLibSample.obj" Model.
        private string ModelPath
        {
            get
            {
#if UNITY_EDITOR
                return AssetDatabase.GetAssetPath(ModelAsset);
#else
                return "Models/TriLibSampleModel.obj";
#endif
            }
        }

        
        /// Loads the "Models/TriLibSample.obj" Model using the given AssetLoaderOptions.
        /// You can create the AssetLoaderOptions by right clicking on the Assets Explorer and selecting "TriLib->Create->AssetLoaderOptions->Pre-Built AssetLoaderOptions".

        private void Start()
        {
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            AssetLoader.LoadModelFromFile(ModelPath, OnLoad, OnMaterialsLoad, OnProgress, OnError, null, assetLoaderOptions);
        }

        
        /// Called when any error occurs.
        /// <param name="obj">The contextualized error, containing the original exception and the context passed to the method where the error was thrown.</param>
        private void OnError(IContextualizedError obj)
        {
            Debug.LogError($"An error occurred while loading your Model: {obj.GetInnerException()}");
        }

        
        /// Called when the Model loading progress changes.
        /// <param name="assetLoaderContext">The context used to load the Model.</param>
        /// <param name="progress">The loading progress.</param>
        private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
        {
            Debug.Log($"Loading Model. Progress: {progress:P}");
        }

        
        /// Called when the Model (including Textures and Materials) has been fully loaded.
        /// <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
        /// <param name="assetLoaderContext">The context used to load the Model.</param>
        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            Debug.Log("Materials loaded. Model fully loaded.");
        }

        
        /// Called when the Model Meshes and hierarchy are loaded.
        
        /// <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
        /// <param name="assetLoaderContext">The context used to load the Model.</param>
        private void OnLoad(AssetLoaderContext assetLoaderContext)
        {
            Debug.Log("Model loaded. Loading materials.");
        }
    }
}
