namespace IFiscalV2.Models.Dto
{

    public class EleccionDto
    {
        public bool SiActiva { get; set; }
        public string SiteId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool SiCargaSuspendida { get; set; }
        public bool SiColadoresSuspendida { get; set; }
        public string PadronActivoId { get; set; }
        public bool SiPartidaria { get; set; }
        public bool Carga_SiMostrarAgrupacion { get; set; }
        public bool SiComprobarBlancos { get; set; }
        public bool TomarBlancosComoValidos { get; set; }
        public int CantidadDeCargos { get; set; }
        public int CantidadDeElectores { get; set; }
        public string Distrito_Nro { get; set; }
        public int Mesa_Votantes { get; set; }
        public bool Si_Bolsin { get; set; }

        public bool Si_Nulos { get; set; } = true;
        public bool Si_Recurridos { get; set; } = true;
        public bool Si_Impugnados { get; set; } = true;
        public bool Si_MinimoBlancos { get; set; } = false;
        public double PorMinimoBlancos { get; set; } = 0d;

        public int Certificado_Paginas { get; set; }
        public int Cantidad_Cargos_Pagina_1 { get; set; }
        public string Certificado_Titulo_Pag_2 { get; set; }

        #region Cargos
        public string Cargo_1_Nombre { get; set; }
        public double Cargo_1_FiltroXPiso { get; set; } = 0;
        public int Cargo_1_Bancas { get; set; } = 0;
        public bool Cargo_1_ExcluirExtranjero { get; set; } = false;


        public string Cargo_2_Nombre { get; set; }
        public double Cargo_2_FiltroXPiso { get; set; } = 0;
        public int Cargo_2_Bancas { get; set; } = 0;
        public bool Cargo_2_ExcluirExtranjero { get; set; } = false;

        public string Cargo_3_Nombre { get; set; }
        public double Cargo_3_FiltroXPiso { get; set; } = 0;
        public int Cargo_3_Bancas { get; set; } = 0;
        public bool Cargo_3_ExcluirExtranjero { get; set; } = false;

        public string Cargo_4_Nombre { get; set; }
        public double Cargo_4_FiltroXPiso { get; set; } = 0;
        public int Cargo_4_Bancas { get; set; } = 0;
        public bool Cargo_4_ExcluirExtranjero { get; set; } = false;

        public string Cargo_5_Nombre { get; set; }
        public double Cargo_5_FiltroXPiso { get; set; } = 0;
        public int Cargo_5_Bancas { get; set; } = 0;
        public bool Cargo_5_ExcluirExtranjero { get; set; } = false;

        public string Cargo_6_Nombre { get; set; }
        public double Cargo_6_FiltroXPiso { get; set; } = 0;
        public int Cargo_6_Bancas { get; set; } = 0;
        public bool Cargo_6_ExcluirExtranjero { get; set; } = false;

        public string Cargo_7_Nombre { get; set; }
        public double Cargo_7_FiltroXPiso { get; set; } = 0;
        public int Cargo_7_Bancas { get; set; } = 0;
        public bool Cargo_7_ExcluirExtranjero { get; set; } = false;
        #endregion

        #region CargosDto por Numero
        public CargoDto Cargo(int n)
        {
            switch (n)
            {
                case 1:
                    return new CargoDto
                    {
                        CargoNro = 1,
                        Nombre = Cargo_1_Nombre,
                        FiltroXPiso = Cargo_1_FiltroXPiso,
                        Bancas = Cargo_1_Bancas,
                        ExcluirDeExtranjero = Cargo_1_ExcluirExtranjero
                    };
                case 2:
                    return new CargoDto
                    {
                        CargoNro = 2,
                        Nombre = Cargo_2_Nombre,
                        FiltroXPiso = Cargo_2_FiltroXPiso,
                        Bancas = Cargo_2_Bancas,
                        ExcluirDeExtranjero = Cargo_2_ExcluirExtranjero
                    };
                case 3:
                    return new CargoDto
                    {
                        CargoNro = 3,
                        Nombre = Cargo_3_Nombre,
                        FiltroXPiso = Cargo_3_FiltroXPiso,
                        Bancas = Cargo_3_Bancas,
                        ExcluirDeExtranjero = Cargo_3_ExcluirExtranjero
                    };
                case 4:
                    return new CargoDto
                    {
                        CargoNro = 4,
                        Nombre = Cargo_4_Nombre,
                        FiltroXPiso = Cargo_4_FiltroXPiso,
                        Bancas = Cargo_4_Bancas,
                        ExcluirDeExtranjero = Cargo_4_ExcluirExtranjero
                    };
                case 5:
                    return new CargoDto
                    {
                        CargoNro = 5,
                        Nombre = Cargo_5_Nombre,
                        FiltroXPiso = Cargo_5_FiltroXPiso,
                        Bancas = Cargo_5_Bancas,
                        ExcluirDeExtranjero = Cargo_5_ExcluirExtranjero
                    };
                case 6:
                    return new CargoDto
                    {
                        CargoNro = 6,
                        Nombre = Cargo_6_Nombre,
                        FiltroXPiso = Cargo_6_FiltroXPiso,
                        Bancas = Cargo_6_Bancas,
                        ExcluirDeExtranjero = Cargo_6_ExcluirExtranjero
                    };
                case 7:
                    return new CargoDto
                    {
                        CargoNro = 7,
                        Nombre = Cargo_7_Nombre,
                        FiltroXPiso = Cargo_7_FiltroXPiso,
                        Bancas = Cargo_7_Bancas,
                        ExcluirDeExtranjero = Cargo_7_ExcluirExtranjero
                    };
                default: return new CargoDto();
            }
        }

        #endregion    }

    }

    public class CargoDto
    {
        public int CargoNro { get; set; }
        public string Nombre { get; set; }
        public double FiltroXPiso { get; set; } = 0;
        public int Bancas { get; set; } = 0;
        public bool ExcluirDeExtranjero { get; set; } = false;
    }

}
