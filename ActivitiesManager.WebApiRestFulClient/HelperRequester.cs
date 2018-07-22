using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ActivitiesManager.WebApiRestFulClient
{
    internal class HelperRequester
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
        internal static T Request<T>(string Url, HttpMethodEnum Method, object Data = null, int TimeOut = 1)
        {
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

                    if (response.IsSuccessStatusCode)
                    {
                        T result = response.Content.ReadAsAsync<T>().Result;
                        return result;
                    }
                    else
                    {
                        return default(T);
                    }
                }
                catch (Exception ex)
                {
                    // Excepciones
                    return default(T);
                }
            }
        }

        internal static async Task<T> RequestAsync<T>(string Url, HttpMethodEnum Method, object Data = null, int TimeOut = 1)
        {
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
                        T result = await response.Content.ReadAsAsync<T>();
                        return result;
                    }
                    else
                    {
                        return default(T);
                    }
                }
                catch (Exception ex)
                {
                    // Excepciones
                    return default(T);
                }
            }
        }
    }
}
