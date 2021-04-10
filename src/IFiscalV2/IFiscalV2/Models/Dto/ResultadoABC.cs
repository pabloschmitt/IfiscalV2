namespace IFiscalV2.Models.Dto
{
    using System.Collections.Generic;
    using System.Linq;

    public class ResultadoABCDto
    {
        public double CE_ELECCION { get; set; } = 0;
        public int TL_VOTOS_VALIDOS { get; set; } = 0;
        public int TL_VOTOS { get; set; } = 0;

        public int MesasEscrutadas { get; set; } = 0;
        public int CantMesas { get; set; } = 0;
        public int CantidadVotantes { get; set; } = 0;

        public List<ResultadoLisParDto> ResultadoPartidos { get; set; } = new List<ResultadoLisParDto>();
        public List<ResultadoLisParDto> ResultadoListas { get; set; } = new List<ResultadoLisParDto>();

        public List<ResultadoLisParDto> ResultadoXListas => ResultadoListas.Where(w => w.TL_Votos > 0).ToList();

    }

    //TODO ResultadoLisParResponse
    public class ResultadoLisParDto
    {
        public string TipoResultado { get; set; } = string.Empty;
        public bool SiSistema { get; set; } = false;

        // Region Partido
        public string PartidoId { get; set; } = string.Empty;
        public double Piso_Eleccion { get; set; } = 0;
        public string PartidoName { get; set; } = string.Empty;
        public string PartidoColor_Ltr { get; set; } = string.Empty;
        public string PartidoColor { get; set; } = string.Empty;
        public double Piso_Partido { get; set; }

        // Region Lista
        public string ListaId { get; set; } = string.Empty;
        public string ListaNro { get; set; }
        public string ListaName { get; set; }
        public string ListaColor_Ltr { get; set; }
        public string ListaColor { get; set; }

        //Region ORDEN
        public int Orden { get; set; } = 0;
        public int OrdenResultado { get; set; } = 0;

        // Region Votos
        public bool SiCargo { get; set; }
        public int TL_Votos { get; set; }
        public decimal Porcentaje { get; set; } = 0;
        public decimal Porcentaje_Eleccion { get; set; } = 0;

        // Region Bancas
        public int BancasAsignadas { get; set; } = 0;
        public string ReglasAplicadas { get; set; } = string.Empty;
        public string CocientesAplicados { get; set; } = string.Empty;

        public int BancasAsignadas_Interna { get; set; } = 0;
        public string ReglasAplicadas_Interna { get; set; } = string.Empty;
        public string CocientesAplicados_Interna { get; set; } = string.Empty;
        public double CE_A_Aplicado { get; set; }
        public double CE_A_Aplicado_Internas { get; set; }
        public string ReglasDHontAplicadas_Internas { get; set; }
        public int BancasAsignadas_internas_dhont { get; set; }
        public int BancasAsignadas_dhont { get; set; }
        public string ReglasDHontAplicadas { get; set; }
        public List<ReglaDHontDto> ReglasDHont { get; set; } = new List<ReglaDHontDto>();
        public List<ReglaDHontDto> ReglasDHont_Internas { get; set; } = new List<ReglaDHontDto>();
    }


    public class ReglaDHontDto
    {
        public string PartidoId { get; set; }
        public string ListaId { get; set; }
        public string BackgroundColor { get; set; } = string.Empty;
        public string Color_Ltr { get; set; } = string.Empty;
        public string PartidoOLista { get; set; } = string.Empty;
        public int Posicion { get; set; }
        public double ValorDHont { get; set; }
        public int DivDHont { get; set; }
    }

    public struct CalculoTipoResultado
    {
        public const string PARTIDO = "PAR";
        public const string LISTA = "LIS";
    }

}
