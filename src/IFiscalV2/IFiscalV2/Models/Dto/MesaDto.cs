namespace IFiscalV2.Models.Dto
{
    public class MesaDto
    {
        public string SiteId { get; set; }
        public string EleccionId { get; set; }
        public string EscuelaId { get; set; }
        public int NroMesa { get; set; }
        public string Id { get; set; }
        public bool SiExtranjera { get; set; }
        public int Votantes { get; set; }
        public int Cant_Votantes { get; set; }
        public int SobresEnLaUrna { get; set; }
        public bool Confirmada { get; set; }
        public bool SiGrabadaConError { get; set; }
        public bool NoCalcularBlancos { get; set; }
        public bool NoValidarBlancos { get; set; }
    }

    public class MesaResponseModel
    {
        public string SiteId { get; set; }
        public string EleccionId { get; set; }
        public string Id { get; set; }
        public string EscuelaId { get; set; }
        public string EscuelaName { get; set; }
        public string Circuito { get; set; }
        public string LocalidadName { get; set; }
        public int NroMesa { get; set; }
        public int Votantes { get; set; }
        public bool SiExtranjera { get; set; }
        public int SobresEnLaUrna { get; set; }
        public bool Confirmada { get; set; }
        public bool SiGrabadaConError { get; set; }
        public bool NoCalcularBlancos { get; set; }
        public bool NoValidarBlancos { get; set; }

        public string BackColor {
            get {
                if (SiGrabadaConError)
                    return "#c62828";
                if (Confirmada)
                    return "#388e3c";
                else
                    return "White";
            }
        }
        

        public string FontColor
        {
            get
            {
                if (SiGrabadaConError)
                    return "Yellow";
                if (Confirmada)
                    return "White";
                else
                    return "Black";
            }
        }

    }

}
