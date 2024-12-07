using ClimaTempo.Models;
using ClimaTempo.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.PlatformConfiguration;
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
        private string min;

        [ObservableProperty]
        private string max;

        [ObservableProperty]
        private double uv;

        [ObservableProperty]
        private List<Clima> proximosDias;

        private Previsao previsao;
        private Previsao proxPrevisao;

        public ICommand BuscarPrevisaoCommand { get; }

       // Dados da Pesquisa
        [ObservableProperty]
        private string cidade_pesquisada;

        //Dados da Lista de Cidade
        [ObservableProperty]
        private List<Cidade> cidade_list;
         
        public ICommand BuscarCidadesCommand { get; }
       


        public PrevisaoViewModel()
        {
            BuscarPrevisaoCommand = new Command<int>(BuscarPrevisao);
            BuscarCidadesCommand = new Command(BuscarCidades);  
        }

        public async  void BuscarPrevisao(int id)
        {
            previsao = await new PrevisaoServices().GetPrevisaoById(id);
            max = previsao.Clima[0].Max.ToString();
            min = previsao.Clima[0].Min.ToString();

        }

        public async void BuscarCidades()
        {
            Cidade_list = new List<Cidade>();
            Cidade_list = await new CidadeService().GetCidadesByName(Cidade_pesquisada);
        }
    }
}

