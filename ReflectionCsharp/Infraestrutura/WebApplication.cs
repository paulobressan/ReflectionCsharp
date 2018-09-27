using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
			//Instancia o uma classe especifica para escutar requisições Http
			var httpListener = new HttpListener();

			//Definir prefixos no Listener
			_prefixos
				.ToList()
				.ForEach(prefixo =>	httpListener.Prefixes.Add(prefixo));

			//Inicializa o Listener para que ele comece a escutar as requisições.
			httpListener.Start();

			//travar a aplicação até que tenha um contexto de uma requisição
			var contexto = httpListener.GetContext();

			//Após obter o contexto vamos ter 2 objetos um a requisição e outro a response
			var requisicao = contexto.Request;
			var resposta = contexto.Response;

			//Respost para o cliente
			var respostaTexto = "Hello World";
			
			//o body da requisição é do tipo Stream e para isso temos que converter o texto em um sequencia de bytes
			var respostaConteudoBytes = Encoding.UTF8.GetBytes(respostaTexto);		

			//Definir o tipo do conteudo que sera retornado
			resposta.ContentType = "text/html; charset=utf-8";
			//definir o código de resposta do HTTP
			resposta.StatusCode = 200;
			//tamanho da resposta, como a resposta é gerado um array de bytes, vamos ver o tamnho de posição
			resposta.ContentLength64 = respostaConteudoBytes.Length;
			//O body da requisição só aceita stram e para escrever nele temos que usar alguns recursos
			//Ele espera 3 parametro, o primeiro é o array de bytes, o segundo é de qual array vai ser iniciado e o terceiro até qual array vai ser escrito
			//Ou seja, temos o tamanho de array que vai ser escrito
			resposta.OutputStream.Write(respostaConteudoBytes, 0, respostaConteudoBytes.Length);
			//Fechar o escritor do stream
			resposta.OutputStream.Close();
			//A requisição foi finalizada, vamos pausar o httpListener
			httpListener.Stop();
		}
	}
}
