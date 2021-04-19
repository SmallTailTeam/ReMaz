using System;
using Cysharp.Threading.Tasks;
using SmallTail.Preload.Attributes;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ReMaz.Scenes
{
    [Preloaded]
    public class SceneChanger : MonoBehaviour, IProgress<float>
    {
        public IObservable<float> SceneLoading => _sceneLoading;
        
        private ISubject<float> _sceneLoading;

        private void Awake()
        {
            _sceneLoading = new Subject<float>();
        }

        public async UniTask LoadScene(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName).ToUniTask(this);
        }

        public void Report(float value)
        {
            _sceneLoading.OnNext(value);
        }
    }
}