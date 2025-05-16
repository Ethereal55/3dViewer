using UnityEngine;
using System.Collections.Generic;

public class WireframeToggler : MonoBehaviour
{
    public MonoBehaviour wireframeScript;
    private bool _isEnabled;

    private Dictionary<MeshRenderer, Component> _addedComponents = new Dictionary<MeshRenderer, Component>();

    // При добавлении новой модели
    public void DisableWireframe()
    {
        _isEnabled = false;
    }

    public void ToggleWireframe()
    {
        MeshRenderer[] meshRenderers = FindObjectsOfType<MeshRenderer>(true);

        if (!_isEnabled)
        {
            foreach (MeshRenderer mesh in meshRenderers)
            {
                if (wireframeScript != null)
                {
                    System.Type componentType = wireframeScript.GetType();
                    Component existingComponent = mesh.GetComponent(componentType);

                    if (existingComponent == null)
                    {
                        Component newComponent = mesh.gameObject.AddComponent(componentType);
                        _addedComponents[mesh] = newComponent;
                    }
                }
            }
            _isEnabled = true;
        }
        else
        {
            foreach (MeshRenderer mesh in meshRenderers)
            {
                if (_addedComponents.TryGetValue(mesh, out Component component))
                {
                    Destroy(component);
                    _addedComponents.Remove(mesh);
                }
            }

            _addedComponents.Clear();
            _isEnabled = false;
        }
    }
}