using Newtonsoft.Json;

namespace Read.API.Common
{
    public class ResponseCommon
    {
        public ResponseCommon(object data, string message = null, ResponseErrorCommon error = null)
        {
            Data = data;
            Message = message;
            Error = error;
        }

        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
        [JsonProperty("error")]
        public ResponseErrorCommon Error { get; set; }
    }

    public class ResponseErrorCommon
    {
        public ResponseErrorCommon(int count, string[] errors)
        {
            Count = count;
            Errors = errors;
        }

        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("erros")]
        public string[] Errors { get; set; }
    }
}