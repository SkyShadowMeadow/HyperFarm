using EzySlice;
using UnityEngine;

namespace Harvesting
{
    public class SlicePlaine : MonoBehaviour
    {
        public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null) {
            return obj.Slice(transform.position, transform.up, crossSectionMaterial);
        }

#if UNITY_EDITOR
    
        public void OnDrawGizmos() {
            EzySlice.Plane cuttingPlane = new EzySlice.Plane();
            cuttingPlane.Compute(transform);
            cuttingPlane.OnDebugDraw();
        }

#endif
    }
}
