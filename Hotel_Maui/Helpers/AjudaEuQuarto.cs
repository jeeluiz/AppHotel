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
                        ValorDiariaAdulto = 50.0,
                        ValorDiariaCrianca = 12.5
                    };
                case TipoQuarto.Luxo:
                    return new CategoriaQuarto()
                    {
                        Id = tipo,
                        Descricao = "Suite Luxo",
                        ValorDiariaAdulto = 80.0,
                        ValorDiariaCrianca = 40.0
                    };
                case TipoQuarto.SuperLuxo:
                    return new CategoriaQuarto()
                    {
                        Id = tipo,
                        Descricao = "Suite Super Luxo",
                        ValorDiariaAdulto = 110.0,
                        ValorDiariaCrianca = 55.0
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            };
        }
        
        
        


    };
}
