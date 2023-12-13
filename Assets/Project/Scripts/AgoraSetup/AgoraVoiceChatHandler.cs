using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agora.Rtc;
using Photon.Pun;
using Yudiz;
using Sportsverse.Audio;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Sportsverse
{
    public class AgoraVoiceChatHandler : PhotonIndestructableSingleton<AgoraVoiceChatHandler> 
    {

        private string agoraAppID = "37ae951aedb44dba87aef667dcd5f2f0";
        //private string agoraChannelName = "Sportsverse";
        private string agoraToken = "";

        IRtcEngine rtcEngine;
        private ILocalSpatialAudioEngine localSpatial;

        float[] pos = new float[] { 0.0F, 0.0F, 0.0F };
        float[] forward = new float[] { 1.0F, 0.0F, 0.0F };
        float[] right = new float[] { 0.0F, 1.0F, 0.0F };
        float[] up = new float[] { 0.0F, 0.0F, 1.0F };

        private uint agoraId;
        private bool isOnMute = false;

        public bool IsOnMute { get => isOnMute; }

        #region UNITY_CALLBACKS
        //private void OnEnable()
        //{
        //    AgoraTokenFetcher.onTokenRecieved += OnTokenRecieved;
        //}
        void Start()
        {
            //SetupVoiceSDKEngine();
            //InitEventHandler();
            InitSDK();
        }
        public override void OnDisable()
        {
            base.OnDisable();
            if (rtcEngine != null)
            {
                Leave();
                rtcEngine.Dispose();
                rtcEngine = null;
            }

            //AgoraTokenFetcher.onTokenRecieved -= OnTokenRecieved;
        }
        #endregion

        #region PUBLIC_METHODS
        public void Join(string channelName)
        {
            // Enables the audio module.
            rtcEngine.EnableAudio();
            // Sets the user role ad broadcaster.
            rtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
            // Joins a channel.
            rtcEngine.JoinChannel(agoraToken, channelName);

            //ConfigureSpatialAudioEngine();
        }
        public void Leave()
        {
            // Leaves the channel.
            rtcEngine.LeaveChannel();
            // Disable the audio modules.
            rtcEngine.DisableAudio();
        }

        public void TurnOnMic()
        {
            //rtcEngine.EnableLocalAudio(true);
            rtcEngine.MuteLocalAudioStream(false);
            isOnMute = false;
        }
        public void TurnOffMic()
        {
            //rtcEngine.EnableLocalAudio(false);
            rtcEngine.MuteLocalAudioStream(true);
            isOnMute = true;
        }
        public void SetAgoraID(uint id)
        {
            Hashtable hash = new Hashtable();
            hash.Add(StringConstants.agoraIdKey, id.ToString());
            PhotonNetwork.SetPlayerCustomProperties(hash);

            agoraId = id;
        }
        public uint GetCurrentAgoraId()
        {
            return agoraId;
        }
        public void SetRemotePlayerSpatialAudio(uint uId, float pan, float gain)
        {
            rtcEngine.SetRemoteVoicePosition(uId,pan, gain);
        }
        #endregion

        #region AGORA
        private void InitSDK()
        {
            agoraToken = "";

            Debug.Log("Token Recieved... Starting Agora");
            SetupVoiceSDKEngine();
            InitEventHandler();
        }
        private void OnTokenRecieved(string token)
        {
            agoraToken = token;

            Debug.Log("Token Recieved... Starting Agora");
            SetupVoiceSDKEngine();
            InitEventHandler();
        }
        private void SetupVoiceSDKEngine()
        {
            // Create an RtcEngine instance.
            rtcEngine = Agora.Rtc.RtcEngine.CreateAgoraRtcEngine();
            RtcEngineContext context = new RtcEngineContext(agoraAppID, 0,
            CHANNEL_PROFILE_TYPE.CHANNEL_PROFILE_LIVE_BROADCASTING,
            AUDIO_SCENARIO_TYPE.AUDIO_SCENARIO_DEFAULT);
            // Initialize RtcEngine.
            rtcEngine.Initialize(context);
            rtcEngine.EnableSoundPositionIndication(true);
        }
        private void InitEventHandler()
        {
            // Creates a UserEventHandler instance.
            UserEventHandler handler = new UserEventHandler(this);
            rtcEngine.InitEventHandler(handler);
        }
        #endregion

        #region PHOTON_CALLBACKS
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Join(PhotonNetwork.CurrentRoom.Name);
            TurnOffMic();
        }
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            Leave();
        }
        #endregion
    }
    internal class UserEventHandler : IRtcEngineEventHandler
    {
        private readonly AgoraVoiceChatHandler _audioSample;

        internal UserEventHandler(AgoraVoiceChatHandler audioSample)
        {
            _audioSample = audioSample;
        }

        // This callback is triggered when the local user joins the channel.
        public override void OnJoinChannelSuccess(RtcConnection connection, int elapsed)
        {
            Debug.Log("On Agora Voice Channel Join Successfull");
            base.OnJoinChannelSuccess(connection, elapsed);

            AgoraVoiceChatHandler.Instance.SetAgoraID(connection.localUid);
            
        }

        public override void OnConnectionStateChanged(RtcConnection connection, CONNECTION_STATE_TYPE state, CONNECTION_CHANGED_REASON_TYPE reason)
        {
            Debug.Log("On Agora Voice Channel OnConnectionStateChanged");

            base.OnConnectionStateChanged(connection, state, reason);
        }
        public override void OnError(int err, string msg)
        {
            Debug.Log("On Agora Voice Channel OnError");

            base.OnError(err, msg);
        }

        public override void OnLeaveChannel(RtcConnection connection, RtcStats stats)
        {
            Debug.Log("On Agora Voice Channel OnLeaveChannel");

            base.OnLeaveChannel(connection, stats);
        }
    }
}