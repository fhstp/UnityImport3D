#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace At.Ac.FhStp.Import3D.ImportTester
{
    public class Importer : MonoBehaviour
    {
        [SerializeField] private Transform modelParent = null!;
        [SerializeField] private UnityEvent modelsLoaded = new UnityEvent();

        private CancellationTokenSource? importCancelSource;

        private void ClearCurrentModels()
        {
            for (var i = 0; i < modelParent.childCount; i++)
            {
                var child = modelParent.GetChild(i);
                Destroy(child.gameObject);
            }
        }

        private async Task ImportModels(CancellationToken ct)
        {
            var modelDirectory = Path.Combine(
                System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "3D Objects/ImportTester");

            if (!Directory.Exists(modelDirectory))
            {
                Debug.LogError($"Directory \"{modelDirectory}\" does not exist!");
                return;
            }

            var config = ImportConfig.Default with
            {
                ActivateRoot = false,
            };

            async Task<GameObject?> ImportSingle(string path)
            {
                try
                {
                    var model = await Import.SingleAsync(path, config);

                    if (ct.IsCancellationRequested)
                    {
                        DestroyImmediate(model);
                        return null;
                    }

                    return model;
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }

                return null;
            }

            var filePaths = Directory.GetFiles(modelDirectory)
                                     .Where(Import.SupportsFileExtension);
            var importTasks = filePaths.Select(ImportSingle);
            var models = await Task.WhenAll(importTasks);

            ClearCurrentModels();

            foreach (var model in models)
            {
                if (model == null) continue;
                model.transform.SetParent(modelParent, true);
            }
            
            modelsLoaded.Invoke();
        }

        private void CancelImport()
        {
            importCancelSource?.Cancel();
            importCancelSource = null;
        }

        public void StartImportingModels()
        {
            CancelImport();

            importCancelSource = new CancellationTokenSource();
            _ = ImportModels(importCancelSource.Token);
        }

        private void OnDisable()
        {
            CancelImport();
        }

        private void Start()
        {
            StartImportingModels();
        }
    }
}