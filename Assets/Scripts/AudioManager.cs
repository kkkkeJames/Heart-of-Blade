// using FMOD;
// using UnityEngine;
// using FMODUnity;
// using Debug = UnityEngine.Debug;
//
// // TODO: use enum for switching ost/sfx statements
//
// public class AudioManager : MonoBehaviour
// {
//     public static AudioManager Instance;
//
//     [Header("Volume Controller")]
//     [SerializeField] private AnimationCurve volumeAnimationCurve;
//     private const float FMOD_MAX_VOLUME = 1.25f;
//     public int currSfxLevel { get; private set; }
//     public int currOstLevel { get; private set; }
//     
//     public readonly int maxSounLevels = 5;
//     public readonly int defaultSoundLevel = 4;
//     
//     [Header("BGM & Amb")]
//     private FMOD.Studio.EventInstance currBgmState;
//     private FMOD.Studio.EventInstance currAmbState;
//
//     private void Awake()
//     {
//         if (Instance != null) 
//         {
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//         DontDestroyOnLoad(gameObject);
//
//         currOstLevel = defaultSoundLevel;
//         currSfxLevel = defaultSoundLevel;
//     }
//     
//     public bool IsPlaying(FMOD.Studio.EventInstance instance) 
//     {
//         instance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
//         return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
//     }
//
//     private void InitializeNewBgmEvent(EventReference bgmAudioEvent)
//     {
//         var bgmState = RuntimeManager.CreateInstance(bgmAudioEvent);
//         currBgmState = bgmState;
//     }
//     
//     public void PlayBgmAudio(EventReference bgmAudioEvent)
//     {
//         if (IsPlaying(currBgmState))
//         {
//             StopBgmAudio();
//         }
//         
//         InitializeNewBgmEvent(bgmAudioEvent);
//         
//         if (!IsPlaying(currBgmState))
//         {
//             RuntimeManager.AttachInstanceToGameObject(currBgmState, Player.Instance.transform, false);
//             currBgmState.start();
//         }
//     }
//     private void InitializeNewAmbEvent(EventReference ambAudioEvent)
//     {
//         var ambState = RuntimeManager.CreateInstance(ambAudioEvent);
//         currAmbState = ambState;
//     }
//     
//     public void PlayAmbAudio(EventReference ambAudioEvent)
//     {
//         if (IsPlaying(currAmbState))
//         {
//             StopAmbAudio();
//         }
//         
//         InitializeNewAmbEvent(ambAudioEvent);
//         
//         if (!IsPlaying(currAmbState))
//         {
//             RuntimeManager.AttachInstanceToGameObject(currAmbState, Player.Instance.transform, false);
//             currAmbState.start();
//         }
//     }
//     public void StopAmbAudio()
//     {
//         currAmbState.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//         currAmbState.release();
//     }
//     public void StopBgmAudio()
//     {
//         currBgmState.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
//         currBgmState.release();
//     }
//
//     public float GetVcaVolume(string vca)
//     {
//         var res = RuntimeManager.GetVCA($"vca:/{vca}VCA").getVolume(out float result);
//         if (res != RESULT.OK)
//             Debug.Log("Get Vca Volume failed.");
//         
//         return result;
//     }
//     
//     /// advances to the next volume gate. Resets to 0 if tries to advance it at max volume.
//     public void SetVolume(string vcaPath, float val)
//     {
//         var vca = RuntimeManager.GetVCA($"vca:/{vcaPath}VCA");
//         if (!vca.isValid())
//         {
//             Debug.LogError($"vca path {vcaPath} is not Valid");
//         }
//         else
//         {
//             float levelToSet = val;
//             if (vcaPath == "Sfx")
//             {
//                 currSfxLevel = (int)levelToSet;
//             }
//             else
//             {
//                 currOstLevel = (int)levelToSet;
//             }
//             
//             float currSfxLevelNormalized = levelToSet / maxSounLevels;
//             
//             float volumeToSet = volumeAnimationCurve.Evaluate(currSfxLevelNormalized);
//             if (volumeToSet < 0 || volumeToSet > 1)
//                 Debug.LogError("Volume to set has to be between 0 and 1.");
//             
//             var res = vca.setVolume(volumeToSet * FMOD_MAX_VOLUME);
//             if (res != RESULT.OK)
//                 Debug.LogError($"SetOstVolumeFailed with {res}");
//         }
//     }
//     
//     public void ChangeGlobalParaByName(string name, float value)
//     {
//         FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription;
//         var result =
//             RuntimeManager.StudioSystem.getParameterDescriptionByName(name, out parameterDescription);
//         if (result != RESULT.OK)
//         {
//             Debug.LogError("Setting Global params failed");
//             return;
//         }
//
//         result = RuntimeManager.StudioSystem.setParameterByID(parameterDescription.id, value);
//         if (result != RESULT.OK)
//         {
//             Debug.LogError("Setting Global params failed");
//         }
//     }
//
//     public void PlaySceneBegins()
//     {
//         RuntimeManager.PlayOneShot("event:/UI/NewScene");
//     }
//     
//     public void PlayOnClick()
//     {
//         RuntimeManager.PlayOneShot("event:/MainMenu/Click", transform.position);
//     }
// }
