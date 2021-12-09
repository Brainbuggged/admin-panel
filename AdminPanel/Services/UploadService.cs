using AdminPanel.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AdminPanel.Services
{
	public class UploadService
	{
		public async Task<string> UploadPhoto(string action, IFormFile file)
		{
      //создаем клиент для запросов
      HttpClient _client = new HttpClient();
      //переводим фото в стрим
      var stream = new MemoryStream((int)file.Length);
      file.CopyTo(stream);
      //инициализируем формдату
      var content = new MultipartFormDataContent();
      //добавляем какую-то подпись, можно стереть
      content.Add(new StringContent("caption"), "caption");
      //создаем стрим контент файла, который будем отправлять
      var t = new StreamContent(stream);
      //подписываем тип данных того, что отправляет
      t.Headers.ContentType
          = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
      //засовываем то что отправляем в формдату
      //t - стрим, созданный из файла фотографии
      //file - показываем что находится в строиме, я так понимаю тут надо както указать либо media, либо attachment, либо что-то другое, что показывает отправку ФОТО
      //FileName - наименование того, что в стрим записано
      content.Add(t, "file", file.FileName);




      var response = await _client.PostAsync($"http://77.73.67.101:93/api/Upload/{action}", content);
      var photoLinq = await response.Content.ReadAsStringAsync();
      var responseBody = JsonConvert.DeserializeObject<ResultDeserealaizer>(photoLinq);
      return responseBody.result.photo_linq;

    }
  }
  public class ResultDeserealaizer
	{
		public string status { get; set; } = ResultStatus.Created.GetText();
		public string type { get; set; } = "https://tools.ietf.org/html/rfc7231#section-6.3.2";
		public string title { get; set; }
		public PhotoDeserealaizer result { get; set; }
	}
  public class PhotoDeserealaizer
  {
    public string photo_linq { get; set; }
  }
}
