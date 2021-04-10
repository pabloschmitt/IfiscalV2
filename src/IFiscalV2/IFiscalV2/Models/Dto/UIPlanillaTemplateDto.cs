namespace IFiscalV2.Models.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class UIEleccion
    {

        public string EleccionId { get; set; }
        public int Mesa_Votantes { get; set; }
        public string EleccionName { get; set; }
        public bool SiPartidaria { get; set; }
        public bool Si_Bolsin { get; set; }

        public bool Si_Nulos { get; set; } = true;
        public bool Si_Recurridos { get; set; } = true;
        public bool Si_Impugnados { get; set; } = true;
        public bool Si_MinimoBlancos { get; set; } = false;
        public double PorMinimoBlancos { get; set; } = 0d;

        public bool SiComprobarBlancos { get; set; }
        public int ToleranciaDeBlancos { get; set; }
        public bool TomarBlancosComoValidos { get; set; }
        public string Distrito_Nro { get; set; }
        public int CantidadDeCargos { get; set; }
        public int Certificado_Paginas { get; set; }
        public int Cantidad_Cargos_Pagina_1 { get; set; }
        public string Certificado_Titulo_Pag_1 { get; set; }
        public string Certificado_Titulo_Pag_2 { get; set; }
        public bool Carga_SiMostrarAgrupacion { get; set; }
    }

    public class UICertTColumn
    {
        public string ColumnDef { get; set; }
        public string ColumnHeader { get; set; }
    }

    public class UIRowTemplate : INotifyPropertyChanged
    {
        
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        //TODO RESOLVER
        public void RiseOnPropertyChanged()
        {
            
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V1"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V2"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V3"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V4"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V5"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V6"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V7"));

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NE1"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NE2"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NE3"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NE4"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NE5"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NE6"));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NE7"));
            
        }


        public bool SiSys { get; set; }
        public string PartidoId { get; set; }
        public string PartidoName { get; set; }
        public string PartidoApodo { get; set; }
        public string PartidoColor { get; set; }
        public string PartidoColorLetras { get; set; }

        public string ListaId { get; set; }
        public string ListaNro { get; set; }
        public int ListaOrden { get; set; }
        public string ListaName { get; set; }
        public string ListaApodo { get; set; }
        public string ListaColor { get; set; }
        public string ListaColorLetras { get; set; }

        # region Votos, Valor, Habilitado o No, TabINDEX es TX1
        public string C1 { get; set; }
        public int V1 { get; set; }

        public bool E1 { get; set; }
        public int Tx1 { get; set; }
        public string C2 { get; set; }
        public int V2 { get; set; }
        public bool E2 { get; set; }
        public int Tx2 { get; set; }
        public string C3 { get; set; }
        public int V3 { get; set; }
        public bool E3 { get; set; }
        public int Tx3 { get; set; }
        public string C4 { get; set; }
        public int V4 { get; set; }
        public bool E4 { get; set; }
        public int Tx4 { get; set; }
        public string C5 { get; set; }
        public int V5 { get; set; }
        public bool E5 { get; set; }
        public int Tx5 { get; set; }
        public string C6 { get; set; }
        public int V6 { get; set; }
        public bool E6 { get; set; }
        public int Tx6 { get; set; }
        public string C7 { get; set; }
        public int V7 { get; set; }
        public bool E7 { get; set; }
        public int Tx7 { get; set; }
        #endregion


        public bool N1 => !E1;
        public bool N2 => !E2;
        public bool N3 => !E3;
        public bool N4 => !E4;
        public bool N5 => !E5;
        public bool N6 => !E6;
        public bool N7 => !E7;

        public bool IsNotSys => !SiSys;

        public bool NE1 { get; set; } = false;
        public bool NE2 { get; set; } = false;
        public bool NE3 { get; set; } = false;
        public bool NE4 { get; set; } = false;
        public bool NE5 { get; set; } = false;
        public bool NE6 { get; set; } = false;
        public bool NE7 { get; set; } = false;


        #region DDD
        public int GetVx( int ix )
        {
            ix += 1;
            switch (ix)
            {
                case 1: return V1;
                case 2: return V2;
                case 3: return V3;
                case 4: return V4;
                case 5: return V5;
                case 6: return V6;
                case 7: return V7;
                default:
                    break;
            }
            return 0;
        }
        public void SetVx(int ix, int v )
        {
            ix += 1;
            switch (ix)
            {
                case 1: V1 = v; break;
                case 2: V2 = v; break;
                case 3: V3 = v; break;
                case 4: V4 = v; break;
                case 5: V5 = v; break;
                case 6: V6 = v; break;
                case 7: V7 = v; break;
                default:
                    break;
            }
        }

        #endregion // DDD


    }

    public class UIMesa
    {
        public string MesaId { get; set; }
        public int NroMesa { get; set; }
        public DateTime UltimaModificacion { get; set; }
        public bool Confirmada { get; set; }
        public bool SiExtranjera { get; set; }
        public bool SiGrabadaConError { get; set; }
        public bool NoCalcularBlancos { get; set; }
        public bool NoValidarBlancos { get; set; }
        public int SobresEnLaUrna { get; set; }
        public int Votantes { get; set; }
        public int Cant_Votantes { get; set; }
        public string EscuelaId { get; set; }
        public string EscuelaName { get; set; }
        public string CircuitoId { get; set; }
        public string CircuitoName { get; set; }
    }

    public class UIPlanillaTemplate
    {
        public UIEleccion EleccionUI { get; set; }
        public UIMesa MesaUI { get; set; }
        public List<UIRowTemplate> PlanillaUI { get; set; } = new List<UIRowTemplate>();
        public List<UICertTColumn> AllDefColumns { get; set; } = new List<UICertTColumn>();
        public List<UICertTColumn> DefTableColumnsP1 { get; set; } = new List<UICertTColumn>();
        public List<UICertTColumn> DefTableColumnsP2 { get; set; } = new List<UICertTColumn>();
    }

    public class CertRowPostModel
    {
        public string PartidoId { get; set; }
        public string ListaId { get; set; }
        public int V1 { get; set; }
        public int V2 { get; set; }
        public int V3 { get; set; }
        public int V4 { get; set; }
        public int V5 { get; set; }
        public int V6 { get; set; }
        public int V7 { get; set; }
    }

    public class CertPostModel
    {
        public string SiteId { get; set; }
        public string EleccionId { get; set; }
        public string MesaId { get; set; }
        public int Votantes { get; set; }
        public int SobresEnLaUrna { get; set; }
        public bool SiGrabadaConError { get; set; }
        public bool NoCalcularBlancos { get; set; }
        public bool NoValidarBlancos { get; set; }
        public List<CertRowPostModel> CertRows { get; set; } = new List<CertRowPostModel>();

    }

}
