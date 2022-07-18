using Hotel_Maui.Enums;
using Hotel_Maui.Model;
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
                    return new Model.CategoriaQuarto()
                    {
                        Id = tipo,
                        Descricao = "Suite Simples",
                        ValorDiariaAdulto = 50.0,
                        ValorDiariaCrianca = 12.5
                    };
                case TipoQuarto.Luxo:
                    return new Model.CategoriaQuarto()
                    {
                        Id = tipo,
                        Descricao = "Suite Luxo",
                        ValorDiariaAdulto = 80.0,
                        ValorDiariaCrianca = 40.0
                    };
                case TipoQuarto.SuperLuxo:
                    return new Model.CategoriaQuarto()
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
        
        //{
        //    Descricao = "Suite Super Luxo",
        //    ValorDiariaAdulto = 110.0,
        //    ValorDiariaCrianca =55.0
        //},

        //new Model.CategoriaQuarto()
        //{
        //    Descricao = "Suite Luxo",
        //    ValorDiariaAdulto = 80.0,
        //    ValorDiariaCrianca =40.0
        //},
        //new Model.CategoriaQuarto()
        //{
        //    Descricao = "Suite Simples",
        //    ValorDiariaAdulto = 50.0,
        //    ValorDiariaCrianca =12.5
        //},


    };
}
