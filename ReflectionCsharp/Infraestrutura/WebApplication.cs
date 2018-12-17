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

            //Respost para o cliente
            //var respostaTexto = "Hello World";

            //Retornando os arquivos css e js
            //O AbsolutePath é a url da requisição porem sem o prefixo /Assets/css/styles.css
            var path = requisicao.Url.AbsolutePath;
            if (path == "/Assets/css/styles.css")
            {
                //O assembly é o contexto da aplicação com todos os arquivos.
                //O metodo GetExecutingAssembly captura o assembly atual em execução
                //O retorno desse metodo é um objeto que representa o contexto da aplicação 
                var assembly = Assembly.GetExecutingAssembly();
                //Nome do recurso, nome do arquivo presente no contexo da aplicação
                //Para acessar um arquivo temos que usar o caminho de assembly que segue o padrão
                //Solution.Namespace.Arquivo
                var recurso = "ReflectionCsharp.Assets.css.styles.css";
                //O Styles.css é um arquivo pequeno então não é necessario preocupar com o seu tamanho.
                /**
				O stream é o fluxo de manipulação de bytes em uma aplicação, 
				quando é necessário trabalhar com arquivos muito grande ou até arquivos pequenos, 
				usamos steam para tornar manipular esse arquivo em pequenos pedaços, isso é feito
				pelo motivo de que podemos trabalhar com arquivo em que não sabemos o seu tamanho
				e para isso é necessário o uso de Stream. Cada variável criada em memória é um 
				balde de bytes onde nele está o valor da variável, quando a variável recebe
				valores pequenos, não vai ter problemas porém se a variável recebe um valor 
				maior isso vai exigir muito recurso tornando a aplicação lenta, para isso o 
				Stream entra para manipular esse arquivos, como se fosse uma mangueira nesse 
				balde pegando somente o fluxo, ou seja pequenos pedaços dos bytes tornando a 
				aplicação rápida.
				 */
                var recursoStream = assembly.GetManifestResourceStream(recurso);
                //Criando um balde com o tamanho do recursoStream (styles.css)
                var bytesRecurso = new Byte[recursoStream.Length];
                //Como sabemos que o styles.css é pequenos, poremos ler completo, iniciando do 0
                //O primeiro parametro é o buffer, ou seja o balde onde vai ser armazenado os dados
                //O segundo é como vai ser canalizado esses bytes, no caso vamos carregar tudo de uma vez
                //E o ultimo é onde termina o array de bytes
                recursoStream.Read(bytesRecurso, 0, (int)recursoStream.Length);

                //RESPONDENDO O STREAM
                //Definir o tipo do conteudo que sera retornado
                resposta.ContentType = "text/css; charset=utf-8";
                //definir o código de resposta do HTTP
                resposta.StatusCode = 200;
                //tamanho da resposta, como a resposta é gerado um array de bytes, vamos ver o tamnho de posição
                resposta.ContentLength64 = recursoStream.Length;
                //O body da requisição só aceita stream e para escrever nele temos que usar alguns recursos
                //Ele espera 3 parametro, o primeiro é o array de bytes, o segundo é de qual array vai ser iniciado e o terceiro até qual array vai ser escrito
                //Ou seja, temos o tamanho de array que vai ser escrito
                resposta.OutputStream.Write(bytesRecurso, 0, bytesRecurso.Length);
                //Fechar o escritor do stream
                resposta.OutputStream.Close();
            }
            else if (path == "/Assets/js/main.js")
            {
                //O assembly é o contexto da aplicação com todos os arquivos.
                //O metodo GetExecutingAssembly captura o assembly atual em execução
                //O retorno desse metodo é um objeto que representa o contexto da aplicação 
                var assembly = Assembly.GetExecutingAssembly();
                //Nome do recurso, nome do arquivo presente no contexo da aplicação
                var recurso = "ReflectionCsharp.Assets.js.main.js";
                //O Styles.css é um arquivo pequeno então não é necessario preocupar com o seu tamanho.
                /**
				O stream é o fluxo de manipulação de bytes em uma aplicação, 
				quando é necessário trabalhar com arquivos muito grande ou até arquivos pequenos, 
				usamos steam para tornar manipular esse arquivo em pequenos pedaços, isso é feito
				pelo motivo de que podemos trabalhar com arquivo em que não sabemos o seu tamanho
				e para isso é necessário o uso de Stream. Cada variável criada em memória é um 
				balde de bytes onde nele está o valor da variável, quando a variável recebe
				valores pequenos, não vai ter problemas porém se a variável recebe um valor 
				maior isso vai exigir muito recurso tornando a aplicação lenta, para isso o 
				Stream entra para manipular esse arquivos, como se fosse uma mangueira nesse 
				balde pegando somente o fluxo, ou seja pequenos pedaços dos bytes tornando a 
				aplicação rápida.
				 */
                var recursoStream = assembly.GetManifestResourceStream(recurso);
                //Criando um balde com o tamanho do recursoStream (styles.css)
                var bytesRecurso = new Byte[recursoStream.Length];
                //Como sabemos que o styles.css é pequenos, poremos ler completo, iniciando do 0
                //O primeiro parametro é o buffer, ou seja o balde onde vai ser armazenado os dados
                //O segundo é como vai ser canalizado esses bytes, no caso vamos carregar tudo de uma vez
                //E o ultimo é onde termina o array de bytes
                recursoStream.Read(bytesRecurso, 0, (int)recursoStream.Length);

                //RESPONDENDO O STREAM
                //Definir o tipo do conteudo que sera retornado
                resposta.ContentType = "application/js; charset=utf-8";
                //definir o código de resposta do HTTP
                resposta.StatusCode = 200;
                //tamanho da resposta, como a resposta é gerado um array de bytes, vamos ver o tamnho de posição
                resposta.ContentLength64 = recursoStream.Length;
                //O body da requisição só aceita stream e para escrever nele temos que usar alguns recursos
                //Ele espera 3 parametro, o primeiro é o array de bytes, o segundo é de qual array vai ser iniciado e o terceiro até qual array vai ser escrito
                //Ou seja, temos o tamanho de array que vai ser escrito
                resposta.OutputStream.Write(bytesRecurso, 0, bytesRecurso.Length);
                //Fechar o escritor do stream
                resposta.OutputStream.Close();
            }

            //o body da requisição é do tipo Stream e para isso temos que converter o texto em um sequencia de bytes
            //var respostaConteudoBytes = Encoding.UTF8.GetBytes(respostaTexto);

            //A requisição foi finalizada, vamos pausar o httpListener
            httpListener.Stop();
        }
    }
}
