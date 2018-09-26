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
			if (_prefixos == null)
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
			
		}
	}
}
