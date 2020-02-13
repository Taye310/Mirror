using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace Mirror.Examples.Chat
{
    public class Player : NetworkBehaviour
    {
        [SyncVar]
        public string playerName;

        public ChatWindow chatWindow => ((ChatNetworkManager)NetworkManager.singleton).chatWindow;

        [Command]
        public void CmdSend(string message)
        {
            if (message.Trim() != "")
                RpcReceive(message.Trim());
        }

        [Command]
        public void CmdSaveChatHistory(string msg)
        {
            Debug.Log("save: " + msg + " to: \n" + Application.streamingAssetsPath + "/chathistory.txt");
            StreamWriter sw = File.AppendText(Application.streamingAssetsPath + "/chathistory.txt");
            sw.WriteLine(playerName + ":  " + msg);
            sw.Flush();
            sw.Close();
        }

        [Command]
        public void CmdReadChatHistory()
        {
            RpcUpdateChatHistory(File.ReadAllText(Application.streamingAssetsPath + "/chathistory.txt"));
        }

        public override void OnStartLocalPlayer()
        {
            chatWindow.gameObject.SetActive(true);
        }

        [ClientRpc]
        public void RpcReceive(string message)
        {
            string prettyMessage = isLocalPlayer ?
                $"<color=red>{playerName}: </color> {message}" :
                $"<color=blue>{playerName}: </color> {message}";

            chatWindow.AppendMessage(prettyMessage);

            Debug.Log(message);
        }

        [ClientRpc]
        public void RpcUpdateChatHistory(string msg)
        {
            chatWindow.chatHistory.text = msg;
        }
    }
}
