using TriLibCore.General;
using UnityEngine;
using TriLibCore.Extensions;
using UnityEngine.UI;

namespace TriLibCore.Samples
{
    public class LoadModelFromFilePickerSample : MonoBehaviour
    {
        public AccurateMeshCounter AccurateMeshCounter;
        public WireframeToggler WireframeToggler;
        //public GlobalSceneInfo GlobalSceneInfo;

        private GameObject _loadedGameObject;

        [SerializeField]
        private Button _loadModelButton;

        [SerializeField]
        private Text _progressText;

        // Creates the AssetLoaderOptions instance and displays the Model file-picker.
        // You can create the AssetLoaderOptions by right clicking on the Assets Explorer and selecting "TriLib->Create->AssetLoaderOptions->Pre-Built AssetLoaderOptions".
        public void LoadModel()
        {
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            var assetLoaderFilePicker = AssetLoaderFilePicker.Create();
            assetLoaderFilePicker.LoadModelFromFilePickerAsync("Select a Model file", OnLoad, OnMaterialsLoad, OnProgress, OnBeginLoad, OnError, null, assetLoaderOptions);
        }

        // Called when the the Model begins to load.
        // <param name="filesSelected">Indicates if any file has been selected.</param>
        private void OnBeginLoad(bool filesSelected)
        {
            _loadModelButton.interactable = !filesSelected;
            _progressText.enabled = filesSelected;
        }


        // Called when any error occurs.
        // <param name="obj">The contextualized error, containing the original exception and the context passed to the method where the error was thrown.</param>
        private void OnError(IContextualizedError obj)
        {
            Debug.LogError($"An error occurred while loading your Model: {obj.GetInnerException()}");
        }


        // Called when the Model loading progress changes.
        // <param name="assetLoaderContext">The context used to load the Model.</param>
        // <param name="progress">The loading progress.</param>
        private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
        {
            _progressText.text = $"Progress: {progress:P}";
        }

        // Called when the Model (including Textures and Materials) has been fully loaded.
        // <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
        // <param name="assetLoaderContext">The context used to load the Model.</param>
        private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
        {
            if (assetLoaderContext.RootGameObject != null)
            {
                Debug.Log("Model fully loaded.");
                AccurateMeshCounter.CalculateVerticles();
            }
            else
            {
                Debug.Log("Model could not be loaded.");
            }
            _loadModelButton.interactable = true;
            _progressText.enabled = false;
        }

        // Called when the Model Meshes and hierarchy are loaded.
        // <remarks>The loaded GameObject is available on the assetLoaderContext.RootGameObject field.</remarks>
        // <param name="assetLoaderContext">The context used to load the Model.</param>
        private void OnLoad(AssetLoaderContext assetLoaderContext)
        {
            WireframeToggler.DisableWireframe();
            //GlobalSceneInfo.Refresh();

            if (_loadedGameObject != null)
            {
                Destroy(_loadedGameObject);
            }
            _loadedGameObject = assetLoaderContext.RootGameObject;
            if (_loadedGameObject != null)
            {
                Camera.main.FitToBounds(assetLoaderContext.RootGameObject, 2f);
            }
        }
    }
}
