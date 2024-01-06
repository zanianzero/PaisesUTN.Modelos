using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net;

namespace EmpresaUTN.UAPI
{
    public class Crud  <T>
    {

        //crearemos select

        public T[] Select(string Url)
        {
            try
            {
                using (var api = new WebClient())
                {
                    api.Headers.Add("Content-Type", "application/json");
                    var json = api.DownloadString(Url);
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(json);
                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("ha ocurrido un error inesperado en select ("+ ex.Message +")");
            }
        }


        //select por id

        public T SelectById(string Url, string id)
        {
           try
            {
                using (var api = new WebClient())
                {
                    api.Headers.Add("Content-Type", "application/json");
                    var json = api.DownloadString(Url +"/"+ id);
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ha ocurrido un error inesperado en select por id ("+ ex.Message +")");
            }

        }

        //update

        public void Update(string Url,string id, T data)
        {
            try
            {
                using (var api = new WebClient())
                {
                    api.Headers.Add("Content-Type", "application/json");
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    api.UploadString(Url +"/"+ id, "PUT", json);


                }
            }
            catch (Exception ex)
            {
                throw new Exception("ha ocurrido un error inesperado en update ("+ ex.Message +")"); 
            }
        }

        //insert

        public T Insert(string Url, T data)
        {
          
            try
            {
                using (var api = new WebClient())
                {
                    api.Headers.Add("Content-Type", "application/json");
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    json = api.UploadString(Url, "POST", json);
                    data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("ha ocurrido un error inesperado en insert ("+ ex.Message +")");
            }
           
        }   

        //delete por id
        public void Delete(string Url, string id)
        {
            try
            {
                using (var api = new WebClient())
                {
                    api.Headers.Add("Content-Type", "application/json");
                    api.UploadString(Url +"/"+ id, "DELETE", "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ha ocurrido un error inesperado en delete ("+ ex.Message +")"); 
            }

        }



    }
}