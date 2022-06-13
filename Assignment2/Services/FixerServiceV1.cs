﻿using Assignment2.Model;
using Assignment2.ServiceInterface;
using Assignment2.Utility;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Assignment2.Services
{
    public class FixerServiceV1: IFixerService1
    {
        public async Task<FixerResponseModelV1> ConvertCurrencyV1(string fromCurrency, decimal fromCurrencyAmount, string toCurrency)
        {
            FixerResponseModelV1 fixerResponse = null;

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ApiInfo.Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("ApiKey", ApiInfo.FixerKey);

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType.Json));

                //Sending request to find web api REST service resource fixer/convert using HttpClient                
                HttpResponseMessage Res = await client.GetAsync(string.Format("/fixer/convert?to={0}&from={1}&amount={2}",
                                                                                              toCurrency, fromCurrency, fromCurrencyAmount));

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the FixerResponseModel
                    fixerResponse = JsonConvert.DeserializeObject<FixerResponseModelV1>(response);
                }
            }
            return fixerResponse;
        }
    }
}
