/*
 * File: Layout.cs
 * -------------------------
 * Used to arrange game elements in the editor.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// Used to arrange game elements in the editor.
/// Works like a layout group, but in the editor.
/// Useful for aligning obstacles.
/// </summary>
/// <remarks>
/// If the scene is saved, the layout will be applied to the objects, at which point the script can be removed.
/// </remarks>

[ExecuteInEditMode]
public class Layout : MonoBehaviour
{
    public float spacing = 1.0f; // The space between objects
    public bool isVertical = true; // Flag to switch between vertical and horizontal
    public bool useChildSize = false; // Flag to adjust spacing based on child size

    /// <summary>
    /// Apply the layout to the child objects.
    /// </summary>
    void Update()
    {
        float position = 0f; // Track the current position

        // Get all child objects
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            // If useChildSize is true, add the size of the child object to the position
            if (useChildSize)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    if (isVertical)
                    {
                        position -= renderer.bounds.size.y;
                    }
                    else
                    {
                        position += renderer.bounds.size.x;
                    }
                }
            }

            // Position each child object
            if (isVertical)
            {
                child.localPosition = new Vector3(0, position - i * spacing, 0);
            }
            else
            {
                child.localPosition = new Vector3(position + i * spacing, 0, 0);
            }

            // If useChildSize is true, add the spacing after positioning the child
            if (useChildSize)
            {
                position -= spacing;
            }
        }
    }
}