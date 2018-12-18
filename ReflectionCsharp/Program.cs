using ReflectionCsharp.Infraestrutura;
using System;

namespace ReflectionCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
			var prefixos = new string[]
			{
				"http://localhost:3000/"
			};
			var webApplication = new WebApplication(prefixos);
			webApplication.Iniciar();
        }
    }
}
