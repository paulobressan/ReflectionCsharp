using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionCsharp.Service.Cambio
{
    public class CambioTesteService : ICambioService
    {
        public decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor) =>
            valor * (decimal)new Random().NextDouble();
    }
}
