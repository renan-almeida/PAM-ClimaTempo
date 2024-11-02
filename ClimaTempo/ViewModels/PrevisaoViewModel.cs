using ClimaTempo.Models;
using ClimaTempo.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClimaTempo.ViewModels
{
    public partial class PrevisaoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string cidade;

        [ObservableProperty]
        private string estado;

        [ObservableProperty]
        private string condicao;

        [ObservableProperty]
        private string condicao_desc;

        [ObservableProperty]
        private DateTime data;


        [ObservableProperty]
        private double min;

        [ObservableProperty]
        private double max;

        [ObservableProperty]
        private double uv;

        [ObservableProperty]
        private List<Clima> proximosDias;

        private Previsao previsao;
        private Previsao proxPrevisao;

        public ICommand BuscarPrevisaoCommand { get; }

        public PrevisaoViewModel()
        {
            BuscarPrevisaoCommand = new Command(BuscarPrevisao);
        }

        public async  void BuscarPrevisao()
        {
            previsao = await new PrevisaoServices().GetPrevisaoById(244);
            

            Cidade = previsao.Cidade;
            Estado = previsao.Estado;
            Condicao = previsao.Clima[0].Condicao;
            Condicao_desc = previsao.Clima[0].Condicao_desc;
            Max =    previsao.Clima[0].Max;
            Min =    previsao.Clima[0].Min;
            Uv = previsao.Clima[0].Indice_uv;
            Data = previsao.Clima[0].Data;


            proxPrevisao = await new PrevisaoServices().GetPrevisaoForXDaysById(244, 3);
            ProximosDias = proxPrevisao.Clima;
           
            
       

        }
    }
}
