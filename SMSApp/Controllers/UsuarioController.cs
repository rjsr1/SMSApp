using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SMSApp.Controllers
{
    public class SMS
    {
        public sendSmsRequest sendSmsRequest;
    }

    public class sendSmsRequest
    {
        public string from { get; set; }
        public string to { get; set; }
        public string schedule { get; set; }
        public string msg { get; set; }
        public string callbackOption { get; set; }
        public string id { get; set; }    
        public string aggregateId { get; set; }      
        //public string account { get; set; }
        //public string password { get; set; }
    }

    public class UsuarioController : Controller
    {
        private string url = "https://api-rest.zenvia360.com.br/services/send-sms";
        private string pathFile = "C:\\";
        private StreamWriter streamWriter;
        private StreamWriter testeJson;

        private IEnumerable<string> meses = Enumerable.Range(1, 12).Select(x=>x.ToString());
        private IEnumerable<string> anos = Enumerable.Range(2010,2030).Select(x => x.ToString());


        private string TextoParaEnviar = "Cliente NET, analisamos sua requisição em {0}/{1} concluímos ser procedente. Não se preocupe, já realizamos todos os ajustes em seu contrato.";
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Usuario usuario)
        {
            //falta implementar validacao de usuario
            if (IsValido())
            {
                //ViewBag.Meses = meses;
                //ViewBag.anos = anos;
                ViewBag.Meses = meses;
                ViewBag.Anos = anos;
                ViewBag.Texto = TextoParaEnviar;
                return View("~/Views/SMSViews/SMSView.cshtml");
            }
            else
            {
                return Index();
            }

        }


        private bool IsValido()
        {
            //falta implementar validacao
            return true;
        }

        public void Enviar(string Texto, string HoraEnvio)
        {
            //usar api de sms                   
            streamWriter = new StreamWriter(pathFile + "Log2.txt");
            streamWriter.WriteLine(Texto);
            streamWriter.WriteLine(HoraEnvio);
            streamWriter.Flush();
            SMSMesseger(Texto, 555199999999);//apenas para teste

        }

        public void SMSMesseger(string mensagem, long destino)
        {

            sendSmsRequest sendSmsRequest = new sendSmsRequest();

            string idUrlEncoded = HttpUtility.UrlEncode("1");
            sendSmsRequest.id = idUrlEncoded;

            string toUrlEncoded = HttpUtility.UrlEncode(destino.ToString());
            sendSmsRequest.to = toUrlEncoded;

            string msgUrlEncoded = HttpUtility.UrlEncode(mensagem);
            sendSmsRequest.msg = msgUrlEncoded;

            string scheduleUrlEncoded = HttpUtility.UrlEncode(DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"));
            sendSmsRequest.schedule = scheduleUrlEncoded;

            string accountUrlEncoded = HttpUtility.UrlEncode("luciano.oliveira@datametrica.com.br");
            //sendSmsRequest.account = accountUrlEncoded;

            string passwordUrlEncoded = HttpUtility.UrlEncode("Dtm@2017");
            //sendSmsRequest.password = passwordUrlEncoded;
            string callbackOptionUrlEncoded = HttpUtility.UrlEncode("ALL");
            sendSmsRequest.callbackOption = callbackOptionUrlEncoded;
            SMS sms = new SMS();
            sms.sendSmsRequest = sendSmsRequest;



            string json = JsonConvert.SerializeObject(sms);
            testeJson = new StreamWriter("C:\\testeJson.txt");
            testeJson.WriteLine(json);
            testeJson.Flush();            

            

            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential(accountUrlEncoded, passwordUrlEncoded);            
            Uri uri = new Uri(url);

            string response=client.UploadData(uri,System.Text.Encoding.ASCII.GetBytes(json)).ToString();
            streamWriter.WriteLine(response);
        }


    }
}