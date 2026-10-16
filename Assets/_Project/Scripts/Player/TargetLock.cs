using UnityEngine;

// Hedef kilitleme v1 - en yakin dusmani bulur
// Sonra: hedef degistirme, coklu dusman onceligi, kamera uyumu (TUR 7+)
public static class TargetLock
{
    public static Transform Nearest(Transform from, float maxDist = 8f)
    {
        EnemyAI best = null;
        float bestD = maxDist;
        var all = Object.FindObjectsOfType<EnemyAI>();
        foreach (var e in all)
        {
            if (e == null || !e.gameObject.activeInHierarchy) continue;
            float d = Vector3.Distance(from.position, e.transform.position);
            if (d < bestD) { bestD = d; best = e; }
        }
        return best != null ? best.transform : null;
    }
}
