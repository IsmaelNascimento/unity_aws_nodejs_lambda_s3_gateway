using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace IsmaelNascimento
{
    public class SampleScript : MonoBehaviour
    {
        [SerializeField] private string pathFile;

        [ContextMenu("TestUploadFile")]
        private void TestUploadFile()
        {
            Debug.Log("Start upload");
            byte[] myBytes = File.ReadAllBytes(pathFile);
            string myBase64 = Convert.ToBase64String(myBytes);

            RequestUploadModel requestUploadModel = new RequestUploadModel
            {
                pathFile = "myImage.png",
                base64 = myBase64
            };

            string json = JsonConvert.SerializeObject(requestUploadModel);

            ApiManager.Instance.UploadFileToS3(json, (error, response) =>
            {
                if (!string.IsNullOrEmpty(error))
                {
                    Debug.LogError(error);
                }
                else
                {
                    ResponseUploadModel responseUploadModel = JsonConvert.DeserializeObject<ResponseUploadModel>(response);
                    Debug.Log("Link file:: " + responseUploadModel.Location);
                }
            });
        }
    }
}