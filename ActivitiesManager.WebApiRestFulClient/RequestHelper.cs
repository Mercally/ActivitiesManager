using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ActivitiesManager.Shared.Models.Web;

namespace ActivitiesManager.WebApiRestFulClient
{
    internal class RequestHelper
    {
        /// <summary>
        /// Envía una petición a la url especificada con las opciones
        /// de configuración requerida
        /// </summary>
        /// <typeparam name="T">Tipo de dato a esperado</typeparam>
        /// <param name="Url">Url destino</param>
        /// <param name="Method">Tipo de método de la petición</param>
        /// <param name="Data">Objeto a enviar en la petición</param>
        /// <param name="TimeOut">Tiempo de espera en minutos</param>
        /// <returns></returns>
        internal static HttpResponse<T> Request<T>(string Url, HttpMethodEnum Method, object Data = null, int TimeOut = 1)
        {
            var Response = new HttpResponse<T>();
            T Value = default(T);
            HttpStatusCode HttpStatus = HttpStatusCode.OK;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromMinutes(TimeOut);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = null;

                    switch (Method)
                    {
                        case HttpMethodEnum.Get:
                            response = client.GetAsync(Url).Result;
                            break;
                        case HttpMethodEnum.PostJson:
                            response = client.PostAsJsonAsync(Url, Data).Result;
                            break;
                        case HttpMethodEnum.PutJson:
                            response = client.PutAsJsonAsync(Url, Data).Result;
                            break;
                        case HttpMethodEnum.Delete:
                            response = client.DeleteAsync(Url).Result;
                            break;
                        default:
                            break;
                    }

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Value = response.Content.ReadAsAsync<T>().Result;
                    }

                    HttpStatus = response.StatusCode;
                }
                catch (Exception ex)
                {
                    // Excepciones
                    Value = default(T);
                    Response.StatusCode = HttpStatusCode.InternalServerError;
                    Response.Exception = ex;
                }
            }

            Response.Value = Value;
            Response.StatusCode = HttpStatus;

            return Response;
        }

        internal static async Task<HttpResponse<T>> RequestAsync<T>(string Url, HttpMethodEnum Method, object Data = null, int TimeOut = 1)
        {
            var Response = new HttpResponse<T>();
            T Value = default(T);
            HttpStatusCode HttpStatus = HttpStatusCode.OK;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.Timeout = TimeSpan.FromMinutes(TimeOut);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = null;

                    switch (Method)
                    {
                        case HttpMethodEnum.Get:
                            response = await client.GetAsync(Url);
                            break;
                        case HttpMethodEnum.PostJson:
                            response = await client.PostAsJsonAsync(Url, Data);
                            break;
                        case HttpMethodEnum.PutJson:
                            response = await client.PutAsJsonAsync(Url, Data);
                            break;
                        case HttpMethodEnum.Delete:
                            response = await client.DeleteAsync(Url);
                            break;
                        default:
                            break;
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        Value = await response.Content.ReadAsAsync<T>();
                    }

                    HttpStatus = response.StatusCode;
                }
                catch (Exception ex)
                {
                    // Excepciones
                    Value = default(T);
                    Response.StatusCode = HttpStatusCode.InternalServerError;
                    Response.Exception = ex;
                }
            }

            Response.Value = Value;
            Response.StatusCode = HttpStatus;

            return Response;
        }
    }
}
