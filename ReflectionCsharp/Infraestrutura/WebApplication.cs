using ReflectionCsharp.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace ReflectionCsharp.Infraestrutura
{
    /// <summary>
    /// Classe representa o servidor Web que vai escutar as requisições HTTP
    /// </summary>
    public class WebApplication
    {
        /// <summary>
        /// Prefixos, são as portas que vão ser escutadas a aplicação.
        /// </summary>
        private readonly string[] _prefixos;

        /// <summary>
        /// Construtor para inicializar o serviço com os prefixos
        /// </summary>
        /// <param name="prefixos"></param>
        public WebApplication(string[] prefixos)
        {
            if (prefixos == null)
                throw new ArgumentNullException(nameof(prefixos));
            _prefixos = prefixos;
        }

        /// <summary>
        /// Inicializa o serviço web e vamos escutar o serviço na porta definida nos prefixos.
        /// </summary>
        public void Iniciar()
        {
            while (true)
            {
                //Manipular valores
                ManipularRequisicao();
            }
        }

        private void ManipularRequisicao()
        {
            //Instancia o uma classe especifica para escutar requisições Http
            var httpListener = new HttpListener();

            //Definir prefixos no Listener
            _prefixos
                .ToList()
                .ForEach(prefixo => httpListener.Prefixes.Add(prefixo));

            //Inicializa o Listener para que ele comece a escutar as requisições.
            httpListener.Start();

            //travar a aplicação até que tenha um contexto de uma requisição
            var contexto = httpListener.GetContext();

            //Após obter o contexto vamos ter 2 objetos um a requisição e outro a response
            var requisicao = contexto.Request;
            var resposta = contexto.Response;

            //Retornando os arquivos css e js
            //O AbsolutePath é a url da requisição porem sem o prefixo /Assets/css/styles.css
            var path = requisicao.Url.AbsolutePath;
            //Se for requisitado um arquivo que na url contem a extenção vamos retorna-lo
            //Se não é uma pagina html e temos que chamar um controller
            if (Utilidades.EhArquivo(path))
            {
                var manipulador = new ManipuladorRequisicaoArquivo();
                manipulador.Manipular(resposta, path);
            }
            else if(path == "/Cambio/MXN")
            {
                //Instanciando o controller para pegar o conteudo do metodo MXN
                var controller = new CambioController();
                var paginaConteudo = controller.MXN();
                resposta.StatusCode = 200;
                resposta.ContentType = "text/html charset=utf-8";
                //Transformar o conteudo da pagina em um buffer(array de bytes)
                var bufferArquivo = Encoding.UTF8.GetBytes(paginaConteudo);
                //Colocar no body da resposta os bytes do conteudo
                resposta.OutputStream.Write(bufferArquivo, 0, bufferArquivo.Length);
                resposta.OutputStream.Close();
            }
            else if (path == "/Cambio/USD")
            {
                //Instanciando o controller para pegar o conteudo do metodo MXN
                var controller = new CambioController();
                var paginaConteudo = controller.USD();
                resposta.StatusCode = 200;
                resposta.ContentType = "text/html charset=utf-8";
                //Transformar o conteudo da pagina em um buffer(array de bytes)
                var bufferArquivo = Encoding.UTF8.GetBytes(paginaConteudo);
                //Colocar no body da resposta os bytes do conteudo
                resposta.OutputStream.Write(bufferArquivo, 0, bufferArquivo.Length);
                resposta.OutputStream.Close();
            }

            //o body da requisição é do tipo Stream e para isso temos que converter o texto em um sequencia de bytes
            //var respostaConteudoBytes = Encoding.UTF8.GetBytes(respostaTexto);

            //A requisição foi finalizada, vamos pausar o httpListener
            httpListener.Stop();
        }
    }
}
