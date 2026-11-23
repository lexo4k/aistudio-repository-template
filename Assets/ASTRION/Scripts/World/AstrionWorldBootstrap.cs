using UnityEngine;

namespace Astrion
{
    public static class AstrionWorldBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateWorld()
        {
            if (Object.FindObjectOfType<AstrionWorldMarker>() != null) return;
            var root = new GameObject("ASTRION — O Último Horizonte");
            root.AddComponent<AstrionWorldMarker>();
            BuildLighting(root.transform);
            BuildGround(root.transform);
            BuildMountains(root.transform);
            BuildRiver(root.transform);
            BuildForest(root.transform);
            CreateLandmarks(root.transform);

            var player = new GameObject("Player");
            player.transform.position = new Vector3(0f, 2f, -8f);
            var character = player.AddComponent<CharacterController>();
            character.height = 1.8f;
            character.radius = .35f;
            character.center = new Vector3(0f, .9f, 0f);
            player.AddComponent<AstrionThirdPersonController>();
            var body = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            body.name = "Explorador";
            body.transform.SetParent(player.transform, false);
            body.transform.localPosition = new Vector3(0f, .9f, 0f);
            body.transform.localScale = new Vector3(.72f, .9f, .72f);
            Object.Destroy(body.GetComponent<Collider>());
            body.GetComponent<Renderer>().material = MakeMaterial(new Color(.18f, .24f, .34f));

            var cameraObject = new GameObject("Astrion Camera");
            var camera = cameraObject.AddComponent<Camera>();
            camera.tag = "MainCamera";
            camera.fieldOfView = 65f;
            cameraObject.AddComponent<AudioListener>();
            cameraObject.AddComponent<AstrionCameraOrbit>().Target = player.transform;
        }

        private static void BuildLighting(Transform parent)
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(.38f, .48f, .62f);
            RenderSettings.ambientEquatorColor = new Color(.24f, .30f, .34f);
            RenderSettings.ambientGroundColor = new Color(.10f, .13f, .10f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(.58f, .67f, .73f);
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = .0025f;
            var sun = new GameObject("Sun");
            sun.transform.SetParent(parent);
            sun.transform.rotation = Quaternion.Euler(32f, -35f, 0f);
            var light = sun.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.25f;
            light.color = new Color(1f, .88f, .72f);
            light.shadows = LightShadows.Soft;
        }

        private static void BuildGround(Transform parent)
        {
            var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Vale — terreno";
            ground.transform.SetParent(parent);
            ground.transform.position = new Vector3(0f, -.2f, 20f);
            ground.transform.localScale = new Vector3(18f, 1f, 22f);
            ground.GetComponent<Renderer>().material = MakeMaterial(new Color(.20f, .31f, .19f));
        }

        private static void BuildMountains(Transform parent)
        {
            var random = new System.Random(21);
            for (int i = 0; i < 34; i++)
            {
                float side = i % 2 == 0 ? -1f : 1f;
                float z = -25f + (float)random.NextDouble() * 115f;
                float x = side * (42f + (float)random.NextDouble() * 45f);
                float height = 28f + (float)random.NextDouble() * 55f;
                var mountain = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                mountain.name = "Pico distante";
                mountain.transform.SetParent(parent);
                mountain.transform.position = new Vector3(x, height * .34f, z);
                mountain.transform.rotation = Quaternion.Euler(0f, (float)random.NextDouble() * 360f, 0f);
                mountain.transform.localScale = new Vector3(18f + height * .25f, height, 18f + height * .25f);
                mountain.GetComponent<Renderer>().material = MakeMaterial(Color.Lerp(new Color(.22f,.29f,.31f), new Color(.48f,.52f,.53f), (float)random.NextDouble()));
            }
        }

        private static void BuildRiver(Transform parent)
        {
            var river = GameObject.CreatePrimitive(PrimitiveType.Cube);
            river.name = "Rio do Horizonte";
            river.transform.SetParent(parent);
            river.transform.position = new Vector3(5f, .02f, 24f);
            river.transform.localScale = new Vector3(7f, .08f, 105f);
            river.GetComponent<Renderer>().material = MakeMaterial(new Color(.12f,.39f,.52f), .72f);
        }

        private static void BuildForest(Transform parent)
        {
            var random = new System.Random(88);
            for (int i = 0; i < 150; i++)
            {
                float x = (float)random.NextDouble() * 75f - 37.5f;
                float z = (float)random.NextDouble() * 100f - 20f;
                if (Mathf.Abs(x - 5f) < 7f) continue;
                var trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                trunk.name = "Árvore — tronco";
                trunk.transform.SetParent(parent);
                trunk.transform.position = new Vector3(x, 1.5f, z);
                trunk.transform.localScale = new Vector3(.35f, 1.5f, .35f);
                trunk.GetComponent<Renderer>().material = MakeMaterial(new Color(.22f,.13f,.08f));
                var crown = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                crown.name = "Árvore — copa";
                crown.transform.SetParent(parent);
                crown.transform.position = new Vector3(x, 4f, z);
                crown.transform.localScale = new Vector3(2.4f, 3.1f, 2.4f);
                crown.GetComponent<Renderer>().material = MakeMaterial(new Color(.10f,.25f,.14f));
            }
        }

        private static void CreateLandmarks(Transform parent)
        {
            var beacon = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            beacon.name = "Marco antigo";
            beacon.transform.SetParent(parent);
            beacon.transform.position = new Vector3(-22f, 2f, 48f);
            beacon.transform.localScale = new Vector3(1.2f, 4f, 1.2f);
            beacon.GetComponent<Renderer>().material = MakeMaterial(new Color(.48f,.42f,.30f));
        }

        private static Material MakeMaterial(Color color, float metallic = 0f)
        {
            var material = new Material(Shader.Find("Standard"));
            material.color = color;
            material.SetFloat("_Metallic", metallic);
            material.SetFloat("_Glossiness", .28f);
            return material;
        }
    }

    public sealed class AstrionWorldMarker : MonoBehaviour { }
}
