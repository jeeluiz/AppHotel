using Hotel.Data.Enums;
using Hotel.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Maui.Helpers
{
    public static class AjudaEuQuarto
    {
        public static CategoriaQuarto ParaCategoria(this TipoQuarto tipo)
        {
            switch (tipo)
            {
                case TipoQuarto.Simples:
                    return new CategoriaQuarto()
                    {
                        Id = tipo,
                        Descricao = "Suite Simples",
                        ValorDiariaAdulto = 80.0,
                        ValorDiariaCrianca = 40.5
                    };
                case TipoQuarto.Luxo:
                    return new CategoriaQuarto()
                    {
                        Id = tipo,
                        Descricao = "Suite Luxo",
                        ValorDiariaAdulto = 150.0,
                        ValorDiariaCrianca = 75.0
                    };
                case TipoQuarto.SuperLuxo:
                    return new CategoriaQuarto()
                    {
                        Id = tipo,
                        Descricao = "Suite Super Luxo",
                        ValorDiariaAdulto = 500.0,
                        ValorDiariaCrianca = 270.0
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            };
        }
    };
}
